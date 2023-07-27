<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstStageProcess.aspx.cs"
    Inherits="Billing.Accountsbootstrap.FirstStageProcess" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 900px;
            text-align: center;
            border: 3px solid #0DA9D0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 40px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            padding: 5px;
        }
        .modalPopup .footer
        {
            padding: 3px;
        }
        .modalPopup .button
        {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
        .modalPopup td
        {
            text-align: left;
        }
        
        .pad
        {
            padding-top: 50px;
        }
    </style>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <script src="../jqueryCalendar/script.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <%-- <script type="text/javascript">
        jQuery(function () {
            jQuery("#inf_custom_someDateField").datepicker();
        });

       

    </script>--%>
    <%--<script type="text/javascript">
                @{ 
var db = Database.Open("SmallBakery"); 
var dbdata = db.Query("select c.Definition,a.UnitPrice from tblStock a,tblcategory  b,tblCategoryUser c where a.CategoryID=b.categoryid and a.SubCategoryID=c.CategoryUserID"); 
var myChart = new Chart(width: 600, height: 400) 
  .AddTitle("Product Sales") 
  .DataBindTable(dataSource: dbdata, xField: "c.Definition") 
  .Write();
}
    </script>--%>
    <script language="javascript" type="text/javascript">

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else

                event.returnValue = false;
        }

    </script>
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
    <title>First Stage Process </title>
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
    <script type="text/javascript">
        function myFunction() {
            window.open("http://localhost:49197/Accountsbootstrap/itempage.aspx?Mode=Sales", "Popup", 'height=300,width=500,resizable=yes,modal=yes,center=yes');
        }
    </script>
    <script type="text/javascript">
        function AddVendor() {
            window.open("http://localhost:49197/Accountsbootstrap/customermaster.aspx?Mode=Sales", "Popup", 'height=300,width=500,resizable=yes,modal=yes,center=yes');
        }
    </script>
    <style>
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
    <link rel="stylesheet" href="../css/chosen.css" />
    <script type="text/javascript">
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
    <form id="Form1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <h2 style="text-align: left; color: #fe0002; margin-top: 0px">
                <asp:Label ID="lblTitle" Text="First Stage Process" runat="server"></asp:Label></h2>
            <h4>
                <div id="Div1" runat="server" visible="false" style="text-align: right; margin-top: -30px;
                    margin-right: 15px;">
                    <%--<asp:Button  ID="btnref" style="margin-top:-47px;margin-left:120px" runat="server" Text="View RefNo" OnClick="Refbutton_click" />--%>
                    <input type="button" value="View RefNo" onclick="window.open('RefHistory.aspx','popUpWindow','height=500,width=900,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">
                    &nbsp;&nbsp
                    <asp:Button ID="gridbutton" Style="margin-top: -47px; margin-left: 132px" runat="server"
                        Text="View grid" OnClick="gridbutton_click" />
                    &nbsp;&nbsp
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="New Customer" OnClientClick="window.open('customermaster.aspx?name=Add%20New');"></asp:LinkButton>
                </div>
            </h4>
            <!-- /.col-lg-12 -->
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                            <div class="row">
                                <!-- /.col-lg-12 -->
                            </div>
                            <!-- /.row -->
                            </br>
                            <div id="add" runat="server" class="row">
                                <div class="col-lg-12" style="margin-top: -35px">
                                    <div class="panel-body">
                                        <div>
                                            <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                            <table class="table table-striped table-bordered table-hover" id="Table1" width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="260" Width="100%">
                                                            <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                                OnRowDataBound="GridView2_RowDataBound" OnRowCommand="Gridview1_SelectedIndexChanged"
                                                                OnRowDeleting="GridView2_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                                Width="100%" runat="server">
                                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" ControlStyle-Width="100%" ItemStyle-Width="4%"
                                                                        HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Right"
                                                                        ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpitem" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Text="Shirt" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Casual" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Trousers" Value="3"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Supplier" ControlStyle-Width="100%" ItemStyle-Width="20%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpsupplier" CssClass="chzn-select" runat="server">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Design No" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdesno" Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Color" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtcolor" Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ordered/Unordered" ControlStyle-Width="100%" HeaderStyle-Width="5%"
                                                                        ItemStyle-Width="8%">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="drpord" Width="75%" Height="26px" runat="server">
                                                                                <asp:ListItem Text="O" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="U" Value="2"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mtr.Rate" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender0312" runat="server"
                                                                                TargetControlID="txtmeter" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txtmeter" Height="30px" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="WSP" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender03452" runat="server"
                                                                                TargetControlID="txtWSP" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txtWSP" Height="30px" runat="server">.00</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MRP" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                                                        <ItemTemplate>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender03472" runat="server"
                                                                                TargetControlID="txtMRP" ValidChars="." FilterType="Numbers, Custom" />
                                                                            <asp:TextBox ID="txtMRP" Height="30px" runat="server">.00</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Add" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnadd" Text="Add" runat="server" OnClick="ButtonAdd1_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Add Color" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnaddColor" Text="AddCol" runat="server" OnClick="ButtonAdd2_Click1" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <asp:LinkButton Text="" ID="lnkFake" runat="server"></asp:LinkButton>
                                                        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                                                            TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                        <asp:Panel ID="pnlPopup" runat="server" ScrollBars="Auto" Height="600px" Width="900px"
                                                            CssClass="modalPopup" Style="display: none">
                                                            <div class="header">
                                                                History
                                                            </div>
                                                            <div class="body">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Updates Avaliable For this Item"
                                                                                AllowPaging="True" PageSize="50000" CssClass="myGridStyle">
                                                                                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
                                                                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Bill No" DataField="BillNo" />
                                                                                    <asp:BoundField HeaderText="Bill Date" DataField="BillDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                                                    <asp:BoundField HeaderText="Customer Name" DataField="LedgerName" />
                                                                                    <asp:BoundField HeaderText="Item" DataField="Definition" />
                                                                                    <asp:BoundField HeaderText="Total Amount" DataField="Amount" DataFormatString="{0:f}" />
                                                                                    <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                                                                    <asp:BoundField HeaderText="Unit Price" DataField="unitprice" DataFormatString="{0:f}" />
                                                                                </Columns>
                                                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                                                <PagerStyle CssClass="pgr"></PagerStyle>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="footer" align="right">
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                                                            </div>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td1" runat="server" visible="false" align="right">
                                                        <asp:Button ID="ButtonAdd1" runat="server" EnableTheming="false" Text="Add New" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%-- <td align="right" style="width:58%">
                                                        <label style="margin-top: 11px;">
                                                            Dis</label>
                                                    </td>
                                                    <td align="left" style="width:23%">--%>
                                                    <%--<asp:TextBox CssClass="form-control" ID="txtdiscount" OnTextChanged="granddiscount" AutoPostBack="true" runat="server" Style="width: 150px;
                                                  <%--          text-align: right">0</asp:TextBox>--%>
                                                    <%--</td>
                                                </tr>--%>
                                                    <%--<tr class="odd gradeX">
                                                        <td align="right">
                                                            <label style="margin-top: 11px;">
                                                                TAX 5 %</label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control" ID="txtTaxamt5" runat="server" Style="width: 150px;
                                                                text-align: right">0</asp:TextBox>
                                                        </td>
                                                    </tr>--%>
                                                    <%--  <tr>
                                                    <td align="right" style="width:58%">
                                                        <label style="margin-top: 11px;">
                                                            TAX</label>
                                                    </td>
                                                    <td align="left" style="width:23%">--%>
                                                    <%--<asp:TextBox CssClass="form-control" ID="txtTaxamt" runat="server" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>--%>
                                                    <%--         </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="width:58%">
                                                            <label style="margin-top: 11px;">
                                                                Grand Total</label>
                                                        </td>
                                                        <td align="left" style="width:23%">--%>
                                                    <%-- <asp:TextBox CssClass="form-control" ID="txtgrandtotal" runat="server" Style="width: 150px;
                                                            text-align: right">0</asp:TextBox>--%>
                                                    <%-- </td>
                                                </tr>--%>
                                                    <%-- <tr class="odd gradeX">
                                                        <td align="right" >
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btncalc" runat="server" Text="Calc" CssClass="btn btn-success" Style="width: 100px;
                                                                margin-left: 30px; margin-top: -4px;" OnClick="btncalc_Click" />
                                                            <asp:RequiredFieldValidator ID="txtgt" ValidationGroup="val1" ControlToValidate="txtgrandtotal"
                                                                ErrorMessage="Please calculate your Grand Total" runat="server"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>--%>
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
                                        </div>
                                        <br />
                                        <asp:Button ID="btnadd" AccessKey="s" runat="server" class="btn btn-info" BorderWidth="3px"
                                            BorderColor="#e41300" BorderStyle="Inset" OnClick="Add_Click" onmouseover="this.style.backgroundColor='#5bc0de'"
                                            onmousedown="this.style.backgroundColor='olive'" onfocus="this.style.backgroundColor='#1b293e'"
                                            Text="Save" ValidationGroup="val1" Width="120px" />
                                        <asp:Button ID="btnExit" AccessKey="d" runat="server" class="btn btn-warning" BorderColor="Black"
                                            OnClick="btnExit_Click" Text="Exit" Width="120px" />
                                        <%-- <asp:Button ID="btnDelete" runat="server" class="btn btn-success" BorderColor="Black"
                                            OnClick="btnDelete_Click" Text="Delete" Width="120px" />--%>
                                    </div>
                                </div>
                            </div>
                            <div id="bulk" runat="server" align="center">
                                <table cellpadding="1" cellspacing="2" width="450px" style="border: 1px solid blue;
                                    height: 150px;">
                                    <tr class="headerPopUp">
                                        <td id="Td2" runat="server" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table style="width: 100%">
                                                <tr style="height: 15px">
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                    </td>
                                                    <td style="width: 35%">
                                                        <div>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <%--<asp:FileUpload ID="FileUpload1" runat="server" />
                                                                     <asp:Button ID="btnUpload" runat="server" Height="31px" class="btn btn-info" Text="Upload"
                                                                        Width="100px" OnClick="btnUpload_Click" />--%>
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                    <asp:Button ID="btnAsyncUpload" Visible="false" runat="server" Text="Async_Upload" OnClick="Async_Upload_File" />
                                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload_File" />
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAsyncUpload" EventName="Click" />
                                                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <asp:GridView ID="GridView2" runat="server">
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                    <td style="width: 35%">
                                                    </td>
                                                </tr>
                                                <tr style="height: 6px">
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 30%">
                                                                </td>
                                                                <td style="width: 35%" align="center">
                                                                </td>
                                                                <td style="width: 35%">
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 10px">
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 15%">
                                                                </td>
                                                                <td style="width: 70%" align="center">
                                                                    <asp:Button ID="Button1" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click" />
                                                                </td>
                                                                <td style="width: 15%">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- /.panel -->
                        </div>
                    </div>
                    <!-- /.row -->
                </div>
                <!-- /#page-wrapper -->
            </div>
            </div> </div> </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvcustomerorder" EventName="RowCommand" />
        </Triggers>
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
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    </form>
</body>
</html>
