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
    public partial class Unpacked : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iid;
        string empid = "";
        double totalfs = 0;
        DataTable dCrt;
        DataSet dsnneeww = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            iid = Request.QueryString.Get("CuttingID");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            empid = Session["Empid"].ToString();
            if (!IsPostBack)
            {



                string super = Session["IsSuperAdmin"].ToString();
                manualcheck_changed(sender, e);


                string companylotno = Request.QueryString.Get("lotno");
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
                } DataSet dpattern = objBs.Patternmaster();
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
                DataTable dt = new DataTable();
                divcode.Visible = false;

                dt.Columns.Add("Transid");
                dt.Columns.Add("Design");
                dt.Columns.Add("Designid");
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


                ViewState["Data"] = dt;

                DataSet dfit = objBs.Fit();
                if (dfit.Tables[0].Rows.Count > 0)
                {
                    Nchkfit.DataSource = dfit.Tables[0];
                    Nchkfit.DataTextField = "Fit";
                    Nchkfit.DataValueField = "Fitid";
                    Nchkfit.DataBind();
                }

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

                btnadd.Enabled = true;

                //    //sTableName = Session["User"].ToString();

                //    divcode.Visible = false;
                //    DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                //    if (dst != null)
                //    {
                //        //if (dst.Tables[0].Rows.Count > 0)
                //        //{
                //        //    ddlSupplier.DataSource = dst.Tables[0];
                //        //    ddlSupplier.DataTextField = "LedgerName";
                //        //    ddlSupplier.DataValueField = "LedgerID";
                //        //    ddlSupplier.DataBind();
                //        //    ddlSupplier.Items.Insert(0, "Select Party Name");

                //        //    //chkSupplier.DataSource = dst.Tables[0];
                //        //    //chkSupplier.DataTextField = "LedgerName";
                //        //    //chkSupplier.DataValueField = "LedgerID";
                //        //    //chkSupplier.DataBind();
                //        //    // ddlSupplier.Items.Insert(0, "Select Party Name");
                //        //}
                //    }

                //    //DataSet dsDNo = objBs.GetDNo();
                //    //if (dsDNo != null)
                //    //{
                //    //    if (dsDNo.Tables[0].Rows.Count > 0)
                //    //    {
                //    //        ddlDNo.DataSource = dsDNo.Tables[0];
                //    //        ddlDNo.DataTextField = "Dno";
                //    //        ddlDNo.DataValueField = "ProcessID";
                //    //        ddlDNo.DataBind();
                //    //        ddlDNo.Items.Insert(0, "Select Design");
                //    //    }
                //    //}

                //    DataSet dsFit = objBs.GetFit();
                //    if (dsFit != null)
                //    {
                //        if (dsFit.Tables[0].Rows.Count > 0)
                //        {
                //            ddlFit.DataSource = dsFit.Tables[0];
                //            ddlFit.DataTextField = "Fit";
                //            ddlFit.DataValueField = "FitID";
                //            ddlFit.DataBind();
                //            ddlFit.Items.Insert(0, "Select Fit");
                //        }
                //    }

                //    DataSet dsize = objBs.Getsizetype();
                //    if (dsize != null)
                //    {
                //        if (dsize.Tables[0].Rows.Count > 0)
                //        {
                //            chkSizes.DataSource = dsize.Tables[0];
                //            chkSizes.DataTextField = "Size";
                //            chkSizes.DataValueField = "Sizeid";
                //            chkSizes.DataBind();
                //            //  chkSizes.Items.Insert(0, "Select Customer");
                //            // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();
                //        }
                //    }

                //    DataSet dswidth = objBs.GetWidth();
                //    if (dswidth != null)
                //    {
                //        if (dswidth.Tables[0].Rows.Count > 0)
                //        {
                //            drpwidth.DataSource = dswidth.Tables[0];
                //            drpwidth.DataTextField = "Width";
                //            drpwidth.DataValueField = "WidthID";
                //            drpwidth.DataBind();
                //            // drpwidth.Items.Insert(0, "Select Width");
                //        }
                //    }


                //    DataSet dsrefno = objBs.getnewsupplierforcut(drpwidth.SelectedValue);
                //    if (dsrefno != null)
                //    {
                //        if (dsrefno.Tables[0].Rows.Count > 0)
                //        {
                //            chkinvno.DataSource = dsrefno.Tables[0];
                //            chkinvno.DataTextField = "refno";
                //            chkinvno.DataValueField = "transid";
                //            chkinvno.DataBind();
                //            //  drpwidth.Items.Insert(0, "Select Width");
                //        }
                //    }

                //    string date = DateTime.Now.ToString("dd/MM/yyyy");

                //    // txtdate.Text = date;

                //    lblUser.Text = Session["UserName"].ToString();
                //    lblUserID.Text = Session["UserID"].ToString();


                //    if (iid != null)
                //    {
                //        DataSet ds1 = objBs.getCuttingProcess(Convert.ToInt32(iid));
                //        if (ds1 != null)
                //        {
                //            if (ds1.Tables[0].Rows.Count > 0)
                //            {
                //                DataSet dsDNo1 = objBs.allGetDNo();
                //                if (dsDNo1 != null)
                //                {
                //                    if (dsDNo1.Tables[0].Rows.Count > 0)
                //                    {
                //                        ddlDNo.DataSource = dsDNo1.Tables[0];
                //                        ddlDNo.DataTextField = "Dno";
                //                        ddlDNo.DataValueField = "ProcessID";
                //                        ddlDNo.DataBind();
                //                        ddlDNo.Items.Insert(0, "Select Design");
                //                    }
                //                }

                //                btnadd.Text = "Update";
                //                double totmeter = Convert.ToDouble(ds1.Tables[0].Rows[0]["Req_Meter"]) + Convert.ToDouble(ds1.Tables[0].Rows[0]["met"]);
                //                txtID.Text = ds1.Tables[0].Rows[0]["CuttingID"].ToString();
                //                TextBox3.Text = ds1.Tables[0].Rows[0]["CuttingID"].ToString();
                //                txtreq_meter.Text = ds1.Tables[0].Rows[0]["Req_Meter"].ToString();
                //                ddlDNo.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["DNo"]).ToString();
                //                txtLotNo.Text = ds1.Tables[0].Rows[0]["LotNo"].ToString();
                //                txtMeter.Text = totmeter.ToString();
                //                txtRate.Text = ds1.Tables[0].Rows[0]["Rate"].ToString();
                //                txtColor.Text = ds1.Tables[0].Rows[0]["Color"].ToString();
                //                radbtn.SelectedValue = ds1.Tables[0].Rows[0]["Type"].ToString();
                //                if (radbtn.SelectedValue == "1")
                //                {
                //                    ddlSupplier.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["PartyName"]).ToString();
                //                    // single.Visible = true;
                //                    // multiple.Visible = false;
                //                }
                //                else
                //                {
                //                    //  single.Visible = false;
                //                    //  multiple.Visible = true;
                //                    string str = ds1.Tables[0].Rows[0]["PartyName"].ToString();
                //                    string[] strList = str.Split(',');


                //                    //foreach (string s in strList)
                //                    //{
                //                    //    foreach (ListItem item in chkSupplier.Items)
                //                    //    {
                //                    //        if (item.Value == s)
                //                    //        {
                //                    //            item.Selected = true;
                //                    //            break;
                //                    //        }

                //                    //    }

                //                    //}

                //                }
                //                txtWidth.Text = ds1.Tables[0].Rows[0]["WidthID"].ToString();
                //                ddlFit.SelectedValue = ds1.Tables[0].Rows[0]["Fit"].ToString();
                //                txtdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");
                //            }
                //        }
                //    }
                //    else
                //    {
                //        txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //        //DataSet ds = objBs.CuttingID();
                //        //if (ds.Tables[0].Rows.Count > 0)
                //        //{
                //        //    if (ds.Tables[0].Rows[0]["CuttingID"].ToString() == "")
                //        //        TextBox3.Text = "1";
                //        //    else
                //        //        TextBox3.Text = ds.Tables[0].Rows[0]["CuttingID"].ToString();
                //        DataSet dss = objBs.getmaaxBillnoforcut();
                //        if (dss.Tables[0].Rows.Count > 0)
                //        {
                //            txtLotNo.Text = dss.Tables[0].Rows[0]["billId"].ToString();
                //        }
                //        btnadd.Text = "Save";
                //        btnadd.Enabled = false;
                //        //  //  FirstGridViewRow();
                //        //}

                //    }
            }
        }
        protected void manualcheck_changed(object sender, EventArgs e)
        {

            if (manualcheck.Checked == true)
            {
                drplotno.Visible = false;
                txtmanualchecked.Visible = true;
                DataSet brandName = objBs.getBrandName();
                if (brandName.Tables[0].Rows.Count > 0)
                {
                    drpbrand.DataSource = brandName.Tables[0];
                    drpbrand.DataTextField = "name";
                    drpbrand.DataValueField = "BrandID";
                    drpbrand.DataBind();
                    drpbrand.Items.Insert(0, "Select Brand Name");
                }

                drpcolortype.Visible = false;
                manualcolor.Visible = true;
            }
            else
            {
                company_SelectedIndexChnaged(sender, e);
            }
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



            if (Nchkfit.SelectedIndex >= 0)
            {

                // CheckBoxList2.Enabled = true;
                //Loop through each item of checkboxlist
                foreach (ListItem item in Nchkfit.Items)
                {
                    //check if item selected
                    if (item.Selected)
                    {
                        // Add participant to the selected list if not alreay added
                        //if (!IsParticipantExists(item.Value))
                        //{

                        //}
                        //else
                        //{
                        dteo = objBs.GetFitforsingle(item.Value);
                        if (dteo != null)
                        {
                            if (dteo.Tables[0].Rows.Count > 0)
                            {
                                dssmer.Merge(dteo);
                            }
                        }


                        //}
                    }
                }
                int numSelected = 0;
                //foreach (ListItem item1 in Nchkfit.Items)
                //{
                //    //check if item selected
                //    if (item1.Selected)
                //    {

                //        numSelected = numSelected + 1;

                //        // Add participant to the selected list if not alreay added
                //        if (!IsParticipantExists(item1.Value))
                //        {

                //        }
                //        else
                //        {
                //            getavgmeter = objBs.getwidthnewprocessprecutting(drpwidth.SelectedItem.Text, item1.Value);
                //            if (getavgmeter.Tables[0].Rows.Count > 0)
                //            {
                //                avgtot = avgtot + Convert.ToDouble(getavgmeter.Tables[0].Rows[0]["w"]);
                //            }
                //        }
                //    }
                //}

                //txtavgmeter.Text = (avgtot / Convert.ToDouble(numSelected)).ToString("0.00");

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
        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalfs = totalfs + Convert.ToDouble(e.Row.Cells[13].Text);
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //  e.Row.Cells[6].Text = "Total:";
                e.Row.Cells[13].Text = totalfs.ToString();
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
                // totalfs = totalfs + Convert.ToDouble(e.Row.Cells[7].Text);
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
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
            }
            //DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
            //if (dcalculate.Tables[0].Rows.Count > 0)
            //{

            //    //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

            //    double wid = 0;
            //    if (drpFit.SelectedValue == "3")
            //    {
            //        wid = Convert.ToDouble(txtavgmeter.Text);
            //    }
            //    else
            //    {
            //        wid = Convert.ToDouble(txtexec.Text);
            //    }

            //    double roundoff = Convert.ToDouble(tot) * wid;
            //    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
            //    if (roundoff > 0.5)
            //    {
            //        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        r = Math.Floor(Convert.ToDouble(roundoff));
            //    }

            //    txtreqmeter.Text = r.ToString();
            //    txtshirt.Text = tot.ToString();
            //    gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            //}

            //   txt36fs.Focus();
            //if (gridsize.Columns[2].Visible == true) //38FS
            //{
            //    if (txt36fs.Text == "0" || txt36fs.Text == "")
            //    {
            //        txt36fs.Text = "";
            //    }
            //    txt36fs.Focus();
            //}
            //else if (gridsize.Columns[3].Visible == true) //38FS
            //{
            //    if (txt38fs.Text == "0" || txt38fs.Text == "")
            //    {
            //        txt38fs.Text = "";
            //    }
            //    txt38fs.Focus();
            //}
            //else if (gridsize.Columns[4].Visible == true)//39Fs
            //{
            //    if (txt39fs.Text == "0" || txt39fs.Text == "")
            //    {
            //        txt39fs.Text = "";
            //    }

            //    txt39fs.Focus();
            //}
            //else if (gridsize.Columns[5].Visible == true)//40Fs
            //{
            //    if (txt40fs.Text == "0" || txt40fs.Text == "")
            //    {
            //        txt40fs.Text = "";
            //    }

            //    txt40fs.Focus();
            //}

            //else if (gridsize.Columns[6].Visible == true) //42FS
            //{
            //    if (txt42fs.Text == "0" || txt42fs.Text == "")
            //    {
            //        txt42fs.Text = "";
            //    }

            //    txt42fs.Focus();
            //}
            //else if (gridsize.Columns[7].Visible == true) //44FS
            //{
            //    if (txt44fs.Text == "0" || txt44fs.Text == "")
            //    {
            //        txt44fs.Text = "";
            //    }

            //    txt44fs.Focus();
            //}

            //else if (gridsize.Columns[8].Visible == true) //36HS
            //{
            //    if (txt36hs.Text == "0" || txt36hs.Text == "")
            //    {
            //        txt36hs.Text = "";
            //    }

            //    txt36hs.Focus();
            //}
            //else if (gridsize.Columns[9].Visible == true) //38HS
            //{
            //    if (txt38hs.Text == "0" || txt38hs.Text == "")
            //    {
            //        txt38hs.Text = "";
            //    }

            //    txt38hs.Focus();
            //}

            //else if (gridsize.Columns[10].Visible == true) //39HS
            //{
            //    if (txt39hs.Text == "0" || txt39hs.Text == "")
            //    {
            //        txt39hs.Text = "";
            //    }

            //    txt39hs.Focus();
            //}
            //else if (gridsize.Columns[11].Visible == true) //40HS
            //{
            //    if (txt40hs.Text == "0" || txt40hs.Text == "")
            //    {
            //        txt40hs.Text = "";
            //    }

            //    txt40hs.Focus();
            //}

            //else if (gridsize.Columns[12].Visible == true) //42HS
            //{
            //    if (txt42hs.Text == "0" || txt42hs.Text == "")
            //    {
            //        txt42hs.Text = "";
            //    }

            //    txt42hs.Focus();
            //}
            //else if (gridsize.Columns[13].Visible == true) //44HS
            //{
            //    if (txt44hs.Text == "0" || txt44hs.Text == "")
            //    {
            //        txt44hs.Text = "";
            //    }

            //    txt44hs.Focus();
            //}


            //    DataSet dteo = objBs.getcutlistdesignfortrans(dddldesign.SelectedValue);
            //    if (dteo.Tables[0].Rows.Count > 0)
            //    {
            //        txtDesignRate.Text = dteo.Tables[0].Rows[0]["rat"].ToString();
            //        txtAvailableMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
            //        if (txtReqMtr.Text == "")
            //        {
            //            txtReqMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
            //        }
            //        else
            //        {

            //        }

            //        DataSet dcalculate1 = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
            //        if (dcalculate1.Tables[0].Rows.Count > 0)
            //        {

            //            double wid = Convert.ToDouble(dcalculate1.Tables[0].Rows[0]["width"]);

            //            double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
            //            double roundoff1 = Convert.ToDouble(txtReqMtr.Text) / wid;
            //            if (roundoff > 0.5)
            //            {
            //                re = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //            }
            //            else
            //            {
            //                re = Math.Floor(Convert.ToDouble(roundoff));
            //            }

            //            if (roundoff1 > 0.5)
            //            {
            //                r1 = Math.Round(Convert.ToDouble(roundoff1), MidpointRounding.AwayFromZero);
            //            }
            //            else
            //            {
            //                r1 = Math.Floor(Convert.ToDouble(roundoff1));
            //            }

            //        }
            //        txtNoofShirts.Text = re.ToString();
            //        txtReqNoShirts.Text = r1.ToString();
            //    }
            //    rr = ((re * 2) / 100);
            //    if (rr > 0.5)
            //    {
            //        rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        rb = Math.Floor(Convert.ToDouble(rr));
            //    }
            //    txtextrashirt.Text = rb.ToString();

            //    // dparty.Focus();
            //}


            //txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            //txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - gndmet).ToString("0.00");

            //// if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    //btnadd.Enabled = false;
            //    ////   btnprocess.Enabled = false;
            //    //return;
            //}

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

        public void removedropdownlist()
        {
            DataSet myDS = (DataSet)ViewState["MyDataSetcolour"];

            dCrt = (DataTable)ViewState["Data"];

            dsnneeww.Tables.Add(dCrt);


            if (dsnneeww.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < dsnneeww.Tables[0].Rows.Count; i++)
                {
                    string trainid = dsnneeww.Tables[0].Rows[i]["Designid"].ToString();
                    if (myDS.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < myDS.Tables[0].Rows.Count; j++)
                        {

                            string idd = myDS.Tables[0].Rows[j]["transfabid"].ToString();
                            if (idd == trainid)
                            {
                                drpcolortype.Items.Remove(drpcolortype.Items.FindByValue(idd));
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
        protected void ckhsize_index(object sender, EventArgs e)
        {
        }
        protected void drpfitchanged(object sender, EventArgs e)
        {
            double r = 0.00;
            double r1 = 0.00;

            double rr = 0.00;
            double rb = 0.00;

            //DataSet getavgmeter = objBs.getwidthnewprocessprecutting(drpwidth.SelectedItem.Text, drpFit.SelectedValue);
            //if (getavgmeter.Tables[0].Rows.Count > 0)
            //{
            //    txtactualmet.Text = Convert.ToDouble(getavgmeter.Tables[0].Rows[0]["w"]).ToString("0.00");
            //}

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


            DataSet dsizee = objBs.Getfitseize(drpFit.SelectedValue);
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
                foreach (ListItem item in chkSizes.Items)
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

                //double avalmeter = Convert.ToDouble(txtReqMtr.Text);
                //double givenmeter = Convert.ToDouble(txtavamet1.Text);

                //double remmeter = avalmeter - givenmeter;

                //bool negative = remmeter < 0;

                //if (negative == true)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Meter Greater than Remaining Meter. Thank you');", true);
                //    txtavamet1.Focus();
                //    return;
                //}
                //else
                //{
                //    Ntxtremmeter.Text = remmeter.ToString("0.00");
                //}
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

                //double roundoff1 = Convert.ToDouble(txtAvailableMtr.Text) / wid;
                //if (roundoff1 > 0.5)
                //{
                //    r1 = Math.Round(Convert.ToDouble(roundoff1), MidpointRounding.AwayFromZero);
                //}
                //else
                //{
                //    r1 = Math.Floor(Convert.ToDouble(roundoff1));
                //}

            }

            //txtremashirt.Text = r.ToString();
            Ntxtactshirt.Text = r.ToString();
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


            getzeroforemptysize();

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

                        dr["Transid"] = "";
                        if (manualcheck.Checked == true)
                        {
                            dr["Design"] = manualcolor.Text;
                            dr["Designid"] = "0";
                        }
                        else
                        {
                            dr["Design"] = drpcolortype.SelectedItem.Text;
                            dr["Designid"] = drpcolortype.SelectedValue;
                        }
                        dr["Rate"] = "0";

                        //dr["meter"] = txtAvailableMtr.Text;
                        //dr["Shirt"] = txtNoofShirts.Text;
                        dr["reqmeter"] = txtavamet1.Text;

                        dr["reqshirt"] = txttotshirt1.Text;

                        dr["Transid"] = "";
                        // dr["Design"] = "Single Bulk Cutting";
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
                        dr["FItLab"] = fitlab;
                        dr["Washlab"] = washlab;
                        dr["Logolab"] = logolab;
                        dr["Total"] = tot.ToString();



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
                        // dr["Design"] = "Single Bulk Cutting";
                        if (manualcheck.Checked == true)
                        {
                            dr["Design"] = manualcolor.Text;
                            dr["Designid"] = "0";
                        }
                        else
                        {
                            dr["Design"] = drpcolortype.SelectedItem.Text;
                            dr["Designid"] = drpcolortype.SelectedValue;
                        }
                        dr["Rate"] = "0";

                        //dr["meter"] = txtAvailableMtr.Text;
                        //dr["Shirt"] = txtNoofShirts.Text;
                        dr["reqmeter"] = txtavamet1.Text;

                        dr["reqshirt"] = txttotshirt1.Text;

                        dr["Transid"] = "";
                        // dr["Design"] = "Single Bulk Cutting";
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



                        dCrt.Rows.Add(dr);
                    }
                }
                else
                {

                }
            }


            gridsize.DataSource = dCrt;
            gridsize.DataBind();

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
            // removedropdownlist();
            System.Threading.Thread.Sleep(30);
        }

        protected void rdbinnertype_SelectedIndexChnaged(object sender, EventArgs e)
        {
            if (drpbranch.SelectedValue == "3")
            {
                lblcompany.Text = "RPL";

                //if (rdbinnertype.SelectedValue == "1")
                //{
                //    lblcompany.Text = "RPL";
                //}
                //else if (rdbinnertype.SelectedValue == "2")
                //{
                //    lblcompany.Text = "F21";
                //}
                //else
                //{
                //    lblcompany.Text = "DHW";
                //}
            }
            else
            {
                lblcompany.Text = "BC";
            }
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
                DataSet dlot = new DataSet();
                dlot = objBs.getalllotnumberforunpack(drpbranch.SelectedValue);
                if (drpbranch.SelectedValue == "3")
                {
                    lblcompany.Text = "RPL";
                    //rdbinnertype.Enabled = true;
                    //if (rdbinnertype.SelectedValue == "1")
                    //{
                    //    lblcompany.Text = "RPL";
                    //}
                    //else
                    //{
                    //    lblcompany.Text = "F21";
                    //}

                    //lblcompany.Text = "RPL" ;
                    // txtcompanylot.Text = companyno;
                }
                else
                {
                   // rdbinnertype.Enabled = false;
                    lblcompany.Text = "BC";
                    // txtcompanylot.Text = companyno;

                }
                if (dlot != null)
                {
                    if (dlot.Tables[0].Rows.Count > 0)
                    {
                        //   drplotno
                        drplotno.DataSource = dlot.Tables[0];
                        drplotno.DataTextField = "CompanyLotNo";
                        drplotno.DataValueField = "masterid";
                        drplotno.DataBind();
                        drplotno.Items.Insert(0, "Select Lot");
                    }
                }

            }
        }

        protected void ddlType_SelectedIndexChnaged(object sender, EventArgs e)
        {
            if (ddlTypeUnpack.SelectedValue == "Type")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Type. Thank you');", true);
                return;

            }
            else
            {
                DataSet dlot = new DataSet();
                dlot = objBs.getmastertypelot(ddlTypeUnpack.SelectedValue);
                if (dlot.Tables[0].Rows.Count > 0)
                {
                    txtsupplierprefix.Text = ddlTypeUnpack.SelectedItem.Text;
                    txtmanualchecked.Text = dlot.Tables[0].Rows[0]["LotNo"].ToString();
                }
            }

        }
        //protected void ckhsize_index(object sender, EventArgs e)
        //{
        //DataSet dssmer = new DataSet();
        //DataSet dteo = new DataSet();
        //if (chkSizes.SelectedIndex >= 0)
        //{
        //    gvcustomerorder.Columns[7].Visible = false;
        //    gvcustomerorder.Columns[8].Visible = false;

        //    gvcustomerorder.Columns[9].Visible = false;
        //    gvcustomerorder.Columns[10].Visible = false;

        //    gvcustomerorder.Columns[11].Visible = false; gvcustomerorder.Columns[12].Visible = false;

        //    gvcustomerorder.Columns[13].Visible = false; gvcustomerorder.Columns[14].Visible = false;

        //    gvcustomerorder.Columns[15].Visible = false; gvcustomerorder.Columns[16].Visible = false;

        //    gvcustomerorder.Columns[17].Visible = false; gvcustomerorder.Columns[18].Visible = false;


        //    gvcustomerorder.Columns[19].Visible = false;
        //    gvcustomerorder.Columns[20].Visible = false;

        //    gvcustomerorder.Columns[21].Visible = false;
        //    gvcustomerorder.Columns[22].Visible = false;

        //    gvcustomerorder.Columns[23].Visible = false; gvcustomerorder.Columns[24].Visible = false;

        //    gvcustomerorder.Columns[25].Visible = false; gvcustomerorder.Columns[26].Visible = false;

        //    gvcustomerorder.Columns[27].Visible = false; gvcustomerorder.Columns[28].Visible = false;

        //    gvcustomerorder.Columns[29].Visible = false; gvcustomerorder.Columns[30].Visible = false;

        //    int lop = 0;
        //    //Loop through each item of checkboxlist
        //    foreach (ListItem item in chkSizes.Items)
        //    {
        //        //check if item selected

        //        if (item.Selected)
        //        {

        //            {
        //                if (item.Value == "1")
        //                {
        //                    gvcustomerorder.Columns[7].Visible = true;
        //                }
        //                if (item.Value == "2")
        //                {
        //                    gvcustomerorder.Columns[8].Visible = true;
        //                }
        //                if (item.Value == "3")
        //                {
        //                    gvcustomerorder.Columns[9].Visible = true;
        //                }
        //                if (item.Value == "4")
        //                {
        //                    gvcustomerorder.Columns[10].Visible = true;
        //                }
        //                if (item.Value == "5")
        //                {
        //                    gvcustomerorder.Columns[11].Visible = true;
        //                }
        //                if (item.Value == "6")
        //                {
        //                    gvcustomerorder.Columns[12].Visible = true;
        //                }
        //                if (item.Value == "7")
        //                {
        //                    gvcustomerorder.Columns[13].Visible = true;
        //                }
        //                if (item.Value == "8")
        //                {
        //                    gvcustomerorder.Columns[14].Visible = true;
        //                }
        //                if (item.Value == "9")
        //                {
        //                    gvcustomerorder.Columns[15].Visible = true;
        //                }
        //                if (item.Value == "10")
        //                {
        //                    gvcustomerorder.Columns[16].Visible = true;
        //                }
        //                if (item.Value == "11")
        //                {
        //                    gvcustomerorder.Columns[17].Visible = true;
        //                }
        //                if (item.Value == "12")
        //                {
        //                    gvcustomerorder.Columns[18].Visible = true;
        //                }


        //                lop++;

        //            }
        //        }
        //    }
        //    //gvcustomerorder.DataSource = dssmer;
        //    //gvcustomerorder.DataBind();
        //}
        //else
        //{
        //    gvcustomerorder.Columns[7].Visible = false;
        //    gvcustomerorder.Columns[8].Visible = false;

        //    gvcustomerorder.Columns[9].Visible = false;
        //    gvcustomerorder.Columns[10].Visible = false;

        //    gvcustomerorder.Columns[11].Visible = false; gvcustomerorder.Columns[12].Visible = false;

        //    gvcustomerorder.Columns[13].Visible = false; gvcustomerorder.Columns[14].Visible = false;

        //    gvcustomerorder.Columns[15].Visible = false; gvcustomerorder.Columns[16].Visible = false;

        //    gvcustomerorder.Columns[17].Visible = false; gvcustomerorder.Columns[18].Visible = false;
        //}
        //}

        protected void call_Click(object sender, EventArgs e)
        {
            //DataSet dcalculate = new DataSet();

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



            //      dcalculate = objBs.getsizeforcutt(ddlFit.SelectedValue, width);
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




            System.Threading.Thread.Sleep(30);
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
                // 
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






            System.Threading.Thread.Sleep(30);
        }




        #endregion

        protected void Add_Click(object sender, EventArgs e)
        {
            //string Mode = Request.QueryString.Get("Mode");
            //DataSet dcalculate = new DataSet();

            //btnadd.Enabled = false;
            //string width = string.Empty;

            //DateTime deliverydate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnadd.Text == "Save")
            {
                int IntialManualchecked = 0;
                IntialManualchecked=Convert.ToInt32(txtmanualchecked.Text);

                if (txtmanualchecked.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Lot Number. Thank you');", true);
                    return;

                }
                else
                {

                }

                if (txtsupplierprefix.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Supplier Initial. Thank you');", true);
                    return;

                }
                else
                {

                }



                DataSet dcheck = new DataSet();
                txtmanualchecked.Text = lblcompany.Text + '-' + txtsupplierprefix.Text + '-' + txtmanualchecked.Text;


                dcheck = objBs.getlotalreadyexistsornot(txtmanualchecked.Text);
                if (dcheck.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This Lot Number Already Exists. Thank you');", true);
                    return;
                }
                else
                {

                }

                int iStatus = 0;

                //  iStatus = objBs.insertStock(drplotno.SelectedItem.Text, drplotno.SelectedValue, drpbrand.SelectedValue, empid, drpbranch.SelectedValue);


               //string supplierprefix ="";
               //int manualchecked = 0;

               // DataSet dlot = new DataSet();
               // dlot = objBs.getmastertypelot(ddlTypeUnpack.SelectedValue);
               // if (dlot.Tables[0].Rows.Count > 0)
               // {
               //     supplierprefix = ddlTypeUnpack.SelectedItem.Text;
               //     manualchecked =Convert.ToInt32(dlot.Tables[0].Rows[0]["LotNo"].ToString());
               // }
                for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                {
                    Label invrefno = (Label)gridsize.Rows[vLoop].FindControl("lblno");
                    TextBox txtdesign = (TextBox)gridsize.Rows[vLoop].FindControl("txtdesigno");
                    TextBox partyname = (TextBox)gridsize.Rows[vLoop].FindControl("txtledgerid");
                    TextBox reqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox reqshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox fitid = (TextBox)gridsize.Rows[vLoop].FindControl("txtfitid");
                    TextBox itemname = (TextBox)gridsize.Rows[vLoop].FindControl("txtitemname");
                    TextBox patternid = (TextBox)gridsize.Rows[vLoop].FindControl("txtpatternid");

                    //FULL SLEEVE
                    TextBox txt30fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts30fs");
                    TextBox txt32fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts32fs");
                    TextBox txt34fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts34fs");
                    TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts36fs");
                    TextBox txtxsfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxsfs");
                    TextBox txtsfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtssfs");
                    TextBox txtmfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsmfs");
                    TextBox txtlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtslfs");
                    TextBox txtxlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxlfs");
                    TextBox txtxxlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxxlfs");
                    TextBox txt3xlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txts3xlfs");
                    TextBox txt4xlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txts4xlfs");

                    //HALF SLEEVE

                    TextBox txt30hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts30hs");
                    TextBox txt32hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts32hs");
                    TextBox txt34hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts34hs");
                    TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts36hs");
                    TextBox txtxshs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxshs");
                    TextBox txtshs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsshs");
                    TextBox txtmhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsmhs");
                    TextBox txtlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtslhs");
                    TextBox txtxlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxlhs");
                    TextBox txtxxlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxxlhs");
                    TextBox txt3xlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txts3xlhs");
                    TextBox txt4xlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txts4xlhs");

                    // TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                    TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");
                    TextBox txtresona = (TextBox)gridsize.Rows[vLoop].FindControl("txtnarration");
                    TextBox txtendbit = (TextBox)gridsize.Rows[vLoop].FindControl("txtendbit");

                    TextBox txtmargin = (TextBox)gridsize.Rows[vLoop].FindControl("txtmar");
                    TextBox txtmrp = (TextBox)gridsize.Rows[vLoop].FindControl("Txtmrp");
                    TextBox txtrate = (TextBox)gridsize.Rows[vLoop].FindControl("Txrtate");

                    TextBox txtusedmeter = (TextBox)gridsize.Rows[vLoop].FindControl("txtuedmter");

                    DropDownList drpreason = (DropDownList)gridsize.Rows[vLoop].FindControl("drpreason");
                    DropDownList ddlType = (DropDownList)gridsize.Rows[vLoop].FindControl("ddlType");

                    //if (ddlAgainst.SelectedValue != "0" && txtchequedd.Text != "" && txtAgainstAmount.Text != "0")

                    int InnerType = 0;
                    if (drpbranch.SelectedValue == "3")
                    {
                        //InnerType = Convert.ToInt32(rdbinnertype.SelectedValue);
                        InnerType = 0;
                    }
                    else
                    {
                        InnerType = 0;
                    }

                    int istockid = objBs.insertstockwisestockunpack("0", "0", "0", txtdesign.Text, drpbrand.SelectedValue, fitid.Text, itemname.Text, drpbranch.SelectedValue, txtmanualchecked.Text, txttoal.Text, "0", txt30fs.Text, txt32fs.Text, txt34fs.Text, txt36fs.Text, txtxsfs.Text, txtsfs.Text, txtmfs.Text, txtlfs.Text, txtxlfs.Text, txtxxlfs.Text, txt3xlfs.Text, txt4xlfs.Text, txt30hs.Text, txt32hs.Text, txt34hs.Text, txt36hs.Text, txtxshs.Text, txtshs.Text, txtmhs.Text, txtlhs.Text, txtxlhs.Text, txtxxlhs.Text, txt3xlhs.Text, txt4xlhs.Text, "Y", InnerType, txtsupplierprefix.Text, IntialManualchecked);

                    //  int iStatus2 = objBs.insertPrStock(invrefno.Text, txtdesign.Text, partyname.Text, reqmeter.Text, txtrate.Text, txt30fs.Text, txt30hs.Text, txt32fs.Text, txt32hs.Text, txt34fs.Text, txt34hs.Text, txt36fs.Text, txt36hs.Text, txtxsfs.Text, txtxshs.Text, txtsfs.Text, txtshs.Text, txtmfs.Text, txtmhs.Text, txtlfs.Text, txtlhs.Text, txtxlfs.Text, txtxlhs.Text, txtxxlfs.Text, txtxxlhs.Text, txt3xlfs.Text, txt3xlhs.Text, txt4xlfs.Text, txt4xlhs.Text, reqshirt.Text, txttoal.Text, fitid.Text, txtdamage.Text, txtresona.Text, txtmargin.Text, txtmrp.Text, drpreason.SelectedValue, txtusedmeter.Text, txtendbit.Text, itemname.Text, patternid.Text, drplotno.SelectedValue, ddlType.SelectedValue, drpbranch.SelectedValue);


                }

                //DataSet dstt = new DataSet();
                //DataTable dttt = new DataTable();

                //DataColumn dc = new DataColumn("id");
                //dttt.Columns.Add(dc);

                //dc = new DataColumn("fabid");
                //dttt.Columns.Add(dc);

                //dc = new DataColumn("name");
                //dttt.Columns.Add(dc);

                //dc = new DataColumn("Givenmeter");
                //dttt.Columns.Add(dc);
                //dc = new DataColumn("usedmeter");
                //dttt.Columns.Add(dc);
                //dc = new DataColumn("endmeter");
                //dttt.Columns.Add(dc);
                //dc = new DataColumn("type");
                //dttt.Columns.Add(dc);
                //dc = new DataColumn("avgmeter");
                //dttt.Columns.Add(dc);


                //dstt.Tables.Add(dttt);


                //for (int vLoop1 = 0; vLoop1 < newgridfablist.Rows.Count; vLoop1++)
                //{
                //    Label lblfabidd = (Label)newgridfablist.Rows[vLoop1].FindControl("lblfabidd");
                //    Label newfabid = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabid");
                //    Label newfabcode = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabcode");
                //    Label lblshirttype = (Label)newgridfablist.Rows[vLoop1].FindControl("lblshirttype");
                //    Label lblavgmeter = (Label)newgridfablist.Rows[vLoop1].FindControl("lblavgmeter");
                //    TextBox givenmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtAvlmeter");
                //    TextBox usedmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtreqmeter");
                //    TextBox endmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtendmeter");

                //    if (endmeter.Text != "0.00" || endmeter.Text != "0")
                //    {
                //        DataRow drd = dstt.Tables[0].NewRow();
                //        drd["id"] = newfabid.Text;
                //        drd["fabid"] = lblfabidd.Text;
                //        drd["name"] = newfabcode.Text;
                //        drd["Givenmeter"] = givenmeter.Text;
                //        drd["usedmeter"] = usedmeter.Text;
                //        drd["endmeter"] = endmeter.Text;

                //        drd["type"] = lblshirttype.Text;

                //        drd["avgmeter"] = lblavgmeter.Text;

                //        dstt.Tables[0].Rows.Add(drd);
                //    }
                //}
                //int idue = objBs.fabricemaster(dstt);

                Response.Redirect("unpacked.aspx");
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
            //        int iStatus2 = objBs.insertTranscut(txtLotNo.Text, orderno.Text, lblid.Text, txtdesign.Text, dparty.SelectedValue, txtmeter.Text, txtreq.Text, txtrate.Text, txt36fs.Text, txt36hs.Text, txt38fs.Text, txt38hs.Text, txt39fs.Text, txt39hs.Text, txt40fs.Text, txt40hs.Text, txt42fs.Text, txt42hs.Text, txt44fs.Text, txt44hs.Text);

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



        }
        protected void chkinvnochanged(object sender, EventArgs e)
        {




        }

        private bool IsParticipantExists(string val)
        {
            bool exists = false;
            //Loop through each item in selected participant checkboxlist
            //foreach (ListItem item in chkinvno.Items)
            //{
            //    //Check if item selected already exists in the selected participant checboxlist or not
            //    if (item.Value == val)
            //    {
            //        exists = true;
            //        break;
            //    }
            //}
            return exists;
        }
        protected void check2_changed(object sender, EventArgs e)
        {


        }

        protected void ddlDNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void brandindexchnaged(object sender, EventArgs e)
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
        }
        protected void radchecked(object sender, EventArgs e)
        {
            //if (radbtn.SelectedValue == "1")
            //{
            //    single.Visible = true;
            //    multiple.Visible = false;
            //}
            //else
            //{
            //    multiple.Visible = true;
            //    single.Visible = false;

            //}
        }

        private void FirstGridViewRow()
        {
            //DataTable dtt = new DataTable();
            //DataRow dr = null;
            //dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Designno", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Ameter", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Rmeter", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Color", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Width", typeof(string)));


            //dr = dtt.NewRow();
            //dr["OrderNo"] = string.Empty;
            //dr["Designno"] = string.Empty;
            //dr["Ameter"] = string.Empty;
            //dr["Rmeter"] = string.Empty;
            //dr["Rate"] = string.Empty;
            //dr["Color"] = string.Empty;
            //dr["Width"] = string.Empty;


            //dtt.Rows.Add(dr);

            //ViewState["CurrentTable1"] = dtt;

            //gvcustomerorder.DataSource = dtt;
            //gvcustomerorder.DataBind();

            //DataTable dttt;
            //DataRow drNew;
            //DataColumn dct;
            //DataSet dstd = new DataSet();
            //dttt = new DataTable();

            //dct = new DataColumn("OrderNo");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Designno");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Ameter");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Rmeter");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Rate");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Color");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Width");
            //dttt.Columns.Add(dct);





            //dstd.Tables.Add(dttt);

            //drNew = dttt.NewRow();

            //drNew["OrderNo"] = 0;
            //drNew["Designno"] = "";
            //drNew["Ameter"] = 0;
            //drNew["Rmeter"] = 0;
            //drNew["Rate"] = 0;
            //drNew["Color"] = 0;
            //drNew["Width"] = "";



            //dstd.Tables[0].Rows.Add(drNew);

            //gvcustomerorder.DataSource = dstd;
            //gvcustomerorder.DataBind();


            //TextBox txn = (TextBox)grvStudentDetails.Rows[0].Cells[1].FindControl("txtName");
            //txn.Focus();
            //Button btnAdd = (Button)grvStudentDetails.FooterRow.Cells[5].FindControl("ButtonAdd");
            //Page.Form.DefaultFocus = btnAdd.ClientID;

        }
        protected void gridmaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void gridmaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }




        }

        protected void gvcustomerorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataSet dsCategory = new DataSet();

            //DataSet dscat = new DataSet();

            //string OrderNo = Request.QueryString.Get("OrderNo");
            ////if (OrderNo != "")
            ////{
            ////    /// dsCategory = objBs.GetCAT_OrderForm();
            ////}
            ////else
            ////    dsCategory = objBs.selectcategorybrandcat(sTableName);

            //DataSet dsDNo = objBs.GetDNo();

            //DataSet dsFit = objBs.GetFit();



            ////else
            ////    dsCategory = objBs.selectcategorymaster_Dealer("tblStock_" + sTableName);


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    DropDownList ddlCategory1 = (DropDownList)(e.Row.FindControl("drpItem") as DropDownList);
            //    ddlCategory1.Focus();
            //    ddlCategory1.Enabled = true;
            //    ddlCategory1.DataSource = dsDNo.Tables[0];
            //    ddlCategory1.DataTextField = "Dno";
            //    ddlCategory1.DataValueField = "ProcessID";
            //    ddlCategory1.DataBind();
            //    ddlCategory1.Items.Insert(0, "Select Design");

            //    //DropDownList ddlDef1 = (DropDownList)(e.Row.FindControl("drpfit") as DropDownList);
            //    //ddlDef1.Focus();
            //    //ddlDef1.Enabled = true;
            //    //ddlDef1.DataSource = dsFit.Tables[0];
            //    //ddlDef1.DataTextField = "Fit";
            //    //ddlDef1.DataValueField = "fitid";
            //    //ddlDef1.DataBind();
            //    //ddlDef1.Items.Insert(0, "Select Fit");



            //}
        }

        private void SetPreviousData()
        {
            //int rowIndex = 0;
            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dt = (DataTable)ViewState["CurrentTable1"];
            //    if (dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {

            //            TextBox txtameter =
            //              (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtaemeter");


            //            DropDownList drpItem =
            //              (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");

            //            TextBox txtrmeter =
            //          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrmeter");




            //            TextBox txtRate =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");
            //            TextBox txtcolor =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");
            //            TextBox txtwidth =
            //               (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtwidth");

            //            TextBox txttno =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");






            //            drpItem.Items.Clear();

            //            DataSet dst = objBs.GetDNo();
            //            drpItem.Items.Add(new ListItem("Select Design", "0"));
            //            drpItem.DataSource = dst;
            //            drpItem.DataBind();
            //            drpItem.DataTextField = "Dno";
            //            drpItem.DataValueField = "Processid";


            //            txtameter.Text = dt.Rows[i]["ameter"].ToString();
            //            txtrmeter.Text = dt.Rows[i]["rmeter"].ToString();

            //            drpItem.SelectedValue = dt.Rows[i]["Designno"].ToString();
            //            txttno.Text = dt.Rows[i]["OrderNo"].ToString();
            //            txtRate.Text = dt.Rows[i]["Rate"].ToString();
            //            txtcolor.Text = dt.Rows[i]["color"].ToString();
            //            txtwidth.Text = dt.Rows[i]["width"].ToString();
            //            txtwidth.Text = dt.Rows[i]["width"].ToString();


            //            rowIndex++;

            //        }
            //    }
            //}
        }

        private void SetRowData()
        {
            //int rowIndex = 0;

            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            //    DataRow drCurrentRow = null;
            //    if (dtCurrentTable.Rows.Count > 0)
            //    {
            //        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //        {


            //            TextBox txtameter =
            //               (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtaemeter");


            //            DropDownList drpItem =
            //              (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");

            //            TextBox txtrmeter =
            //          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrmeter");




            //            TextBox txtRate =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");
            //            TextBox txtcolor =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");
            //            TextBox txtwidth =
            //               (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtwidth");


            //            drCurrentRow = dtCurrentTable.NewRow();
            //            drCurrentRow["Orderno"] = i + 1;
            //            dtCurrentTable.Rows[i - 1]["Designno"] = drpItem.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["Ameter"] = txtameter.Text;
            //            dtCurrentTable.Rows[i - 1]["Rmeter"] = txtrmeter.Text;

            //            dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
            //            dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
            //            dtCurrentTable.Rows[i - 1]["Width"] = txtwidth.Text;



            //            rowIndex++;

            //        }

            //        ViewState["CurrentTable1"] = dtCurrentTable;
            //        gvcustomerorder.DataSource = dtCurrentTable;
            //        gvcustomerorder.DataBind();
            //    }
            //}
            //else
            //{
            //    Response.Write("ViewState is null");
            //}
            //SetPreviousData();
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
            //int No = 0;
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");

            //    TextBox txtameter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtaemeter");
            //    TextBox txtrmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
            //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
            //    TextBox txtwidth = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtwidth");
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");


            //    if (ProductCode.SelectedItem.Text == "Select Design")
            //    {
            //        No = 0;
            //        break;
            //    }
            //    else
            //    {
            //        No = 1;
            //    }
            //}

            //if (No == 1)
            //{

            //    AddNewRow();
            //}
            //else
            //{

            //}
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    //  TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    txtno.Focus();
            //}



        }

        private void AddNewRow()
        {
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");

            //    TextBox txtameter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtaemeter");
            //    TextBox txtrmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
            //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
            //    TextBox txtwidth = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtwidth");
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    // Label txtno = (Label)gvcustomerorder.Rows[vLoop].FindControl("txtno");


            //    int col = vLoop + 1;


            //    txtno.Focus();
            //}

            //int rowIndex = 0;

            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            //    DataRow drCurrentRow = null;
            //    if (dtCurrentTable.Rows.Count > 0)
            //    {
            //        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //        {

            //            TextBox txtameter =
            //               (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtaemeter");


            //            DropDownList drpItem =
            //              (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");

            //            TextBox txtrmeter =
            //          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrmeter");




            //            TextBox txtRate =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");
            //            TextBox txtcolor =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");
            //            TextBox txtwidth =
            //               (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtwidth");


            //            drCurrentRow = dtCurrentTable.NewRow();
            //            drCurrentRow["Orderno"] = i + 1;
            //            dtCurrentTable.Rows[i - 1]["Designno"] = drpItem.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["Ameter"] = txtameter.Text;
            //            dtCurrentTable.Rows[i - 1]["Rmeter"] = txtrmeter.Text;

            //            dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
            //            dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
            //            dtCurrentTable.Rows[i - 1]["Width"] = txtwidth.Text;



            //            rowIndex++;
            //        }
            //        dtCurrentTable.Rows.Add(drCurrentRow);
            //        ViewState["CurrentTable1"] = dtCurrentTable;

            //        gvcustomerorder.DataSource = dtCurrentTable;
            //        gvcustomerorder.DataBind();
            //    }
            //}
            //else
            //{
            //    Response.Write("ViewState is null");
            //}
            //SetPreviousData();
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
        protected void newfabclick(object sender, EventArgs e)
        {
            double tot = 0;

            //DataSet trasncut = new DataSet();
            //trasncut = objBs.gettrancutfab(drplotno.SelectedValue);
            //if (trasncut.Tables[0].Rows.Count > 0)
            //{
            //    newgridfablist.DataSource = trasncut;
            //    newgridfablist.DataBind();
            //}

            //for (int vLoop = 0; vLoop < newgridfablist.Rows.Count; vLoop++)
            //{
            //    TextBox avaliablemeer = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtAvlmeter");
            //    TextBox reqmeter = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtreqmeter");
            //    TextBox endmeter = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtendmeter");

            //    if (endmeter.Text == "")
            //    {
            //        endmeter.Text = "0";
            //    }

            //    reqmeter.Text = Convert.ToDouble(Convert.ToDouble(avaliablemeer.Text) - Convert.ToDouble(endmeter.Text)).ToString("f2");

            //    double totalmeter = Convert.ToDouble(reqmeter.Text) + Convert.ToDouble(endmeter.Text);

            //    if (totalmeter == Convert.ToDouble(avaliablemeer.Text))
            //    {

            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given End bit in greater Than that Given Meter in Row " + vLoop + ".Thank you!!!');", true);
            //        return;
            //    }


            //}
        }
        protected void drplotchanged(object sender, EventArgs e)
        {


            DataSet brandName = objBs.getBrandName();
            if (brandName.Tables[0].Rows.Count > 0)
            {
                drpbrand.DataSource = brandName.Tables[0];
                drpbrand.DataTextField = "name";
                drpbrand.DataValueField = "BrandID";
                drpbrand.DataBind();
                drpbrand.Items.Insert(0, "Select Brand Name");
                drpbrand.Enabled = false;
            }

            DataSet ddata = new DataSet();
            ddata = objBs.getdataforlotnumberfromaster(drplotno.SelectedValue);
            if (ddata.Tables[0].Rows.Count > 0)
            {

                drpbrand.SelectedValue = ddata.Tables[0].Rows[0]["Brandid"].ToString();
            }

            DataSet dgetcolurfab = new DataSet();
            dgetcolurfab = objBs.getmastercolourfabric(drplotno.SelectedValue);
            if (dgetcolurfab.Tables[0].Rows.Count > 0)
            {
                drpcolortype.DataSource = dgetcolurfab.Tables[0];
                drpcolortype.DataTextField = "Designno";
                drpcolortype.DataValueField = "transfabid";
                drpcolortype.DataBind();
                ViewState["MyDataSetcolour"] = dgetcolurfab;
                // drpbrand.Items.Insert(0, "Select Brand Name");
            }

            DataSet dtrans = new DataSet();
            dtrans = objBs.getdatalotnumberformaster(drplotno.SelectedValue);

            if (dtrans != null)
            {


                if (dtrans.Tables[0].Rows.Count > 0)
                {
                    DataSet dfit = objBs.Fit();
                    if (dfit.Tables[0].Rows.Count > 0)
                    {
                        Nchkfit.DataSource = dfit.Tables[0];
                        Nchkfit.DataTextField = "Fit";
                        Nchkfit.DataValueField = "Fitid";
                        Nchkfit.DataBind();
                        Nchkfit.Enabled = false;
                    }

                    for (int i = 0; i < dtrans.Tables[0].Rows.Count; i++)
                    {
                        // string fitid = dtrans.Tables[0].Rows[i]["fitid"].ToString();

                        {
                            //Find the checkbox list items using FindByValue and select it.
                            Nchkfit.Items.FindByValue(dtrans.Tables[0].Rows[i]["fitid"].ToString()).Selected = true;
                        }

                    }
                    Sfitchaged(sender, e);



                    gvcustomerorder.DataSource = dtrans;
                    gvcustomerorder.DataBind();

                    //gridsize.DataSource = dtrans;
                    //gridsize.DataBind();
                }
            }
            else
            {
                gvcustomerorder.DataSource = null;
                gvcustomerorder.DataBind();

                //gridsize.DataSource = null;
                //gridsize.DataBind();
            }

            DataSet ds23 = objBs.msatergettotalqtyCuttingprintreport(Convert.ToInt32(drplotno.SelectedValue));
            if (ds23.Tables[0].Rows.Count > 0)
            {

                GridView1.DataSource = ds23;
                GridView1.DataBind();
            }


            //DataSet trasncut = new DataSet();
            //trasncut = objBs.gettrancutfab(drplotno.SelectedValue);
            //if (trasncut.Tables[0].Rows.Count > 0)
            //{
            //    newgridfablist.DataSource = trasncut;
            //    newgridfablist.DataBind();
            //}

        }

        protected void calc(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0;
            double halftot = 0;
            double fulltot = 0;
            double gndtot = 0.00;
            double gndmet = 0.00;
            string fitid = string.Empty;

            newfabclick(sender, e);

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {

                //FULL SLEEVE
                double half = 0;
                double full = 0;


                TextBox txt30fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts30fs");
                TextBox txt32fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts32fs");
                TextBox txt34fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts34fs");

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts36fs");
                TextBox txtxsfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxsfs");
                TextBox txtsfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtssfs");


                TextBox txtmfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsmfs");
                TextBox txtlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtslfs");
                TextBox txtxlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxlfs");

                TextBox txtxxlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxxlfs");
                TextBox txt3xlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txts3xlfs");
                TextBox txt4xlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txts4xlfs");

                //HALF SLEEVE

                TextBox txt30hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts30hs");
                TextBox txt32hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts32hs");
                TextBox txt34hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts34hs");

                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts36hs");
                TextBox txtxshs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxshs");
                TextBox txtshs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsshs");


                TextBox txtmhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsmhs");
                TextBox txtlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtslhs");
                TextBox txtxlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxlhs");

                TextBox txtxxlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxxlhs");
                TextBox txt3xlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txts3xlhs");
                TextBox txt4xlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txts4xlhs");



                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                TextBox txtfirid = (TextBox)gridsize.Rows[vLoop].FindControl("txtfitid");
                TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");
                TextBox txtresona = (TextBox)gridsize.Rows[vLoop].FindControl("txtnarration");
                TextBox txtendbit = (TextBox)gridsize.Rows[vLoop].FindControl("txtendbit");
                DropDownList drpreason = (DropDownList)gridsize.Rows[vLoop].FindControl("drpreason");

                TextBox txtusedmeter = (TextBox)gridsize.Rows[vLoop].FindControl("txtuedmter");
                TextBox txtreqmtr = (TextBox)gridsize.Rows[vLoop].FindControl("txteqrmeter");

                //tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                //    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt44hs.Text);

                tot = Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
            Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txt3xlfs.Text) + Convert.ToDouble(txt4xlfs.Text) +
            Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txtxshs.Text) + Convert.ToDouble(txtshs.Text) +
            Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txt3xlhs.Text) + Convert.ToDouble(txt4xlhs.Text);

                half = Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txtxshs.Text) + Convert.ToDouble(txtshs.Text) +
            Convert.ToDouble(txtmhs.Text) + Convert.ToDouble(txtlhs.Text) + Convert.ToDouble(txtxlhs.Text) + Convert.ToDouble(txtxxlhs.Text) + Convert.ToDouble(txt3xlhs.Text) + Convert.ToDouble(txt4xlhs.Text);

                full = Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txtxsfs.Text) + Convert.ToDouble(txtsfs.Text) +
            Convert.ToDouble(txtmfs.Text) + Convert.ToDouble(txtlfs.Text) + Convert.ToDouble(txtxlfs.Text) + Convert.ToDouble(txtxxlfs.Text) + Convert.ToDouble(txt3xlfs.Text) + Convert.ToDouble(txt4xlfs.Text);


                txtdamage.Text = (tot - Convert.ToDouble(txtshirt.Text)).ToString();

                txttoal.Text = tot.ToString();
                halftot = halftot + half;
                fulltot = fulltot + full;


                gndtot = tot + Convert.ToDouble(txtdamage.Text);
                //if (txtfirid.Text == "Sharp")
                //{
                //    fitid = "3";
                //}
                //else
                //{
                //    fitid = "4";
                //}

                //return;

                //DataSet dcalculate = objBs.getsizeforcutt(txtfirid.Text, drpwidth.SelectedItem.Text);
                //if (dcalculate.Tables[0].Rows.Count > 0)
                //{

                //    double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

                //    double roundoff = Convert.ToDouble(tot) * wid;
                //    //  double roundoff1 = Convert.ToDouble(txtReqMtr.Text) * wid;
                //    if (roundoff > 0.5)
                //    {
                //        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
                //    }
                //    else
                //    {
                //        r = Math.Floor(Convert.ToDouble(roundoff));
                //    }

                //   // txtusedmeter.Text = r.ToString();
                //    //txttotshirt1.Text = tot.ToString();
                //    //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

                //}




                int col = vLoop + 1;

                //if (gndtot > (Convert.ToDouble(txtshirt.Text)))
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt in Row " +col+ ".Thank you!!!');", true);
                //    btnadd.Enabled = false;

                //    return;
                //}
                //else
                //{
                //    btnadd.Enabled = true;
                //}

                if (txtdamage.Text == "0" || txtdamage.Text == "")
                {
                    btnadd.Enabled = true;
                }
                else
                {
                    //if (drpreason.SelectedValue == "3")
                    //{
                    if (txtresona.Text == "" && ((gndtot > (Convert.ToDouble(txtshirt.Text))) || (gndtot < (Convert.ToDouble(txtshirt.Text)))))
                    // if (txtresona.Text == "")
                    {
                        if (drpreason.SelectedValue == "4")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('In damage Shirt Occurs.So Please Enter Reason For Damage Qty in Row " + col + ".Please Select Reason Type.Thank you!!!');", true);
                            btnadd.Enabled = false;
                            return;
                        }
                        else
                        {
                            btnadd.Enabled = true;

                        }
                    }

                    else
                    {
                        btnadd.Enabled = true;
                    }


                    //}
                    //else
                    //{
                    //    btnadd.Enabled = true;
                    //}

                    //if (drpreason.SelectedValue == "3")
                    //{
                    //    if (txtresona.Text == "")
                    //    {

                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.So Please Enter Reason For Damage Qty in Row " + col + ".Thank you!!!');", true);
                    //        btnadd.Enabled = false;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        btnadd.Enabled = true;
                    //    }
                    //}

                    //else
                    //{
                    //    btnadd.Enabled = true;
                    //}

                }
            }

            //txtusedmeter.Text = txtreqmtr.Text;

            //double totalmeter = 0;
            //for (int vLoopp = 0; vLoopp < newgridfablist.Rows.Count; vLoopp++)
            //{
            //    TextBox avaliablemeer = (TextBox)newgridfablist.Rows[vLoopp].FindControl("newtxtAvlmeter");
            //    TextBox reqmeter = (TextBox)newgridfablist.Rows[vLoopp].FindControl("newtxtreqmeter");
            //    TextBox endmeter = (TextBox)newgridfablist.Rows[vLoopp].FindControl("newtxtendmeter");

            //    if (endmeter.Text == "")
            //    {
            //        endmeter.Text = "0";
            //    }

            //    reqmeter.Text = Convert.ToDouble(Convert.ToDouble(reqmeter.Text) - Convert.ToDouble(endmeter.Text)).ToString("f2");

            //    totalmeter = totalmeter + Convert.ToDouble(endmeter.Text);
            //}

            //   txtusedmeter.Text = Convert.ToDouble(Convert.ToDouble(txtusedmeter.Text) - Convert.ToDouble(totalmeter)).ToString("0.00");

            txtfullqty.Text = fulltot.ToString();
            txthalfqty.Text = halftot.ToString();
            txttotalqty.Text = (fulltot + halftot).ToString();
        }

        protected void change36fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;


            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {

                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttefs");

                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtftfs");


                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttehs");

                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfths");

                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text);

                txttoal.Text = tot.ToString();

                gndtot = tot + Convert.ToDouble(txtdamage.Text);


                int col = vLoop + 1;

                if (gndtot > (Convert.ToDouble(txtshirt.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                    btnadd.Enabled = false;

                    return;
                }
                else
                {
                    btnadd.Enabled = true;
                }


                txt38fs.Focus();

                // dparty.Focus();
            }





        }
        protected void change38fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;


            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttefs");

                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtftfs");


                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttehs");

                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfths");

                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text);

                txttoal.Text = tot.ToString();

                gndtot = tot + Convert.ToDouble(txtdamage.Text);


                int col = vLoop + 1;

                if (gndtot > (Convert.ToDouble(txtshirt.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                    btnadd.Enabled = false;

                    return;
                }
                else
                {
                    btnadd.Enabled = true;
                }

                txt40fs.Focus();
                // dparty.Focus();
            }

            ////  if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //  //  btnadd.Enabled = false;
            //    btnadd.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnadd.Enabled = true;
            //}
        }

        protected void change40fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;


            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttefs");

                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtftfs");


                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttehs");

                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfths");

                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text);

                txttoal.Text = tot.ToString();

                gndtot = tot + Convert.ToDouble(txtdamage.Text);


                int col = vLoop + 1;

                if (gndtot > (Convert.ToDouble(txtshirt.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                    btnadd.Enabled = false;

                    return;
                }
                else
                {
                    btnadd.Enabled = true;
                }

                txt42fs.Focus();
                // dparty.Focus();
            }


            ////   if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            // //   btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnadd.Enabled = true;
            //}
        }
        protected void change42fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;


            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttefs");

                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtftfs");


                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttehs");

                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfths");

                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text);

                txttoal.Text = tot.ToString();

                gndtot = tot + Convert.ToDouble(txtdamage.Text);


                int col = vLoop + 1;

                if (gndtot > (Convert.ToDouble(txtshirt.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                    btnadd.Enabled = false;

                    return;
                }
                else
                {
                    btnadd.Enabled = true;
                }

                txt36hs.Focus();
                // dparty.Focus();
            }


            // if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //  //  btnprocess.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    btnadd.Enabled = true;
            //}
        }


        protected void change36hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;


            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttefs");

                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtftfs");


                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttehs");

                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfths");

                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text);

                txttoal.Text = tot.ToString();

                gndtot = tot + Convert.ToDouble(txtdamage.Text);


                int col = vLoop + 1;

                if (gndtot > (Convert.ToDouble(txtshirt.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                    btnadd.Enabled = false;

                    return;
                }
                else
                {
                    btnadd.Enabled = true;
                }

                txt38hs.Focus();
                // dparty.Focus();
            }

            //  if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;

            //    return;
            //}
            //else
            //{
            //    btnadd.Enabled = true;
            //}
        }
        protected void change38hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttefs");

                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtftfs");


                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttehs");

                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfths");

                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text);

                txttoal.Text = tot.ToString();

                gndtot = tot + Convert.ToDouble(txtdamage.Text);


                int col = vLoop + 1;

                if (gndtot > (Convert.ToDouble(txtshirt.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                    btnadd.Enabled = false;

                    return;
                }
                else
                {
                    btnadd.Enabled = true;
                }

                txt40hs.Focus();
                // dparty.Focus();
            }


            //  if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;

            //    return;
            //}
            //else
            //{
            //    btnadd.Enabled = true;
            //}
        }

        protected void change40hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttefs");

                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtftfs");


                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttehs");

                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfths");

                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text);

                txttoal.Text = tot.ToString();

                gndtot = tot + Convert.ToDouble(txtdamage.Text);


                int col = vLoop + 1;

                if (gndtot > (Convert.ToDouble(txtshirt.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                    btnadd.Enabled = false;

                    return;
                }
                else
                {
                    btnadd.Enabled = true;
                }

                txt42hs.Focus();
                // dparty.Focus();
            }

            //     if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;

            //    return;
            //}
            //else
            //{
            //    btnadd.Enabled = true;
            //}
        }
        protected void change42hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;


            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttsfs");
                TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("txttefs");

                TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzfs");
                TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("txtftfs");


                TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttshs");
                TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("txttehs");

                TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfzhs");
                TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("txtfths");

                TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");

                tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) +
                    Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text);

                txttoal.Text = tot.ToString();

                gndtot = tot + Convert.ToDouble(txtdamage.Text);


                int col = vLoop + 1;

                if (gndtot > (Convert.ToDouble(txtshirt.Text)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
                    btnadd.Enabled = false;

                    return;
                }
                else
                {
                    btnadd.Enabled = true;
                }

                txtdamage.Focus();
                // dparty.Focus();
            }


            //      if (gndtot > Convert.ToDouble(txtReqNoShirts.Text))
            //if (gndtot > (Convert.ToDouble(txtReqNoShirts.Text) + Convert.ToDouble(txtextrashirt.Text)))
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;

            //    return;
            //}
            //else
            //{
            //    btnadd.Enabled = true;
            //}
        }

    }
}