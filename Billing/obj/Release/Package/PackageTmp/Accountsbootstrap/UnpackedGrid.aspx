﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnpackedGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.UnpackedGrid" %>
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
    <title>Unpacked Product</title>
    
   <!-- Bootstrap Core CSS -->
   <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>
    <script src="../Scripts/jquerynew-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
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
<%--<script language="javascript" type="text/javascript">
    $(document).ready(function ($) {

        $('#<%=gvcust.ClientID%>').Scrollable({

        });

    });
</script>--%>

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
             <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
   
   <form runat="server" id="form1">
   <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
<div class="row">
<asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>
                <div class="col-lg-12" style="margin-top:6px">
                    <h1  class="page-header" style="text-align:center;color:#fe0002;">Unpacked Product</h1>
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
                            <div id="Div1" runat="server" visible="false" class="col-lg-16">
                                
			                                
				                                <asp:Label runat="server" ID="lblSelectedValue"></asp:Label>
			                                     <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                 Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"   Text="." 
                                     Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                            <asp:DropDownList CssClass="form-control" ID="ddlfilter" Width="150px" style="margin-top:-20px" runat="server">
                                            <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                             <asp:ListItem Text="CuttingID" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="LotNo" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="PartyName" Value="3"></asp:ListItem>
                                            
                                                </asp:DropDownList>
                                                </div>
                                                     <div class="col-lg-16">
                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" 
                                    ControlToValidate="txtsearch" ErrorMessage="Please enter your searching Data!" Text="."
                                    Style="color: White" />
                                                <asp:TextBox CssClass="form-control" onkeyup="Search_Gridview(this, 'gvcust')"  Enabled="true" ID="txtsearch" runat="server" style="margin-top:-20px" placeholder="Search Text" Width="350px" ></asp:TextBox>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,custom" ValidChars=" /-"
                                    TargetControlID="txtsearch" />
                                    </div>
                                         <div id="Div2" runat="server" visible="false" class="col-lg-17">
                                               <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>
                                                <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" ValidationGroup="val1" OnClick="Search_Click" Width="130px"   /> 
                                       </div>
                                        <div class="col-lg-2">
                                <div class="form-group ">
                                    <asp:DropDownList ID="drpbranch" OnSelectedIndexChanged="company_SelectedIndexChnaged" AutoPostBack="true" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                                            <div id="Div3" runat="server" visible="false" class="col-lg-17">
                                        <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset" OnClick="refresh_Click"  Width="130px"    /> 
                                        </div>
                                            <div class="col-lg-15">&nbsp;&nbsp;</div>
                                                <div class="col-lg-17">&nbsp;&nbsp;</div>
                                                    <div class="col-lg-17">  <asp:Button ID="btnadd1" runat="server" class="btn btn-danger" Text="Add New" Width="150px"  OnClick="Add1_Click" /><%--<asp:Button ID="Btnnew" runat="server" class="btn btn-danger" Text="Add New Cutting" Width="130px"  OnClick="Add1_Click" />--%>  </div>
                                            <div id="Div4" runat="server" visible="false" class="col-lg-17">
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New Job-Work" Width="150px"  OnClick="Add_Click" />  
                                        
                                        
                                              </div>
                                              </div>
                                              </div>
                                        
                              <div class="table-responsive" >
                                        
                                <table class="table table-bordered table-striped">
                                <tr>
                                <td>
                                <asp:GridView ID="gvcust" EmptyDataText="No records Found"   runat="server" 
                                        CssClass="myGridStyle" AllowPaging="true" PageSize="1000"  
                                        OnPageIndexChanging="Page_Change" AutoGenerateColumns="false" 
                                        onrowcommand="gvcust_RowCommand"   
                                        onrowdatabound="gvcust_RowDataBound" >
                             <HeaderStyle BackColor="#3366FF" />
                             <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                              <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
    <Columns>
     <asp:BoundField HeaderText="companyid" Visible="false" DataField="RawId"   /> 
      <asp:BoundField HeaderText="Company Lot"  DataField="companylotno" />
      <asp:BoundField HeaderText="Total Qty" DataField="totalshirt" />
        
      
    <asp:TemplateField HeaderText="Edit" Visible="false"  ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("companylotno") %>' CommandName="edit"><asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
       <asp:ImageButton ID="imgdisable" ImageUrl="~/images/edit.png" runat="server" Visible="false" Enabled="false" ToolTip="Not Allow To Edit" /> 
           <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("companylotno") %>' />
               
     </ItemTemplate>
    
     
     
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
           <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("companylotno") %>' CommandName="delete" OnClientClick="alertMessage()"><asp:Image ID="dlt" runat="server"  ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
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
      <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Admin Print" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("companylotno") %>'
                                                        CommandName="print">
                                                        <asp:Image ID="print" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Cutting Print">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint1" runat="server" CommandArgument='<%#Eval("companylotno") %>'
                                                        CommandName="custprint">
                                                        <asp:Image ID="print1" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Label Print">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnprint12" runat="server" CommandArgument='<%#Eval("companylotno") %>'
                                                        CommandName="labelprint">
                                                        <asp:Image ID="print12" runat="server" ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
   </Columns><FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
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
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

               </div>
                 <asp:panel class="popupConfirmation" id="DivDeleteConfirmation" 
	style="display: none" runat="server">
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
</asp:panel>                 
</form>
</body>

</html>
