<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyerOrderCutting.aspx.cs"
    Inherits="Billing.Accountsbootstrap.BuyerOrderCutting" %>

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
    <title>Buyer Order Cutting </title>
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
    <asp:Label runat="server" ID="lblShipmentDate" ForeColor="White" CssClass="label"
        Visible="false" Text="-15"> </asp:Label>
    <asp:Label runat="server" ID="lblFabHeadId" Text="(1)" ForeColor="White" CssClass="label"
        Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblHeadId" Text="(2)" ForeColor="White" CssClass="label"
        Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblProcess" Text="1" ForeColor="White" CssClass="label"
        Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblEmployee" Text="7" ForeColor="White" CssClass="label"
        Visible="false"> </asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblName" Text="Buyer Order Cutting" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <div class="panel-body">
                    <div class="list-group">
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val2"
                            ID="ValidationSummary2" ShowMessageBox="true" ShowSummary="false" />
                        <div class="form-group">
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Entry No:</label>
                                    <asp:TextBox ID="txtCuttingNo" runat="server" CssClass="form-control" Enabled="false">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Exc.No:
                                    </label>
                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlExcNo" ValueToCompare="Select ExcNo"
                                        Operator="NotEqual" Type="String" ErrorMessage="Please Select ExcNo."></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlExcNo" runat="server" CssClass="chzn-select" Style="height: 30px"
                                        Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlExcNo_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Order Type:</label>
                                    <asp:DropDownList ID="ddlOrderType" runat="server" Style="height: 30px" Width="100%"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Buyer Code:</label>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBuyerCode"
                                        ValueToCompare="BuyerCode" Operator="NotEqual" Type="String" ErrorMessage="Please Select Buyer Code."></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlBuyerCode" runat="server" Style="height: 30px" Width="100%"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" runat="server" visible="false">
                                    <label>
                                        Buyer Name:</label>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBuyerName"
                                        ValueToCompare="BuyerName" Operator="NotEqual" Type="String" ErrorMessage="Please Select Buyer Name."></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlBuyerName" Enabled="false" runat="server" CssClass="form-control"
                                        Style="height: 30px" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        CompanyName</label>
                                    <asp:CompareValidator ID="CompareValidator11" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCompanyName"
                                        ValueToCompare="CompanyName" Operator="NotEqual" Type="String" ErrorMessage="Please Select CompanyName."></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" Style="height: 30px"
                                        Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyName_OnSelectedIndexChanged">
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
                                    <asp:DropDownList ID="ddlFabricCode" runat="server" CssClass="form-control" Style="height: 30px"
                                        Width="100%" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Main Fabric Name:</label>
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlFabricName"
                                        ValueToCompare="FabricName" Operator="NotEqual" Type="String" ErrorMessage="Please Select FabricName."></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlFabricName" runat="server" CssClass="form-control" Style="height: 30px"
                                        Width="100%" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Shipment Mode:</label>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlShipmentMode"
                                        ValueToCompare="Size" Operator="NotEqual" Type="String" ErrorMessage="Please Select Size."></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlShipmentMode" CssClass="form-control" runat="server" Style="height: 30px"
                                        Width="100%" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Payment Mode:</label>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPaymentMode"
                                        ValueToCompare="Size" Operator="NotEqual" Type="String" ErrorMessage="Please Select Size."></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlPaymentMode" runat="server" Style="height: 30px" Width="100%"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Cutting Ledger
                                    </label>
                                    <asp:CompareValidator ID="CompareValidator12" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" initialvalue="0" ControlToValidate="ddlCuttingLedger"
                                        ValueToCompare="Select Cutting Ledger" Operator="NotEqual" Type="String" ErrorMessage="Please Select Cutting Ledger.">
                                    </asp:CompareValidator>
                                    <asp:DropDownList ID="ddlCuttingLedger" runat="server" CssClass="chzn-select" Style="height: 30px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>
                                        Cut. Date:</label>
                                    <asp:TextBox ID="txtCuttingDate" runat="server" CssClass="form-control" Width="100px"
                                        Enabled="false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtCuttingDate"
                                        PopupButtonID="txtCuttingDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Order Date:</label>
                                    <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" Enabled="false"
                                        Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtOrderDate"
                                        PopupButtonID="txtOrderDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Shipment:</label>
                                    <asp:TextBox ID="txtShipmentDate" runat="server" CssClass="form-control" Enabled="false"
                                        Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtShipmentDate"
                                        PopupButtonID="txtShipmentDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                <div class="form-group">
                                    <label>
                                        From Date:</label>
                                    <asp:TextBox ID="txtCuttingFromDate" runat="server" CssClass="form-control" Width="110px">
                                    </asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" TargetControlID="txtCuttingFromDate"
                                        PopupButtonID="txtCuttingFromDate" EnabledOnClient="true" Format="dd/MM/yyyy"
                                        runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                <div class="form-group">
                                    <label>
                                        To Date:</label>
                                    <asp:TextBox ID="txtCuttingToDate" runat="server" CssClass="form-control" Width="110px">
                                    </asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" TargetControlID="txtCuttingToDate"
                                        PopupButtonID="txtCuttingToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                <div class="form-group" runat="server" visible="false">
                                    <label>
                                        Delivery:</label>
                                    <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="form-control" Enabled="false"
                                        Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDeliveryDate"
                                        PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-7">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFabric" Text="Fabric Details" runat="server" Style="font-weight: bold;
                                    color: Red"></asp:Label>
                                <div class="col-lg-12" runat="server" visible="false">
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>
                                                Items :</label>
                                            <asp:CompareValidator ID="CompareValidator7" runat="server" ValidationGroup="val2"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlItems" ValueToCompare="Select Item"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please Select Items."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlItems" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>
                                                Color :</label>
                                            <asp:CompareValidator ID="CompareValidator9" runat="server" ValidationGroup="val2"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlColor" ValueToCompare="Select Color"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please Select Color."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlColor" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label>
                                                Avl.Stock</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtAvlStock"
                                                ValidationGroup="val2" Text="*" ErrorMessage="Please Enter Avl.Stock." Style="color: Red" />
                                            <asp:TextBox ID="txtAvlStock" Height="30px" Width="100%" runat="server" CssClass="form-control"
                                                Text="0" ReadOnly="true"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                TargetControlID="txtAvlStock" ValidChars="." FilterType="Numbers,Custom" />
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label>
                                                Issue Stock</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtIssueStock"
                                                ValidationGroup="val2" Text="*" ErrorMessage="Please Enter Issue Stock." Style="color: Red" />
                                            <asp:TextBox ID="txtIssueStock" Height="30px" Width="100%" runat="server" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                                TargetControlID="txtIssueStock" ValidChars="." FilterType="Numbers,Custom" />
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                        <br />
                                        <div class="form-group">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="val2" OnClick="btnSubmit_OnClick" />
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ID="GVFabricDetails" runat="server" Width="100%" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" CssClass="myGridStyle1">
                                    <HeaderStyle BackColor="White" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                        NextPageText="Next" PreviousPageText="Previous" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:HiddenField ID="hdItemId" runat="server" Value='<%#Eval("ItemId") %>' />
                                                <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId") %>' />
                                                <asp:HiddenField ID="hdRequiredStock" runat="server" Value='<%#Eval("RequiredStock") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Item" HeaderText="Item" />
                                        <asp:BoundField DataField="Color" HeaderText="Color" />
                                        <asp:TemplateField HeaderText="Avl.Stock" HeaderStyle-Width="45px" ItemStyle-Width="45px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAvlStock" Height="30px" Width="100%" runat="server" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RequiredStock" HeaderText="RequiredStock" DataFormatString="{0:f2}" />
                                        <asp:TemplateField HeaderText="Issue Qty" Visible="false" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtIssueQty" Height="30px" Width="100%" runat="server" Text='<%#Eval("IssueQty") %>'
                                                    CssClass="form-control"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                    TargetControlID="txtIssueQty" ValidChars="." FilterType="Numbers,Custom" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel-body" style="margin-top: 30px; height: 100%">
        <div class="col-lg-12">
            <div class="col-lg-10">
                <asp:GridView ID="GVItem" AutoGenerateColumns="False" runat="server" OnRowCommand="GVItem_OnRowCommand"
                    GridLines="Both" Caption="Style Details">
                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                        Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StyleNo" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                            ItemStyle-Font-Size="Large">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdStyleNoId" runat="server" Value='<%#Eval("StyleNoId")  %>' />
                                <asp:Label ID="lblStyleNo" runat="server" Text='<%#Eval("StyleNo")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-Width="30%" ItemStyle-Width="30%"
                            ItemStyle-Font-Size="Large">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Color" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            ItemStyle-Font-Size="Large">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId")  %>' />
                                <asp:Label ID="lblColor" runat="server" Text='<%#Eval("Color")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                            ItemStyle-Font-Size="Large">
                            <ItemTemplate>
                                <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-Font-Size="Large">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%#Eval("CQty")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size" HeaderStyle-Width="8%" ItemStyle-Width="8%"
                            ItemStyle-Font-Size="Large">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdRangeId" runat="server" Value='<%#Eval("RangeId")  %>' />
                                <asp:Label ID="lblSize" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%"
                            ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                    CommandName="View">
                                    <asp:Image ID="imgView" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-lg-2">
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
                        <asp:TemplateField HeaderText="Size" HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdSize" runat="server" Value='<%#Eval("SizeId") %>' />
                                <asp:Label ID="lblSize" Height="30px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQty" Height="30px" Width="80px" runat="server" Text='<%#Eval("CQty")%>'
                                    ReadOnly="true"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312fff" runat="server"
                                    TargetControlID="txtQty" FilterType="Numbers" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="panel-body" style="background-color: Gray; margin-top: 50px; height: 100%">
        <div class="col-lg-12">
            <div class="col-lg-4">
                <label>
                    Remarks:</label>
                <asp:TextBox ID="txtRemarks" AutoComplete="Off" Height="80px" Width="100%" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        Cutting By</label>
                    <asp:CheckBoxList ID="chkCuttingBy" runat="server">
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        Process
                    </label>
                    <asp:CheckBoxList ID="chkProcess" runat="server" Enabled="false">
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        Finishing Process:</label>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" ValidationGroup="val1"
                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlFinishingProcess"
                        ValueToCompare="FinishingProcess" Operator="NotEqual" Type="String" ErrorMessage="Please Select FinishingProcess."></asp:CompareValidator>
                    <asp:DropDownList ID="ddlFinishingProcess" runat="server" CssClass="chzn-select"
                        Style="height: 30px" Width="100%">
                    </asp:DropDownList>
                </div>
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
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/chosen.min.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
