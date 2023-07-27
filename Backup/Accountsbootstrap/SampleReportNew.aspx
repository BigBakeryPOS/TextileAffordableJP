<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SampleReportNew.aspx.cs"
    Inherits="Billing.Accountsbootstrap.SampleReportNew" %>

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
    <title></title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <!-- Bootstrap Core CSS -->
    <link href="../css/responsive-tabs.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" href="../css/bootstrap.min.css" runat="server" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link id="Link2" href="../css/plugins/metisMenu/metisMenu.min.css" runat="server"
        rel="stylesheet" />
    <!-- Custom CSS -->
    <link id="Link3" href="../css/sb-admin-2.css" runat="server" rel="stylesheet" />
    <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
    <script src="../js/jquery.responsiveTabs.min.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.0.min.js" type="text/javascript"></script>
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link rel="stylesheet" href="../css/chosen.css" />
    <script type="text/javascript">
        function Denomination() {


            var gridData = document.getElementById('divoverall');


            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();


            var prtWindow = window.open(windowUrl, windowName,
        'left=100,top=100,right=100,bottom=100,width=700,height=500');
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
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="Form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <h3 style="text-align: left; color: #fe0002; margin-top: -10px">
        <asp:Label ID="lblTitle" Text="LOT Report" runat="server"></asp:Label></h3>
    <div class="row">
        <div class="col-lg-10">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>
                                    Lot No</label>
                                <asp:DropDownList ID="ddlLotNo" runat="server" OnSelectedIndexChanged="drpsamplechanged"
                                    AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="btnPrint" runat="server" class="btn btn-success" Text="Print" Style="margin-top: 25px;
                                width: 80px;" OnClientClick="Denomination()" />
                        </div>
                        <div class="col-lg-5">
                        </div>
                       
                        <!-- /.row -->
                        <div class="col-lg-12" id="divoverall" runat="server">
                            <div>
                                <label>
                                    Details for Lot No :</label><asp:Label runat="server" ID="lblLot"></asp:Label>
                                <div class="table-responsive" id="divLot1" runat="server">
                                    <div id="tabs" style="background-color: #D0D3D6; padding-left: 30px">
                                        <ul>
                                            <li><a href="#tabs-1">Stiching Details</a></li>
                                            <li><a href="#tabs-2">Kaja Details</a></li>
                                            <li><a href="#tabs-3">Embroiding Details</a></li>
                                            <li><a href="#tabs-6">Printing Details</a></li>
                                            <li><a href="#tabs-4">Washing Details</a></li>
                                            
                                            <li><a href="#tabs-5">Iron Details</a></li>
                                        </ul>
                                        <div class="row" id="tabs-1" style="background-color: White; padding-top: 30px">
                                            <div class="container">
                                                <div class="form-group">
                                                    <legend style="text-align: center">Stiching Details</legend>
                                                    <table style="width: 85%">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="Gridoverall" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                    ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                    ShowHeaderWhenEmpty="True">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Pattern" HeaderText="Pattern" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />


                                                                        
                                                                        <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                        
                                                                        <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="PerRate" HeaderText="Rate Per Quantity" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Rate" HeaderText="Total Rate" DataFormatString='{0:f}'
                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#990000" />
                                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                        NextPageText="Next" PreviousPageText="Previous" />
                                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="tabs-2" style="background-color: #D0D3D6; padding-top: 30px">
                                            <div style="background-color: #D0D3D6;">
                                                <h2 align="center">
                                                    <asp:Label ID="Label7" Style="color: Blue;" runat="server">Kaja Details</asp:Label></h2>
                                                <div class=" form-group">
                                                    <div class="table-responsive">
                                                        <table style="width: 85%">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="gridkaja" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                        ShowHeaderWhenEmpty="True">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Pattern" HeaderText="Pattern" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="PerRate" HeaderText="Rate Per Quantity" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Rate" HeaderText="Total Rate" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="#990000" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="tabs-3" style="background-color: #D0D3D6; padding-top: 30px">
                                            <div style="background-color: #D0D3D6;">
                                                <h2 align="center">
                                                    <asp:Label ID="Label8" Style="color: Blue;" runat="server">Embroiding Details</asp:Label></h2>
                                                <div class=" form-group">
                                                    <div class="table-responsive">
                                                        <table style="width: 85%">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="gridemb" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                        ShowHeaderWhenEmpty="True">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Pattern" HeaderText="Pattern" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="PerRate" HeaderText="Rate Per Quantity" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Rate" HeaderText="Total Rate" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="#990000" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="row" id="tabs-6" style="background-color: #D0D3D6; padding-top: 30px">
                                            <div style="background-color: #D0D3D6;">
                                                <h2 align="center">
                                                    <asp:Label ID="Label2" Style="color: Blue;" runat="server">Printing Details</asp:Label></h2>
                                                <div class=" form-group">
                                                    <div class="table-responsive">
                                                        <table style="width: 85%">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="Gridprintall" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                        ShowHeaderWhenEmpty="True">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Pattern" HeaderText="Pattern" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="PerRate" HeaderText="Rate Per Quantity" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Rate" HeaderText="Total Rate" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="#990000" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="tabs-4" style="background-color: #D0D3D6; padding-top: 30px">
                                            <div style="background-color: #D0D3D6;">
                                                <h2 align="center">
                                                    <asp:Label ID="Label9" Style="color: Blue;" runat="server">Washing Details</asp:Label></h2>
                                                <div class=" form-group">
                                                    <div class="table-responsive">
                                                        <table style="width: 85%">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="gridwash" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                        ShowHeaderWhenEmpty="True">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Pattern" HeaderText="Pattern" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="PerRate" HeaderText="Rate Per Quantity" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Rate" HeaderText="Total Rate" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="#990000" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                      
                                        <div class="row" id="tabs-5" style="background-color: #D0D3D6; padding-top: 30px">
                                            <div style="background-color: #D0D3D6;">
                                                <h2 align="center">
                                                    <asp:Label ID="Label10" Style="color: Blue;" runat="server">Iron and Packing Details</asp:Label></h2>
                                                <div class=" form-group">
                                                    <div class="table-responsive">
                                                        <table style="width: 85%">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="gridiron" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                        ShowHeaderWhenEmpty="True">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Pattern" HeaderText="Pattern" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                            
                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="PerRate" HeaderText="Rate Per Quantity" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="Rate" HeaderText="Total Rate" DataFormatString='{0:f}'
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="#990000" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <table class="table table-bordered table-striped">
                                    <tr>
                                        <td>
                                            <div id="divPurchaseInvoice" runat="server">
                                                <asp:GridView ID="PurchaseInvoiceGrid" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                    ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                    ShowHeaderWhenEmpty="True">
                                                    <Columns>
                                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="LotNo" HeaderText="Lot No" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="InvDate" HeaderText="Invoice Date" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Refno" HeaderText="Invoice no" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString='{0:f}'
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Designno" HeaderText="Design no" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="CheckedSign" HeaderText="Checked Sign" ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <HeaderStyle BackColor="#990000" />
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                        NextPageText="Next" PreviousPageText="Previous" />
                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="divAllreport" runat="server">
                                                <asp:GridView ID="gridPurchase" Width="90%" runat="server" EmptyDataText="Sorry Data Not Found!"
                                                    CssClass="myGridStyle" AutoGenerateColumns="false" OnRowCreated="gridPurchase_RowCreated"
                                                    OnRowDataBound="gridPurchase_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Item Name" DataField="itemname" />
                                                        <asp:BoundField HeaderText="Pattern Name" DataField="Patternname" />
                                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
                                                        <asp:BoundField HeaderText="Lot No" DataField="lotno" />
                                                        <asp:BoundField HeaderText="Work Status" DataField="CheckStatus" />
                                                        <asp:BoundField HeaderText="Received date" DataField="date" DataFormatString='{0:d}' />
                                                        <asp:BoundField HeaderText="Employee Name" Visible="false" DataField="name" />
                                                        <asp:BoundField HeaderText="Process Name" DataField="processtype" />
                                                        <asp:BoundField HeaderText="Qty" ItemStyle-HorizontalAlign="Right" DataField="qty"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField HeaderText="Damage Qty" ItemStyle-HorizontalAlign="Right" DataField="damageqty"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField HeaderText="Rate" ItemStyle-HorizontalAlign="Right" DataField="rate"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField HeaderText="Total Rate" ItemStyle-HorizontalAlign="Right" DataField="ratee"
                                                            DataFormatString='{0:f}' />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="selected_tab" runat="server" />
                                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
                                <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
                                <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
                                    rel="stylesheet" type="text/css" />
                                <script type="text/javascript">
                                    var selected_tab = 1;
                                    $(function () {
                                        var tabs = $("#tabs").tabs({
                                            select: function (e, i) {
                                                selected_tab = i.index;
                                            }
                                        });
                                        selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
                                        tabs.tabs('select', selected_tab);
                                        $("form").submit(function () {
                                            $("[id$=selected_tab]").val(selected_tab);
                                        });
                                    });
    
                                </script>
                                <div class="table-responsive" id="divprecutting" runat="server">
                                    <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px"
                                        width="90%" height="100px" class="style1">
                                        <tr>
                                            <td style="height: 1px">
                                                <table width="90%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                                                    class="style1">
                                                    <tr>
                                                        <td align="center">
                                                            <label style="font-size: large; font-weight: bold">
                                                                Pre-Cutting Process</label>
                                                            <asp:Label ID="lbbllott" Style="font-size: large; font-weight: bold" Visible="false"
                                                                runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="595px" height="100px" border="0" class="style1">
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <asp:Label ID="Label133" runat="server" Style="width: 500%; font-weight: bold">
                                Lot Number : </asp:Label>
                                                            <asp:Label ID="Label1" runat="server"></asp:Label><br />
                                                            <asp:Label ID="Label3" runat="server" Style="width: 100px; font-weight: bold">
                                Delivery date : </asp:Label>
                                                            <asp:Label ID="lblDeldate" runat="server"></asp:Label><br />
                                                        </td>
                                                        <td valign="top" align="left">
                                                            <asp:Label ID="Label4" runat="server" Style="width: 100px; font-weight: bold">
                                Width : </asp:Label>
                                                            <asp:Label ID="lblwidth" runat="server"></asp:Label><br />
                                                            <asp:Label ID="Label5" runat="server" Style="width: 100px; font-weight: bold">
                                Cutting Master : </asp:Label>
                                                            <asp:Label ID="lblcut" runat="server"></asp:Label><br />
                                                            <asp:Label Visible="false" ID="Label125" runat="server" Style="width: 100px; font-weight: bold">
                                Fit : </asp:Label>
                                                            <asp:Label ID="lblfit" Visible="false" runat="server"></asp:Label><br />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="80%" height="100px" border="0" class="style1">
                                                    <tr>
                                                        <td valign="top" align="left" style="width: 50%">
                                                            <label style="font-weight: bold">
                                                                Overall Lot Report</label>
                                                            <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myGridStyle"
                                                                GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                                ShowHeader="true" ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true"
                                                                Width="90%" OnRowDataBound="gridprint_RowDataBound" Style="font-family: 'Trebuchet MS';
                                                                font-size: 13px;">
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Fitt" HeaderText="Type/Size" HeaderStyle-Width="25px" />
                                                                    <asp:BoundField HeaderText="30" DataField="s30" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="32" DataField="s32" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="34" DataField="s34" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="36" DataField="s36" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="XS" DataField="sxs" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="S" DataField="ss" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="M" DataField="sm" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="L" DataField="sl" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="XL" DataField="sxl" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="XXL" DataField="sxxl" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="3XL" DataField="s3xl" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="4XL" DataField="s4xl" ItemStyle-HorizontalAlign="Center" />
                                                                    <%--  <asp:BoundField HeaderText="36HS" DataField="ts" />
                                        <asp:BoundField HeaderText="38HS" DataField="te" />
                                        <asp:BoundField HeaderText="39HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44HS" DataField="ffhs" />--%>
                                                                    <asp:BoundField HeaderText="Total" DataField="tot" ItemStyle-HorizontalAlign="Center" />
                                                                    <%--  <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:BoundField HeaderText="Total Shirt" DataField="tott" />--%>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                        <td valign="top" align="left" style="width: 20%">
                                                            <label style="font-weight: bold">
                                                                Cost Rate Calculation</label>
                                                            <div>
                                                                <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myGridStyle"
                                                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="90%" Style="font-family: 'Trebuchet MS';
                                                                    font-size: 13px;">
                                                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="Total Meter" DataFormatString='{0:f}' DataField="met" />
                                                                        <asp:BoundField Visible="false" HeaderText="Rate" DataFormatString='{0:f}' DataField="rat" />
                                                                        <asp:BoundField HeaderText="Total" DataFormatString='{0:f}' DataField="tot" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                        <td valign="top" align="left" style="width: 30%">
                                                            <label style="font-weight: bold">
                                                                Customer Labels Details</label>
                                                            <asp:GridView runat="server" BorderWidth="1" ID="gridlabel" CssClass="myGridStyle"
                                                                GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                                ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="90%" Style="font-family: 'Trebuchet MS';
                                                                font-size: 13px;">
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Party Name" DataField="cut" />
                                                                    <asp:BoundField HeaderText="Main Label" DataField="MainLabel" />
                                                                    <asp:BoundField HeaderText="Fit Label" DataField="flab" />
                                                                    <asp:BoundField HeaderText="Wash care label" DataField="wlab" />
                                                                    <asp:BoundField HeaderText="Logo Embrodeng" DataField="llab" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold">
                                                            <label>
                                                                Avg. Meter:</label>
                                                            <asp:Label ID="Lblvalue" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="font-weight: bold; font-size: 12px">
                                                            <label>
                                                                Fab.Rate +Prod.Cost(Rs:<asp:Label ID="lblmrp" runat="server"></asp:Label>):</label>
                                                            <asp:Label ID="lblratee" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table width="595px" height="643px" border="0.5px" class="style1">
                                                    <tr valign="top">
                                                        <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                                                            <label style="font-weight: bold">
                                                                Detailed Cutting Report</label>
                                                            <asp:GridView runat="server" Visible="false" BorderWidth="1" ID="gridprint" CssClass="myGridStyle"
                                                                GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                                ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="90%" Style="font-family: 'Trebuchet MS';
                                                                font-size: 13px;">
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="Party Name" DataField="brandname" ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="Fit" DataField="Fit" ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                                                        ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="30FS" DataField="30FS" />
                                                                    <asp:BoundField HeaderText="32FS" DataField="32FS" />
                                                                    <asp:BoundField HeaderText="34FS" DataField="34FS" />
                                                                    <asp:BoundField HeaderText="36FS" DataField="36FS" />
                                                                    <asp:BoundField HeaderText="XSFS" DataField="XSFS" />
                                                                    <asp:BoundField HeaderText="SFS" DataField="SFS" />
                                                                    <asp:BoundField HeaderText="MFS" DataField="MFS" />
                                                                    <asp:BoundField HeaderText="LFS" DataField="LFS" />
                                                                    <asp:BoundField HeaderText="XLFS" DataField="XLFS" />
                                                                    <asp:BoundField HeaderText="XXLFS" DataField="XXLFS" />
                                                                    <asp:BoundField HeaderText="3XLFS" DataField="3XLFS" />
                                                                    <asp:BoundField HeaderText="4XLFS" DataField="4XLFS" />
                                                                    <asp:BoundField HeaderText="30HS" DataField="30HS" />
                                                                    <asp:BoundField HeaderText="32HS" DataField="32HS" />
                                                                    <asp:BoundField HeaderText="34HS" DataField="34HS" />
                                                                    <asp:BoundField HeaderText="36HS" DataField="36HS" />
                                                                    <asp:BoundField HeaderText="XSHS" DataField="XSHS" />
                                                                    <asp:BoundField HeaderText="SHS" DataField="SHS" />
                                                                    <asp:BoundField HeaderText="MHS" DataField="MHS" />
                                                                    <asp:BoundField HeaderText="LHS" DataField="LHS" />
                                                                    <asp:BoundField HeaderText="XLHS" DataField="XLHS" />
                                                                    <asp:BoundField HeaderText="XXLHS" DataField="XXLHS" />
                                                                    <asp:BoundField HeaderText="3XLHS" DataField="3XLHS" />
                                                                    <asp:BoundField HeaderText="4XLHS" DataField="4XLHS" />
                                                                    <asp:BoundField HeaderText="Total FS" DataField="TotFS" />
                                                                    <asp:BoundField HeaderText="Total HS" DataField="TotHS" />
                                                                    <asp:BoundField HeaderText="Total Qty" DataField="Qty" />
                                                                    <asp:BoundField HeaderText="Avg Meter" Visible="false" DataFormatString='{0:f}' DataField="AvgMtr" />
                                                                    <asp:BoundField HeaderText="Avg Rate" Visible="false" DataFormatString='{0:f}' DataField="AvgRate" />
                                                                    <asp:BoundField HeaderText="Margin" Visible="false" DataFormatString='{0:f}' DataField="MarginRAte" />
                                                                    <asp:BoundField HeaderText="WSP" Visible="false" DataFormatString='{0:f}' DataField="MRPRat" />
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:GridView runat="server" BorderWidth="1" ID="gridnewprint" CssClass="myGridStyle"
                                                                GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                                ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="80%" OnRowCreated="gridnewprint_RowCreated"
                                                                OnRowDataBound="gridnewprint_RowDataBound" Style="font-family: 'Trebuchet MS';
                                                                font-size: 13px;">
                                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="4%" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="Party Name" DataField="brandname" ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="Fit" DataField="Fit" ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                                                        ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                                                        ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                                                    <asp:BoundField HeaderText="30FS" DataField="30FS" />
                                                                    <asp:BoundField HeaderText="32FS" DataField="32FS" />
                                                                    <asp:BoundField HeaderText="34FS" DataField="34FS" />
                                                                    <asp:BoundField HeaderText="36FS" DataField="36FS" />
                                                                    <asp:BoundField HeaderText="XSFS" DataField="XSFS" />
                                                                    <asp:BoundField HeaderText="SFS" DataField="SFS" />
                                                                    <asp:BoundField HeaderText="MFS" DataField="MFS" />
                                                                    <asp:BoundField HeaderText="LFS" DataField="LFS" />
                                                                    <asp:BoundField HeaderText="XLFS" DataField="XLFS" />
                                                                    <asp:BoundField HeaderText="XXLFS" DataField="XXLFS" />
                                                                    <asp:BoundField HeaderText="3XLFS" DataField="3XLFS" />
                                                                    <asp:BoundField HeaderText="4XLFS" DataField="4XLFS" />
                                                                    <asp:BoundField HeaderText="30HS" DataField="30HS" />
                                                                    <asp:BoundField HeaderText="32HS" DataField="32HS" />
                                                                    <asp:BoundField HeaderText="34HS" DataField="34HS" />
                                                                    <asp:BoundField HeaderText="36HS" DataField="36HS" />
                                                                    <asp:BoundField HeaderText="XSHS" DataField="XSHS" />
                                                                    <asp:BoundField HeaderText="SHS" DataField="SHS" />
                                                                    <asp:BoundField HeaderText="MHS" DataField="MHS" />
                                                                    <asp:BoundField HeaderText="LHS" DataField="LHS" />
                                                                    <asp:BoundField HeaderText="XLHS" DataField="XLHS" />
                                                                    <asp:BoundField HeaderText="XXLHS" DataField="XXLHS" />
                                                                    <asp:BoundField HeaderText="3XLHS" DataField="3XLHS" />
                                                                    <asp:BoundField HeaderText="4XLHS" DataField="4XLHS" />
                                                                    <asp:BoundField HeaderText="Total FS" DataField="TotFS" />
                                                                    <asp:BoundField HeaderText="Total HS" DataField="TotHS" />
                                                                    <asp:BoundField HeaderText="Total Qty" DataField="Qty" />
                                                                    <%--<asp:BoundField HeaderText="Avg Meter" Visible="false" DataFormatString='{0:f}' DataField="AvgMtr" />
                                        <asp:BoundField HeaderText="Avg Rate" Visible="false" DataFormatString='{0:f}' DataField="AvgRate" />
                                        <asp:BoundField HeaderText="Margin" Visible="false" DataFormatString='{0:f}' DataField="MarginRAte" />
                                        <asp:BoundField HeaderText="WSP" Visible="false" DataFormatString='{0:f}' DataField="MRPRat" />--%>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <asp:Button ID="btnExport" Visible="false" Text="Export to Excel" runat="server"
                                        CssClass="btn btn-success" Height="37px" OnClick="btnExport_Click" /></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                </div>
                <!-- /#page-wrapper -->
            </div>
        </div>
        <div class="col-lg-2" runat="server" id="reportDetails">
                            <div class="form-group">
                                <asp:Button ID="btnAllLotProcess" class="btn btn-warning" runat="server" Style="width: 180px;"
                                    Text="All" OnClick="Process_Details" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnInv" class="btn btn-info" runat="server" Style="width: 180px;"
                                    Text="Purchase Invoice Details" OnClick="Process_Details" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnCutting" class="btn btn-success" runat="server" Style="width: 180px;"
                                    Text="Pre - Cutting Details" OnClick="PreCutting_GetDetails" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnStitching" class="btn btn-primary" runat="server" Style="width: 180px;"
                                    Text="Stitching Details" OnClick="Process_Details" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnKaja" class="btn btn-info" runat="server" Style="width: 180px;"
                                    Text="Kaja Details" OnClick="Process_Details" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnEmb" class="btn btn-warning" runat="server" Style="width: 180px;"
                                    Text="Embroiding Details" OnClick="Process_Details" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnprinting" class="btn btn-danger" runat="server" Style="width: 180px;"
                                    Text="Priting Details" OnClick="Process_Details" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnWashing" class="btn btn-danger" runat="server" Style="width: 180px;"
                                    Text="Washing Details" OnClick="Process_Details" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnIron" class="btn btn-success" runat="server" Style="width: 180px;"
                                    Text="Iron & Packing Details" OnClick="Process_Details" />
                            </div>
                        </div>
    </div>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    </form>
</body>
</html>
