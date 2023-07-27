<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyerOrderBookingSummary.aspx.cs"
    Inherits="Billing.Accountsbootstrap.BuyerOrderBookingSummary" %>

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
    <title>BuyerOrder Booking Summary</title>
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
        function ReportPrint() {

            var gridData = document.getElementById('Excel');

            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
           'left=100,top=100,right=100,bottom=100,width=1100,height=1200');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');

            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();


        }
    </script>
</head>
<body>
    <usc:header id="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false">
    </asp:Label>
    <asp:Label runat="server" ID="FontSize" ForeColor="White" CssClass="label" Visible="false"
        Text="17"></asp:Label>
    <form runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-3">
                                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 20px;
                                    font-weight: bold">
                                    BuyerOrder Booking Summary
                                </h1>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlYears" runat="server" CssClass="chzn-select" Width="100%">
                                        <asp:ListItem Text="2020-2021" Value="2020-2021">
                                        </asp:ListItem>
                                        <asp:ListItem Text="2021-2022" Value="2021-2022">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="chzn-select" Width="100%">
                                        <asp:ListItem Text="All" Value="All">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Cancelled" Value="True">
                                        </asp:ListItem>
                                        <asp:ListItem Text="UnCancelled" Value="False">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                                    OnClick="btnSearch_OnClick" Width="110px" />
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-warning" Text="Excel"
                                    Width="110px" OnClick="btnExcel_OnClick" />
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btn" runat="server" Text="Print" CssClass="btn btn-info" OnClientClick="ReportPrint()"
                                    Width="110px" />
                            </div>
                        </div>
                        <div id="Excel" runat="server">
                            <div class="col-lg-12">
                                <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <asp:GridView ID="gvBuyerOrderBookingSummary" Caption="BuyerOrder Booking Summary"
                                    runat="server" EmptyDataText="No Records Found" Width="100%">
                                    <headerstyle backcolor="#59d3b4" borderstyle="Solid" borderwidth="1px" bordercolor="Gray"
                                        font-names="arial" font-size="Smaller" horizontalalign="Center" />
                                    <rowstyle font-names="arial" font-size="Larger" horizontalalign="Center" />
                                    <columns>
                                    </columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
