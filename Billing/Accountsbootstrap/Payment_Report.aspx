<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment_Report.aspx.cs"
    Inherits="Billing.Accountsbootstrap.Payment_Report" %>

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
    <title>Payment Report </title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            //        var prtContent = document.getElementById(gridOpening);
            var WinPrint = window.open('', '', 'letf=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
    </script>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <form id="form1" runat="server">
        <div class="col-lg-12">
            <div class="col-lg-2">
                <h1 class="page-header" style="text-align: center; color: Red; font-size: 20px">
                    Payment Report</h1>
            </div>
            <div class="col-lg-2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <div class="form-group">
                    <asp:Label ID="lblFromDate" runat="server" Style="font-weight: bold">From Date</asp:Label>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                        Text="*" ControlToValidate="txtFromDate" ErrorMessage="Please enter From date!"
                        Style="color: Red" />
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control center-block"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                        runat="server" CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <asp:Label ID="lblToDate" runat="server" Style="font-weight: bold">To Date</asp:Label>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                        Text="*" ControlToValidate="txtToDate" ErrorMessage="Please enter To date!" Style="color: Red" />
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control center-block"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                        runat="server" CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-lg-2" style="margin-top: -5px">
                <div class="form-group">
                    <label>
                        Select Company</label>
                    <asp:DropDownList ID="ddloutlet" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group;col-lg-2" style="padding-top: 20px">
                <asp:Button ID="btnSearch" runat="server" ValidationGroup="val1" Text="Generate Report"
                    CssClass="btn btn-success" Width="130px" Style="margin-left: 15px; margin-top: 2px"
                    OnClick="btnSearch_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div align="right">
                        <asp:button id="btnprint" runat="server" cssclass="btn btn-block center-block" text="Print"
                            width="125px" onclientclick="javascript:CallPrint('bill');" xmlns:asp="#unknown" />
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div id="bill">
                                    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                        ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                    <div class="row">
                                        <div class="col-lg-2">
                                        </div>
                                    </div>
                                    <h2 align="center">
                                        <asp:Label ID="lblMessage" Style="color: Blue;" runat="server"></asp:Label></h2>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td>
                                                            <table id="Table1" visible="false" runat="server" style="border: 1px solid Grey;
                                                                height: 15px; background-color: #59d3b4; text-transform: uppercase" width="100%">
                                                                <tr>
                                                                    <td align="center" style="font-size: small1; width: 6%">
                                                                        Trans No
                                                                    </td>
                                                                    <td align="center" style="font-size: small; width: 23%">
                                                                        Payment Option
                                                                    </td>
                                                                    <td align="center" style="font-size: small; width: 5%">
                                                                        Date
                                                                    </td>
                                                                    <td align="center" style="font-size: small; width: 19%">
                                                                        Ledger Name
                                                                    </td>
                                                                    <td align="center" style="font-size: small; width: 7%">
                                                                        Payment Mode
                                                                    </td>
                                                                    <td align="center" style="font-size: small; width: 5%">
                                                                        Cheque NO
                                                                    </td>
                                                                    <td align="center" style="font-size: small; width: 9%">
                                                                        Amount
                                                                    </td>
                                                                    <td align="center" style="font-size: small;">
                                                                        Narration
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div align="center">
                                                                <asp:GridView ID="gridPurchase" Width="100%" runat="server" EmptyDataText="Data Not Bound"
                                                                    AutoGenerateColumns="false" AllowPaging="false" OnPageIndexChanging="gridPurchase_PageIndexChanging"
                                                                    OnRowDataBound="gridPurchase_RowDataBound" ShowFooter="true">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="PaymentID" Visible="false" />
                                                                        <asp:BoundField DataField="DaybookId" ItemStyle-Width="6%" HeaderText="Day Book No"
                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="LedgerType" ItemStyle-Width="20%" HeaderText="Payment Option"
                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                        <%--           <asp:BoundField DataField="Branch" HeaderText="Branch" ItemStyle-HorizontalAlign="Center" />--%>
                                                                        <asp:BoundField DataField="PaymentDate" ItemStyle-Width="10%" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd-M-yyyy}" />
                                                                        <asp:BoundField DataField="LedgerName" ItemStyle-Width="16%" HeaderText="Ledger Name"
                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Payment_Mode" ItemStyle-Width="8%" HeaderText="Payment Type"
                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Chequeno" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderText="Cheque No" />
                                                                        <asp:BoundField DataField="Amount" ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Right"
                                                                            HeaderText="Amount" DataFormatString="{0:f2}" />
                                                                        <asp:BoundField DataField="Narration" ItemStyle-Width="28%" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderText="Narration" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <br />
                                                                <b>Payment Cash Total: </b>
                                                                <asp:Label ID="lblPaymenttot" runat="server" Text="0"></asp:Label>
                                                                <br />
                                                                <b>Payment Bank Total: </b>
                                                                <asp:Label ID="lblPaymentBanktot" runat="server" Text="0"></asp:Label>
                                                            </div>
                                                            <table border="0" cellspacing="0">
                                                                <tr id="Tr1" runat="server" visible="false">
                                                                    <td width="670px">
                                                                        <asp:Label ID="lblamt" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-lg-4">
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Button ID="btnExport" runat="server" CssClass="btn btn-block center-block" Text="Export To Excel"
                                                Width="125px" OnClick="btnExport_Click" />
                                        </div>
                                        <div class="col-lg-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
