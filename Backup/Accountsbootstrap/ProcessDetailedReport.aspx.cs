using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class ProcessDetailedReport : System.Web.UI.Page
    {
        string sTableName = "";
        BSClass objbs = new BSClass();

        string brach = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet drpEmpp = objbs.Selectname("10");
                if (drpEmpp.Tables[0].Rows.Count > 0)
                {
                    ddljobwork.DataSource = drpEmpp;
                    ddljobwork.DataTextField = "LedgerName";
                    ddljobwork.DataValueField = "LedgerID";
                    ddljobwork.DataBind();
                    ddljobwork.Items.Insert(0, "All");
                }

                DataSet dscutnos = objbs.Getcutnos();
                if (dscutnos.Tables[0].Rows.Count > 0)
                {
                    ddllotno.DataSource = dscutnos;
                    ddllotno.DataTextField = "CompanyFullLotNo";
                    ddllotno.DataValueField = "Cutid";
                    ddllotno.DataBind();
                    ddllotno.Items.Insert(0, "All");
                }



            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }


        protected void Serachclick(object sender, EventArgs e)
        {
            if (txtfromdate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select From-Date.Thank You!!!');", true);
                return;
            }
            if (txttodate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select To-Date.Thank You!!!');", true);
                return;
            }
            if (ddlprocesstype.SelectedValue == "0" || ddlprocesstype.SelectedValue == "" || ddlprocesstype.SelectedValue == "Select Process")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Process.Thank You!!!');", true);
                return;
            }

            DateTime fromdate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dsDetails = new DataSet();

            if (ddlprocesstype.SelectedValue == "1")
            {
                dsDetails = objbs.JobworkDetailsReortMain(fromdate, todate, "tbljpstiching", "tbltransjpstichinghistory", "stichingid", ddljobwork.SelectedValue, ddllotno.SelectedItem.Text);
                gvCustsales.Caption = "Stitching Process";
            }
            else if (ddlprocesstype.SelectedValue == "2")
            {
                dsDetails = objbs.JobworkDetailsReortMain(fromdate, todate, "tblJpEmbroiding", "tbltransjpEmbroidinghistory", "EmbroidingId", ddljobwork.SelectedValue, ddllotno.SelectedItem.Text);
                gvCustsales.Caption = "Embroiding Process";
            }
            else if (ddlprocesstype.SelectedValue == "3")
            {
                dsDetails = objbs.JobworkDetailsReortMain(fromdate, todate, "tblJpPrinting", "tbltransjpPrintinghistory", "PrintingId", ddljobwork.SelectedValue, ddllotno.SelectedItem.Text);
                gvCustsales.Caption = "Printing Process";
            }
            else if (ddlprocesstype.SelectedValue == "4")
            {
                dsDetails = objbs.JobworkDetailsReortMainiron(fromdate, todate, "tblJpIroning", "tbltransjpIroninghistory  ", "IroningId", ddljobwork.SelectedValue, ddllotno.SelectedItem.Text);
                gvCustsales.Caption = "Ironing Process";
            }

            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                gvCustsales.DataSource = dsDetails;
                gvCustsales.DataBind();
            }
            else
            {
                gvCustsales.DataSource = null;
                gvCustsales.DataBind();
            }


            //DataSet dss = objbs.JobworkDetailsReort(fromdate, todate, ddljobwork.SelectedValue, ddllotno.SelectedValue);
            //if (dss.Tables[0].Rows.Count > 0)
            //{

            //    gvCustsales.DataSource = dss;
            //    gvCustsales.DataBind();
            //}
            //else
            //{
            //    gvCustsales.DataSource = null;
            //    gvCustsales.DataBind();
            //}

        }


        public void gvCustsales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime fromdate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region

                GridView gv = e.Row.FindControl("gvLiaLedger") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    string argValue = gvGroup.DataKeys[e.Row.RowIndex].Value.ToString();

                    string[] arg = argValue.Split(',');
                    string lotNo = arg[0];
                    string ID = arg[1];

                    DataSet dsDetails = new DataSet();

                    if (ddlprocesstype.SelectedValue == "1")
                    {
                        dsDetails = objbs.JobworkDetailsReortMainSub(fromdate, todate, "tbljpstiching", "tbltransjpstichinghistory", "stichingid", ddljobwork.SelectedValue, lotNo, Convert.ToInt32(ID));
                        //////gvCustsales.Caption = "Stitching Process";
                    }
                    else if (ddlprocesstype.SelectedValue == "2")
                    {
                        dsDetails = objbs.JobworkDetailsReortMainSub(fromdate, todate, "tblJpEmbroiding", "tbltransjpEmbroidinghistory", "EmbroidingId", ddljobwork.SelectedValue, lotNo, Convert.ToInt32(ID));
                        //////gvCustsales.Caption = "Embroiding Process";
                    }
                    else if (ddlprocesstype.SelectedValue == "3")
                    {
                        dsDetails = objbs.JobworkDetailsReortMainSub(fromdate, todate, "tblJpPrinting", "tbltransjpPrintinghistory", "PrintingId", ddljobwork.SelectedValue, lotNo, Convert.ToInt32(ID));
                        //////gvCustsales.Caption = "Printing Process";
                    }
                    else if (ddlprocesstype.SelectedValue == "4")
                    {
                        dsDetails = objbs.JobworkDetailsReortMainSubiron(fromdate, todate, "tblJpIroning", "tbltransjpIroninghistory  ", "IroningId", ddljobwork.SelectedValue, lotNo, Convert.ToInt32(ID));
                        //////gvCustsales.Caption = "Ironing Process";
                    }

                    if (dsDetails.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = dsDetails;
                        gv.DataBind();

                    }
                }

                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }

        }


        protected void btnexp_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= FabricReceiveDetails.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div2.RenderControl(htmlWrite);
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