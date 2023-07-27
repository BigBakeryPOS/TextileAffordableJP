<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayBook.aspx.cs" Inherits="Billing.Accountsbootstrap.DayBook" %>

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
    <%-- <link rel="Stylesheet" type="text/css" href="../Styles/date.css" />--%>
    <link rel="Stylesheet" type="text/css" href="../Styles/style1.css" />
    <%--<script type="text/javascript" src="../jquery-1.6.2.min.js"></script>
<script type="text/javascript" src="../jquery-ui-1.8.15.custom.min.js"></script>
<link rel="stylesheet" href="../jqueryCalendar.css"/>--%>
    <!-- Bootstrap Core CSS -->
    <%--<link href="../Styles/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../Styles/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../Styles/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../Styles/font-awesome.min.css" rel="stylesheet" type="text/css"/>--%>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
     
    <![endif]-->
    <link href="../images/fav.ico" type="image/x-icon" rel="Shortcut Icon" />
    <style type="text/css">
        .GroupHeaderStyle
        {
            color: Blue;
            font-weight: bold;
            text-transform: uppercase;
        }
        .SubTotalRowStyle
        {
            color: Blue;
            font-weight: bold;
        }
        .GrandTotalRowStyle
        {
            color: red;
            font-weight: bold;
        }
        .GrandTotalRowStyle1
        {
            background-color: White;
            color: Blue;
            font-weight: bold;
        }
        .align1
        {
            text-align: right;
        }
        
        .myGridStyle1 tr th
        {
            padding: 8px;
            color: White;
            background-color: #cccccc;
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


            var gridData = document.getElementById('gvLedger');


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
    <form id="Form1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2" style="margin-top: 6px">
                    <h3 class="page-header" style="text-align: center; color: #fe0002;">
                        Day Book Report</h3>
                </div>
                <div class="col-lg-10" style="margin-top: 6px">
                    <div class="row">
                        <div class="col-lg-2">
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label style="margin-left: -120px">
                                    From Date
                                </label>
                                <asp:TextBox CssClass="form-control" ID="txtfromdate" runat="server" Text="Select Date"
                                    Style="margin-left: -120px"></asp:TextBox>
                            </div>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate"
                                PopupButtonID="txtfromdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                CssClass="cal_Theme1">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label style="margin-left: -80px">
                                    TO Date
                                </label>
                                <asp:TextBox CssClass="form-control" ID="txttodate" runat="server" Text="Select Date"
                                    Style="margin-left: -80px"></asp:TextBox>
                            </div>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate"
                                PopupButtonID="txttodate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                CssClass="cal_Theme1">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group" id="leg" runat="server">
                                <label style="margin-left: -50px">
                                    Select Company</label>
                                <asp:DropDownList ID="ddloutlet" runat="server" CssClass="form-control" Style="margin-left: -50px">
                                    <%--<asp:ListItem Text="select" Value="0"></asp:ListItem>--%>
                                    <asp:ListItem Text="CO1" Value="CO1"></asp:ListItem>
                                    <asp:ListItem Text="CO2" Value="CO2"></asp:ListItem>
                                    <asp:ListItem Text="CO3" Value="CO3"></asp:ListItem>
                                    <%-- <asp:ListItem Text="All" Value="All"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <%-- <asp:Button ID="btnfind" runat="server" Text="Find" CssClass="btn btn-success" 
                                                onclick="btnfind_Click" />

                                                 <asp:Button ID="btnall" runat="server" Text="View All" 
                                                CssClass="btn btn-success" onclick="btnall_Click" 
                                                />--%>
                        </div>
                        <asp:ScriptManager ID="ScriptManager1" ScriptMode="Release" runat="server">
                        </asp:ScriptManager>
                        <div class="col-lg-2">
                            <asp:Button ID="btngen" runat="server" Text="Generate" CssClass="btn btn-success"
                                OnClick="btngen_Click" Width="150px" Style="margin-top: 24px; margin-left: -30px" />
                            <%-- <asp:Button ID="btnreset" runat="server" Text="Reset" 
                                            CssClass="btn btn-success" onclick="btnreset_Click" 
                                                />--%>
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="btnexcel" runat="server" Text="Export to Excel" CssClass="btn btn-primary"
                                OnClick="btnexcel_Click" Width="150px" Style="margin-top: 24px; margin-left: -30px"
                                Visible="false" />
                        </div>
                        <!-- /.row -->
                    </div>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <%-- <asp:Button ID="Button1"  runat="server" CssClass="btn btn-block center-block" Text="Print" 
                                        Width="125px" onclick="Button1_Click"  />   --%>
                        <asp:Button ID="btnprint" runat="server" CssClass="btn btn-block center-block" Text="Print"
                            Width="125px" OnClientClick="Denomination()" />
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12" id="bill">
                                    <div class="col-lg-12">
                                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                        <h2 align="center">
                                            <asp:Label ID="lblMessage" Style="color: Blue;" runat="server"></asp:Label></h2>
                                        <div style="width: 98%; padding-left: 60px" align="center" class="row" id="idt" visible="false"
                                            runat="server">
                                            <br />
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <table runat="server" style="border: 1px solid Grey; height: 30px; background-color: #cccccc;
                                                text-transform: uppercase" width="100%">
                                                <tr>
                                                    <td width="6%" align="center" style="font-size: large">
                                                        Date
                                                    </td>
                                                    <td width="41%" align="center" style="font-size: large">
                                                        Particulars
                                                        <asp:Label ID="lblOB" Visible="false" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="13%" align="center" style="font-size: large">
                                                        Narration
                                                    </td>
                                                    <td width="15%" align="center" style="font-size: large">
                                                        Debit
                                                    </td>
                                                    <td width="15%" align="center" style="font-size: large">
                                                        Credit
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="lblopbal" runat="server"></asp:Label>
                                            <div style="height: 520px; overflow: scroll;" align="center">
                                                <asp:GridView runat="server" ID="gvLedger" AutoGenerateColumns="false" OnRowCreated="gridPurchase_RowCreated"
                                                    AllowPrintPaging="true" OnRowDataBound="gvLedger_RowDataBound">
                                                    <RowStyle Height="19px" />
                                                    <Columns>
                                                        <%-- <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblTranDate" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                                        <asp:BoundField DataField="Date" ItemStyle-Width="5%" DataFormatString="{0:d}" />
                                                        <%-- <asp:BoundField ItemStyle-VerticalAlign="Top" ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center"
                        DataField="Branchcode" HeaderText="Branch" />--%>
                                                        <asp:BoundField DataField="Particulars" ItemStyle-Width="35%" />
                                                        <asp:BoundField DataField="Narration" ItemStyle-Width="20%" />
                                                        <asp:BoundField ItemStyle-Width="13%" DataFormatString="{0:n2}" DataField="Debit"
                                                            ItemStyle-HorizontalAlign="right" />
                                                        <asp:BoundField ItemStyle-Width="13%" DataFormatString="{0:n2}" DataField="Credit"
                                                            ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="right" />
                                                    </Columns>
                                                    <PagerTemplate>
                                                    </PagerTemplate>
                                                </asp:GridView>
                                                <asp:GridView runat="server" ID="gvLed" Visible="false" AutoGenerateColumns="false"
                                                    OnRowCreated="gridPur_RowCreated" AllowPrintPaging="true" OnRowDataBound="gvLed_RowDataBound">
                                                    <RowStyle Height="19px" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTranDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField ItemStyle-VerticalAlign="Top" ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center"
                        DataField="Branchcode" HeaderText="Branch" />--%>
                                                        <asp:BoundField DataField="Particulars" ItemStyle-Width="35%" />
                                                        <asp:BoundField DataField="Narration" ItemStyle-Width="20%" />
                                                        <asp:BoundField ItemStyle-Width="13%" DataFormatString="{0:n}" DataField="Debit"
                                                            ItemStyle-HorizontalAlign="right" />
                                                        <asp:BoundField ItemStyle-Width="13%" DataFormatString="{0:n}" DataField="Credit"
                                                            ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="right" />
                                                    </Columns>
                                                    <PagerTemplate>
                                                    </PagerTemplate>
                                                </asp:GridView>
                                                <table runat="server" width="100%">
                                                    <tr runat="server" visible="false">
                                                        <td width="69%" align="right">
                                                            Total :
                                                        </td>
                                                        <td width="7%" align="right">
                                                            <asp:Label ID="lblSumDebit" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="7%" align="right">
                                                            <asp:Label ID="lblSumCredit" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="9%" align="right">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <%--  <asp:Label ID="lblDebitTotal" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblCreditTotal" runat="server" Visible="false"></asp:Label>
  
    <table border="1" >
        <tr>
        <td width="890px" align="right">
         <label>Closing balance</label>
        </td>
        <td width="250px" align="right"><asp:Label ID="lblDebitClosingTotal" runat="server"></asp:Label></td>
        <td width="260px" align="right"> <asp:Label ID="lblCreditClosingTotal" runat="server"></asp:Label></td>
        </tr>
         <tr>
        <td style="padding-top:10px" width="880px" align="right">
         <label>Total</label>
        </td>
        <td width="250px" align="right"><asp:Label ID="lblNetDebit" runat="server"></asp:Label></td>
        <td width="260px" align="right"> <asp:Label ID="lblNetCredit" runat="server"></asp:Label></td>
        </tr>
    </table>--%>
                                            <br />
                                        </div>
                                        <!-- /#page-wrapper -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
