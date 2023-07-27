<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DespatchReturnGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.DespatchReturnGrid" %>

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
    <title>Despatch Return Grid</title>
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
        function Denomination123() {


            var gridData = document.getElementById('gridcatqty');


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
        function Denomination() {


            var gridData = document.getElementById('gvprint');


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
    <div class="row">
    </div>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header">
                Despatch Return Grid</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <%-- <div class="panel-heading" style="background-color: #0071BD; color: White">
                    Sales Summary Report</div>--%>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <form id="form1" runat="server">
                            <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-2">
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group ">
                                                <asp:Label ID="Label5" runat="server"></asp:Label><br />
                                                <asp:DropDownList ID="drpbranch" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="drpbranch_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
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
                                        <div class="col-lg-1">
                                            <asp:Label ID="Label1" runat="server">Search</asp:Label><br />
                                            <asp:Button ID="btnsearch" runat="server" Text="Search" Visible="true" CssClass="btn btn-success"
                                                OnClick="btnsearch_OnClick" Width="100px" />
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Label ID="Label2" runat="server">Add New</asp:Label><br />
                                            <asp:Button ID="btnadd" runat="server" Text="Add New" Visible="true" CssClass="btn btn-danger"
                                                PostBackUrl="~/Accountsbootstrap/DespatchReturn.aspx" Width="100px" />
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Label ID="lblPrint" runat="server">Print</asp:Label><br />
                                            <asp:Button ID="btn" runat="server" Text="Print" Visible="true" CssClass="btn btn-group"
                                                OnClientClick="Denomination123()" Width="100px" />
                                        </div>
                                        <div class="col-lg-3">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-10">
                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td id="Td1" runat="server" visible="true">
                                                            <asp:GridView ID="gridcatqty" Visible="true" runat="server" EmptyDataText="Sorry Data Not Found!"
                                                                ShowFooter="true" Width="70%" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                OnRowCommand="gvCustsales_RowCommand" OnRowDataBound="gridcatqty_OnRowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="DcNo" HeaderText="DcNo" />
                                                                    <asp:BoundField DataField="DcDate" HeaderText="DcDate" ItemStyle-HorizontalAlign="Center"
                                                                        DataFormatString="{0:dd/MM/yyyy}" />
                                                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                                                    <asp:BoundField DataField="LedgerName" HeaderText="Despatcher" />
                                                                    <asp:BoundField DataField="Narration" HeaderText="Narration" />
                                                                    <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" />
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Print">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("DespatchId") %>'
                                                                                CommandName="print">
                                                                                <asp:Image ID="print" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("DespatchId") %>'
                                                                                CommandName="Edit">
                                                                                <asp:Image ID="imged" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                                            <asp:ImageButton ID="imgdisableed" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                                                Enabled="false" ToolTip="Not Allow To Delete" />
                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-lg-1">
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="row">
                                        <table>
                                            <tr>
                                                <td>
                                                    <div class="col-lg-12">
                                                        <div class="col-lg-2">
                                                        </div>
                                                        <div class="col-lg-8">
                                                            <asp:GridView ID="gvprint" Visible="true" runat="server" EmptyDataText="Sorry Data Not Found!"
                                                                Width="70%" CssClass="myGridStyle" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:BoundField DataField="CompanyLotNo" HeaderText="LotNo" />
                                                                    <asp:BoundField DataField="Fitt" HeaderText="Fitt" />
                                                                    <asp:BoundField DataField="S30" HeaderText="S30" />
                                                                    <asp:BoundField DataField="S32" HeaderText="S32" />
                                                                    <asp:BoundField DataField="S34" HeaderText="S34" />
                                                                    <asp:BoundField DataField="S36" HeaderText="S36" />
                                                                    <asp:BoundField DataField="XS" HeaderText="XS" />
                                                                    <asp:BoundField DataField="S" HeaderText="S" />
                                                                    <asp:BoundField DataField="M" HeaderText="M" />
                                                                    <asp:BoundField DataField="L" HeaderText="L" />
                                                                    <asp:BoundField DataField="XL" HeaderText="XL" />
                                                                    <asp:BoundField DataField="XXL" HeaderText="XXL" />
                                                                    <asp:BoundField DataField="S3XL" HeaderText="2XL" />
                                                                    <asp:BoundField DataField="S4XL" HeaderText="3XL" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div class="col-lg-2">
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%-- <asp:AsyncPostBackTrigger ControlID="gridPurchase" EventName="RowDataBound" />
                                    <asp:AsyncPostBackTrigger ControlID="gridcatqty" EventName="RowDataBound"></asp:AsyncPostBackTrigger>
                                    <asp:AsyncPostBackTrigger ControlID="gridcatqty" EventName="RowDataBound"></asp:AsyncPostBackTrigger>--%>
                                    <asp:PostBackTrigger ControlID="btn"></asp:PostBackTrigger>
                                </Triggers>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gridcatqty" EventName="RowDataBound" />
                                </Triggers>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btn" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="update">
                                <ProgressTemplate>
                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                                        right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/Preloader_10.gif"
                                            AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed;
                                            top: 45%; left: 50%;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
                            <script type="text/javascript">                                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
                            <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
                                runat="server">
                                <div class="popup_Container">
                                    <div class="popup_Titlebar" id="PopupHeader">
                                        <div class="TitlebarLeft">
                                            Fabric Process:</div>
                                        <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                                        </div>
                                    </div>
                                    <div class="popup_Body">
                                        <p>
                                            Are you sure want to delete?
                                        </p>
                                    </div>
                                    <div class="popup_Buttons">
                                        <input id="ButtonDeleleOkay" type="button" value="Yes" />
                                        <input id="ButtonDeleteCancel" type="button" value="No" />
                                    </div>
                                </div>
                            </asp:Panel>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
