<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequirementSheetDetailsNew.aspx.cs"
    Inherits="Billing.Accountsbootstrap.RequirementSheetDetailsNew" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>RequirementSheet Details</title>
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
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
        function SearchEmployees(txtSearch, cblEmployees) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html((count) + ' match');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
        }
    </script>
    <script type="text/javascript">
        function ReportPrint() {

            var gridData = document.getElementById('Excel');

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
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false">
    </asp:Label>
    <asp:Label runat="server" ID="lblProcessforMasterId" Text="5" ForeColor="White" CssClass="label"
        Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1"></asp:Label>
    <form runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 20px;
                                    font-weight: bold">
                                    RequirementSheet Details</h1>
                                <div class="form-group">
                                    <asp:CheckBoxList ID="chkItemHead" CssClass="chkChoice1" runat="server">
                                    </asp:CheckBoxList>
                                    <asp:CheckBoxList ID="chkSwatch" CssClass="chkChoice1" runat="server">
                                        <asp:ListItem>Swatch Sheet</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="col-lg-2" id="BuyerCode" runat="server" visible="true">
                                <div class="form-group">
                                    <label>
                                        Buyer Code:</label>
                                    <asp:DropDownList ID="ddlBuyerCode" OnSelectedIndexChanged="buyer_code" AutoPostBack="true"
                                        runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Type :</label>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="With Color" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="WithOut Color" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="Div1" class="col-lg-2" runat="server" visible="false">
                                <div class="form-group">
                                    <label>
                                        Buyer Name:</label>
                                    <asp:DropDownList ID="ddlBuyerName" runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group" id="ExcNo" runat="server" visible="true">
                                    <asp:TextBox ID="txtfabcontrast" runat="server" Width="100%" placeholder="Find ExcNo"
                                        onkeyup="SearchEmployees(this,'#chkExcNo');">
                                    </asp:TextBox>
                                    <div style="overflow-y: scroll; height: 100px">
                                        <div class="panel panel-default" style="width: 350px">
                                            <asp:CheckBoxList ID="chkExcNo" CssClass="chkChoice1" runat="server" RepeatLayout="Table"
                                                Style="overflow: auto">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" id="AccountingYear" runat="server" visible="false">
                                    <label>
                                        Accounting Year:</label>
                                    <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" Width="110px" MaxLength="4">
                                    </asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                        FilterType="Numbers" TargetControlID="txtYear" />
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group" id="Date" runat="server" visible="true">
                                    <asp:Label ID="lblDate" runat="server" Text="Req. Date" Width="110px" Style="font-weight: bold">
                                    </asp:Label>
                                    <br />
                                    <asp:CheckBox ID="chkUseDate" runat="server" />
                                </div>
                            </div>
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
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search"
                                    OnClick="btnSearch_OnClick" Width="125px" />
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-primary" Text="Excel"
                                    OnClick="btnExcel_OnClick" Width="125px" />
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btn" runat="server" Text="Print" CssClass="btn btn-info" OnClientClick="ReportPrint()"
                                    Width="125px" />
                            </div>
                        </div>
                        <div id="Excel" runat="server">
                            <div class="col-lg-12" id="dvother" runat="server" visible="false">
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-8">
                                    <%-- <div id="Excel" runat="server">--%>
                                    <asp:GridView ID="gvRequirementSheetDetails" runat="server" CssClass="myGridStyle1"
                                        Width="100%" AutoGenerateColumns="False" ShowHeader="false" GridLines="none">
                                        <Columns>
                                            <asp:BoundField HeaderText="Column1" DataField="Column1" />
                                            <asp:BoundField HeaderText="Column2" DataField="Column2" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField HeaderText="Column3" DataField="Column3" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Column4" DataField="Column4" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Column5" DataField="Column5" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                    <%-- </div>--%>
                                </div>
                                <div class="col-lg-2">
                                </div>
                            </div>
                            <div class="col-lg-12" id="dvswatch" runat="server" visible="false">
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-8">
                                    <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
                                    <br />
                                    <br />
                                    <asp:GridView ID="gvSwatchSheet1" runat="server" EmptyDataText="No Records Found"
                                        Caption="Swatch Sheet" ShowHeader="false" AutoGenerateColumns="False" Width="100%"
                                        GridLines="None">
                                        <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="Column1" HeaderText="" />
                                            <asp:BoundField DataField="Column2" HeaderText="" />
                                            <asp:BoundField DataField="Column3" HeaderText="" />
                                            <asp:BoundField DataField="Column4" HeaderText="" />
                                            <asp:BoundField DataField="Column5" HeaderText="" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <asp:GridView ID="gvSwatchSheet2" runat="server" EmptyDataText="No Records Found"
                                        Width="50%" AutoGenerateColumns="False" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Color/Print Swatch">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDescription" Height="150px" Width="150px" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:Label runat="server" ID="lblDescription" Text='<%#Eval("Description")%>' Width="100%"></asp:Label>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Color" HeaderText="Color" ItemStyle-VerticalAlign="Middle" />
                                            <asp:BoundField DataField="ExcNo" HeaderText="ExcNo" ItemStyle-VerticalAlign="Middle" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                </div>
                                <div class="col-lg-2">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
        <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript">            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
