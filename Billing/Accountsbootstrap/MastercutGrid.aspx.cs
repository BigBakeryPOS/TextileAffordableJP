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
    public partial class MastercutGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string superadmin = "";
        DataTable dCrt;
        double totfabr = 0;
        double totrawmat = 0;
        double totalacesscost = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            superadmin = Session["IsSuperAdmin"].ToString();
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                fabSave.Enabled = false;

                string super = Session["IsSuperAdmin"].ToString();

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

            DataSet ds = objBs.selectmastercutting(drpbranch.SelectedValue, ddljobworker.SelectedValue, FromDate, ToDate);
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

        protected void grid_sizerowcommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UPD")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gridsize.Rows[index];

                //Button b = (Button)row.FindControl("btnupdate");
                //b.Text = row.Cells[0].Text;


                double r = 0.00;
                double tot = 0;
                double halftot = 0;
                double fulltot = 0;
                double gndtot = 0.00;
                double gndmet = 0.00;
                //for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
                {

                    //FULL SLEEVE
                    double half = 0;
                    double full = 0;
                    Label TRansfabidlblno = (Label)gridsize.Rows[index].FindControl("lblno");
                    TextBox txtmasterid = (TextBox)gridsize.Rows[index].FindControl("txtno");
                    TextBox txtdesigno = (TextBox)gridsize.Rows[index].FindControl("txtdesigno");

                    DataSet dcheck = objBs.checkmastercuttingdetailscheck(txtmasterid.Text, TRansfabidlblno.Text, txtdesigno.Text);
                    if (dcheck.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not Allow To Update This color.Because This Color is in Stiching Process.Thank you!!!');", true);
                        mpefab.Show();
                        return;
                    }



                    Label lblid = (Label)gridsize.Rows[index].FindControl("lblid");

                    TextBox txt30fs = (TextBox)gridsize.Rows[index].FindControl("txts30fs");
                    TextBox txt32fs = (TextBox)gridsize.Rows[index].FindControl("txts32fs");
                    TextBox txt34fs = (TextBox)gridsize.Rows[index].FindControl("txts34fs");

                    TextBox txt36fs = (TextBox)gridsize.Rows[index].FindControl("txts36fs");
                    TextBox txtxsfs = (TextBox)gridsize.Rows[index].FindControl("txtsxsfs");
                    TextBox txtsfs = (TextBox)gridsize.Rows[index].FindControl("txtssfs");


                    TextBox txtmfs = (TextBox)gridsize.Rows[index].FindControl("txtsmfs");
                    TextBox txtlfs = (TextBox)gridsize.Rows[index].FindControl("txtslfs");
                    TextBox txtxlfs = (TextBox)gridsize.Rows[index].FindControl("txtsxlfs");

                    TextBox txtxxlfs = (TextBox)gridsize.Rows[index].FindControl("txtsxxlfs");
                    TextBox txt3xlfs = (TextBox)gridsize.Rows[index].FindControl("txts3xlfs");
                    TextBox txt4xlfs = (TextBox)gridsize.Rows[index].FindControl("txts4xlfs");

                    //HALF SLEEVE

                    TextBox txt30hs = (TextBox)gridsize.Rows[index].FindControl("txts30hs");
                    TextBox txt32hs = (TextBox)gridsize.Rows[index].FindControl("txts32hs");
                    TextBox txt34hs = (TextBox)gridsize.Rows[index].FindControl("txts34hs");

                    TextBox txt36hs = (TextBox)gridsize.Rows[index].FindControl("txts36hs");
                    TextBox txtxshs = (TextBox)gridsize.Rows[index].FindControl("txtsxshs");
                    TextBox txtshs = (TextBox)gridsize.Rows[index].FindControl("txtsshs");


                    TextBox txtmhs = (TextBox)gridsize.Rows[index].FindControl("txtsmhs");
                    TextBox txtlhs = (TextBox)gridsize.Rows[index].FindControl("txtslhs");
                    TextBox txtxlhs = (TextBox)gridsize.Rows[index].FindControl("txtsxlhs");

                    TextBox txtxxlhs = (TextBox)gridsize.Rows[index].FindControl("txtsxxlhs");
                    TextBox txt3xlhs = (TextBox)gridsize.Rows[index].FindControl("txts3xlhs");
                    TextBox txt4xlhs = (TextBox)gridsize.Rows[index].FindControl("txts4xlhs");



                    TextBox txtshirt = (TextBox)gridsize.Rows[index].FindControl("txtreqshirt");
                    TextBox txtfirid = (TextBox)gridsize.Rows[index].FindControl("txtfitid");
                    TextBox txttoal = (TextBox)gridsize.Rows[index].FindControl("txttotal");
                    TextBox txtdamage = (TextBox)gridsize.Rows[index].FindControl("txtdamage");
                    TextBox txtresona = (TextBox)gridsize.Rows[index].FindControl("txtnarration");
                    TextBox txtendbit = (TextBox)gridsize.Rows[index].FindControl("txtendbit");
                    DropDownList drpreason = (DropDownList)gridsize.Rows[index].FindControl("drpreason");

                    TextBox txtusedmeter = (TextBox)gridsize.Rows[index].FindControl("txtuedmter");
                    TextBox txtreqmtr = (TextBox)gridsize.Rows[index].FindControl("txteqrmeter");

                    TextBox txtavgwtgms = (TextBox)gridsize.Rows[index].FindControl("txtavgwtgms");

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

                    // int col = vLoop + 1;


                    if (txtdamage.Text == "0" || txtdamage.Text == "")
                    {
                        btneditfabdetails.Enabled = true;
                    }
                    else
                    {
                        //if (drpreason.SelectedValue == "3")
                        //{
                        if ((drpreason.SelectedValue == "4" || txtresona.Text == "") && ((gndtot > (Convert.ToDouble(txtshirt.Text))) || (gndtot < (Convert.ToDouble(txtshirt.Text)))))
                        // if (txtresona.Text == "")
                        {
                            if (drpreason.SelectedValue == "4")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('In damage Shirt Occurs.So Please Enter Reason For Damage Qty in Row " + index + ".Please Select Reason Type.Thank you!!!');", true);
                                btneditfabdetails.Enabled = false;
                                return;
                            }
                            else
                            {
                                btneditfabdetails.Enabled = true;

                            }
                        }

                        else
                        {
                            btneditfabdetails.Enabled = true;
                        }
                    }


                    int isucess = objBs.insertTransmastercutnewoneratio(lblid.Text, txt30fs.Text, txt30hs.Text, txt32fs.Text, txt32hs.Text, txt34fs.Text, txt34hs.Text, txt36fs.Text, txt36hs.Text, txtxsfs.Text, txtxshs.Text, txtsfs.Text, txtshs.Text, txtmfs.Text, txtmhs.Text, txtlfs.Text, txtlhs.Text, txtxlfs.Text, txtxlhs.Text, txtxxlfs.Text, txtxxlhs.Text, txt3xlfs.Text, txt3xlhs.Text, txt4xlfs.Text, txt4xlhs.Text, txttoal.Text, txtdamage.Text, txtresona.Text, drpreason.SelectedValue, txtavgwtgms.Text, TRansfabidlblno.Text, txtmasterid.Text, txtdesigno.Text);
                }



            }
            mpefab.Show();
        }

        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {
                DataSet ds = objBs.selectmastercutprocess(drpbranch.SelectedValue);
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
            Response.Redirect("../Accountsbootstrap/Mastercutting.aspx");

        }

        protected void Add1_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/Mastercutting.aspx");

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

            Response.Redirect("../Accountsbootstrap/Mastercutgrid.aspx");

        }

        protected void stocksave_cilck(object sender, EventArgs e)
        {




            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txtmasterid = (TextBox)gridsize.Rows[vLoop].FindControl("txtmasterid");
                TextBox txtcutid = (TextBox)gridsize.Rows[vLoop].FindControl("txtcutid");
                Label lbltransfabid = (Label)gridsize.Rows[vLoop].FindControl("lbltransfabid");
                Label lblbrandid = (Label)gridsize.Rows[vLoop].FindControl("lblbrandid");

                TextBox txtitemname = (TextBox)gridsize.Rows[vLoop].FindControl("txtitemname");
                TextBox txtdesignno = (TextBox)gridsize.Rows[vLoop].FindControl("txtdesignno");
                TextBox txtfitid = (TextBox)gridsize.Rows[vLoop].FindControl("txtfitid");
                TextBox txtfit = (TextBox)gridsize.Rows[vLoop].FindControl("txtfit");

                TextBox txtcompanyid = (TextBox)gridsize.Rows[vLoop].FindControl("txtcompanyid");
                TextBox txtcompanylotno = (TextBox)gridsize.Rows[vLoop].FindControl("txtcompanylotno");




                TextBox txts30fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts30fs");
                TextBox txts32fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts32fs");
                TextBox txts34fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts34fs");
                TextBox txts36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts36fs");
                TextBox txtsxsfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxsfs");
                TextBox txtssfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtssfs");
                TextBox txtsmfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsmfs");
                TextBox txtslfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtslfs");
                TextBox txtsxlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxlfs");
                TextBox txtsxxlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxxlfs");
                TextBox txts3xlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txts3xlfs");
                TextBox txts4xlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txts4xlfs");

                TextBox txts30hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts30hs");
                TextBox txts32hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts32hs");
                TextBox txts34hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts34hs");
                TextBox txts36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts36hs");
                TextBox txtsxshs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxshs");
                TextBox txtsshs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsshs");
                TextBox txtsmhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsmhs");
                TextBox txtslhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtslhs");
                TextBox txtsxlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxlhs");
                TextBox txtsxxlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxxlhs");
                TextBox txts3xlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txts3xlhs");
                TextBox txts4xlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txts4xlhs");


                TextBox txttotal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");


                int istockid = objBs.insertstockwisestock(txtmasterid.Text, txtcutid.Text, lbltransfabid.Text, txtdesignno.Text, lblbrandid.Text, txtfitid.Text, txtitemname.Text, txtcompanyid.Text, txtcompanylotno.Text, txttotal.Text, "0", txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "N", 0);

            }
            Response.Redirect("Mastercutgrid.aspx");

            //  int idue = objBs.fabricemaster(dstt, lblcutid.Text);
        }

        protected void stockprocesswise_click(object sender, EventArgs e)
        {
            int sgrandtot = 0;

            double GrandTotalqty = 0;

            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {
                TextBox txts30fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts30fs");
                TextBox txts32fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts32fs");
                TextBox txts34fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts34fs");
                TextBox txts36fs = (TextBox)gridsize.Rows[vLoop].FindControl("txts36fs");
                TextBox txtsxsfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxsfs");
                TextBox txtssfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtssfs");
                TextBox txtsmfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsmfs");
                TextBox txtslfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtslfs");
                TextBox txtsxlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxlfs");
                TextBox txtsxxlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxxlfs");
                TextBox txts3xlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txts3xlfs");
                TextBox txts4xlfs = (TextBox)gridsize.Rows[vLoop].FindControl("txts4xlfs");

                TextBox txts30hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts30hs");
                TextBox txts32hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts32hs");
                TextBox txts34hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts34hs");
                TextBox txts36hs = (TextBox)gridsize.Rows[vLoop].FindControl("txts36hs");
                TextBox txtsxshs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxshs");
                TextBox txtsshs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsshs");
                TextBox txtsmhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsmhs");
                TextBox txtslhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtslhs");
                TextBox txtsxlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxlhs");
                TextBox txtsxxlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txtsxxlhs");
                TextBox txts3xlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txts3xlhs");
                TextBox txts4xlhs = (TextBox)gridsize.Rows[vLoop].FindControl("txts4xlhs");


                TextBox txttotal = (TextBox)gridsize.Rows[vLoop].FindControl("txttotal");





                int stotot = Convert.ToInt32(txts30fs.Text) + Convert.ToInt32(txts32fs.Text) + Convert.ToInt32(txts34fs.Text) + Convert.ToInt32(txts36fs.Text) + Convert.ToInt32(txtsxsfs.Text) +
                    Convert.ToInt32(txtssfs.Text) + Convert.ToInt32(txtsmfs.Text) + Convert.ToInt32(txtslfs.Text) + Convert.ToInt32(txtsxlfs.Text) + Convert.ToInt32(txtsxxlfs.Text) +
                    Convert.ToInt32(txts3xlfs.Text) + Convert.ToInt32(txts4xlfs.Text) + Convert.ToInt32(txts30hs.Text) + Convert.ToInt32(txts32hs.Text) + Convert.ToInt32(txts34hs.Text) + Convert.ToInt32(txts36hs.Text) + Convert.ToInt32(txtsxshs.Text) +
                    Convert.ToInt32(txtsshs.Text) + Convert.ToInt32(txtsmhs.Text) + Convert.ToInt32(txtslhs.Text) + Convert.ToInt32(txtsxlhs.Text) + Convert.ToInt32(txtsxxlhs.Text) +
                    Convert.ToInt32(txts3xlhs.Text) + Convert.ToInt32(txts4xlhs.Text);

                sgrandtot = sgrandtot + stotot;

                txttotal.Text = stotot.ToString(); ;

                GrandTotalqty = GrandTotalqty + stotot;

            }

            gridsize.Caption = "TotalQty : " + GrandTotalqty.ToString();

            if (sgrandtot == Convert.ToInt32(lblstockgrandtot.Text))
            {
                btnsavecilck.Enabled = true;
            }
            else
            {
                mpe1.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Quantity Some thing Mismatch .Thank you!!!');", true);
                return;
            }

            mpe1.Show();
        }

        protected void newfabclicknew(object sender, EventArgs e)
        {
            double tot = 0;

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

                TextBox newtxtttreqmeter = (TextBox)newgridfablist.Rows[vLoop].FindControl("newtxtttreqmeter");
                if (endmeter.Text == "")
                {
                    endmeter.Text = "0";
                }

                reqmeter.Text = Convert.ToDouble(Convert.ToDouble(avaliablemeer.Text) - Convert.ToDouble(endmeter.Text)).ToString("f2");

                double totalmeter = Convert.ToDouble(reqmeter.Text) + Convert.ToDouble(endmeter.Text);

                string dummy = totalmeter.ToString("0.00");

                if (Convert.ToDouble(dummy) == Convert.ToDouble(avaliablemeer.Text))
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given End bit in greater Than that Given Meter in Row " + vLoop + ".Thank you!!!');", true);
                    return;
                }
                fabSave.Enabled = true;
                mpe.Show();

            }
        }


        protected void fabsaveclick(object sender, EventArgs e)
        {

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
            int idue = objBs.fabricemaster(dstt, lblcutid.Text);
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
        protected void btncostsave_OnClick(object sender, EventArgs e)
        {
            for (int i = 0; i < gvprocessaccesscost.Rows.Count; i++)
            {

                Label txtProcessType = (Label)gvprocessaccesscost.Rows[i].FindControl("txtProcessType");
                TextBox txtProcessCost = (TextBox)gvprocessaccesscost.Rows[i].FindControl("txtProcessCost");

                int costsave = objBs.savecost(Convert.ToInt32(lblmastercostid.Text), txtProcessType.Text, Convert.ToDouble(txtProcessCost.Text), lblllotno.Text);

            }
        }

        protected void btncostupdate_OnClick(object sender, EventArgs e)
        {
            int delcost = objBs.deletecost(Convert.ToInt32(lblmastercostid.Text));

            for (int i = 0; i < gvprocessaccesscost.Rows.Count; i++)
            {

                Label txtProcessType = (Label)gvprocessaccesscost.Rows[i].FindControl("txtProcessType");
                TextBox txtProcessCost = (TextBox)gvprocessaccesscost.Rows[i].FindControl("txtProcessCost");


                int costsave = objBs.savecost(Convert.ToInt32(lblmastercostid.Text), txtProcessType.Text, Convert.ToDouble(txtProcessCost.Text), lblllotno.Text);

            }
        }


        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Cuttingprocess.aspx?CuttingID=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
                int iSucess = objBs.deletemastercut(Convert.ToInt32(e.CommandArgument.ToString()));
                Response.Redirect("MastercutGrid.aspx");
            }
            else if (e.CommandName == "print")
            {
                Response.Redirect("Masterprintcutting.aspx?iCutID=" + e.CommandArgument.ToString());
            }

            else if (e.CommandName == "custprint")
            {
                Response.Redirect("PrintCuttingnewmaster.aspx?iCutID=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "damprint")
            {
                Response.Redirect("Damageprint.aspx?iCutID=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "EditC")
            {
                string fitid = "0";
                string ratio = "0";
                string brandid = "";
                string itemid = "";
                string cutid = "";
                DataSet ddata = new DataSet();
                ddata = objBs.getdataforlotnumberForupdate(e.CommandArgument.ToString());
                if (ddata.Tables[0].Rows.Count > 0)
                {
                    cutid = ddata.Tables[0].Rows[0]["cutid"].ToString();
                   brandid = ddata.Tables[0].Rows[0]["Brandid"].ToString();
                    fitid = ddata.Tables[0].Rows[0]["fit"].ToString();
                    ratio = ddata.Tables[0].Rows[0]["RatioWise"].ToString();
                    itemid = ddata.Tables[0].Rows[0]["itemid"].ToString();
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


                    DataSet dsizee = objBs.Getfitseize(fitid, brandid);
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
                dtrans = objBs.getdataforlotnumberfortranscutting(cutid);
                if (dtrans.Tables[0].Rows.Count > 0)
                {
                    gvcustomerorder.DataSource = dtrans;
                    gvcustomerorder.DataBind();
                }

                DataSet getmastercutdetails = objBs.getmastercutdetails(e.CommandArgument.ToString());
                if (getmastercutdetails.Tables[0].Rows.Count > 0)
                {
                    gridsize.DataSource = getmastercutdetails;
                    gridsize.DataBind();
                }

                mpefab.Show();
            }
            else if (e.CommandName == "PreCost")
            {

                newgridfablist.Visible = false;
                gridsize.Visible = true;
                GridView2.Visible = true;
                btnsavecilck.Enabled = false;
                gvfabriccost.Enabled = true;
                gridrawmaterial.Enabled = true;
                fabSave.Enabled = false;

                lblmastercostid.Text = e.CommandArgument.ToString();

                #region


                DataSet masterlot = objBs.getlotnoforprecost(Convert.ToInt32(e.CommandArgument.ToString()));
                lblllotno.Text = masterlot.Tables[0].Rows[0]["CompanyLotNo"].ToString();

                DataSet ds2345 = objBs.getusedfabricwithavgmeter(e.CommandArgument.ToString());
                if (ds2345.Tables[0].Rows.Count > 0)
                {

                    gvfabriccost.DataSource = ds2345;
                    gvfabriccost.DataBind();
                    mpecost.Show();
                }
                DataSet drawmater = objBs.getusedrawmaterials(e.CommandArgument.ToString());
                if (drawmater.Tables[0].Rows.Count > 0)
                {
                    gridrawmaterial.DataSource = drawmater;
                    gridrawmaterial.DataBind();

                    mpecost.Show();
                }
                else
                {
                    gridrawmaterial.DataSource = null;
                    gridrawmaterial.DataBind();
                }

                DataSet getval = objBs.getcostprocessval(e.CommandArgument.ToString());
                if (getval.Tables[0].Rows.Count > 0)
                {
                    btncostsave.Enabled = false;
                    btncostupdate.Enabled = true;

                    gvprocessaccesscost.DataSource = getval;
                    gvprocessaccesscost.DataBind();
                    mpecost.Show();
                }
                else
                {
                    btncostsave.Enabled = true;
                    btncostupdate.Enabled = false;


                    DataSet dsprocessval = objBs.getprocessval(e.CommandArgument.ToString());
                    if (dsprocessval.Tables[0].Rows.Count > 0)
                    {



                        DataSet ds;
                        DataTable dt;
                        DataRow drNew;
                        DataColumn dc;


                        ds = new DataSet();
                        dt = new DataTable();

                        dc = new DataColumn("Process");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Cost");
                        dt.Columns.Add(dc);
                        ds.Tables.Add(dt);

                        drNew = dt.NewRow();
                        drNew["Process"] = "FABRIC COST";
                        drNew["Cost"] = totfabr;
                        ds.Tables[0].Rows.Add(drNew);


                        for (int i = 0; i < dsprocessval.Tables[0].Rows.Count; i++)
                        {
                            drNew = dt.NewRow();
                            drNew["Process"] = dsprocessval.Tables[0].Rows[i]["ProcessType"].ToString();
                            drNew["Cost"] = dsprocessval.Tables[0].Rows[i]["Cost"].ToString();
                            ds.Tables[0].Rows.Add(drNew);
                        }

                        drNew = dt.NewRow();
                        drNew["Process"] = "TOTAL ACCESSORIES COST";
                        drNew["Cost"] = totrawmat;
                        ds.Tables[0].Rows.Add(drNew);

                        DataSet getmastercuttingrate = objBs.getlotnoforprecost(Convert.ToInt32(e.CommandArgument));
                        if (getmastercuttingrate.Tables[0].Rows.Count > 0)
                        {

                            drNew = dt.NewRow();
                            drNew["Process"] = "Cutting Master COST";
                            drNew["Cost"] = getmastercuttingrate.Tables[0].Rows[0]["Cuttingcost"].ToString();
                            ds.Tables[0].Rows.Add(drNew);
                        }

                        drNew = dt.NewRow();
                        drNew["Process"] = "Cutting/Transport";
                        drNew["Cost"] = 0;
                        ds.Tables[0].Rows.Add(drNew);

                        drNew = dt.NewRow();
                        drNew["Process"] = "Canvas/Fusing";
                        drNew["Cost"] = 0;
                        ds.Tables[0].Rows.Add(drNew);

                        drNew = dt.NewRow();
                        drNew["Process"] = "Miscellaneous";
                        drNew["Cost"] = 0;
                        ds.Tables[0].Rows.Add(drNew);


                        gvprocessaccesscost.DataSource = ds;
                        gvprocessaccesscost.DataBind();


                    }

                }
                #endregion
            }
            else if (e.CommandName == "fabprint")
            {
                newgridfablist.Visible = true;
                gridsize.Visible = false;
                GridView2.Visible = false;
                fabSave.Enabled = false;
                gvfabriccost.Visible = false;

                gridrawmaterial.Visible = false;


                DataSet trasncut = new DataSet();
                trasncut = objBs.gettrancutfab123(e.CommandArgument.ToString());
                lblcutid.Text = e.CommandArgument.ToString();
                if (trasncut.Tables[0].Rows.Count > 0)
                {
                    newgridfablist.DataSource = trasncut;
                    newgridfablist.DataBind();
                    mpe.Show();
                }
            }
            else if (e.CommandName == "stockratio")
            {


                newgridfablist.Visible = false;
                gridsize.Visible = true;
                GridView2.Visible = true;
                btnsavecilck.Enabled = false;
                gvfabriccost.Enabled = false;
                gridrawmaterial.Visible = false;
                string fitchnaged = string.Empty;

                DataSet dscheck = objBs.CheckMasterStockRatio(Convert.ToInt32(e.CommandArgument.ToString()));
                if (dscheck.Tables[0].Rows.Count > 0)
                {

                    btnstockprocess.Enabled = false;
                    btnsavecilck.Enabled = false;


                    DataSet ds23 = objBs.msatergettotalqtyCuttingprintreport(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (ds23.Tables[0].Rows.Count > 0)
                    {
                        #region

                        int grndtot = 0;

                        GridView2.DataSource = ds23;
                        GridView2.DataBind();

                        GridView2.Columns[1].Visible = false;
                        GridView2.Columns[2].Visible = false;

                        GridView2.Columns[3].Visible = false;
                        GridView2.Columns[4].Visible = false;

                        GridView2.Columns[5].Visible = false;
                        GridView2.Columns[6].Visible = false;

                        GridView2.Columns[7].Visible = false;
                        GridView2.Columns[8].Visible = false;

                        GridView2.Columns[9].Visible = false;
                        GridView2.Columns[10].Visible = false;

                        GridView2.Columns[11].Visible = false;
                        GridView2.Columns[12].Visible = false;

                        for (int j = 0; j < ds23.Tables[0].Rows.Count; j++)
                        {
                            #region
                            string S30 = ds23.Tables[0].Rows[j]["s30"].ToString();
                            string S32 = ds23.Tables[0].Rows[j]["s32"].ToString();
                            string S34 = ds23.Tables[0].Rows[j]["s34"].ToString();
                            string S36 = ds23.Tables[0].Rows[j]["s36"].ToString();
                            string SXS = ds23.Tables[0].Rows[j]["sxs"].ToString();
                            string SS = ds23.Tables[0].Rows[j]["ss"].ToString();
                            string SM = ds23.Tables[0].Rows[j]["sm"].ToString();
                            string SL = ds23.Tables[0].Rows[j]["sl"].ToString();
                            string SXL = ds23.Tables[0].Rows[j]["sxl"].ToString();
                            string SXXL = ds23.Tables[0].Rows[j]["sxxl"].ToString();
                            string S3XL = ds23.Tables[0].Rows[j]["s3xl"].ToString();
                            string S4XL = ds23.Tables[0].Rows[j]["s4xl"].ToString();
                            int tot = Convert.ToInt32(ds23.Tables[0].Rows[j]["tot"]);

                            grndtot = grndtot + tot;
                            lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GridView2.Columns[1].Visible = true;
                            }
                            else
                            {

                            }

                            if (S32 != "0")
                            {

                                GridView2.Columns[2].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GridView2.Columns[3].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GridView2.Columns[4].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GridView2.Columns[5].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GridView2.Columns[6].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GridView2.Columns[7].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GridView2.Columns[8].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GridView2.Columns[9].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GridView2.Columns[10].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GridView2.Columns[11].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GridView2.Columns[12].Visible = true;
                            }

                            #endregion

                        }

                        #endregion
                    }

                    #region

                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();

                        dt.Columns.Add("Masterid");
                        dt.Columns.Add("cutid");
                        dt.Columns.Add("Transfabid");
                        dt.Columns.Add("brandid");
                        dt.Columns.Add("Itemname");
                        dt.Columns.Add("Fitid");
                        dt.Columns.Add("Fit");
                        dt.Columns.Add("designno");
                        dt.Columns.Add("companyid");
                        dt.Columns.Add("companylotno");

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

                        dt.Columns.Add("Total");


                        ViewState["Data"] = dt;

                        double grandFinishRowTtl = 0;

                        for (int i = 0; i < dscheck.Tables[0].Rows.Count; i++)
                        {
                            double FinishRowTtl = 0;

                            dCrt = (DataTable)ViewState["Data"];


                            {
                                #region
                                DataRow dr = dCrt.NewRow();

                                dr["Masterid"] = "";
                                dr["cutid"] = "";
                                dr["Transfabid"] = "";
                                dr["brandid"] = "";
                                dr["Itemname"] = dscheck.Tables[0].Rows[i]["Itemname"].ToString();
                                dr["Fitid"] = "";
                                dr["designno"] = dscheck.Tables[0].Rows[i]["DesignCode"].ToString();
                                dr["Fit"] = dscheck.Tables[0].Rows[i]["Fits"].ToString();
                                dr["companyid"] = "";
                                dr["companylotno"] = "";


                                dr["S30FS"] = dscheck.Tables[0].Rows[i]["30FS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["30FS"].ToString());
                                dr["S30HS"] = dscheck.Tables[0].Rows[i]["30HS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["30HS"].ToString());
                                dr["S32FS"] = dscheck.Tables[0].Rows[i]["32FS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["32FS"].ToString());
                                dr["S32HS"] = dscheck.Tables[0].Rows[i]["32HS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["32HS"].ToString());
                                dr["S34FS"] = dscheck.Tables[0].Rows[i]["34FS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["34FS"].ToString());
                                dr["S34HS"] = dscheck.Tables[0].Rows[i]["34HS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["34HS"].ToString());
                                dr["S36FS"] = dscheck.Tables[0].Rows[i]["36FS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["36FS"].ToString());
                                dr["S36HS"] = dscheck.Tables[0].Rows[i]["36HS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["36HS"].ToString());
                                dr["SXSFS"] = dscheck.Tables[0].Rows[i]["XSFS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["XSFS"].ToString());
                                dr["SXSHS"] = dscheck.Tables[0].Rows[i]["XSHS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["XSHS"].ToString());
                                dr["SLFS"] = dscheck.Tables[0].Rows[i]["LFS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["LFS"].ToString());
                                dr["SLHS"] = dscheck.Tables[0].Rows[i]["LHS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["LHS"].ToString());
                                dr["SXLFS"] = dscheck.Tables[0].Rows[i]["XLFS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["XLFS"].ToString());
                                dr["SXLHS"] = dscheck.Tables[0].Rows[i]["XLHS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["XLHS"].ToString());
                                dr["SXXLFS"] = dscheck.Tables[0].Rows[i]["XXLFS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["XXLFS"].ToString());
                                dr["SXXLHS"] = dscheck.Tables[0].Rows[i]["XXLHS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["XXLHS"].ToString());
                                dr["S3XLFS"] = dscheck.Tables[0].Rows[i]["3XLFS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["3XLFS"].ToString());
                                dr["S3XLHS"] = dscheck.Tables[0].Rows[i]["3XLHS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["3XLHS"].ToString());
                                dr["S4XLFS"] = dscheck.Tables[0].Rows[i]["4XLFS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["4XLFS"].ToString());
                                dr["S4XLHS"] = dscheck.Tables[0].Rows[i]["4XLHS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["4XLHS"].ToString());
                                dr["SSFS"] = dscheck.Tables[0].Rows[i]["SFS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["SFS"].ToString());
                                dr["SSHS"] = dscheck.Tables[0].Rows[i]["SHS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["SHS"].ToString());
                                dr["SMFS"] = dscheck.Tables[0].Rows[i]["MFS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["MFS"].ToString());
                                dr["SMHS"] = dscheck.Tables[0].Rows[i]["MHS"].ToString();
                                FinishRowTtl = FinishRowTtl + Convert.ToDouble(dscheck.Tables[0].Rows[i]["MHS"].ToString());

                                dr["Total"] = FinishRowTtl;
                                dCrt.Rows.Add(dr);

                                grandFinishRowTtl = grandFinishRowTtl + FinishRowTtl;
                                #endregion
                            }

                        }

                        gridsize.Caption = "Total Qty : " + grandFinishRowTtl;
                        gridsize.DataSource = dCrt;
                        gridsize.DataBind();
                        mpe1.Show();

                    }


                    #endregion


                    gridsize.Columns[4].Visible = false;
                    gridsize.Columns[5].Visible = false;
                    gridsize.Columns[6].Visible = false;
                    gridsize.Columns[7].Visible = false;
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


                    for (int j = 0; j < dscheck.Tables[0].Rows.Count; j++)
                    {
                        #region

                        string S30 = dscheck.Tables[0].Rows[j]["30fs"].ToString();
                        string S32 = dscheck.Tables[0].Rows[j]["32fs"].ToString();
                        string S34 = dscheck.Tables[0].Rows[j]["34fs"].ToString();
                        string S36 = dscheck.Tables[0].Rows[j]["36fs"].ToString();
                        string SXS = dscheck.Tables[0].Rows[j]["xsfs"].ToString();
                        string SS = dscheck.Tables[0].Rows[j]["sfs"].ToString();
                        string SM = dscheck.Tables[0].Rows[j]["mfs"].ToString();
                        string SL = dscheck.Tables[0].Rows[j]["lfs"].ToString();
                        string SXL = dscheck.Tables[0].Rows[j]["xlfs"].ToString();
                        string SXXL = dscheck.Tables[0].Rows[j]["xxlfs"].ToString();
                        string S3XL = dscheck.Tables[0].Rows[j]["3xlfs"].ToString();
                        string S4XL = dscheck.Tables[0].Rows[j]["4xlfs"].ToString();

                        string hS30 = dscheck.Tables[0].Rows[j]["30hs"].ToString();
                        string hS32 = dscheck.Tables[0].Rows[j]["32hs"].ToString();
                        string hS34 = dscheck.Tables[0].Rows[j]["34hs"].ToString();
                        string hS36 = dscheck.Tables[0].Rows[j]["36hs"].ToString();
                        string hSXS = dscheck.Tables[0].Rows[j]["xshs"].ToString();
                        string hSS = dscheck.Tables[0].Rows[j]["shs"].ToString();
                        string hSM = dscheck.Tables[0].Rows[j]["mhs"].ToString();
                        string hSL = dscheck.Tables[0].Rows[j]["lhs"].ToString();
                        string hSXL = dscheck.Tables[0].Rows[j]["xlhs"].ToString();
                        string hSXXL = dscheck.Tables[0].Rows[j]["xxlhs"].ToString();
                        string hS3XL = dscheck.Tables[0].Rows[j]["3xlhs"].ToString();
                        string hS4XL = dscheck.Tables[0].Rows[j]["4xlhs"].ToString();


                        if (S30 != "0")
                        {

                            gridsize.Columns[4].Visible = true;
                        }
                        if (S32 != "0")
                        {

                            gridsize.Columns[5].Visible = true;
                        }

                        if (S34 != "0")
                        {

                            gridsize.Columns[6].Visible = true;
                        }

                        if (S36 != "0")
                        {

                            gridsize.Columns[7].Visible = true;
                        }

                        if (SXS != "0")
                        {

                            gridsize.Columns[8].Visible = true;
                        }

                        if (SS != "0")
                        {

                            gridsize.Columns[9].Visible = true;
                        }

                        if (SM != "0")
                        {

                            gridsize.Columns[10].Visible = true;
                        }

                        if (SL != "0")
                        {

                            gridsize.Columns[11].Visible = true;
                        }

                        if (SXL != "0")
                        {

                            gridsize.Columns[12].Visible = true;
                        }

                        if (SXXL != "0")
                        {

                            gridsize.Columns[13].Visible = true;
                        }

                        if (S3XL != "0")
                        {

                            gridsize.Columns[14].Visible = true;
                        }

                        if (S4XL != "0")
                        {

                            gridsize.Columns[15].Visible = true;
                        }

                        if (hS30 != "0")
                        {

                            gridsize.Columns[16].Visible = true;
                        }

                        if (hS32 != "0")
                        {

                            gridsize.Columns[17].Visible = true;
                        }

                        if (hS34 != "0")
                        {

                            gridsize.Columns[18].Visible = true;
                        }

                        if (hS36 != "0")
                        {

                            gridsize.Columns[19].Visible = true;
                        }

                        if (hSXS != "0")
                        {

                            gridsize.Columns[20].Visible = true;
                        }

                        if (hSS != "0")
                        {

                            gridsize.Columns[21].Visible = true;
                        }

                        if (hSM != "0")
                        {

                            gridsize.Columns[22].Visible = true;
                        }

                        if (hSL != "0")
                        {

                            gridsize.Columns[23].Visible = true;
                        }

                        if (hSXL != "0")
                        {

                            gridsize.Columns[24].Visible = true;
                        }

                        if (hSXXL != "0")
                        {

                            gridsize.Columns[25].Visible = true;
                        }

                        if (hS3XL != "0")
                        {

                            gridsize.Columns[26].Visible = true;
                        }

                        if (hS4XL != "0")
                        {

                            gridsize.Columns[27].Visible = true;
                        }

                        #endregion

                    }
                }
                else
                {
                    DataSet ds23 = objBs.msatergettotalqtyCuttingprintreport(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (ds23.Tables[0].Rows.Count > 0)
                    {
                        btnstockprocess.Enabled = true;

                        #region

                        int grndtot = 0;

                        GridView2.DataSource = ds23;
                        GridView2.DataBind();

                        GridView2.Columns[1].Visible = false;
                        GridView2.Columns[2].Visible = false;

                        GridView2.Columns[3].Visible = false;
                        GridView2.Columns[4].Visible = false;

                        GridView2.Columns[5].Visible = false;
                        GridView2.Columns[6].Visible = false;

                        GridView2.Columns[7].Visible = false;
                        GridView2.Columns[8].Visible = false;

                        GridView2.Columns[9].Visible = false;
                        GridView2.Columns[10].Visible = false;

                        GridView2.Columns[11].Visible = false;
                        GridView2.Columns[12].Visible = false;

                        for (int j = 0; j < ds23.Tables[0].Rows.Count; j++)
                        {
                            #region
                            string S30 = ds23.Tables[0].Rows[j]["s30"].ToString();
                            string S32 = ds23.Tables[0].Rows[j]["s32"].ToString();
                            string S34 = ds23.Tables[0].Rows[j]["s34"].ToString();
                            string S36 = ds23.Tables[0].Rows[j]["s36"].ToString();
                            string SXS = ds23.Tables[0].Rows[j]["sxs"].ToString();
                            string SS = ds23.Tables[0].Rows[j]["ss"].ToString();
                            string SM = ds23.Tables[0].Rows[j]["sm"].ToString();
                            string SL = ds23.Tables[0].Rows[j]["sl"].ToString();
                            string SXL = ds23.Tables[0].Rows[j]["sxl"].ToString();
                            string SXXL = ds23.Tables[0].Rows[j]["sxxl"].ToString();
                            string S3XL = ds23.Tables[0].Rows[j]["s3xl"].ToString();
                            string S4XL = ds23.Tables[0].Rows[j]["s4xl"].ToString();
                            int tot = Convert.ToInt32(ds23.Tables[0].Rows[j]["tot"]);

                            grndtot = grndtot + tot;
                            lblstockgrandtot.Text = grndtot.ToString();

                            if (S30 != "0")
                            {

                                GridView2.Columns[1].Visible = true;
                            }
                            else
                            {

                            }

                            if (S32 != "0")
                            {

                                GridView2.Columns[2].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                GridView2.Columns[3].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                GridView2.Columns[4].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                GridView2.Columns[5].Visible = true;
                            }

                            if (SS != "0")
                            {

                                GridView2.Columns[6].Visible = true;
                            }

                            if (SM != "0")
                            {

                                GridView2.Columns[7].Visible = true;
                            }

                            if (SL != "0")
                            {

                                GridView2.Columns[8].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                GridView2.Columns[9].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                GridView2.Columns[10].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                GridView2.Columns[11].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                GridView2.Columns[12].Visible = true;
                            }

                            #endregion

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

                        DataSet dcolour = objBs.stocksplitcolurwise(e.CommandArgument.ToString());
                        if (dcolour.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = new DataTable();

                            dt.Columns.Add("Masterid");
                            dt.Columns.Add("cutid");
                            dt.Columns.Add("Transfabid");
                            dt.Columns.Add("brandid");
                            dt.Columns.Add("Itemname");
                            dt.Columns.Add("Fitid");
                            dt.Columns.Add("Fit");
                            dt.Columns.Add("designno");
                            dt.Columns.Add("companyid");
                            dt.Columns.Add("companylotno");

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

                            dt.Columns.Add("Total");


                            ViewState["Data"] = dt;


                            for (int i = 0; i < dcolour.Tables[0].Rows.Count; i++)
                            {
                                dCrt = (DataTable)ViewState["Data"];


                                #region
                                {
                                    DataRow dr = dCrt.NewRow();

                                    dr["Masterid"] = dcolour.Tables[0].Rows[i]["Masterid"].ToString();
                                    dr["cutid"] = dcolour.Tables[0].Rows[i]["cutid"].ToString();
                                    dr["Transfabid"] = dcolour.Tables[0].Rows[i]["Transfabid"].ToString();
                                    dr["brandid"] = dcolour.Tables[0].Rows[i]["brandid"].ToString();
                                    dr["Itemname"] = dcolour.Tables[0].Rows[i]["Itemname"].ToString();
                                    dr["Fitid"] = dcolour.Tables[0].Rows[i]["Fitid"].ToString();
                                    dr["designno"] = dcolour.Tables[0].Rows[i]["designno"].ToString();
                                    fitchnaged = dcolour.Tables[0].Rows[i]["Fitid"].ToString();
                                    dr["Fit"] = dcolour.Tables[0].Rows[i]["Fit"].ToString();

                                    dr["companyid"] = dcolour.Tables[0].Rows[i]["companyid"].ToString();
                                    dr["companylotno"] = dcolour.Tables[0].Rows[i]["companylotno"].ToString();


                                    dr["S30FS"] = "0";
                                    dr["S30HS"] = "0";

                                    dr["S32FS"] = "0";
                                    dr["S32HS"] = "0";

                                    dr["S34FS"] = "0";
                                    dr["S34HS"] = "0";
                                    dr["S36FS"] = "0";
                                    dr["S36HS"] = "0";
                                    dr["SXSFS"] = "0";
                                    dr["SXSHS"] = "0";
                                    dr["SLFS"] = "0";
                                    dr["SLHS"] = "0";
                                    dr["SXLFS"] = "0";
                                    dr["SXLHS"] = "0";
                                    dr["SXXLFS"] = "0";
                                    dr["SXXLHS"] = "0";
                                    dr["S3XLFS"] = "0";
                                    dr["S3XLHS"] = "0";
                                    dr["S4XLFS"] = "0";
                                    dr["S4XLHS"] = "0";
                                    dr["SSFS"] = "0";
                                    dr["SSHS"] = "0";
                                    dr["SMFS"] = "0";
                                    dr["SMHS"] = "0";

                                    dr["Total"] = "0";
                                    dCrt.Rows.Add(dr);


                                    DataSet dsizee = objBs.Getfitseize(fitchnaged);
                                    if ((dsizee.Tables[0].Rows.Count > 0))
                                    {
                                        //Select the checkboxlist items those values are true in database
                                        //Loop through the DataTable
                                        for (int ii = 0; ii <= dsizee.Tables[0].Rows.Count - 1; ii++)
                                        {
                                            //You need to change this as per your DB Design
                                            string size = dsizee.Tables[0].Rows[ii]["Sizeid1"].ToString();



                                            //if (size == "39FS" || size == "39HS" || size == "44FS" || size == "44HS")
                                            //{
                                            //}
                                            //else
                                            {
                                                //Find the checkbox list items using FindByValue and select it.
                                                chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[ii]["Sizeid1"].ToString()).Selected = true;
                                            }

                                        }
                                    }
                                }

                                #endregion


                            }






                            if (chkSizes.SelectedIndex >= 0)
                            {

                                #region
                                gridsize.Columns[4].Visible = false; //30FS
                                gridsize.Columns[5].Visible = false; //32FS

                                gridsize.Columns[6].Visible = false;//34Fs
                                gridsize.Columns[7].Visible = false;//36Fs

                                gridsize.Columns[8].Visible = false; //XSFS
                                gridsize.Columns[9].Visible = false; //SFS

                                gridsize.Columns[10].Visible = false; //MFS
                                gridsize.Columns[11].Visible = false; //LFS

                                gridsize.Columns[12].Visible = false; //XLFS
                                gridsize.Columns[13].Visible = false; //XXLFS

                                gridsize.Columns[14].Visible = false; //3XLFS
                                gridsize.Columns[15].Visible = false; //4XLFS

                                gridsize.Columns[16].Visible = false; //30HS
                                gridsize.Columns[17].Visible = false; //32HS

                                gridsize.Columns[18].Visible = false; //34Hs
                                gridsize.Columns[19].Visible = false; //36Hs

                                gridsize.Columns[20].Visible = false; //XSHS
                                gridsize.Columns[21].Visible = false; //SHS


                                gridsize.Columns[22].Visible = false;  //MHS
                                gridsize.Columns[23].Visible = false;  //LHS


                                gridsize.Columns[24].Visible = false; //XLHS
                                gridsize.Columns[25].Visible = false; //XXLHS

                                gridsize.Columns[26].Visible = false; //3XLHS
                                gridsize.Columns[27].Visible = false; //4XLHS



                                int lop = 0;
                                //Loop through each item of checkboxlist
                                foreach (ListItem item in chkSizes.Items)
                                {
                                    //check if item selected

                                    if (item.Selected)
                                    {

                                        {
                                            if (item.Value == "1") // 30FS
                                            {

                                                gridsize.Columns[4].Visible = true;
                                            }
                                            if (item.Value == "2") // 30HS
                                            {
                                                gridsize.Columns[16].Visible = true;
                                            }
                                            if (item.Value == "3") // 32FS
                                            {
                                                gridsize.Columns[5].Visible = true;
                                            }
                                            if (item.Value == "4") //32HS
                                            {
                                                gridsize.Columns[17].Visible = true;
                                            }
                                            if (item.Value == "5") //34FS
                                            {
                                                gridsize.Columns[6].Visible = true;
                                            }
                                            if (item.Value == "6") //34HS
                                            {
                                                gridsize.Columns[18].Visible = true;
                                            }
                                            if (item.Value == "7") //36FS
                                            {
                                                gridsize.Columns[7].Visible = true;
                                            }
                                            if (item.Value == "8") //36HS
                                            {
                                                gridsize.Columns[19].Visible = true;
                                            }
                                            if (item.Value == "9") //XSFS
                                            {
                                                gridsize.Columns[8].Visible = true;
                                            }
                                            if (item.Value == "10") //XSHS
                                            {
                                                gridsize.Columns[20].Visible = true;
                                            }
                                            if (item.Value == "11") // LFS
                                            {
                                                gridsize.Columns[11].Visible = true;
                                            }
                                            if (item.Value == "12") // LHS
                                            {
                                                gridsize.Columns[23].Visible = true;
                                            }
                                            if (item.Value == "13") //XLFS
                                            {
                                                gridsize.Columns[12].Visible = true;
                                            }
                                            if (item.Value == "14") //XLHS
                                            {
                                                gridsize.Columns[24].Visible = true;
                                            }
                                            if (item.Value == "15") // XXLFS
                                            {
                                                gridsize.Columns[13].Visible = true;
                                            }
                                            if (item.Value == "16") // XXLHS
                                            {
                                                gridsize.Columns[25].Visible = true;
                                            }
                                            if (item.Value == "17") //3XLFS
                                            {
                                                gridsize.Columns[14].Visible = true;
                                            }
                                            if (item.Value == "18") // 3XLHS
                                            {
                                                gridsize.Columns[26].Visible = true;
                                            }
                                            if (item.Value == "19") // 4XLFS
                                            {
                                                gridsize.Columns[15].Visible = true;
                                            }
                                            if (item.Value == "20") // 4XLHS
                                            {
                                                gridsize.Columns[27].Visible = true;
                                            }
                                            if (item.Value == "21") //SFS
                                            {
                                                gridsize.Columns[9].Visible = true;
                                            }
                                            if (item.Value == "22") //SHS
                                            {
                                                gridsize.Columns[21].Visible = true;
                                            }
                                            if (item.Value == "23") // MFS
                                            {
                                                gridsize.Columns[10].Visible = true;
                                            }
                                            if (item.Value == "24") // MHS
                                            {
                                                gridsize.Columns[22].Visible = true;
                                            }


                                            lop++;

                                        }
                                    }
                                }
                                //gvcustomerorder.DataSource = dssmer;
                                //gvcustomerorder.DataBind();

                                #endregion
                            }
                            else
                            {
                                #region

                                gridsize.Columns[4].Visible = false; //30FS
                                gridsize.Columns[5].Visible = false; //32FS

                                gridsize.Columns[6].Visible = false;//34Fs
                                gridsize.Columns[7].Visible = false;//36Fs

                                gridsize.Columns[8].Visible = false; //XSFS
                                gridsize.Columns[9].Visible = false; //SFS

                                gridsize.Columns[10].Visible = false; //MFS
                                gridsize.Columns[11].Visible = false; //LFS

                                gridsize.Columns[12].Visible = false; //XLFS
                                gridsize.Columns[13].Visible = false; //XXLFS

                                gridsize.Columns[14].Visible = false; //3XLFS
                                gridsize.Columns[15].Visible = false; //4XLFS

                                gridsize.Columns[16].Visible = false; //30HS
                                gridsize.Columns[17].Visible = false; //32HS

                                gridsize.Columns[18].Visible = false; //34Hs
                                gridsize.Columns[19].Visible = false; //36Hs

                                gridsize.Columns[20].Visible = false; //XSHS
                                gridsize.Columns[21].Visible = false; //SHS


                                gridsize.Columns[22].Visible = false;  //MHS
                                gridsize.Columns[23].Visible = false;  //LHS


                                gridsize.Columns[24].Visible = false; //XLHS
                                gridsize.Columns[25].Visible = false; //XXLHS

                                gridsize.Columns[26].Visible = false; //3XLHS
                                gridsize.Columns[27].Visible = false; //4XLHS

                                #endregion
                            }


                        }

                        #endregion

                        gridsize.DataSource = dCrt;
                        gridsize.DataBind();
                        mpe1.Show();

                    }
                }
            }


        }

        protected void editmasterdetails(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0;
            double halftot = 0;
            double fulltot = 0;
            double gndtot = 0.00;
            double gndmet = 0.00;
            for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            {

                //FULL SLEEVE
                double half = 0;
                double full = 0;

                Label lblid = (Label)gridsize.Rows[vLoop].FindControl("lblid");

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

                int col = vLoop + 1;


                if (txtdamage.Text == "0" || txtdamage.Text == "")
                {
                    btneditfabdetails.Enabled = true;
                }
                else
                {
                    //if (drpreason.SelectedValue == "3")
                    //{
                    if ((drpreason.SelectedValue == "4" || txtresona.Text == "") && ((gndtot > (Convert.ToDouble(txtshirt.Text))) || (gndtot < (Convert.ToDouble(txtshirt.Text)))))
                    // if (txtresona.Text == "")
                    {
                        if (drpreason.SelectedValue == "4")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('In damage Shirt Occurs.So Please Enter Reason For Damage Qty in Row " + col + ".Please Select Reason Type.Thank you!!!');", true);
                            btneditfabdetails.Enabled = false;
                            return;
                        }
                        else
                        {
                            btneditfabdetails.Enabled = true;

                        }
                    }

                    else
                    {
                        btneditfabdetails.Enabled = true;
                    }
                }


                int isucess = objBs.insertTransmastercutnewoneratio(lblid.Text, txt30fs.Text, txt30hs.Text, txt32fs.Text, txt32hs.Text, txt34fs.Text, txt34hs.Text, txt36fs.Text, txt36hs.Text, txtxsfs.Text, txtxshs.Text, txtsfs.Text, txtshs.Text, txtmfs.Text, txtmhs.Text, txtlfs.Text, txtlhs.Text, txtxlfs.Text, txtxlhs.Text, txtxxlfs.Text, txtxxlhs.Text, txt3xlfs.Text, txt3xlhs.Text, txt4xlfs.Text, txt4xlhs.Text, txttoal.Text, txtdamage.Text, txtresona.Text, drpreason.SelectedValue, txtavgwtgms.Text, "", "", "");
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
               
                int col = vLoop + 1;

              
                if (txtdamage.Text == "0" || txtdamage.Text == "")
                {
                    btneditfabdetails.Enabled = true;
                    mpefab.Show();
                }
                else
                {
                    //if (drpreason.SelectedValue == "3")
                    //{
                    if ((drpreason.SelectedValue == "4" || txtresona.Text == "") && ((gndtot > (Convert.ToDouble(txtshirt.Text))) || (gndtot < (Convert.ToDouble(txtshirt.Text)))))
                    // if (txtresona.Text == "")
                    {
                        if (drpreason.SelectedValue == "4")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('In damage Shirt Occurs.So Please Enter Reason For Damage Qty in Row " + col + ".Please Select Reason Type.Thank you!!!');", true);
                            btneditfabdetails.Enabled = false;
                            mpefab.Show();
                            return;
                        }
                        else
                        {
                            btneditfabdetails.Enabled = true;
                            mpefab.Show();

                        }
                    }

                    else
                    {
                        btneditfabdetails.Enabled = true;
                        mpefab.Show();
                    }
                }
            }

            mpefab.Show();
          
        }

        protected void Gridrawmaterial_rowdatabound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totrawmat = totrawmat + Convert.ToDouble(e.Row.Cells[2].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = totrawmat.ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void gvprocessaccesscost_rowdatabound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //totalacesscost = totalacesscost + Convert.ToDouble(e.Row.Cells[1].Text);
                totalacesscost += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Cost"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total :";
                e.Row.Cells[1].Text = totalacesscost.ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        protected void gvfabriccost_rowbound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totfabr = totfabr + Convert.ToDouble(e.Row.Cells[6].Text);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //  e.Row.Cells[6].Text = "Total:";
                e.Row.Cells[6].Text = totfabr.ToString();
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                // lblfabriccost.Text = totfabr.ToString();
            }
        }


        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string cutid = e.Row.Cells[4].Text;

                GridView gv = e.Row.FindControl("gvfabfetails") as GridView;

                DataSet ds = objBs.getfabdetailsforcutting(cutid);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }


                if (superadmin == "1")
                {
                    if (objBs.chkprocessformaster(cutid))
                    {

                        ((Image)e.Row.FindControl("img")).Visible = false;
                        ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;


                        ((Image)e.Row.FindControl("dlt")).Visible = false;
                        ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = true;

                        ((LinkButton)e.Row.FindControl("btnprecost")).Visible = true;

                    }
                    else
                    {

                        ((Image)e.Row.FindControl("img")).Visible = true;
                        ((ImageButton)e.Row.FindControl("imgdisable")).Visible = false;


                        ((Image)e.Row.FindControl("dlt")).Visible = true;
                        ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = false;

                        ((LinkButton)e.Row.FindControl("btnprecost")).Visible = true;
                    }
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("btnprecost")).Visible = false;
                }

            }
        }
    }
}