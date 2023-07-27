<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemMasterGrop.aspx.cs"
    Inherits="Billing.Accountsbootstrap.ItemMasterGrop" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Item Master </title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
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
            </div>
            <div class="col-lg-8">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                        <i class="fa fa-briefcase"></i>
                        <asp:Label ID="lblName" Text="Item Master" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" OnClick="btnItemTypeGroup_OnClick" Style="background-color: Orange;
                            height: 16px; vertical-align: top" /><label>New</label>&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" OnClick="btnItemTypeGroup_OnClick" Style="background-color: Green;
                            height: 16px; vertical-align: top" /><label>Refresh</label>
                    </div>
                    <div class="panel-body">
                        <div class="list-group">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="form-group">
                            </div>
                            <div class="col-lg-12">
                                <div class="col-lg-6">
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label style="text-align: right">
                                                        Item Type/Group:</label>
                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlItemTypeGroup"
                                                        ValueToCompare="Select ItemGroup" Operator="NotEqual" Type="String" ErrorMessage="Please Select Item Type/Group."></asp:CompareValidator>
                                                    <asp:DropDownList ID="ddlItemTypeGroup" runat="server" CssClass="form-control" Style="height: 30px"
                                                        Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlItemTypeGroup_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnItemTypeGroup" runat="server" OnClick="btnItemTypeGroup_OnClick"
                                                        Style="background-color: Orange; height: 16px; vertical-align: top" />
                                                    <asp:Button ID="btnItemTypeGroupRef" runat="server" OnClick="btnItemTypeGroupRef_OnClick"
                                                        Style="background-color: Green; height: 16px; vertical-align: top" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label style="text-align: right">
                                                        Item Name:</label>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlItemName"
                                                        ValueToCompare="ItemName" Operator="NotEqual" Type="String" ErrorMessage="Please Select Item Name."></asp:CompareValidator>
                                                    <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlItemName_OnSelectedIndexChanged" Style="height: 30px"
                                                        Width="250px">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnItemName" runat="server" OnClick="ItemName_OnClick" Style="background-color: Orange;
                                                        height: 16px; vertical-align: top" />
                                                    <asp:Button ID="btnItemNameRef" runat="server" OnClick="ItemNameRef_OnClick" Style="background-color: Green;
                                                        height: 16px; vertical-align: top" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="form-group">
                                        <label>
                                            Description:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtDescription"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Check Description." Style="color: Red" />
                                        <asp:TextBox ID="txtDescription" runat="server" ReadOnly="true" Width="100%" Height="26px"></asp:TextBox>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" ValidationGroup="val1"
                                                    Style="width: 110px;" OnClick="btnSave_OnClick" />
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" Style="width: 110px;"
                                                    OnClick="btnExit_OnClick" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label style="text-align: right">
                                            Head:</label>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlHead" ValueToCompare="ItemHead"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please Check ItemHead."></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlHead" runat="server" CssClass="form-control" Enabled="false"
                                            Style="height: 30px" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Item Code:</label>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlItemCode"
                                            ValueToCompare="ItemCode" Operator="NotEqual" Type="String" ErrorMessage="Please Check ItemCode."></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlItemCode" runat="server" CssClass="form-control" Enabled="false"
                                            Style="height: 30px" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label style="text-align: right">
                                                        Size:</label>
                                                    <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control" Style="height: 30px"
                                                        Width="100px" AutoPostBack="true" OnSelectedIndexChanged="Description_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <br />
                                                    <asp:DropDownList ID="ddlSizeUoM" runat="server" CssClass="form-control" Style="height: 30px"
                                                        Width="150px" AutoPostBack="true" OnSelectedIndexChanged="Description_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnUoM" runat="server" OnClick="btnUoM_OnClick" Style="background-color: Orange;
                                                        height: 16px; vertical-align: top" />
                                                    <asp:Button ID="btnSizeUoMRef" runat="server" OnClick="btnSizeUoMRef_OnClick" Style="background-color: Green;
                                                        height: 16px; vertical-align: top" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Category:</label>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" Style="height: 30px"
                                                        Width="250px" AutoPostBack="true" OnSelectedIndexChanged="Description_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td runat="server" visible="false">
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="Button3" runat="server" />
                                                    <asp:Button ID="Button4" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        OrderingUoM:</label>
                                                    <asp:DropDownList ID="ddlOrderingUoM" runat="server" CssClass="form-control" Style="height: 30px"
                                                        Width="250px">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnOrderingUoM" runat="server" OnClick="btnUoM_OnClick" Style="background-color: Orange;
                                                        height: 16px; vertical-align: top" />
                                                    <asp:Button ID="btnOrderingUoMRef" runat="server" OnClick="btnOrderingUoMRef_OnClick"
                                                        Style="background-color: Green; height: 16px; vertical-align: top" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="form-group">
                                        <label>
                                            UoM Conversation:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtUoMConversation"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Check UoM Conversation."
                                            Style="color: Red" />
                                        <asp:TextBox ID="txtUoMConversation" runat="server" Width="100%" Height="26px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtUoMConversation" />
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        IssuingUoM:</label>
                                                    <asp:DropDownList ID="ddlIssuingUoM" runat="server" CssClass="form-control" Style="height: 30px"
                                                        Width="250px">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnIssuingUoM" runat="server" OnClick="btnUoM_OnClick" Style="background-color: Orange;
                                                        height: 16px; vertical-align: top" />
                                                    <asp:Button ID="btnIssuingUoMMRef" runat="server" OnClick="btnIssuingUoMMRef_OnClick"
                                                        Style="background-color: Green; height: 16px; vertical-align: top" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="form-group">
                                        <label>
                                            Date:</label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control center-block"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" PopupButtonID="txtFromDate"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Active:</label>
                                        <asp:DropDownList ID="ddlActive" runat="server" CssClass="form-control" Style="height: 30px"
                                            Width="100%">
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-2">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
