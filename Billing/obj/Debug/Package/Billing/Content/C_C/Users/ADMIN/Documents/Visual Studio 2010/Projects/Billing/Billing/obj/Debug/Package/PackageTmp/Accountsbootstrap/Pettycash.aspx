<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pettycash.aspx.cs" Inherits="Billing.Accountsbootstrap.Pettycash" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
     <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />

    <title>Petty cash</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>

    <script type="text/javascript" language="javascript">
        function valchk() {
            if (blankchk(document.getElementById('txtamtfrom'), "Date")
            //&& dropdownchk(document.getElementById('ddlgroup'), "Account Group")  

         &&blankchk(document.getElementById('txtdate'), "Date")
        && blankchk(document.getElementById('txtamount'), "Amount")
        && blankchk(document.getElementById('txtdescp'), "Description")
        ) {
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
   
</body>
<usc:Header ID="Header" runat="server" />
 <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
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
                            <form id="Form2" runat="server">
                                <div class="col-lg-6">
                                    
                                      <label>Recipt No</label>
                                        <div class="form-group input-group">
                                           <asp:TextBox CssClass="form-control" ID="txtreciptno" runat="server"></asp:TextBox>
                                        </div>
                                         <label>Amount Recived By</label>
                                        <div class="form-group input-group">
                                           <asp:TextBox CssClass="form-control" ID="txtname" runat="server"></asp:TextBox>
                                        </div>
                                        
                                        <label>Amount Recived From</label>
                                        <div class="form-group input-group">
                                           <asp:TextBox CssClass="form-control" ID="txtamtfrom" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Date</label>
                                            <asp:TextBox CssClass="form-control" ID="txtdate" runat="server"></asp:TextBox>
                                        </div>
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                                                                   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <div class="form-group">
                                            <label>Amount</label>
                                            <asp:TextBox CssClass="form-control" ID="txtamount" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Description</label>
                                            <asp:TextBox CssClass="form-control" ID="txtdescp" runat="server"></asp:TextBox>
                                        </div>
                                   <asp:Button ID="btnAdd" runat="server" class="btn btn-success" Text="Add" onclick="btnAdd_Click" 
                                             />
                                       
									<div>
                                    <label>Total amount:-</label>
                                    <label runat="server" id="lblamt"></label>
                                    </div>
                                    
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                         
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
</html>
