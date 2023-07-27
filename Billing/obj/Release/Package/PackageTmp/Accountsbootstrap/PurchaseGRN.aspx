<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseGRN.aspx.cs" Inherits="Billing.Accountsbootstrap.PurchaseGRN" %>

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
    <title>Purchase GRN </title>
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
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1,5"> </asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row" style="margin-top: -10px">
        <div class="col-lg-12">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                        <i class="fa fa-briefcase"></i>
                        <asp:Label ID="lblName" Text="Purchase GRN" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                            Party Name:</label>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPartyName"
                                            ValueToCompare="PartyName" Operator="NotEqual" Type="String" ErrorMessage="Please Select Party Name."></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlPartyName" Enabled="true" runat="server" CssClass="chzn-select"
                                            OnSelectedIndexChanged="Party_Click_chnaged" AutoPostBack="true" Style="height: 30px"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Pro.PO.No.:</label>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPoNo" ValueToCompare="PONo"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please Select Pro.PO.No."></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlPoNo" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlPoNo_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Process On :</label>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlProcessOn"
                                            ValueToCompare="ProcessOn" Operator="NotEqual" Type="String" ErrorMessage="Please Select ProcessOn."></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlProcessOn" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <label>
                                            Rec Date:</label>
                                        <asp:TextBox ID="txtRecDate" runat="server" Width="110px" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender121" TargetControlID="txtRecDate"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Order Date:</label>
                                        <asp:TextBox ID="txtOrderDate" runat="server" Width="110px" CssClass="form-control center-block"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender12" TargetControlID="txtOrderDate"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Challan No:</label>
                                        <asp:TextBox ID="txtChallanNo" AutoComplete="Off" runat="server" CssClass="form-control"
                                            Width="110px">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <label>
                                            Date From:</label>
                                        <asp:TextBox ID="txtDeliveryFrom" runat="server" Width="110px" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender13" TargetControlID="txtDeliveryFrom"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Date To:</label>
                                        <asp:TextBox ID="txtDeliveryTo" runat="server" Width="110px" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender14" TargetControlID="txtDeliveryTo"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            GST Type</label>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpGSTType" AutoPostBack="true" runat="server" CssClass="form-control"
                                                    OnSelectedIndexChanged="drpGSTType_SelectedIndexChanged">
                                                    <asp:ListItem Text="Exclusive" Selected="True" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Inclusive" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Delivery Place :</label>
                                        <asp:TextBox ID="txtDeliveryPlace" runat="server" AutoComplete="Off" CssClass="form-control center-block"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Rec. No. :</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtRecNo"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Rec. No." Style="color: Red" />
                                        <asp:TextBox ID="txtRecNo" runat="server" CssClass="form-control center-block" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div id="Div3" class="form-group" runat="server" visible="true">
                                        <label>
                                            Province</label>
                                        <asp:CompareValidator ID="CompareValidator7" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlProvince"
                                            ValueToCompare="0" Operator="NotEqual" Type="String" ErrorMessage="Select Province type"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlProvince" AutoPostBack="false" Style="font-weight: bold"
                                            runat="server" CssClass="form-control" Enabled="false">
                                            <asp:ListItem Text="Select Province type" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Inner" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Outer" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Party Code:</label>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPartyCode"
                                            ValueToCompare="PartyCode" Operator="NotEqual" Type="String" ErrorMessage="Please Select Party Code."></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlPartyCode" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlPartyCode_OnSelectedIndexChanged"
                                            Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Payment Mode</label>
                                              <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPayMode" ValueToCompare="Select Paymode"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please Select Paymode"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlPayMode" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            AutoPostBack="true" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:UpdatePanel ID="po" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div id="Div7" runat="server" visible="false" class="form-group">
                                                    <label id="Label2" runat="server">
                                                        Bank Name</label>
                                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div id="Div17" runat="server" visible="false" class="form-group">
                                                    <label>
                                                        Card/Cheque/NEFT/RTGS No</label>
                                                    <asp:TextBox CssClass="form-control" ID="txtCheque" runat="server">0</asp:TextBox>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Cont.Person :</label>
                                        <asp:TextBox ID="txtContPerson" AutoComplete="Off" runat="server" CssClass="form-control center-block"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Address :</label>
                                        <asp:TextBox ID="txtAddress" AutoComplete="Off" runat="server" CssClass="form-control center-block"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            CompanyName:</label>
                                        <asp:CompareValidator ID="CompareValidator9" runat="server" ValidationGroup="val1"
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
                                            Phone :</label>
                                        <asp:TextBox ID="txtPhone" AutoComplete="Off" runat="server" CssClass="form-control center-block"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            City :</label>
                                        <asp:TextBox ID="txtCity" AutoComplete="Off" runat="server" CssClass="form-control center-block"></asp:TextBox>
                                    </div>
                                    <div id="Div1" class="form-group" runat="server" visible="false">
                                        <label>
                                            TotalAmount :</label>
                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtTotalAmount"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Check TotalAmount." Style="color: Red" />--%>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control center-block"
                                            ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-lg-12">
                <div id="div2" runat="server" style="overflow: auto; height: 150px; width: 100%">
                    <asp:GridView ID="GVItem" AutoGenerateColumns="False" GridLines="Both" runat="server"
                        Width="100%" CssClass="myGridStyle1" OnRowDeleting="GVItem_RowDeleting">
                        <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                            Font-Names="arial" Font-Size="18px" HorizontalAlign="Center" />
                        <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" Font-Bold="true"
                            Font-Size="15px" />
                        <Columns>
                            <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:HiddenField ID="hdTransId" runat="server" Value='<%#Eval("TransId") %>' />
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
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PurchaseForType" HeaderText="Pro.Against" HeaderStyle-Width="12%" />
                            <asp:BoundField DataField="Item" HeaderText="Item" HeaderStyle-Width="25%" />
                            <asp:BoundField DataField="Color" HeaderText="Color" HeaderStyle-Width="10%" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" DataFormatString="{0:f2}" />
                            <asp:BoundField DataField="Shrink" HeaderText="Shrink" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" DataFormatString="{0:f2}" />
                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" DataFormatString="{0:f2}" />
                            <asp:BoundField DataField="rQty" HeaderText="Tot.Rec.Qty" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" DataFormatString="{0:f2}" />
                            <asp:BoundField DataField="BalQty" HeaderText="Bal.Qty" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" DataFormatString="{0:f2}" />
                            <asp:TemplateField HeaderText="Tax" HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:Label ID="txtTax" runat="server" Text='<%#Eval("Tax") %>' Width="120px"></asp:Label>
                                    <asp:Label ID="txtTaxID" runat="server" Text='<%#Eval("TaxID") %>' Width="120px"
                                        Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtRate" runat="server" Text='<%#Eval("Rate") %>' Width="120px"
                                AutoPostBack="true" OnTextChanged="txtRate_OnTextChanged"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtRate" runat="server" Text='<%#Eval("Rate") %>' Width="120px"
                                        AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312rate" runat="server"
                                        TargetControlID="txtRate" ValidChars="." FilterType="Numbers,Custom" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rec Qty  " HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("RecQty") %>' Width="120px"
                                        AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                        TargetControlID="txtQty" ValidChars="." FilterType="Numbers,Custom" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="40px">
                                <ItemTemplate>
                                    <asp:Label ID="txtTotAmount" runat="server" Text='<%#Eval("TotAmount") %>' Width="120px"></asp:Label>
                                    <asp:Label ID="txtAmount" runat="server" Text='<%#Eval("BeforeTAX") %>' Width="120px" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks ">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="100%"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ControlStyle-Width="100%" ShowDeleteButton="True" ButtonType="Button"
                                Visible="false" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br />
            <br />
            <div class="col-lg-12">
                <div class="col-lg-2">
                    <div class="form-group">
                        <label>
                            CGST</label>
                        <asp:TextBox ID="txtTotCGST" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class="form-group">
                        <label>
                            SGST</label>
                        <asp:TextBox ID="txtTotSGST" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class="form-group">
                        <label>
                            IGST</label>
                        <asp:TextBox ID="txtTotIGST" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class="form-group">
                        <label>
                            BeforeTAX Total</label>
                        <asp:TextBox ID="txtTotBeforeTAX" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class="form-group">
                        <label>
                            Grand Total</label>
                        <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class="form-group">
                        <label>
                            Roundoff</label>
                        <asp:TextBox ID="txtRoundoff" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-lg-12">
        <div class="col-lg-10">
        </div>
        <div class="col-lg-1">
            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" ValidationGroup="val1"
                Style="width: 90px;" OnClick="btnSave_OnClick" OnClientClick="Confirm(this)"
                UseSubmitBehavior="false" />
        </div>
        <div class="col-lg-1">
            <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" Style="width: 90px;"
                OnClick="btnExit_OnClick" />
        </div>
    </div>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/chosen.min.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
