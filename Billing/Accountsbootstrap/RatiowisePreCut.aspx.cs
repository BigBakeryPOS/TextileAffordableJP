﻿using System;
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
using DocumentFormat.OpenXml.Wordprocessing;

namespace Billing.Accountsbootstrap
{
    public partial class RatiowisePreCut : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iid;
        DataSet dmerge1 = new DataSet();
        DataSet dsnneeww = new DataSet();
        string empid = "";
        double Q30F = 0; double Q32F = 0; double Q34F = 0; double Q36F = 0;
        double QXSF = 0; double QSF = 0; double QMF = 0; double QLF = 0;
        double QXLF = 0; double QXXLF = 0; double Q3XLF = 0; double Q4XLF = 0;

        double Q30H = 0; double Q32H = 0; double Q34H = 0; double Q36H = 0;
        double QXSH = 0; double QSH = 0; double QMH = 0; double QLH = 0;
        double QXLH = 0; double QXXLH = 0; double Q3XLH = 0; double Q4XLH = 0;
        double QttlFH = 0;
        DataSet dsttlcal = new DataSet();
        DataTable dCrt;
        string CmpyId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            iid = Request.QueryString.Get("CuttingID");

            CmpyId = Session["CmpyId"].ToString();

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            empid = Session["Empid"].ToString();
            if (!IsPostBack)
            {

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
                        //drpbranch.Items.Insert(0, "Select Branch");
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
                        //drpbranch.SelectedValue = Session["cmpyid"].ToString();
                        //drpbranch_OnSelectedIndexChanged(sender, e);
                        //company_SelectedIndexChnaged(sender, e);
                    }
                    drpbranch_OnSelectedIndexChanged(sender, e);
                    //company_SelectedIndexChnaged(sender, e);
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


                DataSet dsEmp = objBs.GetEmployeeDetails(lblEmployee.Text);
                if (dsEmp.Tables[0].Rows.Count > 0)
                {
                    drpcutting.DataSource = dsEmp.Tables[0];
                    drpcutting.DataTextField = "Name";
                    drpcutting.DataValueField = "Employee_Id";
                    drpcutting.DataBind();
                }


                DataSet ds = objBs.gridProcess();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdmaster.DataSource = ds;
                    grdmaster.DataBind();
                }
                else
                {
                    grdmaster.DataSource = null;
                    grdmaster.DataBind();
                }

                // if (CmpyId == "4")
                {
                    //////DataSet dbbc = objBs.GetCompanyDetforboth(drpbranch.SelectedValue);
                    //////if (dbbc.Tables[0].Rows.Count > 0)
                    //////{
                    //////    ddlBottilot.DataSource = dbbc.Tables[0];
                    //////    ddlBottilot.DataTextField = "lotNo";
                    //////    ddlBottilot.DataValueField = "BCID";
                    //////    ddlBottilot.DataBind();
                    //////    //ddlBottilot.Items.Insert(0, "Select LotNo");
                    //////    //  ddlBottilot.Items.Insert(0, "LotNo");
                    //////    // ddlBottilot.SelectedIndex = 0;
                    //////}
                    //////else
                    //////{
                    //////    // ddlBottilot.Items.Insert(0, "LotNo");
                    //////    // ddlBottilot.SelectedIndex = 0;
                    //////}
                }
                //else 
                //{
                //    ddlBottilot.Items.Insert(0, "Select LotNo");
                //    ddlBottilot.SelectedIndex = 0;
                //}




                if (drpbranch.SelectedValue == "3") //RPL
                {
                    //rdbinnertype.Enabled = true;
                    // ddlBottilot.Enabled = false;
                    rdncore.Enabled = false;
                }
                else
                {
                    // rdbinnertype.Enabled = false;
                    // ddlBottilot.Enabled = true;
                    rdncore.Enabled = true;
                }


                radchecked(sender, e);
                Shirttype(sender, e);
                //sTableName = Session["User"].ToString();
                DataTable dt = new DataTable();
                divcode.Visible = false;

                dt.Columns.Add("Transid");
                dt.Columns.Add("Design");
                dt.Columns.Add("Rate");
                dt.Columns.Add("meter");
                dt.Columns.Add("Shirt");
                dt.Columns.Add("Reqmeter");
                dt.Columns.Add("Reqshirt");
                dt.Columns.Add("ledgerid");
                dt.Columns.Add("party");
                dt.Columns.Add("Fitid");
                dt.Columns.Add("Fit");

                dt.Columns.Add("S30FS");
                dt.Columns.Add("S30HS");

                dt.Columns.Add("S32FS");
                dt.Columns.Add("S32HS");

                dt.Columns.Add("S34FS");
                dt.Columns.Add("S34HS");
                dt.Columns.Add("S36FS");
                dt.Columns.Add("S36HS");
                dt.Columns.Add("SXSFS");
                dt.Columns.Add("SXSHS");
                dt.Columns.Add("SLFS");
                dt.Columns.Add("SLHS");
                dt.Columns.Add("SXLFS");
                dt.Columns.Add("SXLHS");
                dt.Columns.Add("SXXLFS");
                dt.Columns.Add("SXXLHS");
                dt.Columns.Add("S3XLFS");
                dt.Columns.Add("S3XLHS");
                dt.Columns.Add("S4XLFS");
                dt.Columns.Add("S4XLHS");
                dt.Columns.Add("SSFS");
                dt.Columns.Add("SSHS");
                dt.Columns.Add("SMFS");
                dt.Columns.Add("SMHS");
                dt.Columns.Add("Itemname");
                dt.Columns.Add("Pattern");
                dt.Columns.Add("PatternName");

                dt.Columns.Add("WSP");
                dt.Columns.Add("avgsize");
                dt.Columns.Add("Extra");
                dt.Columns.Add("LLedger");
                dt.Columns.Add("Mainlab");
                dt.Columns.Add("FItLab");
                dt.Columns.Add("Washlab");
                dt.Columns.Add("Logolab");
                dt.Columns.Add("Total");

                dt.Columns.Add("Contrast");

                ViewState["Data"] = dt;



                S30fs.Visible = false; S30hs.Visible = false;

                S32fs.Visible = false; S32hs.Visible = false;

                S34fs.Visible = false; S34hs.Visible = false;

                S36fs.Visible = false; S36hs.Visible = false;

                Xsfs.Visible = false; Xshs.Visible = false;

                sfs.Visible = false; shs.Visible = false;

                mfs.Visible = false; mhs.Visible = false;
                lfs.Visible = false; lhs.Visible = false;
                xlfs.Visible = false; xlhs.Visible = false;
                xxlfs.Visible = false; xxlhs.Visible = false;
                xxxlfs.Visible = false; xxxlhs.Visible = false;
                xxxxlfs.Visible = false; xxxxlhs.Visible = false;


                //DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                //if (dst != null)
                //{
                //    //if (dst.Tables[0].Rows.Count > 0)
                //    //{
                //    //    ddlSupplier.DataSource = dst.Tables[0];
                //    //    ddlSupplier.DataTextField = "LedgerName";
                //    //    ddlSupplier.DataValueField = "LedgerID";
                //    //    ddlSupplier.DataBind();
                //    //    ddlSupplier.Items.Insert(0, "Select Party Name");

                //    //    //chkSupplier.DataSource = dst.Tables[0];
                //    //    //chkSupplier.DataTextField = "LedgerName";
                //    //    //chkSupplier.DataValueField = "LedgerID";
                //    //    //chkSupplier.DataBind();
                //    //    // ddlSupplier.Items.Insert(0, "Select Party Name");
                //    //}
                //}

                //DataSet dsDNo = objBs.GetDNo();
                //if (dsDNo != null)
                //{
                //    if (dsDNo.Tables[0].Rows.Count > 0)
                //    {
                //        ddlDNo.DataSource = dsDNo.Tables[0];
                //        ddlDNo.DataTextField = "Dno";
                //        ddlDNo.DataValueField = "ProcessID";
                //        ddlDNo.DataBind();
                //        ddlDNo.Items.Insert(0, "Select Design");
                //    }
                //}



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
                        ddlFit.DataSource = dsFit.Tables[0];
                        ddlFit.DataTextField = "Fit";
                        ddlFit.DataValueField = "FitID";
                        ddlFit.DataBind();
                        ddlFit.Items.Insert(0, "Select Fit");


                    }
                }

                DataSet dpattern = objBs.Patternmaster();
                if (dpattern != null)
                {
                    if (dpattern.Tables[0].Rows.Count > 0)
                    {
                        drppattern.DataSource = dpattern.Tables[0];
                        drppattern.DataTextField = "PatternName";
                        drppattern.DataValueField = "PatternId";
                        drppattern.DataBind();
                        //drppattern.Items.Insert(0, "Select Pattern");
                    }

                }



                DataSet dsize = objBs.Getsizetype();
                if (dsize != null)
                {
                    if (dsize.Tables[0].Rows.Count > 0)
                    {
                        chkSizes.DataSource = dsize.Tables[0];
                        chkSizes.DataTextField = "Size";
                        chkSizes.DataValueField = "Sizeid";
                        chkSizes.DataBind();
                        //chkSizes.SelectedValue = "1";
                        //chkSizes.SelectedValue = "2";

                        //chkSizes.SelectedValue = "3";
                        //chkSizes.SelectedValue = "4";

                        //chkSizes.SelectedValue = "7";
                        //chkSizes.SelectedValue = "8";

                        //chkSizes.SelectedValue = "9";
                        //chkSizes.SelectedValue = "10";
                        //  chkSizes.Items.Insert(0, "Select Customer");
                        // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();
                    }
                }

                //if ((dsize.Tables[0].Rows.Count > 0))
                //{
                //    //Select the checkboxlist items those values are true in database
                //    //Loop through the DataTable
                //    for (int i = 0; i <= dsize.Tables[0].Rows.Count - 1; i++)
                //    {
                //        //You need to change this as per your DB Design
                //        string size = dsize.Tables[0].Rows[i]["Size"].ToString();
                //        //if (size == "39FS" || size == "39HS" || size == "44FS" || size == "44HS")
                //        //{
                //        //}
                //        //else
                //        {
                //            //Find the checkbox list items using FindByValue and select it.
                //            chkSizes.Items.FindByValue(dsize.Tables[0].Rows[i]["Sizeid"].ToString()).Selected = true;
                //        }

                //    }
                //}

                //foreach (DataRow row in dsize.Tables[0].Rows["Size"])
                //{
                //    if (row["Size"] == "36FS")
                //    {
                //        chkSizes.SelectedValue = "1";
                //        //
                //        //found
                //    }
                //    if (row["Size"] == "36HS")
                //    {
                //        chkSizes.SelectedValue = "2";
                //        //found
                //    }
                //}

                //if (chkSizes.SelectedItem.Text == "36FS")
                //{
                //    chkSizes.SelectedValue = "1";
                //}

                DataSet dswidth = objBs.GetWidth();
                if (dswidth != null)
                {
                    if (dswidth.Tables[0].Rows.Count > 0)
                    {
                        drpwidth.DataSource = dswidth.Tables[0];
                        drpwidth.DataTextField = "Width";
                        drpwidth.DataValueField = "WidthID";
                        drpwidth.DataBind();


                        contrastwidth.DataSource = dswidth.Tables[0];
                        contrastwidth.DataTextField = "Width";
                        contrastwidth.DataValueField = "WidthID";
                        contrastwidth.DataBind();

                        // drpwidth.Items.Insert(0, "Select Width");
                    }
                }


                DataSet dsrefno = objBs.getnewsupplierforcutNEW(drpwidth.SelectedValue);
                if (dsrefno != null)
                {
                    if (dsrefno.Tables[0].Rows.Count > 0)
                    {
                        chkinvno.DataSource = dsrefno.Tables[0];
                        chkinvno.DataTextField = "invoiceno";
                        chkinvno.DataValueField = "pogrnid";
                        chkinvno.DataBind();
                        //  drpwidth.Items.Insert(0, "Select Width");
                    }
                }

                //DataSet dcheckwidth = objBs.getwidthnewprocess(drpwidth.SelectedItem.Text);
                //if (dcheckwidth.Tables[0].Rows.Count > 0)
                //{
                //    for (int i = 0; i < dcheckwidth.Tables[0].Rows.Count; i++)
                //    {
                //        string size = dcheckwidth.Tables[0].Rows[i]["Sizeid"].ToString();
                //        string value = dcheckwidth.Tables[0].Rows[i]["w"].ToString();
                //        if (size == "3")
                //        {
                //            txtsharp.Text = value;
                //        }
                //        else
                //        {
                //            txtexec.Text = value;
                //        }

                //    }
                //}
                string date = DateTime.Now.ToString("dd/MM/yyyy");

                // txtdate.Text = date;

                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();


                if (iid != null)
                {
                    btnadd.Text = "Go Back";
                    DataSet ds1 = objBs.getprecuttingDetailsTab1(Convert.ToInt32(iid));
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            txtID.Text = ds1.Tables[0].Rows[0]["cutid"].ToString();
                            drpbranch.SelectedValue = ds1.Tables[0].Rows[0]["Companyid"].ToString();
                            drpnewtype.SelectedValue = ds1.Tables[0].Rows[0]["worktypeid"].ToString();

                            if (drpnewtype.SelectedValue == "1")
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

                            //Want to Bind cutting/jobworker name

                            drpcutting.SelectedValue = ds1.Tables[0].Rows[0]["cuttingmaster"].ToString();
                            rdncore.SelectedValue = "2";
                            lblcompany.Text = "CFLEX";
                            txtcompanylot.Text = ds1.Tables[0].Rows[0]["companylotno"].ToString();
                            drpitemtype.SelectedValue = ds1.Tables[0].Rows[0]["itemid"].ToString();
                            lblitemlotcode.Text = ds1.Tables[0].Rows[0]["itemcode"].ToString();
                            txtitemlotno.Text = ds1.Tables[0].Rows[0]["itemlotno"].ToString();
                            txtLotNo.Text = ds1.Tables[0].Rows[0]["lotno"].ToString();

                            txtdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["deliverydate"]).ToString("dd/MM/yyyy");
                            txtdeliverydate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["deldate"]).ToString("dd/MM/yyyy");

                            drpwidth.SelectedValue = ds1.Tables[0].Rows[0]["width"].ToString();
                            txtavgmeter.Text = ds1.Tables[0].Rows[0]["avgmeter"].ToString();
                            txtrolltaka.Text = ds1.Tables[0].Rows[0]["rolltaka"].ToString();
                            ddlbrand.SelectedValue = ds1.Tables[0].Rows[0]["brandid"].ToString();
                            drpNchkfit.SelectedValue = ds1.Tables[0].Rows[0]["fit"].ToString();
                            drpnewsleevetype.SelectedValue = ds1.Tables[0].Rows[0]["sleevetype"].ToString();
                            drpnewlabeltype.SelectedValue = ds1.Tables[0].Rows[0]["labeltype"].ToString();
                            ddlcompletestitching.SelectedValue = ds1.Tables[0].Rows[0]["completestitching"].ToString();

                            txtnarration.Text = ds1.Tables[0].Rows[0]["Narration"].ToString();



                            // Want to Load Fabric Invoice number later and Fab With Color
                            DataSet dsnewrefno = objBs.getnewsupplierforcutupdate(drpwidth.SelectedValue);
                            if (dsnewrefno != null)
                            {
                                if (dsnewrefno.Tables[0].Rows.Count > 0)
                                {
                                    chkinvno.DataSource = dsnewrefno.Tables[0];
                                    chkinvno.DataTextField = "fabno";
                                    chkinvno.DataValueField = "fabid";
                                    chkinvno.DataBind();
                                    //  drpwidth.Items.Insert(0, "Select Width");
                                }
                            }

                            DataSet getfabricdetails = objBs.getprecuttingfabricDetailsTab1(iid, "B");

                            if (getfabricdetails.Tables[0].Rows.Count > 0)
                            {

                                for (int i = 0; i <= getfabricdetails.Tables[0].Rows.Count - 1; i++)
                                {
                                    //Find the checkbox list items using FindByValue and select it.
                                    chkinvno.Items.FindByValue(getfabricdetails.Tables[0].Rows[i]["fabid"].ToString()).Selected = true;
                                }
                            }


                            // BIND FABRIC COLOR

                            DataSet dssmer = new DataSet();
                            DataSet dteo = new DataSet();
                            string cond = "";
                            string cond1 = "";
                            //  dteo = objBs.getjobcardlistdesign(CheckBoxList1.SelectedValue);



                            if (chkinvno.SelectedIndex >= 0)
                            {
                                if (radcuttype.SelectedValue == "2")
                                {

                                    foreach (System.Web.UI.WebControls.ListItem listItem in chkinvno.Items)
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
                                            Ntxtremmeter.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                                            //  txtavamet1.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                                        }
                                    }
                                    else
                                    {
                                        txtAvailableMtr.Text = "0";
                                        txtReqMtr.Text = "0";
                                        txtavamet1.Text = "0";
                                        Ntxtremmeter.Text = "0";
                                    }

                                    reqchanged(sender, e);

                                    foreach (System.Web.UI.WebControls.ListItem item in chkinvno.Items)
                                    {
                                        //check if item selected
                                        if (item.Selected)
                                        {
                                            // Add participant to the selected list if not alreay added
                                            //if (!IsParticipantExists(item.Value))
                                            //{

                                            //}
                                            //else
                                            {
                                                dteo = objBs.getcutlistdesignupdate(item.Value, drpwidth.SelectedValue);
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
                                            newgridfablist.DataSource = dssmer;
                                            newgridfablist.DataBind();
                                        }
                                        else
                                        {
                                            newgridfablist.DataSource = null;
                                            newgridfablist.DataBind();
                                        }

                                    }
                                    else
                                    {
                                        newgridfablist.DataSource = null;
                                        newgridfablist.DataBind();
                                    }


                                    for (int vLoop1 = 0; vLoop1 < newgridfablist.Rows.Count; vLoop1++)
                                    {
                                        System.Web.UI.WebControls.CheckBox chkitemchecked = (System.Web.UI.WebControls.CheckBox)newgridfablist.Rows[vLoop1].FindControl("chkitemchecked");

                                        Label newfabid = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabid");
                                        DataSet getfabricdetailss = objBs.getprecuttingfabricDetailsTab1(iid, "B");

                                        if (getfabricdetailss.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < getfabricdetailss.Tables[0].Rows.Count; i++)
                                            {
                                                string transfabid = getfabricdetailss.Tables[0].Rows[i]["transfabid"].ToString();
                                                string gettotalmeter = Convert.ToDouble(getfabricdetailss.Tables[0].Rows[i]["reqmeter"]).ToString("0.00");
                                                if (transfabid == newfabid.Text)
                                                {
                                                    chkitemchecked.Checked = true;
                                                    Label newfabcode = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabcode");
                                                    TextBox reqqmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtreqmeter");
                                                    System.Web.UI.WebControls.CheckBox dckitem = (System.Web.UI.WebControls.CheckBox)newgridfablist.Rows[vLoop1].FindControl("chkitemchecked");

                                                    reqqmeter.Text = gettotalmeter;
                                                }
                                            }
                                        }
                                    }

                                }

                            }
                            else
                            {

                                dddldesign.DataSource = null;
                                dddldesign.DataBind();
                                dddldesign.ClearSelection();
                                dddldesign.Items.Clear();
                                txtAvailableMtr.Text = "0";
                                txtReqMtr.Text = "0";
                                txtavamet1.Text = "0";
                                Ntxtremmeter.Text = "0";
                                // chkinvno.Enabled = false;

                            }


                            // GETTING ALL PROCESS

                            DataSet getprocessdetails = objBs.getprecuttingprocessdetailsTab1(iid);
                            if (getprocessdetails.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < getprocessdetails.Tables[0].Rows.Count; j++)
                                {
                                    string screen = getprocessdetails.Tables[0].Rows[j]["Screen"].ToString();
                                    string jobworktype = getprocessdetails.Tables[0].Rows[j]["jobwork"].ToString();

                                    if (screen == "Stc")
                                    {
                                        Nchkstch.SelectedValue = jobworktype;
                                    }
                                    else if (screen == "Emb")
                                    {
                                        Nchkemb.SelectedValue = jobworktype;
                                    }
                                    else if (screen == "Kaja")
                                    {
                                        Nchkkbut.SelectedValue = jobworktype;
                                    }
                                    else if (screen == "Wash")
                                    {
                                        Nchkwash.SelectedValue = jobworktype;
                                    }
                                    else if (screen == "stc")
                                    {
                                        Nchkprint.SelectedValue = jobworktype;
                                    }
                                    else if (screen == "Iron")
                                    {
                                        Nchkiron.SelectedValue = jobworktype;
                                    }
                                    else if (screen == "Btag")
                                    {
                                        Nchkbartag.SelectedValue = jobworktype;
                                    }
                                    else if (screen == "Trm")
                                    {
                                        Nchktrimming.SelectedValue = jobworktype;
                                    }
                                    else if (screen == "Cni")
                                    {
                                        Nchkconsai.SelectedValue = jobworktype;
                                    }


                                }
                            }

                            // FILL RATIO GRID PROCESS
                            //DataSet dgetprocessratio = objBs.getprecuttingratiowisedetailsTab2(iid);
                            //if (dgetprocessratio.Tables[0].Rows.Count > 0)
                            //{
                            //    // Want to Process Ratio Fabric List
                            newfabclick(sender, e);

                            // GETTING CONTRAST LIST
                            DataSet getcontrastlist = objBs.getprecuttingfabricDetailsTab4forcontrast(iid, "C");
                            if (getcontrastlist.Tables[0].Rows.Count > 0)
                            {

                                contrastgridfab.DataSource = getcontrastlist.Tables[0];
                                contrastgridfab.DataBind();

                                for (int i = 0; i < getcontrastlist.Tables[0].Rows.Count; i++)
                                {
                                    string transfabid = getcontrastlist.Tables[0].Rows[i]["id"].ToString();
                                    string Designno = getcontrastlist.Tables[0].Rows[i]["design"].ToString();
                                    string shirttype = getcontrastlist.Tables[0].Rows[i]["type"].ToString();
                                    string Avaliablemeter = getcontrastlist.Tables[0].Rows[i]["Avaliablemeter"].ToString();
                                    string Reqmeter = getcontrastlist.Tables[0].Rows[i]["Reqmeter"].ToString();
                                    string AvgMeter = getcontrastlist.Tables[0].Rows[i]["AvgMeter"].ToString();



                                    for (int vLoop1 = 0; vLoop1 < contrastgridfab.Rows.Count; vLoop1++)
                                    {
                                        Label newfabid = (Label)contrastgridfab.Rows[vLoop1].FindControl("newfabid");

                                        TextBox reqqmeter = (TextBox)contrastgridfab.Rows[vLoop1].FindControl("newtxtreqmeter");
                                        TextBox avgmeter = (TextBox)contrastgridfab.Rows[vLoop1].FindControl("txtavgmetercontast");

                                        if (newfabid.Text == transfabid)
                                        {

                                            reqqmeter.Text = Reqmeter;
                                            avgmeter.Text = AvgMeter;
                                        }

                                    }
                                }
                            }
                            //}



                        }

                        //if (ds1.Tables[0].Rows.Count > 0)
                        //{
                        //    DataSet dsDNo1 = objBs.allGetDNo();
                        //    if (dsDNo1 != null)
                        //    {
                        //        if (dsDNo1.Tables[0].Rows.Count > 0)
                        //        {
                        //            ddlDNo.DataSource = dsDNo1.Tables[0];
                        //            ddlDNo.DataTextField = "Dno";
                        //            ddlDNo.DataValueField = "ProcessID";
                        //            ddlDNo.DataBind();
                        //            ddlDNo.Items.Insert(0, "Select Design");
                        //        }
                        //    }

                        //    btnadd.Text = "Update";
                        //    double totmeter = Convert.ToDouble(ds1.Tables[0].Rows[0]["Req_Meter"]) + Convert.ToDouble(ds1.Tables[0].Rows[0]["met"]);
                        //    txtID.Text = ds1.Tables[0].Rows[0]["CuttingID"].ToString();
                        //    TextBox3.Text = ds1.Tables[0].Rows[0]["CuttingID"].ToString();
                        //    txtreq_meter.Text = ds1.Tables[0].Rows[0]["Req_Meter"].ToString();
                        //    ddlDNo.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["DNo"]).ToString();
                        //    txtLotNo.Text = ds1.Tables[0].Rows[0]["LotNo"].ToString();
                        //    txtMeter.Text = totmeter.ToString();
                        //    txtRate.Text = ds1.Tables[0].Rows[0]["Rate"].ToString();
                        //    txtColor.Text = ds1.Tables[0].Rows[0]["Color"].ToString();
                        //    radbtn.SelectedValue = ds1.Tables[0].Rows[0]["Type"].ToString();
                        //    if (radbtn.SelectedValue == "1")
                        //    {
                        //        ddlSupplier.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["PartyName"]).ToString();
                        //        // single.Visible = true;
                        //        // multiple.Visible = false;
                        //    }
                        //    else
                        //    {
                        //        //  single.Visible = false;
                        //        //  multiple.Visible = true;
                        //        string str = ds1.Tables[0].Rows[0]["PartyName"].ToString();
                        //        string[] strList = str.Split(',');


                        //        //foreach (string s in strList)
                        //        //{
                        //        //    foreach (ListItem item in chkSupplier.Items)
                        //        //    {
                        //        //        if (item.Value == s)
                        //        //        {
                        //        //            item.Selected = true;
                        //        //            break;
                        //        //        }

                        //        //    }

                        //        //}

                        //    }
                        //    txtWidth.Text = ds1.Tables[0].Rows[0]["WidthID"].ToString();
                        //    ddlFit.SelectedValue = ds1.Tables[0].Rows[0]["Fit"].ToString();
                        //    txtdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");
                        //}
                    }
                }
                else
                {
                    txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtdeliverydate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //DataSet ds = objBs.CuttingID();
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    if (ds.Tables[0].Rows[0]["CuttingID"].ToString() == "")
                    //        TextBox3.Text = "1";
                    //    else
                    //        TextBox3.Text = ds.Tables[0].Rows[0]["CuttingID"].ToString();
                    DataSet dss = objBs.getmaaxBillnoforcut("P");
                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        txtLotNo.Text = dss.Tables[0].Rows[0]["billId"].ToString();
                    }
                    btnadd.Text = "Save";
                    btnadd.Enabled = false;
                    btnprocess.Enabled = false;
                    radchecked(sender, e);
                    //  //  FirstGridViewRow();
                    //}

                }
            }
        }

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");

                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

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

                DataSet getmaxitemlot = objBs.getmaxlotno(drpitemtype.SelectedValue);
                if (getmaxitemlot.Tables[0].Rows.Count > 0)
                {
                    DataSet getitemcode = objBs.getitemvalue(drpitemtype.SelectedValue);
                    if (getitemcode.Tables[0].Rows.Count > 0)
                    {
                        lblitemlotcode.Text = getitemcode.Tables[0].Rows[0]["Itemcode"].ToString();
                        txtavgmeter.Text = Convert.ToDouble(getitemcode.Tables[0].Rows[0]["AvgGms"]).ToString("0.000");
                        string itemlotno = getmaxitemlot.Tables[0].Rows[0]["itemlotno"].ToString();

                        txtitemnarration.Text = getitemcode.Tables[0].Rows[0]["Narrations"].ToString();

                        txtitemlotno.Text = String.Format("{0:0000}", Convert.ToInt32(itemlotno)); ;
                        txtitemlotno.Focus();
                    }
                }

            }

        }

        protected void rdncore_changed(object sender, EventArgs e)
        {
            if (rdncore.SelectedValue == "1")
            {
                txtcompanysublot.Enabled = true;
                DataSet DSGETSUBTYPE = objBs.GETSUBTYPE(txtcompanylot.Text);
                if (DSGETSUBTYPE.Tables[0].Rows.Count > 0)
                {
                    txtcompanysublot.Text = DSGETSUBTYPE.Tables[0].Rows[0]["CompanySubLotNo"].ToString();
                }
            }
            else
            {
                txtcompanysublot.Enabled = false;
                txtcompanysublot.Text = "0";
            }

        }


        protected void drpbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you!!');", true);
                return;
            }
            else
            {
                DataSet dbbc = objBs.GetCompanyDetforboth(drpbranch.SelectedValue);
                if (dbbc.Tables[0].Rows.Count > 0)
                {
                    ddlBottilot.DataSource = dbbc.Tables[0];
                    ddlBottilot.DataTextField = "lotNo";
                    ddlBottilot.DataValueField = "BCID";
                    ddlBottilot.DataBind();

                }
                else
                {

                }

            }
        }
        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {

            if (drpcutting.SelectedValue == "Select Cutting Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Cutting Name. Thank you!!');", true);
                return;
            }
            if (drpcutting.SelectedValue == "Select Job Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Job Worker Name. Thank you!!');", true);
                return;
            }

            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {




                if (drpcutting.SelectedValue != "")
                {

                    DataSet getcompanylotno = new DataSet();
                    getcompanylotno = objBs.getcompanyno(drpbranch.SelectedValue, drpcutting.SelectedValue);
                    //////if (drpsubtype.SelectedValue == "")
                    //////{
                    //////    drpcutting.SelectedIndex = 0;
                    //////    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Type. Thank you!!');", true);
                    //////    return;
                    //////}
                    //////else if (drpsubtype.SelectedValue == "1")
                    //////{
                    //////    //New
                    //////    getcompanylotno = objBs.getcompanyno(drpbranch.SelectedValue, drpcutting.SelectedValue);
                    //////}
                    //////else if (drpsubtype.SelectedValue == "2")
                    //////{
                    //////    //Exiting
                    //////    getcompanylotno = objBs.getcompanynobew(drpbranch.SelectedValue, drpcutting.SelectedValue);
                    //////}

                    if (getcompanylotno.Tables[0].Rows.Count > 0)
                    {
                        string companyno = getcompanylotno.Tables[0].Rows[0]["companylotno"].ToString();
                        DataSet getledger = objBs.getledgerdet(Convert.ToInt32(drpcutting.SelectedValue));
                        if (getledger.Tables[0].Rows.Count > 0)
                        {
                            string prefix = getledger.Tables[0].Rows[0]["Prefix"].ToString();
                            if (drpbranch.SelectedValue == "3")
                            {
                                txtcompanysublot.Enabled = false;
                                //lblcompany.Text = "RPL" + '-' + prefix;
                                lblcompany.Text = "CFLEX";
                                txtcompanylot.Text = String.Format("{0:0000}", Convert.ToInt32(companyno)); ;

                                //////if (drpsubtype.SelectedValue == "1")
                                //////{
                                //////    txtcompanysublot.Text = "A";
                                //////}
                                //////else
                                //////{
                                //////    DataSet DSGETSUBTYPE = objBs.GETSUBTYPE(companyno);
                                //////    if (DSGETSUBTYPE.Tables[0].Rows[0]["CompanySubLotNo"].ToString() == "")
                                //////    {
                                //////        txtcompanysublot.Text = "A";
                                //////    }
                                //////    else
                                //////    {
                                //////        txtcompanysublot.Text = DSGETSUBTYPE.Tables[0].Rows[0]["CompanySubLotNo"].ToString();
                                //////    }
                                //////}
                            }
                            else
                            {
                                txtcompanysublot.Enabled = true;
                                // lblcompany.Text = "BC" + '-' + prefix;
                                lblcompany.Text = "CFLEX";
                                txtcompanylot.Text = String.Format("{0:0000}", Convert.ToInt32(companyno)); ;

                                //////if (drpsubtype.SelectedValue == "1")
                                //////{
                                //////    txtcompanysublot.Text = "A";
                                //////}
                                //////else
                                //////{
                                //////    DataSet DSGETSUBTYPE = objBs.GETSUBTYPE(companyno);
                                //////    if (DSGETSUBTYPE.Tables[0].Rows.Count > 0)
                                //////    // if (DSGETSUBTYPE.Tables[0].Rows[0]["CompanySubLotNo"].ToString() == "")
                                //////    {
                                //////        txtcompanysublot.Text = DSGETSUBTYPE.Tables[0].Rows[0]["CompanySubLotNo"].ToString();

                                //////    }
                                //////    else
                                //////    {

                                //////    }
                                //////}
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Something Went Wrong Please Contact Administrator. Thank you!!');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Something Went Wrong Please Contact Administrator. Thank you!!');", true);
                        return;
                    }
                }

            }

            rdncore_changed(sender, e);
        }

        protected void drpsubtype_changed(object sender, EventArgs e)
        {
            //if (drpcutting.SelectedValue == "Select Cutting Name")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Cutting Name. Thank you!!');", true);
            //    return;
            //}

            if (drpcutting.SelectedValue == "")
            {

            }
            else
            {
                drpcutting.SelectedIndex = 0;
            }
            txtcompanysublot.Text = "";
            txtcompanylot.Text = "";
        }

        protected void Companylotchecked(object sender, EventArgs e)
        {


            if (drpcutting.SelectedValue == "Select Cutting Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Cutting Name. Thank you!!');", true);
                return;
            }
            if (drpcutting.SelectedValue == "Select Job Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Job Worker Name. Thank you!!');", true);
                return;
            }

            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {

                DataSet dcl = new DataSet();
                if (drpbranch.SelectedValue == "3")
                {
                    dcl = objBs.checkcompanylotno(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue);
                }
                else
                {
                    if (rdncore.SelectedValue == "1")
                    {
                        dcl = objBs.checkcompanylotnonew(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue, txtcompanysublot.Text);
                    }
                    else
                    {
                        dcl = objBs.checkcompanylotno(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue);
                    }

                }

                if (dcl.Tables[0].Rows.Count > 0)
                {
                    txtcompanylot.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Cutting No.Already Exists. Thank you');", true);
                    return;
                }
                else
                {
                    if (drpbranch.SelectedValue == "3")
                    {
                        lblcompany.Text = "CFLEX";
                        //  txtcompanylot.Text = companyno;
                    }
                    else
                    {
                        lblcompany.Text = "CFLEX";
                        //  txtcompanylot.Text = companyno;

                    }
                }


            }


        }

        protected void type_changed(object sender, EventArgs e)
        {

            #region

            if (drpnewtype.SelectedValue == "1")
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

            #endregion


            //#region

            //if (drpnewtype.SelectedValue == "1")
            //{
            //    DataSet dst = objBs.Getcuttmastrr();
            //    if (dst != null)
            //    {
            //        if (dst.Tables[0].Rows.Count > 0)
            //        {
            //            drpcutting.DataSource = dst.Tables[0];
            //            drpcutting.DataTextField = "LedgerName";
            //            drpcutting.DataValueField = "LedgerID";
            //            drpcutting.DataBind();
            //            drpcutting.Items.Insert(0, "Select Cutting Name");
            //        }
            //    }
            //}
            //else
            //{
            //    DataSet dst = objBs.Getjobworkmastrr();
            //    if (dst != null)
            //    {
            //        if (dst.Tables[0].Rows.Count > 0)
            //        {
            //            drpcutting.DataSource = dst.Tables[0];
            //            drpcutting.DataTextField = "LedgerName";
            //            drpcutting.DataValueField = "LedgerID";
            //            drpcutting.DataBind();
            //            drpcutting.Items.Insert(0, "Select Job Name");
            //        }
            //    }
            //}

            //#endregion

        }

        private static string incCol(string col)
        {
            if (col == "") return "A";
            string fPart = col.Substring(0, col.Length - 1);
            char lChar = col[col.Length - 1];
            if (lChar == 'Z') return incCol(fPart) + "A";
            return fPart + ++lChar;


        }


        protected void Shirttype(object sender, EventArgs e)
        {
            if (radbtnshirttype.SelectedValue == "1")
            {
                Newbtnclick.Visible = true;
                metertab.Visible = true;
                tr1.Visible = true;
                btngohead.Visible = true;
                Panel1.Visible = true;
                contrastpart.Visible = true;
            }
            //else if (radbtnshirttype.SelectedValue == "2")
            //{
            //    contrastpart.Visible = true;
            //    Newbtnclick.Visible = false;
            //    metertab.Visible = false;
            //    tr1.Visible = true;
            //    btngohead.Visible = false;
            //    Panel1.Visible = false;
            //    headinglabel.Text = "CONTRAST TYPE";

            //}
            //else if (radbtnshirttype.SelectedValue == "3")
            //{
            //    headinglabel.Text = "REVERSE TYPE";
            //    contrastpart.Visible = true;
            //    Newbtnclick.Visible = false;
            //    metertab.Visible = false;
            //    tr1.Visible = true;
            //    btngohead.Visible = false;
            //    Panel1.Visible = false;

            //}


        }
        protected void ckhsize_index(object sender, EventArgs e)
        {
            //DataSet dssmer = new DataSet();
            //DataSet dteo = new DataSet();
            //if (radbtn.SelectedValue == "1")
            //{
            //    if (chkSizes.SelectedIndex >= 0)
            //    {
            //        tsfs.Visible = false; tshs.Visible = false;

            //        tefs.Visible = false; tehs.Visible = false;

            //        tnfs.Visible = false; tnhs.Visible = false;

            //        fzfs.Visible = false; fzhs.Visible = false;

            //        ftfs.Visible = false; fths.Visible = false;

            //        fffs.Visible = false; ffhs.Visible = false;

            //        int lop = 0;
            //        //Loop through each item of checkboxlist
            //        foreach (ListItem item in chkSizes.Items)
            //        {
            //            //check if item selected

            //            if (item.Selected)
            //            {

            //                {
            //                    if (item.Text == "36FS")
            //                    {
            //                        tsfs.Visible = true;
            //                    }
            //                    if (item.Text == "36HS")
            //                    {
            //                        //gridsize.Columns[8].Visible = true;
            //                        tshs.Visible = true;
            //                    }
            //                    if (item.Text == "38FS")
            //                    {
            //                        //    gridsize.Columns[3].Visible = true;
            //                        tefs.Visible = true;
            //                    }
            //                    if (item.Text == "38HS")
            //                    {
            //                        //gridsize.Columns[9].Visible = true;
            //                        tehs.Visible = true;
            //                    }
            //                    if (item.Text == "39FS")
            //                    {
            //                        // gridsize.Columns[4].Visible = true;
            //                        tnfs.Visible = true;
            //                    }
            //                    if (item.Text == "39HS")
            //                    {
            //                        // gridsize.Columns[10].Visible = true;
            //                        tnhs.Visible = true;
            //                    }
            //                    if (item.Text == "40FS")
            //                    {
            //                        //gridsize.Columns[5].Visible = true;
            //                        fzfs.Visible = true;
            //                    }
            //                    if (item.Text == "40HS")
            //                    {
            //                        // gridsize.Columns[11].Visible = true;
            //                        fzhs.Visible = true;
            //                    }
            //                    if (item.Text == "42FS")
            //                    {
            //                        //  gridsize.Columns[6].Visible = true;
            //                        ftfs.Visible = true;
            //                    }
            //                    if (item.Text == "42HS")
            //                    {
            //                        // gridsize.Columns[12].Visible = true;
            //                        fths.Visible = true;
            //                    }
            //                    if (item.Text == "44FS")
            //                    {
            //                        // gridsize.Columns[7].Visible = true;
            //                        fffs.Visible = true;
            //                    }
            //                    if (item.Text == "44HS")
            //                    {
            //                        // gridsize.Columns[13].Visible = true;

            //                        ffhs.Visible = true;
            //                    }


            //                    lop++;

            //                }
            //            }
            //        }

            //    }
            //    else
            //    {
            //        tsfs.Visible = false; tshs.Visible = false;

            //        tefs.Visible = false; tehs.Visible = false;

            //        tnfs.Visible = false; tnhs.Visible = false;

            //        fzfs.Visible = false; fzhs.Visible = false;

            //        ftfs.Visible = false; fths.Visible = false;

            //        fffs.Visible = false; ffhs.Visible = false;
            //    }
            //}

            //else
            //{

            //    if (chkSizes.SelectedIndex >= 0)
            //    {
            //        gridsize.Columns[2].Visible = false; //36FS
            //        gridsize.Columns[3].Visible = false; //38FS

            //        gridsize.Columns[4].Visible = false;//39Fs
            //        gridsize.Columns[5].Visible = false;//40Fs

            //        gridsize.Columns[6].Visible = false; //42FS
            //        gridsize.Columns[7].Visible = false; //44FS

            //        gridsize.Columns[8].Visible = false; //36HS
            //        gridsize.Columns[9].Visible = false; //38HS

            //        gridsize.Columns[10].Visible = false; //39HS
            //        gridsize.Columns[11].Visible = false; //40HS

            //        gridsize.Columns[12].Visible = false; //42HS
            //        gridsize.Columns[13].Visible = false; //44HS



            //        int lop = 0;
            //        //Loop through each item of checkboxlist
            //        foreach (ListItem item in chkSizes.Items)
            //        {
            //            //check if item selected

            //            if (item.Selected)
            //            {

            //                {
            //                    if (item.Text == "36FS")
            //                    {
            //                        gridsize.Columns[2].Visible = true;
            //                    }
            //                    if (item.Text == "36HS")
            //                    {
            //                        gridsize.Columns[8].Visible = true;
            //                    }
            //                    if (item.Value == "38FS")
            //                    {
            //                        gridsize.Columns[3].Visible = true;
            //                    }
            //                    if (item.Value == "38HS")
            //                    {
            //                        gridsize.Columns[9].Visible = true;
            //                    }
            //                    if (item.Value == "39FS")
            //                    {
            //                        gridsize.Columns[4].Visible = true;
            //                    }
            //                    if (item.Value == "39HS")
            //                    {
            //                        gridsize.Columns[10].Visible = true;
            //                    }
            //                    if (item.Value == "40FS")
            //                    {
            //                        gridsize.Columns[5].Visible = true;
            //                    }
            //                    if (item.Value == "40HS")
            //                    {
            //                        gridsize.Columns[11].Visible = true;
            //                    }
            //                    if (item.Value == "42FS")
            //                    {
            //                        gridsize.Columns[6].Visible = true;
            //                    }
            //                    if (item.Value == "42HS")
            //                    {
            //                        gridsize.Columns[12].Visible = true;
            //                    }
            //                    if (item.Value == "44FS")
            //                    {
            //                        gridsize.Columns[7].Visible = true;
            //                    }
            //                    if (item.Value == "44HS")
            //                    {
            //                        gridsize.Columns[13].Visible = true;
            //                    }


            //                    lop++;

            //                }
            //            }
            //        }
            //        //gvcustomerorder.DataSource = dssmer;
            //        //gvcustomerorder.DataBind();
            //    }
            //    else
            //    {
            //        gridsize.Columns[2].Visible = false;
            //        gridsize.Columns[3].Visible = false;

            //        gridsize.Columns[4].Visible = false;
            //        gridsize.Columns[5].Visible = false;

            //        gridsize.Columns[6].Visible = false; gridsize.Columns[7].Visible = false;

            //        gridsize.Columns[8].Visible = false; gridsize.Columns[9].Visible = false;

            //        gridsize.Columns[10].Visible = false; gridsize.Columns[11].Visible = false;

            //        gridsize.Columns[12].Visible = false; gridsize.Columns[13].Visible = false;


            //    }
            //}
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

        protected void ddlBottilot_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            //contrastwidth.SelectedItem = "";
            chkinvno.SelectedValue = null;
            drpwidth.SelectedValue = null;

            if (ddlBottilot.SelectedValue != "LotNo")
            {


                DataSet ds = objBs.Getbcfabdeat(Convert.ToInt32(ddlBottilot.SelectedValue));

                drpbranch.SelectedValue = ds.Tables[0].Rows[0]["Branch"].ToString();
                drpnewtype.SelectedValue = ds.Tables[0].Rows[0]["TypeOfWorker"].ToString();

                type_changed(sender, e);

                drpcutting.SelectedValue = ds.Tables[0].Rows[0]["Worker"].ToString();
                lblcompany.Text = ds.Tables[0].Rows[0]["CuttingNo"].ToString();
                txtcompanylot.Text = ds.Tables[0].Rows[0]["LotNo"].ToString();
                txtdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["IssueDate"]).ToString("dd/MM/yyyy");
                txtdeliverydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DeliverDate"]).ToString("dd/MM/yyyy");
                drpwidth.SelectedValue = ds.Tables[0].Rows[0]["With"].ToString();
                txtrolltaka.Text = ds.Tables[0].Rows[0]["RollTaka"].ToString();

                drpwidthChange(sender, e);
                // ddlbrand.SelectedValue = ds.Tables[0].Rows[0]["BrandName"].ToString();

                drpbranch.Enabled = false;
                drpnewtype.Enabled = false;
                drpcutting.Enabled = false;
                lblcompany.Enabled = false;
                // txtcompanylot.Enabled = false;
                //txtdate.Enabled = false;
                //  txtdeliverydate.Enabled = false;
                drpwidth.Enabled = false;
                chkinvno.Enabled = false;

                Nchkemb.SelectedValue = ds.Tables[0].Rows[0]["EmbroidingIO"].ToString();
                Nchkstch.SelectedValue = ds.Tables[0].Rows[0]["StichingIO"].ToString();
                Nchkkbut.SelectedValue = ds.Tables[0].Rows[0]["KButtonIO"].ToString();
                Nchkwash.SelectedValue = ds.Tables[0].Rows[0]["WashingIO"].ToString();
                Nchkprint.SelectedValue = ds.Tables[0].Rows[0]["PrintingIO"].ToString();
                Nchkiron.SelectedValue = ds.Tables[0].Rows[0]["IroningIO"].ToString();
                Nchkbartag.SelectedValue = ds.Tables[0].Rows[0]["BarTagIO"].ToString();
                Nchktrimming.SelectedValue = ds.Tables[0].Rows[0]["TrimmingIO"].ToString();
                Nchkconsai.SelectedValue = ds.Tables[0].Rows[0]["ConsaiIO"].ToString();





                DataSet ds1 = new DataSet();
                DataSet dsmer = new DataSet();

                foreach (System.Web.UI.WebControls.ListItem item in ddlBottilot.Items)
                {

                    if (item.Selected)
                    {

                        if (!IsParticipantExists(item.Value))
                        {

                            dsmer = objBs.Getbcmainall(Convert.ToInt32(item.Value));

                            if (dsmer != null)
                            {
                                if (dsmer.Tables[0].Rows.Count > 0)
                                {
                                    ds1.Merge(dsmer);
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                }

                //////DataSet ds1 = objBs.Getbcmain(Convert.ToInt32(ddlBottilot.SelectedValue));

                newgridfablist.DataSource = ds1.Tables[0];
                newgridfablist.DataBind();

                double mtr = 0;

                for (int vLoop1 = 0; vLoop1 < newgridfablist.Rows.Count; vLoop1++)
                {
                    Label newfabid = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabid");
                    Label newfabcode = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabcode");
                    Label lbltype = (Label)newgridfablist.Rows[vLoop1].FindControl("lbltype");

                    TextBox newtxtAvlmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtAvlmeter");
                    TextBox reqqmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtreqmeter");

                    newfabid.Text = ds1.Tables[0].Rows[vLoop1]["Id"].ToString();
                    newfabcode.Text = ds1.Tables[0].Rows[vLoop1]["Design"].ToString();
                    lbltype.Text = ds1.Tables[0].Rows[vLoop1]["type"].ToString();

                    newtxtAvlmeter.Text = Convert.ToDouble(ds1.Tables[0].Rows[vLoop1]["AvaliableMeter"]).ToString("f2");
                    reqqmeter.Text = Convert.ToDouble(ds1.Tables[0].Rows[vLoop1]["AvaliableMeter"]).ToString("f2");

                    mtr = mtr + Convert.ToDouble(ds1.Tables[0].Rows[vLoop1]["AvaliableMeter"].ToString());
                }

                txtAvailableMtr.Text = mtr.ToString("f2");
                txtReqMtr.Text = mtr.ToString("f2");
                Ntxtremmeter.Text = mtr.ToString("f2");

                txtReqNoShirts.Text = "Infinity";
                txtNoofShirts.Text = "Infinity";

                company_SelectedIndexChnaged(sender, e);

            }
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

            if (btnadd.Text == "Go Back")
            {
                Response.Redirect("viewcutting.aspx");
            }

            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you!!');", true);
                return;
            }
            if (ddlcompletestitching.SelectedValue == "Yes")
            {
                if (Nchkstch.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Process (Stiching Must ).Thank you!!!');", true);
                    return;
                }
            }
            else
            {
                if (Nchkiron.SelectedValue == "" || Nchkstch.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Process (Ironing & Stiching Must ).Thank you!!!');", true);
                    return;
                }
            }

            double totshirt = 0;
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


            DateTime deliverydate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime deldate = DateTime.ParseExact(txtdeliverydate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (ddlsample.SelectedValue == "Sample")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Sample. Thank you!!');", true);
                return;
            }

            for (int vLoop = 0; vLoop < newgridfablist.Rows.Count; vLoop++)
            {

                Label newfabcode = (Label)newgridfablist.Rows[vLoop].FindControl("newfabcode");
                Label lblinvdate = (Label)newgridfablist.Rows[vLoop].FindControl("lblinvdate");

                // DateTime lblinvdatee = DateTime.ParseExact(lblinvdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (Convert.ToDateTime(lblinvdate.Text) > (deliverydate))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Fabric Invoice Date And Cutting Issue date MisMatch.Please Check this Fabric " + newfabcode.Text + ".Thank you!!!');", true);
                    return;

                }
                else
                {

                }
            }

            for (int vLoop = 0; vLoop < contrastgridfab.Rows.Count; vLoop++)
            {

                Label newfabcode = (Label)contrastgridfab.Rows[vLoop].FindControl("newfabcode");
                Label lblinvdate = (Label)contrastgridfab.Rows[vLoop].FindControl("lblinvdate");



                if (Convert.ToDateTime(lblinvdate.Text) > deliverydate)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Contrast Fabric Invoice Date And Cutting Issue date MisMatch.Please Check this Fabric " + newfabcode.Text + ".Thank you!!!');", true);
                    return;

                }
                else
                {

                }
            }

            if (btnadd.Text == "Save")
            {
                #region
                if (radcuttype.SelectedValue == "1")
                {
                    #region

                    if (drpcutting.SelectedValue == "Select Cutting Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Cutting Name. Thank you!!');", true);
                        return;
                    }
                    else
                    {

                    }

                    //   int istas = objBs.updatesizesettingg(drpwidth.SelectedItem.Text, txtsharp.Text, txtexec.Text, "");


                    dCrt = (DataTable)ViewState["Data"];
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dCrt);

                    int iStatus = 0;
                    int BCLot = 0;
                    string BCValadd = "";

                    if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
                    {
                        BCLot = 0;
                    }
                    else
                    {
                        BCLot = Convert.ToInt32(ddlBottilot.SelectedValue);



                        foreach (System.Web.UI.WebControls.ListItem item in ddlBottilot.Items)
                        {


                            if (item.Selected)
                            {
                                string BCVal = "";

                                if (!IsParticipantExists(item.Value))
                                {

                                    BCVal = (item.Value).ToString();

                                    BCValadd = BCValadd + "," + BCVal;

                                }
                                else
                                {

                                }
                            }
                        }



                        DataSet dcl = new DataSet();
                        if (rdncore.SelectedValue == "1")
                        {

                            dcl = objBs.checkcompanylotnonew(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue, txtcompanysublot.Text);
                        }
                        else
                        {

                            dcl = objBs.checkcompanylotno(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue);
                        }

                        if (dcl.Tables[0].Rows.Count > 0)
                        {
                            txtcompanylot.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Cutting No.Already Exists. Thank you');", true);
                            return;
                        }
                    }


                    iStatus = objBs.insertcutnewonenew(txtLotNo.Text, deliverydate, drpwidth.SelectedValue, drpcutting.SelectedValue, txtprod.Text, "0", "0", radbtn.SelectedValue, txtadjmeter.Text, lblmin.Text, lblmax.Text, radcuttype.SelectedValue, ddlbrand.SelectedValue, "0", deldate, drpnewtype.SelectedValue, empid, lblcompany.Text, txtcompanylot.Text, drpbranch.SelectedValue, BCLot, 0, "1", txtrolltaka.Text, txtcompanysublot.Text, ddlsample.SelectedItem.Text, txtcontrasts.Text, txtavgmeter.Text, ddlcompletestitching.SelectedValue, Convert.ToInt32(rdncore.SelectedValue), BCValadd, txtnarration.Text, drpnewsleevetype.SelectedValue, drpnewlabeltype.SelectedValue, drpitemtype.SelectedValue, lblitemlotcode.Text, txtitemlotno.Text, txtitemnarration.Text);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        //dr["Transid"] = dddldesign.SelectedValue;dr["Design"] = dddldesign.SelectedItem.Text; dr["Rate"] = txtDesignRate.Text;

                        //dr["meter"] = txtAvailableMtr.Text;dr["Shirt"] = txtNoofShirts.Text;dr["reqmeter"] = txtavamet2.Text; dr["reqshirt"] = txttotshirt2.Text;
                        //dr["ledgerid"] = drpCustomer2.SelectedValue; dr["party"] = drpCustomer2.SelectedItem.Text; dr["Fitid"] = drpFit2.SelectedValue;
                        //dr["Fit"] = drpFit2.SelectedItem.Text;dr["TSFS"] = txt36FS2.Text;dr["TSHS"] = txt36HS2.Text; dr["TEFS"] = txt38FS2.Text;
                        //dr["TEHS"] = txt38HS2.Text;dr["FZFS"] = txt40FS2.Text; dr["FZHS"] = txt38HS2.Text; dr["FTFS"] = txt42FS2.Text;
                        //dr["FTHS"] = txt38HS2.Text; dr["LLedger"] = ledgerr; dr["Mainlab"] = mainlab; dr["FItLab"] = fitlab;dr["Washlab"] = washlab; dr["Logolab"] = logolab;
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
                            string itemname = ds.Tables[0].Rows[i]["Itemname"].ToString();
                            string patternid = ds.Tables[0].Rows[i]["Pattern"].ToString();

                            string s30fs = ds.Tables[0].Rows[i]["S30FS"].ToString();
                            string s30hs = ds.Tables[0].Rows[i]["S30HS"].ToString();

                            string s32fs = ds.Tables[0].Rows[i]["S32FS"].ToString();
                            string s32hs = ds.Tables[0].Rows[i]["S32HS"].ToString();

                            string s34fs = ds.Tables[0].Rows[i]["S34FS"].ToString();
                            string s34hs = ds.Tables[0].Rows[i]["S34HS"].ToString();

                            string s36fs = ds.Tables[0].Rows[i]["S36FS"].ToString();
                            string s36hs = ds.Tables[0].Rows[i]["S36HS"].ToString();

                            string sXSfs = ds.Tables[0].Rows[i]["SXSFS"].ToString();
                            string sXShs = ds.Tables[0].Rows[i]["SXSHS"].ToString();

                            string sSfs = ds.Tables[0].Rows[i]["SSFS"].ToString();
                            string sShs = ds.Tables[0].Rows[i]["SSHS"].ToString();

                            string sMfs = ds.Tables[0].Rows[i]["SMFS"].ToString();
                            string sMhs = ds.Tables[0].Rows[i]["SMHS"].ToString();

                            string sLfs = ds.Tables[0].Rows[i]["SLFS"].ToString();
                            string sLhs = ds.Tables[0].Rows[i]["SLHS"].ToString();

                            string sXLfs = ds.Tables[0].Rows[i]["SXLFS"].ToString();
                            string sXLhs = ds.Tables[0].Rows[i]["SXLHS"].ToString();

                            string sXXLfs = ds.Tables[0].Rows[i]["SXXLFS"].ToString();
                            string sXXLhs = ds.Tables[0].Rows[i]["SXXLHS"].ToString();

                            string s3XLfs = ds.Tables[0].Rows[i]["S3XLFS"].ToString();
                            string s3XLhs = ds.Tables[0].Rows[i]["S3XLHS"].ToString();

                            string s4XLfs = ds.Tables[0].Rows[i]["S4XLFS"].ToString();
                            string s4XLhs = ds.Tables[0].Rows[i]["S4XLHS"].ToString();




                            string wsp = ds.Tables[0].Rows[i]["WSP"].ToString();

                            string Contrast = ds.Tables[0].Rows[i]["Contrast"].ToString();

                            string avgsize = ds.Tables[0].Rows[i]["avgsize"].ToString();
                            string extra = ds.Tables[0].Rows[i]["Extra"].ToString();
                            totshirt = totshirt + Convert.ToInt32(shirt);

                            if (radbtn.SelectedValue == "1")
                            {
                                ledgerr = ddlSupplier.SelectedValue;
                                mainlab = drplab.SelectedValue;
                                fitlab = chkfit.Checked;
                                washlab = Chkwash.Checked;
                                logolab = Chllogo.Checked;
                                maar = Stxtmargin.Text;
                                mmrrpp = Stxtwsp.Text;
                                ddffiitt = drpFit.SelectedValue;
                                //if (Stxtmargin.Text == "0" || Stxtmargin.Text == "")
                                //{
                                //    mmrrpp = "0";
                                //    maar = "10";

                                //}
                                //else
                                //{

                                //    mmrrpp = Stxtmargin.Text;
                                //    maar = "0";
                                //}
                            }
                            else
                            {
                                //for (int vLoop = 0; vLoop < grdcust.Rows.Count; vLoop++)
                                //{


                                //    TextBox txtno = (TextBox)grdcust.Rows[vLoop].FindControl("txtcust");

                                //    if (partyname == txtno.Text)
                                //    {
                                //        //   ledgerr = drpparty.SelectedValue;
                                //        DropDownList drpparty = (DropDownList)grdcust.Rows[vLoop].FindControl("drrplab");
                                //        CheckBox fitll = (CheckBox)grdcust.Rows[vLoop].FindControl("Mchkfit");
                                //        CheckBox wasll = (CheckBox)grdcust.Rows[vLoop].FindControl("Mchkwash");
                                //        CheckBox logll = (CheckBox)grdcust.Rows[vLoop].FindControl("Mchklogo");
                                //        TextBox txtmargins = (TextBox)grdcust.Rows[vLoop].FindControl("txtmargin");
                                //        DropDownList ddrpfit = (DropDownList)grdcust.Rows[vLoop].FindControl("ddrrpfit");
                                //        //if (txtmmrrp.Text == "0" || txtmmrrp.Text == "")
                                //        //{
                                //        //    mmrrpp = "0";
                                //        //    maar = "10";
                                //        //}
                                //        //else
                                //        //{

                                //        //    mmrrpp = txtmmrrp.Text;
                                //        //    maar = "0";
                                //        //}


                                //        ledgerr = party;
                                //        mainlab = drpparty.SelectedValue;
                                //        fitlab = fitll.Checked;
                                //        washlab = wasll.Checked;
                                //        logolab = logll.Checked;
                                //        maar = txtmargins.Text;
                                //        mmrrpp = wsp;
                                //        ddffiitt = ddrpfit.SelectedValue;

                                //    }


                                //}
                            }

                            foreach (System.Web.UI.WebControls.ListItem listItem in chkinvno.Items)
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

                            DataSet dummy = new DataSet();

                            //  int iStatus2 = objBs.insertTranscutnewone(txtLotNo.Text, "0", trainid, design, party, totmeter, reqmeter, rate, tsfs, tshs, tefs, tehs, tnfs, tnhs, fzfs, fzhs, ftfs, fths, fffs, ffhs, shirt, reqshirt, fitid, ledgerr, mainlab, fitlab, washlab, logolab, maar, mmrrpp, extra, ddffiitt, avgsize, radcuttype.SelectedValue, cond1, con, ddlbrand.SelectedValue, dummy);
                            int iStatus2 = objBs.insertTranscutnewone(txtLotNo.Text, "0", trainid, design, party, totmeter, reqmeter, rate, s30fs, s30hs, s32fs, s32hs, s34fs, s34hs, s36fs, s36hs, sXSfs, sXShs, sSfs, sShs, sMfs, sMhs, sLfs, sLhs, sXLfs, sXLhs, sXXLfs, sXXLhs, s3XLfs, s3XLhs, s4XLfs, s4XLhs, shirt, reqshirt, fitid, ledgerr, mainlab, fitlab, washlab, logolab, maar, mmrrpp, extra, ddffiitt, avgsize, radcuttype.SelectedValue, cond1, con, ddlbrand.SelectedValue, dummy, itemname, patternid, Contrast);

                        }
                    }

                    #endregion
                }
                else if (radcuttype.SelectedValue == "2")
                {

                    dCrt = (DataTable)ViewState["Data"];
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dCrt);


                    int row_count = dCrt.Rows.Count;

                    if (row_count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Something Went Wrong Please Check it Again while saving this Pre-Cutting Details. Thank you!!');", true);
                        return;
                    }

                    #region

                    if (drpcutting.SelectedValue == "Select Cutting Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Cutting Name. Thank you!!');", true);
                        return;
                    }
                    if (drpcutting.SelectedValue == "Select Job Name")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Job Worker Name. Thank you!!');", true);
                        return;
                    }
                    else
                    {

                    }

                    #region PROCESS NEW

                    int count = 0;

                    string nIN = string.Empty;
                    string nOUT = string.Empty;
                    string Overallp = string.Empty;


                    DataSet ndstt = new DataSet();


                    DataTable ndttt = new DataTable();

                    DataColumn ndc = new DataColumn("ScreenName");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("LotNo");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("Type");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("TotalQty");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("FullQty");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("HalfQty");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("RemainQty");
                    ndttt.Columns.Add(ndc);


                    ndstt.Tables.Add(ndttt);

                    //if (ddlAgainst.SelectedValue != "0" && txtchequedd.Text != "" && txtAgainstAmount.Text != "0")




                    //Embroy

                    if (Nchkemb.SelectedIndex >= 0)
                    {
                        string ein = "0";
                        string eout = "0";

                        count = count + 1;
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["ScreenName"] = "Emb";
                        ndrd["LotNo"] = txtLotNo.Text;
                        ndrd["Type"] = Nchkemb.SelectedValue;
                        ndrd["TotalQty"] = totshirt;
                        ndrd["FullQty"] = "0";
                        ndrd["HalfQty"] = "0";
                        ndrd["RemainQty"] = totshirt;
                        ndstt.Tables[0].Rows.Add(ndrd);
                    }

                    //Stiching

                    if (Nchkstch.SelectedIndex >= 0)
                    {
                        string sin = "0";
                        string sout = "0";

                        count = count + 1;
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["ScreenName"] = "Stc";
                        ndrd["LotNo"] = txtLotNo.Text;
                        ndrd["Type"] = Nchkstch.SelectedValue;
                        ndrd["TotalQty"] = totshirt;
                        ndrd["FullQty"] = "0";
                        ndrd["HalfQty"] = "0";
                        ndrd["RemainQty"] = totshirt;
                        ndstt.Tables[0].Rows.Add(ndrd);
                    }


                    // KAJA BUTTON

                    if (Nchkkbut.SelectedIndex >= 0)
                    {
                        string kin = "0";
                        string kout = "0";


                        count = count + 1;
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["ScreenName"] = "Kaja";
                        ndrd["LotNo"] = txtLotNo.Text;
                        ndrd["Type"] = Nchkkbut.SelectedValue;
                        ndrd["TotalQty"] = totshirt;
                        ndrd["FullQty"] = "0";
                        ndrd["HalfQty"] = "0";
                        ndrd["RemainQty"] = totshirt;
                        ndstt.Tables[0].Rows.Add(ndrd);


                    }


                    // WASHING

                    if (Nchkwash.SelectedIndex >= 0)
                    {
                        string win = "0";
                        string wout = "0";

                        count = count + 1;
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["ScreenName"] = "Wash";
                        ndrd["LotNo"] = txtLotNo.Text;
                        ndrd["Type"] = Nchkwash.SelectedValue;
                        ndrd["TotalQty"] = totshirt;
                        ndrd["FullQty"] = "0";
                        ndrd["HalfQty"] = "0";
                        ndrd["RemainQty"] = totshirt;
                        ndstt.Tables[0].Rows.Add(ndrd);
                    }

                    //Printing

                    if (Nchkprint.SelectedIndex >= 0)
                    {
                        string Pin = "0";
                        string pout = "0";

                        count = count + 1;
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["ScreenName"] = "Print";
                        ndrd["LotNo"] = txtLotNo.Text;
                        ndrd["Type"] = Nchkprint.SelectedValue;
                        ndrd["TotalQty"] = totshirt;
                        ndrd["FullQty"] = "0";
                        ndrd["HalfQty"] = "0";
                        ndrd["RemainQty"] = totshirt;
                        ndstt.Tables[0].Rows.Add(ndrd);
                    }

                    //Ironing

                    if (Nchkiron.SelectedIndex >= 0)
                    {
                        string Iin = "0";
                        string Iout = "0";

                        count = count + 1;
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["ScreenName"] = "Iron";
                        ndrd["LotNo"] = txtLotNo.Text;
                        ndrd["Type"] = Nchkiron.SelectedValue;
                        ndrd["TotalQty"] = totshirt;
                        ndrd["FullQty"] = "0";
                        ndrd["HalfQty"] = "0";
                        ndrd["RemainQty"] = totshirt;
                        ndstt.Tables[0].Rows.Add(ndrd);
                    }

                    // BAR TAG
                    if (Nchkbartag.SelectedIndex >= 0)
                    {
                        string Iin = "0";
                        string Iout = "0";

                        count = count + 1;
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["ScreenName"] = "Btag";
                        ndrd["LotNo"] = txtLotNo.Text;
                        ndrd["Type"] = Nchkbartag.SelectedValue;
                        ndrd["TotalQty"] = totshirt;
                        ndrd["FullQty"] = "0";
                        ndrd["HalfQty"] = "0";
                        ndrd["RemainQty"] = totshirt;
                        ndstt.Tables[0].Rows.Add(ndrd);
                    }

                    // TRIMMING
                    if (Nchktrimming.SelectedIndex >= 0)
                    {
                        string Iin = "0";
                        string Iout = "0";

                        count = count + 1;
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["ScreenName"] = "Trm";
                        ndrd["LotNo"] = txtLotNo.Text;
                        ndrd["Type"] = Nchktrimming.SelectedValue;
                        ndrd["TotalQty"] = totshirt;
                        ndrd["FullQty"] = "0";
                        ndrd["HalfQty"] = "0";
                        ndrd["RemainQty"] = totshirt;
                        ndstt.Tables[0].Rows.Add(ndrd);
                    }

                    // CONSAI
                    if (Nchkconsai.SelectedIndex >= 0)
                    {
                        string Iin = "0";
                        string Iout = "0";

                        count = count + 1;
                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["ScreenName"] = "Cni";
                        ndrd["LotNo"] = txtLotNo.Text;
                        ndrd["Type"] = Nchkconsai.SelectedValue;
                        ndrd["TotalQty"] = totshirt;
                        ndrd["FullQty"] = "0";
                        ndrd["HalfQty"] = "0";
                        ndrd["RemainQty"] = totshirt;
                        ndstt.Tables[0].Rows.Add(ndrd);
                    }

                    if (ndstt.Tables[0].Rows.Count > 0)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Process Name. Thank you!!');", true);
                        return;
                    }


                    #endregion

                    //    int istas = objBs.updatesizesettingg(drpwidth.SelectedItem.Text, txtsharp.Text, txtexec.Text, "");



                    DataSet dstt = new DataSet();

                    int iStatus = 0;
                    int BCLot = 0;
                    string BCValadd = "";

                    if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
                    {
                        BCLot = 0;
                    }
                    else
                    {
                        BCLot = Convert.ToInt32(ddlBottilot.SelectedValue);
                        //  DataSet dcl = objBs.checkcompanylotnonew(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue, txtcompanysublot.Text);



                        foreach (System.Web.UI.WebControls.ListItem item in ddlBottilot.Items)
                        {


                            if (item.Selected)
                            {
                                string BCVal = "";

                                if (!IsParticipantExists(item.Value))
                                {

                                    BCVal = (item.Value).ToString();

                                    BCValadd = BCValadd + "," + BCVal;

                                }
                                else
                                {

                                }
                            }
                        }

                        BCLot = Convert.ToInt32(ddlBottilot.SelectedValue);
                        DataSet dcl = new DataSet();
                        if (rdncore.SelectedValue == "1")
                        {

                            dcl = objBs.checkcompanylotnonew(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue, txtcompanysublot.Text);
                        }
                        else
                        {

                            dcl = objBs.checkcompanylotno(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue);
                        }

                        if (dcl.Tables[0].Rows.Count > 0)
                        {
                            txtcompanylot.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Cutting No.Already Exists. Thank you');", true);
                            return;
                        }

                        if (dcl.Tables[0].Rows.Count > 0)
                        {
                            txtcompanylot.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Cutting No.Already Exists. Thank you');", true);
                            return;
                        }
                    }

                    iStatus = objBs.insertcutnewonenew(txtLotNo.Text, deliverydate, drpwidth.SelectedValue, drpcutting.SelectedValue, txtprod.Text, "0", "0", radbtn.SelectedValue, txtadjmeter.Text, lblmin.Text, lblmax.Text, radcuttype.SelectedValue, ddlbrand.SelectedValue, drpNchkfit.SelectedValue, deldate, drpnewtype.SelectedValue, empid, lblcompany.Text, txtcompanylot.Text, drpbranch.SelectedValue, BCLot, 0, "1", txtrolltaka.Text, txtcompanysublot.Text, ddlsample.SelectedItem.Text, txtcontrasts.Text, txtavgmeter.Text, ddlcompletestitching.SelectedValue, Convert.ToInt32(rdncore.SelectedValue), BCValadd, txtnarration.Text, drpnewsleevetype.SelectedValue, drpnewlabeltype.SelectedValue, drpitemtype.SelectedValue, lblitemlotcode.Text, txtitemlotno.Text, txtitemnarration.Text);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        //dr["Transid"] = dddldesign.SelectedValue;dr["Design"] = dddldesign.SelectedItem.Text; dr["Rate"] = txtDesignRate.Text;

                        //dr["meter"] = txtAvailableMtr.Text;dr["Shirt"] = txtNoofShirts.Text;dr["reqmeter"] = txtavamet2.Text; dr["reqshirt"] = txttotshirt2.Text;
                        //dr["ledgerid"] = drpCustomer2.SelectedValue; dr["party"] = drpCustomer2.SelectedItem.Text; dr["Fitid"] = drpFit2.SelectedValue;
                        //dr["Fit"] = drpFit2.SelectedItem.Text;dr["TSFS"] = txt36FS2.Text;dr["TSHS"] = txt36HS2.Text; dr["TEFS"] = txt38FS2.Text;
                        //dr["TEHS"] = txt38HS2.Text;dr["FZFS"] = txt40FS2.Text; dr["FZHS"] = txt38HS2.Text; dr["FTFS"] = txt42FS2.Text;
                        //dr["FTHS"] = txt38HS2.Text; dr["LLedger"] = ledgerr; dr["Mainlab"] = mainlab; dr["FItLab"] = fitlab;dr["Washlab"] = washlab; dr["Logolab"] = logolab;
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
                            string itemname = ds.Tables[0].Rows[i]["Itemname"].ToString();
                            string patternid = ds.Tables[0].Rows[i]["PAttern"].ToString();

                            string s30fs = ds.Tables[0].Rows[i]["S30FS"].ToString();
                            string s30hs = ds.Tables[0].Rows[i]["S30HS"].ToString();

                            string s32fs = ds.Tables[0].Rows[i]["S32FS"].ToString();
                            string s32hs = ds.Tables[0].Rows[i]["S32HS"].ToString();

                            string s34fs = ds.Tables[0].Rows[i]["S34FS"].ToString();
                            string s34hs = ds.Tables[0].Rows[i]["S34HS"].ToString();

                            string s36fs = ds.Tables[0].Rows[i]["S36FS"].ToString();
                            string s36hs = ds.Tables[0].Rows[i]["S36HS"].ToString();

                            string sXSfs = ds.Tables[0].Rows[i]["SXSFS"].ToString();
                            string sXShs = ds.Tables[0].Rows[i]["SXSHS"].ToString();

                            string sSfs = ds.Tables[0].Rows[i]["SSFS"].ToString();
                            string sShs = ds.Tables[0].Rows[i]["SSHS"].ToString();

                            string sMfs = ds.Tables[0].Rows[i]["SMFS"].ToString();
                            string sMhs = ds.Tables[0].Rows[i]["SMHS"].ToString();

                            string sLfs = ds.Tables[0].Rows[i]["SLFS"].ToString();
                            string sLhs = ds.Tables[0].Rows[i]["SLHS"].ToString();

                            string sXLfs = ds.Tables[0].Rows[i]["SXLFS"].ToString();
                            string sXLhs = ds.Tables[0].Rows[i]["SXLHS"].ToString();

                            string sXXLfs = ds.Tables[0].Rows[i]["SXXLFS"].ToString();
                            string sXXLhs = ds.Tables[0].Rows[i]["SXXLHS"].ToString();

                            string s3XLfs = ds.Tables[0].Rows[i]["S3XLFS"].ToString();
                            string s3XLhs = ds.Tables[0].Rows[i]["S3XLHS"].ToString();

                            string s4XLfs = ds.Tables[0].Rows[i]["S4XLFS"].ToString();
                            string s4XLhs = ds.Tables[0].Rows[i]["S4XLHS"].ToString();

                            string wsp = ds.Tables[0].Rows[i]["WSP"].ToString();

                            string Contrast = ds.Tables[0].Rows[i]["Contrast"].ToString();

                            string avgsize = ds.Tables[0].Rows[i]["avgsize"].ToString();
                            lblBodyAvgmeter.Text = avgsize;
                            string extra = ds.Tables[0].Rows[i]["Extra"].ToString();

                            if (radbtn.SelectedValue == "1")
                            {
                                ledgerr = ddlSupplier.SelectedValue;
                                mainlab = drplab.SelectedValue;
                                fitlab = chkfit.Checked;
                                washlab = Chkwash.Checked;
                                logolab = Chllogo.Checked;
                                maar = Stxtmargin.Text;
                                mmrrpp = Stxtwsp.Text;
                                ddffiitt = drpFit.SelectedValue;
                                //if (Stxtmargin.Text == "0" || Stxtmargin.Text == "")
                                //{
                                //    mmrrpp = "0";
                                //    maar = "10";

                                //}
                                //else
                                //{

                                //    mmrrpp = Stxtmargin.Text;
                                //    maar = "0";
                                //}
                            }
                            else
                            {
                                //for (int vLoop = 0; vLoop < grdcust.Rows.Count; vLoop++)
                                //{


                                //    TextBox txtno = (TextBox)grdcust.Rows[vLoop].FindControl("txtcust");

                                //    if (partyname == txtno.Text)
                                //    {
                                //        //   ledgerr = drpparty.SelectedValue;
                                //        DropDownList drpparty = (DropDownList)grdcust.Rows[vLoop].FindControl("drrplab");
                                //        CheckBox fitll = (CheckBox)grdcust.Rows[vLoop].FindControl("Mchkfit");
                                //        CheckBox wasll = (CheckBox)grdcust.Rows[vLoop].FindControl("Mchkwash");
                                //        CheckBox logll = (CheckBox)grdcust.Rows[vLoop].FindControl("Mchklogo");
                                //        TextBox txtmargins = (TextBox)grdcust.Rows[vLoop].FindControl("txtmargin");
                                //        DropDownList ddrpfit = (DropDownList)grdcust.Rows[vLoop].FindControl("ddrrpfit");
                                //        //if (txtmmrrp.Text == "0" || txtmmrrp.Text == "")
                                //        //{
                                //        //    mmrrpp = "0";
                                //        //    maar = "10";
                                //        //}
                                //        //else
                                //        //{

                                //        //    mmrrpp = txtmmrrp.Text;
                                //        //    maar = "0";
                                //        //}


                                //        ledgerr = party;
                                //        mainlab = drpparty.SelectedValue;
                                //        fitlab = fitll.Checked;
                                //        washlab = wasll.Checked;
                                //        logolab = logll.Checked;
                                //        maar = txtmargins.Text;
                                //        mmrrpp = wsp;
                                //        ddffiitt = ddrpfit.SelectedValue;

                                //    }


                                //}
                            }

                            foreach (System.Web.UI.WebControls.ListItem listItem in chkinvno.Items)
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






                            int iStatus2 = objBs.insertTranscutnewone(txtLotNo.Text, "0", trainid, design, party, totmeter, reqmeter, rate, s30fs, s30hs, s32fs, s32hs, s34fs, s34hs, s36fs, s36hs, sXSfs, sXShs, sSfs, sShs, sMfs, sMhs, sLfs, sLhs, sXLfs, sXLhs, sXXLfs, sXXLhs, s3XLfs, s3XLhs, s4XLfs, s4XLhs, shirt, reqshirt, fitid, ledgerr, mainlab, fitlab, washlab, logolab, maar, mmrrpp, extra, ddffiitt, avgsize, radcuttype.SelectedValue, cond1, con, ddlbrand.SelectedValue, dstt, itemname, patternid, Contrast);

                        }
                    }

                    //New Update for BCPre
                    //////if (ddlBottilot.SelectedValue != "Select LotNo")
                    //////{
                    //////    for (int vLoop1 = 0; vLoop1 < newgridfablist.Rows.Count; vLoop1++)
                    //////    {
                    //////        CheckBox chkitemchecked = (CheckBox)newgridfablist.Rows[vLoop1].FindControl("chkitemchecked");

                    //////        Label newfabid = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabid");
                    //////        Label newfabcode = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabcode");
                    //////        TextBox reqqmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtreqmeter");

                    //////        TextBox newtxtAvlmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtAvlmeter");
                    //////        if (chkitemchecked.Checked == true)
                    //////        {
                    //////            int IFABPRObc = objBs.BCCutFabforFinal(Convert.ToInt32(ddlBottilot.SelectedValue), newfabid.Text, newfabcode.Text, Convert.ToDouble(reqqmeter.Text));
                    //////        }
                    //////    }
                    //////    //  int bcgabpro = objBs.BCCutFabforFinalmain(Convert.ToInt32(ddlBottilot.SelectedValue));
                    //////}
                    ////////New Update for BCPre
                    //////if (ddlBottilot.SelectedValue != "Select LotNo")
                    //////{
                    //////    for (int vLoop1 = 0; vLoop1 < contrastgridfab.Rows.Count; vLoop1++)
                    //////    {
                    //////        Label newfabid = (Label)contrastgridfab.Rows[vLoop1].FindControl("newfabid");
                    //////        Label newfabcode = (Label)contrastgridfab.Rows[vLoop1].FindControl("newfabcode");
                    //////        Label lbltype = (Label)contrastgridfab.Rows[vLoop1].FindControl("lbltype");
                    //////        TextBox reqqmeter = (TextBox)contrastgridfab.Rows[vLoop1].FindControl("newtxtreqmeter");
                    //////        TextBox avgmeter = (TextBox)contrastgridfab.Rows[vLoop1].FindControl("txtavgmetercontast");

                    //////        int IFABPRObc = objBs.BCCutFabforFinal(Convert.ToInt32(ddlBottilot.SelectedValue), newfabid.Text, newfabcode.Text, Convert.ToDouble(reqqmeter.Text));
                    //////    }
                    //////}

                    DataTable dttt = new DataTable();

                    DataColumn dc = new DataColumn("id");
                    dttt.Columns.Add(dc);

                    dc = new DataColumn("name");
                    dttt.Columns.Add(dc);

                    dc = new DataColumn("Reqmeter");
                    dttt.Columns.Add(dc);

                    dc = new DataColumn("Type");
                    dttt.Columns.Add(dc);

                    dc = new DataColumn("meter");
                    dttt.Columns.Add(dc);


                    dstt.Tables.Add(dttt);

                    //if (ddlAgainst.SelectedValue != "0" && txtchequedd.Text != "" && txtAgainstAmount.Text != "0")
                    for (int vLoop1 = 0; vLoop1 < newgridfablist.Rows.Count; vLoop1++)
                    {
                        System.Web.UI.WebControls.CheckBox chkitemchecked = (System.Web.UI.WebControls.CheckBox)newgridfablist.Rows[vLoop1].FindControl("chkitemchecked");

                        Label newfabid = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabid");
                        Label newfabcode = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabcode");
                        TextBox reqqmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtreqmeter");
                        System.Web.UI.WebControls.CheckBox dckitem = (System.Web.UI.WebControls.CheckBox)newgridfablist.Rows[vLoop1].FindControl("chkitemchecked");
                        if (dckitem.Checked == true)
                        {
                            if (reqqmeter.Text != "0")
                            {
                                DataRow drd = dstt.Tables[0].NewRow();
                                drd["id"] = newfabid.Text;
                                drd["name"] = newfabcode.Text;
                                drd["reqmeter"] = reqqmeter.Text;
                                drd["Type"] = "B";
                                drd["meter"] = lblBodyAvgmeter.Text;
                                if (chkitemchecked.Checked == true)
                                {
                                    dstt.Tables[0].Rows.Add(drd);

                                }
                            }
                        }
                    }

                    if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
                    {
                        int IFABPROCESS = objBs.fabprocssscut(dstt);
                    }
                    else
                    {
                        int IFABPROCESS = objBs.fabprocssscutbc(dstt);
                    }
                    DataSet dstt1 = new DataSet();
                    DataTable dttt1 = new DataTable();
                    DataColumn dc1 = new DataColumn("id");
                    dttt1.Columns.Add(dc1);

                    dc1 = new DataColumn("name");
                    dttt1.Columns.Add(dc1);

                    dc1 = new DataColumn("Reqmeter");
                    dttt1.Columns.Add(dc1);

                    dc1 = new DataColumn("Type");
                    dttt1.Columns.Add(dc1);

                    dc1 = new DataColumn("meter");
                    dttt1.Columns.Add(dc1);


                    dstt1.Tables.Add(dttt1);

                    for (int vLoop1 = 0; vLoop1 < contrastgridfab.Rows.Count; vLoop1++)
                    {
                        Label newfabid = (Label)contrastgridfab.Rows[vLoop1].FindControl("newfabid");
                        Label newfabcode = (Label)contrastgridfab.Rows[vLoop1].FindControl("newfabcode");
                        Label lbltype = (Label)contrastgridfab.Rows[vLoop1].FindControl("lbltype");
                        TextBox reqqmeter = (TextBox)contrastgridfab.Rows[vLoop1].FindControl("newtxtreqmeter");
                        TextBox avgmeter = (TextBox)contrastgridfab.Rows[vLoop1].FindControl("txtavgmetercontast");

                        if (reqqmeter.Text != "0")
                        {
                            DataRow drd1 = dstt1.Tables[0].NewRow();
                            drd1["id"] = newfabid.Text;
                            drd1["name"] = newfabcode.Text;
                            drd1["reqmeter"] = reqqmeter.Text;
                            drd1["Type"] = lbltype.Text;
                            drd1["meter"] = avgmeter.Text;

                            dstt1.Tables[0].Rows.Add(drd1);
                        }
                    }


                    if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
                    {
                        int IFABPROCESS1 = objBs.fabprocssscut(dstt1);
                    }
                    else
                    {
                        int IFABPROCESS1 = objBs.fabprocssscutbc(dstt1);
                    }

                    ////// int iprocess = objBs.processedit(ndstt, txtLotNo.Text, count);
                    int iprocess = objBs.processedit(ndstt, txtLotNo.Text, count, iStatus);
                    for (int Loop1 = 0; Loop1 < NewSizeRatioGrid.Rows.Count; Loop1++)
                    {
                        Label Nlblitemname = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblitemname");
                        Label Nlbltransid = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlbltransid");

                        Label Nlblfitname = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblfitname");
                        Label Nlblfitid = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblfitid");

                        Label Nlblrequiredmeter = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblrequiredmeter");

                        Label Nlblavgmeter = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblavgmeter");


                        TextBox txtcontrast = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("txtcontrast");


                        TextBox Ntxt30fs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt30fs");
                        TextBox Ntxt32fs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt32fs");


                        TextBox Ntxt34fs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt34fs");
                        TextBox Ntxt36fs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt36fs");


                        TextBox Ntxtxsfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxsfs");
                        TextBox Ntxtsfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtsfs");


                        TextBox Ntxtmfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtmfs");
                        TextBox Ntxtlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtlfs");


                        TextBox Ntxtxlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxlfs");
                        TextBox Ntxtxxlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxxlfs");


                        TextBox Ntxt3xlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt3xlfs");
                        TextBox Ntxt4xlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt4xlfs");


                        TextBox Ntxt30hs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt30hs");
                        TextBox Ntxt32hs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt32hs");

                        TextBox Ntxt34hs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt34hs");
                        TextBox Ntxt36hs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt36hs");

                        TextBox Ntxtxshs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxshs");
                        TextBox Ntxtshs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtshs");

                        TextBox Ntxtmhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtmhs");
                        TextBox Ntxtlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtlhs");

                        TextBox Ntxtxlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxlhs");
                        TextBox Ntxtxxlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxxlhs");

                        TextBox Ntxt3xlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt3xlhs");
                        TextBox Ntxt4xlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt4xlhs");




                        Label Nlblprocessshirt = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblprocessshirt");

                        Label Nlbltotalshirt = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlbltotalshirt");


                        Label Nlblprocessmeterratio = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblprocessmeterratio");


                        int iStatus2 = objBs.insertTranscutratioprocess(Nlbltransid.Text, Nlblitemname.Text, Nlblfitid.Text, Nlblrequiredmeter.Text, Nlblavgmeter.Text, Nlbltotalshirt.Text, Nlblprocessshirt.Text, Nlblprocessmeterratio.Text, Ntxt30fs.Text, Ntxt30hs.Text, Ntxt32fs.Text, Ntxt32hs.Text, Ntxt34fs.Text, Ntxt34hs.Text, Ntxt36fs.Text, Ntxt36hs.Text, Ntxtxsfs.Text, Ntxtxshs.Text, Ntxtsfs.Text, Ntxtshs.Text, Ntxtmfs.Text, Ntxtmhs.Text, Ntxtlfs.Text, Ntxtlhs.Text, Ntxtxlfs.Text, Ntxtxlhs.Text, Ntxtxxlfs.Text, Ntxtxxlhs.Text, Ntxt3xlfs.Text, Ntxt3xlhs.Text, Ntxt4xlfs.Text, Ntxt4xlhs.Text);

                    }

                    #endregion
                }

                #endregion
            }

            #region Disabled
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

            #endregion

            //System.Threading.Thread.Sleep(3000);
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
            //DataSet dssmer = new DataSet();
            //DataSet dteo = new DataSet();
            if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
            {
                DataSet dsrefno = objBs.getnewsupplierforcutNEW(drpwidth.SelectedValue);
                if (dsrefno != null)
                {
                    if (dsrefno.Tables[0].Rows.Count > 0)
                    {
                        chkinvno.DataSource = dsrefno.Tables[0];
                        chkinvno.DataTextField = "invoiceno";
                        chkinvno.DataValueField = "pogrnid";
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
            }
            else
            {
                DataSet dsrefno = objBs.getnewsupplierforcutbc(drpwidth.SelectedValue, ddlBottilot.SelectedValue, drpbranch.SelectedValue);
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
            }

            //DataSet dcheckwidth = objBs.getwidthnewprocess(drpwidth.SelectedItem.Text);
            //if (dcheckwidth.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dcheckwidth.Tables[0].Rows.Count; i++)
            //    {
            //        string size = dcheckwidth.Tables[0].Rows[i]["Sizeid"].ToString();
            //        string value = dcheckwidth.Tables[0].Rows[i]["w"].ToString();
            //        if (size == "1")
            //        {
            //            txtsharp.Text = value;
            //        }
            //        else
            //        {
            //            txtexec.Text = value;
            //        }

            //    }
            //}

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

        protected void olddrpwidthChange(object sender, EventArgs e)
        {
            //DataSet dssmer = new DataSet();
            //DataSet dteo = new DataSet();

            DataSet dsrefno = objBs.getnewsupplierforcutNEW(drpwidth.SelectedValue);
            if (dsrefno != null)
            {
                if (dsrefno.Tables[0].Rows.Count > 0)
                {
                    chkinvno.DataSource = dsrefno.Tables[0];
                    chkinvno.DataTextField = "invoiceno";
                    chkinvno.DataValueField = "pogrnid";
                    chkinvno.DataBind();
                    //  drpwidth.Items.Insert(0, "Select Width");
                }
                else
                {
                    chkinvno.DataSource = null;
                    chkinvno.DataTextField = "invoiceno";
                    chkinvno.DataValueField = "pogrnid";
                    chkinvno.DataBind();
                    chkinvno.ClearSelection();
                    chkinvno.Items.Clear();
                }
            }


            //DataSet dcheckwidth = objBs.getwidthnewprocess(drpwidth.SelectedItem.Text);
            //if (dcheckwidth.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dcheckwidth.Tables[0].Rows.Count; i++)
            //    {
            //        string size = dcheckwidth.Tables[0].Rows[i]["Sizeid"].ToString();
            //        string value = dcheckwidth.Tables[0].Rows[i]["w"].ToString();
            //        if (size == "1")
            //        {
            //            txtsharp.Text = value;
            //        }
            //        else
            //        {
            //            txtexec.Text = value;
            //        }

            //    }
            //}

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


            if (ddlbrand.SelectedValue == "Select Brand Name")
            {
                dddldesign.ClearSelection();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Brand Name. Thank you');", true);
                return;
            }

            if (ddlFit.SelectedValue == "Select fit")
            {
                dddldesign.ClearSelection();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Fit. Thank you');", true);
                return;
            }

            if (drplab.SelectedValue == "Select Label")
            {
                dddldesign.ClearSelection();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Label. Thank you');", true);
                return;
            }


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
                    txtDesignRate.Text = dteo.Tables[0].Rows[0]["rat"].ToString();
                    txtAvailableMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                    txtReqMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
                    if (radbtn.SelectedValue == "1")
                    {
                        txtavamet1.Text = txtAvailableMtr.Text;
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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
        protected void remainingshirt(object sender, EventArgs e)
        {

        }
        protected void remainingmeter_chnaged(object sender, EventArgs e)
        {
            double r = 0.00;
            double r1 = 0.00;
            double rr = 0.00;
            double rb = 0.00;
            double rr1 = 0.00;
            double rb1 = 0.00;
            if (txtavamet1.Text == "")
            {
                txtavamet1.Text = "0";
            }

            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                double avalmeter = Convert.ToDouble(txtReqMtr.Text);
                double givenmeter = Convert.ToDouble(txtavamet1.Text);

                double remmeter = avalmeter - givenmeter;

                bool negative = remmeter < 0;

                if (negative == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Meter Greater than Remaining Meter. Thank you');", true);
                    txtavamet1.Focus();
                    return;
                }
                else
                {
                    Ntxtremmeter.Text = remmeter.ToString("0.00");
                }
                //Ntxtremmeter.Text = remmeter;


                // double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //   if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                ////   else
                //   {
                //       wid = Convert.ToDouble(txtexec.Text);
                //   }

                // Ntxtremmeter.Text = txtReqMtr.Text;

                double roundoff = Convert.ToDouble(txtavamet1.Text) / wid;



                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                double roundoff1 = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                if (roundoff1 > 0.5)
                {
                    r1 = Math.Round(Convert.ToDouble(roundoff1), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r1 = Math.Floor(Convert.ToDouble(roundoff1));
                }

            }

            txtremashirt.Text = r.ToString();
            Ntxtactshirt.Text = r.ToString();
        }

        protected void reqchanged(object sender, EventArgs e)
        {
            double r = 0.00;
            double r1 = 0.00;
            double rr = 0.00;
            double rb = 0.00;
            double rr1 = 0.00;
            double rb1 = 0.00;
            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                // double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //   if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                ////   else
                //   {
                //       wid = Convert.ToDouble(txtexec.Text);
                //   }

                // Ntxtremmeter.Text = txtReqMtr.Text;
                if (txtReqMtr.Text == "")
                    txtReqMtr.Text = "0";

                double roundoff = Convert.ToDouble(txtReqMtr.Text) / wid;



                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                string cond1 = "";

                foreach (System.Web.UI.WebControls.ListItem listItem in chkinvno.Items)
                {
                    if (listItem.Text != "All")
                    {
                        if (listItem.Selected)
                        {
                            cond1 += " pogrnid='" + listItem.Value + "' ,";
                        }
                    }
                }
                cond1 = cond1.TrimEnd(',');
                cond1 = cond1.Replace(",", "or");

                if (cond1 != "")
                {
                    DataSet dminmax1 = objBs.getcutlistdesignforminandmaxadditionNEW(cond1, drpwidth.SelectedValue);
                    if (dminmax1.Tables[0].Rows.Count > 0)
                    {
                        txtAvailableMtr.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                        // txtReqMtr.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                        //Ntxtremmeter.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                        //  txtavamet1.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                    }
                }
                else
                {
                    txtAvailableMtr.Text = "0";
                    // txtReqMtr.Text = "0";
                    txtavamet1.Text = "0";
                    // Ntxtremmeter.Text = "0";
                }


                if (txtAvailableMtr.Text == "")
                    txtAvailableMtr.Text = "0";

                double roundoff1 = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                if (roundoff1 > 0.5)
                {
                    r1 = Math.Round(Convert.ToDouble(roundoff1), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r1 = Math.Floor(Convert.ToDouble(roundoff1));
                }

            }
            txtNoofShirts.Text = r1.ToString();
            txtReqNoShirts.Text = r.ToString();

            rr = ((r * 0) / 100);
            if (rr > 0.5)
            {
                rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
            }
            else
            {
                rb = Math.Floor(Convert.ToDouble(rr));
            }
            txtextrashirt.Text = rb.ToString();

            rr1 = ((r * 0) / 100);
            if (rr1 > 0.5)
            {
                rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
            }
            else
            {
                rb1 = Math.Floor(Convert.ToDouble(rr1));
            }
            txtminshirt.Text = rb1.ToString();


            //if (radbtn.SelectedValue == "1")
            //{
            //    getzeroforemptysize();

            //    txtavamet1.Text = txtReqMtr.Text;
            //    // Sddrpartyselected_changed(sender, e);

            //    if (tsfs.Visible == true)
            //    {
            //        txt36FS.Focus();
            //        if (txt36FS.Text == "0")
            //        {
            //            txt36FS.Text = "";
            //        }
            //    }
            //    else if (tefs.Visible == true)
            //    {
            //        txt38FS.Focus();
            //        if (txt38FS.Text == "0")
            //        {
            //            txt38FS.Text = "";
            //        }
            //    }
            //    else if (tnfs.Visible == true)
            //    {
            //        txt39FS.Focus();
            //        if (txt39FS.Text == "0")
            //        {
            //            txt39FS.Text = "";
            //        }
            //    }
            //    else if (fzfs.Visible == true)
            //    {
            //        txt40FS.Focus();
            //        if (txt40FS.Text == "0")
            //        {
            //            txt40FS.Text = "";
            //        }
            //    }
            //    else if (ftfs.Visible == true)
            //    {
            //        txt42FS.Focus();
            //        if (txt42FS.Text == "0")
            //        {
            //            txt42FS.Text = "";
            //        }
            //    }
            //    else if (fffs.Visible == true)
            //    {
            //        txt44FS.Focus();
            //        if (txt44FS.Text == "0")
            //        {
            //            txt44FS.Text = "";
            //        }
            //    }
            //    else if (tshs.Visible == true)
            //    {
            //        txt36HS.Focus();
            //        if (txt36HS.Text == "0")
            //        {
            //            txt36HS.Text = "";
            //        }
            //    }

            //    else if (tehs.Visible == true)
            //    {
            //        txt38HS.Focus();
            //        if (txt38HS.Text == "0")
            //        {
            //            txt38HS.Text = "";
            //        }
            //    }
            //    else if (tnhs.Visible == true)
            //    {
            //        txt39HS.Focus();
            //        if (txt39HS.Text == "0")
            //        {
            //            txt39HS.Text = "";
            //        }
            //    }
            //    else if (fzhs.Visible == true)
            //    {
            //        txt40HS.Focus();
            //        if (txt40HS.Text == "0")
            //        {
            //            txt40HS.Text = "";
            //        }
            //    }
            //    else if (fths.Visible == true)
            //    {
            //        txt42HS.Focus();
            //        if (txt42HS.Text == "0")
            //        {
            //            txt42HS.Text = "";
            //        }
            //    }
            //    else if (ffhs.Visible == true)
            //    {
            //        txt44HS.Focus();
            //        if (txt44HS.Text == "0")
            //        {
            //            txt44HS.Text = "";
            //        }
            //    }
            //}

        }

        protected void drpfitchanged(object sender, EventArgs e)
        {
            double r = 0.00;
            double r1 = 0.00;

            double rr = 0.00;
            double rb = 0.00;

            if (drpitemtype.SelectedValue == "Select Item")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Item. else Please Contact Administrator.Thank you!!!');", true);
                return;
            }

            DataSet getavgmeter = objBs.getwidthnewprocessprecutting(drpitemtype.SelectedValue);
            if (getavgmeter.Tables[0].Rows.Count > 0)
            {
                txtactualmet.Text = Convert.ToDouble(getavgmeter.Tables[0].Rows[0]["w"]).ToString("0.00");
            }

            DataSet dsize = objBs.Getsizetype();
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

            if (ddlbrand.SelectedValue == "Select Brand Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Some thing went wrong.Please Contact Administrator.Thank you!!!');", true);
                return;
            }
            else
            {

                DataSet dsizee = objBs.Getfitseize(drpFit.SelectedValue, ddlbrand.SelectedValue);
                if ((dsizee.Tables[0].Rows.Count > 0))
                {
                    //Select the checkboxlist items those values are true in database
                    //Loop through the DataTable
                    for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                    {
                        //You need to change this as per your DB Design
                        string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();



                        //if (size == "39FS" || size == "39HS" || size == "44FS" || size == "44HS")
                        //{
                        //}
                        //else
                        {
                            //Find the checkbox list items using FindByValue and select it.
                            chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                        }

                    }
                }
            }

            if (chkSizes.SelectedIndex >= 0)
            {
                S30fs.Visible = false; S30hs.Visible = false;

                S32fs.Visible = false; S32hs.Visible = false;

                S34fs.Visible = false; S34hs.Visible = false;

                S36fs.Visible = false; S36hs.Visible = false;

                Xsfs.Visible = false; Xshs.Visible = false;

                sfs.Visible = false; shs.Visible = false;

                mfs.Visible = false; mhs.Visible = false;
                lfs.Visible = false; lhs.Visible = false;
                xlfs.Visible = false; xlhs.Visible = false;
                xxlfs.Visible = false; xxlhs.Visible = false;
                xxxlfs.Visible = false; xxxlhs.Visible = false;
                xxxxlfs.Visible = false; xxxxlhs.Visible = false;

                int lop = 0;
                //Loop through each item of checkboxlist
                foreach (System.Web.UI.WebControls.ListItem item in chkSizes.Items)
                {
                    //check if item selected

                    if (item.Selected)
                    {

                        {
                            if (item.Text == "30FS")
                            {
                                S30fs.Visible = true;
                            }
                            if (item.Text == "30HS")
                            {
                                //gridsize.Columns[8].Visible = true;
                                S30hs.Visible = true;
                            }
                            if (item.Text == "32FS")
                            {
                                //    gridsize.Columns[3].Visible = true;
                                S32fs.Visible = true;
                            }
                            if (item.Text == "32HS")
                            {
                                //gridsize.Columns[9].Visible = true;
                                S32hs.Visible = true;
                            }
                            if (item.Text == "34FS")
                            {
                                // gridsize.Columns[4].Visible = true;
                                S34fs.Visible = true;
                            }
                            if (item.Text == "34HS")
                            {
                                // gridsize.Columns[10].Visible = true;
                                S34hs.Visible = true;
                            }
                            if (item.Text == "36FS")
                            {
                                //gridsize.Columns[5].Visible = true;
                                S36fs.Visible = true;
                            }
                            if (item.Text == "36HS")
                            {
                                // gridsize.Columns[11].Visible = true;
                                S36hs.Visible = true;
                            }
                            if (item.Text == "XSFS")
                            {
                                //  gridsize.Columns[6].Visible = true;
                                Xsfs.Visible = true;
                            }
                            if (item.Text == "XSHS")
                            {
                                // gridsize.Columns[12].Visible = true;
                                Xshs.Visible = true;
                            }
                            if (item.Text == "LFS")
                            {
                                // gridsize.Columns[7].Visible = true;
                                lfs.Visible = true;
                            }
                            if (item.Text == "LHS")
                            {
                                // gridsize.Columns[13].Visible = true;

                                lhs.Visible = true;
                            }

                            if (item.Text == "XLFS")
                            {
                                xlfs.Visible = true;
                            }
                            if (item.Text == "XLHS")
                            {
                                //gridsize.Columns[8].Visible = true;
                                xlhs.Visible = true;
                            }
                            if (item.Text == "XXLFS")
                            {
                                //    gridsize.Columns[3].Visible = true;
                                xxlfs.Visible = true;
                            }
                            if (item.Text == "XXLHS")
                            {
                                //gridsize.Columns[9].Visible = true;
                                xxlhs.Visible = true;
                            }
                            if (item.Text == "3XLFS")
                            {
                                // gridsize.Columns[4].Visible = true;
                                xxxlfs.Visible = true;
                            }
                            if (item.Text == "3XLHS")
                            {
                                // gridsize.Columns[10].Visible = true;
                                xxxlhs.Visible = true;
                            }
                            if (item.Text == "4XLFS")
                            {
                                //gridsize.Columns[5].Visible = true;
                                xxxxlfs.Visible = true;
                            }
                            if (item.Text == "4XLHS")
                            {
                                // gridsize.Columns[11].Visible = true;
                                xxxxlhs.Visible = true;
                            }
                            if (item.Text == "SFS")
                            {
                                //  gridsize.Columns[6].Visible = true;
                                sfs.Visible = true;
                            }
                            if (item.Text == "SHS")
                            {
                                // gridsize.Columns[12].Visible = true;
                                shs.Visible = true;
                            }
                            if (item.Text == "MFS")
                            {
                                // gridsize.Columns[7].Visible = true;
                                mfs.Visible = true;
                            }
                            if (item.Text == "MHS")
                            {
                                // gridsize.Columns[13].Visible = true;

                                mhs.Visible = true;
                            }


                            lop++;

                        }
                    }
                }

            }
            else
            {
                S30fs.Visible = false; S30hs.Visible = false;

                S32fs.Visible = false; S32hs.Visible = false;

                S34fs.Visible = false; S34hs.Visible = false;

                S36fs.Visible = false; S36hs.Visible = false;

                Xsfs.Visible = false; Xshs.Visible = false;

                sfs.Visible = false; shs.Visible = false;

                mfs.Visible = false; mhs.Visible = false;
                lfs.Visible = false; lhs.Visible = false;
                xlfs.Visible = false; xlhs.Visible = false;
                xxlfs.Visible = false; xxlhs.Visible = false;
                xxxlfs.Visible = false; xxxlhs.Visible = false;
                xxxxlfs.Visible = false; xxxxlhs.Visible = false;
            }

            remainingmeter_chnaged(sender, e);
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
            else if (radcuttype.SelectedValue == "3")
            {
                dddldesign.Enabled = false;
                txtDesignRate.Enabled = false;
                txtReqMtr.Enabled = false;
                txtNoofShirts.Enabled = false;
                radchecked(sender, e);

            }
        }

        protected void btnclear_OnClick(object sender, EventArgs e)
        {

            Nchkemb.ClearSelection();
            Nchkstch.ClearSelection();
            Nchkkbut.ClearSelection();
            Nchkwash.ClearSelection();
            Nchkprint.ClearSelection();
            Nchkiron.ClearSelection();
            Nchkbartag.ClearSelection();
            Nchktrimming.ClearSelection();
            Nchkconsai.ClearSelection();
        }
        protected void ProcessShirt(object sender, EventArgs e)
        {
            DataTable dttt;
            DataRow NdrNew11;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("ItemName");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Itemid");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Fitname");
            dttt.Columns.Add(dct);


            dct = new DataColumn("Reqshirt");
            dttt.Columns.Add(dct);


            dct = new DataColumn("Fitid");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Givenmeter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Avgmeter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Actmeter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Totalshirt");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S30FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S32FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S34FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S36FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SXSFS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SSFS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SMFS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SLFS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SXLFS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SXXLFS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S3XLFS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S4XLFS");
            dttt.Columns.Add(dct);



            // HS

            dct = new DataColumn("S30HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S32HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S34HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S36HS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SXSHS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SSHS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SMHS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SLHS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SXLHS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SXXLHS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S3XLHS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("S4XLHS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("EndBit");
            dttt.Columns.Add(dct);

            dct = new DataColumn("TotalshirtSize");
            dttt.Columns.Add(dct);

            dct = new DataColumn("RowId");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);



            for (int vLoop = 0; vLoop < NewSizeRatioGrid.Rows.Count; vLoop++)
            {
                Label Nlblitemname = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblitemname");
                Label Nlbltransid = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlbltransid");
                Label Nlblfitname = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblfitname");
                Label Nlblfitid = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblfitid");
                Label Nlblrequiredmeter = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblrequiredmeter");
                Label Nlblavgmeter = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblavgmeter");
                Label Nlbltotalshirt = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlbltotalshirt");
                TextBox txt30fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt30fs");
                TextBox txt32fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt32fs");
                TextBox txt34fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt34fs");
                TextBox txt36fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt36fs");
                TextBox txtxsfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxsfs");
                TextBox txtsfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtsfs");
                TextBox txtmfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtmfs");
                TextBox txtlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtlfs");
                TextBox txtxlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxlfs");
                TextBox txtxxlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxxlfs");
                TextBox txt3xlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt3xlfs");
                TextBox txt4xlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt4xlfs");
                TextBox txt30hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt30hs");
                TextBox txt32hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt32hs");
                TextBox txt34hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt34hs");
                TextBox txt36hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt36hs");
                TextBox txtxshs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxshs");
                TextBox txtshs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtshs");
                TextBox txtmhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtmhs");
                TextBox txtlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtlhs");
                TextBox txtxlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxlhs");
                TextBox txtxxlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxxlhs");
                TextBox txt3xlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt3xlhs");
                TextBox txt4xlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt4xlhs");
                TextBox txtcontra = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("txtcontra");
                Label lblEndBit = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("lblEndBit");
                Label NlblSizeshirt = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("NlblSizeshirt");
                HiddenField hdRowId = (HiddenField)NewSizeRatioGrid.Rows[vLoop].FindControl("hdRowId");


                double ratio = Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txtxsfs.Text) + Convert.ToDouble(txtsfs.Text) + Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txt3xlfs.Text) + Convert.ToDouble(txt4xlfs.Text) +
                    Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txtxshs.Text) + Convert.ToDouble(txtshs.Text) + Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txt3xlhs.Text) + Convert.ToDouble(txt4xlhs.Text) + Convert.ToDouble(NlblSizeshirt.Text);

                #region FS PROCESS
                // FS
                double os30fs = 0;
                if (txt30fs.Text != "0")
                {
                    double s30fs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt30fs.Text);
                    if (s30fs > 0.5)
                    {
                        os30fs = Math.Round(Convert.ToDouble(s30fs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os30fs = Math.Floor(Convert.ToDouble(s30fs));
                    }
                }


                double os32fs = 0;
                if (txt32fs.Text != "0")
                {
                    double s32fs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt32fs.Text);
                    if (s32fs > 0.5)
                    {
                        os32fs = Math.Round(Convert.ToDouble(s32fs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os32fs = Math.Floor(Convert.ToDouble(s32fs));
                    }
                }


                double os34fs = 0;
                if (txt34fs.Text != "0")
                {
                    double s34fs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt34fs.Text);
                    if (s34fs > 0.5)
                    {
                        os34fs = Math.Round(Convert.ToDouble(s34fs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os34fs = Math.Floor(Convert.ToDouble(s34fs));
                    }
                }

                double os36fs = 0;
                if (txt36fs.Text != "0")
                {
                    double s36fs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt36fs.Text);
                    if (s36fs > 0.5)
                    {
                        os36fs = Math.Round(Convert.ToDouble(s36fs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os36fs = Math.Floor(Convert.ToDouble(s36fs));
                    }
                }

                double osXSfs = 0;
                if (txtxsfs.Text != "0")
                {
                    double sXSfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxsfs.Text);
                    if (sXSfs > 0.5)
                    {
                        osXSfs = Math.Round(Convert.ToDouble(sXSfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osXSfs = Math.Floor(Convert.ToDouble(sXSfs));
                    }
                }

                double ossfs = 0;
                if (txtsfs.Text != "0")
                {
                    double ssfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtsfs.Text);
                    if (ssfs > 0.5)
                    {
                        ossfs = Math.Round(Convert.ToDouble(ssfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        ossfs = Math.Floor(Convert.ToDouble(ssfs));
                    }
                }

                double osmfs = 0;
                if (txtmfs.Text != "0")
                {
                    double smfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtmfs.Text);
                    if (smfs > 0.5)
                    {
                        osmfs = Math.Round(Convert.ToDouble(smfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osmfs = Math.Floor(Convert.ToDouble(smfs));
                    }
                }

                double oslfs = 0;
                if (txtlfs.Text != "0")
                {
                    double slfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtlfs.Text);
                    if (slfs > 0.5)
                    {
                        oslfs = Math.Round(Convert.ToDouble(slfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        oslfs = Math.Floor(Convert.ToDouble(slfs));
                    }
                }


                double osxlfs = 0;
                if (txtxlfs.Text != "0")
                {

                    double sxlfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxlfs.Text);
                    if (sxlfs > 0.5)
                    {
                        osxlfs = Math.Round(Convert.ToDouble(sxlfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osxlfs = Math.Floor(Convert.ToDouble(sxlfs));
                    }
                }


                double osxxlfs = 0;
                if (txtxxlfs.Text != "0")
                {


                    double sxxlfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxxlfs.Text);
                    if (sxxlfs > 0.5)
                    {
                        osxxlfs = Math.Round(Convert.ToDouble(sxxlfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osxxlfs = Math.Floor(Convert.ToDouble(sxxlfs));
                    }
                }


                double os3xlfs = 0;
                if (txt3xlfs.Text != "0")
                {
                    double s3xlfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt3xlfs.Text);
                    if (s3xlfs > 0.5)
                    {
                        os3xlfs = Math.Round(Convert.ToDouble(s3xlfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os3xlfs = Math.Floor(Convert.ToDouble(s3xlfs));
                    }
                }

                double os4xlfs = 0;
                if (txt4xlfs.Text != "0")
                {
                    double s4xlfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt4xlfs.Text);
                    if (s4xlfs > 0.5)
                    {
                        os4xlfs = Math.Round(Convert.ToDouble(s4xlfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os4xlfs = Math.Floor(Convert.ToDouble(s4xlfs));
                    }
                }

                #endregion

                #region HS PROCESS
                //HS
                double os30hs = 0;
                if (txt30hs.Text != "0")
                {
                    double s30hs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt30hs.Text);
                    if (s30hs > 0.5)
                    {
                        os30hs = Math.Round(Convert.ToDouble(s30hs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os30hs = Math.Floor(Convert.ToDouble(s30hs));
                    }
                }


                double os32hs = 0;
                if (txt32hs.Text != "0")
                {
                    double s32hs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt32hs.Text);
                    if (s32hs > 0.5)
                    {
                        os32hs = Math.Round(Convert.ToDouble(s32hs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os32hs = Math.Floor(Convert.ToDouble(s32hs));
                    }
                }


                double os34hs = 0;
                if (txt34hs.Text != "0")
                {
                    double s34hs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt34hs.Text);
                    if (s34hs > 0.5)
                    {
                        os34hs = Math.Round(Convert.ToDouble(s34hs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os34hs = Math.Floor(Convert.ToDouble(s34hs));
                    }
                }

                double os36hs = 0;
                if (txt36hs.Text != "0")
                {
                    double s36hs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt36hs.Text);
                    if (s36hs > 0.5)
                    {
                        os36hs = Math.Round(Convert.ToDouble(s36hs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os36hs = Math.Floor(Convert.ToDouble(s36hs));
                    }
                }

                double osXShs = 0;
                if (txtxshs.Text != "0")
                {
                    double sXShs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxshs.Text);
                    if (sXShs > 0.5)
                    {
                        osXShs = Math.Round(Convert.ToDouble(sXShs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osXShs = Math.Floor(Convert.ToDouble(sXShs));
                    }
                }

                double osshs = 0;
                if (txtshs.Text != "0")
                {
                    double sshs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtshs.Text);
                    if (sshs > 0.5)
                    {
                        osshs = Math.Round(Convert.ToDouble(sshs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osshs = Math.Floor(Convert.ToDouble(sshs));
                    }
                }

                double osmhs = 0;
                if (txtmhs.Text != "0")
                {
                    double smhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtmhs.Text);
                    if (smhs > 0.5)
                    {
                        osmhs = Math.Round(Convert.ToDouble(smhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osmhs = Math.Floor(Convert.ToDouble(smhs));
                    }
                }

                double oslhs = 0;
                if (txtlhs.Text != "0")
                {
                    double slhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtlhs.Text);
                    if (slhs > 0.5)
                    {
                        oslhs = Math.Round(Convert.ToDouble(slhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        oslhs = Math.Floor(Convert.ToDouble(slhs));
                    }
                }


                double osxlhs = 0;
                if (txtxlhs.Text != "0")
                {

                    double sxlhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxlhs.Text);
                    if (sxlhs > 0.5)
                    {
                        osxlhs = Math.Round(Convert.ToDouble(sxlhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osxlhs = Math.Floor(Convert.ToDouble(sxlhs));
                    }
                }


                double osxxlhs = 0;
                if (txtxxlhs.Text != "0")
                {


                    double sxxlhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxxlhs.Text);
                    if (sxxlhs > 0.5)
                    {
                        osxxlhs = Math.Round(Convert.ToDouble(sxxlhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osxxlhs = Math.Floor(Convert.ToDouble(sxxlhs));
                    }
                }


                double os3xlhs = 0;
                if (txt3xlhs.Text != "0")
                {
                    double s3xlhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt3xlhs.Text);
                    if (s3xlhs > 0.5)
                    {
                        os3xlhs = Math.Round(Convert.ToDouble(s3xlhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os3xlhs = Math.Floor(Convert.ToDouble(s3xlhs));
                    }
                }

                double os4xlhs = 0;
                if (txt4xlhs.Text != "0")
                {
                    double s4xlhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt4xlhs.Text);
                    if (s4xlhs > 0.5)
                    {
                        os4xlhs = Math.Round(Convert.ToDouble(s4xlhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os4xlhs = Math.Floor(Convert.ToDouble(s4xlhs));
                    }
                }

                #endregion

                double ostotsizeshirts = 0;
                if (NlblSizeshirt.Text != "0")
                {
                    double stotsizeshirts = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(NlblSizeshirt.Text);
                    if (stotsizeshirts > 0.5)
                    {
                        ostotsizeshirts = Math.Round(Convert.ToDouble(stotsizeshirts), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        ostotsizeshirts = Math.Floor(Convert.ToDouble(stotsizeshirts));
                    }
                }


                double totalshirts = os30fs + os32fs + os34fs + os36fs + osXSfs + ossfs + osmfs + oslfs + osxlfs + osxxlfs + os3xlfs + os4xlfs +
                    os30hs + os32hs + os34hs + os36hs + osXShs + osshs + osmhs + oslhs + osxlhs + osxxlhs + os3xlhs + os4xlhs + ostotsizeshirts;


                NdrNew11 = dttt.NewRow();
                NdrNew11["ItemName"] = Nlblitemname.Text;
                NdrNew11["Itemid"] = Nlbltransid.Text;
                NdrNew11["Fitname"] = drpNchkfit.SelectedItem.Text;
                NdrNew11["Fitid"] = drpNchkfit.SelectedValue;
                NdrNew11["Givenmeter"] = Nlblrequiredmeter.Text;
                NdrNew11["Avgmeter"] = Nlblavgmeter.Text;
                NdrNew11["Actmeter"] = Convert.ToDouble(Convert.ToDouble(Nlblrequiredmeter.Text) / totalshirts).ToString("0.000");
                NdrNew11["Reqshirt"] = Nlbltotalshirt.Text;
                NdrNew11["Totalshirt"] = Convert.ToInt32(totalshirts).ToString();
                //FS
                NdrNew11["S30FS"] = Convert.ToInt32(os30fs).ToString();
                NdrNew11["S32FS"] = Convert.ToInt32(os32fs).ToString();
                NdrNew11["S34FS"] = Convert.ToInt32(os34fs).ToString();
                NdrNew11["S36FS"] = Convert.ToInt32(os36fs).ToString();
                NdrNew11["SXSFS"] = Convert.ToInt32(osXSfs).ToString();
                NdrNew11["SSFS"] = Convert.ToInt32(ossfs).ToString();
                NdrNew11["SMFS"] = Convert.ToInt32(osmfs).ToString();
                NdrNew11["SLFS"] = Convert.ToInt32(oslfs).ToString();
                NdrNew11["SXLFS"] = Convert.ToInt32(osxlfs).ToString();
                NdrNew11["SXXLFS"] = Convert.ToInt32(osxxlfs).ToString();
                NdrNew11["S3XLFS"] = Convert.ToInt32(os3xlfs).ToString();
                NdrNew11["S4XLFS"] = Convert.ToInt32(os4xlfs).ToString();


                //HS
                NdrNew11["S30HS"] = Convert.ToInt32(os30hs).ToString();
                NdrNew11["S32HS"] = Convert.ToInt32(os32hs).ToString();
                NdrNew11["S34HS"] = Convert.ToInt32(os34hs).ToString();
                NdrNew11["S36HS"] = Convert.ToInt32(os36hs).ToString();
                NdrNew11["SXSHS"] = Convert.ToInt32(osXShs).ToString();
                NdrNew11["SSHS"] = Convert.ToInt32(osshs).ToString();
                NdrNew11["SMHS"] = Convert.ToInt32(osmhs).ToString();
                NdrNew11["SLHS"] = Convert.ToInt32(oslhs).ToString();
                NdrNew11["SXLHS"] = Convert.ToInt32(osxlhs).ToString();
                NdrNew11["SXXLHS"] = Convert.ToInt32(osxxlhs).ToString();
                NdrNew11["S3XLHS"] = Convert.ToInt32(os3xlhs).ToString();
                NdrNew11["S4XLHS"] = Convert.ToInt32(os4xlhs).ToString();
                NdrNew11["TotalshirtSize"] = Convert.ToInt32(ostotsizeshirts).ToString();

                NdrNew11["EndBit"] = (Convert.ToDouble(Nlblrequiredmeter.Text) - (Convert.ToDouble(Nlbltotalshirt.Text) * Convert.ToDouble(Nlblavgmeter.Text))).ToString("f2");
                NdrNew11["RowId"] = hdRowId.Value;
                dstd.Tables[0].Rows.Add(NdrNew11);

                //////RatioShirtProcess.DataSource = dstd;
                //////RatioShirtProcess.DataBind();

                //////dsttlcal = dstd;
            }
            RatioShirtProcess.DataSource = dstd;
            RatioShirtProcess.DataBind();
            if (chkSizes.SelectedIndex >= 0)
            {
                RatioShirtProcess.Columns[4].Visible = false; //30FS
                RatioShirtProcess.Columns[5].Visible = false; //32FS

                RatioShirtProcess.Columns[6].Visible = false;//34Fs
                RatioShirtProcess.Columns[7].Visible = false;//36Fs

                RatioShirtProcess.Columns[8].Visible = false; //XSFS
                RatioShirtProcess.Columns[9].Visible = false; //SFS

                RatioShirtProcess.Columns[10].Visible = false; //MFS
                RatioShirtProcess.Columns[11].Visible = false; //LFS

                RatioShirtProcess.Columns[12].Visible = false; //XLFS
                RatioShirtProcess.Columns[13].Visible = false; //xxlFS

                RatioShirtProcess.Columns[14].Visible = false; //3xlHS
                RatioShirtProcess.Columns[15].Visible = false; //4xlHS

                RatioShirtProcess.Columns[16].Visible = false; //30HS

                RatioShirtProcess.Columns[17].Visible = false; //32HS

                RatioShirtProcess.Columns[18].Visible = false; //34HS
                RatioShirtProcess.Columns[19].Visible = false; //36HS

                RatioShirtProcess.Columns[20].Visible = false; //XSHS
                RatioShirtProcess.Columns[21].Visible = false; //SHS

                RatioShirtProcess.Columns[22].Visible = false; //MHS
                RatioShirtProcess.Columns[23].Visible = false; //LHS

                RatioShirtProcess.Columns[24].Visible = false; //XLHS
                RatioShirtProcess.Columns[25].Visible = false; //XXLHS

                RatioShirtProcess.Columns[26].Visible = false; //3XLHS
                RatioShirtProcess.Columns[27].Visible = false; //4XLHS




                int lop = 0;
                //Loop through each item of checkboxlist
                foreach (System.Web.UI.WebControls.ListItem item in chkSizes.Items)
                {
                    //check if item selected

                    if (item.Selected)
                    {

                        {
                            if (item.Text == "30FS")
                            {
                                RatioShirtProcess.Columns[4].Visible = true;
                            }
                            if (item.Text == "32FS")
                            {
                                RatioShirtProcess.Columns[5].Visible = true;
                            }
                            if (item.Text == "34FS")
                            {
                                RatioShirtProcess.Columns[6].Visible = true;
                            }
                            if (item.Text == "36FS")
                            {
                                RatioShirtProcess.Columns[7].Visible = true;
                            }
                            if (item.Text == "XSFS")
                            {
                                RatioShirtProcess.Columns[8].Visible = true;
                            }
                            if (item.Text == "SFS")
                            {
                                RatioShirtProcess.Columns[9].Visible = true;
                            }
                            if (item.Text == "MFS")
                            {
                                RatioShirtProcess.Columns[10].Visible = true;
                            }
                            if (item.Text == "LFS")
                            {
                                RatioShirtProcess.Columns[11].Visible = true;
                            }
                            if (item.Text == "XLFS")
                            {
                                RatioShirtProcess.Columns[12].Visible = true;
                            }
                            if (item.Text == "XXLFS")
                            {
                                RatioShirtProcess.Columns[13].Visible = true;
                            }
                            if (item.Text == "3XLFS")
                            {
                                RatioShirtProcess.Columns[14].Visible = true;
                            }
                            if (item.Text == "4XLFS")
                            {
                                RatioShirtProcess.Columns[15].Visible = true;
                            }


                            // FOR HS

                            if (item.Text == "30HS")
                            {
                                RatioShirtProcess.Columns[16].Visible = true;
                            }

                            if (item.Text == "32HS")
                            {
                                RatioShirtProcess.Columns[17].Visible = true;
                            }

                            if (item.Text == "34HS")
                            {
                                RatioShirtProcess.Columns[18].Visible = true;
                            }

                            if (item.Text == "36HS")
                            {
                                RatioShirtProcess.Columns[19].Visible = true;

                            }

                            if (item.Text == "XSHS")
                            {
                                RatioShirtProcess.Columns[20].Visible = true;
                            }

                            if (item.Text == "SHS")
                            {
                                RatioShirtProcess.Columns[21].Visible = true;
                            }

                            if (item.Text == "MHS")
                            {
                                RatioShirtProcess.Columns[22].Visible = true;
                            }

                            if (item.Text == "LHS")
                            {
                                RatioShirtProcess.Columns[23].Visible = true;
                            }

                            if (item.Text == "XLHS")
                            {
                                RatioShirtProcess.Columns[24].Visible = true;
                            }

                            if (item.Text == "XXLHS")
                            {
                                RatioShirtProcess.Columns[25].Visible = true;
                            }

                            if (item.Text == "3XLHS")
                            {
                                RatioShirtProcess.Columns[26].Visible = true;
                            }

                            if (item.Text == "4XLHS")
                            {
                                RatioShirtProcess.Columns[27].Visible = true;
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
                RatioShirtProcess.Columns[4].Visible = false; //30FS
                RatioShirtProcess.Columns[5].Visible = false; //32FS

                RatioShirtProcess.Columns[6].Visible = false;//34Fs
                RatioShirtProcess.Columns[7].Visible = false;//36Fs

                RatioShirtProcess.Columns[8].Visible = false; //XSFS
                RatioShirtProcess.Columns[9].Visible = false; //SFS

                RatioShirtProcess.Columns[10].Visible = false; //MFS
                RatioShirtProcess.Columns[11].Visible = false; //LFS

                RatioShirtProcess.Columns[12].Visible = false; //XLFS
                RatioShirtProcess.Columns[13].Visible = false; //xxlFS

                RatioShirtProcess.Columns[14].Visible = false; //3xlHS
                RatioShirtProcess.Columns[15].Visible = false; //4xlHS

                RatioShirtProcess.Columns[16].Visible = false; //30HS

                RatioShirtProcess.Columns[17].Visible = false; //32HS

                RatioShirtProcess.Columns[18].Visible = false; //34HS
                RatioShirtProcess.Columns[19].Visible = false; //36HS

                RatioShirtProcess.Columns[20].Visible = false; //XSHS
                RatioShirtProcess.Columns[21].Visible = false; //SHS

                RatioShirtProcess.Columns[22].Visible = false; //MHS
                RatioShirtProcess.Columns[23].Visible = false; //LHS

                RatioShirtProcess.Columns[24].Visible = false; //XLHS
                RatioShirtProcess.Columns[25].Visible = false; //XXLHS

                RatioShirtProcess.Columns[26].Visible = false; //3XLHS
                RatioShirtProcess.Columns[27].Visible = false; //4XLHS

            }




            //////RatioShirtProcess.DataSource = null;
            //////RatioShirtProcess.DataBind();

            //////RatioShirtProcess.DataSource = dsttlcal;
            //////RatioShirtProcess.DataBind();
            UpdatePanel6.Update();

        }

        protected void RatioShirtProcess_OnDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox S30FS = ((TextBox)e.Row.FindControl("S30FS"));
                //TextBox S32FS = ((TextBox)e.Row.FindControl("S32FS"));
                //TextBox S34FS = ((TextBox)e.Row.FindControl("S34FS"));
                //TextBox S36FS = ((TextBox)e.Row.FindControl("S36FS"));
                //TextBox SXSFS = ((TextBox)e.Row.FindControl("SXSFS"));
                //TextBox SSFS = ((TextBox)e.Row.FindControl("SSFS"));
                //TextBox SMFS = ((TextBox)e.Row.FindControl("SMFS"));
                //TextBox SLFS = ((TextBox)e.Row.FindControl("SLFS"));
                //TextBox SXLFS = ((TextBox)e.Row.FindControl("SXLFS"));
                //TextBox SXXLFS = ((TextBox)e.Row.FindControl("SXXLFS"));
                //TextBox S3XLFS = ((TextBox)e.Row.FindControl("S3XLFS"));
                //TextBox S4XLFS = ((TextBox)e.Row.FindControl("S4XLFS"));

                //TextBox S30HS = ((TextBox)e.Row.FindControl("S30HS"));
                //TextBox S32HS = ((TextBox)e.Row.FindControl("S32HS"));
                //TextBox S34HS = ((TextBox)e.Row.FindControl("S34HS"));
                //TextBox S36HS = ((TextBox)e.Row.FindControl("S36HS"));
                //TextBox SXSHS = ((TextBox)e.Row.FindControl("SXSHS"));
                //TextBox SSHS = ((TextBox)e.Row.FindControl("SSHS"));
                //TextBox SMHS = ((TextBox)e.Row.FindControl("SMHS"));
                //TextBox SLHS = ((TextBox)e.Row.FindControl("SLHS"));
                //TextBox SXLHS = ((TextBox)e.Row.FindControl("SXLHS"));
                //TextBox SXXLHS = ((TextBox)e.Row.FindControl("SXXLHS"));
                //TextBox S3XLHS = ((TextBox)e.Row.FindControl("S3XLHS"));
                //TextBox S4XLHS = ((TextBox)e.Row.FindControl("S4XLHS"));

                //TextBox Totalshirt = ((TextBox)e.Row.FindControl("Totalshirt"));

                Q30F = Q30F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S30FS"));
                Q32F = Q32F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S32FS"));
                Q34F = Q34F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S34FS"));
                Q36F = Q36F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S36FS"));
                QXSF = QXSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SXSFS"));
                QSF = QSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SSFS"));
                QMF = QMF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SMFS"));
                QLF = QLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SLFS"));
                QXLF = QXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SXLFS"));
                QXXLF = QXXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SXXLFS"));
                Q3XLF = Q3XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S3XLFS"));
                Q4XLF = Q4XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S4XLFS"));

                Q30H = Q30H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S30HS"));
                Q32H = Q32H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S32HS"));
                Q34H = Q34H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S34HS"));
                Q36H = Q36H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S36HS"));
                QXSH = QXSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SXSHS"));
                QSH = QSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SSHS"));
                QMH = QMH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SMHS"));
                QLH = QLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SLHS"));
                QXLH = QXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SXLHS"));
                QXXLH = QXXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SXXLHS"));
                Q3XLH = Q3XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S3XLHS"));
                Q4XLH = Q4XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "S4XLHS"));

                QttlFH = QttlFH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Totalshirt"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {


                e.Row.Cells[4].Text = Q30F.ToString();
                e.Row.Cells[5].Text = Q32F.ToString();
                e.Row.Cells[6].Text = Q34F.ToString();
                e.Row.Cells[7].Text = Q36F.ToString();
                e.Row.Cells[8].Text = QXSF.ToString();
                e.Row.Cells[9].Text = QSF.ToString();
                e.Row.Cells[10].Text = QMF.ToString();
                e.Row.Cells[11].Text = QLF.ToString();
                e.Row.Cells[12].Text = QXLF.ToString();
                e.Row.Cells[13].Text = QXXLF.ToString();
                e.Row.Cells[14].Text = Q3XLF.ToString();
                e.Row.Cells[15].Text = Q4XLF.ToString();

                e.Row.Cells[16].Text = Q30H.ToString();
                e.Row.Cells[17].Text = Q32H.ToString();
                e.Row.Cells[18].Text = Q34H.ToString();
                e.Row.Cells[19].Text = Q36H.ToString();
                e.Row.Cells[20].Text = QXSH.ToString();
                e.Row.Cells[21].Text = QSH.ToString();
                e.Row.Cells[22].Text = QMH.ToString();
                e.Row.Cells[23].Text = QLH.ToString();
                e.Row.Cells[24].Text = QXLH.ToString();
                e.Row.Cells[25].Text = QXXLH.ToString();
                e.Row.Cells[26].Text = Q3XLH.ToString();
                e.Row.Cells[27].Text = Q4XLH.ToString();

                e.Row.Cells[30].Text = QttlFH.ToString();
                // e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        protected void processall_sampleratio(object sender, EventArgs e)
        {

            for (int vLoop = 0; vLoop < NewSizeRatioGrid.Rows.Count; vLoop++)
            {
                Label Nlblitemname = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblitemname");
                Label Nlbltransid = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlbltransid");

                Label Nlblfitname = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblfitname");
                Label Nlblfitid = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblfitid");

                Label Nlblrequiredmeter = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblrequiredmeter");

                Label Nlblavgmeter = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblavgmeter");

                Label Nlbltotalshirt = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlbltotalshirt");

                Label Nlblprocessshirt = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblprocessshirt");

                Label Nlblprocessmeterratio = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("Nlblprocessmeterratio");


                TextBox txt30fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt30fs");
                TextBox txt32fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt32fs");
                TextBox txt34fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt34fs");
                TextBox txt36fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt36fs");
                TextBox txtxsfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxsfs");
                TextBox txtsfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtsfs");
                TextBox txtmfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtmfs");
                TextBox txtlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtlfs");
                TextBox txtxlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxlfs");
                TextBox txtxxlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxxlfs");
                TextBox txt3xlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt3xlfs");
                TextBox txt4xlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt4xlfs");


                TextBox txt30hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt30hs");
                TextBox txt32hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt32hs");
                TextBox txt34hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt34hs");
                TextBox txt36hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt36hs");
                TextBox txtxshs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxshs");
                TextBox txtshs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtshs");
                TextBox txtmhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtmhs");
                TextBox txtlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtlhs");
                TextBox txtxlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxlhs");
                TextBox txtxxlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxxlhs");
                TextBox txt3xlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt3xlhs");
                TextBox txt4xlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt4xlhs");
                Label NlblSizeshirt = (Label)NewSizeRatioGrid.Rows[vLoop].FindControl("NlblSizeshirt");


                double ratio = Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txtxsfs.Text) + Convert.ToDouble(txtsfs.Text) + Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txt3xlfs.Text) + Convert.ToDouble(txt4xlfs.Text) +
                    Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txtxshs.Text) + Convert.ToDouble(txtshs.Text) + Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txt3xlhs.Text) + Convert.ToDouble(txt4xlhs.Text) + Convert.ToDouble(NlblSizeshirt.Text);

                #region FS PROCESS
                // FS
                double os30fs = 0;
                if (txt30fs.Text != "0")
                {
                    double s30fs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt30fs.Text);
                    if (s30fs > 0.5)
                    {
                        os30fs = Math.Round(Convert.ToDouble(s30fs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os30fs = Math.Floor(Convert.ToDouble(s30fs));
                    }
                }


                double os32fs = 0;
                if (txt32fs.Text != "0")
                {
                    double s32fs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt32fs.Text);
                    if (s32fs > 0.5)
                    {
                        os32fs = Math.Round(Convert.ToDouble(s32fs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os32fs = Math.Floor(Convert.ToDouble(s32fs));
                    }
                }


                double os34fs = 0;
                if (txt34fs.Text != "0")
                {
                    double s34fs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt34fs.Text);
                    if (s34fs > 0.5)
                    {
                        os34fs = Math.Round(Convert.ToDouble(s34fs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os34fs = Math.Floor(Convert.ToDouble(s34fs));
                    }
                }

                double os36fs = 0;
                if (txt36fs.Text != "0")
                {
                    double s36fs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt36fs.Text);
                    if (s36fs > 0.5)
                    {
                        os36fs = Math.Round(Convert.ToDouble(s36fs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os36fs = Math.Floor(Convert.ToDouble(s36fs));
                    }
                }

                double osXSfs = 0;
                if (txtxsfs.Text != "0")
                {
                    double sXSfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxsfs.Text);
                    if (sXSfs > 0.5)
                    {
                        osXSfs = Math.Round(Convert.ToDouble(sXSfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osXSfs = Math.Floor(Convert.ToDouble(sXSfs));
                    }
                }

                double ossfs = 0;
                if (txtsfs.Text != "0")
                {
                    double ssfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtsfs.Text);
                    if (ssfs > 0.5)
                    {
                        ossfs = Math.Round(Convert.ToDouble(ssfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        ossfs = Math.Floor(Convert.ToDouble(ssfs));
                    }
                }

                double osmfs = 0;
                if (txtmfs.Text != "0")
                {
                    double smfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtmfs.Text);
                    if (smfs > 0.5)
                    {
                        osmfs = Math.Round(Convert.ToDouble(smfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osmfs = Math.Floor(Convert.ToDouble(smfs));
                    }
                }

                double oslfs = 0;
                if (txtlfs.Text != "0")
                {
                    double slfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtlfs.Text);
                    if (slfs > 0.5)
                    {
                        oslfs = Math.Round(Convert.ToDouble(slfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        oslfs = Math.Floor(Convert.ToDouble(slfs));
                    }
                }


                double osxlfs = 0;
                if (txtxlfs.Text != "0")
                {

                    double sxlfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxlfs.Text);
                    if (sxlfs > 0.5)
                    {
                        osxlfs = Math.Round(Convert.ToDouble(sxlfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osxlfs = Math.Floor(Convert.ToDouble(sxlfs));
                    }
                }


                double osxxlfs = 0;
                if (txtxxlfs.Text != "0")
                {


                    double sxxlfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxxlfs.Text);
                    if (sxxlfs > 0.5)
                    {
                        osxxlfs = Math.Round(Convert.ToDouble(sxxlfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osxxlfs = Math.Floor(Convert.ToDouble(sxxlfs));
                    }
                }


                double os3xlfs = 0;
                if (txt3xlfs.Text != "0")
                {
                    double s3xlfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt3xlfs.Text);
                    if (s3xlfs > 0.5)
                    {
                        os3xlfs = Math.Round(Convert.ToDouble(s3xlfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os3xlfs = Math.Floor(Convert.ToDouble(s3xlfs));
                    }
                }

                double os4xlfs = 0;
                if (txt4xlfs.Text != "0")
                {
                    double s4xlfs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt4xlfs.Text);
                    if (s4xlfs > 0.5)
                    {
                        os4xlfs = Math.Round(Convert.ToDouble(s4xlfs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os4xlfs = Math.Floor(Convert.ToDouble(s4xlfs));
                    }
                }

                #endregion

                #region HS PROCESS
                //HS
                double os30hs = 0;
                if (txt30hs.Text != "0")
                {
                    double s30hs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt30hs.Text);
                    if (s30hs > 0.5)
                    {
                        os30hs = Math.Round(Convert.ToDouble(s30hs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os30hs = Math.Floor(Convert.ToDouble(s30hs));
                    }
                }


                double os32hs = 0;
                if (txt32hs.Text != "0")
                {
                    double s32hs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt32hs.Text);
                    if (s32hs > 0.5)
                    {
                        os32hs = Math.Round(Convert.ToDouble(s32hs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os32hs = Math.Floor(Convert.ToDouble(s32hs));
                    }
                }


                double os34hs = 0;
                if (txt34hs.Text != "0")
                {
                    double s34hs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt34hs.Text);
                    if (s34hs > 0.5)
                    {
                        os34hs = Math.Round(Convert.ToDouble(s34hs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os34hs = Math.Floor(Convert.ToDouble(s34hs));
                    }
                }

                double os36hs = 0;
                if (txt36hs.Text != "0")
                {
                    double s36hs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt36hs.Text);
                    if (s36hs > 0.5)
                    {
                        os36hs = Math.Round(Convert.ToDouble(s36hs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os36hs = Math.Floor(Convert.ToDouble(s36hs));
                    }
                }

                double osXShs = 0;
                if (txtxshs.Text != "0")
                {
                    double sXShs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxshs.Text);
                    if (sXShs > 0.5)
                    {
                        osXShs = Math.Round(Convert.ToDouble(sXShs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osXShs = Math.Floor(Convert.ToDouble(sXShs));
                    }
                }

                double osshs = 0;
                if (txtshs.Text != "0")
                {
                    double sshs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtshs.Text);
                    if (sshs > 0.5)
                    {
                        osshs = Math.Round(Convert.ToDouble(sshs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osshs = Math.Floor(Convert.ToDouble(sshs));
                    }
                }

                double osmhs = 0;
                if (txtmhs.Text != "0")
                {
                    double smhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtmhs.Text);
                    if (smhs > 0.5)
                    {
                        osmhs = Math.Round(Convert.ToDouble(smhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osmhs = Math.Floor(Convert.ToDouble(smhs));
                    }
                }

                double oslhs = 0;
                if (txtlhs.Text != "0")
                {
                    double slhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtlhs.Text);
                    if (slhs > 0.5)
                    {
                        oslhs = Math.Round(Convert.ToDouble(slhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        oslhs = Math.Floor(Convert.ToDouble(slhs));
                    }
                }


                double osxlhs = 0;
                if (txtxlhs.Text != "0")
                {

                    double sxlhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxlhs.Text);
                    if (sxlhs > 0.5)
                    {
                        osxlhs = Math.Round(Convert.ToDouble(sxlhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osxlhs = Math.Floor(Convert.ToDouble(sxlhs));
                    }
                }


                double osxxlhs = 0;
                if (txtxxlhs.Text != "0")
                {


                    double sxxlhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txtxxlhs.Text);
                    if (sxxlhs > 0.5)
                    {
                        osxxlhs = Math.Round(Convert.ToDouble(sxxlhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        osxxlhs = Math.Floor(Convert.ToDouble(sxxlhs));
                    }
                }


                double os3xlhs = 0;
                if (txt3xlhs.Text != "0")
                {
                    double s3xlhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt3xlhs.Text);
                    if (s3xlhs > 0.5)
                    {
                        os3xlhs = Math.Round(Convert.ToDouble(s3xlhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os3xlhs = Math.Floor(Convert.ToDouble(s3xlhs));
                    }
                }

                double os4xlhs = 0;
                if (txt4xlhs.Text != "0")
                {
                    double s4xlhs = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(txt4xlhs.Text);
                    if (s4xlhs > 0.5)
                    {
                        os4xlhs = Math.Round(Convert.ToDouble(s4xlhs), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        os4xlhs = Math.Floor(Convert.ToDouble(s4xlhs));
                    }
                }

                #endregion

                double ostotsizeshirts = 0;
                if (NlblSizeshirt.Text != "0")
                {
                    double stotsizeshirts = (Convert.ToDouble(Nlbltotalshirt.Text) / ratio) * Convert.ToDouble(NlblSizeshirt.Text);
                    if (stotsizeshirts > 0.5)
                    {
                        ostotsizeshirts = Math.Round(Convert.ToDouble(stotsizeshirts), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        ostotsizeshirts = Math.Floor(Convert.ToDouble(stotsizeshirts));
                    }
                }

                double totalshirts = os30fs + os32fs + os34fs + os36fs + osXSfs + ossfs + osmfs + oslfs + osxlfs + osxxlfs + os3xlfs + os4xlfs +
                    os30hs + os32hs + os34hs + os36hs + osXShs + osshs + osmhs + oslhs + osxlhs + osxxlhs + os3xlhs + os4xlhs + ostotsizeshirts;

                Nlblprocessshirt.Text = Convert.ToInt32(totalshirts).ToString();
                Nlblprocessmeterratio.Text = (Convert.ToDouble(Nlblrequiredmeter.Text) / totalshirts).ToString("0.00");

            }
            ratioprocessall.Enabled = true;
        }
        protected void Ratio_processall(object sender, EventArgs e)
        {
            for (int vLoop = 0; vLoop < NewSizeRatioGrid.Rows.Count; vLoop++)
            {

                // if (vLoop == 0)
                {
                    TextBox Ntxt30fs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt30fs");
                    TextBox Ntxt32fs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt32fs");
                    TextBox Ntxt34fs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt34fs");
                    TextBox Ntxt36fs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt36fs");
                    TextBox Ntxtxsfs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtxsfs");
                    TextBox Ntxtsfs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtsfs");
                    TextBox Ntxtmfs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtmfs");
                    TextBox Ntxtlfs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtlfs");
                    TextBox Ntxtxlfs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtxlfs");
                    TextBox Ntxtxxlfs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtxxlfs");
                    TextBox Ntxt3xlfs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt3xlfs");
                    TextBox Ntxt4xlfs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt4xlfs");


                    TextBox Ntxt30hs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt30hs");
                    TextBox Ntxt32hs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt32hs");
                    TextBox Ntxt34hs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt34hs");
                    TextBox Ntxt36hs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt36hs");
                    TextBox Ntxtxshs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtxshs");
                    TextBox Ntxtshs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtshs");
                    TextBox Ntxtmhs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtmhs");
                    TextBox Ntxtlhs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtlhs");
                    TextBox Ntxtxlhs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtxlhs");
                    TextBox Ntxtxxlhs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxtxxlhs");
                    TextBox Ntxt3xlhs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt3xlhs");
                    TextBox Ntxt4xlhs = (TextBox)NewSizeRatioGrid.Rows[0].FindControl("Ntxt4xlhs");



                    TextBox txt30fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt30fs");
                    TextBox txt32fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt32fs");
                    TextBox txt34fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt34fs");
                    TextBox txt36fs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt36fs");
                    TextBox txtxsfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxsfs");
                    TextBox txtsfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtsfs");
                    TextBox txtmfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtmfs");
                    TextBox txtlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtlfs");
                    TextBox txtxlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxlfs");
                    TextBox txtxxlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxxlfs");
                    TextBox txt3xlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt3xlfs");
                    TextBox txt4xlfs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt4xlfs");


                    TextBox txt30hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt30hs");
                    TextBox txt32hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt32hs");
                    TextBox txt34hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt34hs");
                    TextBox txt36hs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt36hs");
                    TextBox txtxshs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxshs");
                    TextBox txtshs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtshs");
                    TextBox txtmhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtmhs");
                    TextBox txtlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtlhs");
                    TextBox txtxlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxlhs");
                    TextBox txtxxlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxtxxlhs");
                    TextBox txt3xlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt3xlhs");
                    TextBox txt4xlhs = (TextBox)NewSizeRatioGrid.Rows[vLoop].FindControl("Ntxt4xlhs");


                    txt30fs.Text = Ntxt30fs.Text;
                    txt32fs.Text = Ntxt32fs.Text;
                    txt34fs.Text = Ntxt34fs.Text;
                    txt36fs.Text = Ntxt36fs.Text;
                    txtxsfs.Text = Ntxtxsfs.Text;
                    txtsfs.Text = Ntxtsfs.Text;
                    txtmfs.Text = Ntxtmfs.Text;
                    txtlfs.Text = Ntxtlfs.Text;
                    txtxlfs.Text = Ntxtxlfs.Text;
                    txtxxlfs.Text = Ntxtxxlfs.Text;
                    txt3xlfs.Text = Ntxt3xlfs.Text;
                    txt4xlfs.Text = Ntxt4xlfs.Text;

                    txt30hs.Text = Ntxt30hs.Text;
                    txt32hs.Text = Ntxt32hs.Text;
                    txt34hs.Text = Ntxt34hs.Text;
                    txt36hs.Text = Ntxt36hs.Text;
                    txtxshs.Text = Ntxtxshs.Text;
                    txtshs.Text = Ntxtshs.Text;
                    txtmhs.Text = Ntxtmhs.Text;
                    txtlhs.Text = Ntxtlhs.Text;
                    txtxlhs.Text = Ntxtxlhs.Text;
                    txtxxlhs.Text = Ntxtxxlhs.Text;
                    txt3xlhs.Text = Ntxt3xlhs.Text;
                    txt4xlhs.Text = Ntxt4xlhs.Text;
                }
            }

        }
        protected void newfabclick(object sender, EventArgs e)
        {
            double tot = 0;

            ratioprocessall.Enabled = false;
            DataSet dsize = objBs.Getsizetype();
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

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("ItemName");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Itemid");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Fitname");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Fitid");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Givenmeter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Avgmeter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Totalshirt");
            dttt.Columns.Add(dct);

            dct = new DataColumn("RowId");
            dttt.Columns.Add(dct);

            dct = new DataColumn("TotalshirtSize");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);


            DataSet dstd1 = new DataSet();
            DataTable dtddd1 = new DataTable();
            DataRow drNew1;
            DataColumn dct1;
            DataTable dttt1 = new DataTable();

            dct1 = new DataColumn("RowId");
            dttt1.Columns.Add(dct1);

            dct1 = new DataColumn("Size");
            dttt1.Columns.Add(dct1);

            dct1 = new DataColumn("SizeId");
            dttt1.Columns.Add(dct1);

            dct1 = new DataColumn("Qty");
            dttt1.Columns.Add(dct1);

            dstd1.Tables.Add(dttt1);


            if (ddlbrand.SelectedValue == "Select Brand Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Some thing went wrong.Please Contact Administrator.Thank you!!!');", true);
                return;
            }
            else
            {

                DataSet dsizee = objBs.Getfitseize(drpNchkfit.SelectedValue, ddlbrand.SelectedValue);
                if ((dsizee.Tables[0].Rows.Count > 0))
                {
                    //Select the checkboxlist items those values are true in database
                    //Loop through the DataTable
                    // for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                    for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                    {
                        //You need to change this as per your DB Design
                        string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();



                        //if (size == "39FS" || size == "39HS" || size == "44FS" || size == "44HS")
                        //{
                        //}
                        //else
                        {
                            //Find the checkbox list items using FindByValue and select it.
                            chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                        }

                    }
                }
            }

            double r = 0;
            for (int vLoop = 0; vLoop < newgridfablist.Rows.Count; vLoop++)
            {
                TextBox avaliablemeer = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtAvlmeter");
                TextBox reqmeter = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtreqmeter");

                Label fabid = (Label)newgridfablist.Rows[vLoop].FindControl("newfabid");

                Label newfabcode = (Label)newgridfablist.Rows[vLoop].FindControl("newfabcode");

                Label lblinvdate = (Label)newgridfablist.Rows[vLoop].FindControl("lblinvdate");

                System.Web.UI.WebControls.CheckBox dckitem = (System.Web.UI.WebControls.CheckBox)newgridfablist.Rows[vLoop].FindControl("chkitemchecked");
                if (dckitem.Checked == true)
                {
                    DateTime deliverydate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //DateTime lblinvdatee = DateTime.ParseExact(lblinvdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (Convert.ToDateTime(lblinvdate.Text) > deliverydate)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Fabric Invoice Date And Cutting Issue date MisMatch.Please Check this Fabric " + newfabcode.Text + ".Thank you!!!');", true);
                        return;

                    }
                    else
                    {

                    }

                    if (avaliablemeer.Text == "")
                    {
                        avaliablemeer.Text = "0";
                    }
                    if (reqmeter.Text == "")
                    {
                        reqmeter.Text = "0";
                    }

                    if (iid != null)
                    {


                    }
                    else
                    {
                        if (Convert.ToDouble(avaliablemeer.Text) >= Convert.ToDouble(reqmeter.Text))
                        {

                            tot = tot + Convert.ToDouble(reqmeter.Text);


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Meter in greater Than that Avaliable Meter in Row " + vLoop + ".Thank you!!!');", true);
                            return;
                        }
                    }

                    drNew = dttt.NewRow();
                    drNew["ItemName"] = newfabcode.Text;
                    drNew["Itemid"] = fabid.Text;
                    drNew["Fitname"] = drpNchkfit.SelectedItem.Text;
                    drNew["Fitid"] = drpNchkfit.SelectedValue;
                    drNew["Givenmeter"] = reqmeter.Text;
                    drNew["Avgmeter"] = txtavgmeter.Text;
                    drNew["RowId"] = vLoop + 1;
                    drNew["TotalshirtSize"] = "0";
                    double wid = Convert.ToDouble(txtavgmeter.Text);
                    double roundoff = Convert.ToDouble(reqmeter.Text) / wid;
                    if (roundoff > 0.5)
                    {
                        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        r = Math.Floor(Convert.ToDouble(roundoff));
                    }

                    drNew["TotalShirt"] = r.ToString("0.00");


                    dstd.Tables[0].Rows.Add(drNew);

                    DataSet dsBrandSize = objBs.selectallSize_BrandID(ddlbrand.SelectedValue);

                    //int vLoop1 = 0;
                    foreach (DataRow Dr in dsBrandSize.Tables[0].Rows)
                    {
                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = vLoop + 1;
                        drNew1["Size"] = Dr["Size"].ToString();// RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["SizeId"] = Dr["SizeId"].ToString();// RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Qty"] = "";// RowsGVSizeQty[i]["Qty"].ToString();          

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }
                }
            }

            ViewState["NewSizeRatioGrid"] = dttt;

            NewSizeRatioGrid.DataSource = dstd;
            NewSizeRatioGrid.DataBind();

            //DataSet dstd1 = new DataSet();
            //DataTable dtddd1 = new DataTable();
            //DataRow drNew1;
            //DataColumn dct1;
            //DataTable dttt1 = new DataTable();

            //dct1 = new DataColumn("RowId");
            //dttt1.Columns.Add(dct1);

            //dct1 = new DataColumn("Size");
            //dttt1.Columns.Add(dct1);

            //dct1 = new DataColumn("SizeId");
            //dttt1.Columns.Add(dct1);

            //dct1 = new DataColumn("Qty");
            //dttt1.Columns.Add(dct1);

            //dstd1.Tables.Add(dttt1);

            //DataSet dsBrandSize = objBs.selectallSize_BrandID(ddlbrand.SelectedValue);

            //int vLoop1 = 0;
            //foreach (DataRow Dr in dsBrandSize.Tables[0].Rows)
            //{
            //    drNew1 = dttt1.NewRow();

            //    drNew1["RowId"] = vLoop1 + 1;
            //    drNew1["Size"] = Dr["Size"].ToString();// RowsGVSizeQty[i]["Size"].ToString();
            //    drNew1["SizeId"] = Dr["SizeId"].ToString();// RowsGVSizeQty[i]["SizeId"].ToString();
            //    drNew1["Qty"] = "";// RowsGVSizeQty[i]["Qty"].ToString();          

            //    dstd1.Tables[0].Rows.Add(drNew1);
            //    dtddd1 = dstd1.Tables[0];
            //    vLoop1++;

            //}

            ViewState["CurrentTable2"] = dtddd1;

            if (iid != null)
            {
                DataSet dgetprocessratio = objBs.getprecuttingratiowisedetailsTab2(iid);
                if (dgetprocessratio.Tables[0].Rows.Count > 0)
                {
                    for (int Loop1 = 0; Loop1 < NewSizeRatioGrid.Rows.Count; Loop1++)
                    {
                        Label Nlblitemname = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblitemname");
                        Label Nlbltransid = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlbltransid");

                        Label Nlblfitname = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblfitname");
                        Label Nlblfitid = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblfitid");

                        Label Nlblrequiredmeter = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblrequiredmeter");

                        Label Nlblavgmeter = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblavgmeter");


                        // TextBox txtcontrast = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("txtcontrast");


                        TextBox Ntxt30fs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt30fs");
                        TextBox Ntxt32fs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt32fs");


                        TextBox Ntxt34fs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt34fs");
                        TextBox Ntxt36fs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt36fs");


                        TextBox Ntxtxsfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxsfs");
                        TextBox Ntxtsfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtsfs");


                        TextBox Ntxtmfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtmfs");
                        TextBox Ntxtlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtlfs");


                        TextBox Ntxtxlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxlfs");
                        TextBox Ntxtxxlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxxlfs");


                        TextBox Ntxt3xlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt3xlfs");
                        TextBox Ntxt4xlfs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt4xlfs");


                        TextBox Ntxt30hs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt30hs");
                        TextBox Ntxt32hs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt32hs");

                        TextBox Ntxt34hs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt34hs");
                        TextBox Ntxt36hs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt36hs");

                        TextBox Ntxtxshs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxshs");
                        TextBox Ntxtshs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtshs");

                        TextBox Ntxtmhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtmhs");
                        TextBox Ntxtlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtlhs");

                        TextBox Ntxtxlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxlhs");
                        TextBox Ntxtxxlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxtxxlhs");

                        TextBox Ntxt3xlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt3xlhs");
                        TextBox Ntxt4xlhs = (TextBox)NewSizeRatioGrid.Rows[Loop1].FindControl("Ntxt4xlhs");




                        Label Nlblprocessshirt = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblprocessshirt");

                        Label Nlbltotalshirt = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlbltotalshirt");


                        Label Nlblprocessmeterratio = (Label)NewSizeRatioGrid.Rows[Loop1].FindControl("Nlblprocessmeterratio");

                        for (int j = 0; j < dgetprocessratio.Tables[0].Rows.Count; j++)
                        {
                            string invrefno = dgetprocessratio.Tables[0].Rows[j]["invrefno"].ToString();
                            string itemame = dgetprocessratio.Tables[0].Rows[j]["designno"].ToString();
                            string fitid = dgetprocessratio.Tables[0].Rows[j]["fitid"].ToString();
                            string fitname = dgetprocessratio.Tables[0].Rows[j]["fit"].ToString();
                            string givenfab = dgetprocessratio.Tables[0].Rows[j]["Givenfab"].ToString();
                            string avgmeter = dgetprocessratio.Tables[0].Rows[j]["Avgmeter"].ToString();
                            string totalshirt = dgetprocessratio.Tables[0].Rows[j]["totalShirt"].ToString();

                            string S30FS = dgetprocessratio.Tables[0].Rows[j]["30FS"].ToString();
                            string S32FS = dgetprocessratio.Tables[0].Rows[j]["32FS"].ToString();
                            string S34FS = dgetprocessratio.Tables[0].Rows[j]["34FS"].ToString();
                            string S36FS = dgetprocessratio.Tables[0].Rows[j]["36FS"].ToString();
                            string SXSFS = dgetprocessratio.Tables[0].Rows[j]["XSFS"].ToString();
                            string SSFS = dgetprocessratio.Tables[0].Rows[j]["SFS"].ToString();
                            string SMFS = dgetprocessratio.Tables[0].Rows[j]["MFS"].ToString();
                            string SLFS = dgetprocessratio.Tables[0].Rows[j]["LFS"].ToString();
                            string SXLFS = dgetprocessratio.Tables[0].Rows[j]["XLFS"].ToString();
                            string SXXLFS = dgetprocessratio.Tables[0].Rows[j]["XXLFS"].ToString();
                            string S3XLFS = dgetprocessratio.Tables[0].Rows[j]["3XLFS"].ToString();
                            string S4XLFS = dgetprocessratio.Tables[0].Rows[j]["4XLFS"].ToString();
                            string S30HS = dgetprocessratio.Tables[0].Rows[j]["30HS"].ToString();
                            string S32HS = dgetprocessratio.Tables[0].Rows[j]["32HS"].ToString();
                            string S34HS = dgetprocessratio.Tables[0].Rows[j]["34HS"].ToString();
                            string S36HS = dgetprocessratio.Tables[0].Rows[j]["36HS"].ToString();
                            string SXSHS = dgetprocessratio.Tables[0].Rows[j]["XSHS"].ToString();
                            string SSHS = dgetprocessratio.Tables[0].Rows[j]["SHS"].ToString();
                            string SMHS = dgetprocessratio.Tables[0].Rows[j]["MHS"].ToString();
                            string SLHS = dgetprocessratio.Tables[0].Rows[j]["LHS"].ToString();
                            string SXLHS = dgetprocessratio.Tables[0].Rows[j]["XLHS"].ToString();
                            string SXXLHS = dgetprocessratio.Tables[0].Rows[j]["XXLHS"].ToString();
                            string S3XLHS = dgetprocessratio.Tables[0].Rows[j]["3XLHS"].ToString();
                            string S4XLHS = dgetprocessratio.Tables[0].Rows[j]["4XLHS"].ToString();
                            string ProcessShirtt = dgetprocessratio.Tables[0].Rows[j]["ProcessShirt"].ToString();
                            string ProcessMeter = dgetprocessratio.Tables[0].Rows[j]["ProcessMeter"].ToString();

                            if (Nlbltransid.Text == invrefno)
                            {

                                Nlblitemname.Text = itemame;
                                Nlbltransid.Text = invrefno;
                                Nlblfitname.Text = fitname;
                                Nlblfitid.Text = fitid;
                                Nlblrequiredmeter.Text = givenfab;
                                Nlblavgmeter.Text = avgmeter;
                                Nlbltotalshirt.Text = totalshirt;
                                Nlblprocessshirt.Text = ProcessShirtt;
                                Nlblprocessmeterratio.Text = ProcessMeter;

                                Ntxt30fs.Text = S30FS;
                                Ntxt32fs.Text = S32FS;


                                Ntxt34fs.Text = S34FS;
                                Ntxt36fs.Text = S36FS;


                                Ntxtxsfs.Text = SXSFS;
                                Ntxtsfs.Text = SSFS;


                                Ntxtmfs.Text = SMFS;
                                Ntxtlfs.Text = SLFS;


                                Ntxtxlfs.Text = SXLFS;
                                Ntxtxxlfs.Text = SXXLFS;


                                Ntxt3xlfs.Text = S3XLFS;
                                Ntxt4xlfs.Text = S4XLFS;


                                Ntxt30hs.Text = S30HS;
                                Ntxt32hs.Text = S32HS;

                                Ntxt34hs.Text = S34HS;
                                Ntxt36hs.Text = S36HS;

                                Ntxtxshs.Text = SXSHS;
                                Ntxtshs.Text = SSHS;

                                Ntxtmhs.Text = SMHS;
                                Ntxtlhs.Text = SXLHS;

                                Ntxtxlhs.Text = SXLHS;
                                Ntxtxxlhs.Text = SXXLHS;

                                Ntxt3xlhs.Text = S3XLHS;
                                Ntxt4xlhs.Text = S4XLHS;
                            }
                        }


                    }
                    ProcessShirt(sender, e);
                }

            }

            //double r =0;
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{


            //    Label Nlblitemname = (Label)gvcustomerorder.Rows[vLoop].FindControl("Nlblitemname");
            //    Label Nlbltransid = (Label)gvcustomerorder.Rows[vLoop].FindControl("Nlbltransid");
            //    Label Nlblfitname = (Label)gvcustomerorder.Rows[vLoop].FindControl("Nlblfitname");
            //    Label Nlblfitid = (Label)gvcustomerorder.Rows[vLoop].FindControl("Nlblfitid");
            //    Label Nlblrequiredmeter = (Label)gvcustomerorder.Rows[vLoop].FindControl("Nlblrequiredmeter");

            //    Label Nlblavgmeter = (Label)gvcustomerorder.Rows[vLoop].FindControl("Nlblavgmeter");

            //    Label Nlbltotalshirt = (Label)gvcustomerorder.Rows[vLoop].FindControl("Nlbltotalshirt");







            //    Nlblitemname.Text = dstd.Tables[0].Rows[vLoop]["ItemName"].ToString();

            //    Nlbltransid.Text = dstd.Tables[0].Rows[vLoop]["Itemid"].ToString();
            //    Nlblfitname.Text = dstd.Tables[0].Rows[vLoop]["Fitname"].ToString();
            //    Nlblfitid.Text = dstd.Tables[0].Rows[vLoop]["Fitid"].ToString();

            //    Nlblrequiredmeter.Text = dstd.Tables[0].Rows[vLoop]["Givenmeter"].ToString();
            //    Nlblavgmeter.Text = dstd.Tables[0].Rows[vLoop]["Avgmeter"].ToString();

            //    double wid =Convert.ToDouble(Nlblavgmeter.Text);
            //     double roundoff = Convert.ToDouble(Nlblrequiredmeter.Text) / wid;
            //    if (roundoff > 0.5)
            //    {
            //        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        r = Math.Floor(Convert.ToDouble(roundoff));
            //    }

            //    Nlbltotalshirt.Text = r.ToString("0.00");

            //}

            if (chkSizes.SelectedIndex >= 0)
            {
                NewSizeRatioGrid.Columns[4].Visible = false; //30FS
                NewSizeRatioGrid.Columns[5].Visible = false; //32FS

                NewSizeRatioGrid.Columns[6].Visible = false;//34Fs
                NewSizeRatioGrid.Columns[7].Visible = false;//36Fs

                NewSizeRatioGrid.Columns[8].Visible = false; //XSFS
                NewSizeRatioGrid.Columns[9].Visible = false; //SFS

                NewSizeRatioGrid.Columns[10].Visible = false; //MFS
                NewSizeRatioGrid.Columns[11].Visible = false; //LFS

                NewSizeRatioGrid.Columns[12].Visible = false; //XLFS
                NewSizeRatioGrid.Columns[13].Visible = false; //xxlFS

                NewSizeRatioGrid.Columns[14].Visible = false; //3xlHS
                NewSizeRatioGrid.Columns[15].Visible = false; //4xlHS

                NewSizeRatioGrid.Columns[16].Visible = false; //30HS

                NewSizeRatioGrid.Columns[17].Visible = false; //32HS

                NewSizeRatioGrid.Columns[18].Visible = false; //34HS
                NewSizeRatioGrid.Columns[19].Visible = false; //36HS

                NewSizeRatioGrid.Columns[20].Visible = false; //XSHS
                NewSizeRatioGrid.Columns[21].Visible = false; //SHS

                NewSizeRatioGrid.Columns[22].Visible = false; //MHS
                NewSizeRatioGrid.Columns[23].Visible = false; //LHS

                NewSizeRatioGrid.Columns[24].Visible = false; //XLHS
                NewSizeRatioGrid.Columns[25].Visible = false; //XXLHS

                NewSizeRatioGrid.Columns[26].Visible = false; //3XLHS
                NewSizeRatioGrid.Columns[27].Visible = false; //4XLHS




                int lop = 0;
                //Loop through each item of checkboxlist
                foreach (System.Web.UI.WebControls.ListItem item in chkSizes.Items)
                {
                    //check if item selected

                    if (item.Selected)
                    {

                        {
                            if (item.Text == "30FS")
                            {
                                NewSizeRatioGrid.Columns[4].Visible = true;
                            }
                            if (item.Text == "32FS")
                            {
                                NewSizeRatioGrid.Columns[5].Visible = true;
                            }
                            if (item.Text == "34FS")
                            {
                                NewSizeRatioGrid.Columns[6].Visible = true;
                            }
                            if (item.Text == "36FS")
                            {
                                NewSizeRatioGrid.Columns[7].Visible = true;
                            }
                            if (item.Text == "XSFS")
                            {
                                NewSizeRatioGrid.Columns[8].Visible = true;
                            }
                            if (item.Text == "SFS")
                            {
                                NewSizeRatioGrid.Columns[9].Visible = true;
                            }
                            if (item.Text == "MFS")
                            {
                                NewSizeRatioGrid.Columns[10].Visible = true;
                            }
                            if (item.Text == "LFS")
                            {
                                NewSizeRatioGrid.Columns[11].Visible = true;
                            }
                            if (item.Text == "XLFS")
                            {
                                NewSizeRatioGrid.Columns[12].Visible = true;
                            }
                            if (item.Text == "XXLFS")
                            {
                                NewSizeRatioGrid.Columns[13].Visible = true;
                            }
                            if (item.Text == "3XLFS")
                            {
                                NewSizeRatioGrid.Columns[14].Visible = true;
                            }
                            if (item.Text == "4XLFS")
                            {
                                NewSizeRatioGrid.Columns[15].Visible = true;
                            }


                            // FOR HS

                            if (item.Text == "30HS")
                            {
                                NewSizeRatioGrid.Columns[16].Visible = true;
                            }

                            if (item.Text == "32HS")
                            {
                                NewSizeRatioGrid.Columns[17].Visible = true;
                            }

                            if (item.Text == "34HS")
                            {
                                NewSizeRatioGrid.Columns[18].Visible = true;
                            }

                            if (item.Text == "36HS")
                            {
                                NewSizeRatioGrid.Columns[19].Visible = true;

                            }

                            if (item.Text == "XSHS")
                            {
                                NewSizeRatioGrid.Columns[20].Visible = true;
                            }

                            if (item.Text == "SHS")
                            {
                                NewSizeRatioGrid.Columns[21].Visible = true;
                            }

                            if (item.Text == "MHS")
                            {
                                NewSizeRatioGrid.Columns[22].Visible = true;
                            }

                            if (item.Text == "LHS")
                            {
                                NewSizeRatioGrid.Columns[23].Visible = true;
                            }

                            if (item.Text == "XLHS")
                            {
                                NewSizeRatioGrid.Columns[24].Visible = true;
                            }

                            if (item.Text == "XXLHS")
                            {
                                NewSizeRatioGrid.Columns[25].Visible = true;
                            }

                            if (item.Text == "3XLHS")
                            {
                                NewSizeRatioGrid.Columns[26].Visible = true;
                            }

                            if (item.Text == "4XLHS")
                            {
                                NewSizeRatioGrid.Columns[27].Visible = true;
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
                NewSizeRatioGrid.Columns[4].Visible = false; //30FS
                NewSizeRatioGrid.Columns[5].Visible = false; //32FS

                NewSizeRatioGrid.Columns[6].Visible = false;//34Fs
                NewSizeRatioGrid.Columns[7].Visible = false;//36Fs

                NewSizeRatioGrid.Columns[8].Visible = false; //XSFS
                NewSizeRatioGrid.Columns[9].Visible = false; //SFS

                NewSizeRatioGrid.Columns[10].Visible = false; //MFS
                NewSizeRatioGrid.Columns[11].Visible = false; //LFS

                NewSizeRatioGrid.Columns[12].Visible = false; //XLFS
                NewSizeRatioGrid.Columns[13].Visible = false; //xxlFS

                NewSizeRatioGrid.Columns[14].Visible = false; //3xlHS
                NewSizeRatioGrid.Columns[15].Visible = false; //4xlHS

                NewSizeRatioGrid.Columns[16].Visible = false; //30HS

                NewSizeRatioGrid.Columns[17].Visible = false; //32HS

                NewSizeRatioGrid.Columns[18].Visible = false; //34HS
                NewSizeRatioGrid.Columns[19].Visible = false; //36HS

                NewSizeRatioGrid.Columns[20].Visible = false; //XSHS
                NewSizeRatioGrid.Columns[21].Visible = false; //SHS

                NewSizeRatioGrid.Columns[22].Visible = false; //MHS
                NewSizeRatioGrid.Columns[23].Visible = false; //LHS

                NewSizeRatioGrid.Columns[24].Visible = false; //XLHS
                NewSizeRatioGrid.Columns[25].Visible = false; //XXLHS

                NewSizeRatioGrid.Columns[26].Visible = false; //3XLHS
                NewSizeRatioGrid.Columns[27].Visible = false; //4XLHS

            }

            txtReqMtr.Text = tot.ToString("f2");
            Ntxtremmeter.Text = tot.ToString("f2");
            txtavamet1.Text = tot.ToString("f2");
            reqchanged(sender, e);
            UpdatePanel5.Update();
        }

        private bool IsParticipantExistsN(string val)
        {
            bool exists = false;
            //Loop through each item in selected participant checkboxlist
            foreach (System.Web.UI.WebControls.ListItem item in contrastwidth.Items)
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


        protected void contrastwidthselect(object sender, EventArgs e)
        {
            DataSet dsmer = new DataSet();
            DataSet dcur = new DataSet();




            if (contrastwidth.SelectedIndex >= 0)
            {

                foreach (System.Web.UI.WebControls.ListItem item in contrastwidth.Items)
                {
                    //check if item selected
                    if (item.Selected)
                    {
                        // Add participant to the selected list if not alreay added
                        if (!IsParticipantExistsN(item.Value))
                        {

                        }
                        else
                        {
                            if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
                            {
                                dcur = objBs.getnewsupplierforcut(item.Value);
                            }
                            else
                            {
                                dcur = objBs.getnewsupplierforcutbc(item.Value, ddlBottilot.SelectedValue, drpbranch.SelectedValue);
                            }

                            if (dcur != null)
                            {
                                if (dcur.Tables[0].Rows.Count > 0)
                                {
                                    dsmer.Merge(dcur);
                                }
                            }
                        }
                    }
                }
                if (dsmer != null)
                {
                    if (dsmer.Tables.Count > 0)
                    {

                        if (dsmer.Tables[0].Rows.Count > 0)
                        {
                            contrastfabric.DataSource = dsmer.Tables[0];
                            contrastfabric.DataTextField = "fabno";
                            contrastfabric.DataValueField = "fabid";
                            contrastfabric.DataBind();
                        }
                        else
                        {
                            contrastfabric.DataSource = null;
                            contrastfabric.DataTextField = "fabno";
                            contrastfabric.DataValueField = "fabid";
                            contrastfabric.DataBind();
                            contrastfabric.ClearSelection();
                            contrastfabric.Items.Clear();

                        }
                    }
                    else
                    {
                        contrastfabric.DataSource = null;
                        contrastfabric.DataTextField = "fabno";
                        contrastfabric.DataValueField = "fabid";
                        contrastfabric.DataBind();
                        contrastfabric.ClearSelection();
                        contrastfabric.Items.Clear();
                    }

                }
                else
                {
                    contrastfabric.DataSource = null;
                    contrastfabric.DataBind();
                    contrastfabric.ClearSelection();

                }




            }
            else
            {
                contrastfabric.DataSource = null;
                contrastfabric.DataBind();
                contrastfabric.ClearSelection();
                contrastfabric.Items.Clear();


            }

        }

        protected void oldcontrastwidthselect(object sender, EventArgs e)
        {
            DataSet dsmer = new DataSet();
            DataSet dcur = new DataSet();




            if (contrastwidth.SelectedIndex >= 0)
            {

                foreach (System.Web.UI.WebControls.ListItem item in contrastwidth.Items)
                {
                    //check if item selected
                    if (item.Selected)
                    {
                        // Add participant to the selected list if not alreay added
                        if (!IsParticipantExistsN(item.Value))
                        {

                        }
                        else
                        {
                            dcur = objBs.getnewsupplierforcut(item.Value);
                            if (dcur != null)
                            {
                                if (dcur.Tables[0].Rows.Count > 0)
                                {
                                    dsmer.Merge(dcur);
                                }
                            }
                        }
                    }
                }
                if (dsmer != null)
                {
                    if (dsmer.Tables.Count > 0)
                    {

                        if (dsmer.Tables[0].Rows.Count > 0)
                        {
                            contrastfabric.DataSource = dsmer.Tables[0];
                            contrastfabric.DataTextField = "fabno";
                            contrastfabric.DataValueField = "fabid";
                            contrastfabric.DataBind();
                        }
                        else
                        {
                            contrastfabric.DataSource = null;
                            contrastfabric.DataTextField = "fabno";
                            contrastfabric.DataValueField = "fabid";
                            contrastfabric.DataBind();
                            contrastfabric.ClearSelection();
                            contrastfabric.Items.Clear();

                        }
                    }
                    else
                    {
                        contrastfabric.DataSource = null;
                        contrastfabric.DataTextField = "fabno";
                        contrastfabric.DataValueField = "fabid";
                        contrastfabric.DataBind();
                        contrastfabric.ClearSelection();
                        contrastfabric.Items.Clear();
                    }

                }
                else
                {
                    contrastfabric.DataSource = null;
                    contrastfabric.DataBind();
                    contrastfabric.ClearSelection();

                }




            }
            else
            {
                contrastfabric.DataSource = null;
                contrastfabric.DataBind();
                contrastfabric.ClearSelection();
                contrastfabric.Items.Clear();


            }

        }

        protected void olditemfablist(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            string cond = "";
            string cond1 = "";


            if (chkitem.SelectedIndex >= 0)
            {
                foreach (System.Web.UI.WebControls.ListItem listItem in contrastwidth.Items)
                {
                    if (listItem.Text != "All")
                    {
                        if (listItem.Selected)
                        {
                            cond1 += " width='" + listItem.Value + "' ,";
                        }
                    }
                }
                cond1 = cond1.TrimEnd(',');
                cond1 = cond1.Replace(",", "or");

                foreach (System.Web.UI.WebControls.ListItem item in chkitem.Items)
                {
                    //check if item selected
                    if (item.Selected)
                    {

                        {
                            if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
                            {
                                dteo = objBs.getcutlistdesignfablistNew(item.Value, radbtnshirttype.SelectedValue);
                            }
                            else
                            {
                                dteo = objBs.getcutlistdesignfablistNewbc1(item.Value, radbtnshirttype.SelectedValue);
                            }

                            // dteo = objBs.getcutlistdesignfablistNew(item.Value, radbtnshirttype.SelectedValue);
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

                        contrastgridfab.DataSource = dssmer;
                        contrastgridfab.DataBind();

                    }
                    else
                    {
                        contrastgridfab.DataSource = null;
                        contrastgridfab.DataBind();
                        //  dddldesign.ClearSelection();
                        // dddldesign.Items.Insert(0, "Select Design");

                    }

                }
                else
                {
                    contrastgridfab.DataSource = null;
                    contrastgridfab.DataBind();

                }

            }
            else
            {
                contrastgridfab.DataSource = null;
                contrastgridfab.DataBind();
            }

        }

        protected void itemfablist(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            string cond = "";
            string cond1 = "";


            if (chkitem.SelectedIndex >= 0)
            {
                foreach (System.Web.UI.WebControls.ListItem listItem in contrastwidth.Items)
                {
                    if (listItem.Text != "All")
                    {
                        if (listItem.Selected)
                        {
                            cond1 += " width='" + listItem.Value + "' ,";
                        }
                    }
                }
                cond1 = cond1.TrimEnd(',');
                cond1 = cond1.Replace(",", "or");

                foreach (System.Web.UI.WebControls.ListItem item in chkitem.Items)
                {
                    //check if item selected
                    if (item.Selected)
                    {

                        {
                            if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
                            {
                                dteo = objBs.getcutlistdesignfablistNew(item.Value, radbtnshirttype.SelectedValue);
                            }
                            //  else
                            {
                                // dteo = objBs.getcutlistdesignfablistNewbc(item.Value, radbtnshirttype.SelectedValue, ddlBottilot.SelectedValue);
                            }

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

                        contrastgridfab.DataSource = dssmer;
                        contrastgridfab.DataBind();

                    }
                    else
                    {
                        contrastgridfab.DataSource = null;
                        contrastgridfab.DataBind();
                        //  dddldesign.ClearSelection();
                        // dddldesign.Items.Insert(0, "Select Design");

                    }

                }
                else
                {
                    contrastgridfab.DataSource = null;
                    contrastgridfab.DataBind();

                }

            }
            else
            {
                contrastgridfab.DataSource = null;
                contrastgridfab.DataBind();
            }

            for (int vLoop = 0; vLoop < contrastgridfab.Rows.Count; vLoop++)
            {

                Label newfabcode = (Label)contrastgridfab.Rows[vLoop].FindControl("newfabcode");
                Label lblinvdate = (Label)contrastgridfab.Rows[vLoop].FindControl("lblinvdate");

                DateTime date = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                // DateTime invdate = DateTime.ParseExact(lblinvdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (Convert.ToDateTime(lblinvdate.Text) > Convert.ToDateTime(date))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Contrast Fabric Invoice Date And Cutting Issue date MisMatch.Please Check this Fabric " + newfabcode.Text + ".Thank you!!!');", true);
                    return;

                }
                else
                {

                }
            }

        }

        protected void contfablist(object sender, EventArgs e)
        {
            {
                string cond1 = "";
                DataSet dteo = new DataSet();
                DataSet dssmer = new DataSet();

                if (contrastfabric.SelectedIndex >= 0)
                {

                    foreach (System.Web.UI.WebControls.ListItem listItem in contrastwidth.Items)
                    {
                        if (listItem.Text != "All")
                        {
                            if (listItem.Selected)
                            {
                                cond1 += " width='" + listItem.Value + "' ,";
                            }
                        }
                    }
                    cond1 = cond1.TrimEnd(',');
                    cond1 = cond1.Replace(",", "or");


                    foreach (System.Web.UI.WebControls.ListItem item in contrastfabric.Items)
                    {




                        //check if item selected
                        if (item.Selected)
                        {
                            // Add participant to the selected list if not alreay added
                            //if (!IsParticipantExists(item.Value))
                            //{

                            //}
                            //else
                            {
                                if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
                                {
                                    dteo = objBs.getcutlistdesignfablist(item.Value, cond1, "2");
                                }
                                else
                                {
                                    dteo = objBs.getcutlistdesignfablistbc(item.Value, cond1, "2", ddlBottilot.SelectedValue);
                                }

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
                            chkitem.DataSource = dssmer;
                            chkitem.DataTextField = "design";
                            chkitem.DataValueField = "transid";
                            chkitem.DataBind();

                            //newgridfablist.DataSource = dssmer;
                            //newgridfablist.DataBind();

                        }
                        else
                        {

                            chkitem.DataSource = null;
                            chkitem.DataBind();

                            //newgridfablist.DataSource = null;
                            //newgridfablist.DataBind();
                            //  dddldesign.ClearSelection();
                            // dddldesign.Items.Insert(0, "Select Design");

                        }

                    }
                    else
                    {
                        chkitem.DataSource = null;
                        chkitem.DataBind();
                        chkitem.ClearSelection();

                    }
                }
                else
                {
                    chkitem.DataSource = null;
                    chkitem.DataBind();
                    chkitem.ClearSelection();
                    chkitem.Items.Clear();
                }

            }

        }


        protected void oldcontfablist(object sender, EventArgs e)
        {
            {
                string cond1 = "";
                DataSet dteo = new DataSet();
                DataSet dssmer = new DataSet();

                if (contrastfabric.SelectedIndex >= 0)
                {

                    foreach (System.Web.UI.WebControls.ListItem listItem in contrastwidth.Items)
                    {
                        if (listItem.Text != "All")
                        {
                            if (listItem.Selected)
                            {
                                cond1 += " width='" + listItem.Value + "' ,";
                            }
                        }
                    }
                    cond1 = cond1.TrimEnd(',');
                    cond1 = cond1.Replace(",", "or");


                    foreach (System.Web.UI.WebControls.ListItem item in contrastfabric.Items)
                    {




                        //check if item selected
                        if (item.Selected)
                        {
                            // Add participant to the selected list if not alreay added
                            //if (!IsParticipantExists(item.Value))
                            //{

                            //}
                            //else
                            {
                                dteo = objBs.getcutlistdesignfablist(item.Value, cond1, "2");
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
                            chkitem.DataSource = dssmer;
                            chkitem.DataTextField = "design";
                            chkitem.DataValueField = "transid";
                            chkitem.DataBind();

                            //newgridfablist.DataSource = dssmer;
                            //newgridfablist.DataBind();

                        }
                        else
                        {

                            chkitem.DataSource = null;
                            chkitem.DataBind();

                            //newgridfablist.DataSource = null;
                            //newgridfablist.DataBind();
                            //  dddldesign.ClearSelection();
                            // dddldesign.Items.Insert(0, "Select Design");

                        }

                    }
                    else
                    {
                        chkitem.DataSource = null;
                        chkitem.DataBind();
                        chkitem.ClearSelection();

                    }
                }
                else
                {
                    chkitem.DataSource = null;
                    chkitem.DataBind();
                    chkitem.ClearSelection();
                    chkitem.Items.Clear();
                }
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
                    foreach (System.Web.UI.WebControls.ListItem item in chkinvno.Items)
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
                                if (ddlBottilot.SelectedValue == "Select LotNo" || ddlBottilot.SelectedValue == "LotNo" || ddlBottilot.SelectedValue == "")
                                {
                                    dteo = objBs.getcutlistdesign(item.Value, drpwidth.SelectedValue);
                                }
                                else
                                {
                                    dteo = objBs.getcutlistdesignbc(item.Value, drpwidth.SelectedValue);
                                }

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
                            // ViewState["MyDataSet"] = dssmer;
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

                    foreach (System.Web.UI.WebControls.ListItem listItem in chkinvno.Items)
                    {
                        if (listItem.Text != "All")
                        {
                            if (listItem.Selected)
                            {
                                cond1 += " pogrnid='" + listItem.Value + "' ,";
                            }
                        }
                    }
                    cond1 = cond1.TrimEnd(',');
                    cond1 = cond1.Replace(",", "or");

                    if (cond1 != "")
                    {
                        DataSet dminmax1 = objBs.getcutlistdesignforminandmaxadditionNEW(cond1, drpwidth.SelectedValue);
                        if (dminmax1.Tables[0].Rows.Count > 0)
                        {
                            txtAvailableMtr.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                            txtReqMtr.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                            Ntxtremmeter.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                            //  txtavamet1.Text = Convert.ToDouble(dminmax1.Tables[0].Rows[0]["tot"]).ToString("N");
                        }
                    }
                    else
                    {
                        txtAvailableMtr.Text = "0";
                        txtReqMtr.Text = "0";
                        txtavamet1.Text = "0";
                        Ntxtremmeter.Text = "0";
                    }

                    //reqchanged(sender, e);

                    foreach (System.Web.UI.WebControls.ListItem item in chkinvno.Items)
                    {
                        //check if item selected
                        if (item.Selected)
                        {
                            // Add participant to the selected list if not alreay added
                            //if (!IsParticipantExists(item.Value))
                            //{

                            //}
                            //else
                            {
                                dteo = objBs.getcutlistdesignNEW(item.Value, drpwidth.SelectedValue);
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

                            newgridfablist.DataSource = dssmer;
                            newgridfablist.DataBind();

                        }
                        else
                        {
                            newgridfablist.DataSource = null;
                            newgridfablist.DataBind();
                            UpdatePanel1.Update();
                            //  dddldesign.ClearSelection();
                            // dddldesign.Items.Insert(0, "Select Design");

                        }

                    }
                    else
                    {
                        newgridfablist.DataSource = null;
                        newgridfablist.DataBind();
                        UpdatePanel1.Update();

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
                txtAvailableMtr.Text = "0";
                txtReqMtr.Text = "0";
                txtavamet1.Text = "0";
                Ntxtremmeter.Text = "0";
                // chkinvno.Enabled = false;

            }


            //foreach (ListItem listItem in chkinvno.Items)
            //{
            //    if (listItem.Text != "All")
            //    {
            //        if (listItem.Selected)
            //        {
            //            cond += " Fabid='" + listItem.Value + "' ,";
            //        }
            //    }
            //}
            //cond = cond.TrimEnd(',');
            //cond = cond.Replace(",", "or");

            //if (cond != "")
            //{
            //    DataSet dminmax = objBs.getcutlistdesignforminandmax(cond, drpwidth.SelectedValue);
            //    if (dminmax.Tables[0].Rows.Count > 0)
            //    {
            //        lblmin.Text = Convert.ToDouble(dminmax.Tables[0].Rows[0]["mini"]).ToString("N");
            //        lblmax.Text = Convert.ToDouble(dminmax.Tables[0].Rows[0]["maxx"]).ToString("N");
            //    }
            //}
            //else
            //{
            lblmin.Text = "0.00";
            lblmax.Text = "0.00";
            //}

            UpdatePanel1.Update();
        }

        private bool IsParticipantExists(string val)
        {
            bool exists = false;
            //Loop through each item in selected participant checkboxlist
            foreach (System.Web.UI.WebControls.ListItem item in drpNchkfit.Items)
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
            if (chkcust.SelectedIndex >= 0)
            {

                int lop = 0;
                //Loop through each item of checkboxlist
                foreach (System.Web.UI.WebControls.ListItem item in chkcust.Items)
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

                for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                {
                    DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");
                    if (dssmer.Tables[0].Rows.Count > 0)
                    {
                        dparty.DataSource = dssmer.Tables[0];
                        dparty.DataTextField = "LedgerName";
                        dparty.DataValueField = "LedgerID";
                        dparty.DataBind();
                        dparty.Items.Insert(0, "Select Party Name");
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

            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            string cond = "";
            string cond1 = "";
            DataSet getavgmeter = new DataSet();
            double avgtot = 0;
            //  dteo = objBs.getjobcardlistdesign(CheckBoxList1.SelectedValue);



            if (drpNchkfit.SelectedIndex >= 0)
            {
                if (radcuttype.SelectedValue == "2")
                {
                    // CheckBoxList2.Enabled = true;
                    //Loop through each item of checkboxlist
                    foreach (System.Web.UI.WebControls.ListItem item in drpNchkfit.Items)
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
                                dteo = objBs.GetFitforsingle(item.Value);
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
                    int numSelected = 0;
                    foreach (System.Web.UI.WebControls.ListItem item1 in drpNchkfit.Items)
                    {
                        //check if item selected
                        if (item1.Selected)
                        {

                            numSelected = numSelected + 1;

                            // Add participant to the selected list if not alreay added
                            if (!IsParticipantExists(item1.Value))
                            {

                            }
                            else
                            {
                                //getavgmeter = objBs.getwidthnewprocessprecutting(drpwidth.SelectedItem.Text, item1.Value);
                                getavgmeter = objBs.getwidthnewprocessprecuttingNEW(drpwidth.SelectedValue, item1.Value);
                                if (getavgmeter.Tables[0].Rows.Count > 0)
                                {
                                    avgtot = avgtot + Convert.ToDouble(getavgmeter.Tables[0].Rows[0]["w"]);
                                }
                            }
                        }
                    }

                    txtavgmeter.Text = (avgtot / Convert.ToDouble(numSelected)).ToString("0.00");

                    if (dssmer != null)
                    {
                        if (dssmer.Tables[0].Rows.Count > 0)
                        {
                            //CheckBoxList2.DataSource = dssmer;
                            //CheckBoxList2.DataTextField = "Design";
                            //CheckBoxList2.DataValueField = "id";
                            //CheckBoxList2.DataBind();

                            drpFit.DataSource = dssmer;
                            drpFit.DataTextField = "Fit";
                            drpFit.DataValueField = "FitID";
                            drpFit.DataBind();
                            drpFit.Items.Insert(0, "Select Fit");
                            ViewState["MyDataSet"] = dssmer;
                        }
                        else
                        {
                            drpFit.DataSource = null;
                            drpFit.DataBind();
                            drpFit.ClearSelection();
                            // dddldesign.Items.Insert(0, "Select Design");

                        }

                    }
                    else
                    {
                        drpFit.DataSource = null;
                        drpFit.DataBind();
                        drpFit.ClearSelection();

                    }
                }
            }


            //if (radcuttype.SelectedValue == "1")
            //{
            //    if (radbtn.SelectedValue == "1")
            //    {
            //        if (Sddrrpfit.SelectedValue == "Select Fit")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Fit Label');", true);
            //            return;
            //        }
            //        else
            //        {

            //        }

            //        DataSet dsFit = objBs.GetFitforsingle(Sddrrpfit.SelectedValue);
            //        if (dsFit != null)
            //        {
            //            if (dsFit.Tables[0].Rows.Count > 0)
            //            {

            //                drpFit.DataSource = dsFit.Tables[0];
            //                drpFit.DataTextField = "Fit";
            //                drpFit.DataValueField = "FitID";
            //                drpFit.DataBind();

            //            }
            //        }

            //    }
            //}
            //else if (radcuttype.SelectedValue == "2")
            //{
            //    double r = 0.00;
            //    double rr = 0.00;
            //    double rb = 0.00;
            //    double rr1 = 0.00;
            //    double rb1 = 0.00;
            //    string width = string.Empty;
            //    double wid = 0;
            //    DataSet dsFit = objBs.GetFitforsingle(Sddrrpfit.SelectedValue);
            //    if (dsFit != null)
            //    {
            //        if (dsFit.Tables[0].Rows.Count > 0)
            //        {

            //            drpFit.DataSource = dsFit.Tables[0];
            //            drpFit.DataTextField = "Fit";
            //            drpFit.DataValueField = "FitID";
            //            drpFit.DataBind();

            //        }
            //    }
            //    if (drpFit.SelectedValue == "3")
            //    {
            //        wid = Convert.ToDouble(txtsharp.Text);
            //    }
            //    else
            //    {
            //        wid = Convert.ToDouble(txtexec.Text);
            //    }

            //    double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
            //    if (roundoff > 0.5)
            //    {
            //        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        r = Math.Floor(Convert.ToDouble(roundoff));
            //    }


            //    txtNoofShirts.Text = r.ToString();
            //    txtReqNoShirts.Text = r.ToString();



            //    rr = ((r * 15) / 100);
            //    if (rr > 0.5)
            //    {
            //        rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        rb = Math.Floor(Convert.ToDouble(rr));
            //    }
            //    txtextrashirt.Text = rb.ToString();

            //    rr1 = ((r * 2) / 100);
            //    if (rr1 > 0.5)
            //    {
            //        rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        rb1 = Math.Floor(Convert.ToDouble(rr1));
            //    }
            //    txtminshirt.Text = rb1.ToString();




            //}

        }

        protected void Sfitchaged1(object sender, EventArgs e)
        {

            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            string cond = "";
            string cond1 = "";
            DataSet getavgmeter = new DataSet();
            double avgtot = 0;
            //  dteo = objBs.getjobcardlistdesign(CheckBoxList1.SelectedValue);



            if (drpNchkfit.SelectedIndex >= 0)
            {
                if (radcuttype.SelectedValue == "2")
                {
                    // CheckBoxList2.Enabled = true;
                    //Loop through each item of checkboxlist
                    foreach (System.Web.UI.WebControls.ListItem item in drpNchkfit.Items)
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
                                dteo = objBs.GetFitforsingle(item.Value);
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
                    int numSelected = 0;
                    foreach (System.Web.UI.WebControls.ListItem item1 in drpNchkfit.Items)
                    {
                        //check if item selected
                        if (item1.Selected)
                        {

                            numSelected = numSelected + 1;

                            // Add participant to the selected list if not alreay added
                            if (!IsParticipantExists(item1.Value))
                            {

                            }
                            else
                            {
                                if (drpitemtype.SelectedValue == "Select Item")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Item. else Please Contact Administrator.Thank you!!!');", true);
                                    return;
                                }

                                getavgmeter = objBs.getwidthnewprocessprecutting(drpitemtype.SelectedValue);
                                if (getavgmeter.Tables[0].Rows.Count > 0)
                                {
                                    avgtot = avgtot + Convert.ToDouble(getavgmeter.Tables[0].Rows[0]["w"]);
                                }
                            }
                        }
                    }

                    // txtavgmeter.Text = (avgtot / Convert.ToDouble(numSelected)).ToString("0.000");

                    if (dssmer != null)
                    {
                        if (dssmer.Tables[0].Rows.Count > 0)
                        {
                            //CheckBoxList2.DataSource = dssmer;
                            //CheckBoxList2.DataTextField = "Design";
                            //CheckBoxList2.DataValueField = "id";
                            //CheckBoxList2.DataBind();

                            drpFit.DataSource = dssmer;
                            drpFit.DataTextField = "Fit";
                            drpFit.DataValueField = "FitID";
                            drpFit.DataBind();
                            drpFit.Items.Insert(0, "Select Fit");
                            ViewState["MyDataSet"] = dssmer;
                        }
                        else
                        {
                            drpFit.DataSource = null;
                            drpFit.DataBind();
                            drpFit.ClearSelection();
                            // dddldesign.Items.Insert(0, "Select Design");

                        }

                    }
                    else
                    {
                        drpFit.DataSource = null;
                        drpFit.DataBind();
                        drpFit.ClearSelection();

                    }
                }
            }


            //if (radcuttype.SelectedValue == "1")
            //{
            //    if (radbtn.SelectedValue == "1")
            //    {
            //        if (Sddrrpfit.SelectedValue == "Select Fit")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Fit Label');", true);
            //            return;
            //        }
            //        else
            //        {

            //        }

            //        DataSet dsFit = objBs.GetFitforsingle(Sddrrpfit.SelectedValue);
            //        if (dsFit != null)
            //        {
            //            if (dsFit.Tables[0].Rows.Count > 0)
            //            {

            //                drpFit.DataSource = dsFit.Tables[0];
            //                drpFit.DataTextField = "Fit";
            //                drpFit.DataValueField = "FitID";
            //                drpFit.DataBind();

            //            }
            //        }

            //    }
            //}
            //else if (radcuttype.SelectedValue == "2")
            //{
            //    double r = 0.00;
            //    double rr = 0.00;
            //    double rb = 0.00;
            //    double rr1 = 0.00;
            //    double rb1 = 0.00;
            //    string width = string.Empty;
            //    double wid = 0;
            //    DataSet dsFit = objBs.GetFitforsingle(Sddrrpfit.SelectedValue);
            //    if (dsFit != null)
            //    {
            //        if (dsFit.Tables[0].Rows.Count > 0)
            //        {

            //            drpFit.DataSource = dsFit.Tables[0];
            //            drpFit.DataTextField = "Fit";
            //            drpFit.DataValueField = "FitID";
            //            drpFit.DataBind();

            //        }
            //    }
            //    if (drpFit.SelectedValue == "3")
            //    {
            //        wid = Convert.ToDouble(txtsharp.Text);
            //    }
            //    else
            //    {
            //        wid = Convert.ToDouble(txtexec.Text);
            //    }

            //    double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
            //    if (roundoff > 0.5)
            //    {
            //        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        r = Math.Floor(Convert.ToDouble(roundoff));
            //    }


            //    txtNoofShirts.Text = r.ToString();
            //    txtReqNoShirts.Text = r.ToString();



            //    rr = ((r * 15) / 100);
            //    if (rr > 0.5)
            //    {
            //        rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        rb = Math.Floor(Convert.ToDouble(rr));
            //    }
            //    txtextrashirt.Text = rb.ToString();

            //    rr1 = ((r * 2) / 100);
            //    if (rr1 > 0.5)
            //    {
            //        rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        rb1 = Math.Floor(Convert.ToDouble(rr1));
            //    }
            //    txtminshirt.Text = rb1.ToString();




            //}

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
                    ViewState["MyDataSet"] = dsFit;

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
                    //drpCustomer.DataSource = dledgercheck.Tables[0];
                    //drpCustomer.DataTextField = "LedgerName";
                    //drpCustomer.DataValueField = "LedgerID";
                    //drpCustomer.DataBind();
                    //  drpCustomer.Items.Insert(0, "Select Party Name");
                }



            }
        }

        protected void brandindexchnaged(object sender, EventArgs e)
        {
            if (ddlbrand.SelectedValue == "Select Brand Name" || ddlbrand.SelectedValue == "0" || ddlbrand.SelectedValue == "")
            {
                drpNchkfit.ClearSelection();
            }
            else
            {
                DataSet dsFit = objBs.GetBrandFit(Convert.ToInt32(ddlbrand.SelectedValue));
                if (dsFit.Tables[0].Rows.Count > 0)
                {
                    drpNchkfit.SelectedValue = dsFit.Tables[0].Rows[0]["FitID"].ToString();
                    Sfitchaged(sender, e);
                }
                else
                {
                    drpNchkfit.ClearSelection();
                }
            }
            //DataSet dsFit = objBs.GetFit();
            //if (dsFit != null)
            //{
            //    if (dsFit.Tables[0].Rows.Count > 0)
            //    {

            //        drpFit.DataSource = dsFit.Tables[0];
            //        drpFit.DataTextField = "Fit";
            //        drpFit.DataValueField = "FitID";
            //        drpFit.DataBind();
            //        drpFit.Items.Insert(0, "Select Fit");

            //        Sddrrpfit.DataSource = dsFit.Tables[0];
            //        Sddrrpfit.DataTextField = "Fit";
            //        Sddrrpfit.DataValueField = "FitID";
            //        Sddrrpfit.DataBind();
            //        Sddrrpfit.Items.Insert(0, "Select Fit");

            //    }
            //}




            //if (radbtn.SelectedValue == "1")
            //{
            //    if (ddlbrand.SelectedValue == "Select Brand Name")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Brand Name');", true);
            //        return;
            //    }
            //    else
            //    {

            //    }

            //    DataSet dbrandcheck = objBs.getbrandnameforcuttprocessnew1(ddlbrand.SelectedValue, "1");
            //    DataSet branchd = objBs.getbrandnameforcuttprocessnewww(ddlbrand.SelectedValue, "1");

            //    if (branchd.Tables[0].Rows.Count > 0)
            //    {
            //        //ddlbrand.DataSource = branchd.Tables[0];
            //        //ddlbrand.DataTextField = "BrandName";
            //        //ddlbrand.DataValueField = "BrandID";
            //        //ddlbrand.DataBind();
            //        // drpCustomer.Items.Insert(0, "Select Party Name");
            //        Sddrrpfit.SelectedValue = branchd.Tables[0].Rows[0]["fitid"].ToString();
            //        drpFit.SelectedValue = branchd.Tables[0].Rows[0]["fitid"].ToString();
            //        ddlbrand.SelectedValue = branchd.Tables[0].Rows[0]["BrandID"].ToString();
            //    }
            //    if (dbrandcheck.Tables[0].Rows.Count > 0)
            //    {

            //        string fidit = dbrandcheck.Tables[0].Rows[0]["fitid"].ToString();

            //        DataSet dsize = objBs.Getsizetypenew(fidit);
            //        if (dsize != null)
            //        {
            //            if (dsize.Tables[0].Rows.Count > 0)
            //            {
            //                chkSizes.DataSource = dsize.Tables[0];
            //                chkSizes.DataTextField = "Size";
            //                chkSizes.DataValueField = "Sizeid";
            //                chkSizes.DataBind();

            //            }
            //        }







            //        if ((dsize.Tables[0].Rows.Count > 0))
            //        {
            //            //Select the checkboxlist items those values are true in database
            //            //Loop through the DataTable
            //            for (int i = 0; i <= dbrandcheck.Tables[0].Rows.Count - 1; i++)
            //            {
            //                //You need to change this as per your DB Design
            //                string size = dbrandcheck.Tables[0].Rows[i]["Sizeid2"].ToString();

            //                {
            //                    //Find the checkbox list items using FindByValue and select it.
            //                    chkSizes.Items.FindByValue(dbrandcheck.Tables[0].Rows[i]["Sizeid2"].ToString()).Selected = true;
            //                }

            //            }

            //        }
            //        //Formal
            //        if (Sddrrpfit.SelectedValue == "3")
            //        {
            //            tr1.Visible = true;
            //            ckhsize_index(sender, e);
            //            Tr2.Visible = false;
            //            //  Tr3.Visible = false;
            //        }
            //        else if (Sddrrpfit.SelectedValue == "4")
            //        {
            //            //  Tr3.Visible = true;
            //            tr1.Visible = false;
            //            Tr2.Visible = false;
            //            //  ckhsize_indexforboys(sender, e);

            //        }

            //    }
            //    else
            //    {
            //        ddlbrand.SelectedValue = "Select Brand Name";
            //        Sddrrpfit.SelectedValue = "Select Fit";
            //        drpFit.SelectedValue = "Select Fit";
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Size for that Particular Brand.Thank You!!!.');", true);
            //        ddlbrand.Focus();
            //        return;

            //    }



            //}
            //Sfitchaged(sender, e);
            UpdatePanel1.Update();
            //upitem.Update();
        }


        protected void brandindexchnaged1(object sender, EventArgs e)
        {
            //DataSet dsFit = objBs.GetFit();
            //if (dsFit != null)
            //{
            //    if (dsFit.Tables[0].Rows.Count > 0)
            //    {

            //        drpFit.DataSource = dsFit.Tables[0];
            //        drpFit.DataTextField = "Fit";
            //        drpFit.DataValueField = "FitID";
            //        drpFit.DataBind();
            //        drpFit.Items.Insert(0, "Select Fit");

            //        Sddrrpfit.DataSource = dsFit.Tables[0];
            //        Sddrrpfit.DataTextField = "Fit";
            //        Sddrrpfit.DataValueField = "FitID";
            //        Sddrrpfit.DataBind();
            //        Sddrrpfit.Items.Insert(0, "Select Fit");

            //    }
            //}




            //if (radbtn.SelectedValue == "1")
            //{
            //    if (ddlbrand.SelectedValue == "Select Brand Name")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Brand Name');", true);
            //        return;
            //    }
            //    else
            //    {

            //    }

            //    DataSet dbrandcheck = objBs.getbrandnameforcuttprocessnew1(ddlbrand.SelectedValue, "1");
            DataSet branchd = objBs.getbrandnameforcuttprocessnewww(ddlbrand.SelectedValue, "1");

            if (branchd.Tables[0].Rows.Count > 0)
            {
                //ddlbrand.DataSource = branchd.Tables[0];
                //ddlbrand.DataTextField = "BrandName";
                //ddlbrand.DataValueField = "BrandID";
                //ddlbrand.DataBind();
                // drpCustomer.Items.Insert(0, "Select Party Name");
                //   Sddrrpfit.SelectedValue = branchd.Tables[0].Rows[0]["fitid"].ToString();
                drpNchkfit.SelectedValue = branchd.Tables[0].Rows[0]["fitid"].ToString();
                Sfitchaged(sender, e);
                //  ddlbrand.SelectedValue = branchd.Tables[0].Rows[0]["BrandID"].ToString();
            }
            //    if (dbrandcheck.Tables[0].Rows.Count > 0)
            //    {

            //        string fidit = dbrandcheck.Tables[0].Rows[0]["fitid"].ToString();

            //        DataSet dsize = objBs.Getsizetypenew(fidit);
            //        if (dsize != null)
            //        {
            //            if (dsize.Tables[0].Rows.Count > 0)
            //            {
            //                chkSizes.DataSource = dsize.Tables[0];
            //                chkSizes.DataTextField = "Size";
            //                chkSizes.DataValueField = "Sizeid";
            //                chkSizes.DataBind();

            //            }
            //        }







            //        if ((dsize.Tables[0].Rows.Count > 0))
            //        {
            //            //Select the checkboxlist items those values are true in database
            //            //Loop through the DataTable
            //            for (int i = 0; i <= dbrandcheck.Tables[0].Rows.Count - 1; i++)
            //            {
            //                //You need to change this as per your DB Design
            //                string size = dbrandcheck.Tables[0].Rows[i]["Sizeid2"].ToString();

            //                {
            //                    //Find the checkbox list items using FindByValue and select it.
            //                    chkSizes.Items.FindByValue(dbrandcheck.Tables[0].Rows[i]["Sizeid2"].ToString()).Selected = true;
            //                }

            //            }

            //        }
            //        //Formal
            //        if (Sddrrpfit.SelectedValue == "3")
            //        {
            //            tr1.Visible = true;
            //            ckhsize_index(sender, e);
            //            Tr2.Visible = false;
            //            //  Tr3.Visible = false;
            //        }
            //        else if (Sddrrpfit.SelectedValue == "4")
            //        {
            //            //  Tr3.Visible = true;
            //            tr1.Visible = false;
            //            Tr2.Visible = false;
            //            //  ckhsize_indexforboys(sender, e);

            //        }

            //    }
            //    else
            //    {
            //        ddlbrand.SelectedValue = "Select Brand Name";
            //        Sddrrpfit.SelectedValue = "Select Fit";
            //        drpFit.SelectedValue = "Select Fit";
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Size for that Particular Brand.Thank You!!!.');", true);
            //        ddlbrand.Focus();
            //        return;

            //    }



            //}
            //Sfitchaged(sender, e);
        }

        protected void radchecked(object sender, EventArgs e)
        {
            if (radcuttype.SelectedValue == "1")
            {
                btngohead.Visible = false;
                btnprocess.Visible = true;
                btnprocessall.Visible = true;
                if (radbtn.SelectedValue == "1")
                {
                    btnprocessall.Visible = true;
                    tr1.Visible = true;
                    //  tr3.Visible = false;
                    //   tr2.Visible = false;
                    tr4.Visible = false;
                    FirstGridViewRow();
                    sing.Visible = true;
                    mul.Visible = false;
                    addsingle.Visible = false;
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
                    addsingle.Visible = false;
                }

            }

            //    if (radbtn.SelectedValue == "1")
            //    {
            //        btnprocessall.Visible = true;
            //        tr1.Visible = true;
            //        //  tr3.Visible = false;
            //        //   tr2.Visible = false;
            //        tr4.Visible = false;
            //        FirstGridViewRow();
            //        sing.Visible = true;
            //        mul.Visible = false;
            //        addsingle.Visible = false;

            //        //  singormulit.Visible = false;
            //        DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
            //        if (dst != null)
            //        {
            //            if (dst.Tables[0].Rows.Count > 0)
            //            {
            //                ddlSupplier.DataSource = dst.Tables[0];
            //                ddlSupplier.DataTextField = "LedgerName";
            //                ddlSupplier.DataValueField = "LedgerID";
            //                ddlSupplier.DataBind();
            //                ddlSupplier.Items.Insert(0, "Select Party Name");


            //            }
            //        }
            //        DataSet dsDNo = objBs.getmainlabel();
            //        if (dsDNo != null)
            //        {
            //            if (dsDNo.Tables[0].Rows.Count > 0)
            //            {
            //                drplab.DataSource = dsDNo.Tables[0];
            //                drplab.DataTextField = "MainLabel";
            //                drplab.DataValueField = "LabelID";
            //                drplab.DataBind();
            //                drplab.Items.Insert(0, "Select Label");


            //            }
            //        }

            //        DataSet dsFit = objBs.GetFit();
            //        if (dsFit != null)
            //        {
            //            if (dsFit.Tables[0].Rows.Count > 0)
            //            {

            //                Sddrrpfit.DataSource = dsFit.Tables[0];
            //                Sddrrpfit.DataTextField = "Fit";
            //                Sddrrpfit.DataValueField = "FitID";
            //                Sddrrpfit.DataBind();
            //                Sddrrpfit.Items.Insert(0, "Select Fit");
            //            }
            //        }

            //    }
            //    else
            //    {
            //        btnprocessall.Visible = false;
            //        FirstGridViewRow();
            //        tr1.Visible = false;
            //        //tr3.Visible = false;
            //        //   tr2.Visible = false;
            //        btnprocess.Enabled = false;
            //        tr4.Visible = true;
            //        mul.Visible = true;
            //        sing.Visible = false;
            //        rdSingle.Visible = false;
            //        // singormulit.Visible = true;
            //        DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
            //        if (dst != null)
            //        {
            //            if (dst.Tables[0].Rows.Count > 0)
            //            {


            //                chkcust.DataSource = dst.Tables[0];
            //                chkcust.DataTextField = "LedgerName";
            //                chkcust.DataValueField = "LedgerID";
            //                chkcust.DataBind();

            //            }
            //        }

            //    }
            //}
            //else if (radcuttype.SelectedValue == "2")
            //{
            //    btngohead.Visible = true;
            //    btnprocess.Visible = false;
            //    btnprocessall.Visible = false;

            //    if (radbtn.SelectedValue == "1")
            //    {
            //        // btnprocessall.Visible = true;
            //        tr1.Visible = true;
            //        //  tr3.Visible = false;
            //        //   tr2.Visible = false;
            //        tr4.Visible = false;
            //        FirstGridViewRow();
            //        sing.Visible = true;
            //        mul.Visible = false;
            //        addsingle.Visible = false;

            //        //  singormulit.Visible = false;
            //        DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
            //        if (dst != null)
            //        {
            //            if (dst.Tables[0].Rows.Count > 0)
            //            {
            //                ddlSupplier.DataSource = dst.Tables[0];
            //                ddlSupplier.DataTextField = "LedgerName";
            //                ddlSupplier.DataValueField = "LedgerID";
            //                ddlSupplier.DataBind();
            //                ddlSupplier.Items.Insert(0, "Select Party Name");


            //            }
            //        }
            //        DataSet dsDNo = objBs.getmainlabel();
            //        if (dsDNo != null)
            //        {
            //            if (dsDNo.Tables[0].Rows.Count > 0)
            //            {
            //                drplab.DataSource = dsDNo.Tables[0];
            //                drplab.DataTextField = "MainLabel";
            //                drplab.DataValueField = "LabelID";
            //                drplab.DataBind();
            //                drplab.Items.Insert(0, "Select Label");


            //            }
            //        }

            //        DataSet dsFit = objBs.GetFit();
            //        if (dsFit != null)
            //        {
            //            if (dsFit.Tables[0].Rows.Count > 0)
            //            {

            //                Sddrrpfit.DataSource = dsFit.Tables[0];
            //                Sddrrpfit.DataTextField = "Fit";
            //                Sddrrpfit.DataValueField = "FitID";
            //                Sddrrpfit.DataBind();
            //                Sddrrpfit.Items.Insert(0, "Select Fit");
            //            }
            //        }

            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Bulk Cutting For Multiple Party is in Process.Thank You!!!.');", true);
            //        return;
            //        //btnprocessall.Visible = false;
            //        //FirstGridViewRow();
            //        //tr1.Visible = false;
            //        ////tr3.Visible = false;
            //        ////   tr2.Visible = false;
            //        //btnprocess.Enabled = false;
            //        //tr4.Visible = true;
            //        //mul.Visible = true;
            //        //sing.Visible = false;
            //        //rdSingle.Visible = false;
            //        //// singormulit.Visible = true;
            //        //DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
            //        //if (dst != null)
            //        //{
            //        //    if (dst.Tables[0].Rows.Count > 0)
            //        //    {


            //        //        chkcust.DataSource = dst.Tables[0];
            //        //        chkcust.DataTextField = "LedgerName";
            //        //        chkcust.DataValueField = "LedgerID";
            //        //        chkcust.DataBind();

            //        //    }
            //        //}

            //    }
            //}
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
            dtt.Columns.Add(new DataColumn("Rowid", typeof(string)));

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
            dr["Rowid"] = string.Empty;

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

            dct = new DataColumn("Rowid");
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
            drNew["Rowid"] = "";


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

                    foreach (System.Web.UI.WebControls.ListItem item in chkcust.Items)
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

                        HiddenField hdRowId = (HiddenField)gridsize.Rows[rowIndex].Cells[4].FindControl("hdRowId");

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

                                foreach (System.Web.UI.WebControls.ListItem item in chkcust.Items)
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
                        hdRowId.Value = dt.Rows[i]["hdRowId"].ToString();

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

                        HiddenField hdRowId = (HiddenField)gridsize.Rows[rowIndex].Cells[4].FindControl("hdRowId");



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
                        dtCurrentTable.Rows[i - 1]["hdRowId"] = hdRowId.Value;


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
                btnprocessall.Visible = true;
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
                        ViewState["MyDataSet"] = dsFit;

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
                        foreach (System.Web.UI.WebControls.ListItem item in chkcust.Items)
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
                            //drpCustomer.DataSource = dssmer.Tables[0];
                            //drpCustomer.DataTextField = "LedgerName";
                            //drpCustomer.DataValueField = "LedgerID";
                            //drpCustomer.DataBind();
                            //drpCustomer.Items.Insert(0, "Select Party Name");
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
                        //drpCustomer.DataSource = dledgercheck.Tables[0];
                        //drpCustomer.DataTextField = "LedgerName";
                        //drpCustomer.DataValueField = "LedgerID";
                        //drpCustomer.DataBind();
                        //drpCustomer.Items.Insert(0, "Select Party Name");
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
                        foreach (System.Web.UI.WebControls.ListItem item in chkcust.Items)
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
                            //drpCustomer.DataSource = dssmer.Tables[0];
                            //drpCustomer.DataTextField = "LedgerName";
                            //drpCustomer.DataValueField = "LedgerID";
                            //drpCustomer.DataBind();
                            //drpCustomer.Items.Insert(0, "Select Party Name");

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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                ////  btnadd.Enabled = false;
                //btnprocessall.Enabled = false;
                //return;
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

                            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                            if (dcalculate.Tables[0].Rows.Count > 0)
                            {

                                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);
                                double wid = 0;
                                if (drpFit.SelectedValue == "3")
                                {
                                    wid = Convert.ToDouble(txtavgmeter.Text);
                                }
                                else
                                {
                                    wid = Convert.ToDouble(txtexec.Text);
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

                        if (radbtn.SelectedValue == "1")
                        {
                            //   tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                            //  Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);

                            txtavvgmeter.Text = Convert.ToDouble(reqq / tot).ToString("N");

                            gndtot = gndtot + tot;



                            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                            if (dcalculate.Tables[0].Rows.Count > 0)
                            {

                                double wid = 0;
                                if (drpFit.SelectedValue == "3")
                                {
                                    wid = Convert.ToDouble(txtavgmeter.Text);
                                }
                                else
                                {
                                    wid = Convert.ToDouble(txtexec.Text);
                                }

                                double roundoff = Convert.ToDouble(tot) * wid;

                                r = roundoff;


                                txttotshirt1.Text = tot.ToString();
                            }

                            //  txt38FS.Focus();
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
                                    //if (drpCustomer.SelectedValue == "Select Party Name")
                                    //{
                                    //}
                                    //else
                                    {
                                        DataRow dr = dCrt.NewRow();

                                        dr["Transid"] = dddldesign.SelectedValue;
                                        dr["Design"] = dddldesign.SelectedItem.Text;
                                        dr["Rate"] = txtDesignRate.Text;

                                        dr["meter"] = txtAvailableMtr.Text;
                                        dr["Shirt"] = txtNoofShirts.Text;
                                        dr["reqmeter"] = txtavamet1.Text;

                                        dr["reqshirt"] = txttotshirt1.Text;
                                        //dr["ledgerid"] = drpCustomer.SelectedValue;
                                        //dr["party"] = drpCustomer.SelectedItem.Text;
                                        //dr["Fitid"] = drpFit.SelectedValue;
                                        //dr["Fit"] = drpFit.SelectedItem.Text;

                                        //dr["TSFS"] = txt36FS.Text;
                                        //dr["TSHS"] = txt36HS.Text;

                                        //dr["TEFS"] = txt38FS.Text;
                                        //dr["TEHS"] = txt38HS.Text;

                                        //dr["TNFS"] = txt39FS.Text;
                                        //dr["TNHS"] = txt39HS.Text;

                                        //dr["FZFS"] = txt40FS.Text;
                                        //dr["FZHS"] = txt40HS.Text;

                                        //dr["FTFS"] = txt42FS.Text;
                                        //dr["FTHS"] = txt42HS.Text;

                                        //dr["FFFS"] = txt44FS.Text;
                                        //dr["FFHS"] = txt44HS.Text;

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
                                    //if (drpCustomer.SelectedValue == "Select Party Name")
                                    //{
                                    //}
                                    //else
                                    {
                                        DataRow dr = dCrt.NewRow();

                                        dr["Transid"] = dddldesign.SelectedValue;
                                        dr["Design"] = dddldesign.SelectedItem.Text;
                                        dr["Rate"] = txtDesignRate.Text;

                                        dr["meter"] = txtAvailableMtr.Text;
                                        dr["Shirt"] = txtNoofShirts.Text;
                                        dr["reqmeter"] = txtavamet1.Text;

                                        dr["reqshirt"] = txttotshirt1.Text;
                                        //dr["ledgerid"] = drpCustomer.SelectedValue;
                                        //dr["party"] = drpCustomer.SelectedItem.Text;
                                        //dr["Fitid"] = drpFit.SelectedValue;
                                        //dr["Fit"] = drpFit.SelectedItem.Text;

                                        //dr["TSFS"] = txt36FS.Text;
                                        //dr["TSHS"] = txt36HS.Text;

                                        //dr["TEFS"] = txt38FS.Text;
                                        //dr["TEHS"] = txt38HS.Text;

                                        //dr["TNFS"] = txt39FS.Text;
                                        //dr["TNHS"] = txt39HS.Text;

                                        //dr["FZFS"] = txt40FS.Text;
                                        //dr["FZHS"] = txt40HS.Text;

                                        //dr["FTFS"] = txt42FS.Text;
                                        //dr["FTHS"] = txt42HS.Text;

                                        //dr["FFFS"] = txt44FS.Text;
                                        //dr["FFHS"] = txt44HS.Text;

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

                            FirstGridViewRow();
                            // removedropdownlist();
                            dddldesign.Focus();
                            btnadd.Enabled = true;
                        }

                    }
                }
            }
            System.Threading.Thread.Sleep(3000);
        }

        protected void processclick(object sender, EventArgs e)
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


            if (radbtn.SelectedValue == "1")
            {
                // double tot = 0.00;

                //  double r = 0.00;

                //  tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
                //    Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



                gndtot = gndtot + tot;



                DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                if (dcalculate.Tables[0].Rows.Count > 0)
                {

                    //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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

                    //  txtavamet1.Text = r.ToString();
                    txttotshirt1.Text = tot.ToString();
                    //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                }

                // txt38FS.Focus();
                txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
                txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

                btnprocess.Enabled = true;
                btnadd.Enabled = true;

                //if (gndtot < (Convert.ToDouble(txtReqNoShirts.Text) - Convert.ToDouble(txtminshirt.Text)))
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in Lesser Than that Required Shirt.Thank you!!!');", true);
                //    btnadd.Enabled = false;
                //    btnprocess.Enabled = false;
                //    return;
                //}
                //else
                //{
                //    btnprocess.Enabled = true;
                //    btnadd.Enabled = true;
                //}

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
                //    btnadd.Enabled = true;
                //}



            }
            else
            {
                btnprocess.Enabled = true;
                btnadd.Enabled = true;

                for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                {
                    //DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

                    //DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

                    //TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
                    //TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
                    //TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
                    //TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
                    //TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
                    //TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

                    //TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
                    //TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
                    //TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
                    //TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
                    //TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
                    //TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

                    //TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
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
                    //TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

                    //tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                    //    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    //gndtot = gndtot + tot;

                    //int col = vLoop + 1;
                    //if (dfit.SelectedValue != "Select Fit")
                    //{

                    //    DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
                    //    if (dcalculate.Tables[0].Rows.Count > 0)
                    //    {

                    //        double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    //        double roundoff = Convert.ToDouble(tot) * wid;
                    //        //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                    //        //if (roundoff > 0.5)
                    //        //{
                    //        //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                    //        //}
                    //        //else
                    //        //{
                    //        //    r = Math.Floor(Convert.ToDouble(roundoff));
                    //        //}
                    //        r = roundoff;

                    //        txtreqmeter.Text = r.ToString();
                    //        txtshirt.Text = tot.ToString();
                    //        gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                    //    }

                    //    txt38fs.Focus();

                    //    // dparty.Focus();
                    //}


                    //txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
                    //txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");


                }
                //if (gndtot < (Convert.ToDouble(txtReqNoShirts.Text) - Convert.ToDouble(txtminshirt.Text)))
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in Lesser Than that Required Shirt.Thank you!!!');", true);
                //    btnadd.Enabled = false;
                //    btnprocess.Enabled = false;
                //    return;
                //}
                //else
                //{
                //    btnprocess.Enabled = true;
                //    btnadd.Enabled = true;
                //}

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
                //    btnadd.Enabled = true;
                //}
            }
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
            //    return;
            //}
            //else
            //{
            //    btnadd.Enabled = true;
            //    // return;
            //}

            decimal dAmt = 0; decimal dTotal = 0;
            dCrt = (DataTable)ViewState["Data"];
            if (dCrt.Rows.Count == 0)
            {
                if (tr1.Visible == true)
                {
                    //if (drpCustomer.SelectedValue == "Select Party Name")
                    //{
                    //}
                    //else
                    {
                        DataRow dr = dCrt.NewRow();

                        dr["Transid"] = dddldesign.SelectedValue;
                        dr["Design"] = dddldesign.SelectedItem.Text;
                        dr["Rate"] = txtDesignRate.Text;

                        dr["meter"] = txtAvailableMtr.Text;
                        dr["Shirt"] = txtNoofShirts.Text;
                        dr["reqmeter"] = txtavamet1.Text;

                        dr["reqshirt"] = txttotshirt1.Text;
                        //dr["ledgerid"] = drpCustomer.SelectedValue;
                        //dr["party"] = drpCustomer.SelectedItem.Text;
                        //dr["Fitid"] = drpFit.SelectedValue;
                        //dr["Fit"] = drpFit.SelectedItem.Text;

                        //dr["TSFS"] = txt36FS.Text;
                        //dr["TSHS"] = txt36HS.Text;

                        //dr["TEFS"] = txt38FS.Text;
                        //dr["TEHS"] = txt38HS.Text;

                        //dr["TNFS"] = txt39FS.Text;
                        //dr["TNHS"] = txt39HS.Text;

                        //dr["FZFS"] = txt40FS.Text;
                        //dr["FZHS"] = txt40HS.Text;

                        //dr["FTFS"] = txt42FS.Text;
                        //dr["FTHS"] = txt42HS.Text;

                        //dr["FFFS"] = txt44FS.Text;
                        //dr["FFHS"] = txt44HS.Text;

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

                //if (tr2.Visible == true)
                //{
                //    if (drpCustomer2.SelectedValue == "Select Party Name")
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
                //        dr["reqmeter"] = txtavamet2.Text;

                //        dr["reqshirt"] = txttotshirt2.Text;
                //        dr["ledgerid"] = drpCustomer2.SelectedValue;
                //        dr["party"] = drpCustomer2.SelectedItem.Text;
                //        dr["Fitid"] = drpFit2.SelectedValue;
                //        dr["Fit"] = drpFit2.SelectedItem.Text;

                //        dr["TSFS"] = txt36FS2.Text;
                //        dr["TSHS"] = txt36HS2.Text;

                //        dr["TEFS"] = txt38FS2.Text;
                //        dr["TEHS"] = txt38HS2.Text;

                //        dr["FZFS"] = txt40FS2.Text;
                //        dr["FZHS"] = txt40HS2.Text;

                //        dr["FTFS"] = txt42FS2.Text;
                //        dr["FTHS"] = txt42HS2.Text;

                //        if (radbtn.SelectedValue == "1")
                //        {
                //            ledgerr = ddlSupplier.SelectedValue;
                //            mainlab = drplab.SelectedValue;
                //            fitlab = chkfit.Checked;
                //            washlab = Chkwash.Checked;
                //            logolab = Chllogo.Checked;
                //        }
                //        else
                //        {
                //            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                //            //{


                //            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

                //            //    if (drpCustomer.SelectedItem.Text == txtno.Text)
                //            //    {
                //            //        //   ledgerr = drpparty.SelectedValue;
                //            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
                //            //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
                //            //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
                //            //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



                //            //        ledgerr = drpCustomer.SelectedValue;
                //            //        mainlab = drpparty.SelectedValue;
                //            //        fitlab = fitll.Checked;
                //            //        washlab = wasll.Checked;
                //            //        logolab = logll.Checked;
                //            //    }


                //            //}
                //        }
                //        dr["LLedger"] = ledgerr;
                //        dr["Mainlab"] = mainlab;
                //        dr["FItLab"] = fitlab;
                //        dr["Washlab"] = washlab;
                //        dr["Logolab"] = logolab;



                //        dCrt.Rows.Add(dr);
                //    }

                //}
                //if (tr3.Visible == true)
                //{
                //    if (drpCustomer3.SelectedValue == "Select Party Name")
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
                //        dr["reqmeter"] = txtavamet3.Text;

                //        dr["reqshirt"] = txttotshirt3.Text;
                //        dr["ledgerid"] = drpCustomer.SelectedValue;
                //        dr["party"] = drpCustomer3.SelectedItem.Text;
                //        dr["Fitid"] = drpFit3.SelectedValue;
                //        dr["Fit"] = drpFit3.SelectedItem.Text;

                //        dr["TSFS"] = txt36FS3.Text;
                //        dr["TSHS"] = txt36HS3.Text;

                //        dr["TEFS"] = txt38FS3.Text;
                //        dr["TEHS"] = txt38HS3.Text;

                //        dr["FZFS"] = txt40FS3.Text;
                //        dr["FZHS"] = txt40HS3.Text;

                //        dr["FTFS"] = txt42FS3.Text;
                //        dr["FTHS"] = txt42HS3.Text;

                //        if (radbtn.SelectedValue == "1")
                //        {
                //            ledgerr = ddlSupplier.SelectedValue;
                //            mainlab = drplab.SelectedValue;
                //            fitlab = chkfit.Checked;
                //            washlab = Chkwash.Checked;
                //            logolab = Chllogo.Checked;
                //        }
                //        else
                //        {
                //            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                //            //{


                //            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

                //            //    if (drpCustomer.SelectedItem.Text == txtno.Text)
                //            //    {
                //            //        //   ledgerr = drpparty.SelectedValue;
                //            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
                //            //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
                //            //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
                //            //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



                //            //        ledgerr = drpCustomer.SelectedValue;
                //            //        mainlab = drpparty.SelectedValue;
                //            //        fitlab = fitll.Checked;
                //            //        washlab = wasll.Checked;
                //            //        logolab = logll.Checked;
                //            //    }


                //            //}
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
            else
            {
                if (tr1.Visible == true)
                {
                    //if (drpCustomer.SelectedValue == "Select Party Name")
                    //{
                    //}
                    //else
                    {
                        DataRow dr = dCrt.NewRow();

                        dr["Transid"] = dddldesign.SelectedValue;
                        dr["Design"] = dddldesign.SelectedItem.Text;
                        dr["Rate"] = txtDesignRate.Text;

                        dr["meter"] = txtAvailableMtr.Text;
                        dr["Shirt"] = txtNoofShirts.Text;
                        dr["reqmeter"] = txtavamet1.Text;

                        dr["reqshirt"] = txttotshirt1.Text;
                        //dr["ledgerid"] = drpCustomer.SelectedValue;
                        //dr["party"] = drpCustomer.SelectedItem.Text;
                        //dr["Fitid"] = drpFit.SelectedValue;
                        //dr["Fit"] = drpFit.SelectedItem.Text;

                        //dr["TSFS"] = txt36FS.Text;
                        //dr["TSHS"] = txt36HS.Text;

                        //dr["TEFS"] = txt38FS.Text;
                        //dr["TEHS"] = txt38HS.Text;

                        //dr["TNFS"] = txt39FS.Text;
                        //dr["TNHS"] = txt39HS.Text;

                        //dr["FZFS"] = txt40FS.Text;
                        //dr["FZHS"] = txt40HS.Text;

                        //dr["FTFS"] = txt42FS.Text;
                        //dr["FTHS"] = txt42HS.Text;

                        //dr["FFFS"] = txt44FS.Text;
                        //dr["FFHS"] = txt44HS.Text;

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
                            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                            //{


                            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

                            //    if (drpCustomer.SelectedItem.Text == txtno.Text)
                            //    {
                            //        //   ledgerr = drpparty.SelectedValue;
                            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
                            //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
                            //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
                            //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



                            //        ledgerr = drpCustomer.SelectedValue;
                            //        mainlab = drpparty.SelectedValue;
                            //        fitlab = fitll.Checked;
                            //        washlab = wasll.Checked;
                            //        logolab = logll.Checked;
                            //    }


                            //}
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
                //if (tr2.Visible == true)
                //{
                //    if (drpCustomer2.SelectedValue == "Select Party Name")
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
                //        dr["reqmeter"] = txtavamet2.Text;

                //        dr["reqshirt"] = txttotshirt2.Text;
                //        dr["ledgerid"] = drpCustomer2.SelectedValue;
                //        dr["party"] = drpCustomer2.SelectedItem.Text;
                //        dr["Fitid"] = drpFit2.SelectedValue;
                //        dr["Fit"] = drpFit2.SelectedItem.Text;

                //        dr["TSFS"] = txt36FS2.Text;
                //        dr["TSHS"] = txt36HS2.Text;

                //        dr["TEFS"] = txt38FS2.Text;
                //        dr["TEHS"] = txt38HS2.Text;

                //        dr["FZFS"] = txt40FS2.Text;
                //        dr["FZHS"] = txt40HS2.Text;

                //        dr["FTFS"] = txt42FS2.Text;
                //        dr["FTHS"] = txt42HS2.Text;

                //        if (radbtn.SelectedValue == "1")
                //        {
                //            ledgerr = ddlSupplier.SelectedValue;
                //            mainlab = drplab.SelectedValue;
                //            fitlab = chkfit.Checked;
                //            washlab = Chkwash.Checked;
                //            logolab = Chllogo.Checked;
                //        }
                //        else
                //        {
                //            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                //            //{


                //            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

                //            //    if (drpCustomer.SelectedItem.Text == txtno.Text)
                //            //    {
                //            //        //   ledgerr = drpparty.SelectedValue;
                //            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
                //            //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
                //            //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
                //            //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



                //            //        ledgerr = drpCustomer.SelectedValue;
                //            //        mainlab = drpparty.SelectedValue;
                //            //        fitlab = fitll.Checked;
                //            //        washlab = wasll.Checked;
                //            //        logolab = logll.Checked;
                //            //    }


                //            //}
                //        }
                //        dr["LLedger"] = ledgerr;
                //        dr["Mainlab"] = mainlab;
                //        dr["FItLab"] = fitlab;
                //        dr["Washlab"] = washlab;
                //        dr["Logolab"] = logolab;



                //        dCrt.Rows.Add(dr);
                //    }

                //}
                //if (tr3.Visible == true)
                //{
                //    if (drpCustomer3.SelectedValue == "Select Party Name")
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
                //        dr["reqmeter"] = txtavamet3.Text;

                //        dr["reqshirt"] = txttotshirt3.Text;
                //        dr["ledgerid"] = drpCustomer.SelectedValue;
                //        dr["party"] = drpCustomer3.SelectedItem.Text;
                //        dr["Fitid"] = drpFit3.SelectedValue;
                //        dr["Fit"] = drpFit3.SelectedItem.Text;

                //        dr["TSFS"] = txt36FS3.Text;
                //        dr["TSHS"] = txt36HS3.Text;

                //        dr["TEFS"] = txt38FS3.Text;
                //        dr["TEHS"] = txt38HS3.Text;

                //        dr["FZFS"] = txt40FS3.Text;
                //        dr["FZHS"] = txt40HS3.Text;

                //        dr["FTFS"] = txt42FS3.Text;
                //        dr["FTHS"] = txt42HS3.Text;

                //        if (radbtn.SelectedValue == "1")
                //        {
                //            ledgerr = ddlSupplier.SelectedValue;
                //            mainlab = drplab.SelectedValue;
                //            fitlab = chkfit.Checked;
                //            washlab = Chkwash.Checked;
                //            logolab = Chllogo.Checked;
                //        }
                //        else
                //        {
                //            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                //            //{


                //            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Ledgername");

                //            //    if (drpCustomer.SelectedItem.Text == txtno.Text)
                //            //    {
                //            //        //   ledgerr = drpparty.SelectedValue;
                //            //        DropDownList drpparty = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drrplab");
                //            //        CheckBox fitll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkfit");
                //            //        CheckBox wasll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchkwash");
                //            //        CheckBox logll = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("Mchklogo");



                //            //        ledgerr = drpCustomer.SelectedValue;
                //            //        mainlab = drpparty.SelectedValue;
                //            //        fitlab = fitll.Checked;
                //            //        washlab = wasll.Checked;
                //            //        logolab = logll.Checked;
                //            //    }


                //            //}
                //        }
                //        dr["LLedger"] = ledgerr;
                //        dr["Mainlab"] = mainlab;
                //        dr["FItLab"] = fitlab;
                //        dr["Washlab"] = washlab;
                //        dr["Logolab"] = logolab;



                //        dCrt.Rows.Add(dr);
                //    }
                //}

                //DataRow dr = dCrt.NewRow();


                //dr["CatID"] = ddlCategory.SelectedValue;
                //dr["SubCatID"] = lblSubcatid.Text;

                //dr["Group"] = ddlCategory.SelectedItem.Text;
                //dr["item"] = ddlitem.SelectedItem.Text;
                //dr["ExistQty"] = txtAvalQty.Text;

                //dr["Qty"] = txtretQty.Text;
                //dr["Rate"] = txtRate.Text;
                //dr["Amount"] = txtAmount.Text;
                //dr["stockid"] = ddlitem.SelectedValue;
                //dCrt.Rows.Add(dr);

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
            //   rdSingle.Checked = false;
            //   rdMultiple.Checked = false;

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

            txtavamet1.Text = "0";
            txttotshirt1.Text = "0";
            txtavvgmeter.Text = "0";



            //gridsize.DataSource = null;
            //gridsize.DataBind();
            FirstGridViewRow();
            removedropdownlist();


            //drpCustomer2.ClearSelection();
            //drpFit2.ClearSelection();
            //txt36FS2.Text = "";
            //txt36HS2.Text = "";

            //txt38FS2.Text = "";
            //txt38HS2.Text = "";

            //txt40FS2.Text = "";
            //txt40HS2.Text = "";

            //txt42FS2.Text = "";
            //txt42HS2.Text = "";

            //txtavamet2.Text = "";
            //txttotshirt2.Text = "";

            //drpCustomer3.ClearSelection();
            //drpFit3.ClearSelection();
            //txt36FS3.Text = "";
            //txt36HS3.Text = "";

            //txt38FS3.Text = "";
            //txt38HS3.Text = "";

            //txt40FS3.Text = "";
            //txt40HS3.Text = "";

            //txt42FS3.Text = "";
            //txt42HS3.Text = "";

            //txtavamet3.Text = "";
            //txttotshirt3.Text = "";


            dddldesign.Focus();

            System.Threading.Thread.Sleep(3000);

        }

        protected void GOheadprocessclick(object sender, EventArgs e)
        {
            int shirtcount = RatioShirtProcess.Rows.Count;

            if (shirtcount == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Final Process Tab There is no value While Giving Go Head.Thank you!!!');", true);
                return;
            }


            System.Threading.Thread.Sleep(1000);
            // Companylotchecked(sender, e);
            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you!!');", true);
                return;
            }
            if (drpcutting.SelectedValue == "Select Cutting Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Cutting Name. Thank you!!');", true);
                return;
            }
            if (drpcutting.SelectedValue == "Select Job Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Job Worker Name. Thank you!!');", true);
                return;
            }

            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {

                DataSet dcl = new DataSet();
                if (drpbranch.SelectedValue == "3")
                {
                    dcl = objBs.checkcompanylotno(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue);
                }
                else
                {
                    if (rdncore.SelectedValue == "1")
                    {
                        dcl = objBs.checkcompanylotnonew(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue, txtcompanysublot.Text);
                    }
                    else
                    {
                        dcl = objBs.checkcompanylotno(drpbranch.SelectedValue, txtcompanylot.Text, drpcutting.SelectedValue);
                    }

                }

                if (dcl.Tables[0].Rows.Count > 0)
                {
                    txtcompanylot.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Cutting No.Already Exists. Thank you');", true);
                    return;
                }
                else
                {
                    if (drpbranch.SelectedValue == "3")
                    {
                        lblcompany.Text = "CFLEX";

                    }
                    else
                    {
                        lblcompany.Text = "CFLEX";

                    }
                }


            }

            if (ddlcompletestitching.SelectedValue == "Yes")
            {
                if (Nchkstch.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Process (Stiching Must ).Thank you!!!');", true);
                    return;
                }
            }
            else
            {
                if (Nchkiron.SelectedValue == "" || Nchkstch.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Process (Ironing & Stiching Must ).Thank you!!!');", true);
                    return;
                }
            }

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
                getzeroforemptysize();
                if (radbtn.SelectedValue == "1")
                {
                    tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
             Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
             Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
             Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



                    gndtot = gndtot + tot;



                    //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
                    //  if (dcalculate.Tables[0].Rows.Count > 0)
                    {

                        //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                        double wid = 0;
                        //  if (drpFit.SelectedValue == "3")
                        {
                            wid = Convert.ToDouble(txtactualmet.Text);
                        }
                        //else
                        //{
                        //    wid = Convert.ToDouble(txtexec.Text);
                        //}

                        double roundoff = Convert.ToDouble(tot) * wid;
                        r = roundoff;
                        double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                        if (roundoff > 0.5)
                        {
                            r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            r = Math.Floor(Convert.ToDouble(roundoff));
                        }


                        // txtavamet1.Text = r.ToString();
                        txttotshirt1.Text = tot.ToString();

                    }

                    //  txt38FS.Focus();
                    //  txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
                    //    txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

                    btnprocess.Enabled = true;
                    btnadd.Enabled = true;





                }
                else
                {
                    //btnprocess.Enabled = true;
                    //btnadd.Enabled = true;

                    //for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                    //{

                    //    TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
                    //    TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
                    //    if (txtavggsize.Text == "0" || txtavggsize.Text == "")
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please click Calc Button.Thank You!!!');", true);
                    //        btnadd.Enabled = false;
                    //        btnprocess.Enabled = false;
                    //        return;
                    //    }
                    //    if (txtreqmeter.Text == "0" || txtreqmeter.Text == "")
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please click Calc Button.Thank you!!!');", true);
                    //        btnadd.Enabled = false;
                    //        btnprocess.Enabled = false;
                    //        return;

                    //    }



                    //}

                }
            }

            decimal dAmt = 0; decimal dTotal = 0;
            if (radcuttype.SelectedValue == "2")
            {
                dCrt = (DataTable)ViewState["Data"];

                dCrt.Clear();

                for (int vLoop = 0; vLoop < RatioShirtProcess.Rows.Count; vLoop++)
                {
                    Label Nlblitemname = (Label)RatioShirtProcess.Rows[vLoop].FindControl("Nlblitemname");
                    Label Nlbltransid = (Label)RatioShirtProcess.Rows[vLoop].FindControl("Nlbltransid");

                    Label Nlblfitname = (Label)RatioShirtProcess.Rows[vLoop].FindControl("Nlblfitname");
                    Label Nlblfitid = (Label)RatioShirtProcess.Rows[vLoop].FindControl("Nlblfitid");

                    Label Nlblrequiredmeter = (Label)RatioShirtProcess.Rows[vLoop].FindControl("Nlblrequiredmeter");

                    Label Nlblavgmeter = (Label)RatioShirtProcess.Rows[vLoop].FindControl("Nlblavgmeter");

                    Label Nlbltotalshirt = (Label)RatioShirtProcess.Rows[vLoop].FindControl("Nlbltotalshirt");

                    Label Nlblreqshirts = (Label)RatioShirtProcess.Rows[vLoop].FindControl("Nlblreqshirts");

                    Label NlblActmeter = (Label)RatioShirtProcess.Rows[vLoop].FindControl("NlblActmeter");

                    int ccount = (vLoop + 1);

                    if (NlblActmeter.Text == "Infinity")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Given Meter in Row " + ccount + ". Thank you !!!');", true);
                        return;
                    }
                    else
                    {

                    }

                    TextBox txt30fs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt30fs");
                    TextBox txt32fs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt32fs");
                    TextBox txt34fs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt34fs");
                    TextBox txt36fs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt36fs");
                    TextBox txtxsfs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtxsfs");
                    TextBox txtsfs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtsfs");
                    TextBox txtmfs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtmfs");
                    TextBox txtlfs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtlfs");
                    TextBox txtxlfs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtxlfs");
                    TextBox txtxxlfs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtxxlfs");
                    TextBox txt3xlfs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt3xlfs");
                    TextBox txt4xlfs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt4xlfs");


                    TextBox txt30hs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt30hs");
                    TextBox txt32hs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt32hs");
                    TextBox txt34hs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt34hs");
                    TextBox txt36hs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt36hs");
                    TextBox txtxshs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtxshs");
                    TextBox txtshs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtshs");
                    TextBox txtmhs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtmhs");
                    TextBox txtlhs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtlhs");
                    TextBox txtxlhs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtxlhs");
                    TextBox txtxxlhs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxtxxlhs");
                    TextBox txt3xlhs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt3xlhs");
                    TextBox txt4xlhs = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("Ntxt4xlhs");

                    TextBox txtcontra = (TextBox)RatioShirtProcess.Rows[vLoop].FindControl("txtcontra");



                    if (NlblActmeter.Text != "NaN")
                    {


                        if (dCrt.Rows.Count == 0)
                        {
                            //if (tr1.Visible == true)
                            {
                                //if (drpCustomer.SelectedValue == "Select Party Name")
                                //{
                                //}
                                //else
                                {
                                    DataRow dr = dCrt.NewRow();

                                    dr["Transid"] = Nlbltransid.Text;
                                    dr["Design"] = Nlblitemname.Text;
                                    dr["Rate"] = "0";

                                    dr["meter"] = Nlblrequiredmeter.Text;
                                    dr["Shirt"] = Nlbltotalshirt.Text;
                                    dr["reqmeter"] = Nlblrequiredmeter.Text;

                                    dr["reqshirt"] = Nlblreqshirts.Text;



                                    dr["ledgerid"] = "0";
                                    dr["party"] = "0";
                                    dr["Fitid"] = Nlblfitid.Text;
                                    dr["Fit"] = Nlblfitname.Text;

                                    dr["S30FS"] = txt30fs.Text;
                                    dr["S30HS"] = txt30hs.Text;

                                    dr["S32FS"] = txt32fs.Text;
                                    dr["S32HS"] = txt32hs.Text;

                                    dr["S34FS"] = txt34fs.Text;
                                    dr["S34HS"] = txt34hs.Text;

                                    dr["S36FS"] = txt36fs.Text;
                                    dr["S36HS"] = txt36hs.Text;

                                    dr["SXSFS"] = txtxsfs.Text;
                                    dr["SXSHS"] = txtxshs.Text;

                                    dr["SLFS"] = txtlfs.Text;
                                    dr["SLHS"] = txtlhs.Text;

                                    dr["SXLFS"] = txtxlfs.Text;
                                    dr["SXLHS"] = txtxlhs.Text;

                                    dr["SXXLFS"] = txtxxlfs.Text;
                                    dr["SXXLHS"] = txtxxlhs.Text;


                                    dr["S3XLFS"] = txt3xlfs.Text;
                                    dr["S3XLHS"] = txt3xlhs.Text;


                                    dr["S4XLFS"] = txt4xlfs.Text;
                                    dr["S4XLHS"] = txt4xlhs.Text;


                                    dr["SSFS"] = txtsfs.Text;
                                    dr["SSHS"] = txtshs.Text;


                                    dr["SMFS"] = txtmfs.Text;
                                    dr["SMHS"] = txtmhs.Text;


                                    dr["Itemname"] = "";
                                    //   dr["Pattern"] = Nlblfitid.Text;
                                    dr["Pattern"] = "1";
                                    dr["PatternName"] = Nlblfitname.Text;

                                    dr["WSP"] = "0";
                                    dr["avgsize"] = NlblActmeter.Text;
                                    dr["Extra"] = "0";
                                    dr["LLedger"] = "0";
                                    dr["Mainlab"] = "0";
                                    dr["FItLab"] = fitlab;
                                    dr["Washlab"] = washlab;
                                    dr["Logolab"] = logolab;
                                    dr["Total"] = Nlbltotalshirt.Text.ToString();

                                    dr["Contrast"] = txtcontra.Text;

                                    dCrt.Rows.Add(dr);
                                }
                            }
                            //else
                            //{

                            //}
                        }
                        else
                        {
                            //if (tr1.Visible == true)
                            //{
                            //    //if (drpCustomer.SelectedValue == "Select Party Name")
                            //    //{
                            //    //}
                            //    //else
                            {
                                DataRow dr = dCrt.NewRow();

                                dr["Transid"] = Nlbltransid.Text;
                                dr["Design"] = Nlblitemname.Text;
                                dr["Rate"] = "0";

                                dr["meter"] = Nlblrequiredmeter.Text;
                                dr["Shirt"] = Nlbltotalshirt.Text;
                                dr["reqmeter"] = Nlblrequiredmeter.Text;

                                dr["reqshirt"] = Nlblreqshirts.Text;


                                dr["ledgerid"] = "0";
                                dr["party"] = "0";
                                dr["Fitid"] = Nlblfitid.Text;
                                dr["Fit"] = Nlblfitname.Text;

                                dr["S30FS"] = txt30fs.Text;
                                dr["S30HS"] = txt30hs.Text;

                                dr["S32FS"] = txt32fs.Text;
                                dr["S32HS"] = txt32hs.Text;

                                dr["S34FS"] = txt34fs.Text;
                                dr["S34HS"] = txt34hs.Text;

                                dr["S36FS"] = txt36fs.Text;
                                dr["S36HS"] = txt36hs.Text;

                                dr["SXSFS"] = txtxsfs.Text;
                                dr["SXSHS"] = txtxshs.Text;

                                dr["SLFS"] = txtlfs.Text;
                                dr["SLHS"] = txtlhs.Text;

                                dr["SXLFS"] = txtxlfs.Text;
                                dr["SXLHS"] = txtxlhs.Text;

                                dr["SXXLFS"] = txtxxlfs.Text;
                                dr["SXXLHS"] = txtxxlhs.Text;


                                dr["S3XLFS"] = txt3xlfs.Text;
                                dr["S3XLHS"] = txt3xlhs.Text;


                                dr["S4XLFS"] = txt4xlfs.Text;
                                dr["S4XLHS"] = txt4xlhs.Text;


                                dr["SSFS"] = txtsfs.Text;
                                dr["SSHS"] = txtshs.Text;


                                dr["SMFS"] = txtmfs.Text;
                                dr["SMHS"] = txtmhs.Text;


                                dr["Itemname"] = "";
                                //   dr["Pattern"] = Nlblfitid.Text;
                                dr["Pattern"] = "1";
                                dr["PatternName"] = Nlblfitname.Text;

                                dr["WSP"] = "0";
                                dr["avgsize"] = NlblActmeter.Text;
                                dr["Extra"] = "0";
                                dr["LLedger"] = "0";
                                dr["Mainlab"] = "0";
                                dr["FItLab"] = fitlab;
                                dr["Washlab"] = washlab;
                                dr["Logolab"] = logolab;
                                dr["Total"] = Nlbltotalshirt.Text.ToString();

                                dr["Contrast"] = txtcontra.Text;
                                dCrt.Rows.Add(dr);




                            }
                            //}
                            //  else
                            {

                            }
                        }
                    }
                }

            }
            else if (radcuttype.SelectedValue == "1")
            {
                #region

                dCrt = (DataTable)ViewState["Data"];
                if (dCrt.Rows.Count == 0)
                {
                    if (tr1.Visible == true)
                    {
                        //if (drpCustomer.SelectedValue == "Select Party Name")
                        //{
                        //}
                        //else
                        {
                            DataRow dr = dCrt.NewRow();

                            dr["Transid"] = "";
                            dr["Design"] = "Single Bulk Cutting";
                            dr["Rate"] = "0";

                            dr["meter"] = txtAvailableMtr.Text;
                            dr["Shirt"] = txtNoofShirts.Text;
                            dr["reqmeter"] = txtavamet1.Text;

                            dr["reqshirt"] = txttotshirt1.Text;

                            dr["Transid"] = "";
                            dr["Design"] = "Single Bulk Cutting";
                            dr["Rate"] = "0";
                            dr["meter"] = txtavamet1.Text;
                            dr["Shirt"] = Ntxtactshirt.Text;
                            dr["Reqmeter"] = txtavamet1.Text; ;
                            dr["Reqshirt"] = txttotshirt1.Text;
                            dr["ledgerid"] = "0";
                            dr["party"] = "0";
                            dr["Fitid"] = drpFit.SelectedValue;
                            dr["Fit"] = drpFit.SelectedItem.Text;

                            dr["S30FS"] = Btxt30fs.Text;
                            dr["S30HS"] = Btxt30hs.Text;

                            dr["S32FS"] = Btxt32fs.Text;
                            dr["S32HS"] = Btxt32hs.Text;

                            dr["S34FS"] = Btxt34fs.Text;
                            dr["S34HS"] = Btxt34hs.Text;
                            dr["S36FS"] = Btxt36fs.Text;
                            dr["S36HS"] = Btxt36hs.Text;
                            dr["SXSFS"] = Btxtxsfs.Text;
                            dr["SXSHS"] = Btxtxshs.Text;
                            dr["SLFS"] = txtlfs.Text;
                            dr["SLHS"] = txtlhs.Text;
                            dr["SXLFS"] = txtxlfs.Text;
                            dr["SXLHS"] = txtxlhs.Text;
                            dr["SXXLFS"] = txtxxlfs.Text;
                            dr["SXXLHS"] = txtxxlhs.Text;
                            dr["S3XLFS"] = txtxxxlfs.Text;
                            dr["S3XLHS"] = txtxxxlhs.Text;
                            dr["S4XLFS"] = txtxxxxlfs.Text;
                            dr["S4XLHS"] = txtxxxxlhs.Text;
                            dr["SSFS"] = txtsfs.Text;
                            dr["SSHS"] = txtshs.Text;
                            dr["SMFS"] = txtmfs.Text;
                            dr["SMHS"] = txtmhs.Text;
                            dr["Itemname"] = txtitemname.Text;
                            dr["Pattern"] = drppattern.SelectedValue;
                            dr["PatternName"] = drppattern.SelectedItem.Text;

                            dr["WSP"] = Stxtwsp.Text;
                            dr["avgsize"] = txtavvgmeter.Text;
                            dr["Extra"] = "0";
                            dr["LLedger"] = "0";
                            dr["Mainlab"] = "0";
                            dr["FItLab"] = drpFit.SelectedValue;
                            dr["Washlab"] = "0";
                            dr["Logolab"] = "0";
                            dr["Total"] = tot.ToString();

                            dr["Contrast"] = "000";

                            dCrt.Rows.Add(dr);
                        }
                    }
                    else
                    {


                    }



                }
                else
                {
                    if (tr1.Visible == true)
                    {
                        //if (drpCustomer.SelectedValue == "Select Party Name")
                        //{
                        //}
                        //else
                        {
                            DataRow dr = dCrt.NewRow();

                            dr["Transid"] = "";
                            dr["Design"] = "Single Bulk Cutting";
                            dr["Rate"] = "0";

                            dr["meter"] = txtAvailableMtr.Text;
                            dr["Shirt"] = txtNoofShirts.Text;
                            dr["reqmeter"] = txtavamet1.Text;

                            dr["reqshirt"] = txttotshirt1.Text;

                            dr["Transid"] = "";
                            dr["Design"] = "Single Bulk Cutting";
                            dr["Rate"] = "0";
                            dr["meter"] = txtavamet1.Text;
                            dr["Shirt"] = Ntxtactshirt.Text;
                            dr["Reqmeter"] = txtavamet1.Text; ;
                            dr["Reqshirt"] = txttotshirt1.Text;
                            dr["ledgerid"] = "0";
                            dr["party"] = "0";
                            dr["Fitid"] = drpFit.SelectedValue;
                            dr["Fit"] = drpFit.SelectedItem.Text;

                            dr["S30FS"] = Btxt30fs.Text;
                            dr["S30HS"] = Btxt30hs.Text;

                            dr["S32FS"] = Btxt32fs.Text;
                            dr["S32HS"] = Btxt32hs.Text;

                            dr["S34FS"] = Btxt34fs.Text;
                            dr["S34HS"] = Btxt34hs.Text;
                            dr["S36FS"] = Btxt36fs.Text;
                            dr["S36HS"] = Btxt36hs.Text;
                            dr["SXSFS"] = Btxtxsfs.Text;
                            dr["SXSHS"] = Btxtxshs.Text;
                            dr["SLFS"] = txtlfs.Text;
                            dr["SLHS"] = txtlhs.Text;
                            dr["SXLFS"] = txtxlfs.Text;
                            dr["SXLHS"] = txtxlhs.Text;
                            dr["SXXLFS"] = txtxxlfs.Text;
                            dr["SXXLHS"] = txtxxlhs.Text;
                            dr["S3XLFS"] = txtxxxlfs.Text;
                            dr["S3XLHS"] = txtxxxlhs.Text;
                            dr["S4XLFS"] = txtxxxxlfs.Text;
                            dr["S4XLHS"] = txtxxxxlhs.Text;
                            dr["SSFS"] = txtsfs.Text;
                            dr["SSHS"] = txtshs.Text;
                            dr["SMFS"] = txtmfs.Text;
                            dr["SMHS"] = txtmhs.Text;
                            dr["Itemname"] = txtitemname.Text;
                            dr["Pattern"] = drppattern.SelectedValue;
                            dr["PatternName"] = drppattern.SelectedItem.Text;

                            dr["WSP"] = Stxtwsp.Text;
                            dr["avgsize"] = txtavvgmeter.Text;
                            dr["Extra"] = "0";
                            dr["LLedger"] = "0";
                            dr["Mainlab"] = "0";
                            dr["FItLab"] = drpFit.SelectedValue;
                            dr["Washlab"] = "0";
                            dr["Logolab"] = "0";
                            dr["Total"] = tot.ToString();

                            dr["Contrast"] = "000";

                            dCrt.Rows.Add(dr);
                        }
                    }
                    else
                    {

                    }
                }

                #endregion
            }

            gvcustomerorder.DataSource = dCrt;
            gvcustomerorder.DataBind();

            #region


            if (chkSizes.SelectedIndex >= 0)
            {
                gvcustomerorder.Columns[13].Visible = false; //30FS
                gvcustomerorder.Columns[14].Visible = false; //32FS

                gvcustomerorder.Columns[15].Visible = false;//34Fs
                gvcustomerorder.Columns[16].Visible = false;//36Fs

                gvcustomerorder.Columns[17].Visible = false; //XSFS
                gvcustomerorder.Columns[18].Visible = false; //SFS

                gvcustomerorder.Columns[19].Visible = false; //MFS
                gvcustomerorder.Columns[20].Visible = false; //LFS

                gvcustomerorder.Columns[21].Visible = false; //XLFS
                gvcustomerorder.Columns[22].Visible = false; //xxlFS

                gvcustomerorder.Columns[23].Visible = false; //3xlHS
                gvcustomerorder.Columns[24].Visible = false; //4xlHS

                gvcustomerorder.Columns[25].Visible = false; //30HS

                gvcustomerorder.Columns[26].Visible = false; //32HS

                gvcustomerorder.Columns[27].Visible = false; //34HS
                gvcustomerorder.Columns[28].Visible = false; //36HS

                gvcustomerorder.Columns[29].Visible = false; //XSHS
                gvcustomerorder.Columns[30].Visible = false; //SHS

                gvcustomerorder.Columns[31].Visible = false; //MHS
                gvcustomerorder.Columns[32].Visible = false; //LHS

                gvcustomerorder.Columns[33].Visible = false; //XLHS
                gvcustomerorder.Columns[34].Visible = false; //XXLHS

                gvcustomerorder.Columns[35].Visible = false; //3XLHS
                gvcustomerorder.Columns[36].Visible = false; //4XLHS




                int lop = 0;
                //Loop through each item of checkboxlist
                foreach (System.Web.UI.WebControls.ListItem item in chkSizes.Items)
                {
                    //check if item selected

                    if (item.Selected)
                    {

                        {
                            if (item.Text == "30FS")
                            {
                                gvcustomerorder.Columns[13].Visible = true;
                            }
                            if (item.Text == "32FS")
                            {
                                gvcustomerorder.Columns[14].Visible = true;
                            }
                            if (item.Text == "34FS")
                            {
                                gvcustomerorder.Columns[15].Visible = true;
                            }
                            if (item.Text == "36FS")
                            {
                                gvcustomerorder.Columns[16].Visible = true;
                            }
                            if (item.Text == "XSFS")
                            {
                                gvcustomerorder.Columns[17].Visible = true;
                            }
                            if (item.Text == "SFS")
                            {
                                gvcustomerorder.Columns[18].Visible = true;
                            }
                            if (item.Text == "MFS")
                            {
                                gvcustomerorder.Columns[19].Visible = true;
                            }
                            if (item.Text == "LFS")
                            {
                                gvcustomerorder.Columns[20].Visible = true;
                            }
                            if (item.Text == "XLFS")
                            {
                                gvcustomerorder.Columns[21].Visible = true;
                            }
                            if (item.Text == "XXLFS")
                            {
                                gvcustomerorder.Columns[22].Visible = true;
                            }
                            if (item.Text == "3XLFS")
                            {
                                gvcustomerorder.Columns[23].Visible = true;
                            }
                            if (item.Text == "4XLFS")
                            {
                                gvcustomerorder.Columns[24].Visible = true;
                            }


                            // FOR HS

                            if (item.Text == "30HS")
                            {
                                gvcustomerorder.Columns[25].Visible = true;
                            }

                            if (item.Text == "32HS")
                            {
                                gvcustomerorder.Columns[26].Visible = true;
                            }

                            if (item.Text == "34HS")
                            {
                                gvcustomerorder.Columns[27].Visible = true;
                            }

                            if (item.Text == "36HS")
                            {
                                gvcustomerorder.Columns[28].Visible = true;

                            }

                            if (item.Text == "XSHS")
                            {
                                gvcustomerorder.Columns[29].Visible = true;
                            }

                            if (item.Text == "SHS")
                            {
                                gvcustomerorder.Columns[30].Visible = true;
                            }

                            if (item.Text == "MHS")
                            {
                                gvcustomerorder.Columns[31].Visible = true;
                            }

                            if (item.Text == "LHS")
                            {
                                gvcustomerorder.Columns[32].Visible = true;
                            }

                            if (item.Text == "XLHS")
                            {
                                gvcustomerorder.Columns[33].Visible = true;
                            }

                            if (item.Text == "XXLHS")
                            {
                                gvcustomerorder.Columns[34].Visible = true;
                            }

                            if (item.Text == "3XLHS")
                            {
                                gvcustomerorder.Columns[35].Visible = true;
                            }

                            if (item.Text == "4XLHS")
                            {
                                gvcustomerorder.Columns[36].Visible = true;
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
                gvcustomerorder.Columns[13].Visible = false; //30FS
                gvcustomerorder.Columns[14].Visible = false; //32FS

                gvcustomerorder.Columns[15].Visible = false;//34Fs
                gvcustomerorder.Columns[16].Visible = false;//36Fs

                gvcustomerorder.Columns[17].Visible = false; //XSFS
                gvcustomerorder.Columns[18].Visible = false; //SFS

                gvcustomerorder.Columns[19].Visible = false; //MFS
                gvcustomerorder.Columns[20].Visible = false; //LFS

                gvcustomerorder.Columns[21].Visible = false; //XLFS
                gvcustomerorder.Columns[22].Visible = false; //xxlFS

                gvcustomerorder.Columns[23].Visible = false; //3xlHS
                gvcustomerorder.Columns[24].Visible = false; //4xlHS

                gvcustomerorder.Columns[25].Visible = false; //30HS

                gvcustomerorder.Columns[26].Visible = false; //32HS

                gvcustomerorder.Columns[27].Visible = false; //34HS
                gvcustomerorder.Columns[28].Visible = false; //36HS

                gvcustomerorder.Columns[29].Visible = false; //XSHS
                gvcustomerorder.Columns[30].Visible = false; //SHS

                gvcustomerorder.Columns[31].Visible = false; //MHS
                gvcustomerorder.Columns[32].Visible = false; //LHS

                gvcustomerorder.Columns[33].Visible = false; //XLHS
                gvcustomerorder.Columns[34].Visible = false; //XXLHS

                gvcustomerorder.Columns[35].Visible = false; //3XLHS
                gvcustomerorder.Columns[36].Visible = false; //4XLHS

            }


            #endregion

            dddldesign.ClearSelection();
            txtDesignRate.Text = "";
            txtReqMtr.Text = Ntxtremmeter.Text;
            //  txtAvailableMtr.Text = "";
            // txtNoofShirts.Text = "";
            //  txtReqMtr.Text = "";
            //  txtReqNoShirts.Text = "";
            //  txtextrashirt.Text = "";
            // drpCustomer.ClearSelection();
            //   drpFit.ClearSelection();

            Btxt30fs.Text = "0";
            Btxt32fs.Text = "0";
            Btxt34fs.Text = "0";
            Btxt36fs.Text = "0";
            Btxtxsfs.Text = "0";
            txtsfs.Text = "0";
            txtmfs.Text = "0";
            txtlfs.Text = "0";
            txtxlfs.Text = "0";
            txtxxlfs.Text = "0";
            txtxxxlfs.Text = "0";
            txtxxxxlfs.Text = "0";
            //FOR HS
            Btxt30hs.Text = "0";
            Btxt32hs.Text = "0";
            Btxt34hs.Text = "0";
            Btxt36hs.Text = "0";
            Btxtxshs.Text = "0";
            txtshs.Text = "0";
            txtmhs.Text = "0";
            txtlhs.Text = "0";
            txtxlhs.Text = "0";
            txtxxlhs.Text = "0";
            txtxxxlhs.Text = "0";
            txtxxxxlhs.Text = "0";

            txtavamet1.Text = "0";
            txttotshirt1.Text = "0";
            txtavvgmeter.Text = "0";
            FirstGridViewRow();
            removedropdownlist();
            // dddldesign.Focus();
            System.Threading.Thread.Sleep(3000);
            UpdatePanel7.Update();
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
                    string trainid = dsnneeww.Tables[0].Rows[i]["Fitid"].ToString();
                    if (myDS.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < myDS.Tables[0].Rows.Count; j++)
                        {

                            string idd = myDS.Tables[0].Rows[j]["Fitid"].ToString();
                            if (idd == trainid)
                            {
                                drpFit.Items.Remove(drpFit.Items.FindByValue(idd));
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

                    //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                    double wid = 0;
                    if (drpFit.SelectedValue == "3")
                    {
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                ////   btnprocess.Enabled = false;
                //return;
            }

        }

        public void getzeroforemptysize()
        {

            // FOR FS
            if (Btxt30fs.Text == "")
            {
                Btxt30fs.Text = "0";
            }
            if (Btxt32fs.Text == "")
            {
                Btxt32fs.Text = "0";
            }
            if (Btxt34fs.Text == "")
            {
                Btxt34fs.Text = "0";
            }
            if (Btxt36fs.Text == "")
            {
                Btxt36fs.Text = "0";
            }
            if (Btxtxsfs.Text == "")
            {
                Btxtxsfs.Text = "0";
            }
            if (txtsfs.Text == "")
            {
                txtsfs.Text = "0";
            }
            if (txtmfs.Text == "")
            {
                txtmfs.Text = "0";
            }
            if (txtlfs.Text == "")
            {
                txtlfs.Text = "0";
            }
            if (txtxlfs.Text == "")
            {
                txtxlfs.Text = "0";
            }
            if (txtxxlfs.Text == "")
            {
                txtxxlfs.Text = "0";
            }
            if (txtxxxlfs.Text == "")
            {
                txtxxxlfs.Text = "0";
            }
            if (txtxxxxlfs.Text == "")
            {
                txtxxxxlfs.Text = "0";
            }

            //FOR HS
            if (Btxt30hs.Text == "")
            {
                Btxt30hs.Text = "0";
            }
            if (Btxt32hs.Text == "")
            {
                Btxt32hs.Text = "0";
            }
            if (Btxt34hs.Text == "")
            {
                Btxt34hs.Text = "0";
            }
            if (Btxt36hs.Text == "")
            {
                Btxt36hs.Text = "0";
            }
            if (Btxtxshs.Text == "")
            {
                Btxtxshs.Text = "0";
            }
            if (txtshs.Text == "")
            {
                txtshs.Text = "0";
            }
            if (txtmhs.Text == "")
            {
                txtmhs.Text = "0";
            }
            if (txtlhs.Text == "")
            {
                txtlhs.Text = "0";
            }
            if (txtxlhs.Text == "")
            {
                txtxlhs.Text = "0";
            }
            if (txtxxlhs.Text == "")
            {
                txtxxlhs.Text = "0";
            }
            if (txtxxxlhs.Text == "")
            {
                txtxxxlhs.Text = "0";
            }
            if (txtxxxxlhs.Text == "")
            {
                txtxxxxlhs.Text = "0";
            }

        }
        //Single Process
        protected void Schange36fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();

            //if (txt36FS.Text == "0" || txt36FS.Text == "")
            //{
            //    txt36FS.Text = "0";
            //}
            //else
            //{

            //}





            //    tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //      Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                //if (roundoff > 0.5)
                //{
                //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                //}
                //else
                //{
                //    r = Math.Floor(Convert.ToDouble(roundoff));
                //}

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt38FS.Focus();
            //if (txt38FS.Text == "0")
            //{
            //    txt38FS.Text = "";
            //}
            //else
            //{

            //}
            //if (tefs.Visible == true)
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
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(300);
        }

        protected void Schange38fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //  if (txt38FS.Text == "0" || txt38FS.Text == "")
            {
                //      txt38FS.Text = "0";
            }
            //  else
            {

            }

            //   tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //      Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                //if (roundoff > 0.5)
                //{
                //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                //}
                //else
                //{
                //    r = Math.Floor(Convert.ToDouble(roundoff));
                //}

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt39FS.Focus();
            //if (txt39FS.Text == "0")
            //{
            //    txt39FS.Text = "";
            //}
            //else
            //{

            //}
            //if (tefs.Visible == true)
            //{
            //    txt38FS.Focus();
            //    if (txt38FS.Text == "0")
            //    {
            //        txt38FS.Text = "";
            //    }
            //}
            //if (tnfs.Visible == true)
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
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }


        protected void Schange39fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //if (txt39FS.Text == "0" || txt39FS.Text == "")
            //{
            //    txt39FS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
                }

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                //if (roundoff > 0.5)
                //{
                //    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                //}
                //else
                //{
                //    r = Math.Floor(Convert.ToDouble(roundoff));
                //}

                //     txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt40FS.Focus();
            //if (txt40FS.Text == "0")
            //{
            //    txt40FS.Text = "";
            //}
            //else
            //{

            //}
            //if (tefs.Visible == true)
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
            //if (fzfs.Visible == true)
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
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }

        protected void Schange40fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;

            getzeroforemptysize();
            //if (txt40FS.Text == "0" || txt40FS.Text == "")
            //{
            //    txt40FS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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

                //   txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt42FS.Focus();
            //if (txt42FS.Text == "0")
            //{
            //    txt42FS.Text = "";
            //}
            //else
            //{

            //}

            //if (tefs.Visible == true)
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
            //if (ftfs.Visible == true)
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
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }

        protected void Schange42fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //if (txt42FS.Text == "0" || txt42FS.Text == "")
            //{
            //    txt42FS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);+
                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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

                //   txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt44FS.Focus();
            //if (txt44FS.Text == "0")
            //{
            //    txt44FS.Text = "";
            //}
            //else
            //{

            //}
            //if (tefs.Visible == true)
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
            //if (fffs.Visible == true)
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
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }

        protected void Schange44fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //if (txt44FS.Text == "0" || txt44FS.Text == "")
            //{
            //    txt44FS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);+
                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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

                //   txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt36HS.Focus();
            //if (txt36HS.Text == "0")
            //{
            //    txt36HS.Text = "";
            //}
            //else
            //{

            //}
            //if (tshs.Visible == true)
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
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }


        protected void Schange36hs(object sender, EventArgs e)
        {

            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //if (txt36HS.Text == "0" || txt36HS.Text == "")
            //{
            //    txt36HS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt38HS.Focus();
            //if (txt38HS.Text == "0")
            //{
            //    txt38HS.Text = "";
            //}
            //else
            //{

            //}

            //if (tshs.Visible == true)
            //{
            //    txt36HS.Focus();
            //    if (txt36HS.Text == "0")
            //    {
            //        txt36HS.Text = "";
            //    }
            //}

            //if (tehs.Visible == true)
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
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }

        protected void Schange38hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //if (txt38HS.Text == "0" || txt38HS.Text == "")
            //{
            //    txt38HS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);


            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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

                //     txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt39HS.Focus();
            //if (txt39HS.Text == "0")
            //{
            //    txt39HS.Text = "";
            //}
            //else
            //{

            //}

            //if (tehs.Visible == true)
            //{
            //    txt38HS.Focus();
            //    if (txt38HS.Text == "0")
            //    {
            //        txt38HS.Text = "";
            //    }
            //}
            //if (tnhs.Visible == true)
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
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }


        protected void Schange39hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //if (txt39HS.Text == "0" || txt39HS.Text == "")
            //{
            //    txt39HS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //      Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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

                //     txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt40HS.Focus();
            //if (txt40HS.Text == "0")
            //{
            //    txt40HS.Text = "";
            //}
            //else
            //{

            //}

            //if (tehs.Visible == true)
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
            //if (fzhs.Visible == true)
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
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }

        protected void Schange40hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //if (txt40HS.Text == "0" || txt40HS.Text == "")
            //{
            //    txt40HS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //       Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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
                //    txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt42HS.Focus();
            //if (txt42HS.Text == "0")
            //{
            //    txt42HS.Text = "";
            //}
            //else
            //{

            //}
            //if (tehs.Visible == true)
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
            //if (fths.Visible == true)
            //{
            //    txt42HS.Focus();
            //    if (txt42HS.Text == "0")
            //    {
            //        txt42HS.Text = "";
            //    }
            //}
            //else if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }

        protected void Schange42hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //if (txt42HS.Text == "0" || txt42HS.Text == "")
            //{
            //    txt42HS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //     Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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

                //     txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            //txt44HS.Focus();
            //if (txt44HS.Text == "0")
            //{
            //    txt44HS.Text = "";
            //}
            //else
            //{

            //}

            //if (ffhs.Visible == true)
            //{
            //    txt44HS.Focus();
            //    if (txt44HS.Text == "0")
            //    {
            //        txt44HS.Text = "";
            //    }
            //}

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }

        protected void Schange44hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();
            //if (txt44HS.Text == "0" || txt44HS.Text == "")
            //{
            //    txt44HS.Text = "0";
            //}
            //else
            //{

            //}

            //tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //    Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            gndtot = gndtot + tot;



            DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtavgmeter.Text);
                }
                else
                {
                    wid = Convert.ToDouble(txtexec.Text);
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

                //      txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Stxtwsp.Focus();

            // dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }

        }


        public void avgmeter()
        {
            txtavvgmeter.Text = (Convert.ToDouble(txtavamet1.Text) / Convert.ToDouble(txttotshirt1.Text)).ToString("N");


        }


        #region SINGLE NEW PROCESS
        protected void Schange30fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();

            if (Btxt30fs.Text == "0" || Btxt30fs.Text == "")
            {
                Btxt30fs.Text = "0";
            }
            else
            {

            }





            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }


                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxt30fs.Focus();
            if (Btxt30fs.Text == "0")
            {
                Btxt30fs.Text = "";
            }
            else
            {

            }
            if (S32fs.Visible == true)
            {
                Btxt32fs.Focus();
                if (Btxt32fs.Text == "0")
                {
                    Btxt32fs.Text = "";
                }
            }
            else if (S34fs.Visible == true)
            {
                Btxt34fs.Focus();
                if (Btxt34fs.Text == "0")
                {
                    Btxt34fs.Text = "";
                }
            }
            else if (S36fs.Visible == true)
            {
                Btxt36fs.Focus();
                if (Btxt36fs.Text == "0")
                {
                    Btxt36fs.Text = "";
                }
            }
            else if (Xsfs.Visible == true)
            {
                Btxtxsfs.Focus();
                if (Btxtxsfs.Text == "0")
                {
                    Btxtxsfs.Text = "";
                }
            }
            else if (sfs.Visible == true)
            {
                txtsfs.Focus();
                if (txtsfs.Text == "0")
                {
                    txtsfs.Text = "";
                }
            }
            else if (mfs.Visible == true)
            {
                txtmfs.Focus();
                if (txtmfs.Text == "0")
                {
                    txtmfs.Text = "";
                }
            }

            else if (lfs.Visible == true)
            {
                txtlfs.Focus();
                if (txtlfs.Text == "0")
                {
                    txtlfs.Text = "";
                }
            }
            else if (xlfs.Visible == true)
            {
                txtxlfs.Focus();
                if (txtxlfs.Text == "0")
                {
                    txtxlfs.Text = "";
                }
            }
            else if (xxlfs.Visible == true)
            {
                txtxxlfs.Focus();
                if (txtxxlfs.Text == "0")
                {
                    txtxxlfs.Text = "";
                }
            }
            else if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            // txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            //// txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            // if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            // {
            //     ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //     btnadd.Enabled = false;
            //     btnprocess.Enabled = false;
            //     return;
            // }
            // else
            // {
            //     btnprocess.Enabled = true;
            // }
            System.Threading.Thread.Sleep(30);
        }


        protected void Schange32fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (Btxt32fs.Text == "0" || Btxt32fs.Text == "")
            {
                Btxt32fs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //   txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxt32fs.Focus();
            if (Btxt32fs.Text == "0")
            {
                Btxt32fs.Text = "";
            }
            else
            {

            }
            if (S34fs.Visible == true)
            {
                Btxt34fs.Focus();
                if (Btxt34fs.Text == "0")
                {
                    Btxt34fs.Text = "";
                }
            }
            else if (S36fs.Visible == true)
            {
                Btxt36fs.Focus();
                if (Btxt36fs.Text == "0")
                {
                    Btxt36fs.Text = "";
                }
            }
            else if (Xsfs.Visible == true)
            {
                Btxtxsfs.Focus();
                if (Btxtxsfs.Text == "0")
                {
                    Btxtxsfs.Text = "";
                }
            }
            else if (sfs.Visible == true)
            {
                txtsfs.Focus();
                if (txtsfs.Text == "0")
                {
                    txtsfs.Text = "";
                }
            }
            else if (mfs.Visible == true)
            {
                txtmfs.Focus();
                if (txtmfs.Text == "0")
                {
                    txtmfs.Text = "";
                }
            }

            else if (lfs.Visible == true)
            {
                txtlfs.Focus();
                if (txtlfs.Text == "0")
                {
                    txtlfs.Text = "";
                }
            }
            else if (xlfs.Visible == true)
            {
                txtxlfs.Focus();
                if (txtxlfs.Text == "0")
                {
                    txtxlfs.Text = "";
                }
            }
            else if (xxlfs.Visible == true)
            {
                txtxxlfs.Focus();
                if (txtxxlfs.Text == "0")
                {
                    txtxxlfs.Text = "";
                }
            }
            else if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void Schange34fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (Btxt34fs.Text == "0" || Btxt34fs.Text == "")
            {
                Btxt34fs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxt34fs.Focus();
            if (Btxt34fs.Text == "0")
            {
                Btxt34fs.Text = "";
            }
            else
            {

            }
            if (S36fs.Visible == true)
            {
                Btxt36fs.Focus();
                if (Btxt36fs.Text == "0")
                {
                    Btxt36fs.Text = "";
                }
            }
            else if (Xsfs.Visible == true)
            {
                Btxtxsfs.Focus();
                if (Btxtxsfs.Text == "0")
                {
                    Btxtxsfs.Text = "";
                }
            }
            else if (sfs.Visible == true)
            {
                txtsfs.Focus();
                if (txtsfs.Text == "0")
                {
                    txtsfs.Text = "";
                }
            }
            else if (mfs.Visible == true)
            {
                txtmfs.Focus();
                if (txtmfs.Text == "0")
                {
                    txtmfs.Text = "";
                }
            }

            else if (lfs.Visible == true)
            {
                txtlfs.Focus();
                if (txtlfs.Text == "0")
                {
                    txtlfs.Text = "";
                }
            }
            else if (xlfs.Visible == true)
            {
                txtxlfs.Focus();
                if (txtxlfs.Text == "0")
                {
                    txtxlfs.Text = "";
                }
            }
            else if (xxlfs.Visible == true)
            {
                txtxxlfs.Focus();
                if (txtxxlfs.Text == "0")
                {
                    txtxxlfs.Text = "";
                }
            }
            else if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }

        protected void NewSizeRatioGrid_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                //GVSizesView.DataSource = null;
                //GVSizesView.DataBind();

                if (e.CommandArgument.ToString() != "")
                {
                    #region

                    DataTable DTGVSizeDetails = (DataTable)ViewState["NewSizeRatioGrid"];
                    DataRow[] RowsGVSizeDetails = DTGVSizeDetails.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    RowId.Text = e.CommandArgument.ToString();
                    ItemName.Text = RowsGVSizeDetails[0]["ItemName"].ToString();
                    Itemid.Text = RowsGVSizeDetails[0]["Itemid"].ToString();
                    Fitname.Text = RowsGVSizeDetails[0]["Fitname"].ToString();
                    Fitid.Text = RowsGVSizeDetails[0]["Fitid"].ToString();
                    Givenmeter.Text = RowsGVSizeDetails[0]["Givenmeter"].ToString();
                    Avgmeter.Text = RowsGVSizeDetails[0]["Avgmeter"].ToString();
                    Totalshirt.Text = RowsGVSizeDetails[0]["Totalshirt"].ToString();
                    //StyleNo.Text = RowsGVSizeDetails[0]["StyleNo"].ToString();
                    //StyleNoId.Text = RowsGVSizeDetails[0]["StyleNoId"].ToString();
                    //Description.Text = RowsGVSizeDetails[0]["Description"].ToString();
                    //Color.Text = RowsGVSizeDetails[0]["Color"].ToString();
                    //ColorId.Text = RowsGVSizeDetails[0]["ColorId"].ToString();

                    //Sizes.Text = RowsGVSizeDetails[0]["Size"].ToString();
                    //RangeId.Text = RowsGVSizeDetails[0]["RangeId"].ToString();

                    //Rate.Text = RowsGVSizeDetails[0]["Rate"].ToString();
                    //txtrate.Text = Rate.Text;
                    //Qty.Text = RowsGVSizeDetails[0]["Qty"].ToString();

                    //IssueQty.Text = RowsGVSizeDetails[0]["IssueQty"].ToString();
                    //ReceiveQty.Text = RowsGVSizeDetails[0]["ReceiveQty"].ToString();
                    //DamageQty.Text = RowsGVSizeDetails[0]["DamageQty"].ToString();

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();
                    DataRow drNew1;
                    DataColumn dct1;
                    DataTable dttt1 = new DataTable();

                    dct1 = new DataColumn("RowId");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);



                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        //DataSet dsBrandSize = objBs.selectallSize_BrandID(ddlbrand.SelectedValue);
                        //if (dsBrandSize.Tables[0].Rows.Count > 0)
                        //{
                        //    for (int m = 0; m <= dsBrandSize.Tables[0].Rows.Count - 1; m++)
                        //    {
                        drNew1 = dttt1.NewRow();
                        //drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        //drNew1["Size"] = dsBrandSize.Tables[0].Rows[m]["Size"].ToString();// RowsGVSizeQty[i]["Size"].ToString();
                        //drNew1["SizeId"] = dsBrandSize.Tables[0].Rows[m]["SizeId"].ToString();// RowsGVSizeQty[i]["SizeId"].ToString();
                        //drNew1["Qty"] = "";// RowsGVSizeQty[i]["Qty"].ToString();                    

                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();// RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();// RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];
                        //}
                        //}
                    }

                    GVSizes.DataSource = dstd1;
                    GVSizes.DataBind();

                    #endregion
                }
            }
            else if (e.CommandName == "View")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    #region

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();

                    DataRow drNew1;
                    DataColumn dct1;

                    DataTable dttt1 = new DataTable();


                    dct1 = new DataColumn("RowId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("IssueQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("ReceiveQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("DamageQty");
                    dttt1.Columns.Add(dct1);

                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();
                        drNew1["IssueQty"] = RowsGVSizeQty[i]["IssueQty"].ToString();
                        drNew1["ReceiveQty"] = RowsGVSizeQty[i]["ReceiveQty"].ToString();
                        drNew1["DamageQty"] = RowsGVSizeQty[i]["DamageQty"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    //GVSizesView.DataSource = dstd1;
                    //GVSizesView.DataBind();


                    #endregion
                }
            }

        }

        protected void RatioShirtProcess_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                //GVSizesView.DataSource = null;
                //GVSizesView.DataBind();

                if (e.CommandArgument.ToString() != "")
                {
                    #region

                    DataTable DTGVSizeDetails = (DataTable)ViewState["NewSizeRatioGrid"];
                    DataRow[] RowsGVSizeDetails = DTGVSizeDetails.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    RowIdSize.Text = e.CommandArgument.ToString();
                    ItemNameSize.Text = RowsGVSizeDetails[0]["ItemName"].ToString();
                    ItemidSize.Text = RowsGVSizeDetails[0]["Itemid"].ToString();
                    FitnameSize.Text = RowsGVSizeDetails[0]["Fitname"].ToString();
                    FitidSize.Text = RowsGVSizeDetails[0]["Fitid"].ToString();
                    GivenmeterSize.Text = RowsGVSizeDetails[0]["Givenmeter"].ToString();
                    AvgmeterSize.Text = RowsGVSizeDetails[0]["Avgmeter"].ToString();
                    TotalshirtSize.Text = RowsGVSizeDetails[0]["Totalshirt"].ToString();
                    //StyleNo.Text = RowsGVSizeDetails[0]["StyleNo"].ToString();
                    //StyleNoId.Text = RowsGVSizeDetails[0]["StyleNoId"].ToString();
                    //Description.Text = RowsGVSizeDetails[0]["Description"].ToString();
                    //Color.Text = RowsGVSizeDetails[0]["Color"].ToString();
                    //ColorId.Text = RowsGVSizeDetails[0]["ColorId"].ToString();

                    //Sizes.Text = RowsGVSizeDetails[0]["Size"].ToString();
                    //RangeId.Text = RowsGVSizeDetails[0]["RangeId"].ToString();

                    //Rate.Text = RowsGVSizeDetails[0]["Rate"].ToString();
                    //txtrate.Text = Rate.Text;
                    //Qty.Text = RowsGVSizeDetails[0]["Qty"].ToString();

                    //IssueQty.Text = RowsGVSizeDetails[0]["IssueQty"].ToString();
                    //ReceiveQty.Text = RowsGVSizeDetails[0]["ReceiveQty"].ToString();
                    //DamageQty.Text = RowsGVSizeDetails[0]["DamageQty"].ToString();

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();
                    DataRow drNew1;
                    DataColumn dct1;
                    DataTable dttt1 = new DataTable();

                    dct1 = new DataColumn("RowId");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);

                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);



                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        //DataSet dsBrandSize = objBs.selectallSize_BrandID(ddlbrand.SelectedValue);
                        //if (dsBrandSize.Tables[0].Rows.Count > 0)
                        //{
                        //    for (int m = 0; m <= dsBrandSize.Tables[0].Rows.Count - 1; m++)
                        //    {
                        drNew1 = dttt1.NewRow();
                        //drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        //drNew1["Size"] = dsBrandSize.Tables[0].Rows[m]["Size"].ToString();// RowsGVSizeQty[i]["Size"].ToString();
                        //drNew1["SizeId"] = dsBrandSize.Tables[0].Rows[m]["SizeId"].ToString();// RowsGVSizeQty[i]["SizeId"].ToString();
                        //drNew1["Qty"] = "";// RowsGVSizeQty[i]["Qty"].ToString();                    

                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();// RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();// RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];
                        //}
                        //}
                    }

                    RatioShirtProcessSizes.DataSource = dstd1;
                    RatioShirtProcessSizes.DataBind();

                    #endregion
                }
            }
            else if (e.CommandName == "View")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    #region

                    DataSet dstd1 = new DataSet();
                    DataTable dtddd1 = new DataTable();

                    DataRow drNew1;
                    DataColumn dct1;

                    DataTable dttt1 = new DataTable();


                    dct1 = new DataColumn("RowId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Size");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("SizeId");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("Qty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("IssueQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("ReceiveQty");
                    dttt1.Columns.Add(dct1);
                    dct1 = new DataColumn("DamageQty");
                    dttt1.Columns.Add(dct1);

                    dstd1.Tables.Add(dttt1);

                    DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];
                    DataRow[] RowsGVSizeQty = DTGVSizeQty.Select("RowId='" + e.CommandArgument.ToString() + "'");

                    for (int i = 0; i < RowsGVSizeQty.Length; i++)
                    {
                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = RowsGVSizeQty[i]["RowId"].ToString();
                        drNew1["Size"] = RowsGVSizeQty[i]["Size"].ToString();
                        drNew1["SizeId"] = RowsGVSizeQty[i]["SizeId"].ToString();
                        drNew1["Qty"] = RowsGVSizeQty[i]["Qty"].ToString();
                        drNew1["IssueQty"] = RowsGVSizeQty[i]["IssueQty"].ToString();
                        drNew1["ReceiveQty"] = RowsGVSizeQty[i]["ReceiveQty"].ToString();
                        drNew1["DamageQty"] = RowsGVSizeQty[i]["DamageQty"].ToString();

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }

                    //GVSizesView.DataSource = dstd1;
                    //GVSizesView.DataBind();


                    #endregion
                }
            }
            UpdatePanel8.Update();

        }


        protected void btnSubmitQty_OnClick1(object sender, EventArgs e)
        {
            if (GVSizes.Rows.Count > 0)
            {
                double IssueQty = 0;
                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");
                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";

                    IssueQty += Convert.ToDouble(txtIssueQty.Text);
                }
            }
        }

        protected void btnSubmitQty_OnClick(object sender, EventArgs e)
        {
            if (GVSizes.Rows.Count > 0)
            {
                double IssueQty = 0;
                for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                {
                    TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");

                    if (txtIssueQty.Text == "")
                        txtIssueQty.Text = "0";

                    IssueQty += Convert.ToDouble(txtIssueQty.Text);

                }

                #region CurrentTable Removed

                DataTable DTGVSizeDetails = (DataTable)ViewState["NewSizeRatioGrid"];

                DataRow[] DRItem = DTGVSizeDetails.Select("RowId='" + RowId.Text + "'");
                for (int i = 0; i < DRItem.Length; i++)
                    DRItem[i].Delete();
                DTGVSizeDetails.AcceptChanges();

                ViewState["NewSizeRatioGrid"] = DTGVSizeDetails;

                DataTable DTGVSizeQty = (DataTable)ViewState["CurrentTable2"];

                DataRow[] DRSize = DTGVSizeQty.Select("RowId='" + RowId.Text + "'");
                for (int i = 0; i < DRSize.Length; i++)
                    DRSize[i].Delete();
                DTGVSizeQty.AcceptChanges();

                ViewState["CurrentTable2"] = DTGVSizeQty;

                #endregion


                // string HttpCookieValue = "";

                DataSet dstd = new DataSet();
                DataTable dtddd = new DataTable();
                DataRow drNew;
                DataColumn dct;
                DataTable dttt = new DataTable();

                #region

                dct = new DataColumn("ItemName");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Itemid");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Fitname");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Fitid");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Givenmeter");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Avgmeter");
                dttt.Columns.Add(dct);

                dct = new DataColumn("Totalshirt");
                dttt.Columns.Add(dct);

                dct = new DataColumn("RowId");
                dttt.Columns.Add(dct);

                dct = new DataColumn("TotalshirtSize");
                dttt.Columns.Add(dct);

                dstd.Tables.Add(dttt);

                if (ViewState["NewSizeRatioGrid"] != null)
                {
                    DataTable dt = (DataTable)ViewState["NewSizeRatioGrid"];

                    drNew = dttt.NewRow();

                    drNew["RowId"] = RowId.Text;// nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    drNew["ItemName"] = ItemName.Text;
                    drNew["Itemid"] = Itemid.Text;
                    drNew["Fitname"] = Fitname.Text;
                    drNew["Fitid"] = Fitid.Text;
                    drNew["Givenmeter"] = Givenmeter.Text;
                    drNew["Avgmeter"] = Avgmeter.Text;
                    drNew["Totalshirt"] = Totalshirt.Text;
                    drNew["TotalshirtSize"] = IssueQty;
                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                    dtddd.Merge(dt);

                }
                else
                {
                    drNew = dttt.NewRow();

                    drNew["RowId"] = RowId.Text;// nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
                    drNew["ItemName"] = ItemName.Text;
                    drNew["Itemid"] = Itemid.Text;
                    drNew["Fitname"] = Fitname.Text;
                    drNew["Fitid"] = Fitid.Text;
                    drNew["Givenmeter"] = Givenmeter.Text;
                    drNew["Avgmeter"] = Avgmeter.Text;
                    drNew["Totalshirt"] = Totalshirt.Text;
                    drNew["TotalshirtSize"] = IssueQty;
                    //drNew["IssueQty"] = IssueQty;
                    //drNew["ReceiveQty"] = ReceiveQty;
                    //drNew["DamageQty"] = DamageQty;

                    dstd.Tables[0].Rows.Add(drNew);
                    dtddd = dstd.Tables[0];
                }

                #endregion

                ViewState["NewSizeRatioGrid"] = dtddd;
                NewSizeRatioGrid.DataSource = dtddd;
                NewSizeRatioGrid.DataBind();

                DataSet dstd1 = new DataSet();
                DataTable dtddd1 = new DataTable();
                DataRow drNew1;
                DataColumn dct1;
                DataTable dttt1 = new DataTable();

                #region

                dct1 = new DataColumn("RowId");
                dttt1.Columns.Add(dct1);

                dct1 = new DataColumn("Size");
                dttt1.Columns.Add(dct1);

                dct1 = new DataColumn("SizeId");
                dttt1.Columns.Add(dct1);

                dct1 = new DataColumn("Qty");
                dttt1.Columns.Add(dct1);

                dstd1.Tables.Add(dttt1);


                if (ViewState["CurrentTable2"] != null)
                {
                    HttpCookie nameCookie = Request.Cookies["Name"];

                    DataTable dt1 = (DataTable)ViewState["CurrentTable2"];

                    for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                    {
                        HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                        Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");
                        TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");

                        if (txtIssueQty.Text == "")
                            txtIssueQty.Text = "0";

                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = RowId.Text;// HttpCookieValue;
                        drNew1["Size"] = lblSize.Text;
                        drNew1["SizeId"] = hdSize.Value;
                        drNew1["Qty"] = txtIssueQty.Text;

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }
                    dtddd1.Merge(dt1);
                }
                else
                {
                    HttpCookie nameCookie = Request.Cookies["Name"];

                    for (int vLoop = 0; vLoop < GVSizes.Rows.Count; vLoop++)
                    {
                        HiddenField hdSize = (HiddenField)GVSizes.Rows[vLoop].FindControl("hdSize");
                        Label lblSize = (Label)GVSizes.Rows[vLoop].FindControl("lblSize");
                        TextBox txtIssueQty = (TextBox)GVSizes.Rows[vLoop].FindControl("txtIssueQty");

                        if (txtIssueQty.Text == "")
                            txtIssueQty.Text = "0";

                        drNew1 = dttt1.NewRow();

                        drNew1["RowId"] = RowId.Text;// HttpCookieValue;

                        drNew1["Size"] = lblSize.Text;
                        drNew1["SizeId"] = hdSize.Value;
                        drNew1["Qty"] = txtIssueQty.Text;

                        dstd1.Tables[0].Rows.Add(drNew1);
                        dtddd1 = dstd1.Tables[0];

                    }
                }

                #endregion

                ViewState["CurrentTable2"] = dtddd1;


            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Records Found.')", true);
                return;
            }

            GVSizes.DataSource = null;
            GVSizes.DataBind();
        }


        protected void NSchange36fs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (Btxt36fs.Text == "0" || Btxt36fs.Text == "")
            {
                Btxt36fs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxt36fs.Focus();
            if (Btxt36fs.Text == "0")
            {
                Btxt36fs.Text = "";
            }
            else
            {

            }
            if (Xsfs.Visible == true)
            {
                Btxtxsfs.Focus();
                if (Btxtxsfs.Text == "0")
                {
                    Btxtxsfs.Text = "";
                }
            }
            else if (sfs.Visible == true)
            {
                txtsfs.Focus();
                if (txtsfs.Text == "0")
                {
                    txtsfs.Text = "";
                }
            }
            else if (mfs.Visible == true)
            {
                txtmfs.Focus();
                if (txtmfs.Text == "0")
                {
                    txtmfs.Text = "";
                }
            }

            else if (lfs.Visible == true)
            {
                txtlfs.Focus();
                if (txtlfs.Text == "0")
                {
                    txtlfs.Text = "";
                }
            }
            else if (xlfs.Visible == true)
            {
                txtxlfs.Focus();
                if (txtxlfs.Text == "0")
                {
                    txtxlfs.Text = "";
                }
            }
            else if (xxlfs.Visible == true)
            {
                txtxxlfs.Focus();
                if (txtxxlfs.Text == "0")
                {
                    txtxxlfs.Text = "";
                }
            }
            else if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeXSfs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (Btxtxsfs.Text == "0" || Btxtxsfs.Text == "")
            {
                Btxtxsfs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxtxsfs.Focus();
            if (Btxtxsfs.Text == "0")
            {
                Btxtxsfs.Text = "";
            }
            else
            {

            }
            if (sfs.Visible == true)
            {
                txtsfs.Focus();
                if (txtsfs.Text == "0")
                {
                    txtsfs.Text = "";
                }
            }
            else if (mfs.Visible == true)
            {
                txtmfs.Focus();
                if (txtmfs.Text == "0")
                {
                    txtmfs.Text = "";
                }
            }

            else if (lfs.Visible == true)
            {
                txtlfs.Focus();
                if (txtlfs.Text == "0")
                {
                    txtlfs.Text = "";
                }
            }
            else if (xlfs.Visible == true)
            {
                txtxlfs.Focus();
                if (txtxlfs.Text == "0")
                {
                    txtxlfs.Text = "";
                }
            }
            else if (xxlfs.Visible == true)
            {
                txtxxlfs.Focus();
                if (txtxxlfs.Text == "0")
                {
                    txtxxlfs.Text = "";
                }
            }
            else if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }



        protected void SchangeSfs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtsfs.Text == "0" || txtsfs.Text == "")
            {
                txtsfs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtsfs.Focus();
            if (txtsfs.Text == "0")
            {
                txtsfs.Text = "";
            }
            else
            {

            }
            if (mfs.Visible == true)
            {
                txtmfs.Focus();
                if (txtmfs.Text == "0")
                {
                    txtmfs.Text = "";
                }
            }

            else if (lfs.Visible == true)
            {
                txtlfs.Focus();
                if (txtlfs.Text == "0")
                {
                    txtlfs.Text = "";
                }
            }
            else if (xlfs.Visible == true)
            {
                txtxlfs.Focus();
                if (txtxlfs.Text == "0")
                {
                    txtxlfs.Text = "";
                }
            }
            else if (xxlfs.Visible == true)
            {
                txtxxlfs.Focus();
                if (txtxxlfs.Text == "0")
                {
                    txtxxlfs.Text = "";
                }
            }
            else if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeMfs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtmfs.Text == "0" || txtmfs.Text == "")
            {
                txtmfs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtmfs.Focus();
            if (txtmfs.Text == "0")
            {
                txtmfs.Text = "";
            }
            else
            {

            }
            if (lfs.Visible == true)
            {
                txtlfs.Focus();
                if (txtlfs.Text == "0")
                {
                    txtlfs.Text = "";
                }
            }
            else if (xlfs.Visible == true)
            {
                txtxlfs.Focus();
                if (txtxlfs.Text == "0")
                {
                    txtxlfs.Text = "";
                }
            }
            else if (xxlfs.Visible == true)
            {
                txtxxlfs.Focus();
                if (txtxxlfs.Text == "0")
                {
                    txtxxlfs.Text = "";
                }
            }
            else if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeLfs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtlfs.Text == "0" || txtlfs.Text == "")
            {
                txtlfs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtlfs.Focus();
            if (txtlfs.Text == "0")
            {
                txtlfs.Text = "";
            }
            else
            {

            }
            if (xlfs.Visible == true)
            {
                txtxlfs.Focus();
                if (txtxlfs.Text == "0")
                {
                    txtxlfs.Text = "";
                }
            }
            else if (xxlfs.Visible == true)
            {
                txtxxlfs.Focus();
                if (txtxxlfs.Text == "0")
                {
                    txtxxlfs.Text = "";
                }
            }
            else if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }

        protected void SchangeXLfs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtxlfs.Text == "0" || txtxlfs.Text == "")
            {
                txtxlfs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtxlfs.Focus();
            if (txtxlfs.Text == "0")
            {
                txtxlfs.Text = "";
            }
            else
            {

            }
            if (xxlfs.Visible == true)
            {
                txtxxlfs.Focus();
                if (txtxxlfs.Text == "0")
                {
                    txtxxlfs.Text = "";
                }
            }
            else if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeXXLfs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtxxlfs.Text == "0" || txtxxlfs.Text == "")
            {
                txtxxlfs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtxxlfs.Focus();
            if (txtxxlfs.Text == "0")
            {
                txtxxlfs.Text = "";
            }
            else
            {

            }
            if (xxxlfs.Visible == true)
            {
                txtxxxlfs.Focus();
                if (txtxxxlfs.Text == "0")
                {
                    txtxxxlfs.Text = "";
                }
            }
            else if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }

        protected void SchangeXXXLfs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtxxxlfs.Text == "0" || txtxxxlfs.Text == "")
            {
                txtxxxlfs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtxxxlfs.Focus();
            if (txtxxxlfs.Text == "0")
            {
                txtxxxlfs.Text = "";
            }
            else
            {

            }
            if (xxxxlfs.Visible == true)
            {
                txtxxxxlfs.Focus();
                if (txtxxxxlfs.Text == "0")
                {
                    txtxxxxlfs.Text = "";
                }
            }


            //HALF
            else if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeXXXXLfs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtxxxxlfs.Text == "0" || txtxxxxlfs.Text == "")
            {
                txtxxxxlfs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtxxxxlfs.Focus();
            if (txtxxxxlfs.Text == "0")
            {
                txtxxxxlfs.Text = "";
            }
            else
            {

            }


            //HALF
            if (S30hs.Visible == true)
            {
                Btxt30hs.Focus();
                if (Btxt30hs.Text == "0")
                {
                    Btxt30hs.Text = "";
                }
            }
            else if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }



        protected void Schange30hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (Btxt30hs.Text == "0" || Btxt30hs.Text == "")
            {
                Btxt30hs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxt30hs.Focus();
            if (Btxt30hs.Text == "0")
            {
                Btxt30hs.Text = "";
            }
            else
            {

            }


            //HALF
            if (S32hs.Visible == true)
            {
                Btxt32hs.Focus();
                if (Btxt32hs.Text == "0")
                {
                    Btxt32hs.Text = "";
                }
            }
            else if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void Schange32hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (Btxt32hs.Text == "0" || Btxt32hs.Text == "")
            {
                Btxt32hs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxt32hs.Focus();
            if (Btxt32hs.Text == "0")
            {
                Btxt32hs.Text = "";
            }
            else
            {

            }


            //HALF
            if (S34hs.Visible == true)
            {
                Btxt34hs.Focus();
                if (Btxt34hs.Text == "0")
                {
                    Btxt34hs.Text = "";
                }
            }
            else if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void Schange34hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (Btxt34hs.Text == "0" || Btxt34hs.Text == "")
            {
                Btxt34hs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxt34hs.Focus();
            if (Btxt34hs.Text == "0")
            {
                Btxt34hs.Text = "";
            }
            else
            {

            }


            //HALF
            if (S36hs.Visible == true)
            {
                Btxt36hs.Focus();
                if (Btxt36hs.Text == "0")
                {
                    Btxt36hs.Text = "";
                }
            }
            else if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void NSchange36hs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (Btxt36hs.Text == "0" || Btxt36hs.Text == "")
            {
                Btxt36hs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxt36hs.Focus();
            if (Btxt36hs.Text == "0")
            {
                Btxt36hs.Text = "";
            }
            else
            {

            }


            //HALF
            if (Xshs.Visible == true)
            {
                Btxtxshs.Focus();
                if (Btxtxshs.Text == "0")
                {
                    Btxtxshs.Text = "";
                }
            }
            else if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeXShs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (Btxtxshs.Text == "0" || Btxtxshs.Text == "")
            {
                Btxtxshs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            Btxtxshs.Focus();
            if (Btxtxshs.Text == "0")
            {
                Btxtxshs.Text = "";
            }
            else
            {

            }


            //HALF
            if (shs.Visible == true)
            {
                txtshs.Focus();
                if (txtshs.Text == "0")
                {
                    txtshs.Text = "";
                }
            }
            else if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeShs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtshs.Text == "0" || txtshs.Text == "")
            {
                txtshs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtshs.Focus();
            if (txtshs.Text == "0")
            {
                txtshs.Text = "";
            }
            else
            {

            }


            //HALF
            if (mhs.Visible == true)
            {
                txtmhs.Focus();
                if (txtmhs.Text == "0")
                {
                    txtmhs.Text = "";
                }
            }

            else if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }

        protected void SchangeMhs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtmhs.Text == "0" || txtmhs.Text == "")
            {
                txtmhs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtmhs.Focus();
            if (txtmhs.Text == "0")
            {
                txtmhs.Text = "";
            }
            else
            {

            }


            //HALF
            if (lhs.Visible == true)
            {
                txtlhs.Focus();
                if (txtlhs.Text == "0")
                {
                    txtlhs.Text = "";
                }
            }
            else if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeLhs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtlhs.Text == "0" || txtlhs.Text == "")
            {
                txtlhs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtlhs.Focus();
            if (txtlhs.Text == "0")
            {
                txtlhs.Text = "";
            }
            else
            {

            }


            //HALF
            if (xlhs.Visible == true)
            {
                txtxlhs.Focus();
                if (txtxlhs.Text == "0")
                {
                    txtxlhs.Text = "";
                }
            }
            else if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeXLhs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtxlhs.Text == "0" || txtxlhs.Text == "")
            {
                txtxlhs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtxlhs.Focus();
            if (txtxlhs.Text == "0")
            {
                txtxlhs.Text = "";
            }
            else
            {

            }


            //HALF
            if (xxlhs.Visible == true)
            {
                txtxxlhs.Focus();
                if (txtxxlhs.Text == "0")
                {
                    txtxxlhs.Text = "";
                }
            }
            else if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }

        protected void SchangeXXLhs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtxxlhs.Text == "0" || txtxxlhs.Text == "")
            {
                txtxxlhs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtxxlhs.Focus();
            if (txtxxlhs.Text == "0")
            {
                txtxxlhs.Text = "";
            }
            else
            {

            }


            //HALF
            if (xxxlhs.Visible == true)
            {
                txtxxxlhs.Focus();
                if (txtxxxlhs.Text == "0")
                {
                    txtxxxlhs.Text = "";
                }
            }
            else if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeXXXLhs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtxxxlhs.Text == "0" || txtxxxlhs.Text == "")
            {
                txtxxxlhs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                //  txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtxxxlhs.Focus();
            if (txtxxxlhs.Text == "0")
            {
                txtxxxlhs.Text = "";
            }
            else
            {

            }


            //HALF
            if (xxxxlhs.Visible == true)
            {
                txtxxxxlhs.Focus();
                if (txtxxxxlhs.Text == "0")
                {
                    txtxxxxlhs.Text = "";
                }
            }

            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            System.Threading.Thread.Sleep(30);
        }


        protected void SchangeXXXXLhs(object sender, EventArgs e)
        {
            double tot = 0.00;
            double gndtot = 0.00;
            double r = 0.00;
            getzeroforemptysize();


            if (txtxxxxlhs.Text == "0" || txtxxxxlhs.Text == "")
            {
                txtxxxxlhs.Text = "0";
            }
            else
            {

            }



            tot = Convert.ToDouble(Btxt30fs.Text) + Convert.ToDouble(Btxt32fs.Text) + Convert.ToDouble(Btxt34fs.Text) + Convert.ToDouble(Btxt36fs.Text) + Convert.ToDouble(Btxtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
              Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txtxxxlfs.Text) + Convert.ToDouble(txtxxxxlfs.Text) +
              Convert.ToDouble(Btxt30hs.Text) + Convert.ToDouble(Btxt32hs.Text) + Convert.ToDouble(Btxt34hs.Text) + Convert.ToDouble(Btxt36hs.Text) + Convert.ToDouble(Btxtxshs.Text) + Convert.ToDouble(txtshs.Text) +
              Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txtxxxlhs.Text) + Convert.ToDouble(txtxxxxlhs.Text);



            gndtot = gndtot + tot;



            //  DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //  if (dcalculate.Tables[0].Rows.Count > 0)
            {

                //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                double wid = 0;
                //  if (drpFit.SelectedValue == "3")
                {
                    wid = Convert.ToDouble(txtactualmet.Text);
                }
                //else
                //{
                //    wid = Convert.ToDouble(txtexec.Text);
                //}

                double roundoff = Convert.ToDouble(tot) * wid;
                r = roundoff;
                double roundoff1 = Convert.ToDouble(txtavamet1.Text) * wid;
                if (roundoff > 0.5)
                {
                    r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                }
                else
                {
                    r = Math.Floor(Convert.ToDouble(roundoff));
                }

                // txtavamet1.Text = r.ToString();
                txttotshirt1.Text = tot.ToString();
                avgmeter();
                //  gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            }

            txtxxxxlhs.Focus();
            if (txtxxxxlhs.Text == "0")
            {
                txtxxxxlhs.Text = "";
            }
            else
            {

            }




            //  dparty.Focus();



            txtremashirt.Text = (Convert.ToDouble(txtremashirt.Text) - gndtot).ToString();
            // txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                //btnadd.Enabled = false;
                //btnprocess.Enabled = false;
                //return;
            }
            else
            {
                btnprocess.Enabled = true;
            }
            btngohead.Focus();

            System.Threading.Thread.Sleep(30);
        }




        #endregion



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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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
                        wid = Convert.ToDouble(txtavgmeter.Text);
                    }
                    else
                    {
                        wid = Convert.ToDouble(txtexec.Text);
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