<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="Billing.HeaderMaster.Header" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>
<html lang="en">

<head>

    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <title>Category Master - bootsrap</title>
    <link href="../images/fav.ico" type="image/x-icon" rel="Shortcut Icon" />
    
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

    



 <div>
 
 <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
      <div class="container-fluid">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <img src="../images/logo11.png" alt="logo"/>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <ul class="nav navbar-nav navbar-right">
          <li><a href="../Accountsbootstrap/categorymaster.aspx">Category Master</a></li>
		  <li><a href="../Accountsbootstrap/Descriptiongrid.aspx">Description Master</a></li>
            <li><a href="../Accountsbootstrap/viewcustomer.aspx">Customer Master</a></li>
            <li><a href="../Accountsbootstrap/salesgrid.aspx">Sales</a></li>
            <li><a href="../Accountsbootstrap/ReceiptGrid.aspx">Receipt</a></li>
            <li class="dropdown">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
               Reports <b class="caret"></b>
            </a>
            <ul class="dropdown-menu">
               <li><a href="../Accountsbootstrap/CustomerReport.aspx">Customer Report</a></li>
               <li><a href="../Accountsbootstrap/SalesReport.aspx">Sales Report</a></li>
               <li><a href="../Accountsbootstrap/ReceiptReport.aspx">Receipt Report</a></li>
               <li><a href="../Accountsbootstrap/OutstandingPayment.aspx">Outstanding Payment</a></li>
               
            </ul>
         </li>
         <!--   <li><a href="../Accountsbootstrap/salesregister.aspx">Report</a></li>-->
             <li><a href="../Accountsbootstrap/Pettygrid.aspx">Payment Entry</a></li>
                <li><a href="../Accountsbootstrap/Stock.aspx">Stock</a></li>
            <li><a href="../Accountsbootstrap/changepassword.aspx">Change Password</a></li>
			<li><a href="../Accountsbootstrap/login.aspx">Log Out</a></li>
                    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label" Visible="true"> </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
                   
          </ul>
          
        </div>
      </div>
    </nav>
            
           
        </div>
        <!-- /#page-wrapper -->
		
		
		
		<!-- jQuery -->
    <script type="text/javascript" src="../js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script type="text/javascript"  src="../js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script type="text/javascript" src="../js/plugins/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script type="text/javascript" src="../js/sb-admin-2.js"></script>


</body>

</html>
