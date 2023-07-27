using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using DataLayer;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class DilloMultiLotPrint : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string sStore = "";
        string sAddress = "";
        string sTin = "";
        string Phone = "";
        string Mobile = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string barCode = string.Empty;
            //button1_Click(sender, e);
            //lblUserID.Text = Session["UserID"].ToString();
            //sTableName = Session["User"].ToString();
            //sStore = Session["Store"].ToString();
            //sAddress = Session["Address"].ToString();
            //sTin = Session["TIN"].ToString();
            //Phone = Session["PH"].ToString();
            //Mobile = Session["CELL"].ToString();


            //int iD = Convert.ToInt32(Request.QueryString.Get("iSalesID"));
            //int NewiD = Convert.ToInt32(Request.QueryString.Get("NewiSalesID"));
            //string sMode = Request.QueryString.Get("Mode");
            //string Type = Request.QueryString.Get("Type");

            string lot = string.Empty;
          
            lot = Request.QueryString.Get("lotid");
            if (lot != null)
            {
                DataSet dss = new DataSet();
                dss = objBs.getprintformultistiching(lot);
                if (dss.Tables[0].Rows.Count > 0)
                {
                    barCode = dss.Tables[0].Rows[0]["fullno"].ToString();
                    lblMultiNo.Text = dss.Tables[0].Rows[0]["fullno"].ToString();
                    lbltotalQty.Text = Convert.ToInt32(dss.Tables[0].Rows[0]["TotalQty"]).ToString();
                    lblmultidate.Text = Convert.ToDateTime(dss.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                    lblemployeename.Text = dss.Tables[0].Rows[0]["Name"].ToString();

                    gridprint.DataSource = dss;
                    gridprint.DataBind();



                }

                #region Generated Code
              //  barCode = "BG00000000000000";
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                {
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                        Font oFont = new Font("IDAutomationHC39M", 12);
                        PointF point = new PointF(2f, 2f);
                        SolidBrush blackBrush = new SolidBrush(System.Drawing.Color.Black);
                        SolidBrush whiteBrush = new SolidBrush(System.Drawing.Color.White);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();

                        Convert.ToBase64String(byteImage);
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    plBarCode.Controls.Add(imgBarCode);
                }
                #endregion

            }

           
        }


        //barcode code :---
        private void button1_Click(object sender, EventArgs e)
        {
            //Document doc = new Document(new iTextSharp.text.Rectangle(24, 12), 5, 5, 1, 1);

            //try
            //{

            //    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(
            //      Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/codes.pdf", FileMode.Create));
            //    doc.Open();

            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("ID");
            //    dt.Columns.Add("Price");
            //    for (int i = 0; i < 20; i++)
            //    {
            //        DataRow row = dt.NewRow();
            //        row["ID"] = "BG00000000000000" + i.ToString();
            //        row["Price"] = "100," + i.ToString();
            //        dt.Rows.Add(row);
            //    }
            //    System.Drawing.Image img1 = null;
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (i != 0)
            //            doc.NewPage();
            //        PdfContentByte cb1 = writer.DirectContent;
            //        BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_BOLDITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            //        cb1.SetFontAndSize(bf, 2.0f);
            //        cb1.BeginText();
            //        cb1.SetTextMatrix(1.2f, 9.5f);
            //        cb1.ShowText("Bigdbiz Solutions");
            //        cb1.EndText();

            //        PdfContentByte cb2 = writer.DirectContent;
            //        BaseFont bf1 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            //        cb2.SetFontAndSize(bf1, 1.3f);
            //        cb2.BeginText();
            //        cb2.SetTextMatrix(17.5f, 1.0f);
            //        cb2.ShowText(dt.Rows[i]["Price"].ToString());
            //        cb2.EndText();

            //        iTextSharp.text.pdf.PdfContentByte cb = writer.DirectContent;
            //        iTextSharp.text.pdf.Barcode128 bc = new Barcode128();
            //        bc.TextAlignment = Element.ALIGN_LEFT;
            //        bc.Code = dt.Rows[i]["ID"].ToString();
            //        bc.StartStopText = false;
            //        bc.CodeType = iTextSharp.text.pdf.Barcode128.EAN13;
            //        bc.Extended = true;

            //        //System.Drawing.Image bimg = 
            //        //  bc.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
            //        //img1 = bimg;

            //        iTextSharp.text.Image img = bc.CreateImageWithBarcode(cb,
            //          iTextSharp.text.BaseColor.BLACK, iTextSharp.text.BaseColor.BLACK);

                   
            //        cb.SetTextMatrix(1.5f, 3.0f);
            //        img.ScaleToFit(60, 5);
            //        img.SetAbsolutePosition(1.5f, 1);
            //        cb.AddImage(img);
            //      //  lblimgbarcode.ImageUrl = img.ToString();
            //    }

            //    ////////////////////***********************************//////////////////////

            //    doc.Close();
            //    System.Diagnostics.Process.Start(Environment.GetFolderPath(
            //               Environment.SpecialFolder.Desktop) + "/codes.pdf");
            //    //MessageBox.Show("Bar codes generated on desktop fileName=codes.pdf");
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    doc.Close();
            //}
        }


    }
}