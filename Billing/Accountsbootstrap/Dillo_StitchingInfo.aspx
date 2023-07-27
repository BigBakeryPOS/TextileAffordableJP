<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Dillo_StitchingInfo.aspx.cs"
    Inherits="Billing.Accountsbootstrap.Dillo_StitchingInfo" %>

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
    <title>Flexible Apparels || Stitching Info</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
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
    <style type="text/css">
        .style1
        {
            font-size: 12px;
            font-family: Verdana;
        }
        .style2
        {
            height: 120px;
        }
        
        
        .myleft
        {
            border-collapse: collapse;
            width: 85%;
            margin-left: 0px;
            border: 1px solid gray;
            overflow: hidden;
        }
        
        
        
        .myleft tr th
        {
            padding: 8px;
            color: Black;
            border: 1px solid gray;
            font-family: Arial;
            font-size: 10pt;
            text-align: center;
        }
        
        
        
        
        
        .myleft tr:nth-child(even)
        {
        }
        
        
        
        .myleft tr:nth-child(odd)
        {
        }
        
        
        
        .myleft td
        {
            border: 1px solid gray;
            padding: 8px;
        }
    </style>
</head>
<body style="background-color: #c6efce;">
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" Visible="false" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002;">
                Stitching Details
            </h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row" style="background-color: #c6efce;">
        <div class="col-lg-12" style="background-color: #c6efce;">
            <div class="panel panel-default">
                <div style="background-color: #c6efce;" class="panel-body">
                    <form id="Form1" runat="server">
                    <%--   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                    <div class="form-group">
                        <div id="add" runat="server" class="row">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Process Date</label>
                                    <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtProcessDate" runat="server"
                                        MaxLength="150"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtProcessDate"
                                        runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Design Number</label>
                                    <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtdesignnumber" runat="server"
                                        MaxLength="150"></asp:TextBox>
                                        </div>
                                <%--<div class="form-group" id="divcode" runat="server"  Visible="false">
                                <label>
                                    Manual Lot No</label>
                                <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server"></asp:TextBox>
                            </div>--%>
                                <div class="form-group" id="divmanual" runat="server">
                                    <label>
                                        Manual Lot No</label>
                                    <asp:CheckBox ID="chckManualLot" MaxLength="150" runat="server" OnCheckedChanged="Change_ManualLot"
                                        AutoPostBack="true"></asp:CheckBox>
                                </div>
                                <div class="form-group" id="divManualLot" runat="server" visible="false">
                                    <label>
                                        Manual Lot No</label>
                                    <asp:TextBox CssClass="form-control" ID="txtManualLotno" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group" id="divlotNo" runat="server">
                                    <label>
                                        Lot No</label>
                                    <%--   <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1" ControlToValidate="ddlLotNo"
                                    Text="*" ErrorMessage="Please Select Lot No!" Style="color: Red" />--%>
                                    <asp:DropDownList CssClass="form-control" ID="ddlLotNo" MaxLength="150" runat="server"
                                        OnSelectedIndexChanged="StitchingInfo_Load" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group " runat="server" visible="true" id="divCuttingMasterText">
                                    <label>
                                        Cutting Master</label>
                                    <asp:TextBox CssClass="form-control" ID="txtCuttingMaster" MaxLength="150" Enabled="false"
                                        runat="server"></asp:TextBox>
                                    <asp:TextBox CssClass="form-control" ID="txtledgerid" MaxLength="150" Visible="false"
                                        runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group " runat="server" visible="false" id="divCuttingMaster">
                                    <label>
                                        Cutting Master</label>
                                    <%--  <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpCutting" ValueToCompare="Select Job Work"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please select Job Work Master!"></asp:CompareValidator>--%>
                                    <asp:DropDownList ID="drpCutting" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group" runat="server" id="divBrandText">
                                    <label>
                                        Brand Name</label>
                                    <asp:TextBox CssClass="form-control" ID="txtBrand" MaxLength="150" Enabled="false"
                                        runat="server"></asp:TextBox>
                                    <asp:TextBox CssClass="form-control" ID="txtbrandid" Visible="false" MaxLength="150"
                                        Enabled="false" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group " runat="server" id="divBrand">
                                    <label>
                                        Brand</label>
                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlBrand" ValueToCompare="Select Brand Name"
                                        Operator="NotEqual" Type="String" ErrorMessage="Please select Brand name!"></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlBrand" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="brandfill"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </div>

                                <div class="panel panel-default" runat="server" id="divchcksizeManual" style="width: 150px">
                                    <label>
                                        Size</label>
                                    <asp:CheckBoxList ID="chkSizes" Enabled="false" AutoPostBack="true" RepeatDirection="Horizontal"
                                        RepeatColumns="2" CssClass="chkChoice" runat="server">
                                    </asp:CheckBoxList>
                                </div>

                            </div>
                            <!-- /.col-lg-6 (nested) -->
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Unit Name</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                                        ControlToValidate="ddlUnit" Text="*" ErrorMessage="Please Select Unit No!" Style="color: Red" />
                                    <asp:DropDownList CssClass="form-control" ID="ddlUnit" MaxLength="150" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div id="divfull" class="form-group" runat="server">
                                    <label>
                                        Full Qty</label>
                                    <asp:TextBox CssClass="form-control" ID="txtfull" OnTextChanged="Change_TotalFull"
                                        AutoPostBack="true" MaxLength="30" Text="0" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div id="divHalf" class="form-group" runat="server">
                                    <label>
                                        Half Qty</label>
                                    <asp:TextBox CssClass="form-control" ID="txtHalf" OnTextChanged="Change_TotalHalf"
                                        AutoPostBack="true" MaxLength="30" Text="0" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div id="divPiece" class="form-group" runat="server">
                                    <label>
                                        Total Quantity</label>
                                    <asp:TextBox CssClass="form-control" Enabled="false" ID="txtTotalQantity" MaxLength="30"
                                        Text="0" runat="server"></asp:TextBox>
                                </div>
                                <div class="panel panel-default" runat="server" id="divCheckProcess" style="width: 150px">

                                <asp:CheckBox ID="CheckChecking"  MaxLength="150" runat="server">
                                    </asp:CheckBox>
                                    <label>Checking</label><br />

                                    <asp:CheckBox ID="CheckKaja"  MaxLength="150" runat="server">
                                    </asp:CheckBox>
                                    <label>
                                        Kaja</label><br />
                                    <asp:CheckBox ID="CheckEmbroiding" MaxLength="150" runat="server"></asp:CheckBox>
                                    <label>
                                        Embroiding</label><br />



                                         <asp:CheckBox ID="CheckTrimming"  MaxLength="150" runat="server">
                                    </asp:CheckBox>
                                    <label>Trimming</label><br />


                                    <asp:CheckBox ID="CheckWashing" MaxLength="150" runat="server"></asp:CheckBox>
                                    <label>
                                        Washing</label><br />
                                        <asp:CheckBox ID="CheckIron" MaxLength="150" runat="server"></asp:CheckBox>
                                    <label>
                                        Ironing & Packing</label>
                                </div>


                                  
                                  


                                <div class="panel panel-default" id="divchcksize" runat="server" style="width: 1000px">
                                    <%--<label>Size</label>
                                <asp:CheckBoxList ID="chkSizes" RepeatDirection="Horizontal"  Enabled="false" RepeatColumns="2" CssClass="chkChoice" runat="server">
                                        </asp:CheckBoxList>--%>
                                    <label style="font-weight: bold;display:none;">
                                        Size Details</label>

                                         
                              
                                    <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                                        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                        ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                                        Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                          
                                             <asp:TemplateField HeaderText="SIZE" HeaderStyle-Width="10%" >
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txtfit" Enabled="false" Width="100%" Text='<%# Eval("fitt")%>' runat="server" CssClass="form-control" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="36" HeaderStyle-Width="10%" >
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txtts" Enabled="false" Width="100%" Text='<%# Eval("ts")%>' runat="server" CssClass="form-control" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="38" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txtte" Enabled="false" Width="100%" Text='<%# Eval("te")%>' runat="server" CssClass="form-control" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="39" HeaderStyle-Width="10%" >
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txttn" Enabled="false" Width="100%" Text='<%# Eval("tn")%>' runat="server" CssClass="form-control" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="40" HeaderStyle-Width="10%" >
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txtfz" Enabled="false" Width="100%" Text='<%# Eval("fz")%>' runat="server" CssClass="form-control" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="42" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txtft" Enabled="false" Width="100%" Text='<%# Eval("ft")%>' runat="server" CssClass="form-control" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="44" HeaderStyle-Width="10%" >
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txtff" Enabled="false" Width="100%" Text='<%# Eval("ff")%>' runat="server" CssClass="form-control" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" HeaderStyle-Width="12%" >
                                                        <ItemTemplate>
                                                           <asp:TextBox ID="txttot" Enabled="false" Width="100%" Text='<%# Eval("tot")%>' runat="server" CssClass="form-control" ></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                          <%--  <asp:BoundField HeaderText="36" DataField="ts" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="38" DataField="te" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="39" DataField="tn" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="40" DataField="fz" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="42" DataField="ft" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="44" DataField="ff" ItemStyle-HorizontalAlign="Center" />--%>
                                            <%--  <asp:BoundField HeaderText="36HS" DataField="ts" />
                                        <asp:BoundField HeaderText="38HS" DataField="te" />
                                        <asp:BoundField HeaderText="39HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44HS" DataField="ffhs" />--%>
                                         <%--   <asp:BoundField HeaderText="Total" DataField="tot" ItemStyle-HorizontalAlign="Center" />--%>
                                            <%--  <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:BoundField HeaderText="Total Shirt" DataField="tott" />--%>
                                        </Columns>
                                    </asp:GridView>



                                </div>

                            </div>

                            <div class="col-lg-8">
                            
                            <div style="overflow:scroll; height:360px">
                            <!-- /.panel-heading -->
                          <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                                    <div class="panel-body" style="height: 350px">
                                        <div class="table-responsive">
                                            <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                            <table id="Table1" width="100%">
                                                <tr>
                                                    <td colspan="7">
                                                        <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                            OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                            CssClass="chzn-container" Width="80%" Style="overflow: scroll;"
                                                            runat="server">
                                                            <HeaderStyle BackColor="#59d3b4" />
                                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="10%">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Process Type" ItemStyle-Width="40%">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="drpProcess" Width="100%" runat="server" Height="26px" AppendDataBoundItems="true">
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lbltransno" runat="server" CssClass="hidden"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRate" runat="server" AutoPostBack="true" OnTextChanged="ButtonAdd1_Click"
                                                                            Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                     <div>
                                </div>



                                    </div>

                                    </div>

                                    <div id="but" runat="server" class="row">
                            <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" CausesValidation="false"
                                Style="width: 120px; margin-top: 25px" OnClick="Insert_LotDetails" />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" PostBackUrl="Dillo_StitchingInfoGrid.aspx"
                                Style="width: 120px; margin-top: 25px" />
                            <asp:Button ID="btnPrint" runat="server" class="btn btn-success" Text="Print" Style="width: 120px; margin-top: 25px"
                             OnClick="btn_Print"/>
                        </div>

                            </div>

                            <div class="col-lg-6">
                                
                            </div>
                            <div class="col-lg-6">
                                
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">

                                                              <%--  <table id="Table2" width="240%">
                                                <tr>
                                                    <td>--%>

                                  <asp:GridView runat="server" BorderWidth="1" ID="GV_Size" CssClass="myleft" GridLines="Vertical" 
                                        AlternatingRowStyle-CssClass="even" ShowHeader="true" AutoGenerateColumns="false"
                                        ShowFooter="false" PrintPageSize="30" AllowPrintPaging="true" Width="100%"  OnRowDataBound="GV_Size_RowDataBound" OnRowCreated="GV_Size_RowCreated"
                                        Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />


                                            <Columns>
                           <%--                     <asp:TemplateField HeaderText="SIZE" ItemStyle-Width="3%">
                                                 <ItemTemplate>
                                                  <asp:Label ID="lblFS" runat="server" Text="FS" Height="26px" Font-Bold="true"></asp:Label><br />
                                                  <asp:Label ID="lblHS" runat="server" Text="HS" Height="26px" Font-Bold="true"></asp:Label>
                                                 </ItemTemplate>
                                                </asp:TemplateField>--%>

                                             
                                                <asp:TemplateField HeaderText="36 HS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt36HS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt36HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField  HeaderText="38 HS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                <asp:TextBox ID="txt38HS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt38HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                                <asp:TemplateField  HeaderText="39 HS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                <asp:TextBox ID="txt39HS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt39HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderText="40 HS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt40HS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt40HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderText="42 HS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt42HS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt42HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                            
                                               <asp:TemplateField  HeaderText="44 HS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt44HS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt44HS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>

   <asp:TemplateField HeaderText="36 FS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt36FS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt36FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="38 FS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt38FS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt38FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="39 FS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt39FS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt39FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="40 FS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt40FS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt40FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="42 FS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt42FS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt42FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="44 FS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                 <asp:TextBox ID="txt44FS" runat="server" Text="0"  Width="100%" Height="26px" AppendDataBoundItems="true" OnTextChanged="txt44FS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>



                                              </Columns>
                                        </asp:GridView>
                                        <%--</td>
                                        </tr>
                                        </table>--%>

                        
                                <%--    <triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click"/> 
                                </triggers>--%>
                              <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        
                    </div>
                    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
                    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
                    <script type="text/javascript">
                        window.onload = function () {
                            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                        }
                    </script>
                    <%--   </ContentTemplate>
                        </asp:UpdatePanel>--%>
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
