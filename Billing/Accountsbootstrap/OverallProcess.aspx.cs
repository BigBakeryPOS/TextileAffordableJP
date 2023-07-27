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
using System.Web.UI.HtmlControls;

namespace Billing.Accountsbootstrap
{
    public partial class OverallProcess : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
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


                //DataSet ds = objBs.SelectStitchingInfoDetGridNew();

                //if (ds != null)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        gvcust.DataSource = ds;
                //        gvcust.DataBind();
                //    }

                //    else
                //    {
                //        gvcust.DataSource = null;
                //        gvcust.DataBind();
                //    }
                //}
                //else
                //{
                //    gvcust.DataSource = null;
                //    gvcust.DataBind();

                //}
                txtstartdate.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
                txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }




        }

        protected void drpbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objBs.SelectStitchingInfoDetGridNew(drpbranch.SelectedValue);
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

            //string button = string.Empty;
            //button = btnadd.Text;
            //{
            //    button = btnadd.Text;
            //    Response.Redirect("Suppliermaster.aspx?name=" + button.ToString());
            //}
            Response.Redirect("../Accountsbootstrap/Dillo_StitchingInfo.aspx");

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectcustomerDet(6, 2);
            }
            else
            {
                ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);
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

            Response.Redirect("../Accountsbootstrap/viewsupplier.aspx");

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            //DataSet ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);

            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        gvcust.DataSource = ds;
            //        gvcust.DataBind();
            //    }
            //    else
            //    {
            //        gvcust.DataSource = null;
            //        gvcust.DataBind();
            //    }
            //}
            //else
            //{
            //    gvcust.DataSource = null;
            //    gvcust.DataBind();
            //}


            DataTable dt = new DataTable();
            DataSet dss = new DataSet();

            DataTable dt1 = new DataTable();
            DataSet dss1 = new DataSet();
            if (txtstartdate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Start Date!!!.Thanks you!!!')", true);
                return;
            }
            if (txtenddate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select End date!!!.Thanks You!!!')", true);
                return;
            }


            DataSet ds = objBs.SelectStitchingInfoDetGrid_ByDate(txtstartdate.Text, txtenddate.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null)
                {
                    gvcust.DataSource = ds;
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
            DataSet dgett = new DataSet();
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Dillo_StitchingInfo.aspx?LotNo=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
                //int iSucess = objBs.deletecustomer(e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text);

                int iSucess = objBs.Delete_StitchingInfo_All(e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text);

                //  Response.Redirect("viewsupplier.aspx");

                Response.Redirect("Dillo_StitchingInfoGrid.aspx");

                DataSet ds = objBs.SelectStitchingInfoDetGrid();
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
            else if (e.CommandName == "LotDeatils")
            {

                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("FullLotDetails.aspx?FullLotDetailsid=" + e.CommandArgument.ToString());
                }

               // DataSet dstat = objBs.getstatusreport(Convert.ToInt32(e.CommandArgument.ToString()));
              //  if (dstat.Tables[0].Rows.Count > 0)
              //  {
                  //  string LotDetailId = e.CommandArgument.ToString();

                 //   if (dstat.Tables[0].Rows.Count > 0)
                 //   {
                       
                        #region
                        //string CompanyLotNo = "";
                        //for (int i = 0; i < dstat.Tables[0].Rows.Count; i++)
                        //{
                        //    string currentststus = dstat.Tables[0].Rows[i]["Currentstatus"].ToString();
                        //    string ststus = dstat.Tables[0].Rows[i]["Status"].ToString();
                        //    string screen = dstat.Tables[0].Rows[i]["Screen"].ToString();

                        //     CompanyLotNo = dstat.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                        //    if (screen == "Stc")
                        //    {
                        //        DataSet dsStiching = new DataSet();
                        //        dsStiching = objBs.JpAllProcessLotDeatils("tblJpStiching", Convert.ToInt32(LotDetailId));
                        //        GVJpStiching.DataSource = dsStiching;
                        //        GVJpStiching.DataBind();
                        //    }

                        //    if (screen == "Kaja")
                        //    {
                        //        DataSet dsKajaButton = new DataSet();
                        //        dsKajaButton = objBs.JpAllProcessLotDeatils("tblJpKajaButton", Convert.ToInt32(LotDetailId));
                        //        GVJpKajaButton.DataSource = dsKajaButton;
                        //        GVJpKajaButton.DataBind();
                        //    }
                        //    if (screen == "Emb")
                        //    {
                        //        DataSet dsEmbroiding = new DataSet();
                        //        dsEmbroiding = objBs.JpAllProcessLotDeatils("tblJpEmbroiding", Convert.ToInt32(LotDetailId));
                        //        GVJpEmbroiding.DataSource = dsEmbroiding;
                        //        GVJpEmbroiding.DataBind();
                        //    }
                        //    if (screen == "Wash")
                        //    {
                        //        DataSet dsWashing = new DataSet();
                        //        dsWashing = objBs.JpAllProcessLotDeatils("tblJpWashing", Convert.ToInt32(LotDetailId));
                        //        GVJpWashing.DataSource = dsWashing;
                        //        GVJpWashing.DataBind();
                        //    }
                        //    if (screen == "Print")
                        //    {
                        //        DataSet dsPrinting = new DataSet();
                        //        dsPrinting = objBs.JpAllProcessLotDeatils("tblJpPrinting", Convert.ToInt32(LotDetailId));
                        //        GVJpPrinting.DataSource = dsPrinting;
                        //        GVJpPrinting.DataBind();
                        //    }
                        //    if (screen == "Iron")
                        //    {
                        //        DataSet dsIroning = new DataSet();
                        //        dsIroning = objBs.JpAllProcessLotDeatils("tblJpIroning", Convert.ToInt32(LotDetailId));
                        //        GVJpIroning.DataSource = dsIroning;
                        //        GVJpIroning.DataBind();
                        //    }
                        //    if (screen == "Btag")
                        //    {
                        //        DataSet dsBarTag = new DataSet();
                        //        dsBarTag = objBs.JpAllProcessLotDeatils("tblJpBarTag", Convert.ToInt32(LotDetailId));
                        //        GVJpBarTag.DataSource = dsBarTag;
                        //        GVJpBarTag.DataBind();
                        //    }
                        //    if (screen == "Trm")
                        //    {
                        //        DataSet dsTrimming = new DataSet();
                        //        dsTrimming = objBs.JpAllProcessLotDeatils("tblJpTrimming", Convert.ToInt32(LotDetailId));
                        //        GVJpTrimming.DataSource = dsTrimming;
                        //        GVJpTrimming.DataBind();
                        //    }
                        //    if (screen == "Cni")
                        //    {
                        //        DataSet dsConsai = new DataSet();
                        //        dsConsai = objBs.JpAllProcessLotDeatils("tblJpConsai", Convert.ToInt32(LotDetailId));
                        //        GVJpConsai.DataSource = dsConsai;
                        //        GVJpConsai.DataBind();
                        //    }

                           
                        //}



                        //DataSet dsCompanyLotNo = new DataSet();
                        //dsCompanyLotNo = objBs.JpAllProcessdespatch(CompanyLotNo);
                        //GVDespatchstock.DataSource = dsCompanyLotNo;
                        //GVDespatchstock.DataBind();

                        #endregion
                       // mpenewLotDeatils.Show();
                 //   }

               // }

              
            }
            else if (e.CommandName == "Status")
            {

                {

                    divLot1.Visible = true;

                    DataSet dstat = objBs.getstatusreport(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (dstat.Tables[0].Rows.Count > 0)
                    {
                        string cuttid = e.CommandArgument.ToString();

                        if (dstat.Tables[0].Rows.Count > 0)
                        {

                            #region

                            for (int i = 0; i < dstat.Tables[0].Rows.Count; i++)
                            {
                                string currentststus = dstat.Tables[0].Rows[i]["Currentstatus"].ToString();
                                string ststus = dstat.Tables[0].Rows[i]["Status"].ToString();
                                string screen = dstat.Tables[0].Rows[i]["Screen"].ToString();

                                if (screen == "Stc")
                                {
                                    idlblstiching.Visible = true;

                                    if (ststus == "Y")
                                    {
                                        lblstiching.BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (currentststus == "Y")
                                    {
                                        lblstiching.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lblstiching.BackColor = System.Drawing.Color.Yellow;
                                    }
                                }

                                if (screen == "Kaja")
                                {
                                    idlblKaja.Visible = true;

                                    if (ststus == "Y")
                                    {
                                        lblKaja.BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (currentststus == "Y")
                                    {
                                        lblKaja.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lblKaja.BackColor = System.Drawing.Color.Yellow;
                                    }
                                }
                                if (screen == "Emb")
                                {
                                    idlblemb.Visible = true;

                                    if (ststus == "Y")
                                    {
                                        lblemb.BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (currentststus == "Y")
                                    {
                                        lblemb.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lblemb.BackColor = System.Drawing.Color.Yellow;
                                    }
                                }
                                if (screen == "Wash")
                                {
                                    idlblwash.Visible = true;

                                    if (ststus == "Y")
                                    {
                                        lblwash.BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (currentststus == "Y")
                                    {
                                        lblwash.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lblwash.BackColor = System.Drawing.Color.Yellow;
                                    }
                                }
                                if (screen == "Print")
                                {
                                    idlblprint.Visible = true;

                                    if (ststus == "Y")
                                    {
                                        lblprint.BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (currentststus == "Y")
                                    {
                                        lblprint.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lblprint.BackColor = System.Drawing.Color.Yellow;
                                    }
                                }
                                if (screen == "Iron")
                                {
                                    idlbliron.Visible = true;

                                    if (ststus == "Y")
                                    {
                                        lbliron.BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (currentststus == "Y")
                                    {
                                        lbliron.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lbliron.BackColor = System.Drawing.Color.Yellow;
                                    }
                                }



                                if (screen == "Btag")
                                {
                                    idlblbartag.Visible = true;

                                    if (ststus == "Y")
                                    {
                                        lblbartag.BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (currentststus == "Y")
                                    {
                                        lblbartag.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lblbartag.BackColor = System.Drawing.Color.Yellow;
                                    }
                                }


                                if (screen == "Trm")
                                {
                                    idlbltrimming.Visible = true;

                                    if (ststus == "Y")
                                    {
                                        lbltrimming.BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (currentststus == "Y")
                                    {
                                        lbltrimming.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lbltrimming.BackColor = System.Drawing.Color.Yellow;
                                    }
                                }


                                if (screen == "Cni")
                                {
                                    idlblconsai.Visible = true;

                                    if (ststus == "Y")
                                    {
                                        lblconsai.BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (currentststus == "Y")
                                    {
                                        lblconsai.BackColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lblconsai.BackColor = System.Drawing.Color.Yellow;
                                    }
                                }
                            }

                            #endregion

                        }



                        DataSet dshirtall = objBs.getLotNoReportGenerateNew(Convert.ToInt32(cuttid));
                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "3");
                        // lblLot.Text = cuttid;
                        if (dgett.Tables.Count > 0)
                        {
                            if (dgett.Tables[0].Rows.Count > 0)
                            {
                                //reportDetails.Visible = true;
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();

                                dtt.Columns.Add(new DataColumn("Item", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                                // dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                //  dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("DmgQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                                // dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                // dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                                {
                                    int pending = 0;
                                    int k = 0;
                                    int total = 0;

                                    string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                    {
                                        if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString() && dgett.Tables[0].Rows[i]["Fitid"].ToString() == dshirtall.Tables[0].Rows[j]["Fitid"].ToString())
                                        {
                                            DataRow dr = dtt.NewRow();
                                            dr["Item"] = dshirtall.Tables[0].Rows[j]["Itemname"].ToString();
                                            dr["Pattern"] = dshirtall.Tables[0].Rows[j]["PatternName"].ToString();
                                            dr["Fit"] = dshirtall.Tables[0].Rows[j]["fit"].ToString();
                                            dr["checked"] = dshirtall.Tables[0].Rows[j]["jobwork"].ToString();
                                            //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                            dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                            dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                            dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                            dr["DmgQty"] = dshirtall.Tables[0].Rows[j]["DmgQty"].ToString();
                                            dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();

                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                }
                                Gridoverall.DataSource = temp;
                                Gridoverall.DataBind();
                            }
                            else
                            {
                                Gridoverall.DataSource = null;
                                Gridoverall.DataBind();
                            }
                        }



                        //KAJA PROCESS

                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "2");
                        if (dgett.Tables.Count > 0)
                        {
                            #region
                            // DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                            //lblLot.Text = e.CommandArgument.ToString();
                            if (dgett.Tables[0].Rows.Count > 0)
                            {
                                //reportDetails.Visible = true;
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("Item", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("DmgQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                                {
                                    int pending = 0;
                                    int k = 0;
                                    int total = 0;

                                    string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                    {
                                        if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString() && dgett.Tables[0].Rows[i]["Fitid"].ToString() == dshirtall.Tables[0].Rows[j]["Fitid"].ToString())
                                        {
                                            DataRow dr = dtt.NewRow();
                                            dr["Item"] = dshirtall.Tables[0].Rows[j]["Itemname"].ToString();
                                            dr["Pattern"] = dshirtall.Tables[0].Rows[j]["PatternName"].ToString();
                                            dr["Fit"] = dshirtall.Tables[0].Rows[j]["fit"].ToString();
                                            dr["checked"] = dshirtall.Tables[0].Rows[j]["jobwork"].ToString();
                                            //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                            dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                            dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                            dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                            dr["DmgQty"] = dshirtall.Tables[0].Rows[j]["DmgQty"].ToString();
                                            dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();


                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                }
                                //Gridoverall.DataSource = temp;
                                //Gridoverall.DataBind();
                                gridkaja.DataSource = temp;
                                gridkaja.DataBind();
                            }
                            else
                            {
                                gridkaja.DataSource = null;
                                gridkaja.DataBind();
                            }

                            #endregion
                        }
                        //EMBROIDING

                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "4");

                        //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        //lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables.Count > 0)
                        {
                            #region
                            if (dgett.Tables[0].Rows.Count > 0)
                            {
                                //  reportDetails.Visible = true;
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("Item", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                                dtt.Columns.Add(new DataColumn("DmgQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                                {
                                    int pending = 0;
                                    int k = 0;
                                    int total = 0;

                                    string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                    {
                                        if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString() && dgett.Tables[0].Rows[i]["Fitid"].ToString() == dshirtall.Tables[0].Rows[j]["Fitid"].ToString())
                                        {
                                            DataRow dr = dtt.NewRow();
                                            dr["Item"] = dshirtall.Tables[0].Rows[j]["Itemname"].ToString();
                                            dr["Pattern"] = dshirtall.Tables[0].Rows[j]["PatternName"].ToString();
                                            dr["Fit"] = dshirtall.Tables[0].Rows[j]["fit"].ToString();
                                            dr["checked"] = dshirtall.Tables[0].Rows[j]["jobwork"].ToString();
                                            //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                            dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                            dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                            dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                            dr["DmgQty"] = dshirtall.Tables[0].Rows[j]["DmgQty"].ToString();
                                            dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();


                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                }
                                //Gridoverall.DataSource = temp;
                                //Gridoverall.DataBind();
                                gridemb.DataSource = temp;
                                gridemb.DataBind();
                            }
                            else
                            {
                                //Gridoverall.DataSource = null;
                                //Gridoverall.DataBind();
                                gridemb.DataSource = null;
                                gridemb.DataBind();
                            }
                            #endregion
                        }

                        //WASHING

                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "5");

                        //    DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        //lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables.Count > 0)
                        {
                            #region
                            if (dgett.Tables[0].Rows.Count > 0)
                            {
                                //  reportDetails.Visible = true;
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("Item", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                                dtt.Columns.Add(new DataColumn("DmgQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                                {
                                    int pending = 0;
                                    int k = 0;
                                    int total = 0;

                                    string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                    {
                                        if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString() && dgett.Tables[0].Rows[i]["Fitid"].ToString() == dshirtall.Tables[0].Rows[j]["Fitid"].ToString())
                                        {
                                            DataRow dr = dtt.NewRow();
                                            dr["Item"] = dshirtall.Tables[0].Rows[j]["Itemname"].ToString();
                                            dr["Pattern"] = dshirtall.Tables[0].Rows[j]["PatternName"].ToString();
                                            dr["Fit"] = dshirtall.Tables[0].Rows[j]["fit"].ToString();
                                            dr["checked"] = dshirtall.Tables[0].Rows[j]["jobwork"].ToString();
                                            //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                            dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                            dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                            dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                            dr["DmgQty"] = dshirtall.Tables[0].Rows[j]["DmgQty"].ToString();
                                            dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();


                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                }
                                gridwash.DataSource = temp;
                                gridwash.DataBind();
                                //Gridoverall.DataSource = temp;
                                //Gridoverall.DataBind();
                            }
                            else
                            {
                                gridwash.DataSource = null;
                                gridwash.DataBind();
                                //Gridoverall.DataSource = null;
                                //Gridoverall.DataBind();
                            }

                            #endregion
                        }


                        //Printing
                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "8");

                        //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        //lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables.Count > 0)
                        {
                            #region
                            if (dgett.Tables[0].Rows.Count > 0)
                            {
                                // reportDetails.Visible = true;
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("Item", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                                dtt.Columns.Add(new DataColumn("DmgQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                                {
                                    int pending = 0;
                                    int k = 0;
                                    int total = 0;

                                    string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                    {
                                        if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString() && dgett.Tables[0].Rows[i]["Fitid"].ToString() == dshirtall.Tables[0].Rows[j]["Fitid"].ToString())
                                        {
                                            DataRow dr = dtt.NewRow();
                                            dr["Item"] = dshirtall.Tables[0].Rows[j]["Itemname"].ToString();
                                            dr["Pattern"] = dshirtall.Tables[0].Rows[j]["PatternName"].ToString();
                                            dr["Fit"] = dshirtall.Tables[0].Rows[j]["fit"].ToString();
                                            dr["checked"] = dshirtall.Tables[0].Rows[j]["jobwork"].ToString();
                                            //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                            dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                            dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                            dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                            dr["DmgQty"] = dshirtall.Tables[0].Rows[j]["DmgQty"].ToString();
                                            dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();


                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                }
                                gridprint.DataSource = temp;
                                gridprint.DataBind();
                                //Gridoverall.DataSource = temp;
                                //Gridoverall.DataBind();
                            }
                            else
                            {
                                gridprint.DataSource = null;
                                gridprint.DataBind();
                                //Gridoverall.DataSource = null;
                                //Gridoverall.DataBind();
                            }

                            #endregion
                        }

                        //IRON AND PACKING
                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "6");

                        //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        //lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables.Count > 0)
                        {

                            #region
                            if (dgett.Tables[0].Rows.Count > 0)
                            {
                                // reportDetails.Visible = true;
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("Item", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                                dtt.Columns.Add(new DataColumn("DmgQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                                {
                                    int pending = 0;
                                    int k = 0;
                                    int total = 0;

                                    string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                    {
                                        if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString() && dgett.Tables[0].Rows[i]["Fitid"].ToString() == dshirtall.Tables[0].Rows[j]["Fitid"].ToString())
                                        {
                                            DataRow dr = dtt.NewRow();
                                            dr["Item"] = dshirtall.Tables[0].Rows[j]["Itemname"].ToString();
                                            dr["Pattern"] = dshirtall.Tables[0].Rows[j]["PatternName"].ToString();
                                            dr["Fit"] = dshirtall.Tables[0].Rows[j]["fit"].ToString();
                                            dr["checked"] = dshirtall.Tables[0].Rows[j]["jobwork"].ToString();
                                            //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                            dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                            dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                            dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                            dr["DmgQty"] = dshirtall.Tables[0].Rows[j]["DmgQty"].ToString();
                                            dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();


                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                }
                                gridiron.DataSource = temp;
                                gridiron.DataBind();
                                //Gridoverall.DataSource = temp;
                                //Gridoverall.DataBind();
                            }
                            else
                            {
                                gridiron.DataSource = null;
                                gridiron.DataBind();
                                //Gridoverall.DataSource = null;
                                //Gridoverall.DataBind();
                            }

                            #endregion
                        }



                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "9");

                        //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        //lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables.Count > 0)
                        {
                            #region
                            if (dgett.Tables[0].Rows.Count > 0)
                            {
                                // reportDetails.Visible = true;
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("Item", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                                dtt.Columns.Add(new DataColumn("DmgQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                                {
                                    int pending = 0;
                                    int k = 0;
                                    int total = 0;

                                    string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                    {
                                        if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString() && dgett.Tables[0].Rows[i]["Fitid"].ToString() == dshirtall.Tables[0].Rows[j]["Fitid"].ToString())
                                        {
                                            DataRow dr = dtt.NewRow();
                                            dr["Item"] = dshirtall.Tables[0].Rows[j]["Itemname"].ToString();
                                            dr["Pattern"] = dshirtall.Tables[0].Rows[j]["PatternName"].ToString();
                                            dr["Fit"] = dshirtall.Tables[0].Rows[j]["fit"].ToString();
                                            dr["checked"] = dshirtall.Tables[0].Rows[j]["jobwork"].ToString();
                                            //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                            dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                            dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                            dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                            dr["DmgQty"] = dshirtall.Tables[0].Rows[j]["DmgQty"].ToString();
                                            dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();


                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                }
                                gvbartag.DataSource = temp;
                                gvbartag.DataBind();
                                //Gridoverall.DataSource = temp;
                                //Gridoverall.DataBind();
                            }
                            else
                            {
                                gvbartag.DataSource = null;
                                gvbartag.DataBind();
                                //Gridoverall.DataSource = null;
                                //Gridoverall.DataBind();
                            }

                            #endregion
                        }


                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "10");

                        //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        //lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables.Count > 0)
                        {
                            #region
                            if (dgett.Tables[0].Rows.Count > 0)
                            {
                                // reportDetails.Visible = true;
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("Item", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                                dtt.Columns.Add(new DataColumn("DmgQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                                {
                                    int pending = 0;
                                    int k = 0;
                                    int total = 0;

                                    string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                    {
                                        if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString() && dgett.Tables[0].Rows[i]["Fitid"].ToString() == dshirtall.Tables[0].Rows[j]["Fitid"].ToString())
                                        {
                                            DataRow dr = dtt.NewRow();
                                            dr["Item"] = dshirtall.Tables[0].Rows[j]["Itemname"].ToString();
                                            dr["Pattern"] = dshirtall.Tables[0].Rows[j]["PatternName"].ToString();
                                            dr["Fit"] = dshirtall.Tables[0].Rows[j]["fit"].ToString();
                                            dr["checked"] = dshirtall.Tables[0].Rows[j]["jobwork"].ToString();
                                            //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                            dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                            dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                            dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                            dr["DmgQty"] = dshirtall.Tables[0].Rows[j]["DmgQty"].ToString();
                                            dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();


                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                }
                                gvtrimming.DataSource = temp;
                                gvtrimming.DataBind();
                                //Gridoverall.DataSource = temp;
                                //Gridoverall.DataBind();
                            }
                            else
                            {
                                gvtrimming.DataSource = null;
                                gvtrimming.DataBind();
                                //Gridoverall.DataSource = null;
                                //Gridoverall.DataBind();
                            }

                            #endregion
                        }


                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "11");

                        //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        //lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables.Count > 0)
                        {
                            #region
                            if (dgett.Tables[0].Rows.Count > 0)
                            {
                                // reportDetails.Visible = true;
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();
                                dtt.Columns.Add(new DataColumn("Item", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                                dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                                dtt.Columns.Add(new DataColumn("DmgQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                                {
                                    int pending = 0;
                                    int k = 0;
                                    int total = 0;

                                    string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                    {
                                        if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString() && dgett.Tables[0].Rows[i]["Fitid"].ToString() == dshirtall.Tables[0].Rows[j]["Fitid"].ToString())
                                        {
                                            DataRow dr = dtt.NewRow();
                                            dr["Item"] = dshirtall.Tables[0].Rows[j]["Itemname"].ToString();
                                            dr["Pattern"] = dshirtall.Tables[0].Rows[j]["PatternName"].ToString();
                                            dr["Fit"] = dshirtall.Tables[0].Rows[j]["fit"].ToString();
                                            dr["checked"] = dshirtall.Tables[0].Rows[j]["jobwork"].ToString();
                                            //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                            dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                            dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                            dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                            dr["DmgQty"] = dshirtall.Tables[0].Rows[j]["DmgQty"].ToString();
                                            dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();


                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                }
                                gvconsai.DataSource = temp;
                                gvconsai.DataBind();
                                //Gridoverall.DataSource = temp;
                                //Gridoverall.DataBind();
                            }
                            else
                            {
                                gvconsai.DataSource = null;
                                gvconsai.DataBind();
                                //Gridoverall.DataSource = null;
                                //Gridoverall.DataBind();
                            }

                            #endregion
                        }
                    }
                }
                mpe.Show();
            }
            else if (e.CommandName == "ProcessDetails")
            {
                DataSet dsSTICHING = new DataSet();
                DataSet dsKAJA = new DataSet();
                DataSet dsWASHING = new DataSet();
                DataSet dsPRINTING = new DataSet();
                DataSet dsIRONING = new DataSet();
                DataSet dsEMBROIDING = new DataSet();

                DataSet dsbartag = new DataSet();
                DataSet dstrimming = new DataSet();
                DataSet dsconsai = new DataSet();

                DataTable dtCopy6 = new DataTable();
                DataTable dtcopy1 = new DataTable();
                DataTable dtcopy2 = new DataTable();
                DataTable dtcopy3 = new DataTable();
                DataTable dtcopy4 = new DataTable();
                DataTable dtcopy5 = new DataTable();

                DataTable dtcopy8 = new DataTable();
                DataTable dtcopy9 = new DataTable();
                DataTable dtcopy10 = new DataTable();

                divLot1new.Visible = true;

                DataTable dtt = new DataTable();

                dtt.Columns.Add(new DataColumn("LotNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("FullSL", typeof(double)));
                dtt.Columns.Add(new DataColumn("HalfSL", typeof(double)));
                dtt.Columns.Add(new DataColumn("TotalQty", typeof(double)));

                dtt.Columns.Add(new DataColumn("Qty", typeof(double)));
                dtt.Columns.Add(new DataColumn("Definition", typeof(string)));
                dtt.Columns.Add(new DataColumn("Serial_No", typeof(string)));
                dtt.Columns.Add(new DataColumn("category", typeof(string)));

                dtt.Columns.Add(new DataColumn("categoryid", typeof(string)));
                dtt.Columns.Add(new DataColumn("CategoryUserID", typeof(string)));




                dtcopy1 = dtt.Clone();
                dtcopy2 = dtt.Clone();
                dtcopy3 = dtt.Clone();
                dtcopy4 = dtt.Clone();
                dtcopy5 = dtt.Clone();
                dtCopy6 = dtt.Clone();

                dtcopy8 = dtt.Clone();
                dtcopy9 = dtt.Clone();
                dtcopy10 = dtt.Clone();
                // ds.Tables.Add(dtCopy)


                dsSTICHING.Tables.Add(dtcopy1);
                dsKAJA.Tables.Add(dtcopy2);
                dsWASHING.Tables.Add(dtcopy3);
                dsPRINTING.Tables.Add(dtcopy4);
                dsIRONING.Tables.Add(dtcopy5);
                dsEMBROIDING.Tables.Add(dtCopy6);

                dsbartag.Tables.Add(dtcopy8);
                dstrimming.Tables.Add(dtcopy9);
                dsconsai.Tables.Add(dtcopy10);

                DataSet Category = objBs.selectCAT();
                if (Category.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < Category.Tables[0].Rows.Count; k++)
                    {
                        string catidd = Category.Tables[0].Rows[k]["categoryid"].ToString();

                        DataSet ProcessDetails = objBs.ProcessStatusDetails(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Category.Tables[0].Rows[k]["categoryid"].ToString()));
                        if (ProcessDetails.Tables[0].Rows.Count > 0)
                        {







                            for (int j = 0; j < ProcessDetails.Tables[0].Rows.Count; j++)
                            {
                                DataRow dr1 = dtcopy1.NewRow();
                                DataRow dr2 = dtcopy2.NewRow();
                                DataRow dr3 = dtcopy3.NewRow();
                                DataRow dr4 = dtcopy4.NewRow();
                                DataRow dr5 = dtcopy5.NewRow();
                                DataRow dr6 = dtCopy6.NewRow();

                                DataRow dr8 = dtcopy8.NewRow();
                                DataRow dr9 = dtcopy9.NewRow();
                                DataRow dr10 = dtcopy10.NewRow();

                                if (k == 0)
                                {
                                    dr1["LotNo"] = ProcessDetails.Tables[0].Rows[j]["Lot"].ToString();
                                    dr1["FullSL"] = ProcessDetails.Tables[0].Rows[j]["FullSL"].ToString();
                                    dr1["HalfSL"] = ProcessDetails.Tables[0].Rows[j]["HalfSL"].ToString();
                                    dr1["TotalQty"] = ProcessDetails.Tables[0].Rows[j]["TotalQty"].ToString();

                                    dr1["Qty"] = ProcessDetails.Tables[0].Rows[j]["Qty"].ToString();
                                    dr1["Definition"] = ProcessDetails.Tables[0].Rows[j]["Definition"].ToString();
                                    dr1["Serial_No"] = ProcessDetails.Tables[0].Rows[j]["Serial_No"].ToString();
                                    dr1["category"] = ProcessDetails.Tables[0].Rows[j]["category"].ToString();

                                    dr1["categoryid"] = ProcessDetails.Tables[0].Rows[j]["categoryid"].ToString();
                                    dr1["CategoryUserID"] = ProcessDetails.Tables[0].Rows[j]["CategoryUserID"].ToString();

                                }
                                if (k == 1)
                                {
                                    dr2["LotNo"] = ProcessDetails.Tables[0].Rows[j]["Lot"].ToString();
                                    dr2["FullSL"] = ProcessDetails.Tables[0].Rows[j]["FullSL"].ToString();
                                    dr2["HalfSL"] = ProcessDetails.Tables[0].Rows[j]["HalfSL"].ToString();
                                    dr2["TotalQty"] = ProcessDetails.Tables[0].Rows[j]["TotalQty"].ToString();

                                    dr2["Qty"] = ProcessDetails.Tables[0].Rows[j]["Qty"].ToString();
                                    dr2["Definition"] = ProcessDetails.Tables[0].Rows[j]["Definition"].ToString();
                                    dr2["Serial_No"] = ProcessDetails.Tables[0].Rows[j]["Serial_No"].ToString();
                                    dr2["category"] = ProcessDetails.Tables[0].Rows[j]["category"].ToString();

                                    dr2["categoryid"] = ProcessDetails.Tables[0].Rows[j]["categoryid"].ToString();
                                    dr2["CategoryUserID"] = ProcessDetails.Tables[0].Rows[j]["CategoryUserID"].ToString();
                                }
                                if (k == 2)
                                {
                                    dr3["LotNo"] = ProcessDetails.Tables[0].Rows[j]["Lot"].ToString();
                                    dr3["FullSL"] = ProcessDetails.Tables[0].Rows[j]["FullSL"].ToString();
                                    dr3["HalfSL"] = ProcessDetails.Tables[0].Rows[j]["HalfSL"].ToString();
                                    dr3["TotalQty"] = ProcessDetails.Tables[0].Rows[j]["TotalQty"].ToString();

                                    dr3["Qty"] = ProcessDetails.Tables[0].Rows[j]["Qty"].ToString();
                                    dr3["Definition"] = ProcessDetails.Tables[0].Rows[j]["Definition"].ToString();
                                    dr3["Serial_No"] = ProcessDetails.Tables[0].Rows[j]["Serial_No"].ToString();
                                    dr3["category"] = ProcessDetails.Tables[0].Rows[j]["category"].ToString();

                                    dr3["categoryid"] = ProcessDetails.Tables[0].Rows[j]["categoryid"].ToString();
                                    dr3["CategoryUserID"] = ProcessDetails.Tables[0].Rows[j]["CategoryUserID"].ToString();
                                }
                                if (k == 3)
                                {
                                    dr4["LotNo"] = ProcessDetails.Tables[0].Rows[j]["Lot"].ToString();
                                    dr4["FullSL"] = ProcessDetails.Tables[0].Rows[j]["FullSL"].ToString();
                                    dr4["HalfSL"] = ProcessDetails.Tables[0].Rows[j]["HalfSL"].ToString();
                                    dr4["TotalQty"] = ProcessDetails.Tables[0].Rows[j]["TotalQty"].ToString();

                                    dr4["Qty"] = ProcessDetails.Tables[0].Rows[j]["Qty"].ToString();
                                    dr4["Definition"] = ProcessDetails.Tables[0].Rows[j]["Definition"].ToString();
                                    dr4["Serial_No"] = ProcessDetails.Tables[0].Rows[j]["Serial_No"].ToString();
                                    dr4["category"] = ProcessDetails.Tables[0].Rows[j]["category"].ToString();

                                    dr4["categoryid"] = ProcessDetails.Tables[0].Rows[j]["categoryid"].ToString();
                                    dr4["CategoryUserID"] = ProcessDetails.Tables[0].Rows[j]["CategoryUserID"].ToString();
                                }
                                if (k == 4)
                                {
                                    dr5["LotNo"] = ProcessDetails.Tables[0].Rows[j]["Lot"].ToString();
                                    dr5["FullSL"] = ProcessDetails.Tables[0].Rows[j]["FullSL"].ToString();
                                    dr5["HalfSL"] = ProcessDetails.Tables[0].Rows[j]["HalfSL"].ToString();
                                    dr5["TotalQty"] = ProcessDetails.Tables[0].Rows[j]["TotalQty"].ToString();

                                    dr5["Qty"] = ProcessDetails.Tables[0].Rows[j]["Qty"].ToString();
                                    dr5["Definition"] = ProcessDetails.Tables[0].Rows[j]["Definition"].ToString();
                                    dr5["Serial_No"] = ProcessDetails.Tables[0].Rows[j]["Serial_No"].ToString();
                                    dr5["category"] = ProcessDetails.Tables[0].Rows[j]["category"].ToString();

                                    dr5["categoryid"] = ProcessDetails.Tables[0].Rows[j]["categoryid"].ToString();
                                    dr5["CategoryUserID"] = ProcessDetails.Tables[0].Rows[j]["CategoryUserID"].ToString();
                                }
                                if (k == 5)
                                {
                                    dr6["LotNo"] = ProcessDetails.Tables[0].Rows[j]["Lot"].ToString();
                                    dr6["FullSL"] = ProcessDetails.Tables[0].Rows[j]["FullSL"].ToString();
                                    dr6["HalfSL"] = ProcessDetails.Tables[0].Rows[j]["HalfSL"].ToString();
                                    dr6["TotalQty"] = ProcessDetails.Tables[0].Rows[j]["TotalQty"].ToString();

                                    dr6["Qty"] = ProcessDetails.Tables[0].Rows[j]["Qty"].ToString();
                                    dr6["Definition"] = ProcessDetails.Tables[0].Rows[j]["Definition"].ToString();
                                    dr6["Serial_No"] = ProcessDetails.Tables[0].Rows[j]["Serial_No"].ToString();
                                    dr6["category"] = ProcessDetails.Tables[0].Rows[j]["category"].ToString();

                                    dr6["categoryid"] = ProcessDetails.Tables[0].Rows[j]["categoryid"].ToString();
                                    dr6["CategoryUserID"] = ProcessDetails.Tables[0].Rows[j]["CategoryUserID"].ToString();
                                }

                                if (k == 6)
                                {
                                    dr6["LotNo"] = ProcessDetails.Tables[0].Rows[j]["Lot"].ToString();
                                    dr6["FullSL"] = ProcessDetails.Tables[0].Rows[j]["FullSL"].ToString();
                                    dr6["HalfSL"] = ProcessDetails.Tables[0].Rows[j]["HalfSL"].ToString();
                                    dr6["TotalQty"] = ProcessDetails.Tables[0].Rows[j]["TotalQty"].ToString();

                                    dr6["Qty"] = ProcessDetails.Tables[0].Rows[j]["Qty"].ToString();
                                    dr6["Definition"] = ProcessDetails.Tables[0].Rows[j]["Definition"].ToString();
                                    dr6["Serial_No"] = ProcessDetails.Tables[0].Rows[j]["Serial_No"].ToString();
                                    dr6["category"] = ProcessDetails.Tables[0].Rows[j]["category"].ToString();

                                    dr6["categoryid"] = ProcessDetails.Tables[0].Rows[j]["categoryid"].ToString();
                                    dr6["CategoryUserID"] = ProcessDetails.Tables[0].Rows[j]["CategoryUserID"].ToString();
                                }
                                if (k == 7)
                                {
                                    dr6["LotNo"] = ProcessDetails.Tables[0].Rows[j]["Lot"].ToString();
                                    dr6["FullSL"] = ProcessDetails.Tables[0].Rows[j]["FullSL"].ToString();
                                    dr6["HalfSL"] = ProcessDetails.Tables[0].Rows[j]["HalfSL"].ToString();
                                    dr6["TotalQty"] = ProcessDetails.Tables[0].Rows[j]["TotalQty"].ToString();

                                    dr6["Qty"] = ProcessDetails.Tables[0].Rows[j]["Qty"].ToString();
                                    dr6["Definition"] = ProcessDetails.Tables[0].Rows[j]["Definition"].ToString();
                                    dr6["Serial_No"] = ProcessDetails.Tables[0].Rows[j]["Serial_No"].ToString();
                                    dr6["category"] = ProcessDetails.Tables[0].Rows[j]["category"].ToString();

                                    dr6["categoryid"] = ProcessDetails.Tables[0].Rows[j]["categoryid"].ToString();
                                    dr6["CategoryUserID"] = ProcessDetails.Tables[0].Rows[j]["CategoryUserID"].ToString();
                                }
                                if (k == 8)
                                {
                                    dr6["LotNo"] = ProcessDetails.Tables[0].Rows[j]["Lot"].ToString();
                                    dr6["FullSL"] = ProcessDetails.Tables[0].Rows[j]["FullSL"].ToString();
                                    dr6["HalfSL"] = ProcessDetails.Tables[0].Rows[j]["HalfSL"].ToString();
                                    dr6["TotalQty"] = ProcessDetails.Tables[0].Rows[j]["TotalQty"].ToString();

                                    dr6["Qty"] = ProcessDetails.Tables[0].Rows[j]["Qty"].ToString();
                                    dr6["Definition"] = ProcessDetails.Tables[0].Rows[j]["Definition"].ToString();
                                    dr6["Serial_No"] = ProcessDetails.Tables[0].Rows[j]["Serial_No"].ToString();
                                    dr6["category"] = ProcessDetails.Tables[0].Rows[j]["category"].ToString();

                                    dr6["categoryid"] = ProcessDetails.Tables[0].Rows[j]["categoryid"].ToString();
                                    dr6["CategoryUserID"] = ProcessDetails.Tables[0].Rows[j]["CategoryUserID"].ToString();
                                }


                                if (k == 0)
                                {
                                    dsSTICHING.Tables[0].Rows.Add(dr1);
                                }
                                if (k == 1)
                                {
                                    dsKAJA.Tables[0].Rows.Add(dr2);
                                }
                                if (k == 2)
                                {
                                    dsWASHING.Tables[0].Rows.Add(dr3);
                                }

                                if (k == 3)
                                {
                                    dsPRINTING.Tables[0].Rows.Add(dr4);
                                }
                                if (k == 4)
                                {
                                    dsIRONING.Tables[0].Rows.Add(dr5);
                                }
                                if (k == 5)
                                {
                                    dsEMBROIDING.Tables[0].Rows.Add(dr6);
                                }

                                if (k == 6)
                                {
                                    dsbartag.Tables[0].Rows.Add(dr8);
                                }
                                if (k == 7)
                                {
                                    dstrimming.Tables[0].Rows.Add(dr9);
                                }
                                if (k == 8)
                                {
                                    dsconsai.Tables[0].Rows.Add(dr10);
                                }

                            }


                        }

                    }

                    gvstiching.DataSource = dsSTICHING;
                    gvstiching.DataBind();

                    gvkaja.DataSource = dsKAJA;
                    gvkaja.DataBind();

                    gvwashing.DataSource = dsWASHING;
                    gvwashing.DataBind();

                    gvprinting.DataSource = dsPRINTING;
                    gvprinting.DataBind();

                    gvironing.DataSource = dsIRONING;
                    gvironing.DataBind();

                    gvembroiding.DataSource = dsEMBROIDING;
                    gvembroiding.DataBind();

                    gvdbartag.DataSource = dsbartag;
                    gvdbartag.DataBind();
                    gvdtrimming.DataSource = dstrimming;
                    gvdtrimming.DataBind();
                    gvconsai.DataSource = dsconsai;
                    gvconsai.DataBind();


                }





                mpenew.Show();
            }
        }

        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string unit = e.Row.Cells[3].Text;

                foreach (TableCell gr in e.Row.Cells)
                {
                    if (unit == "UNIT 2")
                    {
                        gr.BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (unit == "UNIT 1")
                    {
                        gr.BackColor = System.Drawing.Color.Tomato;
                    }
                }
            }
        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            string button = string.Empty;
            button = Button3.Text;
            {
                button = Button3.Text;
                //Response.Redirect("categorymaster.aspx");
                Response.Redirect("Suppliermaster.aspx?name=" + button.ToString());
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "SupplierMaster_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = new DataSet();



            ds = objBs.selectcustomerDet(6, 2);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("CustomerName"));
                    dt.Columns.Add(new DataColumn("MobileNo"));
                    dt.Columns.Add(new DataColumn("Email"));
                    dt.Columns.Add(new DataColumn("Type"));
                    dt.Columns.Add(new DataColumn("Address"));
                    // dt.Columns.Add(new DataColumn("Area"));
                    //dt.Columns.Add(new DataColumn("City"));
                    dt.Columns.Add(new DataColumn("IsActive"));
                    dt.Columns.Add(new DataColumn("Open-Credit"));
                    dt.Columns.Add(new DataColumn("Open-Debit"));



                    //DataRow dr_export1 = dt.NewRow();
                    //dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["CustomerName"] = dr["LedgerName"];
                        dr_export["MobileNo"] = dr["MobileNo"];
                        dr_export["Email"] = dr["Email"];
                        dr_export["Type"] = dr["contacttypename"];
                        dr_export["Address"] = dr["Address"];
                        //dr_export["Area"] = dr["Area"];
                        //dr_export["City"] = dr["City"];
                        dr_export["IsActive"] = dr["IsActive"];
                        dr_export["Open-Credit"] = dr["Open_Credit"];
                        dr_export["Open-Debit"] = dr["Open_Depit"];
                        dt.Rows.Add(dr_export);
                    }

                    ExportToExcel(filename, dt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }
        }

        public void ExportToExcel(string filename, DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
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