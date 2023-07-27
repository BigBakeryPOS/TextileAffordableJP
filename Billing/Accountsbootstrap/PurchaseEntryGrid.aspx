<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseEntryGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.PurchaseEntryGrid" %>
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
    <title>Purchase Entry Grid </title>

   <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
        <link href="../Styles/style1.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
   

<style type="text/css">
body
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

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
     <script type="text/javascript">
         function alertMessage() {
             alert('Are You Sure, You want to delete Purchase!');
         }
    </script>
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    
</head> 
        
<body>

<usc:Header ID="Header" runat="server" />

             <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
                              <form id="Form1" runat="server">
                               <asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>
<div class="col-lg-2">
       <h2 class="page-header" style="text-align:left;color:#fe0002;margin-top:-9px" >Purchase</h2> </div>
         <div class="col-lg-2">
                                         <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                 Style="color:White" InitialValue="0" ControlToValidate="ddlbillno" ValueToCompare="0"   Text="." 
                                     Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                     </div>
                                     <div class="col-lg-2">
                                      <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" 
                                    ControlToValidate="txtsearch" ErrorMessage="Please enter your searching Data!" Text="."
                                    Style="color:White" />
                                    </div>
                                    <div class="col-lg-2">
                                            <asp:DropDownList ID="ddlbillno" CssClass="form-control"  style="width:170px;margin-top:-4px;margin-left:-400px;" 
                                                runat="server">
                                            <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Invoice No" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Bill No" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Vendor Name" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Invoice Date" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Bill Date" Value="5"></asp:ListItem>
                                                 <%-- <asp:ListItem Text="TransNo" Value="6"></asp:ListItem>--%>

                        <%--                    <asp:ListItem Text="" Value="4"></asp:ListItem>--%>

                                                </asp:DropDownList>
                                                </div>
                                                 <div class="col-lg-2">
                                                <asp:TextBox CssClass="form-control" ID="txtsearch" onkeyup="Search_Gridview(this, 'gvPurchaseEntry')" runat="server" MaxLength="50" style="width:170px;margin-top: -4px;margin-left:-410px;" placeholder="Search text"></asp:TextBox>
                                               <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" 

ValidChars=" /.-"  TargetControlID="txtsearch" />
</div>
      <div class="col-lg-2">                                         
                                               <asp:DropDownList ID="ddlVendor" CssClass="form-control" runat="server" style="width:273px;" Visible="false"></asp:DropDownList>
                                               </div>
                                               
                                         <div class="col-lg-2">        
                                         <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" ValidationGroup="val1"
                                                style="width:120px;margin-top: -4px;margin-left:-105px;" 
                                                onclick="btnsearch_Click2" />
                                                </div>
                                          
                                            <div class="col-lg-2">
                                                <asp:Button ID="btnresret" runat="server" class="btn btn-primary" 
                                                Text="Reset" style="width:120px;margin-top: -34px;margin-left:478px;" 
                                                onclick="btnresret_Click" />
                                               </div> 

                                              <div class="col-lg-2">
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" 
                                                style="width:120px;margin-top: -34px;margin-left:410px;" 
                                                onclick="btnadd_Click" />
                                               </div>

    <%--<div class="row">
                <div class="col-lg-12" >
                    <h1 class="page-header">Purchase Entry Details</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>--%>
                    

          <div class="row">
                <div class="col-lg-12" style="margin-top:-12px">
                    <div class="panel panel-default">
                 
                        

                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                         <div class="panel-body">
                           <div class="table-responsive">
                                        
                                <table class="table table-bordered table-striped">
                                <tr>
                                <td>
                                    
                                        <div>
                                     
                                        </div> 
                               </td>
                                    </tr>    
									  <tr>
                                <td>
                                   

                              
                                 <div style="overflow:auto; height:500px">
                                <asp:GridView ID="gvPurchaseEntry" runat="server" AllowPaging="true" PageSize="10" width="100%"  EmptyDataText="No records Found"  
                                        AutoGenerateColumns="false" CssClass="myGridStyle1" 
                                        onrowcommand="gvPurchaseEntry_RowCommand" 
                                        onpageindexchanging="gvPurchaseEntry_PageIndexChanging"   AllowSorting="true"   onsorting="gridview_Sorting" >
                                 <HeaderStyle BackColor="#3366FF" ForeColor="Black" />
                                 <EmptyDataRowStyle HorizontalAlign="Center"  />
                                <PagerSettings  Mode="Numeric"  />
    <Columns>
    <asp:BoundField SortExpression="DayBookTransNo" HeaderStyle-ForeColor="Black" Visible="false" ItemStyle-Width="3%"  DataField="DayBookTransNo" />
    <asp:BoundField SortExpression="DC_NO" HeaderText="DC NO" HeaderStyle-ForeColor="Black"  ItemStyle-Width="2%" DataField="DC_NO" />
    <asp:BoundField SortExpression="DC_Date" HeaderText="DC Date" HeaderStyle-ForeColor="Black"  ItemStyle-Width="2%" DataField="DC_Date"  DataFormatString="{0:dd/MM/yyyy}" />
    <asp:BoundField SortExpression="Bill_NO" HeaderText="Bill NO" HeaderStyle-ForeColor="Black"  ItemStyle-Width="2%" DataField="Bill_NO" />
    <asp:BoundField SortExpression="Bill_date" HeaderText="Bill Date" HeaderStyle-ForeColor="Black"  ItemStyle-Width="3%" DataField="Bill_date" DataFormatString="{0:dd/MM/yyyy}" />
    <asp:BoundField SortExpression="VendorName1" HeaderText="Supplier Name" HeaderStyle-ForeColor="Black" ItemStyle-Width="8%" DataField="VendorName1" />
    <asp:BoundField SortExpression="PaymentMode" HeaderText="Payment Mode" HeaderStyle-ForeColor="Black"  ItemStyle-Width="3%" DataField="PaymentMode" />
    <asp:BoundField SortExpression="TotalAmount" HeaderText="Total Amount" HeaderStyle-ForeColor="Black"  ItemStyle-Width="5%" DataField="TotalAmount"  DataFormatString="{0:f}" />
     
     <asp:TemplateField ItemStyle-Width="2%" HeaderText="Edit"  ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("DC_NO") %>' CommandName="edit"><asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
      
     </ItemTemplate>
      </asp:TemplateField>

     <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
     <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("DC_NO")+","+Eval("purchaseorderno")%>' CommandName="delete"><asp:Image ID="img1" runat="server" ImageUrl="~/images/DeleteIcon_btn.png" /> </asp:LinkButton>
  <ajaxToolkit:modalpopupextender   
		id="lnkDelete_ModalPopupExtender" runat="server" 
		cancelcontrolid="ButtonDeleteCancel" okcontrolid="ButtonDeleleOkay" 
		targetcontrolid="btnDelete"  popupcontrolid="DivDeleteConfirmation" 
		backgroundcssclass="ModalPopupBG">
        </ajaxToolkit:modalpopupextender>
        <ajaxToolkit:ConfirmButtonExtender id="lnkDelete_ConfirmButtonExtender" 
		runat="server" targetcontrolid="btnDelete" enabled="True" 
		displaymodalpopupid="lnkDelete_ModalPopupExtender">
        </ajaxToolkit:ConfirmButtonExtender>
      
     </ItemTemplate>
     
     
     </asp:TemplateField>
     <%--<asp:TemplateField HeaderText="Delete">
     <ItemTemplate>
           <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("SalesID") %>' CommandName="delete" OnClientClick="alertMessage()"><asp:Image ID="dlt" runat="server" ImageAlign="Middle" ImageUrl="~/images/delete.png" /></asp:LinkButton>
   </ItemTemplate>
    
     
     
     </asp:TemplateField>--%>

       <asp:TemplateField ItemStyle-Width="2%" HeaderText="Print" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
           <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("DC_NO") %>' CommandName="print" ><asp:Image ID="print" runat="server"  ImageUrl="~/images/Print_Icon.jpg" /></asp:LinkButton>
   </ItemTemplate>
    
     
     
     </asp:TemplateField>
    <asp:TemplateField ItemStyle-Width="5%" HeaderText="Preview Image">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl='<%# Eval("uploadfile")%>'
                Width="50px" Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>
   </Columns>
     <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
   <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
   </asp:GridView>

 

   </div>
  
                               </td>
                                </tr>
                                
                                </table>
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
                Purchase:</div>
            <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
            </div>
        </div>
        <div class="popup_Body">
            <p>
                Are you sure ,you want to delete Purchase?
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


                                  <div id="divBackground" >
</div>
<div id="divImage">
<table style="height: 100%; width: 100%">
    <tr>
        <td valign="middle" align="center">
            <img id="imgLoader" alt="" src="images/loader.gif" />
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
                                   
                                    </form>
                               


</body>

</html>
