<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductionStatus.aspx.cs"
    Inherits="Billing.Accountsbootstrap.ProductionStatus" %>

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
    <title>Production Status </title>
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
        var ScrollHeight = 350;
        window.onload = function () {
            var grid = document.getElementById('gvCuttingQtyDetails1');
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }
    </script>
    <script type="text/javascript">
        function ReportPrint() {

            var gridData = document.getElementById('gvCuttingQtyDetails');

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
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1"></asp:Label>
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
                                    Production Status
                                </h1>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Buyer Code:</label>
                                    <asp:DropDownList ID="ddlBuyerCode" runat="server" CssClass="chzn-select" Width="100%">
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
                                    <asp:Label ID="lblDate" runat="server" Text="Order Date" Width="110px" Style="font-weight: bold">
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
                        </div>
                        <div id="Excel" runat="server">
                            <asp:GridView ID="gvCuttingQtyDetails" runat="server" EmptyDataText="No Records Found"  OnRowDataBound="gvCuttingQtyDetails_OnRowDataBound"
                                Caption="Production Status" GridLines="Both" Width="100%" AutoGenerateColumns="false">
                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                    Font-Names="arial" Font-Size="Smaller" />
                                <Columns>
                                    <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                    <asp:BoundField DataField="Process" HeaderText="Process" />
                                    <asp:BoundField DataField="Style" HeaderText="Style" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="OrderQty" HeaderText="OrderQty" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="IssQty" HeaderText="IssQty" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="RecQty" HeaderText="RecQty" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="DmgQty" HeaderText="DmgQty" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="TtlQty" HeaderText="TtlQty" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="RecBalQty" HeaderText="RecBalQty" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="IssBalQty" HeaderText="IssBalQty" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="QtyDate" HeaderText="QtyDate" />
                                </Columns>
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
