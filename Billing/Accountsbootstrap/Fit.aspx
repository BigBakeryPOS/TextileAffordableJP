<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fit.aspx.cs" Inherits="Billing.Accountsbootstrap.Fit" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">

<head>
<meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Width Grid </title>
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
    
    <link href="../Styles/chosen.css" rel="Stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
      <link href="../Styles/style1.css" rel="stylesheet"/>
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    </head>

<body>
 <usc:Header ID="Header" runat="server" />
  <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
                     <div class="row">
                
               
            </div>
             <div class="col-lg-12" style="margin-top:6px">
                   <h1 class="page-header" style="text-align:center;color:#fe0002;font-size:16px; font-weight:bold">Fit Master</h1> 
                
                </div>
<form id="f1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
    <div >

      

        <div>
            <div class="row">
            <div class="col-lg-1"></div>
            <div runat="server" visible="false" class="col-lg-16">
                                   
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                 Style="color:white" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"   Text="*"
                                     Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                       <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" 
                                    ControlToValidate="txtsearch" ErrorMessage="Please enter your searching Data!" Text="*"
                                    Style="color: White" />
                                           <asp:DropDownList CssClass="form-control" ID="ddlfilter" style="margin-top:-20px" Width="150px" runat="server">
                                           <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Fit" Value="1"></asp:ListItem>
                                              <asp:ListItem Text="IsActive" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                          
                                                </div>
                                      
                                   
                                                         <div class="col-lg-16">
                                            <asp:TextBox CssClass="form-control" ID="txtsearch" runat="server" onkeyup="Search_Gridview(this, 'gv')"   placeholder=" Enter Search Text" MaxLength="50" style="width:150px"></asp:TextBox>
                                            
                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -"  TargetControlID="txtsearch" />
                                            <%--<asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label><br />--%>
                                         </div>
                                  <div runat="server" visible="false" class="col-lg-17">
                                         <asp:Button ID="btnsearch" runat="server"  class="btn btn-success" Text="Search" Width="130px" OnClick="search" />
                                   
                                        </div>

                                         <div runat="server" visible="false" class="col-lg-17">
                                         <asp:Button ID="btnresret" runat="server"  class="btn btn-primary" Text="Reset" Width="130px" OnClick="reset"   />
                                    
                                        </div>
              
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            
            <!-- /.row -->
            <div class="row">
             <div class="col-lg-7">
                 <div style="height:392px; overflow:auto" class="table-responsive" >
                              <table class="table table-bordered table-striped">
                                <tr>
                                <td>
                                <asp:GridView ID="gv" runat="server" 
                                        DataKeyNames="FitID" OnSelectedIndexChanged="gv_selectedindex" OnRowCommand="edit" EmptyDataText="Oops! No Activity Performed." 
                             AllowPaging="true" PageSize="100" OnPageIndexChanging="Page_Change"  HeaderStyle-BackColor="#e0e0e0" 
                                        AutoGenerateColumns="false" CssClass="myGridStyle"  AllowSorting="true"
                                          >
                                 <HeaderStyle BackColor="#3366FF" />
                                 <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                 <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
                                
                                <Columns>
                                
                                    <asp:BoundField HeaderText="FitID" DataField="FitID" Visible="false" />
                                    <asp:BoundField HeaderText="Fit" DataField="Fit"  SortExpression="Category" HeaderStyle-ForeColor="Black"  />
                                        <asp:BoundField HeaderText="IsActive" DataField="IsActive"  SortExpression="IsActive" HeaderStyle-ForeColor="Black"   />

                                    

                               <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit"   CommandArgument='<%#Eval("FitID") %>' CommandName="EditRow" runat="server">
      <asp:Image ID="imdedit"  ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
     

      </asp:LinkButton>
    

                                <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
                                  <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("FitID") %>' />
                                 </ItemTemplate>
    
     
     
     </asp:TemplateField>
          <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Delete">
     <ItemTemplate>
    
     <asp:LinkButton ID="btndel"   CommandArgument='<%#Eval("FitID") %>' CommandName="Del" runat="server">
      <asp:Image ID="Image1"  ImageUrl="~/images/DeleteIcon_btn.png" runat="server" />
       <asp:ImageButton ID="imgdisable" ImageUrl="~/images/delete.png" runat="server" Visible="false" Enabled="false" ToolTip="Not Allow To Delete" />
      
      </asp:LinkButton>
     <ajaxToolkit:modalpopupextender   
		id="lnkDelete_ModalPopupExtender" runat="server" 
		cancelcontrolid="ButtonDeleteCancel" okcontrolid="ButtonDeleleOkay" 
		targetcontrolid="btndel"  popupcontrolid="DivDeleteConfirmation" 
		backgroundcssclass="ModalPopupBG">
        </ajaxToolkit:modalpopupextender>
        <ajaxToolkit:ConfirmButtonExtender id="lnkDelete_ConfirmButtonExtender" 
		runat="server" targetcontrolid="btndel" enabled="True" 
		displaymodalpopupid="lnkDelete_ModalPopupExtender">
        </ajaxToolkit:ConfirmButtonExtender>
                                <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
                                 </ItemTemplate>
    
     
     
     </asp:TemplateField> 
    </Columns><FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
   <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                </asp:GridView>
                               </td></tr></table>
                                </div>
                                </div>
                <!-- /.col-lg-8 -->
                <div class="col-lg-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-briefcase"></i> Add Fit
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="list-group">
                            <asp:TextBox ID="txtid" Visible="false" runat="server" ></asp:TextBox>
                          <%--  <label>Select Contact-Type</label>--%>
                           
                            <br />
                            <label>Fit</label>
                                <asp:TextBox placeholder="Enter Width" ID="txtFit" runat="server" CssClass="form-control"></asp:TextBox>
                               
                                  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendername" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom,Numbers" 
ValidChars=" ./\!@#$%^&*,"  TargetControlID="txtFit" />
                                  <br />
                                  <label>Is-Active</label>
                                <asp:DropDownList ID="ddlIsActive" runat="server" class="form-control">
                                    <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                               
                                 <%--  <asp:TextBox placeholder="Enter Address"  TextMode="multiline" Height="100px" ID="txtaddress" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderadd" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Custom,Numbers" 
ValidChars=" /,.\-#"  TargetControlID="txtaddress" />
 <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator111"
                                        ControlToValidate="txtaddress" ErrorMessage="Please Enter Address!"
                                        Style="color: Red" />--%>
                                       
                                       

                            </div>
                            <!-- /.list-group -->
                            <div>
                            <asp:Button ID="btnSubmit" style="width:75px;margin-left: 50px;" runat="server"  class="btn btn-default btn-block" 
                                Text="Save" onclick="btnSubmit_Click" />
                                <asp:Button ID="btnclaear" style="width:75px;margin-top: -34px;margin-left: 150px;" runat="server" class="btn btn-default btn-block" 
                                Text="Cancel" onclick="btncancel_Click" />
                                </div>
                           
                        </div>
                      
                        <!-- /.panel-body -->
                    </div>
                  <div  role="alert"><asp:Label ID="lblSuccess" runat="server" class="alert alert-success" Text="Well Done! You successfully Inserted." Visible="false"></asp:Label>
                   <asp:Label ID="lblFailure" runat="server" Text="Oops! Contact Already Exists." class="alert alert-danger" Visible="false"></asp:Label>
                    <asp:Label ID="lblWarning" runat="server" Text="Whoo!Did You Miss Something?" class="alert alert-warning" Visible="false"></asp:Label></div>
                </div>
                <!-- /.col-lg-4 -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /#page-wrapper -->

    </div>
    <!-- /#wrapper -->

   
    </form>
        <asp:panel class="popupConfirmation" id="DivDeleteConfirmation" 
	style="display: none" runat="server">
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Category List</div>
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
</body>

</html>

