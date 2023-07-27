<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GodownMaster.aspx.cs" Inherits="Billing.Accountsbootstrap.GodownMaster" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Godown Master </title>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <%--<link href="../css/Header.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <style>
        .pagination-ys
        {
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td
        {
            display: inline;
        }
        
        .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span
        {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            color: #dd4814;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            margin-left: -1px;
        }
        
        .pagination-ys table > tbody > tr > td > span
        {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            margin-left: -1px;
            z-index: 2;
            color: #aea79f;
            background-color: #f5f5f5;
            border-color: #dddddd;
            cursor: default;
        }
        
        .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span
        {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span
        {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus
        {
            color: #97310e;
            background-color: #eeeeee;
            border-color: #dddddd;
        }
        .style14
        {
            width: 14%;
        }
        .style15
        {
            width: 15%;
        }
    </style>
    <link href="../Styles/chosen.css" rel="Stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link rel="stylesheet" href="../css/chosen.css" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
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
    <script type="text/javascript">
        function Confirm(myButton) {

            // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // disable the button
                myButton.disabled = true;
                myButton.className = "btn-inactive";
                myButton.value = "processing...";
            }
            return true;


        }
    </script>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="col-lg-2">
                        <div class="form-group">
                            <h2 style="text-align: left; color: #fe0002; font-size: 20px; font-weight: bold">
                                GODOWN MASTER</h2>
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlactiveselect" runat="server" CssClass="form-control">
                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control">
                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                <asp:ListItem Text="GodownCode" Value="1"></asp:ListItem>
                                <asp:ListItem Text="GodownName" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Unit" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="txtsearch" runat="server" placeholder="Search Text"
                                onkeyup="Search_Gridview(this, 'GVGodown')"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <asp:Button ID="btnsearch" runat="server" class="btn btn-info" Text="Search" Width="100px"
                                OnClick="btnsearch_OnClick" />
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <asp:Button ID="btnreset" runat="server" class="btn btn-primary" Text="Reset" Width="100px"
                                OnClick="btnreset_OnClick" />
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <div class="form-group">
                            <asp:Button ID="btnexcel" runat="server" class="btn btn-warning" Text="Export-To-Excel"
                                Width="120px" OnClick="btnexcel_OnClick" />
                        </div>
                    </div>
                    <div class="col-lg-5" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="table-responsive">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="Div1" runat="server" style="height: 520px; overflow: scroll;" align="center">
                                            <asp:GridView ID="GVGodown" runat="server" EmptyDataText="No Records Found" DataKeyNames="Godownid"
                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="GVGodown_OnPageIndexChanging" Width="100%"
                                                AllowSorting="false" OnSorting="GVGodown_OnSorting" OnSelectedIndexChanged="GVGodown_SelectedIndexChanged"
                                                AutoGenerateColumns="false" CssClass="myGridStyle1">
                                                <HeaderStyle BackColor="#3366FF" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                    NextPageText="Next" PreviousPageText="Previous" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Godown Code" DataField="GodownCode" SortExpression="GodownCode" />
                                                    <asp:BoundField HeaderText="Godown Name" DataField="GodownName" SortExpression="GodownName" />
                                                    <asp:BoundField HeaderText="Unit" DataField="UnitName" SortExpression="UnitName" />
                                                    <asp:BoundField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("Godownid") %>' CommandName="Select"
                                                                runat="server">
                                                                <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
            <div class="col-lg-6">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #428bca; color: White; border-color: #06090c">
                            <i class="fa fa-briefcase"></i>
                            <asp:Label ID="lblName" Text="Add Godown Details" runat="server"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="list-group">
                                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val2"
                                    ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>
                                            Godown Code</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtGodowncode"
                                            ValidationGroup="val2" Text="*" ErrorMessage="Please Enter Godown Code" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" ID="txtGodowncode" Style="text-transform: capitalize"
                                            runat="server" placeholder="Enter Godown Code"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server"
                                            FilterType="LowercaseLetters,UppercaseLetters" ValidChars="" TargetControlID="txtGodowncode" />
                                        <asp:HiddenField ID="hdGodownId" runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Godown Name</label>
                                        <asp:RequiredFieldValidator runat="server" ID="txtcomname" ControlToValidate="txtGodownname"
                                            ValidationGroup="val2" Text="*" ErrorMessage="Please Enter Godown Name" Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" ID="txtGodownname" Style="text-transform: capitalize"
                                            runat="server" placeholder="Enter Godown Name"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Unit</label>
                                        <asp:CompareValidator ID="CompareValidator9" runat="server" ValidationGroup="val2"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlunit" ValueToCompare="Select Unit"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please Select Unit"></asp:CompareValidator>
                                        <asp:DropDownList ID="ddlunit" runat="server" CssClass="chzn-select" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Active</label><br />
                                        <asp:DropDownList ID="ddlactive" runat="server" class="form-control" CssClass="form-control">
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnsave" runat="server" class="btn btn-success" Text="Save" OnClick="btnsave_OnClick"
                                            Width="120px" OnClientClick="Confirm(this)" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn btn-danger" Text="Cancel" Width="120px"
                                            OnClick="btnCancel_OnClick" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
</body>
</html>
