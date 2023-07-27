<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentReport.aspx.cs"
    Inherits="Billing.Accountsbootstrap.PaymentReport" %>

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
    <title>PaymentReport</title>
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
        <div class="col-lg-12">
            <div class="col-lg-1">
                <h2 style="text-align: left; color: #fe0002;">
                    Payment
                </h2>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server">Branch</asp:Label><br />
                    <asp:DropDownList ID="drpbranch" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-1">
                <asp:Label ID="Label1" runat="server">Type</asp:Label><br />
                <asp:DropDownList CssClass="form-control" ID="ddltype" Width="120px" runat="server">
                    <asp:ListItem Text="Summary" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Detailed" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-1">
                <div class="form-group">
                    <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control center-block"
                        Width="110px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtFromDate"
                        PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                        CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-lg-1">
                <div class="form-group">
                    <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control center-block" Width="110px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtToDate"
                        PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                        CssClass="cal_Theme1">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="col-lg-2">
                <asp:Label ID="Label6" runat="server">Job Worker</asp:Label><br />
                <asp:DropDownList CssClass="chzn-select" ID="ddljobworker" Width="220px" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server">Process Type</asp:Label>
                    <asp:DropDownList ID="DpProcess" Enabled="true" CssClass="chzn-select form-control"
                        runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-1">
                <asp:Label ID="Label4" runat="server"></asp:Label><br />
                <asp:Button ID="btnsearch" runat="server" Visible="true" class="btn btn-success"
                    Text="Search" ValidationGroup="val1" Style="width: 100px;" OnClick="btnsearch_Click" />
            </div>
            <div class="col-lg-1">
                <asp:Label ID="Label3" runat="server"></asp:Label><br />
                <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Excel" Style="width: 100px;"
                    OnClick="btnexp_Click" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12" style="margin-top: -10px">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="table-responsive">
                        <div id="div2" runat="server">
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <td>
                                        <div style="overflow: auto;">
                                            <asp:GridView ID="PaymentGrid" CssClass="myGridStyle" ShowHeaderWhenEmpty="true"
                                                runat="server" ShowFooter="true" OnRowDataBound="GVDespatchstock_RowDataBound"
                                                Width="70%" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="PaymentNo" HeaderStyle-ForeColor="Black" DataField="PaymentNo" />
                                                    <asp:BoundField HeaderText="PaymentDate" DataField="PaymentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField HeaderText="JobWorker" DataField="LedgerName" />
                                                    <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                                    <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:f}" />
                                                    <asp:BoundField HeaderText="Narration" DataField="Narration" />
                                                </Columns>
                                                <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="div3" runat="server">
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <td>
                                        <div style="overflow: auto;">
                                            <asp:GridView ID="PaymentGrid2" CssClass="myGridStyle" ShowHeaderWhenEmpty="true"
                                                runat="server" ShowFooter="true" OnRowDataBound="GVDespatchstock_RowDataBound1"
                                                Width="100%" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="PaymentNo" DataField="PaymentNo" />
                                                    <asp:BoundField HeaderText="PaymentDate" DataField="PaymentDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField HeaderText="JobWorker" DataField="LedgerName" />
                                                    <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                                    <asp:BoundField HeaderText="Rate" DataField="PieceRate" DataFormatString="{0:f}" />
                                                    <asp:BoundField HeaderText="Amount" DataField="Balance" DataFormatString="{0:f}" />
                                                    <asp:BoundField HeaderText="Advance" DataField="Advance" DataFormatString="{0:f}" />
                                                    <asp:BoundField HeaderText="DebitAmount" DataField="DebitAmount" DataFormatString="{0:f}" />
                                                    <asp:BoundField HeaderText="Paymode" DataField="Paymode" />
                                                    <asp:BoundField HeaderText="BankName" DataField="BankName" />
                                                    <asp:BoundField HeaderText="ChequeNo" DataField="Chequeno" />
                                                    <asp:BoundField HeaderText="LotNo" DataField="CompanyLotNo" />
                                                    <asp:BoundField HeaderText="WorkOrderNo" DataField="WorkOrderNo" />
                                                    <asp:BoundField HeaderText="Narration" DataField="Narration" />
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
    </div> </asp:Panel>
    </form>
</body>
</html>
