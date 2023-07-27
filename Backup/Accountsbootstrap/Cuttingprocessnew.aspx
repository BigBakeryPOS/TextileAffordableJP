<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cuttingprocessnew.aspx.cs"
    Inherits="Billing.Accountsbootstrap.Cuttingprocessnew" %>

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
                            <div class="form-group ">
                                <asp:RadioButtonList ID="radbtn" OnSelectedIndexChanged="radchecked" AutoPostBack="true"
                                    RepeatColumns="2" runat="server">
                                    <asp:ListItem Text="Single Party" Selected="True" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Multiple Party" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="form-group ">
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
                                <asp:TextBox CssClass="form-control" ID="txtLotNo" MaxLength="6" runat="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="Numbers" ValidChars="" TargetControlID="txtLotNo" />
                            </div>
                           
                            <div runat="server" visible="false" class="form-group ">
                                <label>
                                    Design No</label>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlDNo" ValueToCompare="Select Design Name"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please select Design No!"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlDNo" runat="server" class="form-control" OnSelectedIndexChanged="ddlDNo_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div runat="server" visible="false" class="form-group ">
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
                         <div class="form-group">
                                <label>
                                    Date:</label>
                                <asp:TextBox ID="txtdate" runat="server" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                            <div id="single" runat="server" class="form-group ">
                                <label>
                                    Customer Name</label>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlSupplier"
                                    ValueToCompare="Select Party Name" Operator="NotEqual" Type="String" ErrorMessage="Please select Party name!"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlSupplier" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div runat="server" visible="false" class="form-group ">
                                <label>
                                    Required Meter</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator7"
                                    ControlToValidate="txtMeter" ErrorMessage="Please enter Meter" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtreq_meter" MaxLength="6" runat="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    FilterType="Numbers,custom" ValidChars="." TargetControlID="txtreq_meter" />
                            </div>
                            <div runat="server" visible="false" class="form-group ">
                                <label>
                                    Rate</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator4"
                                    ControlToValidate="txtRate" ErrorMessage="Please enter Rate" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtRate" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div runat="server" visible="false" class="form-group ">
                                <label>
                                    Color</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator6"
                                    ControlToValidate="txtColor" ErrorMessage="Please enter Challen No" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtColor" MaxLength="6" runat="server"></asp:TextBox>
                            </div>
                            <div runat="server" visible="false" class="form-group ">
                                <label>
                                    Width</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator5"
                                    ControlToValidate="txtWidth" ErrorMessage="Please enter Width" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtWidth" MaxLength="6" runat="server" Enabled="false"></asp:TextBox>
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
                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                           
                        </div>
                        <div id="multiple" runat="server" class="col-lg-5">
                            <div style="overflow-y: scroll; width: 407px; height: 244px">
                                <div class="panel panel-default" style="width: 407px; background-color: Silver; padding-left: 0px">
                                    <asp:CheckBoxList ID="chkSupplier" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                        Width="100%" RepeatLayout="Table" Style="overflow: auto">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                            <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                        </div>
                    </div>
                     <div class="row">
                     </div>
                     </br>
                    <div class="row">
                        <div class="col-lg-12" style="margin-top: -35px">
                            <div class="panel-body">
                                <div>
                                    <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                    <table class="table table-striped table-bordered table-hover" id="Table1" width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="160" Width="100%">
                                                    <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                        OnRowDataBound="GridView2_RowDataBound" 
                                                        OnRowDeleting="GridView2_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                        Width="100%" runat="server">
                                                        <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                        <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                HeaderStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <%--    <asp:Label ID="txtno"   runat="server" ></asp:Label>--%>
                                                                    <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Design No" HeaderStyle-Width="19%" ItemStyle-Width="17%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="drpItem" CssClass="chzn-select" runat="server" Height="26px"
                                                                        Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpItem_SelectedIndexChanged"
                                                                        AppendDataBoundItems="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Avaliable meter" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtaemeter" Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Required Meter" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrmeter" OnTextChanged="reqmeter" AutoPostBack="true" Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" Height="30px"  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Color" ControlStyle-Width="100%" ItemStyle-Width="8%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtcolor" runat="server" Height="30px" AutoPostBack="false"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                                                        TargetControlID="txtcolor" ValidChars="." FilterType="Numbers, Custom" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Width" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtwidth" Height="30px" runat="server" AutoPostBack="false">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        
                                                          
                                                          
                                                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td1" runat="server" visible="false" align="right">
                                                <asp:Button ID="ButtonAdd1" runat="server" EnableTheming="false" Text="Add New" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <table id="Table3" style="margin-top: -36px" width="45%">
                                                <tr>
                                                    <td>
                                                        <label>
                                                            <%--Total Qty.--%></label>
                                                        <asp:TextBox Visible="false" ID="totqty" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <%--Total Meter.--%>
                                                        </label>
                                                        <asp:TextBox Visible="false" ID="totmeter" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <%--Item His.--%></label>
                                                        <asp:TextBox Visible="false" ID="txtitemhis" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            <%--Cust.His--%>
                                                        </label>
                                                        <asp:TextBox Visible="false" ID="txtcusthis" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <%-- <td>
                                                        <asp:TextBox ID="txtdamt5" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                    </td>--%>
                                                    <td>
                                                        <asp:TextBox ID="txtTamt5" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                    </table>
                                    <%--</tr>
                                            </tbody>--%>
                                    </td> </tr> </tbody>
                                    <table id="Table2" runat="server" visible="false">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="TextBox13" Visible="false" runat="server"
                                                    Style="width: 110px; margin-left: 46px; margin-top: 11px; text-align: right">0</asp:TextBox>
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
                                                <asp:DropDownList ID="ddlAgainst" runat="server" CssClass="form-control" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtchequedd" runat="server" Style="width: 290px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtAgainstAmount" runat="server" Style="width: 200px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlAgainst1" runat="server" CssClass="form-control" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtchequedd1" runat="server" Style="width: 290px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtAgainstAmount1" runat="server" Style="width: 200px;">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="TextBox18" runat="server" Enabled="false"
                                                    Style="width: 250px;">Cash</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtchequedd2" runat="server" Style="width: 290px;">0</asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" ID="txtAgainstAmount2" runat="server" Style="width: 200px;">0</asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <asp:Button ID="Button1" AccessKey="s" Visible="false" runat="server" class="btn btn-info" BorderWidth="3px"
                                    BorderColor="#e41300" BorderStyle="Inset" OnClick="Add_Click" onmouseover="this.style.backgroundColor='#5bc0de'"
                                    onmousedown="this.style.backgroundColor='olive'" onfocus="this.style.backgroundColor='#1b293e'"
                                    Text="Save" ValidationGroup="val1" Width="120px" />

                                     <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" OnClick="Add_Click"
                                ValidationGroup="val1" Style="width: 120px;" />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"
                                Style="width: 120px;" />
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
