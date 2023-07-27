<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialsIssueGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.MaterialsIssueGrid" %>

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
    <title>Materials Issue</title>
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
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false">
    </asp:Label>
    <form runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <div class="col-lg-3">
                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 18px;
                    font-weight: bold">
                    Materials Issue</h1>
            </div>
            <div runat="server" visible="false" class="col-lg-2">
                <asp:DropDownList CssClass="form-control" ID="ddlfilter" AutoPostBack="true" OnSelectedIndexChanged="ddlfilter_OnSelectedIndexChanged"
                    runat="server">
                    <asp:ListItem Text="All" Value="1">
                    </asp:ListItem>
                    <asp:ListItem Text="Pending" Value="2" Selected="True">
                    </asp:ListItem>
                    <asp:ListItem Text="Copmpleted" Value="3">
                    </asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-2">
                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvBuyerOrderCutting')"
                    ID="txtsearch" runat="server" placeholder="Enter Text to Search" Width="250px">
                </asp:TextBox>
            </div>
            <div class="col-lg-1">
            </div>
            <div class="col-lg-1">
                <asp:Button ID="btnadd" runat="server" class="btn btn-primary" Text="Add New" OnClick="Add_Click"
                    Width="130px" />
            </div>
            <div class="col-lg-3">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvBuyerOrderCutting" runat="server" CssClass="myGridStyle1" Width="100%"
                                        EmptyDataText="No Records Found" AutoGenerateColumns="false" OnRowCommand="gvBuyerOrderCutting_RowCommand">
                                        <HeaderStyle BackColor="White" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Material Issue Type" DataField="MissueType" />
                                            <asp:BoundField HeaderText="PRP.NO" DataField="PRPNO" />
                                            <asp:BoundField HeaderText="CuttingDate" DataField="MaterialDate" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField HeaderText="ExcNo" DataField="ExcNo" />
                                            <asp:BoundField HeaderText="Ledger Name" DataField="ledgername" />
                                            <asp:BoundField HeaderText="Issue Fro" DataField="IssueFor" />
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("MaterialissueId") %>'
                                                        CommandName="edit">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("MaterialissueId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Issue Print" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPrint" runat="server" CommandArgument='<%#Eval("MaterialissueId") %>'
                                                        CommandName="IssuePrint">
                                                        <asp:Image ID="imgPrint" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
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
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
