<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customerupdate.aspx.cs" Inherits="Billing.Accountsbootstrap.categoryupdate" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>
<html lang="en">

<head>

    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Account Page - bootsrap</title>
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
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head> 
<body>
<usc:Header ID="Header" runat="server" />
    



 
          <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
        
 
 
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Customer Master</h1>
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
                                <div class="col-lg-6">
                                    
                                         <div class="form-group">
                                            <label>Customer Code</label>
											<asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server" Enabled="false"></asp:TextBox>
                                            
                                            <p class="help-block">Enter Your Correct Code</p>
                                        </div>
                                        <div class="form-group">
                                            <label>Customer Name</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcustomername" MaxLength="50" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="reqName" controltovalidate="txtcustomername" errormessage="Please enter your name!" style="color:Red" />
                                        </div>
                                        <label>Mobile No</label>
                                        <div class="form-group input-group">

                                            <span class="input-group-addon">+91</span>
                                            <asp:TextBox CssClass="form-control" ID="txtmobileno" MaxLength="10" runat="server"></asp:TextBox>
                                           
                                        </div>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="mobno" controltovalidate="txtmobileno" errormessage="Please enter your Mobile No!" style="color:Red" />
                                       <asp:RegularExpressionValidator runat="server" id="rexNumber" controltovalidate="txtmobileno" validationexpression="^[0-9]{10}$" errormessage="Please enter a 10 digit number!" style="color:Red"/>
										<%--<div class="form-group">
                                            <label>Mobile No</label>
                                            
                                            <asp:TextBox CssClass="form-control" ID="txtmobileno" runat="server"></asp:TextBox>
                                        </div>--%>
                                        <div class="form-group">
                                            <label>Phone No</label>
                                            <asp:TextBox CssClass="form-control" ID="txtphoneno" MaxLength="15" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="phono" controltovalidate="txtphoneno" errormessage="Please enter your Phone No!" style="color:Red" />
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
                                        
                                        
										
                                    
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                                <div class="col-lg-6">
                                    
										<div class="form-group">
                                            <label>Address</label>
                                            <asp:TextBox CssClass="form-control" ID="txtaddress" MaxLength="150" runat="server"></asp:TextBox>
											<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="address" controltovalidate="txtaddress" errormessage="Please enter your Address!" style="color:Red" />
                                        </div>
                                        <div class="form-group">
                                            <label>Area</label>
                                            <asp:TextBox CssClass="form-control" ID="txtarea" MaxLength="30" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="area" controltovalidate="txtarea" errormessage="Please enter your Area!" style="color:Red" />
                                        </div>
										<div class="form-group">
                                            <label>City</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcity" MaxLength="30" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="city" controltovalidate="txtcity" errormessage="Please enter your City!" style="color:Red" />
                                        </div>
										<div class="form-group">
                                            <label>Pincode</label>
                                            <asp:TextBox CssClass="form-control" ID="txtpincode" MaxLength="6" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator1" controltovalidate="txtpincode" validationexpression="^[0-9]{6}$" errormessage="Please enter a 6 digit pin code!" style="color:Red"/>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="pincode" controltovalidate="txtpincode" errormessage="Please enter your Pin Code!" style="color:Red" />
                                        </div>
										<div class="form-group">
                                            <label>E-mail</label>
                                            <asp:TextBox CssClass="form-control" ID="txtemail" placeholder="For Ex: test@gmail.com" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="email" controltovalidate="txtemail" errormessage="Please enter your Email!" style="color:Red" />
                                        <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator2" ValidationGroup="val1" controltovalidate="txtemail"  validationexpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" errormessage="Please enter a correct Email Id!" style="color:Red"/>
                                        </div>
                                         <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>
										
                                        <asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />
										<asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click" />
                                        
										
                                </div>
                                </form>
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
