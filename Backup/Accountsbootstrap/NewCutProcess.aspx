<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewCutProcess.aspx.cs"
    Inherits="Billing.Accountsbootstrap.NewCutProcess" %>

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
                Job Work Process</h1>
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
                                    <div class="form-group" id="divcode" runat="server">
                                        <asp:TextBox CssClass="form-control" ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div id="Div2" runat="server" visible="false" class="form-group ">
                                        <label>
                                            ID</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator3"
                                            ControlToValidate="TextBox3" ErrorMessage="Please enter ID" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox3" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            Job Work No</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator1"
                                            ControlToValidate="txtLotNo" ErrorMessage="Please enter Meter" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" Enabled="false" ID="txtLotNo" MaxLength="6"
                                            runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtLotNo" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Job Work Issue Date:</label>
                                        <asp:TextBox ID="txtdate" runat="server" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdate" runat="server"
                                            Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            Select Width</label>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpwidth" ValueToCompare="Select Width"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please select Width!"></asp:CompareValidator>
                                        <asp:DropDownList ID="drpwidth" OnSelectedIndexChanged="drpwidthChange" AutoPostBack="true"
                                            runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            Job Work Master</label>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpjobwork" ValueToCompare="Select Job Work"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please select Job Work Master!"></asp:CompareValidator>
                                        <asp:DropDownList ID="drpjobwork" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div runat="server" class="form-group ">
                                        <label>
                                            Formal</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator2"
                                            ControlToValidate="txtsharp" ErrorMessage="Please enter Sharp" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" ID="txtsharp" MaxLength="6" runat="server">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            FilterType="Numbers,custom" ValidChars="." TargetControlID="txtsharp" />
                                    </div>
                                    <div runat="server" class="form-group ">
                                        <label>
                                            Casual</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator4"
                                            ControlToValidate="txtexec" ErrorMessage="Please enter Margin" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" ID="txtexec" MaxLength="6" runat="server">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            FilterType="Numbers,custom" ValidChars="." TargetControlID="txtexec" />
                                    </div>
                                    <div runat="server" visible="false" class="form-group ">
                                        <label>
                                            Fit</label>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlFit" ValueToCompare="Select Fit"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please select Fit!"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlFit" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div id="Div4" visible="false" runat="server" class="form-group ">
                                        <label>
                                            Production Cost</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator8"
                                            ControlToValidate="txtprod" ErrorMessage="Please enter Production Cost" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" ID="txtprod" MaxLength="6" runat="server">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            FilterType="Numbers,custom" ValidChars="." TargetControlID="txtprod" />
                                    </div>
                                    <div id="Div11" visible="false" runat="server" class="form-group ">
                                        <label>
                                            Margin</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator9"
                                            ControlToValidate="txtmargin" ErrorMessage="Please enter Margin" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" ID="txtmargin" MaxLength="6" runat="server">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                            FilterType="Numbers,custom" ValidChars="." TargetControlID="txtmargin" />
                                    </div>

                                    <div id="Div12" visible="false" runat="server" class="form-group ">
                                        <label>
                                            MRP</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator10"
                                            ControlToValidate="txtmrp" ErrorMessage="Please enter Margin" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" ID="txtmrp" MaxLength="6" runat="server">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                            FilterType="Numbers,custom" ValidChars="." TargetControlID="txtmrp" />
                                    </div>
                                    <div id="Div5" runat="server" visible="false" class="form-group ">
                                        <label>
                                            Adjustment Meter</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator5"
                                            ControlToValidate="txtadjmeter" ErrorMessage="Please enter Margin" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" ID="txtadjmeter" MaxLength="6" runat="server">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                            FilterType="Numbers,custom" ValidChars="." TargetControlID="txtadjmeter" />
                                    </div>
                                    <div id="Div77" visible="false" class="form-group " runat="server">
                                        <label>
                                            Min Meter:</label>
                                        <asp:Label ID="lblmin" runat="server"></asp:Label>
                                    </div>
                                    <div id="Div78" visible="false" class="form-group" runat="server">
                                        <label>
                                            Max Meter:</label>
                                        <asp:Label ID="lblmax" runat="server"></asp:Label>
                                    </div>
                                     <div id="Div6" visible="false" class="form-group " runat="server">
                                        <asp:RadioButtonList ID="radcuttype"  RepeatColumns="2" OnSelectedIndexChanged="radcuttype_selectedindex" AutoPostBack="true" runat="server">
                                            <asp:ListItem Text="single Cutting" Selected="True" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Bulk Cutting" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="Div7" runat="server" class="col-lg-5">
                                        <div style="overflow-y: scroll; width: 284px; height: 170px">
                                            <div class="panel panel-default" style="width: 265px">
                                                <label>
                                                    Fabric Register Number</label>
                                                <asp:CheckBoxList ID="chkinvno" OnSelectedIndexChanged="chkinvnochanged" CssClass="chkChoice"
                                                    AutoPostBack="true" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                                    RepeatLayout="Table" Style="overflow: auto">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                                        <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                                    </div>
                                    <div id="Div10" visible="false" runat="server" class="col-lg-9">
                                        <div style="overflow-y: scroll; width: 315px; height: 244px">
                                            <div class="panel panel-default" style="width: 298px">
                                                <label>
                                                    Design Code</label>
                                                <%-- <asp:CheckBoxList ID="CheckBoxList1" OnSelectedIndexChanged="chkinvnochanged" AutoPostBack="true" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                        Width="100%" RepeatLayout="Table" Style="overflow: auto">--%>
                                                <asp:CheckBoxList ID="CheckBoxList2" CssClass="chkChoice" OnSelectedIndexChanged="check2_changed"
                                                    AutoPostBack="true" RepeatDirection="Horizontal" RepeatColumns="4" runat="server">
                                                </asp:CheckBoxList>
                                                <%--</asp:CheckBoxList>--%>
                                            </div>
                                        </div>
                                    </div>
                                   
                                </div>
                                <div class="col-lg-3">
                                    <div id="Div1" runat="server" class="form-group ">
                                        <asp:RadioButtonList ID="radbtn" OnSelectedIndexChanged="radchecked" AutoPostBack="true"
                                            RepeatColumns="2" runat="server">
                                            <asp:ListItem Text="Single Brand" Selected="True" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Multiple Brand" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="sing" runat="server">
                                        <div class="form-group " runat="server" visible="false">
                                            <label>
                                                Customer Name</label>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlSupplier"
                                                ValueToCompare="Select Party Name" Operator="NotEqual" Type="String" ErrorMessage="Please select Party name!"></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlSupplier" OnSelectedIndexChanged="supplierfill" AutoPostBack="true"
                                                runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group ">
                                            <label>
                                                Brand</label>
                                                <asp:CompareValidator ID="CompareValidator7" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBrand"
                                                ValueToCompare="Select Brand Name" Operator="NotEqual" Type="String" ErrorMessage="Please select Brand name!"></asp:CompareValidator>
                                                <asp:DropDownList ID="ddlBrand" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="brandfill"
                                                CssClass="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="form-group ">
                                            <label>
                                                Fit Label</label>
                                            <asp:CompareValidator ID="CompareValidator6" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="Sddrrpfit" ValueToCompare="Select Fit"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please select Fit name!"></asp:CompareValidator>
                                            <asp:DropDownList ID="Sddrrpfit" CssClass="chzn-select" OnSelectedIndexChanged="Sfitchaged"
                                                AutoPostBack="true" runat="server" Height="26px" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                        <div  class="form-group ">
                                            <label>
                                                Main Label</label>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drplab" ValueToCompare="Select Label"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please select Label name!"></asp:CompareValidator>
                                            <asp:DropDownList ID="drplab" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group ">
                                            <label>
                                                Fit Label</label>
                                            <asp:CheckBox ID="chkfit" runat="server" />
                                        </div>
                                        <div class="form-group ">
                                            <label>
                                                Wash Care Label</label>
                                            <asp:CheckBox ID="Chkwash" runat="server" />
                                        </div>
                                        <div class="form-group ">
                                            <label>
                                                Logo Embrodiery</label>
                                            <asp:CheckBox ID="Chllogo" runat="server" />
                                        </div>
                                        <div runat="server" visible="false" class="form-group ">
                                            <label>
                                                Margin</label>
                                            <asp:TextBox ID="Stxtmargin" Width="20%" runat="server">0</asp:TextBox>
                                        </div>

                                        

                                    </div>
                                    <div id="mul" runat="server">
                                        <div id="Div3" visible="false" runat="server" class="col-lg-5">
                                            <div style="overflow-y: scroll; width: 290px; height: 170px">
                                                <div class="panel panel-default" style="width: 272px">
                                                    <label>
                                                        Select Customer</label>
                                                    <asp:CheckBoxList ID="chkcust" OnSelectedIndexChanged="chkgridview" AutoPostBack="true"
                                                        CssClass="chkChoice" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                        Width="100%" RepeatLayout="Table" Style="overflow: auto">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            
                                        </div>
                                        <div id="Div8" visible="true" runat="server" class="col-lg-5">
                                        <div style="overflow-y: scroll; width: 290px; height: 170px">
                                                <div class="panel panel-default" style="width: 272px">
                                                    <label>
                                                        Select Brand</label>
                                                    <asp:CheckBoxList ID="chckbrand" OnSelectedIndexChanged="chkgridview" AutoPostBack="true"
                                                        CssClass="chkChoice" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                        Width="100%" RepeatLayout="Table" Style="overflow: auto">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            </div>
                                        <div class="form-group">
                                            <asp:GridView ID="grdcust" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="Grdcust_RowDataBound"
                                                CssClass="chzn-container" GridLines="None" Width="100%" runat="server">
                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cust. name" ControlStyle-Width="100%" Visible="false" ItemStyle-Width="3%"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcust" Enabled="false" Text='<%# Eval("BrandName")%>' runat="server"></asp:TextBox>
                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Brand name" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtbrand" Enabled="false" Text='<%# Eval("BrandName")%>' runat="server"></asp:TextBox>
                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Main Label" HeaderStyle-Width="9%" Visible="false" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drrplab" CssClass="chzn-select" runat="server" Height="26px"
                                                                Width="100%">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Main Label" HeaderStyle-Width="9%" Visible="false" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drrpbrand" CssClass="chzn-select" runat="server" Height="26px"
                                                                Width="100%">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Mchkfit" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Wash" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Mchkwash" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Logo" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Mchklogo" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Margin" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtmargin" Height="30px" runat="server">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fit" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddrrpfit" CssClass="chzn-select" runat="server" Height="26px"
                                                                Width="100%">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    
                                </div>
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <div class="col-lg-3">
                                    <div class="panel panel-default" style="width: 150px">
                                        <label>
                                            Size</label>
                                        <asp:CheckBoxList ID="chkSizes" OnSelectedIndexChanged="ckhsize_index" AutoPostBack="true"
                                            RepeatDirection="Horizontal" RepeatColumns="2" CssClass="chkChoice" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                    <div visible="false" runat="server" class="col-lg-6">
                                        <div class="form-group">
                                            <label style="margin-left: -15px">
                                                Remaining Meters</label>
                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="txtremameter" runat="server"
                                                MaxLength="4" Style="text-align: right; width: 140px;">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div visible="false" runat="server" class="col-lg-6">
                                        <div class="form-group">
                                            <label>
                                                Remaining Shirts</label>
                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="txtremashirt" runat="server"
                                                MaxLength="8" Style="text-align: right; width: 140px;">0</asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                            </br>
                            <table runat="server"   class="table" style="background-color: #ffb85f">
                                <tr>
                                    <td>
                                        <label>
                                            Design - Color</label>
                                        <asp:DropDownList ID="dddldesign" Width="150px" runat="server" CssClass="form-control"
                                            OnSelectedIndexChanged="dddldesignchanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label>
                                            Rate</label>
                                        <asp:TextBox ID="txtDesignRate" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Available Mtr</label>
                                        <asp:TextBox ID="txtAvailableMtr" Enabled="false" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            No. of Shirts</label>
                                        <asp:TextBox ID="txtNoofShirts" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Issued Mtr</label>
                                        <asp:TextBox ID="txtReqMtr" OnTextChanged="reqchanged" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            No. of Shirts</label>
                                        <asp:TextBox ID="txtReqNoShirts" runat="server" Enabled="false" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td runat="server" visible="false">
                                        <label>
                                            MAX. Shirts</label>
                                        <asp:TextBox ID="txtextrashirt" Enabled="false" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td runat="server" visible="false">
                                        <label>
                                            MIN. Shirts</label>
                                        <asp:TextBox ID="txtminshirt" Enabled="false" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td visible="false" runat="server">
                                        <label>
                                            Party</label><br />
                                        <asp:RadioButton ID="rdSingle" runat="server" GroupName="rdPar" Text="Single" OnCheckedChanged="rdSingle_CheckedChanged"
                                            AutoPostBack="true" /><br />
                                        <asp:RadioButton ID="rdMultiple" runat="server" GroupName="rdPar" Text="Multiple"
                                            OnCheckedChanged="rdMultiple_CheckedChanged" AutoPostBack="true" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblcatid" runat="server"></asp:Label>
                                        <asp:Label ID="lblSubcatid" runat="server"></asp:Label>
                                        <asp:Label ID="stockid" runat="server"></asp:Label>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table class="table table-striped table-bordered table-hover" style="background-color: #ffb85f">
                                <tr id="tr4" runat="server">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div runat="server" align="right">
                                                    <asp:Button ID="btnavgsize" runat="server" Text="calc." OnClick="callcclick" />
                                                </div>
                                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="200" Width="100%">
                                                    <asp:GridView ID="gridsize" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gridsize_RowDataBound"
                                                        OnRowDeleting="gridsize_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                        Width="100%" Height="25px" runat="server">
                                                        <HeaderStyle BackColor="#F9F9F9" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                        <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Party Name" ControlStyle-Width="100%" ItemStyle-Width="6%" Visible="false"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddrparty" OnSelectedIndexChanged="ddrpartyselected_changed"
                                                                        AutoPostBack="true" runat="server" CssClass="chzn-select">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Brand Name" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddrbrand" OnSelectedIndexChanged="ddrbrandselected_changed"
                                                                        AutoPostBack="true" runat="server" CssClass="chzn-select">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                HeaderStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddrpfit" Enabled="false" runat="server" CssClass="chzn-select">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="36 FS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxttsfs" OnTextChanged="change36fs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="38 FS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxttefs" OnTextChanged="change38fs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="39 FS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxttnfs" OnTextChanged="change39fs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="40 FS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxtfzfs" OnTextChanged="change40fs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="42 FS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxtftfs" OnTextChanged="change42fs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="44 FS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxtfffs" OnTextChanged="change44fs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="36 HS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxttshs" OnTextChanged="change36hs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="38 HS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxttehs" OnTextChanged="change38hs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="39 HS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxttnhs" OnTextChanged="change39hs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="40 HS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxtfzhs" OnTextChanged="change40hs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="42 HS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxtfths" OnTextChanged="change42hs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="44 HS" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxtffhs" OnTextChanged="change44hs" AutoPostBack="true" runat="server"
                                                                        Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WSP" Visible="false" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxtwsp" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req.Meter" Visible="false" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxtreqmeter" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Avg.Size" Visible="false" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="avgsize" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Shirts" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="dtxtshirt" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="dbtnadd" ImageUrl="~/images/edit.png" runat="server" OnClick="ButtonAdd1_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField Visible="false" ShowDeleteButton="True" ButtonType="Button" />
                                                            <%--  <asp:TemplateField HeaderText="Rate" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" Height="30px" Text='<%# Eval("Rate")%>'  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Avaliable meter" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                     <asp:TextBox ID="txtmet" Enabled="false" Text='<%# Eval("meter")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Shirt" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtshirt" Height="30px" Text='<%# Eval("Shirt")%>'  runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText="Required Meter" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txteqrmeter" Text='<%# Eval("reqmeter")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Required Shirt" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtreqshirt" Text='<%# Eval("reqshirt")%>'   Height="30px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                               <asp:TemplateField HeaderText="Fit" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                <ItemTemplate>
                                                                  <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("Fitid")%>' runat="server" ></asp:TextBox>
                                                                   <asp:TextBox ID="txtfit" Enabled="false" Text='<%# Eval("Fit")%>' runat="server" ></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr id="tr1" visible="false" runat="server">
                                    <td>
                                        <label runat="server" visible="false">
                                            Party Name</label>
                                        <asp:DropDownList ID="drpCustomer" Visible="false" runat="server" Enabled="false" Width="150px" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <label>
                                            Brand Name</label>
                                        <asp:DropDownList ID="drpBrand" runat="server" Enabled="false" Width="150px" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <label>
                                            Fit</label>
                                        <asp:DropDownList ID="drpFit" OnSelectedIndexChanged="drpfitchanged" Enabled="false"
                                            AutoPostBack="true" runat="server" CssClass="form-control" Width="150px">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td id="tsfs" runat="server">
                                        <label>
                                            36 FS</label>
                                        <asp:TextBox ID="txt36FS" OnTextChanged="Schange36fs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="tefs" runat="server">
                                        <label>
                                            38 FS</label>
                                        <asp:TextBox ID="txt38FS" runat="server" OnTextChanged="Schange38fs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="tnfs" runat="server">
                                        <label>
                                            39 FS</label>
                                        <asp:TextBox ID="txt39FS" runat="server" OnTextChanged="Schange39fs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="fzfs" runat="server">
                                        <label>
                                            40 FS</label>
                                        <asp:TextBox ID="txt40FS" runat="server" OnTextChanged="Schange40fs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="ftfs" runat="server">
                                        <label>
                                            42 FS</label>
                                        <asp:TextBox ID="txt42FS" runat="server" OnTextChanged="Schange42fs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="fffs" runat="server">
                                        <label>
                                            44 FS</label>
                                        <asp:TextBox ID="txt44FS" runat="server" OnTextChanged="Schange44fs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="tshs" runat="server">
                                        <label>
                                            36 HS</label>
                                        <asp:TextBox ID="txt36HS" runat="server" OnTextChanged="Schange36hs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="tehs" runat="server">
                                        <label>
                                            38 HS</label>
                                        <asp:TextBox ID="txt38HS" runat="server" OnTextChanged="Schange38hs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="tnhs" runat="server">
                                        <label>
                                            39 HS</label>
                                        <asp:TextBox ID="txt39HS" runat="server" OnTextChanged="Schange39hs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="fzhs" runat="server">
                                        <label>
                                            40 HS</label>
                                        <asp:TextBox ID="txt40HS" runat="server" OnTextChanged="Schange40hs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="fths" runat="server">
                                        <label>
                                            42 HS</label>
                                        <asp:TextBox ID="txt42HS" runat="server" OnTextChanged="Schange42hs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="ffhs" runat="server">
                                        <label>
                                            44 HS</label>
                                        <asp:TextBox ID="txt44HS" runat="server" OnTextChanged="Schange44hs" AutoPostBack="true"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td Visible="false" runat="server">
                                        <label>
                                            WSP</label>
                                        <asp:TextBox ID="Stxtwsp" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td Visible="false" runat="server">
                                        <label>
                                            Aval.Meter</label>
                                        <asp:TextBox ID="txtavamet1" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Tot.Shirts</label>
                                        <asp:TextBox ID="txttotshirt1" Enabled="false" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td Visible="false" runat="server">
                                        <label>
                                            Avg.Size</label>
                                        <asp:TextBox ID="txtavvgmeter" Enabled="false" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="addsingle" runat="server">
                                        <label>
                                            Add</label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="Addfirst" CssClass="img-responsive"
                                            ImageUrl="~/images/edit_add.png" EnableViewState="true" />
                                    </td>
                                </tr>
                                <tr visible="false" runat="server">
                                    <td>
                                        <label>
                                            Party Name</label>
                                        <asp:DropDownList ID="drpCustomer2" runat="server" Width="150px" Enabled="false"
                                            CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label>
                                            Fit</label>
                                        <asp:DropDownList ID="drpFit2" runat="server" Enabled="false" CssClass="form-control"
                                            Width="150px">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label>
                                            36 FS</label>
                                        <asp:TextBox ID="txt36FS2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            38 FS</label>
                                        <asp:TextBox ID="txt38FS2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            40 FS</label>
                                        <asp:TextBox ID="txt40FS2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            42 FS</label>
                                        <asp:TextBox ID="txt42FS2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            36 HS</label>
                                        <asp:TextBox ID="txt36HS2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            38 HS</label>
                                        <asp:TextBox ID="txt38HS2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            40 HS</label>
                                        <asp:TextBox ID="txt40HS2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            42 HS</label>
                                        <asp:TextBox ID="txt42HS2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Aval.Meter</label>
                                        <asp:TextBox ID="txtavamet2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Tot.Shirts</label>
                                        <asp:TextBox ID="txttotshirt2" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Add</label>
                                        <asp:ImageButton ID="ImageButton2" runat="server" OnClick="Addsecond" CssClass="img-responsive"
                                            ImageUrl="~/images/edit_add.png" EnableViewState="true" />
                                    </td>
                                </tr>
                                <tr visible="false" runat="server">
                                    <td>
                                        <label>
                                            Party Name</label>
                                        <asp:DropDownList ID="drpCustomer3" runat="server" Width="150px" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label>
                                            Fit</label>
                                        <asp:DropDownList ID="drpFit3" runat="server" CssClass="form-control" Width="150px">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label>
                                            36 FS</label>
                                        <asp:TextBox ID="txt36FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            38 FS</label>
                                        <asp:TextBox ID="txt38FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            39 FS</label>
                                        <asp:TextBox ID="txt39FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            40 FS</label>
                                        <asp:TextBox ID="txt40FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            42 FS</label>
                                        <asp:TextBox ID="txt42FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            44 FS</label>
                                        <asp:TextBox ID="txt44FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            36 HS</label>
                                        <asp:TextBox ID="txt36HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            38 HS</label>
                                        <asp:TextBox ID="txt38HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            39 HS</label>
                                        <asp:TextBox ID="txt39HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            40 HS</label>
                                        <asp:TextBox ID="txt40HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            42 HS</label>
                                        <asp:TextBox ID="txt42HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            44 HS</label>
                                        <asp:TextBox ID="txt44HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Aval.Meter</label>
                                        <asp:TextBox ID="txtavamet3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Tot.Shirts</label>
                                        <asp:TextBox ID="txttotshirt3" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Add</label>
                                        <asp:ImageButton ID="ImageButton3" runat="server" CssClass="img-responsive" ImageUrl="~/images/edit_add.png"
                                            EnableViewState="true" />
                                        <asp:Button ID="btlrecal" Visible="false" runat="server" class="btn btn-info" Text="Calc"
                                            OnClick="Recalclick" ValidationGroup="val1" Style="width: 120px; margin-left: 1000px" />
                                    </td>
                                </tr>
                                <asp:Button ID="btnprocessall" runat="server" class="btn btn-info" Text="Process-All"
                                    OnClick="processclickallnew" ValidationGroup="val1" Style="width: 120px; margin-left: 1000px;
                                    margin-bottom: -32px" />
                                <asp:Button ID="btnprocess" runat="server" class="btn btn-info" Text="Process" OnClick="processclick"
                                    ValidationGroup="val1" Style="width: 120px; margin-left: 1136px" />
                                     <asp:Button ID="btngohead" runat="server" class="btn btn-info" Text="GO Head" OnClick="GOheadprocessclick"
                                    ValidationGroup="val1" Style="width: 120px; margin-left: 1136px" />
                            </table>
                            <div class="row">
                                <div class="col-lg-12" style="margin-top: -35px">
                                    <div class="panel-body">
                                        <div>
                                            <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                            <table class="table table-striped table-bordered table-hover" id="Table1" width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="200" Width="100%">
                                                            <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                                OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                                CssClass="chzn-container" GridLines="None" Width="100%" runat="server">
                                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                                        HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <%--  <asp:TextBox ID="txtno" Enabled="false" Text='<%# Eval("num")%>' runat="server" ></asp:TextBox>--%>
                                                                            <asp:Label ID="lblid" Visible="false" Text='<%# Eval("transid")%>' runat="server"></asp:Label>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Design/Color Code" ControlStyle-Width="100%" ItemStyle-Width="15%"
                                                                        HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdesigno" Enabled="false" Text='<%# Eval("design")%>' runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Brand Name" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtbrandid" Visible="false" Text='<%# Eval("brandid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtbrand" Enabled="false" Text='<%# Eval("brand")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Party Name" Visible="false" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtledgerid" Visible="false" Text='<%# Eval("ledgerid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtparty" Enabled="false" Text='<%# Eval("party")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rate"  ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtRate" Enabled="false" Height="30px" Text='<%# Eval("Rate")%>'
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Avaliable meter" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtmet" Enabled="false" Text='<%# Eval("meter")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Shirt" ControlStyle-Width="100%" Visible="false" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtshirt" Height="30px" Text='<%# Eval("Shirt")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="tctextra" Height="30px" Text='<%# Eval("Extra")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Required Meter" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txteqrmeter" Enabled="false" Text='<%# Eval("reqmeter")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Avg.Size " Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtavgsize" Enabled="false" Text='<%# Eval("avgsize")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                    <asp:TemplateField HeaderText="Fit" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("Fitid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtfit" Enabled="false" Text='<%# Eval("Fit")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttsfs" Enabled="false" Text='<%# Eval("TSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="38 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttefs" Enabled="false" Text='<%# Eval("TEFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="39 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttnfs" Enabled="false" Text='<%# Eval("TNFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="40 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfzfs" Enabled="false" Text='<%# Eval("FZFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="42 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtftfs" Enabled="false" Text='<%# Eval("FTFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="44 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfffs" Enabled="false" Text='<%# Eval("FFFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttshs" Enabled="false" Text='<%# Eval("TSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="38 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttehs" Enabled="false" Text='<%# Eval("TEHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="39 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttnhs" Enabled="false" Text='<%# Eval("TNHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="40 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfzhs" Enabled="false" Text='<%# Eval("FZHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="42 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfths" Enabled="false" Text='<%# Eval("FTHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="44 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtffhs" Enabled="false" Text='<%# Eval("FFHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Total Shirt"  ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtreqshirt" Enabled="false" Text='<%# Eval("reqshirt")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="WSP" Visible="false" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtwwsp" Enabled="false" Text='<%# Eval("WSP")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="42 HS" Visible="false" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="selectcust" Text='<%# Eval("LLedger")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                            <asp:TextBox ID="mainllab" Text='<%# Eval("Mainlab")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                            <asp:TextBox ID="fitllab" Text='<%# Eval("FItLab")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                            <asp:TextBox ID="washllab" Text='<%# Eval("Washlab")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                            <asp:TextBox ID="logollab" Text='<%# Eval("Logolab")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- <asp:TemplateField HeaderText="Abs.36 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxttsfs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Abs.36 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxttshs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                 <asp:TemplateField HeaderText="Abs.38 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxttefs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Abs.38 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxttehs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                 <asp:TemplateField HeaderText="Abs.39 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxttnfs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Abs.39 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxttnhs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Abs.40 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxtfzfs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                <asp:TemplateField HeaderText="Abs.40 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxtfzhs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Abs.42 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxtftfs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                <asp:TemplateField HeaderText="Abs.42 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxtfths" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="Abs.44 FS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxtfffs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Abs.44 HS" ControlStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="abstxtffhs" runat="server" Height="26px" >0</asp:TextBox>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                    <asp:CommandField Visible="false" ShowDeleteButton="True" ButtonType="Button" />
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
                                        <asp:Button ID="Button1" AccessKey="s" Visible="false" runat="server" class="btn btn-info"
                                            BorderWidth="3px" BorderColor="#e41300" BorderStyle="Inset" OnClick="Add_Click"
                                            onmouseover="this.style.backgroundColor='#5bc0de'" onmousedown="this.style.backgroundColor='olive'"
                                            onfocus="this.style.backgroundColor='#1b293e'" Text="Save" ValidationGroup="val1"
                                            Width="120px" />
                                        <asp:Button ID="btncalc" runat="server" Visible="false" class="btn btn-info" Text="Calc."
                                            OnClick="call_Click" ValidationGroup="val1" Style="width: 120px;" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                    right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                        AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                        position: fixed; top: 50%; left: 40%;" />
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
