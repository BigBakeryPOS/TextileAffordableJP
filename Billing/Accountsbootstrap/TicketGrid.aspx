<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.TicketGrid" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ticket</title>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link href="../Styles/chosen.css" rel="Stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" type="text/css" />
    <script src="../js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Search_Gridview(strKey, strGV) {


            var strData = strKey.value.toLowerCase().split(" ");

            var tblData = document.getElementById(strGV);

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)

                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        } 

    </script>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM" runat="server">
    </asp:ScriptManager>
    <div>
        <br />
        <div class="row col-lg-12">
            <div class="col-lg-2" style="color: Red; font-size: medium">
                Ticket Grid</div>
            <div class="col-lg-2">
                <asp:TextBox Style="text-transform: uppercase" ID="txtSearch" onkeyup="Search_Gridview(this, 'gv_Product')"
                    CssClass="form-control" Width="200px" runat="server" placeholder="Search Text"></asp:TextBox></div>
            <div class="col-lg-2">
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary"
                    Width="150px" OnClick="btnReset_Click" /></div>
        </div>
        <div class="row col-lg-12">
            <br />
            <div class="col-lg-8">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #59D3B4">
                        Ticket Grid</div>
                    <div class="panel-body">
                        <asp:GridView ID="gv_Product" runat="server" AutoGenerateColumns="false" CssClass="myGridStyle1"
                            OnRowCommand="gv_Product_RowCommand" OnRowDataBound="gv_Product_rowdatabound"
                            Width="100%" OnRowEditing="gv_Product_RowEditing">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:d}" />
                                <asp:BoundField HeaderText="TicketNo" DataField="TicketId" Visible="false" />
                                <asp:BoundField HeaderText="Vendor" DataField="VendorId" Visible="false" />
                                <asp:BoundField HeaderText="Raise Person" DataField="Name" />
                                <asp:BoundField HeaderText="TicketNo" DataField="TicketNo" />
                                <asp:BoundField HeaderText="Comments" DataField="Comments" />
                                <asp:BoundField HeaderText="Contact Person" DataField="ServiceName" />
                                <asp:BoundField HeaderText="Status" DataField="Status1" />
                                <asp:BoundField HeaderText="Admin Comment" DataField="AdminComment" />
                                <asp:BoundField HeaderText="Priority" DataField="PriorityStatus_" />
                                <asp:BoundField HeaderText="Completed Date" DataField="CompletedDate" DataFormatString="{0:d}" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("TicketId") %>' CommandName="Edit"
                                            runat="server">
                                            <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
                                            <asp:ImageButton ID="imgdisable" Visible="false" Enabled="false" ImageUrl="~/images/lock.png"
                                                ToolTip="Not Allow to Edit" runat="server" />
                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("TicketId") %>' />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-VerticalAlign="Middle" HeaderText="View chat History">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit1" CommandArgument='<%#Eval("TicketId") %>' CommandName="History"
                                            runat="server">
                                            <asp:Image ID="imdedit1" ImageAlign="Middle" ImageUrl="~/images/history_icon.png"
                                                runat="server" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div id="chathistory" runat="server" visible="false" class="col-lg-4">
                <label>
                    Chat History</label>
                <br />
                <br />
                Ticket No :
                <asp:Label ID="lblticketno" runat="server"></asp:Label><br />
                Ticket Date :
                <asp:Label ID="lblticketdate" runat="server"></asp:Label><br />
                <br />
                <label>
                    Chat Details</label>
                <asp:GridView ID="gvChatDetails" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="myGridStyle">
                    <Columns>
                        <%--<asp:BoundField HeaderText="TicketNo" DataField="ticketno" />
                                    <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:d}" />--%>
                        <asp:BoundField HeaderText="Emp Name" DataField="Name" />
                        <asp:BoundField HeaderText="Comments" DataField="Comments" />
                        <asp:BoundField HeaderText="Status" DataField="Status1" />
                        <asp:BoundField HeaderText="Priority" DataField="PriorityStatus_" />
                        <asp:BoundField HeaderText="Entry Date" DataField="ticketdate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss tt}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <%-- Body Section--%>
    </div>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Ticket</div>
                <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                </div>
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure want to delete?
                </p>
            </div>
            <div class="popup_Buttons">
                <input id="ButtonDeleleOkay" type="button" value="Yes" />
                <input id="ButtonDeleteCancel" type="button" value="No" />
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
