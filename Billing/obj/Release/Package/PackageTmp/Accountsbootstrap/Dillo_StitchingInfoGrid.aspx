<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dillo_StitchingInfoGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.Dillo_StitchingInfoGrid" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head runat="server" >
   <%-- <style type="text/css">
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
            width: 1000px;
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
    </style>--%>


    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Stiching Grid Details</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
        <link href="../css/responsive-tabs.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
    <script src="../js/jquery.responsiveTabs.min.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.0.min.js" type="text/javascript"></script>
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
            var isShift = false;
            var seperator = "/";
            function DateFormat(txt, keyCode) {
                if (keyCode == 16)
                    isShift = true;
                //Validate that its Numeric
                if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                    if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                        txt.value += seperator;
                    }
                    return true;
                }
                else {
                    return false;
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
<body style="background-color:#c6efce">
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
                Stitching Details</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row" style="background-color:#c6efce">
        <div class="col-lg-12" style="background-color:#c6efce">
            <div class="panel panel-default">
                <div class="panel-body" style="background-color:#c6efce">
                    <div class="row">
                        <div class="col-lg-12" align="left">
                        <div class="row">

                        <div class="col-lg-4">
                          
                             <asp:Label runat="server" ID="Label1"></asp:Label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"
                                    Text="*" Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                <asp:DropDownList CssClass="form-control" ID="ddlfilter" Width="150px" style="margin-top:-20px" runat="server" Visible="false">
                                    <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Contact Name" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="MobileNo" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                                </asp:DropDownList>

                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtsearch"
                                    ErrorMessage="Please enter your searching Data!" Text="*" Style="color: White" />--%>
                                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gvcust')"  ID="txtsearch" runat="server" style="margin-top:-20px"
                                    placeholder="Enter Text to Search" Width="250px"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -.@"
                                    TargetControlID="txtsearch" />
                        </div>

                        <div class="col-lg-4" align="left">
                      
                               <div class="form-group">
                       <div class="col-lg-4"  >
                                  <div class="col-lg-6"  >
                                    <asp:Label ID="startlabel" Text="Start date:" runat="server" style="margin-left: -200px;width: 100px;margin-bottom: -10px;" font-bold="true"></asp:Label>
                                      </div>
                                    <div class="col-lg-6"  >
                                    <asp:TextBox ID="txtstartdate" onkeydown="return DateFormat(this, event.keyCode)" 
                                    runat="server" CssClass="form-control" style="width: 150px;margin-left: -180px;margin-bottom: -10px;"></asp:TextBox>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtstartdate"
                                        PopupButtonID="txtstartdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                      </div>
                                   
                    </div>
                          <div class="col-lg-4" >
                          <div class="col-lg-6"  >
                             <asp:Label ID="endlabel" Text="End date:" runat="server" style=" margin-left: -100px;" font-bold="true"></asp:Label>
                              </div>
                             <div class="col-lg-6"  >
                                     <asp:TextBox ID="txtenddate" onkeydown="return DateFormat(this, event.keyCode)" 
                                     runat="server" CssClass="form-control" style="width: 150px;margin-left: -90px;"></asp:TextBox>
                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtenddate"
                                        PopupButtonID="txtenddate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                     </div>
                                                    
                          </div>

                            <div class="col-lg-4">
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsearch" runat="server" ValidationGroup="val1" class="btn btn-success"
                                    Text="Search" OnClick="Search_Click" Width="130px" />
                            </div>
                              </div>
          
                       </div>

                           

                    <div class="col-lg-4" align="right">

                           
                                <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"
                                    Width="130px" Visible="false" />
                            
                            
                                <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" OnClick="Add_Click"
                                    Width="130px" />
                            
                           
                                <asp:Button ID="Button3" runat="server" class="btn btn-success" Text="Bulk Addition"
                                    Width="130px" OnClick="btnFormat_Click" Visible="false" />
                           
                            
                                <asp:Button ID="btnexcel" runat="server" class="btn btn-info" Text="Export-To-Excel"
                                    Width="130px" Height="32px" OnClick="btnExcel_Click" />
                           
                            </div> 
                            </div> 
                        </div>
                    
                       <div>
                        <table >
                            <tr>
                                <td >
                              <%--  <div style="width: 100%;height: 300px;margin-left: -107px;">--%>
                             
                                    <asp:GridView ID="gvcust" runat="server" CssClass="myGridStyle" EmptyDataText="No records Found"
                                        AllowPaging="false" AutoGenerateColumns="false"
                                        OnRowCommand="gvcust_RowCommand" OnRowDataBound="gvcust_RowDataBound" 
                                        style="width:100%;margin-left: 10px">
                                        <HeaderStyle BackColor="#c6efce" />
                                        
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:BoundField HeaderText="LotDetail ID" DataField="LotDetailID" Visible="false" />
                                             <asp:BoundField HeaderText="Date" DataField="Processdate" DataFormatString='{0:d}' />
                                            <asp:BoundField HeaderText="Design No" Visible="false" DataField="DesignNo" />
                                            <asp:BoundField HeaderText="Lot No" DataField="LotNo" />
                                            <asp:BoundField HeaderText="Unit Name" DataField="UnitName" />
                                            <asp:BoundField HeaderText="Brand Name" DataField="BrandName" />
                                            <asp:BoundField HeaderText="Cutting Master" DataField="LedgerName" />
                                            <asp:BoundField HeaderText="Total Quantity" DataField="TotalQuantity" />
                                            <asp:BoundField HeaderText="Checking" DataField="chk" />
                                            <asp:BoundField HeaderText="Kaja Process" DataField="kaj" />
                                            
                                            <asp:BoundField HeaderText="Trimming Process" DataField="tri" />
                                            <asp:BoundField HeaderText="Ironing Process" DataField="irn" />
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("LotDetailID") %>'
                                                        CommandName="edit">
                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                                                    <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false"
                                                        Enabled="false" ToolTip="Not Allow To Delete" />
                                                    <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("LotDetailID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Process Status" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnstatus" runat="server" CommandArgument='<%#Eval("LotDetailID") %>'
                                                        CommandName="Status">
                                                        <asp:Image ID="img1" runat="server" ImageUrl="~/images/info_button.png" /></asp:LinkButton>
                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("Cutid") %>'
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
                                        
                                        <%-- <PagerStyle CssClass="GridviewScrollPager" /> --%>
                                  <%--      <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />--%>
                                    </asp:GridView>
                                  
<%--                                    </div>--%>
                                </td>
                            </tr>
                        </table>
                           <asp:LinkButton Text="" ID="lnkFake" runat="server"></asp:LinkButton>
                                    <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                                        TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="pnlPopup" runat="server" ScrollBars="Auto" Height="600px" Width="1000px"
                                        CssClass="modalPopup" Style="display: none">
                                        <div class="header">
                                            Process Status Details
                                        </div>
                                        <div class="body">
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                       <label>Details for Lot No :</label><asp:Label runat="server" ID="lblLot"></asp:Label>
                    <div class="table-responsive" id="divLot1" runat="server">

                            <div id="tabs" style="background-color: #D0D3D6;">
                                <ul>
                                 <li><a href="#tabs-6">OverAll Details</a></li>
                                  <li><a href="#tabs-1">Stiching Details</a></li>
                                         <li><a href="#tabs-2">Kaja Details</a></li>
                                               <li><a href="#tabs-3">Embroiding Details</a></li>
                                                     <li><a href="#tabs-4">Washing Details</a></li>
                                                           <li><a href="#tabs-5">Iron Details</a></li>

                                </ul>

                                <div class="row" id="tabs-6" style="background-color: White; padding-top: 30px">
                                    
        
           <div style="background-color: #D0D3D6;" >


                                     <h2 align="center"> <asp:Label ID="Label3" style="color:Blue;" runat="server">OverAll Details</asp:Label></h2>
                                          <div class=" form-group">
                                      <div class="table-responsive">
            
                                          <table style="width:100%">
                                                <tr>
                                                    <td style="width:100%">
                                                          <asp:Label ID="lblstiching" Text="Stiching"   ForeColor="Black" runat="server" BackColor="Yellow"></asp:Label>
                                                          ------------>
                                                          <asp:Label ID="lblKaja" Text="Kaja" runat="server" ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                          ------------>
                                                          <asp:Label ID="lblemb" Text="Embroiding" runat="server" ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                          ------------>
                                                          <asp:Label ID="lblwash" Text="Washing" runat="server" ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                          ------------>
                                                          <asp:Label ID="lbliron" Text="Iron and Packing" runat="server" ForeColor="Black" BackColor="Yellow"></asp:Label>
                                                          
                                                   
                                                      </td>
                                                </tr>
                                            </table>
                                            </div>
                                        </div>

                                        </div>
                                       
                                       
                                      </div>
                                
                                   <div class="row" id="tabs-1" style="background-color: White; padding-top: 30px">
                                    
        
           <div style="background-color: #D0D3D6;" >


                                     <h2 align="center"> <asp:Label ID="Label2" style="color:Blue;" runat="server">Stiching Details</asp:Label></h2>
                                          <div class=" form-group">
                                      <div class="table-responsive">
            
                                          <table style="width:85%">
                                                <tr>
                                                    <td>
                                                            <asp:GridView ID="Gridoverall" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                            ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                            ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                             <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                             <%--   <asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                               <%--  <asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                            
                                                            </Columns>
                                                            <HeaderStyle BackColor="#990000" />
                                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                NextPageText="Next" PreviousPageText="Previous" />
                                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                   
                                                      </td>
                                                </tr>
                                            </table>
                                            </div>
                                        </div>

                                        </div>
                                       
                                       
                                      </div>

                                          <div class="row" id="tabs-2" style="background-color: #D0D3D6; padding-top: 30px">
                                    <div style="background-color: #D0D3D6;" >


                                     <h2 align="center"> <asp:Label ID="Label7" style="color:Blue;" runat="server">Kaja Details</asp:Label></h2>
                                          <div class=" form-group">
                                      <div class="table-responsive">
                    <table style="width:85%">
                                            <tr>
                                                <td>
                            <asp:GridView ID="gridkaja" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                            ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                            ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                             <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                            
                                                            </Columns>
                                                            <HeaderStyle BackColor="#990000" />
                                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                NextPageText="Next" PreviousPageText="Previous" />
                                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                  
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                        </div>
                                        </div>
                                        </div>
                                          
                               
                                 <div class="row" id="tabs-3" style="background-color: #D0D3D6; padding-top: 30px">
                                    <div style="background-color: #D0D3D6;" >
                          
                                     <h2 align="center"> <asp:Label ID="Label8" style="color:Blue;" runat="server">Embroiding Details</asp:Label></h2>
                                    <div class=" form-group">
                                       <div class="table-responsive">
                                     <table style="width:85%" >
                                            <tr>
                                                <td>
                                                   <asp:GridView ID="gridemb" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                            ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                            ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                             <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="ReceivedQuantity"  HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                            
                                                            </Columns>
                                                            <HeaderStyle BackColor="#990000" />
                                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                NextPageText="Next" PreviousPageText="Previous" />
                                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                    </div>
                                    </div>
                                    </div>

                                      <div class="row" id="tabs-4" style="background-color: #D0D3D6; padding-top: 30px">
                                    <div style="background-color: #D0D3D6;" >
                            
                                 <h2 align="center"> <asp:Label ID="Label9" style="color:Blue;" runat="server">Washing Details</asp:Label></h2>
                                    <div class=" form-group">
                                       <div class="table-responsive">
                                   <table style="width:85%" >
                                            <tr>
                                                <td>
                                                       <asp:GridView ID="gridwash" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                            ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                            ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                             <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                            
                                                            </Columns>
                                                            <HeaderStyle BackColor="#990000" />
                                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                NextPageText="Next" PreviousPageText="Previous" />
                                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                    </div>
                                    </div>
                                    </div>

                            
                              <div class="row" id="tabs-5" style="background-color: #D0D3D6; padding-top: 30px">
                                    <div style="background-color: #D0D3D6;" >
                                      <h2 align="center"> <asp:Label ID="Label10" style="color:Blue;" runat="server">Iron and Packing Details</asp:Label></h2>
                                    <div class=" form-group">
                                            <div class="table-responsive">
                                     <table style="width:85%" >
                                            <tr>
                                                <td>
                                                       <asp:GridView ID="gridiron" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                                            ShowFooter="false" AutoGenerateColumns="false" EmptyDataText="No data found!"
                                                            ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                             <asp:BoundField DataField="checked" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />
                                                              <%--<asp:BoundField DataField="DesignNo" HeaderText="Design Number" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <%--<asp:BoundField DataField="Name" HeaderText="Employee/Jobworker Name" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <%--<asp:BoundField DataField="processtype" HeaderText="Process Type" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <%--<asp:BoundField DataField="date" HeaderText="Received Date" ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="PerRate" Visible="false" HeaderText="Rate Per Quantity" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                                <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="Pending" HeaderText="Pending" ItemStyle-HorizontalAlign="Center" />
                                                                <%--<asp:BoundField DataField="Rate" Visible="false" HeaderText="Total Rate" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />--%>
                                                            
                                                            </Columns>
                                                            <HeaderStyle BackColor="#990000" />
                                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                NextPageText="Next" PreviousPageText="Previous" />
                                                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                    </div>
                                    </div>
                                    </div>
                                    </div>
                                     <asp:HiddenField ID="selected_tab" runat="server" />
                       <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var selected_tab = 1;
    $(function () {
        var tabs = $("#tabs").tabs({
            select: function (e, i) {
                selected_tab = i.index;
            }
        });
        selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
        tabs.tabs('select', selected_tab);
        $("form").submit(function () {
            $("[id$=selected_tab]").val(selected_tab);
        });
    });
    
</script>
                                    </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="footer" align="right">
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                                        </div>
                                    </asp:Panel>
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

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script> 
<%--    <script src="../Scripts/GridScrollJquery.js" type="text/javascript"></script>
    <script src="../Scripts/GridScrollJquery1.js" type="text/javascript"></script>--%>
<%--<script type="text/javascript" src="../Scripts/gridviewScroll.min.js"></script> 
    <script src="../Scripts/gridviewscroll.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        gridviewScroll();
    });

//    $(function () {
//        $("#draggable").draggable();
//    });

    function gridviewScroll() {
        $('#<%=gvcust.ClientID%>').gridviewScroll({
            width:1000,
            height: 300
        });
    } 
</script>--%>
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

