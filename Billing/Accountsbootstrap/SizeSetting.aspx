<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SizeSetting.aspx.cs" Inherits="Billing.Accountsbootstrap.SizeSetting" %>

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
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Size Setting Master - bootsrap</title>
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
    <div>
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header" style="text-align: center; color: #fe0002;">
                Size Master</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div style="height: 350px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="SizeGrid" runat="server" CssClass="myGridStyle" AllowPaging="true" Width="100px"
                                        PageSize="100" AllowSorting="true" EmptyDataText="No Records Found" AutoGenerateColumns="false">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:TemplateField Visible="false" HeaderText="prID" SortExpression="prID" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("Fitid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Fit" HeaderText="Width / Fit" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="36" >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtts"  runat="server" Text='<%# Bind("[36Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderpaid1" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtts" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="44">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtfe"  runat="server" Text='<%# Bind("[44Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderpaid2" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtfe" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="55">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtff"  runat="server" Text='<%# Bind("[55Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderpaidd3" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtff" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="56">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtfs"  runat="server" Text='<%# Bind("[56Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderpaiid3" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtff" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="57">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtfse"  runat="server" Text='<%# Bind("[57Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderpaaid3" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtff" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="58">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtfei"  runat="server" Text='<%# Bind("[58Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderppaid3" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtff" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="54">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt54"  runat="server" Text='<%# Bind("[54Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderppaid54" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtff" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="32">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt32"  runat="server" Text='<%# Bind("[32Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderppaid32" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtff" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="30">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt30"  runat="server" Text='<%# Bind("[30Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderppaid30" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtff" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="27">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt27"  runat="server" Text='<%# Bind("[27Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderppaid27" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtff" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="62">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt62"  runat="server" Text='<%# Bind("[62Width]") %>' MaxLength="10" Width="50px">0</asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderppaid62" runat="server"
                                                        FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtff" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="Div1" runat="server" align="center" class="col-lg-12">
        <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
        <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" OnClick="Save_Click"
            ValidationGroup="val1" Style="width: 117px;" />
        <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" Style="width: 120px;" />
        <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
        <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
    </div>
    </form>
</body>
</html>
