<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DescriptionGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.DescriptionGrid" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">

<head>

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

    <title>Accessories Master </title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function valchk() {
            if (dropdownchk(document.getElementById('ddlcategory'), "Select Category")
            //&& dropdownchk(document.getElementById('ddlgroup'), "Account Group")  
        && blankchk(document.getElementById('txtdescription'), "Description")) {
                alert("true");
            }
            else {
                alert("false");
                return false;
            }
        }
        

    </script>

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
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
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
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>

                    <form runat="server">
                     <asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>
                    <div class="row">
                <div class="col-lg-12" >
              <div class="col-lg-2">
                              <h2 class="page-header" style="text-align:left;color:#fe0002;font-size:20px" >Accessories Master</h2> 
                              </div>
                                                                   
					
										  <div class="col-lg-2">	
                                           <asp:DropDownList runat="server" Visible="false" ID="ddlcategory" CssClass="form-control"  style="width:170px;margin-left: 110px;">
                                       <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                      <asp:ListItem Text="Product ID" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Category Name" Value="2"></asp:ListItem>
                                           <%-- <asp:ListItem Text="Brand Name" Value="3"></asp:ListItem>--%>
                                
                                     
                                          </asp:DropDownList>
                                           <asp:TextBox onkeyup="Search_Gridview(this, 'gridview')" CssClass="form-control" ID="txtdescription" runat="server" placeholder="Search Text" MaxLength="50" style="width:170px;margin-bottom:22px;" ></asp:TextBox>
                                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" 

ValidChars=" -"  TargetControlID="txtdescription" />

                                       </div>
                                       
                                        <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>
                                            
                                         <asp:Button ID="Button1" Visible="false" runat="server" class="btn btn-success" Text="Search" style="width:120px;margin-left: 475px;" onclick="Button1_Click"    />
                                              
                                                 <div runat="server" visible="false" class="col-lg-2">	
                                                <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Reset" style="width:120px;margin-left: 20px;"  onclick="Button2_Click"/>
                                                 </div>
                                                 <div class="col-lg-2">
                                <div class="form-group ">
                                    <asp:DropDownList ID="drpbranch" OnSelectedIndexChanged="company_SelectedIndexChnaged" AutoPostBack="true" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                                                 
                                                 <asp:Button ID="Button3" runat="server" class="btn btn-success" Visible="false"  Text="Bulk Addition" style="width:120px;margin-left: 954px;"  Height="32px" onclick="btnFormat_Click"/>
                                   
                                       <div class="col-lg-2">	
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New" style="width:120px;margin-left: 20px;" onclick="btnadd_Click"/>
                                       </div>
                                        <div class="col-lg-2">	
                                        <asp:Button ID="btnexcel" runat="server" class="btn btn-info"  Text="Export-To-Excel" style="width:120px;margin-left: 24px;"  

Height="32px" onclick="btnExcel_Click"/>

                       </div>                 </div>
                
                <!-- /.col-lg-12 -->
            </div>
         <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                    
                             
                                	<div class="table-responsive" >
                                        
                                <table class="table table-bordered table-striped">
                                <tr>
                                <td>
                                  
                                          </td>
                                        </tr>
                              
                                         
									
                                <tr>
                                <td>

                                  <table id="Table1" visible="false" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;color:Black;
                            text-transform: uppercase" width="100%">
                            <tr>
                                <td align="center" style="font-size: small;width:20%">
                                   Category Name
                                </td>
                                <td align="center" style="font-size: small;width:10%">
                                 Accessories Code   
                                </td>
                                <td align="center" style="font-size: small;width:29%">
                                    Accessories Name
                                </td>
                                <td align="center" style="font-size: small;width:10%">
                                  IsActive  
                                </td>
                                <td align="center" style="font-size: small;width:10%">
                                    Tax                         
                                </td>
                                <td align="center" style="font-size: small;width:10%">
                                    Edit
                                </td>
                                 <td align="center" style="font-size: small;width:10%">
                                   Delete
                                </td>

                                 
                            </tr>
                        </table>


                                <div runat="server" style="overflow:scroll; height:350px">
                                <asp:GridView ID="gridview" runat="server" AllowPaging="false" Width="100%" PageSize="10"  
                                        OnPageIndexChanging="gridview_PageIndexChanging"  style="overflow:scroll; "
                                        AutoGenerateColumns="false"  EmptyDataText="No Records Found"
                                       CssClass="myGridStyle1" OnRowDataBound="gridview_RowDatabound" onrowcommand="gvcust_RowCommand"  
                                       AllowSorting="true"   onsorting="gridview_Sorting" >
                                                 <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                             
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />

                                <Columns>
                                <asp:BoundField  HeaderText="Category User ID" DataField="CategoryuserID"  Visible="false"/>
                                
                                    <asp:BoundField  HeaderText="Category ID" DataField="CategoryID" Visible="false" SortExpression="Category ID" />
                                   
                                    <%--<asp:BoundField HeaderText="Vendor Name" DataField="CustomerName" />--%>
                                              <asp:BoundField SortExpression="category" HeaderText="Category Name" ItemStyle-Width="20%"  HeaderStyle-ForeColor="Black"   DataField="category" />
                                       <asp:BoundField SortExpression="Definition" HeaderText="Accessories Code" ItemStyle-Width="10%" HeaderStyle-ForeColor="Black"  DataField="Definition" />
                                      <asp:BoundField SortExpression="Serial_No" HeaderText="Accessories Name" ItemStyle-Width="28%" HeaderStyle-ForeColor="Black"   DataField="Serial_No" />
                                       <asp:BoundField SortExpression="Meter"  HeaderStyle-ForeColor="Black" Visible="false"  DataField="Meter" />
                                      <asp:BoundField SortExpression="IsActive" HeaderText="Is Active" ItemStyle-Width="10%" HeaderStyle-ForeColor="Black"   DataField="IsActive" />
                                      <asp:BoundField SortExpression="Tax" HeaderText="GST% -Tax" HeaderStyle-ForeColor="Black" ItemStyle-Width="10%"   DataField="Tax" DataFormatString="{0:###,##0.0}"/>
                                       <asp:BoundField SortExpression="Discount" Visible="false" HeaderStyle-ForeColor="Black"  DataField="Discount" />
                                     <asp:BoundField DataField="CategoryuserID"  SortExpression="Definition" Visible="false" />
                                      <asp:TemplateField ItemStyle-Width="5%" HeaderText="Preview Image">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl='<%# Eval("Image")%>'
                Width="50px" Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>
                               <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit" ItemStyle-Width="10%">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit"   CommandArgument='<%#Eval("CategoryuserID") %>' CommandName="Edit" runat="server"> <asp:Image ID="imdedit"  ImageUrl="~/images/pen-checkbox-128.png" runat="server" /></asp:LinkButton>
    
    
                                <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
                                   <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("CategoryuserID") %>' />
                                 </ItemTemplate>
    
     
     
     </asp:TemplateField>
          <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete"  ItemStyle-Width="10%">
     <ItemTemplate>
    
     <asp:LinkButton ID="btndel"  CommandArgument='<%#Eval("categoryuserid") %>' CommandName="Del" runat="server"> 
     <asp:Image ID="Image1"  ImageUrl="~/images/DeleteIcon_btn.png" runat="server"  /></asp:LinkButton>
      <asp:ImageButton ID="imgdisable" ImageUrl="~/images/delete.png" runat="server" Visible="false" Enabled="false" ToolTip="Not Allow To Delete" />
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
                               </div>
                                </td>
                                </tr>
                                
                                </table>
                                
                                
									</div>
                                    </div></div></div>	
                               
                                 </div>
                                <asp:panel class="popupConfirmation" id="DivDeleteConfirmation" 
	style="display: none" runat="server">
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Description List</div>
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
                                
                                <!-- /.col-lg-6 (nested) -->
                                
                
		<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
		

</body>

</html>
