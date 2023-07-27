<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usercreate.aspx.cs" Inherits="Billing.Accountsbootstrap.usercreate" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>User Registration</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function valchk() {
            if (blankchk(document.getElementById('txtBrandname'), "Branch Name")
            {
                alert("true");
            }
            else {
                alert("false");
                return false;
            }
        }
    </script>
    <!-- Bootstrap Core CSS -->
    <link href="../css/responsive-tabs.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" href="../css/bootstrap.min.css" runat="server" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link id="Link2" href="../css/plugins/metisMenu/metisMenu.min.css" runat="server"
        rel="stylesheet" />
    <!-- Custom CSS -->
    <link id="Link3" href="../css/sb-admin-2.css" runat="server" rel="stylesheet" />
    <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
    <script src="../js/jquery.responsiveTabs.min.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.0.min.js" type="text/javascript"></script>
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link rel="stylesheet" href="../css/chosen.css" />
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <form id="Form1" runat="server">
                    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                        ID="val1" ShowMessageBox="true" ShowSummary="false" />
                    <div class="col-lg-12">
                        <div class="col-lg-2">
                            <h1 class="page-header" style="text-align: center; color: #fe0002;">
                                User Master</h1>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group ">
                                <label>
                                    Select Employee</label>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlemployee"
                                    ValueToCompare="Select Employee" Operator="NotEqual" Type="String" ErrorMessage="Please Select Employee"></asp:CompareValidator>
                                <asp:DropDownList CssClass="form-control" ID="ddlemployee" Width="240px" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlemployee_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group ">
                                <label>
                                    User Name</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator1"
                                    ControlToValidate="txtusername" ErrorMessage="Please Enter User Name." Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtusername" Width="240px" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group ">
                                <label>
                                    Password</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="reqName"
                                    ControlToValidate="txtpassword" ErrorMessage="Please Enter Password." Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtpassword" TextMode="Password" Width="240px"
                                    runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group ">
                                <label>
                                    Confirm Password</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator2"
                                    ControlToValidate="txtconfirmpasswprd" ErrorMessage="Please Enter Confirm Passowrd."
                                    Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtconfirmpasswprd" TextMode="Password"
                                    Width="240px" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group ">
                                <label>
                                    Email</label>
                                <asp:TextBox CssClass="form-control" ID="txtEmail" Width="240px" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-group ">
                                <label>
                                    Select Branch</label>
                                <asp:DropDownList ID="drpbranch" runat="server" Width="240px" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <br />
                            <asp:Button ID="btnadd" runat="server" class="btn btn-primary" Text="Save" OnClick="Add_Click"
                                ValidationGroup="val1" Style="width: 117px;" />
                        </div>
                        <div class="col-lg-1">
                            <br />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-info" Text="Exit" OnClick="Exit_Click"
                                Style="width: 120px;" />
                        </div>
                        <div class="col-lg-2">
                            <asp:TextBox CssClass="form-control" ID="txtUserid" runat="server" Enabled="false"
                                Visible="false"></asp:TextBox>
                            <div id="Div2" runat="server" visible="false" class="checkbox">
                                All Branch:<asp:CheckBox Style="margin-left: 20px;" ID="chkRememberMe" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div id="horizontalTab" style="background-color: #D0D3D6; padding-left: 30px">
                            <ul>
                                <li><a href="#tab-2">Master</a></li>
                                <li><a href="#tab-3">Process</a></li>
                                <li><a href="#tab-5">Reports</a></li>
                                <li><a href="#tab-6">Admin</a></li>
                            </ul>
                            <div class="row" id="tab-2" style="background-color: #D0D3D6; padding-top: 50px">
                                <div style="background-color: #D0D3D6;">
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdmaster" AutoGenerateColumns="False" BorderWidth="1px" BorderStyle="Solid"
                                                    GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="myGridStyle"
                                                    Width="900px">
                                                    <RowStyle CssClass="dataRow" />
                                                    <SelectedRowStyle CssClass="SelectdataRow" />
                                                    <AlternatingRowStyle CssClass="altRow" />
                                                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                    <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                    <FooterStyle CssClass="dataRow" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("roleid")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField DataField="Screen" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField DataField="Screenid" HeaderText="Screen" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Screen Visible"
                                                            HeaderStyle-BorderColor="Gray">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial"
                                                                    Font-Size="11px" Checked='<%# Bind("Visible") %>'></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row" id="tab-3" style="background-color: #D0D3D6; padding-top: 50px">
                                <div style="background-color: #D0D3D6;">
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdinventory" AutoGenerateColumns="False" BorderWidth="1px" BorderStyle="Solid"
                                                    GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="myGridStyle"
                                                    Width="900px">
                                                    <RowStyle CssClass="myGridStyle" />
                                                    <SelectedRowStyle CssClass="SelectdataRow" />
                                                    <AlternatingRowStyle CssClass="altRow" />
                                                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                    <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                    <FooterStyle CssClass="dataRow" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("roleid")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField DataField="Screen" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField DataField="Screenid" HeaderText="Screen" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Screen Visible"
                                                            HeaderStyle-BorderColor="Gray">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial"
                                                                    Font-Size="11px" Checked='<%# Bind("Visible") %>'></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row" id="tab-5" style="background-color: #D0D3D6; padding-top: 50px">
                                <div style="background-color: #D0D3D6;">
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdreport" AutoGenerateColumns="False" BorderWidth="1px" BorderStyle="Solid"
                                                    GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="myGridStyle"
                                                    Width="900px">
                                                    <RowStyle CssClass="dataRow" />
                                                    <SelectedRowStyle CssClass="SelectdataRow" />
                                                    <AlternatingRowStyle CssClass="altRow" />
                                                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                    <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                    <FooterStyle CssClass="dataRow" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("roleid")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField DataField="Screen" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField DataField="Screenid" HeaderText="Screen" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Screen Visible"
                                                            HeaderStyle-BorderColor="Gray">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial"
                                                                    Font-Size="11px" Checked='<%# Bind("Visible") %>'></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row" id="tab-6" style="background-color: #D0D3D6; padding-top: 30px">
                                <div style="background-color: #D0D3D6;">
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdadmin" AutoGenerateColumns="False" BorderWidth="1px" BorderStyle="Solid"
                                                    GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="myGridStyle"
                                                    Width="900px">
                                                    <RowStyle CssClass="dataRow" />
                                                    <SelectedRowStyle CssClass="SelectdataRow" />
                                                    <AlternatingRowStyle CssClass="altRow" />
                                                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                    <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                    <FooterStyle CssClass="dataRow" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("roleid")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField DataField="Screen" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:BoundField DataField="Screenid" HeaderText="Screen" ReadOnly="true" ApplyFormatInEditMode="false"
                                                            HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Screen Visible"
                                                            HeaderStyle-BorderColor="Gray">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkboxAdd" runat="server" Style="color: Black" Text="" Font-Names="arial"
                                                                    Font-Size="11px" Checked='<%# Bind("Visible") %>'></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
                            <script type="text/javascript">
                                $(document).ready(function () {
                                    var $tabs = $('#horizontalTab');
                                    $tabs.responsiveTabs({
                                        rotate: false,
                                        startCollapsed: 'accordion',
                                        collapsible: 'accordion',
                                        setHash: true,

                                        activate: function (e, tab) {
                                            $('.info').html('Tab <strong>' + tab.id + '</strong> activated!');
                                        },
                                        activateState: function (e, state) {
                                            //console.log(state);
                                            $('.info').html('Switched from <strong>' + state.oldState + '</strong> state to <strong>' + state.newState + '</strong> state!');
                                        }
                                    });

                                    /* $('#start-rotation').on('click', function () {
                                    $tabs.responsiveTabs('startRotation', 1000);
                                    });
                                    $('#stop-rotation').on('click', function () {
                                    $tabs.responsiveTabs('stopRotation');
                                    });
                                    $('#start-rotation').on('click', function () {
                                    $tabs.responsiveTabs('active');
                                    });
                                    $('#enable-tab').on('click', function () {
                                    $tabs.responsiveTabs('enable', 3);
                                    });
                                    $('#disable-tab').on('click', function () {
                                    $tabs.responsiveTabs('disable', 3);
                                    });
                                    $('.select-tab').on('click', function () {
                                    $tabs.responsiveTabs('activate', $(this).val()); */

                                });
            
        
                            </script>
                        </div>
                    </div>
                    <%--  <script src="../js/jquery.min.js" type="text/javascript"></script>
                    <script src="../js/chosen.min.js" type="text/javascript"></script>
                    <script type="text/javascript">
                        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect:
    true
                        }); </script>--%>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
