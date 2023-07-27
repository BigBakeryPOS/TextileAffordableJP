<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecondStageProcess.aspx.cs" Inherits="Billing.Accountsbootstrap.SecondStageProcess" %>

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
    <title>Second Stage Process - bootsrap</title>
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
   <%-- <style>
table {
    border-collapse: collapse;
}
  .table1 {
    border-collapse: collapse;
}

        .table1, td1 {
    border: 1px solid black;
}

table, td, th {
    border: 1px solid black;
}
</style>--%>
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
                <asp:Label ID="lblTitle" Text="Second stage Process" runat="server"></asp:Label></h2>
            <h4>
               <%-- <div id="Div1" runat="server" visible="false" style="text-align: right; margin-top: -30px;
                    margin-right: 15px;">
                    
                    <input type="button" value="View RefNo" onclick="window.open('RefHistory.aspx','popUpWindow','height=500,width=900,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">
                    &nbsp;&nbsp
                    <asp:Button ID="gridbutton" Style="margin-top: -47px; margin-left: 132px" runat="server"
                        Text="View grid" OnClick="gridbutton_click" />
                    &nbsp;&nbsp
                    <asp:CheckBox Visible="false" ID="chknewcust" onclientclick="window.open('http://www.bigdbiz.com');"
                        runat="server" Text="New Customer" Style="color: #00a8e6" AutoPostBack="false"
                        OnCheckedChanged="checkbox1_changed" />
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="New Customer" OnClientClick="window.open('customermaster.aspx?name=Add%20New');"></asp:LinkButton>
                </div>--%>
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
                                   <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                  <div id="Div199" visible="false" runat="server" class="form-group">
                                        <label>
                                            Second Process Register No</label>
                                        <asp:TextBox CssClass="form-control" ID="txtinvno" Enabled="false"  runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Mobile No</label>
                                               <asp:TextBox CssClass="form-control" ID="txtmobile"  OnTextChanged="txtbillcheck"
                                            AutoPostBack="true" runat="server"></asp:TextBox>
                                     <%--   <asp:DropDownList ID="drpsupplier" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            Width="195px" Height="60px" AutoPostBack="false" runat="server" CssClass="chzn-select">
                                        </asp:DropDownList>--%>
                                        <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                                    </div>
                                     <div class="form-group">
                                        <label>
                                            Customer Name</label>
                                               <asp:TextBox CssClass="form-control" ID="txtcustomername"  runat="server"></asp:TextBox>
                                     <%--   <asp:DropDownList ID="drpsupplier" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            Width="195px" Height="60px" AutoPostBack="false" runat="server" CssClass="chzn-select">
                                        </asp:DropDownList>--%>
                                        <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                                    </div>
                                     <div class="form-group">
                                        <label>
                                            Address</label>
                                               <asp:TextBox CssClass="form-control" ID="txtadd" TextMode="MultiLine"   runat="server"></asp:TextBox>
                                     <%--   <asp:DropDownList ID="drpsupplier" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            Width="195px" Height="60px" AutoPostBack="false" runat="server" CssClass="chzn-select">
                                        </asp:DropDownList>--%>
                                        <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                                    </div>
                                     
                                   
                                </div>
                                <div class="col-lg-2">
                                <div class="form-group">
                                        <label>
                                            Tin/Cst No</label>
                                               <asp:TextBox CssClass="form-control" ID="txttincst"  runat="server"></asp:TextBox>
                                     <%--   <asp:DropDownList ID="drpsupplier" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            Width="195px" Height="60px" AutoPostBack="false" runat="server" CssClass="chzn-select">
                                        </asp:DropDownList>--%>
                                        <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                                    </div>
                                     <div class="form-group">
                                        <label>
                                            Transport</label>
                                               <asp:TextBox CssClass="form-control" ID="txttransport" runat="server"></asp:TextBox>
                                     <%--   <asp:DropDownList ID="drpsupplier" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged"
                                            Width="195px" Height="60px" AutoPostBack="false" runat="server" CssClass="chzn-select">
                                        </asp:DropDownList>--%>
                                        <%-- <asp:TextBox CssClass="form-control"  ID="txtPayMode" runat="server"   TabIndex="3" ></asp:TextBox>--%>
                                    </div>
                                     <div class="form-group">
                                        <label>
                                            Delivery  date</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="val1" Text="*"
                                            ControlToValidate="txtdelidate" ErrorMessage="Please Enter Register Date" runat="server"></asp:RequiredFieldValidator>
                                        <asp:TextBox CssClass="form-control" ID="txtdelidate" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                            onkeydown="return DateFormat(this, event.keyCode)" Text="-----Select Date-----"></asp:TextBox>
                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdelidate"
                                        PopupButtonID="txtdelidate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                    </div>

                                    <div runat="server"  class="form-group">
                                        <label>
                                            Type:</label>
                                            <asp:CheckBoxList ID="chktype" OnSelectedIndexChanged="chktype_Changed" RepeatColumns="2" AutoPostBack="true" runat="server">
                                            <asp:ListItem Text="Shirt/ Casual" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Trouser" Value="2"></asp:ListItem></asp:CheckBoxList>
                                      
                                    </div>
                                </div>
                                 <div class="col-lg-3">
                                 <label>Shirt's Total</label>
                                 <table>
                                 <tr>
                                 <td>
                                 
                                 <table border="1px" style="border-collapse:collapse">
                                
                                 <tr>
                                 <td >
                                 <asp:label Font-Bold="true" runat="server">36FS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt36fs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td  >
                                 <asp:label Font-Bold="true" ID="Label1" runat="server">38FS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt38fs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td  >
                                 <asp:label Font-Bold="true" ID="Label2" runat="server">40FS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt40fs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td  >
                                 <asp:label Font-Bold="true" ID="Label3" runat="server">42FS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt42fs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td  >
                                 <asp:label Font-Bold="true" ID="Label4" runat="server">44FS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt44fs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                 <tr>
                                 <td style="width: 50%;">
                                 <asp:label Font-Bold="true" ID="Label23" runat="server">TOTAL FS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txttotfs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                 </table>
                                 </td>
                               
                                 <td>
                                 <table border="1px" style="border-collapse:collapse">
                                   
                                  <tr>
                                 <td  >
                                 <asp:label Font-Bold="true" ID="Label5" runat="server">36HS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt36hs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td  >
                                 <asp:label Font-Bold="true" ID="Label6" runat="server">38HS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt38hs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td  >
                                 <asp:label Font-Bold="true" ID="Label8" runat="server">40HS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt40hs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td  >
                                 <asp:label Font-Bold="true" ID="Label9" runat="server">42HS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt42hs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>

                                  <tr>
                                 <td  >
                                 <asp:label Font-Bold="true" ID="Label10" runat="server">44HS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txt44hs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td style="width: 50%;">
                                 <asp:label Font-Bold="true" ID="Label24" runat="server">TOTAL HS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txttoths" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                               
                               
                                 </table>
                                 </td>
                                 </tr>
                                 </table>
                                 
                                 </div>

                                   <div style="width:21%" class="col-lg-2">
                                  <label>Casual's Total</label>
                                   <table width="110%">
                                 <tr>
                                 <td>
                                 <table border="1px" style="border-collapse:collapse">
                                 <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" runat="server">S FS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtsfs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label11" runat="server">M FS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtmfs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label12" runat="server">L FS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtlfs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label13" runat="server">XL FS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtxlfs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label14" runat="server">XXL FS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtxxlfs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                 <tr>
                                 <td style="width: 50%;">
                                 <asp:label Font-Bold="true" ID="Label25" runat="server">TOTAL FS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txttotcfs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                 </table>
                                 </td>
                                 
                                 <td>
                                 <table border="1px" style="border-collapse:collapse">
                                 <tr>
                                 <td>
                                 <asp:Label ID="Label28" Font-Bold="true" runat="server">S HS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtshs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label29" runat="server">M HS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtmhs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label30" runat="server">L HS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtlhs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label31" runat="server">XL HS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtxlhs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label32" runat="server">XXL HS</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtxxlhs" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                 <tr>
                                 <td style="width: 50%;">
                                 <asp:label Font-Bold="true" ID="Label33" runat="server">TOTAL HS</asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txttotchs" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                 </table>
                                 </td>
                                 </tr>
                               
                                </table>
                             
                            </div>
                            <div style="width:20%" class="col-lg-2">
                            <label>Trousers Total</label>
                            <table  align="right">
                            <tr>
                            <td>
                            <table align="right" border="1px" style="border-collapse:collapse">
                                 <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label15" runat="server">28</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtt28" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label16" runat="server">30</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtt30" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label17" runat="server">32</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtt32" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label18" runat="server">34</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtt34" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                <td style="width: 50%;">
                                 <asp:label Font-Bold="true" ID="Label26" runat="server">TOTAL </asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txttott" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                 </table>
                                 </td>
                                 <td>
                                  <table align="right" border="1px" style="border-collapse:collapse">
                                   <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label19" runat="server">36</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtt36" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label20" runat="server">38</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtt38" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label21" runat="server">40</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtt40" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                  <tr>
                                 <td>
                                 <asp:Label Font-Bold="true" ID="Label22" runat="server">42</asp:Label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txtt42" Enabled="false" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 </tr>
                                 <tr>
                                <td style="width: 50%;">
                                 <asp:label Font-Bold="true" ID="Label27" runat="server">TOTAL </asp:label>
                                 </td>
                                 <td>
                                 <asp:TextBox ID="txttott1" Enabled="false" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                                 </td>
                                 </tr>
                                 </table>
                                 </td>
                                 </tr>
                                 </table>
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
                                                    <label>  For Casual's /Shirt's</label>
                                                        <asp:Panel ID="Panel1" Visible="false"  runat="server" ScrollBars="Both" Height="260" Width="100%">
                                                            <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                                OnRowDataBound="GridView2_RowDataBound" OnRowCommand="Gridview1_SelectedIndexChanged"
                                                                OnRowDeleting="GridView2_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                                Width="100%" runat="server">
                                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" ControlStyle-Width="100%" ItemStyle-Width="2%"
                                                                        HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                         <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sample No" ControlStyle-Width="100%" ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsamno" Height="30px" OnTextChanged="sample_check" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Type" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                           <asp:DropDownList ID="drpitemtype" Enabled="false" runat="server" >
                                                                           <asp:ListItem Text="Shirt" Value="1"></asp:ListItem>
                                                                           <asp:ListItem Text="Casual" Value="2"></asp:ListItem></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="F"   ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                        <asp:CheckBox ID="chkF" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="H" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                          <asp:CheckBox ID="chkH" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="36/S" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender0324" runat="server"
                                                                            TargetControlID="txt36s" ValidChars="" FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt36s" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt36s_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="38/M" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender0325" runat="server"
                                                                            TargetControlID="txt38m" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt38m" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt38m_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="40/L" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender0326" runat="server"
                                                                            TargetControlID="txt40l" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt40l" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt40l_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="42/XL" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender0327" runat="server"
                                                                            TargetControlID="txt42xl" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt42xl" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt42xl_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="44/XXL" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender0321" runat="server"
                                                                            TargetControlID="txt44xxl" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt44xxl" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt44xxl_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                    <label>  For Trouser</label>
                                                        <asp:Panel ID="Panel2" Visible="false"  runat="server" ScrollBars="Both" Height="260" Width="100%">
                                                            <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowFooter="True"
                                                                OnRowDataBound="GridView1_RowDataBound" OnRowCommand="Gridview1_SelectedIndexChanged"
                                                                OnRowDeleting="GridView1_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                                Width="100%" runat="server">
                                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" ControlStyle-Width="100%" ItemStyle-Width="2%"
                                                                        HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                          
                                                                            <%# Container.DataItemIndex +1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Sample No" ControlStyle-Width="100%" ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsamno" OnTextChanged="sampletro" AutoPostBack="true" Height="30px"  runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Type" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                           <asp:DropDownList ID="drpitemtype" Enabled="false" runat="server" >
                                                                           <asp:ListItem Text="Trouser" Value="3"></asp:ListItem>
                                                                           </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  
                                                                     <asp:TemplateField HeaderText="28" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server"
                                                                            TargetControlID="txt28t" ValidChars="" FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt28t" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt28t_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="30" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server"
                                                                            TargetControlID="txt30t" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt30t" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt30t_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="32" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server"
                                                                            TargetControlID="txt32t" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt32t" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt32t_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="34" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server"
                                                                            TargetControlID="txt34t" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt34t" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt34t_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="36" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                                                                            TargetControlID="txt36t" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt36t" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt36t_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="38" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender201" runat="server"
                                                                            TargetControlID="txt38t" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt38t" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt38t_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="40" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender221" runat="server"
                                                                            TargetControlID="txt40t" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt40t" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt40t_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="42" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender211" runat="server"
                                                                            TargetControlID="txt42t" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txt42t" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txt42t_textchanged">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    
                                                    </td>
                                                </tr>
                                                <tr>
                                                   
                                                   
                                            </table>
                                            
                                            </td> </tr> </tbody>
                                         
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

