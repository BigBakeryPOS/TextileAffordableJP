<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dillo_EmbWorks.aspx.cs" Inherits="Billing.Accountsbootstrap.Dillo_EmbWorks" %>
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
    <title>Flexible Apparels || Enb Job Work</title>
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
    <style>
        .chkChoice input
        {
            margin-left: -20px;
        }
        .chkChoice td
        {
            padding-left: 20px;
        }
    </style>
</head>
<body style="background-color:#c6efce">
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" Visible="false" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">Emb Job Work
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


  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
  <ContentTemplate>


                     <div class="form-group">
                    <div id="add" runat="server" class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                             <div class="col-lg-2">
                          <div id="div1" class="form-group" runat="server">
                                <label>
                                    DC No</label>
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtdcno" runat="server"></asp:TextBox>
                            </div>
                        
                          
                          <div id="div2" class="form-group" runat="server">
                                <label>
                                    Dc Date</label>
                                <asp:TextBox CssClass="form-control" ID="txtdcdate" onkeyup="ValidateDate(this, event.keyCode)"
                                                                    onkeydown="return DateFormat(this, event.keyCode)"  runat="server"></asp:TextBox>
                                      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdcdate" runat="server"
                                            Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                            </div>
                             </div>
                            <div class="col-lg-2">
                            <label>JOB Worker Name</label>
                            <asp:DropDownList ID="drpjobwork" runat="server" CssClass="form-control"></asp:DropDownList>
                         </div>
                        <div class="col-lg-3">
                            <div class="form-group" id="divcode" runat="server"  Visible="false">
                                <label>
                                    Ledgerid</label>
                                <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server"></asp:TextBox>
                            </div>
                            
                           <div class="form-group" >
                           <div style="overflow-y: scroll; width: 317px; height: 170px">
                                            <div class="panel panel-default" style="width: 337px">
                                <label>
                                    Lot No</label>
                             
                                <asp:CheckBoxList CssClass="chkChoice" ID="chkLotNo" RepeatColumns="3" RepeatDirection="Horizontal" runat="server"
                                  OnSelectedIndexChanged="StitchingInfo_Load" AutoPostBack="true"></asp:CheckBoxList>
                                  </div>
                                  </div>
                            </div>

                      
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <div class="col-lg-3">

                        <div id="divsupervisor" class="form-group" runat="server">
                        <label>Sending Supervisor</label>
                        <asp:DropDownList ID="drpsupervisor" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                           

                         <div id="divsendate" class="form-group" runat="server">
                            <label>Sending Date</label>
                                <asp:TextBox CssClass="form-control" ID="txtsendingdate" onkeyup="ValidateDate(this, event.keyCode)"
                                                                    onkeydown="return DateFormat(this, event.keyCode)"  runat="server"></asp:TextBox>
                                      <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtsendingdate" runat="server"
                                            Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                            </div>
                            
                        </div>
                         <div class="col-lg-2">
                          <div id="divPiece" class="form-group" runat="server">
                                <label>
                                    Total Quantity</label>
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtTotalQantity" Text="0" runat="server"></asp:TextBox>
                            </div>
                         </div>

                        <div id="Div15" runat="server" visible="false" class="col-lg-6">
                            <div id="Div34" class="panel panel-default"  runat="server" visible="false" style="width: 550px">
                             <label style="font-weight: bold">
                                    Process Details</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" 
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                     <Columns>
                                       <asp:BoundField DataField="Processmasterid" HeaderText="Process Name" Visible="false" ItemStyle-ForeColor="White"  />
                                     <asp:BoundField DataField="Processtype" HeaderText="Process Name" HeaderStyle-Width="166px" ItemStyle-ForeColor="White" />
                                   
                                        <asp:BoundField HeaderText="Total Qty" DataField="TotalQty" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="White" />
                                        <asp:BoundField HeaderText="Remain Qty" DataField="RemainQty" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="White" />
                                        
                                    
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                       

                        <div id="Div92" class="col-lg-3" runat="server" visible="false">
                             <label style="font-weight: bold">
                                    Rate Details</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="GridViewRate_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                     <Columns>
                                       <asp:BoundField DataField="Processmasterid" HeaderText="Process Name" Visible="false"  ItemStyle-ForeColor="White" />
                                     <asp:BoundField DataField="Processtype" HeaderText="Process Name" HeaderStyle-Width="166px"  ItemStyle-ForeColor="White"/>
                                   <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="White" />
                                    </Columns>
                                </asp:GridView>
                        </div>

                        <div id="Div3" class="col-lg-3" runat="server" visible="false">
                             <label style="font-weight: bold">
                                    Work Process</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView3" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true" OnRowDataBound="GridViewWork_RowDataBound"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                     <Columns>
                                       <asp:BoundField DataField="Fitlab" HeaderText="Fit Lab" ItemStyle-ForeColor="White" />
                                     <asp:BoundField DataField="Washlab" HeaderText="Wash Lab" ItemStyle-ForeColor="White" />
                                   <asp:BoundField  DataField="Logolab" HeaderText="Logo Lab" ItemStyle-ForeColor="White" />
                                    </Columns>
                                </asp:GridView>
                        </div>

                        <div id="Div4" class="col-lg-2" visible="false" runat="server" >
                             <label style="font-weight: bold">
                                    Work Process</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView4" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true" OnRowDataBound="GridViewWork_RowDataBound"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                     <Columns>
                                       <asp:BoundField DataField="IsKaja" HeaderText="Kaja" ItemStyle-ForeColor="White" />
                                     <asp:BoundField DataField="IsEmbroiding" HeaderText="Embroiding" ItemStyle-ForeColor="White" />
                                   <asp:BoundField  DataField="IsWashing" HeaderText="Washing" ItemStyle-ForeColor="White" />
                                    </Columns>
                                </asp:GridView>
                        </div>
                    </div>
                    
                    </div>

                    

                    <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <!-- /.panel-heading -->
                                  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                    <div class="panel-body" style="height:350px">
                                        <div class="table-responsive">
                                            <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                            <table  id="Table1" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="false"  ShowFooter="True" 
                                                            OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                            CssClass="chzn-container"  Width="80%" Height="300px" runat="server">
                                                               <HeaderStyle BackColor="#59d3b4" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                                            <Columns>
                                                              <asp:TemplateField HeaderText="S.No" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                     <asp:TemplateField HeaderText="LOT NO" Visible="false" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="txtcutid" Text='<%# Eval("cutid")%>' runat="server" Visible="false" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtlotno" Text='<%# Eval("lotno")%>' runat="server" Enabled="false" ></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Unit Name" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="txtunitid" Text='<%# Eval("unitid")%>' runat="server" Visible="false" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtunitname" Text='<%# Eval("unit")%>' runat="server" Enabled="false" ></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Brand Name" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="txtbrandid" Text='<%# Eval("Brandid")%>' runat="server" Visible="false" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtbrandname" Text='<%# Eval("Brand")%>' runat="server" Enabled="false" ></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Qty" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="txtqty" Text='<%# Eval("Qty")%>' runat="server" Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate"  ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" Text='<%# Eval("Rate")%>' runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Design No"  ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="txtdesign" Text='<%# Eval("Designno")%>' Enabled="false" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                  <%--  </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                            </div>

                            <div class="col-lg-12">
                    <div id="but" runat="server" class="row">
                    <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" 
                                ValidationGroup="val1" Style="width: 120px; margin-top: 25px" OnCLick="Add_LotProcessDetails" />
                            <asp:Button ID="btnexit" runat="server" PostBackUrl="~/Accountsbootstrap/Dillo_EmbWorkGrid.aspx" class="btn btn-warning" Text="Exit" 
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
        <Triggers></Triggers>
  </asp:UpdatePanel>

  <asp:UpdateProgress ID="UPdateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
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

