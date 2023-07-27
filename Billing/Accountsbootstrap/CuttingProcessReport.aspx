<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuttingProcessReport.aspx.cs" Inherits="Billing.Accountsbootstrap.CuttingProcessReport" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <title>Customer Sales Report</title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
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
    <form runat="server" id="form1" method="post" style="margin-top: 0px">
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:UpdatePanel ID="Updatepnel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">
                       Cutting Process Report</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                  
                                    <asp:Button ID="btnall" Visible="false" runat="server" Text="Generate Report" CssClass="btn btn-success"
                                        OnClick="btnall_Click" />
                                    &nbsp
                                    <asp:Button ID="btnViewAll" Visible="false" runat="server" Text="View All"  CssClass="btn btn-success"
                                        OnClick="btnViewAll_Click" />
                                </div>
                                <div class="col-lg-12">
                                  
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td>
                                                <div runat="server" style="overflow:auto; height:550px" >
                                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    <asp:GridView ID="gvCustsales" runat="server" AllowPaging="false" PageSize="100" CssClass="myGridStyle"
                                                        DataKeyNames="Processid" ShowFooter="true" OnPageIndexChanging="Page_Change" OnRowDataBound="gvCustsales_RowDataBound"
                                                        AutoGenerateColumns="false" EmptyDataText="No data found!" ShowHeaderWhenEmpty="True">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="D.No"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews('dv<%# Eval("Dno") %>', 'imdiv<%# Eval("Dno") %>');"
                                                                        style="text-decoration: none;">
                                                                        <img id="imdiv<%# Eval("Dno") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                                    </a>
                                                                    <%# Eval("Dno")%>
                                                                    <div id="dv<%# Eval("Dno") %>" style="display: none; position: relative;">
                                                                        <asp:GridView runat="server" ID="gvLiaLedger" CssClass="myGridStyle" Width="82%" GridLines="Both"
                                                                            AutoGenerateColumns="false" DataKeyNames="Dno" ShowFooter="true">
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Transid" Visible="false" DataField="Dno" />
                                                                                
                                                                                <asp:BoundField HeaderText="Lot.No" DataField="LotNo" />
                                                                                <asp:BoundField HeaderText="PartyName" DataField="name" />
                                                                                <asp:BoundField HeaderText="Avaliable Meter" Visible="false"  DataField="meter" />
                                                                                <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' />
                                                                                 <asp:BoundField HeaderText="Color" DataField="Color" />
                                                                                  <asp:BoundField HeaderText="Width" DataField="Widthid" />
                                                                                   <asp:BoundField HeaderText="Fit" DataField="Fit" />
                                                                                <asp:BoundField HeaderText="Used Meter" DataField="Req_Meter" DataFormatString='{0:f}' />
                                                                            </Columns>
                                                                             <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                             <asp:BoundField HeaderText="Purchased Meter" DataField="Meter" />
                                                             <asp:BoundField HeaderText="Avaliable Meter" DataField="AvaliableMeter" DataFormatString='{0:f}' />
                                                            
                                                            <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' />
                                                            <asp:BoundField HeaderText="RM Code" DataField="RMCode" />
                                                            <asp:BoundField HeaderText="Checked Sign" DataField="name" />
                                                            <asp:BoundField HeaderText="Challen No" DataField="ChallenNo" />
                                                             <asp:BoundField HeaderText="Width" DataField="Widthname" />
                                                             <asp:ImageField DataImageUrlField = "ClothImage" ControlStyle-Height = "50" ControlStyle-Width = "50" HeaderText="Image Preview" />
                                                           
                                                          
                                                           
                                                        </Columns>
                                                        <HeaderStyle BackColor="#990000" />
                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                            NextPageText="Next" PreviousPageText="Previous" />
                                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="color: Green; font-weight: bold" class="form-group">
                                        <label id="lblNoRecords" style="color: Red" runat="server">
                                        </label>
                                        <br />
                                        <i>You are viewing page
                                            <%=gvCustsales.PageIndex + 1%>
                                            of
                                            <%=gvCustsales.PageCount%>
                                        </i>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnExport" Visible="false" Text="Export to Excel" runat="server" CssClass="btn btn-success"
                                            Height="37px" OnClick="btnExport_Click" /></div>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvCustsales" EventName="RowDataBound" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>

