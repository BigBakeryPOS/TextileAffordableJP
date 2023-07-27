<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="categorydelete.aspx.cs" Inherits="Billing.Accountsbootstrap.categorydelete" %>

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
    <title>Category Master - bootsrap</title>

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
                    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <usc:Header ID="Header" runat="server" />
    <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Category Master</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>


          <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <form runat="server" id="form1" method="post">
                                       
                               <div class="form-group">
                                            
                                            <asp:TextBox CssClass="form-control" ID="txtcategoryId" runat="server" Visible="false"></asp:TextBox>
                                           
                                        </div>	        
		  
         

                                            <div class="form-group">
                                            <label>Category</label>
                                            <asp:ListBox  style="height:100px" runat="server" DataValueField="CategoryID" 
                                                    ID="listcategory" CssClass="form-control" AutoPostBack="true" onselectedindexchanged="listcategory_SelectedIndexChanged" ></asp:ListBox>
                                            <%--<onselectedindexchanged="listcategory_SelectedIndexChanged" asp:DropDownList ID="ddlcategory" CssClass="form-control"  runat="server"></asp:DropDownList>--%>
                                            <asp:TextBox CssClass="form-control" ID="txtcategory" runat="server" placeholder="Select Category to Delete"></asp:TextBox>
                                           
                                        </div>	
                                            <%--<div class="form-group">
                                            <label>Category Description</label>
                                            
                                            <asp:TextBox CssClass="form-control" ID="txtdescription" runat="server"></asp:TextBox>
                                           
                                        </div>	--%>
                                        
                                        									
                                        <asp:Button ID="btndelete" runat="server" class="btn btn-danger" Text="Delete" OnClick="Delete_Click" />
										

         
        
                                        
                                        
										
                                    </form>
                                </div>
                                
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


</body>

</html>
