<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyerOrderMasterCuttingGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.BuyerOrderMasterCuttingGrid" %>

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
    <title>Buyer Order Master Cutting</title>
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
    <asp:Label runat="server" ID="lblProcessforMasterId" Text="5" ForeColor="White" CssClass="label"
        Visible="false"> </asp:Label>
    <form runat="server" id="form1">
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <div class="col-lg-3">
                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 18px;
                    font-weight: bold">
                    Buyer Order Master Cutting</h1>
            </div>
            <div runat="server" visible="false" class="col-lg-2">
                <asp:DropDownList ID="drpyear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlfilter_OnSelectedIndexChanged"
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
                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvBuyerOrderMasterCutting')"
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
                                    <asp:GridView ID="gvBuyerOrderMasterCutting" runat="server" CssClass="myGridStyle1"
                                        EmptyDataText="No Records Found" Width="100%" AutoGenerateColumns="false" OnRowCommand="gvBuyerOrderCutting_RowCommand">
                                        <HeaderStyle BackColor="White" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                            <asp:BoundField HeaderText="MasterCuttingDate" DataField="MasterCuttingDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="CuttingDate" DataField="CuttingDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="OrderType " DataField="OrderType" />
                                            <asp:BoundField HeaderText="CompanyCode" DataField="CompanyCode" />
                                            <asp:BoundField HeaderText="DeliveryDate" DataField="DeliveryDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="CQty" DataField="CQty" />
                                            <asp:BoundField HeaderText="RecQty" DataField="RecQty" />
                                            <asp:BoundField HeaderText="DmgQty" DataField="DmgQty" />
                                            <asp:BoundField HeaderText="BalQty" DataField="BalQty" />
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("BuyerOrderMasterCuttingId") %>'
                                                        CommandName="edit">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("BuyerOrderMasterCuttingId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receive" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnReceive" runat="server" CommandArgument='<%#Eval("BuyerOrderCuttingId") %>'
                                                        CommandName="Receive">
                                                        <asp:Image ID="imgReceive" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisableReceive" ImageUrl="~/images/edit.png" runat="server"
                                                        Visible="false" Enabled="false" ToolTip="Not Allow To Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Export" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit1" runat="server" CommandArgument='<%#Eval("BuyerOrderMasterCuttingId") %>'
                                                        CommandName="ExportExcel">
                                                       Export</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("BuyerOrderMasterCuttingId") %>'
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
                    <!-- /.col-lg-6 (nested) -->
                </div>
                <!-- /.row (nested) -->
            </div>
            <!-- /.panel-body -->
        </div>
        <!-- /.panel -->
    </div>
    <!-- /.col-lg-12 -->
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Buyer Order Master Cutting:-</div>
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
