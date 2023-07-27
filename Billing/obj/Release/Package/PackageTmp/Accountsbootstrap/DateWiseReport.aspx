<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DateWiseReport.aspx.cs" Inherits="Billing.Accountsbootstrap.DateWiseReport" %>

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
    <title>Date Wise Process Report</title>
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
                        Date Wise Process Report</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            Start Date</label>
                                    <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control"></asp:TextBox>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtstartdate"
                                        PopupButtonID="txtstartdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>
                                 <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            End Date</label>
                                     <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control"></asp:TextBox>
                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtenddate"
                                        PopupButtonID="txtenddate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                        
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                     <asp:Button ID="btnsearch" runat="server" Text="Generate Process" OnClick="btnsearch_click" />
                                        
                                    </div>
                                </div>
                                
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td>
                                                    <div id="Div1" runat="server"  >
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                        <label id="Label1" runat="server" style="font-weight:bold">Lot Process</label>
                                                        <asp:GridView ID="gridshirt" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                            ShowFooter="false" DataKeyNames="empid" OnRowDataBound="gvCustsales_RowDataBound" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                            ShowHeaderWhenEmpty="True">
                                                             <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="Employee Name"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews('dv<%# Eval("name") %>', 'imdiv<%# Eval("name") %>');"
                                                                        style="text-decoration: none;">
                                                                        <img id="imdiv<%# Eval("name") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                                    </a>
                                                                    <%# Eval("name")%>
                                                                    <div id="dv<%# Eval("name") %>" style="display: none; position: relative;">
                                                                        <asp:GridView runat="server" ID="gvLiaLedger"  CssClass="myGridStyle"
                                                                         Width="82%" GridLines="Both"
                                                                            AutoGenerateColumns="false" ShowFooter="true" >
                                                                            <Columns>
                                                                             <asp:BoundField HeaderText="Lot No" DataField="lotno" />
                                                                                <asp:BoundField HeaderText="Employee Name" DataField="name" />
                                                                                <asp:BoundField HeaderText="Process Name" DataField="processtype"/>
                                                                                <asp:BoundField HeaderText="Qty" DataField="qty" DataFormatString='{0:f}' />
                                                                                <asp:BoundField HeaderText="Rate"  DataField="rate" DataFormatString='{0:f}' />
                                                                                <asp:BoundField HeaderText="Total Rate" DataField="ratee" DataFormatString='{0:f}' />
                                                                                  <asp:BoundField HeaderText="date" DataField="date" DataFormatString='{0:d}' />
                                                                                  
                                                                            </Columns>
                                                                             <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         
                                                            <asp:BoundField HeaderText="Total Qty" DataField="qty" DataFormatString='{0:f}'  />
                                                            <asp:BoundField HeaderText="Total Amount" DataField="rate" DataFormatString='{0:f}' />
                                                          
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
                                           
                                              <tr>
                                                <td>
                                                    <div visible="false"  id="Div3" runat="server">
                                                        <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                                                        <label>Details for Lot No :</label><asp:Label runat="server" ID="lblLot"></asp:Label>
                                                        <asp:GridView ID="Gridoverall" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                            ShowFooter="false" DataKeyNames="empid" OnRowDataBound="Grodoverall_bound" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                            ShowHeaderWhenEmpty="True">
                                                          <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="Employee Name"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews('dv<%# Eval("name") %>', 'imdiv<%# Eval("name") %>');"
                                                                        style="text-decoration: none;">
                                                                        <img id="imdiv<%# Eval("name") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                                    </a>
                                                                    <%# Eval("name")%>
                                                                    <div id="dv<%# Eval("name") %>" style="display: none; position: relative;">
                                                                        <asp:GridView runat="server" ID="gvLiaLedger1"  CssClass="myGridStyle"
                                                                         Width="82%" GridLines="Both"
                                                                            AutoGenerateColumns="false" ShowFooter="true" >
                                                                            <Columns>
                                                                             <asp:BoundField HeaderText="Lot No" DataField="lotno" />
                                                                                <asp:BoundField HeaderText="Employee Name" DataField="name" />
                                                                                <asp:BoundField HeaderText="Process Name" DataField="processtype"/>
                                                                                <asp:BoundField HeaderText="Qty" DataField="qty" DataFormatString='{0:f}' />
                                                                                <asp:BoundField HeaderText="Rate"  DataField="rate" DataFormatString='{0:f}' />
                                                                                <asp:BoundField HeaderText="Total Rate" DataField="ratee" DataFormatString='{0:f}' />
                                                                                  <asp:BoundField HeaderText="date" DataField="date" DataFormatString='{0:d}' />
                                                                                  
                                                                            </Columns>
                                                                             <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                         
                                                            <asp:BoundField HeaderText="Total Qty" DataField="qty" DataFormatString='{0:f}'  />
                                                            <asp:BoundField HeaderText="Total Amount" DataField="rate" DataFormatString='{0:f}' />
                                                          
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

                                             <tr id="Tr1" runat="server" style="height:50px"></tr>
                                            

                                             

                                           
                                        </table>
                                    </div>
                                    <div style="color: Green; font-weight: bold" class="form-group">
                                        <label id="lblNoRecords" style="color: Red" runat="server">
                                        </label>
                                        <br />
                                    </div>
                                    <div>
                                        <asp:Button ID="btnExport" Visible="false" Text="Export to Excel" runat="server"
                                            CssClass="btn btn-success" Height="37px" OnClick="btnExport_Click" /></div>
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
            <asp:AsyncPostBackTrigger ControlID="gridshirt" EventName="RowDataBound" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
