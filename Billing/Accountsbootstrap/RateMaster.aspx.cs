using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Billing.Accountsbootstrap
{
    public partial class RateMaster : System.Web.UI.Page
    {
        DataSet tbllogin = new DataSet();
        BSClass objBs = new BSClass();
        string sTableName = "";
        string id = string.Empty;
        string Sort_Direction = "Category ASC";
        string empid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

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

                DataSet dss = objBs.getdcemployee();
                if (dss.Tables[0].Rows.Count > 0)
                {
                    ddlPreparedby.DataSource = dss.Tables[0];
                    ddlPreparedby.DataTextField = "LedgerName";
                    ddlPreparedby.DataValueField = "LedgerID";
                    ddlPreparedby.DataBind();
                    ddlPreparedby.Items.Insert(0, "Select Prepared By");
                }

                DataSet dsset = objBs.getFinishedStockRatiolotrate(drpbranch.SelectedValue);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddllotno.DataSource = dsset.Tables[0];
                    ddllotno.DataTextField = "CompanyLotNo";
                    ddllotno.DataValueField = "CompanyLotNo";
                    ddllotno.DataBind();
                    ddllotno.Items.Insert(0, "Select LotNo");
                   
                }

                DataSet ds = objBs.getsalesrate(drpbranch.SelectedValue);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridview.DataSource = ds;
                        gridview.DataBind();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            #region

                            gridview.Columns[0].Visible = true;
                            gridview.Columns[1].Visible = false;
                            gridview.Columns[2].Visible = false;
                            gridview.Columns[3].Visible = false;
                            gridview.Columns[4].Visible = false;

                            gridview.Columns[5].Visible = false;
                            gridview.Columns[6].Visible = false;
                            gridview.Columns[7].Visible = false;
                            gridview.Columns[8].Visible = false;
                            gridview.Columns[9].Visible = false;
                            gridview.Columns[10].Visible = false;
                            gridview.Columns[11].Visible = false;
                            gridview.Columns[12].Visible = false;
                            gridview.Columns[13].Visible = false;
                            gridview.Columns[14].Visible = false;
                            gridview.Columns[15].Visible = false;
                            gridview.Columns[16].Visible = false;

                            gridview.Columns[17].Visible = false;
                            gridview.Columns[18].Visible = false;
                            gridview.Columns[19].Visible = false;
                            gridview.Columns[20].Visible = false;
                            gridview.Columns[21].Visible = false;
                            gridview.Columns[22].Visible = false;
                            gridview.Columns[23].Visible = false;
                            gridview.Columns[24].Visible = false;
                            gridview.Columns[25].Visible = false;
                            gridview.Columns[26].Visible = false;
                            gridview.Columns[27].Visible = false;
                            gridview.Columns[28].Visible = false;

                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {

                                #region
                                string S30 = ds.Tables[0].Rows[j]["R30F"].ToString();
                                string S32 = ds.Tables[0].Rows[j]["R32F"].ToString();
                                string S34 = ds.Tables[0].Rows[j]["R34F"].ToString();
                                string S36 = ds.Tables[0].Rows[j]["R36F"].ToString();
                                string SXS = ds.Tables[0].Rows[j]["RxsF"].ToString();
                                string SS = ds.Tables[0].Rows[j]["RsF"].ToString();
                                string SM = ds.Tables[0].Rows[j]["RmF"].ToString();
                                string SL = ds.Tables[0].Rows[j]["RlF"].ToString();
                                string SXL = ds.Tables[0].Rows[j]["RxlF"].ToString();
                                string SXXL = ds.Tables[0].Rows[j]["RxxlF"].ToString();
                                string S3XL = ds.Tables[0].Rows[j]["R3xlF"].ToString();
                                string S4XL = ds.Tables[0].Rows[j]["R4xlF"].ToString();


                                string HS30 = ds.Tables[0].Rows[j]["R30H"].ToString();
                                string HS32 = ds.Tables[0].Rows[j]["R32H"].ToString();
                                string HS34 = ds.Tables[0].Rows[j]["R34H"].ToString();
                                string HS36 = ds.Tables[0].Rows[j]["R36H"].ToString();
                                string HSXS = ds.Tables[0].Rows[j]["RxsH"].ToString();
                                string HSS = ds.Tables[0].Rows[j]["RsH"].ToString();
                                string HSM = ds.Tables[0].Rows[j]["RmH"].ToString();
                                string HSL = ds.Tables[0].Rows[j]["RlH"].ToString();
                                string HSXL = ds.Tables[0].Rows[j]["RxlH"].ToString();
                                string HSXXL = ds.Tables[0].Rows[j]["RxxlH"].ToString();
                                string HS3XL = ds.Tables[0].Rows[j]["R3xlH"].ToString();
                                string HS4XL = ds.Tables[0].Rows[j]["R4xlH"].ToString();

                               

                                //////grndtot = grndtot + tot;
                                //////lblstockgrandtot.Text = grndtot.ToString();

                                if (S30 != "0" && S30 != "0.0000")
                                {

                                    gridview.Columns[5].Visible = true;
                                }
                                if (S32 != "0" && S32 != "0.0000")
                                {

                                    gridview.Columns[6].Visible = true;
                                }

                                if (S34 != "0" && S34 != "0.0000")
                                {

                                    gridview.Columns[7].Visible = true;
                                }

                                if (S36 != "0" && S36 != "0.0000")
                                {

                                    gridview.Columns[8].Visible = true;
                                }

                                if (SXS != "0" && SXS != "0.0000")
                                {

                                    gridview.Columns[9].Visible = true;
                                }

                                if (SS != "0" && SS != "0.0000")
                                {

                                    gridview.Columns[10].Visible = true;
                                }

                                if (SM != "0" && SM != "0.0000")
                                {

                                    gridview.Columns[11].Visible = true;
                                }

                                if (SL != "0" && SL != "0.0000")
                                {

                                    gridview.Columns[12].Visible = true;
                                }

                                if (SXL != "0" && SXL != "0.0000")
                                {

                                    gridview.Columns[13].Visible = true;
                                }

                                if (SXXL != "0" && SXXL != "0.0000")
                                {

                                    gridview.Columns[14].Visible = true;
                                }

                                if (S3XL != "0" && S3XL != "0.0000")
                                {

                                    gridview.Columns[15].Visible = true;
                                }

                                if (S4XL != "0" && S4XL != "0.0000")
                                {

                                    gridview.Columns[16].Visible = true;
                                }


                                if (HS30 != "0" && HS30 != "0.0000")
                                {

                                    gridview.Columns[17].Visible = true;
                                }
                                if (HS32 != "0" && HS32 != "0.0000")
                                {

                                    gridview.Columns[18].Visible = true;
                                }

                                if (HS34 != "0" && HS34 != "0.0000")
                                {

                                    gridview.Columns[19].Visible = true;
                                }

                                if (HS36 != "0" && HS36 != "0.0000")
                                {

                                    gridview.Columns[20].Visible = true;
                                }

                                if (HSXS != "0" && HSXS != "0.0000")
                                {

                                    gridview.Columns[21].Visible = true;
                                }

                                if (HSS != "0" && HSS != "0.0000")
                                {

                                    gridview.Columns[22].Visible = true;
                                }

                                if (HSM != "0" && HSM != "0.0000")
                                {

                                    gridview.Columns[23].Visible = true;
                                }

                                if (HSL != "0" && HSL != "0.0000")
                                {

                                    gridview.Columns[24].Visible = true;
                                }

                                if (HSXL != "0" && HSXL != "0.0000")
                                {

                                    gridview.Columns[25].Visible = true;
                                }

                                if (HSXXL != "0" && HSXXL != "0.0000")
                                {

                                    gridview.Columns[26].Visible = true;
                                }

                                if (HS3XL != "0" && HS3XL != "0.0000")
                                {

                                    gridview.Columns[27].Visible = true;
                                }

                                if (HS4XL != "0" && HS4XL != "0.0000")
                                {

                                    gridview.Columns[28].Visible = true;
                                }
                                #endregion

                            }

                            #endregion
                        }

                    }
                    else
                    {
                        gridview.DataSource = null;
                        gridview.DataBind();
                    }

                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }



            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void Btn_Reset(object sender, EventArgs e)
        {
            Response.Redirect("Style.aspx");
        }
        protected void Btn_Search(object sender, EventArgs e)
        {
            //DataSet ds = objBs.categorysrchgrid(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), sTableName);

            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        gridview.DataSource = ds;
            //        gridview.DataBind();
            //    }
            //    else
            //    {
            //        gridview.DataSource = null;
            //        gridview.DataBind();
            //    }

            //}
            //else
            //{
            //    gridview.DataSource = null;
            //    gridview.DataBind();
            //}



        }



       

        public void ExportToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                //string filename = "Sales Report.xls";
                string filename = "NewCategory _" + DateTime.Now.ToString() + ".xls";
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








        protected void gridview_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objBs.CheckIfCategoryUsedStyleID(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    ((Image)e.Row.FindControl("Image1")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;
                }

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                #region
                if (drpbranch.SelectedValue=="ALL")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Branch.Thank You !!!')", true);
                    return;
                }
                if (ddllotno.SelectedValue == "Select LotNo")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select LotNo.Thank You !!!')", true);
                    return;
                }
                if (ddlPreparedby.SelectedValue == "Select Prepared By")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Prepared By.Thank You !!!')", true);
                    return;
                }
                else
                {
                    int iStatus = objBs.InsertsalesRate(ddllotno.SelectedItem.Text, Convert.ToInt32(ddlPreparedby.SelectedValue), Convert.ToInt32(empid),Convert.ToDouble(txtf30.Text),Convert.ToDouble(txtf32.Text),Convert.ToDouble(txtf34.Text),Convert.ToDouble(txtf36.Text),Convert.ToDouble(txtfxs.Text),Convert.ToDouble(txtfs.Text),Convert.ToDouble(txtfm.Text),Convert.ToDouble(txtfl.Text),Convert.ToDouble(txtfxl.Text),Convert.ToDouble(txtfxxl.Text),Convert.ToDouble(txtf3xl.Text),Convert.ToDouble(txtf4xl.Text),Convert.ToDouble(txth30.Text),Convert.ToDouble(txth32.Text),Convert.ToDouble(txth34.Text),Convert.ToDouble(txth36.Text),Convert.ToDouble(txthxs.Text),Convert.ToDouble(txths.Text),Convert.ToDouble(txthm.Text),Convert.ToDouble(txthl.Text),Convert.ToDouble(txthxl.Text),Convert.ToDouble(txthxxl.Text),Convert.ToDouble(txth3xl.Text),Convert.ToDouble(txth4xl.Text),Convert.ToInt32(drpbranch.SelectedValue));

                    DataSet ds = objBs.getsalesrate(drpbranch.SelectedValue);
                    gridview.DataSource = ds;
                    gridview.DataBind();

                    #region
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region

                        gridview.Columns[0].Visible = true;
                        gridview.Columns[1].Visible = false;
                        gridview.Columns[2].Visible = false;
                        gridview.Columns[3].Visible = false;
                        gridview.Columns[4].Visible = false;

                        gridview.Columns[5].Visible = false;
                        gridview.Columns[6].Visible = false;
                        gridview.Columns[7].Visible = false;
                        gridview.Columns[8].Visible = false;
                        gridview.Columns[9].Visible = false;
                        gridview.Columns[10].Visible = false;
                        gridview.Columns[11].Visible = false;
                        gridview.Columns[12].Visible = false;
                        gridview.Columns[13].Visible = false;
                        gridview.Columns[14].Visible = false;
                        gridview.Columns[15].Visible = false;
                        gridview.Columns[16].Visible = false;

                        gridview.Columns[17].Visible = false;
                        gridview.Columns[18].Visible = false;
                        gridview.Columns[19].Visible = false;
                        gridview.Columns[20].Visible = false;
                        gridview.Columns[21].Visible = false;
                        gridview.Columns[22].Visible = false;
                        gridview.Columns[23].Visible = false;
                        gridview.Columns[24].Visible = false;
                        gridview.Columns[25].Visible = false;
                        gridview.Columns[26].Visible = false;
                        gridview.Columns[27].Visible = false;
                        gridview.Columns[28].Visible = false;

                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = ds.Tables[0].Rows[j]["R30F"].ToString();
                            string S32 = ds.Tables[0].Rows[j]["R32F"].ToString();
                            string S34 = ds.Tables[0].Rows[j]["R34F"].ToString();
                            string S36 = ds.Tables[0].Rows[j]["R36F"].ToString();
                            string SXS = ds.Tables[0].Rows[j]["RxsF"].ToString();
                            string SS = ds.Tables[0].Rows[j]["RsF"].ToString();
                            string SM = ds.Tables[0].Rows[j]["RmF"].ToString();
                            string SL = ds.Tables[0].Rows[j]["RlF"].ToString();
                            string SXL = ds.Tables[0].Rows[j]["RxlF"].ToString();
                            string SXXL = ds.Tables[0].Rows[j]["RxxlF"].ToString();
                            string S3XL = ds.Tables[0].Rows[j]["R3xlF"].ToString();
                            string S4XL = ds.Tables[0].Rows[j]["R4xlF"].ToString();


                            string HS30 = ds.Tables[0].Rows[j]["R30H"].ToString();
                            string HS32 = ds.Tables[0].Rows[j]["R32H"].ToString();
                            string HS34 = ds.Tables[0].Rows[j]["R34H"].ToString();
                            string HS36 = ds.Tables[0].Rows[j]["R36H"].ToString();
                            string HSXS = ds.Tables[0].Rows[j]["RxsH"].ToString();
                            string HSS = ds.Tables[0].Rows[j]["RsH"].ToString();
                            string HSM = ds.Tables[0].Rows[j]["RmH"].ToString();
                            string HSL = ds.Tables[0].Rows[j]["RlH"].ToString();
                            string HSXL = ds.Tables[0].Rows[j]["RxlH"].ToString();
                            string HSXXL = ds.Tables[0].Rows[j]["RxxlH"].ToString();
                            string HS3XL = ds.Tables[0].Rows[j]["R3xlH"].ToString();
                            string HS4XL = ds.Tables[0].Rows[j]["R4xlH"].ToString();

                            if (S30 != "0")
                            {

                                gridview.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                gridview.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                gridview.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                gridview.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                gridview.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                gridview.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                gridview.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                gridview.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                gridview.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                gridview.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                gridview.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                gridview.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                gridview.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                gridview.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                gridview.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                gridview.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                gridview.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                gridview.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                gridview.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                gridview.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                gridview.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                gridview.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                gridview.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                gridview.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                    #endregion
                }
                #endregion
                Response.Redirect("RateMaster.aspx");
            }
            else
            {
                #region

                if (drpbranch.SelectedValue == "ALL")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Branch.Thank You !!!')", true);
                    return;
                }
                if (ddllotno.SelectedValue == "Select LotNo")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select LotNo.Thank You !!!')", true);
                    return;
                }
                if (ddlPreparedby.SelectedValue == "Select Prepared By")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Prepared By.Thank You !!!')", true);
                    return;
                }
                else
                {
                    int iStatus = objBs.updatesalesRate(ddllotno.SelectedItem.Text, Convert.ToInt32(ddlPreparedby.SelectedValue), Convert.ToInt32(empid), Convert.ToDouble(txtf30.Text), Convert.ToDouble(txtf32.Text), Convert.ToDouble(txtf34.Text), Convert.ToDouble(txtf36.Text), Convert.ToDouble(txtfxs.Text), Convert.ToDouble(txtfs.Text), Convert.ToDouble(txtfm.Text), Convert.ToDouble(txtfl.Text), Convert.ToDouble(txtfxl.Text), Convert.ToDouble(txtfxxl.Text), Convert.ToDouble(txtf3xl.Text), Convert.ToDouble(txtf4xl.Text), Convert.ToDouble(txth30.Text), Convert.ToDouble(txth32.Text), Convert.ToDouble(txth34.Text), Convert.ToDouble(txth36.Text), Convert.ToDouble(txthxs.Text), Convert.ToDouble(txths.Text), Convert.ToDouble(txthm.Text), Convert.ToDouble(txthl.Text), Convert.ToDouble(txthxl.Text), Convert.ToDouble(txthxxl.Text), Convert.ToDouble(txth3xl.Text), Convert.ToDouble(txth4xl.Text), Convert.ToInt32(drpbranch.SelectedValue),Convert.ToInt32(txtId.Text));

                    DataSet ds = objBs.getsalesrate(drpbranch.SelectedValue);
                    gridview.DataSource = ds;
                    gridview.DataBind();

                    #region
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region

                        gridview.Columns[0].Visible = true;
                        gridview.Columns[1].Visible = false;
                        gridview.Columns[2].Visible = false;
                        gridview.Columns[3].Visible = false;
                        gridview.Columns[4].Visible = false;

                        gridview.Columns[5].Visible = false;
                        gridview.Columns[6].Visible = false;
                        gridview.Columns[7].Visible = false;
                        gridview.Columns[8].Visible = false;
                        gridview.Columns[9].Visible = false;
                        gridview.Columns[10].Visible = false;
                        gridview.Columns[11].Visible = false;
                        gridview.Columns[12].Visible = false;
                        gridview.Columns[13].Visible = false;
                        gridview.Columns[14].Visible = false;
                        gridview.Columns[15].Visible = false;
                        gridview.Columns[16].Visible = false;

                        gridview.Columns[17].Visible = false;
                        gridview.Columns[18].Visible = false;
                        gridview.Columns[19].Visible = false;
                        gridview.Columns[20].Visible = false;
                        gridview.Columns[21].Visible = false;
                        gridview.Columns[22].Visible = false;
                        gridview.Columns[23].Visible = false;
                        gridview.Columns[24].Visible = false;
                        gridview.Columns[25].Visible = false;
                        gridview.Columns[26].Visible = false;
                        gridview.Columns[27].Visible = false;
                        gridview.Columns[28].Visible = false;

                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {

                            #region
                            string S30 = ds.Tables[0].Rows[j]["R30F"].ToString();
                            string S32 = ds.Tables[0].Rows[j]["R32F"].ToString();
                            string S34 = ds.Tables[0].Rows[j]["R34F"].ToString();
                            string S36 = ds.Tables[0].Rows[j]["R36F"].ToString();
                            string SXS = ds.Tables[0].Rows[j]["RxsF"].ToString();
                            string SS = ds.Tables[0].Rows[j]["RsF"].ToString();
                            string SM = ds.Tables[0].Rows[j]["RmF"].ToString();
                            string SL = ds.Tables[0].Rows[j]["RlF"].ToString();
                            string SXL = ds.Tables[0].Rows[j]["RxlF"].ToString();
                            string SXXL = ds.Tables[0].Rows[j]["RxxlF"].ToString();
                            string S3XL = ds.Tables[0].Rows[j]["R3xlF"].ToString();
                            string S4XL = ds.Tables[0].Rows[j]["R4xlF"].ToString();


                            string HS30 = ds.Tables[0].Rows[j]["R30H"].ToString();
                            string HS32 = ds.Tables[0].Rows[j]["R32H"].ToString();
                            string HS34 = ds.Tables[0].Rows[j]["R34H"].ToString();
                            string HS36 = ds.Tables[0].Rows[j]["R36H"].ToString();
                            string HSXS = ds.Tables[0].Rows[j]["RxsH"].ToString();
                            string HSS = ds.Tables[0].Rows[j]["RsH"].ToString();
                            string HSM = ds.Tables[0].Rows[j]["RmH"].ToString();
                            string HSL = ds.Tables[0].Rows[j]["RlH"].ToString();
                            string HSXL = ds.Tables[0].Rows[j]["RxlH"].ToString();
                            string HSXXL = ds.Tables[0].Rows[j]["RxxlH"].ToString();
                            string HS3XL = ds.Tables[0].Rows[j]["R3xlH"].ToString();
                            string HS4XL = ds.Tables[0].Rows[j]["R4xlH"].ToString();

                            if (S30 != "0")
                            {

                                gridview.Columns[5].Visible = true;
                            }
                            if (S32 != "0")
                            {

                                gridview.Columns[6].Visible = true;
                            }

                            if (S34 != "0")
                            {

                                gridview.Columns[7].Visible = true;
                            }

                            if (S36 != "0")
                            {

                                gridview.Columns[8].Visible = true;
                            }

                            if (SXS != "0")
                            {

                                gridview.Columns[9].Visible = true;
                            }

                            if (SS != "0")
                            {

                                gridview.Columns[10].Visible = true;
                            }

                            if (SM != "0")
                            {

                                gridview.Columns[11].Visible = true;
                            }

                            if (SL != "0")
                            {

                                gridview.Columns[12].Visible = true;
                            }

                            if (SXL != "0")
                            {

                                gridview.Columns[13].Visible = true;
                            }

                            if (SXXL != "0")
                            {

                                gridview.Columns[14].Visible = true;
                            }

                            if (S3XL != "0")
                            {

                                gridview.Columns[15].Visible = true;
                            }

                            if (S4XL != "0")
                            {

                                gridview.Columns[16].Visible = true;
                            }


                            if (HS30 != "0")
                            {

                                gridview.Columns[17].Visible = true;
                            }
                            if (HS32 != "0")
                            {

                                gridview.Columns[18].Visible = true;
                            }

                            if (HS34 != "0")
                            {

                                gridview.Columns[19].Visible = true;
                            }

                            if (HS36 != "0")
                            {

                                gridview.Columns[20].Visible = true;
                            }

                            if (HSXS != "0")
                            {

                                gridview.Columns[21].Visible = true;
                            }

                            if (HSS != "0")
                            {

                                gridview.Columns[22].Visible = true;
                            }

                            if (HSM != "0")
                            {

                                gridview.Columns[23].Visible = true;
                            }

                            if (HSL != "0")
                            {

                                gridview.Columns[24].Visible = true;
                            }

                            if (HSXL != "0")
                            {

                                gridview.Columns[25].Visible = true;
                            }

                            if (HSXXL != "0")
                            {

                                gridview.Columns[26].Visible = true;
                            }

                            if (HS3XL != "0")
                            {

                                gridview.Columns[27].Visible = true;
                            }

                            if (HS4XL != "0")
                            {

                                gridview.Columns[28].Visible = true;
                            }
                            #endregion

                        }

                        #endregion
                    }
                    #endregion
                }
                #endregion

                Response.Redirect("RateMaster.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
           
            lblName.Text = "Add Rate";
        }

        protected void gridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsset = objBs.getFinishedStockRatiolotsalerate(drpbranch.SelectedValue);
            if (dsset.Tables[0].Rows.Count > 0)
            {
                ddllotno.DataSource = dsset.Tables[0];
                ddllotno.DataTextField = "LotNo";
                ddllotno.DataValueField = "LotNo";
                ddllotno.DataBind();
                ddllotno.Items.Insert(0, "Select LotNo");

            }

            lblName.Text = "Update Rate";

            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
                id = gridview.SelectedDataKey.Value.ToString();
            {

                DataSet ds = objBs.getsalesRateid(Convert.ToInt32(id));
                if (ds.Tables[0].Rows.Count > 0)
                {


                    txtId.Text = ds.Tables[0].Rows[0]["ID"].ToString();

                    ddllotno.Text = ds.Tables[0].Rows[0]["LotNo"].ToString();
                    ddllotno.Enabled = false;
                    ddlPreparedby.Text = ds.Tables[0].Rows[0]["PreparedBy"].ToString();


                    txtf30.Text =Convert.ToDouble(ds.Tables[0].Rows[0]["R30F"]).ToString("f2");
                    txtf32.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R32F"]).ToString("f2");
                    txtf34.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R34F"]).ToString("f2");
                    txtf36.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R36F"]).ToString("f2");
                    txtfxs.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RXSF"]).ToString("f2");
                    txtfs.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RSF"]).ToString("f2");
                    txtfm.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RMF"]).ToString("f2");
                    txtfl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RLF"]).ToString("f2");
                    txtfxl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RXLF"]).ToString("f2");
                    txtfxxl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RXXLF"]).ToString("f2");
                    txtf3xl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R3XLF"]).ToString("f2");
                    txtf4xl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R4XLF"]).ToString("f2");

                    txth30.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R30H"]).ToString("f2");
                    txth32.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R32H"]).ToString("f2");
                    txth34.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R34H"]).ToString("f2");
                    txth36.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R36H"]).ToString("f2");
                    txthxs.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RXSH"]).ToString("f2");
                    txths.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RSH"]).ToString("f2");
                    txthm.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RMH"]).ToString("f2");
                    txthl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RLH"]).ToString("f2");
                    txthxl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RXLH"]).ToString("f2");
                    txthxxl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["RXXLH"]).ToString("f2");
                    txth3xl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R3XLH"]).ToString("f2");
                    txth4xl.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["R4XLH"]).ToString("f2");

                    btnSave.Text = "Update";
                }
            }
        }
    }
}