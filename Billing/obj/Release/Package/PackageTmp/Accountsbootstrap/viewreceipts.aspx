<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewreceipts.aspx.cs" Inherits="Billing.Accountsbootstrap.viewreceipts" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <title></title>
    <link rel="Stylesheet" type="text/css" href="../Styles/date.css" />
    <link rel="Stylesheet" type="text/css" href="../Styles/style1.css" />
    <%--<script type="text/javascript" src="../jquery-1.6.2.min.js"></script>
<script type="text/javascript" src="../jquery-ui-1.8.15.custom.min.js"></script>
<link rel="stylesheet" href="../jqueryCalendar.css"/>--%>
    <!-- Bootstrap Core CSS -->
    <link href="../Styles/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../Styles/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../Styles/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../Styles/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link href="../images/fav.ico" type="image/x-icon" rel="Shortcut Icon" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript">
        //         function pageLoad() {
        //             ShowPopup();
        //             setTimeout(HidePopup, 2000);
        //         }

        //         function ShowPopup() {
        //             $find('modalpopup').show();
        //             //$get('Button1').click();
        //         }

        //         function HidePopup() {
        //             $find('modalpopup').hide();
        //             //$get('btnCancel').click();
        //         }
    </script>
    <script type="text/javascript">
        function alertMessage() {
            alert('Are You Sure, You want to delete This Customer!');
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
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="Form1" runat="server">
    
    <div class="col-lg-12" >
        <div class="col-lg-2">
            <h2 style="text-align: left; color: #fe0002;">
                Receipt</h2>
        </div>
        <div class="col-lg-2">
                <asp:TextBox CssClass="form-control" Enabled="true" ID="txtsearch1" runat="server"
                    placeholder="Search Text (Date Enter yyyy/MM/dd)" onkeyup="Search_Gridview(this, 'gvledgrid')" Style="width: 270px;margin-left: -65px;"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch1"
                    ErrorMessage="Please enter your searching Data!" Text="." Style="color: White" />
                <asp:TextBox CssClass="form-control" Enabled="true" ID="txtsearch" runat="server"
                    placeholder="Search Text" Visible="false" Style="width: 170px; margin-top: -54px;
                    margin-left: 290px;"></asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=""
                    TargetControlID="txtsearch" />
            </div>
            <div class="col-lg-2">
                
                   
                                       
                                        <asp:DropDownList CssClass="form-control" OnSelectedIndexChanged="searchchange" Visible="true"
                                            AutoPostBack="false" ID="ddlfilter" Style="width: 170px"
                                            runat="server">
                                            <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Receipt No" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Receipt Date" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Ledger Name" Value="3"></asp:ListItem>
                                            <%--<asp:ListItem Text="" Value="4"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                         <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                            Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                                            Text="." Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                  
                                    </div>
            
            <div runat="server" visible="false" id="dateserach">
                <div class="col-lg-2">
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="datee" ControlToValidate="txtdate"
                        ErrorMessage="Please enter your searching Data!" Text="." Style="color: White" />
                </div>
                <div class="col-lg-2">
                    <asp:TextBox CssClass="form-control" Enabled="true" ID="txtdate" runat="server" placeholder="--Select Date--"
                        Style="width: 170px; margin-top: -54px; margin-left: 290px;"></asp:TextBox>
                </div>
                <%--<asp:TextBox CssClass="form-control" ID="txtdate" runat="server" Text="--Select Date--"></asp:TextBox>--%>
            </div>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" runat="server"
                Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </ajaxToolkit:CalendarExtender>
            <div class="col-lg-2">
                <asp:Button ID="btnsearch" runat="server" class="btn btn-success" ValidationGroup="val1"
                    Text="Search" OnClick="Search_Click" Style="width: 120px" />
            </div>
            <div class="col-lg-2">
                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"
                    Style="width: 120px" />
            </div>
            <div class="col-lg-2">
                <asp:Button ID="btnnew" runat="server" CssClass="btn btn-danger" Text="Add New" OnClick="btnnew_Click"
                    Style="width: 120px" />
            </div>
       
        <div runat="server" id="normal">
    </div>
    </div>
    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <%-- <div class="row" style="margin-top:100px" >
                <div class="col-lg-12">
                    <h1 class="page-header">Ledger Master</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>--%>
    <div class="row">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
            ID="val1" ShowMessageBox="true" ShowSummary="false" />
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblSelectedValue"></asp:Label>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="Table1" visible="false" runat="server" style="border: 1px solid Grey;
                                        height: 15px; background-color: #59d3b4; color: Black; text-transform: uppercase"
                                        width="100%">
                                        <tr>
                                            <td align="center" runat="server" visible="false" style="font-size: small; width: 0%">
                                                TransNo
                                            </td>
                                            <td align="center" style="font-size: small; width: 1%">
                                                ReceiptNo
                                            </td>
                                            <td align="center" style="font-size: small; width: 0%">
                                                TransDate
                                            </td>
                                            <td align="center" style="font-size: small; width: 0%">
                                                Creditor
                                            </td>
                                            <td align="center" style="font-size: small; width: 0%">
                                                Payment_Mode
                                            </td>
                                            <td align="center" style="font-size: small; width: 0%">
                                                Narration
                                            </td>
                                            <td align="center" style="font-size: small; width: 0%">
                                                Amount
                                            </td>
                                            <td align="center" style="font-size: small; width: 0%">
                                                Print
                                            </td>
                                            <td align="center" style="font-size: small; width: 0%">
                                                Edit
                                            </td>
                                            <td align="center" style="font-size: small; width: 1%">
                                                Delete
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="overflow: auto">
                                        <asp:GridView ID="gvledgrid" runat="server" EmptyDataText="No records Found" Width="100%"
                                            AllowPaging="true" PageSize="50" AutoGenerateColumns="false" CssClass="myGridStyle1"
                                            OnRowCommand="gvledgrid_RowCommand" OnPageIndexChanging="Page_Change" AllowSorting="true"
                                            OnSorting="gridview_Sorting">
                                            <HeaderStyle BackColor="#3366FF" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <%--<asp:BoundField HeaderText="Category ID" DataField="CategoryID" />--%>
                                                <asp:BoundField SortExpression="TransNo" Visible="false" HeaderStyle-ForeColor="Black"
                                                    ItemStyle-Width="5%" DataField="TransNo" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField SortExpression="ReceiptNo" HeaderText="Receipt No" HeaderStyle-ForeColor="Black"
                                                    ItemStyle-Width="4%" DataField="ReceiptNo" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField SortExpression="TransDate" HeaderText="Receipt Date" HeaderStyle-ForeColor="Black"
                                                    ItemStyle-Width="8%" DataField="TransDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField SortExpression="Creditor" HeaderText="Ledger Name" HeaderStyle-ForeColor="Black"
                                                    ItemStyle-Width="10%" DataField="creditor" />
                                                <asp:BoundField SortExpression="Payment_Mode" HeaderText="Payment Mode" HeaderStyle-ForeColor="Black"
                                                    ItemStyle-Width="5%" DataField="Payment_Mode" />
                                                <asp:BoundField SortExpression="Narration" HeaderText="Narration" HeaderStyle-ForeColor="Black"
                                                    ItemStyle-Width="30%" DataField="Narration" />
                                                <asp:BoundField SortExpression="Amount" HeaderStyle-ForeColor="Black" HeaderText="Amount"
                                                    ItemStyle-Width="4%" DataField="Amount" DataFormatString="{0:f2}" />
                                                <asp:TemplateField ItemStyle-Width="3%" HeaderText="Print" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnPrint" CommandArgument='<%#Eval("ReceiptNo") %>' CommandName="Print"
                                                            runat="server">
                                                            <asp:Image ID="imprint" ImageUrl="~/images/Print_Icon.jpg" runat="server" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField ItemStyle-Width="3%" HeaderText="PrintNEW" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnPrintNew" CommandArgument='<%#Eval("ReceiptNo") %>' CommandName="PrintNew"
                                                            runat="server">
                                                            <asp:Image ID="imprintNew" ImageUrl="~/images/Print_Icon.jpg" runat="server" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="3%" HeaderText="Edit" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("TransNo") %>' CommandName="EditRow"
                                                            runat="server">
                                                            <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="2%" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndel" CommandArgument='<%#Eval("TransNo") %>' CommandName="Del"
                                                            runat="server" OnClientClick="alertMessage()">
                                                            <asp:Image ID="Image1" ImageUrl="~/images/DeleteIcon_btn.png" runat="server" /></asp:LinkButton>
                                                        <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                                            CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btndel"
                                                            PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                        <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                                            TargetControlID="btndel" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                                        </ajaxToolkit:ConfirmButtonExtender>
                                                        <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- /#page-wrapper -->
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Receipt:</div>
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
    <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
    </form>
</body>
</html>
