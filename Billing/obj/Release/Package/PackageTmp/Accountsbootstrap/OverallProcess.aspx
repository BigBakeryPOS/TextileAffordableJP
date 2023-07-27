<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OverallProcess.aspx.cs"
    Inherits="Billing.Accountsbootstrap.OverallProcess" %>

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
    <title>Over-All Process Status </title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/responsive-tabs.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
    <script src="../js/jquery.responsiveTabs.min.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.0.min.js" type="text/javascript"></script>
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
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


        function myFunction1() {
            var ButtonControl = document.getElementById("btnprint");
            var fist = document.getElementById("btnexit");

            ButtonControl.style.visibility = "hidden";
            btnexit.style.visibility = "hidden";
            window.print();
        }
    </script>
    <script type="text/javascript">
        function myFunction() {


            var gridData = document.getElementById('pnlPopupLotDeatils');



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
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
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
<body style="background-color: #c6efce">
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1">
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                font-weight: bold">
                Over-All Process Status</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row" style="background-color: #c6efce">
        <div class="col-lg-12" style="background-color: #c6efce">
            <div class="panel panel-default">
                <div class="panel-body" style="background-color: #c6efce">
                    <div class="row">
                        <div class="col-lg-12" align="left">
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group ">
                                        <asp:DropDownList ID="drpbranch" AutoPostBack="true" OnSelectedIndexChanged="drpbranch_OnSelectedIndexChanged"
                                            runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label runat="server" ID="Label1"></asp:Label>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                        Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                                        Text="*" Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                    <asp:DropDownList CssClass="form-control" ID="ddlfilter" Width="150px" Style="margin-top: -20px"
                                        runat="server" Visible="false">
                                        <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Contact Name" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="MobileNo" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="*" Style="color: White" />--%>
                                    <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvcust')"
                                        ID="txtsearch" runat="server" Style="margin-top: -20px" placeholder="Enter Text to Search"
                                        Width="200px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                        FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -.@"
                                        TargetControlID="txtsearch" />
                                </div>
                                <div class="col-lg-4" align="left">
                                    <div class="form-group">
                                        <div class="col-lg-4">
                                            <div runat="server" visible="false" class="col-lg-6">
                                                <asp:Label ID="startlabel" Text="Start date:" runat="server" Style="margin-left: -200px;
                                                    width: 100px; margin-bottom: -10px;" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div runat="server" visible="false" class="col-lg-6">
                                                <asp:TextBox ID="txtstartdate" onkeydown="return DateFormat(this, event.keyCode)"
                                                    runat="server" CssClass="form-control" Style="width: 150px; margin-left: -180px;
                                                    margin-bottom: -10px;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtstartdate"
                                                    PopupButtonID="txtstartdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                    CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div runat="server" visible="false" class="col-lg-6">
                                                <asp:Label ID="endlabel" Text="End date:" runat="server" Style="margin-left: -100px;"
                                                    Font-Bold="true"></asp:Label>
                                            </div>
                                            <div runat="server" visible="false" class="col-lg-6">
                                                <asp:TextBox ID="txtenddate" onkeydown="return DateFormat(this, event.keyCode)" runat="server"
                                                    CssClass="form-control" Style="width: 150px; margin-left: -90px;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtenddate"
                                                    PopupButtonID="txtenddate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                    CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                            <asp:Button ID="btnsearch" runat="server" ValidationGroup="val1" class="btn btn-success"
                                                Text="Search" OnClick="Search_Click" Width="130px" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4" align="right">
                                    <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"
                                        Width="130px" Visible="false" />
                                    <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" OnClick="Add_Click" Visible="false"
                                        Width="130px" />
                                    <asp:Button ID="Button3" runat="server" class="btn btn-success" Text="Bulk Addition"
                                        Width="130px" OnClick="btnFormat_Click" Visible="false" />
                                    <asp:Button ID="btnexcel" runat="server" class="btn btn-info" Text="Export-To-Excel"
                                        Width="130px" Height="32px" OnClick="btnExcel_Click" />
                                </div>
                            </div>
                        </div>
                        <div>
                            <div style="width: 1300px; color: Black; font-weight: bold; padding-top: 20px">
                                <table runat="server" visible="false" border="1" bgcolor="#3090C7" rules="all">
                                    <tr runat="server">
                                        <td style="width: 58px;">
                                            SNo
                                        </td>
                                        <td style="width: 80px;">
                                            LotNo
                                        </td>
                                        <td style="width: 203px;">
                                            Brand
                                        </td>
                                        <td style="width: 139px;">
                                            Master
                                        </td>
                                        <td style="width: 70px;">
                                            <label>
                                                HalfQty</label>
                                        </td>
                                        <td style="width: 70px;">
                                            <label>
                                                FullQty</label>
                                        </td>
                                        <td style="width: 70px;">
                                            <label>
                                                TotalQty</label>
                                        </td>
                                        <td style="width: 57px;">
                                            <label>
                                                Emb</label>
                                        </td>
                                        <td style="width: 57px;">
                                            <label>
                                                Stich</label>
                                        </td>
                                        <%--<td style="width: 58px;">
                                            <label>
                                                Kaja</label>
                                        </td>--%>
                                        <td style="width: 58px;">
                                            <label>
                                                Print</label>
                                        </td>
                                       <%-- <td style="width: 58px;">
                                            <label>
                                                Wash</label>
                                        </td>--%>
                                        <td style="width: 58px;">
                                            <label>
                                                Ironing</label>
                                        </td>
                                      <%--  <td style="width: 58px;">
                                            <label>
                                                BarTag</label>
                                        </td>
                                        <td style="width: 55px;">
                                            <label>
                                                Trimm</label>
                                        </td>
                                        <td style="width: 61px;">
                                            <label>
                                                Consai</label>
                                        </td>--%>
                                        <%--  <td style="width: 74px;">
                                            <label>
                                                Status</label>
                                        </td>--%>
                                        <td style="width: 73px;">
                                            <label>
                                                LotDeatils</label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="height: 500px; width: 1300px; overflow: auto;">
                                <asp:GridView ID="gvcust" runat="server" CssClass="myGridStyle" EmptyDataText="No records Found"
                                    AllowPaging="false" AutoGenerateColumns="false" OnRowCommand="gvcust_RowCommand"
                                    ShowHeader="true" OnRowDataBound="gvcust_RowDataBound" Style="width: 100%; margin-left: 0px">
                                    <HeaderStyle BackColor="#c6efce" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                        NextPageText="Next" PreviousPageText="Previous" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo" ItemStyle-Width="5px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="LotNo" DataField="CompanyLotNo" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="Item Code" DataField="ItemCode" ItemStyle-Width="1px" />
                                        <asp:BoundField HeaderText="Master" DataField="LedgerName" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="HalfQty" DataField="HalfQty" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="FullQty" DataField="FullQty" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="TotalQty" DataField="TotalQuantity" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="Embroiding" DataField="emb" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="Stiching" DataField="stc" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="Kaja" Visible="false" DataField="kaj" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="Print" DataField="pri" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="Washing" Visible="false" DataField="was" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="Ironing" DataField="irn" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="BarTag" DataField="BarTag" Visible="false" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="Trimming" DataField="Trimming" Visible="false" ItemStyle-Width="5px" />
                                        <asp:BoundField HeaderText="IsConsai" DataField="IsConsai" ItemStyle-Width="5px" Visible="false" />
                                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnstatus" runat="server" CommandArgument='<%#Eval("LotDetailID") %>'
                                                    CommandName="Status">
                                                    <asp:Image ID="img1" runat="server" ImageUrl="~/images/info_button.png" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RawDetails" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnProcessDetails" runat="server" CommandArgument='<%#Eval("LotDetailID") %>'
                                                    CommandName="ProcessDetails">
                                                    <asp:Image ID="img1new" runat="server" ImageUrl="~/images/info_button.png" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LotDeatils" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px"
                                            Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnLotDeatils" runat="server" CommandArgument='<%#Eval("LotDetailID") %>'
                                                    CommandName="LotDeatils">
                                                    <asp:Image ID="img1new1" runat="server" ImageUrl="~/images/info_button.png" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" Visible="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("LotDetailID") %>'
                                                    CommandName="edit">
                                                    <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                    Enabled="false" ToolTip="Not Allow To Delete" />
                                                <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("LotDetailID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" Visible="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("Cutid") %>'
                                                    CommandName="delete" OnClientClick="alertMessage()">
                                                    <asp:Image ID="dlt" runat="server" ImageAlign="Middle" ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
                                                <asp:ImageButton ID="imgdisable1" ImageUrl="~/images/delete.png" runat="server" Visible="false"
                                                    Enabled="false" ToolTip="Not Allow To Delete" />
                                                <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                                    CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btndelete"
                                                    PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                                    TargetControlID="btndelete" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                                </ajaxToolkit:ConfirmButtonExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="LotDetail ID" DataField="LotDetailID" Visible="false"
                                            ItemStyle-Width="3px" />
                                    </Columns>
                                    <%--   <HeaderStyle  Width="100%" BackColor="#c6efce" Font-Bold="True" /> 
                                         <RowStyle  Width="100%" BackColor="#c6efce" Font-Bold="True" /> --%>
                                    <%-- <PagerStyle CssClass="GridviewScrollPager" /> --%>
                                    <%--      <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />--%>
                                </asp:GridView>
                            </div>
                            <asp:LinkButton Text="" ID="lnkFake" runat="server"></asp:LinkButton>
                            <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                                TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="pnlPopup" runat="server" ScrollBars="Auto" Height="600px" Width="1200px"
                                CssClass="modalPopup" Style="display: none">
                                <div class="header">
                                    Process Status Details
                                </div>
                                <div class="body">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <div class="table-responsive" id="divLot1" runat="server">
                                                    <div id="tabs" style="background-color: #D0D3D6;">
                                                        <ul>
                                                            <li><a href="#tabs-6">OverAll</a></li>
                                                            <li><a href="#tabs-1">Stitching</a></li>
                                                            <li><a href="#tabs-2">Kaja</a></li>
                                                            <li><a href="#tabs-3">Embroiding</a></li>
                                                            <li><a href="#tabs-4">Washing</a></li>
                                                            <li><a href="#tabs-7">Printing</a></li>
                                                            <li><a href="#tabs-5">Iron</a></li>
                                                            <li><a href="#tabs-8">BarTag</a></li>
                                                            <li><a href="#tabs-9">Trimming</a></li>
                                                            <li><a href="#tabs-10">Consai</a></li>
                                                        </ul>
                                                        <div class="row" id="tabs-6" style="background-color: White; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <asp:Label ID="Label5" Text="Process Not Started/ No Process" Font-Bold="true" Font-Size="Larger"
                                                                    ForeColor="Black" runat="server" BackColor="Yellow"></asp:Label>
                                                                --->
                                                                <asp:Label ID="Label6" Text="Process Started " Font-Bold="true" Font-Size="Larger"
                                                                    ForeColor="Black" runat="server" BackColor="Red"></asp:Label>
                                                                --->
                                                                <asp:Label ID="Label11" Text="Process Completed" Font-Bold="true" Font-Size="Larger"
                                                                    ForeColor="Black" runat="server" BackColor="Green"></asp:Label>
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label3" Style="color: Blue;" runat="server">OverAll Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td style="width: 100%">
                                                                                    <div id="idlblstiching" runat="server" visible="true">
                                                                                        <asp:Label ID="lblstiching" Text="Stiching" Font-Bold="true" Font-Size="Larger" ForeColor="Black"
                                                                                            runat="server" BackColor="Yellow"></asp:Label>
                                                                                        --------->
                                                                                    </div>
                                                                                    <div id="idlblKaja" runat="server" visible="true">
                                                                                        <asp:Label ID="lblKaja" Text="Kaja" runat="server" Font-Bold="true" Font-Size="Larger"
                                                                                            ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                                                        --------->
                                                                                    </div>
                                                                                    <div id="idlblemb" runat="server" visible="true">
                                                                                        <asp:Label ID="lblemb" Text="Embroiding" Font-Bold="true" Font-Size="Larger" runat="server"
                                                                                            ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                                                        --------->
                                                                                    </div>
                                                                                    <div id="idlblwash" runat="server" visible="true">
                                                                                        <asp:Label ID="lblwash" Text="Washing" Font-Bold="true" Font-Size="Larger" runat="server"
                                                                                            ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                                                        --------->
                                                                                    </div>
                                                                                    <div id="idlblprint" runat="server" visible="false">
                                                                                        <asp:Label ID="lblprint" Text="Printing" Font-Bold="true" Font-Size="Larger" runat="server"
                                                                                            ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                                                        --------->
                                                                                    </div>
                                                                                    <div id="idlbliron" runat="server" visible="true">
                                                                                        <asp:Label ID="lbliron" Text="Iron and Packing" Font-Bold="true" Font-Size="Larger"
                                                                                            runat="server" ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                                                        --------->
                                                                                    </div>
                                                                                    <div id="idlblbartag" runat="server" visible="true">
                                                                                        <asp:Label ID="lblbartag" Text="BarTag" Font-Bold="true" Font-Size="Larger" runat="server"
                                                                                            ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                                                        --------->
                                                                                    </div>
                                                                                    <div id="idlbltrimming" runat="server" visible="true">
                                                                                        <asp:Label ID="lbltrimming" Text="Trimming" Font-Bold="true" Font-Size="Larger" runat="server"
                                                                                            ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                                                        --------->
                                                                                    </div>
                                                                                    <div id="idlblconsai" runat="server" visible="true">
                                                                                        <asp:Label ID="lblconsai" Text="Consai" Font-Bold="true" Font-Size="Larger" runat="server"
                                                                                            ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-1" style="background-color: White; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label2" Style="color: Blue;" runat="server">Stiching Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="Gridoverall" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pattern" HeaderText="Pattern Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Fit" HeaderText="Fit Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--   <asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--  <asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="DmgQty" HeaderText="Damage Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-2" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label7" Style="color: Blue;" runat="server">Kaja Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gridkaja" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pattern" HeaderText="Pattern Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Fit" HeaderText="Fit Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="DmgQty" HeaderText="Damage Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-3" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label8" Style="color: Blue;" runat="server">Embroiding Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gridemb" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pattern" HeaderText="Pattern Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Fit" HeaderText="Fit Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="DmgQty" HeaderText="Damage Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-4" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label9" Style="color: Blue;" runat="server">Washing Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gridwash" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pattern" HeaderText="Pattern Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Fit" HeaderText="Fit Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="DmgQty" HeaderText="Damage Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-7" style="background-color: White; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label4" Style="color: Blue;" runat="server">Printing Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gridprint" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pattern" HeaderText="Pattern Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Fit" HeaderText="Fit Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--   <asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--  <asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="DmgQty" HeaderText="Damage Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-5" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label10" Style="color: Blue;" runat="server">Iron and Packing Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gridiron" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pattern" HeaderText="Pattern Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Fit" HeaderText="Fit Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="DesignNo" HeaderText="Design Number" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="DmgQty" HeaderText="Damage Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-8" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label12" Style="color: Blue;" runat="server">BarTag Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvbartag" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pattern" HeaderText="Pattern Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Fit" HeaderText="Fit Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="DesignNo" HeaderText="Design Number" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="DmgQty" HeaderText="Damage Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-9" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label13" Style="color: Blue;" runat="server">Trimming Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvtrimming" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pattern" HeaderText="Pattern Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Fit" HeaderText="Fit Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="DesignNo" HeaderText="Design Number" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="DmgQty" HeaderText="Damage Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-10" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label14" Style="color: Blue;" runat="server">Consai Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvconsai" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="Item" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pattern" HeaderText="Pattern Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Fit" HeaderText="Fit Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="DesignNo" HeaderText="Design Number" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                            <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="DmgQty" HeaderText="Damage Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                                            <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
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
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="footer" align="right">
                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                                </div>
                            </asp:Panel>
                            <asp:LinkButton Text="" ID="lnkFakenew" runat="server"></asp:LinkButton>
                            <ajaxToolkit:ModalPopupExtender ID="mpenew" runat="server" PopupControlID="pnlPopupnew"
                                TargetControlID="lnkFakenew" CancelControlID="Button1" BackgroundCssClass="modalBackground">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="pnlPopupnew" runat="server" ScrollBars="Auto" Height="600px" Width="1200px"
                                CssClass="modalPopup" Style="display: none">
                                <div class="header">
                                    Raw Materials Details
                                </div>
                                <div class="body">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <div class="table-responsive" id="divLot1new" runat="server">
                                                    <div id="tabss" style="background-color: #D0D3D6;">
                                                        <ul>
                                                            <li><a href="#tabs-11">Stiching</a></li>
                                                            <li><a href="#tabs-22">Kaja</a></li>
                                                            <li><a href="#tabs-55">Embroiding</a></li>
                                                            <li><a href="#tabs-33">Washing</a></li>
                                                            <li><a href="#tabs-44">Printing</a></li>
                                                            <li><a href="#tabs-77">Iron</a></li>
                                                            <li><a href="#tabs-88">BarTag</a></li>
                                                            <li><a href="#tabs-99">Trimming</a></li>
                                                            <li><a href="#tabs-1010">Consai</a></li>
                                                        </ul>
                                                        <div class="row" id="tabs-11" style="background-color: White; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label23" Style="color: Blue;" runat="server">Stiching Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvstiching" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="FullSL" HeaderText="FullSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="HalfSL" HeaderText="HalfSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Definition" HeaderText="Accessories Code" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Serial_No" HeaderText="Accessories Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="category" HeaderText="category Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="categoryid" Visible="false" HeaderText="categoryid" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="CategoryUserID" Visible="false" HeaderText="CategoryUserID"
                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-22" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label24" Style="color: Blue;" runat="server">Kaja Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvkaja" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="FullSL" HeaderText="FullSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="HalfSL" HeaderText="HalfSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Definition" Visible="false" HeaderText="Definition" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Serial_No" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="category" HeaderText="category" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="categoryid" Visible="false" HeaderText="categoryid" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="CategoryUserID" Visible="false" HeaderText="CategoryUserID"
                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-55" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label28" Style="color: Blue;" runat="server">Embroiding Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvembroiding" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="FullSL" HeaderText="FullSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="HalfSL" HeaderText="HalfSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Definition" Visible="false" HeaderText="Definition" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Serial_No" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="category" HeaderText="category" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="categoryid" Visible="false" HeaderText="categoryid" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="CategoryUserID" Visible="false" HeaderText="CategoryUserID"
                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-33" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label25" Style="color: Blue;" runat="server">Washing Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvwashing" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="FullSL" HeaderText="FullSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="HalfSL" HeaderText="HalfSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Definition" Visible="false" HeaderText="Definition" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Serial_No" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="category" HeaderText="category" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="categoryid" Visible="false" HeaderText="categoryid" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="CategoryUserID" Visible="false" HeaderText="CategoryUserID"
                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-44" style="background-color: #D0D3D6; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label26" Style="color: Blue;" runat="server">Printing Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvprinting" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="FullSL" HeaderText="FullSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="HalfSL" HeaderText="HalfSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Definition" Visible="false" HeaderText="Definition" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Serial_No" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="category" HeaderText="category" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="categoryid" Visible="false" HeaderText="categoryid" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="CategoryUserID" Visible="false" HeaderText="CategoryUserID"
                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-77" style="background-color: White; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label27" Style="color: Blue;" runat="server">Iron and Packing Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvironing" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="FullSL" HeaderText="FullSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="HalfSL" HeaderText="HalfSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Definition" Visible="false" HeaderText="Definition" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Serial_No" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="category" HeaderText="category" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="categoryid" Visible="false" HeaderText="categoryid" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="CategoryUserID" Visible="false" HeaderText="CategoryUserID"
                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-88" style="background-color: White; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label15" Style="color: Blue;" runat="server">BarTag Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvdbartag" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="FullSL" HeaderText="FullSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="HalfSL" HeaderText="HalfSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Definition" Visible="false" HeaderText="Definition" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Serial_No" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="category" HeaderText="category" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="categoryid" Visible="false" HeaderText="categoryid" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="CategoryUserID" Visible="false" HeaderText="CategoryUserID"
                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-99" style="background-color: White; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label16" Style="color: Blue;" runat="server">Trimming Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvdtrimming" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="FullSL" HeaderText="FullSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="HalfSL" HeaderText="HalfSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Definition" Visible="false" HeaderText="Definition" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Serial_No" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="category" HeaderText="category" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="categoryid" Visible="false" HeaderText="categoryid" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="CategoryUserID" Visible="false" HeaderText="CategoryUserID"
                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="tabs-1010" style="background-color: White; padding-top: 30px">
                                                            <div style="background-color: #D0D3D6;">
                                                                <h2 align="center">
                                                                    <asp:Label ID="Label17" Style="color: Blue;" runat="server">Consai Details</asp:Label></h2>
                                                                <div class=" form-group">
                                                                    <div class="table-responsive">
                                                                        <table style="width: 85%">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="gvdconsai" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                                                        ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                                                        ShowHeaderWhenEmpty="True">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="FullSL" HeaderText="FullSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="HalfSL" HeaderText="HalfSL" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Definition" Visible="false" HeaderText="Definition" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="Serial_No" HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="category" HeaderText="category" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="categoryid" Visible="false" HeaderText="categoryid" ItemStyle-HorizontalAlign="Center" />
                                                                                            <asp:BoundField DataField="CategoryUserID" Visible="false" HeaderText="CategoryUserID"
                                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                                        </Columns>
                                                                                        <HeaderStyle BackColor="#990000" />
                                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField ID="selected_tab" runat="server" />
                                                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
                                                    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
                                                    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
                                                        rel="stylesheet" type="text/css" />
                                                    <script type="text/javascript">
                                                        var selected_tab = 1;
                                                        $(function () {
                                                            var tabs = $("#tabs").tabs({
                                                                select: function (e, i) {
                                                                    selected_tab = i.index;
                                                                }
                                                            });
                                                            selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
                                                            tabs.tabs('select', selected_tab);
                                                            $("form").submit(function () {
                                                                $("[id$=selected_tab]").val(selected_tab);
                                                            });
                                                        });
    
                                                    </script>
                                                    <script type="text/javascript">
                                                        var selected_tab = 1;
                                                        $(function () {
                                                            var tabs = $("#tabss").tabs({
                                                                select: function (e, i) {
                                                                    selected_tab = i.index;
                                                                }
                                                            });
                                                            selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
                                                            tabs.tabs('select', selected_tab);
                                                            $("form").submit(function () {
                                                                $("[id$=selected_tab]").val(selected_tab);
                                                            });
                                                        });
    
                                                    </script>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="footer" align="right">
                                    <asp:Button ID="Button1" runat="server" Text="Close" CssClass="button" />
                                </div>
                            </asp:Panel>
                            <div class="col-lg-12">
                                <div class="col-lg-1">
                                </div>
                                <div class="col-lg-10">
                                    <asp:LinkButton Text="" ID="lnkFakenewLotDeatils" runat="server"></asp:LinkButton>
                                    <ajaxToolkit:ModalPopupExtender ID="mpenewLotDeatils" runat="server" PopupControlID="pnlPopupLotDeatils"
                                        TargetControlID="lnkFakenewLotDeatils" CancelControlID="btnmpenewLotDeatils"
                                        BackgroundCssClass="modalBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopupLotDeatils" runat="server" ScrollBars="Auto" CssClass="modalPopup"
                                        Height="600px">
                                        <div class="header">
                                        </div>
                                        <div class="body">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <div class="table-responsive" id="div1" runat="server">
                                                            <div>
                                                                <asp:GridView ID="GVJpStiching" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="Stiching Process" EmptyDataText="Not In Process">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="GVJpEmbroiding" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="Embroiding Process" EmptyDataText="Not In Process">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="GVJpKajaButton" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="KajaButton Process" EmptyDataText="Not In Process">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="GVJpPrinting" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="Printing Process" EmptyDataText="Not In Process">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="GVJpWashing" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="Washing Process" EmptyDataText="Not In Process">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="GVJpBarTag" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="BarTag Process" EmptyDataText="Not In Process">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="GVJpTrimming" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="Trimming Process" EmptyDataText="Not In Process">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="GVJpConsai" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="Consai Process" EmptyDataText="Not In Process">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="GVJpIroning" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="Ironing Process" EmptyDataText="Not In Process">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                                <asp:GridView ID="GVDespatchstock" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                                                    Width="80%" Caption="Despatch Stock" EmptyDataText="Not To Despatch">
                                                                    <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="DcNo" HeaderText="DcNo" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="DcDate" HeaderText="DcDate" ItemStyle-HorizontalAlign="Center"
                                                                            DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField DataField="TotalDespatchqty" HeaderText="TotalDespatchqty" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="footer" align="right">
                                            <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" />
                                            <asp:Button ID="btnexit" runat="server" Text="Exit" />
                                            <asp:Button ID="btnmpenewLotDeatils" runat="server" Text="Close" CssClass="button" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-lg-1">
                                </div>
                            </div>
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
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
    <%--    <script src="../Scripts/GridScrollJquery.js" type="text/javascript"></script>
    <script src="../Scripts/GridScrollJquery1.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="../Scripts/gridviewScroll.min.js"></script>
    <script src="../Scripts/gridviewscroll.js" type="text/javascript"></script>
    </div>
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
