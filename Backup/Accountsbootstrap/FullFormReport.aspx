<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullFormReport.aspx.cs"
    Inherits="Billing.Accountsbootstrap.FullFormReport" %>

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
    <title>Day Report</title>
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


            var gridData = document.getElementById('panel2');



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
    <script type="text/javascript">


        function myFunction() {
            var ButtonControl = document.getElementById("btnprint");
            var fist = document.getElementById("btnexit");

            ButtonControl.style.visibility = "hidden";
            btnexit.style.visibility = "hidden";
            window.print();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <usc:Header ID="Header" runat="server" />
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Day Report</h1>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group ">
                                    <asp:Label ID="Label16" runat="server" Style="color: Red"></asp:Label><br />
                                    <asp:DropDownList ID="drpbranch" AutoPostBack="true" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <div class="form-group">
                                    <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control center-block"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                        PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control center-block"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                        PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label><br />
                                <asp:Button ID="btnsearch" runat="server" ValidationGroup="val1" class="btn btn-success"
                                    Text="Search" OnClick="Search_Click" Width="130px" />
                            </div>
                            <div class="col-lg-1">
                                <asp:Label ID="Label17" runat="server" Style="color: Red"></asp:Label><br />
                                <asp:Button ID="Button1" runat="server" ValidationGroup="val1" class="btn btn-danger"
                                    Text="print" OnClientClick="Denomination()" Width="130px" />
                            </div>
                            <div class="col-lg-1">
                                <%-- <asp:Label ID="Label10" runat="server" Style="color: Red"></asp:Label><br />
                                <asp:Button ID="Button2" runat="server" ValidationGroup="val1" class="btn btn-danger"
                                    Text="print" OnClick="btnExport_Click" Width="130px" />--%>
                            </div>
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-1">
                                <asp:Label ID="lbladdress" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br />
                                <asp:Label ID="lblcity" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br />
                                <asp:Label ID="lblarea" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br />
                                <asp:Label ID="lblmobileno" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="panel2" runat="server">
                        <asp:Panel ID="panel1" runat="server">
                            <div class="col-lg-12">
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-8">
                                    <table border="1" style="width: 600px; height: 70px">
                                        <tr>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label7" ForeColor="Black" CssClass="label" Font-Size="Large">Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label5" ForeColor="Black" CssClass="label" Font-Size="Large">Issue KG</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label6" ForeColor="Black" CssClass="label" Font-Size="Large">Shirt Qty</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="Label8" ForeColor="Black" CssClass="label" Font-Size="Smaller">Pre Cutting</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblpreReqmeter" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblpreReqShirt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="Label9" ForeColor="Black" CssClass="label">Master Cutting</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblmasterReqmeter" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblmasterReqShirt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="1" style="width: 600px; height: 300px">
                                        <tr>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label1" ForeColor="Black" CssClass="label">Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label115" ForeColor="Black" CssClass="label">Issue Qty</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label215" ForeColor="Black" CssClass="label">Receive Qty</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label315" ForeColor="Black" CssClass="label">Damage Qty</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label12" ForeColor="Black" CssClass="label">Paid Amount</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="Label42" ForeColor="Black" CssClass="label">Stitching Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblStichingissu" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblStichingrec" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblStichingdam" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblStitchingpaidamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="Label4" ForeColor="Black" CssClass="label">Embroiding Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblEmbissu" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblEmbrec" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblEmbdam" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblEmbroidingpaidamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td>
                                                <asp:Label runat="server" ID="Label13" ForeColor="Black" CssClass="label">Kaja Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblKajaissu" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblKajarec" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblKajadam" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblKajapaidamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td>
                                                <asp:Label runat="server" ID="Label18" ForeColor="Black" CssClass="label">Washing Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblwashissu" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblwashrec" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblwashdam" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblWashingpaidamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="Label22" ForeColor="Black" CssClass="label">Printing Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblPrintissu" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblPrintrec" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblPrintdam" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblPrintingpaidamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td>
                                                <asp:Label runat="server" ID="Label26" ForeColor="Black" CssClass="label">Bartag Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblBartagissu" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblBartagrec" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblBartagdam" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblBartagpaidamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td>
                                                <asp:Label runat="server" ID="Label30" ForeColor="Black" CssClass="label">Trimming Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblTrimissu" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblTrimrec" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblTrimdam" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblTrimmingpaidamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td>
                                                <asp:Label runat="server" ID="Label34" ForeColor="Black" CssClass="label">Consai Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblConsaiissu" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblConsairec" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblConsaidam" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblConsaipaidamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Label runat="server" ID="Label20" ForeColor="Black" CssClass="label" Font-Size="Large">Total Amount</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lbltotalpaidamount" ForeColor="Black" Font-Size="Large"
                                                    CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="1" style="width: 550px; height: 100px">
                                        <tr>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label10" ForeColor="Black" CssClass="label">Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label14" ForeColor="Black" CssClass="label">Issue Qty</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label15" ForeColor="Black" CssClass="label">Receive Qty</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label23" ForeColor="Black" CssClass="label">Damage Qty</asp:Label>
                                            </td>
                                              <td runat="server" visible="false" align="center">
                                                <asp:Label runat="server" ID="Label25" ForeColor="Black" CssClass="label">Alter Qty</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label24" ForeColor="Black" CssClass="label">Paid Amount</asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td>
                                                <asp:Label runat="server" ID="Label38" ForeColor="Black" CssClass="label">Ironing</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblIronissujp" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblIronrecjp" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblIrondamjp" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                             <td align="center">
                                                <asp:Label runat="server" ID="lblIronalterjp" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblIroningpaidamtjp" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td>
                                                <asp:Label runat="server" ID="Label19" ForeColor="Black" CssClass="label">Ironing JobWorker</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblIronissujobbworker" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblIronrecjobbworker" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblIrondamjobbworker" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                             <td runat="server" visible="false" align="center">
                                                <asp:Label runat="server" ID="lblIronalterjobbworker" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblIroningpaidamtjobbworker" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Label runat="server" ID="Label27" ForeColor="Black" CssClass="label" Font-Size="Large">Total Amount</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lbltotalironpaidamount" ForeColor="Black" Font-Size="Large"
                                                    CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                     <table border="1" style="width: 550px; height: 70px">
                                      <tr>
                                            <td align="center" colspan="5">
                                                <asp:Label runat="server" ID="Label28" ForeColor="Black" CssClass="label" Font-Size="Large">Grand Total Amount</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblGrandTotalAmount" ForeColor="Black" Font-Size="Large"
                                                    CssClass="label">0</asp:Label>
                                            </td>
                                        </tr>
                                     </table>
                                    <br />
                                    <table border="1" style="width: 600px; height: 70px" runat="server" visible="false">
                                        <tr>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label2" ForeColor="Black" CssClass="label" Font-Size="Large">Process</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="Label3" ForeColor="Black" CssClass="label" Font-Size="Large">Qty</asp:Label>
                                            </td>
                                            <%--<td align="center">
                                            <asp:Label runat="server" ID="Label10" ForeColor="Black" CssClass="label" Font-Size="Large">Amount</asp:Label>
                                        </td>--%>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="Label11" ForeColor="Black" CssClass="label" Font-Size="Smaller">Despatch</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblDespatchTotalQty" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <%--  <td align="center">
                                            <asp:Label runat="server" ID="Label14" ForeColor="Black" CssClass="label">0</asp:Label>
                                        </td>--%>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="Label21" ForeColor="Black" CssClass="label" Font-Size="Smaller">Despatch Return</asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label runat="server" ID="lblDespatchReturnTotalQty" ForeColor="Black" CssClass="label">0</asp:Label>
                                            </td>
                                            <%-- <td align="center">
                                            <asp:Label runat="server" ID="Label24" ForeColor="Black" CssClass="label">0</asp:Label>
                                        </td>--%>
                                        </tr>
                                        <%--<tr>
                                        <td>
                                            <asp:Label runat="server" ID="Label15" ForeColor="Black" CssClass="label">Payment</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label runat="server" ID="lblPaymentTotalQuantity" ForeColor="Black" CssClass="label">0</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label runat="server" ID="lblPaymentTotalAmount" ForeColor="Black" CssClass="label">0</asp:Label>
                                        </td>
                                    </tr>--%>
                                    </table>
                                </div>
                                <div class="col-lg-1">
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-10">
                                <div>
                                    <asp:GridView ID="GVPreCutting" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVPreCutting_OnRowDataBound" ShowFooter="true" Width="80%" Caption="Pre Cutting Process"
                                        EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyFullLotNo" HeaderText="CompanyFullLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Deliverydate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="LedgerName" HeaderText="LedgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Reqmeter" DataFormatString="{0:n}" HeaderText="KG"
                                                ItemStyle-HorizontalAlign="Center" Visible="false" />
                                            <asp:BoundField DataField="ReqShirt" DataFormatString="{0:0}" HeaderText="ReqShirt"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="AvgMeter" HeaderText="AvgKG" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <br />
                                    <asp:GridView ID="GVMasterCutting" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVMasterCutting_OnRowDataBound" ShowFooter="true" Width="80%"
                                        Caption="Master Cutting Process" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyFullLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Deliverydate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="LedgerName" HeaderText="LedgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Reqmeter" DataFormatString="{0:n}" HeaderText="KG"
                                                ItemStyle-HorizontalAlign="Center" Visible="false" />
                                            <asp:BoundField DataField="ReqShirt" DataFormatString="{0:0}" HeaderText="ReqShirt"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="AvgMeter" HeaderText="AvgKG" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <br />
                                    <asp:GridView ID="GVFab" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVFab_OnRowDataBound" ShowFooter="true" Width="80%" Caption="Pre Cutting Fabric"
                                        EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyFullLotNo" HeaderText="CompanyFullLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Deliverydate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="DesignNo" HeaderText="DesignNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Reqmeter" DataFormatString="{0:n}" HeaderText="Req.KG"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <br />
                                    <asp:GridView ID="GVEndbitDay" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVEndbitDay_OnRowDataBound" ShowFooter="true" Width="80%" Caption="Master Cutting Fabric"
                                        EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="CreatedDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="DesignNo" HeaderText="DesignNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Givenmeter" DataFormatString="{0:n}" HeaderText="Given KG"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Reqmeter" DataFormatString="{0:n}" HeaderText="Req.KG"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Endbit" DataFormatString="{0:n}" HeaderText="Endbit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVJpStichingissu" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVJpStichingissu_OnRowDataBound" Width="80%" Caption="Stiching Issue Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVJpStichingrec" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVJpStichingrec_OnRowDataBound" Width="80%" Caption="Stiching Receive Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVJpStichingdam" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVJpStichingdam_OnRowDataBound" Width="80%" Caption="Stiching Damage Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVEmbissu" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVEmbissu_OnRowDataBound" Width="80%" Caption="Embroding Issue Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVEmbrec" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVEmbrec_OnRowDataBound" Width="80%" Caption="Embroding Receive Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVEmbdam" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVEmbdam_OnRowDataBound" Width="80%" Caption="Embroding Damage Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVKajaissu" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVKajaissu_OnRowDataBound" Width="80%" Caption="Kaja Issue Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVKajarec" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVKajarec_OnRowDataBound" Width="80%" Caption="Kaja Receive Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVKajadam" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVKajadam_OnRowDataBound" Width="80%" Caption="Kaja Damage Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVPrintissu" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVPrintissu_OnRowDataBound" Width="80%" Caption="Printing Issue Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVPrintrec" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVPrintrec_OnRowDataBound" Width="80%" Caption="Printing Receive Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVPrintdam" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVPrintdam_OnRowDataBound" Width="80%" Caption="Printing Damage Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVwashissu" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVwashissu_OnRowDataBound" Width="80%" Caption="Washing Issue Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVwashrec" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVwashrec_OnRowDataBound" Width="80%" Caption="Washing Receive Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVwashdam" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVwashdam_OnRowDataBound" Width="80%" Caption="Washing Damage Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVBartagissu" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVBartagissu_OnRowDataBound" Width="80%" Caption="Bartag Issue Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVBartagrec" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVBartagrec_OnRowDataBound" Width="80%" Caption="Bartag Receive Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVBartagdam" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVBartagdam_OnRowDataBound" Width="80%" Caption="Bartag Damage Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVTrimissu" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVTrimissu_OnRowDataBound" Width="80%" Caption="Trimming Issue Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVTrimrec" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVTrimrec_OnRowDataBound" Width="80%" Caption="Trimming Receive Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVTrimdam" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVTrimdam_OnRowDataBound" Width="80%" Caption="Trimming Damage Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVConsaiissu" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVConsaiissu_OnRowDataBound" Width="80%" Caption="Consai Issue Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVConsairec" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVConsairec_OnRowDataBound" Width="80%" Caption="Consai Receive Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVConsaidam" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVConsaidam_OnRowDataBound" Width="80%" Caption="Consai Damage Process"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVIronissujp" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVIronissujp_OnRowDataBound" Width="80%" Caption="Ironing Issue JP"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVIronrecjp" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVIronrecjp_OnRowDataBound" Width="80%" Caption="Ironing Receive JP"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVIrondamjp" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVIrondamjp_OnRowDataBound" Width="80%" Caption="Ironing Damage JP"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVIronalterjp" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVIronalterjp_OnRowDataBound" Width="80%" Caption="Ironing Alter JP"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVIronissujobworker" Visible="true" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVIronissujobworker_OnRowDataBound" Width="80%" Caption="Ironing Issue JobWorker"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVIronrecjobworker" Visible="true" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVIronrecjobworker_OnRowDataBound" Width="80%" Caption="Ironing Receive JobWorker"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVIrondamjobworker" Visible="true" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVIrondamjobworker_OnRowDataBound" Width="80%" Caption="Ironing Damage JobWorker"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVIronalterjobworker" Visible="false" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                        OnRowDataBound="GVIronalterjobworker_OnRowDataBound" Width="80%" Caption="Ironing Alter JobWorker"
                                        ShowFooter="true" EmptyDataText="Not In Process">
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="SendDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ledgerName" HeaderText="ledgerName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="Fit" HeaderText="Fit" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="TotalIssue" HeaderText="TotalQty" DataFormatString="{0:n}"
                                                ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVDespatch" Visible="false" runat="server" EmptyDataText="Sorry Data Not Found!"
                                        Width="70%" CssClass="myGridStyle" Caption="Despatch Report" AutoGenerateColumns="false"
                                        OnRowDataBound="GVDespatchstock_RowDataBound" ShowFooter="true">
                                        <Columns>
                                            <asp:BoundField DataField="DcNo" HeaderText="DcNo" />
                                            <asp:BoundField DataField="DcDate" HeaderText="DcDate" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="LedgerName" HeaderText="Despatcher" />
                                            <asp:BoundField DataField="Narration" HeaderText="Narration" />
                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVDespatchReturn" Visible="false" runat="server" EmptyDataText="Sorry Data Not Found!"
                                        Width="70%" CssClass="myGridStyle" Caption="Despatch Return" AutoGenerateColumns="false"
                                        OnRowDataBound="GVDespatchReturn_RowDataBound" ShowFooter="true">
                                        <Columns>
                                            <asp:BoundField DataField="DcNo" HeaderText="DcNo" />
                                            <asp:BoundField DataField="DcDate" HeaderText="DcDate" ItemStyle-HorizontalAlign="Center"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="LedgerName" HeaderText="Despatcher" />
                                            <asp:BoundField DataField="Narration" HeaderText="Narration" />
                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVPaymentGridjp" Visible="false" CssClass="myGridStyle" ShowHeaderWhenEmpty="true"
                                        Caption="Payment JP Report" EmptyDataText="No Records Found" AllowSorting="true"
                                        runat="server" PageSize="10" ShowFooter="true" OnRowDataBound="GVPaymentGridjp_RowDataBound"
                                        Width="70%" AllowPaging="false" AutoGenerateColumns="false">
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <HeaderStyle BackColor="#3366FF" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="PaymentNo" HeaderStyle-ForeColor="Black" DataField="PaymentNo" />
                                            <asp:BoundField HeaderText="PaymentDate" DataField="PaymentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="JobWorker" DataField="LedgerName" />
                                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Narration" DataField="Narration" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="GVPaymentGridjobworker" CssClass="myGridStyle" ShowHeaderWhenEmpty="true"
                                        Caption="Payment JobWorker Report" EmptyDataText="No Records Found" AllowSorting="true"
                                        runat="server" PageSize="10" ShowFooter="true" OnRowDataBound="GVPaymentGridjobworker_RowDataBound"
                                        Width="70%" AllowPaging="false" AutoGenerateColumns="false">
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <HeaderStyle BackColor="#3366FF" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="PaymentNo" HeaderStyle-ForeColor="Black" DataField="PaymentNo" />
                                            <asp:BoundField HeaderText="PaymentDate" DataField="PaymentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="JobWorker" DataField="LedgerName" />
                                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Narration" DataField="Narration" />
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-lg-1">
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <table width="595px" class="style1" runat="server" visible="false">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="Denomination()" />
                            <asp:Button ID="btnexit" runat="server" Text="Exit" PostBackUrl="~/Accountsbootstrap/FullFormReport.aspx" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    </script>
    </form>
</body>
</html>
