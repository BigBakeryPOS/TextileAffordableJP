<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Billing.Accountsbootstrap.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="../assets/vendors/base/vendors.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assets/demo/demo4/base/style.bundle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
div.main{
    background: #ffffff; /* Old browsers */
background: -moz-radial-gradient(center, ellipse cover,  #ffffff 1%, #1c2b5a 100%); /* FF3.6+ */
background: -webkit-gradient(radial, center center, 0px, center center, 100%, color-stop(1%,#ffffff), color-stop(100%,#1c2b5a)); /* Chrome,Safari4+ */
background: -webkit-radial-gradient(center, ellipse cover,  #ffffff 1%,#1c2b5a 100%); /* Chrome10+,Safari5.1+ */
background: -o-radial-gradient(center, ellipse cover,  #ffffff 1%,#1c2b5a 100%); /* Opera 12+ */
background: -ms-radial-gradient(center, ellipse cover,  #ffffff 1%,#1c2b5a 100%); /* IE10+ */
background: radial-gradient(ellipse at center,  #ffffff 1%,#1c2b5a 100%); /* W3C */
filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#ffffff', endColorstr='#1c2b5a',GradientType=1 ); /* IE6-9 fallback on horizontal gradient */
height:calc(100vh);
width:100%;
}

[class*="fontawesome-"]:before {
  font-family: 'FontAwesome', sans-serif;
}

/* ---------- GENERAL ---------- */

* {
  box-sizing: border-box;
    margin:0px auto;

  &:before,
  &:after {
    box-sizing: border-box;
  }

}

body {
   
    color: #606468;
  font: 87.5%/1.5em 'Open Sans', sans-serif;
  margin: 0;
}

a {
	color: #eee;
	text-decoration: none;
}

a:hover {
	text-decoration: underline;
}

input {
	border: none;
	font-family: 'Open Sans', Arial, sans-serif;
	font-size: 14px;
	line-height: 1.5em;
	padding: 0;
	-webkit-appearance: none;
}

p {
	line-height: 1.5em;
}

.clearfix {
  *zoom: 1;

  &:before,
  &:after {
    content: ' ';
    display: table;
  }

  &:after {
    clear: both;
  }

}

.container {
  left: 50%;
  position: fixed;
  top: 50%;
  transform: translate(-50%, -50%);
}

/* ---------- LOGIN ---------- */

#login form{
	width: 250px;
}
#login, .logo{
    display:inline-block;
    width:40%;
}
#login{
border-right:1px solid #fff;
  padding: 0px 22px;
  width: 59%;
}
.logo{
color:#fff;
font-size:50px;
  line-height: 125px;
}

#login form span.fa {
	background-color: #fff;
	border-radius: 3px 0px 0px 3px;
	color: #000;
	display: block;
	float: left;
	height: 50px;
    font-size:24px;
	line-height: 50px;
	text-align: center;
	width: 50px;
}

#login form input {
	height: 50px;
}
fieldset{
    padding:0;
    border:0;
    margin: 0;

}
#login form input[type="text"], input[type="password"] {
	background-color: #fff;
	border-radius: 0px 3px 3px 0px;
	color: #000;
	margin-bottom: 1em;
	padding: 0 16px;
	width: 200px;
}

#login form input[type="submit"] {
  border-radius: 3px;
  -moz-border-radius: 3px;
  -webkit-border-radius: 3px;
  background-color: #000000;
  color: #eee;
  font-weight: bold;
  /* margin-bottom: 2em; */
  text-transform: uppercase;
  padding: 5px 10px;
  height: 30px;
}

#login form input[type="submit"]:hover {
	background-color: #d44179;
}

#login > p {
	text-align: center;
}

#login > p span {
	padding-left: 5px;
}
.middle {
  display: flex;
  width: 600px;
}
#login form span.fa {
	background-color: #fff;
	border-radius: 3px 0px 0px 3px;
	color: #000;
	display: block;
	float: left;
	height: 50px;
    font-size:24px;
	line-height: 50px;
	text-align: center;
	width: 50px;
}
 
.fa-user-o:before{content:"\f2c0"}
.fa-users:before{content:"\f0c0"}
.fa-user-md:before{content:"\f0f0"}
.fa-user-plus:before{content:"\f234"}


.fa-user:before
{
    content:"\f007";
    }
    
    .fa-lock:before
    {
        content:"\f023"}
        
       .topform{
  border-bottom:2px solid lightgrey;
  margin:0px;
  background-image: url('../images/user.png');
  background-repeat: no-repeat;
  background-color:white;
  background-size: 9%;
  background-position: 50% 50%;
}
.bottomform{
  margin-top:0px;
  margin-bottom:20px;
  background-image: url('../images/pass.png');
  background-repeat: no-repeat;
  background-color:white;
  background-size: 10%;
  background-position: 50% 50%;
}
   </style>
    <link href="../css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/newstyl.css" rel="stylesheet" type="text/css" />
    <script src="../js/index.js" type="text/javascript"></script>
    <script src="../js/JQry.js" type="text/javascript"></script>
</head>
<body>
    <div class="main">
        <div class="container">
            <center>
                <div class="middle">
                    <div id="login">
                        <form id="form1" runat="server">
                        <fieldset class="clearfix">
                            <p>
                                <span class="fa fa-user"></span>
                                <asp:TextBox ID="username" AutoComplete="Off" runat="server" Placeholder="Username"></asp:TextBox>
                            </p>
                            <p>
                                <span class="fa fa-lock"></span>
                                <asp:TextBox ID="password" AutoComplete="Off" runat="server" Placeholder="PassWord"
                                    TextMode="Password"></asp:TextBox>
                            </p>
                            <div>
                                <span style="width: 50%; text-align: right; display: inline-block;">
                                    <asp:Button ID="Button1" Text="Sign-In" runat="server" OnClick="LoginButton_Click" />
                                </span><span style="width: 48%; text-align: left; display: inline-block;"><a style="color: Black"
                                    class="small-text" href="#">Forgot password?</a></span>
                            </div>
                        </fieldset>
                        <div class="clearfix">
                        </div>
                        </form>
                        <div class="clearfix">
                        </div>
                    </div>
                    <!-- end login -->
                    <div class="logo" style="padding-top: 26px;">
                        <asp:Image ID="log" ImageUrl="~/images/ae_logo.png" Style="width: 11pc;" runat="server" />
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
            </center>
            <div class="row">
                <div class="col-lg-12">
                  <div class="row">
                    <div class="col-lg-6">
                        <div align="Left" style="padding-top: 4pc; padding-left: 187px;">
                            <label id="Label1" align="center" runat="server">
                                Powered By</label>
                            <br />
                            <div class="logo">
                                <asp:Image ID="Image1" ImageUrl="../images/blogo.png" Style="width: 25pc;" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div align="Left" style="padding-top: 4pc; padding-left: 86px;">
                            <label id="Label2" align="center" runat="server">
                                Customer Support:
                            </label>
                            <br />
                            <div>
                                <asp:Label ID="Label3" align="center" Text="+91 72009 28169" Font-Bold="true" Font-Size="25px" ForeColor="#00417d" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                         </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
