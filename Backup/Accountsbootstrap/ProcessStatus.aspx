<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessStatus.aspx.cs"
    Inherits="Billing.Accountsbootstrap.ProcessStatus" %>

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
    <title>Qty Details</title>
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


        function switchViews(obj, imG) {
            var div = document.getElementById(obj);
            var img = document.getElementById(imG);
            if (div.style.display == "none") {
                div.style.display = "inline";


                img.src = "../images/minus.gif";

            }
            else {
                div.style.display = "none";
                img.src = "../images/plus.gif";

            }
        }
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
        function Denomination() {


            var gridData = document.getElementById('gvlotprocess');



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
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="col-lg-12">
        <div class="col-lg-12">
            <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">
                Qty Details
            </h1>
        </div>
        <div class="col-lg-12">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-2">
                <div class="form-group ">
                    <label>
                        Branch</label>
                    <asp:DropDownList ID="drpbranch" AutoPostBack="true" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-1">
                <div class="form-group">
                    <label>
                        From Date</label>
                    <asp:TextBox ID="txtfromdate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                        onkeydown="return DateFormat(this, event.keyCode)" CssClass="form-control" Text=""
                        Width="100px" AppendDataBoundItems="true"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtendermult1" TargetControlID="txtfromdate"
                        PopupButtonID="txtmultidate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                        CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-lg-1">
                <div class="form-group">
                    <label>
                        To Date</label>
                    <asp:TextBox ID="txttodate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                        onkeydown="return DateFormat(this, event.keyCode)" CssClass="form-control" Text=""
                        Width="100px" AppendDataBoundItems="true"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txttodate"
                        PopupButtonID="txtmultidate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                        CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-lg-2">
                <asp:Label runat="server" ID="lbl1"></asp:Label><br />
                <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" ValidationGroup="val1"
                    Style="width: 120px;" OnClick="btnsearch_OnClick" />
            </div>
            <div class="col-lg-2">
                <asp:Label ID="Label3" runat="server"></asp:Label><br />
                <asp:TextBox CssClass="form-control" onkeyup="Search_Gridview(this, 'gvlotprocess')"
                    Enabled="true" ID="txtsearch" runat="server" placeholder="Search Text" Width="200px"></asp:TextBox>
            </div>
            <div class="col-lg-1">
                <div class="col-lg-1">
                    <asp:Label ID="lblPrint" runat="server" Visible="false">Print</asp:Label><br />
                    <asp:Button ID="btn" runat="server" Text="Print" Visible="true" CssClass="btn btn-danger"
                        OnClientClick="Denomination()" Width="80px" />
                </div>
            </div>
        </div>
        <div class="row" id="tab-3">
            <div>
                <asp:Panel ID="Panel4" runat="server" ScrollBars="Horizontal" Height="500" Width="100%">
                    <asp:GridView ID="gvlotprocess" AutoGenerateColumns="False" ShowFooter="True" CssClass="myGridStyle"
                        OnRowDataBound="gvexamParticipants_OnRowDataBound" DataKeyNames="Masterid" EmptyDataText="No Data Found"
                        Caption="Full Process Details" GridLines="Both" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ControlStyle-Height="100%">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ControlStyle-Width="100%" />
                            <asp:BoundField DataField="LotValue" HeaderText="LotValue" />
                            <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="UnUse" HeaderText="UnUse" ControlStyle-Width="100%" />
                            <asp:BoundField DataField="Use" HeaderText="Use" ControlStyle-Width="100%" />
                             <asp:BoundField DataField="AQty" HeaderText="Aqty" ControlStyle-Width="100%" />
                            <asp:BoundField DataField="Godown" HeaderText="Godown" ControlStyle-Width="100%" />
                            <asp:BoundField DataField="Despatch" HeaderText="Despatch" ControlStyle-Width="100%" />
                              <asp:BoundField DataField="Return" HeaderText="Return" ControlStyle-Width="100%" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100%" HeaderText="FullProcess"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a href="javascript:switchViews('dv<%# Eval("Masterid") %>', 'imdiv<%# Eval("Masterid") %>');"
                                        style="text-decoration: none;">
                                        <img id="imdiv<%# Eval("Masterid") %>" alt="Show" border="0" src="../images/plus.gif" />
                                    </a>
                                    <%# Eval("LotNo")%>
                                    <div id="dv<%# Eval("Masterid") %>" style="display: none; position: relative;">
                                        <asp:GridView runat="server" ID="gvdetails" CssClass="mGrid" GridLines="Both" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField AccessibleHeaderText="SNo" HeaderText="S.No">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex +1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Process" Visible="true" DataField="Process" />
                                                <asp:BoundField HeaderText="Name" Visible="true" DataField="Name" />
                                                <asp:BoundField HeaderText="Iss" Visible="true" DataField="Iss" />
                                                <asp:BoundField HeaderText="Rec" Visible="true" DataField="Rec" />
                                                <asp:BoundField HeaderText="Dam" Visible="true" DataField="Dam" />
                                                <asp:BoundField HeaderText="Pen" Visible="true" DataField="Pen" />
                                                <asp:BoundField HeaderText="Alt" Visible="true" DataField="Alt" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="idval" runat="server" Text='<%#Eval("Masterid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        window.onload = function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        } </script>
    <asp:Label runat="server" ID="lblWelcome" Visible="false" ForeColor="White" CssClass="label">Welcome
    : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">
    </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    </form>
</body>
</html>
