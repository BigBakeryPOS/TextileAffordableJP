<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinishedStockReport.aspx.cs"
    Inherits="Billing.Accountsbootstrap.FinishedStockReport" %>

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
    <title>Finished Stock Report</title>
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
        function Search_Gridview(strKey, strGV) {


            var strData = strKey.value.toLowerCase().split(" ");

            var tblData = document.getElementById(strGV);

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)

                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }    
    </script>
    <script type="text/javascript">
        function Denomination123() {


            var gridData = document.getElementById('GVFinalStock');


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
        <div class="col-lg-12" style="margin-top: 5px">
            <h1 class="page-header">
                Finished Stock Report</h1>
        </div>
    </div>
    <form id="form1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <%--  <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <div class="row">
                <div class="col-lg-12">
                    <div class="col-lg-2">
                        <div class="form-group ">
                            <asp:Label ID="Label1" runat="server">Branch</asp:Label>
                            <asp:DropDownList ID="drpbranch" Width="150px" OnSelectedIndexChanged="company_SelectedIndexChnaged"
                                AutoPostBack="true" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <div class="form-group ">
                            <asp:Label ID="Label2" runat="server">Type of Stock</asp:Label>
                            <asp:DropDownList ID="ddlstocktype" OnSelectedIndexChanged="ddlstocktype_SelectedIndexChnaged"
                                AutoPostBack="true" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Godown Stock" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Despatch Stock" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Return Stock" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Despatch Version" Value="4"></asp:ListItem>
                                 <asp:ListItem Text="Stock Based on Despatch" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="col-lg-1" runat="server" visible="true">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <div class="form-group">
                            <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                CssClass="cal_Theme1">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-lg-1" runat="server" visible="true">
                        <div class="form-group">
                            <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                CssClass="cal_Theme1">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <asp:Label ID="Label8" runat="server">Type</asp:Label><br />
                            <asp:DropDownList ID="ddltype" CssClass="form-control" Width="100px" runat="server">
                                <asp:ListItem Text="LotNo" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Item" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label4" runat="server"></asp:Label><br />
                        <asp:TextBox CssClass="form-control" onkeyup="Search_Gridview(this, 'GVFinalStock')"
                            Enabled="true" ID="txtsearch" runat="server" placeholder="Search Text" Width="100px"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label5" runat="server"></asp:Label><br />
                        <asp:Button ID="btnserach" runat="server" Text="Search" Visible="true" CssClass="btn btn-success"
                            OnClick="btnsearch_OnClick" Width="100px" />
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label6" runat="server"></asp:Label><br />
                        <asp:Button ID="btn" runat="server" Text="Print" Visible="true" CssClass="btn btn-group"
                            OnClientClick="Denomination123()" Width="100px" />
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label7" runat="server"></asp:Label><br />
                        <asp:Button ID="btnExport" Visible="true" class="btn btn-warning" Text="Excel" runat="server"
                            Width="100px" OnClick="btnExport_Click" />
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="col-lg-2">
                        <div class="form-group ">
                            <asp:Label ID="Label9" runat="server">Old Version</asp:Label>
                            <asp:DropDownList ID="ddldespatchversion" OnSelectedIndexChanged="ddldespatchversion_SelectedIndexChnaged" Width="150px"
                                AutoPostBack="true" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:Label ID="Label10" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="col-lg-10">
                        <div class="row">
                            <asp:GridView ID="GVFinalStock" Visible="true" runat="server" EmptyDataText="Sorry Data Not Found!"
                                Width="100%" CssClass="myGridStyle" AutoGenerateColumns="false" ShowFooter="true"
                                OnRowDataBound="gridprint_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="BrandName" ItemStyle-Width="10%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrandName" Text='<%#Eval("BrandName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Fit" HeaderText="Fit" />
                                    <asp:TemplateField HeaderText="IsuQty" ItemStyle-Width="10%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemname" Text='<%#Eval("Itemname") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DesignCode" HeaderText="Design" />
                                    <asp:BoundField DataField="CompanyLotNo" HeaderText="LotNo" />
                                    <asp:BoundField DataField="R30FS" HeaderText="30/F" />
                                    <asp:BoundField DataField="R32FS" HeaderText="32/F" />
                                    <asp:BoundField DataField="R34FS" HeaderText="34/F" />
                                    <asp:BoundField DataField="R36FS" HeaderText="36/F" />
                                    <asp:BoundField DataField="RXSFS" HeaderText="XS/F" />
                                    <asp:BoundField DataField="RSFS" HeaderText="S/F" />
                                    <asp:BoundField DataField="RMFS" HeaderText="M/F" />
                                    <asp:BoundField DataField="RLFS" HeaderText="L/F" />
                                    <asp:BoundField DataField="RXLFS" HeaderText="XL/F" />
                                    <asp:BoundField DataField="RXXLFS" HeaderText="XXL/F" />
                                    <asp:BoundField DataField="R3XLFS" HeaderText="3XL/F" />
                                    <asp:BoundField DataField="R4XLFS" HeaderText="4XL/F" />
                                    <asp:BoundField DataField="R30HS" HeaderText="30/H" />
                                    <asp:BoundField DataField="R32HS" HeaderText="32/H" />
                                    <asp:BoundField DataField="R34HS" HeaderText="34/H" />
                                    <asp:BoundField DataField="R36HS" HeaderText="36/H" />
                                    <asp:BoundField DataField="RXSHS" HeaderText="XS/H" />
                                    <asp:BoundField DataField="RSHS" HeaderText="S/H" />
                                    <asp:BoundField DataField="RMHS" HeaderText="M/H" />
                                    <asp:BoundField DataField="RLHS" HeaderText="L/H" />
                                    <asp:BoundField DataField="RXLHS" HeaderText="XL/H" />
                                    <asp:BoundField DataField="RXXLHS" HeaderText="XXL/H" />
                                    <asp:BoundField DataField="R3XLHS" HeaderText="3XL/H" />
                                    <asp:BoundField DataField="R4XLHS" HeaderText="4XL/H" />
                                    <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Version" HeaderText="Version" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                                <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-lg-2">
                    </div>
                </div>
            </div>
            <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
            <%--  <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="update">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                        right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/Preloader_10.gif"
                            AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed;
                            top: 45%; left: 50%;" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
            <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
            <script type="text/javascript">                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
        </div>
    </div>
    </form>
</body>
</html>
