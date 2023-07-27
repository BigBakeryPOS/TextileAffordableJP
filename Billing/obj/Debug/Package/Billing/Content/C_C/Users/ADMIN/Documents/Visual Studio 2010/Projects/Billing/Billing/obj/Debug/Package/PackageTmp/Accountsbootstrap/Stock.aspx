<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="Billing.Accountsbootstrap.Stock" %>
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

    <title>Stock Master</title>
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
                    <h1 class="page-header">Stock Master</h1>
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
                                            <label>Category</label>
											<asp:DropDownList ID="ddlcategory" AutoPostBack="true" runat="server"  CssClass="form-control"
                                                onselectedindexchanged="ddlcategory_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Sub Category</label>
                                           <asp:DropDownList ID="ddlSubCategory" runat="server" onselectedindexchanged="ddlSubCategory_SelectedIndexChanged" 
                                               ></asp:DropDownList>
                                        </div>
                                        <label>Quantity</label>
                                        <div class="form-group input-group">
                                           <asp:TextBox CssClass="form-control" ID="txtQty" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Unit Price</label>
                                            <asp:TextBox CssClass="form-control" ID="txtUnitPrice" runat="server"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>
                                   <asp:Button ID="btnAdd" runat="server" class="btn btn-success" Text="Add" 
                                            onclick="btnAdd_Click"  />
                                        <asp:Button ID="btnEdit" runat="server" class="btn btn-warning" Text="Edit/Delete"  />
										
                                    
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                         
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

