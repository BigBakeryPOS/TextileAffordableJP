<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewCutProcessEdit.aspx.cs" Inherits="Billing.Accountsbootstrap.NewCutProcessEdit" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Fabric Process</title>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <script src="" type="text/javascript"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        //        function valchk() {
        //            if (blankchk(document.getElementById('txtBrandname'), "Cheque Name")
        //            {
        //                alert("true");
        //            }
        //            else {
        //                alert("false");
        //                return false;
        //            }
        //        }
    </script>
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
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header" style="text-align: center; color: #fe0002;">
                Pre-Cutting Process</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <form id="Form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                       
                        <div class="col-lg-3">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                         
                         
                            <div id="Div21" runat="server" visible="false" class="form-group ">
                                <label>
                                    ID</label>
                                     <asp:TextBox CssClass="form-control" ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator3"
                                    ControlToValidate="TextBox3" ErrorMessage="Please enter ID" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox3" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group ">
                                <label>
                                    Lot No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator1"
                                    ControlToValidate="txtLotNo" ErrorMessage="Please enter Meter" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtLotNo" MaxLength="6" runat="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="Numbers" ValidChars="" TargetControlID="txtLotNo" />
                            </div>

                            <div class="form-group">
                                <label>
                                   Cutting Issue Date:</label>
                                <asp:TextBox ID="txtdate" runat="server" Text="-Select Date-" Enabled="false" CssClass="form-control"> </asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                            
                              
                             
                         
                        </div>
                         <div class="col-lg-3">
                          <div  class="form-group ">
                                <label>
                                    Select Width</label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpwidth" ValueToCompare="Select Width"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please select Width!"></asp:CompareValidator>
                                <asp:DropDownList ID="drpwidth" Enabled="false" OnSelectedIndexChanged="drpwidthChange" AutoPostBack="true" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                             <div  class="form-group ">
                                <label>
                                    Cutting  Master</label>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpcutting" ValueToCompare="Select Cutting"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please select Cutting Master!"></asp:CompareValidator>
                                <asp:DropDownList ID="drpcutting" Enabled="false"  runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        
                         </div>
                        <div runat="server" visible="false" class="col-lg-3">
                         <div id="Div42" runat="server"  class="form-group ">
                                <label>
                                    Production Cost</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator8"
                                    ControlToValidate="txtprod" ErrorMessage="Please enter Production Cost" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtprod" MaxLength="6" runat="server" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="Numbers,custom" ValidChars="." TargetControlID="txtprod" />
                            </div>
                           <div id="Div4" runat="server" class="form-group ">
                            <label>
                                    Type</label>
                                <asp:RadioButtonList ID="radbtn" Enabled="false" OnSelectedIndexChanged="radchecked" AutoPostBack="true"
                                    RepeatColumns="2" runat="server">
                                    <asp:ListItem Text="Single Party"  Selected="True" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Multiple Party" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                              </div>

                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                    </div>
                     <div class="row">
                     </div>
                     </br>
                    
                                          
                    <div class="row">
                     <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
                        <div class="col-lg-12" style="margin-top: -35px">
                            <div class="panel-body">
                                <div>
                                    <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                    <table class="table table-striped table-bordered table-hover" id="Table1" width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Panelformal" Visible="false" runat="server" ScrollBars="Both" Width="100%">
                                                    <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                        OnRowDataBound="GridView2_RowDataBound"  OnSelectedIndexChanged="YourGridView_SelectedIndexChanged" 
                                                        OnRowDeleting="GridView2_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                        Width="100%" runat="server">
                                                        <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                        <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                      <%--  <asp:TextBox ID="txtno" Enabled="false" Text='<%# Eval("num")%>' runat="server" ></asp:TextBox>--%>
                                                                         <asp:Label ID="lbltransid" Visible="false" Text='<%# Eval("transid")%>' runat="server" ></asp:Label>
                                                                          <asp:Label ID="lblinvre" Visible="false" Text='<%# Eval("Invrefno")%>' runat="server" ></asp:Label>
                                                                   <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Design/Color Code" ControlStyle-Width="100%" ItemStyle-Width="15%"
                                                                >
                                                                <ItemTemplate>
                                                                        <asp:TextBox ID="txtdesigno" Enabled="false" Text='<%# Eval("designno")%>' runat="server" ></asp:TextBox>
                                                                   <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Party Name" Visible="false" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                <ItemTemplate>
                                                                  <asp:TextBox ID="txtledgerid" Visible="false" Text='<%# Eval("partyname")%>' runat="server" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtparty" Enabled="false" Text='<%# Eval("ledgername")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Brand Name" ControlStyle-Width="100%" ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbrandid" Visible="false" Text='<%# Eval("brandid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtbrand" Enabled="false" Text='<%# Eval("brand")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Rate" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" Enabled="false" Height="30px" Text='<%# Eval("Rate")%>'  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Ava. meter" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                     <asp:TextBox ID="txtmet" Enabled="false" Text='<%# Eval("totalmeter")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             
                                                          
                                                            <asp:TemplateField HeaderText="Req. Meter" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txteqrmeter" Enabled="false" Text='<%# Eval("reqmeter")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Avg.Size " ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtavgsize" Enabled="false" Text='<%# Eval("avgsize")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                              <%-- <asp:TemplateField HeaderText="Act. Meter"  ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtactmeter" Enabled="false" Text='<%# Eval("Actmeter")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Act.Size "  ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtActsize" Enabled="false" Text='<%# Eval("Actsize")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                 <asp:TemplateField HeaderText="Req. Shirt" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtreqshirt" Enabled="false" Text='<%# Eval("reqshirt")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          

                                                               <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                  <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("dfit")%>' runat="server" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtfit" Enabled="false" Text='<%# Eval("Fit")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Shirt"  Visible="false" ControlStyle-Width="100%"   ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtshirt" Enabled="false" Height="30px" Text='<%# Eval("Shirt")%>' runat="server"></asp:TextBox>
                                                                          
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="36 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txttsfs" OnTextChanged="change36fs" AutoPostBack="true" Text='<%# Eval("TSFS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="38 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txttefs" OnTextChanged="change38fs" AutoPostBack="true"  Text='<%# Eval("TEFS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="39 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txttnfs" OnTextChanged="change39fs" AutoPostBack="true"  Text='<%# Eval("TNFS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="40 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate> 
                                                                        <asp:TextBox ID="txtfzfs" OnTextChanged="change40fs" AutoPostBack="true"  Text='<%# Eval("FZFS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="42 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtftfs" OnTextChanged="change42fs" AutoPostBack="true" Text='<%# Eval("FTFS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="44 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtfffs" OnTextChanged="change44fs" AutoPostBack="true"  Text='<%# Eval("FFFS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="36 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txttshs" OnTextChanged="change36hs" AutoPostBack="true" Text='<%# Eval("TSHS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                               
                                                                 <asp:TemplateField HeaderText="38 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txttehs" OnTextChanged="change38hs" AutoPostBack="true" Text='<%# Eval("TEHS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                               
                                                                 <asp:TemplateField HeaderText="39 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txttnhs" OnTextChanged="change39hs" AutoPostBack="true" Text='<%# Eval("TNHS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="40 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtfzhs" OnTextChanged="change40hs" AutoPostBack="true"  Text='<%# Eval("FZHS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="42 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtfths"  OnTextChanged="change42hs" AutoPostBack="true" Text='<%# Eval("FTHS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="44 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtffhs"  OnTextChanged="change44hs" AutoPostBack="true" Text='<%# Eval("FFHS")%>' runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField Text="Update" CommandName="Select" ItemStyle-Width="20"  />
                                                             
                                                            <asp:CommandField Visible="false" ShowDeleteButton="True" ButtonType="Button" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>

                                                <asp:Panel ID="Panelcasual" Visible="false" runat="server" ScrollBars="Both" Width="100%">
                                                    <asp:GridView ID="GridView1" AutoGenerateColumns="False" ShowFooter="True"
                                                        OnRowDataBound="GridView2_RowDataBound"  OnSelectedIndexChanged="YourGridView_SelectedIndexChanged" 
                                                        OnRowDeleting="GridView2_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                        Width="100%" runat="server">
                                                        <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                        <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                      <%--  <asp:TextBox ID="txtno" Enabled="false" Text='<%# Eval("num")%>' runat="server" ></asp:TextBox>--%>
                                                                         <asp:Label ID="lbltransid" Visible="false" Text='<%# Eval("transid")%>' runat="server" ></asp:Label>
                                                                          <asp:Label ID="lblinvre" Visible="false" Text='<%# Eval("Invrefno")%>' runat="server" ></asp:Label>
                                                                   <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Design/Color Code" ControlStyle-Width="100%" ItemStyle-Width="15%"
                                                                >
                                                                <ItemTemplate>
                                                                        <asp:TextBox ID="txtdesigno" Enabled="false" Text='<%# Eval("designno")%>' runat="server" ></asp:TextBox>
                                                                   <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Party Name" Visible="false" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                <ItemTemplate>
                                                                  <asp:TextBox ID="txtledgerid" Visible="false" Text='<%# Eval("partyname")%>' runat="server" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtparty" Enabled="false" Text='<%# Eval("ledgername")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Brand Name" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbrandid" Visible="false" Text='<%# Eval("brandid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtbrand" Enabled="false" Text='<%# Eval("brand")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Rate" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" Enabled="false" Height="30px" Text='<%# Eval("Rate")%>'  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Ava. meter" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                     <asp:TextBox ID="txtmet" Enabled="false" Text='<%# Eval("totalmeter")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             
                                                          
                                                            <asp:TemplateField HeaderText="Req. Meter" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txteqrmeter" Enabled="false" Text='<%# Eval("reqmeter")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Avg.Size " ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtavgsize" Enabled="false" Text='<%# Eval("avgsize")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Act. Meter"  ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtactmeter" Enabled="false" Text='<%# Eval("Actmeter")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Act.Size "  ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtActsize" Enabled="false" Text='<%# Eval("Actsize")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     

                                                                 <asp:TemplateField HeaderText="Req. Shirt" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtreqshirt" Enabled="false" Text='<%# Eval("reqshirt")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                               <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                  <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("dfit")%>' runat="server" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtfit" Enabled="false" Text='<%# Eval("Fit")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Shirt" ControlStyle-Width="100%" Visible="false"   ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtshirt" Enabled="false" Height="30px" Text='<%# Eval("Shirt")%>' runat="server"></asp:TextBox>
                                                                          
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="S FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttsfs" OnTextChanged="change36fs" AutoPostBack="true"  Text='<%# Eval("TSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttefs" OnTextChanged="change38fs" AutoPostBack="true"  Text='<%# Eval("TEFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="39 FS" Visible="false" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttnfs" OnTextChanged="change39fs" AutoPostBack="true" Text='<%# Eval("TNFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfzfs" OnTextChanged="change40fs" AutoPostBack="true"  Text='<%# Eval("FZFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtftfs" OnTextChanged="change42fs" AutoPostBack="true"  Text='<%# Eval("FTFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfffs" OnTextChanged="change44fs" AutoPostBack="true"  Text='<%# Eval("FFFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttshs" OnTextChanged="change36hs" AutoPostBack="true"  Text='<%# Eval("TSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttehs" OnTextChanged="change38hs" AutoPostBack="true" Text='<%# Eval("TEHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="39 HS" Visible="false" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttnhs" OnTextChanged="change39hs" AutoPostBack="true"  Text='<%# Eval("TNHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfzhs" OnTextChanged="change40hs" AutoPostBack="true"  Text='<%# Eval("FZHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfths" OnTextChanged="change42hs" AutoPostBack="true"  Text='<%# Eval("FTHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtffhs"  OnTextChanged="change44hs" AutoPostBack="true" Text='<%# Eval("FFHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                <asp:ButtonField Text="Update" CommandName="Select" ItemStyle-Width="20"  />
                                                             
                                                            <asp:CommandField Visible="false" ShowDeleteButton="True" ButtonType="Button" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>

                                                <asp:Panel ID="Panelboys" Visible="false" runat="server" ScrollBars="Both" Width="100%">
                                                    <asp:GridView ID="GridView2" AutoGenerateColumns="False" ShowFooter="True"
                                                        OnRowDataBound="GridView2_RowDataBound"  OnSelectedIndexChanged="YourGridView_SelectedIndexChanged" 
                                                        OnRowDeleting="GridView2_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                        Width="100%" runat="server">
                                                        <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                        <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                      <%--  <asp:TextBox ID="txtno" Enabled="false" Text='<%# Eval("num")%>' runat="server" ></asp:TextBox>--%>
                                                                         <asp:Label ID="lbltransid" Visible="false" Text='<%# Eval("transid")%>' runat="server" ></asp:Label>
                                                                          <asp:Label ID="lblinvre" Visible="false" Text='<%# Eval("Invrefno")%>' runat="server" ></asp:Label>
                                                                   <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Design/Color Code" ControlStyle-Width="100%" ItemStyle-Width="10%"
                                                                >
                                                                <ItemTemplate>
                                                                        <asp:TextBox ID="txtdesigno" Enabled="false" Text='<%# Eval("designno")%>' runat="server" ></asp:TextBox>
                                                                   <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Party Name" Visible="false" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                <ItemTemplate>
                                                                  <asp:TextBox ID="txtledgerid" Visible="false" Text='<%# Eval("partyname")%>' runat="server" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtparty" Enabled="false" Text='<%# Eval("ledgername")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Brand Name" ControlStyle-Width="100%" ItemStyle-Width="7%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbrandid" Visible="false" Text='<%# Eval("brandid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtbrand" Enabled="false" Text='<%# Eval("brand")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Rate" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" Enabled="false" Height="30px" Text='<%# Eval("Rate")%>'  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Ava. meter" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                     <asp:TextBox ID="txtmet" Enabled="false" Text='<%# Eval("totalmeter")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             
                                                          
                                                            <asp:TemplateField HeaderText="Req. Meter" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txteqrmeter" Enabled="false" Text='<%# Eval("reqmeter")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Avg.Size " ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtavgsize" Enabled="false" Text='<%# Eval("avgsize")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                              <asp:TemplateField HeaderText="Act. Meter"  ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtactmeter" Enabled="false" Text='<%# Eval("Actmeter")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Act.Size "  ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtActsize" Enabled="false" Text='<%# Eval("Actsize")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Req. Shirt" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtreqshirt" Enabled="false" Text='<%# Eval("reqshirt")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                               <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                  <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("dfit")%>' runat="server" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtfit" Enabled="false" Text='<%# Eval("Fit")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Shirt" ControlStyle-Width="100%" Visible="false"   ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtshirt" Enabled="false" Height="30px" Text='<%# Eval("Shirt")%>' runat="server"></asp:TextBox>
                                                                          
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="28 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttsfs" OnTextChanged="change36fs" AutoPostBack="true"  Text='<%# Eval("TSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttefs" OnTextChanged="change38fs" AutoPostBack="true" Text='<%# Eval("TEFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttnfs" OnTextChanged="change39fs" AutoPostBack="true"  Text='<%# Eval("TNFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfzfs" OnTextChanged="change40fs" AutoPostBack="true"  Text='<%# Eval("FZFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtftfs" OnTextChanged="change42fs" AutoPostBack="true"  Text='<%# Eval("FTFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="38 FS"  ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfffs" OnTextChanged="change44fs" AutoPostBack="true"  Text='<%# Eval("FFFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="28 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttshs" OnTextChanged="change36hs" AutoPostBack="true"  Text='<%# Eval("TSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttehs" OnTextChanged="change38hs" AutoPostBack="true"  Text='<%# Eval("TEHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttnhs" OnTextChanged="change39hs" AutoPostBack="true"  Text='<%# Eval("TNHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfzhs" OnTextChanged="change40hs" AutoPostBack="true" Text='<%# Eval("FZHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfths" OnTextChanged="change42hs" AutoPostBack="true"  Text='<%# Eval("FTHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="38 HS"  ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtffhs" OnTextChanged="change44hs" AutoPostBack="true" Text='<%# Eval("FFHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                <asp:ButtonField Text="Update" CommandName="Select" ItemStyle-Width="20"  />
                                                             
                                                            <asp:CommandField Visible="false" ShowDeleteButton="True" ButtonType="Button" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td4" runat="server" visible="false" align="right">
                                                <asp:Button ID="ButtonAdd1" runat="server" EnableTheming="false" Text="Add New" />
                                            </td>
                                        </tr>
                                        <tr>
                                          
                                    </table>
                                    <%--</tr>
                                            </tbody>--%>
                                    </td> </tr> </tbody>
                                   
                                </div>
                                <br />
                                <asp:Button ID="Button1" AccessKey="s" Visible="false" runat="server"  class="btn btn-info" BorderWidth="3px"
                                    BorderColor="#e41300" BorderStyle="Inset" OnClick="Add_Click" onmouseover="this.style.backgroundColor='#5bc0de'"
                                    onmousedown="this.style.backgroundColor='olive'" onfocus="this.style.backgroundColor='#1b293e'"
                                    Text="Update" ValidationGroup="val1" Width="120px" />
                                     <asp:Button ID="btncalc" runat="server" Visible="false" class="btn btn-info" Text="Calc." OnClick="call_Click"
                                ValidationGroup="val1" Style="width: 120px;" />
                                     <asp:Button ID="btnadd" runat="server" Visible="false" class="btn btn-info" Text="Save" OnClick="Add_Click"
                                ValidationGroup="val1" Style="width: 120px;" />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"
                                Style="width: 120px;" />
                               
                            </div>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                       
                    </div>
                </div>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" >
        
         <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999;  opacity: 0.7;">
          <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif" AlternateText="Loading ..." ToolTip="Loading ..." style=" width:150px; padding: 10px;position:fixed;top:50%;left:40%;" />
       
          <%--<asp:Image ID="imgUpdateProgress1" runat="server" ImageUrl="../images/loading.gif" />--%>
        </div>
    </ProgressTemplate>
    </asp:UpdateProgress>

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
    <!-- /.row -->
    <!-- /#page-wrapper -->
    <!-- jQuery -->
</body>
</html>

