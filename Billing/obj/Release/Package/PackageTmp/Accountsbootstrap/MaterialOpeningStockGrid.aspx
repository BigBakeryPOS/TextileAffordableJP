<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialOpeningStockGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.MaterialOpeningStockGrid" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Material OpeningStock</title>
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
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                font-weight: bold">
                Material OpeningStock</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" align="center">
                            <div class="col-lg-1">
                            </div>
                            <div id="Div1" runat="server" visible="false" class="col-lg-16">
                                <asp:Label runat="server" ID="Label1"></asp:Label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                                    Text="*" Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                <asp:DropDownList CssClass="form-control" ID="ddlfilter" Width="150px" Style="margin-top: -20px"
                                    runat="server">
                                    <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Contact Name" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="MobileNo" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-16">
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="*" Style="color: White" />
                                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvMaterialOpeningStockEntry')"
                                    ID="txtsearch" runat="server" Style="margin-top: -20px" placeholder="Enter Text to Search"
                                    Width="250px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -.@"
                                    TargetControlID="txtsearch" />
                            </div>
                            <div id="Div2" runat="server" visible="false" class="col-lg-17">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsearch" runat="server" ValidationGroup="val1" class="btn btn-success"
                                    Text="Search" Width="130px" />
                            </div>
                            <div id="Div3" runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" Width="130px" />
                            </div>
                            <div class="col-lg-4">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                            <div class="col-lg-17">
                                <asp:Button ID="btnadd" runat="server" class="btn btn-primary" Text="Add New" OnClick="Add_Click"
                                    Width="130px" />
                            </div>
                            <div id="Div4" runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="Button3" runat="server" class="btn btn-success" Text="Bulk Addition"
                                    Width="130px" />
                            </div>
                            <div class="col-lg-17">
                                <asp:Button ID="btnexcel" runat="server" class="btn btn-info" Text="Export-To-Excel"
                                    Width="130px" Height="32px" Visible="false" />
                            </div>
                        </div>
                    </div>
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvMaterialOpeningStockEntry" runat="server" CssClass="myGridStyle1"
                                        Width="100%" AutoGenerateColumns="false" OnRowCommand="gvBuyerOrderCutting_RowCommand">
                                        <HeaderStyle BackColor="White" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="CompanyName " DataField="CompanyName" />
                                            <asp:BoundField HeaderText="ProcessOn" DataField="category" />
                                            <asp:BoundField HeaderText="Item" DataField="Description" />
                                            <asp:BoundField HeaderText="Color" DataField="Color" />
                                            <asp:BoundField HeaderText="Qty" DataField="Qty" />
                                            <asp:BoundField HeaderText="Rate" DataField="ItemRate" />
                                            <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                                            <asp:BoundField HeaderText="Date" DataField="DefaultDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("OPMaterialStockId") %>'
                                                        CommandName="edits">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("OPMaterialStockId") %>'
                                                        CommandName="deletes" OnClientClick="alertMessage()">
                                                        <asp:Image ID="dlt" runat="server" ImageAlign="Middle" ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
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
        <asp:LinkButton Text="" ID="LinkButton1" runat="server"></asp:LinkButton>
        <ajaxToolkit:ModalPopupExtender ID="mpecost" runat="server" PopupControlID="Panelmpecost"
            TargetControlID="LinkButton1">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hdOPMaterialStockId" runat="server" />
        <asp:Panel ID="Panelmpecost" runat="server" ScrollBars="Auto" Height="600px" Width="100%"
            BackColor="Silver" Style="display: none">
            <div class="header" align="right">
            </div>
            <div class="panel-body">
                <div class="col-lg-12">
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-5">
                        <div class="form-group">
                            <label>
                                Company :
                            </label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:Label ID="lblcompany" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>
                                ProcessOn :
                            </label>
                            <asp:DropDownList ID="ddlProcessOn" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>
                                Item :
                            </label>
                            <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>
                                Color :
                            </label>
                            <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>
                                Avl.Stock :
                            </label>
                            <asp:TextBox ID="txtAvlStock" runat="server" Enabled="false" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>
                                Qty :
                            </label>
                            <asp:TextBox ID="txtQty" AutoComplete="Off" runat="server" CssClass="form-control">
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                TargetControlID="txtQty" ValidChars="." FilterType="Numbers,Custom" />
                        </div>
                        <div class="form-group">
                            <label>
                                Rate :
                            </label>
                            <asp:TextBox ID="txtRate" AutoComplete="Off" runat="server" CssClass="form-control">
                            </asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                TargetControlID="txtRate" ValidChars="." FilterType="Numbers,Custom" />
                        </div>
                        <div class="form-group">
                            <label>
                                Remarks :
                            </label>
                            <asp:TextBox ID="txtRemarks" AutoComplete="Off" runat="server" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_OnClick"
                                            Width="110px" Height="37px" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" OnClick="btnClose_OnClick"
                                            Width="110px" Height="37px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
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
                    Customer List</div>
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
