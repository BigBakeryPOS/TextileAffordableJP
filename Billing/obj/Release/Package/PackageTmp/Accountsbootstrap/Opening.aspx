<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Opening.aspx.cs" Inherits="Billing.Accountsbootstrap.Opening" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <title>Opening Stock Grid </title>
      <!-- Start Styles. Move the 'style' tags and everything between them to between the 'head' tags -->




   <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
        <!--<link href="../Styles/style1.css" rel="stylesheet"/>-->

        <link href="../Styles/style1.css" rel="stylesheet" />

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
   <script type = "text/javascript">
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
</head>
<body>

<usc:Header ID="Header" runat="server" />

<asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
   
   
   <div class="row">
                <div class="col-lg-12" style="margin-top:-7px">
                    <h2 class="page-header" style="text-align:left;color:Red">Setting</h2>
                </div>
               
            </div>


          <div class="row">
                <div class="col-lg-12" style="margin-top:-17px">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                            
                                 <form id="form1" runat="server">
                                   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                 <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />

                                 <div class="row">
                                 <div class="col-lg-2">
                                 <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ></asp:TextBox>

                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*" ErrorMessage="Please Enter the Date" ValidationGroup="val1" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                                 
                                 </div>
                                 <div class="col-lg-2">
                                 <asp:DropDownList ID="DropDownList1" runat="server"  CssClass="form-control">
                                     <asp:ListItem Text="Select Screen" Value="0" Selected="True"></asp:ListItem>
                                     <asp:ListItem Text="Sales" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="SalesReturn" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="PurchaseReturn" Value="4"></asp:ListItem>
                                 </asp:DropDownList>
                                 <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="DropDownList1"
                                                                ValueToCompare="Select Screen" Operator="NotEqual" Type="String" ErrorMessage="Please Select Screen"></asp:CompareValidator>
                                 </div>

                                 <div class="col-lg-2">
                                 <asp:DropDownList ID="DropDownList2" runat="server"  CssClass="form-control" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true">
                                     <asp:ListItem Text="Select Company" Value="0" Selected="True"></asp:ListItem>
                                   
                                 </asp:DropDownList>
                                 <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="DropDownList1"
                                                                ValueToCompare="Select Screen" Operator="NotEqual" Type="String" ErrorMessage="Please Select Screen"></asp:CompareValidator>
                                 </div><div class="col-lg-2" runat="server">
                                 <asp:Label ID="Label1" runat="server" Text=""></asp:Label></div>
                                  <div class="col-lg-2" runat="server" id="edit" visible="false">
                                 <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                 <asp:RequiredFieldValidator ID="req1" runat="server" Text="*" ErrorMessage="Please Enter the Date" ValidationGroup="val1" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                 <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ></asp:TextBox>
                                  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtDate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom" ValidChars="/-."  TargetControlID="txtDate" />

                              
                               <%--  <asp:Label ID="lblBrand" runat="server" Text="Brand"></asp:Label>
                                   <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBrand"
                                                                ValueToCompare="Select Brand" Operator="NotEqual" Type="String" ErrorMessage="Please Select Brand Name"></asp:CompareValidator>
                                 <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true" CssClass="form-control" 
                                          onselectedindexchanged="ddlBrand_SelectedIndexChanged"></asp:DropDownList>--%>
                                        <br />
                                  <asp:Label ID="lblCategory" runat="server"  Text=""></asp:Label>
                                 
                                <%-- <asp:DropDownList ID="ddlCategory" runat="server"  AutoPostBack="true" CssClass="form-control" 
                                          onselectedindexchanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>--%>
                                          <br />

                                  <asp:Label ID="lblItem" runat="server" Text="Product Code"></asp:Label>
                                  <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlItem"
                                                                ValueToCompare="Select Product" Operator="NotEqual" Type="String" ErrorMessage="Please Select product Code"></asp:CompareValidator>
                                 <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control"></asp:DropDownList><br />

                                 <asp:Label ID="lblProduct" runat="server" Text="Product"></asp:Label>
                                  <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlproduct"
                                                                ValueToCompare="Select Product" Operator="NotEqual" Type="String" ErrorMessage="Please Select Product"></asp:CompareValidator>
                                 <asp:DropDownList ID="ddlproduct" runat="server"  CssClass="form-control"></asp:DropDownList><br />





                                  <asp:Label ID="lblNos" runat="server" Text="Qty"></asp:Label>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ErrorMessage="Pease Enter the Qty" ValidationGroup="val1" ControlToValidate="txtNos"></asp:RequiredFieldValidator>
                                  <asp:TextBox ID="txtNos" runat="server" CssClass="form-control"></asp:TextBox>
                                   <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" ValidChars=""  TargetControlID="txtNos" />
                                  <br />
                                  <div class="col-lg-4">
                                 </div>
                                <%--  <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success center-block" 
                                          Text="Save" onclick="btnSave_Click"  ></asp:Button>
                                  <asp:Button ID="btnexit" runat="server" 
                                   class="btn btn-warning" Text="Exit" OnClick="Exit_Click"></asp:Button>--%>
                                 </div>
                                 </div>
                                 
                                 <div class="row">
                                <div class="col-lg-12"  runat="server" id="add">
                                    <div class="panel panel-default">
                                        <!-- /.panel-heading -->
                                        <div class="panel-body">
                                            <div class="table-responsive">
                                                <asp:Label ID="Label4" runat="server" Style="color: Red"></asp:Label>
                                                <table class="table table-striped table-bordered table-hover" id="Table1" width="100%">
                                                    <tr>
                                                        <td colspan="7">
                                                            <asp:GridView ID="GridView2" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="GridView2_RowDataBound"
                                                                OnRowDeleting="GridView2_RowDeleting" CssClass="myGrid" GridLines="None" Width="100%"
                                                                runat="server">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Category"  Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpCategory" Width="100%"  runat="server" Height="26px" AutoPostBack="true"
                                                                                OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="PostType" HeaderStyle-Width="30%" ItemStyle-Width="19%">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ProductCode" runat="server" Width="100%" Height="26px" 
                                                                           AppendDataBoundItems="true">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Op Stock" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtOpStock" runat="server" Height="26px"></asp:TextBox>
                                                                           
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="LedgerName" ControlStyle-Width="100%" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpItem" runat="server" Height="26px" Width="100%"
                                                                                AppendDataBoundItems="true">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <%-- <asp:TemplateField HeaderText="Brand" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtBrand" Enabled="false" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Stock" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtStock" Enabled="false" runat="server" Height="26px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                    <asp:TemplateField HeaderText="Date" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDate" runat="server" onkeyup = "ValidateDate(this, event.keyCode)"
 onkeydown = "return DateFormat(this, event.keyCode)"
></asp:TextBox>
                                                                               <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" PopupButtonID="txtDate"
                                        EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="ButtonAdd1" runat="server" AutoPostback="false" EnableTheming="false"
                                                                                Text="Add New" OnClick="ButtonAdd1_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ControlStyle-Width="100%" ShowDeleteButton="True" ButtonType="Button" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            
                                            <!-- /.table-responsive -->
                                            <!-- /.col-lg-6 (nested) -->
                                        </div>
                                        </div>
                                        </div>
                                        </div>
                                         <div class="col-lg-12" style="padding-left:500px">
                                        
                                        <asp:Button ID="btnSave" runat="server" class="btn btn-success" ValidationGroup="val1"
                                                Text="Save" Style="width: 120px;" OnClick="btnSave_Click" />
                                            <asp:Button ID="Button1" runat="server" Style="width: 120px;" class="btn btn-warning"
                                                Text="Exit" OnClick="btnexit_Click" />
                                            <asp:Label ID="lblError" runat="server" Style="color: Red"></asp:Label>
                                         
                                            </div>
                                           <%--   <script src="../js/chosen/jquery-1.6.1.min.js" type="text/javascript"></script>
		<script src="../js/chosen/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>--%>
                                        </form>
                                        <!-- /.row (nested) -->
                                   
                                <!-- /.panel -->
                           
                            
                         </div>
                    </div>
                 </div>
           </div>
  </body>         
</html>
