<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unpacked.aspx.cs" Inherits="Billing.Accountsbootstrap.Unpacked" %>

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
    <title>UnPacked Process</title>
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
                UnPacked Process</h1>
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
                                    </div>
                                    <div class="form-group ">
                                        <label>
                                            Branch</label>
                                        <asp:DropDownList ID="drpbranch" OnSelectedIndexChanged="company_SelectedIndexChnaged"
                                            AutoPostBack="true" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <label>
                                            Type</label>
                                        <asp:DropDownList ID="ddlTypeUnpack" OnSelectedIndexChanged="ddlType_SelectedIndexChnaged"
                                            AutoPostBack="true" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Type" Value="Type"></asp:ListItem>
                                            <asp:ListItem Text="RI" Value="RI"></asp:ListItem>
                                             <asp:ListItem Text="BC" Value="BC"></asp:ListItem>
                                            <asp:ListItem Text="HI" Value="HI"></asp:ListItem>
                                            <asp:ListItem Text="SAMPLE" Value="SAMPLE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <%-- <div class="form-group ">
                                        <label>
                                            Inner Type</label>
                                        <asp:RadioButtonList ID="rdbinnertype" CssClass="chkChoice1" runat="server" RepeatColumns="3"  OnSelectedIndexChanged="rdbinnertype_SelectedIndexChnaged" AutoPostBack="true"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="RAPPHAEL" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="FASHION21" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="DH WHITE" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>--%>
                                    <div class="form-group ">
                                        <asp:CheckBox ID="manualcheck" runat="server" Checked="true" Enabled="false" OnCheckedChanged="manualcheck_changed"
                                            AutoPostBack="true" />
                                        <label>
                                            Lot No</label>
                                        <asp:DropDownList ID="drplotno" runat="server" CssClass="form-control" OnSelectedIndexChanged="drplotchanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblcompany" Font-Bold="true" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtsupplierprefix" placeholder="Enter Supplier Inital" Font-Bold="true"
                                            runat="server" Width="50%" Style="margin-left: 6pc; margin-top: -2pc; width: 60%;"
                                            CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            FilterType="UppercaseLetters,LowercaseLetters" ValidChars="" TargetControlID="txtsupplierprefix" />
                                        <asp:TextBox ID="txtmanualchecked" placeholder="Enter Lot No" Style="margin-left: 18pc;
                                            margin-top: -2pc; width: 50%;" Width="50%" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtmanualchecked" />
                                    </div>
                                    <div id="Div1" class="form-group" runat="server">
                                        <label>
                                            Brand Name</label>
                                        <asp:DropDownList ID="drpbrand" runat="server" OnSelectedIndexChanged="brandindexchnaged"
                                            AutoPostBack="true" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="overflow-y: scroll; width: 284px; height: 120px">
                                        <div class="panel panel-default" style="width: 265px">
                                            <label>
                                                Fit Label</label>
                                            <%--  <asp:CompareValidator ID="CompareValidator6" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="Nchkfit" ValueToCompare="Select Fit"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please select Fit name!"></asp:CompareValidator>--%>
                                            <%--  <asp:DropDownList ID="Sddrrpfit" CssClass="chzn-select" OnSelectedIndexChanged="Sfitchaged"
                                                AutoPostBack="true" runat="server" Height="26px" Width="100%">
                                            </asp:DropDownList>--%>
                                            <asp:CheckBoxList ID="Nchkfit" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                                                CssClass="chkChoice1" OnSelectedIndexChanged="Sfitchaged" AutoPostBack="true">
                                            </asp:CheckBoxList>
                                            <%--<asp:CheckBoxList ID="Schkfit" runat="server"  RepeatDirection="Horizontal"
                                             RepeatColumns="1" CssClass="chkChoice1" OnSelectedIndexChanged="Sfitchaged" AutoPostBack="true" ></asp:CheckBoxList>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                                        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                        ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                                        Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                            <asp:BoundField DataField="Fitt" HeaderText="Type/Size" HeaderStyle-Width="25px" />
                                            <asp:BoundField HeaderText="30" DataField="s30" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="32" DataField="s32" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="34" DataField="s34" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="36" DataField="s36" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="XS" DataField="sxs" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="S" DataField="ss" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="M" DataField="sm" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="L" DataField="sl" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="XL" DataField="sxl" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="XXL" DataField="sxxl" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="3XL" DataField="s3xl" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="4XL" DataField="s4xl" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Total" DataField="tot" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
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
                                                <tr is="mastercut" runat="server" visible="false">
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="150" Width="100%">
                                                            <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                                OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                                CssClass="chzn-container" GridLines="None" Width="250%" runat="server">
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
                                                                    <asp:TemplateField HeaderText="Design/Color Code" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="15%" HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdesigno" Enabled="false" Text='<%# Eval("designno")%>' runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Name" ControlStyle-Width="100%" ItemStyle-Width="5%"
                                                                        HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtitemname" Enabled="false" Text='<%# Eval("Itemname")%>' runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pattern Name" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="2%" HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtpatternid" Visible="false" runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtpatternname" Enabled="false" runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brand Name" Visible="false" HeaderStyle-Width="5%"
                                                                        ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtledgerid" Visible="false" runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtparty" Enabled="false" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Required Meter" ControlStyle-Width="100%" ItemStyle-Width="4%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txteqrmeter" Enabled="false" Text='<%# Eval("reqmeter")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Required Shirt" ControlStyle-Width="100%" ItemStyle-Width="4%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtreqshirt" Enabled="false" Text='<%# Eval("reqshirt")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fit" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("Fitid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtfit" Enabled="false" Text='<%# Eval("Fit")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30fs" Enabled="false" Text='<%# Eval("S30FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32fs" Enabled="false" Text='<%# Eval("S32FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34fs" Enabled="false" Text='<%# Eval("S34FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36fs" Enabled="false" Text='<%# Eval("S36FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxsfs" Enabled="false" Text='<%# Eval("SXSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtssfs" Enabled="false" Text='<%# Eval("SSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmfs" Enabled="false" Text='<%# Eval("SMFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslfs" Enabled="false" Text='<%# Eval("SLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlfs" Enabled="false" Text='<%# Eval("SXLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlfs" Enabled="false" Text='<%# Eval("SXXLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlfs" Enabled="false" Text='<%# Eval("S3XLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL FS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlfs" Enabled="false" Text='<%# Eval("S4XLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30hs" Enabled="false" Text='<%# Eval("S30HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32hs" Enabled="false" Text='<%# Eval("S32HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34hs" Enabled="false" Text='<%# Eval("S34HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36hs" Enabled="false" Text='<%# Eval("S36HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxshs" Enabled="false" Text='<%# Eval("SXSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsshs" Enabled="false" Text='<%# Eval("SSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmhs" Enabled="false" Text='<%# Eval("SMHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslhs" Enabled="false" Text='<%# Eval("SLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlhs" Enabled="false" Text='<%# Eval("SXLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlhs" Enabled="false" Text='<%# Eval("SXXLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlhs" Enabled="false" Text='<%# Eval("S3XLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL HS" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlhs" Enabled="false" Text='<%# Eval("S4XLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr id="tr1" runat="server">
                                                    <td colspan="2">
                                                        <table>
                                                            <tr>
                                                                <td id="Td2" runat="server" style="width: 8%">
                                                                    <label>
                                                                        Item Name</label>
                                                                    <%-- <asp:DropDownList ID="drpCustomer" runat="server" Enabled="false" Width="150px" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                                                    <asp:TextBox ID="txtitemname" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td id="Td9" runat="server" visible="true" style="width: 8%">
                                                                    <label>
                                                                        Color Type</label>
                                                                    <asp:DropDownList ID="drpcolortype" runat="server" CssClass="form-control" Width="100%">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="manualcolor" runat="server" Visible="false"></asp:TextBox>
                                                                </td>
                                                                <td id="Td3" runat="server" style="width: 8%">
                                                                    <label>
                                                                        Fit</label>
                                                                    <asp:DropDownList ID="drpFit" OnSelectedIndexChanged="drpfitchanged" AutoPostBack="true"
                                                                        runat="server" CssClass="form-control" Width="100%">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td id="Td4" runat="server" visible="false" style="width: 8%">
                                                                    <label>
                                                                        Pattern Type</label>
                                                                    <asp:DropDownList ID="drppattern" runat="server" CssClass="form-control" Width="100%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td id="Td56" runat="server" style="width: 5%" visible="false">
                                                                    <label>
                                                                        Aval.Meter</label>
                                                                    <asp:TextBox ID="txtavamet1" runat="server" AutoPostBack="true" Width="100%" CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="S30fs" runat="server">
                                                                    <label>
                                                                        30 FS</label>
                                                                    <asp:TextBox ID="Btxt30fs" OnTextChanged="Schange30fs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="S32fs" runat="server">
                                                                    <label>
                                                                        32 FS</label>
                                                                    <asp:TextBox ID="Btxt32fs" OnTextChanged="Schange32fs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="S34fs" runat="server">
                                                                    <label>
                                                                        34 FS</label>
                                                                    <asp:TextBox ID="Btxt34fs" OnTextChanged="Schange34fs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="S36fs" runat="server">
                                                                    <label>
                                                                        36 FS</label>
                                                                    <asp:TextBox ID="Btxt36fs" OnTextChanged="NSchange36fs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="Xsfs" runat="server">
                                                                    <label>
                                                                        XS FS</label>
                                                                    <asp:TextBox ID="Btxtxsfs" OnTextChanged="SchangeXSfs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="sfs" runat="server">
                                                                    <label>
                                                                        S FS</label>
                                                                    <asp:TextBox ID="txtsfs" OnTextChanged="SchangeSfs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="mfs" runat="server">
                                                                    <label>
                                                                        M FS</label>
                                                                    <asp:TextBox ID="txtmfs" OnTextChanged="SchangeMfs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="lfs" runat="server">
                                                                    <label>
                                                                        L FS</label>
                                                                    <asp:TextBox ID="txtlfs" OnTextChanged="SchangeLfs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="xlfs" runat="server">
                                                                    <label>
                                                                        XL FS</label>
                                                                    <asp:TextBox ID="txtxlfs" OnTextChanged="SchangeXLfs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="xxlfs" runat="server">
                                                                    <label>
                                                                        XXL FS</label>
                                                                    <asp:TextBox ID="txtxxlfs" OnTextChanged="SchangeXXLfs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="xxxlfs" runat="server">
                                                                    <label>
                                                                        3XL FS</label>
                                                                    <asp:TextBox ID="txtxxxlfs" OnTextChanged="SchangeXXXLfs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="xxxxlfs" runat="server">
                                                                    <label>
                                                                        4XL FS</label>
                                                                    <asp:TextBox ID="txtxxxxlfs" OnTextChanged="SchangeXXXXLfs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="S30hs" runat="server">
                                                                    <label>
                                                                        30 HS</label>
                                                                    <asp:TextBox ID="Btxt30hs" OnTextChanged="Schange30hs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="S32hs" runat="server">
                                                                    <label>
                                                                        32 HS</label>
                                                                    <asp:TextBox ID="Btxt32hs" OnTextChanged="Schange32hs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="S34hs" runat="server">
                                                                    <label>
                                                                        34 HS</label>
                                                                    <asp:TextBox ID="Btxt34hs" OnTextChanged="Schange34hs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="S36hs" runat="server">
                                                                    <label>
                                                                        36 HS</label>
                                                                    <asp:TextBox ID="Btxt36hs" OnTextChanged="NSchange36hs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="Xshs" runat="server">
                                                                    <label>
                                                                        XS HS</label>
                                                                    <asp:TextBox ID="Btxtxshs" OnTextChanged="SchangeXShs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="shs" runat="server">
                                                                    <label>
                                                                        S HS</label>
                                                                    <asp:TextBox ID="txtshs" OnTextChanged="SchangeShs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="mhs" runat="server">
                                                                    <label>
                                                                        M HS</label>
                                                                    <asp:TextBox ID="txtmhs" OnTextChanged="SchangeMhs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="lhs" runat="server">
                                                                    <label>
                                                                        L HS</label>
                                                                    <asp:TextBox ID="txtlhs" OnTextChanged="SchangeLhs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="xlhs" runat="server">
                                                                    <label>
                                                                        XL HS</label>
                                                                    <asp:TextBox ID="txtxlhs" OnTextChanged="SchangeXLhs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="xxlhs" runat="server">
                                                                    <label>
                                                                        XXL HS</label>
                                                                    <asp:TextBox ID="txtxxlhs" OnTextChanged="SchangeXXLhs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="xxxlhs" runat="server">
                                                                    <label>
                                                                        3XL HS</label>
                                                                    <asp:TextBox ID="txtxxxlhs" OnTextChanged="SchangeXXXLhs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="xxxxlhs" runat="server">
                                                                    <label>
                                                                        4XL HS</label>
                                                                    <asp:TextBox ID="txtxxxxlhs" OnTextChanged="SchangeXXXXLhs" AutoPostBack="true" runat="server"
                                                                        CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="Td5" visible="false" runat="server" style="width: 5%">
                                                                    <label>
                                                                        Act.Meter</label>
                                                                    <asp:TextBox ID="txtactualmet" Width="100%" runat="server" CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="Td6" visible="false" runat="server" style="width: 5%">
                                                                    <label>
                                                                        Act.Shirt</label>
                                                                    <asp:TextBox ID="Ntxtactshirt" Width="100%" runat="server" CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="Td66" visible="false" runat="server">
                                                                    <label>
                                                                        WSP</label>
                                                                    <asp:TextBox ID="Stxtwsp" runat="server" CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="Td7" visible="true" runat="server" style="width: 5%">
                                                                    <label>
                                                                        Tot.Shirts</label>
                                                                    <asp:TextBox ID="txttotshirt1" Enabled="false" Width="100%" runat="server" CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="Td8" runat="server" visible="false" style="width: 5%">
                                                                    <label>
                                                                        Avg.Size</label>
                                                                    <asp:TextBox ID="txtavvgmeter" Enabled="false" Width="100%" runat="server" CssClass="form-control">0</asp:TextBox>
                                                                </td>
                                                                <td id="addsingle" runat="server">
                                                                    <label>
                                                                    </label>
                                                                    <asp:Button ID="ImageButton1" runat="server" OnClick="GOheadprocessclick" Text="Add" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div runat="server" visible="false">
                                                            <label>
                                                                Size</label>
                                                            <asp:CheckBoxList ID="chkSizes" OnSelectedIndexChanged="ckhsize_index" AutoPostBack="true"
                                                                RepeatDirection="Horizontal" RepeatColumns="2" CssClass="chkChoice1" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                        <label>
                                                            Details</label>
                                                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="200">
                                                            <asp:GridView ID="gridsize" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gridmaster_RowDataBound"
                                                                OnRowDeleting="gridmaster_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                                runat="server" Width="250%">
                                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" Visible="false" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtno" Visible="false" runat="server"></asp:TextBox>
                                                                            <asp:Label ID="lblid" Visible="false" runat="server"></asp:Label>
                                                                            <asp:Label ID="lblno" Visible="false" runat="server"></asp:Label>
                                                                            <asp:TextBox ID="txtmar" Enabled="false" runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="Txtmrp" Enabled="false" runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="Txrtate" Enabled="false" runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Design/Color Code" Visible="true" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdesigno" Enabled="false" Text='<%# Eval("Design")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtdesignid" Enabled="false" Visible="false" Text='<%# Eval("Designid")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Name" ControlStyle-Width="100%" ItemStyle-Width="5%"
                                                                        HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtitemname" Width="100%" Enabled="false" Text='<%# Eval("Itemname")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brand Name" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtledgerid" Visible="false" runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtparty" Enabled="false" Width="100%" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pattern Name" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="2%" HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtpatternid" Visible="false" runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtpatternname" Enabled="false" Text='<%# Eval("PAtternname")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req.Meter" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txteqrmeter" Width="100%" Enabled="false" Text='<%# Eval("reqmeter")%>'
                                                                                Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req.Shirt" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtreqshirt" Width="100%" Enabled="false" Text='<%# Eval("reqshirt")%>'
                                                                                Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("Fitid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtfit" Width="100%" Enabled="false" Text='<%# Eval("Fit")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Shirts" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttotal" Width="100%" Enabled="false" Text='<%# Eval("reqshirt")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30fs" Width="100%" Enabled="true" Text='<%# Eval("S30FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32fs" Width="100%" Enabled="true" Text='<%# Eval("S32FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34fs" Width="100%" Enabled="true" Text='<%# Eval("S34FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36fs" Width="100%" Enabled="true" Text='<%# Eval("S36FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxsfs" Width="100%" Enabled="true" Text='<%# Eval("SXSFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtssfs" Width="100%" Enabled="true" Text='<%# Eval("SSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmfs" Width="100%" Enabled="true" Text='<%# Eval("SMFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslfs" Width="100%" Enabled="true" Text='<%# Eval("SLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlfs" Width="100%" Enabled="true" Text='<%# Eval("SXLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlfs" Width="100%" Enabled="true" Text='<%# Eval("SXXLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlfs" Width="100%" Enabled="true" Text='<%# Eval("S3XLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlfs" Width="100%" Enabled="true" Text='<%# Eval("S4XLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30hs" Width="100%" Enabled="true" Text='<%# Eval("S30HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32hs" Width="100%" Enabled="true" Text='<%# Eval("S32HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34hs" Width="100%" Enabled="true" Text='<%# Eval("S34HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36hs" Width="100%" Enabled="true" Text='<%# Eval("S36HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxshs" Width="100%" Enabled="true" Text='<%# Eval("SXSHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsshs" Width="100%" Enabled="true" Text='<%# Eval("SSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmhs" Width="100%" Enabled="true" Text='<%# Eval("SMHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslhs" Width="100%" Enabled="true" Text='<%# Eval("SLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlhs" Width="100%" Enabled="true" Text='<%# Eval("SXLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlhs" Width="100%" Enabled="true" Text='<%# Eval("SXXLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlhs" Width="100%" Enabled="true" Text='<%# Eval("S3XLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlhs" Width="100%" Enabled="true" Text='<%# Eval("S4XLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dmg.Qty" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdamage" Width="100%" runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Usedmeter" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtuedmter" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Reason Type" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList Width="100%" ID="drpreason" runat="server">
                                                                                <asp:ListItem Text="Select Reason" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="Damage" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Width Shortage" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Extra" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="Other" Value="3" Selected="True"></asp:ListItem>
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
                                                                    <asp:TemplateField HeaderText="Type" Visible="false" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                                        ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList Width="100%" ID="ddltype" runat="server">
                                                                                <asp:ListItem Selected="True" Text="UnPacked" Value="1"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
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
                                        <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" PostBackUrl="~/Accountsbootstrap/UnpackedGrid.aspx"
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
