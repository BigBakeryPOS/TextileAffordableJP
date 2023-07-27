<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.SalesGrid" %>


<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
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
    <%--<script type = "text/javascript">
        var GridId = "<%=gvsales.ClientID %>";
        var ScrollHeight = 300;
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }
</script>--%>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Sales Grid </title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <style type="text/css">
        .fixedHeader
        {
            font-weight: bold;
            position: absolute;
            background-color: #006699;
            color: #ffffff;
            height: 25px;
            top: expression(Sys.UI.DomElement.getBounds(document.getElementById<br/>("Panel1")).y-25);
        }
    </style>
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
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
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
            alert('Are You Sure, You want to delete Sales!');
        }
    </script>
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1" method="post">
    <div class="col-lg-6">
       <%-- <h2 style="text-align: left; color: #fe0002">
            <asp:Label ID="Label1" Text="Password Protected :" runat="server"></asp:Label>
            <asp:TextBox AutoCompleteType="Disabled" ID="txtpas" Style="width: 20%; margin-left: 18pc;
                margin-top: -2pc;" runat="server" TextMode="Password" CssClass="form-control"
                Width="50%"></asp:TextBox>
        </h2>--%>
        <div class="col-lg-2">
            <h2 style="text-align: left; color: #fe0002">
                <asp:Label ID="lblTitle" Text="Sales" runat="server"></asp:Label></h2>
        </div>
        <div class="col-lg-4">
            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                Style="color: White" InitialValue="0" ControlToValidate="ddlbillno" ValueToCompare="0"
                Text="." Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtAutoName"
                ErrorMessage="Please enter your searching Data!" Text="." Style="color: White" />
            <asp:DropDownList Visible="false" ID="ddlbillno" CssClass="form-control" Style="width: 170px;
                margin-left: 110px; margin-top: -59px" runat="server">
                <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                <asp:ListItem Text="Bill No" Value="1"></asp:ListItem>
                <asp:ListItem Text="Bill Date" Value="2"></asp:ListItem>
                <asp:ListItem Text="Customer Name" Value="3"></asp:ListItem>
                <asp:ListItem Text="Trans No" Value="4"></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtAutoName" onkeyup="Search_Gridview(this, 'gvsales')" runat="server"
                CssClass="form-control" placeholder="Search Text" Style="width: 170px; margin-bottom: 50px;
                margin-left: 100px;" OnTextChanged="txtAutoName_TextChanged"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -/."
                TargetControlID="txtAutoName" />
            <asp:DropDownList ID="ddlcustomername" CssClass="form-control" Visible="false" runat="server"
                Style="width: 150px; padding-right: 150px">
            </asp:DropDownList>
            <asp:Button ID="btnsearch" Visible="false" runat="server" ValidationGroup="val1"
                class="btn btn-success" Text="Search" OnClick="Search_Click" Style="width: 120px;
                margin-top: -59px; margin-left: 275px;" />
            <asp:Button ID="btnrefresh" Visible="false" runat="server" class="btn btn-primary"
                Text="Reset" OnClick="refresh_Click" Style="width: 120px; margin-top: -59px;
                margin-left: 10px;" />
            <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" OnClick="Add_Click"
                Style="width: 120px; margin-top: -85px; margin-left: 610px;" />
            <asp:DropDownList ID="drpbilltypes" CssClass="form-control" OnSelectedIndexChanged="drpbilltype"
                Style="width: 120px; margin-top: -85px; margin-left: 387px;" AutoPostBack="true"
                runat="server">
                <asp:ListItem Value="0" Text="All" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Cash"></asp:ListItem>
                <asp:ListItem Value="2" Text="Credit"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12" style="margin-top: -5px">
            <div class="panel panel-default">
                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="overflow: auto; height: 500px">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:GridView ID="gvsales" runat="server" OnRowDataBound="gvsales_rowdatabound" AllowSorting="true"
                                                OnSorting="gridview_Sorting" EmptyDataText="No records Found" AllowPaging="false"
                                                Width="100%" OnPageIndexChanging="Page_Change" AutoGenerateColumns="false" CssClass="myGridStyle1"
                                                OnRowCommand="gvsales_RowCommand">
                                                <HeaderStyle BackColor="#3366FF" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                                <PagerSettings FirstPageText="1" Mode="Numeric" />
                                                <Columns>
                                                    <%--<asp:BoundField HeaderText="Customer ID" DataField="CustomerID" />--%>
                                                    <asp:BoundField SortExpression="BillNo" HeaderStyle-ForeColor="Black" HeaderText="BillNo"
                                                        DataField="FullBillno" />
                                                    <asp:BoundField SortExpression="BillDate" HeaderStyle-ForeColor="Black" HeaderText="Bill Date"
                                                        DataField="BillDate" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField SortExpression="LedgerName" HeaderStyle-ForeColor="Black" HeaderText="Customer Name"
                                                        DataField="LedgerName1" />
                                                    <%--<asp:BoundField  HeaderStyle-ForeColor="Black"   HeaderText="RTO Zone"  DataField="RToZone" />--%>
                                                    <asp:BoundField SortExpression="Paymentmode" HeaderStyle-ForeColor="Black" HeaderText="Paymode"
                                                        DataField="Paymentmode" />
                                                    <asp:BoundField SortExpression="Mobileno" Visible="false" HeaderStyle-ForeColor="Black"
                                                        HeaderText="Mobile No" DataField="Mobileno" />
                                                    <asp:BoundField SortExpression="Transport" Visible="false" HeaderStyle-ForeColor="Black"
                                                        HeaderText="Transport" DataField="Transport" />
                                                    <%--<asp:BoundField HeaderText="Area" DataField="Area" />
    <asp:BoundField HeaderText="City" DataField="City" />--%>
                                                    <asp:BoundField SortExpression="NetAmount" HeaderStyle-ForeColor="Black" HeaderText="TotalAmount"
                                                        DataField="roundoff" DataFormatString="{0:f}" />
                                                    <asp:TemplateField ItemStyle-Width="3%" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("SalesID")  %>'
                                                                Text="View" CommandName="editt"></asp:LinkButton>
                                                            <%--      <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="1%" HeaderText="DC" ItemStyle-HorizontalAlign="Center"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDC" runat="server" CommandArgument='<%#Eval("SalesID") %>'
                                                                Text="DC" CommandName="DC"></asp:LinkButton>
                                                            <%--      <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PACKING" Visible="true" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="pack" Width="50%" runat="server" Text='<%#Eval("Packing")%>' CommandArgument='<%#Eval("SalesID")%>'
                                                                CommandName="pack"></asp:TextBox>
                                                            <asp:Button ID="btnpack" runat="server" CommandArgument='<%#Eval("SalesID")%>' Text="P"
                                                                CommandName="btnp"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CHECKED" Visible="true" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="checkk" Width="50%" runat="server" Text='<%#Eval("CheckSta")%>'
                                                                CommandArgument='<%#Eval("SalesID")%>' CommandName="ch"></asp:TextBox>
                                                            <asp:Button ID="btncheckk" runat="server" CommandArgument='<%#Eval("SalesID")%>'
                                                                Text="C" CommandName="btnc"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RE-CHGECK" Visible="true" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="recheck" Width="50%" runat="server" Text='<%#Eval("Recheck")%>'
                                                                CommandArgument='<%#Eval("SalesID")%>' CommandName="rch"></asp:TextBox>
                                                            <asp:Button ID="btnrecheck" runat="server" CommandArgument='<%#Eval("SalesID")%>'
                                                                Text="R-C" CommandName="btnr"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("SalesID") %>'
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
                                                    <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("SalesID") %>'
                                                                CommandName="print">
                                                                <asp:Image ID="print" runat="server" ImageAlign="Middle" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transport Copy" ItemStyle-HorizontalAlign="Center"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnprinttransport" runat="server" CommandArgument='<%#Eval("SalesID") %>'
                                                                CommandName="printtrans">
                                                                <asp:Image ID="pprint" runat="server" ImageAlign="Middle" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField SortExpression="SalesID" HeaderStyle-ForeColor="Black" HeaderText="SalesID"
                                                        DataField="SalesID" Visible="false" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField SortExpression="TransNo" HeaderStyle-ForeColor="Black" HeaderText="Trans No"
                                                        DataField="TransNo" Visible="false" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField SortExpression="SalesID" HeaderStyle-ForeColor="Black" DataField="SalesID"
                                                        Visible="false" />
                                                    <asp:BoundField SortExpression="Bill_To" Visible="false" HeaderStyle-ForeColor="Black"
                                                        DataField="Bill_To" />
                                                </Columns>
                                                <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" />
                                                <%--  <HeaderStyle CssClass="header" Font-Bold="True" Height="20px" />--%>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
                    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
                    <script type="text/javascript">                        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
                </div>
                <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
                    runat="server">
                    <div class="popup_Container">
                        <div class="popup_Titlebar" id="PopupHeader">
                            <div class="TitlebarLeft">
                                Sales:</div>
                            <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                            </div>
                        </div>
                        <div class="popup_Body">
                            <p>
                                Are you sure ,you want to delete Sales?
                            </p>
                        </div>
                        <div class="popup_Buttons" align="center">
                            <input id="ButtonDeleleOkay" type="button" value="Yes" />
                            <input id="ButtonDeleteCancel" type="button" value="No" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
