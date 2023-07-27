<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuttingProcessEntryGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.CuttingProcessEntryGrid" %>

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
    <title>Cutting Process Entry</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                font-weight: bold">
                Cutting Process Entry</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                      <div class="row">
                        <div class="col-lg-12" align="center">
                         <div class="col-lg-1">
                                <div class="form-group" id="FromDate" runat="server" visible="true">
                                    <asp:Label ID="lblFrom" runat="server" Text="From" Width="110px" Style="font-weight: bold">
                                    </asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Width="110px">
                                    </asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                        PopupButtonID="txtOrderDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group" id="ToDate" runat="server" visible="true">
                                    <asp:Label ID="lblTo" runat="server" Text="To" Width="110px" Style="font-weight: bold">
                                    </asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="110px">
                                    </asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                        PopupButtonID="txtOrderDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search"
                                    OnClick="btnSearch_OnClick" Width="110px" />
                            </div>
                            <div class="col-lg-1">
                            </div>
                            <div class="col-lg-2">
                            <br />
                                <asp:DropDownList ID="drpyear" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpyear_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div id="Div1" runat="server" visible="false" class="col-lg-16">
                                <asp:Label runat="server" ID="Label1"></asp:Label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Style="color: White" initialvalue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                                    Text="*" Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                <asp:DropDownList CssClass="form-control" ID="ddlfilter" Width="150px" Style="margin-top: -20px"
                                    runat="server">
                                    <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Contact Name" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="MobileNo" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-16">
                            <br />
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="*" Style="color: White" />
                                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvCuttingProcessEntry')"
                                    ID="txtsearch" runat="server" Style="margin-top: -20px" placeholder="Enter Text to Search"
                                    Width="168px"></asp:TextBox>
                            </div>
                            <div id="Div2" runat="server" visible="false" class="col-lg-17">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsearch" runat="server" ValidationGroup="val1" class="btn btn-success"
                                    Text="Search" OnClick="Search_Click" Width="100px" />
                            </div>
                            <div id="Div3" runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"
                                    Width="130px" />
                            </div>
                            <div class="col-lg-4">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                            <div class="col-lg-17">
                                <asp:Button ID="btnadd" runat="server" class="btn btn-primary" Text="Add New" OnClick="Add_Click"
                                    Width="130px" />
                            </div>
                            <div id="Div4" runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="Button3" runat="server" class="btn btn-success" Text="Bulk Addition"
                                    Width="130px" />
                            </div>
                            <div class="col-lg-17">
                                <asp:Button ID="btnexcel" runat="server" class="btn btn-info" Text="Export-To-Excel"
                                    Width="130px" Height="32px" Visible="false" />
                            </div>
                        </div>
                    </div>
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvCuttingProcessEntry" runat="server" CssClass="myGridStyle1" OnRowDataBound="gvCuttingDetails2_OnRowDataBound"
                                        Width="100%" AutoGenerateColumns="false" OnRowCommand="gvCuttingProcessEntry_OnRowCommand">
                                        <HeaderStyle BackColor="White" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Pro.Order Print" ItemStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnproPrint" runat="server" CommandArgument='<%#Eval("ProcessEntryId") %>'
                                                        CommandName="ProPrint">
                                                        <asp:Image ID="imgPrintpro" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="EntryNo " DataField="FullEntryNo" />
                                            <asp:BoundField HeaderText="EntryDate" DataField="EntryDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                            <asp:BoundField HeaderText="JobWorker/Inhouse" DataField="Supplier" />
                                            <asp:BoundField HeaderText="ProcessFrom " Visible="false" DataField="ProcessFrom" />
                                            <asp:BoundField HeaderText="Process " DataField="Process" />
                                            <asp:BoundField HeaderText="Dlv.Date" DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="Iss" DataField="TotalIssued" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Rec" DataField="TotalReceived" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Dmg" DataField="TotalDamaged" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Issued Amount" DataField="Totamnt" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Want To Pay" DataField="WTotamnt" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Debit Amount" DataField="DTotamnt" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("ProcessEntryId") %>'
                                                        CommandName="edit">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receive" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnReceive" runat="server" CommandArgument='<%#Eval("ProcessEntryId") %>'
                                                        CommandName="Receive">
                                                        <asp:Image ID="imgReceive" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisableReceive" ImageUrl="~/images/edit.png" runat="server"
                                                        Visible="false" Enabled="false" ToolTip="Not Allow To Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Isu Print" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPrint" runat="server" CommandArgument='<%#Eval("ProcessEntryId") %>'
                                                        CommandName="IssuePrint">
                                                        <asp:Image ID="imgPrint" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RecPrint" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRecPrint" runat="server" CommandArgument='<%#Eval("ProcessEntryId") %>'
                                                        CommandName="ReceivePrint">
                                                        <asp:Image ID="imgRecPrint" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("ProcessEntryIdandProcess") %>'
                                                        CommandName="delete1" OnClientClick="alertMessage()">
                                                        <asp:Image ID="dlt" runat="server" ImageUrl="~/images/DeleteIcon_btn.png" Visible="true" /></asp:LinkButton>
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
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:GridView ID="gvSketches" CssClass="Gridview" runat="server" AutoGenerateColumns="true"
                                        GridLines="None" ShowHeader="false">
                                        <HeaderStyle CssClass="headerstyle" />
                                        <Columns>
                                            <%--<asp:ImageField DataImageUrlField="Column0" ItemStyle-Height="18px" ItemStyle-Width="1px" />--%>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="GridView1" CssClass="Gridview" runat="server" AutoGenerateColumns="false"
                                        GridLines="None" ShowHeader="false">
                                        <HeaderStyle CssClass="headerstyle" />
                                        <Columns>
                                            <asp:ImageField DataImageUrlField="Sketch" ItemStyle-Height="18px" ItemStyle-Width="1px" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- /.col-lg-6 (nested) -->
                </div>
                <!-- /.row (nested) -->
            </div>
            <!-- /.panel-body -->
        </div>
        <!-- /.panel -->
    </div>
    <!-- /.col-lg-12 -->
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    
     <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                   Cutting ProcessEntry:-</div>
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
