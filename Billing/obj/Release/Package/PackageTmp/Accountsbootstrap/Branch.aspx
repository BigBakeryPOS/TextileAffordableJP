<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Branch.aspx.cs" Inherits="Billing.Accountsbootstrap.Branch" %>
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

    <title>Branch Registration</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>

    <script type="text/javascript" language="javascript">
        function valchk() {
            if (blankchk(document.getElementById('txtBrandname'), "Branch Name")
            {
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
        
 
 
            <div class="row" >
                <div class="col-lg-12">
                    <h1 class="page-header"  style="text-align:center;color:#fe0002;" >Branch Master</h1>
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
                             <div class="col-lg-4"></div>
                                <div class="col-lg-3">
                                    <asp:ValidationSummary runat="server" HeaderText="Validation Messages"  ValidationGroup="val1" ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                        <div class="form-group" id="divcode" runat="server"  >
                                         <asp:TextBox  CssClass="form-control" ID="txtBranchid" runat="server" Enabled="false"></asp:TextBox>                                              
                                            
                                        </div>
                                        <div class="form-group " >
                                            <label>Branch Code</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" id="RequiredFieldValidator1" controltovalidate="txtBranchCode" errormessage="Please enter Brand name!" style="color:Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtBranchCode"  runat="server" ></asp:TextBox>
                                            
                                        </div>
                                        <div class="form-group " >
                                            <label>Branch Name</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" id="reqName" controltovalidate="txtBranchname" errormessage="Please enter Brand name!" style="color:Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtBranchname"  runat="server" ></asp:TextBox>
                                            
                                        </div>

                                         <div class="form-group " >
                                            <label>Branch Address</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" id="RequiredFieldValidator2" controltovalidate="txtBranchaddress" errormessage="Please enter Brand name!" style="color:Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtBranchaddress"  runat="server" ></asp:TextBox>
                                            
                                        </div>
                                         <div class="form-group " >
                                            <label>Phone number</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" id="RequiredFieldValidator3" controltovalidate="txtphonenumber" errormessage="Please enter Brand name!" style="color:Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtphonenumber"  runat="server" ></asp:TextBox>
                                            
                                        </div>

                                        <div class="form-group" >
                                            <label >Is Active</label>
                                           <asp:DropDownList CssClass="form-control" ID="ddlIsActive" runat="server" > 
                                      <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                      <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                    
                                      </asp:DropDownList>
                                                                                 </div>
                                        
                                        <%--<div class="form-group input-group">

                                            <label>Heading</label>
                                            <asp:DropDownList ID="ddlHeadingType" runat="server" class="form-control" 
                                                ></asp:DropDownList>
                                           
                                        </div>--%>
                                     
                                    
                                         <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>
										<asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" OnClick="Add_Click" ValidationGroup="val1"  style="width:117px;"/>
                                        <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"  style="width:120px;"/>
                                    
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                                <div class="col-lg-4">
                                    
										
                                        <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
										<%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                                        
										
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
