<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.OrderGrid" %>

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
    <title>Fabric Purchase Order</title>
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
                     Fabric Purchase Order</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                             <div class="col-lg-12">
                                <div class="col-lg-6">
                                <div class="col-lg-1">
                                <label >From date</label>
                                </div>
                                   <div class="col-lg-2">
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate"
                                        PopupButtonID="txtfromdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                    </div> 
                                    <div class="col-lg-3">
                                 <label>To date</label>
                                 </div>
                                  <div class="col-lg-4">
                                  <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" style="width:100px;margin-left: -100px;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate"
                                        PopupButtonID="txttodate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                     </div> 
                                    
                                     <div class="col-lg-5">
                                    <asp:Button ID="btnserch" runat="server" OnClick="Serachclick" Text="Process" style="width: 100px;margin-top: -30px;margin-left: 280px;" /> &nbsp
                                    </div> 
                                     <div class="col-lg-6">
                                     <asp:Button ID="btnfresh" runat="server" OnClick="refeshClcik"  Text="Refresh" style=" width: 100px;margin-top: -30px;margin-left: 120px;"  />
                                       </div> 
                                       <div>
                                    <asp:Button ID="btnall" Visible="false" runat="server" Text="Generate Report" CssClass="btn btn-success"
                                        OnClick="btnall_Click" />
                                  
                                    <asp:Button ID="btnViewAll" Visible="false" runat="server" Text="View All"  CssClass="btn btn-success"
                                        OnClick="btnViewAll_Click" />
                                      </div>
                                </div>

                                <div class="col-lg-6">
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="." Style="color: White" />
                                <asp:TextBox CssClass="form-control" ID="txtsearch" runat="server"
                                    Style="margin-top: -20px" placeholder="Search Text" Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,custom" ValidChars=" /-"
                                    TargetControlID="txtsearch" />
                                    <asp:DropDownList ID="ddlsupplier" runat="server" CssClass="form-control" AutoPostBack="true"
                                     OnSelectedIndexChanged="ddlsupplier_SelectedIndexChanged" style="width: 200px;margin-left: 250px;margin-top: -33px;"></asp:DropDownList>
                                       <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" style="width:130px;margin-top: -35px;margin-left: 500px;"
                                    OnClick="Add_Click" />
                            </div>
                 
                            </div>
                                <div class="col-lg-12">
                                  
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td>
                                                <div id="Div1" runat="server" style="overflow:auto; height:550px" >
                                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                    <asp:GridView ID="gvCustsales" runat="server" AllowPaging="false" OnRowCommand="gvCustsales_RowCommand" PageSize="100" CssClass="myGridStyle"
                                                        DataKeyNames="orderid" ShowFooter="true" OnPageIndexChanging="Page_Change" OnRowDataBound="gvCustsales_RowDataBound"
                                                        AutoGenerateColumns="false" EmptyDataText="No data found!" ShowHeaderWhenEmpty="True">
                                                        <Columns>
                                                            
                                                           <asp:BoundField HeaderText="Order No" DataField="orderno" />
                                                             <asp:BoundField HeaderText="Order Date" DataField="orderdate" DataFormatString='{0:dd-MM-yyyy}' />
                                                             <%--<asp:BoundField HeaderText="Invoice Date" DataField="invdate" DataFormatString='{0:dd-MM-yyyy}' />--%>
                                                            
                                                            <asp:BoundField HeaderText="Supplier Name" DataField="ledgername"  />
                                                             <asp:BoundField HeaderText="Supplier OrderNO" DataField="SupplierOrderno"  />
                                                             <asp:BoundField HeaderText="Company Code" DataField="Companycode"  />
                                                             <asp:BoundField DataField="recmeter" HeaderText="Remain Meter" DataFormatString='{0:f2}' />
                                                             <asp:BoundField DataField="remain" HeaderText="Received Meter" DataFormatString='{0:f2}' />
                                                              <asp:BoundField HeaderText="Total Meter" DataField="TotalMeter" DataFormatString='{0:f2}'  />
                                                              
                                                              
                                                              
                                                            <%--<asp:BoundField HeaderText="Checked Sign" DataField="name" />
                                                             <asp:BoundField HeaderText="Total Meter" DataField="TotalMeter" />
                                                          --%>
                                                          <asp:TemplateField HeaderText="Edit" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("orderid") %>'
                                                        CommandName="Cancel">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("orderid") %>' />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("orderid")%>'
                                                        CommandName="Print">
                                                        <asp:Image ID="img1" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable1" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID1" runat="server" Value='<%# Bind("orderid")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="New Print" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprintnew" runat="server" CommandArgument='<%#Eval("orderid")%>'
                                                        CommandName="Printnew">
                                                        <asp:Image ID="img1ew" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable1ew" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID1ew" runat="server" Value='<%# Bind("orderid")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

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