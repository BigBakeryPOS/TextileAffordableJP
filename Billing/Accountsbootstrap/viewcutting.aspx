<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewcutting.aspx.cs" Inherits="Billing.Accountsbootstrap.viewcutting" %>

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
    <title>Fabric Process - bootsrap</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <script src="../Scripts/jquerynew-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
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
        function alertMessage() {
            alert('Are You Sure, You want to delete This Customer!');
        }

        function switchViews(obj, imG) {
            var div = document.getElementById(obj);
            var img = document.getElementById(imG);
            if (div.style.display == "none") {
                div.style.display = "inline";


                img.src = "../images/minus.gif";

            }
            else {
                div.style.display = "none";
                img.src = "../images/plus.gif";

            }
        }
    </script>
    <%--<script language="javascript" type="text/javascript">
    $(document).ready(function ($) {

        $('#<%=gvcust.ClientID%>').Scrollable({

        });

    });
</script>--%>
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
                Pre-Cutting Process</h1>
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
                                <asp:Label ID="Label2" runat="server"></asp:Label><br />
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
                                <asp:Label ID="Label3" runat="server"></asp:Label><br />
                                <asp:Button ID="btnadd" runat="server" Visible="true" class="btn btn-danger" Text="New Ratio Wise Pre Cutting"
                                    Width="250px" PostBackUrl="~/Accountsbootstrap/RatiowisePreCut.aspx" />
                                <asp:Button ID="btnshirtadd" Visible="false" runat="server" class="btn btn-danger"
                                    Text="New Shirt Wise Pre Cutting" Width="250px" PostBackUrl="~/Accountsbootstrap/ShirtwisePreCut.aspx" />
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
                            <div class="col-lg-2">
                                <asp:Button ID="btnadd1" runat="server" class="btn btn-danger" Text="Add New Pre-Cutting"
                                    Width="150px" OnClick="Add1_Click" Visible="false" /><%--<asp:Button ID="Btnnew" runat="server" class="btn btn-danger" Text="Add New Cutting" Width="130px"  OnClick="Add1_Click" />--%>
                            </div>
                            <div runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"
                                    Width="130px" />
                            </div>
                            <div class="col-lg-15">
                                &nbsp;&nbsp;</div>
                            <div class="col-lg-17">
                                &nbsp;&nbsp;</div>
                        </div>
                    </div>
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvcust" EmptyDataText="No records Found" runat="server" CssClass="myGridStyle"
                                        AllowPaging="true" PageSize="1000" OnPageIndexChanging="Page_Change" AutoGenerateColumns="false"
                                        DataKeyNames="CompanyFullLotNo" OnRowCommand="gvcust_RowCommand" OnRowDataBound="gvcust_RowDataBound">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" HeaderText="FabDetails"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%-- <a href="javascript:switchViews('dv<%# Eval("CompanyFullLotNo") %>', 'imdiv<%# Eval("CompanyFullLotNo") %>');"
                                                        style="text-decoration: none;">
                                                        <img id="imdiv<%# Eval("CompanyFullLotNo") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                    </a>--%>
                                                    <div id="dv<%# Eval("CompanyFullLotNo") %>">
                                                        <asp:GridView runat="server" ID="gvfabfetails" AutoGenerateColumns="false" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Inv.No" DataField="Refno" />
                                                                <asp:BoundField HeaderText="InvDate" DataField="InvDate" DataFormatString='{0:dd-MM-yyyy}' />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Cutting ID" Visible="false" DataField="cutid" />
                                            <asp:BoundField HeaderText="Item Code" DataField="itemcode" />
                                            <asp:BoundField HeaderText="Work Type" DataField="typee1" Visible="false" />
                                            <asp:BoundField DataField="CompanyFullLotNo" HeaderText="Company Lot No" />
                                            <asp:BoundField DataField="LotCombination" Visible="false" HeaderText="Combinations" />
                                            <asp:BoundField HeaderText="LotNo" DataField="LotNo" />
                                            <asp:BoundField HeaderText="Width" DataField="width" />
                                            <asp:BoundField HeaderText="Issue date" DataField="Deliverydate" DataFormatString='{0:dd/MM/yyyy}' />
                                            <asp:BoundField HeaderText="Receive date" DataField="Deldate" DataFormatString='{0:dd/MM/yyyy}' />
                                              <asp:BoundField HeaderText="Entry date" DataField="CreatedDate" DataFormatString='{0:dd/MM/yyyy}' />
                                            <asp:BoundField HeaderText="Cutting Master" DataField="Ledgername" />
                                            <asp:BoundField HeaderText="Production Cost" Visible="false" DataField="Productcost" />
                                            <asp:BoundField HeaderText="Margin" Visible="false" DataField="Margin" />
                                            <asp:BoundField HeaderText="MRP" Visible="false" DataField="Mrp" />
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("cutid") %>'
                                                        CommandName="edit">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Edit" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("cutid") %>' />
                                                    <%--<asp:HiddenField ID="idcheque" runat="server" Value='<%# Bind("FromCheque") %>' />
                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("ToCheque") %>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("cutid") %>'
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
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Admin Print" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("cutid") %>'
                                                        CommandName="print">
                                                        <asp:Image ID="print" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Cutting Print">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint1" runat="server" CommandArgument='<%#Eval("cutid") %>'
                                                        CommandName="custprint">
                                                        <asp:Image ID="print1" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Add Process">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAddProcess" runat="server" CommandArgument='<%#Eval("cutid") %>'
                                                        CommandName="AddProcess">
                                                        <asp:Image ID="idAddProcess" Width="20px" runat="server" ImageUrl="~/images/edit_add.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit Cutting Details">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btneditcutdetails" runat="server" CommandArgument='<%#Eval("cutid") %>'
                                                        CommandName="EditC">
                                                        <asp:Image ID="ideditcutdetails" Width="20px" runat="server" ImageUrl="~/images/stock_task.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit Fabric details">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btneditfabdetails" runat="server" CommandArgument='<%#Eval("cutid") %>'
                                                        CommandName="EditF">
                                                        <asp:Image ID="ideditfabdetails" Width="20px" runat="server" ImageUrl="~/images/stock_task.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Label Print">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint12" runat="server" CommandArgument='<%#Eval("cutid") %>'
                                                        CommandName="labelprint">
                                                        <asp:Image ID="print12" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
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
                    <asp:LinkButton Text="" ID="lnkFake" runat="server"></asp:LinkButton>
                    <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                        TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup" runat="server" ScrollBars="Auto" Height="600px" Width="800px"
                        CssClass="modalPopup" Style="display: none">
                        <div class="header">
                            Add Process
                        </div>
                        <div class="body">
                            <label>
                                Select Mode</label>
                            <asp:RadioButtonList ID="rdbmode" RepeatDirection="Horizontal" Width="200px" CssClass="chkChoice"
                                runat="server">
                                <asp:ListItem Text="Add Process" Selected="True" Value="1"></asp:ListItem>
                                <%-- <asp:ListItem Text="LotCombination" Value="2"></asp:ListItem>--%>
                            </asp:RadioButtonList>
                            <br />
                            <table border="1">
                                <tr>
                                    <td style="width: 100px">
                                        <label>
                                            Process</label>
                                    </td>
                                    <td style="width: 150px">
                                        <label style="padding-left: 10px">
                                            In</label>
                                        <label style="padding-left: 60px">
                                            JobWork</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Embroiding</label>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Label ID="lblNchkemb" runat="server" Style="display: none"></asp:Label>
                                        <asp:RadioButtonList ID="Nchkemb" RepeatDirection="Horizontal" Width="200px" CssClass="chkChoice"
                                            runat="server">
                                            <asp:ListItem Text="" Value="In"></asp:ListItem>
                                            <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Stitching</label>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Label ID="lblNchkstch" runat="server" Style="display: none"></asp:Label>
                                        <asp:RadioButtonList ID="Nchkstch" RepeatDirection="Horizontal" RepeatColumns="2"
                                            Width="200px" CssClass="chkChoice" runat="server">
                                            <asp:ListItem Text="" Value="In"></asp:ListItem>
                                            <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td>
                                        <label>
                                            K.Button</label>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Label ID="lblNchkkbut" runat="server" Style="display: none"></asp:Label>
                                        <asp:RadioButtonList ID="Nchkkbut" RepeatDirection="Horizontal" RepeatColumns="2"
                                            Width="200px" CssClass="chkChoice" runat="server">
                                            <asp:ListItem Text="" Value="In"></asp:ListItem>
                                            <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td>
                                        <label>
                                            Washing</label>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Label ID="lblNchkwash" runat="server" Style="display: none"></asp:Label>
                                        <asp:RadioButtonList ID="Nchkwash" RepeatDirection="Horizontal" RepeatColumns="2"
                                            Width="200px" CssClass="chkChoice" runat="server">
                                            <asp:ListItem Text="" Value="In"></asp:ListItem>
                                            <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Printing</label>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Label ID="lblNchkprint" runat="server" Style="display: none"></asp:Label>
                                        <asp:RadioButtonList ID="Nchkprint" RepeatDirection="Horizontal" RepeatColumns="2"
                                            Width="200px" CssClass="chkChoice" runat="server">
                                            <asp:ListItem Text="" Value="In"></asp:ListItem>
                                            <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Ironing/Packing</label>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Label ID="lblNchkiron" runat="server" Style="display: none"></asp:Label>
                                        <asp:RadioButtonList ID="Nchkiron" RepeatDirection="Horizontal" RepeatColumns="2"
                                            Width="200px" CssClass="chkChoice" runat="server">
                                            <asp:ListItem Text="" Value="In"></asp:ListItem>
                                            <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td>
                                        <label>
                                            Bar Tag</label>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Label ID="lblNchkbartag" runat="server" Style="display: none"></asp:Label>
                                        <asp:RadioButtonList ID="Nchkbartag" RepeatDirection="Horizontal" RepeatColumns="2"
                                            Width="200px" CssClass="chkChoice" runat="server">
                                            <asp:ListItem Text="" Value="In"></asp:ListItem>
                                            <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td>
                                        <label>
                                            Trimming</label>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Label ID="lblNchktrimming" runat="server" Style="display: none"></asp:Label>
                                        <asp:RadioButtonList ID="Nchktrimming" RepeatDirection="Horizontal" RepeatColumns="2"
                                            Width="200px" CssClass="chkChoice" runat="server">
                                            <asp:ListItem Text="" Value="In"></asp:ListItem>
                                            <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td>
                                        <label>
                                            Consai</label>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Label ID="lblNchkconsai" runat="server" Style="display: none"></asp:Label>
                                        <asp:RadioButtonList ID="Nchkconsai" RepeatDirection="Horizontal" RepeatColumns="2"
                                            Width="200px" CssClass="chkChoice" runat="server">
                                            <asp:ListItem Text="" Value="In"></asp:ListItem>
                                            <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <%-- <table  border="1">
                                <tr >
                                   
                                        Type LotCombination :
                                    <asp:TextBox ID="txtLotCombination" Visible="false" runat="server" CssClass="form-control" Width="300px" placeholder="Type LotCombination"></asp:TextBox>
                                </tr>
                            </table>--%>
                            <br />
                        </div>
                        <div class="footer" align="right">
                            <asp:Label ID="lblcutid" runat="server" Style="display: none"></asp:Label>
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" OnClick="btnClear_OnClick" />
                            <asp:Button ID="btnAddprocess" runat="server" Text="Add Process" CssClass="button"
                                OnClick="btnAddprocess_OnClick" />
                        </div>
                    </asp:Panel>
                    <asp:LinkButton Text="" ID="lnkFake1" runat="server"></asp:LinkButton>
                    <ajaxToolkit:ModalPopupExtender ID="mpecut" runat="server" PopupControlID="Panel1"
                        TargetControlID="lnkFake1" CancelControlID="btnClosecut" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="Panel1" runat="server" BackColor="Aqua" ScrollBars="Auto" Height="600px"
                        Width="800px" CssClass="modalPopup" Style="display: none">
                        <%-- <asp:UpdatePanel ID="updpanel" runat="server" UpdateMode="Conditional" >
                        <ContentTemplate>--%>
                        <div align="center" class="header">
                            <b>Cutting Details</b>
                            <hr style="border-top: 1px solid #333" />
                        </div>
                        <div class="body" style="background-color: Red">
                            <div class="col-lg-12">
                                <asp:Label ID="lblcutidnew" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbljobtype" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblitemid" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblitemcode" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblitemlotno" runat="server" Visible="false"></asp:Label>
                                <div class="col-lg-6">
                                    <div class="form-group ">
                                        <label>
                                            Cutting/Job Work Master</label>
                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpcutting" ValueToCompare="Select Cutting"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please select Cutting Master!"></asp:CompareValidator>
                                        <asp:DropDownList ID="drpcutting" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group ">
                                        <div class="col-lg-6">
                                            <label>
                                                Select Item :
                                            </label>
                                            <asp:DropDownList ID="drpitemtype" runat="server" CssClass="form-control" OnSelectedIndexChanged="Itemlotnumber_chnaged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-6">
                                            <label>
                                                Item Lot No:
                                            </label>
                                            <br />
                                            <div>
                                                <asp:Label ID="lblitemlotcode" Font-Bold="true" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtitemlotno" Enabled="false" Style="margin-left: 3pc; margin-top: -1pc;
                                                    width: 71%;" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                    <div class="form-group">
                                        <br />
                                        <label>
                                            Cutting Issue Date:</label>
                                        <asp:TextBox ID="txtdate" runat="server" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtdate" runat="server"
                                            Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Receive Date:</label>
                                        <asp:TextBox ID="txtdeliverydate" runat="server" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtdeliverydate"
                                            runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div>
                                        <label>
                                            Brand Name</label>
                                        <asp:CompareValidator ID="CompareValidator7" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlbrand" ValueToCompare="Select Brand Name"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please select Brand name!"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlbrand" OnSelectedIndexChanged="brandindexchnaged" AutoPostBack="true"
                                            runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <br />
                                    <div id="fitdiv" runat="server">
                                        <label>
                                            Fit Label</label>
                                        <asp:DropDownList ID="drpNchkfit" class="form-control" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div id="Div21" runat="server">
                                        <label>
                                            Sleeve</label>
                                        <asp:DropDownList ID="drpnewsleevetype" class="form-control" runat="server">
                                            <asp:ListItem Text="Half Sleeve" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Full Sleeve" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3/4 Sleeve" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div id="Div23" runat="server">
                                        <label>
                                            Label</label>
                                        <asp:DropDownList ID="drpnewlabeltype" class="form-control" runat="server">
                                            <asp:ListItem Text="Half (Blue)" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Full (Blue)" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Half (Pink)" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Full (Pink)" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="footer" align="right">
                            <asp:Label ID="Label4" runat="server" Style="display: none"></asp:Label>
                            <asp:Button ID="btnClosecut" runat="server" Text="Close" CssClass="button" />
                            <asp:Button ID="btneditcutdetails" runat="server" Text="Update" CssClass="button"
                                OnClick="btneditcut_OnClick" />
                        </div>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </asp:Panel>
                    <asp:LinkButton Text="" ID="lnkFake2" runat="server"></asp:LinkButton>
                    <ajaxToolkit:ModalPopupExtender ID="mpefab" runat="server" PopupControlID="Panel2"
                        TargetControlID="lnkFake2" CancelControlID="btnClosefab" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="600px" Width="800px"
                        CssClass="modalPopup" Style="display: none">
                        <div class="header">
                            Fabric Details
                        </div>
                        <div class="body">
                            <div class="table-responsive" id="divLot1" runat="server">
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="2" id="Td1" runat="server" valign="top" align="left" style="width: 100%">
                                            <label style="font-weight: bold">
                                                Fabric Usage</label>
                                            <div>
                                                <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myGridStyle"
                                                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                                    ShowHeader="true" ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true"
                                                    Width="100%" Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcuttingid" runat="server" Visible="false" Text='<%# Eval("cutid")%>'></asp:Label>
                                                                <asp:Label ID="lbltransfabid" runat="server" Visible="false" Text='<%# Eval("transfabid")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="FABRIC Type" DataField="shirttype" />
                                                        <asp:BoundField HeaderText="FABRIC Name" DataField="designno" />
                                                        <asp:BoundField HeaderText="Current Avl.KG" DataFormatString='{0:f}' DataField="Avaliablemeter" />
                                                        <asp:BoundField HeaderText="Used KG" DataFormatString='{0:f}' DataField="reqmeter" />
                                                        <asp:TemplateField HeaderText="Alter KG">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblshirttype" runat="server" Visible="false" Text='<%# Eval("shirttype")%>'></asp:Label>
                                                                <asp:Label ID="lblavlkg" runat="server" Visible="false" Text='<%# Eval("Avaliablemeter")%>'></asp:Label>
                                                                <asp:Label ID="lblreqkg" runat="server" Visible="false" Text='<%# Eval("reqmeter")%>'></asp:Label>
                                                                <asp:TextBox ID="txtchnagekg" runat="server" Text='<%# Eval("reqmeter")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="footer" align="right">
                            <asp:Label ID="Label5" runat="server" Style="display: none"></asp:Label>
                            <asp:Button ID="btnClosefab" runat="server" Text="Close" CssClass="button" />
                            <asp:Button ID="btneditfabdetails" runat="server" Text="Update" CssClass="button"
                                OnClick="btneditfab_OnClick" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </div>
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
    </form>
</body>
</html>
