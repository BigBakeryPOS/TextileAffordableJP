<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnUsedFab.aspx.cs" Inherits="Billing.Accountsbootstrap.UnUsedFab" %>

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
    <title>Fabric Less</title>
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
    <div class="row">
    </div>
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Fabric Less</h1>
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
                                        <div class="col-lg-1">
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                                            </asp:ScriptManager>
                                            <div class="form-group">
                                                <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                                                <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                                    OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                                    PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                    CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                                                <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                                    OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                                    PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                    CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="lblsupplier" runat="server">Supplier Name</asp:Label>
                                            <asp:DropDownList ID="ddlsupplier" runat="server" AutoPostBack="true" class="chzn-select"
                                                Width="220px" Height="80px" OnSelectedIndexChanged="ddlsupplier_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Label ID="lblPrint" runat="server">Print</asp:Label><br />
                                            <asp:Button ID="btn" runat="server" Text="Print" Visible="true" CssClass="btn btn-danger"
                                                OnClientClick="Denomination()" Width="100px" />
                                        </div>
                                        <div class="col-lg-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server">Admin</asp:Label><br />
                                                <asp:TextBox ID="txtadminpass" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                                    TextMode="Password" OnTextChanged="txtadminpass_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" runat="server">Meter</asp:Label><br />
                                                <asp:TextBox ID="txtissuemeter" runat="server" CssClass="form-control center-block"
                                                    AutoPostBack="true" OnTextChanged="txtissuemeter_OnTextChanged" Enabled="false">0</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Label ID="Label3" runat="server">LessMeter</asp:Label><br />
                                            <asp:Button ID="btnlessmeter" runat="server" Text="Process" Enabled="false" class="btn btn-success"
                                                OnClick="btnlessmeter_OnClick" Width="100px" />
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Label ID="lbladdress" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br />
                                            <asp:Label ID="lblcity" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br />
                                            <asp:Label ID="lblarea" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label><br />
                                            <asp:Label ID="lblmobileno" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="Div1" class="row" runat="server" visible="true">
                                        <div class="col-lg-2">
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlcompany" runat="server" Width="180px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged">
                                                <%-- <asp:ListItem Text="ALL" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="RAPPHAEL" Value="1" ></asp:ListItem>
                                                <asp:ListItem Text="BOTTICELLI" Value="2" ></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-1">
                                            <div class="form-group">
                                                <asp:RadioButton ID="rdbboth" runat="server" Text="Both" CssClass="center-block"
                                                    GroupName="a" Checked="true" AutoPostBack="true" OnCheckedChanged="rdbboth_OnCheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <div class="form-group">
                                                <asp:RadioButton ID="rdbfinished" runat="server" Text="Finished" CssClass="center-block"
                                                    GroupName="a" AutoPostBack="true" OnCheckedChanged="rdbfinished_OnCheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <asp:RadioButton ID="rdbunfinished" runat="server" Text="UnFinished" CssClass="center-block"
                                                    GroupName="a" AutoPostBack="true" OnCheckedChanged="rdbunfinished_OnCheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlmoveto" runat="server" Width="180px">
                                                <asp:ListItem Text="Move To" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Body" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Contrast" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="End Bit" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td id="Td1" runat="server" visible="true">
                                                            <div id="gridcatqty1" runat="server">
                                                                <div class="col-lg-12">
                                                                    <div class="col-lg-5">
                                                                    </div>
                                                                    <div class="col-lg-2">
                                                                        <asp:Image ID="log" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 8pc;" runat="server"
                                                                            Visible="false" />
                                                                    </div>
                                                                    <div class="col-lg-5">
                                                                    </div>
                                                                </div>
                                                                <asp:GridView ID="gridcatqty" Visible="true" Width="100%" runat="server" EmptyDataText="Sorry Data Not Found!"
                                                                    CssClass="mGrid" AutoGenerateColumns="false" OnRowDataBound="gridcatqty_RowDataBound1">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DesignNo" HeaderText="Design" HeaderStyle-Width="10%" />
                                                                        <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" HeaderStyle-Width="5%" />
                                                                        <asp:BoundField DataField="FabNo" HeaderText="FabNo" HeaderStyle-Width="5%" />
                                                                        <asp:BoundField DataField="refno" HeaderText="refno" HeaderStyle-Width="5%" />
                                                                        <asp:BoundField DataField="InvDate" HeaderText="InvDate" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-Width="5%" DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right"
                                                                            HeaderStyle-Width="5%" DataFormatString='{0:f}' />
                                                                        <asp:BoundField DataField="Meter" HeaderText="Meter" ItemStyle-HorizontalAlign="Right"
                                                                            HeaderStyle-Width="5%" DataFormatString='{0:f}' />
                                                                        <%--<asp:BoundField DataField="AvaliableMeter" HeaderText="AvaliableMeter" ItemStyle-HorizontalAlign="Right"
                                                                            DataFormatString='{0:f}' />--%>
                                                                        <asp:TemplateField HeaderText="AvaliableMeter" HeaderStyle-Width="5%" Visible="true"
                                                                            ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbllAvaliableMeter" Text='<%# Eval("AvaliableMeter","{0:n}")%>' runat="server"
                                                                                    Enabled="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="CHK" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkitemchecked" runat="server" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Transid" ControlStyle-Width="100%" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbllTransid" Text='<%# Eval("Transid")%>' runat="server" Enabled="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-lg-4">
                                            </div>
                                        </div>
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
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
