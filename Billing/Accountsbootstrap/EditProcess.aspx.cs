using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Text;
using System.Data;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class EditProcess : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string empid = "";

        double Q30F = 0; double Q32F = 0; double Q34F = 0; double Q36F = 0;
        double QXSF = 0; double QSF = 0; double QMF = 0; double QLF = 0;
        double QXLF = 0; double QXXLF = 0; double Q3XLF = 0; double Q4XLF = 0;

        double Q30H = 0; double Q32H = 0; double Q34H = 0; double Q36H = 0;
        double QXSH = 0; double QSH = 0; double QMH = 0; double QLH = 0;
        double QXLH = 0; double QXXLH = 0; double Q3XLH = 0; double Q4XLH = 0;
        double QttlFH = 0; double GVtotalshirt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string Stitchingid = string.Empty;
            string Requests = string.Empty;
            Stitchingid = Request.QueryString.Get("Stitchingid");


            Requests = Request.QueryString.Get("name");
            empid = Session["Empid"].ToString();

            if (!IsPostBack)
            {
                txtmultidate.Text = DateTime.Now.ToString("dd/MM/yyyy");

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
                        drpbranch.Items.Insert(0, "Select Branch");
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
                DataSet drpEmpp = objbs.Alljobworkmasterpayment();
                drpMultiemployee.DataSource = drpEmpp;
                drpMultiemployee.DataTextField = "ledgerName";
                drpMultiemployee.DataValueField = "ledgerId";
                drpMultiemployee.DataBind();
                drpMultiemployee.Items.Insert(0, "Select JobWorker");

                drpCurrentJobWorker.DataSource = drpEmpp;
                drpCurrentJobWorker.DataTextField = "ledgerName";
                drpCurrentJobWorker.DataValueField = "ledgerId";
                drpCurrentJobWorker.DataBind();
                drpCurrentJobWorker.Items.Insert(0, "Select Name");

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

        }
        protected void ddlmode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlmode.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Mode. Thank you !!!');", true);
                return;
            }
            else if (ddlmode.SelectedValue == "1")
            {
                btnadd.Text = "Change";
                drpMultiemployee.Enabled = true;
                txtChangeRate.Enabled = true;
                chkworker.Enabled = true;
                chkrate.Enabled = true;

            }
            else if (ddlmode.SelectedValue == "2")
            {
                btnadd.Text = "Delete";
                drpMultiemployee.Enabled = false;
                txtChangeRate.Enabled = false;
                chkworker.Enabled = false;
                chkrate.Enabled = false;
            }

        }
        protected void chkprocess_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlmode.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Mode. Thank you !!!');", true);
                return;
            }

            #region
            string Processid = "";
            DataSet ds2 = new DataSet();
            #region

            if (ddlmode.SelectedValue == "1")
            {
                #region
                if (chkprocess.SelectedValue == "2")
                {

                    ds2 = objbs.gettingproceessall("tblJpStiching", "tblTransJpStiching", "StichingId", drpbranch.SelectedValue);
                    Processid = "StichingId";
                }
                else if (chkprocess.SelectedValue == "3")
                {
                    ds2 = objbs.gettingproceessall("tblJpEmbroiding", "tblTransJpEmbroiding", "EmbroidingId", drpbranch.SelectedValue);
                    Processid = "EmbroidingId";
                }
                else if (chkprocess.SelectedValue == "1")
                {
                    ds2 = objbs.gettingproceessall("tblJpKajaButton", "tblTransJpKajaButton", "KajaButtonId", drpbranch.SelectedValue);
                    Processid = "KajaButtonId";
                }
                else if (chkprocess.SelectedValue == "7")
                {
                    ds2 = objbs.gettingproceessall("tblJpPrinting", "tblTransJpPrinting", "PrintingId", drpbranch.SelectedValue);
                    Processid = "PrintingId";
                }
                else if (chkprocess.SelectedValue == "4")
                {
                    ds2 = objbs.gettingproceessall("tblJpWashing", "tblTransJpWashing", "WashingId", drpbranch.SelectedValue);
                    Processid = "WashingId";
                }
                else if (chkprocess.SelectedValue == "8")
                {
                    ds2 = objbs.gettingproceessall("tblJpBarTag", "tblTransJpBarTag", "BarTagId", drpbranch.SelectedValue);
                    Processid = "BarTagId";
                }
                else if (chkprocess.SelectedValue == "9")
                {
                    ds2 = objbs.gettingproceessall("tblJpTrimming", "tblTransJpTrimming", "TrimmingId", drpbranch.SelectedValue);
                    Processid = "TrimmingId";
                }
                else if (chkprocess.SelectedValue == "10")
                {
                    ds2 = objbs.gettingproceessall("tblJpConsai", "tblTransJpConsai", "ConsaiId", drpbranch.SelectedValue);
                    Processid = "ConsaiId";
                }
                else if (chkprocess.SelectedValue == "5")
                {
                    ds2 = objbs.gettingproceessall("tblJpIroning", "tblTransJpIroning", "IroningId", drpbranch.SelectedValue);
                    Processid = "IroningId";
                }
                #endregion
            }
            else if (ddlmode.SelectedValue == "2")
            {
                #region
                if (chkprocess.SelectedValue == "2")
                {

                    ds2 = objbs.processfordelete("tblJpStiching", "tblTransJpStiching", "StichingId", drpbranch.SelectedValue);
                    Processid = "StichingId";
                }
                else if (chkprocess.SelectedValue == "3")
                {
                    ds2 = objbs.processfordelete("tblJpEmbroiding", "tblTransJpEmbroiding", "EmbroidingId", drpbranch.SelectedValue);
                    Processid = "EmbroidingId";
                }
                else if (chkprocess.SelectedValue == "1")
                {
                    ds2 = objbs.processfordelete("tblJpKajaButton", "tblTransJpKajaButton", "KajaButtonId", drpbranch.SelectedValue);
                    Processid = "KajaButtonId";
                }
                else if (chkprocess.SelectedValue == "7")
                {
                    ds2 = objbs.processfordelete("tblJpPrinting", "tblTransJpPrinting", "PrintingId", drpbranch.SelectedValue);
                    Processid = "PrintingId";
                }
                else if (chkprocess.SelectedValue == "4")
                {
                    ds2 = objbs.processfordelete("tblJpWashing", "tblTransJpWashing", "WashingId", drpbranch.SelectedValue);
                    Processid = "WashingId";
                }
                else if (chkprocess.SelectedValue == "8")
                {
                    ds2 = objbs.processfordelete("tblJpBarTag", "tblTransJpBarTag", "BarTagId", drpbranch.SelectedValue);
                    Processid = "BarTagId";
                }
                else if (chkprocess.SelectedValue == "9")
                {
                    ds2 = objbs.processfordelete("tblJpTrimming", "tblTransJpTrimming", "TrimmingId", drpbranch.SelectedValue);
                    Processid = "TrimmingId";
                }
                else if (chkprocess.SelectedValue == "10")
                {
                    ds2 = objbs.processfordelete("tblJpConsai", "tblTransJpConsai", "ConsaiId", drpbranch.SelectedValue);
                    Processid = "ConsaiId";
                }
                else if (chkprocess.SelectedValue == "5")
                {
                    ds2 = objbs.processfordelete("tblJpIroning", "tblTransJpIroning", "IroningId", drpbranch.SelectedValue);
                    Processid = "IroningId";
                }
                #endregion
            }





            #endregion
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlLotNo.DataSource = ds2.Tables[0];
                ddlLotNo.DataTextField = "CompanyLotNo";
                ddlLotNo.DataValueField = Processid;
                ddlLotNo.DataBind();
                ddlLotNo.Items.Insert(0, "Select LotNo");

            }
            else
            {
                ddlLotNo.Items.Insert(0, "Select LotNo");
            }
            #endregion
        }

        protected void ddlLotNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            //drpCurrentJobWorker.SelectedValue = "";
            //lblCurrentRate.Text = "0";

            //drpCurrentJobWorker.SelectedIndex = 0;
            //txtChangeRate.Text = "0";

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

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    drpCurrentJobWorker.SelectedValue = ds2.Tables[0].Rows[0]["workerId"].ToString();
                    lblCurrentRate.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["Rate"]).ToString("f2");

                    lbllotqty.Text = ds2.Tables[0].Rows[0]["TotalIssue"].ToString();
                }
            }
        }

        protected void btnadd_OnClick(object sender, EventArgs e)
        {
            DateTime Date = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            #region

            if (ddlmode.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Mode. Thank you !!!');", true);
                return;
            }
            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you !!!');", true);
                return;
            }
            if (chkprocess.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Process. Thank you !!!');", true);
                return;
            }

            if (ddlLotNo.SelectedValue == "Select LotNo")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select LotNo. Thank you !!!');", true);
                return;
            }
            if (txtnarration.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Narration. Thank you !!!');", true);
                return;
            }
            #endregion


            if (btnadd.Text == "Change")
            {
                if (chkworker.Checked == true)
                {
                    #region

                    if (drpMultiemployee.SelectedValue == "Select JobWorker")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select JobWorker. Thank you !!!');", true);
                        return;
                    }

                    #endregion
                }
                if (chkrate.Checked == true)
                {
                    #region
                    if (Convert.ToDouble(txtChangeRate.Text) < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate. Thank you !!!');", true);
                        return;
                    }
                    #endregion
                }

                if (chkworker.Checked == true || chkrate.Checked == true)
                {
                    int CurrentJobWorker = 0; int ChangeJobWorker = 0; double CurrentRate = 0; double ChangeRate = 0;
                    #region
                    if (chkworker.Checked == true && chkrate.Checked == true)
                    {
                        CurrentJobWorker = Convert.ToInt32(drpCurrentJobWorker.SelectedValue);
                        ChangeJobWorker = Convert.ToInt32(drpMultiemployee.SelectedValue);

                        CurrentRate = Convert.ToDouble(lblCurrentRate.Text);
                        ChangeRate = Convert.ToDouble(txtChangeRate.Text);
                    }
                    else if (chkworker.Checked == true)
                    {
                        CurrentJobWorker = Convert.ToInt32(drpCurrentJobWorker.SelectedValue);
                        ChangeJobWorker = Convert.ToInt32(drpMultiemployee.SelectedValue);
                    }
                    else if (chkrate.Checked == true)
                    {
                        CurrentRate = Convert.ToDouble(lblCurrentRate.Text);
                        ChangeRate = Convert.ToDouble(txtChangeRate.Text);
                    }
                    int save = objbs.insetProcessWorkerRateHistory(Convert.ToInt32(chkprocess.SelectedValue), Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(CurrentJobWorker), Convert.ToInt32(ChangeJobWorker), Convert.ToDouble(CurrentRate), Convert.ToDouble(ChangeRate), txtnarration.Text, Date, Convert.ToInt32(empid), Convert.ToInt32(drpbranch.SelectedValue), ddlLotNo.SelectedItem.Text);

                    if (chkworker.Checked == true)
                    {
                        #region
                        if (chkprocess.SelectedValue == "2")
                        {
                            int upprocess = objbs.updateProcessjobworker("tblJpStiching", "tblTransJpStiching", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, Convert.ToInt32(ChangeJobWorker));

                        }
                        else if (chkprocess.SelectedValue == "3")
                        {
                            int upprocess = objbs.updateProcessjobworker("tblJpEmbroiding", "tblTransJpEmbroiding", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, Convert.ToInt32(ChangeJobWorker));

                        }
                        else if (chkprocess.SelectedValue == "1")
                        {
                            int upprocess = objbs.updateProcessjobworker("tblJpKajaButton", "tblTransJpKajaButton", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, Convert.ToInt32(ChangeJobWorker));

                        }
                        else if (chkprocess.SelectedValue == "7")
                        {
                            int upprocess = objbs.updateProcessjobworker("tblJpPrinting", "tblTransJpPrinting", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, Convert.ToInt32(ChangeJobWorker));

                        }
                        else if (chkprocess.SelectedValue == "4")
                        {
                            int upprocess = objbs.updateProcessjobworker("tblJpWashing", "tblTransJpWashing", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, Convert.ToInt32(ChangeJobWorker));

                        }
                        else if (chkprocess.SelectedValue == "8")
                        {
                            int upprocess = objbs.updateProcessjobworker("tblJpBarTag", "tblTransJpBarTag", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, Convert.ToInt32(ChangeJobWorker));

                        }
                        else if (chkprocess.SelectedValue == "9")
                        {
                            int upprocess = objbs.updateProcessjobworker("tblJpTrimming", "tblTransJpTrimming", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, Convert.ToInt32(ChangeJobWorker));

                        }
                        else if (chkprocess.SelectedValue == "10")
                        {
                            int upprocess = objbs.updateProcessjobworker("tblJpConsai", "tblTransJpConsai", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, Convert.ToInt32(ChangeJobWorker));
                        }
                        else if (chkprocess.SelectedValue == "5")
                        {
                            int upprocess = objbs.updateProcessjobworker("tblJpIroning", "tblTransJpIroning", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, Convert.ToInt32(ChangeJobWorker));

                        }



                        #endregion
                    }
                    if (chkrate.Checked == true)
                    {
                        #region
                        if (chkprocess.SelectedValue == "2")
                        {

                            int upprocess = objbs.updateProcessTotalAmount("tblJpStiching", "tblTransJpStiching", "tblTransJpStichinghistory", "StichingId", ddlLotNo.SelectedValue, Convert.ToDouble(txtChangeRate.Text), "STITCHING", ddlLotNo.SelectedItem.Text);

                        }
                        else if (chkprocess.SelectedValue == "3")
                        {
                            int upprocess = objbs.updateProcessTotalAmount("tblJpEmbroiding", "tblTransJpEmbroiding", "tblTransJpEmbroidinghistory", "EmbroidingId", ddlLotNo.SelectedValue, Convert.ToDouble(txtChangeRate.Text), "EMBROIDERING", ddlLotNo.SelectedItem.Text);

                        }
                        else if (chkprocess.SelectedValue == "1")
                        {
                            int upprocess = objbs.updateProcessTotalAmount("tblJpKajaButton", "tblTransJpKajaButton", "tblTransJpKajaButtonhistory", "KajaButtonId", ddlLotNo.SelectedValue, Convert.ToDouble(txtChangeRate.Text), "KAJA BUTTON", ddlLotNo.SelectedItem.Text);

                        }
                        else if (chkprocess.SelectedValue == "7")
                        {
                            int upprocess = objbs.updateProcessTotalAmount("tblJpPrinting", "tblTransJpPrinting", "tblTransJpPrintinghistory", "PrintingId", ddlLotNo.SelectedValue, Convert.ToDouble(txtChangeRate.Text), "PRINTING", ddlLotNo.SelectedItem.Text);

                        }
                        else if (chkprocess.SelectedValue == "4")
                        {
                            int upprocess = objbs.updateProcessTotalAmount("tblJpWashing", "tblTransJpWashing", "tblTransJpWashinghistory", "WashingId", ddlLotNo.SelectedValue, Convert.ToDouble(txtChangeRate.Text), "WASHING", ddlLotNo.SelectedItem.Text);

                        }
                        else if (chkprocess.SelectedValue == "8")
                        {
                            int upprocess = objbs.updateProcessTotalAmount("tblJpBarTag", "tblTransJpBarTag", "tblTransJpBarTaghistory", "BarTagId", ddlLotNo.SelectedValue, Convert.ToDouble(txtChangeRate.Text), "BarTag Process", ddlLotNo.SelectedItem.Text);

                        }
                        else if (chkprocess.SelectedValue == "9")
                        {
                            int upprocess = objbs.updateProcessTotalAmount("tblJpTrimming", "tblTransJpTrimming", "tblTransJpTrimminghistory", "TrimmingId", ddlLotNo.SelectedValue, Convert.ToDouble(txtChangeRate.Text), "Trimming Process", ddlLotNo.SelectedItem.Text);

                        }
                        else if (chkprocess.SelectedValue == "10")
                        {
                            int upprocess = objbs.updateProcessTotalAmount("tblJpConsai", "tblTransJpConsai", "tblTransJpConsaihistory", "ConsaiId", ddlLotNo.SelectedValue, Convert.ToDouble(txtChangeRate.Text), "Consai Process", ddlLotNo.SelectedItem.Text);
                        }
                        else if (chkprocess.SelectedValue == "5")
                        {
                            int upprocess = objbs.updateProcessTotalAmount("tblJpIroning", "tblTransJpIroning", "tblTransJpIroninghistory", "IroningId", ddlLotNo.SelectedValue, Convert.ToDouble(txtChangeRate.Text), "IRON", ddlLotNo.SelectedItem.Text);

                        }



                        #endregion
                    }
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please to Select Change Type. Thank you !!!');", true);
                    return;
                }

            }
            else if (btnadd.Text == "Delete")
            {
                #region
                int save = objbs.deleteprodess(chkprocess.SelectedValue, Convert.ToInt32(ddlLotNo.SelectedValue), ddlLotNo.SelectedItem.Text, drpbranch.SelectedValue);
                int savehistory = objbs.deleteProcesHistory(Convert.ToInt32(chkprocess.SelectedValue), Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpCurrentJobWorker.SelectedValue), Convert.ToDouble(lblCurrentRate.Text), txtnarration.Text, Date, Convert.ToInt32(empid), Convert.ToInt32(drpbranch.SelectedValue), ddlLotNo.SelectedItem.Text);

                DataSet getironmat = objbs.getironmatforupdate(Convert.ToInt32(ddlLotNo.SelectedValue), drpbranch.SelectedValue, Convert.ToInt32(chkprocess.SelectedValue));
                if (getironmat.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < getironmat.Tables[0].Rows.Count; i++)
                    {

                        int ItemId = Convert.ToInt32(getironmat.Tables[0].Rows[i]["ItemId"].ToString());
                        int Qty = Convert.ToInt32(getironmat.Tables[0].Rows[i]["Qty"].ToString());


                        int insert = objbs.updateironmaterial(ItemId, Qty);

                    }
                }
                int delironmat = objbs.deleteironmaterial(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(chkprocess.SelectedValue));
                #endregion
            }

            System.Threading.Thread.Sleep(3000);
            Response.Redirect("Home_Page.aspx");
        }

    }
}