<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consai.aspx.cs" Inherits="Billing.Accountsbootstrap.Consai" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Consai Issue / Receive</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 900px;
            text-align: center;
            border: 3px solid #0DA9D0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 40px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            padding: 5px;
        }
        .modalPopup .footer
        {
            padding: 3px;
        }
        .modalPopup .button
        {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
        .modalPopup td
        {
            text-align: left;
        }
        
        .pad
        {
            padding-top: 50px;
        }
    </style>
    <%-- <script type="text/javascript" language="javascript">
        function valchk() {
            if (blankchk(document.getElementById('txtcustomername'), "Customer Name")
            //&& dropdownchk(document.getElementById('ddlgroup'), "Account Group")  
        && phonechk(document.getElementById('txtmobileno'), "MobileNo") && phonechk(document.getElementById('txtphoneno'), "PhoneNo")
        && blankchk(document.getElementById('txtblnce'), "Opening Balance") 
        && blankchk(document.getElementById('txtmobileno'), "MobileNo")
        && blankchk(document.getElementById('txtphoneno'), "Phone No") && blankchk(document.getElementById('txtarea'), "Area")
        && blankchk(document.getElementById('txtaddress'), "Address") && blankchk(document.getElementById('txtcity'), "City")
        && emailchk(document.getElementById('txtemail'), "Email")) {
                alert("true");
            }
            else {
                alert("false");
                return false;
            }
        }
	</script>--%>
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
    <style type="text/css">
        /* The side navigation menu */
        .sidenav
        {
            height: 25%; /* 100% Full-height */
            width: 0; /* 0 width - change this with JavaScript */
            position: absolute; /* Stay in place */
            z-index: 1; /* Stay on top */
            top: 0;
            left: 0;
            background-color: #e6e61a; /* Black*/
            overflow-x: hidden; /* Disable horizontal scroll */
            padding-top: 0px; /* Place content 60px from the top */
            transition: 0.5s; /* 0.5 second transition effect to slide in the sidenav */
        }
        
        /* The navigation menu links */
        .sidenav a
        {
            text-decoration: none;
            font-size: 25px;
            color: #818181;
            display: block;
            transition: 0.3s;
        }
        
        /* When you mouse over the navigation links, change their color */
        .sidenav a:hover, .offcanvas a:focus
        {
            color: #f1f1f1;
        }
        
        /* Position and style the close button (top right corner) */
        .sidenav .closebtn
        {
            position: absolute;
            top: 0;
            right: 25px;
            font-size: 16px;
            margin-left: 50px;
        }
        
        /* Style page content - use this if you want to push the page content to the right when you open the side navigation */
        #main
        {
            transition: margin-left .5s;
            padding: 20px;
        }
        
        /* On smaller screens, where height is less than 450px, change the style of the sidenav (less padding and a smaller font size) */
        @media screen and (max-height: 450px)
        {
            .sidenav
            {
                padding-top: 15px;
            }
            .sidenav a
            {
                font-size: 5px;
            }
        }
    </style>
    <script type="text/javascript">

        function openNav() {
            //  alert("HI");
            document.getElementById("mySidenav").style.width = "400px";
        }

        /* Set the width of the side navigation to 0 */
        function closeNav() {
            //  alert("HIII");
            document.getElementById("mySidenav").style.width = "0";
        }


        //         function openNav1() {
        //              alert("HI");
        //             document.getElementById("mySidenav1").style.width = "200px";
        //         }

        //         /* Set the width of the side navigation to 0 */
        //         function closeNav1() {
        //             document.getElementById("mySidenav1").style.width = "0";
        //         }
    </script>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
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
        <script type="text/javascript">
            var ScrollHeight = 300;
            window.onload = function () {
                var grid = document.getElementById('gvnewconsai');
                var gridWidth = grid.offsetWidth;
                var gridHeight = grid.offsetHeight;
                var headerCellWidths = new Array();
                for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                    headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
                }
                grid.parentNode.appendChild(document.createElement("div"));
                var parentDiv = grid.parentNode;

                var table = document.createElement("table");
                for (i = 0; i < grid.attributes.length; i++) {
                    if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                        table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                    }
                }
                table.style.cssText = grid.style.cssText;
                table.style.width = gridWidth + "px";
                table.appendChild(document.createElement("tbody"));
                table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
                var cells = table.getElementsByTagName("TH");

                var gridRow = grid.getElementsByTagName("TR")[0];
                for (var i = 0; i < cells.length; i++) {
                    var width;
                    if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                        width = headerCellWidths[i];
                    }
                    else {
                        width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                    }
                    cells[i].style.width = parseInt(width - 3) + "px";
                    gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
                }
                parentDiv.removeChild(grid);

                var dummyHeader = document.createElement("div");
                dummyHeader.appendChild(table);
                parentDiv.appendChild(dummyHeader);
                var scrollableDiv = document.createElement("div");
                if (parseInt(gridHeight) > ScrollHeight) {
                    gridWidth = parseInt(gridWidth) + 17;
                }
                scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
                scrollableDiv.appendChild(grid);
                parentDiv.appendChild(scrollableDiv);
            }
    </script>
</head>
<body style="background-color: #c6efce">
    <usc:Header ID="Header" runat="server" />
    <form id="Form1" runat="server">
  <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-lg-12" style="margin-top: 6px">
                    <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">
                        Consai Issue / Receive
                    </h1>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
                <div class="row" style="background-color: #c6efce">
                    <div class="col-lg-12">
                        <div class="panel panel-default" style="background-color: #c6efce">
                            <div class="panel-body" style="background-color: #c6efce">
                                <div class="form-group">
                                    <div id="Div1" runat="server" class="row">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                        <div class="col-lg-2">
                                            <div class="form-group ">
                                                <label>
                                                    Branch</label>
                                                <asp:DropDownList ID="drpbranch" OnSelectedIndexChanged="company_SelectedIndexChnaged"
                                                    AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
                                            <div class="form-group">
                                                <label>
                                                    Lot No</label>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlLotNo" ValueToCompare="Select Lot No"
                                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select LotNo"></asp:CompareValidator>
                                                <asp:DropDownList CssClass="chzn-select" Width="200px" Height="150px" ID="ddlLotNo"
                                                    MaxLength="150" runat="server" OnSelectedIndexChanged="StitchingInfo_Load" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:DropDownList Visible="false" CssClass="form-control" ID="drpmultiunit" MaxLength="150"
                                                    runat="server" OnSelectedIndexChanged="MultiUnit_SelectedIndex" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label>
                                                    Date</label>
                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                                    ControlToValidate="txtmultidate" Text="*" ErrorMessage="Please date!" Style="color: Red" />
                                                <asp:TextBox ID="txtmultidate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                                    onkeydown="return DateFormat(this, event.keyCode)" CssClass="form-control" Text=""
                                                    Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtendermult1" TargetControlID="txtmultidate"
                                                    PopupButtonID="txtmultidate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                    CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                                </ItemTemplate>
                                            </div>
                                            <div class="form-group" runat="server" visible="false">
                                                <label>
                                                    Multiple No</label>
                                                <asp:TextBox ID="txtmultiplecode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:TextBox ID="txtmultiid" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group" runat="server" id="emp" visible="true">
                                                <label>
                                                    Select
                                                </label>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpMultiemployee"
                                                    ValueToCompare="Select Name" Operator="NotEqual" Type="String" ErrorMessage="Please Select Name"></asp:CompareValidator>
                                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2" ControlToValidate="drpMultiemployee"
                                    Text="*" ErrorMessage="Please Select Employee" Style="color: Red" />--%>
                                                <asp:DropDownList CssClass="form-control" ID="drpMultiemployee" MaxLength="150" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group" runat="server" id="job" visible="false">
                                                <label>
                                                    Select
                                                </label>
                                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator3" ControlToValidate="drpMultiemployee"
                                    Text="*" ErrorMessage="Please Select Employee" Style="color: Red" />--%>
                                                <asp:DropDownList CssClass="form-control" ID="DropDownList1" MaxLength="150" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <div class="form-group">
                                                <label>
                                                    Total Qty</label>
                                                <asp:TextBox CssClass="form-control" ID="txttotalqty" MaxLength="150" Enabled="false"
                                                    runat="server">0</asp:TextBox>
                                            </div>
                                        </div>
                                        <div id="Div2" class="col-lg-1" runat="server">
                                            <div class="form-group">
                                                <label>
                                                    Amount</label>
                                                <asp:TextBox CssClass="form-control" ID="txtAmount" MaxLength="150" runat="server">0</asp:TextBox>
                                            </div>
                                            <div id="Div3" class="form-group" runat="server" visible="false">
                                                <label>
                                                    Received Qty</label>
                                                <asp:TextBox CssClass="form-control" ID="txtreceivedQty" MaxLength="150" Enabled="false"
                                                    runat="server">0</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <div id="idType" runat="server" visible="false" class="form-group ">
                                                <div class="form-group">
                                                    <label>
                                                        Type</label>
                                                    <asp:DropDownList CssClass="form-control" ID="ddltype" MaxLength="170" runat="server">
                                                        <asp:ListItem Text="Type" Value="Type"></asp:ListItem>
                                                        <asp:ListItem Text="Receive" Value="Receive"></asp:ListItem>
                                                        <asp:ListItem Text="Damage" Value="Damage"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                        </div>
                                        <div id="Div4" runat="server" visible="false" class="form-group ">
                                            <label>
                                                Cutting Master</label>
                                            <asp:TextBox CssClass="form-control" ID="txtCuttingMaster" MaxLength="150" Enabled="false"
                                                runat="server"></asp:TextBox>
                                            <asp:TextBox CssClass="form-control" ID="txtledgerid" MaxLength="150" Visible="false"
                                                runat="server"></asp:TextBox>
                                        </div>
                                        <div id="Div5" runat="server" visible="false" class="form-group">
                                            <label>
                                                Brand Name</label>
                                            <asp:TextBox CssClass="form-control" ID="txtBrand" MaxLength="150" Enabled="false"
                                                runat="server"></asp:TextBox>
                                            <asp:TextBox CssClass="form-control" ID="txtbrandid" Visible="false" MaxLength="150"
                                                Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                        <div id="Div6" runat="server" visible="false" class="form-group">
                                            <span onclick="openNav()">Process Details
                                                <asp:CheckBox ID="DetailView" runat="server" OnCheckedChanged="Detail_checked" AutoPostBack="true"
                                                    Text="" />
                                            </span>
                                        </div>
                                        <div id="Div7" runat="server" visible="false" class="form-group">
                                            <span onclick="openNav()">Rate Details
                                                <asp:CheckBox ID="Ratedetail" runat="server" OnCheckedChanged="RateDetail_checked"
                                                    AutoPostBack="true" Text="" />
                                            </span>
                                        </div>
                                        <div id="mySidenav" visible="false" runat="server" class="sidenav" style="width: 400px;
                                            padding-left: 28px;">
                                            <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
                                            <label runat="server" id="processs" style="font-weight: bold">
                                                Process Details</label>
                                            <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Both"
                                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                                ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                                                font-size: 13px;">
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                <Columns>
                                                    <asp:BoundField DataField="Processmasterid" ItemStyle-Font-Bold="true" HeaderText="Process Name"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="Processtype" ItemStyle-Font-Bold="true" HeaderText="Process Name"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="166px" />
                                                    <asp:BoundField HeaderText="Total Qty" ItemStyle-Font-Bold="true" DataField="TotalQty"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField HeaderText="Remain Qty" DataField="RemainQty" ItemStyle-Font-Bold="true"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                            </asp:GridView>
                                            <label runat="server" id="ratee" style="font-weight: bold">
                                                Rate Details</label>
                                            <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myleft" GridLines="Both"
                                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                                ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="GridViewRate_RowDataBound"
                                                Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                <Columns>
                                                    <asp:BoundField DataField="Processmasterid" HeaderText="Process Name" Visible="false" />
                                                    <asp:BoundField DataField="Processtype" ItemStyle-Font-Bold="true" HeaderText="Process Name"
                                                        HeaderStyle-Width="166px" />
                                                    <asp:BoundField HeaderText="Rate" ItemStyle-Font-Bold="true" DataField="Rate" DataFormatString="{0:0.00}"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div runat="server" visible="false" runat="server" id="divWork">
                                            <label style="font-weight: bold">
                                                Work Process</label>
                                            <asp:GridView runat="server" BorderWidth="1" ID="GridView3" CssClass="myleft" GridLines="Vertical"
                                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                                ShowFooter="true" OnRowDataBound="GridViewWork_RowDataBound" PrintPageSize="30"
                                                AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                <Columns>
                                                    <asp:BoundField DataField="IsKaja" ItemStyle-Font-Bold="true" HeaderText="Kaja" ItemStyle-ForeColor="White" />
                                                    <asp:BoundField DataField="IsEmbroiding" ItemStyle-Font-Bold="true" HeaderText="Embroiding"
                                                        ItemStyle-ForeColor="White" />
                                                    <asp:BoundField DataField="IsWashing" ItemStyle-Font-Bold="true" HeaderText="Washing"
                                                        ItemStyle-ForeColor="White" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <!-- /.col-lg-6 (nested) -->
                                    <div id="Div8" runat="server" visible="false" class="col-lg-3">
                                        <div class="form-group ">
                                            <label>
                                                Process Date</label>
                                            <asp:TextBox CssClass="form-control" ID="txtProcessDate" MaxLength="150" Enabled="false"
                                                runat="server"></asp:TextBox>
                                        </div>
                                        <div id="Div9" runat="server" visible="false" class="form-group">
                                            <label>
                                                Unit Name</label>
                                            <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2" ControlToValidate="ddlUnit"
                                    Text="*" ErrorMessage="Please Select Lot No!" Style="color: Red" />
                                <asp:DropDownList CssClass="form-control" ID="ddlUnit" MaxLength="150" runat="server"></asp:DropDownList>--%>
                                            <asp:TextBox CssClass="form-control" ID="txtUnitName" MaxLength="150" Enabled="false"
                                                runat="server"></asp:TextBox>
                                            <asp:TextBox CssClass="form-control" ID="txtUnitID" Visible="false" MaxLength="150"
                                                Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                        <div id="Div10" runat="server" visible="false" style="width: 50%" class="form-group col-lg-3">
                                            <label>
                                                Full Qty</label>
                                            <asp:TextBox CssClass="form-control" ID="txtfull" Enabled="false" Width="50%" MaxLength="30"
                                                Text="0" runat="server"></asp:TextBox>
                                        </div>
                                        <div id="Div11" runat="server" visible="false" style="width: 50%" class="form-group col-lg-3">
                                            <label>
                                                Half Qty</label>
                                            <asp:TextBox CssClass="form-control" ID="txtHalf" Enabled="false" Width="50%" MaxLength="30"
                                                Text="0" runat="server"></asp:TextBox>
                                        </div>
                                        <div runat="server" visible="false" id="divPiece" class="form-group" runat="server">
                                            <label>
                                                Total Quantity</label>
                                            <asp:TextBox CssClass="form-control" ID="txtTotalQantity" Enabled="false" MaxLength="30"
                                                Text="0" runat="server"></asp:TextBox>
                                        </div>
                                        <div runat="server" visible="false" id="Div12" class="form-group" runat="server">
                                            <label>
                                                Design No</label>
                                            <asp:TextBox CssClass="form-control" ID="txtdesignno" Enabled="false" MaxLength="30"
                                                Text="0" runat="server"></asp:TextBox>
                                        </div>
                                        <%--  <div id="divrate" runat="server" visible="false" style="overflow:scroll; height:260px">--%>
                                        <%--    <asp:LinkButton Text="" ID="Lnk" runat="server"></asp:LinkButton>
                               <ajaxToolkit:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="plpop"
                                                            TargetControlID="Lnk" CancelControlID="btclose" BackgroundCssClass="modalBackground">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Panel ID="plpop" runat="server" ScrollBars="Auto" Height="600px" Width="900px"
                                                            CssClass="modalPopup" Style="display: none">--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12" >
                                        <%--<div class="panel panel-default">--%>
                                        <!-- /.panel-heading -->
                                        <div >
                                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                            <div class="panel-body">
                                                <div>
                                                    <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                                    <table id="Table1">
                                                        <tr>
                                                            <td colspan="5">
                                                                <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                                    OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                                    CssClass="chzn-container" Width="100%" runat="server">
                                                                    <HeaderStyle BackColor="#59d3b4" />
                                                                    <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No" >
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Lot No" Visible="false" ControlStyle-Width="100%"
                                                                            ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="drpLotno" OnSelectedIndexChanged="drpLot_selected" CssClass="chzn-select"
                                                                                    AutoPostBack="true" Width="100%" runat="server" Height="26px" AppendDataBoundItems="true">
                                                                                </asp:DropDownList>
                                                                                <asp:Label ID="lbltransid" runat="server" Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ItemName" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblitemname" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblfitid" Style="display: none" runat="server"></asp:Label>
                                                                                <asp:Label ID="lblfit" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pattern" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPatternid" Style="display: none" runat="server"></asp:Label>
                                                                                <asp:Label ID="lblPattern" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText=" Process Type" ControlStyle-Width="100%" ItemStyle-Width="25%">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="drpProcess" OnSelectedIndexChanged="drpprocess_selected" CssClass="chzn-select"
                                                                                    AutoPostBack="true" Width="100%" runat="server" Height="26px" AppendDataBoundItems="true">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--  <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="25%">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="drpEmp" Width="100%" runat="server" Height="26px"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Total Qty" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtTotalFQty" Enabled="false" runat="server" Width="100%" Height="26px"
                                                                                    AppendDataBoundItems="true"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Send Qty" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsendFQty" OnTextChanged="sendqty_chnaged" AutoPostBack="true"
                                                                                    Enabled="false" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remain Qty" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRemainQty" Enabled="false" runat="server" Width="100%" Height="26px"
                                                                                    AppendDataBoundItems="true"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Snd HS Qty" Visible="false" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsendHQty" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sending Date" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="date" runat="server" Enabled="true" onkeyup="ValidateDate(this, event.keyCode)"
                                                                                    onkeydown="return DateFormat(this, event.keyCode)" Text="" Width="100%" Height="26px"
                                                                                    AppendDataBoundItems="true"></asp:TextBox>
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" TargetControlID="date" PopupButtonID="date"
                                                                                    EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                                                                </ajaxToolkit:CalendarExtender>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%-- <asp:TemplateField HeaderText="Bundle No" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                    <ItemTemplate> 
                                                                    <asp:TextBox ID="txtBundle" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Rec. Qty" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtrecFQty" Enabled="false" runat="server" Width="100%" Height="26px"
                                                                                    AppendDataBoundItems="true">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Damage Qty" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtdamageqty" Enabled="false" runat="server" Width="100%" Height="26px"
                                                                                    AppendDataBoundItems="true">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Rec.HS Qty" Visible="false" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtrecHQty" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Received Date" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="Recdate" runat="server" Enabled="true" onkeyup="ValidateDate(this, event.keyCode)"
                                                                                    onkeydown="return DateFormat(this, event.keyCode)" Text="" Width="100%" Height="26px"
                                                                                    AppendDataBoundItems="true"></asp:TextBox>
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender65" TargetControlID="Recdate" PopupButtonID="Recdate"
                                                                                    EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                                                                </ajaxToolkit:CalendarExtender>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Rate" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtRate" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="ButtonAdd1" runat="server" AutoPostback="true" EnableTheming="false"
                                                                                    Text="Add" OnClick="ButtonAdd1_Click" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:CommandField ShowDeleteButton="True" Visible="false" ButtonType="Button" />
                                                                    </Columns>
                                                                    <%-- <HeaderStyle CssClass="GridviewScrollHeader" BackColor="#c6efce" Font-Bold="True" /> 
                                                             <RowStyle CssClass="GridviewScrollItem" BackColor="#c6efce" Font-Bold="True" /> --%>
                                                                </asp:GridView>
                                                             
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                               <asp:GridView ID="gvnewconsai" AutoGenerateColumns="False" ShowFooter="True" CssClass="chzn-container"
                                                                    Width="100%" runat="server" OnRowDataBound="gvnewconsai_OnRowDataBound">
                                                                    <HeaderStyle BackColor="#59d3b4" />
                                                                    <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No" >
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Design" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldesignno" Text='<%#Eval("Design") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="30 FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts30fsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts30fs" Text='<%#Eval("30fs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="32 FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts32fsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts32fs" Text='<%#Eval("32fs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="34 FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts34fsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts34fs" Text='<%#Eval("34fs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="36 FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts36fsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts36fs" Text='<%#Eval("36fs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="XS FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsxsfsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtsxsfs" Text='<%#Eval("xsfs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtssfsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtssfs" Text='<%#Eval("sfs") %>' Width="100%" Enabled="true" runat="server"
                                                                                    Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="M FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsmfsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtsmfs" Text='<%#Eval("mfs") %>' Width="100%" Enabled="true" runat="server"
                                                                                    Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="L FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtslfsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtslfs" Text='<%#Eval("lfs") %>' Width="100%" Enabled="true" runat="server"
                                                                                    Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="XL FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsxlfsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtsxlfs" Text='<%#Eval("xlfs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="XXL FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsxxlfsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtsxxlfs" Text='<%#Eval("xxlfs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="3XL FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts3xlfsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts3xlfs" Text='<%#Eval("3xlfs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="4XL FS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts4xlfsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts4xlfs" Text='<%#Eval("4xlfs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="30 HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts30hsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts30hs" Text='<%#Eval("30hs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="32 HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts32hsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts32hs" Text='<%#Eval("32hs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="34 HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts34hsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts34hs" Text='<%#Eval("34hs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="36 HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts36hsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts36hs" Text='<%#Eval("36hs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="XS HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsxshsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtsxshs" Text='<%#Eval("xshs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="S HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsshsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtsshs" Text='<%#Eval("shs") %>' Width="100%" Enabled="true" runat="server"
                                                                                    Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="M HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsmhsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtsmhs" Text='<%#Eval("mhs") %>' Width="100%" Enabled="true" runat="server"
                                                                                    Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="L HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtslhsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtslhs" Text='<%#Eval("lhs") %>' Width="100%" Enabled="true" runat="server"
                                                                                    Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="XL HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsxlhsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtsxlhs" Text='<%#Eval("xlhs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="XXL HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsxxlhsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txtsxxlhs" Text='<%#Eval("xxlhs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="3XL HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts3xlhsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts3xlhs" Text='<%#Eval("3xlhs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="4XL HS" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts4xlhsac" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                                <asp:TextBox ID="txts4xlhs" Text='<%#Eval("4xlhs") %>' Width="100%" Enabled="true"
                                                                                    runat="server" Height="26px">0</asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Send Qty" 
                                                                            Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtsendFQtyac" Enabled="false" runat="server" Width="100%" Height="26px"
                                                                                    AppendDataBoundItems="true"></asp:TextBox>
                                                                                <asp:TextBox ID="txtsendFQty" Text='<%#Eval("TtlQty") %>' Enabled="false" runat="server"
                                                                                    Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="StockRatioId" 
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStockRatioId" Text='<%#Eval("StockRatioId") %>' Enabled="false"
                                                                                    runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Masterid" 
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMasterid" Text='<%#Eval("Masterid") %>' Enabled="false" runat="server"
                                                                                    Width="100%" Height="26px" AppendDataBoundItems="true"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Transfabid" 
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTransfabid" Text='<%#Eval("Transfabid") %>' Enabled="false" runat="server"
                                                                                    Width="100%" Height="26px" AppendDataBoundItems="true"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#59d3b4" />
                                                                </asp:GridView>
                                            <%--  </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                        </div>
                                        <div class="col-lg-12">
                                            <div class="col-lg-4">
                                                <div id="but" runat="server" class="row">
                                                    <asp:Button ID="Button1" runat="server" class="btn btn-info" Text="Calc" Visible="false"
                                                        Style="width: 120px; margin-top: 25px" OnClick="Add1_Click" />
                                                    <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" ValidationGroup="val1"
                                                        Style="width: 120px; margin-top: 25px" OnClick="Add_LotProcessDetails" />
                                                    <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" PostBackUrl="~/Accountsbootstrap/ConsaiGrid.aspx"
                                                        Style="width: 120px; margin-top: 25px" />
                                                    <asp:Button ID="btncalc" runat="server" class="btn btn-default" Text="Calc" Style="width: 120px;
                                                        margin-top: 25px" OnClick="btncalc_Click" />
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtnarration" runat="server" CssClass="form-control center-block"
                                                        Placeholder="Type Your Narration" Width="300px" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--</div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                        </div>
                    </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
            </div>
            <!-- /.col-lg-12 -->
            </div> </div>
            <%--   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script> 
<script type="text/javascript" src="../Scripts/gridviewScroll.min.js"></script> 
    <script src="../Scripts/gridviewscroll.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvcustomerorder.ClientID%>').gridviewScroll({width: 800, height: 300 });
        } 
</script>--%>
  <%--      </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                    position: fixed; top: 50%; left: 40%;" />
               
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <!-- /.row (nested) -->
    <!-- /.row -->
    <!-- /#page-wrapper -->
    <!-- jQuery -->
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
<%--    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>--%>
    <asp:Label runat="server" ID="lblWelcome" Visible="false" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    </form>
</body>
</html>
