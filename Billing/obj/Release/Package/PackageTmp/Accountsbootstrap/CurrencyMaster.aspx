<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrencyMaster.aspx.cs"
    Inherits="Billing.Accountsbootstrap.CurrencyMaster" %>

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
    <title>Currency Master</title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
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
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-2">
                <h2 class="page-header" style="text-align: left; color: #fe0002; font-size: 20px">
                    Currency Master</h2>
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
                AccessKey="N" Style="width: 120px; margin-top: -54px; margin-left: 221px;" />
        </div>
        <div class="col-lg-2">
            <asp:Button ID="btnexcel" runat="server" class="btn btn-info" Text="Export-To-Excel"
                Style="width: 120px; margin-top: -34px; margin-left: 535px;" Height="32px" />
        </div>
        <%--<label>Alt+N</label>--%>
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
                                            DataKeyNames="CurrencyId" AutoGenerateColumns="false" CssClass="myGridStyle1"
                                            OnSelectedIndexChanged="gridview_OnSelectedIndexChanged" OnRowCommand="gvCurrencyDetails_RowCommand">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField DataField="CurrencyName" SortExpression="CurrencyName" HeaderText="CurrencyName"
                                                    ItemStyle-Width="25%" />
                                                <asp:BoundField DataField="Value" SortExpression="Value" HeaderText="Value" ItemStyle-Width="25%" />
                                                <asp:BoundField DataField="LastUpdate" SortExpression="LastUpdate" HeaderText="LastUpdate"
                                                    DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                                <asp:BoundField DataField="IsActive" SortExpression="IsActive" HeaderText="Is Active"
                                                    ItemStyle-Width="25%" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderText="Modify">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("CurrencyId") %>' CommandName="Select"
                                                            runat="server">
                                                            <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
                                                        </asp:LinkButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("CurrencyId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    HeaderText="History">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%#Eval("CurrencyId") %>'
                                                            CommandName="CurrencyHistory">View
                                                        </asp:LinkButton>
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
        <div class="col-lg-4" style="margin-left: -14px">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #336699; color: White;">
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblName" Text="Add Currency" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="list-group">
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val2"
                            ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="txtCurrencyID" runat="server" Visible="false"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>
                                Enter Currency</label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtCurrency"
                                ValidationGroup="val2" Text="*" ErrorMessage="Please enter your Currency." Style="color: Red" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -"
                                TargetControlID="txtCurrency" />
                            <asp:TextBox CssClass="form-control" ID="txtCurrency" runat="server" placeholder="To Add New Currency"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>
                                Value</label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtValue"
                                ValidationGroup="val2" Text="*" ErrorMessage="Please enter Value." Style="color: Red" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtValue" />
                            <asp:TextBox CssClass="form-control" ID="txtValue" runat="server" placeholder="To Add New Value"></asp:TextBox>
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
    <div class="col-lg-12">
        <div class="col-lg-4">
        </div>
        <div class="col-lg-4">
            <center>
                <asp:LinkButton Text="" ID="LinkButton1" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="mpecost" runat="server" PopupControlID="Panelmpecost"
                    TargetControlID="LinkButton1">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="Panelmpecost" runat="server" ScrollBars="Auto" Height="600px" Width="100%"
                    BackColor="Silver" Style="display: none">
                    <div class="header" align="right">
                        <asp:Button ID="btnexit" runat="server" Text="Exit" CssClass="button" OnClick="btnresret_OnClick" />
                    </div>
                    <asp:GridView ID="gvCurrencyDetails" runat="server" AllowPaging="false" ShowFooter="true"
                        Width="100%" AutoGenerateColumns="false" EmptyDataText="No data found!" ShowHeaderWhenEmpty="True">
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField AccessibleHeaderText="SNo" HeaderText="S.No">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex +1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="CurrencyName" DataField="CurrencyName" />
                            <asp:BoundField HeaderText="Value" DataField="Value" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField HeaderText="IsActive" DataField="IsActive" />
                            <asp:BoundField HeaderText="Type" DataField="Type" />
                            <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                        </Columns>
                    </asp:GridView>
                    <div class="footer" align="left">
                    </div>
                </asp:Panel>
            </center>
        </div>
        <div class="col-lg-4">
        </div>
    </div>
    </form>
</body>
</html>
