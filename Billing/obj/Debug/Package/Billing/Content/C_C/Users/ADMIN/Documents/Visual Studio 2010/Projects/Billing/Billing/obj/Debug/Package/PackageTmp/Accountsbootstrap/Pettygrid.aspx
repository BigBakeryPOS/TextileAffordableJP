<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pettygrid.aspx.cs" Inherits="Billing.Accountsbootstrap.Pettygrid" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    

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

                    <form id="Form1" runat="server">
                    <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Petty Cash</h1>
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
                                            
                                            <asp:TextBox CssClass="form-control" ID="txtdescription" runat="server" MaxLength="50" style="width: 200px;margin-left: 170px;margin-top: -49px;" ></asp:TextBox>
                                         <asp:Button ID="Button1" runat="server" class="btn btn-success" Text="Search" style="margin-top:-34px; margin-left:397px"
                                                onclick="Button1_Click"    />
                                                
                                        </div>
                                        
										<div class="table-responsive">
                                        
                                <table class="table table-bordered table-striped">
                                <tr>
                                <td colspan="4" align="left">
                                   <asp:GridView ID="gvpetty" runat="server" AutoGenerateColumns="false">
                                   <Columns>
                                    <asp:BoundField HeaderText="Recipt No" DataField="CashId" />
                                    <asp:BoundField HeaderText="Amount Recived By" DataField="Received_By" />                               
                                    <asp:BoundField HeaderText="Amount Recived From" DataField="Amount_rec_From" />
                                    <asp:BoundField HeaderText="Date" DataField="Date" />
                                    <asp:BoundField HeaderText="Amount" DataField="Amount" />                                         
                                    <asp:BoundField HeaderText="Description" DataField="Description" />
                                  
                                    <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>    
                                   <asp:LinkButton ID="btndel"   CommandArgument='<%#Eval("CashId") %>' CommandName="Del" runat="server"> <asp:Image ID="Image1"  ImageUrl="~/images/delete.png" runat="server" /></asp:LinkButton>
                                   <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
                                     </ItemTemplate>
    
     
     
                                   </asp:TemplateField> 
                                   </Columns>
                                   </asp:GridView>
                                   
                                
                                   </td>
                                   </tr>
                                
                                   </table>
                                   <asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Add" onclick="btnadd_Click"/>
                                   </div>
										
									</div>
                                    </div></div></div></div>	
                                    </div>
                                 
                               
                                     </form>
                                
                                <!-- /.col-lg-6 (nested) -->
                                
                
		
		

</body>

</html>
