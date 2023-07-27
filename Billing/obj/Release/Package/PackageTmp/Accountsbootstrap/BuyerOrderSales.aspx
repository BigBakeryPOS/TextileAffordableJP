<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyerOrderSales.aspx.cs"
    Inherits="Billing.Accountsbootstrap.BuyerOrderSales" %>

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
    <title>BuyerOrder Sales </title>
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1"> </asp:Label>
    <form id="Form1" runat="server">
        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
            ID="val1" ShowMessageBox="true" ShowSummary="false" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="row" style="margin-top: -2px">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                        <i class="fa fa-briefcase"></i>
                        <asp:Label ID="lblName" Text="BuyerOrder Sales" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                    <div class="panel-body">
                        <div class="list-group">
                            <div class="col-lg-12">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <br />
                                        <asp:RadioButtonList ID="rdbselect" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbselect_SelectedIdexChanged">
                                            <asp:ListItem Value="1" Selected="True">Direct Sales</asp:ListItem>
                                            <asp:ListItem Value="2">From Exec</asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group">
                                        <label>
                                            Exc Opening Stock from:</label>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select" Style="height: 30px; color: Black"
                                            Width="150px">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCompany"
                                            ValueToCompare="Select" Operator="NotEqual" Type="String" ErrorMessage="Please Select Stock From."></asp:CompareValidator>
                                    </div>



                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Bill. No. :</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtBillNo"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Bill. No." Style="color: Red" />
                                        <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control center-block" AutoPostBack="true" OnTextChanged="txtBillNo_TextChanged" Enabled="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Inv. No. :</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtInvNo"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Inv. No." Style="color: Red" />
                                        <asp:TextBox ID="txtInvNo" runat="server" CssClass="form-control center-block" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Inv. Date:</label>
                                        <asp:TextBox ID="txtInvDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender121" TargetControlID="txtInvDate"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group" runat="server" visible="false">

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

                                    <div class="form-group">
                                        <label>
                                            Party Code:</label>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPartyCode"
                                            ValueToCompare="PartyCode" Operator="NotEqual" Type="String" ErrorMessage="Please Select Party Code."></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlPartyCode" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlPartyCode_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-lg-4">

                                    <div id="divexec" runat="server" visible="false">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtExcNo" runat="server" Width="100%" AutoComplete="off" placeholder="Find StyleNo"
                                                onkeyup="SearchEmployees(this,'#chkExcNo');"></asp:TextBox>
                                            <div style="overflow-y: scroll; height: 100px">
                                                <div class="panel panel-default" style="width: 350px">
                                                    <asp:CheckBoxList ID="chkExcNo" CssClass="chkChoice1" runat="server" RepeatLayout="Table"
                                                        Style="overflow: auto">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-lg-1">
                                                <asp:Button ID="btnSearch" runat="server" class="btn btn-success" Text="Search" OnClick="btnSearch_OnClick"
                                                    Width="100px" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">

                                    <div class="form-group">

                                        <label>
                                            Payment Mode</label>
                                        <asp:DropDownList ID="ddlPayMode" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            AutoPostBack="true" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

                                </div>


                                <div class="col-lg-2">
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
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPayMode"
                                    ValueToCompare="Select Paymode" Operator="NotEqual" Type="String" ErrorMessage="Please Select Pay Code."></asp:CompareValidator>
                                <div class="form-group" runat="server" visible="false">
                                    <label>
                                        GST Type</label>
                                    <asp:DropDownList ID="drpGSTType" AutoPostBack="true" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Exclusive" Selected="True" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Inclusive" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-12">
                        <div class="col-lg-10">
                            <div id="div2" runat="server" style="overflow: auto; height: 250px; width: 100%">
                                <asp:GridView ID="GVItem" AutoGenerateColumns="False" GridLines="Both" runat="server"
                                    Caption="Style Details" Width="100%" CssClass="myGridStyle1" OnRowCommand="GVItem_OnRowCommand">
                                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                        Font-Names="arial" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:HiddenField ID="hdAllID" runat="server" Value='<%#Eval("AllID") %>' />
                                                <asp:HiddenField ID="hdBuyerOrderMasterCuttingId" runat="server" Value='<%#Eval("BuyerOrderMasterCuttingId") %>' />
                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId") %>' />
                                                <asp:HiddenField ID="hdIssueQty" runat="server" Value='<%#Eval("IssueQty") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ExcNo" HeaderText="ExcNo" HeaderStyle-Width="12%" />
                                        <asp:BoundField DataField="StyleNo" HeaderText="StyleNo" HeaderStyle-Width="25%" />
                                        <asp:BoundField DataField="Color" HeaderText="Color" HeaderStyle-Width="25%" />
                                        <asp:BoundField DataField="Range" HeaderText="Range" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="HSNCode" HeaderText="HSNCode" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="TaxID" HeaderText="TaxID" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="Tax" HeaderText="Tax" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" DataFormatString="{0:f0}" />
                                        <asp:BoundField DataField="IssueQty" HeaderText="IssueQty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" DataFormatString="{0:f0}" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" DataFormatString="{0:f2}" />
                                        <asp:TemplateField HeaderText="BeforeTAX" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtBeforeTAX" Height="30px" Width="50px" runat="server" Text='<%#Eval("BeforeTAX")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtCGST" Height="30px" Width="50px" runat="server" Text='<%#Eval("CGST")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtSGST" Height="30px" Width="50px" runat="server" Text='<%#Eval("SGST")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtIGST" Height="30px" Width="50px" runat="server" Text='<%#Eval("IGST")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:Label ID="txtAmount" Height="30px" Width="50px" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" DataFormatString="{0:f2}" />--%>
                                        <asp:TemplateField HeaderText="Assign Qty" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("AllID") %>'
                                                    CommandName="AssignQty">
                                                    <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <asp:Label ID="AllID" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="BuyerOrderMasterCuttingId" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="RowId" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="ExcNo" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="StyleNo" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Color" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Range" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Qty" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="IssueQty" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Rate" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Amount" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="HSNCode" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Tax" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="TaxID" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="CGST" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="SGST" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="IGST" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="BeforeTAX" runat="server" Visible="false"></asp:Label>
                            <asp:GridView ID="GVSizes" AutoGenerateColumns="False" GridLines="Both" runat="server"
                                Caption="Assign Qty Details">
                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                            <asp:HiddenField ID="hdExcStockId" runat="server" Value='<%#Eval("ExcStockId") %>' />
                                            <asp:HiddenField ID="hdStyleId" runat="server" Value='<%#Eval("StyleId") %>' />
                                            <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId") %>' />
                                            <asp:HiddenField ID="hdSizeId" runat="server" Value='<%#Eval("SizeId") %>' />
                                            <asp:HiddenField ID="hdQty" runat="server" Value='<%#Eval("Qty") %>' />
                                            <asp:HiddenField ID="hdBuyerOrderMasterCuttingId" runat="server" Value='<%#Eval("BuyerOrderMasterCuttingId") %>' />
                                            <asp:HiddenField ID="hdRangeId" runat="server" Value='<%#Eval("RangeId") %>' />
                                            <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId") %>' />
                                            <asp:HiddenField ID="hdTransSizeId" runat="server" Value='<%#Eval("TransSizeId") %>' />
                                            <asp:HiddenField ID="hdRate" runat="server" Value='<%#Eval("Rate") %>' />
                                            <asp:HiddenField ID="hdSize" runat="server" Value='<%#Eval("Size") %>' />
                                            <asp:HiddenField ID="hdTaxID" runat="server" Value='<%#Eval("TaxID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Size" DataField="Size" />
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Qty" DataField="Qty" />
                                    <asp:TemplateField HeaderText="IssueQty" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtIssueQty" AutoComplete="Off" Height="30px" Width="50px" runat="server"
                                                Text='<%#Eval("IssueQty")%>'></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3125" runat="server"
                                                TargetControlID="txtIssueQty" FilterType="Numbers" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Button ID="btnSubmitQty" runat="server" Text="Submit Qty" Width="100%" OnClick="btnSubmitQty_OnClick" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="col-lg-12">
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>
                                    CGST</label>
                                <asp:TextBox ID="txtTotCGST" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <label>
                                    SGST</label>
                                <asp:TextBox ID="txtTotSGST" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1">
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
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>
                                    Narration</label>
                                <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="col-lg-12">
                        <div class="col-lg-1">
                            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" ValidationGroup="val1"
                                Style="width: 100px;" OnClick="btnSave_OnClick" OnClientClick="Confirm(this)"
                                UseSubmitBehavior="false" />
                        </div>
                        <div class="col-lg-1">
                            <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" Style="width: 100px;"
                                OnClick="btnExit_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="../js/jquery.min.js" type="text/javascript"></script>
        <script src="../js/chosen.min.js" type="text/javascript"></script>
        <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
