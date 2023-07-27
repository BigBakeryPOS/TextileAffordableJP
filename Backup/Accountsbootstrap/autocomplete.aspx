<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="autocomplete.aspx.cs" Inherits="Billing.Accountsbootstrap.autocomplete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>  
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>  
    <link rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />  


     <script type="text/javascript">
         $(function () {
             $("#txtEmpName").autocomplete({
                 source: function (request, response) {
                     var param = { empName: $('#txtEmpName').val() };
                     $.ajax({
                         url: "WebForm1.aspx/GetEmpNames",
                         data: JSON.stringify(param),
                         dataType: "json",
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         dataFilter: function (data) { return data; },
                         success: function (data) {
                             response($.map(data.d, function (item) {
                                 return {
                                     value: item
                                 }
                             }))
                         },
                         error: function (XMLHttpRequest, textStatus, errorThrown) {
                             var err = eval("(" + XMLHttpRequest.responseText + ")");
                             alert(err.Message)
                             // console.log("Ajax Error!");  
                         }
                     });
                 },
                 minLength: 2 //This is the Char length of inputTextBox  
             });
         });  
    </script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
     EmpName : <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>  
        <br />  
        <br />  
        <br />  
     </div>
    </form>
</body>
</html>
