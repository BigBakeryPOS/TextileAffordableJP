<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MastercutGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.MastercutGrid" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head runat="server">
    <style type="text/css">
        a img
        {
            border: none;
        }
        ol li
        {
            list-style: decimal outside;
        }
        div#container
        {
            width: 780px;
            margin: 0 auto;
            padding: 1em 0;
        }
        div.side-by-side
        {
            width: 100%;
            margin-bottom: 1em;
        }
        div.side-by-side > div
        {
            float: left;
            width: 50%;
        }
        div.side-by-side > div > em
        {
            margin-bottom: 10px;
            display: block;
        }
        .clearfix:after
        {
            content: "\0020";
            display: block;
            height: 0;
            clear: both;
            overflow: hidden;
            visibility: hidden;
        }
    </style>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Master Cutting Process - bootsrap</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../css/responsive-tabs.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
    <script src="../js/jquery.responsiveTabs.min.js" type="text/javascript"></script>
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            ShowPopup();
            setTimeout(HidePopup, 2000);
        }

        function ShowPopup() {
            $find('modalpopup').show();
            //$get('Button1').click();
        }

        function HidePopup() {
            $find('modalpopup').hide();
            //$get('btnCancel').click();
        }
    </script>
    <script type="text/javascript">
        function Search_Gridview(strKey, strGV) {


            var strData = strKey.value.toLowerCase().split(" ");

            var tblData = document.getElementById(strGV);

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)

                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }    
    </script>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        function alertMessage() {
            alert('Are You Sure, You want to delete This Brand!');
        }
    </script>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1">
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <div class="row">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header" style="text-align: center; color: #fe0002;">
                Master Cutting Process</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <%-- <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Group Master</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group ">
                                    <asp:Label ID="Label1" runat="server"></asp:Label><br />
                                    <asp:DropDownList ID="drpbranch" OnSelectedIndexChanged="company_SelectedIndexChnaged"
                                        AutoPostBack="true" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="Label3" runat="server"></asp:Label><br />
                                <asp:TextBox CssClass="form-control" onkeyup="Search_Gridview(this, 'gvcust')" Enabled="true"
                                    ID="txtsearch" runat="server" placeholder="Search Text" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="." Style="color: White" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,custom" ValidChars=" /-"
                                    TargetControlID="txtsearch" />
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                        OnTextChanged="Date_OnTextChanged" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                        PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                        OnTextChanged="Date_OnTextChanged" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                        PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="Label6" runat="server" Style="color: Red"></asp:Label><br />
                                <asp:DropDownList CssClass="form-control" ID="ddljobworker" Width="150px" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddljobworker_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="Label4" runat="server"></asp:Label><br />
                                <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" Width="130px"
                                    OnClick="Add_Click" />
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                            </div>
                            <div runat="server" visible="false" class="col-lg-16">
                                <asp:Label runat="server" ID="lblSelectedValue"></asp:Label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                                    Text="." Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                <asp:DropDownList CssClass="form-control" ID="ddlfilter" Width="150px" Style="margin-top: -20px"
                                    runat="server">
                                    <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="CuttingID" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="LotNo" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="PartyName" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div runat="server" visible="false" class="col-lg-17">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" ValidationGroup="val1"
                                    OnClick="Search_Click" Width="130px" />
                            </div>
                            <div runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"
                                    Width="130px" />
                            </div>
                            <div class="col-lg-15">
                                &nbsp;&nbsp;</div>
                            <div class="col-lg-17">
                                &nbsp;&nbsp;</div>
                            <div class="col-lg-17">
                                &nbsp;&nbsp;
                                <%--<asp:Button ID="Btnnew" runat="server" class="btn btn-danger" Text="Add New Cutting" Width="130px"  OnClick="Add1_Click" />--%>
                            </div>
                        </div>
                    </div>
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvcust" EmptyDataText="No records Found" runat="server" CssClass="myGridStyle"
                                        AllowPaging="false" PageSize="10" OnPageIndexChanging="Page_Change" AutoGenerateColumns="false"
                                        DataKeyNames="CompanyLotNo" OnRowCommand="gvcust_RowCommand" Width="85%" OnRowDataBound="gvcust_RowDataBound">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" HeaderText="FabDetails"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%-- <a href="javascript:switchViews('dv<%# Eval("CompanyLotNo") %>', 'imdiv<%# Eval("CompanyLotNo") %>');"
                                                        style="text-decoration: none;">
                                                        <img id="imdiv<%# Eval("CompanyLotNo") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                    </a>--%>
                                                    <div id="dv<%# Eval("CompanyLotNo") %>">
                                                        <asp:GridView runat="server" ID="gvfabfetails" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Inv.No" DataField="Refno" />
                                                                <asp:BoundField HeaderText="InvDate" DataField="InvDate" DataFormatString='{0:dd-MM-yyyy}' />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Master ID" DataField="Masterid" />
                                            <asp:BoundField HeaderText="Item Code" DataField="itemcode" />
                                            <asp:BoundField HeaderText="LotNo" DataField="LotNo" />
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="Company Lot No" />
                                            <asp:BoundField HeaderText="Width" DataField="width" />
                                            <asp:BoundField HeaderText="Cutting date" DataField="Deliverydate" DataFormatString='{0:dd/MM/yyyy}' />
                                            <asp:BoundField HeaderText="Master Cut date" DataField="CreatedDate" DataFormatString='{0:dd/MM/yyyy}' />
                                            <asp:BoundField HeaderText="Entry date" DataField="CreatedDate" DataFormatString='{0:dd/MM/yyyy}' />
                                            <asp:BoundField HeaderText="Cutting Master" DataField="Ledgername" />
                                            <asp:TemplateField HeaderText="Edit" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="edit">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("Masterid") %>' />
                                                    <%--<asp:HiddenField ID="idcheque" runat="server" Value='<%# Bind("FromCheque") %>' />
                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("ToCheque") %>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit Master Details">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btneditmasterdetails" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="EditC">
                                                        <asp:Image ID="ideditmasterdetails" Width="20px" runat="server" ImageUrl="~/images/stock_task.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="Delete" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="delete" OnClientClick="alertMessage()">
                                                        <asp:Image ID="dlt" runat="server" ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable1" ImageUrl="~/images/delete.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                                        CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btndelete"
                                                        PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                    <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                                        TargetControlID="btndelete" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                                    </ajaxToolkit:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Master Print">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="print">
                                                        <asp:Image ID="print" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="delete" OnClientClick="alertMessage()">
                                                        <asp:Image ID="dlt" runat="server" ImageUrl="~/images/DeleteIcon_btn.png" Visible="false" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable1" ImageUrl="~/images/delete.png" runat="server" Visible="true"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                                        CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btndelete"
                                                        PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                    <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                                        TargetControlID="btndelete" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                                    </ajaxToolkit:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center" HeaderText="Cutting Print">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint1" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="custprint">
                                                        <asp:Image ID="print1" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Damage Print" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint12" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="damprint">
                                                        <asp:Image ID="print12" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Fabric End Meter">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnfabprint" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="fabprint">EndMeter
                                                        <%--<asp:Image ID="printtt" runat="server" ImageUrl="~/images/Print_Icon.jpg" />--%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Stock Wise Ratio"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnstockratio" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="stockratio">ColorWise
                                                        <%--<asp:Image ID="stockprinttt" runat="server" ImageUrl="~/images/Print_Icon.jpg" />--%></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Pre Cost">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprecost" runat="server" CommandArgument='<%#Eval("Masterid") %>'
                                                        CommandName="PreCost">Cost
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:LinkButton Text="" ID="lnkFake" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
        TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" ScrollBars="Auto" Height="600px" Width="1200px"
        CssClass="modalPopup" Style="display: none">
        <div class="header">
            Process Status Details
        </div>
        <div class="body">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div class="table-responsive" id="divLot1" runat="server">
                            <div id="tabs" runat="server" style="background-color: #D0D3D6;">
                                <ul>
                                    <li><a href="#tabs-1">Fabric Details</a></li>
                                </ul>
                                <div class="row" id="tabs-1" style="background-color: White; padding-top: 30px">
                                    <div style="background-color: #D0D3D6;">
                                        <h2 align="center">
                                            <asp:Label ID="Label2" Style="color: Blue;" runat="server">Fabric Details</asp:Label></h2>
                                        <div class=" form-group">
                                            <div class="table-responsive">
                                                <table style="width: 85%">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="newgridfablist" Style="padding-left: 2pc" AutoGenerateColumns="False"
                                                                ShowFooter="True" CssClass="chzn-container" GridLines="None" Width="100%" runat="server">
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
                                                                            <asp:TextBox ID="newtxtAvlmeter" Enabled="false" Text='<%# Eval("Givenmeter","{0:n}")%>'
                                                                                runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Wt./gms." HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="newtxtreqmeter" Enabled="false" Text='<%# Eval("Givenmeter","{0:n}")%>'
                                                                                runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="End Bit(Wt./gms.)" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="newtxtendmeter" Text='<%# Eval("Endbit","{0:n}")%>' runat="server"
                                                                                CssClass="form-control"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Used Wt./gms." HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="newtxtttreqmeter" Enabled="false" Text='<%# Eval("reqmeter","{0:n}")%>'
                                                                                runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="selected_tab" runat="server" />
                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
                            <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
                            <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
                                rel="stylesheet" type="text/css" />
                            <script type="text/javascript">
                                var selected_tab = 1;
                                $(function () {
                                    var tabs = $("#tabs").tabs({
                                        select: function (e, i) {
                                            selected_tab = i.index;
                                        }
                                    });
                                    selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
                                    tabs.tabs('select', selected_tab);
                                    $("form").submit(function () {
                                        $("[id$=selected_tab]").val(selected_tab);
                                    });
                                });
    
                            </script>
                            <script type="text/javascript">
                                var selected_tab = 1;
                                $(function () {
                                    var tabs = $("#tabss").tabs({
                                        select: function (e, i) {
                                            selected_tab = i.index;
                                        }
                                    });
                                    selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
                                    tabs.tabs('select', selected_tab);
                                    $("form").submit(function () {
                                        $("[id$=selected_tab]").val(selected_tab);
                                    });
                                });
    
                            </script>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="footer" align="right">
            <asp:Label ID="lblcutid" runat="server" Visible="false"></asp:Label>
            <asp:Button ID="process" runat="server" Text="Process" OnClick="newfabclicknew" />
            <asp:Button ID="fabSave" runat="server" Text="Save" OnClick="fabsaveclick" />
            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
        </div>
    </asp:Panel>
    <asp:LinkButton Text="" ID="LinkButton1" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpecost" runat="server" PopupControlID="Panelmpecost"
        TargetControlID="LinkButton1" CancelControlID="Button3" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="Panelmpecost" runat="server" ScrollBars="Auto" Height="600px" Width="100%"
        CssClass="modalPopup" Style="display: none">
        <div class="header" align="center">
        </div>
        <div class="body">
            <div class="table-responsive" id="div1" runat="server">
                <div class="header" runat="server" style="text-align: center">
                    <asp:Label ID="lblTitle" Text="Add Pre Costing : " runat="server" Font-Bold="true"
                        Font-Size="Larger"></asp:Label>
                    <asp:Label ID="lblllotno" runat="server" Font-Bold="true" Font-Size="Larger"></asp:Label>
                </div>
                <div class="body">
                    <div id="Div2" runat="server">
                        <div class="row" id="Div6" style="background-color: White; padding-top: 30px">
                            <div style="background-color: #D0D3D6;">
                                <div class=" form-group">
                                    <div class="table-responsive">
                                        <table style="width: 100%">
                                            <tr>
                                                <td id="Td1" runat="server" style="width: 5%">
                                                </td>
                                                <td runat="server" style="width: 35%">
                                                    <asp:GridView runat="server" BorderWidth="1" ID="gvfabriccost" CssClass="myGridStyle"
                                                        GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                        ShowHeader="true" ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true"
                                                        OnRowDataBound="gvfabriccost_rowbound" Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="FABRIC Type" DataField="typ" />
                                                            <asp:BoundField HeaderText="Given Kg/Gms" DataFormatString='{0:f}' DataField="gvmeter" />
                                                            <asp:BoundField HeaderText="Used Kg/Gms" DataFormatString='{0:f}' DataField="usedmeter" />
                                                            <asp:BoundField HeaderText="End Kg/Gms" DataFormatString='{0:f}' DataField="endbit" />
                                                            <asp:BoundField HeaderText="Total Rate" DataFormatString='{0:f}' DataField="rate" />
                                                            <asp:BoundField HeaderText="Avg.Kg/Gms" DataFormatString='{0:f}' DataField="avgmeter" />
                                                            <asp:BoundField HeaderText="FABRIC Rate" DataFormatString='{0:f}' DataField="fabrate" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                                <td id="Td5" runat="server" style="width: 5%">
                                                </td>
                                                <td id="Td6" runat="server" style="width: 20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="Td3" runat="server" style="width: 5%">
                                                </td>
                                                <td>
                                                    <div align="center">
                                                        <label style="font-weight: bold">
                                                            Raw Material Usage</label>
                                                        <asp:GridView ID="gridrawmaterial" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                            ShowFooter="true" OnRowDataBound="Gridrawmaterial_rowdatabound" AutoGenerateColumns="false"
                                                            EmptyDataText="No data found!" ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="prodname" HeaderText="Accessories Code" ItemStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField DataField="Qty" HeaderText="Total Qty" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="purchaserate" HeaderText="Purchase Rate / Qty" DataFormatString='{0:f}'
                                                                    ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="totrate" HeaderText="Total Rate" DataFormatString='{0:f}'
                                                                ItemStyle-HorizontalAlign="Center" />--%>
                                                            </Columns>
                                                            <HeaderStyle BackColor="#990000" />
                                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                NextPageText="Next" PreviousPageText="Previous" />
                                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                                <td id="Td2" runat="server" style="width: 35%">
                                                    <asp:GridView runat="server" BorderWidth="1" ID="gvprocessaccesscost" CssClass="myGridStyle"
                                                        GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                        OnRowDataBound="gvprocessaccesscost_rowdatabound" ShowHeader="true" ShowFooter="true"
                                                        PrintPageSize="30" AllowPrintPaging="true" Style="font-family: 'Trebuchet MS';
                                                        font-size: 13px;">
                                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Process" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtProcessType" Enabled="true" Text='<%# Eval("Process")%>' Width="180px"
                                                                        runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cost" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtProcessCost" Enabled="true" Text='<%# Eval("Cost","{0:n}")%>'
                                                                        Width="70px" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </td>
                                                <td id="Td4" runat="server" style="width: 20%">
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="footer" align="right">
            <asp:Label ID="lblmastercostid" runat="server" Visible="false"></asp:Label>
            <asp:Button ID="btncostsave" runat="server" Text="Save" OnClick="btncostsave_OnClick" />
            <asp:Button ID="btncostupdate" runat="server" Text="Change" OnClick="btncostupdate_OnClick" />
            <asp:Button ID="Button3" runat="server" Text="Close" CssClass="button" />
        </div>
    </asp:Panel>
    <asp:LinkButton Text="" ID="lnkFake1" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1"
        TargetControlID="lnkFake1" CancelControlID="btnClose1" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlPopup1" runat="server" ScrollBars="Auto" Height="600px" Width="100%"
        CssClass="modalPopup" Style="display: none">
        <div class="header">
            STOCK WISE Details
        </div>
        <div class="body">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <div class="table-responsive" id="div3" runat="server">
                            <div id="Div4" runat="server">
                                Stock Wise Details
                                <div class="row" id="Div5" style="background-color: White; padding-top: 30px">
                                    <div style="background-color: #D0D3D6;">
                                        <div class=" form-group">
                                            <div class="table-responsive">
                                                <div class="col-lg-6" id="sizediv" runat="server" visible="false" style="margin-left: -3pc;">
                                                    <div class="panel panel-default" style="width: 170px">
                                                        <label>
                                                            Size</label>
                                                        <asp:CheckBoxList ID="chkSizes" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="chkChoice1"
                                                            runat="server">
                                                        </asp:CheckBoxList>
                                                        <asp:Label ID="lblstockgrandtot" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                </div>
                                                <table style="width: 100%">
                                                    <tr style="margin-right: 2pc" align="center">
                                                        <td>
                                                            <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myleft" GridLines="Both"
                                                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                                                ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true" Width="90%" Style="font-family: 'Trebuchet MS';
                                                                font-size: 13px; margin-right: -4pc">
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
                                                        </td>
                                                    </tr>
                                                    <tr style="margin-right: 2pc" align="left">
                                                        <td>
                                                            <asp:GridView ID="gridsize1" AutoGenerateColumns="False" ShowFooter="True" CssClass="chzn-container"
                                                                GridLines="None" runat="server" Width="90%" Style="margin-left: 2pc">
                                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" Visible="false" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtmasterid" Visible="false" Text='<%# Eval("Masterid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtcutid" Visible="false" Text='<%# Eval("cutid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtcompanyid" Visible="false" Text='<%# Eval("companyid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtcompanylotno" Visible="false" Text='<%# Eval("companylotno")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <asp:Label ID="lbltransfabid" Visible="false" Text='<%# Eval("Transfabid")%>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblbrandid" Visible="false" Text='<%# Eval("brandid")%>' runat="server"></asp:Label>
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
                                                                    <asp:TemplateField HeaderText="Item Code/Color" ControlStyle-Width="100%" ItemStyle-Width="5%"
                                                                        HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdesignno" Width="100%" TextMode="MultiLine" Enabled="false" Text='<%# Eval("designno")%>'
                                                                                runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("Fitid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtfit" Width="100%" Enabled="false" Text='<%# Eval("Fit")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30fs" Width="100%" Enabled="true" Text='<%# Eval("S30FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32fs" Width="100%" Enabled="true" Text='<%# Eval("S32FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34fs" Width="100%" Enabled="true" Text='<%# Eval("S34FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36fs" Width="100%" Enabled="true" Text='<%# Eval("S36FS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxsfs" Width="100%" Enabled="true" Text='<%# Eval("SXSFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtssfs" Width="100%" Enabled="true" Text='<%# Eval("SSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmfs" Width="100%" Enabled="true" Text='<%# Eval("SMFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslfs" Width="100%" Enabled="true" Text='<%# Eval("SLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlfs" Width="100%" Enabled="true" Text='<%# Eval("SXLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlfs" Width="100%" Enabled="true" Text='<%# Eval("SXXLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlfs" Width="100%" Enabled="true" Text='<%# Eval("S3XLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL FS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlfs" Width="100%" Enabled="true" Text='<%# Eval("S4XLFS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30hs" Width="100%" Enabled="true" Text='<%# Eval("S30HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32hs" Width="100%" Enabled="true" Text='<%# Eval("S32HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34hs" Width="100%" Enabled="true" Text='<%# Eval("S34HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36hs" Width="100%" Enabled="true" Text='<%# Eval("S36HS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxshs" Width="100%" Enabled="true" Text='<%# Eval("SXSHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsshs" Width="100%" Enabled="true" Text='<%# Eval("SSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmhs" Width="100%" Enabled="true" Text='<%# Eval("SMHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslhs" Width="100%" Enabled="true" Text='<%# Eval("SLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlhs" Width="100%" Enabled="true" Text='<%# Eval("SXLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlhs" Width="100%" Enabled="true" Text='<%# Eval("SXXLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlhs" Width="100%" Enabled="true" Text='<%# Eval("S3XLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL HS" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlhs" Width="100%" Enabled="true" Text='<%# Eval("S4XLHS")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Shirts" ItemStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttotal" Width="100%" Enabled="false" Text='<%# Eval("Total")%>'
                                                                                runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnstockprocess" OnClick="stockprocesswise_click" runat="server"
                Text="Process" CssClass="button" />
            <asp:Button ID="btnsavecilck" OnClick="stocksave_cilck" runat="server" Text="Save"
                CssClass="button" />
            <asp:Button ID="btnClose1" runat="server" Text="Close" CssClass="button" />
        </div>
    </asp:Panel>
    <asp:LinkButton Text="" ID="lnkFake2" runat="server"></asp:LinkButton>
    <ajaxToolkit:ModalPopupExtender ID="mpefab" runat="server" PopupControlID="Panel2"
        TargetControlID="lnkFake2" CancelControlID="btnClosefab" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" Height="450px" Width="900px" CssClass="modalPopup"
        Style="display: none">
        <div class="body">
            <div class="table-responsive" id="div7" runat="server">
                <table style="width: 100%">
                    <tr>
                        <label>
                            Pre-Cutting Detailed Report</label>
                        <td colspan="2">
                            <asp:Panel ID="Panelll1" runat="server" ScrollBars="Both" Height="200" Width="100%">
                                <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
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
                                        <asp:TemplateField HeaderText="Item Name" Visible="false" ControlStyle-Width="100%"
                                            ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtitemname" Enabled="false" Text='<%# Eval("Itemname")%>' runat="server"></asp:TextBox>
                                                <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
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
                            <asp:Panel ID="Panell1" runat="server" Height="200">
                                <asp:Button ID="btncal" runat="server" CssClass="btn btn-danger" Text="Calculate"
                                    OnClick="calc" />
                                <asp:GridView ID="gridsize" AutoGenerateColumns="False" ShowFooter="True" OnRowCommand="grid_sizerowcommand"
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
                                        <asp:TemplateField HeaderText="Item Name" Visible="false" ControlStyle-Width="100%"
                                            ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtitemname" Width="100%" Enabled="false" Text='<%# Eval("Itemname")%>'
                                                    runat="server"></asp:TextBox>
                                                <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand Name" Visible="false" ControlStyle-Width="100%"
                                            ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtledgerid" Visible="false" Text='<%# Eval("mrp")%>' runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtparty" Enabled="false" Width="100%" Text='<%# Eval("mrp")%>'
                                                    runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pattern Name" Visible="false" ControlStyle-Width="100%"
                                            ItemStyle-Width="2%" HeaderStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtpatternid" Visible="false" Text='<%# Eval("mrp")%>' runat="server"></asp:TextBox>
                                                <asp:TextBox ID="txtpatternname" Enabled="false" Text='<%# Eval("mrp")%>' runat="server"></asp:TextBox>
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
                                                <asp:TextBox ID="txtreqshirt" Width="100%" Enabled="false" Text='<%# Eval("reqshirt")%>'
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
                                                <asp:TextBox ID="txtdamage" Text='<%# Eval("damageqty")%>' Width="100%" runat="server"
                                                    Height="26px">0</asp:TextBox>
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
                                                <asp:Label ID="lblidd" Visible="true" Text='<%# Eval("reasonname")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comments" HeaderStyle-Width="13%" ItemStyle-Width="13%"
                                            ControlStyle-Width="100%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnarration" Text='<%# Eval("reason")%>' Width="100%" runat="server"
                                                    Height="26px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End bit" Visible="false" HeaderStyle-Width="13%" ItemStyle-Width="13%"
                                            ControlStyle-Width="100%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtendbit" Width="100%" runat="server" Height="26px">0</asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-Width="10%" ControlStyle-Width="100%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnupdate" runat="server" Text="Update" CommandName="UPD" CommandArgument='<%# Container.DataItemIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <%--<tr>
                                            <td colspan="2" id="Td8" runat="server" valign="top" align="left" style="width: 100%">
                                                <label style="font-weight: bold">
                                                    Master Cutting Details</label>
                                                <div>
                                                  
                                                </div>
                                            </td>
                                        </tr>--%>
                </table>
            </div>
        </div>
        <div class="footer" align="right">
            <asp:Label ID="Label5" runat="server" Style="display: none"></asp:Label>
            <asp:Button ID="btnClosefab" runat="server" Text="Close" CssClass="button" />
            <asp:Button ID="btneditfabdetails" Visible="false" runat="server" Text="Update" CssClass="button"
                OnClick="editmasterdetails" />
        </div>
    </asp:Panel>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Fabric Process:</div>
                <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                </div>
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure want to delete?
                </p>
            </div>
            <div class="popup_Buttons">
                <input id="ButtonDeleleOkay" type="button" value="Yes" />
                <input id="ButtonDeleteCancel" type="button" value="No" />
            </div>
        </div>
    </asp:Panel>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
