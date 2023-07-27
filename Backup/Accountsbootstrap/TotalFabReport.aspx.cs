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
    public partial class TotalFabReport : System.Web.UI.Page
    {
        string sTableName = "";
        BSClass objbs = new BSClass();
        double amount1 = 0;
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

                DataSet dbfab = objbs.Getfab();
                if (dbfab.Tables[0].Rows.Count > 0)
                {
                    ddldcno.DataSource = dbfab;
                    ddldcno.DataTextField = "FabNo";
                    ddldcno.DataValueField = "Fabid";
                    ddldcno.DataBind();
                    ddldcno.Items.Insert(0, "All");
                }

                DataSet dsup = objbs.getnewsupplierforfab();
                if (dsup.Tables[0].Rows.Count > 0)
                {
                    drpsupplier.DataSource = dsup.Tables[0];
                    drpsupplier.DataTextField = "LEdgerName";
                    drpsupplier.DataValueField = "LedgerID";
                    drpsupplier.DataBind();
                    drpsupplier.Items.Insert(0, "All");
                }

            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }


        public void DownloadAsPDF()
        {
            try
            {
                string strHtml = string.Empty;
                string pdfFileName = Request.PhysicalApplicationPath + "\\files\\" + "GenerateHTMLTOPDF.pdf";

                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                //  Div2.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                strHtml = sr.ReadToEnd();
                sr.Close();

                CreatePDFFromHTMLFile(strHtml, pdfFileName);

                Response.ContentType = "application/x-download";
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", "GenerateHTMLTOPDF.pdf"));
                Response.WriteFile(pdfFileName);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void CreatePDFFromHTMLFile(string HtmlStream, string FileName)
        {
            //try
            //{
            //    object TargetFile = FileName;
            //    string ModifiedFileName = string.Empty;
            //    string FinalFileName = string.Empty;


            //    GeneratePDF.HtmlToPdfBuilder builder = new GeneratePDF.HtmlToPdfBuilder(iTextSharp.text.PageSize.A4);
            //    GeneratePDF.HtmlPdfPage first = builder.AddPage();
            //    first.AppendHtml(HtmlStream);
            //    byte[] file = builder.RenderPdf();
            //    File.WriteAllBytes(TargetFile.ToString(), file);

            //    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(TargetFile.ToString());
            //    ModifiedFileName = TargetFile.ToString();
            //    ModifiedFileName = ModifiedFileName.Insert(ModifiedFileName.Length - 4, "1");

            //    iTextSharp.text.pdf.PdfEncryptor.Encrypt(reader, new FileStream(ModifiedFileName, FileMode.Append), iTextSharp.text.pdf.PdfWriter.STRENGTH128BITS, "", "", iTextSharp.text.pdf.PdfWriter.AllowPrinting);
            //    reader.Close();
            //    if (File.Exists(TargetFile.ToString()))
            //        File.Delete(TargetFile.ToString());
            //    FinalFileName = ModifiedFileName.Remove(ModifiedFileName.Length - 5, 1);
            //    File.Copy(ModifiedFileName, FinalFileName);
            //    if (File.Exists(ModifiedFileName))
            //        File.Delete(ModifiedFileName);

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void ddldcno_OnSelectedIndexChanged(object sender, EventArgs e)
        {

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

            DateTime fromdate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dss = objbs.FabricDetailReort(fromdate, todate, drpsupplier.SelectedValue, ddldcno.SelectedValue);
            if (dss.Tables[0].Rows.Count > 0)
            {
                //gvCustsales.Caption = "Fabric and Cutting Process Report From " + txtfromdate.Text + " To " + txttodate.Text + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm ss");
                gvCustsales.DataSource = dss;
                gvCustsales.DataBind();
            }
            else
            {
                gvCustsales.DataSource = null;
                gvCustsales.DataBind();
            }

        }

        protected void lnkDownload_OnClick(object sender, EventArgs e)
        {
            string FilePath = (sender as LinkButton).CommandName;
            if (FilePath == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Check Data');", true);
                return;

            }
            else
            {
                //   Response.Clear();
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
                Response.WriteFile(FilePath);
                Response.End();
            }
        }


        public void gvCustsales_RowDataBound(object sender, GridViewRowEventArgs e)
        {


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
                    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);

                    DataSet ds = objbs.getoverallfabreport(groupID);


                    DataSet dsfab = new DataSet();
                    DataTable dtfab = new DataTable();
                    DataColumn dcfab = new DataColumn();

                    dcfab = new DataColumn("CDate");
                    dtfab.Columns.Add(dcfab);
                    dcfab = new DataColumn("MDate");
                    dtfab.Columns.Add(dcfab);
                    dcfab = new DataColumn("Master");
                    dtfab.Columns.Add(dcfab);
                    dcfab = new DataColumn("LotNo");
                    dtfab.Columns.Add(dcfab);
                    dcfab = new DataColumn("ItemName");
                    dtfab.Columns.Add(dcfab);

                    dcfab = new DataColumn("CutMeter");
                    dtfab.Columns.Add(dcfab);
                    dcfab = new DataColumn("EndBit");
                    dtfab.Columns.Add(dcfab);
                    dcfab = new DataColumn("MasterMeter");
                    dtfab.Columns.Add(dcfab);

                    dcfab = new DataColumn("BalanceMeter");
                    dtfab.Columns.Add(dcfab);
                    dcfab = new DataColumn("ShirtType");
                    dtfab.Columns.Add(dcfab);
                    dcfab = new DataColumn("Color");
                    dtfab.Columns.Add(dcfab);
                    dcfab = new DataColumn("FabricType");
                    dtfab.Columns.Add(dcfab);

                    

                    dsfab.Tables.Add(dtfab);



                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        double ttlCutMeter = 0;
                        double ttlEndBit = 0;
                        double ttlMasterMeter = 0;



                        DataSet dscut = objbs.fabdetailsincut(Convert.ToInt32(ds.Tables[0].Rows[i]["transid"].ToString()));
                        DataSet dsmaster = objbs.fabdetailsinmaster(Convert.ToInt32(ds.Tables[0].Rows[i]["transid"].ToString()));
                        int dsmastercount = dsmaster.Tables[0].Rows.Count;

                        for (int k = 0; k < dscut.Tables[0].Rows.Count; k++)
                        {

                            string MasterDeliveryDate = "";
                            double EndBit = 0;
                            double MasterMeter = 0;

                            if (dsmaster.Tables[0].Rows.Count > 0)
                            {
                                if (dsmastercount - k > 0)
                                {
                                    MasterDeliveryDate = Convert.ToDateTime(dsmaster.Tables[0].Rows[k]["DeliveryDate"]).ToString("dd/MM/yyyy");
                                    EndBit = Convert.ToDouble(dsmaster.Tables[0].Rows[k]["Endbit"].ToString());
                                    MasterMeter = Convert.ToDouble(dsmaster.Tables[0].Rows[k]["Reqmeter"].ToString());
                                }
                                else
                                {
                                    ttlMasterMeter = ttlMasterMeter + Convert.ToDouble(dscut.Tables[0].Rows[k]["Reqmeter"].ToString());
                                }

                            }
                            else
                            {
                                ttlMasterMeter = ttlMasterMeter + Convert.ToDouble(dscut.Tables[0].Rows[k]["Reqmeter"].ToString());
                            }
                            DataRow drfab = dsfab.Tables[0].NewRow();
                            drfab["CDate"] = Convert.ToDateTime(dscut.Tables[0].Rows[k]["DeliveryDate"]).ToString("dd/MM/yyyy");
                            drfab["MDate"] = MasterDeliveryDate;
                            drfab["Master"] = dscut.Tables[0].Rows[k]["LedgerName"].ToString();
                            drfab["LotNo"] = dscut.Tables[0].Rows[k]["CompanyFullLotNo"].ToString();
                            drfab["Itemname"] = dscut.Tables[0].Rows[k]["Itemname"].ToString();

                            drfab["CutMeter"] = Convert.ToDouble(dscut.Tables[0].Rows[k]["Reqmeter"]).ToString("f2");
                            drfab["EndBit"] = EndBit;
                            drfab["MasterMeter"] = MasterMeter;

                            ttlCutMeter = ttlCutMeter + Convert.ToDouble(dscut.Tables[0].Rows[k]["Reqmeter"].ToString());
                            ttlEndBit = ttlEndBit + EndBit;

                            ttlMasterMeter = ttlMasterMeter + MasterMeter;



                            drfab["BalanceMeter"] = "-";
                            drfab["ShirtType"] = dscut.Tables[0].Rows[k]["ShirtType"].ToString();
                            drfab["Color"] = ds.Tables[0].Rows[i]["Color"].ToString();

                            drfab["FabricType"] = ds.Tables[0].Rows[i]["FabricType"].ToString();

                            dsfab.Tables[0].Rows.Add(drfab);
                        }
                        {
                            DataRow drfab = dsfab.Tables[0].NewRow();



                            drfab["CDate"] = "";
                            drfab["MDate"] = "";
                            drfab["Master"] = "";
                            drfab["ItemName"] = "";
                            drfab["LotNo"] = "Total :";

                            drfab["CutMeter"] = Convert.ToDouble(ds.Tables[0].Rows[i]["OrgMeter"]).ToString("f2");
                            drfab["EndBit"] = ttlEndBit;
                            drfab["MasterMeter"] = ttlMasterMeter;

                            drfab["BalanceMeter"] = Convert.ToDouble(ds.Tables[0].Rows[i]["AvaliableMeter"]).ToString("f2");
                            if (ds.Tables[0].Rows[i]["Status"].ToString() == "Y")
                            {
                                drfab["ShirtType"] = "completed";
                            }
                            else
                            {
                                drfab["ShirtType"] = "Uncomplete";
                            }
                            drfab["Color"] = ds.Tables[0].Rows[i]["Color"].ToString();

                            drfab["FabricType"] = ds.Tables[0].Rows[i]["FabricType"].ToString();

                            dsfab.Tables[0].Rows.Add(drfab);
                        }


                    }

                    gv.DataSource = dsfab;
                    gv.DataBind();
                }

                #endregion
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }

        }

        public void gvLiaLedger_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow pr = ((DataRowView)e.Row.DataItem).Row;

                if (pr["LotNo"].ToString() == "Total :")
                {
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                }
            }

        }

        protected void btnexp_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= FabricCuttingDetails.xls");
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