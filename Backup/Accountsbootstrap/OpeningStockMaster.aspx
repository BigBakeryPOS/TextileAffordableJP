<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpeningStockMaster.aspx.cs" Inherits="Billing.Accountsbootstrap.OpeningStockMaster" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">

<head id="Head1" runat="server">
   <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <title>Accessories Stock Entry </title>


    <style type="text/css">
		a img{border: none;}
		ol li{list-style: decimal outside;}
		div#container{width: 780px;margin: 0 auto;padding: 1em 0;}
		div.side-by-side{width: 100%;margin-bottom: 1em;}
		div.side-by-side > div{float: left;width: 50%;}
		div.side-by-side > div > em{margin-bottom: 10px;display: block;}
		.clearfix:after{content: "\0020";display: block;height: 0;clear: both;overflow: hidden;visibility: hidden;}
		
	    .style4
        {
            width: 5%;
        }
        .style6
        {
            width: 4%;
        }
        .style7
        {
            width: 8%;
        }
		
	</style>


      <!-- Start Styles. Move the 'style' tags and everything between them to between the 'head' tags -->



   <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
      <link href="../Styles/style1.css" rel="stylesheet"/>
        <!--<link href="../Styles/style1.css" rel="stylesheet"/>-->

    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

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
            alert('Are You Sure, You want to delete Opening Stock Entry!');
        }
    </script>
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
</head>
<body>
<usc:Header ID="Header" runat="server" />
<asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
   
   
   <div class="row">
                
               
            </div>


          <div class="row">
          <div class="col-lg-12"  style="margin-top:-5px">
                 <div class="col-lg-2">
                    <h2 class="page-header" style="text-align:left;color:Red">Accessories Opening Stock Entry</h2>
                    </div>
                     
                     
                     <form id="form1" runat="server">
                                  <asp:ValidationSummary runat="server" HeaderText="Validation Messages"  ValidationGroup="val1" ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                   <div class="row">
                                <div class="col-lg-12">
                                  <div class="col-lg-2">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                 Style="color:White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"   Text="." 
                                     Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                     </div>

                                      <div class="col-lg-2">
                                 <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" 
                                    ControlToValidate="txtsearch" ErrorMessage="Please enter your searching Data!" Text="."
                                    Style="color:White" />
                                    </div>
                                <div class="col-lg-2">
                                <asp:DropDownList CssClass="form-control" Width="150px" ID="ddlfilter" style="margin-top:-80px;margin-left:-200px" runat="server">
                                           <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                             <asp:ListItem Text="Category" Value="1"></asp:ListItem>
                                              <asp:ListItem Text="Product Code" Value="2"></asp:ListItem>
                                                  <asp:ListItem Text="Product" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                                 <asp:Label ID="lblerror1" runat="server" style="color:Red"></asp:Label>
                                                </div>
                                                    <div class="col-lg-2">
                                                <asp:TextBox ID="txtsearch" Width="150px" style="margin-top:-80px;margin-left:-230px" runat="server" placeholder="Search Text" CssClass="form-control" ></asp:TextBox>
                                                </div>

                                                <div class="col-lg-2"></div>
                                    <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>

                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -.,"  TargetControlID="txtsearch" />
                                    </div>
                                    </div>
                              
                               
                                

                                 
                                     <div class="col-lg-2">
                                    <asp:Button ID="btnsearch" runat="server" Width="130px" style="margin-top:-95px;margin-left:645px" class="btn btn-success" ValidationGroup="val1"
                                             Text="Search" onclick="btnsearch_Click"     /> 
                                    </div>
                                      <div class="col-lg-2">
                                    <asp:Button ID="btnreset" runat="server" Width="130px" style="margin-top:-95px;margin-left:589px" class="btn btn-primary" 
                                              Text="Reset" onclick="btnreset_Click"  /> 
                                    </div>
                                      <div class="col-lg-2">
                                    <asp:Button ID="btnadd" runat="server" Width="130px" style="margin-top:-95px;margin-left:530px" class="btn btn-danger " 
                                              Text="Add New" onclick="btnadd_Click"  />
                                    </div>
                                    </div>
                                     </div>




                </div>
                <div class="col-lg-12" style="margin-top:-19px">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                          
                                
                                                
 
                                  <br />
                                    
                                     <div class="row">
                                     
                                    <div class="table-responsive" >
                              <table class="table table-bordered table-striped">
                                <tr>
                                <td>


                                <table id="Table1" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;color:Black;
                            text-transform: uppercase" width="100%">
                            <tr>
                                <td align="center" style="font-size: small;width:10%" >
                                   StockDate
                                </td>
                                <td align="center" style="font-size: small;width:18%" >
                               category
                                </td>
                                <td align="center" style="font-size: small;width:35%">
                                   Item Name
                                </td>
                                <td align="center" style="font-size: small;width:10%" >
                                   Item Code
                                </td>
                               
                                <td align="center" style="font-size: small;;width:10%" >
                                   Nos                          
                                </td>
                                 <td align="center" style="font-size: small;;width:9%">
                                   Edit                          
                                </td>
                                 <td align="center" style="font-size: small;;width:10%">
                                   Delete                          
                                </td>
                                                                        
                                                                
                            </tr>
                        </table>
                                      <div id="Div1" runat="server" style="overflow:auto; height:430px">
                                     <asp:GridView ID="gridOpening" runat="server" CssClass="myGridStyle1"  PageSize="10" width="100%" AllowPaging="true"   onpageindexchanging="openGrid_PageIndexChanging"   EmptyDataText="No records Found" 
                                         AutoGenerateColumns="false" onrowcommand="gridOpening_RowCommand">
                                         <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                     <Columns>
                                      <asp:BoundField DataField="OpenStockID" ItemStyle-Width="5%"  Visible="false" />
                                     <asp:BoundField DataField="StockDate" ItemStyle-Width="3%" dataformatstring="{0:dd/MM/yyyy}" />
                              <%--        <asp:BoundField DataField="BrandName" HeaderText="Brand Name" />--%>
                                       <asp:BoundField DataField="category" ItemStyle-Width="15%"  />
                                          <asp:BoundField DataField="Serial_No" ItemStyle-Width="15%"  />
                                        <asp:BoundField DataField="Definition" ItemStyle-Width="5%"  />
                                         <asp:BoundField DataField="Nos" ItemStyle-Width="5%"  />
                                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                         <ItemTemplate>
                                         <asp:LinkButton ID="btnedit"    CommandArgument='<%#Eval("OpenStockID") %>' CommandName="Edit" runat="server"> <asp:Image ID="imdedit"  ImageUrl="~/images/pen-checkbox-128.png" runat="server" /></asp:LinkButton>
                                            </ItemTemplate>
                                            
     </asp:TemplateField>
          <asp:TemplateField ItemStyle-Width="5%" HeaderText="" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
    
     <asp:LinkButton ID="btndel"   CommandArgument='<%#Eval("OpenStockID") %>' CommandName="Del" OnClientClick="alertMessage()" runat="server"> <asp:Image ID="Image1"  ImageUrl="~/images/DeleteIcon_btn.png" runat="server" /></asp:LinkButton>
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
   </ItemTemplate>  
     </asp:TemplateField> 
                                    </Columns>
                                  </asp:GridView>
                                  </div>
                                  </td>
                                  </tr>
                                  </table>
                                    </div>
                                     </div>
                                   <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
         </form>
               </div>
                 <asp:panel class="popupConfirmation" id="DivDeleteConfirmation" 
	style="display:none"  runat="server">
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft" style="color:Red" >
                Opening Stock Entry:</div>
            <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
            </div>
        </div>
        <div class="popup_Body" >
            <p>
               Are You Sure, You want to delete Opening Stock Entry?
            </p>
        </div>
        <div class="popup_Buttons" align="center">
            <input id="ButtonDeleleOkay" type="button" value="Yes" />
            <input id="ButtonDeleteCancel" type="button" value="No" />
        </div>
    </div>
</asp:panel>     
                                     
                                
                                </div>
                            </div>
                        </div>
                   
   </body>
  </html>