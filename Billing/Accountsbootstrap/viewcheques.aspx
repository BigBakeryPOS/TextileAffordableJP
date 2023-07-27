<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewcheques.aspx.cs" Inherits="Billing.Accountsbootstrap.viewcheques" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">

<head>
<style type="text/css">
		a img{border: none;}
		ol li{list-style: decimal outside;}
		div#container{width: 780px;margin: 0 auto;padding: 1em 0;}
		div.side-by-side{width: 100%;margin-bottom: 1em;}
		div.side-by-side > div{float: left;width: 50%;}
		div.side-by-side > div > em{margin-bottom: 10px;display: block;}
		.clearfix:after{content: "\0020";display: block;height: 0;clear: both;overflow: hidden;visibility: hidden;}
		
	</style>
	
    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <title>Cheques Grid Master - bootsrap</title>
    
   <!-- Bootstrap Core CSS -->
   <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
     <link href="../Styles/style1.css" rel="stylesheet"/>

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
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
</head> 
<body>
<usc:Header ID="Header" runat="server" />
             <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
   
   <form runat="server" id="form1">
   <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
<div class="row">
<asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>
                <div class="col-lg-12" style="margin-top:-4px">
                   <div class="col-lg-2">
                    <h2  class="page-header" style="text-align:left;color:#fe0002;">Cheque Master</h2></div>

                     <div>
                                          
			                                
				                                <asp:Label runat="server" ID="lblSelectedValue"></asp:Label>
			                                     <div class="col-lg-2">
                                                 <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                 Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"   Text="." 
                                     Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator></div>
                                     <div class="col-lg-2">
                                            <asp:DropDownList CssClass="form-control" ID="ddlfilter"  style="width:170px;margin-left: -250px;" runat="server">
                                            <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Bank Name" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Cheque ID" Value="2"></asp:ListItem>
                                            
                                                </asp:DropDownList></div>
                                                <div class="col-lg-2">
                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" 
                                    ControlToValidate="txtsearch" ErrorMessage="Please enter your searching Data!" Text="."
                                    Style="color: White" /></div>
                                    <div class="col-lg-2">
                                                <asp:TextBox CssClass="form-control"  Enabled="true" ID="txtsearch" runat="server"  placeholder="Search Text" style="width:170px;margin-top: -1px;margin-left: -490px;" ></asp:TextBox>
                                                </div>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers" ValidChars=""
                                    TargetControlID="txtsearch" />
                                               <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>
                                               
                                              <div class="col-lg-2">
                                                <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" ValidationGroup="val1" OnClick="Search_Click"  style="width:120px;margin-left: -420px;"   />
                                                 </div>
                                        <div class="col-lg-2">
                                        <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"  style="width:120px;margin-top:-35px;margin-left:650px;"    /> 
                                        </div>
                                        <div class="col-lg-2">
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" style="width:120px;margin-top: -36px;margin-left: 574px;"   OnClick="Add_Click" /> 
                                         </div>
                                            
                                             

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
                <div class="col-lg-8" >
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                            
                                     <div class="table-responsive" >
                                        
                                <table  class="table table-bordered table-striped">
                                <tr>
                                <td>
                                
                                   
</td>
                                </tr>
                                        
                              
                                <tr>
                                <td>

                                <table id="Table1" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;color:Black;
                            text-transform: uppercase" width="100%">
                            <tr>
                                <td align="center" style="font-size: small;width:0%">
                                  ID
                                </td>
                                <td align="right" style="font-size: small;width:0%">
                              bankid
                                </td>
                                <td align="right" style="font-size: small;width:0%">
                                 BankName
                                </td>
                                <td align="right" style="font-size: small;width:0%">
                               FromCheque
                                </td>
                                <td align="center" style="font-size: small;width:0%">
                                ToCheque      
                                </td>
                                 <td align="left" style="font-size: small;width:0%">
                               Edit
                                </td>
                                <td align="center" style="font-size: small;width:1%">
                                Delete      
                                </td>
                                                                   
                                         
                                                                        
                                                                
                            </tr>
                        </table>



                                <div style="overflow:auto; height:300px">
                                <asp:GridView ID="gvcust" EmptyDataText="No records Found"   runat="server" 
                                        CssClass="myGridStyle1"  DataKeyNames="ID"
                                        OnPageIndexChanging="Page_Change" AutoGenerateColumns="false" 
                                        onrowcommand="gvcust_RowCommand"  Width="100%" 
                                        onrowdatabound="gvcust_RowDataBound" 
                                        onselectedindexchanged="gvcust_SelectedIndexChanged" >
                             <HeaderStyle BackColor="#3366FF" />
                             <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                              <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
    <Columns>
     <asp:BoundField ItemStyle-Width="2%" DataField="ID"   />
      <asp:BoundField ItemStyle-Width="4%" DataField="bankid" />
   
    <asp:BoundField ItemStyle-Width="8%" DataField="BankName" />
    <asp:BoundField ItemStyle-Width="4%" DataField="FromCheque" />
    <asp:BoundField ItemStyle-Width="4%" DataField="ToCheque" />
   
    <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="Select"><asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
       <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false" Enabled="false" ToolTip="Not Allow To Delete" /> 
           <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ID") %>' />
                <%--<asp:HiddenField ID="idcheque" runat="server" Value='<%# Bind("FromCheque") %>' />
                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("ToCheque") %>' />--%>
     </ItemTemplate>
    
     
     
     </asp:TemplateField>
     <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
           <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="delete" OnClientClick="alertMessage()"><asp:Image ID="dlt" runat="server"  ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
    <asp:ImageButton ID="imgdisable1" ImageUrl="~/images/delete.png" runat="server" Visible="false" Enabled="false" ToolTip="Not Allow To Delete" /> 
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

                  <div class="col-lg-4" style="margin-left:-14px">
                                       <div class="panel panel-default">
                                      
                                      <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val2"
                                ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />

                        <div class="panel-heading" style="background-color:#59d3b4;color:#333333;border-color:#06090c">
                            <i class="fa fa-briefcase"></i> <asp:label id="lblName" Text="Add Cheque" runat="server"></asp:label>
                        </div>
                             <div class="panel-body">
                                <div class="list-group">
                                  
                                        <div class="form-group" id="divcode" runat="server"  >
                                         <asp:TextBox  CssClass="form-control" ID="txtID" runat="server"  Enabled="false"></asp:TextBox>                                              
                                            
                                        </div>
                                        <div class="form-group ">
                                            <label>ID</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val2" Text="*" id="RequiredFieldValidator3" controltovalidate="TextBox3" errormessage="Please enter ID" style="color:Red" />
                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox3"  runat="server" ></asp:TextBox>
                                            
                                        </div>
                                        <div class="form-group ">
                                            <label>Bank Name</label>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val2"  Text="*" style="color:Red" InitialValue="0"
                                                ControlToValidate="ddlBank" ValueToCompare="Select Bank" Operator="NotEqual"  Type="String"   errormessage="Please select Bank name!"></asp:CompareValidator>
                                             
                                            <asp:DropDownList ID="ddlBank" runat="server" class="form-control">
                                                </asp:DropDownList>   
                                            
                                        </div>
                                        <div class="form-group ">
                                            <label>From Cheque No</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val2" Text="*" id="RequiredFieldValidator1" controltovalidate="TextBox1" errormessage="Please enter From Cheque No" style="color:Red" />
                                            <asp:TextBox CssClass="form-control" ID="TextBox1" MaxLength="6"  runat="server" ></asp:TextBox>
                                              <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Numbers" ValidChars=""
                                    TargetControlID="TextBox1" />
                                        </div>
                                        <div class="form-group " >
                                            <label>To Cheque No</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val2" Text="*" id="RequiredFieldValidator2" controltovalidate="TextBox2" errormessage="Please enter To Cheque No" style="color:Red" />
                                            <asp:TextBox CssClass="form-control" ID="TextBox2" MaxLength="6"  runat="server" ></asp:TextBox>
                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                    FilterType="Numbers" ValidChars=""
                                    TargetControlID="TextBox2" />
                                            
                                        </div>
                                         <asp:Label ID="Label1" runat="server" style="color:Red"></asp:Label>
										<asp:Button ID="btnSave" runat="server" class="btn btn-info" Text="Save"  
                                            ValidationGroup="val2" Style="width: 120px;" onclick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn btn-warning" Text="Cancel" 
                                            Style="width: 120px;" onclick="btnCancel_Click"  />
                                    
                                </div>
                                </div>
                                </div>
                                
                   </div>
                
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
                Cheque Master:</div>
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
