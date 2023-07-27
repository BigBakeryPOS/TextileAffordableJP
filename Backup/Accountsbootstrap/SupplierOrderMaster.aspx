<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierOrderMaster.aspx.cs"
    Inherits="Billing.Accountsbootstrap.SupplierOrderMaster" %>

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
    <title>Sample Order Master </title>
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
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1"> </asp:Label>
    <asp:Label runat="server" ID="lblShipmentDate" ForeColor="White" CssClass="label"
        Visible="false" Text="-15"> </asp:Label>
    <asp:Label runat="server" ID="lblFabHeadId" Text="(1)" ForeColor="White" CssClass="label"
        Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblHeadId" Text="(2)" ForeColor="White" CssClass="label"
        Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblCurrency" Text="2" ForeColor="White" CssClass="label"
        Visible="false"> </asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                            <i class="fa fa-briefcase"></i>
                            <asp:Label ID="lblName" Text="Sample Order Master" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" Style="background-color: Orange; height: 16px;
                                vertical-align: top" Enabled="false" /><label>New</label>&nbsp;&nbsp;
                            <asp:Button ID="Button2" runat="server" Style="background-color: Green; height: 16px;
                                vertical-align: top" Enabled="false" /><label>Refresh</label>
                        </div>
                        <div class="panel-body">
                            <div class="list-group">
                                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                    ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                                <div class="form-group">
                                </div>
                                <div class="col-lg-12">
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label>
                                                Order Type:</label>
                                            <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="form-control" Style="height: 30px"
                                                Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlBuyerCode_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Buyer Code:</label>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBuyerCode"
                                                ValueToCompare="BuyerCode" Operator="NotEqual" Type="String" ErrorMessage="Please Select Buyer Code."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlBuyerCode" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlBuyerCode_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Buyer Name:</label>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBuyerName"
                                                ValueToCompare="BuyerName" Operator="NotEqual" Type="String" ErrorMessage="Please Select Buyer Name."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlBuyerName" Enabled="false" runat="server" CssClass="form-control"
                                                Style="height: 30px" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label>
                                                Exc.No:</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtExcNo"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Exc.No." Style="color: Red" />
                                            <asp:TextBox ID="txtExcNo" runat="server" Width="100%" Enabled="false" ReadOnly="true"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Buyer PO No:</label>
                                            <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtBuyerPONo"
                                        ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Buyer PO No." Style="color: Red" />--%>
                                            <asp:TextBox ID="txtBuyerPONo" runat="server" Width="100%" CssClass="form-control"
                                                AutoComplete="Off" OnTextChanged="Check_BuyerOrder" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Shipment Mode:</label>
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlShipmentMode"
                                                ValueToCompare="Size" Operator="NotEqual" Type="String" ErrorMessage="Please Select Size."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlShipmentMode" runat="server" CssClass="form-control" Style="height: 30px"
                                                Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label>
                                                Main Fabric Code:</label>
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlFabricCode"
                                                ValueToCompare="FabricCode" Operator="NotEqual" Type="String" ErrorMessage="Please Select FabricCode."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlFabricCode" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlFabricCode_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Main Fabric Name:</label>
                                            <asp:CompareValidator ID="CompareValidator6" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlFabricName"
                                                ValueToCompare="FabricName" Operator="NotEqual" Type="String" ErrorMessage="Please Select FabricName."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlFabricName" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Payment Mode:</label>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPaymentMode"
                                                ValueToCompare="Size" Operator="NotEqual" Type="String" ErrorMessage="Please Select Size."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control" Style="height: 30px"
                                                Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label>
                                                Order Date:</label>
                                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control center-block"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtOrderDate"
                                                PopupButtonID="txtOrderDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Shipment Date:</label>
                                            <asp:TextBox ID="txtShipmentDate" runat="server" CssClass="form-control center-block"
                                                AutoComplete="Off" AutoPostBack="true" OnTextChanged="txtShipmentDate_OnTextChanged"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtShipmentDate"
                                                PopupButtonID="txtShipmentDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Delivery Date:</label>
                                            <asp:TextBox ID="txtDeliveryDate" runat="server" AutoComplete="Off" CssClass="form-control center-block"></asp:TextBox>
                                            <asp:TextBox ID="txtODeliveryDate" runat="server" Visible="false" CssClass="form-control center-block"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDeliveryDate"
                                                PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label>
                                                Payment Terms:</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtPaymentTerms"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Payment Terms." Style="color: Red" />
                                            <asp:TextBox ID="txtPaymentTerms" runat="server" Width="100%" Height="26px" AutoComplete="Off"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <%--<label>
                                        By the Way of:</label>--%>
                                            <label>
                                                Notes :
                                            </label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtBytheWayof"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Notes" Style="color: Red" />
                                            <asp:TextBox ID="txtBytheWayof" runat="server" Width="100%" Height="26px" AutoComplete="Off"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <br />
                                            <label>
                                                Labels:</label>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnLabel" runat="server" OnClick="btnLabel_OnClick" Style="background-color: Gray;
                                                height: 16px; vertical-align: top" Width="50px" />
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="form-group">
                                                        <label>
                                                            Currency:</label>
                                                        <asp:DropDownList ID="ddlCurrency" runat="server" Style="height: 30px" Width="80px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="form-group">
                                                        <label>
                                                            Amount:</label>
                                                        <asp:TextBox ID="txtAmount" ReadOnly="true" runat="server" Width="100%" Height="30px"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnCurrency" runat="server" OnClick="btnCurrency_OnClick" Style="background-color: Orange;
                                                            height: 16px; vertical-align: top" />
                                                        <asp:Button ID="btnCurrencyRef" runat="server" OnClick="btnCurrencyRef_OnClick" Style="background-color: Green;
                                                            height: 16px; vertical-align: top" />
                                                    </div>
                                                    <div runat="server" id="divnarration" visible="false" class="form-group">
                                                        <label>
                                                            Reason :</label>
                                                        <asp:TextBox ID="txtdeliveryreason" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="form-group">
                                                        <label>
                                                            Approved:</label><br />
                                                        <asp:CheckBox ID="chkApproved" runat="server" Style="height: 30px" />
                                                    </div>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <div class="form-group">
                                                        <label>
                                                            Lock:</label><br />
                                                        <asp:CheckBox ID="chkLock" runat="server" Style="height: 30px" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="form-group">
                                                        <label>
                                                            Cancel:</label><br />
                                                        <asp:CheckBox ID="chkCancel" runat="server" Style="height: 30px" />
                                                    </div>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <div class="form-group">
                                                        <label>
                                                            Shipped:</label><br />
                                                        <asp:CheckBox ID="chkShipped" runat="server" Style="height: 30px" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="form-group">
                                                        <label>
                                                            Hold:</label><br />
                                                        <asp:CheckBox ID="chkHold" runat="server" Style="height: 30px" />
                                                    </div>
                                                </td>
                                                <td colspan="2">
                                                <asp:Button id="btnupdateShipped" runat="server" OnClick="btnupdateShipped_OnClick" Text="Update" Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="col-lg-12">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:RadioButtonList ID="raditemtype" runat="server" OnSelectedIndexChanged="itemtype_chnaged"
                    Width="210px" AutoPostBack="true" RepeatColumns="2">
                    <asp:ListItem Text="Set Wise" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Style Wise" Value="1" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <div class="col-lg-12">
                    <div class="col-lg-2">
                        <table>
                            <tr>
                                <td>
                                    <div runat="server" visible="false" id="setwise" class="form-group">
                                        <label>
                                            Style:</label><br />
                                        <asp:DropDownList ID="drpset" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="200px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Style:</label><br />
                                        <asp:DropDownList ID="ddlStyle" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="200px" OnSelectedIndexChanged="Style_changed" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="Button3" runat="server" OnClick="btnStyleRefresh_OnClick" Style="background-color: Green;
                                            height: 16px; vertical-align: top" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="form-group">
                                        <asp:Label ID="lblitemdesc" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-lg-2">
                        <table>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <label>
                                            Color:</label><br />
                                        <asp:DropDownList ID="ddlColor" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="200px">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="Button4" runat="server" OnClick="btnColorRefresh_OnClick" Style="background-color: Green;
                                            height: 16px; vertical-align: top" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <label>
                                Rate:</label>
                            <asp:TextBox ID="txtRate" runat="server" Width="100%" Height="26px" AutoComplete="Off"
                                CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender313d" runat="server"
                                TargetControlID="txtRate" ValidChars="." FilterType="Numbers,Custom" />
                        </div>
                        <div class="form-group">
                            <label>
                                Qty:</label>
                            <asp:TextBox ID="txtQty" runat="server" Width="100%" AutoComplete="Off" Height="26px"
                                CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender313ds" runat="server"
                                TargetControlID="txtQty" FilterType="Numbers" />
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <label>
                                Cut.Extr %</label><br />
                            <asp:TextBox ID="txtcuttingratio" runat="server" AutoComplete="Off" Width="100%"
                                Height="26px" CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                TargetControlID="txtcuttingratio" ValidChars="." FilterType="Numbers,Custom" />
                        </div>
                        <div class="form-group">
                            <label>
                                Affec Qty:</label>
                            <asp:TextBox ID="txtAffectedQty" runat="server" Width="100%" Height="26px" CssClass="form-control"
                                ReadOnly="true"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender313dss" runat="server"
                                TargetControlID="txtAffectedQty" FilterType="Numbers" />
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <table>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <label>
                                            Size:</label>
                                        <asp:DropDownList ID="ddlSize" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="160px" AutoPostBack="true" OnSelectedIndexChanged="ddlSize_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSizeRefresh" runat="server" OnClick="btnSizeRefresh_OnClick" Style="background-color: Green;
                                            height: 16px; vertical-align: top" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="form-group">
                                        <label>
                                            Cut. Qty:</label>
                                        <asp:TextBox ID="txtCQty" Enabled="false" Visible="true" runat="server" Width="100%"
                                            Height="26px" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-3">
                        <asp:GridView ID="GVSizes" AutoGenerateColumns="False" GridLines="Both" runat="server">
                            <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdSize" runat="server" Value='<%#Eval("SizeId") %>' />
                                        <asp:Label ID="lblSize" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ratio">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRatio" runat="server" Width="70px" Text='<%#Eval("Ratio")%>'></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312sdf" runat="server"
                                            TargetControlID="txtRatio" ValidChars="." FilterType="Numbers,Custom" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" Width="70px" Text='<%#Eval("Qty")%>'></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312fff" runat="server"
                                            TargetControlID="txtQty" FilterType="Numbers" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="true" HeaderText="Cut.Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCqty" Text='<%#Eval("CQty")%>' Width="70px" runat="server"></asp:Label>
                                        <asp:Label ID="lblCRatio" Visible="false" runat="server" Text='<%#Eval("CRatio")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <asp:Button ID="btnCheckDetails" runat="server" Text="Check Ratio Qty" Width="130px"
                                class="btn btn-success" OnClick="btnCheckDetails_OnClick" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit Color Wise" Width="130px"
                                class="btn btn-warning" OnClick="btnSubmit_OnClick" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnSubmit1" runat="server" Text="Submit and Clear" Width="130px"
                                class="btn btn-danger" OnClick="btnSubmit1_OnClick" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="col-lg-9">
                        <asp:GridView ID="GVItem" AutoGenerateColumns="False" runat="server" OnRowCommand="GVItem_OnRowCommand"
                            GridLines="Both" OnRowDeleting="GVItem_RowDeleting" Caption="Style Details">
                            <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="1%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="StyleNo" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                    ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdStyleNoId" runat="server" Value='<%#Eval("StyleNoId") %>' />
                                        <asp:Label ID="lblStyleNo" runat="server" Text='<%#Eval("StyleNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" HeaderStyle-Width="40%" ItemStyle-Width="40%"
                                    ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Color" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                    ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId") %>' />
                                        <asp:Label ID="lblColor" runat="server" Text='<%#Eval("Color") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="4%" ItemStyle-Width="4%"
                                    ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ord.Qty" HeaderStyle-Width="4%" ItemStyle-Width="4%"
                                    ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Afft.Qty" HeaderStyle-Width="4%" ItemStyle-Width="4%"
                                    ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAffectedQty" runat="server" Text='<%#Eval("AffectedQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cut.Ext%" HeaderStyle-Width="4%" ItemStyle-Width="4%"
                                    ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCRatio" runat="server" Text='<%#Eval("CRatio") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cut.Qty" HeaderStyle-Width="4%" ItemStyle-Width="4%"
                                    ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCQty" runat="server" Text='<%#Eval("CQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size" HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                    ItemStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdRangeId" runat="server" Value='<%#Eval("RangeId") %>' />
                                        <asp:Label ID="lblSize" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modify" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%"
                                    ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId") %>' />
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                            CommandName="Modify">
                                            <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="2%"
                                    ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                            CommandName="View">
                                            <asp:Image ID="imgView" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-lg-3">
                        <asp:GridView ID="GVSizesView" AutoGenerateColumns="False" GridLines="Both" runat="server"
                            Caption="Style Size Details">
                            <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size" HeaderStyle-Width="100px" ItemStyle-Font-Size="Large"
                                    ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdSize" runat="server" Value='<%#Eval("SizeId") %>' />
                                        <asp:Label ID="lblSize" Height="30px" Width="150px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ratio" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRatio" Height="30px" Width="80px" runat="server" Text='<%#Eval("Ratio")%>'></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312sdf" runat="server"
                                            TargetControlID="txtRatio" ValidChars="." FilterType="Numbers,Custom" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" Height="30px" Width="80px" runat="server" Text='<%#Eval("Qty")%>'></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312fff" runat="server"
                                            TargetControlID="txtQty" FilterType="Numbers" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="true" HeaderText="Cut.Qty" HeaderStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCqty" Text='<%#Eval("CQty")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblCRatio" Visible="false" runat="server" Text='<%#Eval("CRatio") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCheckDetails" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmit1" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div>
        <div class="col-lg-12">
            <div class="col-lg-5">
                <label>
                    Remarks:</label>
                <asp:TextBox ID="txtRemarks" Height="80px" Width="100%" runat="server" AutoComplete="Off"
                    CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-lg-5">
            </div>
            <div class="col-lg-1">
                <br />
                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" ValidationGroup="val1"
                    Style="width: 110px;" OnClick="btnSave_OnClick" OnClientClick="Confirm(this)"
                    UseSubmitBehavior="false" />
            </div>
            <div class="col-lg-1">
                <br />
                <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" Style="width: 110px;"
                    OnClick="btnExit_OnClick" />
            </div>
        </div>
    </div>
    <div class="col-lg-12">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:LinkButton Text="" ID="LinkButton1" runat="server"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="mpecost" runat="server" PopupControlID="Panelmpecost"
                    TargetControlID="LinkButton1">
                </ajaxToolkit:ModalPopupExtender>
                <asp:HiddenField ID="hdpopupid" runat="server" />
                <asp:Panel ID="Panelmpecost" runat="server" ScrollBars="Auto" Height="700px" Width="100%"
                    BackColor="Silver" Style="display: none">
                    <div class="header" align="right">
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" OnClick="btnClose_OnClick" />
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <asp:Label ID="lblLabelText" runat="server" Text="Label Details:-" Style="font-weight: bold"></asp:Label>
                            </div>
                            <div class="col-lg-9" style="padding-top: 2pc">
                                <div id="Div3" runat="server">
                                    <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvLabels')"
                                        ID="txtsearch" runat="server" Style="margin-top: -20px" placeholder="Enter Text to Search"
                                        Width="100%"></asp:TextBox>
                                    <asp:GridView ID="gvLabels" AutoGenerateColumns="False" ShowFooter="True" Width="100%"
                                        GridLines="Both" runat="server">
                                        <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLabelItem" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="40%">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdItemId" Value='<%# Eval("itemmasterid")%>' runat="server" />
                                                    <asp:Label ID="lblItemCode" Text='<%# Eval("description")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Image ID="lbliamge" runat="server" ImageUrl='<%# Eval("itemimage")%>' Style="border-width: 0px;
                                                        width: 5pc;" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:ImageField DataImageUrlField="itemimage" HeaderText="Image" ItemStyle-Width="15%"  />--%>
                                            <asp:TemplateField HeaderText="Notes">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLabelText" runat="server" Width="300px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image Upload">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="fp_Upload" runat="server" />
                                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_OnClick"
                                                                Width="100px" />
                                                            <asp:Image ID="img_Photo" runat="server" Width="100px" BorderColor="1" />
                                                            <asp:Label ID="lblFile_Path" runat="server" Visible="false"></asp:Label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnUpload" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-lg-1">
                            </div>
                        </div>
                    </div>
                    <div class="footer" align="left">
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/chosen.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect:
    true
        }); </script>
    </form>
</body>
</html>
