<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pro_lossNEWDatewise.aspx.cs" Inherits="Billing.Accountsbootstrap.Pro_lossNEWDatewise" %>

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
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link rel="Stylesheet" type="text/css" href="../Styles/style1.css" />
    <title>Profit Page - bootsrap</title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
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
        function switchViews(obj, imG) {
            var div = document.getElementById(obj);
            var img = document.getElementById(imG);

            if (div.style.display == "none") {
                div.style.display = "inline";


                img.src = "/images/minus.gif";

            }
            else {
                div.style.display = "none";
                img.src = "/images/plus.gif";

            }
        }

    </script>
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
    <div class="row">
        <form id="Form1" runat="server" role="form">
        <div class="col-lg-12" style="margin-top: 6px">
            <div class="col-lg-2">
                <h3 class="page-header" style="text-align: left; color: #fe0002; font-size: 20px">
                    Profit And Loss Report</h3>
            </div>
            <div class="col-lg-2">
                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                <div class="form-group" runat="server" visible="true">
                    <label>
                        From Date</label>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="txtfrmdate" ValueToCompare="Select Date"
                        Operator="NotEqual" Type="String" ErrorMessage="Please Select From Date"></asp:CompareValidator>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                        Text="*" ControlToValidate="txtfrmdate" ErrorMessage="Please enter From date!"
                        Style="color: Red" />
                    <asp:TextBox CssClass="form-control" ID="txtfrmdate" runat="server" Text="Select Date"></asp:TextBox>
                </div>
                <ajaxToolkit:CalendarExtender ID="txtfrmdate1" TargetControlID="txtfrmdate" Format="dd/MM/yyyy"
                    runat="server" CssClass="cal_Theme1">
                </ajaxToolkit:CalendarExtender>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </div>
            <div class="col-lg-2" runat="server" visible="true">
                <div class="form-group">
                    <label>
                        To Date</label>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="txttodate" ValueToCompare="Select Date"
                        Operator="NotEqual" Type="String" ErrorMessage="Please Select To date"></asp:CompareValidator>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                        Text="*" ControlToValidate="txttodate" ErrorMessage="Please enter To date!" Style="color: Red" />
                    <asp:TextBox CssClass="form-control" ID="txttodate" runat="server" Text="Select Date"></asp:TextBox>
                </div>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txttodate"
                    Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                </ajaxToolkit:CalendarExtender>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        Select Company</label>
                    <asp:DropDownList ID="ddloutlet" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <%--  <div class="form-group">
                                            <label>Select Company</label>
                                            <asp:DropDownList ID="ddloutlet" runat="server" CssClass="form-control">
                                            <%--<asp:ListItem Text="select" Value="0"></asp:ListItem>--%>
            <%--<asp:ListItem Text="CO1" Value="CO1"></asp:ListItem>
                                              <asp:ListItem Text="CO2" Value="CO2"></asp:ListItem>
                                               <asp:ListItem Text="CO3" Value="CO3"></asp:ListItem>
                                               <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                            </asp:DropDownList>
                                                                                       
                                        </div>--%>
            <div class="col-lg-2" style="margin-top: 23px">
                <div class="form-group">
                    <asp:Button ID="btnreport" runat="server" class="btn btn-info" ValidationGroup="val1"
                        Text="Generate Report" Style="width: 160px;" OnClick="btnreport_Click" />
                </div>
            </div>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div align="right">
                    <asp:button id="btnprint" runat="server" text="Print" width="125px" cssclass="btn btn-block center-block"
                        onclientclick="javascript:CallPrint('idt');" xmlns:asp="#unknown" />
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" id="bill1">
                           
                            <div class="row" visible="false" runat="server">
                                <div class="col-lg-12">
                                    <%--<table id="Table2" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;
                            text-transform: uppercase" width="100%">
                            <tr>
                                
                                 <td align="center" style="font-size: small1; width: 52%">
                                    Particulars
                                </td>
                                
                                <td align="center" style="font-size: small; width: 16%">
                                    Debit
                                </td>
                                <td align="center" style="font-size: small; width: 16%">
                                    Credit
                                </td>
                                <td align="center" style="font-size: small; width: 26%">
                                  GP 
                                </td>
                                
                            </tr>
                        </table>--%>
                                    <div align="center">
                                        <asp:GridView runat="server" Width="100%" ID="gvLiaLedger" ShowFooter="true" GridLines="Both"
                                            AutoGenerateColumns="false" OnRowDataBound="gBalance_RowDataBound" DataKeyNames="LedgerID">
                                            <Columns>
                                                <%--<asp:BoundField DataField="Folionumber" HeaderText="LNO"  />--%>
                                                <asp:BoundField DataField="LedgerName" ItemStyle-Width="52%" HeaderText="PURCHASE & SALES" />
                                                <%--<asp:BoundField DataField="Branch" HeaderText="Branch"  />--%>
                                                <%--  <asp:BoundField DataField="Debit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Credit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />--%>
                                                <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblDebit" runat="server"
                                                            Text='<%# Eval("Debit","{0:f3}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblCredit" runat="server"
                                                            Text='<%# Eval("Credit","{0:f3}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="26%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lbltotal" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <table id="Table1" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;
                                        text-transform: uppercase" width="100%">
                                        <tr>
                                            <td align="center" style="font-size: small1; width: 52%">
                                            </td>
                                            <td align="center" style="font-size: small; width: 16%">
                                            </td>
                                            <td align="center" style="font-size: small; width: 16%">
                                            </td>
                                            <td align="center" style="font-size: small; width: 26%">
                                            </td>
                                        </tr>
                                    </table>
                                    <div align="center">
                                        <asp:GridView runat="server" Width="100%" ID="GridView4" ShowFooter="true" GridLines="Both"
                                            AutoGenerateColumns="false" OnRowDataBound="GridView4_RowDataBound" DataKeyNames="LedgerID">
                                            <Columns>
                                                <%--<asp:BoundField DataField="Folionumber" HeaderText="LNO"  />--%>
                                                <asp:BoundField DataField="LedgerName" ItemStyle-Width="52%" HeaderText="Income" />
                                                <%--<asp:BoundField DataField="Branch" HeaderText="Branch"  />--%>
                                                <%--  <asp:BoundField DataField="Debit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Credit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />--%>
                                                <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblDebit" runat="server"
                                                            Text='<%# Eval("Debit","{0:f3}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblCredit" runat="server"
                                                            Text='<%# Eval("Credit","{0:f3}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="26%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lbltotal" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div align="center">
                                        <asp:GridView runat="server" Width="100%" ID="GridView1" ShowFooter="true" GridLines="Both"
                                            AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="LedgerID">
                                            <Columns>
                                                <%--<asp:BoundField DataField="Folionumber" HeaderText="LNO"  />--%>
                                                <asp:BoundField DataField="LedgerName" ItemStyle-Width="52%" HeaderText="EXPENSES" />
                                                <%--<asp:BoundField DataField="Branch" HeaderText="Branch"  />--%>
                                                <%--  <asp:BoundField DataField="Debit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Credit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />--%>
                                                <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblDebit" runat="server"
                                                            Text='<%# Eval("Debit","{0:f3}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblCredit" runat="server"
                                                            Text='<%# Eval("Credit","{0:f3}") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="26%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lbltotal" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <br />
                                    <table id="Table3" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;
                                        text-transform: uppercase" width="40%">
                                        <tr>
                                            <td align="center" style="font-size: small1; width: 52%">
                                                Category Name
                                            </td>
                                            <td align="center" style="font-size: small; width: 16%">
                                                Amount
                                            </td>
                                        </tr>
                                    </table>
                                    <div align="left">
                                        <asp:GridView runat="server" Width="40%" ID="GridView2" ShowFooter="true" GridLines="Both"
                                            AutoGenerateColumns="false" OnRowDataBound="GridView2_RowDataBound" DataKeyNames="CategoryID">
                                            <Columns>
                                                <asp:BoundField DataField="Category" ItemStyle-Width="52%" HeaderText="PURCHASE" />
                                                <asp:BoundField DataField="aempty" ItemStyle-Width="52%" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n3}" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <br />
                                    <table id="Table4" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;
                                        text-transform: uppercase" width="40%">
                                        <tr>
                                            <td align="center" style="font-size: small1; width: 52%">
                                                Category Name
                                            </td>
                                            <td align="center" style="font-size: small; width: 16%">
                                                Amount
                                            </td>
                                        </tr>
                                    </table>
                                    <div align="Left">
                                        <asp:GridView runat="server" Width="40%" ID="GridView3" ShowFooter="true" GridLines="Both"
                                            AutoGenerateColumns="false" OnRowDataBound="GridView3_RowDataBound" DataKeyNames="CategoryID">
                                            <Columns>
                                                <asp:BoundField DataField="Category" ItemStyle-Width="52%" HeaderText="SALES" />
                                                <asp:BoundField DataField="aempty" ItemStyle-Width="52%" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:n3}" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <!-- /.col-lg-12 -->
                            </div>
                        </div>
                    </div>
                    <!-- /.row -->
                    <div class="row" id="idt" visible="false" runat="server">
                        <div class="col-lg-12">
                         <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                            <h2 align="center">
                                <asp:Label ID="lblMessage" Style="color: Blue;" runat="server"></asp:Label></h2>

                            <div class="panel panel-default">
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <table style="border: 1px solid black; width: 100%">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <b>Profit & Loss </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-right: 1px solid black; width: 50%">
                                                <table width="70%" border="0" style="font-weight: bold;">
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Opening Stock
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblOpeningStock" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Purchase A/c
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblPurchaseTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Purchase Return
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblPurchaseReturnTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td align="right" style="border-top: 1px solid black; border-bottom: 0px solid black;">
                                                            &nbsp;<asp:Label ID="lblFirstMidTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                            <img onclick="javascript:switchViews('dvDX','imgDX');" id="imgDX" runat="server"
                                                                src="~/Images/plus.gif" alt="Show" />
                                                            &nbsp;Direct Expenses
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblDXTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <div id="dvDX" style="display: none;" runat="server">
                                                                <asp:GridView runat="server" BorderWidth="1" ID="gvDirectExp" GridLines="Both" AlternatingRowStyle-CssClass="even"
                                                                    AutoGenerateColumns="false" Width="100%" Style="font-family: 'Trebuchet MS';
                                                                    font-size: 11px;">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="Number" ItemStyle-HorizontalAlign="Left" HeaderText="NO" />--%>
                                                                        <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                                        <asp:BoundField DataField="BranchCode" ItemStyle-HorizontalAlign="Left" HeaderText="Branch" />
                                                                        <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Gross Profit
                                                        </td>
                                                        <td align="right" style="border-top: 0px solid black; border-bottom: 0px solid black;">
                                                            &nbsp;<asp:Label ID="lblGP" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td align="left">
                                                            &nbsp;Total
                                                        </td>
                                                        <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                                            &nbsp;<asp:Label ID="lblDbTot" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table width="70%" border="0" style="font-weight: bold;">
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Closing Stock
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblClosingStock" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Sales A/c
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblSalesTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Sales Return
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblSalesReturnTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td align="right" style="border-top: 1px solid black; border-bottom: 0px solid black;">
                                                            &nbsp;<asp:Label ID="lblSecondMidTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                            <img onclick="javascript:switchViews('dvDI','imgDI');" id="imgDI" runat="server"
                                                                src="~/Images/plus.gif" alt="Show" />
                                                            &nbsp;Direct Income
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblDIncome" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <div id="dvDI" style="display: none;" runat="server">
                                                                <asp:GridView runat="server" BorderWidth="1" ID="gvDirectInc" GridLines="Both" AlternatingRowStyle-CssClass="even"
                                                                    AutoGenerateColumns="false" Width="100%" Style="font-family: 'Trebuchet MS';
                                                                    font-size: 11px;">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="Number" ItemStyle-HorizontalAlign="Left" HeaderText="NO" />--%>
                                                                        <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                                        <asp:BoundField DataField="BranchCode" ItemStyle-HorizontalAlign="Left" HeaderText="Branch" />
                                                                        <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Gross Loss
                                                        </td>
                                                        <td align="right" style="border-top: 0px solid black; border-bottom: 0px solid black;">
                                                            &nbsp;<asp:Label ID="lblGL" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left">
                                                            &nbsp;Total
                                                        </td>
                                                        <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                                            &nbsp;<asp:Label ID="lblCrTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                   
                                        <tr>
                                            <td style="border-right: 1px solid black; width: 50%">
                                                <table width="70%" border="0" style="font-weight: bold;">
                                                <tr>
                                                        <td align="left">
                                                            &nbsp;Gross Loss(B/F)
                                                        </td>
                                                        <td align="right" style="border-top: 0px solid black; border-bottom: 0px solid black;">
                                                            &nbsp;<asp:Label ID="lblGrossLossBF" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                            <img onclick="javascript:switchViews('dvIDX','imgIDX');" id="imgIDX" runat="server"
                                                                src="~/Images/plus.gif" alt="Show" />
                                                            &nbsp;Indirect Expenses
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblIDXExptotal" Text="0" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <div id="dvIDX" style="display: none;" runat="server">
                                                                <asp:GridView runat="server" BorderWidth="1" ID="gvIDirectExp" GridLines="Both" AlternatingRowStyle-CssClass="even"
                                                                    AutoGenerateColumns="false" Width="100%" Style="font-family: 'Trebuchet MS';
                                                                    font-size: 11px;">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />--%>
                                                                        <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                                        <asp:BoundField DataField="BranchCode" ItemStyle-HorizontalAlign="Left" HeaderText="Branch" />
                                                                        <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Net Profit
                                                        </td>
                                                        <td align="right" style="border-top: 0px solid black; border-bottom: 0px solid black;">
                                                            &nbsp;<asp:Label ID="lblNetProfit" Text="0" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left">
                                                            &nbsp;Net Total
                                                        </td>
                                                        <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                                            &nbsp;<asp:Label ID="lblDbNetTotal" Text="0" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table width="70%" border="0" style="font-weight: bold;">
                                                <tr>
                                                        <td align="left">
                                                            &nbsp;Gross Profit(B/F)
                                                        </td>
                                                        <td align="right" style="border-top: 0px solid black; border-bottom: 0px solid black;">
                                                            &nbsp;<asp:Label ID="lblGrossProfitBF" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                            <img onclick="javascript:switchViews('dvIDI','imgIDI');" id="imgIDI" runat="server"
                                                                src="~/Images/plus.gif" alt="Show" />
                                                            &nbsp;Indirect Income
                                                        </td>
                                                        <td align="right">
                                                            &nbsp;<asp:Label ID="lblIDIncome" Text="0" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <div id="dvIDI" style="display: none;" runat="server">
                                                                <asp:GridView runat="server" BorderWidth="1" ID="gvIDirectInc" GridLines="Both" AlternatingRowStyle-CssClass="even"
                                                                    AutoGenerateColumns="false" Width="100%" Style="font-family: 'Trebuchet MS';
                                                                    font-size: 11px;">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="Folionumber" ItemStyle-HorizontalAlign="Left" HeaderText="L.FNO" />--%>
                                                                        <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger" />
                                                                        <asp:BoundField DataField="BranchCode" ItemStyle-HorizontalAlign="Left" HeaderText="Branch" />
                                                                        <asp:BoundField DataField="Expenses" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;Net Loss
                                                        </td>
                                                        <td align="right" style="border-top: 0px solid black; border-bottom: 0px solid black;">
                                                            &nbsp;<asp:Label ID="lblNetLoss" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left">
                                                            &nbsp;Net Total
                                                        </td>
                                                        <td align="right" style="border-top: 1px solid black; border-bottom: 1px solid black;">
                                                            &nbsp;<asp:Label ID="lblCrNetTotal" Text="0" runat="server" CssClass="lblFont"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- /.table-responsive -->
                                    <!-- /.col-lg-6 (nested) -->
                                </div>
                                <!-- /.row (nested) -->
                            </div>
                            <!-- /.panel-body -->
                        </div>
                        <!-- /.panel -->
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /#page-wrapper -->
        </div>
    </div>
    </div> </div> </div> </form>
    <!-- jQuery -->
</body>
</html>
