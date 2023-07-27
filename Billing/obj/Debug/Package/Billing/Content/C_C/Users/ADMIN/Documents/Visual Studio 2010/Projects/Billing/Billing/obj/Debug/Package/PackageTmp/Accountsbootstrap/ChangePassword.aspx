<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Billing.Accountsbootstrap.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <title>Change Password</title>
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
   
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Change Login Password </h3>
                        
                    </div>
                    <div class="panel-heading"> 
                    <%--<h5 class="panel-title">Welcome</h5>--%>
                     <h5 class="panel-title"><asp:Label ID="lblusername" runat="server" style=""></asp:Label></h5>
                    </div>
                    <div class="panel-body">
                        <form id="Form2" action="" runat="server">
                            <fieldset>
                                <div class="form-group">
                                Enter Old Password
                                <asp:TextBox CssClass="form-control" ID="txtoldpw" runat="server"></asp:TextBox>
                                    
                                </div>
                                <div class="form-group">
                                Enter NewPassword
                                    <asp:TextBox CssClass="form-control" ID="txtnewpw" TextMode="Password" runat="server"></asp:TextBox>
                                </div>
                                 <div class="form-group">
                                Re Enter New Password
                                    <asp:TextBox CssClass="form-control" ID="txtrepw" TextMode="Password" runat="server"></asp:TextBox>
                                </div>
                              <div>
                              <asp:CompareValidator ID="cmppass" runat="server" ControlToValidate="txtrepw"  ControlToCompare="txtnewpw" Operator="Equal" ErrorMessage="Password Does Not Match"></asp:CompareValidator>
                              </div>
                                <!-- Change this to a button or input when using this as a form -->
                                <asp:Button class="btn btn-lg btn-success btn-block" ID="LoginButton" 
                                    runat="server"  Text="Submit" onclick="LoginButton_Click" />
                           
                                
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</body>
</html>
