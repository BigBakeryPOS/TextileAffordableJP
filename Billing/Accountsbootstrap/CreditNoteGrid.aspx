<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditNoteGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.CreditNoteGrid"%>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Credit Note </title>
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
        function alertMessage() {
            alert('Are You Sure, You want to delete This Customer!');
        }
    </script>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1">


   <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />

   <asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>
   <div class="row">
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" style="margin-top: 6px">
                            <div class="col-lg-2">
                                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                                    font-weight: bold">
                                    Credit Note
                                </h1>
                            </div>
               
                 <%--  <div class="form-group">--%>
                                <div> </div>
                                          <asp:Label runat="server" ID="Label1"></asp:Label>

                                          <div class="col-lg-2">
                                           <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                 Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"   Text="." 
                                     Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                     </div>

                                     <div class="col-lg-2">
                                            <asp:DropDownList CssClass="form-control" ID="ddlfilter" Visible="false" style="width:170px;margin-left: 110px;" runat="server">
                                            <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="TransNo" Value="1"></asp:ListItem>
                                             <asp:ListItem Text="Note NO" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Date" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Ledger Name" Value="4"></asp:ListItem>
                                            
                                                </asp:DropDownList>
                                                </div>

                                                <div class="col-lg-2">
                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" 
                                    ControlToValidate="txtsearch" ErrorMessage="Please enter your searching Data!" Text="."
                                    Style="color: White" />
                                    </div>
                                    <div class="col-lg-2">
                                     <asp:TextBox CssClass="form-control"  Enabled="true" ID="txtsearch1" runat="server" placeholder="Search Text"  style="width:170px;margin-top: -10px;margin-left: -600px;" onkeyup="Search_Gridview(this, 'CreditDebitGrid')" ></asp:TextBox></div>
                                                <asp:TextBox CssClass="form-control"  Enabled="true" ID="txtsearch" runat="server" placeholder="Search Text" Visible="false" style="width:170px;margin-top: -54px;margin-left: 290px;" ></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars="/"
                                    TargetControlID="txtsearch" />
                                          <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>

                                        <div class="col-lg-2">
                                        <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search"  ValidationGroup="val1"
                                              style="width:120px;margin-top: -59px;margin-left: 475px;" 
                                              onclick="btnsearch_Click" /> </div>
                                              <div class="col-lg-2">
                                        <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset"  
                                              style="width:120px;margin-top: -11px;margin-left: -600px;" 
                                              onclick="btnrefresh_Click"   /> </div>
                                              <div class="col-lg-2">
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New"  
                                              style="width:120px;margin-top: -34px;margin-left: 483px;" 
                                              onclick="btnadd_Click" />  
                                              </div>
                                        </div>
                </div>
                <!-- /.col-lg-12 -->
            </div>


          <div class="row">
                <div class="col-lg-12" style="margin-top:-22px">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">

                            <div class="table-responsive">
                                        
                                <table class="table table-bordered table-striped">
                                <tr><td> 
                               
                                       </td></tr>
                                <tr>
                                <td>

                                  <table id="Table1" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;color:Black;
                            text-transform: uppercase" width="100%">
                            <tr>
                                <td align="center" style="font-size: small;width:6%">
                                 DayBook_ID
                                </td>
                                <td align="center" style="font-size: small;width:5%">
                                Note_NO
                                </td>
                                <td align="center" style="font-size: small;width:12%">
                                 Date
                                </td>
                                <td align="center" style="font-size: small;width:20%">
                               LedgerName
                                </td>
                                <td align="center" style="font-size: small;width:22%">
                                 Amount         
                                </td>
                                 <td align="center" style="font-size: small;width:12%">
                                  Narration                 
                                </td>
                                 <td align="center" style="font-size: small;width:22%">
                                 Type             
                                </td>
                                     
                                         
                                                                        
                                                                
                            </tr>
                        </table>


                                       
                  
                             
                                <div style="overflow:auto; height:300px">
                                <asp:GridView ID="CreditDebitGrid"  runat="server" CssClass="myGridStyle1" AllowPaging="true" style="width:100%;margin-left:1px"
                                        PageSize="10"  OnPageIndexChanging="CreditDebitGrid_PageIndexChanging" EmptyDataText="No records Found" 
                                        AutoGenerateColumns="false" onrowcommand="CreditDebitGrid_RowCommand"  
                                        Width="85%" >
                             <HeaderStyle BackColor="#3366FF"  />
                             <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                              <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
    <Columns>
    <%--<asp:BoundField HeaderText="Customer ID" DataField="CustomerID" />--%>
                                     <asp:BoundField HeaderText="TransNo" DataField="DayBook_ID"    />
                                    <asp:BoundField HeaderText="Note NO" DataField="Note_NO"    />
                                    <asp:BoundField HeaderText="Date" DataField="Date"  DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="Ledger Name" DataField="LedgerName"    />
                                    <asp:BoundField HeaderText="Amount" DataField="Amount"  DataFormatString="{0:f}"   />
                                    <asp:BoundField HeaderText="Narration" DataField="Narration"    />
                                    <asp:BoundField HeaderText="Type" DataField="Type"    />
                                
                                            
     <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("DayBook_ID") %>' CommandName="edit"><asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
      
     </ItemTemplate>
    
     
     
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
           <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("DayBook_ID") %>' CommandName="delete" OnClientClick="alertMessage()"><asp:Image ID="dlt" runat="server"  ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
   
    <ajaxToolkit:modalpopupextender   
		id="lnkDelete_ModalPopupExtender" runat="server" 
		cancelcontrolid="ButtonDeleteCancel" okcontrolid="ButtonDeleleOkay" 
		targetcontrolid="btndelete"  popupcontrolid="DivDeleteConfirmation" 
		backgroundcssclass="ModalPopupBG">
        </ajaxToolkit:modalpopupextender>
        <ajaxToolkit:ConfirmButtonExtender id="lnkDelete_ConfirmButtonExtender" 
		runat="server" targetcontrolid="btndelete" enabled="True" 
		displaymodalpopupid="lnkDelete_ModalPopupExtender">
        </ajaxToolkit:ConfirmButtonExtender>
   </ItemTemplate>
     </asp:TemplateField>
   </Columns><FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
   <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
   </asp:GridView>  
   </div>
   </td></tr></table>                           
                                        
		            
										
                                    
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
                    </div></div>
            </div>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

               </div>
                 <asp:panel class="popupConfirmation" id="DivDeleteConfirmation" 
	style="display: none" runat="server">
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
               CreditDebit Note:</div>
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
</asp:panel>                 
</form>
</body>
</html>
