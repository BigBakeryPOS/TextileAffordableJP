<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterCuttingDetailReport.aspx.cs" Inherits="Billing.Accountsbootstrap.MasterCuttingDetailReport" %>


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
    <title>MasterCuttingDetail Report </title>
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
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false">
    </asp:Label>
    <asp:Label runat="server" ID="FontSize" ForeColor="White" CssClass="label" Visible="false"
        Text="17"></asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1"></asp:Label>
    <asp:Label runat="server" ID="lblProcessforMasterId" Text="5" ForeColor="White" CssClass="label"
        Visible="false"></asp:Label>
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
                                <h1 class="page-header" style="text-align: left; color: #fe0002; font-size: 20px;
                                    font-weight: bold">
                                    MasterCutting Detail Report
                                </h1>
                                <div id="Div1" class="form-group" runat="server" visible="false">
                                    <label>
                                        Report Type:</label>
                                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="chzn-select" Width="100%">
                                        <asp:ListItem Text="Issued" Value="1">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Received" Value="2">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Damaged" Value="3">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Process :</label>
                                    <asp:DropDownList ID="ddlProcess" runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Jobwork Ledger
                                    </label>
                                    <asp:DropDownList ID="ddlProcessLedger" runat="server" CssClass="chzn-select" Style="height: 30px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group" id="ExcNo" runat="server" visible="true">
                                    <asp:TextBox ID="txtfabcontrast" runat="server" Width="100%" placeholder="Find ExcNo"
                                        onkeyup="SearchEmployees(this,'#chkExcNo');">
                                    </asp:TextBox>
                                    <div style="overflow-y: scroll; height: 100px">
                                        <div class="panel panel-default" style="width: 350px">
                                            <asp:CheckBoxList ID="chkExcNo" CssClass="chkChoice1" runat="server" RepeatLayout="Table"
                                                Style="overflow: auto">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group" id="Date" runat="server" visible="true">
                                    <asp:Label ID="lblDate" runat="server" Text="Entry Date" Width="110px" Style="font-weight: bold">
                                    </asp:Label>
                                    <br />
                                    <asp:CheckBox ID="chkUseDate" runat="server" />
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group" id="FromDate" runat="server" visible="true">
                                    <asp:Label ID="lblFrom" runat="server" Text="From" Width="110px" Style="font-weight: bold">
                                    </asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Width="110px">
                                    </asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                        PopupButtonID="txtOrderDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group" id="ToDate" runat="server" visible="true">
                                    <asp:Label ID="lblTo" runat="server" Text="To" Width="110px" Style="font-weight: bold">
                                    </asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="110px">
                                    </asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                        PopupButtonID="txtOrderDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                                    OnClick="btnSearch_OnClick" Width="110px" />
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-warning" Text="Excel"
                                    Width="110px" OnClick="btnExcel_OnClick" />
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btn" runat="server" Text="Print" CssClass="btn btn-info" OnClientClick="ReportPrint()"
                                    Width="110px" />
                            </div>
                             <div class="col-lg-1">
                                <asp:Button ID="Button1" runat="server" Text="Exit" CssClass="btn btn-info"  OnClick="btnExit_OnClick"
                                Width="110px" />
                            </div>
                        </div>
                        <div id="Excel" runat="server">
                            <div class="col-lg-12">
                                <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <asp:GridView ID="gvProductionReport" CssClass="" runat="server" Caption="Production Detail Report"
                                    EmptyDataText="No Records Found" Width="100%" AutoGenerateColumns="false" OnRowDataBound="gvProductionReport_OnRowDataBound" ShowFooter="true">
                                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                        Font-Names="arial" Font-Size="Smaller" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MasterCuttingNo" HeaderText="MasterCuttingNo" />
                                        <asp:BoundField DataField="DefaultDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="ExcNo" HeaderText="ExcNo" />
                                        <asp:BoundField DataField="StyleNo" HeaderText="StyleNo" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="Color" HeaderText="Color" FooterStyle-HorizontalAlign="Center"/>
                                        
                                        <asp:BoundField DataField="RecQty" HeaderText="RecQty" DataFormatString="{0:f0}"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="DmgQty" HeaderText="DmgQty" DataFormatString="{0:f0}"
                                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Center"/>
                                    </Columns>
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
