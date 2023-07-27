<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemType.aspx.cs" Inherits="Billing.Accountsbootstrap.ItemType" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Item Type</title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
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
    <style>
        .pagination-ys
        {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td
        {
            display: inline;
        }
        
        .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span
        {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            color: #dd4814;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            margin-left: -1px;
        }
        
        .pagination-ys table > tbody > tr > td > span
        {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            margin-left: -1px;
            z-index: 2;
            color: #aea79f;
            background-color: #f5f5f5;
            border-color: #dddddd;
            cursor: default;
        }
        
        .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span
        {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span
        {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus
        {
            color: #97310e;
            background-color: #eeeeee;
            border-color: #dddddd;
        }
        .style14
        {
            width: 14%;
        }
        .style15
        {
            width: 15%;
        }
    </style>
    <link href="../Styles/chosen.css" rel="Stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //         Client Side Search (Autocomplete)
            //         Get the search Key from the TextBox
            //         Iterate through the 1st Column.
            //         td:nth-child(1) - Filters only the 1st Column
            //         If there is a match show the row [$(this).parent() gives the Row]
            //         Else hide the row [$(this).parent() gives the Row]

            $('#txtsearch').keyup(function (event) {
                var searchKey = $(this).val().toLowerCase();
                $("#gridview tr td:nth-child(1)").each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(searchKey) >= 0) {
                        $(this).parent().show();
                    }
                    else {
                        $(this).parent().hide();
                    }
                });
            });
        });

    </script>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-2">
                <h2 class="page-header" style="text-align: left; color: #fe0002; font-size: 20px">
                    Item Type</h2>
            </div>
        </div>
        <div class="col-lg-2">
            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                Style="color: white" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                Text="*" Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                ErrorMessage="Please enter your searching Data!" Text="*" Style="color: White" />
            <asp:DropDownList CssClass="form-control" ID="ddlfilter" Visible="false" Style="width: 150px;
                margin-left: 1px;" runat="server">
                <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                <asp:ListItem Text="Category" Value="1"></asp:ListItem>
                <asp:ListItem Text="IsActive" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-lg-2">
            <asp:TextBox CssClass="form-control" ID="txtsearch" runat="server" placeholder="Search Text"
                MaxLength="50" Style="width: 170px; margin-top: -34px; margin-left: 57px;"></asp:TextBox>
        </div>
        <div class="col-lg-2">
            <asp:Button ID="btnresret" runat="server" class="btn btn-primary" Text="Reset" Style="width: 120px;
                margin-top: -34px; margin-left: 90px;" OnClick="btnresret_OnClick" />
        </div>
        <div id="Div1" runat="server" visible="false" class="col-lg-2">
            <asp:Button ID="Button2" runat="server" class="btn btn-success" Text="Bulk Addition"
                Style="width: 120px; margin-top: -34px; margin-left: 430px;" Height="32px" />
        </div>
        <div class="col-lg-2">
            <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Visible="false"
                ValidationGroup="val1" Text="Search" Style="width: 120px; margin-top: -59px;
                margin-left: 10px;" />
        </div>
        <div class="col-lg-2">
            <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Visible="false" Text="Add New"
                AccessKey="N" Style="width: 120px; margin-top: -54px; margin-left: 221px;"  />
        </div>
        <div class="col-lg-2">
            <asp:Button ID="btnexcel" runat="server" class="btn btn-info" Text="Export-To-Excel"
                Style="width: 120px; margin-top: -34px; margin-left: 535px;" Height="32px" />
        </div>
        <%--<label>Alt+N</label>--%>
    </div>
    <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-8">
            <div class="panel panel-default">
                <div class="panel-body" style="margin-top: 0px;">
                    <div class="table-responsive">
                        <table width="100%">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="Div2" runat="server" style="height: 520px; overflow: scroll;" align="center">
                                        <asp:GridView ID="gridview" runat="server" Width="100%" EmptyDataText="No Records Found"
                                            DataKeyNames="ItemId" AutoGenerateColumns="false"
                                            CssClass="myGridStyle1" 
                                             OnSelectedIndexChanged="gridview_OnSelectedIndexChanged">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField DataField="ItemCode" SortExpression="ItemCode" HeaderText="ItemCode"
                                                    ItemStyle-Width="25%" />
                                                <asp:BoundField DataField="ItemDescription" SortExpression="ItemDescription" HeaderText="ItemDescription"
                                                    ItemStyle-Width="25%" />
                                                <asp:BoundField DataField="ItemHead" SortExpression="ItemHead" HeaderText="ItemHead"
                                                    ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="Itemgroupname" SortExpression="Itemgroupname" HeaderText="ItemGroup"
                                                    ItemStyle-Width="15%" />
                                                <asp:BoundField DataField="IsActive" SortExpression="IsActive" HeaderText="Is Active"
                                                    ItemStyle-Width="5%" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="Modify">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("ItemId") %>' CommandName="Select"
                                                            runat="server">
                                                            <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
                                                        </asp:LinkButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ItemId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderText="Delete"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndel" CommandArgument='<%#Eval("ItemId") %>' CommandName="Del"
                                                            runat="server">
                                                            <asp:Image ID="Image1" ImageUrl="~/images/DeleteIcon_btn.png" runat="server" />
                                                            <asp:ImageButton ID="imgdisable" ImageUrl="~/images/delete.png" runat="server" Visible="false"
                                                                Enabled="false" ToolTip="Not Allow To Delete" />
                                                        </asp:LinkButton>
                                                        <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                                            CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btndel"
                                                            PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                        <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                                            TargetControlID="btndel" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <%--    <div class="col-lg-1"></div>--%>
        <div class="col-lg-4" style="margin-left: -14px">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #336699; color: White;">
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblName" Text="Add ItemType" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="list-group">
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val2"
                            ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="txtItemId" runat="server" Visible="false"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>
                                Select Item Head</label>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val2"
                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlItemHead"
                                ValueToCompare="Select ItemHead" Operator="NotEqual" Type="String" ErrorMessage="Please Select Item Head."></asp:CompareValidator>
                            <asp:DropDownList ID="ddlItemHead" runat="server" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlItemHead_OnSelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>
                                Select Item Group</label>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val2"
                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlItemGroup"
                                ValueToCompare="Select ItemGroup" Operator="NotEqual" Type="String" ErrorMessage="Please Select Item Group."></asp:CompareValidator>
                            <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:Label ID="Label2" runat="server" Style="color: Red"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>
                                Enter Item Code</label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtItemCode"
                                ValidationGroup="val2" Text="*" ErrorMessage="Please enter your Item Code." Style="color: Red" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" &!@#$%^*(){}_+=.<>/?-"
                                TargetControlID="txtItemCode" />
                            <asp:TextBox CssClass="form-control" ID="txtItemCode" runat="server" placeholder="To Add New Item Code"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>
                                Enter Item Description</label>
                            <asp:RequiredFieldValidator runat="server" ID="txtcat" ControlToValidate="txtItemDescription"
                                ValidationGroup="val2" Text="*" ErrorMessage="Please enter your Item Description"
                                Style="color: Red" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" &!@#$%^*(){}_+=.<>/?-"
                                TargetControlID="txtItemDescription" />
                            <asp:TextBox CssClass="form-control" ID="txtItemDescription" runat="server" placeholder="To Add New Item Description"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>
                                IsActive</label>
                            <asp:DropDownList ID="ddlIsActive" runat="server" class="form-control">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Button ID="Button1" runat="server" class="btn btn-info" Text="Save" ValidationGroup="val2"
                            Style="width: 120px;" AccessKey="s" OnClick="Button1_Click" />
                        <label>
                        </label>
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-warning" Text="Cancel" Style="width: 120px;"
                            OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Item Group</div>
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
