using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;
using iTextSharp.text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web;

namespace Billing.Accountsbootstrap
{
    public partial class FullLotDetails : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";

        DataTable dt1;

        double totrawmat = 0;

        int StichTotalIssue = 0; int StichTotalReceive = 0; int StichTotalDamage = 0;
        int EmbTotalIssue = 0; int EmbTotalReceive = 0; int EmbTotalDamage = 0;
        int KajaTotalIssue = 0; int KajaTotalReceive = 0; int KajaTotalDamage = 0;
        int PrintTotalIssue = 0; int PrintTotalReceive = 0; int PrintTotalDamage = 0;
        int WashTotalIssue = 0; int WashTotalReceive = 0; int WashTotalDamage = 0;
        int BartagTotalIssue = 0; int BartagTotalReceive = 0; int BartagTotalDamage = 0;
        int TrimTotalIssue = 0; int TrimTotalReceive = 0; int TrimTotalDamage = 0;
        int ConsaiTotalIssue = 0; int ConsaiTotalReceive = 0; int ConsaiTotalDamage = 0;
        int IronTotalIssue = 0; int IronTotalReceive = 0; int IronTotalDamage = 0; int IronTotalalter = 0; double IronTotalPaidAmount = 0;

        double PaymentAmount = 0; int PaymentQuantity = 0; double PaymentDebitAmount = 0; double PaymentMiscellaneous = 0;


        double Reqmeter = 0; double Endbit = 0;

        int DespatchTotalIssue = 0; int DespatchTotalReceive = 0; int DespatchTotalDamage = 0;
        int DespatchreturnTotalIssue = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");



            string FullLotDetailsid = Request.QueryString.Get("FullLotDetailsid");


            sTableName = Session["User"].ToString();
            DataSet ds23 = new DataSet();
            if (FullLotDetailsid != null)
            {

                DataSet dstat = objBs.getstatusreport(Convert.ToInt32(FullLotDetailsid));
                if (dstat.Tables[0].Rows.Count > 0)
                {
                    string LotDetailId = FullLotDetailsid.ToString();

                    DataSet dsprecut = objBs.getsumofprecut(Convert.ToInt32(dstat.Tables[0].Rows[0]["Cutid"].ToString()));
                    DataSet dsmastercut = objBs.getsumofmastercut(Convert.ToInt32(FullLotDetailsid));

                    DataSet ds2 = objBs.JpFullLotDetailsnewpro(Convert.ToInt32(dstat.Tables[0].Rows[0]["Cutid"].ToString()));

                    if (ds2.Tables[0].Rows[0]["Narration"].ToString() == "")
                    {
                        lblLot.Text = ds2.Tables[0].Rows[0]["Companyfulllotno"].ToString();
                    }
                    else
                    {
                        lblLot.Text = ds2.Tables[0].Rows[0]["Companyfulllotno"].ToString() +"@"+ ds2.Tables[0].Rows[0]["Narration"].ToString();
                    }
                    lblDeldate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["deliverydate"]).ToString("dd/MM/yyyy");
                    lblLedgerName.Text = ds2.Tables[0].Rows[0]["LedgerName"].ToString();
                    lbllfit.Text = ds2.Tables[0].Rows[0]["Fit1"].ToString();

                    lblbrand.Text = ds2.Tables[0].Rows[0]["BrandName"].ToString();
                    lblItem.Text = ds2.Tables[0].Rows[0]["ItemName1"].ToString();

                    lblcompanyname.Text = ds2.Tables[0].Rows[0]["companyname1"].ToString();
                    lblShirtDescription.Text = ds2.Tables[0].Rows[0]["ItemName"].ToString() + " - " + ds2.Tables[0].Rows[0]["Fit1"].ToString();
                    lblCompleteStitching.Text = ds2.Tables[0].Rows[0]["CompleteStitching"].ToString();
                    lblLotCombination.Text = ds2.Tables[0].Rows[0]["LotCombination"].ToString();
                    if (dstat.Tables[0].Rows.Count > 0)
                    {
                        #region

                        DataSet Ds = new DataSet();
                        DataRow Dr;
                        DataTable Dt = new DataTable();
                        DataColumn Dc = new DataColumn();

                        Dc = new DataColumn("Process");
                        Dt.Columns.Add(Dc);

                        Dc = new DataColumn("TotalQty");
                        Dt.Columns.Add(Dc);
                        Ds.Tables.Add(Dt);

                        Dr = Dt.NewRow();
                        Dr["Process"] = "Pre Cutting";
                        Dr["TotalQty"] = dsprecut.Tables[0].Rows[0]["Total"].ToString();
                        Ds.Tables[0].Rows.Add(Dr);

                        Dr = Dt.NewRow();
                        Dr["Process"] = "Master Cutting";
                        Dr["TotalQty"] = dsmastercut.Tables[0].Rows[0]["Total"].ToString();
                        Ds.Tables[0].Rows.Add(Dr);


                        GVCutting.DataSource = Ds;
                        GVCutting.DataBind();

                        #endregion

                        #region
                        DataSet detailforfab = new DataSet();
                        detailforfab = objBs.getdetailforfab(Convert.ToInt32(FullLotDetailsid));
                        GVFabricdetails.DataSource = detailforfab;
                        GVFabricdetails.DataBind();
                        #endregion

                        #region
                        string CompanyLotNo = "";
                        for (int i = 0; i < dstat.Tables[0].Rows.Count; i++)
                        {
                            string currentststus = dstat.Tables[0].Rows[i]["Currentstatus"].ToString();
                            string ststus = dstat.Tables[0].Rows[i]["Status"].ToString();
                            string screen = dstat.Tables[0].Rows[i]["Screen"].ToString();

                            CompanyLotNo = dstat.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                            if (screen == "Stc")
                            {
                                DataSet dsStiching = new DataSet();
                                dsStiching = objBs.JpAllProcessLotDeatils("tblJpStiching", Convert.ToInt32(LotDetailId));
                                GVJpStiching.DataSource = dsStiching;
                                GVJpStiching.DataBind();
                            }

                            if (screen == "Kaja")
                            {
                                DataSet dsKajaButton = new DataSet();
                                dsKajaButton = objBs.JpAllProcessLotDeatils("tblJpKajaButton", Convert.ToInt32(LotDetailId));
                                GVJpKajaButton.DataSource = dsKajaButton;
                                GVJpKajaButton.DataBind();
                            }
                            if (screen == "Emb")
                            {
                                DataSet dsEmbroiding = new DataSet();
                                dsEmbroiding = objBs.JpAllProcessLotDeatils("tblJpEmbroiding", Convert.ToInt32(LotDetailId));
                                GVJpEmbroiding.DataSource = dsEmbroiding;
                                GVJpEmbroiding.DataBind();
                            }
                            if (screen == "Wash")
                            {
                                DataSet dsWashing = new DataSet();
                                dsWashing = objBs.JpAllProcessLotDeatils("tblJpWashing", Convert.ToInt32(LotDetailId));
                                GVJpWashing.DataSource = dsWashing;
                                GVJpWashing.DataBind();
                            }
                            if (screen == "Print")
                            {
                                DataSet dsPrinting = new DataSet();
                                dsPrinting = objBs.JpAllProcessLotDeatils("tblJpPrinting", Convert.ToInt32(LotDetailId));
                                GVJpPrinting.DataSource = dsPrinting;
                                GVJpPrinting.DataBind();
                            }
                            if (screen == "Iron")
                            {
                                DataSet dsIroning = new DataSet();
                                dsIroning = objBs.JpAllProcessLotDeatilsiron("tblJpIroning", Convert.ToInt32(LotDetailId));
                                GVJpIroning.DataSource = dsIroning;
                                GVJpIroning.DataBind();
                            }
                            if (screen == "Btag")
                            {
                                DataSet dsBarTag = new DataSet();
                                dsBarTag = objBs.JpAllProcessLotDeatils("tblJpBarTag", Convert.ToInt32(LotDetailId));
                                GVJpBarTag.DataSource = dsBarTag;
                                GVJpBarTag.DataBind();
                            }
                            if (screen == "Trm")
                            {
                                DataSet dsTrimming = new DataSet();
                                dsTrimming = objBs.JpAllProcessLotDeatils("tblJpTrimming", Convert.ToInt32(LotDetailId));
                                GVJpTrimming.DataSource = dsTrimming;
                                GVJpTrimming.DataBind();
                            }
                            if (screen == "Cni")
                            {
                                DataSet dsConsai = new DataSet();
                                dsConsai = objBs.JpAllProcessLotDeatils("tblJpConsai", Convert.ToInt32(LotDetailId));
                                GVJpConsai.DataSource = dsConsai;
                                GVJpConsai.DataBind();
                            }


                        }



                        DataSet dsCompanyLotNo = new DataSet();
                        dsCompanyLotNo = objBs.JpAllProcessdespatch(CompanyLotNo);
                        GVDespatchstock.DataSource = dsCompanyLotNo;
                        GVDespatchstock.DataBind();

                        DataSet dseturn = new DataSet();
                        dseturn = objBs.JpAllProcessdespatchreturn(CompanyLotNo);
                        GVDespatchReturn.DataSource = dseturn;
                        GVDespatchReturn.DataBind();


                        DataSet dsCompanyLotNofinish = new DataSet();
                        dsCompanyLotNofinish = objBs.JpAllProcessfinish(CompanyLotNo);
                        GVFinishedStock.DataSource = dsCompanyLotNofinish;
                        GVFinishedStock.DataBind();

                        //btnExport_Click(sender, e);


                        #endregion


                        #region Payment
                        DataSet dspayment = new DataSet();
                        dspayment = objBs.getpaymentforfulldetails(ds2.Tables[0].Rows[0]["Companyfulllotno"].ToString());
                        GVPaymentDetails.DataSource = dspayment;
                        GVPaymentDetails.DataBind();
                        #endregion

                    }


                    DataSet drawmater = objBs.getusedrawmaterials(FullLotDetailsid);
                    if (drawmater.Tables[0].Rows.Count > 0)
                    {
                        gridrawmaterial.DataSource = drawmater;
                        gridrawmaterial.DataBind();
                    }
                    else
                    {
                        gridrawmaterial.DataSource = null;
                        gridrawmaterial.DataBind();
                    }
                }


            }


        }

        public void ExportPDF()
        {
            DataSet dsCompanyLotNo = new DataSet();
            dsCompanyLotNo = objBs.JpAllProcessdespatch("RPL-3041");
            GVDespatchstock.DataSource = dsCompanyLotNo;
            GVDespatchstock.DataBind();

            DataSet dsIroning = new DataSet();
            dsIroning = objBs.JpAllProcessLotDeatils("tblJpIroning", Convert.ToInt32(302));
            GVJpIroning.DataSource = dsIroning;
            GVJpIroning.DataBind();

            DataSet dsIroningk = new DataSet();
            dsIroningk = objBs.JpAllProcessLotDeatils("tblJpKajaButton", Convert.ToInt32(302));
            GVJpIroning.DataSource = dsIroningk;
            GVJpIroning.DataBind();


            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.pdf", "PDFExport"));
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //For First DataTable
            System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite1 = new HtmlTextWriter(stringWrite1);
            DataGrid myDataGrid1 = new DataGrid();
            myDataGrid1.DataSource = dsIroning;
            myDataGrid1.DataBind();
            myDataGrid1.RenderControl(htmlWrite1);

            //For Second DataTable 
            System.IO.StringWriter stringWrite2 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);
            DataGrid myDataGrid2 = new DataGrid();
            myDataGrid2.DataSource = dsIroningk;
            myDataGrid2.DataBind();
            myDataGrid2.RenderControl(htmlWrite2);
            //You can add more DataTable
            StringReader sr = new StringReader(stringWrite1.ToString() + stringWrite2.ToString());
            Document pdfDoc = new Document(new Rectangle(288f, 144f), 10f, 10f, 10f, 0f);
            pdfDoc.SetPageSize(PageSize.A4.Rotate());
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.Response.End();
        }
        protected void btnExport_Click1(object sender, EventArgs e)
        {


            ExportPDF();


            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.Page.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();


        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnExport_Clicknew(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=UserDetails.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GVJpIroning.AllowPaging = false;
            GVJpIroning.DataBind();
            GVJpIroning.RenderControl(hw);
            GVJpIroning.HeaderRow.Style.Add("width", "15%");
            GVJpIroning.HeaderRow.Style.Add("font-size", "10px");
            GVJpIroning.Style.Add("text-decoration", "none");
            GVJpIroning.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
            GVJpIroning.Style.Add("font-size", "8px");
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A2, 7f, 7f, 7f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }


        protected void GVJpStiching_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "TotalIssue") != "")
                {
                    StichTotalIssue = StichTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "TotalReceive") != "")
                {
                    StichTotalReceive = StichTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                }
                if (DataBinder.Eval(e.Row.DataItem, "TotalDamage") != null)
                {
                    StichTotalDamage = StichTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = StichTotalIssue.ToString(); ;
                e.Row.Cells[4].Text = StichTotalReceive.ToString(); ;
                e.Row.Cells[5].Text = StichTotalDamage.ToString(); ;

            }
            #endregion
        }
        protected void GVJpEmbroiding_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                EmbTotalIssue = EmbTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                EmbTotalReceive = EmbTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                EmbTotalDamage = EmbTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = EmbTotalIssue.ToString(); ;
                e.Row.Cells[4].Text = EmbTotalReceive.ToString(); ;
                e.Row.Cells[5].Text = EmbTotalDamage.ToString(); ;

            }
            #endregion
        }
        protected void GVJpKajaButton_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                KajaTotalIssue = KajaTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                KajaTotalReceive = KajaTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                KajaTotalDamage = KajaTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = KajaTotalIssue.ToString(); ;
                e.Row.Cells[4].Text = KajaTotalReceive.ToString(); ;
                e.Row.Cells[5].Text = KajaTotalDamage.ToString(); ;

            }
            #endregion
        }
        protected void GVJpPrinting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PrintTotalIssue = PrintTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                PrintTotalReceive = PrintTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                PrintTotalDamage = PrintTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = PrintTotalIssue.ToString(); ;
                e.Row.Cells[4].Text = PrintTotalReceive.ToString(); ;
                e.Row.Cells[5].Text = PrintTotalDamage.ToString(); ;

            }
            #endregion
        }
        protected void GVJpWashing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                WashTotalIssue = WashTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                WashTotalReceive = WashTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                WashTotalDamage = WashTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = WashTotalIssue.ToString(); ;
                e.Row.Cells[4].Text = WashTotalReceive.ToString(); ;
                e.Row.Cells[5].Text = WashTotalDamage.ToString(); ;

            }
            #endregion
        }
        protected void GVJpBarTag_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BartagTotalIssue = BartagTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                BartagTotalReceive = BartagTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                BartagTotalDamage = BartagTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = BartagTotalIssue.ToString(); ;
                e.Row.Cells[4].Text = BartagTotalReceive.ToString(); ;
                e.Row.Cells[5].Text = BartagTotalDamage.ToString(); ;

            }
            #endregion
        }
        protected void GVJpTrimming_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TrimTotalIssue = TrimTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                TrimTotalReceive = TrimTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                TrimTotalDamage = TrimTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = TrimTotalIssue.ToString(); ;
                e.Row.Cells[4].Text = TrimTotalReceive.ToString(); ;
                e.Row.Cells[5].Text = TrimTotalDamage.ToString(); ;

            }
            #endregion
        }
        protected void GVJpConsai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ConsaiTotalIssue = ConsaiTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                ConsaiTotalReceive = ConsaiTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                ConsaiTotalDamage = ConsaiTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = ConsaiTotalIssue.ToString(); ;
                e.Row.Cells[4].Text = ConsaiTotalReceive.ToString(); ;
                e.Row.Cells[5].Text = ConsaiTotalDamage.ToString(); ;

            }
            #endregion
        }
        protected void GVJpIroning_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IronTotalPaidAmount = IronTotalPaidAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PaidAmount"));
                IronTotalIssue = IronTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                IronTotalReceive = IronTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                IronTotalDamage = IronTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
                IronTotalalter = IronTotalalter + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AlterQty"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total:";
                e.Row.Cells[2].Text = IronTotalPaidAmount.ToString("f2");
                e.Row.Cells[3].Text = IronTotalIssue.ToString();
                e.Row.Cells[4].Text = IronTotalReceive.ToString();
                e.Row.Cells[5].Text = IronTotalDamage.ToString();
                e.Row.Cells[6].Text = IronTotalalter.ToString();
                

            }
            #endregion
        }

        protected void GVDespatchstock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DespatchTotalIssue = DespatchTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDespatchqty"));
                // DespatchTotalReceive = DespatchTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                // DespatchTotalDamage = DespatchTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = DespatchTotalIssue.ToString(); ;
                //  e.Row.Cells[4].Text = DespatchTotalReceive.ToString(); ;
                // e.Row.Cells[5].Text = DespatchTotalDamage.ToString(); ;

            }
            #endregion
        }

        protected void GVDespatchReturn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DespatchreturnTotalIssue = DespatchreturnTotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDespatchqty"));
                // DespatchTotalReceive = DespatchTotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                // DespatchTotalDamage = DespatchTotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = DespatchreturnTotalIssue.ToString(); ;
                //  e.Row.Cells[4].Text = DespatchTotalReceive.ToString(); ;
                // e.Row.Cells[5].Text = DespatchTotalDamage.ToString(); ;

            }
            #endregion
        }


        protected void GVFabricdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Reqmeter = Reqmeter + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Reqmeter"));
                Endbit = Endbit + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Endbit"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "Total:";
                e.Row.Cells[5].Text = Reqmeter.ToString();
                e.Row.Cells[6].Text = Endbit.ToString();


            }
            #endregion
        }


        protected void GVPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PaymentAmount = PaymentAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                PaymentQuantity = PaymentQuantity + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                PaymentDebitAmount = PaymentDebitAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DebitAmount"));
                PaymentMiscellaneous = PaymentMiscellaneous + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Miscellaneous"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = PaymentAmount.ToString("f2");
                e.Row.Cells[4].Text = PaymentQuantity.ToString();
                e.Row.Cells[6].Text = PaymentDebitAmount.ToString("f2");
                e.Row.Cells[7].Text = PaymentMiscellaneous.ToString("f2");

                lblExpensesamt.Text = PaymentAmount.ToString("f2");
                lblDebitamt.Text = PaymentDebitAmount.ToString("f2");
                lblMiscellaneousamt.Text = PaymentMiscellaneous.ToString("f2");

                lblLotValueamt.Text = ((PaymentAmount + PaymentMiscellaneous) - PaymentDebitAmount).ToString("f2");
            }
            #endregion
        }

        protected void Gridrawmaterial_rowdatabound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totrawmat = totrawmat + Convert.ToDouble(e.Row.Cells[2].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = totrawmat.ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }
}