<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyerOrderCuttingGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.BuyerOrderCuttingGrid" %>

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
    <title>Buyer Order Cutting</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../css/chosen.css" />
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
    <script type="text/javascript">
        function Confirm(myButton) {

            // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // disable the button
                myButton.disabled = true;
                myButton.className = "btn-inactive";
                myButton.value = "processing...";
            }
            return true;


        }
    </script>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <div class="col-lg-3">
                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 18px;
                    font-weight: bold">
                    Buyer Order Cutting</h1>
            </div>
            <div class="col-lg-2">
                <asp:DropDownList ID="drpyear" runat="server" CssClass="form-control" OnSelectedIndexChanged="Year_selected"
                    AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2">
                <asp:DropDownList CssClass="form-control" ID="ddlfilter" AutoPostBack="true" OnSelectedIndexChanged="ddlfilter_OnSelectedIndexChanged"
                    runat="server">
                    <asp:ListItem Text="All" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Pending" Value="2" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Copmpleted" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-2">
                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvBuyerOrderCutting')"
                    ID="txtsearch" runat="server" placeholder="Enter Text to Search" Width="250px"></asp:TextBox>
            </div>
            <div class="col-lg-1">
            </div>
            <div class="col-lg-1">
                <asp:Button ID="btnadd" runat="server" class="btn btn-primary" Text="Add New" OnClick="Add_Click"
                    Width="130px" />
            </div>
            <div class="col-lg-3">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvBuyerOrderCutting" runat="server" CssClass="myGridStyle1" Width="100%"
                                        EmptyDataText="No Records Found" AutoGenerateColumns="false" OnRowCommand="gvBuyerOrderCutting_RowCommand">
                                        <HeaderStyle BackColor="White" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="CuttingNo" DataField="FullCuttingNo" />
                                            <asp:BoundField HeaderText="CuttingDate" DataField="CuttingDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                            <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" />
                                            <asp:BoundField HeaderText="BuyerPoNo" DataField="BuyerPoNo" />
                                            <asp:BoundField HeaderText="ItemCode" DataField="ItemCode" />
                                            <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="ShipmentDate" DataField="ShipmentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="DeliveryDate" DataField="DeliveryDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="CQty" DataField="CQty" />
                                            <asp:BoundField HeaderText="RecQty" DataField="RecQty" />
                                            <asp:BoundField HeaderText="DmgQty" DataField="DmgQty" />
                                            <asp:BoundField HeaderText="BalQty" DataField="BalQty" />
                                            <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:f2}"
                                                Visible="false" />
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("BuyerOrderCuttingId") %>'
                                                        CommandName="edit">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("BuyerOrderCuttingId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Add Materials" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAddMaterials" runat="server" CommandArgument='<%#Eval("BuyerOrderCuttingId") %>'
                                                        CommandName="AddMaterials">
                                                        <asp:Image ID="imgAddMaterials" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisAddMaterials" ImageUrl="~/images/edit.png" runat="server"
                                                        Visible="false" Enabled="false" ToolTip="Not Allow To Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPrint" runat="server" CommandArgument='<%#Eval("BuyerOrderCuttingId") %>'
                                                        CommandName="Print">
                                                        <asp:Image ID="imgPrint" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Export" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit1" runat="server" CommandArgument='<%#Eval("BuyerOrderCuttingId") %>'
                                                        CommandName="ExportExcel">
                                                       Export</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("BuyerOrderCuttingId") %>'
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
                                        GridLines="None" ShowHeader="false">
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
    </div>
    <div class="col-lg-12">
        <asp:LinkButton Text="" ID="LinkButton1" runat="server"></asp:LinkButton>
        <ajaxToolkit:ModalPopupExtender ID="mpecost" runat="server" PopupControlID="Panelmpecost"
            TargetControlID="LinkButton1">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hdBuyerOrderCuttingId" runat="server" />
        <asp:HiddenField ID="hdCompanyId" runat="server" />
        <asp:Panel ID="Panelmpecost" runat="server" ScrollBars="Auto" Height="600px" Width="100%"
            BackColor="Silver" Style="display: none">
            <div class="header" align="right">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_OnClick" />
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" OnClick="btnClose_OnClick" />
            </div>
            <div class="panel-body">
                <div class="col-lg-12">
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-9">
                        <div id="Div5" runat="server">
                            <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvLabels')"
                                ID="TextBox1" runat="server" Style="margin-top: -20px" placeholder="Enter Text to Search"
                                Width="250px"></asp:TextBox>
                            <asp:GridView ID="GVFabricDetails" runat="server" Width="100%" EmptyDataText="No Records Found"
                                Caption="Material Details" AutoGenerateColumns="false" CssClass="myGridStyle1">
                                <HeaderStyle BackColor="White" />
                                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    NextPageText="Next" PreviousPageText="Previous" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdItemId" runat="server" Value='<%#Eval("ItemId") %>' />
                                            <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId") %>' />
                                            <asp:HiddenField ID="hdRequiredStock" runat="server" Value='<%#Eval("RequiredStock") %>' />
                                            <asp:HiddenField ID="hdIssueStock" runat="server" Value='<%#Eval("IssueStock") %>' />
                                            <asp:HiddenField ID="hdUsedQty" runat="server" Value='<%#Eval("UsedQty") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Item" HeaderText="Item" />
                                    <asp:BoundField DataField="Color" HeaderText="Color" />
                                    <asp:BoundField DataField="RequiredStock" HeaderText="RequiredStock" DataFormatString="{0:f2}" />
                                    <asp:BoundField DataField="IssueStock" HeaderText="IssuedStock" DataFormatString="{0:f2}" />
                                    <asp:BoundField DataField="UsedQty" HeaderText="UsedQty" DataFormatString="{0:f2}" />
                                    <asp:TemplateField HeaderText="Avl.Stock" HeaderStyle-Width="45px" ItemStyle-Width="45px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvlStock" Height="30px" Width="100%" runat="server" Text='<%#Eval("AvlStock") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Qty" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtIssueQty" Height="30px" Width="100%" runat="server" Text='<%#Eval("IssueQty") %>'
                                                CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                TargetControlID="txtIssueQty" ValidChars="." FilterType="Numbers,Custom" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-lg-2">
                    </div>
                </div>
            </div>
            <div class="footer" align="left">
            </div>
        </asp:Panel>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
     <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Buyer Order Cutting:-</div>
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
