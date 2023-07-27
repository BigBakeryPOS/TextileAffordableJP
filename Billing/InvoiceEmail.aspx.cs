using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;


namespace Billing
{
    public partial class InvoiceEmail : System.Web.UI.Page
    {
        StringBuilder sb = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            sb.Append("  <table width='100%'>    <tr>    <td align ='center'>    <asp:Image ID='imglogo' runat='server' ImageUrl='~/images/logo11.png' />    </td>    </tr>    <tr>    <td>");
            sb.Append(" <table width='100%'>    <tr>    </tr>   <tr>    <td>    <asp:Label ID='lbladdgead' runat='server'>Address</asp:Label>    </td>    <td style='text-align:right' >    <label id='lblbill' runat='server'  >Bill No:-</label>    </td>");
            sb.Append("<td style='text-align:left'>    <label id='lblbillnum' runat='server' >001</label>    </td>    </tr>    <tr>    <td>    <label id='lblcompname' runat='server'>Ak Ahamed&co</label>    </td>    <td style='text-align:right'>    <label id='lbldate' runat='server' >Bill Date:-</label>    </td>    <td style='text-align:left' >    <label id='lblbilldate' runat='server'>19/02/2015</label>    </td>");
            sb.Append(" </tr>    <tr>    <td>    <label id='lblstreet' runat='server'>Navabatkhana street</label>    </td>    </tr>     <tr>    <td>    <label id='lblarea' runat='server'>Mahal</label>    </td>    </tr>     <tr>    <td>    <label id='lblcity' runat='server'>Madurai</label>    </td>    </tr>     <tr>    <td>    <label id='lblpin' runat='server'>625001</label>    </td>    </tr>    <tr>    <td>    <table >");
            sb.Append(" <tr>    <td style ='font-weight:bold; width:150px'   >    <label id='cat' runat='server' >Category</label>    </td>    <td style ='font-weight:bold;width:150px '>    <label id='desc' runat='server' >Description</label>    </td>    <td style ='font-weight:bold;width:150px'>    <label id='qty' runat='server' >Qty</label>    </td>    <td style ='font-weight:bold;width:150px'>    <label id='rate' runat='server' >Rate</label>    </td>    <td style ='font-weight:bold;width:150px'>    <label id='amt' runat='server' >Amount</label>    </td>    </tr>"); 
            //sb.AppendFormat("<tr><td>Sub-Category:</td><td>{0}</td></tr>", ddlPage.SelectedValue);
            sb.Append(" <tr>    <td style =' width:150px'   >    <label id='lblcat1' runat='server' >Tshirts</label>    </td>    <td style ='width:150px '>    <label id='lbldesc1' runat='server' >Full hand</label>    </td>    <td style ='width:150px'>    <label id='lblqty1' runat='server' >10</label>    </td>    <td style ='width:150px'>    <label id='lblrate1' runat='server' >300</label>    </td>    <td style ='width:150px'>    <label id='lblamt1' runat='server' >3000</label>    </td>    </tr>");
            sb.Append("  </table>     <tr>    <td>    <table >    <tr>     <td align='right' style ='width:650px' >    <label id='lblsub' runat='server'>Subtotal:-7000</label>    </td>    </tr>     <tr>     <td align='right' style ='width:650px'>    <label id='lbldisc'runat='server'>Discount:-7000</label>    </td>    </tr>    </table>    </td>        </tr>    </td>    </tr>    </table>    </td>    </tr>    </table>");
            //sb.AppendFormat('<tr><td>Description:</td><td>{0}</td></tr>', txtComments.Text.Trim());
            var fromAddress = "pratheepkumar@onlinehanger.com";
            var toAddress = "harishbabu@bigdbiz.com";
            const string fromPassword = "online@123";
            string subject = "test subject";
            string body = "From: Pratheep ";
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "mail.onlinehanger.com";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 200000;


            }
            smtp.Send(fromAddress, toAddress, subject, "test");
        }
    }
       
        }
    