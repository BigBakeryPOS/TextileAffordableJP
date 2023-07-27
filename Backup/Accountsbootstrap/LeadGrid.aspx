<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeadGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.LeadGrid" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Affordable Export Lead Generator Details</title>
    <style type="text/css">
        .button-success, .button-error, .button-warning, .button-secondary
        {
            color: white;
            border-radius: 5px;
            text-shadow: 0 7px 5px rgba(0, 0, 0, 0.2);
        }
        
        .button-success1
        {
            background: rgb(28, 184, 65); /* this is a green */
        }
        
        .button-error
        {
            background: rgb(202, 60, 60); /* this is a maroon */
        }
        
        .button-warning
        {
            background: rgb(223, 117, 20); /* this is an orange */
        }
        
        .button-secondary
        {
            background: rgb(66, 184, 221); /* this is a light blue */
        }
        
        .index1
        {
            text-align: center;
            font-size: 28px;
            font-weight: bold;
            background-color: orange;
            padding-top: 10px;
            padding-bottom: 10px;
            margin-left: 525px;
            margin-right: 525px;
            font-family: Californian FB;
        }
        .buttonhrm
        {
            margin-top: 25px;
        }
        
         body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 900px;
            text-align: center;
            border: 3px solid #0DA9D0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 40px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            padding: 5px;
        }
        .modalPopup .footer
        {
            padding: 3px;
        }
        .modalPopup .button
        {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
        .modalPopup td
        {
            text-align: left;
        }
        
        .pad
        {
            padding-top: 50px;
        }
    </style>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
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


            //            var strData = strKey.value.toLowerCase().split(" ");
            var strGV = '<%=gv_Employee.ClientID%>';

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
    <link rel="stylesheet" href="../Css/bootstrap.min.css" />
    <%--<script type="text/javascript" src="../jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jquery/bootstrap.min.js"></script>--%>
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link href="../Styles/chosen.css" rel="Stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <usc:Header ID="Header" runat="server" />
    <form id="Form2" runat="server">
   
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <div>
            <div class="row">
              <div class="col-lg-12" style="margin-top: 6px">
               
               <h1 class="page-header" style="text-align:center;color:#fe0002;font-size:20px; font-weight:bold">
                    Lead Entry Grid</h1>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="panel">
                    <div class="panel-body">
                        <div class="row"><div class="col-lg-1"></div>
                           <div class="col-lg-10" align="center">
                           <div class="panel panel-default">
                    <div class="panel-body">
                             <div class="col-lg-3">
                                <asp:TextBox CssClass="form-control" ID="txtsearch" onkeyup="Search_Gridview(this, 'gv_Employee')"   placeholder="Search Text DATE:yyyy-MM-dd" MaxLength="50" style="width:100%" runat="server"></asp:TextBox>
                            </div>
                             <div class="col-lg-2">
                                <asp:DropDownList ID="ddlfilter" runat="server" CssClass="form-control" >
                                <asp:ListItem Text="Company Name" Value="l.CompanyName" ></asp:ListItem>
                                <asp:ListItem Text="Customer Name" Value="l.ContactName1" ></asp:ListItem>
                                <asp:ListItem Text="Contact Number 1" Value="l.PrimaryContact" ></asp:ListItem>
                                <asp:ListItem Text="Next Appointment" Value="l.NextAppointment" ></asp:ListItem>
                                <asp:ListItem Text="Is Generate" Value="l.Isgenerate" ></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                             <div class="col-lg-1">
                                <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-success" OnClick="Search_click"  />
                            </div>

                            <div id="Div2" runat="server" visible="true" class="col-lg-1">
                         <asp:Button ID="btnresret" runat="server" Visible="true" CssClass="btn btn-warning" PostBackUrl="../Accountsbootstrap/Leadgrid.aspx" Text="Reset" />
                            </div>

                            <div id="Div1" runat="server" visible="true" class="col-lg-1">
                         <asp:Button ID="btnAdd" runat="server" Visible="true" CssClass="btn btn-success" PostBackUrl="../Accountsbootstrap/LeadMaster.aspx?name=add" Text="Add New"/>
                            </div>
                               <div id="Div3" runat="server" visible="false" class="col-lg-3">
                         <asp:Button ID="btnexcel" runat="server" Visible="true" CssClass="btn btn-success"  Text="Export to Excel"  OnClick="btnexcel_click" Width="150px"/>
                            </div>

                            <div id="Div4" runat="server" visible="true" class="col-lg-2">
                            <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlChangeEvent_Status" CssClass="form-control" AutoPostBack="true">
                           
                            </asp:DropDownList>
                            </div>

                            </div>
                            </div>
                          
                          
                     </div><div class="col-lg-1"></div>
                      </div>
                   
    <div class="row">
                           <div class="col-lg-12" align="center">
                           <div class="panel panel-default">
                    <div class="panel-body">
                                    <asp:GridView ID="gv_Employee" EmptyDataText="Sorry!! No Records Found" 
                                        runat="server" Style="margin-left: 0px;" AutoGenerateColumns="false" 
                                        CssClass="myGridStyle"  onrowcommand="gv_Employee_RowCommand" onrowediting="gv_Employee_RowEditing" 
                                      >
                                        <HeaderStyle BackColor="#3366FF" />
                                        <PagerSettings Mode="Numeric" />
                                        <Columns>
                                        <asp:TemplateField HeaderText = "Sl.No" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                         <asp:BoundField HeaderText="Company Name" DataField="CompanyName" />
                                            <asp:BoundField HeaderText="Customer Name" DataField="ContactName1" />
                                            <%--<asp:BoundField HeaderText="Customer Address" DataField="Address" />--%>
                                            <asp:BoundField HeaderText="Contact Number" DataField="PrimaryContact" />
                                            <%--<asp:BoundField HeaderText="Company Number" DataField="CompanyPhoneNo" />--%>
                                            <asp:BoundField HeaderText="Emailid" DataField="Emailid" />
                                            <asp:BoundField HeaderText="Lead date" DataField="NextAppointment" DataFormatString='{0:d}' />
                                            <asp:BoundField HeaderText="Lead Follow Up" DataField="AppointmentTime"  />
                                            <asp:BoundField HeaderText="Status Name" DataField="statusname" />
                                            <asp:BoundField HeaderText="Reference Type" DataField="referencename" />
                                            <asp:BoundField HeaderText="Is Lead Generate" DataField="isgenerate" />
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" CommandArgument='<%# Eval("Leadid") + "," + Eval("IsGenerate")%>' CommandName="Edit"
                                                        runat="server">
                                                        <asp:Image ID="imdedit" ImageUrl="~/images/edit.png" runat="server" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Appointment">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnnxtedit" CommandArgument='<%# Eval("Leadid") + "," + Eval("IsGenerate")%>' CommandName="NXT"
                                                        runat="server">
                                                        <asp:Image ID="imdnxtedit" Width="2pc" ImageAlign="Middle" style="margin-left:1.5pc" ImageUrl="~/images/nextapp.png" runat="server" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-VerticalAlign="Middle" HeaderText="View chat History">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit1"  CommandArgument='<%#Eval("Leadid") %>' CommandName="History"
                                                        runat="server">
                                                        <asp:Image ID="imdedit1" ImageAlign="Middle" ImageUrl="~/images/history_icon.png" runat="server" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Generate as Client">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btngenedit" CommandArgument='<%# Eval("Leadid") + "," + Eval("IsGenerate")%>'  CommandName="CLNT"
                                                        runat="server">
                                                        <asp:Image ID="imdgenedit" Width="4pc" ImageAlign="Middle"  ImageUrl="~/images/client.png" runat="server" /></asp:LinkButton>
                                                         <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                                        CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btngenedit"
                                                        PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                                    </ajaxToolkit:ModalPopupExtender>
                                                    <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                                        TargetControlID="btngenedit" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                                    </ajaxToolkit:ConfirmButtonExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btndel" CommandArgument='<%#Eval("Leadid") %>' CommandName="Del"
                                                        runat="server">
                                                        <asp:Image ID="Image1" ImageUrl="~/images/delete.png" runat="server" /></asp:LinkButton>
                                                   
                                                 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                     <asp:GridView ID="GridView2" EmptyDataText="Sorry!! No Records Found" 
                                        runat="server" Style="margin-left: 140px;" AutoGenerateColumns="false" 
                                        CssClass="myGridSty" onrowcommand="GridView2_RowCommand" onrowediting="GridView2_RowEditing" 
                                      >
                                        <HeaderStyle BackColor="#3366FF" />
                                        <PagerSettings Mode="Numeric" />
                                        <Columns>
                                        
                                       <asp:BoundField HeaderText="Company Name" DataField="CompanyName" />
                                            <asp:BoundField HeaderText="Customer Name" DataField="customername" />
                                            <asp:BoundField HeaderText="Customer address" DataField="customerAddress" />
                                            <asp:BoundField HeaderText="Contact Number" DataField="CompanyNumber" />
                                            <asp:BoundField HeaderText="Company Number" DataField="Customermobile" />
                                            <asp:BoundField HeaderText="Emailid" DataField="Emailid" />
                                            <asp:BoundField HeaderText="Employee Name" DataField="name" />   
                                            <asp:BoundField HeaderText="Lead Date" DataField="Appointdate" DataFormatString='{0:d}' />
                                            <asp:BoundField HeaderText="Lead FollowUp" DataField="AppointTime"  />
                                            <asp:BoundField HeaderText="status" DataField="sts" />
                                                <asp:TemplateField Visible="false" HeaderText="Comments">
                                                <ItemTemplate>
                                                <asp:Label ID="lblentryid" runat="server" Visible="false" Text='<%#Eval("Leadid") %>'></asp:Label>
                                                <asp:TextBox ID="txtcomments" runat="server" ></asp:TextBox>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField Visible="false" HeaderText="Status">
                                                <ItemTemplate>
                                                <asp:DropDownList ID="drpstatus" runat="server">
                                                <asp:ListItem Text="Following" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Reject" Value="3"></asp:ListItem></asp:DropDownList>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("Leadid") %>' CommandName="Edit"
                                                        runat="server">
                                                        <asp:Image ID="imdedit" ImageUrl="~/images/edit.png" runat="server" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-VerticalAlign="Middle" HeaderText="View chat History">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit1"  CommandArgument='<%#Eval("Leadid") %>' CommandName="History"
                                                        runat="server">
                                                        <asp:Image ID="imdedit1" ImageAlign="Middle" ImageUrl="~/images/history_icon.png" runat="server" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                        </Columns>
                                    </asp:GridView>
                             </div>
                              <asp:LinkButton Text="" ID="lnkFake" runat="server"></asp:LinkButton>
                                                        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                                                            TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                        <asp:Panel ID="pnlPopup" runat="server" ScrollBars="Auto" Height="600px" Width="900px"
                                                            CssClass="modalPopup" Style="display: none">
                                                            <div class="header">
                                                               Lead Follow up History - <asp:Label ID="lblleaddate" runat="server" ></asp:Label>
                                                            </div>
                                                            <div class="body">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText="No Updates Avaliable For this Item"
                                                                                AllowPaging="True" PageSize="50000" CssClass="myGridSty">
                                                                                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
                                                                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                                                <Columns>
                                            
                                            <asp:BoundField HeaderText="Company Name" DataField="CompanyName" />
                                            <asp:BoundField HeaderText="Customer Name" DataField="ContactName1" />
                                            <asp:BoundField HeaderText="Result Of Meet" DataField="ResultOfmeet" />
                                            <asp:BoundField HeaderText="Lead Followup Date" DataField="Nextappointment" DataFormatString='{0:d}' />
                                            <asp:BoundField HeaderText="Remarks/Comments" ItemStyle-BackColor="White" DataField="comments" />
                                             <asp:BoundField HeaderText="Current Status" DataField="statusname" />
                                             <asp:BoundField HeaderText="Last Updated Entry" DataField="entryDate" DataFormatString='{0:d}' />
                                             <asp:BoundField HeaderText="Entry Type" DataField="EntryType"  />
                                             
                                                                                </Columns>
                                                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                                                <PagerStyle CssClass="pgr"></PagerStyle>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="footer" align="right">
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                                                            </div>
                                                        </asp:Panel></div></div>
                             <div class="col-lg-1"></div>
                             </div>
                    </div>
                    <asp:Panel CssClass="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
                        runat="server">
                        <div class="popup_Container">
                            <div class="popup_Titlebar" id="PopupHeader">
                                <div class="TitlebarLeft">
                                   Blaack Forest CRM</div>
                                <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                                </div>
                            </div>
                            <div class="popup_Body">
                                <p>
                                    Are you sure want to Convert as Client?
                                </p>
                                <label>Select Status</label>
                                <asp:DropDownList ID="drpstus" runat="server" CssClass="form-control" ></asp:DropDownList>
                            </div>
                            <div class="popup_Buttons">
                                <input id="ButtonDeleleOkay" type="button" value="Yes" />
                                <input id="ButtonDeleteCancel" type="button" value="No" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    </form>
    <asp:Label ID="lblempid" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblempname" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbllogintime" runat="server"></asp:Label>
    <asp:Label ID="lbllogtime" runat="server"></asp:Label>
    <asp:Label ID="id" runat="server"></asp:Label>
</body>
</html>
