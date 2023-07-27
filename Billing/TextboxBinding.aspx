<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextboxBinding.aspx.cs" Inherits="Billing.TextboxBinding" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">  
    </asp:ScriptManager>  
    <div>
    <table>
    <tr>
    <td>Category
    <asp:TextBox ID="txtcategory" runat="server" AutoPostBack="true" 
            ontextchanged="txtcategory_TextChanged"></asp:TextBox>
             <asp:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"  
                    CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtcategory"  
                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">  
                </asp:AutoCompleteExtender>  
    </td>
     <td>Description
    <asp:TextBox ID="txtdescp" runat="server" AutoPostBack="true" 
             ontextchanged="txtdescp_TextChanged"></asp:TextBox>
               <asp:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"  
                    CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtdescp"  
                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">  
                </asp:AutoCompleteExtender> 
    </td>
    <td>Quantity
    <asp:TextBox ID="txtQty" runat="server" ></asp:TextBox>
    </td>
     <td>Rate
    <asp:TextBox ID="txtrate" runat="server" ></asp:TextBox>
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
