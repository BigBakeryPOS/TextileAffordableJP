<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceEmail.aspx.cs" Inherits="Billing.InvoiceEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form  id="form1" runat="server">
    <div >
    <table width="100%">
    <tr>
    <td align ="center">
    <asp:Image ID="imglogo" runat="server" ImageUrl="~/images/logo11.png" />
    </td>
    </tr>
    <tr>
    <td>
    <table width="100%">
    <tr>
   
    </tr>
    <tr>
    <td>
    <asp:Label ID="lbladdgead" runat="server">Address</asp:Label>
    </td>
    <td style="text-align:right" >
    <label id="lblbill" runat="server"  >Bill No:-</label>
    </td>
    <td style="text-align:left">
    <label id="lblbillnum" runat="server" >001</label>
    </td>
    </tr>
    <tr>
    <td>
    <label id="lblcompname" runat="server">Ak Ahamed&co</label>
    </td>
    <td style="text-align:right">
    <label id="lbldate" runat="server" >Bill Date:-</label>
    </td>
    <td style="text-align:left" >
    <label id="lblbilldate" runat="server" >19/02/2015</label>
    </td>
    </tr>
    <tr>
    <td>
    <label id="lblstreet" runat="server">Navabatkhana street</label>
    </td>
    </tr>
     <tr>
    <td>
    <label id="lblarea" runat="server">Mahal</label>
    </td>
    </tr>
     <tr>
    <td>
    <label id="lblcity" runat="server">Madurai</label>
    </td>
    </tr>
     <tr>
    <td>
    <label id="lblpin" runat="server">625001</label>
    </td>
    </tr>
    <tr>
    <td>
    <table >
    <tr>
    <td style ="font-weight:bold; width:150px"   >
    <label id="cat" runat="server" >Category</label>
    </td>
    <td style ="font-weight:bold;width:150px ">
    <label id="desc" runat="server" >Description</label>
    </td>
    <td style ="font-weight:bold;width:150px">
    <label id="qty" runat="server" >Qty</label>
    </td>
    <td style ="font-weight:bold;width:150px">
    <label id="rate" runat="server" >Rate</label>
    </td>
    <td style ="font-weight:bold;width:150px">
    <label id="amt" runat="server" >Amount</label>
    </td>
    </tr>
     <tr>
    <td style =" width:150px"   >
    <label id="lblcat1" runat="server" >Tshirts</label>
    </td>
    <td style ="width:150px ">
    <label id="lbldesc1" runat="server" >Full hand</label>
    </td>
    <td style ="width:150px">
    <label id="lblqty1" runat="server" >10</label>
    </td>
    <td style ="width:150px">
    <label id="lblrate1" runat="server" >300</label>
    </td>
    <td style ="width:150px">
    <label id="lblamt1" runat="server" >3000</label>
    </td>
    </tr>
   
    
   
    </table>
    
     <tr>
    <td>
    <table >
    <tr>
     <td align="right" style ="width:650px" >
    <label id="lblsub" runat="server">Subtotal:-7000</label>
    </td>
    </tr>
     <tr>
     <td align="right" style ="width:650px" >
    <label id="lbldisc" runat="server">Discount:-7000</label>
    </td>
    </tr>
    </table>
    </td>
    
    </tr>
    </td>
    </tr>
    </table>
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
