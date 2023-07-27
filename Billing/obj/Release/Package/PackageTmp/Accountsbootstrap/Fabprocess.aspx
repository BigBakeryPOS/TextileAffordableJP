<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fabprocess.aspx.cs" Inherits="Billing.Accountsbootstrap.Fabprocess" %>

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
    </style>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <script src="../jqueryCalendar/script.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <script type="text/javascript">
        jQuery(function () {
            jQuery("#inf_custom_someDateField").datepicker();
        });

       

    </script>
    <script type="text/javascript">
                @{ 
var db = Database.Open("SmallBakery"); 
var dbdata = db.Query("select c.Definition,a.UnitPrice from tblStock a,tblcategory  b,tblCategoryUser c where a.CategoryID=b.categoryid and a.SubCategoryID=c.CategoryUserID"); 
var myChart = new Chart(width: 600, height: 400) 
  .AddTitle("Product Sales") 
  .DataBindTable(dataSource: dbdata, xField: "c.Definition") 
  .Write();
}
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
    <title>Fabric Receive</title>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2 style="text-align: left; color: #fe0002; margin-top: 0px">
                <asp:Label ID="lblTitle" Text="Fabric Receive" runat="server"></asp:Label></h2>
            <h4>
                <div runat="server" visible="false" style="text-align: right; margin-top: -30px;
                    margin-right: 15px;">
                    <%--<asp:Button  ID="btnref" style="margin-top:-47px;margin-left:120px" runat="server" Text="View RefNo" OnClick="Refbutton_click" />--%>
                    <input type="button" value="View RefNo" onclick="window.open('RefHistory.aspx','popUpWindow','height=500,width=900,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">
                    &nbsp;&nbsp
                    <asp:Button ID="gridbutton" Style="margin-top: -47px; margin-left: 132px" runat="server"
                        Text="View grid" OnClick="gridbutton_click" />
                    &nbsp;&nbsp
                    <asp:CheckBox Visible="false" ID="chknewcust" onclientclick="window.open('http://www.bigdbiz.com');"
                        runat="server" Text="New Customer" Style="color: #00a8e6" AutoPostBack="false"
                        OnCheckedChanged="checkbox1_changed" />
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="New Customer" OnClientClick="window.open('customermaster.aspx?name=Add%20New');"></asp:LinkButton>
                </div>
            </h4>
            <!-- /.col-lg-12 -->
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                <div class="col-lg-2">
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Order Type</label>
                                        <asp:DropDownList ID="ddltype" OnSelectedIndexChanged="ddltype_SelectedIndexChanged"
                                            Width="195px" Height="60px" AutoPostBack="true" runat="server" CssClass="chzn-select">
                                            <asp:ListItem Text="Direct " Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Order" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Order No</label>
                                        <asp:DropDownList ID="ddlordno" OnSelectedIndexChanged="ddlordno_SelectedIndexChanged"
                                            Width="195px" Height="60px" AutoPostBack="true" runat="server" CssClass="chzn-select">
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                                    </div>
                                    <div id="Div31" runat="server" class="form-group">
                                        <label>
                                            Purchase Type</label>
                                        <asp:DropDownList ID="drppurchasetype" Width="195px" Height="60px" runat="server"
                                            CssClass="chzn-select">
                                            <asp:ListItem Text="Online" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Customer Order" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div id="Div19" runat="server" class="form-group">
                                        <label>
                                            Fabric Register No</label>
                                        <asp:TextBox CssClass="form-control" ID="txtinvno" Enabled="false" OnTextChanged="txtbillcheck"
                                            AutoPostBack="false" runat="server"></asp:TextBox>
                                    </div>
                                    <div id="Div22" runat="server" class="form-group">
                                        <label>
                                            Supplier Order No</label>
                                        <asp:TextBox CssClass="form-control" ID="txtsupplieroderno" runat="server">0</asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <div class="form-group">
                                        <label>
                                            Invoice date</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="val1" Text="*"
                                            ControlToValidate="txtinvdate" ErrorMessage="Please Enter Register Date" runat="server"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="form-control" ID="txtinvdate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                            onkeydown="return DateFormat(this, event.keyCode)" Text="-----Select Date-----"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" TargetControlID="txtinvdate"
                                            PopupButtonID="txtinvdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                            CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div id="Div20" runat="server" class="form-group">
                                        <label>
                                            Supplier Invoice No</label>
                                        <asp:TextBox CssClass="form-control" ID="txtinrefno" OnTextChanged="txtbillcheck"
                                            AutoPostBack="false" runat="server"></asp:TextBox>
                                    </div>
                                    <div id="Div1" runat="server" visible="false" class="form-group">
                                        <label>
                                            Vou.No</label>
                                        <asp:TextBox CssClass="form-control" ID="txtvouno" runat="server"></asp:TextBox>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Customer Name</label>
                                        <asp:DropDownList runat="server" Width="195px" Height="60px" CssClass="chzn-select"
                                            ID="ddlcustomerID" EnableViewState="true" class="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlcustomerID_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbladdress" runat="server" BorderColor="Black" Width="195px" Font-Size="14px"
                                            Style="overflow: auto; height: 62px"></asp:Label>
                                        <asp:TextBox ID="txtCustname" Visible="false" CssClass="form-control" TabIndex="1"
                                            runat="server"></asp:TextBox>
                                        <asp:Button ID="newcstomer" Visible="false" runat="server" Text="New" ToolTip="Create new customer"
                                            OnClick="newcstomer_click" />
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Bill Date</label>
                                        <asp:TextBox CssClass="form-control" ID="txtdate1" runat="server" Text="-----Select Date-----"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate1" PopupButtonID="txtdate1"
                                        EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                    <div runat="server" visible="false" class="form-group">
                                        <label id="lblbillTo" runat="server">
                                            Bill To</label>
                                        <asp:DropDownList ID="bblbillto" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="bblbillto_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                        </label>
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
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
                                    <div runat="server" visible="false" class="form-group">
                                        <label id="Label2" runat="server">
                                            Bank Name</label>
                                        <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Supplier Name</label>
                                        <asp:DropDownList ID="drpsupplier" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            Width="195px" Height="60px" AutoPostBack="false" runat="server" CssClass="chzn-select">
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Checked Sign</label>
                                        <asp:DropDownList ID="drpchecked" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            Width="195px" Height="60px" AutoPostBack="false" runat="server" CssClass="chzn-select">
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                                    </div>
                                    <div id="Div4" runat="server" class="form-group">
                                        <label>
                                            Select Company</label>
                                        <asp:DropDownList ID="drpcompany" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div id="Div3" runat="server" visible="false" class="form-group">
                                        <label>
                                            Transport No.</label>
                                        <asp:TextBox CssClass="form-control" ID="txtTransport" runat="server"></asp:TextBox>
                                    </div>
                                    <div id="Div21" runat="server" visible="false" class="form-group">
                                        <label>
                                            Bale Qty</label>
                                        <asp:TextBox CssClass="form-control" ID="txtbaleQty" runat="server"></asp:TextBox>
                                    </div>
                                    <div id="Div30" runat="server" visible="true" class="form-group">
                                        <label>
                                            Province</label>
                                        <asp:DropDownList ID="ddlProvince" OnSelectedIndexChanged="select" AutoPostBack="true"
                                            runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Tamilnadu" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Other State" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div id="Div23" class="form-group" runat="server" visible="true">
                                        <label>
                                            Purchase Invoice Copy</label>
                                        <asp:FileUpload ID="idupload1" runat="server" />
                                        <asp:Button ID="btnUpload2" runat="server" Text="Upload" OnClick="btnUpload2_Click" />
                                        <asp:Label ID="imgpreview1" runat="server"></asp:Label>
                                    </div>
                                    <div id="Div32" runat="server" visible="true" class="form-group">
                                        <label>
                                            Narration</label>
                                        <asp:TextBox CssClass="form-control" ID="txtNarration" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        <label id="Label3" runat="server" style="color: Red">
                                        </label>
                                    </div>
                                    <div id="Div33" runat="server" visible="true" class="form-group">
                                        <label>
                                            Neting Charges With GST 5 %</label>
                                        <asp:TextBox CssClass="form-control" ID="txtnetingcharge" OnTextChanged="Net_charge"
                                            AutoPostBack="true" runat="server">0</asp:TextBox>
                                        <asp:Label ID="lblnetingcharge" runat="server" Visible="false" Text="5"></asp:Label>
                                        </label>
                                    </div>
                                    <div class="form-group" visible="false" runat="server">
                                        <label>
                                            Bale Open date</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="val1" Text="*"
                                            ControlToValidate="txtregdate" ErrorMessage="Please Enter Register Date" runat="server"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="form-control" ID="txtregdate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                            onkeydown="return DateFormat(this, event.keyCode)" Text="-----Select Date-----"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtregdate"
                                            PopupButtonID="txtregdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                            CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div id="Div2" runat="server" visible="false" class="form-group">
                                        <label>
                                            LR No.</label>
                                        <asp:TextBox CssClass="form-control" ID="txtlrno" runat="server"></asp:TextBox>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Delivery Challan No</label>
                                        <asp:TextBox ID="txtDelChalan" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Voucher type</label>
                                        <asp:DropDownList Enabled="false" ID="ddlvouchertype" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Credit" Selected="True" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label style="margin-top: -11px;">
                                            Cash A/C</label>
                                        <asp:DropDownList ID="ddlCash" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            AutoPostBack="true" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Select Payment Mode" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                        </label>
                                        <asp:CheckBox ID="chknew" runat="server" Text="New Customer" AutoPostBack="true"
                                            OnCheckedChanged="chknew_CheckedChanged" />
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            City</label>
                                        <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" TabIndex="4"></asp:TextBox>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Address</label>
                                        <asp:TextBox CssClass="form-control" ID="TextBox6" TabIndex="2" runat="server"></asp:TextBox>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Pincode</label>
                                        <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" TabIndex="5"></asp:TextBox>
                                        <%--<asp:LinkButton ID="LinkButton1" Text="Add-Contacts" runat="server" AccessKey="N" OnClientClick="return AddVendor()"></asp:LinkButton>--%>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
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
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Cheque No</label>
                                        <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group" runat="server" visible="false">
                                        <label>
                                            Brand Name</label>
                                        <asp:DropDownList ID="ddlBrand" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Width</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="drpwidth" OnSelectedIndexChanged="ddlrep_SelectedIndexChanged"
                                            AutoPostBack="false" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div id="Div5" runat="server" visible="false" class="form-group">
                                        <label>
                                            <%-- Lr date--%></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="val1" Text="*"
                                            ControlToValidate="txtlrdate" ErrorMessage="Please Enter LR Date" runat="server"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="form-control" Visible="false" ID="txtlrdate" runat="server"
                                            onkeyup="ValidateDate(this, event.keyCode)" onkeydown="return DateFormat(this, event.keyCode)"
                                            Text="-----Select Date-----"></asp:TextBox>
                                    </div>
                                    <div id="Div6" runat="server" visible="false" class="form-group">
                                        <label>
                                            Disc %</label>
                                        <asp:TextBox CssClass="form-control" ID="txtDisc" runat="server"></asp:TextBox>
                                    </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtlrdate"
                                        PopupButtonID="txtlrdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                    <div id="Div7" runat="server" visible="false" class="form-group">
                                        <label>
                                            Due days</label>
                                        <asp:TextBox CssClass="form-control" ID="txtdueday" runat="server"></asp:TextBox>
                                    </div>
                                    <div id="Div8" runat="server" visible="false" class="form-group">
                                        <label>
                                        </label>
                                        <asp:TextBox ID="TextBox12" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div id="Div13" runat="server" visible="false" class="form-group">
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
                                    <div id="Div16" runat="server" visible="false" class="form-group">
                                        <label>
                                            Mobile No.</label>
                                        <asp:TextBox CssClass="form-control" ID="txtmobileno" runat="server" AutoPostBack="true"
                                            TabIndex="3" OnTextChanged="txtmobileno_TextChanged2"></asp:TextBox>
                                        <label id="lblmoberror" runat="server" style="color: Red">
                                        </label>
                                    </div>
                                    <%--    <div class="col-lg-4" >--%>
                                    <div id="Div9" runat="server" visible="false" class="form-group">
                                        <asp:TextBox CssClass="form-control" Width="50px" ID="txtcustomername" runat="server"
                                            Enabled="false" Visible="false"></asp:TextBox>
                                    </div>
                                    <div id="Div10" runat="server" visible="false" class="form-group">
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div id="Div11" runat="server" visible="false" class="form-group">
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
                                    <div class="form-group">
                                        <label>
                                            Total.Bill. KG</label>
                                        <asp:TextBox ID="txtbillmet" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Discount Amount Per.KG</label>
                                        <asp:TextBox ID="txtdisamount" Enabled="true" CssClass="form-control" OnTextChanged="disc_Chnaged"
                                            AutoPostBack="true" runat="server">0</asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Total Discount Amount</label>
                                        <asp:TextBox ID="txttotdisamount" Enabled="false" CssClass="form-control" runat="server">0</asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Total.REC. KG</label>
                                        <asp:TextBox ID="txttotmet" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Total Sub Amount</label>
                                        <asp:TextBox ID="txtsubtotal" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Total Amount</label>
                                        <asp:TextBox ID="txttoal" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    Total Roll:<asp:Label ID="lblroll" runat="server" ></asp:Label>
                                    <div class="form-group">
                                        <asp:Button ID="btnpreview" runat="server" Text="Generate Barcode Preview" OnClick="previewclick"
                                            Visible="false" />
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Details</label>
                                        <asp:TextBox CssClass="form-control" ID="txtdestination" runat="server"></asp:TextBox>
                                    </div>
                                    <div id="Div12" runat="server" visible="false" class="form-group">
                                        <label>
                                            <%--Order date--%></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="val1" Text="*"
                                            ControlToValidate="txtorderdate" ErrorMessage="Please Enter Order Date" runat="server"></asp:RequiredFieldValidator>
                                        <asp:TextBox Visible="false" CssClass="form-control" ID="txtorderdate" runat="server"
                                            onkeyup="ValidateDate(this, event.keyCode)" onkeydown="return DateFormat(this, event.keyCode)"
                                            Text="-----Select Date-----"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtorderdate"
                                            PopupButtonID="txtorderdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                            CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Chase no.</label>
                                        <asp:TextBox CssClass="form-control" ID="txtorderno" runat="server"></asp:TextBox>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
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
                                            <%-- Packing slip .no--%></label>
                                        <asp:TextBox Visible="false" CssClass="form-control" ID="txtpackingslip" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            <%--No. of packages--%></label>
                                        <asp:TextBox Visible="false" CssClass="form-control" ID="txtnopackage" runat="server"></asp:TextBox>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
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
                                <div class="col-lg-12" style="margin-top: -35px">
                                    <div class="panel-body">
                                        <div>
                                            <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                            <table class="table table-striped table-bordered table-hover" id="Table1" width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="260" Width="100%">
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
                                                                           
                                                                            <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdtransid" runat="server" />
                                                                             <asp:HiddenField ID="hdChkCutId" runat="server" Value='<%#Eval("ChkCutId")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fabric Type" ControlStyle-Width="100%" HeaderStyle-Width="20%"
                                                                        ItemStyle-Width="20%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlfabtype" Width="95%" Height="26px" runat="server" AutoPostBack="true"
                                                                                OnSelectedIndexChanged="ddlfabtype_OnSelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Style" Visible="false" ControlStyle-Width="100%" HeaderStyle-Width="8%"
                                                                        ItemStyle-Width="8%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlfabstyle" Width="75%" Height="26px" runat="server">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mode" ControlStyle-Width="100%" HeaderStyle-Width="7%"
                                                                        ItemStyle-Width="7%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlfabmode" Width="95%" Height="26px" runat="server">
                                                                                <asp:ListItem Text="BODY" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="RIB/CONTRAST" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Other Charges" Value="3"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Shrinkage" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtShrinkage" Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pinning" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtPinning" Height="30px" Text="0" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fabric Type" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtitem" Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Design No" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdesno" Height="30px" MaxLength="3" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Color" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtcolor" Visible="false" Height="30px" runat="server"></asp:TextBox>
                                                                            <asp:DropDownList ID="drpcolor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcolortype_OnSelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Roll / Taka" ControlStyle-Width="100%" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtpiece" Height="30px" runat="server"></asp:TextBox>
                                                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderpiece031s2" runat="server"
                                                                                TargetControlID="txtpiece" ValidChars="." FilterType="Numbers, Custom" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Width" ControlStyle-Width="100%" HeaderStyle-Width="5%"
                                                                        ItemStyle-Width="8%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpwid" Width="75%" Height="26px" runat="server">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Order Meter" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtorderpiece" Enabled="false" Height="30px" runat="server" Text="0"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rem.Meter" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtremmeter" Enabled="false" Height="30px" runat="server">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bill.KG" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender031s2" runat="server"
                                                                                TargetControlID="txtbillmeter" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txtbillmeter" OnTextChanged="txtbillmeter_textchanged" AutoPostBack="true"
                                                                                Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rec.KG" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender0312" runat="server"
                                                                                TargetControlID="txtmeter" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txtmeter" OnTextChanged="txtmeter_textchanged" AutoPostBack="true"
                                                                                Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rate" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender032" runat="server"
                                                                                TargetControlID="txtRate" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txtRate" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txtrrattee_textchanged">.00</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tax" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender032l" runat="server"
                                                                                TargetControlID="txttax" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txttax" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txtrrattee_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tax Amount" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender032ll" runat="server"
                                                                                TargetControlID="txttaxamount" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txttaxamount" Height="30px" runat="server" Enabled="false">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount" ControlStyle-Width="100%" ItemStyle-Width="8%">
                                                                        <ItemTemplate>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender03ll2" runat="server"
                                                                                TargetControlID="txtamount" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txtamount" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txtrrattee_textchanged">.00</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="File Upload" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="8%">
                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updFU">
                                                                                <ContentTemplate>
                                                                                    <asp:FileUpload ID="idupload" runat="server" />
                                                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload1_Click" />
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ImageLabel" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="12%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="imgpreview" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Image" Visible="false" ControlStyle-Width="39%" ItemStyle-Width="12%">
                                                                        <ItemTemplate>
                                                                            <%-- <asp:TextBox ID="txtAmount" Height="30px" runat="server" Enabled="false"></asp:TextBox>--%>
                                                                            <asp:Image ID="imgurl" Height="36px" Width="20px" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="BarCode" ControlStyle-Width="65%" ItemStyle-Width="50%"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbarcode" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CancelMeter" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtcancelmeter" Height="30px" runat="server" OnTextChanged="txtcancelmeter_TextChanged"
                                                                                AutoPostBack="true"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cancel" ControlStyle-Width="100%" ItemStyle-Width="10%"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkid" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Issue" HeaderStyle-Font-Size="10px" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpextralist" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Text="No Issue" Value="No Issue"></asp:ListItem>
                                                                                <asp:ListItem Text="Extra" Value="Extra"></asp:ListItem>
                                                                                <asp:ListItem Text="Minus" Value="Minus"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Narration" Visible="false" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtnarration" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="New Color" HeaderStyle-Font-Size="12px" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnadd" Text="Add" CssClass="btn btn-info" runat="server" OnClick="ButtonAdd1_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Same Color" HeaderStyle-Font-Size="12px" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnaddColor" Text="Add" CssClass="btn btn-danger" runat="server"
                                                                                OnClick="ButtonAdd2_Click1" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <div id="Div24" runat="server" style="padding-left: 0px">
                                                            <div class="col-lg-2">
                                                                <label runat="server" style="width: 100%">
                                                                    Allow charges in Total Value</label>
                                                                <asp:CheckBox ID="chkchargesapply" OnCheckedChanged="chargesapply_checkedchnage"
                                                                    AutoPostBack="true" runat="server" />
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <label>
                                                                    Commission Charges
                                                                    <asp:TextBox ID="txtcom" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                        OnTextChanged="txtLchange" Style="width: 150px; text-align: right">0</asp:TextBox>
                                                                </label>
                                                            </div>
                                                            <div id="Div25" class="col-lg-2" runat="server">
                                                                <label>
                                                                    Freight Charges
                                                                    <asp:TextBox CssClass="form-control" ID="txtFreight" OnTextChanged="txtLchange" AutoPostBack="true"
                                                                        runat="server" Style="width: 150px; text-align: right">0</asp:TextBox></label></div>
                                                            <div id="Div26" visible="false" class="col-lg-2" runat="server">
                                                                <label>
                                                                    Loading/Unloading
                                                                    <asp:TextBox CssClass="form-control" ID="txtLU" OnTextChanged="txtLchange" AutoPostBack="true"
                                                                        runat="server" Style="width: 150px; text-align: right">0</asp:TextBox></label></div>
                                                            <div id="Div27" class="col-lg-2" runat="server">
                                                                <label>
                                                                    CGST
                                                                    <asp:TextBox CssClass="form-control" ID="txtcgst" runat="server" Style="width: 150px;
                                                                        text-align: right" Enabled="false">0</asp:TextBox>
                                                                </label>
                                                                <asp:TextBox CssClass="form-control" ID="txtdiscount" OnTextChanged="granddiscount"
                                                                    AutoPostBack="true" runat="server" Visible="false" Style="width: 150px; text-align: right">0</asp:TextBox></div>
                                                            <div id="Div28" class="col-lg-2" runat="server">
                                                                <label>
                                                                    SGST
                                                                    <asp:TextBox CssClass="form-control" ID="txtsgst" runat="server" Style="width: 150px;
                                                                        text-align: right" Enabled="false">0</asp:TextBox>
                                                                </label>
                                                                <asp:TextBox CssClass="form-control" ID="txtdiscountamount" OnTextChanged="granddiscount"
                                                                    AutoPostBack="true" runat="server" Visible="false" Style="width: 150px; text-align: right">0</asp:TextBox></div>
                                                            <div id="Div29" class="col-lg-2" runat="server">
                                                                <label>
                                                                    IGST
                                                                    <asp:TextBox CssClass="form-control" ID="txtigst" runat="server" Style="width: 150px;
                                                                        text-align: right" Enabled="false">0</asp:TextBox>
                                                                </label>
                                                                <asp:TextBox CssClass="form-control" Visible="false" ID="txtTaxamt" runat="server"
                                                                    Style="width: 150px; text-align: right">0</asp:TextBox>
                                                            </div>
                                                            <div class="col-lg-2" runat="server" visible="false" style="margin-left: 56pc;">
                                                                <label>
                                                                    Grand Total
                                                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" Style="width: 150px;
                                                                        text-align: right">0</asp:TextBox></label></div>
                                                            <asp:TextBox CssClass="form-control" Visible="false" ID="txtroundoff" runat="server"
                                                                Style="width: 150px; text-align: right">0</asp:TextBox>
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
                                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Updates Avaliable For this Item"
                                                                                AllowPaging="True" PageSize="50000" CssClass="myGridStyle">
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
                                                    <td id="Td1" runat="server" visible="false" align="right">
                                                        <asp:Button ID="ButtonAdd1" runat="server" EnableTheming="false" Text="Add New" />
                                                    </td>
                                                </tr>
                                                <tr>
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvcustomerorder" EventName="RowCommand" />
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload2" />
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
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    </form>
</body>
</html>
