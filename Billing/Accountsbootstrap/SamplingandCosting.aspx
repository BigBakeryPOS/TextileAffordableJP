<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SamplingandCosting.aspx.cs"
    Inherits="Billing.Accountsbootstrap.SamplingandCosting" %>

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
    <title>Sampling & Costing </title>
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
        Visible="false" Text="1"></asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                        <i class="fa fa-briefcase"></i>
                        <asp:Label ID="lblName" Text="Sampling & Costing" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Style="background-color: Orange; height: 16px;
                            vertical-align: top" Enabled="false" /><label>New</label>&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Style="background-color: Green; height: 16px;
                            vertical-align: top" Enabled="false" /><label>Refresh</label>
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
                                            StyleNo As Well as New:</label>
                                        <asp:DropDownList ID="ddlStyles" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlStyles_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Style No:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtStyleNo"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Style No." Style="color: Red" />
                                        <asp:TextBox ID="txtStyleNo" OnTextChanged="style_NO" AutoPostBack="true" runat="server"
                                            AutoComplete="Off" Width="100%" Height="26px">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1151" runat="server"
                                            FilterType="Numbers,UppercaseLetters,LowercaseLetters,Custom" ValidChars=" !@#$%^*(){}_+=.<>/?-"
                                            TargetControlID="txtStyleNo" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Buyer Code:</label>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBuyerCode"
                                            ValueToCompare="BuyerCode" Operator="NotEqual" Type="String" ErrorMessage="Please Select Buyer Code.">
                                        </asp:CompareValidator>
                                        <asp:DropDownList ID="ddlBuyerCode" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlBuyerCode_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Buyer Name:</label>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBuyerName"
                                            ValueToCompare="BuyerName" Operator="NotEqual" Type="String" ErrorMessage="Please Select Buyer Name.">
                                        </asp:CompareValidator>
                                        <asp:DropDownList ID="ddlBuyerName" Enabled="false" runat="server" CssClass="form-control"
                                            Style="height: 30px" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Buyer Print StyleNo:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtBuyerPrintStyle"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Buyer Print StyleNo."
                                            Style="color: Red" />
                                        <asp:TextBox ID="txtBuyerPrintStyle" runat="server" Width="100%" AutoComplete="Off"
                                            Height="26px">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Description:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtDescription"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Description." Style="color: Red" />
                                        <asp:TextBox ID="txtDescription" AutoComplete="Off" runat="server" Width="100%" Height="26px">
                                        </asp:TextBox>
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Size Range:</label>
                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlSize" ValueToCompare="Size"
                                                        Operator="NotEqual" Type="String" ErrorMessage="Please Select Size.">
                                                    </asp:CompareValidator>
                                                    <asp:DropDownList ID="ddlSize" runat="server"  OnSelectedIndexChanged="ddlSize_OnSelectedIndexChanged" AutoPostBack="true" CssClass="form-control"
                                            Style="height: 30px" Width="100%">
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
                                    </table>
                                    <div class="form-group">
                                        <label>
                                            Fabrication Cost:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtFabricationCost"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Fabrication Cost."
                                            Style="color: Red" />
                                        <asp:TextBox ID="txtFabricationCost" runat="server" Width="100%" Height="26px" AutoPostBack="true"
                                            AutoComplete="Off" OnTextChanged="txtFabricationCost_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtFabricationCost" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Embroidery[Machine] Cost:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtEmbroideryMachineCost"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Embroidery[Machine] Cost."
                                            Style="color: Red" />
                                        <asp:TextBox ID="txtEmbroideryMachineCost" runat="server" Width="100%" Height="26px"
                                            AutoComplete="Off" AutoPostBack="true" OnTextChanged="txtEmbroideryMachineCost_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtEmbroideryMachineCost" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Embroidery[Hand]Cost:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtEmbroideryHandCost"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Embroidery[Hand]Cost."
                                            Style="color: Red" />
                                        <asp:TextBox ID="txtEmbroideryHandCost" runat="server" Width="100%" Height="26px"
                                            AutoComplete="Off" AutoPostBack="true" OnTextChanged="txtEmbroideryHandCost_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtEmbroideryHandCost" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Piece Process Cost:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtPieceProcessCost"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Piece Process Cost."
                                            Style="color: Red" />
                                        <asp:TextBox ID="txtPieceProcessCost" runat="server" Width="100%" Height="26px" AutoPostBack="true"
                                            AutoComplete="Off" OnTextChanged="txtPieceProcessCost_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtPieceProcessCost" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Finishing and Packing Cost:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtFinishingandPackingCost"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Finishing and Packing Cost."
                                            Style="color: Red" />
                                        <asp:TextBox ID="txtFinishingandPackingCost" runat="server" Width="100%" Height="26px"
                                            AutoComplete="Off" AutoPostBack="true" OnTextChanged="txtFinishingandPackingCost_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtFinishingandPackingCost" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Logistics Cost:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator20" ControlToValidate="txtLogisticsCost"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Logistics Cost." Style="color: Red" />
                                        <asp:TextBox ID="txtLogisticsCost" runat="server" Width="100%" Height="26px" AutoComplete="Off"
                                            AutoPostBack="true" OnTextChanged="txtLogisticsCost_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtLogisticsCost" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Model Photo
                                        </label>
                                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                            <ContentTemplate>
                                                <asp:FileUpload ID="fp_Upload" runat="server" />
                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-warning"
                                                    OnClick="btnUpload_OnClick" Width="100px" />
                                                <asp:Image ID="img_Photo" runat="server" Width="100px" BorderColor="1" />
                                                <asp:Label ID="lblFile_Path" runat="server" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpload" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <br />
                                        <br />
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Date:</label>
                                        <asp:TextBox ID="txtDate" AutoComplete="Off" runat="server" CssClass="form-control center-block">
                                        </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" PopupButtonID="txtFromDate"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Rejection:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="txtRejection"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Rejection." Style="color: Red" />
                                        <asp:TextBox ID="txtRejection" runat="server" Width="100%" Height="26px" AutoPostBack="true"
                                            AutoComplete="Off" OnTextChanged="txtRejection_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender113" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtRejection" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Extra Margin:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="txtExtraMargin"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Extra Margin." Style="color: Red" />
                                        <asp:TextBox ID="txtExtraMargin" runat="server" Width="100%" Height="26px" AutoPostBack="true"
                                            AutoComplete="Off" OnTextChanged="txtExtraMargin_OnTextChanged">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender114" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtExtraMargin" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            SmpCost Per Piece:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="txtSmpCostPerPiece"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Cost Per Piece." Style="color: Red" />
                                        <asp:TextBox ID="txtSmpCostPerPiece" runat="server" ReadOnly="true" Width="100%"
                                            AutoComplete="Off" Height="26px">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender115" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtSmpCostPerPiece" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            PrdCost Per Piece:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator19" ControlToValidate="txtPrdCostPerPiece"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Cost Per Piece." Style="color: Red" />
                                        <asp:TextBox ID="txtPrdCostPerPiece" runat="server" ReadOnly="true" Width="100%"
                                            AutoComplete="Off" Height="26px">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtPrdCostPerPiece" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Total SmpCost [INR]:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="txtTotalSmpCostINR"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Total Cost [INR]."
                                            Style="color: Red" />
                                        <asp:TextBox ID="txtTotalSmpCostINR" runat="server" ReadOnly="true" Width="100%"
                                            AutoComplete="Off" Height="26px">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender112" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtTotalSmpCostINR" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Total PrdCost [INR]:</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtTotalPrdCostINR"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Total Cost [INR]."
                                            Style="color: Red" />
                                        <asp:TextBox ID="txtTotalPrdCostINR" runat="server" ReadOnly="true" Width="100%"
                                            AutoComplete="Off" Height="26px">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtTotalPrdCostINR" />
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Cost in:</label>
                                                    <asp:DropDownList ID="ddlCostCurrency" runat="server" CssClass="form-control" Style="height: 30px"
                                                        Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlCostCurrency_OnSelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <br />
                                                    <asp:Button ID="btnCostCurrency" runat="server" OnClick="btnCostCurrency_OnClick"
                                                        Style="background-color: Orange; height: 16px; vertical-align: top" />
                                                    <asp:Button ID="btnCostCurrencyRef" runat="server" OnClick="btnCostCurrencyRef_OnClick"
                                                        Style="background-color: Green; height: 16px; vertical-align: top" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Sample Cost in:</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="txtTotalSmpCost"
                                                        ValidationGroup="val1" Text="*" ErrorMessage="Please Check Total Cost in." Style="color: Red" />
                                                    <asp:TextBox ID="txtTotalSmpCost" ReadOnly="true" runat="server" Width="100%" Height="30px">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Production Cost in:</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" ControlToValidate="txtTotalPrdCost"
                                                        ValidationGroup="val1" Text="*" ErrorMessage="Please Check Total Cost in." Style="color: Red" />
                                                    <asp:TextBox ID="txtTotalPrdCost" ReadOnly="true" runat="server" Width="100%" Height="30px">
                                                    </asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        GST
                                                    </label>
                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddltax" ValueToCompare="Select GST"
                                                        Operator="NotEqual" Type="String" ErrorMessage="Please Select GST"></asp:CompareValidator>
                                                    <asp:DropDownList CssClass="form-control" ID="ddltax" runat="server">

                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator21" ControlToValidate="ddltax"
                                                        ValidationGroup="val1" Text="*" ErrorMessage="Please Select GST." Style="color: Red" />

                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        HSNCode:</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator213" ControlToValidate="txtHSNCode"
                                                        ValidationGroup="val1" Text="*" ErrorMessage="Please Enter HSN Code." Style="color: Red" />
                                                    <asp:TextBox ID="txtHSNCode" runat="server" Width="100%" AutoComplete="Off" Height="26px">
                                                    </asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" ValidationGroup="val1"
                                                    Style="width: 90px;" OnClick="btnSave_OnClick" />
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" Style="width: 90px;"
                                                    OnClick="btnExit_OnClick" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-8">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblItemDetails" runat="server" Style="color: Red"></asp:Label>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="col-lg-5">
                                            <div class="form-group">
                                                <label>
                                                    Items :</label>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val2"
                                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlItems" ValueToCompare="Select Item"
                                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Items.">
                                                </asp:CompareValidator>
                                                <asp:DropDownList ID="ddlItems" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                    Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlItems_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label>
                                                    Rate</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtRate"
                                                    ValidationGroup="val2" Text="*" ErrorMessage="Please Enter Rate." Style="color: Red" />
                                                <asp:TextBox ID="txtRate" Height="30px" Width="100%" runat="server" AutoComplete="Off"
                                                    CssClass="form-control">
                                                </asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                    TargetControlID="txtRate" ValidChars="." FilterType="Numbers,Custom" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label>
                                                    Smp.Avg/Qty</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtSmpAvg"
                                                    ValidationGroup="val2" Text="*" ErrorMessage="Please Enter Smp.Avg//Qty" Style="color: Red" />
                                                <asp:TextBox ID="txtSmpAvg" Height="30px" AutoComplete="Off" Width="100%" runat="server"
                                                    CssClass="form-control">
                                                </asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                                    TargetControlID="txtSmpAvg" ValidChars="." FilterType="Numbers,Custom" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label>
                                                    Prd.Avg/Qty</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" ControlToValidate="txtPrdAvg"
                                                    ValidationGroup="val2" Text="*" ErrorMessage="Please Enter Prd.Avg/Qty" Style="color: Red" />
                                                <asp:TextBox ID="txtPrdAvg" Height="30px" AutoComplete="Off" Width="100%" runat="server"
                                                    CssClass="form-control">
                                                </asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                    TargetControlID="txtPrdAvg" ValidChars="." FilterType="Numbers,Custom" />
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <br />
                                            <div class="form-group">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="val2" OnClick="btnSubmit_OnClick" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'GVdsItemCodeDescriptionItemType')"
                                        ID="txtsearch" runat="server" placeholder="Enter Text to Search" Width="200px"
                                        AutoCompleteType="Disabled" Visible="false">
                                    </asp:TextBox>
                                    <div id="Div3">
                                        <asp:HiddenField ID="hdRowIndex" runat="server" />
                                        <asp:GridView ID="GVdsItemCodeDescriptionItemType" runat="server" OnRowCommand="GVdsItemCodeDescriptionItemType_RowCommand"
                                            Width="100%" EmptyDataText="No Records Found" AutoGenerateColumns="false" CssClass="myGridStyle1"
                                            OnRowDeleting="GVdsItemCodeDescriptionItemType_RowDeleting" OnRowDataBound="GVdsItemCodeDescriptionItemType_RowDataBound"
                                            OnRowEditing="OnRowEditing">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                        <asp:HiddenField ID="hdItemId" runat="server" Value='<%#Eval("ItemId") %>' />
                                                        <asp:HiddenField ID="hdItem" runat="server" Value='<%#Eval("Item") %>' />
                                                        <asp:HiddenField ID="hdRate" runat="server" Value='<%#Eval("Rate") %>' />
                                                        <asp:HiddenField ID="hdSmpAvg" runat="server" Value='<%#Eval("SmpAvg") %>' />
                                                        <asp:HiddenField ID="hdPrdAvg" runat="server" Value='<%#Eval("PrdAvg") %>' />
                                                        <asp:HiddenField ID="hdSmpCost" runat="server" Value='<%#Eval("SmpCost") %>' />
                                                        <asp:HiddenField ID="hdPrdCost" runat="server" Value='<%#Eval("PrdCost") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Item" HeaderText="Item" />
                                                <asp:BoundField DataField="Rate" HeaderText="Rate" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="SmpAvg" HeaderText="SmpAvg/Qty" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="PrdAvg" HeaderText="PrdAvg/Qty" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="SmpCost" HeaderText="SmpCost" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="PrdCost" HeaderText="PrdCost" DataFormatString="{0:f2}"
                                                    ItemStyle-HorizontalAlign="Right" />
                                                <asp:TemplateField HeaderText="Color" HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlColor" Height="30px" Width="150px" runat="server" CssClass="chzn-select">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ControlStyle-Width="100%" ShowDeleteButton="True" ButtonType="Button" />
                                                <asp:CommandField ControlStyle-Width="100%" ShowEditButton="true" ButtonType="Button" />
                                                <%--<asp:Button Text="Select" runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />--%>
                                            </Columns>
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnAddItems" runat="server" OnClick="btnAddItems_OnClick" Text="ADD ITEMS" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b style="color: Silver; font-size: large">Item
                                        Smp Cost : </b>
                                    <asp:Label ID="lblItemSmpCost" runat="server" Text="0" Style="color: Silver; font-weight: bold;
                                        font-size: larger"></asp:Label>
                                    <b style="color: Silver; font-size: large">Item Prd Cost : </b>
                                    <asp:Label ID="lblItemPrdCost" runat="server" Text="0" Style="color: Silver; font-weight: bold;
                                        font-size: larger"></asp:Label>
                                    <asp:GridView ID="GVItem" AutoGenerateColumns="False" CssClass="myGridStyle1" runat="server"
                                        OnRowDeleting="GVItem_RowDeleting">
                                        <HeaderStyle BackColor="White" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                    <asp:HiddenField ID="hdItemId" runat="server" Value='<%#Eval("ItemId") %>' />
                                                    <asp:HiddenField ID="hdItem" runat="server" Value='<%#Eval("Item") %>' />
                                                    <asp:HiddenField ID="hdRate" runat="server" Value='<%#Eval("Rate") %>' />
                                                    <asp:HiddenField ID="hdSmpAvg" runat="server" Value='<%#Eval("SmpAvg") %>' />
                                                    <asp:HiddenField ID="hdPrdAvg" runat="server" Value='<%#Eval("PrdAvg") %>' />
                                                    <asp:HiddenField ID="hdSmpCost" runat="server" Value='<%#Eval("SmpCost") %>' />
                                                    <asp:HiddenField ID="hdPrdCost" runat="server" Value='<%#Eval("PrdCost") %>' />
                                                    <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Item" HeaderText="Item" />
                                            <asp:BoundField DataField="Rate" HeaderText="Rate" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="SmpAvg" HeaderText="SmpAvg/Qty" DataFormatString="{0:f2}"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="PrdAvg" HeaderText="PrdAvg/Qty" DataFormatString="{0:f2}"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="SmpCost" HeaderText="SmpCost" DataFormatString="{0:f2}"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="PrdCost" HeaderText="PrdCost" DataFormatString="{0:f2}"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Color" HeaderText="Color" />
                                            <asp:CommandField ControlStyle-Width="100%" ShowDeleteButton="True" ButtonType="Button" />
                                        </Columns>
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <br />
                                    <br />
                                    <div id="Div1">
                                        <asp:Button ID="Button3" runat="server" OnClick="btnsupplierdetails_OnClick" Text="Get Suggested Supplier Details" />
                                        <label>
                                            Supplier Details</label>
                                        <asp:GridView ID="Gridsupplierdetails" AutoGenerateColumns="False" CssClass="myGridStyle1"
                                            Width="100%" OnRowDataBound="Grid_Supplier" runat="server">
                                            <HeaderStyle BackColor="White" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:HiddenField ID="hdItemId" runat="server" Value='<%#Eval("ItemId") %>' />
                                                        <asp:HiddenField ID="hdItem" runat="server" Value='<%#Eval("Item") %>' />
                                                        <asp:HiddenField ID="hdRate" runat="server" Value='<%#Eval("Rate") %>' />
                                                        <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Item" HeaderText="Item" />
                                                <asp:BoundField DataField="Rate" HeaderText="Rate" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="Color" HeaderText="Color" />
                                                <asp:TemplateField HeaderText="Party Type" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlPartyType" runat="server" OnSelectedIndexChanged="Party_chnaged"
                                                            AutoPostBack="true" class="form-control">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Suggested Supplier" HeaderStyle-Width="25%">
                                                    <ItemTemplate>
                                                        <div style="padding-left: 10px; overflow-y: scroll;">
                                                            <asp:CheckBoxList ID="chksupplierlist" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        <asp:TextBox ID="TextBox2" Width="100%" Visible="false" runat="server" onkeyup="SearchEmployees(this,'#chksupplierlist');"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <asp:Button ID="btnpcsprocess" runat="server" Text=">>->>" OnClick="Pcs_process_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Selected Suppliers" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="selectedsupplierlist" runat="server" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                        <asp:Label ID="selectedsupplierlistID" Visible="false" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <br />
                                    <div>
                                        <div runat="server" id="divpcsprocess" visible="true" class="col-lg-12">
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>
                                                        All Pcs Process</label>
                                                    <div style="padding-left: 10px; overflow-y: scroll;">
                                                        <asp:CheckBoxList ID="chkpcsprocess" runat="server">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <asp:Button ID="btnpcsprocess" runat="server" Text=">>" OnClick="Pcsnew_process_Click" />
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>
                                                        selected Pcs Process</label>
                                                    <asp:GridView ID="gvPcsProcessDetails" CssClass="myGridStyle1" Width="100%" runat="server"
                                                        AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Pcs Process Details">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPname" Text='<%#  Eval("Pname") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblpid" Visible="false" Text='<%#  Eval("Pid") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                   </div>

                                         <div class="col-lg-3">
                                            <asp:GridView ID="GVSizes" AutoGenerateColumns="False" GridLines="Both" runat="server">
                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
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
                                                            <asp:Label ID="lblSize" Height="30px" Width="100px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQty" Height="30px" Width="100%" runat="server" Text='<%#Eval("Rate","{0:n}")%>'></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312fff" runat="server"
                                                                TargetControlID="txtQty" FilterType="Custom,Numbers" ValidChars="." />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>



                                   

                               
                            </div>
                        </div>
                    </div>
                </div>
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
