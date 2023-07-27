<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyerOrderSalesReport.aspx.cs"
    Inherits="Billing.Accountsbootstrap.BuyerOrderSalesReport" %>

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
    <title>BuyerOrder Details</title>
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
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="FontSize" ForeColor="White" CssClass="label" Visible="false"
        Text="17"> </asp:Label>
    <form runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 20px;
                                    font-weight: bold">
                                    BuyerOrder Sales Report
                                </h1>
                            </div>
                            <div class="col-lg-2">
                                <%--  <div class="form-group"><asp:RadioButtonList ID="rdbselect" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" >
                                      <asp:ListItem Value="1">Direct</asp:ListItem>
                                      <asp:ListItem Value="2">From Exec</asp:ListItem>

                                                          </asp:RadioButtonList></div>--%>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBuyerName" runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div></div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                                <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control center-block"
                                                    Width="100px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                                    PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                    CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                          
                                                <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control center-block" Width="100px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                                    PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                    CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </div>
                                </div>
                            </div>
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                                    OnClick="btnSearch_OnClick" Width="110px" />
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btn" runat="server" Text="Print" CssClass="btn btn-info" OnClientClick="ReportPrint()"
                                    Width="110px" />
                            </div>
                        </div>
                        <div id="Excel" runat="server">
                            <div class="col-lg-12">
                                <asp:Label ID="lblCaption" runat="server" Text="BuyerOrder ExcNo Details"></asp:Label>
                                <br /> <br />
                                <table>
                                    <tr>
                                        <td style="width: 30%" valign="top">
                                            <asp:GridView ID="gvBuyerOrderDetails" runat="server" EmptyDataText="No Records Found"
                                                AutoGenerateColumns="false" CssClass="myGridStyle" >    
                                               
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="InvoiceNo" DataField="FullInvoiceNo" />
                                            <asp:BoundField HeaderText="InvoiceDate" DataField="InvoiceDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="BuyerName" DataField="LedgerName" />
                                            <asp:BoundField HeaderText="Qty" DataField="Qty" ItemStyle-HorizontalAlign="Right" />
                                             <asp:BoundField HeaderText="Amount" DataField="Roundoff" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                            </Columns>
                                                </asp:GridView>
                                        </td>
                                        <td style="width: 70%">
                                            <%--<asp:GridView ID="gvBuyerOrderImages" runat="server" EmptyDataText="No Records Found"
                                                GridLines="None" AutoGenerateColumns="false" ShowHeader="false" Height="50px">
                                                <Columns>
                                                    <asp:ImageField DataImageUrlField="Sketch1" HeaderText="Sketch1" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Height="8px" />
                                                    <asp:ImageField DataImageUrlField="Sketch2" HeaderText="Sketch2" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:ImageField DataImageUrlField="Sketch3" HeaderText="Sketch3" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:ImageField DataImageUrlField="Sketch4" HeaderText="Sketch4" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:ImageField DataImageUrlField="Sketch5" HeaderText="Sketch5" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:ImageField DataImageUrlField="Sketch6" HeaderText="Sketch6" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:ImageField DataImageUrlField="Sketch7" HeaderText="Sketch7" ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                            </asp:GridView>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="width: 100%">
                                            <asp:GridView ID="gvBuyerOrderStyles" runat="server" EmptyDataText="No Records Found"
                                                Width="100%" ShowHeader="false">
                                                <Columns>
                                                </Columns>
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
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
