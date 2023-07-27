<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dept_Master.aspx.cs" Inherits="Billing.Accountsbootstrap.Dept_Master" %>

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
    <title>Affordable Exports || Department</title>
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
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.js"></script>
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


        $(document).ready(function () {

            $('#txtsearch').keyup(function (event) {
                var searchKey = $(this).val().toLowerCase();
                $("#gv tr td:nth-child(1)").each(function () {
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
    </div>
    <div class="col-lg-12" style="margin-top: 6px">
        <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
            font-weight: bold">
            Department Master</h1>
    </div>
    <form id="f1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div>
            <div class="row">
                <div class="col-lg-1">
                </div>
                <div id="Div1" runat="server" visible="false" class="col-lg-16">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                        Style="color: white" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                        Text="*" Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                        ErrorMessage="Please enter your searching Data!" Text="*" Style="color: White" />
                    <asp:DropDownList Visible="false" CssClass="form-control" ID="ddlfilter" runat="server">
                        <asp:ListItem Text="Grade" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-16">
                    <asp:TextBox CssClass="form-control" ID="txtsearch" runat="server" placeholder="Enter Search Text"
                        MaxLength="50" Style="width: 350px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                        FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -"
                        TargetControlID="txtsearch" />
                    <%--<asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label><br />--%>
                </div>
                <div id="Div2" runat="server" visible="false" class="col-lg-17">
                    <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" Width="130px"
                        OnClick="search" />
                </div>
                <div id="Div3" runat="server" visible="false" class="col-lg-17">
                    <asp:Button ID="btnresret" runat="server" class="btn btn-primary" Text="Reset" Width="130px"
                        OnClick="reset" />
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-7">
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gv" runat="server" DataKeyNames="DeptId" OnSelectedIndexChanged="gv_selectedindex"
                                        OnRowCommand="edit" EmptyDataText="Oops! No Activity Performed." AllowPaging="true"
                                        PageSize="1000" OnPageIndexChanging="Page_Change" HeaderStyle-BackColor="#e0e0e0" Width="85%"
                                        AutoGenerateColumns="false" CssClass="myGridStyle1" AllowSorting="true">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="DesiginationId" DataField="DeptId" Visible="false" />
                                            <asp:BoundField HeaderText="Department Name" DataField="DeptName" 
                                                HeaderStyle-ForeColor="white" />
                                            <%--<asp:BoundField HeaderText="IsActive" DataField="IsActive"  SortExpression="IsActive" HeaderStyle-ForeColor="Black"   />--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("DeptId") %>' CommandName="EditRow"
                                                        runat="server">
                                                        <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
                                                    </asp:LinkButton>
                                                    <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("DeptId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndel" CommandArgument='<%#Eval("DeptId") %>' CommandName="Del"
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
                                                    <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
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
                </div>
                <!-- /.col-lg-8 -->
                <div class="col-lg-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-briefcase"></i>Add Department
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="list-group">
                                <asp:TextBox ID="txtid" Visible="false" runat="server"></asp:TextBox>
                                <%--  <label>Select Contact-Type</label>--%>
                                <br />
                                <label>
                                    Department</label>
                                <asp:TextBox placeholder="Enter Department" ID="txtdep" runat="server" CssClass="form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendername" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Custom,Numbers" ValidChars=" ./\!@#$%^&*,"
                                    TargetControlID="txtdep" />
                                <br />
                                <label>
                                    IsActive</label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlIsActive"
                                    ValidationGroup="val2" Text="*" ErrorMessage="Please Select IsActive!" Style="color: Red" />
                                <asp:DropDownList ID="ddlIsActive" runat="server" class="form-control">
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                                <%--  <asp:TextBox placeholder="Enter Address"  TextMode="multiline" Height="100px" ID="txtaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderadd" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom,Numbers" 
ValidChars=" /,.\-#"  TargetControlID="txtaddress" />
 <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator111"
                                        ControlToValidate="txtaddress" ErrorMessage="Please Enter Address!"
                                        Style="color: Red" />--%>
                            </div>
                            <!-- /.list-group -->
                            <div>
                                <asp:Button ID="btnSubmit" Style="width: 75px; margin-left: 50px;" runat="server"
                                    class="btn btn-default btn-block" Text="Save" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnclaear" Style="width: 75px; margin-top: -34px; margin-left: 150px;"
                                    runat="server" class="btn btn-default btn-block" Text="Cancel" OnClick="btncancel_Click" />
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <div role="alert">
                        <asp:Label ID="lblSuccess" runat="server" class="alert alert-success" Text="Well Done! You successfully Inserted."
                            Visible="false"></asp:Label>
                        <asp:Label ID="lblFailure" runat="server" Text="Oops! Contact Already Exists." class="alert alert-danger"
                            Visible="false"></asp:Label>
                        <asp:Label ID="lblWarning" runat="server" Text="Whoo!Did You Miss Something?" class="alert alert-warning"
                            Visible="false"></asp:Label></div>
                </div>
                <!-- /.col-lg-4 -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /#page-wrapper -->
    </div>
    <!-- /#wrapper -->
    </form>
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
</body>
</html>
