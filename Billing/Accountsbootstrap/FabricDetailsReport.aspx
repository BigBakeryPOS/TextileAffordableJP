<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FabricDetailsReport.aspx.cs"
    Inherits="Billing.Accountsbootstrap.FabricDetailsReport" %>

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
    <title>Fabric Full Detail Report</title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
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
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <style type="text/css">
        .GroupHeaderStyle
        {
            background-color: #afc3dd;
            color: Black;
            font-weight: bold;
            text-transform: uppercase;
        }
        .SubTotalRowStyle
        {
            background-color: #cccccc;
            color: Black;
            font-weight: bold;
        }
        .GrandTotalRowStyle
        {
            background-color: #000000;
            color: white;
            font-weight: bold;
        }
        .align1
        {
            text-align: right;
        }
        
        .myGridStyle1 tr th
        {
            padding: 8px;
            color: #afc3dd;
            background-color: #000000;
            border: 1px solid gray;
            font-family: Arial;
            font-weight: bold;
            text-align: center;
            text-transform: uppercase;
        }
        
        
        
        
        
        .myGridStyle1 tr:nth-child(even)
        {
            background-color: #ffffff;
        }
        
        
        
        .myGridStyle1 tr:nth-child(odd)
        {
            background-color: #ffffff;
        }
        
        
        
        .myGridStyle1 td
        {
            border: 1px solid gray;
            padding: 8px;
        }
    </style>
    <script type="text/javascript">
        function Denomination() {


            var gridData = document.getElementById('gridcatqty1');



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
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <usc:Header ID="Header" runat="server" />
    <form id="form1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Fabric Full Detail Report</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="Label3" runat="server">Company</asp:Label><br />
                                        <asp:DropDownList ID="ddlcompany" runat="server" CssClass="form-control" Width="180px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="Label2" runat="server">Type</asp:Label><br />
                                        <asp:DropDownList ID="ddltype" runat="server" Width="150px" CssClass="form-control">
                                            <asp:ListItem Text="Summary" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Detailed" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <div class="form-group">
                                            <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control center-block"
                                                Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                                PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                        <div class="form-group">
                                            <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control center-block" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                                PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblsupplier" runat="server">Supplier Name</asp:Label><br />
                                        <asp:DropDownList ID="ddlsupplier" runat="server" class="chzn-select" Width="220px"
                                            Height="80px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Label ID="Label5" runat="server">Search</asp:Label><br />
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success"
                                            OnClick="btnsearch_OnClick" Width="100px" />
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Label ID="lblPrint" runat="server">Print</asp:Label><br />
                                        <asp:Button ID="btn" runat="server" Text="Print" CssClass="btn btn-danger" OnClientClick="Denomination()"
                                            Width="100px" />
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Label ID="Label7" runat="server">Export Excel</asp:Label><br />
                                        <asp:Button ID="btnExport" class="btn btn-warning" Text="Excel" runat="server" Width="100px"
                                            OnClick="btnexcel_OnClick" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="row" runat="server" visible="true">
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-2">
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="Label4" runat="server">Fab Mode</asp:Label><br />
                                        <asp:DropDownList ID="ddlfabmode" runat="server" Width="180px" CssClass="form-control">
                                            <asp:ListItem Text="ALL" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="BODY" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="CONTRAST" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1">
                                        <div class="form-group">
                                            <asp:RadioButton ID="rdbboth" runat="server" Text="Both" CssClass="center-block"
                                                GroupName="a" Checked="true" />
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                        <div class="form-group">
                                            <asp:RadioButton ID="rdbfinished" runat="server" Text="Finished" CssClass="center-block"
                                                GroupName="a" />
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <asp:RadioButton ID="rdbunfinished" runat="server" Text="UnFinished" CssClass="center-block"
                                                GroupName="a" />
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                </div>
                            </div>
                            <div id="div2" runat="server">
                                <div class="col-lg-12">
                                    <table class="table table-bordered table-striped">
                                        <tr>
                                            <td runat="server" visible="true">
                                                <asp:GridView ID="gridcatqty" runat="server" EmptyDataText="Sorry Data Not Found!"
                                                    ShowHeader="true" ShowFooter="true" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                    OnRowDataBound="gridcatqty_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                                        <asp:BoundField DataField="FabNo" HeaderText="DcNo" />
                                                        <asp:BoundField DataField="refno" HeaderText="InvNo" />
                                                        <asp:BoundField DataField="InvDate" HeaderText="InvDate" ItemStyle-HorizontalAlign="Center"
                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="LedgerName" HeaderText="LedgerName" />
                                                        <asp:BoundField DataField="DesignNo" HeaderText="Design" />
                                                        <asp:BoundField DataField="Meter" HeaderText="Purchase Kg/gms" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField DataField="AvaliableMeter" HeaderText="Avaliable Kg/gms" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField DataField="BillMeter" HeaderText="BillMeter Kg/gms" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField DataField="Tax" HeaderText="Tax %" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField DataField="TaxAmount" HeaderText="TaxAmount" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString='{0:f}' />
                                                        <asp:BoundField DataField="FabMode" HeaderText="FabMode" />
                                                        <asp:BoundField DataField="LessMeter" HeaderText="Endbit" ItemStyle-HorizontalAlign="Right"
                                                            DataFormatString='{0:f}' />
                                                    </Columns>
                                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
                        <script type="text/javascript">                            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
