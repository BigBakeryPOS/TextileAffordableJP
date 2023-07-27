<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemProcessOrderEntryGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.ItemProcessOrderEntryGrid" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <style type="text/css">
        a img
        {
            border: none;
        }
        ol li
        {
            list-style: decimal outside;
        }
        div#container
        {
            width: 780px;
            margin: 0 auto;
            padding: 1em 0;
        }
        div.side-by-side
        {
            width: 100%;
            margin-bottom: 1em;
        }
        div.side-by-side > div
        {
            float: left;
            width: 50%;
        }
        div.side-by-side > div > em
        {
            margin-bottom: 10px;
            display: block;
        }
        .clearfix:after
        {
            content: "\0020";
            display: block;
            height: 0;
            clear: both;
            overflow: hidden;
            visibility: hidden;
        }
    </style>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>ItemProcess Order</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            ShowPopup();
            setTimeout(HidePopup, 2000);
        }

        function ShowPopup() {
            $find('modalpopup').show();
            //$get('Button1').click();
        }

        function HidePopup() {
            $find('modalpopup').hide();
            //$get('btnCancel').click();
        }
    </script>
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
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1">
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" style="margin-top: 6px">
                            <div class="col-lg-2">
                                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                                    font-weight: bold">
                                    ItemProcess Order
                                </h1>
                            </div>
                            <div class="col-lg-3">
                                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'GVItemProcessOrder')"
                                    ID="txtsearch" runat="server" placeholder="Enter Text to Search" Width="250px"></asp:TextBox>
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnadd" runat="server" class="btn btn-primary" Text="Add New" OnClick="Add_Click"
                                    Width="130px" />
                            </div>
                        </div>
                    </div>
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="GVItemProcessOrder" runat="server" CssClass="myGridStyle1" EmptyDataText="No records Found"
                                        Width="100%" AutoGenerateColumns="false" OnRowCommand="GVItemProcessOrder_RowCommand">
                                        <HeaderStyle BackColor="White" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Order No" DataField="FullPONo" />
                                            <asp:BoundField HeaderText="CompanyName" DataField="LedgerName" />
                                            <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" />
                                            <asp:BoundField HeaderText="ProcessOn" DataField="category" />
                                            <asp:BoundField HeaderText="DeliveryPlace" DataField="DeliveryPlace" />
                                            <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="FromDate" DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="ToDate" DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="Qty" DataField="Qty" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField HeaderText="TotalQty" DataField="TotalQty" DataFormatString="{0:f2}"  ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField HeaderText="IssQty" DataField="RecQty" DataFormatString="{0:f2}"  ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField HeaderText="Amount" DataField="TotalAmount" DataFormatString="{0:f2}"  ItemStyle-HorizontalAlign="Right"/>
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("ItemEntryId") %>'
                                                        CommandName="edit">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ItemEntryId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Meter Cancel" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnCancelMeter" runat="server" CommandArgument='<%#Eval("ItemEntryId") %>'
                                                        CommandName="CancelMeter">
                                                        Cancel</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPrint" runat="server" CommandArgument='<%#Eval("ItemEntryId") %>'
                                                        CommandName="Print">
                                                        <asp:Image ID="imgPrint" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("ItemEntryId") %>'
                                                        CommandName="delete1" OnClientClick="alertMessage()">
                                                        <asp:Image ID="dlt" runat="server" ImageUrl="~/images/DeleteIcon_btn.png" Visible="true" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable1" ImageUrl="~/images/delete.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                                        CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btndelete"
                                                        PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                    <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                                        TargetControlID="btndelete" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                                    </ajaxToolkit:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- /.col-lg-6 (nested) -->
                </div>
                <!-- /.row (nested) -->
            </div>
            <!-- /.panel-body -->
        </div>
        <!-- /.panel -->
    </div>
    <div class="col-lg-12">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:LinkButton Text="" ID="LinkButton1" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="mpecost" runat="server" PopupControlID="Panelmpecost"
                    TargetControlID="LinkButton1">
                </ajaxToolkit:ModalPopupExtender>
                <asp:HiddenField ID="hdpopupid" runat="server" />
                <asp:Panel ID="Panelmpecost" runat="server" ScrollBars="Auto" Height="700px" Width="100%"
                    BackColor="Silver" Style="display: none">
                    <div class="header" align="right">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_OnClick" />
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" OnClick="btnClose_OnClick" />
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div class="col-lg-12" style="padding-top: 2pc">
                                <asp:GridView ID="GVItem" AutoGenerateColumns="False" GridLines="Both" runat="server">
                                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                        Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                    <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:HiddenField ID="hdTransId" runat="server" Value='<%#Eval("TransId") %>' />
                                                <asp:HiddenField ID="hdQty" runat="server" Value='<%#Eval("Qty") %>' />
                                                <asp:HiddenField ID="hdReceivedQty" runat="server" Value='<%#Eval("ReceivedQty") %>' />
                                                <asp:HiddenField ID="hdCanceledQty" runat="server" Value='<%#Eval("CanceledQty") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PurchaseForType" HeaderText="Pro.Against" />
                                        <asp:BoundField DataField="IssueItem" HeaderText="IssueItem" />
                                        <asp:BoundField DataField="ReceiveItem" HeaderText="ReceiveItem" />
                                        <asp:BoundField DataField="IssColor" HeaderText="IssColor" />
                                        <asp:BoundField DataField="RecColor" HeaderText="RecColor" />
                                        <asp:BoundField DataField="Qty" HeaderText="Order Qty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" />
                                        <asp:BoundField DataField="CanceledQty" HeaderText="CanceledQty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" />
                                        <asp:TemplateField HeaderText="Cancel Qty" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCancelQty" Text='<%#Eval("CancelQty") %>' runat="server" Width="120px"
                                                    AutoComplete="Off"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                    TargetControlID="txtCancelQty" ValidChars="." FilterType="Numbers,Custom" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="footer" align="left">
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    ItemProcess Order:-</div>
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
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
