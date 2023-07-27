<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="Billing.Accountsbootstrap.Ticket" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ticket</title>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
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
            <div class="col-lg-2">
                <asp:TextBox Style="text-transform: uppercase" ID="txtSearch" onkeyup="Search_Gridview(this, 'gv_Product')"
                    CssClass="form-control" Width="200px" runat="server" placeholder="Search Text"></asp:TextBox></div>
            <div class="col-lg-2">
                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                <div id="Div1" runat="server" visible="true" class="form-group" style="">
                    <asp:TextBox CssClass="form-control" Style="" ID="txtfromdate" runat="server" Text="Select From Date"></asp:TextBox>
                </div>
                <ajaxToolkit:CalendarExtender ID="txtfrmdat1" TargetControlID="txtfromdate" Format="dd/MM/yyyy"
                    runat="server" CssClass="cal_Theme1">
                </ajaxToolkit:CalendarExtender>
            </div>
            <div id="Div2" runat="server" visible="true" class="col-lg-2">
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" Style="" ID="txttodate" runat="server" Text="Select To Date"></asp:TextBox>
                </div>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txttodate"
                    Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                </ajaxToolkit:CalendarExtender>
            </div>
            <div class="col-lg-2">
                <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary"
                    Width="150px" OnClick="btnGenerate_Click" /></div>
            <div class="col-lg-2">
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary"
                    Width="150px" OnClick="btnReset_Click" /></div>
            <div class="col-lg-2">
                <asp:Button ID="btnExcell" runat="server" Text="Generate Excell" CssClass="btn btn-info"
                    Width="150px" OnClick="btnExcel_Click" /></div>
        </div>
        <div class="row col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #59D3B4">
                    Ticket Grid</div>
                <div class="panel-body">
                    <asp:GridView ID="gv_Product" runat="server" AutoGenerateColumns="false" CssClass="myGridStyle"
                        OnRowCommand="gv_Product_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="TicketId" DataField="TicketId" Visible="false" />
                            <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Raise Name" DataField="Name" />
                            <asp:BoundField HeaderText="TicketNo" DataField="TicketNo" />
                            <asp:BoundField HeaderText="Comments" DataField="Comments" />
                            <asp:BoundField HeaderText="Status" DataField="Status1" />
                            <asp:BoundField HeaderText="Service Name" DataField="Sname" />
                            <asp:BoundField HeaderText="Admin Comments" DataField="AdminComment" />
                            <asp:BoundField HeaderText="Priority" DataField="PriorityStatus_" />
                            <asp:BoundField HeaderText="Completed Date" DataField="CompletedDate" DataFormatString="{0:d}" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="View">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("TicketId") %>' CommandName="View"
                                        runat="server">
                                        <asp:Image ID="imdedit" ImageUrl="~/images/ViewHistory.png" Width="30px" runat="server" /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
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
