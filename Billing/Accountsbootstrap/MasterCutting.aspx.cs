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
    public partial class MasterCutting : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iid;
        string empid = "";
        double Q30F = 0; double Q32F = 0; double Q34F = 0; double Q36F = 0;
        double QXSF = 0; double QSF = 0; double QMF = 0; double QLF = 0;
        double QXLF = 0; double QXXLF = 0; double Q3XLF = 0; double Q4XLF = 0;

        double Q30H = 0; double Q32H = 0; double Q34H = 0; double Q36H = 0;
        double QXSH = 0; double QSH = 0; double QMH = 0; double QLH = 0;
        double QXLH = 0; double QXXLH = 0; double Q3XLH = 0; double Q4XLH = 0;
        double QttlFH = 0; double GVtotalshirt = 0;
        DataSet dsttlcal = new DataSet();

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


                DataSet dbitem = objBs.griditem();
                if (dbitem.Tables[0].Rows.Count > 0)
                {
                    drpitemtype.DataSource = dbitem.Tables[0];
                    drpitemtype.DataTextField = "ItemName";
                    drpitemtype.DataValueField = "itemid";
                    drpitemtype.DataBind();
                    drpitemtype.Items.Insert(0, "Select Item");
                }

                btnadd.Enabled = false;



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
                dlot = objBs.getalllotnumberformaster(drpbranch.SelectedValue);
                if (dlot != null)
                {
                    if (dlot.Tables[0].Rows.Count > 0)
                    {
                        //   drplotno
                        drplotno.DataSource = dlot.Tables[0];
                        drplotno.DataTextField = "CompanyFullLotNo";
                        drplotno.DataValueField = "CutID";
                        drplotno.DataBind();
                        drplotno.Items.Insert(0, "Select Lot");
                    }
                }

            }
        }
        protected void ckhsize_index(object sender, EventArgs e)
        {
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
        }

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



        protected void Add_Click(object sender, EventArgs e)
        {
          
            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you!!');", true);
                return;
            }
            DateTime deliverydate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnadd.Text == "Save")
            {
                if (drplotno.SelectedValue == "Select Lot")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Lot Number. Thank you');", true);
                    return;

                }
                else
                {

                }

                if (lblratiotype.Text == "0")
                {
                    int iStatus = 0;

                    iStatus = objBs.insertmastercutnewone(drplotno.SelectedItem.Text, deliverydate, drpwidth.SelectedValue, ddlcutting.SelectedValue, txtcost.Text, drplotno.SelectedValue, txtcutcost.Text, txttotalqty.Text, txthalfqty.Text, txtfullqty.Text, drpbrand.SelectedValue, empid, drpbranch.SelectedValue,txtitemnarration.Text);

                    //int s30fs = 0, s32fs = 0, s34fs = 0, s36fs = 0, sxsfs = 0, ssfs = 0, smfs = 0, slfs = 0, sxlfs = 0, sxxlfs = 0, s3xlfs = 0, s4xlfs = 0,
                    //    s30hs = 0, s32hs = 0, s34hs = 0, s36hs = 0, sxshs = 0, sshs = 0, smhs = 0, slhs = 0, sxlhs = 0, sxxlhs = 0, s3xlhs = 0, s4xlhs = 0;
                    //int totshirtfshs = 0;


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
                        //Label lblbrandid = (Label)gridsize.Rows[vLoop].FindControl("lblbrandid");

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


                        //if (ddlAgainst.SelectedValue != "0" && txtchequedd.Text != "" && txtAgainstAmount.Text != "0")

                        //totshirtfshs = Convert.ToInt32(txt30fs.Text) + Convert.ToInt32(txt32fs.Text) + Convert.ToInt32(txt34fs.Text) + Convert.ToInt32(txt36fs.Text) + Convert.ToInt32(txtxsfs.Text) +
                        //    Convert.ToInt32(txtsfs.Text) + Convert.ToInt32(txtmfs.Text) + Convert.ToInt32(txtlfs.Text) + Convert.ToInt32(txtxlfs.Text) + Convert.ToInt32(txtxxlfs.Text) +
                        //    Convert.ToInt32(txt3xlfs.Text) + Convert.ToInt32(txt4xlfs.Text) +
                        //    Convert.ToInt32(txt30hs.Text) + Convert.ToInt32(txt32hs.Text) + Convert.ToInt32(txt34hs.Text) + Convert.ToInt32(txt36hs.Text) + Convert.ToInt32(txtxshs.Text) +
                        //    Convert.ToInt32(txtshs.Text) + Convert.ToInt32(txtmhs.Text) + Convert.ToInt32(txtlhs.Text) + Convert.ToInt32(txtxlhs.Text) + Convert.ToInt32(txtxxlhs.Text) +
                        //    Convert.ToInt32(txt3xlhs.Text) + Convert.ToInt32(txt4xlhs.Text);

                        int iStatus2 = objBs.insertTransmastercutnewone(invrefno.Text, txtdesign.Text, partyname.Text, reqmeter.Text, txtrate.Text, txt30fs.Text, txt30hs.Text, txt32fs.Text, txt32hs.Text, txt34fs.Text, txt34hs.Text, txt36fs.Text, txt36hs.Text, txtxsfs.Text, txtxshs.Text, txtsfs.Text, txtshs.Text, txtmfs.Text, txtmhs.Text, txtlfs.Text, txtlhs.Text, txtxlfs.Text, txtxlhs.Text, txtxxlfs.Text, txtxxlhs.Text, txt3xlfs.Text, txt3xlhs.Text, txt4xlfs.Text, txt4xlhs.Text, reqshirt.Text, txttoal.Text, fitid.Text, txtdamage.Text, txtresona.Text, txtmargin.Text, txtmrp.Text, drpreason.SelectedValue, txtusedmeter.Text, txtendbit.Text, itemname.Text, patternid.Text, drplotno.SelectedValue);


                    }
                }
                else if (lblratiotype.Text == "1")
                {
                   string CompleteStitching = "";
                   DataSet dcalculate = objBs.Getstichingsnocontra(Convert.ToInt32(drplotno.SelectedValue));
                   CompleteStitching = dcalculate.Tables[0].Rows[0]["CompleteStitching"].ToString();


                    int iStatus = 0;

                    iStatus = objBs.insertmastercutnewone(drplotno.SelectedItem.Text, deliverydate, drpwidth.SelectedValue, ddlcutting.SelectedValue, txtcost.Text, drplotno.SelectedValue, txtcutcost.Text, txttotalqty.Text, txthalfqty.Text, txtfullqty.Text, drpbrand.SelectedValue, empid, drpbranch.SelectedValue,txtitemnarration.Text);

                    int s30fs = 0, s32fs = 0, s34fs = 0, s36fs = 0, sxsfs = 0, ssfs = 0, smfs = 0, slfs = 0, sxlfs = 0, sxxlfs = 0, s3xlfs = 0, s4xlfs = 0,
                        s30hs = 0, s32hs = 0, s34hs = 0, s36hs = 0, sxshs = 0, sshs = 0, smhs = 0, slhs = 0, sxlhs = 0, sxxlhs = 0, s3xlhs = 0, s4xlhs = 0;
                    int totshirtfshs = 0;
                    string comitemname = string.Empty;
                    string comfitid = string.Empty;

                    int istockidmaster = 0;

                    for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                    {
                        Label invrefno = (Label)gridsize.Rows[vLoop].FindControl("lblno");
                        TextBox txtdesign = (TextBox)gridsize.Rows[vLoop].FindControl("txtdesigno");
                        TextBox brandid = (TextBox)gridsize.Rows[vLoop].FindControl("txtledgerid");
                        TextBox reqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("txteqrmeter");
                        TextBox reqshirt = (TextBox)gridsize.Rows[vLoop].FindControl("txtreqshirt");
                        TextBox fitid = (TextBox)gridsize.Rows[vLoop].FindControl("txtfitid");
                        TextBox itemname = (TextBox)gridsize.Rows[vLoop].FindControl("txtitemname");
                        TextBox patternid = (TextBox)gridsize.Rows[vLoop].FindControl("txtpatternid");
                        Label lblContrast = (Label)gridsize.Rows[vLoop].FindControl("lblContrast");
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

                        TextBox txtavgwtgms = (TextBox)gridsize.Rows[vLoop].FindControl("txtavgwtgms");


                        //if (ddlAgainst.SelectedValue != "0" && txtchequedd.Text != "" && txtAgainstAmount.Text != "0")

                        totshirtfshs = totshirtfshs + Convert.ToInt32(txt30fs.Text) + Convert.ToInt32(txt32fs.Text) + Convert.ToInt32(txt34fs.Text) + Convert.ToInt32(txt36fs.Text) + Convert.ToInt32(txtxsfs.Text) +
                            Convert.ToInt32(txtsfs.Text) + Convert.ToInt32(txtmfs.Text) + Convert.ToInt32(txtlfs.Text) + Convert.ToInt32(txtxlfs.Text) + Convert.ToInt32(txtxxlfs.Text) +
                            Convert.ToInt32(txt3xlfs.Text) + Convert.ToInt32(txt4xlfs.Text) +
                            Convert.ToInt32(txt30hs.Text) + Convert.ToInt32(txt32hs.Text) + Convert.ToInt32(txt34hs.Text) + Convert.ToInt32(txt36hs.Text) + Convert.ToInt32(txtxshs.Text) +
                            Convert.ToInt32(txtshs.Text) + Convert.ToInt32(txtmhs.Text) + Convert.ToInt32(txtlhs.Text) + Convert.ToInt32(txtxlhs.Text) + Convert.ToInt32(txtxxlhs.Text) +
                            Convert.ToInt32(txt3xlhs.Text) + Convert.ToInt32(txt4xlhs.Text);
                        if (vLoop == 0)
                        {
                            comitemname = txtdesign.Text;
                            comfitid = fitid.Text;
                        }

                        int iStatus2 = objBs.insertTransmastercutnewoneratio(invrefno.Text, txtdesign.Text, brandid.Text, reqmeter.Text, txtrate.Text, txt30fs.Text, txt30hs.Text, txt32fs.Text, txt32hs.Text, txt34fs.Text, txt34hs.Text, txt36fs.Text, txt36hs.Text, txtxsfs.Text, txtxshs.Text, txtsfs.Text, txtshs.Text, txtmfs.Text, txtmhs.Text, txtlfs.Text, txtlhs.Text, txtxlfs.Text, txtxlhs.Text, txtxxlfs.Text, txtxxlhs.Text, txt3xlfs.Text, txt3xlhs.Text, txt4xlfs.Text, txt4xlhs.Text, reqshirt.Text, txttoal.Text, fitid.Text, txtdamage.Text, txtresona.Text, txtmargin.Text, txtmrp.Text, drpreason.SelectedValue, txtusedmeter.Text, txtendbit.Text, itemname.Text, patternid.Text, drplotno.SelectedValue, txtavgwtgms.Text);
                        if (CompleteStitching == "No")
                        {
                            int istockid = objBs.insertstockwisestock("New", drplotno.SelectedValue, invrefno.Text, txtdesign.Text, brandid.Text, fitid.Text, itemname.Text, drpbranch.SelectedValue, drplotno.SelectedItem.Text, txttoal.Text, "0", txt30fs.Text, txt32fs.Text, txt34fs.Text, txt36fs.Text, txtxsfs.Text, txtsfs.Text, txtmfs.Text, txtlfs.Text, txtxlfs.Text, txtxxlfs.Text, txt3xlfs.Text, txt4xlfs.Text, txt30hs.Text, txt32hs.Text, txt34hs.Text, txt36hs.Text, txtxshs.Text, txtshs.Text, txtmhs.Text, txtlhs.Text, txtxlhs.Text, txtxxlhs.Text, txt3xlhs.Text, txt4xlhs.Text, "N", 0);

                        }
                    }

                    int istatus = objBs.insertTranslotdetailscutnewoneratio(drplotno.SelectedValue, totshirtfshs.ToString(), comitemname, comfitid);

                    int istatusval = objBs.fabdetailsinmasternewval();
                    istockidmaster = istatusval;

                    #region Stock Ratio For Process

                    DataSet processtypebillno = objBs.getprocesstypeBillNo();
                    string blno = processtypebillno.Tables[0].Rows[0]["master"].ToString();

                    DataSet processtype = objBs.getprocesstype(istockidmaster);

                    for (int r = 0; r < processtype.Tables[0].Rows.Count; r++)
                    {

                        int ProcessTypeID = Convert.ToInt32(processtype.Tables[0].Rows[r]["ProcessTypeID"].ToString());

                        for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                        {
                            Label invrefno = (Label)gridsize.Rows[vLoop].FindControl("lblno");
                            TextBox txtdesign = (TextBox)gridsize.Rows[vLoop].FindControl("txtdesigno");
                            TextBox brandid = (TextBox)gridsize.Rows[vLoop].FindControl("txtledgerid");
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


                            TextBox txttoal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");
                            TextBox txtdamage = (TextBox)gridsize.Rows[vLoop].FindControl("txtdamage");
                            TextBox txtresona = (TextBox)gridsize.Rows[vLoop].FindControl("txtnarration");
                            TextBox txtendbit = (TextBox)gridsize.Rows[vLoop].FindControl("txtendbit");

                            TextBox txtmargin = (TextBox)gridsize.Rows[vLoop].FindControl("txtmar");
                            TextBox txtmrp = (TextBox)gridsize.Rows[vLoop].FindControl("Txtmrp");
                            TextBox txtrate = (TextBox)gridsize.Rows[vLoop].FindControl("Txrtate");

                            TextBox txtusedmeter = (TextBox)gridsize.Rows[vLoop].FindControl("txtuedmter");

                            DropDownList drpreason = (DropDownList)gridsize.Rows[vLoop].FindControl("drpreason");

                         
                            if (vLoop == 0)
                            {
                                comitemname = txtdesign.Text;
                                comfitid = fitid.Text;
                            }
                            int istockid = objBs.insertstockwisestocknew("New", drplotno.SelectedValue, invrefno.Text, txtdesign.Text, brandid.Text, fitid.Text, itemname.Text, drpbranch.SelectedValue, drplotno.SelectedItem.Text, txttoal.Text, "0", txt30fs.Text, txt32fs.Text, txt34fs.Text, txt36fs.Text, txtxsfs.Text, txtsfs.Text, txtmfs.Text, txtlfs.Text, txtxlfs.Text, txtxxlfs.Text, txt3xlfs.Text, txt4xlfs.Text, txt30hs.Text, txt32hs.Text, txt34hs.Text, txt36hs.Text, txtxshs.Text, txtshs.Text, txtmhs.Text, txtlhs.Text, txtxlhs.Text, txtxxlhs.Text, txt3xlhs.Text, txt4xlhs.Text, "N", 0, ProcessTypeID, blno);
                        }
                    }
                    #endregion


                }

                DataSet dstt = new DataSet();
                DataTable dttt = new DataTable();

                DataColumn dc = new DataColumn("id");
                dttt.Columns.Add(dc);

                dc = new DataColumn("fabid");
                dttt.Columns.Add(dc);

                dc = new DataColumn("name");
                dttt.Columns.Add(dc);

                dc = new DataColumn("Givenmeter");
                dttt.Columns.Add(dc);
                dc = new DataColumn("usedmeter");
                dttt.Columns.Add(dc);
                dc = new DataColumn("endmeter");
                dttt.Columns.Add(dc);
                dc = new DataColumn("type");
                dttt.Columns.Add(dc);
                dc = new DataColumn("avgmeter");
                dttt.Columns.Add(dc);


                dstt.Tables.Add(dttt);


                for (int vLoop1 = 0; vLoop1 < newgridfablist.Rows.Count; vLoop1++)
                {
                    Label lblfabidd = (Label)newgridfablist.Rows[vLoop1].FindControl("lblfabidd");
                    Label newfabid = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabid");
                    Label newfabcode = (Label)newgridfablist.Rows[vLoop1].FindControl("newfabcode");
                    Label lblshirttype = (Label)newgridfablist.Rows[vLoop1].FindControl("lblshirttype");
                    Label lblavgmeter = (Label)newgridfablist.Rows[vLoop1].FindControl("lblavgmeter");
                    TextBox givenmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtAvlmeter");
                    TextBox usedmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtreqmeter");
                    TextBox endmeter = (TextBox)newgridfablist.Rows[vLoop1].FindControl("newtxtendmeter");

                    if (endmeter.Text != "0.00" || endmeter.Text != "0")
                    {
                        DataRow drd = dstt.Tables[0].NewRow();
                        drd["id"] = newfabid.Text;
                        drd["fabid"] = lblfabidd.Text;
                        drd["name"] = newfabcode.Text;
                        drd["Givenmeter"] = givenmeter.Text;
                        drd["usedmeter"] = usedmeter.Text;
                        drd["endmeter"] = endmeter.Text;

                        drd["type"] = lblshirttype.Text;

                        drd["avgmeter"] = lblavgmeter.Text;

                        dstt.Tables[0].Rows.Add(drd);
                    }
                }
                int idue = objBs.fabricemaster(dstt);

                Response.Redirect("Mastercutgrid.aspx");
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
                GVtotalshirt = GVtotalshirt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "totalshirt"));

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

                //QttlFH = QttlFH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Totalshirt"));
                QttlFH = QttlFH + Q30F + Q32F + Q34F + Q36F + QXSF + QSF + QMF + QLF + QXLF + QXXLF + Q3XLF + Q4XLF + Q30H + Q32H + Q34H + Q36H + QXSH + QSH + QMH + QLH + QXLH + QXXLH + Q3XLH + Q4XLH;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = GVtotalshirt.ToString();

                e.Row.Cells[8].Text = Q30F.ToString();
                e.Row.Cells[9].Text = Q32F.ToString();
                e.Row.Cells[10].Text = Q34F.ToString();
                e.Row.Cells[11].Text = Q36F.ToString();
                e.Row.Cells[12].Text = QXSF.ToString();
                e.Row.Cells[13].Text = QSF.ToString();
                e.Row.Cells[14].Text = QMF.ToString();
                e.Row.Cells[15].Text = QLF.ToString();
                e.Row.Cells[16].Text = QXLF.ToString();
                e.Row.Cells[17].Text = QXXLF.ToString();
                e.Row.Cells[18].Text = Q3XLF.ToString();
                e.Row.Cells[19].Text = Q4XLF.ToString();

                e.Row.Cells[20].Text = Q30H.ToString();
                e.Row.Cells[21].Text = Q32H.ToString();
                e.Row.Cells[22].Text = Q34H.ToString();
                e.Row.Cells[23].Text = Q36H.ToString();
                e.Row.Cells[24].Text = QXSH.ToString();
                e.Row.Cells[25].Text = QSH.ToString();
                e.Row.Cells[26].Text = QMH.ToString();
                e.Row.Cells[27].Text = QLH.ToString();
                e.Row.Cells[28].Text = QXLH.ToString();
                e.Row.Cells[29].Text = QXXLH.ToString();
                e.Row.Cells[30].Text = Q3XLH.ToString();
                e.Row.Cells[31].Text = Q4XLH.ToString();

                e.Row.Cells[33].Text = QttlFH.ToString();
                // e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void btncalc_Click(object sender, EventArgs e)
        {

            double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;
            double grandtotal = 0;
            double grandtotalamount = 0;

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                double total = 0;

                //TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                //TextBox txtRate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");



                TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                F30 = F30 + Convert.ToDouble(txts30fs.Text);
                TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                H30 = H30 + Convert.ToDouble(txts30hs.Text);

                TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                F32 = F32 + Convert.ToDouble(txts32fs.Text);
                TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                H32 = H32 + Convert.ToDouble(txts32hs.Text);

                TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                F34 = F34 + Convert.ToDouble(txts34fs.Text);
                TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                H34 = H34 + Convert.ToDouble(txts34hs.Text);

                TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                F36 = F36 + Convert.ToDouble(txts36fs.Text);
                TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                H36 = H36 + Convert.ToDouble(txts36hs.Text);

                TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                FXS = FXS + Convert.ToDouble(txtsxsfs.Text);
                TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");
                HXS = HXS + Convert.ToDouble(txtsxshs.Text);

                TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                FS = FS + Convert.ToDouble(txtssfs.Text);
                TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                HS = HS + Convert.ToDouble(txtsshs.Text);

                TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                FM = FM + Convert.ToDouble(txtsmfs.Text);
                TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                HM = HM + Convert.ToDouble(txtsmhs.Text);

                TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                FL = FL + Convert.ToDouble(txtslfs.Text);
                TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                HL = HL + Convert.ToDouble(txtslhs.Text);

                TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                FXL = FXL + Convert.ToDouble(txtsxlfs.Text);
                TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                HXL = HXL + Convert.ToDouble(txtsxlhs.Text);

                TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                FXXL = FXXL + Convert.ToDouble(txtsxxlfs.Text);
                TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                HXXL = HXXL + Convert.ToDouble(txtsxxlhs.Text);

                TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                F3XL = F3XL + Convert.ToDouble(txts3xlfs.Text);
                TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                H3XL = H3XL + Convert.ToDouble(txts3xlhs.Text);

                TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                F4XL = F4XL + Convert.ToDouble(txts4xlfs.Text);
                TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");
                H4XL = H4XL + Convert.ToDouble(txts4xlhs.Text);



                total = Convert.ToDouble(txts30fs.Text) + Convert.ToDouble(txts30hs.Text) + Convert.ToDouble(txts32fs.Text) + Convert.ToDouble(txts32hs.Text) + Convert.ToDouble(txts34fs.Text) + Convert.ToDouble(txts34hs.Text) + Convert.ToDouble(txts36fs.Text) + Convert.ToDouble(txts36hs.Text) + Convert.ToDouble(txtsxsfs.Text) + Convert.ToDouble(txtsxshs.Text) + Convert.ToDouble(txtslfs.Text) + Convert.ToDouble(txtslhs.Text) + Convert.ToDouble(txtssfs.Text) + Convert.ToDouble(txtsshs.Text) + Convert.ToDouble(txtsmfs.Text) + Convert.ToDouble(txtsmhs.Text) + Convert.ToDouble(txtsxlfs.Text) + Convert.ToDouble(txtsxlhs.Text) + Convert.ToDouble(txtsxxlfs.Text) + Convert.ToDouble(txtsxxlhs.Text) + Convert.ToDouble(txts3xlfs.Text) + Convert.ToDouble(txts3xlhs.Text) + Convert.ToDouble(txts4xlfs.Text) + Convert.ToDouble(txts4xlhs.Text);
                //txtsendFQty.Text = total.ToString();
                //grandtotal = grandtotal + total;
                //grandtotalamount = grandtotalamount + (Convert.ToDouble(txtsendFQty.Text) * Convert.ToDouble(txtRate.Text));

                //if (txtRate.Text == "" || txtRate.Text == "0" || txtRate.Text == "0.0000")
                //{
                //    if (txtsendFQty.Text == "0")
                //    {

                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate in  " + Convert.ToInt32(i + 1) + " Row . Thank you!!');", true);
                //        return;
                //    }
                //}
            }
            lb30f.Text = F30.ToString();
            lb32f.Text = F32.ToString();
            lb34f.Text = F34.ToString();
            lb36f.Text = F36.ToString();
            lbxsf.Text = FXS.ToString();
            lbsf.Text = FS.ToString();
            lbmf.Text = FM.ToString();
            lblf.Text = FL.ToString();
            lbxlf.Text = FXL.ToString();
            lbxxlf.Text = FXXL.ToString();
            lb3xlf.Text = F3XL.ToString();
            lb4xlf.Text = F4XL.ToString();


            lb30h.Text = H30.ToString();
            lb32h.Text = H32.ToString();
            lb34h.Text = H34.ToString();
            lb36h.Text = H36.ToString();
            lbxsh.Text = HXS.ToString();
            lbsh.Text = HS.ToString();
            lbmh.Text = HM.ToString();
            lblh.Text = HL.ToString();
            lbxlh.Text = HXL.ToString();
            lbxxlh.Text = HXXL.ToString();
            lb3xlh.Text = H3XL.ToString();
            lb4xlh.Text = H4XL.ToString();

            LabelTotal.Text = grandtotal.ToString();
            // txtAmount.Text = grandtotalamount.ToString("f2");
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
            double totalmeter = 0;

            //DataSet trasncut = new DataSet();
            //trasncut = objBs.gettrancutfab(drplotno.SelectedValue);
            //if (trasncut.Tables[0].Rows.Count > 0)
            //{
            //    newgridfablist.DataSource = trasncut;
            //    newgridfablist.DataBind();
            //}

            for (int vLoop = 0; vLoop < newgridfablist.Rows.Count; vLoop++)
            {
                TextBox avaliablemeer = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtAvlmeter");
                TextBox reqmeter = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtreqmeter");
                TextBox endmeter = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtendmeter");

                if (endmeter.Text == "")
                {
                    endmeter.Text = "0";
                }

                reqmeter.Text = Convert.ToDouble(Convert.ToDouble(avaliablemeer.Text) - Convert.ToDouble(endmeter.Text)).ToString("f2");

                totalmeter = (Convert.ToDouble(reqmeter.Text) + Convert.ToDouble(endmeter.Text));


                string dummy = totalmeter.ToString("0.00");

                if (Convert.ToDouble(dummy) == Convert.ToDouble(avaliablemeer.Text))
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given End bit in greater Than that Given Meter in Row " + vLoop + ".Thank you!!!');", true);
                    return;
                }


            }

            for (int vLoop = 0; vLoop < newgridfablist.Rows.Count; vLoop++)
            {
                Label transfabid = (Label)newgridfablist.Rows[vLoop].FindControl("newfabid");
                TextBox reqmeter = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtreqmeter");

                for (int vLoop1 = 0; vLoop1 < gridsize.Rows.Count; vLoop1++)
                {
                    TextBox txttransfabid = (TextBox)gridsize.Rows[vLoop1].FindControl("txttransfabid");
                    TextBox txteqrmeter = (TextBox)gridsize.Rows[vLoop1].FindControl("txteqrmeter");


                    if (txttransfabid.Text == transfabid.Text)
                    {
                        txteqrmeter.Text = reqmeter.Text;
                    }


                }
                
            }
        }
        protected void drplotchanged(object sender, EventArgs e)
        {
            DataSet dst = objBs.GetLedgerALLType();
            if (dst != null)
            {
                if (dst.Tables[0].Rows.Count > 0)
                {
                    ddlcutting.DataSource = dst.Tables[0];
                    ddlcutting.DataTextField = "LedgerName";
                    ddlcutting.DataValueField = "LedgerID";
                    ddlcutting.DataBind();
                    //ddlcutting.Items.Insert(0, "Select  Name");
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

            DataSet brandName = objBs.getBrandName();
            if (brandName.Tables[0].Rows.Count > 0)
            {
                drpbrand.DataSource = brandName.Tables[0];
                drpbrand.DataTextField = "name";
                drpbrand.DataValueField = "BrandID";
                drpbrand.DataBind();
                // drpbrand.Items.Insert(0, "Select Brand Name");
            }

            string fitid = "0";
            string ratio = "0";
            DataSet ddata = new DataSet();
            ddata = objBs.getdataforlotnumber(drplotno.SelectedValue);
            if (ddata.Tables[0].Rows.Count > 0)
            {
                txtitemnarration.Text = ddata.Tables[0].Rows[0]["ItemNarrations"].ToString();
                txtdate.Text = Convert.ToDateTime(ddata.Tables[0].Rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");
                drpwidth.SelectedValue = ddata.Tables[0].Rows[0]["Width"].ToString();
                ddlcutting.SelectedValue = ddata.Tables[0].Rows[0]["Cuttingmaster"].ToString();
                txtcost.Text = ddata.Tables[0].Rows[0]["Productcost"].ToString();
                drpbrand.SelectedValue = ddata.Tables[0].Rows[0]["Brandid"].ToString();
                fitid = ddata.Tables[0].Rows[0]["fit"].ToString();
                ratio = ddata.Tables[0].Rows[0]["RatioWise"].ToString();
                drpitemtype.SelectedValue = ddata.Tables[0].Rows[0]["itemid"].ToString();
                drpitemtype.Enabled = false;
                lblratiotype.Text = ratio;

                DataSet getcuttingrate = objBs.getrateforcuttingmaster(ddlcutting.SelectedValue, drpitemtype.SelectedValue);
               
                    if (getcuttingrate.Tables.Count > 0)
                    {
                        if (getcuttingrate.Tables[0].Rows.Count > 0)
                        {
                            txtcutcost.Text = Convert.ToDouble(getcuttingrate.Tables[0].Rows[0]["cutrate"]).ToString("0.00");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Contact Administrator.For Rate Issue For cutting Master.Thank you!!!');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Contact Administrator.For Rate Issue For cutting Master.Thank you!!!');", true);
                        return;
                    }
                
              


            }

            #region SIZE PROCESS

            if (ratio == "1")
            {
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


                DataSet dsizee = objBs.Getfitseize(fitid, drpbrand.SelectedValue);
                if ((dsizee.Tables[0].Rows.Count > 0))
                {
                    //Select the checkboxlist items those values are true in database
                    //Loop through the DataTable
                    for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                    {
                        //You need to change this as per your DB Design
                        string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();




                        {
                            //Find the checkbox list items using FindByValue and select it.
                            chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                        }

                    }
                }

                // CHECK AND VISIBLE SIZES

                if (chkSizes.SelectedIndex >= 0)
                {
                    #region

                    gvcustomerorder.Columns[8].Visible = false; //30FS
                    gvcustomerorder.Columns[9].Visible = false; //32FS

                    gvcustomerorder.Columns[10].Visible = false;//34Fs
                    gvcustomerorder.Columns[11].Visible = false;//36Fs

                    gvcustomerorder.Columns[12].Visible = false; //XSFS
                    gvcustomerorder.Columns[13].Visible = false; //SFS

                    gvcustomerorder.Columns[14].Visible = false; //MFS
                    gvcustomerorder.Columns[15].Visible = false; //LFS

                    gvcustomerorder.Columns[16].Visible = false; //XLFS
                    gvcustomerorder.Columns[17].Visible = false; //xxlFS

                    gvcustomerorder.Columns[18].Visible = false; //3xlHS
                    gvcustomerorder.Columns[19].Visible = false; //4xlHS

                    gvcustomerorder.Columns[20].Visible = false; //30HS

                    gvcustomerorder.Columns[21].Visible = false; //32HS

                    gvcustomerorder.Columns[22].Visible = false; //34HS
                    gvcustomerorder.Columns[23].Visible = false; //36HS

                    gvcustomerorder.Columns[24].Visible = false; //XSHS
                    gvcustomerorder.Columns[25].Visible = false; //SHS

                    gvcustomerorder.Columns[26].Visible = false; //MHS
                    gvcustomerorder.Columns[27].Visible = false; //LHS

                    gvcustomerorder.Columns[28].Visible = false; //XLHS
                    gvcustomerorder.Columns[29].Visible = false; //XXLHS

                    gvcustomerorder.Columns[30].Visible = false; //3XLHS
                    gvcustomerorder.Columns[31].Visible = false; //4XLHS


                    #endregion

                    int lop = 0;
                    //Loop through each item of checkboxlist
                    foreach (ListItem item in chkSizes.Items)
                    {
                        #region
                        //check if item selected

                        if (item.Selected)
                        {

                            {
                                if (item.Text == "30FS")
                                {
                                    gvcustomerorder.Columns[8].Visible = true;
                                }
                                if (item.Text == "32FS")
                                {
                                    gvcustomerorder.Columns[9].Visible = true;
                                }
                                if (item.Text == "34FS")
                                {
                                    gvcustomerorder.Columns[10].Visible = true;
                                }
                                if (item.Text == "36FS")
                                {
                                    gvcustomerorder.Columns[11].Visible = true;
                                }
                                if (item.Text == "XSFS")
                                {
                                    gvcustomerorder.Columns[12].Visible = true;
                                }
                                if (item.Text == "SFS")
                                {
                                    gvcustomerorder.Columns[13].Visible = true;
                                }
                                if (item.Text == "MFS")
                                {
                                    gvcustomerorder.Columns[14].Visible = true;
                                }
                                if (item.Text == "LFS")
                                {
                                    gvcustomerorder.Columns[15].Visible = true;
                                }
                                if (item.Text == "XLFS")
                                {
                                    gvcustomerorder.Columns[16].Visible = true;
                                }
                                if (item.Text == "XXLFS")
                                {
                                    gvcustomerorder.Columns[17].Visible = true;
                                }
                                if (item.Text == "3XLFS")
                                {
                                    gvcustomerorder.Columns[18].Visible = true;
                                }
                                if (item.Text == "4XLFS")
                                {
                                    gvcustomerorder.Columns[19].Visible = true;
                                }


                                // FOR HS

                                if (item.Text == "30HS")
                                {
                                    gvcustomerorder.Columns[20].Visible = true;
                                }

                                if (item.Text == "32HS")
                                {
                                    gvcustomerorder.Columns[21].Visible = true;
                                }

                                if (item.Text == "34HS")
                                {
                                    gvcustomerorder.Columns[22].Visible = true;
                                }

                                if (item.Text == "36HS")
                                {
                                    gvcustomerorder.Columns[23].Visible = true;

                                }

                                if (item.Text == "XSHS")
                                {
                                    gvcustomerorder.Columns[24].Visible = true;
                                }

                                if (item.Text == "SHS")
                                {
                                    gvcustomerorder.Columns[25].Visible = true;
                                }

                                if (item.Text == "MHS")
                                {
                                    gvcustomerorder.Columns[26].Visible = true;
                                }

                                if (item.Text == "LHS")
                                {
                                    gvcustomerorder.Columns[27].Visible = true;
                                }

                                if (item.Text == "XLHS")
                                {
                                    gvcustomerorder.Columns[28].Visible = true;
                                }

                                if (item.Text == "XXLHS")
                                {
                                    gvcustomerorder.Columns[29].Visible = true;
                                }

                                if (item.Text == "3XLHS")
                                {
                                    gvcustomerorder.Columns[30].Visible = true;
                                }

                                if (item.Text == "4XLHS")
                                {
                                    gvcustomerorder.Columns[31].Visible = true;
                                }





                                lop++;

                            }
                        }

                        #endregion
                    }
                    //gvcustomerorder.DataSource = dssmer;
                    //gvcustomerorder.DataBind();
                }
                else
                {
                    #region
                    gvcustomerorder.Columns[8].Visible = false; //30FS
                    gvcustomerorder.Columns[9].Visible = false; //32FS

                    gvcustomerorder.Columns[10].Visible = false;//34Fs
                    gvcustomerorder.Columns[11].Visible = false;//36Fs

                    gvcustomerorder.Columns[12].Visible = false; //XSFS
                    gvcustomerorder.Columns[13].Visible = false; //SFS

                    gvcustomerorder.Columns[14].Visible = false; //MFS
                    gvcustomerorder.Columns[15].Visible = false; //LFS

                    gvcustomerorder.Columns[16].Visible = false; //XLFS
                    gvcustomerorder.Columns[17].Visible = false; //xxlFS

                    gvcustomerorder.Columns[18].Visible = false; //3xlHS
                    gvcustomerorder.Columns[19].Visible = false; //4xlHS

                    gvcustomerorder.Columns[20].Visible = false; //30HS

                    gvcustomerorder.Columns[21].Visible = false; //32HS

                    gvcustomerorder.Columns[22].Visible = false; //34HS
                    gvcustomerorder.Columns[23].Visible = false; //36HS

                    gvcustomerorder.Columns[24].Visible = false; //XSHS
                    gvcustomerorder.Columns[25].Visible = false; //SHS

                    gvcustomerorder.Columns[26].Visible = false; //MHS
                    gvcustomerorder.Columns[27].Visible = false; //LHS

                    gvcustomerorder.Columns[28].Visible = false; //XLHS
                    gvcustomerorder.Columns[29].Visible = false; //XXLHS

                    gvcustomerorder.Columns[30].Visible = false; //3XLHS
                    gvcustomerorder.Columns[31].Visible = false; //4XLHS

                    #endregion
                }


                if (chkSizes.SelectedIndex >= 0)
                {
                    #region

                    gridsize.Columns[8].Visible = false; //30FS
                    gridsize.Columns[9].Visible = false; //32FS

                    gridsize.Columns[10].Visible = false;//34Fs
                    gridsize.Columns[11].Visible = false;//36Fs

                    gridsize.Columns[12].Visible = false; //XSFS
                    gridsize.Columns[13].Visible = false; //SFS

                    gridsize.Columns[14].Visible = false; //MFS
                    gridsize.Columns[15].Visible = false; //LFS

                    gridsize.Columns[16].Visible = false; //XLFS
                    gridsize.Columns[17].Visible = false; //xxlFS

                    gridsize.Columns[18].Visible = false; //3xlHS
                    gridsize.Columns[19].Visible = false; //4xlHS

                    gridsize.Columns[20].Visible = false; //30HS

                    gridsize.Columns[21].Visible = false; //32HS

                    gridsize.Columns[22].Visible = false; //34HS
                    gridsize.Columns[23].Visible = false; //36HS

                    gridsize.Columns[24].Visible = false; //XSHS
                    gridsize.Columns[25].Visible = false; //SHS

                    gridsize.Columns[26].Visible = false; //MHS
                    gridsize.Columns[27].Visible = false; //LHS

                    gridsize.Columns[28].Visible = false; //XLHS
                    gridsize.Columns[29].Visible = false; //XXLHS

                    gridsize.Columns[30].Visible = false; //3XLHS
                    gridsize.Columns[31].Visible = false; //4XLHS




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
                                    gridsize.Columns[8].Visible = true;
                                }
                                if (item.Text == "32FS")
                                {
                                    gridsize.Columns[9].Visible = true;
                                }
                                if (item.Text == "34FS")
                                {
                                    gridsize.Columns[10].Visible = true;
                                }
                                if (item.Text == "36FS")
                                {
                                    gridsize.Columns[11].Visible = true;
                                }
                                if (item.Text == "XSFS")
                                {
                                    gridsize.Columns[12].Visible = true;
                                }
                                if (item.Text == "SFS")
                                {
                                    gridsize.Columns[13].Visible = true;
                                }
                                if (item.Text == "MFS")
                                {
                                    gridsize.Columns[14].Visible = true;
                                }
                                if (item.Text == "LFS")
                                {
                                    gridsize.Columns[15].Visible = true;
                                }
                                if (item.Text == "XLFS")
                                {
                                    gridsize.Columns[16].Visible = true;
                                }
                                if (item.Text == "XXLFS")
                                {
                                    gridsize.Columns[17].Visible = true;
                                }
                                if (item.Text == "3XLFS")
                                {
                                    gridsize.Columns[18].Visible = true;
                                }
                                if (item.Text == "4XLFS")
                                {
                                    gridsize.Columns[19].Visible = true;
                                }


                                // FOR HS

                                if (item.Text == "30HS")
                                {
                                    gridsize.Columns[20].Visible = true;
                                }

                                if (item.Text == "32HS")
                                {
                                    gridsize.Columns[21].Visible = true;
                                }

                                if (item.Text == "34HS")
                                {
                                    gridsize.Columns[22].Visible = true;
                                }

                                if (item.Text == "36HS")
                                {
                                    gridsize.Columns[23].Visible = true;

                                }

                                if (item.Text == "XSHS")
                                {
                                    gridsize.Columns[24].Visible = true;
                                }

                                if (item.Text == "SHS")
                                {
                                    gridsize.Columns[25].Visible = true;
                                }

                                if (item.Text == "MHS")
                                {
                                    gridsize.Columns[26].Visible = true;
                                }

                                if (item.Text == "LHS")
                                {
                                    gridsize.Columns[27].Visible = true;
                                }

                                if (item.Text == "XLHS")
                                {
                                    gridsize.Columns[28].Visible = true;
                                }

                                if (item.Text == "XXLHS")
                                {
                                    gridsize.Columns[29].Visible = true;
                                }

                                if (item.Text == "3XLHS")
                                {
                                    gridsize.Columns[30].Visible = true;
                                }

                                if (item.Text == "4XLHS")
                                {
                                    gridsize.Columns[31].Visible = true;
                                }





                                lop++;

                            }
                        }
                    }
                    //gridsize.DataSource = dssmer;
                    //gridsize.DataBind();

                    #endregion
                }
                else
                {
                    #region

                    gridsize.Columns[8].Visible = false; //30FS
                    gridsize.Columns[9].Visible = false; //32FS

                    gridsize.Columns[10].Visible = false;//34Fs
                    gridsize.Columns[11].Visible = false;//36Fs

                    gridsize.Columns[12].Visible = false; //XSFS
                    gridsize.Columns[13].Visible = false; //SFS

                    gridsize.Columns[14].Visible = false; //MFS
                    gridsize.Columns[15].Visible = false; //LFS

                    gridsize.Columns[16].Visible = false; //XLFS
                    gridsize.Columns[17].Visible = false; //xxlFS

                    gridsize.Columns[18].Visible = false; //3xlHS
                    gridsize.Columns[19].Visible = false; //4xlHS

                    gridsize.Columns[20].Visible = false; //30HS

                    gridsize.Columns[21].Visible = false; //32HS

                    gridsize.Columns[22].Visible = false; //34HS
                    gridsize.Columns[23].Visible = false; //36HS

                    gridsize.Columns[24].Visible = false; //XSHS
                    gridsize.Columns[25].Visible = false; //SHS

                    gridsize.Columns[26].Visible = false; //MHS
                    gridsize.Columns[27].Visible = false; //LHS

                    gridsize.Columns[28].Visible = false; //XLHS
                    gridsize.Columns[29].Visible = false; //XXLHS

                    gridsize.Columns[30].Visible = false; //3XLHS
                    gridsize.Columns[31].Visible = false; //4XLHS

                    #endregion
                }

            }
            else
            {
                #region
                gvcustomerorder.Columns[8].Visible = true; //30FS
                gvcustomerorder.Columns[9].Visible = true; //32FS

                gvcustomerorder.Columns[10].Visible = true;//34Fs
                gvcustomerorder.Columns[11].Visible = true;//36Fs

                gvcustomerorder.Columns[12].Visible = true; //XSFS
                gvcustomerorder.Columns[13].Visible = true; //SFS

                gvcustomerorder.Columns[14].Visible = true; //MFS
                gvcustomerorder.Columns[15].Visible = true; //LFS

                gvcustomerorder.Columns[16].Visible = true; //XLFS
                gvcustomerorder.Columns[17].Visible = true; //xxlFS

                gvcustomerorder.Columns[18].Visible = true; //3xlHS
                gvcustomerorder.Columns[19].Visible = true; //4xlHS

                gvcustomerorder.Columns[20].Visible = true; //30HS

                gvcustomerorder.Columns[21].Visible = true; //32HS

                gvcustomerorder.Columns[22].Visible = true; //34HS
                gvcustomerorder.Columns[23].Visible = true; //36HS

                gvcustomerorder.Columns[24].Visible = true; //XSHS
                gvcustomerorder.Columns[25].Visible = true; //SHS

                gvcustomerorder.Columns[26].Visible = true; //MHS
                gvcustomerorder.Columns[27].Visible = true; //LHS

                gvcustomerorder.Columns[28].Visible = true; //XLHS
                gvcustomerorder.Columns[29].Visible = true; //XXLHS

                gvcustomerorder.Columns[30].Visible = true; //3XLHS
                gvcustomerorder.Columns[31].Visible = true; //4XLHS


                gridsize.Columns[8].Visible = true; //30FS
                gridsize.Columns[9].Visible = true; //32FS

                gridsize.Columns[10].Visible = true;//34Fs
                gridsize.Columns[11].Visible = true;//36Fs

                gridsize.Columns[12].Visible = true; //XSFS
                gridsize.Columns[13].Visible = true; //SFS

                gridsize.Columns[14].Visible = true; //MFS
                gridsize.Columns[15].Visible = true; //LFS

                gridsize.Columns[16].Visible = true; //XLFS
                gridsize.Columns[17].Visible = true; //xxlFS

                gridsize.Columns[18].Visible = true; //3xlHS
                gridsize.Columns[19].Visible = true; //4xlHS

                gridsize.Columns[20].Visible = true; //30HS

                gridsize.Columns[21].Visible = true; //32HS

                gridsize.Columns[22].Visible = true; //34HS
                gridsize.Columns[23].Visible = true; //36HS

                gridsize.Columns[24].Visible = true; //XSHS
                gridsize.Columns[25].Visible = true; //SHS

                gridsize.Columns[26].Visible = true; //MHS
                gridsize.Columns[27].Visible = true; //LHS

                gridsize.Columns[28].Visible = true; //XLHS
                gridsize.Columns[29].Visible = true; //XXLHS

                gridsize.Columns[30].Visible = true; //3XLHS
                gridsize.Columns[31].Visible = true; //4XLHS

                #endregion
            }

            #endregion


           


            DataSet dtrans = new DataSet();
            dtrans = objBs.getdataforlotnumberfortranscutting(drplotno.SelectedValue);
            if (dtrans.Tables[0].Rows.Count > 0)
            {
                gvcustomerorder.DataSource = dtrans;
                gvcustomerorder.DataBind();

                gridsize.DataSource = dtrans;
                gridsize.DataBind();
            }

            DataSet trasncut = new DataSet();
            trasncut = objBs.gettrancutfab(drplotno.SelectedValue);
            if (trasncut.Tables[0].Rows.Count > 0)
            {
                newgridfablist.DataSource = trasncut;
                newgridfablist.DataBind();
            }


            if (dtrans.Tables[0].Rows.Count > 0)
            {
                #region
                gridsize.Columns[8].Visible = false;
                gridsize.Columns[9].Visible = false;
                gridsize.Columns[10].Visible = false;
                gridsize.Columns[11].Visible = false;
                gridsize.Columns[12].Visible = false;
                gridsize.Columns[13].Visible = false;
                gridsize.Columns[14].Visible = false;
                gridsize.Columns[15].Visible = false;
                gridsize.Columns[16].Visible = false;
                gridsize.Columns[17].Visible = false;
                gridsize.Columns[18].Visible = false;
                gridsize.Columns[19].Visible = false;

                gridsize.Columns[20].Visible = false;
                gridsize.Columns[21].Visible = false;
                gridsize.Columns[22].Visible = false;
                gridsize.Columns[23].Visible = false;
                gridsize.Columns[24].Visible = false;
                gridsize.Columns[25].Visible = false;
                gridsize.Columns[26].Visible = false;
                gridsize.Columns[27].Visible = false;
                gridsize.Columns[28].Visible = false;
                gridsize.Columns[29].Visible = false;
                gridsize.Columns[30].Visible = false;
                gridsize.Columns[31].Visible = false;

                for (int j = 0; j < dtrans.Tables[0].Rows.Count; j++)
                {

                    #region
                    string S30 = dtrans.Tables[0].Rows[j]["s30FS"].ToString();
                    string S32 = dtrans.Tables[0].Rows[j]["s32FS"].ToString();
                    string S34 = dtrans.Tables[0].Rows[j]["s34FS"].ToString();
                    string S36 = dtrans.Tables[0].Rows[j]["s36FS"].ToString();
                    string SXS = dtrans.Tables[0].Rows[j]["SxsFS"].ToString();
                    string SS = dtrans.Tables[0].Rows[j]["SsFS"].ToString();
                    string SM = dtrans.Tables[0].Rows[j]["SmFS"].ToString();
                    string SL = dtrans.Tables[0].Rows[j]["SlFS"].ToString();
                    string SXL = dtrans.Tables[0].Rows[j]["SxlFS"].ToString();
                    string SXXL = dtrans.Tables[0].Rows[j]["SxxlFS"].ToString();
                    string S3XL = dtrans.Tables[0].Rows[j]["s3xlFS"].ToString();
                    string S4XL = dtrans.Tables[0].Rows[j]["s4xlFS"].ToString();


                    string HS30 = dtrans.Tables[0].Rows[j]["s30HS"].ToString();
                    string HS32 = dtrans.Tables[0].Rows[j]["s32HS"].ToString();
                    string HS34 = dtrans.Tables[0].Rows[j]["s34HS"].ToString();
                    string HS36 = dtrans.Tables[0].Rows[j]["s36HS"].ToString();
                    string HSXS = dtrans.Tables[0].Rows[j]["SxsHS"].ToString();
                    string HSS = dtrans.Tables[0].Rows[j]["SsHS"].ToString();
                    string HSM = dtrans.Tables[0].Rows[j]["SmHS"].ToString();
                    string HSL = dtrans.Tables[0].Rows[j]["SlHS"].ToString();
                    string HSXL = dtrans.Tables[0].Rows[j]["SxlHS"].ToString();
                    string HSXXL = dtrans.Tables[0].Rows[j]["SxxlHS"].ToString();
                    string HS3XL = dtrans.Tables[0].Rows[j]["s3xlHS"].ToString();
                    string HS4XL = dtrans.Tables[0].Rows[j]["s4xlHS"].ToString();

                    if (S30 != "0")
                    {

                        gridsize.Columns[8].Visible = true;
                    }
                    if (S32 != "0")
                    {

                        gridsize.Columns[9].Visible = true;
                    }

                    if (S34 != "0")
                    {

                        gridsize.Columns[10].Visible = true;
                    }

                    if (S36 != "0")
                    {

                        gridsize.Columns[11].Visible = true;
                    }

                    if (SXS != "0")
                    {

                        gridsize.Columns[12].Visible = true;
                    }

                    if (SS != "0")
                    {

                        gridsize.Columns[13].Visible = true;
                       
                    }

                    if (SM != "0")
                    {

                        gridsize.Columns[14].Visible = true;
                        
                    }

                    if (SL != "0")
                    {

                        gridsize.Columns[15].Visible = true;
                    
                    }

                    if (SXL != "0")
                    {

                        gridsize.Columns[16].Visible = true;
                        
                    }

                    if (SXXL != "0")
                    {

                        gridsize.Columns[17].Visible = true;
                      
                    }

                    if (S3XL != "0")
                    {

                        gridsize.Columns[18].Visible = true;
                       
                    }

                    if (S4XL != "0")
                    {

                        gridsize.Columns[19].Visible = true;
                       
                    }


                    if (HS30 != "0")
                    {

                        gridsize.Columns[20].Visible = true;
                    }
                    if (HS32 != "0")
                    {

                        gridsize.Columns[21].Visible = true;
                    }

                    if (HS34 != "0")
                    {

                        gridsize.Columns[22].Visible = true;
                    }

                    if (HS36 != "0")
                    {

                        gridsize.Columns[23].Visible = true;
                    }

                    if (HSXS != "0")
                    {

                        gridsize.Columns[24].Visible = true;
                    }

                    if (HSS != "0")
                    {

                        gridsize.Columns[25].Visible = true;
                       
                    }

                    if (HSM != "0")
                    {

                        gridsize.Columns[26].Visible = true;
                       
                    }

                    if (HSL != "0")
                    {

                        gridsize.Columns[27].Visible = true;
                      
                    }

                    if (HSXL != "0")
                    {

                        gridsize.Columns[28].Visible = true;
                       
                    }

                    if (HSXXL != "0")
                    {

                        gridsize.Columns[29].Visible = true;
                      
                    }

                    if (HS3XL != "0")
                    {

                      
                        gridsize.Columns[30].Visible = true;
                    }

                    if (HS4XL != "0")
                    {

                        gridsize.Columns[31].Visible = true;
                       
                    }
                    #endregion

                }

                #endregion


            }

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

                TextBox txtavgwtgms = (TextBox)gridsize.Rows[vLoop].FindControl("txtavgwtgms");

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


                txtavgwtgms.Text = (Convert.ToDouble(txtreqmtr.Text) / tot).ToString("0.000");

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




            {

                double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;
                double grandtotal = 0;
                double grandtotalamount = 0;

                for (int i = 0; i < gridsize.Rows.Count; i++)
                {
                    double total = 0;

                    //TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                    //TextBox txtRate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");



                    TextBox txts30fs = (TextBox)gridsize.Rows[i].FindControl("txts30fs");
                    F30 = F30 + Convert.ToDouble(txts30fs.Text);
                    TextBox txts30hs = (TextBox)gridsize.Rows[i].FindControl("txts30hs");
                    H30 = H30 + Convert.ToDouble(txts30hs.Text);

                    TextBox txts32fs = (TextBox)gridsize.Rows[i].FindControl("txts32fs");
                    F32 = F32 + Convert.ToDouble(txts32fs.Text);
                    TextBox txts32hs = (TextBox)gridsize.Rows[i].FindControl("txts32hs");
                    H32 = H32 + Convert.ToDouble(txts32hs.Text);

                    TextBox txts34fs = (TextBox)gridsize.Rows[i].FindControl("txts34fs");
                    F34 = F34 + Convert.ToDouble(txts34fs.Text);
                    TextBox txts34hs = (TextBox)gridsize.Rows[i].FindControl("txts34hs");
                    H34 = H34 + Convert.ToDouble(txts34hs.Text);

                    TextBox txts36fs = (TextBox)gridsize.Rows[i].FindControl("txts36fs");
                    F36 = F36 + Convert.ToDouble(txts36fs.Text);
                    TextBox txts36hs = (TextBox)gridsize.Rows[i].FindControl("txts36hs");
                    H36 = H36 + Convert.ToDouble(txts36hs.Text);

                    TextBox txtsxsfs = (TextBox)gridsize.Rows[i].FindControl("txtsxsfs");
                    FXS = FXS + Convert.ToDouble(txtsxsfs.Text);
                    TextBox txtsxshs = (TextBox)gridsize.Rows[i].FindControl("txtsxshs");
                    HXS = HXS + Convert.ToDouble(txtsxshs.Text);

                    TextBox txtssfs = (TextBox)gridsize.Rows[i].FindControl("txtssfs");
                    FS = FS + Convert.ToDouble(txtssfs.Text);
                    TextBox txtsshs = (TextBox)gridsize.Rows[i].FindControl("txtsshs");
                    HS = HS + Convert.ToDouble(txtsshs.Text);

                    TextBox txtsmfs = (TextBox)gridsize.Rows[i].FindControl("txtsmfs");
                    FM = FM + Convert.ToDouble(txtsmfs.Text);
                    TextBox txtsmhs = (TextBox)gridsize.Rows[i].FindControl("txtsmhs");
                    HM = HM + Convert.ToDouble(txtsmhs.Text);

                    TextBox txtslfs = (TextBox)gridsize.Rows[i].FindControl("txtslfs");
                    FL = FL + Convert.ToDouble(txtslfs.Text);
                    TextBox txtslhs = (TextBox)gridsize.Rows[i].FindControl("txtslhs");
                    HL = HL + Convert.ToDouble(txtslhs.Text);

                    TextBox txtsxlfs = (TextBox)gridsize.Rows[i].FindControl("txtsxlfs");
                    FXL = FXL + Convert.ToDouble(txtsxlfs.Text);
                    TextBox txtsxlhs = (TextBox)gridsize.Rows[i].FindControl("txtsxlhs");
                    HXL = HXL + Convert.ToDouble(txtsxlhs.Text);

                    TextBox txtsxxlfs = (TextBox)gridsize.Rows[i].FindControl("txtsxxlfs");
                    FXXL = FXXL + Convert.ToDouble(txtsxxlfs.Text);
                    TextBox txtsxxlhs = (TextBox)gridsize.Rows[i].FindControl("txtsxxlhs");
                    HXXL = HXXL + Convert.ToDouble(txtsxxlhs.Text);

                    TextBox txts3xlfs = (TextBox)gridsize.Rows[i].FindControl("txts3xlfs");
                    F3XL = F3XL + Convert.ToDouble(txts3xlfs.Text);
                    TextBox txts3xlhs = (TextBox)gridsize.Rows[i].FindControl("txts3xlhs");
                    H3XL = H3XL + Convert.ToDouble(txts3xlhs.Text);

                    TextBox txts4xlfs = (TextBox)gridsize.Rows[i].FindControl("txts4xlfs");
                    F4XL = F4XL + Convert.ToDouble(txts4xlfs.Text);
                    TextBox txts4xlhs = (TextBox)gridsize.Rows[i].FindControl("txts4xlhs");
                    H4XL = H4XL + Convert.ToDouble(txts4xlhs.Text);



                    grandtotal = grandtotal + Convert.ToDouble(txts30fs.Text) + Convert.ToDouble(txts30hs.Text) + Convert.ToDouble(txts32fs.Text) + Convert.ToDouble(txts32hs.Text) + Convert.ToDouble(txts34fs.Text) + Convert.ToDouble(txts34hs.Text) + Convert.ToDouble(txts36fs.Text) + Convert.ToDouble(txts36hs.Text) + Convert.ToDouble(txtsxsfs.Text) + Convert.ToDouble(txtsxshs.Text) + Convert.ToDouble(txtslfs.Text) + Convert.ToDouble(txtslhs.Text) + Convert.ToDouble(txtssfs.Text) + Convert.ToDouble(txtsshs.Text) + Convert.ToDouble(txtsmfs.Text) + Convert.ToDouble(txtsmhs.Text) + Convert.ToDouble(txtsxlfs.Text) + Convert.ToDouble(txtsxlhs.Text) + Convert.ToDouble(txtsxxlfs.Text) + Convert.ToDouble(txtsxxlhs.Text) + Convert.ToDouble(txts3xlfs.Text) + Convert.ToDouble(txts3xlhs.Text) + Convert.ToDouble(txts4xlfs.Text) + Convert.ToDouble(txts4xlhs.Text);
                    //txtsendFQty.Text = total.ToString();
                    //grandtotal = grandtotal + total;
                    //grandtotalamount = grandtotalamount + (Convert.ToDouble(txtsendFQty.Text) * Convert.ToDouble(txtRate.Text));

                    //if (txtRate.Text == "" || txtRate.Text == "0" || txtRate.Text == "0.0000")
                    //{
                    //    if (txtsendFQty.Text == "0")
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate in  " + Convert.ToInt32(i + 1) + " Row . Thank you!!');", true);
                    //        return;
                    //    }
                    //}
                }
                lb30f.Text = F30.ToString();
                lb32f.Text = F32.ToString();
                lb34f.Text = F34.ToString();
                lb36f.Text = F36.ToString();
                lbxsf.Text = FXS.ToString();
                lbsf.Text = FS.ToString();
                lbmf.Text = FM.ToString();
                lblf.Text = FL.ToString();
                lbxlf.Text = FXL.ToString();
                lbxxlf.Text = FXXL.ToString();
                lb3xlf.Text = F3XL.ToString();
                lb4xlf.Text = F4XL.ToString();


                lb30h.Text = H30.ToString();
                lb32h.Text = H32.ToString();
                lb34h.Text = H34.ToString();
                lb36h.Text = H36.ToString();
                lbxsh.Text = HXS.ToString();
                lbsh.Text = HS.ToString();
                lbmh.Text = HM.ToString();
                lblh.Text = HL.ToString();
                lbxlh.Text = HXL.ToString();
                lbxxlh.Text = HXXL.ToString();
                lb3xlh.Text = H3XL.ToString();
                lb4xlh.Text = H4XL.ToString();

                LabelTotal.Text = grandtotal.ToString();
                // txtAmount.Text = grandtotalamount.ToString("f2");
            }
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