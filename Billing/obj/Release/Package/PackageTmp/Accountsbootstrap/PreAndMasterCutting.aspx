<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreAndMasterCutting.aspx.cs"
    Inherits="Billing.Accountsbootstrap.PreAndMasterCutting" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1" runat="server">
    <style type="text/css">
        a img
        {
            border: none;
        }
        ol li
        {
            list-style: decimal outside;
        }
        div#container
        {
            width: 780px;
            margin: 0 auto;
            padding: 1em 0;
        }
        div.side-by-side
        {
            width: 100%;
            margin-bottom: 1em;
        }
        div.side-by-side > div
        {
            float: left;
            width: 50%;
        }
        div.side-by-side > div > em
        {
            margin-bottom: 10px;
            display: block;
        }
        .clearfix:after
        {
            content: "\0020";
            display: block;
            height: 0;
            clear: both;
            overflow: hidden;
            visibility: hidden;
        }
    </style>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Pre And Master Cutting</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <script src="../Scripts/jquerynew-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        function alertMessage() {
            alert('Are You Sure, You want to delete This Brand!');
        }
    </script>
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
        function Denomination() {


            var gridData = document.getElementById('gvcust');



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
    <form runat="server" id="form1">
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <div class="row">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header" style="text-align: center; color: #fe0002;">
                Pre And Master Cutting</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <label>
                                    Branch
                                </label>
                                <div class="form-group ">
                                    <asp:DropDownList ID="drpbranch" runat="server" CssClass="form-control" Width="170px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <label>
                                    Type
                                </label>
                                <div class="form-group ">
                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" Width="100px">
                                        <asp:ListItem Text="Summary" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Detailed" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <label>
                                    From Date
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control center-block"
                                        Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                        PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <label>
                                    To Date
                                </label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control center-block" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                        PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <label>
                                    Job Worker</label>
                                <asp:DropDownList ID="ddlsupplier" runat="server" class="chzn-select" Width="190px"
                                    Height="80px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                <label>
                                    Item</label><br />
                                <asp:DropDownList ID="ddlitem" runat="server" class="chzn-select" Width="190px" Height="80px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1">
                                <label>
                                    Search</label>
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" Width="100px"
                                    OnClick="btnsearch_OnClick" />
                            </div>
                            <div class="col-lg-1">
                                <label>
                                    Export-Excel</label>
                                <asp:Button ID="btnexcel" runat="server" class="btn btn-warning" Text="Excel" Width="100px"
                                    OnClick="btnexcel_OnClick" />
                            </div>
                            <div class="col-lg-1">
                                <label>
                                    Print</label>
                                <asp:Button ID="btn" runat="server" Text="Print" Visible="true" CssClass="btn btn-danger"
                                    OnClientClick="Denomination()" Width="100px" />
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <div id="div2" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvcust" Visible="true" runat="server" EmptyDataText="Sorry Data Not Found!"
                                            Width="100%" CssClass="myGridStyle" AutoGenerateColumns="false" ShowFooter="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Cutid" HeaderText="Cutid" Visible="false" />
                                                <asp:BoundField DataField="LotNo" HeaderText="LotNo" />
                                                <asp:BoundField HeaderText="CutDate" DataField="CutDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="Master" HeaderText="Master" />
                                                <asp:BoundField HeaderText="MasterDate" DataField="MasterDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" />
                                                <asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                                                <asp:BoundField DataField="BrandName" HeaderText="BrandName" />
                                                <asp:BoundField DataField="Fit" HeaderText="Fit" Visible="false" />
                                                <asp:BoundField DataField="Width" HeaderText="Width" Visible="false" />
                                                <asp:BoundField DataField="Designno" HeaderText="Designno" />
                                                <asp:BoundField DataField="CF30" HeaderText="CF/30" />
                                                <asp:BoundField DataField="MF30" HeaderText="MF/30" />
                                                <asp:BoundField DataField="DF30" HeaderText="DF/30" />
                                                <asp:BoundField DataField="CF32" HeaderText="CF/32" />
                                                <asp:BoundField DataField="MF32" HeaderText="MF/32" />
                                                <asp:BoundField DataField="DF32" HeaderText="DF/32" />
                                                <asp:BoundField DataField="CF34" HeaderText="CF/34" />
                                                <asp:BoundField DataField="MF34" HeaderText="MF/34" />
                                                <asp:BoundField DataField="DF34" HeaderText="DF/34" />
                                                <asp:BoundField DataField="CF36" HeaderText="CF/36" />
                                                <asp:BoundField DataField="MF36" HeaderText="MF/36" />
                                                <asp:BoundField DataField="DF36" HeaderText="DF/36" />
                                                <asp:BoundField DataField="CFXS" HeaderText="CF/XS" />
                                                <asp:BoundField DataField="MFXS" HeaderText="MF/XS" />
                                                <asp:BoundField DataField="DFXS" HeaderText="DF/XS" />
                                                <asp:BoundField DataField="CFS" HeaderText="CF/S" />
                                                <asp:BoundField DataField="MFS" HeaderText="MF/S" />
                                                <asp:BoundField DataField="DFS" HeaderText="DF/S" />
                                                <asp:BoundField DataField="CFM" HeaderText="CF/M" />
                                                <asp:BoundField DataField="MFM" HeaderText="MF/M" />
                                                <asp:BoundField DataField="DFM" HeaderText="DF/M" />
                                                <asp:BoundField DataField="CFL" HeaderText="CF/L" />
                                                <asp:BoundField DataField="MFL" HeaderText="MF/L" />
                                                <asp:BoundField DataField="DFL" HeaderText="DF/L" />
                                                <asp:BoundField DataField="CFXL" HeaderText="CF/XL" />
                                                <asp:BoundField DataField="MFXL" HeaderText="MF/XL" />
                                                <asp:BoundField DataField="DFXL" HeaderText="DF/XL" />
                                                <asp:BoundField DataField="CFXXL" HeaderText="CF/XXL" />
                                                <asp:BoundField DataField="MFXXL" HeaderText="MF/XXL" />
                                                <asp:BoundField DataField="DFXXL" HeaderText="DF/XXL" />
                                                <asp:BoundField DataField="CF3XL" HeaderText="CF/3XL" />
                                                <asp:BoundField DataField="MF3XL" HeaderText="MF/3XL" />
                                                <asp:BoundField DataField="DF3XL" HeaderText="DF/3XL" />
                                                <asp:BoundField DataField="CF4XL" HeaderText="CF/4XL" />
                                                <asp:BoundField DataField="MF4XL" HeaderText="MF/4XL" />
                                                <asp:BoundField DataField="DF4XL" HeaderText="DF/4XL" />
                                                <asp:BoundField DataField="CH30" HeaderText="CH/30" />
                                                <asp:BoundField DataField="MH30" HeaderText="MH/30" />
                                                <asp:BoundField DataField="DH30" HeaderText="DH/30" />
                                                <asp:BoundField DataField="CH32" HeaderText="CH/32" />
                                                <asp:BoundField DataField="MH32" HeaderText="MH/32" />
                                                <asp:BoundField DataField="DH32" HeaderText="DH/32" />
                                                <asp:BoundField DataField="CH34" HeaderText="CH/34" />
                                                <asp:BoundField DataField="MH34" HeaderText="MH/34" />
                                                <asp:BoundField DataField="DH34" HeaderText="DH/34" />
                                                <asp:BoundField DataField="CH36" HeaderText="CH/36" />
                                                <asp:BoundField DataField="MH36" HeaderText="MH/36" />
                                                <asp:BoundField DataField="DH36" HeaderText="DH/36" />
                                                <asp:BoundField DataField="CHXS" HeaderText="CH/XS" />
                                                <asp:BoundField DataField="MHXS" HeaderText="MH/XS" />
                                                <asp:BoundField DataField="DHXS" HeaderText="DH/XS" />
                                                <asp:BoundField DataField="CHS" HeaderText="CH/S" />
                                                <asp:BoundField DataField="MHS" HeaderText="MH/S" />
                                                <asp:BoundField DataField="DHS" HeaderText="DH/S" />
                                                <asp:BoundField DataField="CHM" HeaderText="CH/M" />
                                                <asp:BoundField DataField="MHM" HeaderText="MH/M" />
                                                <asp:BoundField DataField="DHM" HeaderText="DH/M" />
                                                <asp:BoundField DataField="CHL" HeaderText="CH/L" />
                                                <asp:BoundField DataField="MHL" HeaderText="MH/L" />
                                                <asp:BoundField DataField="DHL" HeaderText="DH/L" />
                                                <asp:BoundField DataField="CHXL" HeaderText="CH/XL" />
                                                <asp:BoundField DataField="MHXL" HeaderText="MH/XL" />
                                                <asp:BoundField DataField="DHXL" HeaderText="DH/XL" />
                                                <asp:BoundField DataField="CHXXL" HeaderText="CH/XXL" />
                                                <asp:BoundField DataField="MHXXL" HeaderText="MH/XXL" />
                                                <asp:BoundField DataField="DHXXL" HeaderText="DH/XXL" />
                                                <asp:BoundField DataField="CH3XL" HeaderText="CH/3XL" />
                                                <asp:BoundField DataField="MH3XL" HeaderText="MH/3XL" />
                                                <asp:BoundField DataField="DH3XL" HeaderText="DH/3XL" />
                                                <asp:BoundField DataField="CH4XL" HeaderText="CH/4XL" />
                                                <asp:BoundField DataField="MH4XL" HeaderText="MH/4XL" />
                                                <asp:BoundField DataField="DH4XL" HeaderText="DH/4XL" />
                                                <asp:BoundField DataField="CFT" HeaderText="CF-Ttl" />
                                                <asp:BoundField DataField="MFT" HeaderText="MF-Ttl" />
                                                <asp:BoundField DataField="DFT" HeaderText="DF-Ttl" />
                                                <asp:BoundField DataField="CHT" HeaderText="CH-Ttl" />
                                                <asp:BoundField DataField="MHT" HeaderText="MH-Ttl" />
                                                <asp:BoundField DataField="DHT" HeaderText="DH-Ttl" />
                                                <asp:BoundField DataField="CFHT" HeaderText="Cut-Ttl" />
                                                <asp:BoundField DataField="MFHT" HeaderText="Mtr-Ttl" />
                                                <asp:BoundField DataField="DFHT" HeaderText="Dmg-Ttl" />
                                                <asp:BoundField DataField="Changes" HeaderText="Changes" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
</body>
</html>
