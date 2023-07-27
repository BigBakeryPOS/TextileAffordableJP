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
using System.IO;
using System.Globalization;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text;
//using iTextSharp.tool.xml;

namespace Billing.Accountsbootstrap
{
    public partial class FullFormReport : System.Web.UI.Page
    {
        #region

        BSClass objBs = new BSClass();

        int Stichingissu = 0;
        int Stichingrec = 0;
        int Stichingdam = 0;

        int Embissu = 0;
        int Embrec = 0;
        int Embdam = 0;

        int Kajaissu = 0;
        int Kajarec = 0;
        int Kajadam = 0;

        int Printissu = 0;
        int Printrec = 0;
        int Printdam = 0;

        int washissu = 0;
        int washrec = 0;
        int washdam = 0;

        int Bartagissu = 0;
        int Bartagrec = 0;
        int Bartagdam = 0;

        int Trimissu = 0;
        int Trimrec = 0;
        int Trimdam = 0;

        int Consaiissu = 0;
        int Consairec = 0;
        int Consaidam = 0;

        int Ironissujp = 0;
        int Ironrecjp = 0;
        int Irondamjp = 0;
        int Ironalterjp = 0;

        int Ironissujobworker = 0;
        int Ironrecjobworker = 0;
        int Irondamjobworker = 0;
        int Ironalterjobworker = 0;

        int DespatchTotalQty = 0; int DespatchTotalReceive = 0; int DespatchTotalDamage = 0;

        int DespatchReturnTotalQty = 0;

        int PaymentTotalQuantityjp = 0; double PaymentTotalAmountjp = 0;
        int PaymentTotalQuantityjobworker = 0; double PaymentTotalAmountjobworker = 0;


        double preReqmeter = 0; int preReqShirt = 0; double masterReqmeter = 0; int masterReqShirt = 0; double FabReqmeter = 0; double EndbiGivenmeter = 0; double EndbiReqmeter = 0; double EndbiEndbit = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                //DataSet dss = objBs.GetSupplierLedgername();
                //if (dss.Tables[0].Rows.Count > 0)
                //{
                //    ddlsupplier.DataSource = dss.Tables[0];
                //    ddlsupplier.DataTextField = "LedgerName";
                //    ddlsupplier.DataValueField = "LedgerID";
                //    ddlsupplier.DataBind();
                //    ddlsupplier.Items.Insert(0, "ALL");
                //}



                //DataSet dscompany = objBs.Getcompanyyname();
                //if (dscompany.Tables[0].Rows.Count > 0)
                //{
                //    ddlcompany.DataSource = dscompany.Tables[0];
                //    ddlcompany.DataTextField = "CompanyName";
                //    ddlcompany.DataValueField = "ComapanyID";
                //    ddlcompany.DataBind();
                //    ddlcompany.Items.Insert(0, "ALL");
                //}

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


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

            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void btnShow_OnClick(object sender, EventArgs e)
        {

            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            pdfDoc.Open();

            string val = 0.ToString();

            pdfDoc.Add(new Paragraph(val));

            pdfDoc.Close();

            Response.ContentType = "application/pdf";

            Response.AddHeader("content-disposition", "attachment;" +

                                           "filename=sample.pdf");

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Write(pdfDoc);

            Response.End();

        }


        protected void btnExport_Click(object sender, EventArgs e)
        {
            //ExportPDF();
          //  ExportToPDF(sender,e);

            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);

            //// lblUsername1.Visible = true;
            ////lblUsername1.Text = "Dear " + fname.Text + ' ' + mname.Text + ' ' + lname.Text + ',' + "<br/>";
            //panel2.Visible = true;
            //GVPreCutting.ShowHeader = true;
            //GVPreCutting.ShowFooter = true;

            //// img1.ImageUrl = Server.MapPath(@"images/logo.png");
            //panel2.RenderControl(hw);

            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
            //Response.End();


            // Response.ContentType = "application/pdf";
            //// Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
            // Response.AddHeader("content-disposition", "attachment;filename=Panel2.pdf");
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            // StringWriter sw = new StringWriter();
            // HtmlTextWriter hw = new HtmlTextWriter(sw);

            // panel2.Visible = true;
            // panel2.Page.RenderControl(hw);

            // StringReader sr = new StringReader(sw.ToString());
            // Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            // HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            // PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            // pdfDoc.Open();
            // htmlparser.Parse(sr);
            // pdfDoc.Close();
            // Response.Write(pdfDoc);
            // Response.End();


            /////////////////////////////////////

            //Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            //Graphics graphics = Graphics.FromImage(bitmap as Image);

            //graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            //bitmap.Save("c:\\screenshot.jpeg", ImageFormat.Jpeg);
        }

        public void ExportPDF()
        {

            DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataSet dsmasterDay = objBs.masterDay(FromDate, ToDate, drpbranch.SelectedValue);


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
            myDataGrid1.DataSource = dsmasterDay;
            myDataGrid1.DataBind();
            myDataGrid1.RenderControl(htmlWrite1);

            //For Second DataTable 
            System.IO.StringWriter stringWrite2 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite2 = new HtmlTextWriter(stringWrite2);
            DataGrid myDataGrid2 = new DataGrid();
            myDataGrid2.DataSource = dsmasterDay;
            myDataGrid2.DataBind();
            myDataGrid2.RenderControl(htmlWrite2);

            ////For Second DataTable 
            System.IO.StringWriter stringWrite3 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite3 = new HtmlTextWriter(stringWrite3);
            DataGrid myDataGrid3 = new DataGrid();
            myDataGrid3.DataSource = dsmasterDay;
            myDataGrid3.DataBind();
            myDataGrid3.RenderControl(htmlWrite3);

            //You can add more DataTable
            StringReader sr = new StringReader(stringWrite1.ToString() + "  <br />" + stringWrite2.ToString() + "  <br />" + stringWrite3.ToString() + "  <br />" + "  <br />");
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
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //       server control at run time. */
        //}

        //protected void ExportToPDF(object sender, EventArgs e)
        //{
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //        {
        //            //gvSalesValue.HeaderRow.Cells[7].Visible = false;
        //            //gvSalesValue.HeaderRow.Cells[12].Visible = false;
        //            //gvSalesValue.HeaderRow.Cells[13].Visible = false;
        //            ////GridViewm.HeaderRow.Cells[16].Visible = false;
        //            //gvSalesValue.HeaderRow.Cells[17].Visible = false;

        //            panel1.RenderControl(hw);

        //            //panel1.HeaderRow.Style.Add("width", "6%");
        //            //panel1.HeaderRow.Style.Add("font-size", "8px");
        //            panel1.Style.Add("text-decoration", "none");
        //            panel1.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
        //            panel1.Style.Add("font-size", "6px");

        //            StringReader sr = new StringReader(sw.ToString());
        //            Document pdfDoc = new Document(PageSize.A2, 7f, 7f, 7f, 0f);
        //            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //            pdfDoc.Open();

        //            XMLWorkerHelper.GetInstance().ParseXHtml(pdfWrite, docWorkingDocument, srdDocToString);

        //            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //            pdfDoc.Close();
        //            Response.ContentType = "application/pdf";
        //            Response.AddHeader("content-disposition", "attachment;filename=MonthlyReport.pdf");
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //            Response.Write(pdfDoc);
        //            Response.End();
        //        }
        //    }
        //}
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */

            //txtFrom.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
            //txtto.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //From = DateTime.Parse(txtFrom.Text.Trim(), Cul, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //To = DateTime.Parse(txtto.Text.Trim(), Cul, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            //DataSet ds = objbs.GetMarketingEdit_Report(Convert.ToDateTime(From), Convert.ToDateTime(To));
            //GridViewm.DataSource = ds.Tables[0];
            //GridViewm.DataBind();
        }

        //private string createPDFFromHtml(string htmlString, string outputFileName)
        //{
        //    string result = string.Empty;

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(htmlString) && !string.IsNullOrEmpty(outputFileName) && !File.Exists(outputFileName))
        //        {
        //            using (FileStream fos = new FileStream(outputFileName, FileMode.Create))
        //            {
        //                using (MemoryStream inputMemoryStream = new MemoryStream(Encoding.ASCII.GetBytes(htmlString)))
        //                {
        //                    using (TextReader textReader = new StreamReader(inputMemoryStream, Encoding.ASCII))
        //                    {
        //                        using (Document pdfDoc = new Document())
        //                        {
        //                            using (PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, fos))
        //                            {
        //                                XMLWorkerHelper helper = XMLWorkerHelper.GetInstance();
        //                                pdfDoc.Open();
        //                                helper.ParseXHtml(pdfWriter, pdfDoc, textReader);
        //                                result = "Successfully Created new HTML--> PDF Document!";
        //                                pdfWriter.CloseStream = false;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "Exception: " + ex.Message;
        //    }

        //    return result;
        //}

        protected void Search_Click(object sender, EventArgs e)
        {

            #region
            lblpreReqmeter.Text = "0";
            lblpreReqShirt.Text = "0";

            lblmasterReqmeter.Text = "0";
            lblmasterReqShirt.Text = "0";

            lblStichingissu.Text = "0";
            lblStichingrec.Text = "0";
            lblStichingdam.Text = "0";
            lblStitchingpaidamt.Text = "0";

            lblEmbissu.Text = "0";
            lblEmbrec.Text = "0";
            lblEmbdam.Text = "0";
            lblEmbroidingpaidamt.Text = "0";

            lblKajaissu.Text = "0";
            lblKajarec.Text = "0";
            lblKajadam.Text = "0";
            lblKajapaidamt.Text = "0";

            lblwashissu.Text = "0";
            lblwashrec.Text = "0";
            lblwashdam.Text = "0";
            lblWashingpaidamt.Text = "0";

            lblPrintissu.Text = "0";
            lblPrintrec.Text = "0";
            lblPrintdam.Text = "0";
            lblPrintingpaidamt.Text = "0";

            lblBartagissu.Text = "0";
            lblBartagrec.Text = "0";
            lblBartagdam.Text = "0";
            lblBartagpaidamt.Text = "0";

            lblTrimissu.Text = "0";
            lblTrimrec.Text = "0";
            lblTrimdam.Text = "0";
            lblTrimmingpaidamt.Text = "0";


            lblConsaiissu.Text = "0";
            lblConsairec.Text = "0";
            lblConsaidam.Text = "0";
            lblConsaipaidamt.Text = "0";

            lblIronissujp.Text = "0";
            lblIronrecjp.Text = "0";
            lblIrondamjp.Text = "0";
            lblIronalterjp.Text = "0";
            lblIroningpaidamtjp.Text = "0";

            lblIronissujobbworker.Text = "0";
            lblIronrecjobbworker.Text = "0";
            lblIrondamjobbworker.Text = "0";
            lblIronalterjobbworker.Text = "0";
            lblIroningpaidamtjobbworker.Text = "0";

            lbltotalpaidamount.Text = "0";
            lbltotalironpaidamount.Text = "0";
            lblGrandTotalAmount.Text = "0";

            #endregion

            DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            #region Pre and Master

            DataSet dsCuttingDay = objBs.CuttingDay(FromDate, ToDate, drpbranch.SelectedValue);
            if (dsCuttingDay.Tables[0].Rows.Count > 0)
            {
                GVPreCutting.DataSource = dsCuttingDay;
                GVPreCutting.DataBind();
            }
            else
            {
                GVPreCutting.DataSource = null;
                GVPreCutting.DataBind();
            }
            DataSet dsmasterDay = objBs.masterDay(FromDate, ToDate, drpbranch.SelectedValue);
            if (dsmasterDay.Tables[0].Rows.Count > 0)
            {
                GVMasterCutting.DataSource = dsmasterDay;
                GVMasterCutting.DataBind();
            }
            else
            {
                GVMasterCutting.DataSource = null;
                GVMasterCutting.DataBind();
            }
            DataSet dsFabDay = objBs.FabDay(FromDate, ToDate, drpbranch.SelectedValue);
            if (dsFabDay.Tables[0].Rows.Count > 0)
            {
                GVFab.DataSource = dsFabDay;
                GVFab.DataBind();
            }
            else
            {
                GVFab.DataSource = null;
                GVFab.DataBind();
            }
            DataSet dsEndbitDay = objBs.EndbitDay(FromDate, ToDate, drpbranch.SelectedValue);
            if (dsEndbitDay.Tables[0].Rows.Count > 0)
            {
                GVEndbitDay.DataSource = dsEndbitDay;
                GVEndbitDay.DataBind();
            }
            else
            {
                GVEndbitDay.DataSource = null;
                GVEndbitDay.DataBind();
            }

            #endregion

            DataSet dsissu = new DataSet();
            DataSet dsrec = new DataSet();
            DataSet dsdam = new DataSet();
            DataSet dsalter = new DataSet();

            #region Stitching Process :-
            dsissu = objBs.proissu("tblJpStiching", "tbltransjpstichinghistory", "StichingId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVJpStichingissu.DataSource = dsissu;
                GVJpStichingissu.DataBind();
            }
            else
            {
                GVJpStichingissu.DataSource = null;
                GVJpStichingissu.DataBind();
            }
            dsrec = objBs.proissu("tblJpStiching", "tbltransjpstichinghistory", "StichingId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVJpStichingrec.DataSource = dsrec;
                GVJpStichingrec.DataBind();
            }
            else
            {
                GVJpStichingrec.DataSource = null;
                GVJpStichingrec.DataBind();
            }
            dsdam = objBs.proissu("tblJpStiching", "tbltransjpstichinghistory", "StichingId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVJpStichingdam.DataSource = dsdam;
                GVJpStichingdam.DataBind();
            }
            else
            {
                GVJpStichingdam.DataSource = null;
                GVJpStichingdam.DataBind();
            }
            #endregion

            #region Emb Process :-
            dsissu = objBs.proissu("tblJpEmbroiding", "tbltransjpEmbroidinghistory", "EmbroidingId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVEmbissu.DataSource = dsissu;
                GVEmbissu.DataBind();
            }
            else
            {
                GVEmbissu.DataSource = null;
                GVEmbissu.DataBind();
            }
            dsrec = objBs.proissu("tblJpEmbroiding", "tbltransjpEmbroidinghistory", "EmbroidingId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVEmbrec.DataSource = dsrec;
                GVEmbrec.DataBind();
            }
            else
            {
                GVEmbrec.DataSource = null;
                GVEmbrec.DataBind();
            }
            dsdam = objBs.proissu("tblJpEmbroiding", "tbltransjpEmbroidinghistory", "EmbroidingId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVEmbdam.DataSource = dsdam;
                GVEmbdam.DataBind();
            }
            else
            {
                GVEmbdam.DataSource = null;
                GVEmbdam.DataBind();
            }
            #endregion

            #region Wash Process :-
            dsissu = objBs.proissu("tblJpWashing", "tbltransjpWashinghistory", "WashingId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVwashissu.DataSource = dsissu;
                GVwashissu.DataBind();
            }
            else
            {
                GVwashissu.DataSource = null;
                GVwashissu.DataBind();
            }
            dsrec = objBs.proissu("tblJpWashing", "tbltransjpWashinghistory", "WashingId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVwashrec.DataSource = dsrec;
                GVwashrec.DataBind();
            }
            else
            {
                GVwashrec.DataSource = null;
                GVwashrec.DataBind();
            }
            dsdam = objBs.proissu("tblJpWashing", "tbltransjpWashinghistory", "WashingId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVwashdam.DataSource = dsdam;
                GVwashdam.DataBind();
            }
            else
            {
                GVwashdam.DataSource = null;
                GVwashdam.DataBind();
            }
            #endregion

            #region Print Process :-
            dsissu = objBs.proissu("tblJpPrinting", "tbltransjpPrintinghistory", "PrintingId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVPrintissu.DataSource = dsissu;
                GVPrintissu.DataBind();
            }
            else
            {
                GVPrintissu.DataSource = null;
                GVPrintissu.DataBind();
            }
            dsrec = objBs.proissu("tblJpPrinting", "tbltransjpPrintinghistory", "PrintingId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVPrintrec.DataSource = dsrec;
                GVPrintrec.DataBind();
            }
            else
            {
                GVPrintrec.DataSource = null;
                GVPrintrec.DataBind();
            }
            dsdam = objBs.proissu("tblJpPrinting", "tbltransjpPrintinghistory", "PrintingId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVPrintdam.DataSource = dsdam;
                GVPrintdam.DataBind();
            }
            else
            {
                GVPrintdam.DataSource = null;
                GVPrintdam.DataBind();
            }
            #endregion

            #region Kaja Process :-
            dsissu = objBs.proissu("tblJpKajaButton", "tbltransjpKajaButtonhistory", "KajaButtonId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVKajaissu.DataSource = dsissu;
                GVKajaissu.DataBind();
            }
            else
            {
                GVKajaissu.DataSource = null;
                GVKajaissu.DataBind();
            }
            dsrec = objBs.proissu("tblJpKajaButton", "tbltransjpKajaButtonhistory", "KajaButtonId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVKajarec.DataSource = dsrec;
                GVKajarec.DataBind();
            }
            else
            {
                GVKajarec.DataSource = null;
                GVKajarec.DataBind();
            }
            dsdam = objBs.proissu("tblJpKajaButton", "tbltransjpKajaButtonhistory", "KajaButtonId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVKajadam.DataSource = dsdam;
                GVKajadam.DataBind();
            }
            else
            {
                GVKajadam.DataSource = null;
                GVKajadam.DataBind();
            }
            #endregion

            #region BarTag Process :-
            dsissu = objBs.proissu("tblJpBarTag", "tbltransjpBarTaghistory", "BarTagId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVBartagissu.DataSource = dsissu;
                GVBartagissu.DataBind();
            }
            else
            {
                GVBartagissu.DataSource = null;
                GVBartagissu.DataBind();
            }
            dsrec = objBs.proissu("tblJpBarTag", "tbltransjpBarTaghistory", "BarTagId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVBartagrec.DataSource = dsrec;
                GVBartagrec.DataBind();
            }
            else
            {
                GVBartagrec.DataSource = null;
                GVBartagrec.DataBind();
            }
            dsdam = objBs.proissu("tblJpBarTag", "tbltransjpBarTaghistory", "BarTagId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVBartagdam.DataSource = dsdam;
                GVBartagdam.DataBind();
            }
            else
            {
                GVBartagdam.DataSource = null;
                GVBartagdam.DataBind();
            }
            #endregion

            #region Trimm Process :-
            dsissu = objBs.proissu("tblJpTrimming", "tbltransjpTrimminghistory", "TrimmingId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVTrimissu.DataSource = dsissu;
                GVTrimissu.DataBind();
            }
            else
            {
                GVTrimissu.DataSource = null;
                GVTrimissu.DataBind();
            }
            dsrec = objBs.proissu("tblJpTrimming", "tbltransjpTrimminghistory", "TrimmingId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVTrimrec.DataSource = dsrec;
                GVTrimrec.DataBind();
            }
            else
            {
                GVTrimrec.DataSource = null;
                GVTrimrec.DataBind();
            }
            dsdam = objBs.proissu("tblJpTrimming", "tbltransjpTrimminghistory", "TrimmingId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVTrimdam.DataSource = dsdam;
                GVTrimdam.DataBind();
            }
            else
            {
                GVTrimdam.DataSource = null;
                GVTrimdam.DataBind();
            }
            #endregion

            #region Consai Process :-
            dsissu = objBs.proissu("tblJpConsai", "tbltransjpConsaihistory", "ConsaiId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVConsaiissu.DataSource = dsissu;
                GVConsaiissu.DataBind();
            }
            else
            {
                GVConsaiissu.DataSource = null;
                GVConsaiissu.DataBind();
            }
            dsrec = objBs.proissu("tblJpConsai", "tbltransjpConsaihistory", "ConsaiId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVConsairec.DataSource = dsrec;
                GVConsairec.DataBind();
            }
            else
            {
                GVConsairec.DataSource = null;
                GVConsairec.DataBind();
            }
            dsdam = objBs.proissu("tblJpConsai", "tbltransjpConsaihistory", "ConsaiId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVConsaidam.DataSource = dsdam;
                GVConsaidam.DataBind();
            }
            else
            {
                GVConsaidam.DataSource = null;
                GVConsaidam.DataBind();
            }
            #endregion

            #region Iron Process JP:-
            dsissu = objBs.proissuforironjpiss("tbljpIroning ", "tbltransjpIroninghistory", "IroningId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVIronissujp.DataSource = dsissu;
                GVIronissujp.DataBind();
            }
            else
            {
                GVIronissujp.DataSource = null;
                GVIronissujp.DataBind();
            }
            dsrec = objBs.proissuforironjp("tbljpIroning ", "tbltransjpIroninghistory", "IroningId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVIronrecjp.DataSource = dsrec;
                GVIronrecjp.DataBind();
            }
            else
            {
                GVIronrecjp.DataSource = null;
                GVIronrecjp.DataBind();
            }
            dsdam = objBs.proissuforironjp("tbljpIroning ", "tbltransjpIroninghistory", "IroningId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVIrondamjp.DataSource = dsdam;
                GVIrondamjp.DataBind();
            }
            else
            {
                GVIrondamjp.DataSource = null;
                GVIrondamjp.DataBind();
            }

            dsalter = objBs.proissuforironjp("tbljpIroning ", "tbltransjpIroninghistory", "IroningId", "Alter", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsalter.Tables[0].Rows.Count > 0)
            {
                GVIronalterjp.DataSource = dsalter;
                GVIronalterjp.DataBind();
            }
            else
            {
                GVIronalterjp.DataSource = null;
                GVIronalterjp.DataBind();
            }
            #endregion

            #region Iron Process JobWorker:-
            dsissu = objBs.proissuforironjobworkerissue("tbljpIroning ", "tbltransjpIroninghistory", "IroningId", "Issue", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissu.Tables[0].Rows.Count > 0)
            {
                GVIronissujobworker.DataSource = dsissu;
                GVIronissujobworker.DataBind();
            }
            else
            {
                GVIronissujobworker.DataSource = null;
                GVIronissujobworker.DataBind();
            }
            dsrec = objBs.proissuforironjobworker("tbljpIroning ", "tbltransjpIroninghistory", "IroningId", "Receive", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsrec.Tables[0].Rows.Count > 0)
            {
                GVIronrecjobworker.DataSource = dsrec;
                GVIronrecjobworker.DataBind();
            }
            else
            {
                GVIronrecjobworker.DataSource = null;
                GVIronrecjobworker.DataBind();
            }
            dsdam = objBs.proissuforironjobworker("tbljpIroning ", "tbltransjpIroninghistory", "IroningId", "Damage", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsdam.Tables[0].Rows.Count > 0)
            {
                GVIrondamjobworker.DataSource = dsdam;
                GVIrondamjobworker.DataBind();
            }
            else
            {
                GVIrondamjp.DataSource = null;
                GVIrondamjobworker.DataBind();
            }

            dsalter = objBs.proissuforironjobworker("tbljpIroning ", "tbltransjpIroninghistory", "IroningId", "Alter", FromDate, ToDate, drpbranch.SelectedValue);
            if (dsalter.Tables[0].Rows.Count > 0)
            {
                GVIronalterjobworker.DataSource = dsalter;
                GVIronalterjobworker.DataBind();
            }
            else
            {
                GVIronalterjobworker.DataSource = null;
                GVIronalterjobworker.DataBind();
            }

            #endregion

            #region Despatch and Payment
            DataSet dsDespatch = objBs.serchdeapatch(FromDate, ToDate,drpbranch.SelectedValue);
            if (dsDespatch.Tables[0].Rows.Count > 0)
            {
                GVDespatch.DataSource = dsDespatch;
                GVDespatch.DataBind();
            }
            else
            {
                GVDespatch.DataSource = null;
                GVDespatch.DataBind();
            }

            DataSet dsDespatchreturn = objBs.serchdeapatchRet(FromDate, ToDate,drpbranch.SelectedValue);
            if (dsDespatchreturn.Tables[0].Rows.Count > 0)
            {
                GVDespatchReturn.DataSource = dsDespatchreturn;
                GVDespatchReturn.DataBind();
            }
            else
            {
                GVDespatchReturn.DataSource = null;
                GVDespatchReturn.DataBind();
            }

            DataSet ds = objBs.SearchPaymentDetailsnewjp(FromDate, ToDate, drpbranch.SelectedValue);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVPaymentGridjp.DataSource = ds;
                    GVPaymentGridjp.DataBind();
                }
                else
                {
                    GVPaymentGridjp.DataSource = null;
                    GVPaymentGridjp.DataBind();
                }
            }
            DataSet dsjobworker = objBs.SearchPaymentDetailsnewjobworker(FromDate, ToDate, drpbranch.SelectedValue);

            if (dsjobworker != null)
            {
                if (dsjobworker.Tables[0].Rows.Count > 0)
                {
                    GVPaymentGridjobworker.DataSource = dsjobworker;
                    GVPaymentGridjobworker.DataBind();
                }
                else
                {
                    GVPaymentGridjobworker.DataSource = null;
                    GVPaymentGridjobworker.DataBind();
                }
            }

            #endregion

            #region
            DataSet dsissupayment = objBs.Processissupayment(FromDate, ToDate, drpbranch.SelectedValue);
            if (dsissupayment.Tables[0].Rows.Count > 0)
            {
                #region

                for (int i = 0; i < dsissupayment.Tables[0].Rows.Count; i++)
                {
                    if (dsissupayment.Tables[0].Rows[i]["Processid"].ToString() == "1")
                    {
                        lblKajapaidamt.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }
                    if (dsissupayment.Tables[0].Rows[i]["Processid"].ToString() == "2")
                    {
                        lblStitchingpaidamt.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }
                    if (dsissupayment.Tables[0].Rows[i]["Processid"].ToString() == "3")
                    {
                        lblEmbroidingpaidamt.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }
                    if (dsissupayment.Tables[0].Rows[i]["Processid"].ToString() == "4")
                    {
                        lblWashingpaidamt.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }
                    //if (dsissupayment.Tables[0].Rows[i]["Processid"].ToString() == "5")
                    //{
                    //    lblIroningpaidamtjp.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");

                    //    lblIroningpaidamtjobbworker.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    //}
                    if (dsissupayment.Tables[0].Rows[i]["Processid"].ToString() == "7")
                    {
                        lblPrintingpaidamt.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }
                    if (dsissupayment.Tables[0].Rows[i]["Processid"].ToString() == "8")
                    {
                        lblBartagpaidamt.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }
                    if (dsissupayment.Tables[0].Rows[i]["Processid"].ToString() == "9")
                    {
                        lblTrimmingpaidamt.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }
                    if (dsissupayment.Tables[0].Rows[i]["Processid"].ToString() == "10")
                    {
                        lblConsaipaidamt.Text = Convert.ToDouble(dsissupayment.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }
                }
                #endregion
            }

            DataSet dsonlyiron = objBs.Processissupaymentonlyiron(FromDate, ToDate, drpbranch.SelectedValue);
            if (dsonlyiron.Tables[0].Rows.Count > 0)
            {
                #region
                for (int i = 0; i < dsonlyiron.Tables[0].Rows.Count; i++)
                {
                    if (dsonlyiron.Tables[0].Rows[i]["contactTypeID"].ToString() == "9")
                    {
                        lblIroningpaidamtjp.Text = Convert.ToDouble(dsonlyiron.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }
                    if (dsonlyiron.Tables[0].Rows[i]["contactTypeID"].ToString() == "10")
                    {
                        lblIroningpaidamtjobbworker.Text = Convert.ToDouble(dsonlyiron.Tables[0].Rows[i]["Amount"]).ToString("f2");
                    }

                }
                #endregion
            }

            lbltotalpaidamount.Text = (Convert.ToDouble(lblStitchingpaidamt.Text) + Convert.ToDouble(lblEmbroidingpaidamt.Text) + Convert.ToDouble(lblKajapaidamt.Text) + Convert.ToDouble(lblWashingpaidamt.Text) + Convert.ToDouble(lblPrintingpaidamt.Text) + Convert.ToDouble(lblBartagpaidamt.Text) + Convert.ToDouble(lblTrimmingpaidamt.Text) + Convert.ToDouble(lblConsaipaidamt.Text)).ToString("f2");
            lbltotalironpaidamount.Text =(Convert.ToDouble(lblIroningpaidamtjp.Text) + Convert.ToDouble(lblIroningpaidamtjobbworker.Text)).ToString("f2");

            lblGrandTotalAmount.Text=(Convert.ToDouble(lbltotalpaidamount.Text ) + Convert.ToDouble(lbltotalironpaidamount.Text )).ToString("f2");

            #endregion
        }

        //public static void Capture(string CapturedFilePath)
        //{
        //    Bitmap bitmap = new Bitmap
        // (Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

        //    Graphics graphics = Graphics.FromImage(bitmap as System.Drawing.Image);
        //    graphics.CopyFromScreen(25, 25, 25, 25, bitmap.Size);

        //    bitmap.Save(CapturedFilePath, ImageFormat.Bmp);
        //}



        protected void GVPreCutting_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                preReqmeter = preReqmeter + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reqmeter"));
                preReqShirt = preReqShirt + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ReqShirt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Total:";
                e.Row.Cells[6].Text = preReqmeter.ToString("f2");
                e.Row.Cells[7].Text = preReqShirt.ToString(); ;

              //  lblpreReqmeter.Text = preReqmeter.ToString("f2");
                lblpreReqShirt.Text = preReqShirt.ToString(); ;
            }
            #endregion
        }
        protected void GVMasterCutting_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                masterReqmeter = masterReqmeter + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reqmeter"));
                masterReqShirt = masterReqShirt + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ReqShirt"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Total:";
                e.Row.Cells[6].Text = masterReqmeter.ToString("f2"); ;
                e.Row.Cells[7].Text = masterReqShirt.ToString(); ;

              //  lblmasterReqmeter.Text = masterReqmeter.ToString("f2");
                lblmasterReqShirt.Text = masterReqShirt.ToString(); ;
            }
            #endregion
        }
        protected void GVFab_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FabReqmeter = FabReqmeter + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reqmeter"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = FabReqmeter.ToString("f2"); ;

                lblpreReqmeter.Text = FabReqmeter.ToString("f2"); ;
            }
            #endregion
        }
        protected void GVEndbitDay_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                EndbiGivenmeter = EndbiGivenmeter + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Givenmeter"));
                EndbiReqmeter = EndbiReqmeter + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Reqmeter"));
                EndbiEndbit = EndbiEndbit + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Endbit"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = EndbiGivenmeter.ToString("f2");
                e.Row.Cells[4].Text = EndbiReqmeter.ToString("f2");
                e.Row.Cells[5].Text = EndbiEndbit.ToString("f2");

                lblmasterReqmeter.Text = EndbiReqmeter.ToString("f2"); ;

            }
            #endregion
        }


        protected void GVJpStichingissu_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Stichingissu = Stichingissu + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Stichingissu.ToString();
                lblStichingissu.Text = Stichingissu.ToString();
            }
            #endregion
        }
        protected void GVJpStichingrec_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Stichingrec = Stichingrec + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Stichingrec.ToString();
                lblStichingrec.Text = Stichingrec.ToString();

            }
            #endregion
        }
        protected void GVJpStichingdam_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Stichingdam = Stichingdam + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Stichingdam.ToString();
                lblStichingdam.Text = Stichingdam.ToString();
            }
            #endregion
        }

        protected void GVEmbissu_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Embissu = Embissu + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Embissu.ToString();
                lblEmbissu.Text = Embissu.ToString();
            }
            #endregion
        }
        protected void GVEmbrec_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Embrec = Embrec + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Embrec.ToString();
                lblEmbrec.Text = Embrec.ToString();
            }
            #endregion
        }
        protected void GVEmbdam_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Embdam = Embdam + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Embdam.ToString();
                lblEmbdam.Text = Embdam.ToString();
            }
            #endregion
        }

        protected void GVKajaissu_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Kajaissu = Kajaissu + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Kajaissu.ToString();
                lblKajaissu.Text = Kajaissu.ToString();
            }
            #endregion
        }
        protected void GVKajarec_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Kajarec = Kajarec + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Kajarec.ToString();
                lblKajarec.Text = Kajarec.ToString();
            }
            #endregion
        }
        protected void GVKajadam_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Kajadam = Kajadam + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Kajadam.ToString();
                lblKajadam.Text = Kajadam.ToString();
            }
            #endregion
        }

        protected void GVPrintissu_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Printissu = Printissu + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Printissu.ToString();
                lblPrintissu.Text = Printissu.ToString();
            }
            #endregion
        }
        protected void GVPrintrec_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Printrec = Printrec + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Printrec.ToString();
                lblPrintrec.Text = Printrec.ToString();
            }
            #endregion
        }
        protected void GVPrintdam_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Printdam = Printdam + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Printdam.ToString();
                lblPrintdam.Text = Printdam.ToString();
            }
            #endregion
        }

        protected void GVwashissu_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                washissu = washissu + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = washissu.ToString();
                lblwashissu.Text = washissu.ToString();
            }
            #endregion
        }
        protected void GVwashrec_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                washrec = washrec + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = washrec.ToString();
                lblwashrec.Text = washrec.ToString();
            }
            #endregion
        }
        protected void GVwashdam_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                washdam = washdam + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = washdam.ToString();
                lblwashdam.Text = washdam.ToString();
            }
            #endregion
        }

        protected void GVBartagissu_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Bartagissu = Bartagissu = +Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Bartagissu.ToString();
                lblBartagissu.Text = Bartagissu.ToString();
            }
            #endregion
        }
        protected void GVBartagrec_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Bartagrec = Bartagrec + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Bartagrec.ToString();
                lblBartagrec.Text = Bartagrec.ToString();
            }
            #endregion
        }
        protected void GVBartagdam_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Bartagdam = Bartagdam + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Bartagdam.ToString();
                lblBartagdam.Text = Bartagdam.ToString();
            }
            #endregion
        }

        protected void GVTrimissu_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Trimissu = Trimissu + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Trimissu.ToString();
                lblTrimissu.Text = Trimissu.ToString();
            }
            #endregion
        }
        protected void GVTrimrec_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Trimrec = Trimrec + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Trimrec.ToString();
                lblTrimrec.Text = Trimrec.ToString();
            }
            #endregion
        }
        protected void GVTrimdam_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Trimdam = Trimdam + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Trimdam.ToString();
                lblTrimdam.Text = Trimdam.ToString();
            }
            #endregion
        }

        protected void GVConsaiissu_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Consaiissu = Consaiissu + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Consaiissu.ToString();
                lblConsaiissu.Text = Consaiissu.ToString();
            }
            #endregion
        }
        protected void GVConsairec_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Consairec = Consairec + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Consairec.ToString();
                lblConsairec.Text = Consairec.ToString();
            }
            #endregion
        }
        protected void GVConsaidam_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Consaidam = Consaidam + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Consaidam.ToString();
                lblConsaidam.Text = Consaidam.ToString();
            }
            #endregion
        }

        protected void GVIronissujp_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Ironissujp = Ironissujp + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Ironissujp.ToString();
                lblIronissujp.Text = Ironissujp.ToString();
            }
            #endregion
        }
        protected void GVIronrecjp_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Ironrecjp = Ironrecjp + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Ironrecjp.ToString();
                lblIronrecjp.Text = Ironrecjp.ToString();
            }
            #endregion
        }
        protected void GVIrondamjp_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Irondamjp = Irondamjp + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Irondamjp.ToString();
                lblIrondamjp.Text = Irondamjp.ToString();
            }
            #endregion
        }
        protected void GVIronalterjp_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Ironalterjp = Ironalterjp + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Ironalterjp.ToString();
                lblIronalterjp.Text = Ironalterjp.ToString();
            }
            #endregion
        }


        protected void GVIronissujobworker_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Ironissujobworker = Ironissujobworker + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Ironissujobworker.ToString();
                lblIronissujobbworker.Text = Ironissujobworker.ToString();
            }
            #endregion
        }
        protected void GVIronrecjobworker_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Ironrecjobworker = Ironrecjobworker + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Ironrecjobworker.ToString();
                lblIronrecjobbworker.Text = Ironrecjobworker.ToString();
            }
            #endregion
        }
        protected void GVIrondamjobworker_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Irondamjobworker = Irondamjobworker + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Irondamjobworker.ToString();
                lblIrondamjobbworker.Text = Irondamjobworker.ToString();
            }
            #endregion
        }
        protected void GVIronalterjobworker_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Ironalterjobworker = Ironalterjobworker + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[6].Text = Ironalterjobworker.ToString();
                lblIronalterjobbworker.Text = Ironalterjobworker.ToString();
            }
            #endregion
        }

        protected void GVDespatchstock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DespatchTotalQty = DespatchTotalQty + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalQty"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total:";
                e.Row.Cells[5].Text = DespatchTotalQty.ToString(); ;
                lblDespatchTotalQty.Text = DespatchTotalQty.ToString();

            }
            #endregion
        }

        protected void GVDespatchReturn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DespatchReturnTotalQty = DespatchReturnTotalQty + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalQty"));

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total:";
                e.Row.Cells[5].Text = DespatchReturnTotalQty.ToString(); ;
                lblDespatchReturnTotalQty.Text = DespatchReturnTotalQty.ToString();

            }
            #endregion
        }

        protected void GVPaymentGridjp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PaymentTotalQuantityjp = PaymentTotalQuantityjp + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                PaymentTotalAmountjp = PaymentTotalAmountjp + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = PaymentTotalQuantityjp.ToString();
                e.Row.Cells[4].Text = PaymentTotalAmountjp.ToString();

                //////lblPaymentTotalQuantity.Text = PaymentTotalQuantityjp.ToString();
                //////lblPaymentTotalAmount.Text = PaymentTotalAmountjp.ToString();


            }
            #endregion
        }


        protected void GVPaymentGridjobworker_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PaymentTotalQuantityjobworker = PaymentTotalQuantityjobworker + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                PaymentTotalAmountjobworker = PaymentTotalAmountjobworker + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total:";
                e.Row.Cells[3].Text = PaymentTotalQuantityjobworker.ToString();
                e.Row.Cells[4].Text = PaymentTotalAmountjobworker.ToString();

                //lblPaymentTotalQuantity.Text = PaymentTotalQuantityjobworker.ToString();
                //lblPaymentTotalAmount.Text = PaymentTotalAmountjobworker.ToString();


            }
            #endregion
        }
    }
}