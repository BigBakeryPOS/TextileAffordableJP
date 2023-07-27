<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Billing.Accountsbootstrap.Payment" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Employee Payment</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <script language="javascript" type="text/javascript">

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else

                event.returnValue = false;
        }

    </script>
    <script type="text/javascript">
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;

            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <%--<script type="text/javascript">

        var ScrollHeight = 300;
        window.onload = function () {
            var grid = document.getElementById('gvpayment');
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }
    </script>--%>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="Form1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-2">
                <h2 style="text-align: left; color: #fe0002; margin-top: -2px">
                    <asp:Label ID="lblTitle" Text="Payment" runat="server"></asp:Label></h2>
            </div>
            <div class="col-lg-2">
            </div>
            <div class="col-lg-8">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    Branch</label>
                                <asp:DropDownList ID="drpbranch" runat="server" CssClass="form-control" AutoPostBack="false"
                                    OnSelectedIndexChanged="drpbranch_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>
                                    From Date</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                    Text="*" ControlToValidate="txtironrecdate" ErrorMessage="Please select Date !"
                                    Style="color: Red" />
                                <asp:TextBox CssClass="form-control" onkeyup="ValidateDate(this, event.keyCode)"
                                    onkeydown="return DateFormat(this, event.keyCode)" ID="txtironrecdate" runat="server"
                                    Text="--Select Date--"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtironrecdate"
                                    runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <label>
                                    To Date</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                                    Text="*" ControlToValidate="txtironrecdate2" ErrorMessage="Please select Date !"
                                    Style="color: Red" />
                                <asp:TextBox CssClass="form-control" onkeyup="ValidateDate(this, event.keyCode)"
                                    onkeydown="return DateFormat(this, event.keyCode)" ID="txtironrecdate2" runat="server"
                                    Text="--Select Date--"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtironrecdate2"
                                    runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                <label>
                                    Type</label>
                                <asp:RadioButtonList ID="rdbprocesstype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbprocesstype_OnSelectedIndexChanged">
                                    <%--<asp:ListItem Text="Ironer In" Value="IronerIn" Selected="True" />--%>
                                    <%--<asp:ListItem Text="Ironer Out" Value="IronerOut" />--%>
                                    <asp:ListItem Text="Others" Value="Others" Selected="True" />
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="form-group">
                                <label>
                                    Payment No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" Text="*"
                                    ControlToValidate="txtPaymentNo" ErrorMessage="Please enter your Payment No!"
                                    Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtPaymentNo" MaxLength="15" runat="server"
                                    Enabled="false"></asp:TextBox><asp:TextBox ID="TextBox1" Visible="false" runat="server"
                                        Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <div class="form-group">
                                    <label>
                                        Payment Date</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator5"
                                        Text="*" ControlToValidate="txtpdate" ErrorMessage="Please select Date !" Style="color: Red" />
                                    <asp:TextBox CssClass="form-control" onkeyup="ValidateDate(this, event.keyCode)"
                                        onkeydown="return DateFormat(this, event.keyCode)" ID="txtpdate" runat="server"
                                        Text="--Select Date--"></asp:TextBox>
                                </div>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtpdate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <label>
                                    Process</label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="DpProcess" ValueToCompare="Select Process Name"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Process Name"></asp:CompareValidator>
                                <asp:DropDownList ID="DpProcess" Enabled="true" CssClass="chzn-select form-control"
                                    TabIndex="50" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                <label>
                                    LotNo</label>
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtLotNo" MaxLength="15"
                                    runat="server"></asp:TextBox><asp:Label ID="txtLotDetailId" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>
                                    TotalAmount</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="" id="RequiredFieldValidator1" Text="*" controltovalidate="txtAddress" errormessage="Please enter your Address!" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtTotalAmount" MaxLength="15"
                                    runat="server"></asp:TextBox>
                                <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtFromDate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>--%>
                            </div>
                            <div class="form-group">
                                <label>
                                    Narration</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="Narration"
                                    Text="*" ControlToValidate="txtNarration" ErrorMessage="Please enter your narration"
                                    Style="color: Red" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" +,!@#$%^&*()-/:;."
                                    TargetControlID="txtNarration" />
                                <asp:TextBox CssClass="form-control" ID="txtNarration" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    JobWork</label>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlEName" ValueToCompare="Select Employee Name"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Ledger Name"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlEName" CssClass="chzn-select form-control" runat="server"
                                    TabIndex="51">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                <label>
                                    PaidAmount</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="" id="RequiredFieldValidator2" Text="*" controltovalidate="txtArea" errormessage="Please enter your Area!" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtPaidAmount" MaxLength="15"
                                    runat="server"></asp:TextBox>
                                <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtToDate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>--%>
                                <asp:Button ID="btlprcess" runat="server" OnClick="Process_CIclk" Visible="false"
                                    Text="Process" />
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                <label>
                                    Amount</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="city" ControlToValidate="txtAmount"
                                    Text="*" ErrorMessage="Please enter your Amount!" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtAmount" MaxLength="8" AutoPostBack="true"
                                    runat="server" onkeypress="return NumberOnly()"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    Payment Mode</label>
                                <asp:DropDownList ID="ddlPayment" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPayment_OnSelectedIndexChanged">
                                    <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="(NEFT/IMPS/RTGS or CHEQUE)" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>
                                    BankName
                                </label>
                                <asp:DropDownList ID="drpbanklist" runat="server" CssClass="form-control" OnSelectedIndexChanged="bank_select"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:TextBox CssClass="form-control" ID="txtbank" Visible="false" Enabled="false"
                                    runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Cheque/UTI No.
                                </label>
                                <asp:TextBox CssClass="form-control" ID="txtcheque" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <br />
                            <table>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label1" runat="server" Style="width: 500px; font-weight: bold; font-size: inherit">TotalQty:</asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lbllTotalQty" runat="server" Style="width: 100px; font-weight: bold;
                                            color: Red; font-size: larger">0</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label2" runat="server" Style="width: 500px; font-weight: bold; font-size: inherit">TotalAmount:</asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lbllTotalAmount" runat="server" Style="width: 100px; font-weight: bold;
                                            color: Red; font-size: larger">0.00</asp:Label>
                                    </td>
                                </tr>
                                <tr id="idbillAmt" runat="server" visible="false">
                                    <td style="text-align: right">
                                        <asp:Label ID="Label4" runat="server" Style="width: 500px; font-weight: bold; font-size: inherit">Ttl Bill Amt:</asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblbillAmt" runat="server" Style="width: 100px; font-weight: bold;
                                            color: Red; font-size: larger">0.00</asp:Label>
                                    </td>
                                </tr>
                                <tr id="idDebitAmt" runat="server" visible="false">
                                    <td style="text-align: right">
                                        <asp:Label ID="Label3" runat="server" Style="width: 500px; font-weight: bold; font-size: inherit">Ttl Dr Amt:</asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblDebitAmt" runat="server" Style="width: 100px; font-weight: bold;
                                            color: Red; font-size: larger">0.00</asp:Label>
                                    </td>
                                </tr>
                                <tr id="idMisc" runat="server" visible="false">
                                    <td style="text-align: right" runat="server" visible="false">
                                        <asp:Label ID="Label6" runat="server" Style="width: 500px; font-weight: bold; font-size: inherit">Ttl Misc.Amt:</asp:Label>
                                    </td>
                                    <td style="text-align: left" runat="server" visible="false">
                                        <asp:Label ID="lblMisc" runat="server" Style="width: 100px; font-weight: bold; color: Red;
                                            font-size: larger">0.00</asp:Label>
                                    </td>
                                </tr>
                                <tr id="idadv" runat="server" visible="false">
                                    <td style="text-align: right">
                                        <asp:Label ID="Label7" runat="server" Style="width: 500px; font-weight: bold; font-size: inherit">Ttl Adv.Amt:</asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lbladv" runat="server" Style="width: 100px; font-weight: bold; color: Red;
                                            font-size: larger">0.00</asp:Label>
                                    </td>
                                </tr>
                                <tr id="idsampleAmt" runat="server" visible="false">
                                    <td style="text-align: right">
                                        <asp:Label ID="Label5" runat="server" Style="width: 500px; font-weight: bold; font-size: inherit">Sample Amount:</asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="lblsampleamt" runat="server" Style="width: 100px; font-weight: bold;
                                            color: Red; font-size: larger">0.00</asp:Label>
                                    </td>
                                </tr>
                                <tr id="bankdetails" runat="server" visible="false">
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    B.Name:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblbankname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    B.Acc.No:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblbankaccno" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    B.Desc:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblbankdesc" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="idtrimsdebitamt" runat="server" visible="false">
                                    <td style="text-align: right">
                                        <label>
                                            Trims Amount:
                                        </label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txttrimsdebitamt" Enabled="true" runat="server" Width="80px">0</asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-12">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-12">
                <asp:GridView runat="server" ID="GridView1" CssClass="myGridStyle" AutoGenerateColumns="false"
                    ShowHeader="true" ShowFooter="false" Width="100%" OnRowDataBound="gridprint_RowDataBound">
                    <HeaderStyle BackColor="#59d3b4" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LotNo" ControlStyle-Width="100%" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblCompanyLotNo" Text='<%#Eval("CompanyLotNo") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="CompanyLotNo" HeaderText="LotNo" />--%>
                        <%--<asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" />--%>
                        <%--<asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" />--%>
                        <asp:TemplateField HeaderText="PaidAmount" ControlStyle-Width="100%" ItemStyle-Width="10%"
                            Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblPaidAmount" Text='<%#Eval("PaidAmount","{0:n}") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Rate" HeaderText="Rate" Visible="false" />
                        <asp:BoundField DataField="Rate" HeaderText="Rate" Visible="false" />
                        <asp:BoundField DataField="Rec30FS" HeaderText="30-FS" />
                        <asp:BoundField DataField="Rec32FS" HeaderText="32-FS" />
                        <asp:BoundField DataField="Rec34FS" HeaderText="34-FS" />
                        <asp:BoundField DataField="Rec36FS" HeaderText="36-FS" />
                        <asp:BoundField DataField="RecXSFS" HeaderText="XS-FS" />
                        <asp:BoundField DataField="RecSFS" HeaderText="S-FS" />
                        <asp:BoundField DataField="RecMFS" HeaderText="M-FS" />
                        <asp:BoundField DataField="RecLFS" HeaderText="L-FS" />
                        <asp:BoundField DataField="RecXLFS" HeaderText="XL-FS" />
                        <asp:BoundField DataField="RecXXLFS" HeaderText="2XL-FS" />
                        <asp:BoundField DataField="Rec3XLFS" HeaderText="3XL-FS" />
                        <asp:BoundField DataField="Rec4XLFS" HeaderText="4XL-FS" />
                        <asp:BoundField DataField="Rec30HS" HeaderText="30-HS" />
                        <asp:BoundField DataField="Rec32HS" HeaderText="32-HS" />
                        <asp:BoundField DataField="Rec34HS" HeaderText="34-HS" />
                        <asp:BoundField DataField="Rec36HS" HeaderText="36-HS" />
                        <asp:BoundField DataField="RecXSHS" HeaderText="XS-HS" />
                        <asp:BoundField DataField="RecSHS" HeaderText="S-HS" />
                        <asp:BoundField DataField="RecMHS" HeaderText="M-HS" />
                        <asp:BoundField DataField="RecLHS" HeaderText="L-HS" />
                        <asp:BoundField DataField="RecXLHS" HeaderText="XL-HS" />
                        <asp:BoundField DataField="RecXXLHS" HeaderText="2XL-HS" />
                        <asp:BoundField DataField="Rec3XLHS" HeaderText="3XL-HS" />
                        <asp:BoundField DataField="Rec4XLHS" HeaderText="4XL-HS" />
                        <asp:TemplateField HeaderText="Total" ControlStyle-Width="100%" ItemStyle-Width="10%"
                            Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblTtlQty" Text='<%#Eval("TtlQty") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate" ControlStyle-Width="100%" ItemStyle-Width="10%"
                            Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblRate" Text='<%#Eval("Rate","{0:n}") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SalaryRate" ControlStyle-Width="100%" ItemStyle-Width="10%"
                            Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblgrandant" Text='<%#Eval("Payamount","{0:n}") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="TtlQty" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />--%>
                        <asp:TemplateField HeaderText="PayAmount" ControlStyle-Width="100%" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:TextBox ID="lblpayamount" Text='<%#Eval("Payamount","{0:n}") %>' runat="server">0</asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StichingId" ControlStyle-Width="100%" ItemStyle-Width="10%"
                            Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIroningId" Text='<%#Eval("IroningId") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#59d3b4" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div style="text-align: center">
        <asp:GridView ID="gvpayment" AutoGenerateColumns="False" ShowFooter="True" runat="server"
            EmptyDataText="No Data Found" OnRowDataBound="GVPaymentGrid_RowDataBound" RowStyle-BackColor="Bisque">
            <Columns>
                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cutting No">
                    <ItemTemplate>
                        <asp:Label ID="lblCompanyLotNo" Text='<%#Eval("CompanyLotNo") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Work Order No">
                    <ItemTemplate>
                        <asp:Label ID="lbliddno" Text='<%#Eval("idd") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString='{0:dd/MM/yyyy}' />
                <asp:TemplateField HeaderText="Isu.Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalIssue" Text='<%#Eval("TotalIssue") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rec.Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalReceive" Text='<%#Eval("TotalReceive") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dmg.Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalDamage" Text='<%#Eval("TotalDamage") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LotValue">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalrecamt" Text='<%#Eval("TotalAmount","{0:n}") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ttl.Debit Amt.">
                    <ItemTemplate>
                        <asp:TextBox ID="txtpreDebitAmt" Width="100%" Text='<%#Eval("PreDebitAmount","{0:n}") %>'
                            runat="server" Enabled="false"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server"
                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtpreDebitAmt" />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Ttl.Paid Amt.">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTotalPaidAmt" Width="100%" Text='<%#Eval("TotalPaid","{0:n}") %>'
                            runat="server" Enabled="false"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender451" runat="server"
                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtTotalPaidAmt" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Debit Amt.">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDebitAmt" Width="100%" Text='<%#Eval("DebitAmount","{0:n}") %>'
                            runat="server"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender46" runat="server"
                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtDebitAmt" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PaymentQty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPaymentQty" Width="100%" Text='<%#Eval("PaymentQty","{0:0}") %>'
                            runat="server"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender42" runat="server"
                            FilterType="Numbers,Custom" ValidChars="-" TargetControlID="txtPaymentQty" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" Text='<%#Eval("Rate","{0:n}") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PaidAmount">
                    <ItemTemplate>
                        <asp:Label ID="lblAlreadyPaid" Text='<%#Eval("PaidAmount","{0:n}") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Balance To-Pay">
                    <ItemTemplate>
                        <asp:Label ID="lblBalAmount" Font-Bold="true" ForeColor="Red" Font-Size="Larger" Text='<%#Eval("BalAmount","{0:n}") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Amount To Pay" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox ID="lblpayamountNew" Width="100%" runat="server" Text='<%#Eval("PayableAmount","{0:n}") %>' Enabled="false">0</asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenreder43" runat="server"
                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="lblpayamountNew" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount To Pay" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox ID="lblpayamount" Width="100%" runat="server" Text='<%#Eval("PayableAmount","{0:n}") %>' Enabled="false">0</asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server"
                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="lblpayamount" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Paid Advance">
                    <ItemTemplate>
                        <asp:Label ID="lblAdvancePaid" Text='<%#Eval("Advance","{0:n}") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  Visible="false" HeaderText="Advance Amt">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAdvance" runat="server"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server"
                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtAdvance" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Payment Type" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:DropDownList ID="drppaymenttype" runat="server" CssClass="form-control" >
                        <asp:ListItem Text="Advance" Value="Advance" ></asp:ListItem>
                        <asp:ListItem Text="Balance" Value="Balance" ></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Narration">
                    <ItemTemplate>
                        <asp:TextBox ID="txtnarration" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Allow it" Visible="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAllowit" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Close" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkitemchecked" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Miscellaneous" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox ID="txtmiscellaneous" runat="server">0</asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="StichingId" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblStichingId" Text='<%#Eval("ValId") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#59d3b4" />
        </asp:GridView>
    </div>
    <div class="row" style="margin-top: 50px">
        <div class="col-lg-12">
            <div class="col-lg-7">
            </div>
            <div class="col-lg-5">
                <asp:Button runat="server" ID="save" OnClick="Add_Click" class="btn btn-info" Width="120px"
                    Text="Save" />
                <asp:Button runat="server" ID="exit" PostBackUrl="~/Accountsbootstrap/PaymentGridView.aspx"
                    class="btn btn-default" Text="Exit" Width="120px" />
                <asp:Button runat="server" ID="btncalc" OnClick="Calc_Click" class="btn btn-danger"
                    Text="Calc" Width="120px" />
                <asp:Button runat="server" ID="btnsearch" OnClick="btnsearch_Click" class="btn btn-success"
                    Text="Search" Width="120px" />
            </div>
        </div>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <%-- <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>--%>
    </form>
</body>
</html>
