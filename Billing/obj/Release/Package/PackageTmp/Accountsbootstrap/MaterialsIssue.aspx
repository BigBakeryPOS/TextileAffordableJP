<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialsIssue.aspx.cs"
    Inherits="Billing.Accountsbootstrap.MaterialsIssue" %>

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
    <title>Materials Issue </title>
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false">
    </asp:Label>
    <asp:Label runat="server" ID="FontSize" ForeColor="White" CssClass="label" Visible="false"
        Text="17"></asp:Label>
    <form runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 20px;
                                font-weight: bold">
                                Materials Issue
                            </h1>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Material Issue No
                                    </label>
                                    <asp:TextBox ID="txtmaterialissno" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        MAterial Issue. Date:</label>
                                    <asp:TextBox ID="txtmaterialdate" runat="server" CssClass="form-control" Width="100px"
                                        Enabled="false"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtmaterialdate"
                                        PopupButtonID="txtCuttingDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Select Issue Type
                                    </label>
                                    <asp:DropDownList ID="drpissuetype" runat="server" CssClass="form-control" OnSelectedIndexChanged="issuetype_change"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="Cutting Issue" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Production Process Issue" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Select Exc.No
                                    </label>
                                    <asp:DropDownList ID="ddlExcNo" runat="server" CssClass="chzn-select" Width="100%"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlExcNo_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Against PRP.NO
                                    </label>
                                    <asp:DropDownList ID="drpprpno" runat="server" CssClass="chzn-select" OnSelectedIndexChanged="prp_chnaged"
                                        AutoPostBack="true" Style="height: 30px" Width="100%">
                                    </asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        CompanyName</label>
                                    <asp:CompareValidator ID="CompareValidator11" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCompanyName"
                                        ValueToCompare="CompanyName" Operator="NotEqual" Type="String" ErrorMessage="Please Select CompanyName."></asp:CompareValidator>
                                    <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" Style="height: 30px"
                                        Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyName_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Jobwork Ledger
                                    </label>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                        Text="*" Style="color: Red" initialvalue="0" ControlToValidate="ddlProcessLedger"
                                        ValueToCompare="Select Jobwork Ledger" Operator="NotEqual" Type="String" ErrorMessage="Please Select Jobwork Ledger.">
                                    </asp:CompareValidator>
                                    <asp:DropDownList ID="ddlProcessLedger" runat="server" CssClass="chzn-select" Style="height: 30px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Issue For
                                    </label>
                                    <br />
                                    <asp:Label ID="lblissuefor" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSave_OnClick"
                                    Width="110px" />
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-info" OnClick="btnExit_OnClick"
                                    Width="110px" />
                            </div>
                        </div>
                        <div id="Excel" runat="server">
                            <div class="panel-body">
                                <div class="col-lg-12">
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-9">
                                        <div id="Div5" runat="server">
                                            <asp:TextBox CssClass="form-control" Visible="false" Enabled="true" onkeyup="Search_Gridview(this, 'gvLabels')"
                                                ID="TextBox1" runat="server" Style="margin-top: -20px" placeholder="Enter Text to Search"
                                                Width="250px">
                                            </asp:TextBox>
                                            <asp:GridView ID="GVFabricDetails" runat="server" Width="100%" EmptyDataText="No Records Found"
                                                Caption="Material Details" AutoGenerateColumns="false" CssClass="myGridStyle1">
                                                <HeaderStyle BackColor="White" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                    NextPageText="Next" PreviousPageText="Previous" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                            <asp:HiddenField ID="hdItemId" runat="server" Value='<%#Eval("ItemId") %>' />
                                                            <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId") %>' />
                                                            <asp:HiddenField ID="hdRequiredStock" runat="server" Value='<%#Eval("RequiredStock") %>' />
                                                            <asp:HiddenField ID="hdIssueStock" runat="server" Value='<%#Eval("IssueStock") %>' />
                                                            <%--<asp:HiddenField ID="hdUsedQty"  runat="server" Value='<%#Eval("UsedQty") %>' />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Item" HeaderText="Item" />
                                                    <asp:BoundField DataField="Color" HeaderText="Color" />
                                                    <asp:BoundField DataField="RequiredStock" HeaderText="RequiredStock" DataFormatString="{0:f2}"
                                                        ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="IssueStock" HeaderText="IssuedStock" DataFormatString="{0:f2}"
                                                        ItemStyle-HorizontalAlign="Right" />
                                                    <%--<asp:BoundField DataField="UsedQty" HeaderText="UsedQty" DataFormatString="{0:f2}"
                                                        ItemStyle-HorizontalAlign="Right" />--%>
                                                    <asp:TemplateField HeaderText="Avl.Stock" HeaderStyle-Width="45px" ItemStyle-Width="45px"
                                                        ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <label>
                                                            </label>
                                                            <asp:Label ID="lblAvlStock" Height="30px" Width="100%" runat="server" Text='<%#Eval("AvlStock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issue Qty" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                                                        ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIssueQty" Height="30px" Width="100%" runat="server" Text='<%#Eval("IssueQty") %>'
                                                                CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                                TargetControlID="txtIssueQty" ValidChars="." FilterType="Numbers,Custom" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                    </div>
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
