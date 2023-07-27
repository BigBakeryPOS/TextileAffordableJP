<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KajaButtonGrid.aspx.cs"
    Inherits="Billing.Accountsbootstrap.KajaButtonGrid" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head runat="server">
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
    <title>KajaButton Issue / Receive</title>
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
      <script type="text/javascript">
          function Denomination() {


              var gridData = document.getElementById('gvcust');



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
            <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                font-weight: bold">
                KajaButton Issue / Receive</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <div class="form-group ">
                                  <asp:Label ID="Label3" runat="server" Style="color: Red"></asp:Label><br />
                                    <asp:DropDownList ID="drpbranch" OnSelectedIndexChanged="company_SelectedIndexChnaged"
                                        AutoPostBack="true" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="*" Style="color: White" />
                                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvcust')"
                                    ID="txtsearch" runat="server"  placeholder="Enter Text to Search"
                                    Width="200px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -.@"
                                    TargetControlID="txtsearch" />
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                        OnTextChanged="Date_OnTextChanged" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                        PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="true" CssClass="form-control center-block"
                                        OnTextChanged="Date_OnTextChanged" Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate"
                                        PopupButtonID="txtToDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <asp:Label ID="Label6" runat="server" Style="color: Red"></asp:Label><br />
                                <asp:DropDownList CssClass="form-control" ID="ddljobworker" Width="150px" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddljobworker_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div  class="col-lg-2">
                              <asp:Label ID="Label2" runat="server" Style="color: Red"></asp:Label><br />
                                <asp:DropDownList ID="drppending" OnSelectedIndexChanged="pending_changed" AutoPostBack="true"
                                    CssClass="form-control" runat="server">
                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Selected="True" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="Completed" Value="Y"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1">
                              <asp:Label ID="Label4" runat="server" Style="color: Red"></asp:Label><br />
                                <asp:Button ID="Button3" runat="server" class="btn btn-success" Text="Add New" PostBackUrl="~/Accountsbootstrap/KajaButton1.aspx"
                                    Width="100px" />
                            </div>
                             <div class="col-lg-1">
                                <asp:Label ID="lblPrint" runat="server" Visible="false">Print</asp:Label><br />
                                <asp:Button ID="btn" runat="server" Text="Print" Visible="true" CssClass="btn btn-danger"
                                    OnClientClick="Denomination()" Width="80px" />
                            </div>
                        </div>
                        <br />
                        <div class="col-lg-12" align="center">
                            <div class="col-lg-1">
                            </div>
                            <div id="Div1" runat="server" visible="false" class="col-lg-16">
                                <asp:Label runat="server" ID="Label1"></asp:Label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                                    Text="*" Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                <asp:DropDownList CssClass="form-control" ID="ddlfilter" Width="150px" Style="margin-top: -20px"
                                    runat="server">
                                    <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Contact Name" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="MobileNo" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div runat="server" visible="false" class="col-lg-17">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsearch" runat="server" ValidationGroup="val1" class="btn btn-success"
                                    Text="Search" Width="130px" />
                            </div>
                            <div id="Div3" runat="server" visible="false" class="col-lg-17">
                                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" Width="130px" />
                            </div>
                            <div class="col-lg-2">
                                <asp:DropDownList ID="drpMultiemployee" OnSelectedIndexChanged="Employee_changed"
                                    Visible="false" AutoPostBack="true" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div id="Div2" runat="server" class="col-lg-17">
                                <asp:Button ID="btnexcel" Visible="false" runat="server" class="btn btn-info" Text="Export-To-Excel"
                                    Width="130px" Height="32px" />
                            </div>
                            <div id="Div4" runat="server" style="width: 22%" class="col-lg-18">
                                <asp:TextBox ID="Barcode" runat="server" OnTextChanged="Barcode_indexChanged" Visible="false"
                                    AutoPostBack="true" CssClass="form-control" placeholder="Scan Barcode Value"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvcust" runat="server" CssClass="myGridStyle" EmptyDataText="No records Found"
                                        AllowPaging="true" PageSize="100000" DataKeyNames="KajaButtonId" AutoGenerateColumns="false"
                                        OnRowCommand="gvcust_RowCommand" OnRowDataBound="gvRowdatabound" ShowFooter="true">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <%--<asp:BoundField HeaderText="Customer ID" DataField="CustomerID" Visible="false" />--%>
                                            <asp:BoundField DataField="KajaButtonId" Visible="false" />
                                            <asp:BoundField DataField="LotNo" Visible="false" HeaderText="LotNo" />
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="LotNo" />
                                            <asp:BoundField HeaderText=" Name" DataField="name" />
                                            <asp:BoundField HeaderText="Date" DataField="date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <%--<asp:BoundField HeaderText="Total Quantity" DataField="TotalQty" DataFormatString='{0:f}' />
                                            <asp:BoundField HeaderText="Sent Quantity" DataField="sendQty" DataFormatString='{0:f}' />
                                            <asp:BoundField HeaderText="TotalAmount" DataField="TotalAmount" DataFormatString='{0:f}' />--%>
                                            <asp:BoundField HeaderText="Issue Qty" DataField="TotalIssue" />
                                            <asp:BoundField HeaderText="Receive Qty" DataField="TotalReceive" />
                                            <asp:BoundField HeaderText="Damage Qty" DataField="TotalDamage" />
                                            <asp:BoundField HeaderText="PaidAmount" DataField="PaidAmount" DataFormatString='{0:f}' />
                                             <asp:BoundField HeaderText="Dr.Amount" DataField="DebitAmount" DataFormatString='{0:f}' />
                                            <asp:BoundField HeaderText="Misc" DataField="Miscellaneous" DataFormatString='{0:f}' />
                                               <asp:BoundField HeaderText="Print" DataField="IsPrint" DataFormatString='{0:f}' />
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("KajaButtonId") %>'
                                                        CommandName="Editt">
                                                        <asp:Image ID="imged" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisableed" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Partial Send" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnpartial" runat="server" CommandArgument='<%#Eval("KajaButtonId") %>'
                                                        CommandName="partial">
                                                        <asp:Image ID="imgedpar" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisableedpar" ImageUrl="~/images/edit.png" runat="server"
                                                        Visible="false" Enabled="false" ToolTip="Not Allow To Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnreceived" runat="server" CommandArgument='<%#Eval("KajaButtonId") %>'
                                                        CommandName="Received">
                                                        <asp:Image ID="imgrec" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisablerec" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("KajaButtonId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issue Print" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("KajaButtonId") %>'
                                                        CommandName="printt">
                                                        <asp:Image ID="imgep" runat="server" ImageUrl="~/images/print (2).png" /></asp:LinkButton>
                                                    <%--<asp:ImageButton ID="imgdisableep" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Print" />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receive Print" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprintrec" runat="server" CommandArgument='<%#Eval("KajaButtonId") %>'
                                                        CommandName="printtrec">
                                                        <asp:Image ID="imgeprec" runat="server" ImageUrl="~/images/print (2).png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Received Print" ItemStyle-HorizontalAlign="Center"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint1" runat="server" CommandArgument='<%#Eval("KajaButtonId") %>'
                                                        CommandName="printt1">
                                                        <asp:Image ID="imgep1" runat="server" ImageUrl="~/images/print (2).png" /></asp:LinkButton>
                                                    <%--<asp:ImageButton ID="imgdisableep" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Print" />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPayment" runat="server" CommandArgument='<%#Eval("KajaButtonId") %>'
                                                        CommandName="Payment">
                                                        <asp:Image ID="imgrec1" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisablerec1" ImageUrl="~/images/edit.png" runat="server"
                                                        Visible="false" Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID1" runat="server" Value='<%# Bind("KajaButtonId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("KajaButtonId") %>'
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
