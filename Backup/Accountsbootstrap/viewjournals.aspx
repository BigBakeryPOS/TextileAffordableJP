<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewjournals.aspx.cs" Inherits="Billing.Accountsbootstrap.viewjournals" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <title>Journal Grid </title>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link rel="Stylesheet" type="text/css" href="../Styles/style1.css" />
    <%--<script type="text/javascript" src="../jquery-1.6.2.min.js"></script>
<script type="text/javascript" src="../jquery-ui-1.8.15.custom.min.js"></script>
<link rel="stylesheet" href="../jqueryCalendar.css"/>--%>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="~/font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link href="../images/fav.ico" type="image/x-icon" rel="Shortcut Icon" />
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
        function alertMessage() {
            alert('Are You Sure, You want to delete This Customer!');
        }
    </script>
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="Form1" runat="server">
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <%--               
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>--%>
    <%-- <div class="row" style="margin-top:100px" >
                <div class="col-lg-12">
                    <h1 class="page-header">Ledger Master</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>--%>
    <div class="row">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="col-lg-12" style="margin-top: -5px">
            <div class="col-lg-2">
                <h2 class="page-header" style="text-align: left; color: #fe0002;">
                    Journal</h2>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                        Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                        Text="." Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                    <asp:DropDownList CssClass="form-control" ID="ddlfilter" Style="width: 170px; margin-left: -50px;"
                        runat="server">
                        <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                        <asp:ListItem Text="TransNo" Value="1"></asp:ListItem>
                        <asp:ListItem Text="JV No" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Debtor" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Creditor" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Date" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-2">
                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                    ErrorMessage="Please enter your searching Data!" Text="." Style="color: White" />
                <asp:TextBox CssClass="form-control" Enabled="true" ID="txtsearch" runat="server"
                    placeholder="Search Text" Style="width: 170px; margin-left: -39px;"></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars="/"
                    TargetControlID="txtsearch" />
            </div>
            <div class="col-lg-2">
                <asp:Button ID="btnsearch" runat="server" ValidationGroup="val1" class="btn btn-success"
                    Text="Search" OnClick="Search_Click" Style="width: 120px; margin-top: 19px; margin-left: 155px;" />
            </div>
            <div class="col-lg-2">
                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"
                    Style="width: 120px; margin-top: 19px; margin-left: -255px;" />
            </div>
            <div class="col-lg-2">
                <asp:Button ID="btnnew" runat="server" CssClass="btn btn-danger" Text="Add New" OnClick="btnnew_Click"
                    Style="width: 120px; margin-top: 18px; margin-left: 30px;" />
            </div>
        </div>
        <!-- /.col-lg-12 -->
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Table1" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;
                                            color: Black; text-transform: uppercase" width="100%">
                                            <tr>
                                                <td align="center" style="font-size: small; width: 6%">
                                                    TransNo
                                                </td>
                                                <td align="center" style="font-size: small; width: 5%">
                                                    JV_No
                                                </td>
                                                <td align="center" style="font-size: small; width: 12%">
                                                    TransDate
                                                </td>
                                                <td align="center" style="font-size: small; width: 20%">
                                                    Debtor
                                                </td>
                                                <td align="center" style="font-size: small; width: 22%">
                                                    Creditor
                                                </td>
                                                <td align="center" style="font-size: small; width: 12%">
                                                    Narration
                                                </td>
                                                <td align="center" style="font-size: small; width: 22%">
                                                    Amount
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="overflow: auto; height: 300px">
                                            <asp:GridView ID="gvledgrid" runat="server" EmptyDataText="No records Found" Width="100%"
                                                AllowPaging="false" PageSize="10" AutoGenerateColumns="false" CssClass="myGridStyle1"
                                                AllowSorting="true" OnRowCommand="gvledgrid_RowCommand" OnPageIndexChanging="Page_Change"
                                                ShowHeader="false">
                                                <HeaderStyle BackColor="#3366FF" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                                <PagerSettings FirstPageText="1" Mode="Numeric" />
                                                <Columns>
                                                    <%--<asp:BoundField HeaderText="Category ID" DataField="CategoryID" />--%>
                                                    <asp:BoundField HeaderText="TransNo" DataField="TransNo" />
                                                    <asp:BoundField HeaderText="JV No" DataField="JV_No" />
                                                    <asp:BoundField HeaderText="Date" DataField="TransDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField HeaderText="Debtor" DataField="Debtor" />
                                                    <asp:BoundField HeaderText="Creditor" DataField="Creditor" />
                                                    <asp:BoundField HeaderText="Narration" DataField="Narration" />
                                                    <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:0.00}" />
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("TransNo") %>' CommandName="Edit"
                                                                runat="server">
                                                                <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btndel" CommandArgument='<%#Eval("TransNo") %>' CommandName="Del"
                                                                runat="server">
                                                                <asp:Image ID="Image1" ImageUrl="~/images/DeleteIcon_btn.png" runat="server" /></asp:LinkButton>
                                                            <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
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
                        <!-- /#page-wrapper -->
                    </div>
                </div>
            </div>
        </div>
        <%--  </ContentTemplate>
</asp:UpdatePanel>--%>
        <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
        <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript">            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
        <%--</div>--%>
        <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
            runat="server">
            <div class="popup_Container">
                <div class="popup_Titlebar" id="PopupHeader">
                    <div class="TitlebarLeft">
                        Journal:</div>
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
