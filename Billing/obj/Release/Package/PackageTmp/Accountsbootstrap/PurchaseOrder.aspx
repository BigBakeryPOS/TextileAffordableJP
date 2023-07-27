<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" 
    Inherits="Billing.Accountsbootstrap.PurchaseOrder" %>

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
    <title>Purchase Order </title>
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
    <style type="text/css">
        .zoomed-element
        {
            zoom: 1.8;
        }
    </style>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"></asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false">
    </asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1,5"></asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row" style="margin-top: -10px">
        <div class="col-lg-12">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                        <i class="fa fa-briefcase"></i>
                        <asp:Label ID="lblName" Text="Purchase Order" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblsuffexname" runat="server" Text="PO " ></asp:Label>
                        
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
                                            Process On :</label>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlProcessOn"
                                            ValueToCompare="ProcessOn" Operator="NotEqual" Type="String" ErrorMessage="Please Select ProcessOn.">
                                        </asp:CompareValidator>
                                        <asp:DropDownList ID="ddlProcessOn" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlProcessOn_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Order Date:</label>
                                        <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control center-block">
                                        </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender12" TargetControlID="txtOrderDate"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                     <div class="form-group">
                                        <label>
                                            CompanyName:</label>
                                        <asp:CompareValidator ID="CompareValidator11" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCompany" ValueToCompare="CompanyName"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please Select CompanyName."></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Date From:</label>
                                        <asp:TextBox ID="txtDeliveryFrom" runat="server" CssClass="form-control">
                                        </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender13" TargetControlID="txtDeliveryFrom"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Date To:</label>
                                        <asp:TextBox ID="txtDeliveryTo" runat="server" CssClass="form-control">
                                        </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender14" TargetControlID="txtDeliveryTo"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Purchase For :</label>
                                        <asp:DropDownList ID="ddlPurchaseFor" runat="server" CssClass="chzn-select" Width="100%"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlPurchaseFor_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Delivery Place :</label>
                                        <asp:TextBox ID="txtDeliveryPlace" runat="server" AutoComplete="Off"  CssClass="form-control center-block">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Pro.Ord. No. :</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtProOrderNo"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter PoNo." Style="color: Red" />
                                        <asp:TextBox ID="txtProOrderNo" runat="server" CssClass="form-control center-block"
                                            ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group" id="BuyerExcNo" runat="server" visible="true">
                                        <label>
                                            Exc.No :</label>
                                        <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlExcNo" ValueToCompare="ExcNo"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please Select Exc.No.">
                                        </asp:CompareValidator>
                                        <asp:DropDownList ID="ddlExcNo" runat="server" CssClass="chzn-select" Width="100%"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlExcNo_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group" id="BuyerCode" runat="server" visible="false">
                                        <label>
                                            BuyerCode :</label>
                                        <asp:CompareValidator ID="CompareValidator7" runat="server" ValidationGroup="val2"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBuyerCode"
                                            ValueToCompare="BuyerCode" Operator="NotEqual" Type="String" ErrorMessage="Please Select BuyerCode.">
                                        </asp:CompareValidator>
                                        <asp:DropDownList ID="ddlBuyerCode" runat="server" CssClass="chzn-select" Width="100%"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlBuyerCode_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Party Code:</label>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPartyCode"
                                            ValueToCompare="PartyCode" Operator="NotEqual" Type="String" ErrorMessage="Please Select Party Code.">
                                        </asp:CompareValidator>
                                        <asp:DropDownList ID="ddlPartyCode" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlPartyCode_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Party Name:</label>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPartyName"
                                            ValueToCompare="PartyName" Operator="NotEqual" Type="String" ErrorMessage="Please Select Party Name.">
                                        </asp:CompareValidator>
                                        <asp:DropDownList ID="ddlPartyName" Enabled="false" runat="server" CssClass="chzn-select"
                                            Style="height: 30px" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Buyer Name:</label>
                                        <asp:TextBox ID="txtBuyerName" runat="server" CssClass="form-control center-block"
                                            ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Cont.Person :</label>
                                        <asp:TextBox ID="txtContPerson" runat="server" AutoComplete="Off" CssClass="form-control center-block">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Address :</label>
                                        <asp:TextBox ID="txtAddress" runat="server" AutoComplete="Off" CssClass="form-control center-block">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group" id="BuyerShipment" runat="server" visible="true">
                                        <label>
                                            Shipment Date:</label>
                                        <asp:TextBox ID="txtShipmentDate" runat="server" CssClass="form-control center-block"
                                            ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group" id="StockCity" runat="server" visible="false">
                                        <label>
                                            City:</label>
                                        <asp:TextBox ID="txtBuyerCity" runat="server" AutoComplete="Off" CssClass="form-control center-block"
                                            ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Phone :</label>
                                        <asp:TextBox ID="txtPhone" AutoComplete="Off" runat="server" CssClass="form-control center-block">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            City :</label>
                                        <asp:TextBox ID="txtCity" AutoComplete="Off" runat="server" CssClass="form-control center-block">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            TotalAmount :</label>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control center-block"
                                            ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            Items :</label>
                                        <%--<asp:CompareValidator ID="CompareValidator4" runat="server" 
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlIssueItems"
                                            ValueToCompare="Select Issue Item" Operator="NotEqual" Type="String" ErrorMessage="Please Select Issue Items.">
                                        </asp:CompareValidator>--%>
                                        <asp:DropDownList ID="ddlIssueItems" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlIssueItems_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Color Items :</label>
                                        <%--<asp:CompareValidator ID="CompareValidator10" runat="server" 
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlColor" ValueToCompare="Select Color"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please Select Color.">
                                        </asp:CompareValidator>--%>
                                        <asp:DropDownList ID="ddlColor" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <label>
                                            Qty</label>
                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtQty"
                                             Text="*" ErrorMessage="Please Enter Qty." Style="color: Red" />--%>
                                        <asp:TextBox ID="txtQty" Height="30px" Width="100%" runat="server" AutoPostBack="true" AutoComplete="Off"
                                            OnTextChanged="txtQty_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                            TargetControlID="txtQty" ValidChars="." FilterType="Numbers,Custom" />
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <label>
                                            Shrink</label>
                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtShrink"
                                             Text="*" ErrorMessage="Please Enter Shrink." Style="color: Red" />--%>
                                        <asp:TextBox ID="txtShrink" Height="30px" Width="100%" runat="server" AutoPostBack="true" AutoComplete="Off"
                                            OnTextChanged="txtShrink_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            TargetControlID="txtShrink" ValidChars="." FilterType="Numbers,Custom" />
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <label>
                                            Ttl Qty</label>
                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtTotalQty"
                                             Text="*" ErrorMessage="Please Enter Ttl Qty." Style="color: Red" />--%>
                                        <asp:TextBox ID="txtTotalQty" Height="30px" Width="100%" runat="server" ReadOnly="true">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            TargetControlID="txtTotalQty" ValidChars="." FilterType="Numbers,Custom" />
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <label>
                                            Rate</label>
                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtRate"
                                             Text="*" ErrorMessage="Please Enter Rate." Style="color: Red" />--%>
                                        <asp:TextBox ID="txtRate" Height="30px" Width="100%" runat="server" AutoPostBack="true" AutoComplete="Off"
                                            OnTextChanged="txtRate_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            TargetControlID="txtRate" ValidChars="." FilterType="Numbers,Custom" />
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <label>
                                            Amount</label>
                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtAmount"
                                             Text="*" ErrorMessage="Please Enter Amount." Style="color: Red" />--%>
                                        <asp:TextBox ID="txtAmount" Height="30px" Width="100%" runat="server" ReadOnly="true">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            TargetControlID="txtAmount" ValidChars="." FilterType="Numbers,Custom" />
                                    </div>
                                </div>
                                <div class="col-lg-1" runat="server" visible="false">
                                    <br />
                                    <asp:CheckBox ID="chkReq" runat="server" Text="Req." />
                                    <asp:CheckBox ID="chkRec" runat="server" Text="Rec." />
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="val2" OnClick="btnSubmit_OnClick" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="col-lg-3">
                                    <asp:Label ID="lblIssue1" runat="server" Style="color: Red"></asp:Label><br />
                                    <asp:Label ID="lblIssue2" runat="server" Style="color: Red"></asp:Label>
                                </div>
                                <div class="col-lg-5">
                                </div>
                                <div class="col-lg-4">
                                    <label>
                                        Remarks</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtRemarks" AutoComplete="Off" TextMode="MultiLine" Height="30px" Width="100%" runat="server">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-12">
        <div id="div1" runat="server" style="overflow: auto; height: 150px; width: 100%">
        <asp:HiddenField ID="hdRowIndex" runat="server" />
            <asp:GridView ID="GVItem" AutoGenerateColumns="False" GridLines="Both" runat="server" OnRowEditing="OnRowEditing" OnRowCommand="GVItem_RowCommand"
                OnRowDeleting="GVItem_RowDeleting">
                <headerstyle backcolor="#59d3b4" borderstyle="Solid" borderwidth="1px" bordercolor="Gray"
                    font-names="arial" font-size="Smaller"  />
                <rowstyle borderstyle="Solid" borderwidth="0.5px" bordercolor="Gray" />
                <columns>
                    <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                            <asp:HiddenField ID="hdPurchaseForId" runat="server" Value='<%#Eval("PurchaseForId") %>' />
                            <asp:HiddenField ID="hdPurchaseForTypeId" runat="server" Value='<%#Eval("PurchaseForTypeId") %>' />
                            <asp:HiddenField ID="hdItemId" runat="server" Value='<%#Eval("ItemId") %>' />
                            <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId") %>' />
                            <asp:HiddenField ID="hdQty" runat="server" Value='<%#Eval("Qty") %>' />
                            <asp:HiddenField ID="hdShrink" runat="server" Value='<%#Eval("Shrink") %>' />
                            <asp:HiddenField ID="hdTotalQty" runat="server" Value='<%#Eval("TotalQty") %>' />
                            <asp:HiddenField ID="hdRate" runat="server" Value='<%#Eval("Rate") %>' />
                            <asp:HiddenField ID="hdAmount" runat="server" Value='<%#Eval("Amount") %>' />
                            <asp:HiddenField ID="hdRemarks" runat="server" Value='<%#Eval("Remarks") %>' />
                            <asp:HiddenField ID="hdIsRequest" runat="server" Value='<%#Eval("IsRequest") %>' />
                            <asp:HiddenField ID="hdIsReceive" runat="server" Value='<%#Eval("IsReceive") %>' />
                            <asp:HiddenField ID="hdRecQty" runat="server" Value='<%#Eval("RecQty") %>' />
                            <asp:HiddenField ID="hditem" runat="server" Value='<%#Eval("Item") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PurchaseForType" HeaderText="Pro.Against" />
                    <asp:BoundField DataField="Item" HeaderText="Item" />
                    <asp:BoundField DataField="Color" HeaderText="Color" />
                    <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Shrink" HeaderText="Shrink" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalQty" HeaderText="Iss.Qty" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Rate" HeaderText="Rate" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="RecQty" HeaderText="Rec.Qty" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" />
                    <asp:CommandField ControlStyle-Width="100%" ShowEditButton="true" ButtonType="Button" />
                    <asp:CommandField ControlStyle-Width="100%" ShowDeleteButton="True" ButtonType="Button" />
                </columns>
            </asp:GridView>
        </div>
    </div>
    <div class="col-lg-12">
        <div class="col-lg-8">
        </div>
         <div class="col-lg-2">
            <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" ValidationGroup="va31"
                 PostBackUrl="~/Accountsbootstrap/PurchaseOrderGrid.aspx" />
        </div>
        <div class="col-lg-2">
            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" ValidationGroup="val1"
                Style="width: 90px;" OnClick="btnSave_OnClick"  />
        </div>
       
    </div>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/chosen.min.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
