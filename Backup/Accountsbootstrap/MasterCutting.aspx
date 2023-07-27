<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterCutting.aspx.cs"
    Inherits="Billing.Accountsbootstrap.MasterCutting" %>

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
    <title>Master Cutting Process</title>
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
                Master Cutting Process</h1>
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
                                <div class="col-lg-1">
                                    <asp:TextBox ID="txthalfqty" runat="server" Visible="false">0</asp:TextBox>
                                    <asp:TextBox ID="txtfullqty" runat="server" Visible="false">0</asp:TextBox>
                                    <asp:TextBox ID="txttotalqty" runat="server" Visible="false">0</asp:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                        ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                    <div class="form-group" id="divcode" visible="false" runat="server">
                                        <asp:TextBox CssClass="form-control" ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:Label ID="lblratiotype" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-lg-6" id="sizediv" runat="server" visible="false" style="margin-left: -3pc;">
                                        <div class="panel panel-default" style="width: 170px">
                                            <label>
                                                Size</label>
                                            <asp:CheckBoxList ID="chkSizes" OnSelectedIndexChanged="ckhsize_index" AutoPostBack="true"
                                                RepeatDirection="Horizontal" RepeatColumns="2" CssClass="chkChoice1" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            Branch</label>
                                        <asp:DropDownList ID="drpbranch" OnSelectedIndexChanged="company_SelectedIndexChnaged"
                                            AutoPostBack="true" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            Lot No</label>
                                        <asp:DropDownList ID="drplotno" runat="server" CssClass="form-control" OnSelectedIndexChanged="drplotchanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Cutting Date:</label>
                                        <asp:TextBox ID="txtdate" Enabled="false" runat="server" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdate" runat="server"
                                            Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <label>
                                        Item Narration</label>
                                    <asp:TextBox ID="txtitemnarration" runat="server" TextMode="MultiLine" CssClass="form-control" Enabled="false"
                                        Width="250px"></asp:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group ">
                                        <label>
                                            Select Width</label>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpwidth" ValueToCompare="Select Width"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please select Width!"></asp:CompareValidator>
                                        <asp:DropDownList ID="drpwidth" Enabled="false" OnSelectedIndexChanged="drpwidthChange"
                                            AutoPostBack="true" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div id="Div5" runat="server" class="form-group ">
                                        <label>
                                            Cutting Master</label>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlcutting" ValueToCompare="Select Party Name"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please select Party name!"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlcutting" Enabled="false" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            Select Item :
                                        </label>
                                        <asp:DropDownList ID="drpitemtype" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group" runat="server">
                                        <label>
                                            Brand Name</label>
                                        <asp:DropDownList ID="drpbrand" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="Div1" runat="server" class="col-lg-5">
                                    <label>
                                        Fabric List</label>
                                    <div id="Div2" runat="server" style="overflow: scroll; height: 10pc">
                                        <asp:GridView ID="newgridfablist" AutoGenerateColumns="False" ShowFooter="True" CssClass="chzn-container"
                                            GridLines="None" Width="100%" runat="server">
                                            <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                            <RowStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" />
                                            <Columns>
                                                <asp:BoundField DataField="bdytyp" HeaderText="Fabric Type" HeaderStyle-Width="5%" />
                                                <asp:TemplateField HeaderText="Fab Code" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                    HeaderStyle-Width="2%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="newfabcode" Text='<%# Eval("DesignNo")%>' runat="server"></asp:Label>
                                                        <asp:Label ID="newfabid" Text='<%# Eval("transfabid")%>' runat="server" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblshirttype" Text='<%# Eval("ShirtType")%>' runat="server" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblfabidd" Text='<%# Eval("fabid")%>' runat="server" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblavgmeter" Text='<%# Eval("AvgMeter")%>' runat="server" Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Given Wt./gms." HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="newtxtAvlmeter" Enabled="false" Text='<%# Eval("Reqmeter","{0:n}")%>'
                                                            runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Used Wt./gms." HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="newtxtreqmeter" Enabled="false" Text='<%# Eval("Reqmeter","{0:n}")%>'
                                                            runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Bit(Wt./gms.)" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="newtxtendmeter" Text='<%# Eval("Endbit","{0:f0}")%>' runat="server"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div visible="false" id="Div8" runat="server">
                                        <asp:Button ID="Newbtnclick" runat="server" OnClick="newfabclick" Text="Process" /></div>
                                </div>
                                <div runat="server" visible="false" class="col-lg-3">
                                    <label>
                                        Cutting Master Cost-Per Qty</label>
                                    <asp:TextBox ID="txtcutcost" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div runat="server" visible="false" class="col-lg-3">
                                    <div class="form-group ">
                                        <label>
                                            Cost Price</label>
                                        <asp:TextBox ID="txtcost" runat="server" Enabled="false" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                            </br> </br>
                            <div class="row">
                                <div class="col-lg-12" style="margin-top: -35px">
                                    <div class="panel-body">
                                        <div>
                                            <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                            <table class="table table-striped table-bordered table-hover" id="Table1" width="100%">
                                                <tr>
                                                    <label>
                                                        Pre-Cutting Detailed Report</label>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="150" Width="100%">
                                                            <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                                OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                                CssClass="chzn-container" GridLines="None" Width="65%" runat="server">
                                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                                        HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtno" Visible="false" Text='<%# Eval("cutid")%>' runat="server"></asp:TextBox>
                                                                            <asp:Label ID="lblid" Visible="false" Text='<%# Eval("transid")%>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblno" Visible="false" Text='<%# Eval("invrefno")%>' runat="server"></asp:Label>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Color Code" Visible="true" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="8%" HeaderStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblonlycolor" Text='<%# Eval("onlycolor")%>' runat="server"></asp:Label>
                                                                            <asp:TextBox ID="txtdesigno" Visible="false" Enabled="false" Text='<%# Eval("designno")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Contrast" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblContrast" Text='<%# Eval("Contrast")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pattern Name" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="2%" HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtpatternid" Visible="false" Text='<%# Eval("Patternid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtpatternname" Enabled="false" Text='<%# Eval("PAtternname")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtitemname" Enabled="false" Text='<%# Eval("Itemname")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brand Name" Visible="false" HeaderStyle-Width="5%"
                                                                        ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtledgerid" Visible="false" Text='<%# Eval("ledgerid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtparty" Enabled="false" Text='<%# Eval("Ledgername")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req.Wt./gms." ControlStyle-Width="100%" ItemStyle-Width="7%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txteqrmeter" Enabled="false" Text='<%# Eval("reqmeter")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req.Shirt" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtreqshirt" Enabled="false" Text='<%# Eval("totalshirt")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fit" Visible="false" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("Fitid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtfit" Enabled="false" Text='<%# Eval("Fit")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30fs" Enabled="false" Text='<%# Eval("S30FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32fs" Enabled="false" Text='<%# Eval("S32FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34fs" Enabled="false" Text='<%# Eval("S34FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36fs" Enabled="false" Text='<%# Eval("S36FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxsfs" Enabled="false" Text='<%# Eval("SXSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtssfs" Enabled="false" Text='<%# Eval("SSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmfs" Enabled="false" Text='<%# Eval("SMFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslfs" Enabled="false" Text='<%# Eval("SLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlfs" Enabled="false" Text='<%# Eval("SXLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlfs" Enabled="false" Text='<%# Eval("SXXLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlfs" Enabled="false" Text='<%# Eval("S3XLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlfs" Enabled="false" Text='<%# Eval("S4XLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30hs" Enabled="false" Text='<%# Eval("S30HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32hs" Enabled="false" Text='<%# Eval("S32HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34hs" Enabled="false" Text='<%# Eval("S34HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36hs" Enabled="false" Text='<%# Eval("S36HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxshs" Enabled="false" Text='<%# Eval("SXSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsshs" Enabled="false" Text='<%# Eval("SSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmhs" Enabled="false" Text='<%# Eval("SMHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslhs" Enabled="false" Text='<%# Eval("SLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlhs" Enabled="false" Text='<%# Eval("SXLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlhs" Enabled="false" Text='<%# Eval("SXXLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlhs" Enabled="false" Text='<%# Eval("S3XLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlhs" Enabled="false" Text='<%# Eval("S4XLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Avg.Wt./gms." ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtavgwtgms" Width="100%" Enabled="false" Text='<%# Eval("avgwtgms")%>'
                                                                                Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <label>
                                                            Master Cutting Details</label>
                                                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="200">
                                                            <asp:GridView ID="gridsize" AutoGenerateColumns="False" ShowFooter="True" OnRowDeleting="gridmaster_RowDeleting"
                                                                CssClass="chzn-container" GridLines="None" runat="server" Width="100%">
                                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" Visible="false" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtno" Visible="false" Text='<%# Eval("cutid")%>' runat="server"></asp:TextBox>
                                                                            <asp:Label ID="lblid" Visible="false" Text='<%# Eval("transid")%>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblno" Visible="false" Text='<%# Eval("invrefno")%>' runat="server"></asp:Label>
                                                                            <asp:TextBox ID="txtmar" Enabled="false" Text='<%# Eval("margin")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="Txtmrp" Enabled="false" Text='<%# Eval("mrp")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="Txrtate" Enabled="false" Text='<%# Eval("rate")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txttransfabid" Enabled="false" Text='<%# Eval("transfabid")%>' runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Color Code" Visible="true" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblonlycolor" Text='<%# Eval("onlycolor")%>' runat="server"></asp:Label>
                                                                            <asp:TextBox ID="txtdesigno" Visible="false" Enabled="false" Text='<%# Eval("designno")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Contrast" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblContrast" Enabled="false" Text='<%# Eval("Contrast")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brand Name" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtledgerid" Visible="false" Text='<%# Eval("ledgerid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtparty" Enabled="false" Width="100%" Text='<%# Eval("Ledgername")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtitemname" Width="100%" Enabled="false" Text='<%# Eval("Itemname")%>'
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pattern Name" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="2%" HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtpatternid" Visible="false" Text='<%# Eval("Patternid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtpatternname" Enabled="false" Text='<%# Eval("PAtternname")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req.Wt./gms." ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txteqrmeter" Width="100%" Enabled="false" Text='<%# Eval("reqmeter")%>'
                                                                                Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req.Shirt" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtreqshirt" Width="100%" Enabled="false" Text='<%# Eval("totalshirt")%>'
                                                                                Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fit" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("Fitid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtfit" Width="100%" Enabled="false" Text='<%# Eval("Fit")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30fs" Width="100%" Enabled="true" Text='<%# Eval("S30FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32fs" Width="100%" Enabled="true" Text='<%# Eval("S32FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34fs" Width="100%" Enabled="true" Text='<%# Eval("S34FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36fs" Width="100%" Enabled="true" Text='<%# Eval("S36FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxsfs" Width="100%" Enabled="true" Text='<%# Eval("SXSFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtssfs" Width="100%" Enabled="true" Text='<%# Eval("SSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmfs" Width="100%" Enabled="true" Text='<%# Eval("SMFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslfs" Width="100%" Enabled="true" Text='<%# Eval("SLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlfs" Width="100%" Enabled="true" Text='<%# Eval("SXLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlfs" Width="100%" Enabled="true" Text='<%# Eval("SXXLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlfs" Width="100%" Enabled="true" Text='<%# Eval("S3XLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlfs" Width="100%" Enabled="true" Text='<%# Eval("S4XLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30hs" Width="100%" Enabled="true" Text='<%# Eval("S30HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32hs" Width="100%" Enabled="true" Text='<%# Eval("S32HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34hs" Width="100%" Enabled="true" Text='<%# Eval("S34HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36hs" Width="100%" Enabled="true" Text='<%# Eval("S36HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxshs" Width="100%" Enabled="true" Text='<%# Eval("SXSHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsshs" Width="100%" Enabled="true" Text='<%# Eval("SSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmhs" Width="100%" Enabled="true" Text='<%# Eval("SMHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslhs" Width="100%" Enabled="true" Text='<%# Eval("SLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlhs" Width="100%" Enabled="true" Text='<%# Eval("SXLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlhs" Width="100%" Enabled="true" Text='<%# Eval("SXXLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlhs" Width="100%" Enabled="true" Text='<%# Eval("S3XLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlhs" Width="100%" Enabled="true" Text='<%# Eval("S4XLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dmg.Qty" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdamage" Width="100%" runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Shirts" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttotal" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Avg.Wt./gms." ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtavgwtgms" Width="100%" Enabled="false" Text='<%# Eval("avgwtgms")%>'
                                                                                Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Usedmeter" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtuedmter" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Reason Type" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                                        ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList Width="100%" ID="drpreason" runat="server">
                                                                                <asp:ListItem Text="Select Reason" Selected="True" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="Damage" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Width Shortage" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Extra" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="Other" Value="3"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Comments" HeaderStyle-Width="13%" ItemStyle-Width="13%"
                                                                        ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtnarration" Width="100%" runat="server" Height="26px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="End bit" Visible="false" HeaderStyle-Width="13%" ItemStyle-Width="13%"
                                                                        ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtendbit" Width="100%" runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div runat="server" visible="false" class="col-lg-12">
                                                            <div align="center" class="col-lg-12">
                                                                <label>
                                                                    OVERALL DETAILS</label>
                                                                <table width="100%" border="2" style="border-spacing: 1px; border-collapse: collapse"
                                                                    class="style1">
                                                                    <tr>
                                                                        <td align="center">
                                                                            30/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            32/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            34/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            36/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            XS/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            S/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            M/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            L/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            XL/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            XXL/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            3XL/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            4XL/FS
                                                                        </td>
                                                                        <td align="center">
                                                                            30/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            32/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            34/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            36/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            XS/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            S/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            M/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            L/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            XL/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            XXL/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            3XL/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            4XL/HS
                                                                        </td>
                                                                        <td align="center">
                                                                            Total
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb30f" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb32f" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb34f" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb36f" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbxsf" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbsf" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbmf" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblf" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbxlf" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbxxlf" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb3xlf" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb4xlf" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb30h" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb32h" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb34h" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb36h" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbxsh" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbsh" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbmh" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblh" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbxlh" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lbxxlh" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb3xlh" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lb4xlh" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="LabelTotal" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td1" runat="server" align="right">
                                                        <asp:Button ID="ButtonAdd1" runat="server" Visible="false" EnableTheming="false"
                                                            Text="Add New" />
                                                        <asp:Button ID="btncal" runat="server" Text="Calculate" OnClick="calc" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--</tr>
                                            </tbody>--%>
                                        </div>
                                        <br />
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
