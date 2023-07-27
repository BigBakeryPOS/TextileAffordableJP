
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseGRNReport.aspx.cs"
    Inherits="Billing.Accountsbootstrap.PurchaseGRNReport" %>

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
    <title>Purchase GRN Report</title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../Styles/chosen.css" rel="Stylesheet" />
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
        function SearchEmployees(txtSearch, cblEmployees) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html((count) + ' match');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
        }
    </script>
    <script type="text/javascript">
        function ReportPrint() {

            var gridData = document.getElementById('Excel');

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
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1,5"> </asp:Label>
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
                                    Purchase GRN Report</h1>
                                <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" Width="100%"
                                    Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_OnSelectedIndexChanged">
                                    <asp:ListItem Text="Buyer Order Sheet" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Month Wise Order List" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Shipment Wise Summary" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Shipment Wise Details" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Exc. Wise Rate of Style" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Style Average Sheet" Value="6"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2" id="BuyerCode" runat="server" visible="true">
                                <div class="form-group">
                                    <label>
                                        Party Code:</label>
                                    <asp:DropDownList ID="ddlPartyCode" runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Item :</label>
                                    <asp:DropDownList ID="ddlItem" runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="Div1" class="col-lg-2" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Party Name:</label>
                                    <asp:DropDownList ID="ddlPartyName" runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group" id="ExcNo" runat="server" visible="true">
                                    <asp:TextBox ID="txtfabcontrast" runat="server" Width="100%" placeholder="Find RecNo"
                                        onkeyup="SearchEmployees(this,'#chkRecPONo');"></asp:TextBox>
                                    <div style="overflow-y: scroll; height: 100px">
                                        <div class="panel panel-default" style="width: 350px">
                                            <asp:CheckBoxList ID="chkRecPONo" CssClass="chkChoice1" runat="server" RepeatLayout="Table"
                                                Style="overflow: auto">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" id="AccountingYear" runat="server" visible="false">
                                    <label>
                                        Accounting Year:</label>
                                    <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" Width="110px" MaxLength="4"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                        FilterType="Numbers" TargetControlID="txtYear" />
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group" id="Date" runat="server" visible="true">
                                    <asp:Label ID="lblDate" runat="server" Text="Date" Width="110px" Style="font-weight: bold"></asp:Label>
                                    <br />
                                    <asp:CheckBox ID="chkUseDate" runat="server" />
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group" id="FromDate" runat="server" visible="true">
                                    <asp:Label ID="lblFrom" runat="server" Text="From" Width="110px" Style="font-weight: bold"></asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                        PopupButtonID="txtOrderDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group" id="ToDate" runat="server" visible="true">
                                    <asp:Label ID="lblTo" runat="server" Text="To" Width="110px" Style="font-weight: bold"></asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                        PopupButtonID="txtOrderDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search"
                                    OnClick="btnSearch_OnClick" Width="125px" />
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-primary" Text="Excel"
                                    OnClick="btnExcel_OnClick" Width="125px" />
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btn" runat="server" Text="Print" CssClass="btn btn-info" OnClientClick="ReportPrint()"
                                    Width="125px" />
                            </div>
                        </div>
                        <div id="Excel" runat="server">
                            <asp:GridView ID="gvOrderReport" runat="server" CssClass="myGridStyle1" EmptyDataText="No Records Found" ShowFooter="true" OnRowDataBound="gvOrderReport_OnRowDataBound"
                                Width="100%" AutoGenerateColumns="false" Caption="Purchase GRN Report">
                                <HeaderStyle BackColor="White" />
                                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    NextPageText="Next" PreviousPageText="Previous" />
                                <Columns>
                                    <asp:BoundField HeaderText="ChallanNo " DataField="ChallanNo" />
                                    <asp:BoundField HeaderText="RecNo " DataField="FullRecPONo" />
                                    <asp:BoundField HeaderText="Date " DataField="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="OrderNo " DataField="FullPONo" />
                                    <asp:BoundField HeaderText="Party " DataField="Party" />
                                    <asp:BoundField HeaderText="PurchaseFor " DataField="PurchaseFor"  Visible="false" />
                                    <asp:BoundField HeaderText="Exc. " DataField="purchasefortype" />
                                    <asp:BoundField HeaderText="Item " DataField="Item" />
                                    <asp:BoundField HeaderText="Color " DataField="Color" />
                                    <asp:BoundField HeaderText="Qty " DataField="Qty" Visible="false"  DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="Shrink " DataField="Shrink" Visible="false"  DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="TotalQty " DataField="TotalQty" Visible="false"  DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="Rate " DataField="Rate"  DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="RecQty " DataField="RecQty"  DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="Amount " DataField="Amount" Visible="false" />
                                    <asp:BoundField HeaderText="Remarks " DataField="Remarks" Visible="false" />
                                     <asp:BoundField HeaderText=" " DataField="Units" />
                                </Columns>
                                <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            </asp:GridView>
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
