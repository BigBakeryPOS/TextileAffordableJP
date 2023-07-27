<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewcuttingBC.aspx.cs"
    Inherits="Billing.Accountsbootstrap.viewcuttingBC" %>

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
    <script type="text/javascript">
        function Denomination() {


            var gridData = document.getElementById('gvcust');



            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();


            var prtWindow = window.open(windowUrl, windowName,
           'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');



            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();


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
                                <div class="form-group">
                                    <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                        OnTextChanged="txtFromDate_TextChanged" Width="100px"></asp:TextBox>
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
                                        OnTextChanged="txtToDate_TextChanged" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                        PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="lblsupplier" runat="server">Job Worker</asp:Label>
                                <asp:DropDownList ID="ddlsupplier" runat="server" AutoPostBack="true" class="chzn-select"
                                    Width="190px" Height="80px" OnSelectedIndexChanged="ddlsupplier_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <br />
                                <asp:TextBox CssClass="form-control" onkeyup="Search_Gridview(this, 'gvcust')" Enabled="true"
                                    ID="txtsearch" runat="server" Style="margin-top: -20px" placeholder="Search Text"
                                    Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="." Style="color: White" /><br />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,custom" ValidChars=" /-"
                                    TargetControlID="txtsearch" />
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <div class="form-group ">
                                    <asp:DropDownList ID="drpbranch" OnSelectedIndexChanged="company_SelectedIndexChnaged"
                                        AutoPostBack="true" runat="server" CssClass="form-control" Width="170px">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <asp:Button ID="btnadd1" runat="server" class="btn btn-danger" Text="Add New Pre-Cutting"
                                    Width="150px" PostBackUrl="~/Accountsbootstrap/NewPrecutprocessBC.aspx" />
                            </div>
                            <div class="col-lg-1">
                                <asp:Label ID="lblPrint" runat="server">Print</asp:Label><br />
                                <asp:Button ID="btn" runat="server" Text="Print" Visible="true" CssClass="btn btn-danger"
                                    OnClientClick="Denomination()" Width="100px" />
                            </div>
                            <div class="col-lg-1">
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                            </div>
                            <div id="Div1" runat="server" visible="false" class="col-lg-16">
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
                            <div id="Div2" runat="server" visible="false" class="col-lg-17">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" ValidationGroup="val1"
                                    OnClick="Search_Click" Width="130px" />
                            </div>
                            <div id="Div3" runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"
                                    Width="130px" />
                            </div>
                            <div class="col-lg-15">
                                &nbsp;&nbsp;</div>
                            <div class="col-lg-17">
                                &nbsp;&nbsp;</div>
                            <div class="col-lg-17">
                                <%--<asp:Button ID="Btnnew" runat="server" class="btn btn-danger" Text="Add New Cutting" Width="130px"  OnClick="Add1_Click" />--%>
                            </div>
                            <div id="Div4" runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New Job-Work"
                                    Width="150px" OnClick="Add_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <div id="Div5" runat="server" style="height: 300px; overflow: scroll;">
                                        <asp:GridView ID="gvcust" EmptyDataText="No records Found" runat="server" CssClass="myGridStyle"
                                            OnPageIndexChanging="Page_Change" ShowFooter="true" AutoGenerateColumns="false"
                                            OnRowCommand="gvcust_RowCommand" OnRowDataBound="RatioShirtProcess_OnDataBound">
                                            <HeaderStyle BackColor="#3366FF" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField HeaderText="DCNo" DataField="LotNo" />
                                                <asp:BoundField HeaderText="DC Date" DataField="IssueDate" DataFormatString='{0:dd/MM/yyyy}' />
                                                <asp:BoundField DataField="LedgerName" HeaderText="Master Name" />
                                                <asp:BoundField HeaderText="ActualMeter" DataField="ActualMeter" DataFormatString="{0:n}"/>
                                                <asp:BoundField HeaderText="RemainMeter" DataField="Meter" DataFormatString="{0:n}" />
                                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("BCid") %>'
                                                            CommandName="edit">
                                                            <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                        <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                            Enabled="false" ToolTip="Not Allow To Edit" />
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("BCid") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("BCid") %>'
                                                            CommandName="Delete">
                                                            <asp:Image ID="Image1" ImageUrl="~/images/DeleteIcon_btn.png" runat="server" />
                                                        </asp:LinkButton>
                                                        <asp:ImageButton ID="imgdisabledel" ImageUrl="~/images/delete.png" runat="server" Visible="false"
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
                                                <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("BCid") %>'
                                                            CommandName="print">
                                                            <asp:Image ID="img1" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                        <asp:ImageButton ID="imgdisable1" ImageUrl="~/images/Print_Icon.jpg" runat="server"
                                                            Visible="false" Enabled="false" ToolTip="Not Allow To Edit" />
                                                        <asp:HiddenField ID="ldgID1" runat="server" Value='<%# Bind("BCid") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Delete" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("cutid") %>'
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
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Admin Print">
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
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Label Print">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint12" runat="server" CommandArgument='<%#Eval("cutid") %>'
                                                        CommandName="labelprint">
                                                        <asp:Image ID="print12" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </div>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" co runat="server">
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
