<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessDetailedReport.aspx.cs"
    Inherits="Billing.Accountsbootstrap.ProcessDetailedReport" %>

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
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <title>Process Detailed Report</title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Styles/chosen.css" />
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <form id="form1" method="post" style="margin-top: 0px" runat="server">
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">
                Process Detailed Report</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-1">
                <label>
                    From Date</label>
                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate"
                    PopupButtonID="txtfromdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                    CssClass="cal_Theme1">
                </ajaxToolkit:CalendarExtender>
            </div>
            <div class="col-lg-1">
                <label>
                    To Date</label>
                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate"
                    PopupButtonID="txttodate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                    CssClass="cal_Theme1">
                </ajaxToolkit:CalendarExtender>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        JobWorker</label>
                    <br />
                    <asp:DropDownList ID="ddljobwork" Width="195px" Height="60px" runat="server" CssClass="chzn-select">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-2">
                <label>
                    LotNo</label>
                <div class="form-group">
                    <asp:DropDownList CssClass="chzn-select" ID="ddllotno" Width="150px" Height="150px"
                        runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-2">
                <label>
                    Process</label>
                <div class="form-group">
                    <asp:DropDownList CssClass="chzn-select" ID="ddlprocesstype" Width="150px" Height="150px"
                        runat="server">
                        <asp:ListItem Text="Select Process" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Stitching" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Embroiding" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Printing" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Ironing/Packing" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-lg-1">
                <asp:Button ID="btnsearch" runat="server" Text="Generate" CssClass="btn btn-success"
                    OnClick="Serachclick" />
            </div>
            <div class="col-lg-1">
                <asp:Button ID="btnreset" runat="server" Text="Excel" CssClass="btn btn-danger" Width="100px"
                    OnClick="btnexp_Click" />
            </div>
            <div class="col-lg-1">
                <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btn btn-group" Width="100px"
                    OnClientClick="Denomination()" Visible="false" />
            </div>
            <div class="col-lg-2">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <div id="div2" runat="server">
                    <table class="table table-bordered table-striped">
                        <tr>
                            <td>
                                <div id="Div1" runat="server" style="overflow: auto;">
                                    <asp:GridView ID="gvCustsales" runat="server" CssClass="myGridStyle" DataKeyNames="lotDataKeyNames"
                                        ShowFooter="true" AutoGenerateColumns="false" OnRowDataBound="gvCustsales_RowDataBound"
                                        EmptyDataText="No data found!" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:BoundField HeaderText="LotNo" DataField="CompanyLotNo" />
                                            <asp:BoundField HeaderText="Itemcode" DataField="itemcode" />
                                            <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString='{0:dd/MM/yyyy}' />
                                            <asp:BoundField HeaderText="Master" DataField="LedgerName" />
                                             <asp:BoundField HeaderText="Qty" DataField="Qty" />

                                              <asp:BoundField HeaderText="Work Order" DataField="WorkOrder" />

                                             

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" 
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <a href="javascript:switchViews('dv<%# Eval("WorkOrder") %>', 'imdiv<%# Eval("WorkOrder") %>');"
                                                        style="text-decoration: none;">
                                                        <img id="imdiv<%# Eval("WorkOrder") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                    </a>
                                                    <%# Eval("WorkOrder")%>
                                                    <div id="dv<%# Eval("WorkOrder") %>" style="display: none; position: relative;">
                                                        <asp:GridView runat="server" ID="gvLiaLedger" CssClass="myGridStyle" Width="82%"
                                                            GridLines="Both" AutoGenerateColumns="false" ShowFooter="false">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Date" DataField="SendDate" DataFormatString='{0:dd/MM/yyyy}' />
                                                                <asp:BoundField HeaderText="Type" DataField="Type" />
                                                                <asp:BoundField HeaderText="Qty" DataField="Qty" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
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
        </div>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    </form>
</body>
</html>
