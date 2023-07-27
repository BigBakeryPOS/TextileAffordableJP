<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error_Page.aspx.cs" Inherits="Billing.Accountsbootstrap.Error_Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Pranav Cards Error Page</title>
    <!-- TO DISABLE BACK BUTTON ON BROWSER-->
      <script type = "text/javascript" >
          history.pushState(null, null, 'Error_Page.aspx');
          window.addEventListener('popstate', function (event) {
              history.pushState(null, null, 'Error_Page.aspx');
          });
    </script>
    <!-- BUTTON STYLE-->
   <style type="text/css">
.btn-primary {
  color: #fff;
  background-color: #5cb85c;
 
  font-weight:bold; 
  border-radius:5px;
}
</style>
    <style type="text/css">
        .style1
        {
            width: 803px;
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
     <table width="100%">
     <tr>
     <td align="center"><img src="../images/Pranav_logo.jpg" /></td>
     </tr>
     <tr><td align="justify" valign="baseline" style="font-family:Calibri; font-size:25px; padding-left:100px; padding-right:100px"><h3>
     <p>Your session has Expired! This might be for the below reasons:
     <br /><br />
     1. Long time idle of current login id<br />
     2. Multiple login without proper logout<br />
    
     </p>
     Kindly contact your system administrator for details!     
     </h3></td></tr>
     <tr>
     <td align="center"><asp:Button ID="btnredirect" runat="server" Text="REDIRECT" 
             Height="54px" style="margin-right: 0px; font-size:large" Width="144px" 
             CssClass="btn-primary style1" onclick="btnredirect_Click"/></td>
     </tr></table>
    </div>
    </form>
</body>
</html>

