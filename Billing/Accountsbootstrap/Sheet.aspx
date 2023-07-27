<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sheet.aspx.cs" Inherits="Billing.Accountsbootstrap.Sheet" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link rel="Stylesheet" type="text/css" href="../Styles/style1.css" />
    <title>Sheet Page - bootstrap</title>
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


                img.src = "../images/minus.gif";

            }
            else {
                div.style.display = "none";
                img.src = "../images/plus.gif";

            }
        }

    </script>
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
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
    <form id="Form1" runat="server" role="form">
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h3 class="page-header" style="text-align: left; color: #fe0002;">
                Balance Sheet Report</h3>
        </div>
        <div class="col-lg-2" style="margin-top: -40px; margin-left: 340px;">
            <div class="form-group">
                <label style="margin-top: 1px; margin-left: 1px;">
                    From Date</label>
                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                    Text="*" ControlToValidate="txtfrmdate" ErrorMessage="Please enter From date!"
                    Style="color: Red" />
                <asp:TextBox CssClass="form-control" ID="txtfrmdate" runat="server" Text="Select Date"
                    Style="margin-top: 1px; margin-left: 1px;"></asp:TextBox>
            </div>
            <ajaxToolkit:CalendarExtender ID="txtfrmdate1" TargetControlID="txtfrmdate" Format="dd/MM/yyyy"
                runat="server" CssClass="cal_Theme1">
            </ajaxToolkit:CalendarExtender>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <div class="col-lg-2" style="margin-top: -40px; margin-left: 40px;">
            <div class="form-group">
                <label>
                    To Date</label>
                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                    Text="*" ControlToValidate="txttodate" ErrorMessage="Please enter To date!" Style="color: Red" />
                <asp:TextBox CssClass="form-control" ID="txttodate" runat="server" Text="Select Date"></asp:TextBox>
            </div>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txttodate"
                Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
            </ajaxToolkit:CalendarExtender>
        </div>
        <div class="col-lg-2" style="margin-top: -40px; margin-left: 50px;">
            <div class="form-group">
                <label>
                    Select Company</label>
                <asp:DropDownList ID="ddloutlet" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-2" style="margin-top: -50px; margin-left: 1170px">
            <div class="form-group">
                <asp:Button ID="btnreport" runat="server" class="btn btn-info" ValidationGroup="val1"
                    Text="Generate Report" Style="width: 160px;" OnClick="btnreport_Click" />
            </div>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div align="right">
                    <asp:button id="Button3" runat="server" cssclass="btn btn-block center-block" text="Print"
                        width="125px" onclientclick="javascript:CallPrint('bill');" xmlns:asp="#unknown" />
                </div>
                <div class="panel-body" id="bill">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                            <h2 align="center">
                                <asp:Label ID="lblMessage" Style="color: Blue;" runat="server"></asp:Label></h2>
                            <div id="ss" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <table id="Table2" visible="false" runat="server" style="border: 1px solid Grey;
                                            height: 15px; background-color: #59d3b4; text-transform: uppercase" width="100%">
                                            <tr>
                                                <td align="center" style="font-size: small1; width: 52%">
                                                    Particulars
                                                </td>
                                                <td align="center" style="font-size: small; width: 12%">
                                                    Debit
                                                </td>
                                                <td align="center" style="font-size: small; width: 22%">
                                                    Credit
                                                </td>
                                                <td align="center" style="font-size: small; width: 26%">
                                                </td>
                                            </tr>
                                        </table>
                                        <div align="center">
                                            <asp:GridView runat="server" Width="100%" ID="gvLiaLedger" ShowFooter="true" GridLines="Both"
                                                AutoGenerateColumns="false" OnRowDataBound="gBalance_RowDataBound" DataKeyNames="LedgerID">
                                                <Columns>
                                                    <%--<asp:BoundField DataField="Folionumber" HeaderText="LNO"  />--%>
                                                    <asp:BoundField DataField="LedgerName" HeaderText="Particulars" />
                                                    <%--<asp:BoundField DataField="Branch" HeaderText="Branch"  />--%>
                                                    <%--  <asp:BoundField DataField="Debit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Credit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />--%>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="Debit" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblDebit" runat="server"
                                                                Text='<%# Eval("Debit","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblCredit" runat="server"
                                                                Text='<%# Eval("Credit","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lbltotal" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <%--</ItemTemplate>
                                        </asp:TemplateField>--%>
                                    <%--   </Columns>

                                </asp:GridView>--%>
                                </div>
                            </div>
                            <!-- /.row -->
                            <div class="row" id="idt" visible="false" runat="server">
                                <div class="col-lg-12">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="table-responsive">
                                                        <table width="90%" class="table table-bordered table-striped">
                                                            <tr style="height: 10%">
                                                                <td align="center">
                                                                    <b>Liability</b>
                                                                </td>
                                                                <td align="center">
                                                                    <b>Asset</b>
                                                                </td>
                                                            </tr>
                                                                        <tr style="height: 100%">
                                                                            <td valign="top">
                                                                                <asp:GridView runat="server" ID="gvLiabilityBalance" GridLines="None" AutoGenerateColumns="false"
                                                                                    AllowPrintPaging="true" Width="100%" DataKeyNames="HeadingID" Style="font-family: 'Trebuchet MS';
                                                                                    font-size: 15px;" OnRowDataBound="gvLiabilityBalance_RowDataBound" ShowFooter="false"
                                                                                    ShowHeader="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <a href="javascript:switchViews('div<%# Eval("HeadingID") %>', 'imgdiv<%# Eval("HeadingID") %>');"
                                                                                                    style="text-decoration: none;">
                                                                                                    <img id="imgdiv<%# Eval("HeadingID") %>" alt="Show" border="0" src="../Images/plus.gif" />
                                                                                                </a>
                                                                                                <%# Eval("HeadingName") %>
                                                                                                <br />
                                                                                                <div id="div<%# Eval("HeadingID") %>" style="display: none; position: relative;">
                                                                                                    <asp:GridView runat="server" ID="gvLiaGroup" GridLines="None" AutoGenerateColumns="false"
                                                                                                        AllowPrintPaging="true" CssClass="myGridStyles" Width="90%" DataKeyNames="GroupID"
                                                                                                        Style="font-family: 'Trebuchet MS'; font-size: 15px;" OnRowDataBound="gvLiaGroup_RowDataBound"
                                                                                                        ShowFooter="false" ShowHeader="false">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                                                                                <ItemTemplate>
                                                                                                                    <a href="javascript:switchViews('dv<%# Eval("GroupID") %>', 'imdiv<%# Eval("GroupID") %>');"
                                                                                                                        style="text-decoration: none;">
                                                                                                                        <img id="imdiv<%# Eval("GroupID") %>" alt="Show" border="0" src="../Images/plus.gif" />
                                                                                                                    </a>
                                                                                                                    <%# Eval("GroupName") %>
                                                                                                                    <br />
                                                                                                                    <div id="dv<%# Eval("GroupID") %>" style="display: none; position: relative;">
                                                                                                                        <asp:GridView runat="server" BorderWidth="1" ID="gvLiaLedger" CssClass="myGridStyle"
                                                                                                                            GridLines="Both" AutoGenerateColumns="false" Width="90%" Style="font-family: 'Trebuchet MS';
                                                                                                                            font-size: 15px;">
                                                                                                                            <Columns>
                                                                                                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger Name" />
                                                                                                                                <asp:BoundField DataField="Debit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                                                                                    HeaderText="Debit" />
                                                                                                                                <asp:BoundField DataField="Credit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                                                                                    HeaderText="Credit" />
                                                                                                                            </Columns>
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 15px;" ID="lblSum" runat="server"
                                                                                                                        Text='<%# Eval("sum","{0:f2}") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 15px;" ID="lblSum" runat="server"
                                                                                                    Text='<%# Eval("sum","{0:f2}") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerTemplate>
                                                                                    </PagerTemplate>
                                                                                </asp:GridView>
                                                                                <asp:Label ID="lblNetProfit" Text="0" runat="server" Font-Bold="true"></asp:Label>
                                                                                <asp:Label ID="lblNetLoss" Text="0" runat="server" Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <asp:GridView runat="server" BorderWidth="0" ID="gvAssetBalance" GridLines="None"
                                                                                    AutoGenerateColumns="false" DataKeyNames="HeadingID" AllowPrintPaging="true"
                                                                                    Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 15px;" OnRowDataBound="gvAssetBalance_RowDataBound"
                                                                                    ShowFooter="false" ShowHeader="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                                                            <ItemTemplate>
                                                                                                <a href="javascript:switchViews('diva<%# Eval("HeadingID") %>', 'imgadiv<%# Eval("HeadingID") %>');"
                                                                                                    style="text-decoration: none;">
                                                                                                    <img id="imgadiv<%# Eval("HeadingID") %>" alt="Show" border="0" src="../Images/plus.gif" />
                                                                                                </a>
                                                                                                <%# Eval("HeadingName") %>
                                                                                                <br />
                                                                                                <div id="diva<%# Eval("HeadingID") %>" style="display: none; position: relative;">
                                                                                                    <asp:GridView runat="server" ID="gvAssetGroup" GridLines="None" AutoGenerateColumns="false"
                                                                                                        AllowPrintPaging="true" CssClass="myGridStyle" Width="90%" DataKeyNames="GroupID"
                                                                                                        Style="font-family: 'Trebuchet MS'; font-size: 15px;" OnRowDataBound="gvAssetGroup_RowDataBound"
                                                                                                        ShowFooter="false" ShowHeader="false">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                                                                                <ItemTemplate>
                                                                                                                    <a href="javascript:switchViews('dva<%# Eval("GroupID") %>', 'imadiv<%# Eval("GroupID") %>');"
                                                                                                                        style="text-decoration: none;">
                                                                                                                        <img id="imadiv<%# Eval("GroupID") %>" alt="Show" border="0" src="../Images/plus.gif" />
                                                                                                                    </a>
                                                                                                                    <%# Eval("GroupName") %>
                                                                                                                    <br />
                                                                                                                    <div id="dva<%# Eval("GroupID") %>" style="display: none; position: relative;">
                                                                                                                        <asp:GridView runat="server" ID="gvAssetLedger" CssClass="myGridStyle" GridLines="Both"
                                                                                                                            AutoGenerateColumns="false" Width="90%" Style="font-family: 'Trebuchet MS'; font-size: 15px;"
                                                                                                                            DataKeyNames="LedgerID">
                                                                                                                            <Columns>
                                                                                                                                <asp:BoundField DataField="LedgerName" ItemStyle-HorizontalAlign="Left" HeaderText="Ledger Name" />
                                                                                                                                <asp:BoundField DataField="Debit" ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"
                                                                                                                                    HeaderText="Debit" />
                                                                                                                                <asp:BoundField DataField="Credit" ItemStyle-HorizontalAlign="right" DataFormatString="{0:f2}"
                                                                                                                                    HeaderText="Credit" />
                                                                                                                            </Columns>
                                                                                                                        </asp:GridView>
                                                                                                                    </div>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 15px;" ID="lblSum" runat="server"
                                                                                                                        Text='<%# Eval("sum","{0:f2}") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </div>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 15px;" ID="lblSum" runat="server"
                                                                                                    Text='<%# Eval("sum","{0:f2}") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <PagerTemplate>
                                                                                    </PagerTemplate>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                         <tr>
                                                    <td valign="top" align="right">
                                                        <div id="pnlLib" visible="false" runat="server">
                                                            <i>Difference in Opening Balance : </i>&nbsp;<asp:Label ID="lblLib" runat="server"
                                                                CssClass="lblFont"></asp:Label></div>
                                                        &nbsp;
                                                    </td>
                                                    <td valign="top" align="right">
                                                        <div id="pnlAst" visible="false" runat="server">
                                                            <i>Difference in Opening Balance : </i>&nbsp;<asp:Label ID="lblAst" runat="server"
                                                                CssClass="lblFont"></asp:Label></div>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblCreditTotal" runat="server" Style="text-align: left"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label CssClass="tblLeft" ID="lblDebitTotal" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                            <tr>
                                                                <td>
                                                                    <table id="Table1" visible="false" runat="server" style="border: 1px solid Grey;
                                                                        height: 15px; background-color: #59d3b4; text-transform: uppercase" width="100%">
                                                                        <tr>
                                                                            <td align="center" style="font-size: small; width: 70px">
                                                                                LedgerName
                                                                            </td>
                                                                            <td align="center" style="font-size: small; width: 350px">
                                                                                Debit
                                                                            </td>
                                                                            <td align="center" style="font-size: small">
                                                                                Credit
                                                                            </td>
                                                                        </tr>
                                                            
                                                                    </table>
                                                    </div>
                                                   
                                                </div>
                                                 <tr>
                        <td class="lblFont" valign="top" align="left">
                            <a href="Pro_lossnew.aspx" style="font-size:large">Profit & Loss Account</a>
                        </td>
                        <td>
                        </td>
                    </tr>
                                               
                                                <tr>
                                                    <%--<td align="right">
                                                    <hr style="border-style: inset; border-width: 1px" />
                                                    <asp:Label ID="Label1" Text="Total :   " runat="server" align="center"></asp:Label>
                                                    <asp:Label CssClass="tblLeft" ID="lblCreditTotal" runat="server"></asp:Label>
                                                    <hr style="border-style: inset; border-width: 1px" />
                                                </td>
                                                <td align="right">
                                                    <hr style="border-style: inset; border-width: 1px" />
                                                    <asp:Label ID="Label2" Text="Total :   " runat="server" align="center"></asp:Label>
                                                    <asp:Label CssClass="tblLeft" ID="lblDebitTotal" runat="server"></asp:Label>
                                                    <hr style="border-style: inset; border-width: 1px" />
                                                </td>--%>
                                                </tr>
                                                <asp:GridView runat="server" ID="gvLedger" AutoGenerateColumns="false" OnRowCreated="gridPurchase_RowCreated"
                                                    Visible="false" AllowPrintPaging="true" OnRowDataBound="gvLedger_RowDataBound">
                                                    <RowStyle Height="19px" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTranDate" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField ItemStyle-VerticalAlign="Top" ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center"
                        DataField="Branchcode" HeaderText="Branch" />--%>
                                                        <asp:BoundField DataField="Particulars" ItemStyle-Width="35%" />
                                                        <asp:BoundField DataField="Narration" ItemStyle-Width="20%" />
                                                        <asp:BoundField ItemStyle-Width="13%" DataFormatString="{0:n}" DataField="Debit"
                                                            ItemStyle-HorizontalAlign="right" />
                                                        <asp:BoundField ItemStyle-Width="13%" DataFormatString="{0:n}" DataField="Credit"
                                                            ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="right" />
                                                    </Columns>
                                                    <PagerTemplate>
                                                    </PagerTemplate>
                                                </asp:GridView>
                                                <asp:GridView runat="server" ID="gvLed" Visible="false" AutoGenerateColumns="false"
                                                    OnRowCreated="gridPur_RowCreated" AllowPrintPaging="true" OnRowDataBound="gvLed_RowDataBound">
                                                    <RowStyle Height="19px" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTranDate" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField ItemStyle-VerticalAlign="Top" ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center"
                        DataField="Branchcode" HeaderText="Branch" />--%>
                                                        <asp:BoundField DataField="Particulars" ItemStyle-Width="35%" />
                                                        <asp:BoundField DataField="Narration" ItemStyle-Width="20%" />
                                                        <asp:BoundField ItemStyle-Width="13%" DataFormatString="{0:n}" DataField="Debit"
                                                            ItemStyle-HorizontalAlign="right" />
                                                        <asp:BoundField ItemStyle-Width="13%" DataFormatString="{0:n}" DataField="Credit"
                                                            ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="right" />
                                                    </Columns>
                                                    <PagerTemplate>
                                                    </PagerTemplate>
                                                </asp:GridView>
                                                <asp:GridView runat="server" Width="100%" ID="GridView1" ShowFooter="true" Visible="false"
                                                    GridLines="Both" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound"
                                                    DataKeyNames="LedgerID">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="Folionumber" HeaderText="LNO"  />--%>
                                                        <asp:BoundField DataField="LedgerName" ItemStyle-Width="52%" HeaderText="EXPENSES" />
                                                        <%--<asp:BoundField DataField="Branch" HeaderText="Branch"  />--%>
                                                        <%--  <asp:BoundField DataField="Debit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Credit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />--%>
                                                        <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblDebit" runat="server"
                                                                    Text='<%# Eval("Debit","{0:f2}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblCredit" runat="server"
                                                                    Text='<%# Eval("Credit","{0:f2}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="26%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lbltotal" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView runat="server" Width="100%" ID="GridView2" ShowFooter="true" Visible="false"
                                                    GridLines="Both" AutoGenerateColumns="false" OnRowDataBound="GridView2_RowDataBound"
                                                    DataKeyNames="LedgerID">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="Folionumber" HeaderText="LNO"  />--%>
                                                        <asp:BoundField DataField="LedgerName" ItemStyle-Width="52%" HeaderText="" />
                                                        <%--<asp:BoundField DataField="Branch" HeaderText="Branch"  />--%>
                                                        <%--  <asp:BoundField DataField="Debit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Credit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />--%>
                                                        <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblDebit" runat="server"
                                                                    Text='<%# Eval("Debit","{0:f2}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblCredit" runat="server"
                                                                    Text='<%# Eval("Credit","{0:f2}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="26%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lbltotal" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView runat="server" Width="100%" ID="gvLiaLedger1" Visible="false" ShowFooter="true"
                                                    GridLines="Both" AutoGenerateColumns="false" OnRowDataBound="gBalance1_RowDataBound"
                                                    DataKeyNames="LedgerID">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="Folionumber" HeaderText="LNO"  />--%>
                                                        <asp:BoundField DataField="LedgerName" ItemStyle-Width="52%" HeaderText="PURCHASE & SALES" />
                                                        <%--<asp:BoundField DataField="Branch" HeaderText="Branch"  />--%>
                                                        <%--  <asp:BoundField DataField="Debit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField DataField="Credit" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="right"
                                                                 HeaderStyle-HorizontalAlign="Center" />--%>
                                                        <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblDebit" runat="server"
                                                                    Text='<%# Eval("Debit","{0:f2}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="16%" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 14px;" ID="lblCredit" runat="server"
                                                                    Text='<%# Eval("Credit","{0:f2}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="26%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lbltotal" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                </table>
                                                <%--<button type="submit" class="btn btn-danger">Doc Print</button>--%>
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
                        </div>
                        <!-- /.row -->
                    </div>
                    <!-- /#page-wrapper -->
                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
    <!-- jQuery -->
</body>
</html>
