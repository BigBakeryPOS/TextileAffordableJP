<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesUpdate.aspx.cs" Inherits="Billing.Accountsbootstrap.SalesUpdate" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html lang="en">

<head id="Head1" runat="server">

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
<script type="text/javascript">
    jQuery(function () {
        jQuery("#inf_custom_someDateField").datepicker();
    });
                </script>
	
	
    <title>CashSales Page - bootsrap</title>

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
                    <h1 class="page-header">Sales</h1>
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
                                    <form id="Form1" runat="server" role="form">
                                        <div class="form-group">
                                            <label>Bill No</label>
											<asp:TextBox CssClass="form-control" ID="txtbillno" Enabled="false" runat="server"></asp:TextBox>
                                            
                                            
                                        </div>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <div class="form-group">
                                            <label>Bill Date</label>
                                            <asp:TextBox CssClass="form-control" ID="txtdate" runat="server" Text="-----Slect Date-----"></asp:TextBox>
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                       <%--<div class="form-group">
                                            <label>Customer Name</label>
                                        <asp:DropDownList runat="server" ID="ddlcustomerID" class="form-control"  AutoPostBack="true"
                                                onselectedindexchanged="ddlcustomerID_SelectedIndexChanged"  >
                                           
                                        </asp:DropDownList>
                                        </div>--%>
										<div class="form-group">
                                            <label></label>
                                            <asp:TextBox CssClass="form-control" ID="txtcustomername" runat="server" Enabled="false" ></asp:TextBox>
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
                                            <label>City</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcity" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Pincode</label>
                                            <asp:TextBox CssClass="form-control" ID="txtpincode" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
										<div class="form-group">
                                            <label></label>
                                            <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server" Visible="false"></asp:TextBox>
                                            <%--<asp:TextBox CssClass="form-control" ID="txtsalesID" runat="server" Visible="false"></asp:TextBox>--%>
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
                                            <td><asp:DropDownList runat="server" ID="ddlcategory" class="form-control" 
                                                    AutoPostBack="true" >
                                           
                                             </asp:DropDownList></td>
                                             <td><asp:DropDownList runat="server" ID="ddldef" CssClass="form-control" ></asp:DropDownList></td>
                                            <%--<td><asp:TextBox CssClass="form-control" ID="txtdef" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:TextBox CssClass="form-control" ID="txtqty" MaxLength="10" style="text-align:right" runat="server"></asp:TextBox></td>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtqty" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtrate" runat="server" 
                                                    ontextchanged="txtrate_TextChanged1"  AutoPostBack="True" style="text-align:right"></asp:TextBox></td>
                                                    
                                            <td class="center"><asp:TextBox CssClass="form-control" style="text-align:right" 
                                                    ID="txtamount" runat="server" AutoPostBack="True" ReadOnly="true"></asp:TextBox ></td>
											
                                        </tr>
                                    </tbody>
                                    <tbody>
                                        <tr class="odd gradeX">
                                            <td><asp:DropDownList runat="server" ID="ddlcategory1" class="form-control" AutoPostBack="true" >
                                                                                       
                                            </asp:DropDownList></td>
                                            <td><asp:DropDownList runat="server" ID="ddldef1" CssClass="form-control" ></asp:DropDownList></td>
                                            <%--<td><asp:TextBox CssClass="form-control" ID="ddldef1" runat="server"></asp:TextBox></td>--%>
                                            <td><asp:TextBox CssClass="form-control" ID="txtqty1" MaxLength="10" style="text-align:right" runat="server"></asp:TextBox></td>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtqty1" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtrate1" style="text-align:right" runat="server" ontextchanged="txtrate_TextChanged2"  AutoPostBack="True"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtamount1" style="text-align:right" runat="server" Enabled="false" AutoPostBack="true"></asp:TextBox></td>
											
                                        </tr>
                                    </tbody>
                                    <tbody>
                                        <tr class="odd gradeX">
                                            <td><asp:DropDownList runat="server" ID="ddlcategory2" class="form-control" AutoPostBack="true" >
                                                                                       
                                            </asp:DropDownList></td>
                                            <td><asp:DropDownList runat="server" ID="ddldef2" CssClass="form-control" ></asp:DropDownList></td>
                                            <td><asp:TextBox CssClass="form-control" ID="txtqty2" MaxLength="10" style="text-align:right" runat="server"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" style="text-align:right" ID="txtrate2" runat="server" ontextchanged="txtrate_TextChanged3"  AutoPostBack="True"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" style="text-align:right" 
                                                    ID="txtamount2" runat="server" Enabled="false" 
                                                     AutoPostBack="true"></asp:TextBox></td>
											
                                        </tr>
                                    </tbody>
                                    <tbody>
                                        <tr class="odd gradeX">
                                            <td><asp:DropDownList runat="server" ID="ddlcategory3" class="form-control" AutoPostBack="true">                                                                                       
                                            </asp:DropDownList></td>
                                            <td><asp:DropDownList runat="server" ID="ddldef3" CssClass="form-control" ></asp:DropDownList></td>
                                            <td><asp:TextBox CssClass="form-control" ID="txtqty3" MaxLength="10" style="text-align:right" runat="server"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtrate3" style="text-align:right" runat="server" ontextchanged="txtrate_TextChanged4"  AutoPostBack="True"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtamount3" 
                                                    style="text-align:right" runat="server" Enabled="false" AutoPostBack="true"></asp:TextBox></td>
											
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
								<div class="form-group">
                                            <label style="margin-left: 1095px;">Total</label>
                                            <%--<input class="form-control" placeholder="" style="width: 110px;margin-left: 1143px;margin-top: -36px;">--%>
                 <asp:TextBox CssClass="form-control" ID="txttotal" runat="server" Enabled="false"  
                                                style="width: 110px;margin-left: 1143px;margin-top: -36px;text-align:right"  ></asp:TextBox>
                                        </div>
                                        
                            </div>
							<asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />
                                        
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
        
</body>

</html>

