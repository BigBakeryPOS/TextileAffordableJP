<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditFirstGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.EditFirstGrid" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>First Stage Process</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
      <link rel="Stylesheet" type="text/css" href="../css/date.css" />
      <link href="../Styles/style1.css" rel="stylesheet"/>
      <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <%-- <script type="text/javascript" language="javascript">
        function valchk() {
            if (blankchk(document.getElementById('txtcustomername'), "Customer Name")
            //&& dropdownchk(document.getElementById('ddlgroup'), "Account Group")  
        && phonechk(document.getElementById('txtmobileno'), "MobileNo") && phonechk(document.getElementById('txtphoneno'), "PhoneNo")
        && blankchk(document.getElementById('txtblnce'), "Opening Balance") 
        && blankchk(document.getElementById('txtmobileno'), "MobileNo")
        && blankchk(document.getElementById('txtphoneno'), "Phone No") && blankchk(document.getElementById('txtarea'), "Area")
        && blankchk(document.getElementById('txtaddress'), "Address") && blankchk(document.getElementById('txtcity'), "City")
        && emailchk(document.getElementById('txtemail'), "Email")) {
                alert("true");
            }
            else {
                alert("false");
                return false;
            }
        }
	</script>--%>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" href="../Styles/chosen.css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12">
            <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">
            </h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
               
                    <form id="Form1" runat="server">
                     <div class="form-group">
                    <div id="add" runat="server" class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                      
                      
                            <div class="form-group" id="divcode" visible="false" runat="server">
                                <label>
                                    Trans id</label>
                                <asp:TextBox CssClass="form-control" ID="txttransid" runat="server" Visible="false"></asp:TextBox>
                            </div>
                            <div class="col-lg-1">
                            <label>Item</label>
                          <asp:DropDownList ID="drpitem" Enabled="false" runat="server" CssClass="form-control" >
                          <asp:ListItem Text="Shirts" Value="1"></asp:ListItem>
                          <asp:ListItem Text="Casual" Value="2"></asp:ListItem>
                          <asp:ListItem Text="Trousers" Value="3"></asp:ListItem></asp:DropDownList>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <div class="col-lg-2">
                        <label>Supplier</label>
                        <asp:DropDownList ID="drpsupplier" Width="100%"  runat="server" CssClass="chzn-select" ></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                        <label>Design Code</label>
                            <asp:TextBox ID="txtdesign" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                        <label>Colour/Shade code</label>
                         <asp:TextBox ID="txtcolor" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                            <label>Ordered/Unordered</label>
                          <asp:DropDownList ID="drporder" runat="server" CssClass="form-control" >
                          <asp:ListItem Text="O" Value="1"></asp:ListItem>
                          <asp:ListItem Text="U" Value="2"></asp:ListItem>
                          </asp:DropDownList>
                        </div>
                         <div class="col-lg-1">
                         <label>Mtr.</label>
                         <asp:TextBox ID="txtmtr" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                         <div class="col-lg-1">
                         <label>WSP</label>
                         <asp:TextBox ID="txtwsp" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                         <div class="col-lg-1">
                         <label>MRP</label>
                         <asp:TextBox ID="txtmrp" runat="server" CssClass="form-control"></asp:TextBox>
                         <asp:TextBox id="sample" runat="server" Visible="false"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                     
                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                            <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" OnClick="Add_Click"
                                ValidationGroup="val1" Style="width: 120px; margin-top: 25px" />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"
                                Style="width: 120px; margin-top: 25px" />
                            <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                            <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                   
                    </div>
                     <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
    <!-- /#page-wrapper -->
    <!-- jQuery -->
</body>
</html>

