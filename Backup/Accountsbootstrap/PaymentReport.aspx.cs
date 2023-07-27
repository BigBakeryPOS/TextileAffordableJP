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
    public partial class PaymentReport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string Sort_Direction = "LedgerName ASC";
        string Sort_Direction1 = "Narration ASC";
        string sTableName = "";

        int TotalQuantity = 0; double TotalAmount = 0;

        double DetailBalance = 0; double DetailAdvance = 0; double DetailDebitAmount = 0; double DetailQuantity = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

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

                DataSet dst = objBs.Alljobworkmasterpayment();
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

                DataSet drpProcess = objBs.SelectAllProcessTypeLotProcess();
                if (drpProcess.Tables[0].Rows.Count > 0)
                {
                    DpProcess.DataSource = drpProcess;
                    DpProcess.DataTextField = "ProcessType";
                    DpProcess.DataValueField = "ProcessMasterID";
                    DpProcess.DataBind();
                    DpProcess.Items.Insert(0, "ALL");
                }

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();

                //DataSet ds = objBs.getpaymentReport(drpbranch.SelectedValue);

                //if (ds.Tables[0].Rows.Count > 0)
                //{

                //    PaymentGrid.DataSource = ds;
                //    PaymentGrid.DataBind();
                //}
                //else
                //{
                //    PaymentGrid.DataSource = null;
                //    PaymentGrid.DataBind();
                //}


            }
        }




        protected void btnsearch_Click(object sender, EventArgs e)
        {
            PaymentGrid.DataSource = null;
            PaymentGrid.DataBind();
            PaymentGrid2.DataSource = null;
            PaymentGrid2.DataBind();


            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds = new DataSet();
            if (ddltype.SelectedValue == "1")
            {
                 ds = objBs.getpaymentReportsrch(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue);

                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     PaymentGrid.DataSource = ds;
                     PaymentGrid.DataBind();
                 }
                 else
                 {
                     PaymentGrid.DataSource = null;
                     PaymentGrid.DataBind();
                 }

            }
            else
            {
               // ds = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue);


                DataSet dsdetails = new DataSet();

                if (DpProcess.SelectedValue == "2")
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "tblJpStiching", "StichingId");
                
                }
                else if (DpProcess.SelectedValue == "3")
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "tblJpEmbroiding", "EmbroidingId");
                 
                }
                else if (DpProcess.SelectedValue == "1")
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "tblJpKajaButton", "KajaButtonId");
                    
                }
                else if (DpProcess.SelectedValue == "4")
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "tblJpWashing", "WashingId");
                 
                }
                else if (DpProcess.SelectedValue == "7")
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "tblJpPrinting", "PrintingId");
                  
                }
                else if (DpProcess.SelectedValue == "5")
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "tblJpIroning", "IroningId");
                  
                }
                else if (DpProcess.SelectedValue == "8")
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "tblJpBarTag", "BarTagId");
                  
                }
                else if (DpProcess.SelectedValue == "9")
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "tblJpTrimming", "TrimmingId");
                   
                }
                else if (DpProcess.SelectedValue == "10")
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "tblJpConsai", "ConsaiId");
                   
                }
                else
                {
                    dsdetails = objBs.getpaymentReportsrchdetail(fromdate, todate, DpProcess.SelectedValue, drpbranch.SelectedValue, ddljobworker.SelectedValue, "", "");
                }
                if (dsdetails.Tables.Count > 0)
                {
                    if (dsdetails.Tables[0].Rows.Count > 0)
                    {
                        DataView DV = dsdetails.Tables[0].DefaultView;
                        DV.Sort = "PaymentNo desc";

                        PaymentGrid2.DataSource = DV;
                        PaymentGrid2.DataBind();
                    }
                    else
                    {
                        PaymentGrid2.DataSource = null;
                        PaymentGrid2.DataBind();
                    }
                }
                else
                {
                    PaymentGrid2.DataSource = null;
                    PaymentGrid2.DataBind();
                }
            }

           

        }

        protected void GVDespatchstock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotalQuantity = TotalQuantity + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                TotalAmount = TotalAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = TotalQuantity.ToString();
                e.Row.Cells[4].Text = TotalAmount.ToString();
            }
            #endregion
        }

        protected void GVDespatchstock_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            #region

         

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DetailQuantity = DetailQuantity + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));

                DetailBalance = DetailBalance + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Balance"));
                DetailAdvance = DetailAdvance + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Advance"));
                DetailDebitAmount = DetailDebitAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DebitAmount"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";

                e.Row.Cells[3].Text = DetailQuantity.ToString();
                e.Row.Cells[5].Text = DetailBalance.ToString("f2");
                e.Row.Cells[6].Text = DetailAdvance.ToString("f2");
                e.Row.Cells[7].Text = DetailDebitAmount.ToString("f2");

            }
            #endregion
        }

        protected void btnexp_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= PaymentReports.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            if (ddltype.SelectedValue == "1")
            {
                div2.RenderControl(htmlWrite);
            }
            else
            {
                div3.RenderControl(htmlWrite);
            }
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }


    }
}