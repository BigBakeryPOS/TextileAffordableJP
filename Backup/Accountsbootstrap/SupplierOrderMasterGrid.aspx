<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierOrderMasterGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.SupplierOrderMasterGrid" %>

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
    <title>Sample Order Master</title>
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
    <div class="col-lg-12" style="margin-top: 1px">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="col-lg-2">
                            <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                                font-weight: bold">
                                Sample Order Master</h1>
                        </div>
                        <div class="col-lg-2">
                            <asp:DropDownList ID="drpyear" runat="server" CssClass="form-control" OnSelectedIndexChanged="Year_selected"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvBuyerOrder')"
                                ID="txtsearch" runat="server" placeholder="Enter Text to Search" Width="250px"></asp:TextBox>
                        </div>
                        <div class="col-lg-1">
                            <asp:Button ID="btnadd" runat="server" class="btn btn-primary" Text="Add New" OnClick="Add_Click"
                                Width="110px" />
                        </div>
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-3">
                            <asp:GridView ID="gridsummary" runat="server" Width="100%" AutoGenerateColumns="false"
                                OnRowDataBound="gridsummary_RowDatabound">
                                <HeaderStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField HeaderText="Currency Type " DataField="currencyname" />
                                    <asp:BoundField HeaderText="Amount" DataField="amnt" />
                                    <asp:BoundField HeaderText="Order count" DataField="cnt" />
                                     <asp:BoundField HeaderText="Pcs count" DataField="Bqty" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div style="height: 450px; overflow: auto" class="table-responsive">
                    <table class="table table-bordered table-striped">
                        <tr>
                            <td>
                                <asp:GridView ID="gvBuyerOrder" runat="server" CssClass="myGridStyle" Width="100%"
                                    AutoGenerateColumns="false" OnRowCommand="gvBuyerOrder_RowCommand" OnRowDataBound="gvcust_RowDatabound">
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
                                        <asp:BoundField HeaderText="OrderType " DataField="OrderType" Visible="false" />
                                        <asp:BoundField HeaderText="CompanyName" DataField="LedgerName" Visible="false" />
                                        <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" Visible="false" />
                                        <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                        <asp:BoundField HeaderText="BuyerPoNo" DataField="BuyerPoNo" />
                                        <asp:BoundField HeaderText="ItemCode" DataField="ItemCode" />
                                        <asp:BoundField HeaderText="Ord.Date" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField HeaderText="Ship.Date" DataField="ShipmentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField HeaderText="Dlv.Date" ItemStyle-Font-Bold="true" DataField="DeliveryDate"
                                            DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField HeaderText="B.Qty" DataField="BQty" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="C.Qty" DataField="CQty" ItemStyle-HorizontalAlign="Right" Visible="false"/>
                                        <asp:BoundField HeaderText="C.Qty" DataField="MQty" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="S.Qty" DataField="ShippedQty" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:f2}"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="Currency" DataField="currencyname" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Dlv.Date" DataField="IschangedDeliveryDate" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="RateChange" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRateChange" runat="server" CommandArgument='<%#Eval("BuyerOrderId") %>'
                                                    CommandName="RateChange">
                                                        RC</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("BuyerOrderId") %>'
                                                    CommandName="edit">
                                                    <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                    Enabled="false" ToolTip="Not Allow To Delete" />
                                                <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("BuyerOrderId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Exl" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnedit1" runat="server" CommandArgument='<%#Eval("BuyerOrderId") %>'
                                                    CommandName="ExportExcel">
                                                       Exl</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("BuyerOrderId") %>'
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
                                <asp:GridView ID="gvSketches" CssClass="Gridview" runat="server" AutoGenerateColumns="true"
                                    ShowHeader="false" GridLines="Horizontal">
                                    <HeaderStyle CssClass="headerstyle" />
                                    <Columns>
                                        <%--<asp:ImageField DataImageUrlField="Column0" ItemStyle-Height="18px" ItemStyle-Width="1px" />--%>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="GridView1" CssClass="Gridview" runat="server" AutoGenerateColumns="false"
                                    GridLines="None" ShowHeader="false">
                                    <HeaderStyle CssClass="headerstyle" />
                                    <Columns>
                                        <asp:ImageField DataImageUrlField="Sketch" ItemStyle-Height="18px" ItemStyle-Width="1px" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
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
                                                <asp:HiddenField ID="hdTransBuyerOrderId" runat="server" Value='<%#Eval("TransBuyerOrderId") %>' />
                                                <asp:HiddenField ID="hdQty" runat="server" Value='<%#Eval("Qty") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="StyleNo" HeaderText="StyleNo" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Color" HeaderText="Color" />
                                        <asp:BoundField DataField="Range" HeaderText="Range" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:f0}" />
                                        <asp:BoundField DataField="AffectedQty" HeaderText="AffectedQty" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:f0}" />
                                        <asp:BoundField DataField="CRatio" HeaderText="CRatio" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:f0}" />
                                        <asp:BoundField DataField="CQty" HeaderText="CQty" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:f0}" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:f2}" />
                                        <asp:TemplateField HeaderText="New Rate" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRate" Text='<%#Eval("Rate") %>' runat="server" Width="120px"
                                                    AutoComplete="Off"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                    TargetControlID="txtRate" ValidChars="." FilterType="Numbers,Custom" />
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
                    Sample Order Master:-</div>
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
