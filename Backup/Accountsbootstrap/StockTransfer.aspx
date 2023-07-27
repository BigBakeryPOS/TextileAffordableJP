<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockTransfer.aspx.cs"
    Inherits="Billing.Accountsbootstrap.StockTransfer" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Stock Transfer </title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../css/chosen.css" rel="Stylesheet" />
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
    <link rel="stylesheet" href="../css/chosen.css" />
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
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1"> </asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row" style="margin-top: -10px">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblName" Text="Stock Transfer" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <div class="panel-body">
                    <div class="list-group">
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                        <div class="form-group">
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>
                                    Entry No:</label>
                                <asp:TextBox ID="txtEntryNo" runat="server" CssClass="form-control" Width="100px"
                                    Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>
                                    Entry Date:</label>
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control" Width="110px"
                                    ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender12" TargetControlID="txtEntryDate"
                                    EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>
                                    Challan No :</label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtChallanNo"
                                    ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Challan No." Style="color: Red" />
                                <asp:TextBox ID="txtChallanNo" runat="server" CssClass="form-control" Width="120px"
                                    AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    Transfer Type:</label>
                                <asp:DropDownList ID="ddlTransferType" runat="server" CssClass="form-control" Style="height: 30px"
                                    Width="100%">
                                    <asp:ListItem Text="Inward" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Outward" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    From Party:</label>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlFromPartyCode"
                                    ValueToCompare="Select From Party" Operator="NotEqual" Type="String" ErrorMessage="Please Select From Party."></asp:CompareValidator>
                                <asp:DropDownList ID="ddlFromPartyCode" runat="server" CssClass="chzn-select" Style="height: 30px"
                                    Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    To Party:</label>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlToPartyCode"
                                    ValueToCompare="Select To Party" Operator="NotEqual" Type="String" ErrorMessage="Please Select To Party."></asp:CompareValidator>
                                <asp:DropDownList ID="ddlToPartyCode" runat="server" CssClass="chzn-select" Style="height: 30px"
                                    Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    CompanyName:</label>
                                <asp:CompareValidator ID="CompareValidator11" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCompany" ValueToCompare="Select CompanyName"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select CompanyName."></asp:CompareValidator>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="chzn-select" Style="height: 30px"
                                    Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    Item Group :</label>
                                <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="chzn-select" Style="height: 30px"
                                    Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlItemGroup_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>
                                    Issue Name :</label>
                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="chzn-select" Style="height: 30px"
                                    Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>
                                    Color :</label>
                                <asp:DropDownList ID="ddlColor" runat="server" CssClass="chzn-select" Style="height: 30px"
                                    Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>
                                    Qty</label>
                                <asp:TextBox ID="txtQty" Height="30px" Width="100%" runat="server" AutoComplete="Off"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                    TargetControlID="txtQty" ValidChars="." FilterType="Numbers,Custom" />
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>
                                    Rate</label>
                                <asp:TextBox ID="txtRate" Height="30px" Width="100%" runat="server" AutoComplete="Off"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    TargetControlID="txtRate" ValidChars="." FilterType="Numbers,Custom" />
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <br />
                            <div class="form-group">
                                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnSubmit_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="col-lg-2">
            </div>
            <div class="col-lg-8">
                <div id="div5" runat="server" style="overflow: auto; height: 150px; width: 100%">
                    <asp:HiddenField ID="hdRowIndex" runat="server" />
                    <asp:GridView ID="GVItem" AutoGenerateColumns="False" GridLines="Both" runat="server"
                        OnRowEditing="GVItem_OnRowEditing" OnRowDeleting="GVItem_RowDeleting" OnRowCommand="GVItem_RowCommand">
                        <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                        <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                        <Columns>
                            <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:HiddenField ID="hdItemGroupId" runat="server" Value='<%#Eval("ItemGroupId") %>' />
                                    <asp:HiddenField ID="hdItemId" runat="server" Value='<%#Eval("ItemId") %>' />
                                    <asp:HiddenField ID="hdItemName" runat="server" Value='<%#Eval("ItemName") %>' />
                                    <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId") %>' />
                                    <asp:HiddenField ID="hdColor" runat="server" Value='<%#Eval("Color") %>' />
                                    <asp:HiddenField ID="hdQty" runat="server" Value='<%#Eval("Qty") %>' />
                                    <asp:HiddenField ID="hdRate" runat="server" Value='<%#Eval("Rate") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                            <asp:BoundField DataField="Color" HeaderText="ColorName" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Rate" HeaderText="Rate" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" />
                            <asp:CommandField ControlStyle-Width="100%" ShowEditButton="true" ButtonType="Button" />
                            <asp:CommandField ControlStyle-Width="100%" ShowDeleteButton="True" ButtonType="Button" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-lg-2">
            </div>
        </div>
        <div class="col-lg-12">
            <div class="col-lg-4">
                <label>
                    Remarks:</label>
                <asp:TextBox ID="txtRemarks" AutoComplete="Off" Height="80px" Width="100%" runat="server"
                    CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-lg-6">
            </div>
            <div class="col-lg-1">
                <br />
                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" ValidationGroup="val1"
                    Style="width: 90px;" OnClick="btnSave_OnClick" OnClientClick="Confirm(this)"
                    UseSubmitBehavior="false" />
            </div>
            <div class="col-lg-1">
                <br />
                <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" Style="width: 90px;"
                    OnClick="btnExit_OnClick" />
            </div>
        </div>
    </div>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/chosen.min.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen();
        $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
