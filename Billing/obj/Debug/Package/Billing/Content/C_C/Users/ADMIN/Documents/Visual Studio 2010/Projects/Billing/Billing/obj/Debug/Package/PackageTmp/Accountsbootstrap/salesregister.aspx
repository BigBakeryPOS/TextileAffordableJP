<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="salesregister.aspx.cs" Inherits="Billing.Accountsbootstrap.salesregister" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>
<html lang="en">

<head runat="server">

    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

	<link rel="Stylesheet" type="text/css" href="../css/date.css" />
	
	
    <title>Sales Register Page - bootsrap</title>

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
    



 
 
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Report</h1>
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
                                    <form runat="server" role="form">
                                        <div class="form-group">
                                            <label>From Date</label>
											<asp:TextBox CssClass="form-control" ID="txtfrmdate" runat="server" Text="--Select Date--"></asp:TextBox>
                                                                                       
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="txtfrmdate1" TargetControlID="txtfrmdate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
										<div class="form-group">
                                            <label>To Date</label>
											<asp:TextBox CssClass="form-control" ID="txttodate" runat="server" Text="--Select Date--"></asp:TextBox>
                                                                                       
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txttodate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                        
										<div class="form-group">
                                            <label>Filter By</label>
                                            <select id="disabledSelect" class="form-control">
                                                    <option>Select Category</option>
													<option value="1">Sarees</option>
													<option value="2">Pavadai</option>
													<option value="3">Churidhar</option>
													<option value="4">Shirt and Pant</option>
													<option value="5">Shirt</option>
													<option value="6">Dhoti</option>
													<option value="7">Blouse</option>
                                                </select>
                                        </div>
										<div class="form-group">
                                            <label>Condition</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcondition" runat="server"></asp:TextBox>
                                        </div>			                                 
                                        <button type="reset" class="btn btn-primary">Refresh</button>
										
                                    
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
                                            <th>Bill Ref No</th>
                                            <th>Type</th>
                                            <th>Party</th>
											<th>Total</th>
											<th>Disc Amt</th>
											<th>NetAmt</th>
											<th>Cash</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="odd gradeX">
                                            <td></td>
                                            <td></td>
                                            <td></td>
											<td></td>
                                            <td></td>
                                            <td></td>
                                            <td class="center"></td>
                                            <td class="center"></td>
											<td class="center"></td>
                                        </tr>
                                    </tbody>
                                </table>
								
                            </div>
							
										<button type="submit" class="btn btn-danger">Doc Print</button>
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
        </form>
        <!-- /#page-wrapper -->
		</div>
        </div>
        </div>
        </div>
        </div>
		
		
		<!-- jQuery -->
   

</body>

</html>
