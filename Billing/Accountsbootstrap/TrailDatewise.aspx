<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrailDatewise.aspx.cs"
    Inherits="Billing.Accountsbootstrap.TrailDatewise" %>

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
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <title>Trail Page - bootsrap</title>
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <div class="row">
        <form id="Form1" runat="server" role="form">
        <div class="col-lg-12" style="margin-top: 6px">
            <div class="col-lg-2">
                <h3 class="page-header" style="text-align: left; color: #fe0002;">
                    Trail Balance Report</h3>
            </div>
            <div class="col-lg-2">
                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                <div class="form-group" runat="server" visible="true">
                    <label>
                        From Date</label>
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
            <div class="col-lg-2" style="margin-top: 23px">
                <div class="form-group">
                    <asp:Button ID="btnreport" runat="server" ValidationGroup="val1" class="btn btn-info"
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
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2">
                        </div>
                    </div>
                    <div>
                        <div class="row">
                            <!-- /.col-lg-12 -->
                        </div>
                        <!-- /.row -->
                        <div class="row" id="idt" visible="false" runat="server">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <!-- /.panel-heading -->
                                    <div class="panel-body">
                                        <table id="Table1" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;
                                            text-transform: uppercase" width="100%">
                                            <tr>
                                                <td align="center" style="font-size: small1; width: 1050px">
                                                    Particulars
                                                </td>
                                                <td align="center" style="font-size: small">
                                                    Debit(Rs)
                                                </td>
                                                <td align="center" style="font-size: small">
                                                    Credit(Rs)
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="height: 520px; overflow: scroll" align="center">
                                            <asp:GridView runat="server" Width="100%" CssClass="myGridStyle1" ID="gvTrailBalance"
                                                AutoGenerateColumns="false" DataKeyNames="GroupID" OnRowDataBound="gvTrailBalance_RowDataBound"
                                                ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <a href="javascript:switchViews('dv<%# Eval("GroupID") %>', 'imdiv<%# Eval("GroupID") %>');"
                                                                style="text-decoration: none;">
                                                                <img id="imdiv<%# Eval("GroupID") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                            </a>
                                                            <%# Eval("GroupName") %>
                                                            <div id="dv<%# Eval("GroupID") %>" style="display: none; position: relative;">
                                                                <asp:GridView runat="server" ID="gvLiaLedger" CssClass="myGridStyle" GridLines="Both"
                                                                    AutoGenerateColumns="false" DataKeyNames="LedgerID">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="Folionumber" HeaderText="LNO"  />--%>
                                                                        <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" />
                                                                        <%--<asp:BoundField DataField="Branch" HeaderText="Branch"  />--%>
                                                                        <asp:BoundField DataField="Debit" DataFormatString="{0:F}" ItemStyle-HorizontalAlign="right"
                                                                            HeaderText="Debit" HeaderStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Credit" DataFormatString="{0:F}" ItemStyle-HorizontalAlign="right"
                                                                            HeaderText="Credit" HeaderStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblDebit" runat="server"
                                                                Text='<%# Eval("Debit","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label Style="font-family: 'Trebuchet MS'; font-size: 11px;" ID="lblCredit" runat="server"
                                                                Text='<%# Eval("Credit","{0:f2}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div align="right">
                                            <asp:Button ID="Button3" runat="server" CssClass="btn btn-block center-block" Text="Print"
                                                Width="125px" OnClick="Button3_Click" />
                                        </div>
                                        <table>
                                            <tr>
                                                <td width="80%">
                                                    &nbsp;
                                                </td>
                                                <td width="10%" align="right">
                                                    <asp:Label ID="lblDebitTotal" runat="server" Visible="false"></asp:Label>
                                                </td>
                                                <td width="10%" align="right">
                                                    <asp:Label ID="lblCreditTotal" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
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
                </form>
                <!-- /#page-wrapper -->
            </div>
        </div>
    </div>
    </div> </div>
    <!-- jQuery -->
</body>
</html>
