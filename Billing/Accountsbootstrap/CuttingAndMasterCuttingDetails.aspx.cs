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
    public partial class CuttingAndMasterCuttingDetails : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double ttlissuemtr = 0; double ttlActualMeter = 0;
        double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;
        DataRow[] rows;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dst = objBs.Getjobworker();
                if (dst.Tables[0].Rows.Count > 0)
                {
                    ddlsupplier.DataSource = dst.Tables[0];
                    ddlsupplier.DataTextField = "LedgerName";
                    ddlsupplier.DataValueField = "LedgerID";
                    ddlsupplier.DataBind();
                    ddlsupplier.Items.Insert(0, "All");
                }
                DataSet dsItem = objBs.Getitem();
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlitem.DataSource = dsItem.Tables[0];
                    ddlitem.DataTextField = "ItemCode";
                    ddlitem.DataValueField = "Itemid";
                    ddlitem.DataBind();
                    ddlitem.Items.Insert(0, "All");
                }

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
                        drpbranch.Items.Insert(0, "All");
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

            }
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {
            gvcust.DataSource = null;
            gvcust.DataBind();

            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dstd = new DataSet();

            string Caption = "";
            if (ddltype.SelectedValue == "1")
            {
                if (ddlcutting.SelectedValue == "1")
                {
                    Caption = "Pre Cutting Detailed Report";
                    dstd = objBs.precuttingsummaryReport(drpbranch.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue, ddlitem.SelectedValue);

                }
                else
                {
                    Caption = "Master Cutting Detailed Report";
                    dstd = objBs.mastercuttingsummaryReport(drpbranch.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue, ddlitem.SelectedValue);
                }

            }
            else
            {
                if (ddlcutting.SelectedValue == "1")
                {
                    Caption = "Pre Cutting Summary Report";
                    dstd = objBs.precuttingdetailedReport(drpbranch.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue, ddlitem.SelectedValue);
                }
                else
                {
                    Caption = "Master Cutting Summary Report";
                    dstd = objBs.mastercuttingdetailedReport(drpbranch.SelectedValue, fromdate, todate, ddlsupplier.SelectedValue, ddlitem.SelectedValue);
                }

            }
            if (dstd.Tables[0].Rows.Count > 0)
            {
                gvcust.Caption = Caption;
                gvcust.DataSource = dstd;
                gvcust.DataBind();

                #region

                gvcust.Columns[11].Visible = false;
                gvcust.Columns[12].Visible = false;
                gvcust.Columns[13].Visible = false;
                gvcust.Columns[14].Visible = false;
                gvcust.Columns[15].Visible = false;
                gvcust.Columns[16].Visible = false;
                gvcust.Columns[17].Visible = false;
                gvcust.Columns[18].Visible = false;
                gvcust.Columns[19].Visible = false;
                gvcust.Columns[20].Visible = false;
                gvcust.Columns[21].Visible = false;
                gvcust.Columns[22].Visible = false;
                gvcust.Columns[23].Visible = false;
                gvcust.Columns[24].Visible = false;
                gvcust.Columns[25].Visible = false;
                gvcust.Columns[26].Visible = false;
                gvcust.Columns[27].Visible = false;
                gvcust.Columns[28].Visible = false;
                gvcust.Columns[29].Visible = false;
                gvcust.Columns[30].Visible = false;
                gvcust.Columns[31].Visible = false;
                gvcust.Columns[32].Visible = false;
                gvcust.Columns[33].Visible = false;
                gvcust.Columns[34].Visible = false;

                #endregion

                for (int j = 0; j < dstd.Tables[0].Rows.Count; j++)
                {
                    #region
                    string FS30 = dstd.Tables[0].Rows[j]["F30"].ToString();
                    string FS32 = dstd.Tables[0].Rows[j]["F32"].ToString();
                    string FS34 = dstd.Tables[0].Rows[j]["F34"].ToString();
                    string FS36 = dstd.Tables[0].Rows[j]["F36"].ToString();
                    string FSXS = dstd.Tables[0].Rows[j]["Fxs"].ToString();
                    string FSS = dstd.Tables[0].Rows[j]["Fs"].ToString();
                    string FSM = dstd.Tables[0].Rows[j]["Fm"].ToString();
                    string FSL = dstd.Tables[0].Rows[j]["Fl"].ToString();
                    string FSXL = dstd.Tables[0].Rows[j]["Fxl"].ToString();
                    string FSXXL = dstd.Tables[0].Rows[j]["Fxxl"].ToString();
                    string FS3XL = dstd.Tables[0].Rows[j]["F3xl"].ToString();
                    string FS4XL = dstd.Tables[0].Rows[j]["F4xl"].ToString();
                    string HS30 = dstd.Tables[0].Rows[j]["H30"].ToString();
                    string HS32 = dstd.Tables[0].Rows[j]["H32"].ToString();
                    string HS34 = dstd.Tables[0].Rows[j]["H34"].ToString();
                    string HS36 = dstd.Tables[0].Rows[j]["H36"].ToString();
                    string HSXS = dstd.Tables[0].Rows[j]["Hxs"].ToString();
                    string HSS = dstd.Tables[0].Rows[j]["Hs"].ToString();
                    string HSM = dstd.Tables[0].Rows[j]["Hm"].ToString();
                    string HSL = dstd.Tables[0].Rows[j]["Hl"].ToString();
                    string HSXL = dstd.Tables[0].Rows[j]["Hxl"].ToString();
                    string HSXXL = dstd.Tables[0].Rows[j]["Hxxl"].ToString();
                    string HS3XL = dstd.Tables[0].Rows[j]["H3xl"].ToString();
                    string HS4XL = dstd.Tables[0].Rows[j]["H4xl"].ToString();


                    #endregion

                    #region

                    if (FS30 != "0")
                    {
                        gvcust.Columns[11].Visible = true;
                    }
                    if (FS32 != "0")
                    {
                        gvcust.Columns[12].Visible = true;
                    }
                    if (FS34 != "0")
                    {
                        gvcust.Columns[13].Visible = true;
                    }
                    if (FS36 != "0")
                    {
                        gvcust.Columns[14].Visible = true;
                    }
                    if (FSXS != "0")
                    {
                        gvcust.Columns[15].Visible = true;
                    }
                    if (FSS != "0")
                    {
                        gvcust.Columns[16].Visible = true;
                    }
                    if (FSM != "0")
                    {
                        gvcust.Columns[17].Visible = true;
                    }
                    if (FSL != "0")
                    {
                        gvcust.Columns[18].Visible = true;
                    }
                    if (FSXL != "0")
                    {
                        gvcust.Columns[19].Visible = true;
                    }
                    if (FSXXL != "0")
                    {
                        gvcust.Columns[20].Visible = true;
                    }
                    if (FS3XL != "0")
                    {
                        gvcust.Columns[21].Visible = true;
                    }
                    if (FS4XL != "0")
                    {
                        gvcust.Columns[22].Visible = true;
                    }



                    if (HS30 != "0")
                    {
                        gvcust.Columns[23].Visible = true;
                    }
                    if (HS32 != "0")
                    {
                        gvcust.Columns[24].Visible = true;
                    }
                    if (HS34 != "0")
                    {
                        gvcust.Columns[25].Visible = true;
                    }
                    if (HS36 != "0")
                    {
                        gvcust.Columns[26].Visible = true;
                    }
                    if (HSXS != "0")
                    {
                        gvcust.Columns[27].Visible = true;
                    }
                    if (HSS != "0")
                    {
                        gvcust.Columns[28].Visible = true;
                    }
                    if (HSM != "0")
                    {
                        gvcust.Columns[29].Visible = true;
                    }
                    if (HSL != "0")
                    {
                        gvcust.Columns[30].Visible = true;
                    }
                    if (HSXL != "0")
                    {
                        gvcust.Columns[31].Visible = true;
                    }
                    if (HSXXL != "0")
                    {
                        gvcust.Columns[32].Visible = true;
                    }
                    if (HS3XL != "0")
                    {
                        gvcust.Columns[33].Visible = true;
                    }
                    if (HS4XL != "0")
                    {
                        gvcust.Columns[34].Visible = true;
                    }
                    #endregion

                }
            }
            else
            {

                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region



                F30 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "F30"));
                F32 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "F32"));
                F34 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "F34"));
                F36 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "F36"));
                FXS += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FXS"));
                FS += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FS"));
                FM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FM"));
                FL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FL"));
                FXL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FXL"));
                FXXL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FXXl"));
                F3XL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "F3Xl"));
                F4XL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "F4XL"));

                H30 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "H30"));
                H32 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "H32"));
                H34 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "H34"));
                H36 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "H36"));
                HXS += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HXS"));
                HS += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HS"));
                HM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HM"));
                HL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HL"));
                HXL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HXL"));
                HXXL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HXXl"));
                H3XL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "H3Xl"));
                H4XL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "H4XL"));

                TOTAL += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalQty"));

                #endregion

                #region


                if (e.Row.Cells[11].Text == "0")
                {
                    e.Row.Cells[11].Text = "-";
                }
                if (e.Row.Cells[12].Text == "0")
                {
                    e.Row.Cells[12].Text = "-";
                }
                if (e.Row.Cells[13].Text == "0")
                {
                    e.Row.Cells[13].Text = "-";
                }
                if (e.Row.Cells[14].Text == "0")
                {
                    e.Row.Cells[14].Text = "-";
                }
                if (e.Row.Cells[15].Text == "0")
                {
                    e.Row.Cells[15].Text = "-";
                }
                if (e.Row.Cells[16].Text == "0")
                {
                    e.Row.Cells[16].Text = "-";
                }
                if (e.Row.Cells[17].Text == "0")
                {
                    e.Row.Cells[17].Text = "-";
                }
                if (e.Row.Cells[18].Text == "0")
                {
                    e.Row.Cells[18].Text = "-";
                }
                if (e.Row.Cells[19].Text == "0")
                {
                    e.Row.Cells[19].Text = "-";
                }
                if (e.Row.Cells[20].Text == "0")
                {
                    e.Row.Cells[20].Text = "-";
                }
                if (e.Row.Cells[21].Text == "0")
                {
                    e.Row.Cells[21].Text = "-";
                }
                if (e.Row.Cells[22].Text == "0")
                {
                    e.Row.Cells[22].Text = "-";
                }
                if (e.Row.Cells[23].Text == "0")
                {
                    e.Row.Cells[23].Text = "-";
                }
                if (e.Row.Cells[24].Text == "0")
                {
                    e.Row.Cells[24].Text = "-";
                }
                if (e.Row.Cells[25].Text == "0")
                {
                    e.Row.Cells[25].Text = "-";
                }
                if (e.Row.Cells[26].Text == "0")
                {
                    e.Row.Cells[26].Text = "-";
                }
                if (e.Row.Cells[27].Text == "0")
                {
                    e.Row.Cells[27].Text = "-";
                }
                if (e.Row.Cells[28].Text == "0")
                {
                    e.Row.Cells[28].Text = "-";
                }
                if (e.Row.Cells[29].Text == "0")
                {
                    e.Row.Cells[29].Text = "-";
                }
                if (e.Row.Cells[30].Text == "0")
                {
                    e.Row.Cells[30].Text = "-";
                }
                if (e.Row.Cells[31].Text == "0")
                {
                    e.Row.Cells[31].Text = "-";
                }
                if (e.Row.Cells[32].Text == "0")
                {
                    e.Row.Cells[32].Text = "-";
                }
                if (e.Row.Cells[33].Text == "0")
                {
                    e.Row.Cells[33].Text = "-";
                }
                if (e.Row.Cells[34].Text == "0")
                {
                    e.Row.Cells[34].Text = "-";
                }
                #endregion

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                #region
                e.Row.Cells[11].Text = F30.ToString();
                e.Row.Cells[12].Text = F32.ToString();
                e.Row.Cells[13].Text = F34.ToString();
                e.Row.Cells[14].Text = F36.ToString();
                e.Row.Cells[15].Text = FXS.ToString();
                e.Row.Cells[16].Text = FS.ToString();
                e.Row.Cells[17].Text = FM.ToString();
                e.Row.Cells[18].Text = FL.ToString();
                e.Row.Cells[19].Text = FXL.ToString();
                e.Row.Cells[20].Text = FXXL.ToString();
                e.Row.Cells[21].Text = F3XL.ToString();
                e.Row.Cells[22].Text = F4XL.ToString();

                e.Row.Cells[23].Text = H30.ToString();
                e.Row.Cells[24].Text = H32.ToString();
                e.Row.Cells[25].Text = H34.ToString();
                e.Row.Cells[26].Text = H36.ToString();
                e.Row.Cells[27].Text = HXS.ToString();
                e.Row.Cells[28].Text = HS.ToString();
                e.Row.Cells[29].Text = HM.ToString();
                e.Row.Cells[30].Text = HL.ToString();
                e.Row.Cells[31].Text = HXL.ToString();
                e.Row.Cells[32].Text = HXXL.ToString();
                e.Row.Cells[33].Text = H3XL.ToString();
                e.Row.Cells[34].Text = H4XL.ToString();

                e.Row.Cells[35].Text = TOTAL.ToString();



                #endregion
            }
        }

        protected void btnexcel_OnClick(object sender, EventArgs e)
        {
            string Caption = "";
            if (ddltype.SelectedValue == "1")
            {
                if (ddlcutting.SelectedValue == "1")
                {
                    Caption = "PreCuttingDetailedReport";
                }
                else
                {
                    Caption = "MasterCuttingDetailedReport";
                }

            }
            else
            {
                if (ddlcutting.SelectedValue == "1")
                {
                    Caption = "PreCuttingSummaryReport";
                }
                else
                {
                    Caption = "MasterCuttingSummaryReport";
                }

            }
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= " + Caption + ".xls");
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