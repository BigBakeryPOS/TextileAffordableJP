<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DilloMultiLotGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.DilloMultiLotGrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
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
    <title>Flexible Apparels || Multi Lot Process Grid</title>
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

     <script type="text/javascript">


         function switchViews1(obj, imG) {
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

    <script type="text/javascript">


        function switchViews2(obj, imG) {
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
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 class="page-header" style="text-align: center; color: #fe0002;font-size:16px; font-weight:bold">
               Multi Lot Process Details</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" align="center">
                            <div class="col-lg-1">
                            </div>
                            <div id="Div1" runat="server" visible="false" class="col-lg-16">
                                <asp:Label runat="server" ID="Label1"></asp:Label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                                    Text="*" Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                <asp:DropDownList CssClass="form-control" ID="ddlfilter" Width="150px" style="margin-top:-20px" runat="server">
                                    <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Contact Name" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="MobileNo" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-17">
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="*" Style="color: White" />
                                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvcust')"  ID="txtsearch" runat="server" style="margin-top:-20px"
                                    placeholder="Enter Text to Search" Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -.@"
                                    TargetControlID="txtsearch" />
                            </div>
                            <div id="Div2" runat="server" visible="false" class="col-lg-17">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsearch" runat="server" ValidationGroup="val1" class="btn btn-success"
                                    Text="Search"  Width="130px" />
                            </div>
                            <div id="Div3" runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset"
                                    Width="130px" />
                            </div>
                            <div class="col-lg-2">
                            <asp:DropDownList ID="drpMultiemployee" OnSelectedIndexChanged="Employee_changed" AutoPostBack="true" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                             </div>
                              <div runat="server" visible="false" class="col-lg-2">
                            <asp:DropDownList ID="drppending" OnSelectedIndexChanged="pending_changed" AutoPostBack="true" CssClass="form-control" runat="server">
                            <asp:ListItem Text="All" Value="All"></asp:ListItem>
                            <asp:ListItem Text="Pending" Selected="True" Value="N"></asp:ListItem>
                            <asp:ListItem Text="Completed" Value="Y"></asp:ListItem></asp:DropDownList>
                               
                            </div>
                            <div class="col-lg-17">
                                <asp:Button ID="Button3" runat="server" class="btn btn-success" Text="Add New" PostBackUrl="~/Accountsbootstrap/DilloMultipleLot.aspx"
                                    Width="130px"  />
                            </div>
                          
                            <div runat="server"  class="col-lg-17">
                                <asp:Button ID="btnexcel" visible="false" runat="server" class="btn btn-info" Text="Export-To-Excel"
                                    Width="130px" Height="32px" />
                            </div>
                             <div id="Div4" runat="server" style="width:22%"  class="col-lg-18">
                                <asp:TextBox ID="Barcode" runat="server" OnTextChanged="Barcode_indexChanged" AutoPostBack="true" CssClass="form-control" placeholder="Scan Barcode Value"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="height:392px; overflow:auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvcust" runat="server" CssClass="myGridStyle" EmptyDataText="No records Found"
                                        AllowPaging="true" PageSize="100000"  DataKeyNames="multiid" 
                                        AutoGenerateColumns="false" onrowcommand="gvcust_RowCommand" OnRowDataBound="gvcust_RowDataBound">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <%--<asp:BoundField HeaderText="Customer ID" DataField="CustomerID" Visible="false" />--%>
                                             <asp:BoundField  DataField="multiid" Visible="false" />
                                             <asp:BoundField DataField="Fullno" Visible="false" HeaderText="Multi Number" />
                                              <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="Multi Number"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews('dv<%# Eval("Fullno") %>', 'imdiv<%# Eval("Fullno") %>');"
                                                                        style="text-decoration: none;">
                                                                        <img id="imdiv<%# Eval("Fullno") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                                    </a>
                                                                    <%# Eval("Fullno")%>
                                                                    <div id="dv<%# Eval("Fullno") %>" style="display: none; position: relative;">
                                                                        <asp:GridView runat="server" ID="gvLiaLedger"  CssClass="myGridStyle"
                                                                         Width="82%" GridLines="Both"
                                                                            AutoGenerateColumns="false"  ShowFooter="true" >
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="DesignNo"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews('dv<%# Eval("ProcessType") %>', 'imdiv<%# Eval("ProcessType") %>');"
                                                                        style="text-decoration: none;">
                                                                       
                                                                    </a>
                                                                    <%# Eval("ProcessType")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                                
                                                                                <asp:BoundField HeaderText="Lot No" DataField="lotno" />
                                                                                <asp:BoundField HeaderText="Send Qty" DataField="SendFQty" DataFormatString='{0:f}'/>
                                                                                <asp:BoundField HeaderText="Received Qty" DataField="ReceivedFQty" DataFormatString='{0:f}' />
                                                                               
                                                                                  <asp:BoundField HeaderText="Remain Qty" DataField="Remainqty" DataFormatString='{0:f}' />
                                                                                  <asp:BoundField HeaderText="Bundle No" DataField="bundleno" />
                                                                                  
                                                                            </Columns>
                                                                             <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Unit Name" DataField="unitname" />
                                            <asp:BoundField HeaderText="Employee Name" DataField="name" />
                                            <asp:BoundField HeaderText="Date" DataField="date" DataFormatString='{0:d}' />
                                            <asp:BoundField HeaderText="Total Quantity" DataField="TotalQty" Visible="false" DataFormatString='{0:f}' />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="Total Qty"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews1('dv1<%# Eval("TotalQty","{0:f0}") %>', 'imdiv1<%# Eval("TotalQty","{0:f0}") %>');"
                                                                        style="text-decoration: none;">
                                                                        <img id="imdiv1<%# Eval("TotalQty","{0:f0}") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                                    </a>
                                                                    <%# Eval("TotalQty", "{0:f0}")%>
                                                                    <div id="dv1<%# Eval("TotalQty","{0:f0}") %>" style="display: none; position: relative;">
                                                                        <asp:GridView runat="server" ID="gvLiaLedger1"  CssClass="myGridStyle"
                                                                         Width="82%" GridLines="Both" OnRowDataBound="gvRowdatabound"
                                                                            AutoGenerateColumns="false"  ShowFooter="true" >
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="DesignNo"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews1('dv<%# Eval("ProcessType") %>', 'imdiv<%# Eval("ProcessType") %>');"
                                                                        style="text-decoration: none;">
                                                                       
                                                                    </a>
                                                                    <%# Eval("ProcessType")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                                
                                                                                <asp:BoundField HeaderText="Lot No" DataField="lotno" />
                                                                                <asp:BoundField HeaderText="Total Qty" DataField="SendFQty" DataFormatString='{0:f}'/>
                                                                                <asp:BoundField HeaderText="Received Qty" DataField="ReceivedFQty" DataFormatString='{0:f}' />
                                                                               
                                                                                  <asp:BoundField HeaderText="Remain Qty" DataField="Remainqty" DataFormatString='{0:f}' />
                                                                                  <asp:BoundField HeaderText="Bundle No" DataField="bundleno" />
                                                                                  <asp:BoundField HeaderText="Entry Date" DataField="senddate" />
                                                                                  
                                                                            </Columns>
                                                                             <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Received Quantity" DataField="pending" Visible="false" DataFormatString='{0:f}' />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="Received Qty"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews2('dv2<%# Eval("pending") %>', 'imdiv2<%# Eval("pending") %>');"
                                                                        style="text-decoration: none;">
                                                                        <img id="imdiv2<%# Eval("pending","{0:n}") %>" alt="Show" border="0" src="../images/plus.gif" />
                                                                    </a>
                                                                    <%# Eval("pending")%>
                                                                    <div id="dv2<%# Eval("pending") %>" style="display: none; position: relative;">
                                                                        <asp:GridView runat="server" ID="gvLiaLedger2"  CssClass="myGridStyle"
                                                                         Width="82%" GridLines="Both"
                                                                            AutoGenerateColumns="false"  ShowFooter="true" >
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" HeaderText="DesignNo"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="javascript:switchViews2('dv<%# Eval("ProcessType") %>', 'imdiv<%# Eval("ProcessType") %>');"
                                                                        style="text-decoration: none;">
                                                                       
                                                                    </a>
                                                                    <%# Eval("ProcessType")%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                                
                                                                                <asp:BoundField HeaderText="Lot No" DataField="lotno" />
                                                                                <asp:BoundField HeaderText="Total Qty" DataField="SendFQty" DataFormatString='{0:f}'/>
                                                                                <asp:BoundField HeaderText="Received Qty" DataField="ReceivedFQty" DataFormatString='{0:f}' />
                                                                               
                                                                                  <asp:BoundField HeaderText="Remain Qty" Visible="false" DataField="Remainqty" DataFormatString='{0:f}' />
                                                                                  <asp:BoundField HeaderText="Bundle No" DataField="bundleno" />
                                                                                  <asp:BoundField HeaderText="Received  Date" DataField="ReceivedDate" />
                                                                                  
                                                                            </Columns>
                                                                             <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Remain Quantity" DataField="remain"  DataFormatString='{0:f0}'/>
                                           
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("multiid") %>'
                                                        CommandName="Editt">
                                                        <asp:Image ID="imged" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisableed" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnreceived" runat="server" CommandArgument='<%#Eval("multiid") %>'
                                                        CommandName="Received">
                                                        <asp:Image ID="imgrec" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisablerec" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("multiid") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("multiid") %>'
                                                        CommandName="printt">
                                                        <asp:Image ID="imgep" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <%--<asp:ImageButton ID="imgdisableep" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Print" />--%>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("multiid") %>'
                                                        CommandName="delete" OnClientClick="alertMessage()">
                                                        <asp:Image ID="dlt" runat="server" ImageAlign="Middle" ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
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
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
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
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </div>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Customer List</div>
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
