<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login1.aspx.cs" Inherits="Billing.Accountsbootstrap.Login1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>Slide Login Form Flat Responsive Widget Template :: w3layouts</title>

<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta name="keywords" content="Slide Login Form template Responsive, Login form web template, Flat Pricing tables, Flat Drop downs Sign up Web Templates, Flat Web Templates, Login sign up Responsive web template, SmartPhone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyEricsson, Motorola web design" />

	 <script>
	     addEventListener("load", function () {
	         setTimeout(hideURLbar, 0);
	     }, false);

	     function hideURLbar() {
	         window.scrollTo(0, 1);
	     }
    </script>

	<!-- Custom Theme files -->
	<link href="../css1/style.css" rel="stylesheet" type="text/css" media="all" />
	<link href="../css1/font-awesome.min.css" rel="stylesheet" type="text/css" media="all" />
	<!-- //Custom Theme files -->

	<!-- web font -->
	<link href="//fonts.googleapis.com/css?family=Hind:300,400,500,600,700" rel="stylesheet">
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
	<!-- //web font -->

</head>
<body>

<!-- main -->
<div class="w3layouts-main"> 
	<div class="bg-layer">
		<h1>Login form</h1>
		<div class="header-main">
			<div class="main-icon">
				<span><asp:Image ID="log" ImageUrl="../images/ae_logo.png" style="width: 11pc;" runat="server"  /></span>
			</div>
             <form id="form1" runat="server">
			<div class="header-left-bottom">
				<form action="#" method="post">
					<div class="icon1">
						<span class="fa fa-user"></span>
                        <asp:TextBox ID="txtemail" runat="server"   placeholder="User Name" ></asp:TextBox>
					</div>
					<div class="icon1">
						<span class="fa fa-lock"></span>
						<%--<input type="password" placeholder="Password" required=""/>--%>
                        <asp:TextBox ID="txtpassowrd" TextMode="Password" runat="server" placeholder="Password"  ></asp:TextBox>
					</div>
					<div runat="server" visible="false" class="login-check">
						 <label class="checkbox"><input type="checkbox" name="checkbox" checked=""><i> </i> Keep me logged in</label>
					</div>
					<div class="bottom">
						<button class="btn">Log In</button>
					</div>
					<div class="links">
						<p><a href="#">Forgot Password?</a></p>
						<%--<p class="right"><a href="#">New User? Register</a></p>--%>
						<div class="clear"></div>
					</div>
				</form>	
			</div>
            </form>
			<div runat="server" visible="false" class="social">
				<ul>
					<li>or login using : </li>
					<li><a href="#" class="facebook"><span class="fa fa-facebook"></span></a></li>
					<li><a href="#" class="twitter"><span class="fa fa-twitter"></span></a></li>
					<li><a href="#" class="google"><span class="fa fa-google-plus"></span></a></li>
				</ul>
			</div>
		</div>
		
		<!-- copyright -->
		 <div align="center" style="padding-top:4pc">
                           <label id="Label1"  align="center" runat="server"> Powered By</label> <br />
                            <div class="logo" ><asp:Image ID="Image1" ImageUrl="../images/blogo.png" style="width: 25pc;" runat="server"  />
                            </div>
                            </div>
		<!-- //copyright --> 
	</div>
</div>	
<!-- //main -->

</body>
</html>
