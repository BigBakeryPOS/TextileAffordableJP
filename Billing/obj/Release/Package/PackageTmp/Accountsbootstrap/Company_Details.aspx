<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Company_Details.aspx.cs"
    Inherits="Billing.Accountsbootstrap.Pranav_Details" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Company Details</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script type="text/javascript" language="javascript">
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
    </script>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <link rel="stylesheet" href="../css/chosen.css" />
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
            <h1 class="page-header" style="text-align: center; color: #fe0002;">
                Company Details
            </h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <form id="Form1" runat="server">
                               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>
                                    Company Code</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                                    ControlToValidate="txtcompanycode" Text="*" ErrorMessage="Please enter Company Code"
                                    Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtcompanycode" MaxLength="15" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Company Name</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="reqName" ControlToValidate="txtcompanyname"
                                    Text="*" ErrorMessage="Please enter name" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtcompanyname" MaxLength="50" runat="server"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="txtcompanyID" MaxLength="50" runat="server"
                                    Visible="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Mobile No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="mobno" ControlToValidate="txtmobileno"
                                    Text="*" ErrorMessage="Please enter your Mobile No" Style="color: Red" />
                                <asp:RegularExpressionValidator runat="server" ID="rexNumber" ValidationGroup="val1"
                                    ControlToValidate="txtmobileno" Text="*" ValidationExpression="^[0-9]{10}$" ErrorMessage="Please enter a 10 digit number"
                                    Style="color: Red" />
                                <div class="form-group input-group">
                                    <span class="input-group-addon">+91</span>
                                    <asp:TextBox CssClass="form-control" ID="txtmobileno" MaxLength="10" runat="server"></asp:TextBox>
                                </div>
                            </div>
                              <div class="form-group">
                                <label>
                                    Phone No</label>
                                <%--  <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="phono" controltovalidate="txtphoneno" Text="*"  errormessage="Please enter Phone No" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtphoneno" MaxLength="15" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Address</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="address" controltovalidate="txtaddress" Text="*"  errormessage="Please enter Address" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtaddress" MaxLength="150" runat="server"></asp:TextBox>
                            </div>
                           
                             <div class="form-group">
                                <label>
                                    Area</label>
                                <%-- <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="area" controltovalidate="txtarea" Text="*"  errormessage="Please enter Area" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtarea" MaxLength="30" runat="server"></asp:TextBox>
                            </div>

                        </div>
                      
                        <div class="col-lg-3">
                          
                            <div class="form-group">
                                <label>
                                    Country</label>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCountry" ValueToCompare="Select Country"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Country."></asp:CompareValidator>

                                <asp:DropDownList ID="ddlCountry" class="form-control" Width="100%" OnSelectedIndexChanged="ddlCountry_OnSelectedIndexChanged"
                                    CssClass="chzn-select" AutoPostBack="true" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>
                                    State
                                </label>
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                <%--<asp:CheckBox runat="server" ID="chkDistrict" Text="New District" AutoPostBack="true"
                                    Visible="false" OnCheckedChanged="chkDistrict_CheckedChanged" />
                                <asp:TextBox Visible="false" Style="font-weight: bold" CssClass="form-control" ID="txtDistrict"
                                    Width="100%" MaxLength="30" runat="server"></asp:TextBox>--%>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlState" ValueToCompare="Select State"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select State."></asp:CompareValidator>
                                <asp:DropDownList ID="ddlState" class="form-control" Width="100%" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlState_OnSelectedIndexChanged"
                                    CssClass="chzn-select">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>
                                    City
                                </label>
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                <%--<asp:CheckBox runat="server" ID="chkCity" Text="New City" AutoPostBack="true" OnCheckedChanged="chkCity_CheckedChanged"
                                    Visible="false" />
                                <asp:TextBox Visible="false" Style="font-weight: bold" CssClass="form-control" ID="TextBox1"
                                    Width="100%" MaxLength="30" runat="server"></asp:TextBox>--%>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCity" ValueToCompare="Select City"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select City."></asp:CompareValidator>
                                <asp:DropDownList ID="ddlCity" class="form-control" Width="100%" runat="server" CssClass="chzn-select">
                                </asp:DropDownList>
                            </div>

                             <div class="form-group">
                                <label>
                                    Pincode</label>
                                <%--   <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="pincode" controltovalidate="txtpincode" Text="*"  errormessage="Please enter Pin Cod!" style="color:Red" />--%>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ValidationGroup="val1"
                                    Text="*" ControlToValidate="txtpincode" ValidationExpression="^[0-9]{6}$" ErrorMessage="Please enter a 6 digit pin code!"
                                    Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtpincode" MaxLength="6" runat="server"></asp:TextBox>
                            </div>
                           
                             <div class="form-group">
                                <label>
                                    E-mail</label>
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ValidationGroup="val1"
                                    Text="*" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ErrorMessage="Please enter a correct Email Id!" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtemail" placeholder="For Ex: test@gmail.com"
                                    runat="server"></asp:TextBox>
                            </div>
                               

                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <div class="col-lg-3">
                            
                             <div class="form-group">
                                <label>
                                    BankName</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="address" controltovalidate="txtaddress" Text="*"  errormessage="Please enter Address" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtbankname"  runat="server"></asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label>
                                    Bank Address</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="address" controltovalidate="txtaddress" Text="*"  errormessage="Please enter Address" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtbankAddress" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label>
                                    Account Number</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="address" controltovalidate="txtaddress" Text="*"  errormessage="Please enter Address" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtaccountnumber" runat="server"></asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label>
                                    IFSC Code</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="address" controltovalidate="txtaddress" Text="*"  errormessage="Please enter Address" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtIFSCCode"  runat="server"></asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label>
                                    SwiftCode</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="address" controltovalidate="txtaddress" Text="*"  errormessage="Please enter Address" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtswiftcode"  runat="server"></asp:TextBox>
                            </div>
                           
                        </div>
                        <div class="col-lg-3">
                            <%--<div class="form-group">
                            <label>
                                City</label>
                            
                            <asp:TextBox CssClass="form-control" ID="txtcity" MaxLength="30" runat="server"></asp:TextBox>
                        </div>--%>
                            <div class="form-group">
                                <label>
                                    GST Number</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                    ControlToValidate="txttin" Text="*" ErrorMessage="Please enter GST Number" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txttin" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    CST Number</label>
                                <asp:TextBox CssClass="form-control" ID="txtcst" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    PAN Number</label>
                                <asp:TextBox CssClass="form-control" ID="txtpan" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-grop">
                                    <label>
                                        Logo Upload:
                                    </label>
                                    <asp:Image ID="img_Photo" runat="server" class="img-fluid" Height="100px" Width="100px" />
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <label>
                                                Image Upload</label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                            <br />
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary"
                                                OnClick="btnUpload_Clickimg" Width="100px" />
                                        
                                            <asp:Label ID="lblFile_Path" runat="server" Visible="false"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnUpload" />
                                          
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                            <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                        </div>
                        <div class="col-lg-5">
                            <br />
                        </div>
                        <div class="col-lg-5">
                            <br />
                            <div style="text-align: center" align="center">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Save" OnClick="Add_Click"
                                    ValidationGroup="val1" Style="width: 120px" />
                                <asp:Button ID="btnUpdate" runat="server" class="btn btn-info" Text="Edit" Visible="false"
                                    Style="width: 120px" ValidationGroup="val1" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" Style="width: 120px"
                                    OnClick="Exit_Click" />
                            </div>
                        </div>
                          <script src="../js/jquery.min.js" type="text/javascript"></script>
        <script src="../js/chosen.min.js" type="text/javascript"></script>
        <script type="text/javascript">            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
                        </form>
                    </div>
                    <!-- /.col-lg-6 (nested) -->
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
    <!-- /.row -->
    <!-- /#page-wrapper -->
    <!-- jQuery -->
</body>
</html>
