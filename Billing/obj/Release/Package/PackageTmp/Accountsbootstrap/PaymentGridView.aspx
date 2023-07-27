<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentGridView.aspx.cs"
    Inherits="Billing.Accountsbootstrap.PaymentGridView" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
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
        
        .HeaderFreez
        {
            position: relative;
            top: expression(this.offsetParent.scrollTop);
            z-index: 10;
        }
    </style>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Payment Grid Master - bootsrap</title>
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
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        function alertMessage() {
            alert('Are You Sure, You want to delete This Customer!');
        }
    </script>
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <script type="text/javascript">


        //$(document).ready(function () {
        //            //
        //            // Client Side Search (Autocomplete)
        //            // Get the search Key from the TextBox
        //            // Iterate through the 1st Column.
        //            // td:nth-child(1) - Filters only the 1st Column
        //            // If there is a match show the row [$(this).parent() gives the Row]
        //            // Else hide the row [$(this).parent() gives the Row]
        //          { $('#filter').keyup(function (event) {
        //                var searchKey = $(this).val();

        //                $("#PaymentGrid tr td:nth-child(4)").each(function () {
        //                    var cellText = $(this).text();
        //                    if (cellText.indexOf(searchKey) >= 0) {
        //                        $(this).parent().show();
        //                    }
        //                    else {
        //                        $(this).parent().hide();}

        //                });

        //            });

        //        });



        //    $(document).ready(function () {
        //
        // Client Side Search (Autocomplete)
        // Get the search Key from the TextBox
        // Iterate through the 1st Column.
        // td:nth-child(1) - Filters only the 1st Column
        // If there is a match show the row [$(this).parent() gives the Row]
        // Else hide the row [$(this).parent() gives the Row]

        //        $('#TextBox1').keyup(function (event) {
        //            var searchKey = $(this).val().toLowerCase();
        //            $("#PaymentGrid tr:nth-child(n) td").each(function () {
        //                var cellText = $(this).text().toLowerCase();
        //                if (cellText.indexOf(searchKey) >= 0) {
        //                    $(this).parent().show();
        //                }
        //                else {
        //                    $(this).parent().hide();
        //                }
        //            });
        //        });
        //    });


        //    Gridvie all row search:-------

        //    $(document).ready(function () {
        //        $("#txtsearch1").keyup(function () {
        //            _this = this;

        //            $.each($("#PaymentGrid tbody").find("tr"), function () {

        //                if ($(this).text().toLowerCase().indexOf($(_this).val().toLowerCase()) == -1)
        //                    $(this).hide();
        //                else
        //                    $(this).show();
        //            });
        //        });
        //    });


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
    <style type="text/css">
        .GVFixedHeader
        {
            font-weight: bold;
            background-color: Green;
            position: relative;
            top: expression(this.parentNode.parentNode.parentNode.scrollTop-1);
        }
    </style>
    <%--<link href="../css/Header.css" rel="stylesheet" type="text/css" />--%>
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
        <div class="col-lg-12" style="margin-top: 0px">
            <div class="col-lg-2">
                <h2 style="text-align: left; color: #fe0002;">
                    Payment
                </h2>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server"></asp:Label><br />
                    <asp:DropDownList ID="drpbranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpbranch_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-2">
                <asp:Label ID="Label1" runat="server"></asp:Label><br />
                <asp:TextBox CssClass="form-control" Enabled="true" ID="txtsearch1" runat="server"
                    onkeyup="Search_Gridview(this, 'PaymentGrid')" placeholder="Search Text"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="col-lg-2">
                <div class="form-group">
                    <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control center-block"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtFromDate"
                        PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                        CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control center-block"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtToDate"
                        PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                        CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-lg-2">
                <asp:Label ID="Label6" runat="server">Worker</asp:Label><br />
                <asp:DropDownList CssClass="chzn-select" ID="ddljobworker" Width="180px" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server">Process</asp:Label>
                    <asp:DropDownList ID="DpProcess" Enabled="true" CssClass="chzn-select form-control"
                        runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-1">
                <asp:Label ID="Label4" runat="server">Search</asp:Label><br />
                <asp:Button ID="btnsearch" runat="server" Visible="true" class="btn btn-success"
                    Text="Search" ValidationGroup="val1" Style="width: 100px;" OnClick="btnsearch_Click" />
            </div>
            <div class="col-lg-1">
                <asp:Label ID="Label3" runat="server">Add New</asp:Label><br />
                <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" Style="width: 100px;"
                    OnClick="btnadd_Click" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12" style="margin-top: -10px">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <div style="overflow: auto; height: 300px">
                                        <asp:GridView ID="PaymentGrid" CssClass="myGridStyle" ShowHeaderWhenEmpty="true"
                                            EmptyDataText="No Records Found" AllowSorting="true" runat="server" PageSize="10"
                                            ShowFooter="true" OnRowDataBound="GVDespatchstock_RowDataBound" Width="70%" AllowPaging="false"
                                            AutoGenerateColumns="false" OnRowCommand="PaymentGrid_RowCommand">
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                            <HeaderStyle BackColor="#3366FF" CssClass="HeaderFreez" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="Next" PreviousPageText="Previous" />
                                            <Columns>
                                                <asp:BoundField HeaderText="PaymentNo" HeaderStyle-ForeColor="Black" DataField="PaymentNo" />
                                                <asp:BoundField HeaderText="PaymentDate" DataField="PaymentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField HeaderText="JobWorker" DataField="LedgerName" />
                                                <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                                <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:f}" />
                                                <asp:BoundField HeaderText="Narration" DataField="Narration" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Print">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("PaymentID") %>'
                                                            CommandName="Print">
                                                            <asp:Image ID="print" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("PaymentID") %>'
                                                            CommandName="edit">
                                                            <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("PaymentID") %>'
                                                            CommandName="delete" OnClientClick="alertMessage()">
                                                            <asp:Image ID="dlt" runat="server" ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
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
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
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
    </div>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Payment:</div>
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
