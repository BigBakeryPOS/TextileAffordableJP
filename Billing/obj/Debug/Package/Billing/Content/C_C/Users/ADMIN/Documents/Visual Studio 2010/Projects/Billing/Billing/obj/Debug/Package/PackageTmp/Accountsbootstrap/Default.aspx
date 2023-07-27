<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
	<script src="../jqueryCalendar/script.js" type="text/javascript"></script>
	<script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
<script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
<link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css"/>
    <title></title>
    <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
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
    <form id="form1" runat = "server">
    <asp:Panel id="pnlContents" runat = "server">
        <asp:Image ID="imglogo" runat="server" ImageUrl="~/images/logo1.png" />
  <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
                    <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                   
                                        <div class="form-group">
                                            <label>Bill No</label>
											<asp:Label ID="lblbillno" runat="server"></asp:Label>
                                            
                                            
                                        </div>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <div class="form-group">
                                            <label>Bill Date</label>
                                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                                        </div>
                                        
                                       <div class="form-group">
                                            <asp:Label ID="lblcustname" runat="server"></asp:Label><br />
                                            <asp:Label ID="lbladd" runat="server"></asp:Label>
                                         <br /> <asp:Label ID="lblarea" runat="server"></asp:Label><br />
                                         <asp:Label ID="lblcity"  runat="server"></asp:Label><br />
                                         <asp:Label ID="lblpin" runat="server"></asp:Label>
                                                                                                                          
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
                                            
                                            <th>Category</th>
                                            <th>Description</th>
                                            <th>Qty</th>
                                            <th>Rate</th>
											<th>Amount</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="odd gradeX">
                                            <td><asp:Label ID="lblcat1" runat="server"></asp:Label>
                                           
                                             </td>
                                          <td><asp:Label ID="lbldesci" runat="server"></asp:Label>
                                           
                                             </td>
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtdef" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:Label ID="lblqty1" runat="server"></asp:Label>
                                           
                                             </td>
                                           
                                            <td class="center"><asp:Label ID="lblrate1" runat="server"></asp:Label>
                                                   </td>
                                                    
                                            <td class="center"><asp:Label ID="lblamt1" runat="server"></asp:Label></td>
											
                                        </tr>
                                    </tbody>
                                    <tbody>
                                        <tr class="odd gradeX">
                                            <td><asp:Label ID="lblcat2" runat="server"></asp:Label>
                                           
                                             </td>
                                         <td><asp:Label ID="lbldesc2" runat="server"></asp:Label>
                                           
                                             </td>
                                            <%--<td><asp:TextBox CssClass="form-control" ID="ddldef1" runat="server"></asp:TextBox></td>--%>
                                                                                       <td><asp:Label ID="lblqty2" runat="server"></asp:Label>
                                           
                                             </td>
                                         
                                            <td class="center"><asp:Label ID="lblrate2" runat="server"></asp:Label></td>
                                            <td class="center"><asp:Label ID="lblamt2" runat="server"></asp:Label></td>
											
                                        </tr>
                                    </tbody>
                                    <tbody>
                                        <tr class="odd gradeX">
                                           <td><asp:Label ID="lblcat3" runat="server"></asp:Label>
                                           
                                             </td><td><asp:Label ID="lbldesc3" runat="server"></asp:Label>
                                           
                                             </td>
                                                                                       <td><asp:Label ID="lblqty3" runat="server"></asp:Label>
                                           
                                             </td>
                                            <td class="center"><asp:Label ID="lblrate3" runat="server"></asp:Label></td>
                                            <td class="center"><asp:Label ID="lblamt3" runat="server"></asp:Label></td>
											
                                        </tr>
                                    </tbody>
                                    <tbody>
                                        <tr class="odd gradeX">
                                           <td><asp:Label ID="lblcat4" runat="server"></asp:Label>
                                           
                                             </td><td><asp:Label ID="lbldesc4" runat="server"></asp:Label>
                                           
                                             </td>
                                                                                       <td><asp:Label ID="lblqty4" runat="server"></asp:Label>
                                           
                                             </td>
                                            <td class="center"><asp:Label ID="lblrate4" runat="server"></asp:Label></td>
                                            <td class="center"><asp:Label ID="lblamt4" runat="server"></asp:Label>
                                            

                                            <label style="margin-top: 11px;" >Sub-Total :</label>
                                            <%--<input class="form-control" placeholder="" style="width: 110px;margin-left: 1143px;margin-top: -36px;">--%>
                <asp:Label ID="lblsub" runat="server"
                                                style="width: 110px;margin-left: 10px;margin-top: -29px;text-align:right" ></asp:Label><br />
                                            
                                            <label style="margin-top: 14px;">Dis % :</label>
                                   <asp:Label ID="lbldis" runat="server"     style="width: 110px;margin-left: 10px; margin-top:-36px; text-align:right" ></asp:Label><br />
                                        

                                        <label style="margin-top :18px;">TAX % :</label>
                                      <asp:Label ID="lbltax" runat="server"  style="width: 110px;margin-left: 10px; margin-top:-36px; text-align:right" ></asp:Label><br />
                                        

                                        <label style="margin-top :22px;">Grand Total :</label>
                                    <asp:Label ID="lblgranttotal" runat="server"   style="width: 110px;margin-left: 10px; margin-top:-36px; text-align:right"></asp:Label>
                                        
                                            
                                            </td>
											
                                        </tr>
                                    </tbody>
                                </table>
                                 <%--<asp:GridView ID="GVSales" runat="server"></asp:GridView>--%>
                                 <%--<div class="form-group">
                                 <asp:Button ID="Calc" runat="server" class="btn btn-success" Text="CALC" OnClick="Calc_Click" style="margin-left: 1018px;margin-top: -8px;"/>
                                  </div>--%>
                                 <%--<div class="form-group">
                                            <label style="margin-left: 1095px;">Tax</label>
                                            
                 <asp:TextBox CssClass="form-control" ID="txttax" runat="server" style="width: 110px;margin-left: 1143px;margin-top: -36px;"></asp:TextBox>
                                        </div>--%>
								
                                        
                                         
                                        
                            </div>
							<asp:Button ID="Button1"  runat="server" class="btn btn-success"   
                                Text="Print" OnClientClick = "return PrintPanel();"  />
                           
                                       
                            <!-- /.table-responsive -->
                                <!-- /.col-lg-6 (nested) -->
                            </div>
                            </form>
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
  
    <br />
   </asp:Panel>
    </form>
</body>
</html>
