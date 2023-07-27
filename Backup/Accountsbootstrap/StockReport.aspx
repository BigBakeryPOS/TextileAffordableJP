<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockReport.aspx.cs" Inherits="Billing.Accountsbootstrap.StockReport" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Stock Report</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function valchk() {
            if (blankchk(document.getElementById('txtBrandname'), "Branch Name")) {
                alert("true");
            }
            else {
                alert("false");
                return false;
            }
        }
    </script>
    <!-- Bootstrap Core CSS -->
    <link href="../css/responsive-tabs.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" href="../css/bootstrap.min.css" runat="server" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link id="Link2" href="../css/plugins/metisMenu/metisMenu.min.css" runat="server"
        rel="stylesheet" />
    <!-- Custom CSS -->
    <link id="Link3" href="../css/sb-admin-2.css" runat="server" rel="stylesheet" />
    <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
    <script src="../js/jquery.responsiveTabs.min.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.0.min.js" type="text/javascript"></script>
    <link href="css/mGrid.css" rel="stylesheet" type="text/css" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link rel="stylesheet" href="Tabs/css/font-awesome.min.css" />
    <link rel="stylesheet" href="Tabs/css/style.min.css" />
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <script type="text/javascript">
        function Denomination() {


            var gridData = document.getElementById('gvcategoryiyems');



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
    <style type="text/css">
        .Hide
        {
            display: none;
        }
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <form id="Form1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #0071BD; color: White">
                    Stock Report</div>
                <div class="panel-body">
                    <div class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <div class="col-lg-12">
                            <div id="Div1" class="col-lg-2" runat="server" visible="false">
                                <label>
                                    Request No:</label>
                                <asp:TextBox ID="txtpono" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div id="Div2" class="col-lg-2" runat="server" visible="false">
                                <label>
                                    Date:
                                </label>
                                <asp:TextBox ID="txtpodate" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                            </div>
                            <div id="Div3" class="col-lg-2" runat="server" visible="false">
                                <label>
                                    Order By:
                                </label>
                                <asp:TextBox ID="txtOrderBy" runat="server" CssClass="form-control">Staff</asp:TextBox>
                            </div>
                            <div id="Div4" class="col-lg-2" runat="server" visible="false">
                                <label>
                                    Vendor :
                                </label>
                                <asp:DropDownList ID="ddlvendor" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-2">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-12">
        <div class="col-lg-4">
        </div>
        <div class="col-lg-2">
            <div class="form-group ">
                <label>
                    Category</label><br />
                <asp:DropDownList ID="ddlcategory" AutoPostBack="true" runat="server" CssClass="chzn-select"
                    Height="1250px" Width="180px" OnSelectedIndexChanged="ddlcategory_OnSelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-2">
            <div class="form-group ">
                <label>
                    Item</label>
                <asp:DropDownList ID="ddlItem" AutoPostBack="true" runat="server" CssClass="chzn-select"
                    Height="1250px" Width="230px" OnSelectedIndexChanged="ddlItem_OnSelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-1">
        </div>
        <div class="col-lg-1">
            <asp:Button ID="btn" runat="server" Text="Print" Visible="true" CssClass="btn btn-danger"
                OnClientClick="Denomination()" Width="100px" />
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <div class="col-lg-12">
        <div class="col-lg-2">
        </div>
        <div class="col-lg-8">
            <div class="form-group ">
                <table class="table table-bordered table-striped">
                    <tr align="center">
                        <td>
                            <div id="div20" runat="server" style="height: 370px; overflow-x: auto;">
                                <asp:GridView ID="gvcategoryiyems" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                    Width="700px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                        <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                        <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                        <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                            DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                        <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                            DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                        <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                            HeaderStyle-Font-Size="Smaller" />
                                        <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                    </Columns>
                                    <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#de446e" ForeColor="White" HorizontalAlign="Center" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <div class="col-lg-12" runat="server" visible="false">
        <div class="col-lg-2">
        </div>
        <div class="col-lg-8">
            <div id="horizontalTab" style="background-color: #D0D3D6; padding-left: 0px">
                <%-- <ul>
                                    <li><a href="#tab-1">Gateaux</a></li>
                                    <li><a href="#tab-2">Snacks</a></li>
                                    <li><a href="#tab-3">Puddings</a></li>
                                    <li><a href="#tab-4">Beverages</a></li>
                                    <li><a href="#tab-5">Sweets</a></li>
                                    <li><a href="#tab-6">Party</a></li>
                                    <li><a href="#tab-7">Moouse</a></li>
                                    <li><a href="#tab-8">Cookies</a></li>
                                    <li><a href="#tab-9">Cheese</a></li>
                                    <li><a href="#tab-10">Stores</a></li>
                                    <li><a href="#tab-11">BDC</a></li>
                                    <li><a href="#tab-12">Breads</a></li>
                                    <li><a href="#tab-13">Sponges</a></li>
                                    <li><a href="#tab-14">ReadyMade Sponges</a></li>
                                    <li><a href="#tab-15">ReadyMade Cakes</a></li>
                                    <li><a href="#tab-16">Ice Creams</a></li>
                                </ul>--%>
                <div>
                    <div id="tabs">
                        <div style="background-color: #6a9394; width: 40px" class="c-tabs-nav">
                            <a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link is-active"
                                id="tab0" runat="server"><span>
                                    <label id="lblitem" runat="server">
                                    </label>
                                </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                    id="tab1" runat="server"><span>
                                        <label id="lblitem1" runat="server">
                                        </label>
                                    </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                        id="tab2" runat="server"><span>
                                            <label id="lblitem2" runat="server">
                                            </label>
                                        </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                            id="tab3" runat="server"><span>
                                                <label id="lblitem3" runat="server">
                                                </label>
                                            </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                id="tab4" runat="server"><span>
                                                    <label id="lblitem4" runat="server">
                                                    </label>
                                                </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                    id="tab5" runat="server"><span>
                                                        <label id="lblitem5" runat="server">
                                                        </label>
                                                    </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                        id="tab6" runat="server"><span>
                                                            <label id="lblitem6" runat="server">
                                                            </label>
                                                        </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                            id="tab7" runat="server"><span>
                                                                <label id="lblitem7" runat="server">
                                                                </label>
                                                            </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                                id="tab8" runat="server"><span>
                                                                    <label id="lblitem8" runat="server">
                                                                    </label>
                                                                </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                                    id="tab9" runat="server"><span>
                                                                        <label id="lblitem9" runat="server">
                                                                        </label>
                                                                    </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                                        id="tab10" runat="server"><span>
                                                                            <label id="lblitem10" runat="server">
                                                                            </label>
                                                                        </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                                            id="tab11" runat="server"><span>
                                                                                <label id="lblitem11" runat="server">
                                                                                </label>
                                                                            </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                                                id="tab12" runat="server"><span>
                                                                                    <label id="lblitem12" runat="server">
                                                                                    </label>
                                                                                </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                                                    id="tab13" runat="server"><span>
                                                                                        <label id="lblitem13" runat="server">
                                                                                        </label>
                                                                                    </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                                                        id="tab14" runat="server"><span>
                                                                                            <label id="lblitem14" runat="server">
                                                                                            </label>
                                                                                        </span></a><a href="#" style="visibility: hidden; background-color: cadetblue" class="c-tabs-nav__link"
                                                                                            id="tab15" runat="server"><span>
                                                                                                <label id="lblitem15" runat="server">
                                                                                                </label>
                                                                                            </span></a>
                        </div>
                        <%--  <div class="table-responsive">--%>
                        <div class="c-tab is-active">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6;">
                                    <div>
                                        <table class="table table-bordered table-striped">
                                            <tr align="center">
                                                <td>
                                                    <div id="divid" runat="server" style="height: 370px; overflow-x: auto;">
                                                        <asp:GridView ID="gvGateaux" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                            Width="700px">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                                <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                                <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                                <asp:BoundField HeaderText="categoryID" DataField="CategoryID" HeaderStyle-Font-Size="Smaller"
                                                                    ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                                                                <asp:BoundField HeaderText="SubcategoryID" DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller"
                                                                    ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" />
                                                                <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                    HeaderStyle-Font-Size="Smaller" />
                                                                <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                                <%--<asp:TemplateField HeaderText="Units" >
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                            <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                            <HeaderStyle BackColor="#de446e" ForeColor="White" HorizontalAlign="Center" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6; padding-left: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div21" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvSnacks" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" Width="50px" runat="server" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#de446e" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="padding-top: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div5" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvPuddings" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" Width="50px" runat="server" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="padding-top: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div6" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvBeverages" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" Width="50px" runat="server" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="padding-top: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div7" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvSweets" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" Width="50px" runat="server" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="padding-top: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div8" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvcandles" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6; padding-left: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div9" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvMousse" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="padding-top: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div10" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvCookies" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="padding-top: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div11" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvcheese" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6; padding-left: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div12" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvStores" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6; padding-left: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div13" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvBday" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6; padding-left: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div14" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvbread" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6; padding-left: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div15" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvSponges" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" Width="50px" runat="server" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6; padding-left: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div16" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvReadySp" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6; padding-left: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div17" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvRmCake" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="c-tab">
                            <div class="c-tab__content">
                                <div class="row" style="background-color: #D0D3D6; padding-left: 30px">
                                    <table class="table table-bordered table-striped">
                                        <tr align="center">
                                            <td>
                                                <div id="div18" runat="server" style="height: 370px; overflow-x: auto;">
                                                    <asp:GridView ID="gvIce" runat="server" AutoGenerateColumns="false" CssClass="mGrid"
                                                        Width="700px">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Group" DataField="Category" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Code" DataField="Definition" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Accessories Name" DataField="Serial_No" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="categoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="SubcategoryID" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"
                                                                DataField="CategoryUserID" HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Available Stock" DataField="Available_Qty" DataFormatString="{0:f}"
                                                                HeaderStyle-Font-Size="Smaller" />
                                                            <asp:BoundField HeaderText="Units" DataField="Units" HeaderStyle-Font-Size="Smaller" />
                                                            <%--<asp:TemplateField HeaderText="Units">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddUnits" runat="server" Width="50px" Enabled="false">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#990100" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <%--</div>--%>
                    </div>
                </div>
                <div id="hide" runat="server">
                </div>
            </div>
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var $tabs = $('#horizontalTab');
            $tabs.responsiveTabs({
                rotate: false,
                startCollapsed: 'accordion',
                collapsible: 'accordion',
                setHash: true,

                activate: function (e, tab) {
                    $('.info').html('Tab <strong>' + tab.id + '</strong> activated!');
                },
                activateState: function (e, state) {
                    //console.log(state);
                    $('.info').html('Switched from <strong>' + state.oldState + '</strong> state to <strong>' + state.newState + '</strong> state!');
                }
            });

            /* $('#start-rotation').on('click', function () {
            $tabs.responsiveTabs('startRotation', 1000);
            });
            $('#stop-rotation').on('click', function () {
            $tabs.responsiveTabs('stopRotation');
            });
            $('#start-rotation').on('click', function () {
            $tabs.responsiveTabs('active');
            });
            $('#enable-tab').on('click', function () {
            $tabs.responsiveTabs('enable', 3);
            });
            $('#disable-tab').on('click', function () {
            $tabs.responsiveTabs('disable', 3);
            });
            $('.select-tab').on('click', function () {
            $tabs.responsiveTabs('activate', $(this).val()); */

        });
            
        
    </script>
    <%--<div class="form-group input-group">

                                            <label>Heading</label>
                                            <asp:DropDownList ID="ddlHeadingType" runat="server" class="form-control" 
                                                ></asp:DropDownList>
                                           
                                        </div>--%>
    <div>
        <asp:GridView ID="gvUserInfo" runat="server">
            <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
        </asp:GridView>
    </div>
    <div id="Div19" runat="server" align="center" class="col-lg-12" visible="false">
        <asp:Button ID="btnsave" runat="server" Text="Save" UseSubmitBehavior="false" OnClientClick="this.disabled=true;"
            CssClass="btn btn-success" OnClick="btnsave_Click" />
        <asp:Button ID="btnexit" runat="server" Text="Exit" CssClass="btn btn-danger" OnClick="btnexit_Click" />
    </div>
    <div class="loading" align="center" runat="server" visible="false">
        Loading. Please wait.<br />
        <br />
        <img src="../images/Preloader_10.gif" alt="" />
    </div>
    <script src="Tabs/js/src/tabs.js"></script>
    <script>
        var myTabs = tabs({
            el: '#tabs',
            tabNavigationLinks: '.c-tabs-nav__link',
            tabContentContainers: '.c-tab'
        });

        myTabs.init();
    </script>
    <!-- EXTERNAL SCRIPTS FOR CALLMENICK.COM, PLEASE DO NOT INCLUDE -->
    <script src="Tabs/js/lib/githubicons.js"></script>
    <script src="Tabs/js/lib/carbonad.js"></script>
    <!-- /.col-lg-6 (nested) -->
    <!-- /.col-lg-6 (nested) -->
    <!-- EXTERNAL SCRIPTS FOR CALLMENICK.COM, PLEASE DO NOT INCLUDE -->
    <%--    <script type="text/javascript" src="Tabs/js/lib/githubicons.js"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>--%>
    <!-- /EXTERNAL SCRIPTS -->
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    </form>
</body>
</html>
