<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewprocess.aspx.cs" Inherits="Billing.Accountsbootstrap.viewprocess" %>

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
    <title>Fabric Process - bootsrap</title>
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
            alert('Are You Sure, You want to delete This Brand!');
        }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(".pic").live("mousemove", function (e) {
            var dv = $("#popup");
            if (dv.length == 0) {
                var src = $(this)[0].src;
                var dv = $("<div />").css({ height: 100, width: 100, position: "absolute" });
                var img = $("<img />").css({ height: 250, width: 250 }).attr("src", src);
                dv.append(img);
                dv.attr("id", "popup");
                $("body").append(dv);
            }
            dv.css({ left: e.pageX, top: e.pageY });
        });
        $(".pic").live("mouseout", function () {
            $("#popup").remove();
        });
         
  
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1">
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <div class="row">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header" style="text-align: center; color: #fe0002;">
                Fabric Process</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <%-- <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Group Master</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-1">
                            </div>
                            <div runat="server" visible="false" class="col-lg-16">
                                <asp:Label runat="server" ID="lblSelectedValue"></asp:Label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                                    Text="." Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                <asp:DropDownList CssClass="form-control" ID="ddlfilter" Width="150px" Style="margin-top: -20px"
                                    runat="server">
                                    <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="ProcessID" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Width" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="DNo" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-16">
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="." Style="color: White" />
                                <asp:TextBox CssClass="form-control" ID="txtsearch" runat="server"
                                    Style="margin-top: -20px" placeholder="Search Text" Width="350px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,custom" ValidChars=" /-"
                                    TargetControlID="txtsearch" />
                            </div>
                            <div runat="server" visible="false" class="col-lg-17">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" ValidationGroup="val1"
                                    OnClick="Search_Click" Width="130px" />
                            </div>
                            <div runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"
                                    Width="130px" />
                            </div>
                            <div class="col-lg-15">
                                &nbsp;&nbsp;</div>
                            <div class="col-lg-17">
                                &nbsp;&nbsp;</div>
                            <div class="col-lg-17">
                                &nbsp;&nbsp;</div>
                            <div class="col-lg-17">
                                <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" Width="130px"
                                    OnClick="Add_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvcust" EmptyDataText="No records Found" runat="server" CssClass="myGridStyle"
                                        AllowPaging="true" PageSize="1000" OnPageIndexChanging="Page_Change" AutoGenerateColumns="false"
                                        OnRowCommand="gvcust_RowCommand" Width="85%" OnRowDataBound="gvcust_RowDataBound">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Invoice No" Visible="false" DataField="fabno" />
                                            <asp:BoundField HeaderText="Invoice No" DataField="refno" />
                                            <asp:BoundField HeaderText="Invoice Date" DataField="regdate" DataFormatString='{0:dd/MM/yyyy}' />
                                            <asp:BoundField HeaderText="Inv.Ref.Date" DataField="invdate" DataFormatString='{0:dd/MM/yyyy}' />
                                            <asp:BoundField HeaderText="Supplier Name" DataField="ledgername" />
                                            <asp:BoundField HeaderText="Total Meter" DataField="totalmeter" DataFormatString='{0:f}' />
                                            <asp:BoundField HeaderText="Total Amount" DataField="totalamount" DataFormatString='{0:f}' />
                                            <asp:BoundField HeaderText="Checked Sign" DataField="name" />
                                            <asp:BoundField HeaderText="Brand Name" DataField="BrandName" />
                                            <%-- <asp:ImageField DataImageUrlField = "ClothImage" ControlStyle-Height = "50" ControlStyle-Width = "50" HeaderText="Image Preview" />--%>
                                            <asp:TemplateField HeaderText="Edit" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("fabid") %>'
                                                        CommandName="edit">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("fabid") %>' />
                                                    <%--<asp:HiddenField ID="idcheque" runat="server" Value='<%# Bind("FromCheque") %>' />
                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("ToCheque") %>' />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("fabid") %>'
                                                        CommandName="delete" OnClientClick="alertMessage()">
                                                        <asp:Image ID="dlt" runat="server" ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable1" ImageUrl="~/images/delete.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                                        CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btndelete"
                                                        PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                    <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                                        TargetControlID="btndelete" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                                    </ajaxToolkit:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Print">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("fabid") %>'
                                                        CommandName="print">
                                                        <asp:Image ID="print" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="PrintCR">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprintCR" runat="server" CommandArgument='<%#Eval("fabid") %>'
                                                        CommandName="printCR">
                                                        <asp:Image ID="printCR" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
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
                <!-- /.col-lg-6 (nested) -->
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </div>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Fabric Process:</div>
                <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                </div>
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure want to delete?
                </p>
            </div>
            <div class="popup_Buttons">
                <input id="ButtonDeleleOkay" type="button" value="Yes" />
                <input id="ButtonDeleteCancel" type="button" value="No" />
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
