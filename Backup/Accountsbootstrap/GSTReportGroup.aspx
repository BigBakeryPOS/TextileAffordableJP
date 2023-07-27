<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GSTReportGroup.aspx.cs"
    Inherits="Billing.Accountsbootstrap.GSTReportGroup" %>

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
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <title>GST Page - bootsrap</title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
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
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <div class="col-lg-2">
                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 20px">
                    GST Report</h1>
            </div>
            <div class="col-lg-2">
                <form id="Form1" runat="server" role="form">
                <div class="form-group">
                    <label>
                        From Date</label>
                    <asp:TextBox CssClass="form-control" ID="txtfrmdate" runat="server" Text="Select Date"></asp:TextBox>
                </div>
                <ajaxToolkit:CalendarExtender ID="txtfrmdate1" TargetControlID="txtfrmdate" Format="dd/MM/yyyy"
                    runat="server" CssClass="cal_Theme1">
                </ajaxToolkit:CalendarExtender>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        To Date</label>
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
                        <%--  <asp:ListItem Text="CO1" Value="CO1"></asp:ListItem>
                        <asp:ListItem Text="CO2" Value="CO2"></asp:ListItem>
                        <asp:ListItem Text="CO3" Value="CO3"></asp:ListItem>
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                </div>
                <div class="form-group">
                    <asp:Button ID="btnreport" runat="server" class="btn btn-info" Text="Generate Report"
                        Style="width: 160px;" OnClick="btnreport_Click" />
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
                    <asp:button id="btnprint" runat="server" cssclass="btn btn-block center-block" text="Print"
                        width="125px" onclientclick="javascript:CallPrint('bill');" xmlns:asp="#unknown" />
                </div>
                <div class="panel-body" id="bill">
                    <div class="row">
                        <div class="col-lg-12">
                            <div id="Div3">
                                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                <h2 align="center">
                                    <asp:Label ID="lblMessage" Style="color: Blue;" runat="server"></asp:Label></h2>
                                <div>
                                    <div class="row">
                                        <!-- /.col-lg-12 -->
                                    </div>
                                    <!-- /.row -->
                                    <div class="row" id="idt" visible="true" runat="server">
                                        <div class="col-lg-12">
                                            <div align="center">
                                                <asp:GridView runat="server" Width="100%" ID="gvCash" GridLines="Both" ShowFooter="true"
                                                    AutoGenerateColumns="false" AllowPrintPaging="true" ShowHeader="true" OnRowDataBound="gvCash_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="SalesID" HeaderText="Sales ID" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                        <asp:BoundField DataField="Without GST" HeaderStyle-HorizontalAlign="Right" HeaderText="Without GST"
                                                            DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField DataField="With GST" HeaderStyle-HorizontalAlign="Right" HeaderText="With GST"
                                                            DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                                    </Columns>
                                                    <PagerTemplate>
                                                    </PagerTemplate>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <table id="Tablee1" runat="server" visible="false" style="width: 100%">
                                                <tr>
                                                    <td width="66%" align="right">
                                                        Opening Balance
                                                    </td>
                                                    <td width="11%" align="right">
                                                        <asp:Label ID="lblOBDR" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td width="11%" align="right">
                                                        <asp:Label ID="lblOBCR" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td width="7%" align="right">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60%" align="right">
                                                        Total
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblDebitSum" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblCreditSum" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60%" align="right">
                                                        Closing Balance
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblDebitDiff" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblCreditDiff" runat="server" Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
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
                </div>
                <!-- /.row -->
            </div>
            <!-- /#page-wrapper -->
        </div>
    </div>
    </div> </div> </div>
    <!-- jQuery -->
    </form>
</body>
</html>
