<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_EmployeeJobCard.aspx.cs" Inherits="Billing.Accountsbootstrap.Print_EmployeeJobCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Employee Monthly Job Card</title>

    <script type="text/javascript">


        function myFunction() {
            var ButtonControl = document.getElementById("btnprint");
            var fist = document.getElementById("btnexit");

            ButtonControl.style.visibility = "hidden";
            btnexit.style.visibility = "hidden";
            window.print();
        }
    </script>


</head>
<body onload='windows.print()'>
    <form id="form1" runat="server">
    <div id="divprint" runat="server">
    <table width="100%">

        <tr>
    <td align="center" class="style1" colspan="2">
    <h2>Employee Monthly Job Card</h2>
    </td> 
    
    </tr>

    
    <tr>
    
    <td align="right">

     Employee Name : <asp:Label ID="lblCPhoneno" runat="server"></asp:Label> &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp&nbsp &nbsp &nbsp &nbsp &nbsp
  
    </td>
    </tr>


    </table>

    <table width="100%">
    <tr>
   
    <td align="left" style="border-right:solid 1px;border-left:solid 1px;border-bottom:solid 1px;border-top:solid 1px;width:4px;" > 
   S.No.
    </td>
    
    <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;border-top:solid 1px;">
    Date
    </td>
     <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;border-top:solid 1px;">
    Lot No
    </td>

      <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;border-top:solid 1px;">
    Process
    </td>

      <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;border-top:solid 1px;">
    Given Qty
    </td>


      <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;border-top:solid 1px;">
    Rec. Qty
    </td>

      <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;border-top:solid 1px;">
    Sign
    </td>

    
    </tr>


    <tr style="height:800px;">
   
    <td align="left" style="border-right:solid 1px;border-left:solid 1px;border-bottom:solid 1px;width:4px;">
  
    </td>
    
    <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;">
  
    </td>
    <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;">
  
    </td>
    <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;">
  
    </td>
    <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;">
  
    </td>


      <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;">
  
    </td>

      <td align="left" style="border-right:solid 1px;border-bottom:solid 1px;">
  
    </td>

    
    </tr>




    </table>

 

    <table width="100%">
    <tr>
    <td align="center">
<%--     <asp:Button ID="btnprint" runat="server" Text="Print"  OnClientClick="myFunction()"/>

     <asp:Button ID="btnexit" runat="server" Text="Exit"  onclick="btnexit_Click"/>--%>
    </td>
    </tr>    
    </table>
    </div>
    </form>
</body>
</html>
