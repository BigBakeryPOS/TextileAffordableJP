<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customermaster.aspx.cs"
    Inherits="Billing.Accountsbootstrap.AccPage" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Customer Registration</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
      <link rel="Stylesheet" type="text/css" href="../css/date.css" />
      <link href="../Styles/style1.css" rel="stylesheet"/>
      <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
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
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12">
            <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">
            </h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
               
 <form id="Form1" runat="server">

 <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
 <ContentTemplate>

                     <div class="form-group">
                    <div id="add" runat="server" class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                       <%-- <div class="col-lg-2">
                        </div>--%>
                        <div class="col-lg-3">
                            <div class="form-group" id="divcode" runat="server">
                                <label>
                                    Ledgerid</label>
                                <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server" Visible="false"></asp:TextBox>
                            </div>
                            <div class="col-lg-6">
                            <div class="form-group">
                                <label>
                                    Contact ID</label>
                                <asp:TextBox CssClass="form-control" Width="153px" Style="margin-left: -15px" ID="txtCustomerid" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                            <div class="form-group" style="margin-top: 0px">
                                                <label  runat="server">
                                                    Inital</label>
                                                <asp:TextBox CssClass="form-control" style="text-transform:uppercase" ID="TextBox1" 
                                    runat="server" Width="140px"></asp:TextBox>
                                            </div>
                                        </div>
                            <div class="form-group">
                                <label>
                                    Contact Name</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="reqName" Text="*"
                                    ControlToValidate="txtcustomername" ErrorMessage="Please enter your name!" Style="color: Red" />
                                    <asp:TextBox CssClass="form-control" ID="txtcustomername" MaxLength="50" AutoPostBack="true" 
                                    runat="server" OnTextChanged="txtcustomername_TextChanged" ></asp:TextBox>
                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" 

ValidChars=" ."  TargetControlID="txtcustomername" />
                                    </div>
                                     <div class="form-group">
                                <label>
                                    Print Name</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2" Text="*"
                                    ControlToValidate="txtprintname" ErrorMessage="Please enter your Print name!" Style="color: Red" />
                                    <asp:TextBox CssClass="form-control" ID="txtprintname" MaxLength="50" 
                                    runat="server"  ></asp:TextBox>
                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" 

ValidChars=" ."  TargetControlID="txtprintname" />
                                    </div>
                            <label>
                                Mobile No</label>
                            <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="mobno" Text="*"
                                ControlToValidate="txtmobileno" ErrorMessage="Please enter your Mobile No!" Style="color: Red" /><br />
                            <asp:RegularExpressionValidator runat="server" ID="rexNumber" ValidationGroup="val1"
                                ControlToValidate="txtmobileno" ValidationExpression="^[0-9]{10}$" ErrorMessage="Please enter a 10 digit number!"
                                Style="color: Red" />--%>
                            <div class="form-group input-group" >
                                <span class="input-group-addon">+91</span>
                                <asp:TextBox CssClass="form-control" ID="txtmobileno" MaxLength="10" runat="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" 

ValidChars=""  TargetControlID="txtmobileno" />

                            </div>
                            <%--<div class="form-group">
                                            <label>Mobile No</label>
                                            
                                            <asp:TextBox CssClass="form-control" ID="txtmobileno" runat="server"></asp:TextBox>
                                        </div>--%>
                            <div class="form-group">
                                <label>
                                    Phone No</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="mobno" Text="*"
                                ControlToValidate="txtphoneno" ErrorMessage="Please enter your Phone No!" Style="color: Red" />
                               
                                <asp:TextBox CssClass="form-control" ID="txtphoneno" MaxLength="12" runat="server"></asp:TextBox>

                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,Custom" 

ValidChars=" -"  TargetControlID="txtphoneno" />

                            </div>
                            <div class="form-group" >
                                <label>
                                    Address</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="address" ControlToValidate="txtaddress"
                                    Text="*" ErrorMessage="Please enter your Address!" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtaddress" MaxLength="150" runat="server" TextMode="MultiLine"></asp:TextBox>
                                

                            </div>
                              <div class="form-group" >
                                <label>
                                    GSTIN </label>
                              
                                <asp:TextBox CssClass="form-control" ID="txtgstin" MaxLength="150" runat="server" ></asp:TextBox>
                                

                            </div>
                            <div class="form-group" style="display:none">
                                <label>
                                    Area</label>
                                
                                <asp:TextBox CssClass="form-control" ID="txtarea" MaxLength="30" runat="server"></asp:TextBox>
                            </div>
                            <%--<label>Opening Balance</label>
                                        <div class="form-group input-group">
                                            <asp:TextBox CssClass="form-control" ID="txtblnce" runat="server" style="text-align:right" ></asp:TextBox>
                                            
                                        </div>--%>
                            <%--<div class="form-group">
                                            <label>Opening Balance</label>
                                            <asp:TextBox CssClass="form-control" ID="txtblnce" placeholder="For Ex: 0.00" runat="server" style="text-align:right"></asp:TextBox>
                                        </div>
                            --%>
                            <div class="form-group" style="display:none">
                                <label>
                                    Delivery Address</label>
                                
                                <asp:TextBox CssClass="form-control" ID="txtDelivery" MaxLength="150" runat="server"></asp:TextBox>
                                

                            </div>
                            <div class="form-group" style="display:none">
                                <label>
                                    Designation</label>
                              
                                <asp:TextBox CssClass="form-control" ID="txtDesignation" MaxLength="150" runat="server"></asp:TextBox>
                                

                            </div>
                            <div class="form-group" style="display:none">
                                <label>
                                    City</label>
                             
                                <asp:TextBox CssClass="form-control" ID="txtcity" MaxLength="30" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-3">
                            
                            <div class="form-group">
                                <label>
                                    Pincode</label>
                              <%--  <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="pincode" Text="*"
                                    ControlToValidate="txtpincode" ErrorMessage="Please enter your Pin Code!" Style="color: Red" />
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ValidationGroup="val1"
                                    Text="*" ControlToValidate="txtpincode" ValidationExpression="^[0-9]{6}$" ErrorMessage="Please enter a 6 digit pin code!"
                                    Style="color: Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtpincode" MaxLength="6" runat="server"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" 

ValidChars=""  TargetControlID="txtpincode" />

                            </div>
                            <div class="form-group">
                                <label>
                                    E-mail</label>
                                <%--                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="email" controltovalidate="txtemail" Text="*" errormessage="Please enter your Email!" style="color:Red" />--%>
                               <%-- <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ValidationGroup="val1"
                                    Text="*" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ErrorMessage="Please enter a correct Email Id!" Style="color: Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtemail" placeholder="For Ex: test@gmail.com"
                                    runat="server"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" 

ValidChars="_-@."  TargetControlID="txtemail" />


                            </div>
                            <div class="form-group" runat="server" Visible="false" >
                                <label>
                                    Contact Type</label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCustomerType"
                                    ValueToCompare="Select Contact Type" Operator="NotEqual" Type="String" ErrorMessage="Please Select Contact type"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlCustomerType" runat="server" AutoPostBack="true" Visible="false" class="form-control"
                                    OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label style="margin-left: -15px">
                                        Opening Balance</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                        ControlToValidate="txtOBalance" Text="*" ErrorMessage="Please enter your opening balance amount!"
                                        Style="color: Red" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                        FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtOBalance" />
                                    <asp:TextBox CssClass="form-control" ID="txtOBalance" runat="server" MaxLength="8"
                                        Style="text-align: right; width: 140px;">0</asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label>
                                        Choose Type</label>
                                    <asp:DropDownList ID="ddlCDType" runat="server" class="form-control" Width="140px">
                                        <asp:ListItem Text="Credit" Value="Credit Note"></asp:ListItem>
                                        <asp:ListItem Text="Debit" Value="Debit Note"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group" id="Div2" runat="server">
                                <label>
                                    Is Active</label>
                                <asp:DropDownList ID="ddlIsActive" runat="server" class="form-control">
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                             <div class="form-group">
                                        <label>
                                            Province</label>
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlProvince"
                                    ValueToCompare="0" Operator="NotEqual" Type="String" ErrorMessage="Select Province type"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlProvince"  style="font-weight:bold" 
                                            runat="server"   CssClass="form-control" >
                                            <asp:ListItem Text="Select Province type" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Inner" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Outer" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                            <div class="form-group" runat="server">
                                <label>
                                    Tin No</label>
                                <asp:TextBox CssClass="form-control" ID="txtTinNO" MaxLength="30" Text="0" runat="server"></asp:TextBox>
                            </div>

                         
                        
                            

                            
                        </div>
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-3">
                        <div class="form-group" style="display:none">
                                <label>
                                    Selling Price</label>
                               
                                  
                                <asp:DropDownList ID="ddlPrice" runat="server" class="form-control">
                                <asp:ListItem Text="MRP" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="DSP" Value="2"></asp:ListItem>
                                <asp:ListItem Text="WSP" Value="3"></asp:ListItem>
                                   
                                </asp:DropDownList>
                            </div>
                        <div class="form-group" style="display:none">
                                <label>
                                    CST</label>
                               
                                <asp:TextBox CssClass="form-control" ID="txtCST" runat="server">CST</asp:TextBox>
                           
                            </div>
                            <div class="form-group" style="display:none">
                                <label>
                                    PAN </label>
                                <asp:TextBox CssClass="form-control" ID="txtPAN" runat="server">PAN</asp:TextBox>
                            </div>
                            <div class="form-group" style="display:none">
                                <label>
                                    Address Proof </label>
                                <asp:TextBox CssClass="form-control" ID="txtAddressProof" runat="server">add</asp:TextBox>
                            </div>
                            <div class="form-group" style="display:none">
                                <label>
                                    ID Proof </label>
                                <asp:TextBox CssClass="form-control" ID="txtIDProof" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group" style="display:none">
                                <label>
                                   Birth Date  </label>
                                <asp:TextBox CssClass="form-control" ID="txtdob" runat="server"></asp:TextBox>
                                  <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdob" PopupButtonID="txtdate1"
                                        EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                            </div>
                            <div class="form-group" style="display:none">
                                <label>
                                   Anniversary Date  </label>
                                <asp:TextBox CssClass="form-control" ID="txtAnniversary" runat="server"></asp:TextBox>
                                  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtAnniversary" PopupButtonID="txtdate1"
                                        EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                            </div>
                            <div class="form-group" >
                                <label>
                                   Credit Days  </label>
                                <asp:TextBox CssClass="form-control" ID="txtCreditDays" runat="server">0</asp:TextBox>
                            </div>
                             <div class="form-group" >
                                <label>
                                  Select Agent   </label>
                               <asp:DropDownList ID="drpagent" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                            <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" OnClick="Add_Click"
                                ValidationGroup="val1" Style="width: 120px; margin-top: 25px" />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"
                                Style="width: 120px; margin-top: 25px" />
                            <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                            <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                    <div id="div1" runat="server" align="center">
                        <table cellpadding="1" cellspacing="2" width="450px" style="border: 1px solid black;
                            height: 250px;">
                            <tr style="height: 30px">
                                <td>
                                    <table width="450px" style="border: 1px solid black; height: 36px;">
                                        <tr>
                                            <td runat="server" colspan="4">
                                               
                                                    <asp:RadioButtonList ID="masterradio" runat="server" RepeatLayout="Table"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem style="padding-left:10px" Text="Customer" Value="1"></asp:ListItem>
                                                       
                                                        <asp:ListItem style="padding-left:10px" Text="Vendor" Value="6"></asp:ListItem>
                                                       
                                                        <asp:ListItem style="padding-left:10px" Text="Dealer" Value="2"></asp:ListItem>
                                                        <asp:ListItem style="padding-left:10px" Text="Service Center" Value="5"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table style="width: 100%">
                                       
                                        <tr>
                                            <td style="width: 30%">
                                            </td>
                                            <td style="width: 35%">
                                                <div>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                    <asp:GridView ID="GridView1" runat="server">
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                            <td style="width: 35%">
                                            </td>
                                        </tr>
                                        <tr style="height: 6px">
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 30%">
                                                        </td>
                                                        <td style="width: 35%" align="center">
                                                            <asp:Button ID="btnUpload" runat="server" Height="31px" class="btn btn-info" Text="Upload"
                                                                Width="100px" OnClick="btnUpload_Click" />
                                                        </td>
                                                        <td style="width: 35%">
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 10px">
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 15%">
                                                        </td>
                                                        <td style="width: 70%" align="center">
                                                            <asp:Button ID="Button2" runat="server" class="btn btn-info" Text="Download the Sample Excel Format"
                                                                Height="31px" OnClick="btnFormat_Click" />
                                                            <asp:Button ID="Button1" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click" />
                                                        </td>
                                                        <td style="width: 15%">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
</div>
                    </div>

 </ContentTemplate>
 <Triggers>
 </Triggers>
 </asp:UpdatePanel>


 <asp:UpdateProgress ID="Updateprogress" runat="server" AssociatedUpdatePanelID="Updatepanel1">
 <ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
 <asp:Image  ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                    position: fixed; top: 50%; left: 40%;" />
  </div>
 </ProgressTemplate>
 </asp:UpdateProgress>

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
