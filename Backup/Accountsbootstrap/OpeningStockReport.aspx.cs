using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLayer;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class OpeningStockReport : System.Web.UI.Page
    {   OpeningStockEntry objBs = new OpeningStockEntry();
    BSClass objbs = new BSClass();
        string sTableName = "";
        public Double dQtyamt = 0.0;
        public Double crateamt = 0.0;
        public Double cratetotamt = 0.0;
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objBs.getGrid_Newwithamount(sTableName);
                gridOpening.DataSource = ds;
                gridOpening.DataBind();
            }
        }


        protected void gvstock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            double Qty = 0;
            double RAte = 0;
            double stockRAte = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Qty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Nos"));
                RAte = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "purchaserate"));
                stockRAte = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "amnt"));
                dQtyamt = dQtyamt + Qty;
                crateamt = crateamt + RAte;
                cratetotamt = cratetotamt + stockRAte;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "Total:";
                e.Row.Cells[4].Text = dQtyamt.ToString("N");
                e.Row.Cells[5].Text = crateamt.ToString("f2");
                e.Row.Cells[6].Text = cratetotamt.ToString("f2");
            }

        }


       
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            gridOpening.PagerSettings.Visible = false;
            //  GridView1.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridOpening.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            //  sb.Append("test1");
            sb.Append("printWin.document.write(\"");
            sb.Append("APM MOTORS </br>");
            sb.Append("OpeningStock Report </br>");
            sb.Append(" </br></br>");
            sb.Append("</br>");

            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gridOpening.PagerSettings.Visible = true;
        }
    }
}