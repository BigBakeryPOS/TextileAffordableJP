<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home_Page.aspx.cs" Inherits="Billing.Accountsbootstrap.Home_Page" %>

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
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <title>Home</title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <asp:Label ID="lblTitle" runat="server" Text="Nandhini Nighties & Inskirts" Style="font-size: x-large;
            font-weight: bold; color: #336699"></asp:Label>
    </center>
    <div class="col-lg-12">
        <div class="col-lg-4">
            <div class="panel panel">
                <div class="panel-heading" style="background: #ab9e8c">
                    <div class="row">
                        <div align="center">
                            <h3 style="color: White">
                                BuyerOrder Details!</h3>
                            <br />
                        </div>
                        <div class="col-xs-12">
                            <div>
                                <asp:GridView ID="gvOrderDetails" runat="server" EmptyDataText="No records Found"
                                    AutoGenerateColumns="false" Width="100%">
                                    <HeaderStyle ForeColor="Black" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px"
                                        BorderColor="Gray" Font-Names="arial" Font-Size="Large" />
                                    <RowStyle ForeColor="Black" Font-Names="arial" Font-Size="Larger" />
                                    <Columns>
                                        <asp:BoundField DataField="SNo" HeaderText="SNo" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Counts" HeaderText="Counts" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="BuyerOrderReport.aspx">
                    <div class="panel-footer" style="background: #ab9e8c; color: White">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
            <div class="panel panel">
                <div class="panel-heading" style="background: #999">
                    <div class="row">
                        <div align="center">
                            <h3 style="color: White">
                                Cutting Details!</h3>
                        </div>
                        <div class="col-xs-12">
                            <div>
                                <asp:GridView ID="gvCuttingDetails" runat="server" EmptyDataText="No records Found"
                                    RowStyle-ForeColor="Black" AutoGenerateColumns="false" Width="100%">
                                    <HeaderStyle ForeColor="Black" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px"
                                        BorderColor="Gray" Font-Names="arial" Font-Size="Large" />
                                    <RowStyle ForeColor="Black" Font-Names="arial" Font-Size="Larger" />
                                    <Columns>
                                        <asp:BoundField DataField="SNo" HeaderText="SNo" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Counts" HeaderText="Counts" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="RecQty" HeaderText="RecQty" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="DmgQty" HeaderText="DmgQty" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="CuttingDetails.aspx">
                    <div class="panel-footer" style="background: #999; color: White">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="panel panel">
                <div class="panel-heading" style="background: #da9f9d">
                    <div class="row">
                        <div align="center">
                            <h3 style="color: White">
                                Purchase Order Details!</h3>
                        </div>
                        <div class="col-xs-12">
                            <div>
                                <asp:GridView ID="gvPo" runat="server" EmptyDataText="No records Found" RowStyle-ForeColor="Black"
                                    AutoGenerateColumns="false" Width="100%">
                                    <HeaderStyle ForeColor="Black" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px"
                                        BorderColor="Gray" Font-Names="arial" Font-Size="Large" />
                                    <RowStyle ForeColor="Black" Font-Names="arial" Font-Size="Larger" />
                                    <Columns>
                                        <asp:BoundField DataField="SNo" HeaderText="SNo" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Counts" HeaderText="Counts" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="PurchaseOrderReport.aspx">
                    <div class="panel-footer" style="background: #da9f9d; color: White">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
            <div class="panel panel">
                <div class="panel-heading" style="background: #d6c595">
                    <div class="row">
                        <div align="center">
                            <h3 style="color: White">
                                ItemProcess Issue Details!</h3>
                        </div>
                        <div class="col-xs-12">
                            <div>
                                <asp:GridView ID="gvItemProcessIssueDetails" runat="server" EmptyDataText="No records Found"
                                    RowStyle-ForeColor="Black" AutoGenerateColumns="false" Width="100%">
                                    <HeaderStyle ForeColor="Black" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px"
                                        BorderColor="Gray" Font-Names="arial" Font-Size="Large" />
                                    <RowStyle ForeColor="Black" Font-Names="arial" Font-Size="Larger" />
                                    <Columns>
                                        <asp:BoundField DataField="SNo" HeaderText="SNo" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Counts" HeaderText="Counts" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="ItemProcessOrderReport.aspx">
                    <div class="panel-footer" style="background: #d6c595; color: White">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
            <div class="panel panel">
                <div class="panel-heading" style="background: #96b988eb">
                    <div class="row">
                        <div align="center">
                            <h3 style="color: White">
                                Process Issue Details!</h3>
                        </div>
                        <div class="col-xs-12 ">
                            <div>
                                <asp:GridView ID="gvProcessIssueDetails" runat="server" EmptyDataText="No Records Found!"
                                    RowStyle-ForeColor="Black" AutoGenerateColumns="false" Width="100%">
                                    <HeaderStyle ForeColor="Black" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px"
                                        BorderColor="Gray" Font-Names="arial" Font-Size="Large" />
                                    <RowStyle ForeColor="Black" Font-Names="arial" Font-Size="Larger" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Process" HeaderText="Process" />
                                        <asp:BoundField DataField="Counts" HeaderText="Counts" />
                                        <asp:BoundField DataField="Issue" HeaderText="Issue" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="ChallanOut.aspx">
                    <div class="panel-footer" style="background: #96b988eb; color: White">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="panel panel">
                <div class="panel-heading" style="background: #79adda">
                    <div class="row">
                        <div align="center">
                            <h3 style="color: White">
                                Purchase GRN Details!</h3>
                        </div>
                        <div class="col-xs-12 ">
                            <div>
                                <asp:GridView ID="gvPurchase" runat="server" EmptyDataText="No records Found" RowStyle-ForeColor="Black"
                                    AutoGenerateColumns="false" Width="100%">
                                    <HeaderStyle ForeColor="Black" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px"
                                        BorderColor="Gray" Font-Names="arial" Font-Size="Large" />
                                    <RowStyle ForeColor="Black" Font-Names="arial" Font-Size="Larger" />
                                    <Columns>
                                        <asp:BoundField DataField="SNo" HeaderText="SNo" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Counts" HeaderText="Counts" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"/>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="PurchaseGRNReport.aspx">
                    <div class="panel-footer" style="background: #79adda; color: White">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
            <div class="panel panel">
                <div class="panel-heading" style="background: #a897a9">
                    <div class="row">
                        <div align="center">
                            <h3 style="color: White">
                                ItemProcess Receive Details!</h3>
                        </div>
                        <div class="col-xs-12 ">
                            <div>
                                <asp:GridView ID="gvItemProcessReceiveDetails" runat="server" EmptyDataText="No records Found"
                                    RowStyle-ForeColor="Black" AutoGenerateColumns="false" Width="100%">
                                    <HeaderStyle ForeColor="Black" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px"
                                        BorderColor="Gray" Font-Names="arial" Font-Size="Large" />
                                    <RowStyle ForeColor="Black" Font-Names="arial" Font-Size="Larger" />
                                    <Columns>
                                        <asp:BoundField DataField="SNo" HeaderText="SNo" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Counts" HeaderText="Counts" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="ItemProcessReceiveReport.aspx">
                    <div class="panel-footer" style="background: #a897a9; color: White">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
            <div class="panel panel">
                <div class="panel-heading" style="background: #7fb1aceb">
                    <div class="row">
                        <div align="center">
                            <h3 style="color: White">
                                Process Receive Details!</h3>
                        </div>
                        <div class="col-xs-12 ">
                            <div>
                                <asp:GridView ID="gvProcessReceiveDetails" runat="server" EmptyDataText="No Records Found!"
                                    RowStyle-ForeColor="Black" AutoGenerateColumns="false" Width="100%">
                                    <HeaderStyle ForeColor="Black" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px"
                                        BorderColor="Gray" Font-Names="arial" Font-Size="Large" />
                                    <RowStyle ForeColor="Black" Font-Names="arial" Font-Size="Larger" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Process" HeaderText="Process" />
                                        <asp:BoundField DataField="Counts" HeaderText="Counts" />
                                        <asp:BoundField DataField="Receive" HeaderText="Receive" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Damage" HeaderText="Damage" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="ChallanIn.aspx">
                    <div class="panel-footer" style="background: #7fb1aceb; color: White">
                        <span class="pull-left">View Details</span> <span class="pull-right"><i class="fa fa-arrow-circle-right">
                        </i></span>
                        <div class="clearfix">
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
