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
    public partial class DespatchGrid : System.Web.UI.Page
    {
        DataSet ds1 = new DataSet();
        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        int TotalQty = 0;
        string Empid = "";
         string superadmin="";
        protected void Page_Load(object sender, EventArgs e)
        {
            sTableName = Session["User"].ToString();
            Empid = Session["Empid"].ToString();
             superadmin = Session["IsSuperAdmin"].ToString();
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

                    }
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dss = objBs.getdespatchdetails(drpbranch.SelectedValue);
                if (dss.Tables[0].Rows.Count > 0)
                {
                    gridcatqty.DataSource = dss;
                    gridcatqty.DataBind();
                }



            }
        }
        protected void drpbranch_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dss = objBs.getdespatchdetails(drpbranch.SelectedValue);
            if (dss.Tables[0].Rows.Count > 0)
            {
                gridcatqty.DataSource = dss;
                gridcatqty.DataBind();
            }

        }
        public void gvCustsales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("DespatchStock.aspx?DespatchId=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "print")
            {
                Response.Redirect("DespatchPrint.aspx?DSPid=" + e.CommandArgument.ToString());

                //DataSet dss = objBs.GETPRINTOFDESPATCH(Convert.ToInt32(e.CommandArgument.ToString()));
                //if (dss.Tables[0].Rows.Count > 0)
                //{
                //    string dcno = dss.Tables[0].Rows[0]["DcNo"].ToString();
                //    string dcdate = Convert.ToDateTime(dss.Tables[0].Rows[0]["DcDate"]).ToString("dd/MM/yyyy");
                //    string ledgername = dss.Tables[0].Rows[0]["ledgername"].ToString();
                //    string Narration = dss.Tables[0].Rows[0]["Narration"].ToString();
                //    string TotalQty = dss.Tables[0].Rows[0]["TotalQty"].ToString();
                //    string Customer = dss.Tables[0].Rows[0]["CustomerName"].ToString();

                //    gvprint.Caption = " DcNo :- " + dcno + " , " + " DcDate :- " + dcdate +  " <br />" + " Customer Name :- " + Customer + " , " + " Despatcher Name :- " +  ledgername + " <br />" + " Narration :- " + Narration + " , " + " TotalQty :- " + TotalQty + " <br /> " + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                //    gvprint.DataSource = dss;
                //    gvprint.DataBind();

                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Denomination", "Denomination();", true);
                //}

            }
            else if (e.CommandName == "Delete")
            {


                DataSet DsDespatchStock = objBs.geteditforDespatchStock(Convert.ToInt32(e.CommandArgument.ToString()));//Update Stock
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

                    int transsavedespatch = objBs.deletedespatchupstock(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(FinishedStockRatioId), Convert.ToInt32(S30FS), Convert.ToInt32(S32FS), Convert.ToInt32(S34FS), Convert.ToInt32(S36FS), Convert.ToInt32(SXSFS), Convert.ToInt32(SSFS), Convert.ToInt32(SMFS), Convert.ToInt32(SLFS), Convert.ToInt32(SXLFS), Convert.ToInt32(SXXLFS), Convert.ToInt32(S3XLFS), Convert.ToInt32(S4XLFS), Convert.ToInt32(S30HS), Convert.ToInt32(S32HS), Convert.ToInt32(S34HS), Convert.ToInt32(S36HS), Convert.ToInt32(SXSHS), Convert.ToInt32(SSHS), Convert.ToInt32(SMHS), Convert.ToInt32(SLHS), Convert.ToInt32(SXLHS), Convert.ToInt32(SXXLHS), Convert.ToInt32(S3XLHS), Convert.ToInt32(S4XLHS), Convert.ToInt32(TotalDespatchqty), Itemname, CompanyLotNo, DesignCode, Version, Convert.ToInt32(Empid), Convert.ToDouble(R30FS), Convert.ToDouble(R32FS), Convert.ToDouble(R34FS), Convert.ToDouble(R36FS), Convert.ToDouble(RXSFS), Convert.ToDouble(RSFS), Convert.ToDouble(RMFS), Convert.ToDouble(RLFS), Convert.ToDouble(RXLFS), Convert.ToDouble(RXXLFS), Convert.ToDouble(R3XLFS), Convert.ToDouble(R4XLFS), Convert.ToDouble(R30HS), Convert.ToDouble(R32HS), Convert.ToDouble(R34HS), Convert.ToDouble(R36HS), Convert.ToDouble(RXSHS), Convert.ToDouble(RSHS), Convert.ToDouble(RMHS), Convert.ToDouble(RLHS), Convert.ToDouble(RXLHS), Convert.ToDouble(RXXLHS), Convert.ToDouble(R3XLHS), Convert.ToDouble(R4XLHS), Convert.ToDouble(TotalDespatchAmt));
                }
                int deletetransdespatch = objBs.deletedespatch(Convert.ToInt32(e.CommandArgument.ToString()));

                Response.Redirect("DespatchGrid.aspx");
            }
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {


            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet dss = objBs.serchdeapatch(fromdate, todate,drpbranch.SelectedValue);
            if (dss.Tables[0].Rows.Count > 0)
            {
                string dcno = dss.Tables[0].Rows[0]["DcNo"].ToString();
                string dcdate = Convert.ToDateTime(dss.Tables[0].Rows[0]["DcDate"]).ToString("dd/MM/yyyy");
                string ledgername = dss.Tables[0].Rows[0]["ledgername"].ToString();
                string Narration = dss.Tables[0].Rows[0]["Narration"].ToString();
                string TotalQty = dss.Tables[0].Rows[0]["TotalQty"].ToString();
                string Customer = dss.Tables[0].Rows[0]["CustomerName"].ToString();

            }

            gridcatqty.Caption = " Despatch Detail Report From " + txtFromDate.Text + " To " + txtToDate.Text + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            gridcatqty.DataSource = dss;
            gridcatqty.DataBind();
        }
        protected void gridcatqty_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotalQty = TotalQty + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalQty"));

                if (superadmin == "1")
                {
                    ((System.Web.UI.WebControls.Image)e.Row.FindControl("Image1")).Visible = true;
                    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = false;

                    ((System.Web.UI.WebControls.Image)e.Row.FindControl("imged")).Visible = true;
                    ((ImageButton)e.Row.FindControl("imgdisableed")).Visible = false;
                }
                else
                {
                    ((System.Web.UI.WebControls.Image)e.Row.FindControl("Image1")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;

                    ((System.Web.UI.WebControls.Image)e.Row.FindControl("imged")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisableed")).Visible = true;
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total:";
                e.Row.Cells[5].Text = TotalQty.ToString();
                
            }
            #endregion
        }
    }
}