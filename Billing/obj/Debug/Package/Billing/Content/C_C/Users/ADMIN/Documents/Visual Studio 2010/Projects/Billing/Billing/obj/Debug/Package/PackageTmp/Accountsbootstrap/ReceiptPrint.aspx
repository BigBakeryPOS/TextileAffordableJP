<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptPrint.aspx.cs" Inherits="Billing.Accountsbootstrap.ReceiptPrint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <title>Receipt Page - bootsrap</title>

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

    <asp:Image ID="imglogo" runat="server" ImageUrl="~/images/logo1.png" />



 

       
        <form id="Form1" runat="server" role="form">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Receipt</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    
                                        <div class="form-group">
                                            <label>Receipt Number:-</label><asp:Label ID="lblreceiptno" runat="server"></asp:Label>
											
                                            
                                            
                                        </div>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <div class="form-group">
                                            <label>Receipt Date:-</label><asp:Label ID="lblreceiptdate" runat="server"></asp:Label>
                                           <asp:label id="lblcustomercode" runat="server" visible="false"></asp:label>
                                        </div>
                                   
                                        <div class="form-group">
                                      <asp:Label ID="lblcustomername" runat="server"></asp:Label><br />
                                      <asp:Label ID="lbladdress" runat="server"></asp:Label><br />
                                      <asp:Label ID="lblarea" runat="server"></asp:Label><br />
                                      <asp:Label ID="lblcity" runat="server"></asp:Label><br />
                                      <asp:Label ID="lblpincode" runat="server"></asp:Label><br />



                                        </div>
                                                                              
                                        									
										
										
                                        <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" id="Table1">
                                        <thead>
                                        <tr>
                                        <th>Payment Mode</th>
                                            <th>Bank Name</th>
                                            <th> Reference No</th>
                                        </tr></thead>
                                         <tbody>
                                         <tr class="odd gradeX">
                                         <td> <asp:Label ID="lblpaymentmode" runat="server"></asp:Label></td>
                                            <td><asp:Label ID="lblbankname" runat="server"></asp:Label></td>
                                            <td><asp:Label ID="lblrefno" runat="server"></asp:Label></td>
                                         </tr>
                                         </tbody>
                                         </table>
                                        
                                        </div>
                                        
                                        
										
                                    
                                </div>
                                <div>
            <div class="row">
                
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>Bill No</th>
                                            <th>Bill Date</th>
                                            
                                            <th>Bill Amount</th>
                                            <th>Balance</th>
											<th>Amount</th>
                                           
                                        </tr>
                                    </thead>
                                    <tbody>
                                    
                                        <tr class="odd gradeX">
                                        <td>
                                       <asp:Label ID="lblbillno" runat="server"></asp:Label>
                                        </td>
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtbillno1" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:Label ID="lblbilldate" runat="server"></asp:Label></td>
                                            
                                            <td class="center"><asp:Label ID="lblbillamount" runat="server"></asp:Label></td>
                                            <td class="center"><asp:Label ID="lblbalance" runat="server"></asp:Label>
                                                    </td>
											<td class="center"><asp:Label ID="lblamount" runat="server"></asp:Label></td>
                                            
                                        </tr>
                                         <tr class="odd gradeX">
                                        <td>
                                       <asp:Label ID="lblbillno1" runat="server"></asp:Label>
                                        </td>
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtbillno1" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:Label ID="lblbilldate1" runat="server"></asp:Label></td>
                                            
                                            <td class="center"><asp:Label ID="lblbillamount1" runat="server"></asp:Label></td>
                                            <td class="center"><asp:Label ID="lblbalance1" runat="server"></asp:Label>
                                                    </td>
											<td class="center"><asp:Label ID="lblamount1" runat="server"></asp:Label></td>
                                            
                                        </tr>
                                         <tr class="odd gradeX">
                                        <td>
                                       <asp:Label ID="lblbillno2" runat="server"></asp:Label>
                                        </td>
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtbillno1" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:Label ID="lblbilldate2" runat="server"></asp:Label></td>
                                            
                                            <td class="center"><asp:Label ID="lblbillamount2" runat="server"></asp:Label></td>
                                            <td class="center"><asp:Label ID="lblbalance2" runat="server"></asp:Label>
                                                    </td>
											<td class="center"><asp:Label ID="lblamount2" runat="server"></asp:Label></td>
                                            
                                        </tr>
                                         <tr class="odd gradeX">
                                        <td>
                                       <asp:Label ID="lblbillno3" runat="server"></asp:Label>
                                        </td>
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtbillno1" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:Label ID="lblbilldate3" runat="server"></asp:Label></td>
                                            
                                            <td class="center"><asp:Label ID="lblbillamount3" runat="server"></asp:Label></td>
                                            <td class="center"><asp:Label ID="lblbalance3" runat="server"></asp:Label>
                                                    </td>
											<td class="center"><asp:Label ID="lblamount3" runat="server"></asp:Label></td>
                                            
                                        </tr>
                                       

                                       

                                      
                                    </tbody>
                                </table>
								
                            </div>
                            <label>Amounts in Words:-</label>
                            <label id="lblamountinwords" runat="server"></label>.ONLY<br />
							<asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Print"/>
                             <script language="javascript" type="text/javascript">
                                 window.print();
</script>
                         
                                        
										
                            <!-- /.table-responsive -->
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
		</div>
        </div>
        </div>
        </div>
      
		
		
		<!-- jQuery -->
   </ContentTemplate>
</asp:UpdatePanel>
</form>
</body>

</html>
