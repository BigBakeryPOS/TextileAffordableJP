<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NDilloEmbroideringProcess.aspx.cs" Inherits="Billing.Accountsbootstrap.NDilloEmbroideringProcess" %>
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
    <title>Flexible Apparels || Embroidering Process</title>
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
            height: 100%; /* 100% Full-height */
            width: 0; /* 0 width - change this with JavaScript */
            position: absolute; /* Stay in place */
            z-index: 1; /* Stay on top */
            top: 0;
            left: 0;
            background-color: #e6e61a; /* Black*/
            overflow-x: hidden; /* Disable horizontal scroll */
            padding-top: 60px; /* Place content 60px from the top */
            transition: 0.5s; /* 0.5 second transition effect to slide in the sidenav */
        }
        
        /* The navigation menu links */
        .sidenav a
        {
            padding: 8px 8px 8px 32px;
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
            font-size: 36px;
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
                font-size: 18px;
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
    <asp:Label runat="server" ID="lblWelcome" Visible="false" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="Form1" runat="server">
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">
                Embroidering Process
            </h1>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
        <div class="row" style="background-color: #c6efce">
            <div class="col-lg-12">
                <div class="panel panel-default" style="background-color: #c6efce">
                    <div class="panel-body" style="background-color: #c6efce">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <div id="add" runat="server" class="row">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                        <div class="col-lg-2">
                                            <div class="form-group" id="divcode" runat="server" visible="false">
                                                <label>
                                                    Ledgerid</label>
                                                <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server"></asp:TextBox>
                                            </div>
                                            <%-- <div class="form-group" >
                                <label>
                                    Lot No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1" ControlToValidate="ddlLotNo"
                                    Text="*" ErrorMessage="Please Select Lot No!" Style="color: Red" />
                                <asp:DropDownList CssClass="form-control" ID="ddlLotNo" MaxLength="150" runat="server"
                                  OnSelectedIndexChanged="StitchingInfo_Load" AutoPostBack="true"></asp:DropDownList>
                            </div>--%>
                                            <div class="form-group ">
                                                <label>
                                                    Cutting Master</label>
                                                <asp:TextBox CssClass="form-control" ID="txtCuttingMaster" MaxLength="150" Enabled="false"
                                                    runat="server"></asp:TextBox>
                                                <asp:TextBox CssClass="form-control" ID="txtledgerid" MaxLength="150" Visible="false"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    Brand Name</label>
                                                <asp:TextBox CssClass="form-control" ID="txtBrand" MaxLength="150" Enabled="false"
                                                    runat="server"></asp:TextBox>
                                                <asp:TextBox CssClass="form-control" ID="txtbrandid" Visible="false" MaxLength="150"
                                                    Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group ">
                                                <label>
                                                    Process Date</label>
                                                <asp:TextBox CssClass="form-control" ID="txtProcessDate" MaxLength="150" Enabled="false"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    Unit Name</label>
                                                <asp:TextBox CssClass="form-control" ID="txtUnitName" MaxLength="150" Enabled="false"
                                                    runat="server"></asp:TextBox>
                                                <asp:TextBox CssClass="form-control" ID="txtUnitID" Visible="false" MaxLength="150"
                                                    Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div style="width: 100%" class="form-group col-lg-1">
                                                <label>
                                                    Full Qty</label>
                                                <asp:TextBox CssClass="form-control" ID="txtfull" Enabled="false" Width="50%" MaxLength="30"
                                                    Text="0" runat="server"></asp:TextBox>
                                            </div>
                                            <div style="width: 100%" class="form-group col-lg-1">
                                                <label>
                                                    Half Qty</label>
                                                <asp:TextBox CssClass="form-control" ID="txtHalf" Enabled="false" Width="50%" MaxLength="30"
                                                    Text="0" runat="server"></asp:TextBox>
                                            </div>
                                           
                                        </div>
                                        <!-- /.col-lg-6 (nested) -->
                                        <div class="col-lg-1">
                                         <div id="divPiece" class="form-group" runat="server">
                                                <label>
                                                    Total Quantity</label>
                                                <asp:TextBox CssClass="form-control" ID="txtTotalQantity" Enabled="false" MaxLength="30"
                                                    Text="0" runat="server"></asp:TextBox>
                                            </div>
                                            <div id="Div1" class="form-group" runat="server">
                                                <label>
                                                    Design No</label>
                                                <asp:TextBox CssClass="form-control" ID="txtdesignno" Enabled="false" MaxLength="30"
                                                    Text="0" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-2" >
                                         <div id="Div2" class="form-group" runat="server">
                                        <label>Select Employee</label>
                                        <asp:DropDownList ID="drpemployee" runat="server" CssClass="chzn-select" ></asp:DropDownList>
                                        <asp:TextBox ID="txtnewlotprocess" runat="server" Visible="false"></asp:TextBox>
                                        </div>
                                          </div>
                                        <div class="col-lg-2" >
                                         <div id="Div3" class="form-group" runat="server">
                                        <label>Date</label>
                                        <asp:TextBox ID="txtstichdate" runat="server" ></asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender6" TargetControlID="txtstichdate" PopupButtonID="date"
                                          EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                              </ajaxToolkit:CalendarExtender>
                                        </div>
                                        </div>
                                        <div class="col-lg-9">
                                       
                                       
                                            <%--<div class="panel panel-default">--%>
                                            <!-- /.panel-heading -->
                                            <div style="overflow: scroll; height: 300px">
                                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%">
                                                                                        <ItemTemplate>
                                                                                            <%# Container.DataItemIndex + 1 %>
                                                                                            <asp:Label ID="lbltransid" runat="server" style="display:none" ></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                      <asp:TemplateField HeaderText="Lot No" ItemStyle-Width="25%">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="drplotno" OnSelectedIndexChanged="LotIndex_Chnaged" CssClass="chzn-select" AutoPostBack="true"
                                                                                                Width="100%" runat="server" Height="26px" AppendDataBoundItems="true">
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Process Type" ItemStyle-Width="25%">
                                                                                        <ItemTemplate>
                                                                                            <asp:DropDownList ID="drpProcess" OnSelectedIndexChanged="Process_TypeIndex" CssClass="chzn-select" AutoPostBack="true"
                                                                                                Width="100%" runat="server" Height="26px" AppendDataBoundItems="true">
                                                                                            </asp:DropDownList>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="AvaliableQty" ItemStyle-Width="10%">
                                                                                        <ItemTemplate>
                                                                                           <asp:TextBox ID="txtavaQuantity" Enabled="false"
                                                                                                runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:TemplateField HeaderText="Sending Qty" ItemStyle-Width="10%">
                                                                                        <ItemTemplate>
                                                                                           <asp:TextBox ID="txtsendQuantity"  OnTextChanged="txtsendfqtychnaged_text" AutoPostBack="true"
                                                                                                runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Received Quantity" ItemStyle-Width="10%">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtRecQuantity" Enabled="false" OnTextChanged="txtrecqtychnaged_text" AutoPostBack="true"
                                                                                                runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="date" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                                                                                onkeydown="return DateFormat(this, event.keyCode)" Text="" Width="100%" Height="26px"
                                                                                                AppendDataBoundItems="true"></asp:TextBox>
                                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" TargetControlID="date" PopupButtonID="date"
                                                                                                EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                                                                            </ajaxToolkit:CalendarExtender>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="10%">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtRate" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Button ID="ButtonAdd1" runat="server" AutoPostback="true" EnableTheming="false"
                                                                                                Text="Add" OnClick="ButtonAdd1_Click" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                                                </Columns>
                                                                                <%-- <HeaderStyle CssClass="GridviewScrollHeader" BackColor="#c6efce" Font-Bold="True" /> 
                                                             <RowStyle CssClass="GridviewScrollItem" BackColor="#c6efce" Font-Bold="True" /> --%>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </div>
                                            <div id="but" runat="server" class="row">
                                                <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" ValidationGroup="val1"
                                                    Style="width: 120px; margin-top: 25px" OnClick="Add_LotProcessDetails" />
                                                <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" PostBackUrl="NDilloEmbroideringGrid.aspx"
                                                    Style="width: 120px; margin-top: 25px" />
                                            </div>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                </div>
                                <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
                                <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
                                <script type="text/javascript">
                                    window.onload = function () {
                                        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                                    }
                                </script>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                            <ProgressTemplate>
                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                                    right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                                        AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                                        position: fixed; top: 50%; left: 40%;" />
                                    <%--<asp:Image ID="imgUpdateProgress1" runat="server" ImageUrl="../images/loading.gif" />--%>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        </div>
                        </div>
                        </div>
                        </div>
    </form>
    <!-- /.row (nested) -->
    </div>
    <!-- /.panel-body -->
    </div>
    <!-- /.panel -->
    </div>
    <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <!-- /#page-wrapper -->
    <!-- jQuery -->
</body>
</html>
