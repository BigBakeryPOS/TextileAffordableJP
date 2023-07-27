<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cutprocess.aspx.cs" Inherits="Billing.Accountsbootstrap.Cutprocess" %>

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
                Cutting Process</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <form id="Form1" runat="server">


        <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
 

        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-3">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="form-group" id="divcode" runat="server">
                                <asp:TextBox CssClass="form-control" ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div runat="server" visible="false" class="form-group ">
                                <asp:RadioButtonList ID="radbtn" OnSelectedIndexChanged="radchecked" AutoPostBack="false"
                                    RepeatColumns="2" runat="server">
                                    <asp:ListItem Text="Single Party" Selected="True" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Multiple Party" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div runat="server" visible="false" class="form-group ">
                                <label>
                                    ID</label>
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
                                   Delivery Date:</label>
                                <asp:TextBox ID="txtdate" runat="server" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                             <div  class="form-group ">
                                <label>
                                    Fit</label>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlFit" ValueToCompare="Select Fit"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please select Fit!"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlFit" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                           
                            <div id="Div1" runat="server" visible="false" class="form-group ">
                                <label>
                                    Design No</label>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlDNo" ValueToCompare="Select Design Name"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please select Design No!"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlDNo" runat="server" class="form-control" OnSelectedIndexChanged="ddlDNo_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div id="Div2" runat="server" visible="false" class="form-group ">
                                <label>
                                    Available Meter</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator2"
                                    ControlToValidate="txtMeter" ErrorMessage="Please enter Meter" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtMeter" MaxLength="6" runat="server" Enabled="false"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Numbers,custom" ValidChars="." TargetControlID="txtMeter" />
                            </div>
                        </div>
                        <div class="col-lg-3">
                         <div  class="form-group ">
                                <label>
                                    Select Width</label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpwidth" ValueToCompare="Select Width"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please select Width!"></asp:CompareValidator>
                                <asp:DropDownList ID="drpwidth" OnSelectedIndexChanged="drpwidthChange" AutoPostBack="true" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>

                          <div id="Div7"  runat="server" class="col-lg-5">
                            <div style="overflow-y: scroll; width: 290px; height: 170px">
                                  <div class="panel panel-default" style="width: 272px">
                                <label>Invoice Number</label>
                                    <asp:CheckBoxList ID="chkinvno" OnSelectedIndexChanged="chkinvnochanged" CssClass="chkChoice" AutoPostBack="true" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                        Width="100%" RepeatLayout="Table" Style="overflow: auto">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                           
                            <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                            <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                        </div>
                         
                        

                           

                            <div  visible="false" runat="server" class="form-group ">
                                <label>
                                    Customer Name</label>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlSupplier"
                                    ValueToCompare="Select Party Name" Operator="NotEqual" Type="String" ErrorMessage="Please select Party name!"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlSupplier" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div id="Div3" runat="server" visible="false" class="form-group ">
                                <label>
                                    Required Meter</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator7"
                                    ControlToValidate="txtMeter" ErrorMessage="Please enter Meter" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtreq_meter" MaxLength="6" runat="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    FilterType="Numbers,custom" ValidChars="." TargetControlID="txtreq_meter" />
                            </div>
                            <div id="Div4" runat="server" visible="false" class="form-group ">
                                <label>
                                    Rate</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator4"
                                    ControlToValidate="txtRate" ErrorMessage="Please enter Rate" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtRate" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div id="Div5" runat="server" visible="false" class="form-group ">
                                <label>
                                    Color</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator6"
                                    ControlToValidate="txtColor" ErrorMessage="Please enter Challen No" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtColor" MaxLength="6" runat="server"></asp:TextBox>
                            </div>
                            <div id="Div6" runat="server" visible="false" class="form-group ">
                                <label>
                                    Width</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator5"
                                    ControlToValidate="txtWidth" ErrorMessage="Please enter Width" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtWidth" MaxLength="6" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                           
                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                           
                        </div>

                         <div class="col-lg-3">
                          <div id="Div8"  runat="server" class="col-lg-5">
                            <div style="overflow-y: scroll; width: 315px; height: 244px">
                                 <div class="panel panel-default" style="width: 298px">
                                 <label>Design Code</label>
                                   <%-- <asp:CheckBoxList ID="CheckBoxList1" OnSelectedIndexChanged="chkinvnochanged" AutoPostBack="true" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                        Width="100%" RepeatLayout="Table" Style="overflow: auto">--%>
                                           <asp:CheckBoxList ID="CheckBoxList2" CssClass="chkChoice" OnSelectedIndexChanged="check2_changed" AutoPostBack="true"  RepeatDirection="Horizontal" RepeatColumns="4"  runat="server">
                            </asp:CheckBoxList>
                                    <%--</asp:CheckBoxList>--%>
                                </div>
                            </div>

                        </div>


                            <div class="panel panel-default" style="width: 330px">
                                  
                         
                        </div>
                         </div>
                        <div class="col-lg-2">
                         <div class="panel panel-default" style="width: 150px">
                                   <label>Size</label>
                            <asp:CheckBoxList ID="chkSizes" OnSelectedIndexChanged="ckhsize_index"  AutoPostBack="true"   RepeatDirection="Horizontal" RepeatColumns="2" CssClass="chkChoice" runat="server">
                            </asp:CheckBoxList>
                        </div>
                        </div>
                    </div>
                     <div class="row">
                     </div>
                     <br>
                    <br></br>
                    <br></br>
                    <br>
                    <br></br>
                    <br></br>
                    <div class="row">
                        <div class="col-lg-12" style="margin-top: -35px">
                            <div class="panel-body">
                                <div>
                                    <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                    <table ID="Table1" class="table table-striped table-bordered table-hover" 
                                        width="100%">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server" Height="200" ScrollBars="Both" 
                                                    Width="100%">
                                                    <asp:GridView ID="gvcustomerorder" runat="server" AutoGenerateColumns="False" 
                                                        CssClass="chzn-container" GridLines="None" 
                                                        OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting" 
                                                        ShowFooter="True" Width="100%">
                                                        <HeaderStyle BackColor="#59d3b4" BorderColor="Gray" BorderStyle="Solid" 
                                                            BorderWidth="1px" Font-Names="arial" Font-Size="Smaller" 
                                                            HorizontalAlign="Center" />
                                                        <RowStyle BorderColor="Gray" BorderStyle="Solid" BorderWidth="0.5px" />
                                                        <Columns>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderStyle-Width="2%" 
                                                                HeaderText="S.No" ItemStyle-Width="3%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtno" runat="server" Enabled="false" Text='<%# Eval("num")%>'></asp:TextBox>
                                                                    <asp:Label ID="lblid" runat="server" Text='<%# Eval("transid")%>' 
                                                                        Visible="false"></asp:Label>
                                                                    <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderStyle-Width="5%" 
                                                                HeaderText="Design/Color Code" ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtdesigno" runat="server" Enabled="false" 
                                                                        Text='<%# Eval("design")%>'></asp:TextBox>
                                                                    <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:ImageField ControlStyle-Height="33px" ControlStyle-Width="50px" 
                                                                DataImageUrlField="Imagepath" HeaderStyle-Width="2%" HeaderText="Preview Image" 
                                                                ItemStyle-Width="2%" Visible="false" />
                                                            <asp:TemplateField HeaderStyle-Width="9%" HeaderText="Party Name" 
                                                                ItemStyle-Width="9%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="drpparty" runat="server" CssClass="chzn-select" 
                                                                        Height="26px" Width="100%">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Rate" 
                                                                ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" runat="server" Height="30px" Text='<%# Eval("Rat")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Avaliable meter" 
                                                                ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtmet" runat="server" Enabled="false" 
                                                                        Text='<%# Eval("met")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Required Meter" 
                                                                ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrmeter" runat="server" Height="30px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="36 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txttsfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="36 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txttshs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="38 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txttefs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="38 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txttehs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="39 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txttnfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="39 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txttnhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="40 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtfzfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="40 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtfzhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="42 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtftfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="42 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtfths" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="44 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtfffs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="44 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtffhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.36 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxttsfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.36 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxttshs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.38 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxttefs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.38 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxttehs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.39 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxttnfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.39 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxttnhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.40 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxtfzfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.40 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxtfzhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.42 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxtftfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.42 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxtfths" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.44 FS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxtfffs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" HeaderText="Abs.44 HS">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="abstxtffhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" Visible="false" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td ID="Td1" runat="server" align="right" visible="false">
                                                <asp:Button ID="ButtonAdd1" runat="server" EnableTheming="false" 
                                                    Text="Add New" />
                                            </td>
                                        </tr>
                                        <table ID="Table3" style="margin-top: -36px" width="45%">
                                            <tr>
                                                <td>
                                                    <label>
                                                    <%--Total Qty.--%>
                                                    </label>
                                                    <asp:TextBox ID="totqty" runat="server" CssClass="form-control" Enabled="false" 
                                                        Visible="false"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <label>
                                                    <%--Total Meter.--%>
                                                    </label>
                                                    <asp:TextBox ID="totmeter" runat="server" CssClass="form-control" 
                                                        Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <label>
                                                    <%--Item His.--%>
                                                    </label>
                                                    <asp:TextBox ID="txtitemhis" runat="server" CssClass="form-control" 
                                                        Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <label>
                                                    <%--Cust.His--%>
                                                    </label>
                                                    <asp:TextBox ID="txtcusthis" runat="server" CssClass="form-control" 
                                                        Enabled="false" Visible="false"></asp:TextBox>
                                                </td>
                                                <%-- <td>
                                                        <asp:TextBox ID="txtdamt5" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                    </td>--%>
                                                <td>
                                                    <asp:TextBox ID="txtTamt5" runat="server" CssClass="form-control" 
                                                        Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </table>
                                    <%--</tr>
                                            </tbody>--%>
                                    </td>
                                    </tr>
                                    </tbody>
                                    <table ID="Table2" runat="server" visible="false">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control" 
                                                    Style="width: 110px; margin-left: 46px; margin-top: 11px; text-align: right" 
                                                    Visible="false">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <%--<asp:TextBox CssClass="form-control" ID="txtDiscamt" Visible="false"  Enabled="false" runat="server" style="width: 110px;margin-left: 43px; margin-top:17px; text-align:right" >0</asp:TextBox>--%>
                                                <%-- <asp:Label ID="lblDisc" runat="server" ></asp:Label>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Payment Type
                                            </td>
                                            <td>
                                                Cheque/Card/DD No
                                            </td>
                                            <td>
                                                Amount
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlAgainst" runat="server" CssClass="form-control" 
                                                    Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtchequedd" runat="server" CssClass="form-control" 
                                                    Style="width: 290px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAgainstAmount" runat="server" CssClass="form-control" 
                                                    Style="width: 200px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlAgainst1" runat="server" CssClass="form-control" 
                                                    Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtchequedd1" runat="server" CssClass="form-control" 
                                                    Style="width: 290px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAgainstAmount1" runat="server" CssClass="form-control" 
                                                    Style="width: 200px;">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control" 
                                                    Enabled="false" Style="width: 250px;">Cash</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtchequedd2" runat="server" CssClass="form-control" 
                                                    Style="width: 290px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAgainstAmount2" runat="server" CssClass="form-control" 
                                                    Style="width: 200px;">0</asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <asp:Button ID="Button1" runat="server" AccessKey="s" BorderColor="#e41300" 
                                    BorderStyle="Inset" BorderWidth="3px" class="btn btn-info" OnClick="Add_Click" 
                                    onfocus="this.style.backgroundColor='#1b293e'" 
                                    onmousedown="this.style.backgroundColor='olive'" 
                                    onmouseover="this.style.backgroundColor='#5bc0de'" Text="Save" 
                                    ValidationGroup="val1" Visible="false" Width="120px" />
                                <asp:Button ID="btncalc" runat="server" class="btn btn-info" 
                                    OnClick="call_Click" Style="width: 120px;" Text="Calc." 
                                    ValidationGroup="val1" />
                                <asp:Button ID="btnadd" runat="server" class="btn btn-info" OnClick="Add_Click" 
                                    Style="width: 120px;" Text="Save" ValidationGroup="val1" />
                                <asp:Button ID="btnexit" runat="server" class="btn btn-warning" 
                                    OnClick="Exit_Click" Style="width: 120px;" Text="Exit" />
                            </div>
                        </div>
                    </div>
                    </br>
                     </br>
                </div>
            </div>
        </div>

       </ContentTemplate>
        <Triggers></Triggers>
     </asp:UpdatePanel>

     <asp:UpdateProgress ID="Updateprogress" runat="server" AssociatedUpdatePanelID="Updatepanel1">
        <ProgressTemplate>
        <div  style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; opacity: 0.7;">

        <asp:Image  ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                    position: fixed; top: 50%; left: 40%;"/>
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
