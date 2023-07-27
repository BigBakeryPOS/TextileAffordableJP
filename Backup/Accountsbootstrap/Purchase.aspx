<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purchase.aspx.cs" Inherits="Billing.Accountsbootstrap.Purchase" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html lang="en">
<head id="Head1" runat="server">
    <script language="javascript" type="text/javascript">

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }

    </script>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Purchase Form</title>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
  
    <link href="../Styles/style1.css" rel="stylesheet" />
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
            window.open("http://localhost:57111/Accountsbootstrap/itempage.aspx?Mode=Purchase", "Popup", 'height=300,width=500,resizable=yes,modal=yes,center=yes');
        }
    </script>

      <link rel="stylesheet" href="../css/chosen.css" />


      <script type = "text/javascript">
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
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" Visible="false">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="black" CssClass="label" Visible="false">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="black" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1" method="post">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12" >

                 <h2  style="text-align:left; color: #fe0002;margin-top:0px">
                        <asp:Label ID="lblTitle" Text="Purchase" runat="server"></asp:Label>
                         </h2>
                         <h4>
                        <div style="text-align:right;margin-top:-30px;margin-right:15px;">
                         <asp:Button ID="gridbutton" runat="server" Text="View grid" 
                                                onclick="gridbutton_Click" />
                  &nbsp;&nbsp
                                            <asp:CheckBox ID="chknew" Visible="false" runat="server" Text="New Vendor" style="color:#00a8e6" AutoPostBack="false" OnCheckedChanged="chknew_CheckedChanged" />
                                            <asp:LinkButton ID="LinkButton1" runat="server"  Text="New Vendor" onclientclick="window.open('customermaster.aspx?name=Add%20New');"></asp:LinkButton>            
             </div>
                             <h4>
                             </h4>
                             <%--  <h1 class="page-header" style="text-align: center; color: #fe0002;">
                        Purchase</h1>--%>
                             <div class="panel panel-default">
                                 <div class="panel-body">
                                     <div class="row">
                                         <asp:ValidationSummary ID="val1" runat="server" 
                                             HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="false" 
                                             ValidationGroup="val1" />
                                         <asp:ScriptManager ID="ScriptManager2" runat="server">
                                         </asp:ScriptManager>
                                         <div class="col-lg-1">
                                         </div>
                                         <div class="col-lg-2">
                                             <div class="form-group" runat="server" visible="false">
                                                 <label>
                                                 Purchase Type</label>
                                                 <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                     ControlToValidate="ddlPurchaseType" ErrorMessage="Please Select Purchase type" 
                                                     InitialValue="0" Operator="NotEqual" Style="color: Red" Text="*" Type="String" 
                                                     ValidationGroup="val1" ValueToCompare="0"></asp:CompareValidator>
                                                 <asp:DropDownList ID="ddlPurchaseType" runat="server" AutoPostBack="true" 
                                                     CssClass="form-control" Enabled="false" 
                                                     OnSelectedIndexChanged="ddlPurchaseType_SelectedIndexChanged">
                                                     <%--     <asp:ListItem Text="Select Type" Value="0"></asp:ListItem>--%>
                                                     <asp:ListItem Text="Direct Purchase" Value="1"></asp:ListItem>
                                                     <asp:ListItem Text="Purchase Order" Value="2"></asp:ListItem>
                                                 </asp:DropDownList>
                                             </div>
                                             <div ID="dvi1" runat="server" class="form-group" visible="false">
                                                 <label ID="lblPurchaseOrder" runat="server">
                                                 PurchaseOrder No</label>
                                                 <%--asp:CompareValidator ID="CompareValidator15" runat="server" Enabled="false" ValidationGroup="val1"  Text="*" style="color:Red" InitialValue="0"
                          ControlToValidate="ddlPurchaseOrder" ValueToCompare="Select No" Operator="Equal"  Type="String"   errormessage="Please Select PurchaseOrder No"></asp:CompareValidator>--%>
                                                 <asp:DropDownList ID="ddlPurchaseOrder" runat="server" AutoPostBack="true" 
                                                     class="form-control" 
                                                     OnSelectedIndexChanged="ddlPurchaseOrder_SelectedIndexChanged" Width="140px">
                                                 </asp:DropDownList>
                                             </div>
                                             <div class="form-group">
                                                 <label>
                                                 Invoice Number</label>
                                                 <%-- <asp:Label runat="server" ID="lblDcNo" Style="font-weight: bold">Invoice Number</asp:Label>--%>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                     ControlToValidate="txtDCNo" ErrorMessage="Please enter Invoice No!" 
                                                     Style="color: Red" Text="*" ValidationGroup="" />
                                                 <asp:TextBox ID="txtDCNo" runat="server" CssClass="form-control" 
                                                     onkeypress="return NumberOnly()"></asp:TextBox>
                                             </div>
                                             <div class="form-group">
                                                 <label>
                                                 Invoice Date</label>
                                                 <%--  <asp:Label runat="server" ID="lblDCDate" Style="font-weight: bold">Invoice Date</asp:Label>--%>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                     ControlToValidate="txtDCDate" ErrorMessage="Enter Invoice Date" 
                                                     Style="color: Red" Text="*" ValidationGroup="val1"></asp:RequiredFieldValidator>
                                                 <br />
                                                 <asp:TextBox ID="txtDCDate" runat="server" CssClass="form-control" 
                                                     onkeydown="return DateFormat(this, event.keyCode)" 
                                                     onkeyup="ValidateDate(this, event.keyCode)"></asp:TextBox>
                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                     CssClass="cal_Theme1" Format="dd/MM/yyyy" TargetControlID="txtDCDate">
                                                 </ajaxToolkit:CalendarExtender>
                                             </div>

                                           <div class="form-group">
                                                 <label>
                                                 Province</label>
                                            <asp:DropDownList ID="ddlProvince"  runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Select Province type" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="TamilNadu" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Others" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
              
                                             </div>

                                         </div>
                                         <div class="col-lg-2">
                                             <div class="form-group">
                                                 <label>
                                                 Bill No</label>
                                                 <%--            <asp:Label runat="server" ID="Label3" Style="font-weight: bold">Bill No. </asp:Label>--%>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" 
                                                     runat="server" FilterType="Numbers" TargetControlID="txtpono" ValidChars="" />
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                     controltovalidate="txtpono" errormessage="Please enter Bill NO!" 
                                                     style="color:Red" Text="*" ValidationGroup="val1" />
                                                 <asp:TextBox ID="txtpono" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                                     runat="server" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom" 
                                                     TargetControlID="txtpono" ValidChars="" />
                                             </div>
                                             <div class="form-group">
                                                 <label>
                                                 Bill Date</label>
                                                 <%--      <asp:Label runat="server" ID="Label4" Style="font-weight: bold;">Bill Date</asp:Label>--%>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                     ControlToValidate="txtpodate" ErrorMessage="Enter Bill Date" Style="color: Red" 
                                                     Text="*" ValidationGroup="val1"></asp:RequiredFieldValidator>
                                                 <br />
                                                 <asp:TextBox ID="txtpodate" runat="server" CssClass="form-control" 
                                                     onkeydown="return DateFormat(this, event.keyCode)" 
                                                     onkeyup="ValidateDate(this, event.keyCode)" Text="--Select Date--"></asp:TextBox>
                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                     CssClass="cal_Theme1" Format="dd/MM/yyyy" TargetControlID="txtpodate">
                                                 </ajaxToolkit:CalendarExtender>
                                             </div>



                                             <%--  <div class="form-group">
                                <asp:Label runat="server" ID="Label1" style="font-weight:bold"  >Vendor Name </asp:Label>
                                <asp:DropDownList runat="server" ID="ddlvendor" class="form-control"  AutoPostBack="true"
                                                onselectedindexchanged="ddlcustomerID_SelectedIndexChanged"  >
                                           
                                        </asp:DropDownList>
                               
                               

                                <asp:TextBox ID="txtSupplied" runat="server" CssClass="form-control" TextMode="MultiLine" Height="80px"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="val1" ControlToValidate="txtSupplied" style="color:Red" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
                                 </div>--%>
                                             <div class="form-group">
                                                 <label>
                                                 Supplier Name</label>
                                                 <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                                     ControlToValidate="ddlvendor" ErrorMessage="Please Select Supplier" 
                                                     InitialValue="0" Operator="NotEqual" Style="color: Red" Text="*" Type="String" 
                                                     ValidationGroup="val1" ValueToCompare="Select Supplier"></asp:CompareValidator>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                     ControlToValidate="txtCustname" ErrorMessage="Enter Supplier Name" 
                                                     Style="color: Red" Text="*" ValidationGroup="val1"></asp:RequiredFieldValidator>
                                                 <br />
                                                 <asp:DropDownList ID="ddlvendor" runat="server" AutoPostBack="true" 
                                                     class="form-control" CssClass="chzn-select" Height="60px" 
                                                     OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged" Width="195px">
                                                 </asp:DropDownList>
                                                 <asp:TextBox ID="txtCustname" runat="server" CssClass="form-control" 
                                                     TabIndex="1" Visible="false"></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                                     runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom" 
                                                     TargetControlID="txtCustname" ValidChars=" " />
                                             </div>
                                         </div>
                                         <%--<asp:Label ID="Label2" runat="server"  style="color:Red"></asp:Label>--%>
                                         <%--<div class="form-group">
                                            <label></label>
                                            <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                                        </div>--%>
                                         <div class="col-lg-2">
                                             <div runat="server" class="form-group">
                                                 <label>
                                                 City</label>
                                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Text="*"
                                                ValidationGroup="val1" Enabled="false" ControlToValidate="txtcity" Style="color: Red"
                                                ErrorMessage="Enter city Name"></asp:RequiredFieldValidator><br />--%>
                                                 <asp:TextBox ID="txtcity" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                             </div>
                                             <div class="form-group">
                                                 <label>
                                                 Area</label>
                                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Text="*"
                                                ValidationGroup="val1" Enabled="false" ControlToValidate="txtArea" Style="color: Red"
                                                ErrorMessage="Enter Area"></asp:RequiredFieldValidator><br />--%>
                                                 <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                             </div>
                                             <div class="form-group">
                                                 <label>
                                                 Address</label>
                                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="val1"
                                                Enabled="false" ControlToValidate="txtaddress" Text="*" Style="color: Red" ErrorMessage="Enter Address"></asp:RequiredFieldValidator><br />--%>
                                                 <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control" 
                                                     TabIndex="2"></asp:TextBox>
                                             </div>
                                         </div>
                                         <div class="col-lg-2">
                                             <div class="form-group">
                                                 <label>
                                                 Pincode</label>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                     ControlToValidate="txtpincode" ErrorMessage="Please enter a 6 digit pin code!" 
                                                     Style="color: Red" Text="*" ValidationExpression="^[0-9]{6}$" 
                                                     ValidationGroup="val1" />
                                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="val1"
                                                Enabled="false" Text="*" ControlToValidate="txtpincode" Style="color: Red" ErrorMessage="Enter Pincode"></asp:RequiredFieldValidator><br />--%>
                                                 <asp:TextBox ID="txtpincode" runat="server" CssClass="form-control" 
                                                     MaxLength="6" TabIndex="5"></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" 
                                                     runat="server" FilterType="Numbers,Custom" TargetControlID="txtpincode" 
                                                     ValidChars="" />
                                                 <%--<asp:LinkButton ID="LinkButton1" Text="Add-Contacts" runat="server" AccessKey="N" OnClientClick="return AddVendor()"></asp:LinkButton>--%>
                                             </div>
                                             <div class="form-group">
                                                 <label>
                                                 Mobile No.</label>
                                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Enabled="false"
                                                ValidationGroup="val1" ControlToValidate="txtmobileno" Text="*" Style="color: Red"
                                                ErrorMessage="Enter Mobile no"></asp:RequiredFieldValidator><br />--%>
                                                 <%-- <asp:RegularExpressionValidator runat="server" ID="rexNumber" ValidationGroup="val1" Text="*"
                                ControlToValidate="txtmobileno" ValidationExpression="^[0-9]{10}$" ErrorMessage="Please enter a 10 digit number!"
                                Style="color: Red" />--%>
                                                 <asp:TextBox ID="txtmobileno" runat="server" AutoPostBack="true" 
                                                     CssClass="form-control" MaxLength="33" TabIndex="3"></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                                     runat="server" FilterType="Numbers,Custom" TargetControlID="txtmobileno" 
                                                     ValidChars="," />
                                             </div>
                                             <div class="form-group">
                                                 <label>
                                                 Narration</label>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                     ControlToValidate="txtNarration" ErrorMessage="Enter Narration" 
                                                     Style="color: Red" Text="*" ValidationGroup="val1"></asp:RequiredFieldValidator>
                                                 <br />--%>
                                                 <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control"></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                                     runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" 
                                                     TargetControlID="txtNarration" ValidChars=" +,!@#$%^&amp;*()-/:;." />
                                             </div>
                                             <div class="form-group">
                                                 <label ID="lbl1" runat="server" visible="false">
                                                 Mail ID</label>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                     ControlToValidate="txtMailid" ErrorMessage="Please enter a correct Email Id!" 
                                                     Style="color: Red" Text="*" 
                                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                     ValidationGroup="val1" />
                                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Enabled="false"
                                                Text="*" ValidationGroup="val1" ControlToValidate="txtMailid" Style="color: Red"
                                                ErrorMessage="Enter MailID"></asp:RequiredFieldValidator><br />--%>
                                                 <asp:TextBox ID="txtMailid" runat="server" CssClass="form-control" TabIndex="4" 
                                                     Visible="false"></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" 
                                                     runat="server" FilterType="UppercaseLetters,LowercaseLetters,Numbers,Custom" 
                                                     TargetControlID="txtMailid" ValidChars="_-.@" />
                                             </div>
                                         </div>
                                         <div class="col-lg-2">
                                             <%--    <div class="col-lg-4" >--%>
                                             <div class="form-group">
                                                 <label>
                                                 Payment Mode</label>
                                                 <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                                     ControlToValidate="ddlPayMode" ErrorMessage="Please Select Payment Mode" 
                                                     InitialValue="0" Operator="NotEqual" Style="color: Red" Text="*" Type="String" 
                                                     ValidationGroup="val1" ValueToCompare="0"></asp:CompareValidator>
                                                 <asp:DropDownList ID="ddlPayMode" runat="server" AutoPostBack="true" 
                                                     CssClass="form-control" 
                                                     OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged">
                                                     <asp:ListItem Text="Select Payment Mode" Value="0"></asp:ListItem>
                                                     <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                                     <asp:ListItem Text="Cheque" Value="Cheque"></asp:ListItem>
                                                     <asp:ListItem Selected="True" Text="Credit" Value="Credit"></asp:ListItem>
                                                     <asp:ListItem Text="DD" Value="DD"></asp:ListItem>
                                                 </asp:DropDownList>
                                             </div>
                                             <div class="form-group">
                                                 <label ID="Label5" runat="server">
                                                 Bank Name</label>
                                                 <br />
                                                 <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="true" 
                                                     Class="form-control" CssClass="chzn-select" Height="60px" 
                                                     OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" Width="195px">
                                                 </asp:DropDownList>
                                             </div>
                                             <div class="form-group" style="margin-top: 18px">
                                                 <label>
                                                 Cheque No</label>
                                                 <asp:DropDownList ID="ddlChequeNo" runat="server" class="form-control" 
                                                     CssClass="chzn-select" Height="60px" Width="195px">
                                                 </asp:DropDownList>
                                                 <%--       <asp:TextBox CssClass="form-control"  ID="txtCheque" runat="server" ></asp:TextBox>--%>
                                             </div>
                                         </div>
                                         <div class="col-lg-2">
                                         </div>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-lg-12" style="margin-top:1px">
                                     <%--     <div class="panel-body">--%>
                                     <div class="table-responsive">
                                         <asp:Label ID="lblError" runat="server" Style="color: Red"></asp:Label>
                                         <table ID="dataTables-example" 
                                             class="table table-striped table-bordered table-hover">
                                             <tr>
                                                 <td colspan="2">
                                                     <asp:Panel ID="Panel1" runat="server" Height="300" ScrollBars="Both" 
                                                         Width="100%">
                                                         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                             CssClass="chzn-container" GridLines="None" 
                                                             OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting" 
                                                             ShowFooter="True" Width="100%">
                                                             <HeaderStyle BackColor="#59d3b4" BorderColor="Gray" BorderStyle="Solid" 
                                                                 BorderWidth="1px" Font-Names="arial" Font-Size="Smaller" 
                                                                 HorizontalAlign="Center" />
                                                             <RowStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="0.5px" />
                                                             <Columns>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderStyle-Width="5%" 
                                                                     HeaderText="S.No" ItemStyle-Width="6%">
                                                                     <ItemTemplate>
                                                                         <%--    <asp:Label ID="txtno"   runat="server" ></asp:Label>--%>
                                                                         <asp:TextBox ID="txtno" runat="server" Height="30px"></asp:TextBox>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderStyle-Width="19%" HeaderText="Category" 
                                                                     ItemStyle-Width="17%" Visible="false">
                                                                     <ItemTemplate>
                                                                         <asp:DropDownList ID="drpCategory" runat="server" AppendDataBoundItems="true" 
                                                                             AutoPostBack="true" CssClass="chzn-select" Height="26px" 
                                                                             OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" Width="100%">
                                                                         </asp:DropDownList>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Product Code" 
                                                                     ItemStyle-Width="13%" Visible="false">
                                                                     <ItemTemplate>
                                                                         <asp:DropDownList ID="productCode" runat="server" AppendDataBoundItems="true" 
                                                                             AutoPostBack="true" CssClass="chzn-select" Height="26px" 
                                                                             OnSelectedIndexChanged="productCode_SelectedIndexChanged" Width="100%">
                                                                         </asp:DropDownList>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderStyle-Width="19%" HeaderText=" Product " 
                                                                     ItemStyle-Width="17%">
                                                                     <ItemTemplate>
                                                                         <asp:DropDownList ID="drpItem" runat="server" AppendDataBoundItems="true" 
                                                                             AutoPostBack="true" CssClass="chzn-select" Height="26px" 
                                                                             OnSelectedIndexChanged="drpItem_SelectedIndexChanged" Width="100%">
                                                                         </asp:DropDownList>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                          <%--                       <asp:TemplateField ControlStyle-Width="100%" HeaderText="Ref.no" 
                                                                     ItemStyle-Width="6%">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtrefno" runat="server" Height="30px" Width="85px"></asp:TextBox>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderText="Cretificate No" 
                                                                     ItemStyle-Width="6%">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtcerno" runat="server" Height="30px" Width="85px"></asp:TextBox>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>--%>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderText="Stock" 
                                                                     ItemStyle-Width="6%">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtStock" runat="server" Enabled="false" Height="30px" 
                                                                             Width="85px"></asp:TextBox>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderText="POQty" 
                                                                     ItemStyle-Width="6%" Visible="false">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtPOQty" runat="server" AutoPostBack="true" Enabled="false" 
                                                                             Height="30px" Width="85px"></asp:TextBox>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderText="Qty" 
                                                                     ItemStyle-Width="8%">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtQty" runat="server" AutoPostBack="true" Height="30px" 
                                                                             OnTextChanged="txtQty_TextChanged" Width="85px"></asp:TextBox>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" 
                                                                             runat="server" FilterType="Numbers, Custom" TargetControlID="txtQty" 
                                                                             ValidChars="." />
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderText="Rate" 
                                                                     ItemStyle-Width="10%">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtRate" runat="server" AutoPostBack="true" Height="30px" 
                                                                             OnTextChanged="txtRate_TextChanged" Width="150px"></asp:TextBox>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" 
                                                                             runat="server" FilterType="Numbers,Custom" TargetControlID="txtRate" 
                                                                             ValidChars="." />
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderText="Discount %" 
                                                                     ItemStyle-Width="6%">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtDiscount" runat="server" AutoPostBack="true" Height="30px" 
                                                                             MaxLength="6" OnTextChanged="txtDiscount_TextChanged" Width="85px"></asp:TextBox>
                                                                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" 
                                                                             runat="server" FilterType="Numbers,Custom" TargetControlID="txtDiscount" 
                                                                             ValidChars="." />
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderText="Tax %" 
                                                                     ItemStyle-Width="8%" Visible="false">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtTax" Text="0" runat="server" AutoPostBack="true" Height="30px" 
                                                                             OnTextChanged="txttax_TextChanged" Width="85px"></asp:TextBox>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderText="GST %" 
                                                                     ItemStyle-Width="6%">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtcst" runat="server" AutoPostBack="true" Height="30px" 
                                                                             OnTextChanged="txtCst_TextChanged" Width="85px"></asp:TextBox>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ControlStyle-Width="100%" HeaderText="Amount" 
                                                                     ItemStyle-Width="12%">
                                                                     <ItemTemplate>
                                                                         <asp:TextBox ID="txtAmount" runat="server" Enabled="false" Height="30px" 
                                                                             Width="150px"></asp:TextBox>
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField ItemStyle-Width="9%">
                                                                     <ItemTemplate>
                                                                         <asp:Button ID="ButtonAdd1" runat="server" AutoPostback="false" 
                                                                             EnableTheming="false" Height="30px" OnClick="ButtonAdd1_Click" Text="Add New" 
                                                                             Visible="false" />
                                                                     </ItemTemplate>
                                                                 </asp:TemplateField>
                                                                 <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                             </Columns>
                                                         </asp:GridView>
                                                     </asp:Panel>
                                             
                                    
                                    

                                                          <div class="col-lg-12">
                                                       <div class="col-lg-2" ></div>
                                                       <div class="col-lg-2" ></div>
                                                       <div class="col-lg-2" ></div>
                                                       <div class="col-lg-2" ></div>
                                                        <div class="col-lg-2">
                                                             <label>
                                                             Freight Charges
                                                             <asp:TextBox ID="txtFreight" runat="server" AutoPostBack="true" 
                                                                 CssClass="form-control" OnTextChanged="txtLchange" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>
                                                             </label>
                                                         </div>
                                                         <div class="col-lg-2">
                                                             <label>
                                                             Loading/Unloading
                                                             <asp:TextBox ID="txtLU" runat="server" AutoPostBack="true" 
                                                                 CssClass="form-control" OnTextChanged="txtLchange" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>
                                                             </label>
                                                         </div>
                                                       </div>

                                                       
                                                          <div class="col-lg-12" style="display:none;">
                                                       <div class="col-lg-2" ></div>
                                                       <div class="col-lg-2" ></div>
                                                       <div class="col-lg-2" ></div>
                                                       <div class="col-lg-2">
                                                             <label>
                                                             Disc Amount
                                                             <asp:TextBox ID="txtdiscount" runat="server" AutoPostBack="true" 
                                                                 CssClass="form-control" Enabled="false" OnTextChanged="granddiscount" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>
                                                             </label>
                                                         </div>
                                                         <div class="col-lg-2">
                                                             <label>
                                                             Tax Amount
                                                             <asp:TextBox ID="txtTaxamt" runat="server" CssClass="form-control" 
                                                                 Enabled="false" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>
                                                             </label>
                                                         </div>
                                                         <div class="col-lg-2">
                                                             <label>
                                                             Cst Amount
                                                             <asp:TextBox ID="txtcstamnt" runat="server" CssClass="form-control" 
                                                                 Enabled="false" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>
                                                             </label>
                                                         </div>

                                                       </div>


                                                      <div class="col-lg-12">
                                                       <div class="col-lg-2" ></div>
                                                       <div class="col-lg-2" ></div>
                                                       <div class="col-lg-2" ></div>
                                                                <div class="col-lg-2">
                                                            <label>
                                                            CGST
                                                            <asp:TextBox CssClass="form-control" ID="txtcgst" runat="server" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox> </label></div>


                                                                <div class="col-lg-2">
                                                            <label>
                                                            SGST
                                                            <asp:TextBox CssClass="form-control" ID="txtsgst" runat="server" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox> </label></div>


                                                                <div class="col-lg-2">
                                                            <label>
                                                            IGST
                                                            <asp:TextBox CssClass="form-control" ID="txtigst" runat="server" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox> </label></div>
                                                            </div>


                                                               <div class="col-lg-12">
                                                       <div class="col-lg-2" >
                                                       <label>Received</label>
                                                       <asp:TextBox ID="txtreceived" runat="server" CssClass="form-control" ></asp:TextBox></div>
                                                       <div class="col-lg-2" ><label>Checking</label>
                                                       <asp:TextBox ID="txtchecking" runat="server" CssClass="form-control" ></asp:TextBox></div>
                                                       <div class="col-lg-2" >
                                                       <label>
                    File Upload</label>
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
                    <asp:FileUpload ID="fp_Upload" runat="server"  />
                     <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary" 
                                   onclick="btnUpload_Click"   Width="100px"/><asp:Image ID="img_Photo" runat="server" Width="70px"  BorderColor="1"/>
                            <asp:Label ID="lblFile_Path" runat="server" Visible="false"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
        <asp:PostBackTrigger ControlID = "btnUpload" />
    </Triggers>
                            </asp:UpdatePanel>
                    <br /></div>
                                                       <div class="col-lg-2" ></div>

                                                         <div class="col-lg-2">
                                                             <label>
                                                             Grand Total</label>
                                                             <asp:TextBox ID="txtgrandtotal" runat="server" CssClass="form-control" 
                                                                 Enabled="false" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>
                                                             
                                                             </div>
                                                        <div class="col-lg-2">

                                                            <label> Round Off</label>
                                                             <asp:TextBox ID="txtroundoff" runat="server" CssClass="form-control" 
                                                                 Enabled="false" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>
                                                             </label>
                                                         </div>

                                                       </div>




                                                 </td>
                                             </tr>
                                          
                                         </table>
                                     </div>
                                     <asp:Button ID="btnadd" runat="server" class="btn btn-success" 
                                         OnClick="Add_Click" Text="Save" ValidationGroup="val1" Width="120px" />
                                     <asp:Button ID="btnExit" runat="server" class="btn btn-warning" 
                                         OnClick="btnExit_Click" Text="Exit" Width="120px" />
                                     <%--  </div>--%>
                                 </div>
                             </div>
                             <h4>
                             </h4>
                             <h4>
                             </h4>
            </h4>
                </div>
                <%-- <div class="row">
                                <div class="col-lg-12">
                                <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover">
                                </table>
                                 </div>
                                
                               </div>
                               </div>
                                </div>--%>
                <%-- <div class="col-lg-12">
                               This Report is generated from entERPrise ERP Solution developed by Bigdbiz Solutions
                               </div>--%>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <!-- /.row (nested) -->
            </div>
            <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
            </div>
            <!-- /.col-lg-12 -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" >
        
         <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999;  opacity: 0.7;">
          <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif" AlternateText="Loading ..." ToolTip="Loading ..." style=" width:150px; padding: 10px;position:fixed;top:50%;left:40%;" />
       
          <%--<asp:Image ID="imgUpdateProgress1" runat="server" ImageUrl="../images/loading.gif" />--%>
        </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
		<script src="../js/chosen.min.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

    </form>
</body>
</html>
