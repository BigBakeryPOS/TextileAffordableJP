<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cashsales.aspx.cs" Inherits="Billing.Accountsbootstrap.cashsales" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
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
        
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            border-width: 3px;
            border-style: solid;
            color: White;
            padding-top: 10px;
            padding-left: 10px;
            width: 900px;
            height: 540px;
        }
    </style>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <script type="text/javascript">
        jQuery(function () {
            jQuery("#inf_custom_someDateField").datepicker();
        });

       

    </script>
    <script language="javascript" type="text/javascript">

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else

                event.returnValue = false;
        }

    </script>
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
    <title>CashSales Page - bootsrap</title>
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
        function myFunction() {
            window.open("http://localhost:49197/Accountsbootstrap/itempage.aspx?Mode=Sales", "Popup", 'height=300,width=500,resizable=yes,modal=yes,center=yes');
        }
    </script>
    <script type="text/javascript">
        function AddVendor() {
            window.open("http://localhost:49197/Accountsbootstrap/customermaster.aspx?Mode=Sales", "Popup", 'height=300,width=500,resizable=yes,modal=yes,center=yes');
        }
    </script>
    <style>
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
    <link rel="stylesheet" href="../css/chosen.css" />
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <form id="Form1" runat="server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <h2 style="text-align: left; color: #fe0002; margin-top: 0px">
        <asp:Label ID="lblTitle" Text="Sales Invoice" runat="server"></asp:Label></h2>
    <h4>
        <%--<div runat="server" style="text-align:right;margin-top:-30px;margin-right:15px;">
                             
                             <input type="button" value="View RefNo" onclick="window.open('RefHistory.aspx','popUpWindow','height=500,width=900,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">
                                 &nbsp;&nbsp

                            <asp:Button  ID="gridbutton" style="margin-top:-47px;margin-left:132px" runat="server" Text="View grid" OnClick="gridbutton_click" />
                                 &nbsp;&nbsp
                                            <asp:CheckBox Visible="false" ID="chknewcust" onclientclick="window.open('http://www.bigdbiz.com');" runat="server" Text="New Customer" style="color:#00a8e6" AutoPostBack="false" OnCheckedChanged="checkbox1_changed"  />
                                              <asp:LinkButton ID="LinkButton1" runat="server"  Text="New Customer" onclientclick="window.open('customermaster.aspx?name=Add%20New');"></asp:LinkButton>            

                                            </div>--%>
        <asp:CheckBox Visible="false" ID="chknewcust" onclientclick="window.open('http://www.bigdbiz.com');"
            runat="server" Text="New Customer" Style="color: #00a8e6" AutoPostBack="false"
            OnCheckedChanged="checkbox1_changed" />
    </h4>
    <!-- /.col-lg-12 -->
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div runat="server" visible="false" class="col-lg-12">
                            <div class="col-lg-2">
                                <label>
                                    Billing Type :</label>
                            </div>
                            <div class="col-lg-3" style="margin-left: -110px">
                                <asp:RadioButtonList ID="rbtype" runat="server" RepeatColumns="2" AutoPostBack="true"
                                    OnSelectedIndexChanged="rbtype_OnSelectedIndexChanged">
                                    <asp:ListItem Text="Direct Sales" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="From Proforma Invoice" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-lg-2">
                                <div id="Div22" runat="server" visible="false">
                                    <label>
                                        Select Proforma Invoice No</label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div id="Div23" runat="server" visible="false">
                                    <asp:DropDownList ID="drpPO" runat="server" AutoPostBack="true" TabIndex="3" CssClass="form-control"
                                        OnSelectedIndexChanged="drpPO_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-2">
                                <label>
                                </label>
                                <label id="Label8" runat="server" visible="false">
                                </label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    Sales Type</label>
                                <asp:DropDownList ID="drpSalesType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Whole sales" Selected="True" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Retail" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                            </div>
                            <div class="form-group">
                                <label>
                                    Book</label>
                                <asp:DropDownList ID="ddlbook" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                    AutoPostBack="true" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Cash sales" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Credit sales" Selected="True" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                            </div>
                            <div id="Div1" runat="server" class="form-group">
                                <label>
                                    Bill Number</label><br />
                                <asp:Label ID="cmpnameyear" runat="server" Font-Bold="true" Font-Size="Larger" Text=""></asp:Label>
                                <table style="text-align: left">
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblprefix" runat="server" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox Width="50px" ID="txtbillno" Enabled="false" OnTextChanged="txtbillcheck"
                                                AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblsufix" runat="server" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="yearss" runat="server" Font-Bold="true" Font-Size="Larger" Text="/2018-19"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <div id="Diiv1" runat="server" visible="false" class="form-group">
                                <label>
                                    Vou.No</label>
                                <asp:TextBox CssClass="form-control" ID="txtvouno" runat="server"></asp:TextBox>
                            </div>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtvoudate"
                                PopupButtonID="txtvoudate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                CssClass="cal_Theme1">
                            </ajaxToolkit:CalendarExtender>
                            <div runat="server" id="exiscust" class="form-group">
                                <label>
                                    Customer Name</label>
                                <asp:DropDownList runat="server" Width="195px" Height="60px" CssClass="chzn-select"
                                    ID="ddlcustomerID" EnableViewState="true" class="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlcustomerID_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label Visible="false" ID="lbladdress" runat="server" BorderColor="Black" Width="195px"
                                    Font-Size="14px" Style="overflow: auto; height: 62px"></asp:Label>
                                <asp:TextBox ID="txtCustname" Visible="false" CssClass="form-control" TabIndex="1"
                                    runat="server"></asp:TextBox>
                                <asp:Button ID="newcstomer" Visible="false" runat="server" Text="New" ToolTip="Create new customer"
                                    OnClick="newcstomer_click" />
                            </div>
                            <div class="form-group" runat="server" id="kl">
                                <label>
                                    Customer Name</label>
                                <asp:TextBox ID="TextBox2" Width="189px" ForeColor="Red" CssClass="form-control"
                                    runat="server"></asp:TextBox>
                            </div>
                            <div id="Div2" runat="server" visible="false" class="form-group">
                                <label>
                                    Bill Date</label>
                                <asp:TextBox CssClass="form-control" ID="txtdate1" runat="server" Text="-----Select Date-----"></asp:TextBox>
                            </div>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate1" PopupButtonID="txtdate1"
                                EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                            </ajaxToolkit:CalendarExtender>
                            <div id="Div3" runat="server" visible="false" class="form-group">
                                <label id="lblbillTo" runat="server">
                                    Bill To</label>
                                <asp:DropDownList ID="bblbillto" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="bblbillto_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div id="Div4" runat="server" visible="false" class="form-group">
                                <label>
                                </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                            </div>
                            <div id="Div5" runat="server" visible="false" class="form-group">
                                <label style="margin-top: -11px;">
                                    Payment Mode</label>
                                <asp:DropDownList ID="ddlPayMode" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                    AutoPostBack="false" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select Payment Mode" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Credit" Selected="True" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                            </div>
                            <%--<asp:Label ID="Label2" runat="server"  style="color:Red"></asp:Label>--%>
                            <%--<div class="form-group">
                                            <label></label>
                                            <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                                        </div>--%>
                            <div id="Div6" runat="server" visible="false" class="form-group">
                                <label>
                                    Narration</label>
                                <asp:TextBox CssClass="form-control" ID="txtNarration" runat="server"></asp:TextBox>
                                <label id="Label3" runat="server" style="color: Red">
                                </label>
                            </div>
                            <div id="Div7" runat="server" visible="false" class="form-group">
                                <label id="Label2" runat="server">
                                    Bank Name</label>
                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>
                                    Customer Address</label>
                                <asp:TextBox ID="txtadd" TextMode="MultiLine" Width="189px" ForeColor="Red" CssClass="form-control"
                                    Style="resize: none" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="chkshipaddr" runat="server" Text="Same as Billing Address" Checked="false"
                                    AutoPostBack="true" OnCheckedChanged="chkshipaddr_CheckedChanged" />
                                <label>
                                    Shipping Address</label>
                                <asp:TextBox ID="txtShipaddress" TextMode="MultiLine" Width="189px" ForeColor="Red"
                                    CssClass="form-control" Style="resize: none" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group" style="display: none">
                                <label>
                                    Agent Name</label>
                                <asp:DropDownList Width="195px" Height="60px" runat="server" CssClass="chzn-select"
                                    ID="ddlRepname" OnSelectedIndexChanged="ddlrep_SelectedIndexChanged" AutoPostBack="false"
                                    class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div id="Div16" runat="server" class="form-group">
                                <label>
                                    Mobile No.</label>
                                <asp:TextBox CssClass="form-control" ID="txtmobileno" runat="server" AutoPostBack="false"
                                    OnTextChanged="txtmobileno_TextChanged2"></asp:TextBox>
                                <label id="lblmoberror" runat="server" style="color: Red">
                                </label>
                            </div>
                            <div class="form-group">
                                <label>
                                    Province</label>
                                <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="Select Province type" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="TamilNadu" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Others" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" style="display: none">
                                <label>
                                    Vehicle No.</label>
                                <asp:TextBox CssClass="form-control" ID="txtlrno" runat="server"></asp:TextBox>
                            </div>
                            <div id="Divy2" runat="server" visible="false" class="form-group">
                                <label>
                                    Voucher type</label>
                                <asp:DropDownList Enabled="false" ID="ddlvouchertype" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Credit" Selected="True" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div id="Dive3" runat="server" visible="false" class="form-group">
                                <label style="margin-top: 0px;">
                                    Cash A/C</label>
                                <asp:DropDownList ID="ddlCash" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                    AutoPostBack="true" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select Payment Mode" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                            </div>
                            <div id="Divc4" runat="server" visible="false" class="form-group">
                                <label>
                                </label>
                                <asp:CheckBox ID="chknew" runat="server" Text="New Customer" AutoPostBack="true"
                                    OnCheckedChanged="chknew_CheckedChanged" />
                            </div>
                            <div id="Divb8" runat="server" visible="false" class="form-group">
                                <label>
                                    City</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" TabIndex="4"></asp:TextBox>
                            </div>
                            <div id="Diva9" runat="server" visible="false" class="form-group">
                                <label>
                                    Address</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox6" TabIndex="2" runat="server"></asp:TextBox>
                            </div>
                            <div id="Div110" runat="server" visible="false" class="form-group">
                                <label>
                                    Pincode</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" TabIndex="5"></asp:TextBox>
                                <%--<asp:LinkButton ID="LinkButton1" Text="Add-Contacts" runat="server" AccessKey="N" OnClientClick="return AddVendor()"></asp:LinkButton>--%>
                            </div>
                            <div id="Divd11" runat="server" visible="false" class="form-group">
                                <label>
                                    Mobile No.</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" AutoPostBack="true"
                                    TabIndex="3" OnTextChanged="txtmobileno_TextChanged2"></asp:TextBox>
                                <label id="Label4" runat="server" style="color: Red">
                                </label>
                            </div>
                            <%--    <div class="col-lg-4" >--%>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" Width="50px" ID="TextBox9" runat="server" Enabled="false"
                                    Visible="false" OnTextChanged="TextBox9_TextChanged"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label>
                                </label>
                                <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" Visible="false"></asp:TextBox>
                                <%--<asp:TextBox CssClass="form-control" ID="txtsalesID" runat="server" Visible="false"></asp:TextBox>--%>
                            </div>
                            <div id="Divff12" runat="server" visible="false" class="form-group">
                                <label>
                                    Cheque No</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div id="Divfg5" runat="server" visible="false" class="form-group">
                                <label>
                                    Disc %</label>
                                <asp:TextBox CssClass="form-control" ID="txtDisc" runat="server"></asp:TextBox>
                            </div>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtlrdate"
                                PopupButtonID="txtlrdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                CssClass="cal_Theme1">
                            </ajaxToolkit:CalendarExtender>
                            <div id="Divjh6" runat="server" visible="false" class="form-group">
                                <label>
                                    Due days</label>
                                <asp:TextBox CssClass="form-control" ID="txtdueday" runat="server"></asp:TextBox>
                            </div>
                            <div id="Divgh7" runat="server" visible="false" class="form-group">
                                <label>
                                </label>
                                <asp:TextBox ID="TextBox12" runat="server" Visible="false"></asp:TextBox>
                            </div>
                            <div id="Divjh13" runat="server" visible="false" class="form-group">
                                <label>
                                    City</label>
                                <asp:TextBox CssClass="form-control" ID="txtcity" runat="server" TabIndex="4"></asp:TextBox>
                            </div>
                            <div id="Div14" runat="server" visible="false" class="form-group">
                                <label>
                                    Address</label>
                                <asp:TextBox CssClass="form-control" ID="txtaddress" TabIndex="2" runat="server"></asp:TextBox>
                            </div>
                            <div id="Div15" runat="server" visible="false" class="form-group">
                                <label>
                                    Pincode</label>
                                <asp:TextBox CssClass="form-control" ID="txtpincode" runat="server" TabIndex="5"></asp:TextBox>
                                <%--<asp:LinkButton ID="LinkButton1" Text="Add-Contacts" runat="server" AccessKey="N" OnClientClick="return AddVendor()"></asp:LinkButton>--%>
                            </div>
                            <%--    <div class="col-lg-4" >--%>
                            <div id="Div8" runat="server" visible="false" class="form-group">
                                <asp:TextBox CssClass="form-control" Width="50px" ID="txtcustomername" runat="server"
                                    Enabled="false" Visible="false"></asp:TextBox>
                            </div>
                            <div id="Div9" runat="server" visible="false" class="form-group">
                                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div id="Div10" runat="server" visible="false" class="form-group">
                                <label>
                                </label>
                                <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server" Visible="false"></asp:TextBox>
                                <%--<asp:TextBox CssClass="form-control" ID="txtsalesID" runat="server" Visible="false"></asp:TextBox>--%>
                            </div>
                            <div id="Div17" runat="server" visible="false" class="form-group">
                                <label>
                                    Cheque No</label>
                                <asp:TextBox CssClass="form-control" ID="txtCheque" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group" style="display: none">
                                <label>
                                    Engine No.</label>
                                <asp:TextBox CssClass="form-control" ID="txtTransport" runat="server" value="0"></asp:TextBox>
                            </div>
                            <div class="form-group" style="display: none">
                                <label>
                                    Details</label>
                                <asp:TextBox CssClass="form-control" ID="txtdestination" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Bill date</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="val1" Text="*"
                                    ControlToValidate="txtvoudate" ErrorMessage="Please Enter SR Date" runat="server"></asp:RequiredFieldValidator>
                                <asp:TextBox CssClass="form-control" ID="txtvoudate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                    onkeydown="return DateFormat(this, event.keyCode)" Text="-----Select Date-----"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Delivery Date</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="val1" Text="*"
                                    ControlToValidate="txtduedate" ErrorMessage="Please Enter LR Date" runat="server"></asp:RequiredFieldValidator>
                                <asp:TextBox CssClass="form-control" ID="txtduedate" onkeyup="ValidateDate(this, event.keyCode)"
                                    onkeydown="return DateFormat(this, event.keyCode)" runat="server" Text="-----Select Date-----"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" TargetControlID="txtduedate"
                                    PopupButtonID="txtduedate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                    CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <label>
                                    Through
                                </label>
                                <asp:TextBox CssClass="form-control" ID="txtthrough" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div id="Div11" runat="server" visible="false" class="form-group">
                                <%--  <asp:Button ID="Button1" runat="server" Text="Add New Customer" CssClass="btn-success" OnClientClick="opencustomer()" />--%>
                                <label>
                                    Sales Type</label>
                                <%-- <asp:DropDownList ID="ddlSalestype" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSalestype_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Select Sales Type" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Normal Sales" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Textile Sales" Value="2"></asp:ListItem>
                                      </asp:DropDownList>--%>
                            </div>
                            <div class="form-group">
                                <label>
                                    Order no.</label>
                                <asp:TextBox CssClass="form-control" ID="txtorderno" runat="server" value="0"></asp:TextBox>
                            </div>
                            <div id="Div12" runat="server" class="form-group">
                                <label>
                                    Order date</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="val1" Text="*"
                                    ControlToValidate="txtorderdate" ErrorMessage="Please Enter Order Date" runat="server"></asp:RequiredFieldValidator>
                                <asp:TextBox CssClass="form-control" ID="txtorderdate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                    onkeydown="return DateFormat(this, event.keyCode)" Text="-----Select Date-----"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtorderdate"
                                    PopupButtonID="txtorderdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                    CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <label>
                                    Transport Name</label>
                                <asp:TextBox CssClass="form-control" ID="txttransportupd" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    Lr No.</label>
                                <asp:TextBox CssClass="form-control" ID="txtlrnoupd" runat="server"></asp:TextBox>
                            </div>
                            <div id="Div20" runat="server" class="form-group">
                                <label>
                                    Lr date</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="val1" Text="*"
                                    ControlToValidate="txtlrdate" ErrorMessage="Please Enter LR Date" runat="server"></asp:RequiredFieldValidator>
                                <asp:TextBox CssClass="form-control" ID="txtlrdate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                    onkeydown="return DateFormat(this, event.keyCode)" Text="-----Select Date-----"></asp:TextBox>
                            </div>
                            <div id="Div13" runat="server" visible="false" class="form-group">
                                <asp:Button ID="Button1" runat="server" Text="Add New Customer" CssClass="btn-success"
                                    OnClientClick="opencustomer()" />
                            </div>
                            <div class="form-group" style="display: none">
                                <label>
                                    Lorry No.</label>
                                <asp:TextBox CssClass="form-control" ID="ttxlorryno" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    <%-- Packing slip .no--%></label>
                                <asp:TextBox Visible="false" CssClass="form-control" ID="txtpackingslip" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    No. of packages--</label>
                                <asp:TextBox CssClass="form-control" ID="txtnopackage" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group" style="display: none">
                                <label id="Label6" runat="server" style="font-weight: bold; color: Red">
                                    Rto/Unit Office:</label>
                                <asp:Label ID="lblarea" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div id="Div18" class="form-group" visible="false" runat="server">
                                <label>
                                    Price List</label>
                                <asp:CompareValidator ID="CompareValidator8" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPriceList"
                                    ValueToCompare="Select PriceList" Operator="NotEqual" Type="String" ErrorMessage="Please Select Price List"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlPriceList" Style="font-weight: bold" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <!-- /.col-lg-12 -->
                    </div>
                    <!-- /.row -->
                    </br>
                    <div class="row">
                        <div class="col-lg-10" style="margin-top: -35px">
                            <div class="panel-body">
                                <div>
                                    <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                    <table class="table table-striped table-bordered table-hover" id="Table1" width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="300" Width="100%">
                                                    <div style="overflow: auto; height: 500px">
                                                        <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                            OnRowDataBound="GridView2_RowDataBound" OnRowCommand="Gridview1_SelectedIndexChanged"
                                                            OnRowDeleting="GridView2_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                            Width="100%" runat="server">
                                                            <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                            <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                                    HeaderStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <%--    <asp:Label ID="txtno"   runat="server" ></asp:Label>--%>
                                                                        <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="Category" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="drpCategory" CssClass="chzn-select" runat="server" Height="26px"
                                                                                    AutoPostBack="true" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged"
                                                                                    AppendDataBoundItems="true">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                <%-- <asp:TemplateField HeaderText="Product Code" Visible="false" HeaderStyle-Width="10%"
                                                                            ItemStyle-Width="13%">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="ProductCode" CssClass="chzn-select" runat="server" Width="100%"
                                                                                    Height="26px" AutoPostBack="true" OnSelectedIndexChanged="ProductCode_SelectedIndexChanged"
                                                                                    AppendDataBoundItems="true">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText=" Product " HeaderStyle-Width="19%" ItemStyle-Width="17%">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="drpItem" CssClass="chzn-select" runat="server" Height="26px"
                                                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpItem_SelectedIndexChanged"
                                                                            AppendDataBoundItems="true">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description" ControlStyle-Width="100%" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtDesc" Height="30px" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField HeaderText=" Segment " HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="drpsegment" CssClass="chzn-select" runat="server" Height="26px"
                                                                                    Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpsegment_SelectedIndexChanged"
                                                                                    AppendDataBoundItems="true">
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                <%-- <asp:TemplateField HeaderText="Item Name" ControlStyle-Width="100%" ItemStyle-Width="10%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList ID="drpsaleitem" CssClass="chzn-select" runat="server" Width="100%">
                                                                                </asp:DropDownList>
                                                                                <asp:TextBox ID="txtitemprintname" Visible="false" Height="30px" Enabled="true" runat="server"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                <%-- <asp:TemplateField HeaderText="Stock" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtStock" Height="30px" Enabled="false" runat="server"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                <%-- <asp:TemplateField HeaderText="20" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt20" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt34_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="22" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt22" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt34_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="24" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt24" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt34_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="26" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt26" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt34_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="28" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt28" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt34_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="30" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt30" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt34_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="32" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt32" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt34_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="34" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt34" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt34_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="36" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt36" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt36_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="38" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt38" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt38_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="40" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt40" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt40_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="42" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt42" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt42_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="44" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt44" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="46" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt46" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="xs" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtxs" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="s" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txts" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="M" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtm" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="l" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtl" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="xl" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtxl" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="xxl" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtxxl" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="3xl" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt3xl" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="4xl" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt4xl" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="50" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt50" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Meter" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtmeter" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44_TextChanged"
                                                                                    Text="0"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Qty" ControlStyle-Width="100%" ItemStyle-Width="8%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtQty" runat="server" Height="30px" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                                                            TargetControlID="txtQty" ValidChars="." FilterType="Numbers, Custom" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRate" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txtRate_TextChanged">.00</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Discount %" Visible="false" ControlStyle-Width="100%"
                                                                    ItemStyle-Width="8%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtDiscount" Height="30px" runat="server" AutoPostBack="true" Text="0"
                                                                            OnTextChanged="txtDiscount_TextChanged">0</asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GST %" ControlStyle-Width="100%" ItemStyle-Width="8%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTax" AutoPostBack="true" OnTextChanged="txttax_textchanged" Height="30px"
                                                                            runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount" ControlStyle-Width="100%" ItemStyle-Width="8%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAmount" Height="30px" runat="server" Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty Hist." ControlStyle-Width="100%" ItemStyle-Width="4%">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnEdit" runat="server" SkinID="edit" Height="30px" Width="73px"
                                                                            Text="View" CommandName="Select" />
                                                                        <%--   <asp:Button ID="viewitem" OnClick="Button123_Click"  runat="server" EnableTheming="false"
                                                                            Text="ItemHistory"  />--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Itm.Hist." ControlStyle-Width="100%" ItemStyle-Width="4%">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="viewitem" runat="server" EnableTheming="false" Text="Itm.His." CommandName="Itemhis"
                                                                            Height="30px" Width="73px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cust. Hist." ControlStyle-Width="80%" ItemStyle-Width="4%">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnEdit1" runat="server" SkinID="edit" Height="30px" Width="73px"
                                                                            Text="View" CommandName="Select1" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>
                                                <div class="col-lg-12">
                                                    <div class="col-lg-2">
                                                        <label>
                                                            Billed By
                                                            <asp:TextBox CssClass="form-control" ID="txtbilledby" runat="server" Style="width: 150px;
                                                                text-align: right"></asp:TextBox></label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            Packing</label>
                                                        <asp:TextBox ID="txtpack" runat="server" CssClass="form-control"></asp:TextBox></div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            <asp:CheckBox ID="chkcertificate" Visible="false" runat="server" /></label>
                                                        <label>
                                                            Checking</label>
                                                        <asp:TextBox ID="txtchecking" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            Re-Check</label>
                                                        <asp:TextBox ID="txtrecheck" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            Freight Charges
                                                            <asp:TextBox CssClass="form-control" ID="txtFreight" OnTextChanged="txtLchange" AutoPostBack="true"
                                                                runat="server" Style="width: 150px; text-align: right">0</asp:TextBox></label></div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            Loading/Unloading
                                                            <asp:TextBox CssClass="form-control" ID="txtLU" OnTextChanged="txtLchange" AutoPostBack="true"
                                                                runat="server" Style="width: 150px; text-align: right">0</asp:TextBox></label></div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            Discount %
                                                            <asp:TextBox CssClass="form-control" ID="txtdiscount" OnTextChanged="granddiscount"
                                                                AutoPostBack="true" runat="server" Style="width: 150px; text-align: right">0</asp:TextBox></label></div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            Discount Amount
                                                            <asp:TextBox CssClass="form-control" ID="txtdiscountamount" OnTextChanged="granddiscount"
                                                                AutoPostBack="true" runat="server" Style="width: 150px; text-align: right">0</asp:TextBox></label></div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2" style="display: none">
                                                        <label>
                                                            Tax
                                                            <asp:TextBox CssClass="form-control" ID="txtTaxamt" runat="server" Style="width: 150px;
                                                                text-align: right">0</asp:TextBox>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            CGST
                                                            <asp:TextBox CssClass="form-control" ID="txtcgst" runat="server" Style="width: 150px;
                                                                text-align: right">0</asp:TextBox>
                                                        </label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            SGST
                                                            <asp:TextBox CssClass="form-control" ID="txtsgst" runat="server" Style="width: 150px;
                                                                text-align: right">0</asp:TextBox>
                                                        </label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            IGST
                                                            <asp:TextBox CssClass="form-control" ID="txtigst" runat="server" Style="width: 150px;
                                                                text-align: right">0</asp:TextBox>
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            Grand Total
                                                            <asp:TextBox CssClass="form-control" ID="txtgrandtotal" runat="server" Style="width: 150px;
                                                                text-align: right">0</asp:TextBox></label></div>
                                                    <div class="col-lg-2">
                                                        <label>
                                                            Round Off
                                                            <asp:TextBox CssClass="form-control" ID="txtroundoff" runat="server" Style="width: 150px;
                                                                text-align: right">0</asp:TextBox></label>
                                                    </div>
                                                </div>
                                                <asp:LinkButton Text="" ID="lnkFake" runat="server"></asp:LinkButton>
                                                <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                                                    TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <asp:Panel ID="pnlPopup" runat="server" ScrollBars="Auto" Height="600px" Width="900px"
                                                    CssClass="modalPopup" Style="display: none">
                                                    <div class="header">
                                                        History
                                                    </div>
                                                    <div class="body">
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <div id="Div19" runat="server" style="overflow: auto; height: 450px">
                                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Updates Avaliable For this Item"
                                                                            AllowPaging="false" Width="100%" CssClass="myGridStyle1">
                                                                            <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
                                                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Bill No" DataField="BillNo" />
                                                                                <asp:BoundField HeaderText="Bill Date" DataField="BillDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                <asp:BoundField HeaderText="Customer Name" DataField="LedgerName" />
                                                                                <asp:BoundField HeaderText="Item" DataField="Definition" />
                                                                                <asp:BoundField HeaderText="Total Amount" DataField="Amount" DataFormatString="{0:f}" />
                                                                                <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                                                                <asp:BoundField HeaderText="Unit Price" DataField="unitprice" DataFormatString="{0:f}" />
                                                                            </Columns>
                                                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="footer" align="right">
                                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Tdd1" runat="server" visible="false" align="right">
                                                <asp:Button ID="ButtonAdd1" runat="server" EnableTheming="false" Text="Add New" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%-- <td align="right" style="width:58%">
                                                        <label style="margin-top: 11px;">
                                                            Dis</label>
                                                    </td>
                                                    <td align="left" style="width:23%">--%>
                                                <%--<asp:TextBox CssClass="form-control" ID="txtdiscount" OnTextChanged="granddiscount" AutoPostBack="true" runat="server" Style="width: 150px;
                                                  <%--          text-align: right">0</asp:TextBox>--%>
                                                <%--</td>
                                                </tr>--%>
                                                <%--<tr class="odd gradeX">
                                                        <td align="right">
                                                            <label style="margin-top: 11px;">
                                                                TAX 5 %</label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control" ID="txtTaxamt5" runat="server" Style="width: 150px;
                                                                text-align: right">0</asp:TextBox>
                                                        </td>
                                                    </tr>--%>
                                                <%--  <tr>
                                                    <td align="right" style="width:58%">
                                                        <label style="margin-top: 11px;">
                                                            TAX</label>
                                                    </td>
                                                    <td align="left" style="width:23%">--%>
                                                <%--<asp:TextBox CssClass="form-control" ID="txtTaxamt" runat="server" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>--%>
                                                <%--         </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width:58%">
                                                            <label style="margin-top: 11px;">
                                                                Grand Total</label>
                                                        </td>
                                                        <td align="left" style="width:23%">--%>
                                                <%-- <asp:TextBox CssClass="form-control" ID="txtgrandtotal" runat="server" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>--%>
                                                <%-- </td>
                                                </tr>--%>
                                                <%-- <tr class="odd gradeX">
                                                        <td align="right" >
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btncalc" runat="server" Text="Calc" CssClass="btn btn-success" Style="width: 100px;
                                                                margin-left: 30px; margin-top: -4px;" OnClick="btncalc_Click" />
                                                            <asp:RequiredFieldValidator ID="txtgt" ValidationGroup="val1" ControlToValidate="txtgrandtotal"
                                                                ErrorMessage="Please calculate your Grand Total" runat="server"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>--%>
                                                <table id="Table3" style="margin-top: -36px" width="45%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <%--Total Qty.--%></label>
                                                            <asp:TextBox Visible="false" ID="totqty" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <%--Total Meter.--%>
                                                            </label>
                                                            <asp:TextBox Visible="false" ID="totmeter" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <%--Item His.--%></label>
                                                            <asp:TextBox Visible="false" ID="txtitemhis" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <label>
                                                                <%--Cust.His--%>
                                                            </label>
                                                            <asp:TextBox Visible="false" ID="txtcusthis" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </td>
                                                        <%-- <td>
                                                        <asp:TextBox ID="txtdamt5" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                    </td>--%>
                                                        <td>
                                                            <asp:TextBox ID="txtTamt5" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                    </table>
                                    <%--</tr>
                                            </tbody>--%>
                                    </td> </tr> </tbody>
                                    <table id="Table2" runat="server" visible="false">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="TextBox13" Visible="false" runat="server"
                                                    Style="width: 110px; margin-left: 46px; margin-top: 11px; text-align: right">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <%--<asp:TextBox CssClass="form-control" ID="txtDiscamt" Visible="false"  Enabled="false" runat="server" style="width: 110px;margin-left: 43px; margin-top:17px; text-align:right" >0</asp:TextBox>--%>
                                                <%-- <asp:Label ID="lblDisc" runat="server" ></asp:Label>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Payment Type
                                            </td>
                                            <td>
                                                Cheque/Card/DD No
                                            </td>
                                            <td>
                                                Amount
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlAgainst" runat="server" CssClass="form-control" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtchequedd" runat="server" Style="width: 290px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtAgainstAmount" runat="server" Style="width: 200px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlAgainst1" runat="server" CssClass="form-control" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtchequedd1" runat="server" Style="width: 290px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtAgainstAmount1" runat="server" Style="width: 200px;">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="TextBox18" runat="server" Enabled="false"
                                                    Style="width: 250px;">Cash</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtchequedd2" runat="server" Style="width: 290px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtAgainstAmount2" runat="server" Style="width: 200px;">0</asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <asp:Button ID="btnadd" AccessKey="s" runat="server" class="btn btn-info" BorderWidth="3px"
                                    BorderColor="#e41300" BorderStyle="Inset" OnClick="Add_Click" onmouseover="this.style.backgroundColor='#5bc0de'"
                                    onmousedown="this.style.backgroundColor='olive'" onfocus="this.style.backgroundColor='#1b293e'"
                                    Text="Save" ValidationGroup="val1" Width="120px" />
                                <asp:Button ID="btnExit" AccessKey="d" runat="server" class="btn btn-warning" BorderColor="Black"
                                    OnClick="btnExit_Click" Text="Exit" Width="120px" />
                                <asp:Button ID="btnPrint" runat="server" class="btn btn-danger" BorderColor="Black"
                                    OnClick="btnPrint_Click" Text="Print" Width="120px" />
                                <asp:Button ID="btnDelete" runat="server" class="btn btn-success" BorderColor="Black"
                                    OnClick="btnDelete_Click" Text="Delete" Width="120px" />
                            </div>
                        </div>
                    </div>
                    <!-- /.panel -->
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /#page-wrapper -->
    </div>
    </div> </div> </div>
    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvcustomerorder" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                    position: fixed; top: 50%; left: 40%;" />
                <%--<asp:Image ID="imgUpdateProgress1" runat="server" ImageUrl="../images/loading.gif" />--%>
    </div> </ProgressTemplate> </asp:UpdateProgress>--%>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    </form>
</body>
</html>
<script type="text/javascript">
    function opencustomer() {
        var url = "";
        url = location.protocol + '//' + location.host + "/Accountsbootstrap/customermaster.aspx?name=Add New";
        var target = window.open(url, '_blank');
        target.focus();
    }
</script>
