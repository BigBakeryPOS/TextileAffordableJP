<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="itempage.aspx.cs" Inherits="Billing.Accountsbootstrap.itempage" %>
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

    <title>Item Page - bootsrap</title>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function valchk() {
            if (dropdownchk(document.getElementById('ddlcategory'), "Select Category")
            //&& dropdownchk(document.getElementById('ddlgroup'), "Account Group")  
        && blankchk(document.getElementById('txtdescription'), "Description")) {
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
                    <h1 class="page-header">Description Master</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
        <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <form runat="server" method="post">
                                        <div class="form-group">
                                            <label>Category</label>
											
                                           <asp:DropDownList runat="server" ID="ddlcategory" class="form-control">
                                           <asp:ListItem Text="--select Category--" Value="0"></asp:ListItem>
                                           
    <asp:ListItem Text="Sarees" Value="1" ></asp:ListItem>
    <asp:ListItem Text="Pavadai" Value="2" ></asp:ListItem>
    <asp:ListItem Text="Churidhar" Value="3" ></asp:ListItem>
    <asp:ListItem Text="Shirt and Pant" Value="4" ></asp:ListItem>
    <asp:ListItem Text="Shirt" Value="5" ></asp:ListItem>
    <asp:ListItem Text="Dhoti" Value="6" ></asp:ListItem>
    <asp:ListItem Text="Blouse" Value="7" ></asp:ListItem>

                                                </asp:DropDownList>
                                            <p class="help-block">Select Your Category</p>
                                        </div>
                                        <div class="form-group">
                                            <label>Description</label>
                                            <asp:TextBox CssClass="form-control" ID="txtdescription" runat="server" MaxLength="150" ></asp:TextBox>
                                         
                                        </div>



                                        <asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Save" 
                                            onclick="btnadd_Click"  />
                                        <asp:Button ID="btnexit" runat="server" class="btn  btn-danger" Text="Exit" onclick="btnexit_Click"
                                            />

                                        <%--<button type="submit" class="btn btn-success" onclick="Add_Click">Add</button>--%>
                                        
										
                                       
										
                                        
										
                                    
                                </div>
                      
                                </form>
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
        </div>
        <!-- /#page-wrapper -->
		
		
		

</body>

</html>

