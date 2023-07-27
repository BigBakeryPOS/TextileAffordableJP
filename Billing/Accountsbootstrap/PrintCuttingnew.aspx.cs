using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;

namespace Billing.Accountsbootstrap
{
    public partial class PrintCuttingnew : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double meter = 0.00;
        double Qty = 0.00;
        int count = 0;
        double totalfs = 0;
        double Q30F = 0; double Q32F = 0; double Q34F = 0; double Q36F = 0;
        double QXSF = 0; double QSF = 0; double QMF = 0; double QLF = 0;
        double QXLF = 0; double QXXLF = 0; double Q3XLF = 0; double Q4XLF = 0;

        double Q30H = 0; double Q32H = 0; double Q34H = 0; double Q36H = 0;
        double QXSH = 0; double QSH = 0; double QMH = 0; double QLH = 0;
        double QXLH = 0; double QXXLH = 0; double Q3XLH = 0; double Q4XLH = 0;
        double ttlreqmeter = 0; double ttltotfs = 0; double ttltoths = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            string iSalesID = Request.QueryString.Get("iCutID");
            sTableName = Session["User"].ToString();
            if (iSalesID != null)
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


                

                gridprint.Visible = true;
                DataSet ds2 = objBs.Cuttingprintreport(Convert.ToInt32(iSalesID));
                if (ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        lblitemnarrations.Text = ds2.Tables[0].Rows[0]["itemnarrations"].ToString();
                        

                        lblLot.Text = ds2.Tables[0].Rows[0]["CompanyFullLotNo"].ToString();
                        lblllot.Text = ds2.Tables[0].Rows[0]["Cutid"].ToString();
                        lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["deliverydate"]).ToString("dd-MM-yyyy");
                        lblwidth.Text = ds2.Tables[0].Rows[0]["width"].ToString();
                        lblfit.Text = ds2.Tables[0].Rows[0]["fitid"].ToString();
                        lblfitName.Text = ds2.Tables[0].Rows[0]["fit"].ToString();
                        lblcut.Text = ds2.Tables[0].Rows[0]["Cut"].ToString();
                        lblrolltaka.Text = ds2.Tables[0].Rows[0]["RollTaka"].ToString();
                        lblCompleteStitching.Text = ds2.Tables[0].Rows[0]["CompleteStitching"].ToString();
                        lblsleeve.Text = ds2.Tables[0].Rows[0]["Sleevetype"].ToString();
                        lbllabeldesign.Text = ds2.Tables[0].Rows[0]["labeltype"].ToString();
                        gridnewprint.DataSource = ds2;
                        gridnewprint.DataBind();

                        gridnewprint.Visible = true;

                        DataSet dsizee = objBs.Getfitseize(lblfit.Text);
                        if ((dsizee.Tables[0].Rows.Count > 0))
                        {

                            for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                            {

                                string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();




                                {

                                    chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                                }

                            }
                        }


                        if (chkSizes.SelectedIndex >= 0)
                        {
                            gridnewprint.Columns[5].Visible = false; //30FS
                            gridnewprint.Columns[6].Visible = false; //32FS

                            gridnewprint.Columns[7].Visible = false;//34Fs
                            gridnewprint.Columns[8].Visible = false;//36Fs

                            gridnewprint.Columns[9].Visible = false; //XSFS
                            gridnewprint.Columns[10].Visible = false; //SFS

                            gridnewprint.Columns[11].Visible = false; //MFS
                            gridnewprint.Columns[12].Visible = false; //LFS

                            gridnewprint.Columns[13].Visible = false; //XLFS
                            gridnewprint.Columns[14].Visible = false; //xxlFS

                            gridnewprint.Columns[15].Visible = false; //3xlHS
                            gridnewprint.Columns[16].Visible = false; //4xlHS

                            gridnewprint.Columns[17].Visible = false; //30HS

                            gridnewprint.Columns[18].Visible = false; //32HS

                            gridnewprint.Columns[19].Visible = false; //34HS
                            gridnewprint.Columns[20].Visible = false; //36HS

                            gridnewprint.Columns[21].Visible = false; //XSHS
                            gridnewprint.Columns[22].Visible = false; //SHS

                            gridnewprint.Columns[23].Visible = false; //MHS
                            gridnewprint.Columns[24].Visible = false; //LHS

                            gridnewprint.Columns[25].Visible = false; //XLHS
                            gridnewprint.Columns[26].Visible = false; //XXLHS

                            gridnewprint.Columns[27].Visible = false; //3XLHS
                            gridnewprint.Columns[28].Visible = false; //4XLHS

                            gridnewprint.Columns[29].Visible = false; //3XLHS
                            gridnewprint.Columns[30].Visible = false; //4XLHS

                            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                            {

                                #region
                                string S30 = ds2.Tables[0].Rows[j]["30FS"].ToString();
                                string S32 = ds2.Tables[0].Rows[j]["32FS"].ToString();
                                string S34 = ds2.Tables[0].Rows[j]["34FS"].ToString();
                                string S36 = ds2.Tables[0].Rows[j]["36FS"].ToString();
                                string SXS = ds2.Tables[0].Rows[j]["xsFS"].ToString();
                                string SS = ds2.Tables[0].Rows[j]["sFS"].ToString();
                                string SM = ds2.Tables[0].Rows[j]["mFS"].ToString();
                                string SL = ds2.Tables[0].Rows[j]["lFS"].ToString();
                                string SXL = ds2.Tables[0].Rows[j]["xlFS"].ToString();
                                string SXXL = ds2.Tables[0].Rows[j]["xxlFS"].ToString();
                                string S3XL = ds2.Tables[0].Rows[j]["3xlFS"].ToString();
                                string S4XL = ds2.Tables[0].Rows[j]["4xlFS"].ToString();


                                string HS30 = ds2.Tables[0].Rows[j]["30HS"].ToString();
                                string HS32 = ds2.Tables[0].Rows[j]["32HS"].ToString();
                                string HS34 = ds2.Tables[0].Rows[j]["34HS"].ToString();
                                string HS36 = ds2.Tables[0].Rows[j]["36HS"].ToString();
                                string HSXS = ds2.Tables[0].Rows[j]["xsHS"].ToString();
                                string HSS = ds2.Tables[0].Rows[j]["sHS"].ToString();
                                string HSM = ds2.Tables[0].Rows[j]["mHS"].ToString();
                                string HSL = ds2.Tables[0].Rows[j]["lHS"].ToString();
                                string HSXL = ds2.Tables[0].Rows[j]["xlHS"].ToString();
                                string HSXXL = ds2.Tables[0].Rows[j]["xxlHS"].ToString();
                                string HS3XL = ds2.Tables[0].Rows[j]["3xlHS"].ToString();
                                string HS4XL = ds2.Tables[0].Rows[j]["4xlHS"].ToString();

                                string totfs = ds2.Tables[0].Rows[j]["totfs"].ToString();
                                string toths = ds2.Tables[0].Rows[j]["toths"].ToString();
                                //int tot = Convert.ToInt32(ds2.Tables[0].Rows[j]["Total"]);




                                if (S30 != "0")
                                {

                                    gridnewprint.Columns[5].Visible = true;
                                }
                                if (S32 != "0")
                                {

                                    gridnewprint.Columns[6].Visible = true;
                                }

                                if (S34 != "0")
                                {

                                    gridnewprint.Columns[7].Visible = true;
                                }

                                if (S36 != "0")
                                {

                                    gridnewprint.Columns[8].Visible = true;
                                }

                                if (SXS != "0")
                                {

                                    gridnewprint.Columns[9].Visible = true;
                                }

                                if (SS != "0")
                                {

                                    gridnewprint.Columns[10].Visible = true;
                                }

                                if (SM != "0")
                                {

                                    gridnewprint.Columns[11].Visible = true;
                                }

                                if (SL != "0")
                                {

                                    gridnewprint.Columns[12].Visible = true;
                                }

                                if (SXL != "0")
                                {

                                    gridnewprint.Columns[13].Visible = true;
                                }

                                if (SXXL != "0")
                                {

                                    gridnewprint.Columns[14].Visible = true;
                                }

                                if (S3XL != "0")
                                {

                                    gridnewprint.Columns[15].Visible = true;
                                }

                                if (S4XL != "0")
                                {

                                    gridnewprint.Columns[16].Visible = true;
                                }


                                if (HS30 != "0")
                                {

                                    gridnewprint.Columns[17].Visible = true;
                                }
                                if (HS32 != "0")
                                {

                                    gridnewprint.Columns[18].Visible = true;
                                }

                                if (HS34 != "0")
                                {

                                    gridnewprint.Columns[19].Visible = true;
                                }

                                if (HS36 != "0")
                                {

                                    gridnewprint.Columns[20].Visible = true;
                                }

                                if (HSXS != "0")
                                {

                                    gridnewprint.Columns[21].Visible = true;
                                }

                                if (HSS != "0")
                                {

                                    gridnewprint.Columns[22].Visible = true;
                                }

                                if (HSM != "0")
                                {

                                    gridnewprint.Columns[23].Visible = true;
                                }

                                if (HSL != "0")
                                {

                                    gridnewprint.Columns[24].Visible = true;
                                }

                                if (HSXL != "0")
                                {

                                    gridnewprint.Columns[25].Visible = true;
                                }

                                if (HSXXL != "0")
                                {

                                    gridnewprint.Columns[26].Visible = true;
                                }

                                if (HS3XL != "0")
                                {

                                    gridnewprint.Columns[27].Visible = true;
                                }

                                if (HS4XL != "0")
                                {

                                    gridnewprint.Columns[28].Visible = true;
                                }

                                if (totfs != "0")
                                {

                                    gridnewprint.Columns[29].Visible = true;
                                }
                                if (toths != "0")
                                {

                                    gridnewprint.Columns[30].Visible = true;
                                }
                                #endregion

                            }
                        }
                        else
                        {
                            gridnewprint.Columns[5].Visible = false; //30FS
                            gridnewprint.Columns[6].Visible = false; //32FS

                            gridnewprint.Columns[7].Visible = false;//34Fs
                            gridnewprint.Columns[8].Visible = false;//36Fs

                            gridnewprint.Columns[9].Visible = false; //XSFS
                            gridnewprint.Columns[10].Visible = false; //SFS

                            gridnewprint.Columns[11].Visible = false; //MFS
                            gridnewprint.Columns[12].Visible = false; //LFS

                            gridnewprint.Columns[13].Visible = false; //XLFS
                            gridnewprint.Columns[14].Visible = false; //xxlFS

                            gridnewprint.Columns[15].Visible = false; //3xlHS
                            gridnewprint.Columns[16].Visible = false; //4xlHS

                            gridnewprint.Columns[17].Visible = false; //30HS

                            gridnewprint.Columns[18].Visible = false; //32HS

                            gridnewprint.Columns[19].Visible = false; //34HS
                            gridnewprint.Columns[20].Visible = false; //36HS

                            gridnewprint.Columns[21].Visible = false; //XSHS
                            gridnewprint.Columns[22].Visible = false; //SHS

                            gridnewprint.Columns[23].Visible = false; //MHS
                            gridnewprint.Columns[24].Visible = false; //LHS

                            gridnewprint.Columns[25].Visible = false; //XLHS
                            gridnewprint.Columns[26].Visible = false; //XXLHS

                            gridnewprint.Columns[27].Visible = false; //3XLHS
                            gridnewprint.Columns[28].Visible = false; //4XLHS

                        }

                        //gridprint.DataSource = ds2;
                        //gridprint.DataBind();
                        //}
                        //if (ds2.Tables[0].Rows.Count > 0)
                        //{
                        //    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                        //    {
                        //        meter = meter + Convert.ToDouble(ds2.Tables[0].Rows[i]["reqmeter"]);
                        //        Qty = Qty + Convert.ToDouble(ds2.Tables[0].Rows[i]["Qty"]);
                        //    }
                        //    Lblvalue.Text = (meter / Qty).ToString("0.00");
                        //    count = ds2.Tables[0].Rows.Count;
                        //}

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            if (ds2.Tables[0].Rows[0]["AvgMeter"].ToString() == "")
                            {
                                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                                {
                                    meter = meter + Convert.ToDouble(ds2.Tables[0].Rows[i]["reqmeter"]);
                                    Qty = Qty + Convert.ToDouble(ds2.Tables[0].Rows[i]["Qty"]);
                                }
                                Lblvalue.Text = (meter / Qty).ToString("0.00");
                                count = ds2.Tables[0].Rows.Count;
                            }
                            else
                            {
                                Lblvalue.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["AvgMeter"]).ToString("f2");
                            }



                        }


                        DataSet ds23 = objBs.gettotalqtyCuttingprintreportnew(Convert.ToInt32(iSalesID));
                        if (ds23.Tables[0].Rows.Count > 0)
                        {

                            GridView1.DataSource = ds23;
                            GridView1.DataBind();
                        }



                        //DataSet ds234 = objBs.gettotalrateCuttingprintreport(Convert.ToInt32(iSalesID));
                        //if (ds234.Tables[0].Rows.Count > 0)
                        //{

                        //    GridView2.DataSource = ds234;
                        //    GridView2.DataBind();


                        //    double total = 90 + Convert.ToDouble(ds234.Tables[0].Rows[0]["tot"]) / Qty;
                        //    lblratee.Text = total.ToString("0.00");
                        //}

                        //DataSet dlabell = objBs.getcustomerlabels(Convert.ToInt32(iSalesID));
                        //if (dlabell.Tables[0].Rows.Count > 0)
                        //{
                        //    gridlabel.DataSource = dlabell;
                        //    gridlabel.DataBind();
                        //}

                        DataSet ProcessStatus = objBs.getProcessStatus(Convert.ToInt32(iSalesID));
                        if (ProcessStatus.Tables[0].Rows.Count > 0)
                        {
                            gvProcessStatus.DataSource = ProcessStatus;
                            gvProcessStatus.DataBind();
                        }

                        DataSet dsfablist = objBs.gettotalfablistreportnew(Convert.ToInt32(iSalesID));
                        if (dsfablist.Tables[0].Rows.Count > 0)
                        {
                            fablistcalcalcuationgrid.DataSource = dsfablist;
                            fablistcalcalcuationgrid.DataBind();
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Something Went Wrong in Pre-Cutting Master.Thank you!!!');", true);
                    return;
                }
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewcutting.aspx");
        }

        int tempcounter = 0;

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalfs = totalfs + Convert.ToDouble(e.Row.Cells[13].Text);
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //  e.Row.Cells[6].Text = "Total:";
                e.Row.Cells[13].Text = totalfs.ToString();
                // totalfs = totalfs + Convert.ToDouble(e.Row.Cells[7].Text);
                //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        protected void RatioShirtProcess_OnDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox S30FS = ((TextBox)e.Row.FindControl("S30FS"));
                //TextBox S32FS = ((TextBox)e.Row.FindControl("S32FS"));
                //TextBox S34FS = ((TextBox)e.Row.FindControl("S34FS"));
                //TextBox S36FS = ((TextBox)e.Row.FindControl("S36FS"));
                //TextBox SXSFS = ((TextBox)e.Row.FindControl("SXSFS"));
                //TextBox SSFS = ((TextBox)e.Row.FindControl("SSFS"));
                //TextBox SMFS = ((TextBox)e.Row.FindControl("SMFS"));
                //TextBox SLFS = ((TextBox)e.Row.FindControl("SLFS"));
                //TextBox SXLFS = ((TextBox)e.Row.FindControl("SXLFS"));
                //TextBox SXXLFS = ((TextBox)e.Row.FindControl("SXXLFS"));
                //TextBox S3XLFS = ((TextBox)e.Row.FindControl("S3XLFS"));
                //TextBox S4XLFS = ((TextBox)e.Row.FindControl("S4XLFS"));

                //TextBox S30HS = ((TextBox)e.Row.FindControl("S30HS"));
                //TextBox S32HS = ((TextBox)e.Row.FindControl("S32HS"));
                //TextBox S34HS = ((TextBox)e.Row.FindControl("S34HS"));
                //TextBox S36HS = ((TextBox)e.Row.FindControl("S36HS"));
                //TextBox SXSHS = ((TextBox)e.Row.FindControl("SXSHS"));
                //TextBox SSHS = ((TextBox)e.Row.FindControl("SSHS"));
                //TextBox SMHS = ((TextBox)e.Row.FindControl("SMHS"));
                //TextBox SLHS = ((TextBox)e.Row.FindControl("SLHS"));
                //TextBox SXLHS = ((TextBox)e.Row.FindControl("SXLHS"));
                //TextBox SXXLHS = ((TextBox)e.Row.FindControl("SXXLHS"));
                //TextBox S3XLHS = ((TextBox)e.Row.FindControl("S3XLHS"));
                //TextBox S4XLHS = ((TextBox)e.Row.FindControl("S4XLHS"));

                //TextBox Totalshirt = ((TextBox)e.Row.FindControl("Totalshirt"));
                ttlreqmeter = ttlreqmeter + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "reqmeter"));

                Q30F = Q30F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30FS"));
                Q32F = Q32F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32FS"));
                Q34F = Q34F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34FS"));
                Q36F = Q36F + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36FS"));
                QXSF = QXSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XSFS"));
                QSF = QSF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SFS"));
                QMF = QMF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MFS"));
                QLF = QLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "LFS"));
                QXLF = QXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XLFS"));
                QXXLF = QXXLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XXLFS"));
                Q3XLF = Q3XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3XLFS"));
                Q4XLF = Q4XLF + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4XLFS"));

                Q30H = Q30H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "30HS"));
                Q32H = Q32H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "32HS"));
                Q34H = Q34H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "34HS"));
                Q36H = Q36H + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "36HS"));
                QXSH = QXSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XSHS"));
                QSH = QSH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SHS"));
                QMH = QMH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "MHS"));
                QLH = QLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "LHS"));
                QXLH = QXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XLHS"));
                QXXLH = QXXLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "XXLHS"));
                Q3XLH = Q3XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "3XLHS"));
                Q4XLH = Q4XLH + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "4XLHS"));

                ttltotfs = ttltotfs + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "totfs"));
                ttltoths = ttltoths + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "toths"));

                #region




                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.Cells[5].Text = "-";
                }
                if (e.Row.Cells[6].Text == "0")
                {
                    e.Row.Cells[6].Text = "-";
                }
                if (e.Row.Cells[7].Text == "0")
                {
                    e.Row.Cells[7].Text = "-";
                }
                if (e.Row.Cells[8].Text == "0")
                {
                    e.Row.Cells[8].Text = "-";
                }
                if (e.Row.Cells[9].Text == "0")
                {
                    e.Row.Cells[9].Text = "-";
                }
                if (e.Row.Cells[10].Text == "0")
                {
                    e.Row.Cells[10].Text = "-";
                }
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
                #endregion

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total :";
                e.Row.Cells[3].Text = ttlreqmeter.ToString();

                e.Row.Cells[5].Text = Q30F.ToString();
                e.Row.Cells[6].Text = Q32F.ToString();
                e.Row.Cells[7].Text = Q34F.ToString();
                e.Row.Cells[8].Text = Q36F.ToString();
                e.Row.Cells[9].Text = QXSF.ToString();
                e.Row.Cells[10].Text = QSF.ToString();
                e.Row.Cells[11].Text = QMF.ToString();
                e.Row.Cells[12].Text = QLF.ToString();
                e.Row.Cells[13].Text = QXLF.ToString();
                e.Row.Cells[14].Text = QXXLF.ToString();
                e.Row.Cells[15].Text = Q3XLF.ToString();
                e.Row.Cells[16].Text = Q4XLF.ToString();

                e.Row.Cells[17].Text = Q30H.ToString();
                e.Row.Cells[18].Text = Q32H.ToString();
                e.Row.Cells[19].Text = Q34H.ToString();
                e.Row.Cells[20].Text = Q36H.ToString();
                e.Row.Cells[21].Text = QXSH.ToString();
                e.Row.Cells[22].Text = QSH.ToString();
                e.Row.Cells[23].Text = QMH.ToString();
                e.Row.Cells[24].Text = QLH.ToString();
                e.Row.Cells[25].Text = QXLH.ToString();
                e.Row.Cells[26].Text = QXXLH.ToString();
                e.Row.Cells[27].Text = Q3XLH.ToString();
                e.Row.Cells[28].Text = Q4XLH.ToString();

                e.Row.Cells[29].Text = ttltotfs.ToString();
                e.Row.Cells[30].Text = ttltoths.ToString();

                
                // e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        protected void Print(object sender, EventArgs e)
        {
            gridprint.UseAccessibleHeader = true;
            gridprint.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridprint.FooterRow.TableSection = TableRowSection.TableFooter;
            this.gridprint.ShowFooter = true;
            gridprint.Attributes["style"] = "border-collapse:separate";
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
                {
                    row.Attributes["style"] = "page-break-after:always;";
                }
            }
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridprint.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
            sb.Append(style + gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();");
            sb.Append("};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gridprint.AllowPaging = true;
            gridprint.DataBind();
        }


    }
}