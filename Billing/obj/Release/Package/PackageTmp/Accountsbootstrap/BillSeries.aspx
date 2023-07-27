<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillSeries.aspx.cs" Inherits="Billing.Accountsbootstrap.BillSeries" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Category Grid </title>
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
<style>
    .pagination-ys {
    /*display: inline-block;*/
    padding-left: 0;
    margin: 20px 0;
    border-radius: 4px;
}
 
.pagination-ys table > tbody > tr > td {
    display: inline;
}
 
.pagination-ys table > tbody > tr > td > a,
.pagination-ys table > tbody > tr > td > span {
    position: relative;
    float: left;
    padding: 8px 12px;
    line-height: 1.42857143;
    text-decoration: none;
    color: #dd4814;
    background-color: #ffffff;
    border: 1px solid #dddddd;
    margin-left: -1px;
}
 
.pagination-ys table > tbody > tr > td > span {
    position: relative;
    float: left;
    padding: 8px 12px;
    line-height: 1.42857143;
    text-decoration: none;    
    margin-left: -1px;
    z-index: 2;
    color: #aea79f;
    background-color: #f5f5f5;
    border-color: #dddddd;
    cursor: default;
}
 
.pagination-ys table > tbody > tr > td:first-child > a,
.pagination-ys table > tbody > tr > td:first-child > span {
    margin-left: 0;
    border-bottom-left-radius: 4px;
    border-top-left-radius: 4px;
}
 
.pagination-ys table > tbody > tr > td:last-child > a,
.pagination-ys table > tbody > tr > td:last-child > span {
    border-bottom-right-radius: 4px;
    border-top-right-radius: 4px;
}
 
.pagination-ys table > tbody > tr > td > a:hover,
.pagination-ys table > tbody > tr > td > span:hover,
.pagination-ys table > tbody > tr > td > a:focus,
.pagination-ys table > tbody > tr > td > span:focus {
    color: #97310e;
    background-color: #eeeeee;
    border-color: #dddddd;
}
</style>
    
    <link href="../css/chosen.css" rel="Stylesheet" />
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




    <%--<script type="text/javascript">
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
    </script>--%>


    
    <script type="text/javascript">
        $(document).ready(function () {

            //         Client Side Search (Autocomplete)
            //         Get the search Key from the TextBox
            //         Iterate through the 1st Column.
            //         td:nth-child(1) - Filters only the 1st Column
            //         If there is a match show the row [$(this).parent() gives the Row]
            //         Else hide the row [$(this).parent() gives the Row]

            $('#txtsearch').keyup(function (event) {
                var searchKey = $(this).val().toLowerCase();
                $("#gridview tr:nth-child(odd)").each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(searchKey) >= 0) {
                        $(this).parent().show();
                    }
                    else {
                        $(this).parent().hide();
                    }
                });
            });
        });

    </script>
    </head>
<body>
   <usc:Header ID="Header" runat="server" />
    

    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>

                    <form id="Form1" runat="server">
                    <asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>


<%--<asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />--%>

<div class="row">
    <div class="col-lg-12" >
                   <h2 class="page-header" style="text-align:left;color:#fe0002;" >Bill Series</h2> 
                
                </div>
                <!-- /.col-lg-12 -->
            </div>

         <div class="row">
                <div class="col-lg-8">
                    <div class="panel panel-default">
                        
                        <div class="panel-body" style="margin-top: 0px;">

                        <div class="table-responsive">
                                        
                                <table>
                                <tr><td> 
                                        <div style="margin-top:20px">
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                 Style="color:white" InitialValue="0" ControlToValidate="ddlfilter" ValueToCompare="0"   Text="*"
                                     Operator="NotEqual" Type="String" ErrorMessage="Please Select Search By"></asp:CompareValidator>
                                       <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" 
                                    ControlToValidate="txtsearch" ErrorMessage="Please enter your searching Data!" Text="*"
                                    Style="color: White" />
                                           <asp:DropDownList CssClass="form-control" ID="ddlfilter" Visible="false" style="width:150px;margin-left:1px;" runat="server">
                                           <asp:ListItem Text="Search By" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Category" Value="1"></asp:ListItem>
                                              <asp:ListItem Text="IsActive" Value="2"></asp:ListItem>
                                            
                                                </asp:DropDownList>

                                                
                                            <asp:TextBox CssClass="form-control" ID="txtsearch"  runat="server" placeholder="Search Text" MaxLength="50" style="width:170px;margin-top: -34px;margin-left:57px;" ></asp:TextBox>
                                            
                                             <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -"  TargetControlID="txtsearch" />--%>
                                            <%--<asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label><br />--%>
                                                                                 <asp:Button ID="btnresret" runat="server" class="btn btn-primary" Text="Reset"  style="width:120px;margin-top:-34px;margin-left: 248px;" onclick="Btn_Reset" />
                                         <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Visible="false" ValidationGroup="val1" Text="Search" style="width:120px;margin-top: -59px;margin-left: 10px;"    onclick="Btn_Search"  />

                                         <asp:Button ID="Button2" runat="server" class="btn btn-success" Visible="false"  Text="Bulk Addition" style="width:120px;margin-top: -34px;margin-left:430px;"  Height="32px" onclick="btnFormat_Click"/>
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-danger" Visible="false" Text="Add New" AccessKey="N" style="width:120px;margin-top: -54px;margin-left: 821px;"   onclick="btnadd_Click" />
                                         <asp:Button ID="btnexcel" runat="server" class="btn btn-info" Visible="false"  Text="Export-To-Excel" style="width:120px;margin-top: -34px;margin-left: 575px;"  

Height="32px" onclick="btnExcel_Click"/>

                                        
                                        <%--<label>Alt+N</label>--%>
                                        </div> </td></tr>
                                          
      
                                <tr>
                                <td>
                             
                                <asp:GridView ID="gridview" runat="server" Width="200%" style="margin-top:25px;margin-left:54px"  EmptyDataText="No Records Found" 
                                    OnPageIndexChanging="Page_Change"     DataKeyNames="Billseriesid"
                                        AutoGenerateColumns="false" CssClass="myGridStyle"  AllowSorting="true"
                                        onrowcommand="gvcat_RowCommand" OnRowDataBound="gridview_RowDatabound" 
                                        onsorting="gridview_Sorting" 
                                        onselectedindexchanged="gridview_SelectedIndexChanged">
                                 <HeaderStyle BackColor="#3366FF" />
                                 <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                              <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
                                 <%--<PagerSettings Mode="Numeric" PageButtonCount="5" FirstPageText="&lt;i class=&quot;icon-step-forward&quot;&gt;&lt;/i&gt;" LastPageText="&lt;i class=&quot;icon-step-backward&quot;&gt;&lt;/i&gt;" NextPageText="&lt;i class=&quot;icon-step-forward&quot;&gt;&lt;/i&gt;" PreviousPageText="&lt;i class=&quot;icon-step-backward&quot;&gt;&lt;/i&gt;" />--%>
                                
                                <Columns>
                                
                                   <asp:BoundField HeaderText="Billseriesid" Visible="false" DataField="Billseriesid" />
                                    <asp:BoundField HeaderText="Screen Name" DataField="ScreenName"  SortExpression="ScreenName" HeaderStyle-ForeColor="Black"  />
                                        <asp:BoundField HeaderText="Type" DataField="types"  SortExpression="Type" HeaderStyle-ForeColor="Black"   />
                                          <asp:BoundField HeaderText="Bill No" DataField="Billno"  SortExpression="IsActive" HeaderStyle-ForeColor="Black"   />

                                    

                               <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit"   CommandArgument='<%#Eval("Billseriesid") %>' CommandName="Select" runat="server">
      <asp:Image ID="imdedit"  ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
     

      </asp:LinkButton>
    

                                <%--<asp:LinkButton ID="btnedit" runat="server"  Text="edit"  CommandArgument='<%#Eval("categoryid") %>' CommandName="edit"></asp:LinkButton>--%>
                                  <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("Billseriesid") %>' />
                                 </ItemTemplate>
    
     
     
     </asp:TemplateField>
          <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
     <ItemTemplate>
    
     <asp:LinkButton ID="btndel"   CommandArgument='<%#Eval("Billseriesid") %>' CommandName="Del" runat="server">
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
                                
                                
                               
										
									
                                    
                                    <div class="col-lg-6" style="margin-top: 88px;">
                                    <div >
                                        <h2><label><font color="red"></font></label></h2>
                                <table >
                                <tr>
                                <td colspan="4" align="left">
                                    <asp:GridView ID="GVStockAlert" Visible="false" runat="server" AutoGenerateColumns="false" CssClass="myGridStyle" >
                                    <Columns>
                                <asp:BoundField DataField="Category" HeaderText="Category"  /> 
                                <asp:BoundField DataField="Definition" HeaderText="Definition"  /> 
                                <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity"  />--%> 
                                <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" DataFormatString="{0:n2}" /> 
                                <asp:BoundField DataField="Available_QTY" HeaderText="Available_QTY"  /> 
                                <asp:BoundField DataField="MinQty" HeaderText="MinQty"  /> 
                                   
                                    </Columns>
                                </asp:GridView>
                                </td>
                                </tr>
                                </table>
                                </div>
                                    </div>
                                    </div></div>
                                    
                                   
                                    </div>
                                    <%--    <div class="col-lg-1"></div>--%>
                                    <div class="col-lg-4" style="margin-left:-14px">
                                       <div class="panel panel-default">
                                      
                        <div class="panel-heading" style="background-color:#59d3b4;color:#333333;border-color:#06090c">
                            <i class="fa fa-briefcase"></i> <asp:label id="lblName" Text="Add Bill Series" runat="server"></asp:label>
                        </div>
                             <div class="panel-body">
                                <div class="list-group">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val2"
                                ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="txtbillid" runat="server" Visible="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Screen Name</label>
                                <asp:ListBox Visible="false" Style="height: 100px" runat="server" DataValueField="CategoryID"
                                    ID="listcategory" CssClass="form-control" AutoPostBack="true"></asp:ListBox>
                                <%--<onselectedindexchanged="listcategory_SelectedIndexChanged" asp:DropDownList ID="ddlcategory" CssClass="form-control"  runat="server"></asp:DropDownList>--%>
                               
                             <asp:DropDownList ID="drpscreen" runat="server" CssClass="form-control" >
                             <asp:ListItem Text="Select Screen" Value="0"></asp:ListItem>
                             <asp:ListItem Text="Sales" Value="1"></asp:ListItem>
                             <asp:ListItem Text="Purchase" Value="2"></asp:ListItem>

                             </asp:DropDownList>
                             <%--   <asp:TextBox CssClass="form-control" ID="txtcategory" runat="server" placeholder="To Add New Category"></asp:TextBox>--%>
                                <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label>
                                    Type</label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddltype"
                                  ValidationGroup="val2" Text="*" ErrorMessage="Please Select IsActive!" Style="color: Red" />
                                <asp:DropDownList ID="ddltype" runat="server" class="form-control">
                                    <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>
                                    Start Bill Series</label>
                                 <asp:RequiredFieldValidator runat="server" ID="txtcat" ControlToValidate="txtbill"
                                    ValidationGroup="val2" Text="*" ErrorMessage="Please enter your Bill!" Style="color: Red" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="Numbers" ValidChars=""
                                    TargetControlID="txtbill" />
                           <asp:TextBox ID="txtbill" runat="server" CssClass="form-control" ></asp:TextBox>
                            </div>
                           
                            <asp:Button ID="Button1" runat="server" class="btn btn-info" Text="Save" 
                                ValidationGroup="val2" Style="width: 120px;" AccessKey="s" 
                                        onclick="Button1_Click" />
                            <label>
                            </label>
                            <asp:Button ID="btnCancel" runat="server" class="btn btn-warning" Text="Cancel" 
                                        Style="width: 120px;" onclick="btnCancel_Click" />
                            </div>
                            </div>
                            </div>
                            
                             
                                    </div>	
             
                     
                                </div>
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
                               
                                </form>
</body>
</html>

