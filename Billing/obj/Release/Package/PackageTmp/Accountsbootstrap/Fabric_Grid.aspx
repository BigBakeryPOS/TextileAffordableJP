<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fabric_Grid.aspx.cs" Inherits="Billing.Accountsbootstrap.Fabric_Grid" %>

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
    <title>Fabric Receive & Return</title>
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
    <link rel="stylesheet" href="../Styles/chosen.css" />
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
    <style type="text/css">
        .body
        {
            margin: 0;
            padding: 0;
            height: 100%;
        }
        .modal
        {
            position: absolute;
            top: 0px;
            left: 0px;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
        }
        #divImage
        {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 550px;
            width: 600px;
            padding: 3px;
            border: solid 1px black;
        }
    </style>
    <script type="text/javascript">
        function LoadDiv(url) {
            var img = new Image();

            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            var imgLoader = document.getElementById("imgLoader");
            imgLoader.style.display = "block";
            img.onload = function () {
                imgFull.src = img.src;
                imgFull.style.display = "block";
                imgLoader.style.display = "none";
            };
            img.src = url;
            var width = document.body.clientWidth;
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }
            imgDiv.style.left = (width - 650) / 2 + "px";
            imgDiv.style.top = "20px";
            bcgDiv.style.width = "100%";

            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }
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
        function Search_Gridviewret(strKey, strGV) {


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
        function Denomination() {


            var gridData = document.getElementById('gvCustsales');



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
                    <div class="col-lg-4">
                        <h1 class="page-header">
                            Fabric Receive & Return</h1>
                    </div>
                    <div class="col-lg-3">
                        <label>
                            Select Type</label>
                        <div class="form-group">
                            <asp:RadioButtonList ID="rbltype" runat="server" CssClass="center-block" AutoPostBack="true"
                                OnSelectedIndexChanged="rbltype_OnSelectedIndexChanged" RepeatColumns="2">
                                <asp:ListItem Text="Purchase" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Return" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-lg-5">
                    </div>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-1">
                                    <asp:Button ID="btnall" Visible="false" runat="server" Text="Generate Report" CssClass="btn btn-success" />
                                    &nbsp
                                    <asp:Button ID="btnViewAll" Visible="false" runat="server" Text="View All" CssClass="btn btn-success"
                                        OnClick="btnViewAll_Click" />
                                </div>
                                <div class="col-lg-2">
                                    <%--<asp:ScriptManager ID="ScriptManager2" runat="server">
                                    </asp:ScriptManager>--%>
                                    <div class="form-group">
                                        <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                                        <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                            OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                            PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                            CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                                        <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                            OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                            PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                            CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label ID="lblsupplier" runat="server">Supplier Name</asp:Label>
                                    <asp:DropDownList ID="ddlsupplier" runat="server" AutoPostBack="true" class="chzn-select"
                                        Width="220px" Height="80px" OnSelectedIndexChanged="ddlsupplier_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-1">
                                    <br />
                                    <asp:Button ID="btn" runat="server" Text="Print" Visible="true" CssClass="btn btn-danger"
                                        OnClientClick="Denomination()" Width="100px" />
                                </div>
                                <div class="col-lg-2">
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                        ErrorMessage="Please enter your searching Data!" Text="." Style="color: White" />
                                    <asp:TextBox CssClass="form-control" ID="txtsearch" runat="server" onkeyup="Search_Gridview(this, 'gvCustsales')"
                                        placeholder="Search Text" Width="180px"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                        FilterType="LowercaseLetters, UppercaseLetters,Numbers,custom" ValidChars=" /-"
                                        TargetControlID="txtsearch" />
                                </div>
                                <div class="col-lg-2">
                                    <br />
                                    <asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Add New" Width="130px"
                                        OnClick="Add_Click" />
                                </div>
                                <div class="col-lg-12">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="panel panel-default">
                                                <div class="col-lg-2">
                                                </div>
                                                <div class="col-lg-8">
                                                    <div id="ret" runat="server" visible="false">
                                                        <table border="2" cellpadding="0" cellspacing="0" runat="server">
                                                            <tr>
                                                                <td>
                                                                    <div id="Div2" runat="server" class="form-group">
                                                                        <label>
                                                                            ReturnNo</label>
                                                                        <asp:TextBox CssClass="form-control" ID="txtreturnno" Enabled="false" runat="server"
                                                                            Width="150px"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="form-group">
                                                                        <label>
                                                                            Return Date</label>
                                                                        <asp:TextBox ID="txtreturndate" runat="server" CssClass="form-control center-block"
                                                                            Width="170px"></asp:TextBox>
                                                                        <ajaxToolkit:CalendarExtender ID="calext" TargetControlID="txtreturndate" PopupButtonID="txtreturndate"
                                                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div id="Div3" runat="server" class="form-group">
                                                                        <label>
                                                                            Transport</label>
                                                                        <asp:TextBox CssClass="form-control" ID="txttransport" Enabled="true" runat="server"
                                                                            Width="150px"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div id="Div4" runat="server" class="form-group">
                                                                        <label>
                                                                            Narration</label>
                                                                        <asp:TextBox CssClass="form-control" ID="txtnarration" Enabled="true" runat="server"
                                                                            Width="150px"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="form-group">
                                                                        <label>
                                                                            PrePared By</label><br />
                                                                        <asp:DropDownList ID="ddlpreparedby" Width="195px" Height="60px" runat="server" CssClass="chzn-select">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="form-control" ID="txtretgrid" runat="server" onkeyup="Search_Gridviewret(this, 'gvreturn')"
                                                                        placeholder="Search Text" Width="180px"></asp:TextBox>
                                                                    <asp:Button ID="Btncalc" runat="server" Text="Calc" CssClass="button" OnClick="Btncalc_OnClick" />
                                                                    <asp:Label ID="lbllretmeter" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <div id="scrl" runat="server" style="overflow: auto; height: 350px">
                                                                        <asp:GridView runat="server" ID="gvreturn" CssClass="myGridStyle" Width="50%" GridLines="Both"
                                                                            AutoGenerateColumns="false" ShowFooter="true">
                                                                            <Columns>
                                                                                <asp:BoundField HeaderText="Fabric Type" DataField="Itemname" />
                                                                                <asp:BoundField HeaderText="Design" DataField="Design" />
                                                                                <asp:BoundField HeaderText="Color" DataField="Color" />
                                                                                <asp:BoundField HeaderText="Roll/Taka" DataField="Piece" />
                                                                                <%--<asp:BoundField HeaderText="Avaliable Meter" DataField="AvaliableMeter" DataFormatString='{0:f}' />--%>
                                                                                <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' />
                                                                                <asp:BoundField HeaderText="Width" DataField="Widthname" />
                                                                                <asp:TemplateField HeaderText="Avaliable KG" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAvaliableMeter" Text='<%# Eval("AvaliableMeter","{0:n}")%>' runat="server"
                                                                                            Enabled="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="5%" HeaderText="Return KG">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtreturn" runat="server" CssClass="form-control center-block">0</asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Transid" ControlStyle-Width="100%" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbllTransid" Text='<%# Eval("Transid")%>' runat="server" Enabled="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Fabid" ControlStyle-Width="100%" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFabid" Text='<%# Eval("Fabid ")%>' runat="server" Enabled="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                    <asp:Button ID="btnreturnsave" runat="server" Text="Save" CssClass="button" OnClick="btnreturnsave_OnClick" />
                                                                    <asp:Button ID="btnexit" runat="server" Text="Exit" CssClass="button" OnClick="btnexit_OnClick" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-striped">
                                            <tr>
                                                <td>
                                                    <div id="Div1" runat="server" style="overflow: auto; height: 550px">
                                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>.
                                                        <asp:GridView ID="gvCustsales" runat="server" AllowPaging="false" PageSize="100"
                                                            CssClass="myGridStyle" DataKeyNames="fabid" ShowFooter="true" OnRowCommand="gvCustsales_RowCommand"
                                                            AutoGenerateColumns="false" EmptyDataText="No data found!" ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="Fab.No"
                                                                    HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                                    <ItemTemplate>
                                                                        <a href="javascript:switchViews('dv<%# Eval("fabno") %>', 'imdiv<%# Eval("fabno") %>');"
                                                                            style="text-decoration: none;">
                                                                            <img id="imdiv<%# Eval("fabno") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                                        </a>
                                                                        <%# Eval("fabno")%>
                                                                        <div id="dv<%# Eval("fabno") %>" style="display: none; position: relative;">
                                                                            <asp:GridView runat="server" ID="gvLiaLedger" CssClass="myGridStyle" Width="82%"
                                                                                GridLines="Both" AutoGenerateColumns="false" DataKeyNames="transid" ShowFooter="true"
                                                                                OnRowCommand="gvlia_comm">
                                                                                <Columns>
                                                                                    <asp:BoundField HeaderText="Fabric Type" DataField="Itemname" />
                                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="DesignNo"
                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <a href="javascript:switchViews('dv<%# Eval("Design") %>', 'imdiv<%# Eval("Design") %>');"
                                                                                                style="text-decoration: none;"></a>
                                                                                            <%# Eval("Design")%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField HeaderText="Color" DataField="Color" />
                                                                                    <asp:BoundField HeaderText="Roll/Taka" DataField="Piece" />
                                                                                    <asp:BoundField HeaderText="Avaliable KG" DataField="AvaliableMeter" DataFormatString='{0:f}' />
                                                                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' />
                                                                                    <asp:BoundField HeaderText="Width" DataField="Widthname" />
                                                                                    <asp:BoundField HeaderText="Is Completed(Y/N)" Visible="true" DataField="Status" />
                                                                                    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Preview Image">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("imagepath")%>'
                                                                                                Width="50px" Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Edit" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("transid") %>'
                                                                                                CommandName="edit">
                                                                                                <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                                                            <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                                                                Enabled="false" ToolTip="Not Allow To Delete" />
                                                                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("transid") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="DCNo" DataField="fabno" />
                                                                <asp:BoundField HeaderText="Invoice Ref.No" DataField="Refno" />
                                                                <asp:BoundField HeaderText="Invoice Date" DataField="invdate" DataFormatString='{0:dd-MM-yyyy}' />
                                                                <asp:BoundField HeaderText="Entry Date" DataField="CreatedDate" DataFormatString='{0:dd/MM/yyyy}' />
                                                                <asp:BoundField HeaderText="Bale Open Date" DataField="regdate" DataFormatString='{0:dd-MM-yyyy}'
                                                                    Visible="false" />
                                                                <asp:BoundField HeaderText="Supplier" DataField="ledgername" />
                                                                <asp:BoundField HeaderText="Checked Sign" DataField="name" />
                                                                <asp:BoundField HeaderText="Total KG" DataField="TotalMeter" />
                                                                <asp:BoundField HeaderText="Total Amt" DataField="TotalAmount" />
                                                                <%--<asp:ImageField DataImageUrlField="Imagepath" HeaderText="Samaple Image" Visible="false" />--%>
                                                                <asp:TemplateField HeaderText="Download Here">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDownload" runat="server" CommandName='<%# Eval("Imagepath") %>'
                                                                            Text="D" OnClick="lnkDownload_OnClick" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("Fabid") %>'
                                                                            CommandName="Edit">
                                                                            <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                                        <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                                            ToolTip="Not Allow To Delete" />
                                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("Fabid") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Print">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("fabid") %>'
                                                                            CommandName="print">
                                                                            <asp:Image ID="print" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ViewAll" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnstatus" runat="server" CommandArgument='<%#Eval("fabid") %>'
                                                                            CommandName="Status">
                                                                            <asp:Image ID="img1" runat="server" ImageUrl="~/images/info_button.png" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Return" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnretmtr" runat="server" CommandArgument='<%#Eval("fabid") %>'
                                                                            CommandName="Return">
                                                                            <asp:Image ID="img1ret" runat="server" ImageUrl="~/images/info_button.png" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ReturnPrint">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnreturnprint" runat="server" CommandArgument='<%#Eval("ReturnId") %>'
                                                                            CommandName="ReturnPrint">
                                                                            <asp:Image ID="returnprint" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
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
                                        <asp:LinkButton Text="" ID="lnkFakenew" runat="server"></asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="mpenew" runat="server" PopupControlID="pnlPopupnew"
                                            TargetControlID="lnkFakenew" CancelControlID="Button1" BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="pnlPopupnew" runat="server" ScrollBars="Auto" Height="70%" Width="70%"
                                            CssClass="modalPopup" Style="display: none">
                                            <div class="header">
                                            </div>
                                            <div align="center" style="background-color: #57c5f1">
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView runat="server" ID="GVFullFabric" Style="overflow: scroll; height: 60%"
                                                                CssClass="myGridStyle" Width="100%" GridLines="Both" AutoGenerateColumns="false"
                                                                OnRowDataBound="gridprint_RowDataBound" ShowFooter="true" Caption="Raw Materials Details">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Fabric Type" DataField="itemname" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="Design No" Visible="false" DataField="DesignNo" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="Color" DataField="Color" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="Width" DataField="Width" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="Rate" ItemStyle-HorizontalAlign="Center" DataField="Rate"
                                                                        DataFormatString='{0:f}' />
                                                                    <asp:BoundField HeaderText="Billing KG" DataField="billMeter" DataFormatString='{0:f}'
                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="Purchase KG" DataField="Meter" DataFormatString='{0:f}'
                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="Avaliable KG" DataField="AvaliableMeter" DataFormatString='{0:f}'
                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="Pinning" Visible="false" DataField="Pinning" DataFormatString='{0:f}'
                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:ImageField DataImageUrlField="Imagepath" ControlStyle-Height="80" HeaderText="Sample Image"
                                                                        Visible="false" />
                                                                    <asp:ImageField Visible="false" ControlStyle-Width="100px" HeaderText="Sample" />
                                                                </Columns>
                                                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="footer" align="right">
                                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="button" />
                                            </div>
                                        </asp:Panel>
                                        <%--<asp:LinkButton Text="" ID="lnkreturnnew" runat="server"></asp:LinkButton>--%>
                                        <%--<ajaxToolkit:ModalPopupExtender ID="mpereturn" runat="server" PopupControlID="pnlPopupnew1"
                                            TargetControlID="lnkreturnnew" CancelControlID="Button2" BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>--%>
                                        <%--<asp:Panel ID="pnlPopupnew1" runat="server" ScrollBars="Auto" Height="70%" Width="70%"
                                            CssClass="modalPopup" Style="display: none">
                                            <div class="header">
                                            </div>
                                            <div class="body">
                                            </div>
                                            <div class="footer" align="right">
                                            </div>
                                        </asp:Panel>--%>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnExport" Visible="false" Text="Export to Excel" runat="server"
                                            CssClass="btn btn-success" Height="37px" /></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divBackground">
            </div>
        </ContentTemplate>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvCustsales" EventName="RowDataBound" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <div id="divImage">
        <table style="height: 100%; width: 100%">
            <tr>
                <td valign="middle" align="center">
                    <%--<img id="imgLoader" alt=""
    src="images/loader.gif" />--%>
                    <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
                </td>
            </tr>
            <tr>
                <td align="center" valign="bottom">
                    <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
                </td>
            </tr>
        </table>
    </div>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({
            allow_single_deselect: true
        }); </script>
    </form>
</body>
</html>
