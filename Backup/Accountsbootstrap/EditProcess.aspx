<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProcess.aspx.cs" Inherits="Billing.Accountsbootstrap.EditProcess" %>

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
    <title>Change JobWorker & Rate</title>
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
</head>
<body style="background-color: #c6efce">
    <usc:Header ID="Header" runat="server" />
    <form id="Form1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="col-lg-12">
                        <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">
                            Change JobWorker & Rate
                        </h1>
                    </div>
                </div>
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
                                        <div class="col-lg-1">
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group ">
                                                <label>
                                                    Branch</label>
                                                <asp:DropDownList ID="drpbranch" AutoPostBack="true" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <div class="form-group ">
                                                <label>
                                                    Mode</label>
                                                <asp:DropDownList ID="ddlmode" AutoPostBack="true" runat="server" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlmode_OnSelectedIndexChanged" Width="100px">
                                                    <asp:ListItem Text="Mode" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Changes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Delete" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label>
                                                    Select Process</label>
                                                <asp:RadioButtonList ID="chkprocess" runat="server" RepeatColumns="9" AutoPostBack="true"
                                                    OnSelectedIndexChanged="chkprocess_OnSelectedIndexChanged">
                                                    <asp:ListItem Text="Stc" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Kaja" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Emb" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Wash" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Print" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Btag" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="Trm" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Cni" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Iron" Value="5"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:TextBox ID="txtid" runat="server" Visible="false"></asp:TextBox>
                                            <div class="form-group">
                                                <label>
                                                    Lot No</label>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                                    Width="20px" Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlLotNo"
                                                    ValueToCompare="Select Lot No" Operator="NotEqual" Type="String" ErrorMessage="Please Select Lot No"></asp:CompareValidator>
                                                <asp:DropDownList CssClass="chzn-select" ID="ddlLotNo" Width="200px" Height="150px"
                                                    MaxLength="150" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLotNo_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <div class="form-group">
                                                <label>
                                                    Date</label><asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                                        ControlToValidate="txtmultidate" Text="*" ErrorMessage="Please date!" Style="color: Red" />
                                                <asp:TextBox ID="txtmultidate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                                    onkeydown="return DateFormat(this, event.keyCode)" CssClass="form-control" Text=""
                                                    Width="150px" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtendermult1" TargetControlID="txtmultidate"
                                                    PopupButtonID="txtmultidate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                    CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                                </ItemTemplate>
                                            </div>
                                            <div id="Div111" class="form-group" runat="server" visible="false">
                                                <label>
                                                    Multiple No</label>
                                                <asp:TextBox ID="txtmultiplecode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:TextBox ID="txtmultiid" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="col-lg-2">
                        </div>
                        <div class="col-lg-6">
                            <table border="2" style="height: 150px; width: 600px">
                                <tr>
                                    <td style="width: 250px">
                                        <asp:Label ID="Label6" runat="server" Text="Current JobWorker" Style="font-size: x-large;
                                            color: Black; font-weight: bold"> </asp:Label><br />
                                        <asp:DropDownList CssClass="chzn-select" ID="drpCurrentJobWorker" MaxLength="150"
                                            Enabled="false" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 250px">
                                        <asp:Label ID="Label4" runat="server" Text="Current JobWorker" Style="font-size: x-large;
                                            color: Black; font-weight: bold">Change JobWorker </asp:Label><br />
                                        <asp:DropDownList CssClass="chzn-select" ID="drpMultiemployee" MaxLength="150" runat="server">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpMultiemployee"
                                            ValueToCompare="Select Name" Operator="NotEqual" Type="String" ErrorMessage="Please Select Name"></asp:CompareValidator>
                                    </td>
                                    <td style="width: 70px" align="center">
                                        <asp:Label ID="Label1" runat="server" Text="Current JobWorker" Style="font-size: x-large;
                                            color: Black; font-weight: bold">Select </asp:Label><br />
                                        <asp:CheckBox ID="chkworker" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Current Rate" Style="font-size: x-large;
                                            color: Black; font-weight: bold"> </asp:Label><br />
                                        <asp:Label runat="server" ID="lblCurrentRate" ForeColor="Black" CssClass="label"
                                            Style="font-size: x-large;" Visible="true">0.00</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label5" runat="server" Text="Change Rate" Style="font-size: x-large;
                                            color: Black; font-weight: bold"> </asp:Label><br />
                                        <asp:TextBox ID="txtChangeRate" runat="server" CssClass="form-control center-block"
                                            Width="150px">0</asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Current JobWorker" Style="font-size: x-large;
                                            color: Black; font-weight: bold">Select </asp:Label><br />
                                        <asp:CheckBox ID="chkrate" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-1">
                            <asp:Label ID="Label7" runat="server" Text="Quantity:" Style="font-size: x-large;
                                color: Black; font-weight: bold"> </asp:Label><br />
                            <asp:Label runat="server" ID="lbllotqty" ForeColor="Black" CssClass="label" Style="font-size: x-large;"
                                Visible="true">0</asp:Label>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtnarration" runat="server" CssClass="form-control center-block"
                                    Placeholder="Type Your Narration" Width="300px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="col-lg-9">
                    </div>
                    <div class="col-lg-3">
                        <div id="Div13" runat="server" class="row">
                            <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" ValidationGroup="val1"
                                Style="width: 120px;" OnClick="btnadd_OnClick" />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" PostBackUrl="~/Accountsbootstrap/Home_Page.aspx"
                                Style="width: 120px;" />
                        </div>
                    </div>
                </div>
            </div>
            </div> </div>
        </ContentTemplate>
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
    </asp:UpdateProgress>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    <asp:Label runat="server" ID="lblWelcome" Visible="false" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    </form>
</body>
</html>
