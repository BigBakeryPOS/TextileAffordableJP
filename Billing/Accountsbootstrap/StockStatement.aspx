<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockStatement.aspx.cs"
    Inherits="Billing.Accountsbootstrap.StockStatement" %>

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
    <title>Stock Statement </title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../Styles/chosen.css" rel="Stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <link rel="stylesheet" href="../css/chosen.css" />
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
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblMaximumRows" ForeColor="White" CssClass="label"
        Visible="false" Text="5"> </asp:Label>
    <asp:Label runat="server" ID="lblInitialDate" ForeColor="White" CssClass="label"
        Visible="false" Text="01/04/2020"> </asp:Label>
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
                                    Stock Statement
                                </h1>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label>
                                        Company :
                                    </label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        ItemHead :
                                    </label>
                                    <asp:DropDownList ID="ddlItemHead" runat="server" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlItemHead_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        ItemGroup :
                                    </label>
                                    <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlItemGroup_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>
                                        From Date:</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender13" TargetControlID="txtFromDate"
                                        EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>
                                        To Date:</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender14" TargetControlID="txtToDate"
                                        EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>
                                        Item :
                                    </label>
                                    <asp:DropDownList ID="ddlItem" runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Color :
                                    </label>
                                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="chzn-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search"
                                    OnClick="btnSearch_OnClick" Width="110px" />
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-primary" Text="Excel"
                                    OnClick="btnExcel_OnClick" Width="110px" />
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btn" runat="server" Text="Print" CssClass="btn btn-info" OnClientClick="ReportPrint()"
                                    Width="110px" />
                            </div>
                        </div>
                        <div id="Excel" runat="server">
                            <div class="col-lg-12">
                                <div class="col-lg-1">
                                </div>
                                <div class="col-lg-10">
                                    <asp:GridView ID="gvMaterialStockEntry" runat="server" CssClass="myGridStyle1" EmptyDataText="No Records Found"
                                        Width="100%" AutoGenerateColumns="false" Caption="Material Stock Report" OnRowCommand="gridviewhrm_RowCommand" OnRowDataBound="gvMaterialStockEntry_OnRowDataBound">
                                        <HeaderStyle BackColor="White" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="ItemCode " DataField="ItemCode" />
                                            <asp:BoundField HeaderText="ItemDescription" DataField="ItemDescription" />
                                            <asp:BoundField HeaderText="Color" DataField="Color" />
                                            <asp:BoundField HeaderText="Opening" DataField="OP" DataFormatString="{0:f2}" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Closing" DataField="CL" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="" DataField="Units" />
                                            <asp:BoundField HeaderText="Last Rate" DataField="Rate" DataFormatString="{0:f2}"
                                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Value" DataField="Value" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right"
                                                FooterStyle-HorizontalAlign="Right" />
                                            <asp:TemplateField HeaderText="Check">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("ItemIdandColorId") %>' CommandName="ItemIdandColorId"
                                                        runat="server">Check
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                                <div class="col-lg-1">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
