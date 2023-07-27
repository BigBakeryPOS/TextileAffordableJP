<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Billing.Accountsbootstrap.Default" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script language="javascript">
        $(document).ready(function () {
            var gridHeader = $('#<%=GridView1.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
            $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
            $('#<%=GridView1.ClientID%> tr th').each(function (i) {
                // Here Set Width of each th from gridview to new table(clone table) th 
                $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
            });
            $("#GHead").append(gridHeader);
            $('#GHead').css('position', 'absolute');
            $('#GHead').css('top', $('#<%=GridView1.ClientID%>').offset().top);

        });
    </script>
</head>
<body>
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="form1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="HeaderFreez">
        <Columns>
            <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" />
            <asp:BoundField DataField="Address" HeaderText="Address" />
            <asp:BoundField DataField="City" HeaderText="City" />
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>
