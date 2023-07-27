<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Abu_Manufacture.aspx.cs"
    Inherits="Billing.Accountsbootstrap.Abu_Manufacture" %>

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
    <title>Cutting Info Grid</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../css/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            ShowPopup();
            setTimeout(HidePopup, 2000);
        }

        function ShowPopup() {
            $find('modalpopup').show();
            //$get('Button1').click();
        }

        function HidePopup() {
            $find('modalpopup').hide();
            //$get('btnCancel').click();
        }
    </script>
    <script type="text/javascript">
        function Denomination() {


            var gridData = document.getElementById('gvcustinfo');


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
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        function alertMessage() {
            alert('Are You Sure, You want to delete This Customer!');
        }
    </script>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="form1" runat="server">
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                font-weight: bold">
                Employee Production Report</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" align="center">
                            <div class="col-lg-2">
                                <div class="form-group" runat="server" visible="false">
                                    <label>
                                        Employers</label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <%--OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"--%>
                                </div>
                            </div>
                            <div class="col-lg-2" style="width: 150px;" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Design Number</label>
                                    <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtDesignNo" runat="server"
                                        MaxLength="150"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2" style="width: 150px;" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Status</label>
                                    <asp:DropDownList CssClass="form-control" ID="ddStatus" runat="server">
                                        <asp:ListItem Text="All"></asp:ListItem>
                                        <asp:ListItem Text="In-Progress" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Completed" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2" style="width: 168px;">
                                <div class="form-group">
                                    <label>
                                        Date</label>
                                    <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtStitchingFromDate" runat="server"
                                        MaxLength="150"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtStitchingFromDate"
                                        runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2" style="width: 150px;" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Stitching To Date</label>
                                    <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtStitchingToDate" runat="server"
                                        MaxLength="150"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtStitchingToDate"
                                        runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-14" runat="server" visible="false">
                                <asp:Button ID="btnSearch" runat="server" class="btn btn-info" Text="Search" Style="margin-top: 25px;
                                    width: 80px;" OnClick="btnSearch_Data" />
                            </div>
                            <div class="col-lg-17" runat="server" visible="false">
                                <asp:Button ID="btnClear" runat="server" class="btn btn-danger" Text="Clear" Style="margin-top: 25px;
                                    width: 80px;" OnClick="btnClear_Data" />
                            </div>
                            <div class="col-lg-15">
                                <asp:Button ID="btnPrint" runat="server" class="btn btn-success" Text="Print" Style="margin-top: 25px;
                                    width: 80px;" OnClientClick="Denomination()" />
                            </div>
                        </div>
                    </div>
                    <div style="height: 392px;" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvcustinfo" runat="server" CssClass="myGridStyle" EmptyDataText="No records Found"
                                        Caption="Abu Garments Employee Manufacture Report" PageSize="100000" OnPageIndexChanging="Page_Change"
                                        AutoGenerateColumns="false" OnRowCommand="gvcustinfo_RowCommand" OnRowDataBound="gvcustinfo_RowDataBound">
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Employee Name" ItemStyle-Font-Bold="true" DataField="EmployeeName" />
                                            <asp:BoundField HeaderText="K" ItemStyle-Font-Bold="true" DataField="K" />
                                            <asp:BoundField HeaderText="B" ItemStyle-Font-Bold="true" DataField="B" />
                                            <asp:BoundField HeaderText="Label" ItemStyle-Font-Bold="true" DataField="Label" />
                                            <asp:BoundField HeaderText="Back" ItemStyle-Font-Bold="true" DataField="Back" />
                                            <asp:BoundField HeaderText="Pocket" ItemStyle-Font-Bold="true" DataField="Pocket" />
                                            <asp:BoundField HeaderText="Front" ItemStyle-Font-Bold="true" DataField="Front" />
                                            <asp:BoundField HeaderText="Sleeve F/S Attach" ItemStyle-Font-Bold="true" DataField="SleeveFS" />
                                            <asp:BoundField HeaderText="F/S" ItemStyle-Font-Bold="true" DataField="F_S" />
                                            <asp:BoundField HeaderText="H/S Sleeve" ItemStyle-Font-Bold="true" DataField="HSSleeve" />
                                            <asp:BoundField HeaderText="H/S Sleeve Attach" ItemStyle-Font-Bold="true" DataField="HSSleeveAttach" />
                                            <asp:BoundField HeaderText="Slleve Top" ItemStyle-Font-Bold="true" DataField="SleeveTop" />
                                            <asp:BoundField HeaderText="Side" ItemStyle-Font-Bold="true" DataField="Side" />
                                            <asp:BoundField HeaderText="Bottom" ItemStyle-Font-Bold="true" DataField="Bottom" />
                                            <asp:BoundField HeaderText="Cuff Ready" ItemStyle-Font-Bold="true" DataField="CuffReady" />
                                            <asp:BoundField HeaderText="Cuff Attach" ItemStyle-Font-Bold="true" DataField="CuffAttach" />
                                            <asp:BoundField HeaderText="Collar Ready" ItemStyle-Font-Bold="true" DataField="CollarReady" />
                                            <asp:BoundField HeaderText="Collar Attach" ItemStyle-Font-Bold="true" DataField="CollarReady" />
                                            <asp:TemplateField HeaderText="Enter Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox CssClass="form-control" TabIndex="2" ID="txtDesignNo1" runat="server"
                                                        MaxLength="150" Style="width: 70px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Enter Incentive">
                                                <ItemTemplate>
                                                    <asp:TextBox CssClass="form-control" TabIndex="2" ID="txtDesignNo11" runat="server"
                                                        MaxLength="150" Style="width: 70px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 1160px">
                                    <label>
                                    </label>
                                    <asp:Label ID="lbltotal" Style="font-size: medium; font-weight: bold" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- /.col-lg-6 (nested) -->
                </div>
                <!-- /.row (nested) -->
            </div>
            <!-- /.panel-body -->
        </div>
        <!-- /.panel -->
    </div>
    <!-- /.col-lg-12 -->
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Customer List</div>
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
