<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemoveLot.aspx.cs" Inherits="Billing.Accountsbootstrap.RemoveLot" %>

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
    <title>Lot Remove Process </title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script src="../js/Searchjs.js" type="text/javascript"></script>
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
        function SearchEmployees(txtSearch, cblEmployees) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html((count) + ' match');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
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
            Lot Remove Process</h1>
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
                    <asp:DropDownList CssClass="form-control" ID="ddlfilter" Style="margin-top: -20px"
                        Width="150px" runat="server">
                        <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Width" Value="1"></asp:ListItem>
                        <asp:ListItem Text="IsActive" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-lg-16">
                    <asp:TextBox CssClass="form-control" ID="txtsearch" runat="server" onkeyup="Search_Gridview(this, 'gv')"
                        placeholder=" Enter Search Text" MaxLength="50" Style="width: 150px"></asp:TextBox>
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
                                    <asp:GridView ID="gv" runat="server" DataKeyNames="Removeid" OnSelectedIndexChanged="gv_selectedindex"
                                        OnRowCommand="edit" EmptyDataText="Oops! No Activity Performed." AllowPaging="true"
                                        PageSize="100" OnPageIndexChanging="Page_Change" HeaderStyle-BackColor="#e0e0e0"
                                        AutoGenerateColumns="false" CssClass="myGridStyle" AllowSorting="true">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="WidthID" DataField="Removeid" Visible="false" />
                                            <asp:BoundField HeaderText="Remove Date" DataField="dd" DataFormatString="{0:d}" />
                                            <asp:BoundField HeaderText="Remove Lot Details" DataField="RemoveLotNo" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("Removeid") %>' CommandName="EditRow"
                                                        runat="server">
                                                        <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
                                                    </asp:LinkButton>
                                                    <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("Removeid") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndel" CommandArgument='<%#Eval("Removeid") %>' CommandName="Del"
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
                            <i class="fa fa-briefcase"></i>Lot Remove Process
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="list-group">
                                <asp:TextBox ID="txtid" Visible="false" runat="server"></asp:TextBox>
                                <%--  <label>Select Contact-Type</label>--%>
                                <br />
                                <div runat="server" id="ledger"  class="col-lg-4">
                                    <div style="padding-left: 10px; overflow-y: scroll; width: 420px; height: 142px">
                                        <asp:CheckBoxList ID="chllotno" OnSelectedIndexChanged="chllotno_changed" AutoPostBack="true" runat="server" Font-Size="Smaller" RepeatDirection="Vertical"
                                            RepeatColumns="1">
                                        </asp:CheckBoxList>
                                    </div>
                                    <asp:TextBox ID="txtsearching" runat="server" onkeyup="SearchEmployees(this,'#chllotno');"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                <asp:UpdatePanel ID="Upl" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <label>
                                        Lot No</label>
                                    <asp:TextBox Enabled="false" ID="txtlotno" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                    </ContentTemplate>
                                    <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="chllotno" EventName="SelectedIndexChanged" />
    </Triggers>--%>
                                  </asp:UpdatePanel>
                                </div>
                                <br />
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
