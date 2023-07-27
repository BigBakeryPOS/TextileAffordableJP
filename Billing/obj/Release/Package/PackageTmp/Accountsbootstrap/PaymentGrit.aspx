<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentGrit.aspx.cs" Inherits="Billing.Accountsbootstrap.PaymentGrit" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">

<head runat="server">
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
    <title>Payment Grid Master - bootsrap</title>
    
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
            alert('Are You Sure, You want to delete This Customer!');
        }
    </script>


  <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>

<script type="text/javascript">


//$(document).ready(function () {
//            //
//            // Client Side Search (Autocomplete)
//            // Get the search Key from the TextBox
//            // Iterate through the 1st Column.
//            // td:nth-child(1) - Filters only the 1st Column
//            // If there is a match show the row [$(this).parent() gives the Row]
//            // Else hide the row [$(this).parent() gives the Row]
//          { $('#filter').keyup(function (event) {
//                var searchKey = $(this).val();

//                $("#PaymentGrid tr td:nth-child(4)").each(function () {
//                    var cellText = $(this).text();
//                    if (cellText.indexOf(searchKey) >= 0) {
//                        $(this).parent().show();
//                    }
//                    else {
//                        $(this).parent().hide();}

//                });

//            });

//        });



//    $(document).ready(function () {
        //
        // Client Side Search (Autocomplete)
        // Get the search Key from the TextBox
        // Iterate through the 1st Column.
        // td:nth-child(1) - Filters only the 1st Column
        // If there is a match show the row [$(this).parent() gives the Row]
        // Else hide the row [$(this).parent() gives the Row]

//        $('#TextBox1').keyup(function (event) {
//            var searchKey = $(this).val().toLowerCase();
//            $("#PaymentGrid tr:nth-child(n) td").each(function () {
//                var cellText = $(this).text().toLowerCase();
//                if (cellText.indexOf(searchKey) >= 0) {
//                    $(this).parent().show();
//                }
//                else {
//                    $(this).parent().hide();
//                }
//            });
//        });
//    });


//    Gridvie all row search:-------

//    $(document).ready(function () {
//        $("#txtsearch1").keyup(function () {
//            _this = this;

//            $.each($("#PaymentGrid tbody").find("tr"), function () {

//                if ($(this).text().toLowerCase().indexOf($(_this).val().toLowerCase()) == -1)
//                    $(this).hide();
//                else
//                    $(this).show();
//            });
//        });
//    });


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

   <asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>
    <div class="row">
    <div class="col-lg-12" style="margin-top:-12px">
                   <h2  style="text-align:left;color:#fe0002;" >Payment </h2> 



                    <div class="form-group" >
                                 <div class="col-lg-2">                                                      
                                         <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                 Style="color: White" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"   Text="." 
                                     Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                     </div>

                                            <asp:DropDownList CssClass="form-control" OnSelectedIndexChanged="searchchanged" Visible="false" AutoPostBack="true" ID="ddlfilter" style="width:170px;margin-left: 110px;margin-top: -20px;" runat="server">
                                            <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="TransNo" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Payment No" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Payment Date" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Ledger Name" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                                
                                               <div runat="server" id="normaltext">
                                                <div class="col-lg-2">
                                                 <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" 
                                    ControlToValidate="txtsearch" ErrorMessage="Please enter your searching Data!" Text="."
                                    Style="color: White" />
                                    </div>
                                    <div class="col-lg-2">
                                       <asp:TextBox CssClass="form-control"  Enabled="true" ID="txtsearch1" runat="server" onkeyup="Search_Gridview(this, 'PaymentGrid')"
                                        placeholder="Search Text" style="width:170px;margin-top:-41px;margin-left:-209px;"  ></asp:TextBox>
                                       </div>
                                                <asp:TextBox CssClass="form-control"  Enabled="true" ID="txtsearch" runat="server" Visible="false" placeholder="Search Text" style="width:170px;margin-top: -54px;margin-left: 290px;" ></asp:TextBox>
                                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=""
                                    TargetControlID="txtsearch" />
                                          <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>
                                          </div>
                                          <div runat="server"  visible="false" id="dateserach">
                                    <div class="col-lg-2">
                                          <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="datee" 
                                    ControlToValidate="txtdate" ErrorMessage="Please enter your searching Data!" Text="."
                                    Style="color: White" />
                                    </div>
                                     <div class="col-lg-2">
                                     <asp:TextBox CssClass="form-control"  Enabled="true" ID="txtdate" runat="server" placeholder="--Select Date--" style="width:170px;margin-top: -54px;margin-left: 290px;" ></asp:TextBox>
                                     
                                      <%--<asp:TextBox CssClass="form-control" ID="txtdate" runat="server" Text="--Select Date--"></asp:TextBox>--%>
                                
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                                </div>
                                </div>
                                         

                                        <asp:Button ID="btnsearch" runat="server" Visible="false" class="btn btn-success" Text="Search"  ValidationGroup="val1"
                                                style="width:120px;margin-top: -60px;margin-left: 445px;" 
                                                onclick="btnsearch_Click" /> 
                                        <div class="col-lg-2">
                                        <asp:Button ID="btnrefresh" runat="server" class="btn btn-primary" Text="Reset"  
                                                style="width:120px;margin-top: -42px;margin-left:-200px;" 
                                                onclick="btnrefresh_Click"   /> 
                                                </div>
                                                <div class="col-lg-2">
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Text="Add New"    
                                                style="width:120px;margin-top: -42px;margin-left:-240px;" 
                                                onclick="btnadd_Click" />  
                                                </div>


                              
                                                 
                                   </div>

                
                </div>
                <!-- /.col-lg-12 -->
            </div>


          <div class="row" >
                <div class="col-lg-12" style="margin-top:-10px">
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
                                <td align="center" style="font-size: small;width:3%">
                                  TransNo
                                </td>
                                <td align="center" style="font-size: small;width:0%">
                                  RefNo
                                </td>
                                <td align="center" style="font-size: small;width:17%">
                                  TransDate
                                </td>
                                <td align="center" style="font-size: small;width:0%">
                                 LedgerName
                                </td>
                                <td align="left" style="font-size: small;width:0%">
                                   paymode                   
                                </td>
                                 <td align="left" style="font-size: small;width:0%">
                                   Amount                   
                                </td>
                                 <td align="center" style="font-size: small;width:0%">
                                   Narration                  
                                </td>
                                 <td align="center" style="font-size: small;width:0%">
                                  Edit                    
                                </td>
                                <td align="center" style="font-size: small;width:1%">
                                   Delete                       
                                </td>          
                                         
                                                                        
                                                                
                            </tr>
                        </table>




                                       
                  <div style="overflow:auto; height:300px">
                             
                                <asp:GridView ID="PaymentGrid"   CssClass="myGridStyle1" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" 
                                 onsorting="gridview_Sorting" AllowSorting="true"  runat="server" PageSize="10" width="100%" AllowPaging="false"
                                 AutoGenerateColumns="false" onpageindexchanging="PaymentGrid_PageIndexChanging" 
                                 onrowcommand="PaymentGrid_RowCommand"  > 
                                             <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                             <HeaderStyle BackColor="#3366FF"  />
                              <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
    <Columns>

                                              <asp:BoundField  SortExpression="TransNo" HeaderStyle-ForeColor="Black"  ItemStyle-Width="4%" DataField="TransNo"    />
                                        <asp:BoundField SortExpression="RefNo" HeaderStyle-ForeColor="Black"   ItemStyle-Width="3%" DataField="RefNo"  Visible="false"   />
                                    <asp:BoundField SortExpression="TransDate" HeaderStyle-ForeColor="Black"   ItemStyle-Width="4%" DataField="TransDate"  DataFormatString="{0:dd/MM/yyyy}"/>
                                    <asp:BoundField SortExpression="LedgerName" HeaderStyle-ForeColor="Black"   ItemStyle-Width="12%" DataField="LedgerName"/>
                                    <asp:BoundField SortExpression="paymode" HeaderStyle-ForeColor="Black"   ItemStyle-Width="6%" DataField="paymode"    />
                                    <asp:BoundField SortExpression="Amount" HeaderStyle-ForeColor="Black"   ItemStyle-Width="7%" DataField="Amount"  DataFormatString="{0:f}"  />
                                    <asp:BoundField SortExpression="ProcessType" HeaderStyle-ForeColor="Black"   ItemStyle-Width="7%" DataField="ProcessType" />
                                      <asp:BoundField SortExpression="Narration" HeaderStyle-ForeColor="Black"   ItemStyle-Width="8%" DataField="Narration"    />
                          
                                  <%--  <asp:BoundField HeaderText="Type" DataField="Type"    />--%>
                                  
                             
                                
     <asp:TemplateField ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("TransNo") %>' CommandName="edit"><asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
      
     </ItemTemplate>
    
     
     
     </asp:TemplateField>
     <asp:TemplateField  ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
     <ItemTemplate>
           <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#Eval("TransNo") %>' CommandName="delete" OnClientClick="alertMessage()"><asp:Image ID="dlt" runat="server"  ImageUrl="~/images/DeleteIcon_btn.png" /></asp:LinkButton>
   
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
   </div>
                                    
                                    
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
          
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript">		    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

               </div>
                 <asp:panel class="popupConfirmation" id="DivDeleteConfirmation" 
	style="display: none" runat="server">
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Payment:</div>
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
