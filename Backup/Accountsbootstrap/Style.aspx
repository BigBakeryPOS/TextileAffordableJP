<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Style.aspx.cs" Inherits="Billing.Accountsbootstrap.Style" %>

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
    <title>Style Master</title>
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
    <%--<script type="text/javascript">
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
    </script>--%>
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
                    Style Master</h2>
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
                MaxLength="50" Style="width: 170px; margin-top: -34px; margin-left: 57px;" Visible="false"></asp:TextBox>
        </div>
        <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -"  TargetControlID="txtsearch" />--%>
        <%--<asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label><br />--%>
        <div class="col-lg-2">
            <asp:Button ID="btnresret" runat="server" class="btn btn-primary" Text="Reset" Style="width: 120px;
                margin-top: -34px; margin-left: 90px;" OnClick="Btn_Reset" Visible="false"/>
        </div>
        <div class="col-lg-2">
            <asp:Button ID="Button2" runat="server" class="btn btn-success" Text="Bulk Addition"
                Style="width: 120px; margin-top: -34px; margin-left: 430px;" Height="32px" Visible="false" />
        </div>
        <div class="col-lg-2">
            <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Visible="false"
                ValidationGroup="val1" Text="Search" Style="width: 120px; margin-top: -59px;
                margin-left: 10px;" OnClick="Btn_Search" />
        </div>
        <div class="col-lg-2">
            <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Visible="false" Text="Add New"
                AccessKey="N" Style="width: 120px; margin-top: -54px; margin-left: 221px;" />
        </div>
        <div class="col-lg-2">
            <asp:Button ID="btnexcel" runat="server" class="btn btn-info" Text="Export-To-Excel"
                Style="width: 120px; margin-top: -34px; margin-left: 535px;" Height="32px" OnClick="btnExcel_Click" Visible="false"/>
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
                                    <%--<table id="Table1" runat="server" style="border:1px solid Grey;height:30px;background-color:#59d3b4;text-transform:uppercase"  width="100%" >
                <tr>
                                       
                    <td align="center" style="font-size:large" width="65%">
                      Category    <asp:Label ID="lblOB" Visible="false" runat="server"></asp:Label>
                    </td>
                    <td align="center" style="font-size:large" width="5px">
                         IsActive
                    </td>
                    <td align="center" style="font-size:large" width="12px"">
                         Edit
                    </td>
                    <td align="center" style="font-size:large" width="9px">
                         Delete
                    </td>
                </tr>
            </table>--%>
                                    <div id="Div1" runat="server" style="height: 520px; overflow: scroll;" align="center">
                                        <asp:GridView ID="gridview" runat="server" Width="100%" EmptyDataText="No Records Found"
                                            DataKeyNames="StyleID" AutoGenerateColumns="false" CssClass="myGridStyle1" AllowSorting="true"
                                            OnRowDataBound="gridview_RowDatabound" OnSelectedIndexChanged="gridview_SelectedIndexChanged">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField Visible="false" DataField="StyleID" />
                                                <asp:BoundField DataField="FabricType" HeaderText="FabricType" />
                                                <asp:BoundField DataField="Style" HeaderText="Style" />
                                                <asp:BoundField DataField="IsActive" HeaderText="Active" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("StyleID") %>' CommandName="Select"
                                                            runat="server">
                                                            <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
                                                        </asp:LinkButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("StyleID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete" ItemStyle-Width="10%"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndel" CommandArgument='<%#Eval("StyleID") %>' CommandName="Del"
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
                    <div class="col-lg-6" style="margin-top: 88px;">
                        <div>
                            <h2>
                                <label>
                                    <font color="red"></font>
                                </label>
                            </h2>
                            <table>
                                <tr>
                                    <td colspan="4" align="left">
                                        <asp:GridView ID="GVStockAlert" Visible="false" runat="server" AutoGenerateColumns="false"
                                            CssClass="myGridStyle">
                                            <Columns>
                                                <asp:BoundField DataField="Category" HeaderText="Category" />
                                                <asp:BoundField DataField="Definition" HeaderText="Definition" />
                                                <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" DataFormatString="{0:n2}" />
                                                <asp:BoundField DataField="Available_QTY" HeaderText="Available_QTY" />
                                                <asp:BoundField DataField="MinQty" HeaderText="MinQty" />
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
        <div class="col-lg-4" style="margin-left: -14px">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #59d3b4; color: #333333; border-color: #06090c">
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblName" Text="Add Style" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="list-group">
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val2"
                            ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="txtstyleId" runat="server" Visible="false"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>
                                FabricType</label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlIsActive"
                                ValidationGroup="val2" Text="*" ErrorMessage="Please Select IsActive!" Style="color: Red" />
                            <asp:DropDownList ID="ddlfabrictype" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>
                                Style</label>
                            <asp:RequiredFieldValidator runat="server" ID="txtcat" ControlToValidate="txtStyle"
                                ValidationGroup="val2" Text="*" ErrorMessage="Please enter your FabricType!"
                                Style="color: Red" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -"
                                TargetControlID="txtStyle" />
                            <asp:TextBox CssClass="form-control" ID="txtStyle" Style="text-transform: capitalize;"
                                runat="server" placeholder="To Add New Style"></asp:TextBox>
                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>
                                IsActive</label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlIsActive"
                                ValidationGroup="val2" Text="*" ErrorMessage="Please Select IsActive!" Style="color: Red" />
                            <asp:DropDownList ID="ddlIsActive" runat="server" class="form-control">
                                <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
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
                    Category List</div>
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
