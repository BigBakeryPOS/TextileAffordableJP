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
    public partial class NewCutProcessEdit : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iid;
        string Sfit= string.Empty;
        DataSet dmerge1 = new DataSet();

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

              
            //    string date = DateTime.Now.ToString("dd/MM/yyyy");

                // txtdate.Text = date;

                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();


                if (iid != null)
                {
                    DataSet ds1 = objBs.getCuttingProcess(Convert.ToInt32(iid));
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            txtID.Text = ds1.Tables[0].Rows[0]["cutid"].ToString();
                            txtLotNo.Text = ds1.Tables[0].Rows[0]["LotNo"].ToString();
                            txtdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");
                            drpwidth.SelectedValue = ds1.Tables[0].Rows[0]["width"].ToString();
                            drpcutting.SelectedValue = ds1.Tables[0].Rows[0]["Cuttingmaster"].ToString();
                            txtprod.Text = ds1.Tables[0].Rows[0]["productcost"].ToString();
                            string rad = ds1.Tables[0].Rows[0]["t"].ToString();
                            string fit = ds1.Tables[0].Rows[0]["dfit"].ToString();
                            ViewState["Fit"] = fit;
                            if (rad == "Single")
                            {
                                radbtn.SelectedValue = "1";
                            }
                            else
                            {
                                radbtn.SelectedValue = "2";
                            }

                            DataTable dttt;
                            DataRow drNew;
                            DataColumn dct;
                            DataSet dstd = new DataSet();
                            dttt = new DataTable();

                            dct = new DataColumn("Transid");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Invrefno");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("designno");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("brand");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("brandid");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("partyname");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("ledgername");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Rate");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("totalmeter");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("reqmeter");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("avgsize");
                            dttt.Columns.Add(dct);

                           // dct = new DataColumn("Actmeter");
                          //  dttt.Columns.Add(dct);

                         //   dct = new DataColumn("actsize");
                         //   dttt.Columns.Add(dct);

                            dct = new DataColumn("reqshirt");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("shirt");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("dfit");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Fit");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("TSFS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("TSHS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("TEFS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("TEHS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("TNFS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("TNHS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("FZFS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("FZHS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("FTFS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("FTHS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("FFFS");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("FFHS");
                            dttt.Columns.Add(dct);

                            //dct = new DataColumn("Total");
                            //dttt.Columns.Add(dct);



                            dstd.Tables.Add(dttt);

                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                drNew = dttt.NewRow();
                                drNew["Transid"] = dr["Transid"];
                                drNew["Invrefno"] = dr["Invrefno"];
                                drNew["designno"] = dr["Designno"];
                                drNew["partyname"] = dr["partyname"];
                                drNew["brand"] = dr["brandname"];
                                drNew["brandid"] = dr["brandid"];
                                drNew["ledgername"] = dr["ledgername"];
                                drNew["Rate"] = dr["Rate"];
                                drNew["totalmeter"] = dr["totalmeter"];
                                drNew["reqmeter"] = dr["reqmeter"];
                                drNew["avgsize"] = dr["avgsize"];
                             //   drNew["Actmeter"] = dr["Actmeter"];
                                //drNew["Actsize"] = dr["Actsize"];
                                drNew["reqshirt"] = dr["reqshirt"];
                                drNew["shirt"] = dr["totalshirt"];
                                drNew["dfit"] = dr["dfit"];
                                drNew["Fit"] = dr["Fit"];
                                drNew["TSFS"] = dr["36FS"];
                                drNew["TSHS"] = dr["36hs"];
                                drNew["TEFS"] = dr["38fs"];
                                drNew["TEHS"] = dr["38hs"];
                                drNew["TNFS"] = dr["39fs"];
                                drNew["TNHS"] = dr["39hs"];
                                drNew["FZFS"] = dr["40fs"];
                                drNew["FZHS"] = dr["40hs"];
                                drNew["FTFS"] = dr["42fs"];
                                drNew["FTHS"] = dr["42hs"];
                                drNew["FFFS"] = dr["44fs"];
                                drNew["FFHS"] = dr["44hs"];
                                //drNew["Total"] = dr["avgsize"];

                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            ViewState["CurrentTable1"] = dttt;

                            if (fit == "3")
                            {
                                Panelcasual.Visible = false;
                                Panelformal.Visible = true;
                                Panelboys.Visible = false;
                                gvcustomerorder.DataSource = dstd;
                                gvcustomerorder.DataBind();


                                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                                {
                                    Label lbltransid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lbltransid");
                                    Label lblinvrefno = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblinvre");
                                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesigno");
                                    TextBox txtpartyid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtledgerid");
                                    TextBox txtpartyname = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtparty");
                                    TextBox txtbrandid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbrandid");
                                    TextBox txtbrandname = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtbrand");
                                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmet");

                                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");

                                    TextBox txtActmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtactmeter");
                                    TextBox txtactsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtactsize");

                                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                                    TextBox txtshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtshirt");
                                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                                    lbltransid.Text = dstd.Tables[0].Rows[vLoop]["Transid"].ToString();
                                    lblinvrefno.Text = dstd.Tables[0].Rows[vLoop]["Invrefno"].ToString();
                                    txtdesign.Text = dstd.Tables[0].Rows[vLoop]["designno"].ToString();
                                    txtpartyid.Text = dstd.Tables[0].Rows[vLoop]["partyname"].ToString();
                                    txtpartyname.Text = dstd.Tables[0].Rows[vLoop]["ledgername"].ToString();
                                    txtbrandid.Text = dstd.Tables[0].Rows[vLoop]["brandid"].ToString();
                                    txtbrandname.Text = dstd.Tables[0].Rows[vLoop]["brand"].ToString();

                                    txtrate.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Rate"]).ToString("N");
                                    txtmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["totalmeter"]).ToString("N");

                                    txtreqmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["reqmeter"]).ToString("N");
                                    txtavgsize.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["avgsize"]).ToString("N");
                                    txtreqshirt.Text = dstd.Tables[0].Rows[vLoop]["reqshirt"].ToString();
                                    txtshirt.Text = dstd.Tables[0].Rows[vLoop]["shirt"].ToString();
                                    txtfitid.Text = dstd.Tables[0].Rows[vLoop]["dfit"].ToString();
                                    txtfit.Text = dstd.Tables[0].Rows[vLoop]["Fit"].ToString();
                                    txt36fs.Text = dstd.Tables[0].Rows[vLoop]["TSFS"].ToString();
                                    txt38fs.Text = dstd.Tables[0].Rows[vLoop]["TEFS"].ToString();
                                    txt39fs.Text = dstd.Tables[0].Rows[vLoop]["TNFS"].ToString();
                                    txt40fs.Text = dstd.Tables[0].Rows[vLoop]["FZFS"].ToString();
                                    txt42fs.Text = dstd.Tables[0].Rows[vLoop]["FTFS"].ToString();
                                    txt44fs.Text = dstd.Tables[0].Rows[vLoop]["FFFS"].ToString();
                                    txt36hs.Text = dstd.Tables[0].Rows[vLoop]["TSHS"].ToString();
                                    txt38hs.Text = dstd.Tables[0].Rows[vLoop]["TEHS"].ToString();
                                    txt39hs.Text = dstd.Tables[0].Rows[vLoop]["TNHS"].ToString();
                                    txt40hs.Text = dstd.Tables[0].Rows[vLoop]["FZHS"].ToString();
                                    txt42hs.Text = dstd.Tables[0].Rows[vLoop]["FTHS"].ToString();

                                    txt44hs.Text = dstd.Tables[0].Rows[vLoop]["FFHS"].ToString();

                                }
                            }
                            else if (fit == "4")
                            {
                                Panelcasual.Visible = false;
                                Panelformal.Visible = false;
                                Panelboys.Visible = true;

                                GridView2.DataSource = dstd;
                                GridView2.DataBind();


                                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                                {
                                    Label lbltransid = (Label)GridView2.Rows[vLoop].FindControl("lbltransid");
                                    Label lblinvrefno = (Label)GridView2.Rows[vLoop].FindControl("lblinvre");
                                    TextBox txtdesign = (TextBox)GridView2.Rows[vLoop].FindControl("txtdesigno");
                                    TextBox txtpartyid = (TextBox)GridView2.Rows[vLoop].FindControl("txtledgerid");
                                    TextBox txtpartyname = (TextBox)GridView2.Rows[vLoop].FindControl("txtparty");
                                    TextBox txtbrandid = (TextBox)GridView2.Rows[vLoop].FindControl("txtbrandid");
                                    TextBox txtbrandname = (TextBox)GridView2.Rows[vLoop].FindControl("txtbrand");
                                    TextBox txtrate = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                                    TextBox txtmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txtmet");

                                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");

                                    TextBox txtActmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txtactmeter");
                                    TextBox txtactsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtactsize");

                                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                                    TextBox txtshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtshirt");
                                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                                    TextBox txt39fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                                    TextBox txt40fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                                    TextBox txt42fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                                    TextBox txt44fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                                    TextBox txt39hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                                    TextBox txt40hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                                    TextBox txt42hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                                    TextBox txt44hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                                    lbltransid.Text = dstd.Tables[0].Rows[vLoop]["Transid"].ToString();
                                    lblinvrefno.Text = dstd.Tables[0].Rows[vLoop]["Invrefno"].ToString();
                                    txtdesign.Text = dstd.Tables[0].Rows[vLoop]["designno"].ToString();
                                    txtpartyid.Text = dstd.Tables[0].Rows[vLoop]["partyname"].ToString();
                                    txtpartyname.Text = dstd.Tables[0].Rows[vLoop]["ledgername"].ToString();
                                    txtbrandid.Text = dstd.Tables[0].Rows[vLoop]["brandid"].ToString();
                                    txtbrandname.Text = dstd.Tables[0].Rows[vLoop]["brand"].ToString();

                                    txtrate.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Rate"]).ToString("N");
                                    txtmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["totalmeter"]).ToString("N");

                                    txtreqmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["reqmeter"]).ToString("N");
                                    txtavgsize.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["avgsize"]).ToString("N");
                                    txtreqshirt.Text = dstd.Tables[0].Rows[vLoop]["reqshirt"].ToString();
                                    txtshirt.Text = dstd.Tables[0].Rows[vLoop]["shirt"].ToString();
                                    txtfitid.Text = dstd.Tables[0].Rows[vLoop]["dfit"].ToString();
                                    txtfit.Text = dstd.Tables[0].Rows[vLoop]["Fit"].ToString();
                                    txt36fs.Text = dstd.Tables[0].Rows[vLoop]["TSFS"].ToString();
                                    txt38fs.Text = dstd.Tables[0].Rows[vLoop]["TEFS"].ToString();
                                    txt39fs.Text = dstd.Tables[0].Rows[vLoop]["TNFS"].ToString();
                                    txt40fs.Text = dstd.Tables[0].Rows[vLoop]["FZFS"].ToString();
                                    txt42fs.Text = dstd.Tables[0].Rows[vLoop]["FTFS"].ToString();
                                    txt44fs.Text = dstd.Tables[0].Rows[vLoop]["FFFS"].ToString();
                                    txt36hs.Text = dstd.Tables[0].Rows[vLoop]["TSHS"].ToString();
                                    txt38hs.Text = dstd.Tables[0].Rows[vLoop]["TEHS"].ToString();
                                    txt39hs.Text = dstd.Tables[0].Rows[vLoop]["TNHS"].ToString();
                                    txt40hs.Text = dstd.Tables[0].Rows[vLoop]["FZHS"].ToString();
                                    txt42hs.Text = dstd.Tables[0].Rows[vLoop]["FTHS"].ToString();

                                    txt44hs.Text = dstd.Tables[0].Rows[vLoop]["FFHS"].ToString();

                                }
                            }
                            else if (fit == "5")
                            {

                                Panelcasual.Visible = true;
                                Panelformal.Visible = false;
                                Panelboys.Visible = false;
                                GridView1.DataSource = dstd;
                                GridView1.DataBind();


                                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                                {
                                    Label lbltransid = (Label)GridView1.Rows[vLoop].FindControl("lbltransid");
                                    Label lblinvrefno = (Label)GridView1.Rows[vLoop].FindControl("lblinvre");
                                    TextBox txtdesign = (TextBox)GridView1.Rows[vLoop].FindControl("txtdesigno");
                                    TextBox txtpartyid = (TextBox)GridView1.Rows[vLoop].FindControl("txtledgerid");
                                    TextBox txtpartyname = (TextBox)GridView1.Rows[vLoop].FindControl("txtparty");
                                    TextBox txtbrandid = (TextBox)GridView1.Rows[vLoop].FindControl("txtbrandid");
                                    TextBox txtbrandname = (TextBox)GridView1.Rows[vLoop].FindControl("txtbrand");
                                    TextBox txtrate = (TextBox)GridView1.Rows[vLoop].FindControl("txtRate");
                                    TextBox txtmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txtmet");

                                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");

                                    TextBox txtActmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txtactmeter");
                                    TextBox txtactsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtactsize");

                                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                                    TextBox txtshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtshirt");
                                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                                    TextBox txt36fs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                                    TextBox txt38fs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                                    TextBox txt39fs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                                    TextBox txt40fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                                    TextBox txt42fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                                    TextBox txt36hs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                                    TextBox txt38hs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                                    TextBox txt39hs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                                    TextBox txt40hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                                    TextBox txt42hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                                    lbltransid.Text = dstd.Tables[0].Rows[vLoop]["Transid"].ToString();
                                    lblinvrefno.Text = dstd.Tables[0].Rows[vLoop]["Invrefno"].ToString();
                                    txtdesign.Text = dstd.Tables[0].Rows[vLoop]["designno"].ToString();
                                    txtpartyid.Text = dstd.Tables[0].Rows[vLoop]["partyname"].ToString();
                                    txtpartyname.Text = dstd.Tables[0].Rows[vLoop]["ledgername"].ToString();
                                    txtbrandid.Text = dstd.Tables[0].Rows[vLoop]["brandid"].ToString();
                                    txtbrandname.Text = dstd.Tables[0].Rows[vLoop]["brand"].ToString();

                                    txtrate.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Rate"]).ToString("N");
                                    txtmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["totalmeter"]).ToString("N");

                                    txtreqmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["reqmeter"]).ToString("N");
                                    txtavgsize.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["avgsize"]).ToString("N");
                                    txtreqshirt.Text = dstd.Tables[0].Rows[vLoop]["reqshirt"].ToString();
                                    txtshirt.Text = dstd.Tables[0].Rows[vLoop]["shirt"].ToString();
                                    txtfitid.Text = dstd.Tables[0].Rows[vLoop]["dfit"].ToString();
                                    txtfit.Text = dstd.Tables[0].Rows[vLoop]["Fit"].ToString();
                                    txt36fs.Text = dstd.Tables[0].Rows[vLoop]["TSFS"].ToString();
                                    txt38fs.Text = dstd.Tables[0].Rows[vLoop]["TEFS"].ToString();
                                    txt39fs.Text = dstd.Tables[0].Rows[vLoop]["TNFS"].ToString();
                                    txt40fs.Text = dstd.Tables[0].Rows[vLoop]["FZFS"].ToString();
                                    txt42fs.Text = dstd.Tables[0].Rows[vLoop]["FTFS"].ToString();
                                    txt44fs.Text = dstd.Tables[0].Rows[vLoop]["FFFS"].ToString();
                                    txt36hs.Text = dstd.Tables[0].Rows[vLoop]["TSHS"].ToString();
                                    txt38hs.Text = dstd.Tables[0].Rows[vLoop]["TEHS"].ToString();
                                    txt39hs.Text = dstd.Tables[0].Rows[vLoop]["TNHS"].ToString();
                                    txt40hs.Text = dstd.Tables[0].Rows[vLoop]["FZHS"].ToString();
                                    txt42hs.Text = dstd.Tables[0].Rows[vLoop]["FTHS"].ToString();

                                    txt44hs.Text = dstd.Tables[0].Rows[vLoop]["FFHS"].ToString();

                                }
                            }


                            ////DataTable dt = new DataTable();
                            ////divcode.Visible = false;

                            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            //{

                            //}


                        }
                    }
                }
                else
                {
                    txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //DataSet ds = objBs.CuttingID();
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    if (ds.Tables[0].Rows[0]["CuttingID"].ToString() == "")
                    //        TextBox3.Text = "1";
                    //    else
                    //        TextBox3.Text = ds.Tables[0].Rows[0]["CuttingID"].ToString();
                    DataSet dss = objBs.getmaaxBillnoforcut("");
                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        txtLotNo.Text = dss.Tables[0].Rows[0]["billId"].ToString();
                    }
                    btnadd.Text = "Save";
                    btnadd.Enabled = false;
                    radchecked(sender, e);
                    //  //  FirstGridViewRow();
                    //}

                }
            }
        }
        protected void ckhsize_index(object sender, EventArgs e)
        {
            
        }

        protected void call_Click(object sender, EventArgs e)
        {
           
        }

        protected void ddrpartyselected_changed(object sender, EventArgs e)
        {
           


        }

        protected void Sddrpartyselected_changed(object sender, EventArgs e)
        {
           

        }

        protected void Add_Click(object sender, EventArgs e)
        {

            //string ledgerr = string.Empty;
            //string mainlab = string.Empty;
            //string party = string.Empty;
            //string maar = string.Empty;
            //string mmrrpp = string.Empty;
            //string ddffiitt = string.Empty;

            //bool fitlab = false;
            //bool washlab = false;
            //bool logolab = false;
            //string partyname = string.Empty;
            ////string Mode = Request.QueryString.Get("Mode");
            ////DataSet dcalculate = new DataSet();

            ////btnadd.Enabled = false;
            ////string width = string.Empty;

            //DateTime deliverydate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //if (btnadd.Text == "Save")
            //{
            //    if (drpcutting.SelectedValue == "Select Cutting Name")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Cutting Name. Thank you!!');", true);
            //        return;
            //    }
            //    else
            //    {

            //    }

            //    int istas = objBs.updatesizesettingg(drpwidth.SelectedItem.Text, txtsharp.Text, txtexec.Text);


            //    dCrt = (DataTable)ViewState["Data"];
            //    DataSet ds = new DataSet();
            //    ds.Tables.Add(dCrt);

            //    int iStatus = 0;

            //    iStatus = objBs.insertcutnewone(txtLotNo.Text, deliverydate, drpwidth.SelectedValue, drpcutting.SelectedValue, txtprod.Text, "0", "0", radbtn.SelectedValue);

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

            //            int iStatus2 = objBs.insertTranscutnewone(txtLotNo.Text, "0", trainid, design, party, totmeter, reqmeter, rate, tsfs, tshs, tefs, tehs, tnfs, tnhs, fzfs, fzhs, ftfs, fths, fffs, ffhs, shirt, reqshirt, fitid, ledgerr, mainlab, fitlab, washlab, logolab, maar, mmrrpp, extra, ddffiitt, avgsize);

            //        }
            //    }
            //}
          
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


        }

        protected void dddldesignchanged(object sender, EventArgs e)
        {
           

        }
        protected void reqchanged(object sender, EventArgs e)
        {
            //double r = 0.00;
            //double rr = 0.00;
            //double rb = 0.00;
            //double rr1 = 0.00;
            //double rb1 = 0.00;
            //DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //if (dcalculate.Tables[0].Rows.Count > 0)
            //{

            //    // double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

            //    double wid = 0;
            //    if (drpFit.SelectedValue == "3")
            //    {
            //        wid = Convert.ToDouble(txtsharp.Text);
            //    }
            //    else
            //    {
            //        wid = Convert.ToDouble(txtexec.Text);
            //    }

            //    double roundoff = Convert.ToDouble(txtReqMtr.Text) / wid;
            //    if (roundoff > 0.5)
            //    {
            //        r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        r = Math.Floor(Convert.ToDouble(roundoff));
            //    }

            //}
            ////  txtNoofShirts.Text = r.ToString();
            //txtReqNoShirts.Text = r.ToString();

            //rr = ((r * 15) / 100);
            //if (rr > 0.5)
            //{
            //    rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
            //}
            //else
            //{
            //    rb = Math.Floor(Convert.ToDouble(rr));
            //}
            //txtextrashirt.Text = rb.ToString();

            //rr1 = ((r * 2) / 100);
            //if (rr1 > 0.5)
            //{
            //    rb1 = Math.Round(Convert.ToDouble(rr1), MidpointRounding.AwayFromZero);
            //}
            //else
            //{
            //    rb1 = Math.Floor(Convert.ToDouble(rr1));
            //}
            //txtminshirt.Text = rb1.ToString();


            //if (radbtn.SelectedValue == "1")
            //{
            //    txtavamet1.Text = txtReqMtr.Text;
            //    Sddrpartyselected_changed(sender, e);
            //}

        }

        protected void drpfitchanged(object sender, EventArgs e)
        {
            //double r = 0.00;
            //double r1 = 0.00;

            //double rr = 0.00;
            //double rb = 0.00;
            //DataSet dteo = objBs.getcutlistdesignfortrans(dddldesign.SelectedValue);
            //if (dteo.Tables[0].Rows.Count > 0)
            //{
            //    txtDesignRate.Text = dteo.Tables[0].Rows[0]["rat"].ToString();
            //    txtAvailableMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
            //    if (txtReqMtr.Text == "")
            //    {
            //        txtReqMtr.Text = dteo.Tables[0].Rows[0]["met"].ToString();
            //    }
            //    else
            //    {

            //    }

            //    DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //    if (dcalculate.Tables[0].Rows.Count > 0)
            //    {

            //        //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

            //        double wid = 0;
            //        if (drpFit.SelectedValue == "3")
            //        {
            //            wid = Convert.ToDouble(txtsharp.Text);
            //        }
            //        else
            //        {
            //            wid = Convert.ToDouble(txtexec.Text);
            //        }

            //        double roundoff = Convert.ToDouble(txtAvailableMtr.Text) / wid;
            //        double roundoff1 = Convert.ToDouble(txtReqMtr.Text) / wid;
            //        if (roundoff > 0.5)
            //        {
            //            r = Math.Round(Convert.ToDouble(roundoff), MidpointRounding.AwayFromZero);
            //        }
            //        else
            //        {
            //            r = Math.Floor(Convert.ToDouble(roundoff));
            //        }

            //        if (roundoff1 > 0.5)
            //        {
            //            r1 = Math.Round(Convert.ToDouble(roundoff1), MidpointRounding.AwayFromZero);
            //        }
            //        else
            //        {
            //            r1 = Math.Floor(Convert.ToDouble(roundoff1));
            //        }

            //    }
            //    txtNoofShirts.Text = r.ToString();
            //    txtReqNoShirts.Text = r1.ToString();
            //}
            //rr = ((r * 15) / 100);
            //if (rr > 0.5)
            //{
            //    rb = Math.Round(Convert.ToDouble(rr), MidpointRounding.AwayFromZero);
            //}
            //else
            //{
            //    rb = Math.Floor(Convert.ToDouble(rr));
            //}
            //txtextrashirt.Text = rb.ToString();
        }
        protected void chkinvnochanged(object sender, EventArgs e)
        {

           

        }

       

        protected void chkgridview(object sender, EventArgs e)
        {

           
        }


        protected void check2_changed(object sender, EventArgs e)
        {

           
        }

        protected void ddlDNo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void Sfitchaged(object sender, EventArgs e)
        {
           

        }

        protected void supplierfill(object sender, EventArgs e)
        {
            
        }

        protected void radchecked(object sender, EventArgs e)
        {
            
        }

        private void FirstGridViewRow()
        {
           

        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gridsize_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void Grdcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               

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

           
        }

        private void SetRowData()
        {
           
        }

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {

          
        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
           

        }

        private void AddNewRow()
        {
            
        }

        protected void reqmeter(object sender, EventArgs e)
        {
            
        }

        protected void rdSingle_CheckedChanged(object sender, EventArgs e)
        {

           
        }

        protected void rdMultiple_CheckedChanged(object sender, EventArgs e)
        {
           
        }
        protected void Recalclick(object sender, EventArgs e)
        {
           
        }
        protected void callcclick(object sender, EventArgs e)
        {

           

        }

        protected void processclick(object sender, EventArgs e)
        {
            //double tot = 0.00;
            //double tot2 = 0.00;
            //double tot3 = 0.00;
            //double r = 0.00;
            //double tooo = 0.00;
            //double gndtot = 0.00;
            //double gndmet = 0.00;

            //string ledgerr = string.Empty;
            //string mainlab = string.Empty;

            //bool fitlab = false;
            //bool washlab = false;
            //bool logolab = false;


            //if (radbtn.SelectedValue == "1")
            //{
            //    // double tot = 0.00;

            //    //  double r = 0.00;

            //    tot = Convert.ToDouble(txt36FS.Text) + Convert.ToDouble(txt38FS.Text) + Convert.ToDouble(txt39FS.Text) + Convert.ToDouble(txt40FS.Text) + Convert.ToDouble(txt42FS.Text) + Convert.ToDouble(txt44FS.Text) +
            //    Convert.ToDouble(txt36HS.Text) + Convert.ToDouble(txt38HS.Text) + Convert.ToDouble(txt39HS.Text) + Convert.ToDouble(txt40HS.Text) + Convert.ToDouble(txt42HS.Text) + Convert.ToDouble(txt44HS.Text);



            //    gndtot = gndtot + tot;



            //    DataSet dcalculate = objBs.getsizeforcutt(drpFit.SelectedValue, drpwidth.SelectedItem.Text);
            //    if (dcalculate.Tables[0].Rows.Count > 0)
            //    {

            //        //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

            //        double wid = 0;
            //        if (drpFit.SelectedValue == "3")
            //        {
            //            wid = Convert.ToDouble(txtsharp.Text);
            //        }
            //        else
            //        {
            //            wid = Convert.ToDouble(txtexec.Text);
            //        }

            //        double roundoff = Convert.ToDouble(tot) * wid;
                 
            //        r = roundoff;

            //        //  txtavamet1.Text = r.ToString();
            //        txttotshirt1.Text = tot.ToString();
            //        //   gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            //    }

            //    txt38FS.Focus();
            //    txtremashirt.Text = (Convert.ToDouble(txtReqNoShirts.Text) - gndtot).ToString();
            //    txtremameter.Text = (Convert.ToDouble(txtReqMtr.Text) - r).ToString("0.00");

            //    btnprocess.Enabled = true;
            //    btnadd.Enabled = true;

               



            //}
            //else
            //{
            //    btnprocess.Enabled = true;
            //    btnadd.Enabled = true;

            //    for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            //    {
                    
            //        TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
            //        TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
            //        if (txtavggsize.Text == "0" || txtavggsize.Text == "")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please click Calc Button.Thank You!!!');", true);
            //            btnadd.Enabled = false;
            //            btnprocess.Enabled = false;
            //            return;
            //        }
            //        if (txtreqmeter.Text == "0" || txtreqmeter.Text == "")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please click Calc Button.Thank you!!!');", true);
            //            btnadd.Enabled = false;
            //            btnprocess.Enabled = false;
            //            return;

            //        }
                   

            //    }
               
            //}
          
            //decimal dAmt = 0; decimal dTotal = 0;
            //dCrt = (DataTable)ViewState["Data"];
            //if (dCrt.Rows.Count == 0)
            //{
            //    if (tr1.Visible == true)
            //    {
            //        if (drpCustomer.SelectedValue == "Select Party Name")
            //        {
            //        }
            //        else
            //        {
            //            DataRow dr = dCrt.NewRow();

            //            dr["Transid"] = dddldesign.SelectedValue;
            //            dr["Design"] = dddldesign.SelectedItem.Text;
            //            dr["Rate"] = txtDesignRate.Text;

            //            dr["meter"] = txtAvailableMtr.Text;
            //            dr["Shirt"] = txtNoofShirts.Text;
            //            dr["reqmeter"] = txtavamet1.Text;

            //            dr["reqshirt"] = txttotshirt1.Text;
            //            dr["ledgerid"] = drpCustomer.SelectedValue;
            //            dr["party"] = drpCustomer.SelectedItem.Text;
            //            dr["Fitid"] = drpFit.SelectedValue;
            //            dr["Fit"] = drpFit.SelectedItem.Text;

            //            dr["TSFS"] = txt36FS.Text;
            //            dr["TSHS"] = txt36HS.Text;

            //            dr["TEFS"] = txt38FS.Text;
            //            dr["TEHS"] = txt38HS.Text;

            //            dr["TNFS"] = txt39FS.Text;
            //            dr["TNHS"] = txt39HS.Text;

            //            dr["FZFS"] = txt40FS.Text;
            //            dr["FZHS"] = txt40HS.Text;

            //            dr["FTFS"] = txt42FS.Text;
            //            dr["FTHS"] = txt42HS.Text;

            //            dr["FFFS"] = txt44FS.Text;
            //            dr["FFHS"] = txt44HS.Text;

            //            dr["avgsize"] = txtavvgmeter.Text;

            //            dr["WSP"] = Stxtwsp.Text;
            //            dr["Extra"] = txtextrashirt.Text;

            //            if (radbtn.SelectedValue == "1")
            //            {
            //                ledgerr = ddlSupplier.SelectedValue;
            //                mainlab = drplab.SelectedValue;
            //                fitlab = chkfit.Checked;
            //                washlab = Chkwash.Checked;
            //                logolab = Chllogo.Checked;
            //            }

            //            dr["LLedger"] = ledgerr;
            //            dr["Mainlab"] = mainlab;
            //            dr["FItLab"] = fitlab;
            //            dr["Washlab"] = washlab;
            //            dr["Logolab"] = logolab;



            //            dCrt.Rows.Add(dr);
            //        }
            //    }
            //    else
            //    {
            //        for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            //        {
            //            DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

            //            DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

            //            TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
            //            TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
            //            TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
            //            TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
            //            TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
            //            TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

            //            TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
            //            TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
            //            TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
            //            TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
            //            TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
            //            TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

            //            TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
            //            TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
            //            TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
            //            TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

            //            if (dfit.SelectedValue == "Select Fit")
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
            //                //dr["TNFS"] = txt39FS.Text;
            //                //dr["TNHS"] = txt39HS.Text;

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

            //    }

               

            //}
            //else
            //{
            //    if (tr1.Visible == true)
            //    {
            //        if (drpCustomer.SelectedValue == "Select Party Name")
            //        {
            //        }
            //        else
            //        {
            //            DataRow dr = dCrt.NewRow();

            //            dr["Transid"] = dddldesign.SelectedValue;
            //            dr["Design"] = dddldesign.SelectedItem.Text;
            //            dr["Rate"] = txtDesignRate.Text;

            //            dr["meter"] = txtAvailableMtr.Text;
            //            dr["Shirt"] = txtNoofShirts.Text;
            //            dr["reqmeter"] = txtavamet1.Text;

            //            dr["reqshirt"] = txttotshirt1.Text;
            //            dr["ledgerid"] = drpCustomer.SelectedValue;
            //            dr["party"] = drpCustomer.SelectedItem.Text;
            //            dr["Fitid"] = drpFit.SelectedValue;
            //            dr["Fit"] = drpFit.SelectedItem.Text;

            //            dr["TSFS"] = txt36FS.Text;
            //            dr["TSHS"] = txt36HS.Text;

            //            dr["TEFS"] = txt38FS.Text;
            //            dr["TEHS"] = txt38HS.Text;

            //            dr["TNFS"] = txt39FS.Text;
            //            dr["TNHS"] = txt39HS.Text;

            //            dr["FZFS"] = txt40FS.Text;
            //            dr["FZHS"] = txt40HS.Text;

            //            dr["FTFS"] = txt42FS.Text;
            //            dr["FTHS"] = txt42HS.Text;

            //            dr["FFFS"] = txt44FS.Text;
            //            dr["FFHS"] = txt44HS.Text;

            //            dr["WSP"] = Stxtwsp.Text;
            //            dr["avgsize"] = txtavvgmeter.Text;
            //            dr["Extra"] = txtextrashirt.Text;

            //            if (radbtn.SelectedValue == "1")
            //            {
            //                ledgerr = ddlSupplier.SelectedValue;
            //                mainlab = drplab.SelectedValue;
            //                fitlab = chkfit.Checked;
            //                washlab = Chkwash.Checked;
            //                logolab = Chllogo.Checked;
            //            }
            //            else
            //            {
                           
            //            }
            //            dr["LLedger"] = ledgerr;
            //            dr["Mainlab"] = mainlab;
            //            dr["FItLab"] = fitlab;
            //            dr["Washlab"] = washlab;
            //            dr["Logolab"] = logolab;



            //            dCrt.Rows.Add(dr);
            //        }
            //    }
            //    else
            //    {
            //        for (int vLoop = 0; vLoop < gridsize.Rows.Count; vLoop++)
            //        {
            //            DropDownList dparty = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrparty");

            //            DropDownList dfit = (DropDownList)gridsize.Rows[vLoop].FindControl("ddrpfit");

            //            TextBox txt36fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttsfs");
            //            TextBox txt38fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttefs");
            //            TextBox txt39fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnfs");
            //            TextBox txt40fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzfs");
            //            TextBox txt42fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtftfs");
            //            TextBox txt44fs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfffs");

            //            TextBox txt36hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttshs");
            //            TextBox txt38hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttehs");
            //            TextBox txt39hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxttnhs");
            //            TextBox txt40hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfzhs");
            //            TextBox txt42hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtfths");
            //            TextBox txt44hs = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtffhs");

            //            TextBox txtwsp = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtwsp");
            //            TextBox txtavggsize = (TextBox)gridsize.Rows[vLoop].FindControl("avgsize");
            //            TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
            //            TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");


            //            DataRow dr = dCrt.NewRow();

            //            dr["Transid"] = dddldesign.SelectedValue;
            //            dr["Design"] = dddldesign.SelectedItem.Text;
            //            dr["Rate"] = txtDesignRate.Text;

            //            dr["meter"] = txtAvailableMtr.Text;
            //            dr["Shirt"] = txtNoofShirts.Text;
            //            dr["reqmeter"] = txtreqmeter.Text;

            //            dr["reqshirt"] = txtshirt.Text;
            //            dr["ledgerid"] = dparty.SelectedValue;
            //            dr["party"] = dparty.SelectedItem.Text;
            //            dr["Fitid"] = dfit.SelectedValue;
            //            dr["Fit"] = dfit.SelectedItem.Text;

            //            dr["TSFS"] = txt36fs.Text;
            //            dr["TSHS"] = txt36hs.Text;

            //            dr["TEFS"] = txt38fs.Text;
            //            dr["TEHS"] = txt38hs.Text;

            //            dr["TNFS"] = txt39fs.Text;
            //            dr["TNHS"] = txt39hs.Text;

            //            dr["FZFS"] = txt40fs.Text;
            //            dr["FZHS"] = txt40hs.Text;

            //            dr["FTFS"] = txt42fs.Text;
            //            dr["FTHS"] = txt42hs.Text;

            //            dr["FFFS"] = txt44fs.Text;
            //            dr["FFHS"] = txt44hs.Text;

            //            dr["WSP"] = txtwsp.Text;
            //            dr["avgsize"] = txtavggsize.Text;
            //            dr["Extra"] = txtextrashirt.Text;

            //            if (radbtn.SelectedValue == "1")
            //            {
            //                ledgerr = ddlSupplier.SelectedValue;
            //                mainlab = drplab.SelectedValue;
            //                fitlab = chkfit.Checked;
            //                washlab = Chkwash.Checked;
            //                logolab = Chllogo.Checked;
            //            }
            //            dr["LLedger"] = ledgerr;
            //            dr["Mainlab"] = mainlab;
            //            dr["FItLab"] = fitlab;
            //            dr["Washlab"] = washlab;
            //            dr["Logolab"] = logolab;



            //            dCrt.Rows.Add(dr);

            //        }


            //    }
              

            //}

            //gvcustomerorder.DataSource = dCrt;
            //gvcustomerorder.DataBind();


            //dddldesign.ClearSelection();
            //txtDesignRate.Text = "";
            //txtAvailableMtr.Text = "";
            //txtNoofShirts.Text = "";
            //txtReqMtr.Text = "";
            //txtReqNoShirts.Text = "";
            //txtextrashirt.Text = "";
            ////   rdSingle.Checked = false;
            ////   rdMultiple.Checked = false;

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

           
            //FirstGridViewRow();
            //removedropdownlist();
            //dddldesign.Focus();



        }

        public void removedropdownlist()
        {
            //DataSet myDS = (DataSet)ViewState["MyDataSet"];

            //dCrt = (DataTable)ViewState["Data"];
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dCrt);

            //if (ds.Tables[0].Rows.Count > 0)
            //{


            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        string trainid = ds.Tables[0].Rows[i]["Transid"].ToString();
            //        if (myDS.Tables[0].Rows.Count > 0)
            //        {
            //            for (int j = 0; j < myDS.Tables[0].Rows.Count; j++)
            //            {

            //                string idd = myDS.Tables[0].Rows[j]["id"].ToString();
            //                if (idd == trainid)
            //                {
            //                    dddldesign.Items.Remove(dddldesign.Items.FindByValue(idd));
            //                    // dddldesign.Items.Remove(dddldesign.Items[i]);

            //                }
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
            //double r = 0.00;
            //double tot = 0.00;
            //double gndtot = 0.00;
            //double gndmet = 0.00;


            //double re = 0.00;
            //double r1 = 0.00;

            //double rr = 0.00;
            //double rb = 0.00;


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
            //    TextBox txtreqmeter = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtreqmeter");
            //    TextBox txtshirt = (TextBox)gridsize.Rows[vLoop].FindControl("dtxtshirt");

            //    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
            //        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

            //    gndtot = gndtot + tot;

            //    int col = vLoop + 1;

            //    DataSet dcalculate = objBs.getsizeforcutt(dfit.SelectedValue, drpwidth.SelectedItem.Text);
            //    if (dcalculate.Tables[0].Rows.Count > 0)
            //    {

            //        //  double wid = Convert.ToDouble(dcalculate.Tables[0].Rows[0]["width"]);

            //        double wid = 0;
            //        if (drpFit.SelectedValue == "3")
            //        {
            //            wid = Convert.ToDouble(txtsharp.Text);
            //        }
            //        else
            //        {
            //            wid = Convert.ToDouble(txtexec.Text);
            //        }

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

            //        txtreqmeter.Text = r.ToString();
            //        txtshirt.Text = tot.ToString();
            //        gndmet = gndmet + Convert.ToDouble(txtreqmeter.Text);

            //    }

            //    //   txt36fs.Focus();
            //    if (gridsize.Columns[2].Visible == true) //38FS
            //    {
            //        if (txt36fs.Text == "0" || txt36fs.Text == "")
            //        {
            //            txt36fs.Text = "";
            //        }
            //        txt36fs.Focus();
            //    }
            //    else if (gridsize.Columns[3].Visible == true) //38FS
            //    {
            //        if (txt38fs.Text == "0" || txt38fs.Text == "")
            //        {
            //            txt38fs.Text = "";
            //        }
            //        txt38fs.Focus();
            //    }
            //    else if (gridsize.Columns[4].Visible == true)//39Fs
            //    {
            //        if (txt39fs.Text == "0" || txt39fs.Text == "")
            //        {
            //            txt39fs.Text = "";
            //        }

            //        txt39fs.Focus();
            //    }
            //    else if (gridsize.Columns[5].Visible == true)//40Fs
            //    {
            //        if (txt40fs.Text == "0" || txt40fs.Text == "")
            //        {
            //            txt40fs.Text = "";
            //        }

            //        txt40fs.Focus();
            //    }

            //    else if (gridsize.Columns[6].Visible == true) //42FS
            //    {
            //        if (txt42fs.Text == "0" || txt42fs.Text == "")
            //        {
            //            txt42fs.Text = "";
            //        }

            //        txt42fs.Focus();
            //    }
            //    else if (gridsize.Columns[7].Visible == true) //44FS
            //    {
            //        if (txt44fs.Text == "0" || txt44fs.Text == "")
            //        {
            //            txt44fs.Text = "";
            //        }

            //        txt44fs.Focus();
            //    }

            //    else if (gridsize.Columns[8].Visible == true) //36HS
            //    {
            //        if (txt36hs.Text == "0" || txt36hs.Text == "")
            //        {
            //            txt36hs.Text = "";
            //        }

            //        txt36hs.Focus();
            //    }
            //    else if (gridsize.Columns[9].Visible == true) //38HS
            //    {
            //        if (txt38hs.Text == "0" || txt38hs.Text == "")
            //        {
            //            txt38hs.Text = "";
            //        }

            //        txt38hs.Focus();
            //    }

            //    else if (gridsize.Columns[10].Visible == true) //39HS
            //    {
            //        if (txt39hs.Text == "0" || txt39hs.Text == "")
            //        {
            //            txt39hs.Text = "";
            //        }

            //        txt39hs.Focus();
            //    }
            //    else if (gridsize.Columns[11].Visible == true) //40HS
            //    {
            //        if (txt40hs.Text == "0" || txt40hs.Text == "")
            //        {
            //            txt40hs.Text = "";
            //        }

            //        txt40hs.Focus();
            //    }

            //    else if (gridsize.Columns[12].Visible == true) //42HS
            //    {
            //        if (txt42hs.Text == "0" || txt42hs.Text == "")
            //        {
            //            txt42hs.Text = "";
            //        }

            //        txt42hs.Focus();
            //    }
            //    else if (gridsize.Columns[13].Visible == true) //44HS
            //    {
            //        if (txt44hs.Text == "0" || txt44hs.Text == "")
            //        {
            //            txt44hs.Text = "";
            //        }

            //        txt44hs.Focus();
            //    }


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
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Shirts in greater Than that Required Shirt.Thank you!!!');", true);
            //    btnadd.Enabled = false;
            //    //   btnprocess.Enabled = false;
            //    return;
            //}

        }

        public void getzeroforemptysize()
        {
            //if (txt38FS.Text == "")
            //{
            //    txt38FS.Text = "0";
            //}
            //if (txt39FS.Text == "")
            //{
            //    txt39FS.Text = "0";
            //}
            //if (txt40FS.Text == "")
            //{
            //    txt40FS.Text = "0";
            //}
            //if (txt42FS.Text == "")
            //{
            //    txt42FS.Text = "0";
            //}
            //if (txt44FS.Text == "")
            //{
            //    txt44FS.Text = "0";
            //}
            //if (txt36HS.Text == "")
            //{
            //    txt36HS.Text = "0";
            //}
            //if (txt38HS.Text == "")
            //{
            //    txt38HS.Text = "0";
            //}
            //if (txt39HS.Text == "")
            //{
            //    txt39HS.Text = "0";
            //}
            //if (txt40HS.Text == "")
            //{
            //    txt40HS.Text = "0";
            //}
            //if (txt42HS.Text == "")
            //{
            //    txt42HS.Text = "0";
            //}
            //if (txt44HS.Text == "")
            //{
            //    txt44HS.Text = "0";
            //}
        }

        protected void edit(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    GridViewRow row = gvcustomerorder.SelectedRow;
                    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count -1; vLoop++)
                    {
                        TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                        TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                        TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                        TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                        TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                        TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                        TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                        TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                        TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                        TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                        TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                        TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                        TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                        TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                        TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                        TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                        TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");
                    }
                   
                }
            }
        }
        protected void YourGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Grab the selected row
            GridViewRow row = gvcustomerorder.SelectedRow;

            //Find your textbox within the row
            Label lbltransid = row.FindControl("lbltransid") as Label;
            TextBox avgsize = row.FindControl("txtavgsize") as TextBox;
            TextBox reqshirt = row.FindControl("txtreqshirt") as TextBox;
            TextBox txt36fs = row.FindControl("txttsfs") as TextBox;
            TextBox txt38fs = row.FindControl("txttefs") as TextBox;
            TextBox txt39fs = row.FindControl("txttnfs") as TextBox;
            TextBox txt40fs = row.FindControl("txtfzfs") as TextBox;
            TextBox txt42fs = row.FindControl("txtftfs") as TextBox;
            TextBox txt44fs = row.FindControl("txtfffs") as TextBox;


            TextBox txt36hs = row.FindControl("txttshs") as TextBox;
            TextBox txt38hs = row.FindControl("txttehs") as TextBox;
            TextBox txt39hs = row.FindControl("txttnhs") as TextBox;
            TextBox txt40hs = row.FindControl("txtfzhs") as TextBox;
            TextBox txt42hs = row.FindControl("txtfths") as TextBox;
            TextBox txt44hs = row.FindControl("txtffhs") as TextBox;

            int iscus = objBs.UpdateTranscutnewone(lbltransid.Text, avgsize.Text, reqshirt.Text, txt36fs.Text, txt36hs.Text, txt38fs.Text, txt38hs.Text, txt39fs.Text, txt39hs.Text, txt40fs.Text, txt40hs.Text, txt42fs.Text, txt42hs.Text, txt44fs.Text, txt44hs.Text);
            System.Threading.Thread.Sleep(300);
          
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
            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change38fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();
            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change39fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change40fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change42fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change44fs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }

        protected void change36hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change38hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change39hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change40hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change42hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }
        protected void change44hs(object sender, EventArgs e)
        {
            double r = 0.00;
            double tot = 0.00;
            double gndtot = 0.00;
            double gndmet = 0.00;

            getmultiplesizesetting();

            Sfit = ViewState["Fit"].ToString();
            if (Sfit == "3")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfit");
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) + Convert.ToDouble(txt39fs.Text) + Convert.ToDouble(txt40fs.Text) + Convert.ToDouble(txt42fs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text) + Convert.ToDouble(txt39hs.Text) + Convert.ToDouble(txt40hs.Text) + Convert.ToDouble(txt42hs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "4")
            {
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView2.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView2.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView2.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView2.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView2.Rows[vLoop].FindControl("txtfit");
                    TextBox txt28fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt30fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt32fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt34fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt28hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt30hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt32hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt34hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txt28fs.Text) + Convert.ToDouble(txt30fs.Text) + Convert.ToDouble(txt32fs.Text) + Convert.ToDouble(txt34fs.Text) + Convert.ToDouble(txt36fs.Text) + Convert.ToDouble(txt38fs.Text) +
                        Convert.ToDouble(txt28hs.Text) + Convert.ToDouble(txt30hs.Text) + Convert.ToDouble(txt32hs.Text) + Convert.ToDouble(txt34hs.Text) + Convert.ToDouble(txt36hs.Text) + Convert.ToDouble(txt38hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            else if (Sfit == "5")
            {
                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txtreqmeter = (TextBox)GridView1.Rows[vLoop].FindControl("txteqrmeter");
                    TextBox txtavgsize = (TextBox)GridView1.Rows[vLoop].FindControl("txtavgsize");
                    TextBox txtreqshirt = (TextBox)GridView1.Rows[vLoop].FindControl("txtreqshirt");
                    TextBox txtfitid = (TextBox)GridView1.Rows[vLoop].FindControl("txtfitid");
                    TextBox txtfit = (TextBox)GridView1.Rows[vLoop].FindControl("txtfit");
                    TextBox txtSfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txtMfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txtLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txtXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txtXXLfs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txtShs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txtMhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txtLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txtXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txtXXLhs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

                    tot = Convert.ToDouble(txtSfs.Text) + Convert.ToDouble(txtMfs.Text) + Convert.ToDouble(txtLfs.Text) + Convert.ToDouble(txtXLfs.Text) + Convert.ToDouble(txtXXLfs.Text) + Convert.ToDouble(txt44fs.Text) +
                        Convert.ToDouble(txtShs.Text) + Convert.ToDouble(txtMhs.Text) + Convert.ToDouble(txtLhs.Text) + Convert.ToDouble(txtXLhs.Text) + Convert.ToDouble(txtXXLhs.Text) + Convert.ToDouble(txt44hs.Text);

                    gndtot = gndtot + tot;

                    int col = vLoop + 1;
                    {

                        double wid = 0;

                        txtreqshirt.Text = tot.ToString();
                        txtavgsize.Text = (Convert.ToDouble(txtreqmeter.Text) / tot).ToString("N");
                    }

                }
            }
            UpdatePanel2.Update();

            System.Threading.Thread.Sleep(300);
        }

        public void getmultiplesizesetting()
        {
            Sfit = ViewState["Fit"].ToString();
            if(Sfit=="3")
            {

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txt36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtffhs");

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
            else if (Sfit == "4")
            {

                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    TextBox txt36fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)GridView2.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView2.Rows[vLoop].FindControl("txtffhs");

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
            else if (Sfit == "5")
            {

                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    TextBox txt36fs = (TextBox)GridView1.Rows[vLoop].FindControl("txttsfs");
                    TextBox txt38fs = (TextBox)GridView1.Rows[vLoop].FindControl("txttefs");
                    TextBox txt39fs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnfs");
                    TextBox txt40fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzfs");
                    TextBox txt42fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtftfs");
                    TextBox txt44fs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfffs");
                    TextBox txt36hs = (TextBox)GridView1.Rows[vLoop].FindControl("txttshs");
                    TextBox txt38hs = (TextBox)GridView1.Rows[vLoop].FindControl("txttehs");
                    TextBox txt39hs = (TextBox)GridView1.Rows[vLoop].FindControl("txttnhs");
                    TextBox txt40hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfzhs");
                    TextBox txt42hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtfths");
                    TextBox txt44hs = (TextBox)GridView1.Rows[vLoop].FindControl("txtffhs");

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
            System.Threading.Thread.Sleep(300);

        }

    }
}