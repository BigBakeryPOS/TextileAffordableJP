<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCustomer.aspx.cs" Inherits="Billing.Accountsbootstrap.ViewCustomer" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">

<head>
<style type="text/css">
		a img{border: none;}
		ol li{list-style: decimal outside;}
		div#container{width: 780px;margin: 0 auto;padding: 1em 0;}
		div.side-by-side{width: 100%;margin-bottom: 1em;}
		div.side-by-side > div{float: left;width: 50%;}
		div.side-by-side > div > em{margin-bottom: 10px;display: block;}
		.clearfix:after{content: "\0020";display: block;height: 0;clear: both;overflow: hidden;visibility: hidden;}
		
	</style>
	
    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <title>Customer Grid Master - bootsrap</title>
    
   <!-- Bootstrap Core CSS -->
   <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
     <link href="../Styles/style1.css" rel="stylesheet"/>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        function alertMessage() {
            alert('Are You Sure, You want to delete This Customer!');
        }
    </script>
</head> 
<body>

             <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
   <usc:Header ID="Header" runat="server" />
   <form runat="server" id="form1">
    <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Customer Master</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>


          <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    
                                    <div class="form-group">
                                            <div id="container">
			                                <h2>Selected Value :
				                                <asp:Label runat="server" ID="lblSelectedValue"></asp:Label></h2>
			                                    <div class="side-by-side clearfix">

				                            <div>
                                            <asp:DropDownList class="chzn-select" ID="ddlfilter" style="width:150px;" runat="server">
                                            <asp:ListItem Text="Name" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="MobileNo" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                                                </asp:DropDownList>

                                                </div>
                                                </div>
                                                </div>
                                                </div>
                                                <div class="form-group">
                                                <asp:TextBox CssClass="form-control"  Enabled="true" ID="txtsearch" runat="server" style="width:200px;margin-top: -70px;margin-left: 167px;" ></asp:TextBox>
                                        <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" OnClick="Search_Click" style="margin-top: -34px;margin-left: 380px;"  /> 
                                        <asp:Button ID="btnrefresh" runat="server" class="btn btn-warning" Text="Reset" OnClick="refresh_Click" style="margin-top: -34px;margin-left: 480px;"  /> 
                                        </div> 
                               <div class="table-responsive">
                                        
                                <table class="table table-bordered table-striped">
                                <tr>
                                <td>
                                <asp:GridView ID="gvcust"   runat="server" CssClass="myGridStyle" AllowPaging="true" PageSize="5"  OnPageIndexChanging="Page_Change" AutoGenerateColumns="false" onrowcommand="gvcust_RowCommand">
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" PreviousPageText="Previous" />
    <Columns>
    <%--<asp:BoundField HeaderText="Customer ID" DataField="CustomerID" />--%>
    <asp:BoundField HeaderText="Customer Name" DataField="CustomerName" />
    <asp:BoundField HeaderText="Mobile No" DataField="MobileNo" />
    <asp:BoundField HeaderText="Area" DataField="Area" />
    <asp:BoundField HeaderText="Email" DataField="Email" />
     <asp:TemplateField HeaderText="Edit">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("CustomerID") %>' CommandName="edit"><asp:Image ID="img" runat="server" ImageUrl="~/images/edit.png" /></asp:LinkButton>
      
     </ItemTemplate>
    
     
     
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Delete">
     <ItemTemplate>
           <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("CustomerID") %>' CommandName="delete" OnClientClick="alertMessage()"><asp:Image ID="dlt" runat="server" ImageAlign="Middle" ImageUrl="~/images/delete.png" /></asp:LinkButton>
   </ItemTemplate>
    
     
     
     </asp:TemplateField>
   </Columns>
   </asp:GridView>
                                </td>
                                </tr>
                                </table>
                                </div>
                                     <asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Add" OnClick="Add_Click" />  
                                        
										

         
        
                                        
                                        
										
                                    
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
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</form>
</body>

</html>
