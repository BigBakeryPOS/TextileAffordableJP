<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receiptpage.aspx.cs" Inherits="Billing.Accountsbootstrap.receipt" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html lang="en">

<head runat="server">

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
<usc:Header ID="Header" runat="server" />
    



 

          <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
        <form id="Form1" runat="server" role="form">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Receipt page</h1>
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
                                            <label>Receipt Number</label>
											<asp:TextBox CssClass="form-control" Enabled="false" ID="txtreceiptno" runat="server"></asp:TextBox>
                                            
                                            
                                        </div>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <div class="form-group">
                                            <label>Receipt Date</label>
                                            <asp:TextBox CssClass="form-control" ID="txtreceiptdate" runat="server" Text="--Select Date--"></asp:TextBox>
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtreceiptdate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                        <div class="form-group">
                                            <label>Customer Name</label>
                                        <asp:DropDownList runat="server" ID="ddlcustomerID" class="form-control"  AutoPostBack="true"
                                                onselectedindexchanged="ddlcustomerID_SelectedIndexChanged"  >
                                           
                                        </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label></label>
                                            <asp:TextBox CssClass="form-control" ID="txtcustomername" runat="server" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Address</label>
                                            <asp:TextBox CssClass="form-control" ID="txtaddress" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Area</label>
                                            <asp:TextBox CssClass="form-control" ID="txtarea" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
										
										<div class="form-group">
                                            <label>Customer City</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcity" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Pincode</label>
                                            <asp:TextBox CssClass="form-control" ID="txtpincode" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
										<div class="form-group">
                                            <label></label>
                                            <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server" Visible="false"></asp:TextBox>
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
                                         <td> <asp:DropDownList CssClass="form-control" ID="ddmodeofpayment" runat="server" 
                                                    AutoPostBack="true" 
                                                    onselectedindexchanged="ddmodeofpayment_SelectedIndexChanged"></asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtbankname" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtrefno" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox></td>
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
                                        <asp:DropDownList CssClass="form-control" ID="ddbillno1" runat="server"  AutoPostBack="true"
                                                onselectedindexchanged="ddbillno1_SelectedIndexChanged"></asp:DropDownList>
                                        
                                        </td>
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtbillno1" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:TextBox CssClass="form-control" ID="txtbilldate1" runat="server"></asp:TextBox></td>
                                            
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbillamount1" runat="server"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbalance1" 
                                                    runat="server" ontextchanged="txtbalance1_TextChanged" AutoPostBack="true"></asp:TextBox></td>
											<td class="center"><asp:TextBox CssClass="form-control" ID="txtamount1" runat="server"></asp:TextBox></td>
                                            
                                        </tr>
                                        <tr class="odd gradeX">
                                       <td> <asp:DropDownList CssClass="form-control" ID="ddbillno2" runat="server"  
                                               AutoPostBack="true" onselectedindexchanged="ddbillno2_SelectedIndexChanged"
                                                ></asp:DropDownList></td>
                                        
                                       
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtbillno1" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:TextBox CssClass="form-control" ID="txtbilldate2" runat="server"></asp:TextBox></td>
                                            
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbillamount2" runat="server"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbalance2"  
                                                    runat="server" AutoPostBack="true" ontextchanged="txtbalance2_TextChanged"></asp:TextBox></td>
											<td class="center"><asp:TextBox CssClass="form-control" ID="txtamount2" runat="server"></asp:TextBox></td>
                                        </tr>

                                         <tr class="odd gradeX">
                                       <td> <asp:DropDownList CssClass="form-control" ID="ddbillno3" runat="server"  
                                               AutoPostBack="true" onselectedindexchanged="ddbillno3_SelectedIndexChanged"
                                                ></asp:DropDownList></td>
                                        
                                       
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtbillno1" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:TextBox CssClass="form-control" ID="txtbilldate3" runat="server"></asp:TextBox></td>
                                            
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbillamount3" runat="server"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbalance3"  
                                                    runat="server" AutoPostBack="true" ontextchanged="txtbalance3_TextChanged"></asp:TextBox></td>
											<td class="center"><asp:TextBox CssClass="form-control" ID="txtamount3" runat="server"></asp:TextBox></td>
                                        </tr>

                                         <tr class="odd gradeX">
                                       <td> <asp:DropDownList CssClass="form-control" ID="ddbillno4" runat="server"  
                                               AutoPostBack="true" onselectedindexchanged="ddbillno4_SelectedIndexChanged"
                                                ></asp:DropDownList></td>
                                        
                                       
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtbillno1" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:TextBox CssClass="form-control" ID="txtbilldate4" runat="server"></asp:TextBox></td>
                                            
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbillamount4" runat="server"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbalance4"  
                                                    runat="server" AutoPostBack="true" ontextchanged="txtbalance4_TextChanged"></asp:TextBox></td>
											<td class="center"><asp:TextBox CssClass="form-control" ID="txtamount4" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </tbody>
                                </table>
								
                            </div>
							<asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Save" OnClick="Add_Click" />
                            <asp:LinkButton ID="btnop" runat="server" 
                                Text="click here to get oustranding payemt record" onclick="btnop_Click"></asp:LinkButton>
                                        
										
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

