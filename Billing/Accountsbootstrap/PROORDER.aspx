<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PROORDER.aspx.cs" Inherits="Billing.Accountsbootstrap.PROORDER" %>

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
    <title>Production Order Process Entry </title>
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
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"></asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false">
    </asp:Label>
    <asp:Label runat="server" ID="lblProcessforMasterId" Text="5" ForeColor="White" CssClass="label"
        Visible="false"></asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblName" Text="Production Order Entry" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <div class="panel-body">
                    <div class="list-group">
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                        <div class="form-group">
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>
                                        Entry No:</label>
                                    <asp:TextBox ID="txtEntryNo" runat="server" CssClass="form-control center-block"
                                        Width="100px" Enabled="false">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>
                                        Entry Date:</label>
                                    <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control center-block"
                                        Width="100px" Enabled="false">
                                    </asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtEntryDate"
                                        PopupButtonID="txtEntryDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>
                                        From Date:</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control center-block"
                                        Width="100px">
                                    </asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                        PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>
                                        To Date:</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control center-block" Width="100px">
                                    </asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                        PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Exc.No:
                                    </label>
                                    <asp:CompareValidator ID="CompareValidator8" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" initialvalue="0" ControlToValidate="ddlExcNo" ValueToCompare="Select ExcNo"
                                        Operator="NotEqual" Type="String" ErrorMessage="Please Select ExcNo.">
                                    </asp:CompareValidator>
                                    <asp:DropDownList ID="ddlExcNo" runat="server" CssClass="chzn-select" Style="height: 30px"
                                        Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlExcNo_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                       <%--     <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        ProcessFrom:</label>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" initialvalue="0" ControlToValidate="ddlProcessFrom"
                                        ValueToCompare="Select ProcessFrom" Operator="NotEqual" Type="String" ErrorMessage="Please Select Process.">
                                    </asp:CompareValidator>
                                    <asp:DropDownList ID="ddlProcessFrom" runat="server" CssClass="chzn-select" Style="height: 30px"
                                        Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlProcessFrom_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Process:
                                    </label>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" initialvalue="0" ControlToValidate="ddlProcess" ValueToCompare="Select Process"
                                        Operator="NotEqual" Type="String" ErrorMessage="Please Select Process.">
                                    </asp:CompareValidator>
                                    <asp:DropDownList ID="ddlProcess" runat="server" CssClass="chzn-select" Style="height: 30px"
                                        Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Jobwork Ledger
                                    </label>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" initialvalue="0" ControlToValidate="ddlProcessLedger"
                                        ValueToCompare="Select Jobwork Ledger" Operator="NotEqual" Type="String" ErrorMessage="Please Select Jobwork Ledger.">
                                    </asp:CompareValidator>
                                    <asp:DropDownList ID="ddlProcessLedger" runat="server" CssClass="chzn-select" Style="height: 30px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel-body" style="margin-top: 30px; height: 100%">
        <div class="col-lg-12">
            <div class="col-lg-9">
                <asp:GridView ID="GVItem" AutoGenerateColumns="False" runat="server" OnRowCommand="GVItem_OnRowCommand"
                    GridLines="Both" Caption="Style Details">
                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                        Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                                <asp:HiddenField ID="hdTransItemId" runat="server" Value='<%#Eval("TransItemId")  %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StyleNo" HeaderStyle-Width="100px" 
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdStyleNoId" runat="server" Value='<%#Eval("StyleNoId")  %>' />
                                <asp:Label ID="lblStyleNo" Height="30px" Width="100px" runat="server" Text='<%#Eval("StyleNo")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-Width="250px" 
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" Height="30px" Width="200px" runat="server" Text='<%#Eval("Description")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Color" HeaderStyle-Width="30px" 
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId")  %>' />
                                <asp:Label ID="lblColor" Height="30px" Width="90px" runat="server" Text='<%#Eval("Color")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="40px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblRate" Height="30px" Width="40px" runat="server" Text='<%#Eval("Rate")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="40px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" Height="30px" Width="40px" runat="server" Text='<%#Eval("CQty")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IssueQty" HeaderStyle-Width="40px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblIssueQty" Height="30px" Width="40px" runat="server" Text='<%#Eval("IssueQty")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="ReceiveQty" HeaderStyle-Width="40px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblReceiveQty" Height="30px" Width="40px" runat="server" Text='<%#Eval("ReceiveQty")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DamageQty" HeaderStyle-Width="40px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblDamageQty" Height="30px" Width="40px" runat="server" Text='<%#Eval("DamageQty")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Size" HeaderStyle-Width="60px" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdRangeId" runat="server" Value='<%#Eval("RangeId")  %>' />
                                <asp:Label ID="lblSize" Height="30px" Width="90px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SetQty" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1px"
                            ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")  %>' />
                                <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                    CommandName="Modify">
                                    <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1px"
                            ItemStyle-Width="1px">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                    CommandName="View">
                                    <asp:Image ID="imgView" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:GridView ID="GVFabricRawDetails" runat="server" Width="100%" EmptyDataText="No Records Found"
                    AutoGenerateColumns="false" CssClass="myGridStyle1" Caption="Required Materials">
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
                                <asp:HiddenField ID="hdAvlStock" runat="server" Value='<%#Eval("AvlStock") %>' />
                                <asp:HiddenField ID="hdWantedRaw" runat="server" Value='<%#Eval("WantedRaw") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Item" HeaderText="Item" />
                        <asp:BoundField DataField="Color" HeaderText="Color" />
                        <asp:BoundField DataField="AvlStock" HeaderText="AvlStock" />
                        <asp:BoundField DataField="WantedRaw" HeaderText="WantedRaw" DataFormatString="{0:f2}"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                </asp:GridView>
            </div>
            <div class="col-lg-3">
                <asp:Button ID="btnSubmitQty" runat="server" Text="Submit Qty" Width="100%" OnClick="btnSubmitQty_OnClick" />
                <asp:Label ID="TransItemId" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="RowId" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="StyleNo" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="StyleNoId" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="Description" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="Color" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="ColorId" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="Rate" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="RangeId" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="Sizes" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="Qty" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="IssueQty" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="ReceiveQty" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="DamageQty" runat="server" Visible="false"></asp:Label>
                <asp:TextBox ID="txtrate" runat="server" AutoComplete="Off" CssClass="form-control" placeholder="Enter Rate"></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender31251rate" runat="server" ValidChars="."
                                    TargetControlID="txtrate" FilterType="Custom,Numbers" />
                <asp:GridView ID="GVSizes" AutoGenerateColumns="False" GridLines="Both" runat="server" ShowFooter="true" OnRowDataBound="GVSize_rowdatabound"
                    Caption="Assign Qty Details">
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
                                <asp:HiddenField ID="hdTransSizeId" runat="server" Value='<%#Eval("TransSizeId") %>' />
                                <asp:HiddenField ID="hdQty" runat="server" Value='<%#Eval("CQty") %>' />
                                <asp:HiddenField ID="hdSize" runat="server" Value='<%#Eval("SizeId") %>' />
                                <asp:Label ID="lblSize" Height="30px" Width="150px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="50px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" Height="30px" Width="50px" runat="server" Text='<%#Eval("CQty")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IssueQty" HeaderStyle-Width="50px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:TextBox ID="txtIssueQty" AutoComplete="off" Height="30px" Width="100%" runat="server" OnTextChanged="Issue_changed" AutoPostBack="true" Text='<%#Eval("IssueQty")%>'></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3125" runat="server"
                                    TargetControlID="txtIssueQty" FilterType="Numbers" />
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="ReceiveQty" HeaderStyle-Width="50px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:TextBox ID="txtReceiveQty" AutoComplete="off" Height="30px" Width="100%" runat="server" OnTextChanged="Receive_changed" AutoPostBack="true" Text='<%#Eval("ReceiveQty")%>'></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender31251" runat="server"
                                    TargetControlID="txtReceiveQty" FilterType="Numbers" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DamageQty" HeaderStyle-Width="50px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDamageQty" AutoComplete="off" Height="30px" Width="100%" runat="server" OnTextChanged="Damage_changed" AutoPostBack="true" Text='<%#Eval("DamageQty")%>'></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender31252" runat="server"
                                    TargetControlID="txtDamageQty" FilterType="Numbers" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
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
                                <asp:Label ID="lblSize" Height="30px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="100px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" Height="30px" runat="server" Text='<%#Eval("CQty")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IssueQty" HeaderStyle-Width="100px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblIssueQty" Height="30px" runat="server" Text='<%#Eval("IssueQty")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="ReceiveQty" HeaderStyle-Width="100px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblReceiveQty" Height="30px" runat="server" Text='<%#Eval("ReceiveQty")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DamageQty" HeaderStyle-Width="100px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblDamageQty" Height="30px" runat="server" Text='<%#Eval("DamageQty")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
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
                <asp:TextBox ID="txtRemarks" Height="80px" Width="100%" runat="server" CssClass="form-control">
                </asp:TextBox>
            </div>
            <div class="col-lg-2" id="ChallanNo" runat="server" visible="false">
                <label>
                    Challan No:</label>
                <asp:TextBox ID="txtChallanNo" runat="server" CssClass="form-control">
                </asp:TextBox>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        Finishing Process:
                    </label>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                        Text="*" Style="color: Red" initialvalue="0" ControlToValidate="ddlFinishingProcess"
                        ValueToCompare="FinishingProcess" Operator="NotEqual" Type="String" ErrorMessage="Please Select FinishingProcess.">
                    </asp:CompareValidator>
                    <asp:DropDownList ID="ddlFinishingProcess" runat="server" CssClass="chzn-select"
                        Style="height: 30px" Width="100%" Enabled="false">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        Company Name:
                    </label>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                        Text="*" Style="color: Red" initialvalue="0" ControlToValidate="ddlCompanyName"
                        ValueToCompare="CompanyName" Operator="NotEqual" Type="String" ErrorMessage="Please Select CompanyName.">
                    </asp:CompareValidator>
                    <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="chzn-select" Style="height: 30px"
                        Width="100%" Enabled="false">
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

