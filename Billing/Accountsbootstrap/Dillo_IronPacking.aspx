<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dillo_IronPacking.aspx.cs" Inherits="Billing.Accountsbootstrap.Dillo_IronPacking" %>

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
    <title>Flexible Apparels || Iron & Packing Process</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
      <link rel="Stylesheet" type="text/css" href="../css/date.css" />
      <link href="../Styles/style1.css" rel="stylesheet"/>
      <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <link rel="stylesheet" href="../Styles/chosen.css" />
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

    <script type="text/javascript">
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body style="background-color:#c6efce">
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" Visible="false" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">Iron & Packing Process
            </h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row" style="background-color:#c6efce">
        <div class="col-lg-12" style="background-color:#c6efce">
            <div class="panel panel-default">
                <div class="panel-body" style="background-color:#c6efce">
               
                    <form id="Form1" runat="server">

                    
                    <asp:UpdatePanel ID="Updatepanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>


                     <div class="form-group">
                    <div id="add" runat="server" class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                        <div class="col-lg-2">
                            <div class="form-group" id="divcode" runat="server"  Visible="false">
                                <label>
                                    Ledgerid</label>
                                <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server"></asp:TextBox>
                            </div>
                            
                            <div runat="server" visible="false" class="form-group" >
                                <label>
                                    Job Work</label>
                                <asp:CheckBox ID="chckJobWork" MaxLength="150" runat="server" OnCheckedChanged="Change_JobWork" AutoPostBack="true"></asp:CheckBox>
                            </div>

                            <div class="form-group" >
                                <label>
                                    Unit Name</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2" ControlToValidate="ddlUnit"
                                    Text="*" ErrorMessage="Please Select Unit Name!" Style="color: Red" />
                                <asp:DropDownList CssClass="form-control" ID="ddlUnit" OnSelectedIndexChanged="UnitChange" AutoPostBack="true" MaxLength="150" runat="server"></asp:DropDownList>
                            </div>

                            <div class="form-group ">
                                        <label>
                                            Cutting Master</label>
                                        <asp:TextBox CssClass="form-control" ID="txtCuttingMaster" MaxLength="150" Enabled="false" runat="server"></asp:TextBox>
                                        <asp:TextBox CssClass="form-control" ID="txtledgerid" MaxLength="150"  Visible="false" runat="server"></asp:TextBox>

                                         <asp:TextBox CssClass="form-control" ID="txtfinishedQty" runat="server" Visible="false"></asp:TextBox>
                                                 <asp:TextBox CssClass="form-control" ID="txtOldrecQty" runat="server" Visible="false"></asp:TextBox>
                                                 <asp:TextBox CssClass="form-control" ID="txtcheckingbalQty" runat="server" Visible="false"></asp:TextBox>
                                                 <asp:TextBox CssClass="form-control" ID="txtnewfinishedqty" runat="server" Visible="false"></asp:TextBox>
                                    </div>

                            <div class="form-group" >
                                <label>
                                    Brand Name</label>
                                <asp:TextBox CssClass="form-control" ID="txtBrand" MaxLength="150" Enabled="false" runat="server"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="txtbrandid" Visible="false" MaxLength="150" Enabled="false" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group" >
                            <label style="font-weight: bold">
                                    Rate Details</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="GridViewRate_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                     <Columns>
                                       <asp:BoundField DataField="Processmasterid" HeaderText="Process Name" Visible="false"  />
                                     <asp:BoundField DataField="Processtype" HeaderText="Process Name" HeaderStyle-Width="166px" />
                                   <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                                </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <div class="col-lg-2">
                           
                           <div class="form-group" >
                                <label>
                                    Lot No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1" ControlToValidate="ddlLotNo"
                                    Text="*" ErrorMessage="Please Select Lot No!" Style="color: Red" />
                                <asp:DropDownList CssClass="form-control" ID="ddlLotNo" MaxLength="150" runat="server"
                                  OnSelectedIndexChanged="StitchingInfo_Load" AutoPostBack="true"></asp:DropDownList>
                            </div>

                           

                           <div class="form-group" runat="server" visible="false">
                                <label>
                                    Unit Name</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2" ControlToValidate="ddlUnit"
                                    Text="*" ErrorMessage="Please Select Lot No!" Style="color: Red" />
                                <asp:DropDownList CssClass="form-control" ID="ddlUnit" MaxLength="150" runat="server"></asp:DropDownList>--%>
                                <asp:TextBox CssClass="form-control" ID="txtUnitName" MaxLength="150" Enabled="false" runat="server"></asp:TextBox>
                                <asp:TextBox CssClass="form-control" ID="txtUnitID" Visible="false" MaxLength="150" Enabled="false" runat="server"></asp:TextBox>
                            </div>

                            <div id="divPiece" class="form-group" runat="server">
                                <label>
                                    Total Quantity</label>
                                <asp:TextBox CssClass="form-control" ID="txtTotalQantity" Enabled="false" MaxLength="30" Text="0" runat="server"></asp:TextBox>
                            </div>
                             <div id="div1" class="form-group" runat="server">
                                <label>
                                    Total Arrived</label>
                                <asp:TextBox CssClass="form-control" ID="txtarrivedQty" Enabled="false" MaxLength="30" Text="0" runat="server"></asp:TextBox>
                            </div>
                             <div id="div2" class="form-group" runat="server">
                                <label>
                                    Design No</label>
                                <asp:TextBox CssClass="form-control" ID="txtdesign" Enabled="false" MaxLength="30" Text="0" runat="server"></asp:TextBox>
                            </div>
                            
                        </div>
                        <div class="col-lg-6">
                            
                             <label runat="server" id="processs"  style="font-weight: bold;display:none;">
                                    Process Details</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Both"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"  ShowHeader="true" ShowFooter="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" DataKeyNames="Processmasterid"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;" Caption="PROCESS DETAILS">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                     <Columns>


                                                          <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100%" HeaderText="Process Name"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews('dv<%# Eval("Processmasterid") %>', 'imdiv<%# Eval("Processmasterid") %>');"
                                                                        style="text-decoration: none;">
                                                                        <img id="imdiv<%# Eval("Processmasterid") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                                    </a>
                                                                    <%# Eval("Processtype")%>
                                                                    <div id="dv<%# Eval("Processmasterid") %>" style="display: none; position: relative;">
                                                                        <asp:GridView runat="server" ID="gvdetails" CssClass="mGrid" GridLines="Both"
                                                                            AutoGenerateColumns="false" DataKeyNames="TransID" >
                                                                              <HeaderStyle BackColor="Transparent" />
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Transid" Visible="false" DataField="TransID" />
                                                                                <asp:BoundField HeaderText="Size"  DataField="size" />
                                                                                <asp:BoundField HeaderText="FS" DataField="FS" />
                                                                                <asp:BoundField HeaderText="HS" DataField="HS" />
                                                                                
                                                                            </Columns>
                                                                            
                                                                        </asp:GridView>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                              
                                       <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HDLotDetailID" runat="server" 
                                                    Value='<%# Eval("LotDetailID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <%--   <asp:BoundField DataField="Processmasterid" ItemStyle-Font-Bold="true"  HeaderText="Process Name" Visible="false"  />--%>
                                     <asp:BoundField DataField="Processtype" ItemStyle-Font-Bold="true"  HeaderText="Process Name"/>

                                       <asp:BoundField DataField="36HS" ItemStyle-Font-Bold="true"  HeaderText="36HS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="38HS" ItemStyle-Font-Bold="true"  HeaderText="38HS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="39HS" ItemStyle-Font-Bold="true"  HeaderText="39HS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="40HS" ItemStyle-Font-Bold="true"  HeaderText="40HS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="42HS" ItemStyle-Font-Bold="true"  HeaderText="42HS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="44HS" ItemStyle-Font-Bold="true"  HeaderText="44HS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="36FS" ItemStyle-Font-Bold="true"  HeaderText="36FS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="38FS" ItemStyle-Font-Bold="true"  HeaderText="38FS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="39FS" ItemStyle-Font-Bold="true"  HeaderText="39FS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="40FS" ItemStyle-Font-Bold="true"  HeaderText="40FS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="42FS" ItemStyle-Font-Bold="true"  HeaderText="42FS" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="44FS" ItemStyle-Font-Bold="true"  HeaderText="44FS" ItemStyle-HorizontalAlign="Center" />

                                     <asp:BoundField HeaderText="Kaja Finished Qty" ItemStyle-Font-Bold="true" DataField="FinishedQty" ItemStyle-HorizontalAlign="Center" />
                                     <asp:BoundField HeaderText="Total Qty" ItemStyle-Font-Bold="true" DataField="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                     <asp:BoundField HeaderText="Remain Qty" DataField="RemainQty" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" />
                                        
                                    
                                    </Columns>
                                </asp:GridView>
                      
                        </div>

                        <div class="col-lg-8">



                        </div>
                    </div>
                    
                    </div>

                    

                    <div class="col-lg-12">

                                            <div style="overflow:scroll; height:360px">
                                    <!-- /.panel-heading -->
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                    <div class="panel-body" style="height:350px">
                                        <div class="table-responsive">
                                            <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                            <table  id="Table1" width="100%">
                                                <tr>
                                                    <td colspan="7">
                                                        <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                            OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                            CssClass="chzn-container"  Width="100%" runat="server">
                                                               <HeaderStyle BackColor="#59d3b4" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                                            <Columns>
                                                              <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                                    <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                   <%--  <asp:TemplateField HeaderText="Transid" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                 <asp:TextBox ID="txttransid" Height="30px" Text='<%# Eval("Transid")%>' runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="Process Type" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="drpProcess" OnSelectedIndexChanged="drpprocess_selected" Width="100%" AutoPostBack="true"  runat="server" Height="26px" AppendDataBoundItems="true">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="drpEmp" runat="server" Height="26px" Width="100%" ></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="36HS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt36HS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt36HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="38HS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt38HS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt38HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="39HS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt39HS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt39HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="40HS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt40HS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt40HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="42HS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt42HS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt42HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="44HS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt44HS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt44FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="36FS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt36FS" Text="0" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt36FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="38FS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt38FS" Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt38FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="39FS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt39FS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt39FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="40FS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt40FS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt40FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="42FS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt42FS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt42FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="44FS" ItemStyle-Width="3%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt44FS"  Text="0"  runat="server" Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt44FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Received Quantity"  ItemStyle-Width="10%">
                                                                     <ItemTemplate>
                                                                        <asp:TextBox ID="txtRecQuantity" runat="server"  Height="26px" Width="100%" AutoPostBack="true" OnTextChanged="txtRange_Change" AppendDataBoundItems="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="date" runat="server" onkeyup="ValidateDate(this, event.keyCode)"
                                                                    onkeydown="return DateFormat(this, event.keyCode)"  Height="26px" Width="100%" AppendDataBoundItems="true"></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" TargetControlID="date"
                                                                    PopupButtonID="date" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                                    CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Rate" ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" runat="server" Height="26px" Width="100%" AppendDataBoundItems="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Add"  ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                       <%-- <asp:Button ID="ButtonAdd1" runat="server" Visible="false" AutoPostback="false" EnableTheming="false" Text="Add" OnClick="ButtonAdd1_Click" />--%>
                                                                        <asp:Button ID="ButtonAdd1" runat="server" Text="Add" Width="100%" OnClick="ButtonAdd1_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="100%" HeaderStyle-Width="5%" ButtonType="Button" />

                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                
                            </div>

                            <div class="col-lg-12">
                                             <div id="but" runat="server" class="row">
                    <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" 
                                ValidationGroup="val1" Style="width: 120px; margin-top: 25px" OnCLick="Add_LotProcessDetails" />
                            <asp:Button ID="btnexit" runat="server" PostBackUrl="~/Accountsbootstrap/Dillo_Irongrid.aspx" class="btn btn-warning" Text="Exit" 
                                Style="width: 120px; margin-top: 25px" />
                    </div>
                    </div>

                    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>

                            </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdateProgress ID="Updateprogress" runat="server" AssociatedUpdatePanelID="Updatepanel2">
                    <ProgressTemplate>
                    <div  style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                    position: fixed; top: 50%; left: 40%;" />
                    </div>
                    </ProgressTemplate>
                    </asp:UpdateProgress>

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
