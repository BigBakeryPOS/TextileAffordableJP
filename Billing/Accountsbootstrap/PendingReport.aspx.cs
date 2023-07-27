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
    public partial class PendingReport : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string empid = "";

        double Pendingqty = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string Stitchingid = string.Empty;
            string Requests = string.Empty;
            Stitchingid = Request.QueryString.Get("Stitchingid");


            Requests = Request.QueryString.Get("name");
            empid = Session["Empid"].ToString();

            if (!IsPostBack)
            {
                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                string super = Session["IsSuperAdmin"].ToString();
                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objbs.GetCompanyDet();
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
                    DataSet dbraqnch = objbs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();

                    }
                }
                DataSet dst = objbs.Getjobworkmastrr();
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

                DataSet dslot = objbs.getpendinglotforstitch(drpbranch.SelectedValue);
                if (dslot != null)
                {
                    if (dslot.Tables[0].Rows.Count > 0)
                    {
                        ddlLotNo.DataSource = dslot.Tables[0];
                        ddlLotNo.DataTextField = "CompanyLotNo";
                        ddlLotNo.DataValueField = "StichingId";
                        ddlLotNo.DataBind();
                        ddlLotNo.Items.Insert(0, "ALL");
                    }
                }

                DataSet ds = objbs.getdetailforpending(drpbranch.SelectedValue);
                gvpending.DataSource = ds;
                gvpending.DataBind();

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

        }
        protected void gvRowdatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Pendingqty = Pendingqty + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pendingqty"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total";

                e.Row.Cells[5].Text = Pendingqty.ToString();
              

            }
        }

        protected void btnexcel_OnClick(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet DSPendingProcess = new DataSet();
            #region
            if (ddlPieceType.SelectedValue == "1")
            {
                #region
                if (chkprocess.SelectedValue == "2")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpStiching", "tblTransJpStiching", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "3")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpEmbroiding", "tblTransJpEmbroiding", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "1")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpKajaButton", "tblTransJpKajaButton", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "7")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpPrinting", "tblTransJpPrinting", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "4")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpWashing", "tblTransJpWashing", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "8")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpBarTag", "tblTransJpBarTag", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "9")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpTrimming", "tblTransJpTrimming", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "10")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpConsai", "tblTransJpConsai", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);
                }
                else if (chkprocess.SelectedValue == "5")
                {
                    DSPendingProcess = objbs.getdetailforpendingalliron(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroning", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                #endregion
            }
            else if (ddlPieceType.SelectedValue == "2")
            {
                #region
                {

                    if (chkprocess.SelectedValue == "2")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpStiching", "tblTransJpStichinghistory", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "3")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpEmbroiding", "tblTransJpEmbroidinghistory", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "1")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpKajaButton", "tblTransJpKajaButtonhistory", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "7")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpPrinting", "tblTransJpPrintinghistory", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "4")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpWashing", "tblTransJpWashinghistory", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "8")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpBarTag", "tblTransJpBarTaghistory", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "9")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpTrimming", "tblTransJpTrimminghistory", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "10")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpConsai", "tblTransJpConsaihistory", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");
                    }
                    else if (chkprocess.SelectedValue == "5")
                    {
                        DSPendingProcess = objbs.getdetailforironhistory(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroninghistory", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }

                }
                #endregion

            }
            else if (ddlPieceType.SelectedValue == "3")
            {
                #region
                {

                    if (chkprocess.SelectedValue == "2")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpStiching", "tblTransJpStichinghistory", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "3")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpEmbroiding", "tblTransJpEmbroidinghistory", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "1")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpKajaButton", "tblTransJpKajaButtonhistory", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "7")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpPrinting", "tblTransJpPrintinghistory", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "4")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpWashing", "tblTransJpWashinghistory", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "8")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpBarTag", "tblTransJpBarTaghistory", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "9")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpTrimming", "tblTransJpTrimminghistory", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "10")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpConsai", "tblTransJpConsaihistory", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");
                    }
                    else if (chkprocess.SelectedValue == "5")
                    {
                        DSPendingProcess = objbs.getdetailforironhistory(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroninghistory", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }

                }
                #endregion

            }
            else if (ddlPieceType.SelectedValue == "4")
            {
                #region
                {

                    if (chkprocess.SelectedValue == "2")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpStiching", "tblTransJpStichinghistory", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "3")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpEmbroiding", "tblTransJpEmbroidinghistory", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "1")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpKajaButton", "tblTransJpKajaButtonhistory", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "7")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpPrinting", "tblTransJpPrintinghistory", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "4")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpWashing", "tblTransJpWashinghistory", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "8")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpBarTag", "tblTransJpBarTaghistory", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "9")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpTrimming", "tblTransJpTrimminghistory", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "10")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpConsai", "tblTransJpConsaihistory", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");
                    }
                    else if (chkprocess.SelectedValue == "5")
                    {
                       // DSPendingProcess = objbs.getdetailforironhistory(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroninghistory", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");
                        DSPendingProcess = objbs.getdetailforironhistoryiss(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroninghistory", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");
                    }

                }
                #endregion
            }
            #endregion

            if (DSPendingProcess != null)
            {
                if (DSPendingProcess.Tables[0].Rows.Count > 0)
                {
                    gvpending.DataSource = DSPendingProcess;
                    gvpending.DataBind();

                    #region

                    
                    int Pendingqty = 0;

                    DataSet ndstt = new DataSet();
                    DataTable ndttt = new DataTable();
                    DataColumn ndc = new DataColumn("JobWorker");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("Fit");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("CompanyLotNo");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("Date");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("Design");
                    ndttt.Columns.Add(ndc);

                    ndc = new DataColumn("Pending Qty");
                    ndttt.Columns.Add(ndc);
                    ndstt.Tables.Add(ndttt);
                    for (int i = 0; i < DSPendingProcess.Tables[0].Rows.Count; i++)
                    {

                        DataRow ndrd = ndstt.Tables[0].NewRow();
                        ndrd["JobWorker"] = DSPendingProcess.Tables[0].Rows[i]["LedgerName"].ToString();
                        ndrd["Fit"] = DSPendingProcess.Tables[0].Rows[i]["Fit"].ToString();
                        ndrd["CompanyLotNo"] = DSPendingProcess.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                        ndrd["Date"] = Convert.ToDateTime(DSPendingProcess.Tables[0].Rows[i]["Date"]).ToString("dd/MM/yyyy");
                        ndrd["Design"] = DSPendingProcess.Tables[0].Rows[i]["ItemName"].ToString();
                        ndrd["Pending Qty"] = DSPendingProcess.Tables[0].Rows[i]["Pendingqty"].ToString();
                        Pendingqty += Convert.ToInt32(DSPendingProcess.Tables[0].Rows[i]["Pendingqty"].ToString());

                        ndstt.Tables[0].Rows.Add(ndrd);
                    }

                    DataRow ndrd1 = ndstt.Tables[0].NewRow();
                    ndrd1["JobWorker"] = "";
                    ndrd1["Fit"] = "";
                    ndrd1["CompanyLotNo"] = "";
                    ndrd1["Date"] = "";
                    ndrd1["Design"] = "Total"; 
                    ndrd1["Pending Qty"] = Pendingqty.ToString();
                    ndstt.Tables[0].Rows.Add(ndrd1);

                    #endregion

                    ExportToExcel(ndstt.Tables[0]);
                }
                else
                {
                    gvpending.DataSource = null;
                    gvpending.DataBind();
                }
            }
        }

        public void ExportToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                string filename = "";

                #region
                if (ddlPieceType.SelectedValue == "1")
                {
                    #region
                    {
                        if (chkprocess.SelectedValue == "2")
                        {
                            filename = " Pending Details for Stitching_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "3")
                        {
                            filename = " Pending Details for Embroidery" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "1")
                        {
                            filename = " Pending Details for KajaButton" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "7")
                        {
                            filename = " Pending Details for Printing_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "4")
                        {
                            filename = " Pending Details for Washing_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "8")
                        {
                            filename = " Pending Details for Bartag" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "9")
                        {
                            filename = " Pending Details for Trimming" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "10")
                        {
                            filename = " Pending Details for Consai" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "5")
                        {
                            filename = " Pending Details for Ironing_" + DateTime.Now.ToString() + ".xls";
                        }

                    }
                    #endregion
                }
                else if (ddlPieceType.SelectedValue == "2")
                {
                    #region
                    {
                        if (chkprocess.SelectedValue == "2")
                        {
                            filename = " Received Details for Stitching_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "3")
                        {
                            filename = " Received Details for Embroidery" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "1")
                        {
                            filename = " Received Details for KajaButton" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "7")
                        {
                            filename = " Received Details for Printing_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "4")
                        {
                            filename = " Received Details for Washing_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "8")
                        {
                            filename = " Received Details for Bartag" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "9")
                        {
                            filename = " Received Details for Trimming" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "10")
                        {
                            filename = " Received Details for Consai" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "5")
                        {
                            filename = " Received Details for Ironing_" + DateTime.Now.ToString() + ".xls";
                        }

                    }
                    #endregion
                }
                else if (ddlPieceType.SelectedValue == "3")
                {
                    #region
                    {
                        if (chkprocess.SelectedValue == "2")
                        {
                            filename = " Damage Details for Stitching_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "3")
                        {
                            filename = " Damage Details for Embroidery" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "1")
                        {
                            filename = " Damage Details for KajaButton" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "7")
                        {
                            filename = " Damage Details for Printing_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "4")
                        {
                            filename = " Damage Details for Washing_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "8")
                        {
                            filename = " Damage Details for Bartag" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "9")
                        {
                            filename = " Damage Details for Trimming" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "10")
                        {
                            filename = " Damage Details for Consai" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "5")
                        {
                            filename = " Damage Details for Ironing_" + DateTime.Now.ToString() + ".xls";
                        }

                    }
                    #endregion
                }
                else if (ddlPieceType.SelectedValue == "4")
                {
                    #region
                    {
                        if (chkprocess.SelectedValue == "2")
                        {
                            filename = " Issue Details for Stitching_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "3")
                        {
                            filename = " Issue Details for Embroidery" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "1")
                        {
                            filename = " Issue Details for KajaButton" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "7")
                        {
                            filename = " Issue Details for Printing_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "4")
                        {
                            filename = " Issue Details for Washing_" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "8")
                        {
                            filename = " Issue Details for Bartag" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "9")
                        {
                            filename = " Issue Details for Trimming" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "10")
                        {
                            filename = " Issue Details for Consai" + DateTime.Now.ToString() + ".xls";
                        }
                        else if (chkprocess.SelectedValue == "5")
                        {
                            filename = " Issue Details for Ironing_" + DateTime.Now.ToString() + ".xls";
                        }

                    }
                    #endregion
                }
                #endregion
               
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

        protected void ddlmode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           

        }
        protected void chkprocess_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           

            
            string Processid = "";
            DataSet ds2 = new DataSet();

            #region
                if (chkprocess.SelectedValue == "2")
                {

                    ds2 = objbs.CHKproceessall("tblJpStiching", "tblTransJpStiching", "StichingId", drpbranch.SelectedValue);
                    Processid = "StichingId";
                }
                else if (chkprocess.SelectedValue == "3")
                {
                    ds2 = objbs.CHKproceessall("tblJpEmbroiding", "tblTransJpEmbroiding", "EmbroidingId", drpbranch.SelectedValue);
                    Processid = "EmbroidingId";
                }
                else if (chkprocess.SelectedValue == "1")
                {
                    ds2 = objbs.CHKproceessall("tblJpKajaButton", "tblTransJpKajaButton", "KajaButtonId", drpbranch.SelectedValue);
                    Processid = "KajaButtonId";
                }
                else if (chkprocess.SelectedValue == "7")
                {
                    ds2 = objbs.CHKproceessall("tblJpPrinting", "tblTransJpPrinting", "PrintingId", drpbranch.SelectedValue);
                    Processid = "PrintingId";
                }
                else if (chkprocess.SelectedValue == "4")
                {
                    ds2 = objbs.CHKproceessall("tblJpWashing", "tblTransJpWashing", "WashingId", drpbranch.SelectedValue);
                    Processid = "WashingId";
                }
                else if (chkprocess.SelectedValue == "8")
                {
                    ds2 = objbs.CHKproceessall("tblJpBarTag", "tblTransJpBarTag", "BarTagId", drpbranch.SelectedValue);
                    Processid = "BarTagId";
                }
                else if (chkprocess.SelectedValue == "9")
                {
                    ds2 = objbs.CHKproceessall("tblJpTrimming", "tblTransJpTrimming", "TrimmingId", drpbranch.SelectedValue);
                    Processid = "TrimmingId";
                }
                else if (chkprocess.SelectedValue == "10")
                {
                    ds2 = objbs.CHKproceessall("tblJpConsai", "tblTransJpConsai", "ConsaiId", drpbranch.SelectedValue);
                    Processid = "ConsaiId";
                }
                else if (chkprocess.SelectedValue == "5")
                {
                    ds2 = objbs.CHKproceessall("tblJpIroning", "tblTransJpIroning", "IroningId", drpbranch.SelectedValue);
                    Processid = "IroningId";
                }
             
            


            #endregion

            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlLotNo.DataSource = ds2.Tables[0];
                ddlLotNo.DataTextField = "CompanyLotNo";
                ddlLotNo.DataValueField = Processid;
                ddlLotNo.DataBind();
                ddlLotNo.Items.Insert(0, "ALL");

            }
            else
            {
                ddlLotNo.Items.Insert(0, "Select LotNo");
            }
          
        }

        protected void ddlLotNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlLotNo.SelectedValue != "Select LotNo")
            {
                string Processid = "";
                DataSet ds2 = new DataSet();
                #region
                if (chkprocess.SelectedValue == "2")
                {
                    ds2 = objbs.gettingindiutialproceessall("tblJpStiching", "tblTransJpStiching", "StichingId", ddlLotNo.SelectedValue);
                    Processid = "StichingId";
                }
                else if (chkprocess.SelectedValue == "3")
                {
                    ds2 = objbs.gettingindiutialproceessall("tblJpEmbroiding", "tblTransJpEmbroiding", "EmbroidingId", ddlLotNo.SelectedValue);
                    Processid = "EmbroidingId";
                }
                else if (chkprocess.SelectedValue == "1")
                {
                    ds2 = objbs.gettingindiutialproceessall("tblJpKajaButton", "tblTransJpKajaButton", "KajaButtonId", ddlLotNo.SelectedValue);
                    Processid = "KajaButtonId";
                }
                else if (chkprocess.SelectedValue == "7")
                {
                    ds2 = objbs.gettingindiutialproceessall("tblJpPrinting", "tblTransJpPrinting", "PrintingId", ddlLotNo.SelectedValue);
                    Processid = "PrintingId";
                }
                else if (chkprocess.SelectedValue == "4")
                {
                    ds2 = objbs.gettingindiutialproceessall("tblJpWashing", "tblTransJpWashing", "WashingId", ddlLotNo.SelectedValue);
                    Processid = "WashingId";
                }
                else if (chkprocess.SelectedValue == "8")
                {
                    ds2 = objbs.gettingindiutialproceessall("tblJpBarTag", "tblTransJpBarTag", "BarTagId", ddlLotNo.SelectedValue);
                    Processid = "BarTagId";
                }
                else if (chkprocess.SelectedValue == "9")
                {
                    ds2 = objbs.gettingindiutialproceessall("tblJpTrimming", "tblTransJpTrimming", "TrimmingId", ddlLotNo.SelectedValue);
                    Processid = "TrimmingId";
                }
                else if (chkprocess.SelectedValue == "10")
                {
                    ds2 = objbs.gettingindiutialproceessall("tblJpConsai", "tblTransJpConsai", "ConsaiId", ddlLotNo.SelectedValue);
                    Processid = "ConsaiId";
                }
                else if (chkprocess.SelectedValue == "5")
                {
                    ds2 = objbs.gettingindiutialproceessall("tblJpIroning", "tblTransJpIroning", "IroningId", ddlLotNo.SelectedValue);
                    Processid = "IroningId";
                }
                #endregion
            }
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //if (drpbranch.SelectedValue == "Select Branch")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you !!!');", true);
            //    return;
            //}
            //if (chkprocess.SelectedValue == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Process. Thank you !!!');", true);
            //    return;
            //}

            DataSet DSPendingProcess = new DataSet();
            #region
            if (ddlPieceType.SelectedValue == "1")
            {
                #region
                if (chkprocess.SelectedValue == "2")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpStiching", "tblTransJpStiching", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "3")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpEmbroiding", "tblTransJpEmbroiding", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "1")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpKajaButton", "tblTransJpKajaButton", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "7")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpPrinting", "tblTransJpPrinting", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "4")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpWashing", "tblTransJpWashing", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "8")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpBarTag", "tblTransJpBarTag", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "9")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpTrimming", "tblTransJpTrimming", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                else if (chkprocess.SelectedValue == "10")
                {
                    DSPendingProcess = objbs.getdetailforpendingall(ddltype.SelectedValue, "tblJpConsai", "tblTransJpConsai", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);
                }
                else if (chkprocess.SelectedValue == "5")
                {
                    DSPendingProcess = objbs.getdetailforpendingalliron(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroning", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text);

                }
                #endregion
            }
            else if (ddlPieceType.SelectedValue == "2")
            {
                #region
                {

                    if (chkprocess.SelectedValue == "2")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpStiching", "tblTransJpStichinghistory", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "3")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpEmbroiding", "tblTransJpEmbroidinghistory", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "1")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpKajaButton", "tblTransJpKajaButtonhistory", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "7")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpPrinting", "tblTransJpPrintinghistory", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "4")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpWashing", "tblTransJpWashinghistory", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "8")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpBarTag", "tblTransJpBarTaghistory", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "9")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpTrimming", "tblTransJpTrimminghistory", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }
                    else if (chkprocess.SelectedValue == "10")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpConsai", "tblTransJpConsaihistory", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");
                    }
                    else if (chkprocess.SelectedValue == "5")
                    {
                        DSPendingProcess = objbs.getdetailforironhistory(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroninghistory", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Receive");

                    }

                }
                #endregion

            }
            else if (ddlPieceType.SelectedValue == "3")
            {
                #region
                {

                    if (chkprocess.SelectedValue == "2")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpStiching", "tblTransJpStichinghistory", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "3")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpEmbroiding", "tblTransJpEmbroidinghistory", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "1")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpKajaButton", "tblTransJpKajaButtonhistory", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "7")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpPrinting", "tblTransJpPrintinghistory", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "4")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpWashing", "tblTransJpWashinghistory", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "8")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpBarTag", "tblTransJpBarTaghistory", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "9")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpTrimming", "tblTransJpTrimminghistory", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }
                    else if (chkprocess.SelectedValue == "10")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpConsai", "tblTransJpConsaihistory", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");
                    }
                    else if (chkprocess.SelectedValue == "5")
                    {
                        DSPendingProcess = objbs.getdetailforironhistory(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroninghistory", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Damage");

                    }

                }
                #endregion

            }
            else if (ddlPieceType.SelectedValue == "4")
            {
                #region
                {

                    if (chkprocess.SelectedValue == "2")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpStiching", "tblTransJpStichinghistory", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "3")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpEmbroiding", "tblTransJpEmbroidinghistory", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "1")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpKajaButton", "tblTransJpKajaButtonhistory", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "7")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpPrinting", "tblTransJpPrintinghistory", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "4")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpWashing", "tblTransJpWashinghistory", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "8")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpBarTag", "tblTransJpBarTaghistory", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "9")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpTrimming", "tblTransJpTrimminghistory", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");

                    }
                    else if (chkprocess.SelectedValue == "10")
                    {
                        DSPendingProcess = objbs.getdetailforrechistory(ddltype.SelectedValue, "tblJpConsai", "tblTransJpConsaihistory", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");
                    }
                    else if (chkprocess.SelectedValue == "5")
                    {
                        //DSPendingProcess = objbs.getdetailforironhistory(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroninghistory", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");
                        DSPendingProcess = objbs.getdetailforironhistoryiss(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroninghistory", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Issue");
                    }

                }
                #endregion
            }
            else if (ddlPieceType.SelectedValue == "5")
            {

                DSPendingProcess = objbs.getdetailforironhistory(ddltype.SelectedValue, "tblJpIroning", "tblTransJpIroninghistory", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, fromDate, toDate, ddljobworker.SelectedValue, chkprocess.SelectedValue, drpbranch.SelectedValue, txtsearchfabtype.Text, "Alter");

            }
            #endregion


            if (DSPendingProcess.Tables[0].Rows.Count > 0)
            {
                gvpending.DataSource = DSPendingProcess;
                gvpending.DataBind();
            }
            else
            {
                gvpending.DataSource = null;
                gvpending.DataBind();
            }
          

          
        }

       

    }
}

