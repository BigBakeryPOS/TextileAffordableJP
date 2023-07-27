<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DespatchSales.aspx.cs"
    Inherits="Billing.Accountsbootstrap.DespatchSales" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head runat="server">
    <style type="text/css">
        a img
        {
            border: none;
        }
        ol li
        {
            list-style: decimal outside;
        }
        div#container
        {
            width: 780px;
            margin: 0 auto;
            padding: 1em 0;
        }
        div.side-by-side
        {
            width: 100%;
            margin-bottom: 1em;
        }
        div.side-by-side > div
        {
            float: left;
            width: 50%;
        }
        div.side-by-side > div > em
        {
            margin-bottom: 10px;
            display: block;
        }
        .clearfix:after
        {
            content: "\0020";
            display: block;
            height: 0;
            clear: both;
            overflow: hidden;
            visibility: hidden;
        }
    </style>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Despatch</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    
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
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1">
    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="Validation"
        ID="val1" ShowMessageBox="true" ShowSummary="false" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblName" Text="Despatch BuyerOrder Sales" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" style="margin-top: 6px">
                            <div class="col-lg-2">
                               <%-- <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                                    font-weight: bold">
                                    BuyerOrder Sales
                                </h1>--%>
                            </div>
                            <div class="col-lg-3">
                                <asp:TextBox CssClass="form-control" Enabled="true" Visible="false" onkeyup="Search_Gridview(this, 'GVItem')"
                                    ID="txtsearch" runat="server" placeholder="Enter Text to Search" Width="250px"></asp:TextBox>
                            </div>
                             <div class="col-lg-2">
                                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 16px;
                                    font-weight: bold">
                                   Invoice No
                                </h1>
                            </div>
                            <div class="col-lg-3">
                               <asp:DropDownList ID="ddlInvoiceno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceno_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="Validation"
                                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlInvoiceno" ValueToCompare="Select"
                                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select InvoiceNo."></asp:CompareValidator>
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnadd" runat="server" class="btn btn-primary" ValidationGroup="Validation" Text="View" OnClick="Add_Click"
                                    Width="130px" />
                            </div>
                        </div>
                    </div>
                    <div style="height: 392px; overflow: auto" class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <tr>
                                <td>
                                    <asp:GridView ID="GVItem" runat="server" CssClass="myGridStyle1" EmptyDataText="No records Found"
                                        Width="100%" AutoGenerateColumns="false" OnRowCommand="GVItem_RowCommand">
                                        <HeaderStyle BackColor="White" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="Next" PreviousPageText="Previous" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="InvoiceNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbuyerordersalesid" runat="server" Text='<%#Eval("BuyerOrdersalesid") %>' Visible="false" ></asp:Label>
                                                    <asp:Label ID="lblinvoiceno" runat="server" Text='<%#Eval("FullInvoiceNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="InvoiceDate">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblinvoicedate" runat="server" Text='<%#Eval("InvoiceDate") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                          <asp:TemplateField HeaderText="BuyerName">
                                              <ItemTemplate>
                                                   <asp:Label ID="lblledgerid" runat="server" Text='<%#Eval("LedgerId") %>' Visible="false" ></asp:Label>
                                                  <asp:Label ID="lblbuyername" runat="server" Text='<%#Eval("LedgerName") %>'></asp:Label>
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Qty">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblqty" runat="server" Text='<%#Eval("Qty") %>'></asp:Label>
                                               </ItemTemplate>

                                           </asp:TemplateField>
                                            
                                           <asp:TemplateField HeaderText="LRNo">
                                               <ItemTemplate><asp:TextBox ID="txtLRno" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator38" ControlToValidate="txtLRno"
                                        ValidationGroup="Validation1" Text="*" ErrorMessage="Please Enter LR No" Style="color: Red" />

                                               </ItemTemplate>

                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LR Date">
                                               <ItemTemplate><asp:TextBox ID="txtLRdate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator45" ControlToValidate="txtLRdate"
                                        ValidationGroup="Validation1" Text="*" ErrorMessage="Please Enter LRDate" Style="color: Red" />
                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtLRdate" Format="dd/MM/yyyy"
                                            runat="server" CssClass="cal">
                                        </ajaxToolkit:CalendarExtender>
                                               
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator39" ControlToValidate="txtLRdate"
                                        ValidationGroup="Validation1" Text="*" ErrorMessage="Please Enter LR Date" Style="color: Red" />
                                                   </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transport">
                                               <ItemTemplate><asp:TextBox ID="txtTransport" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator40" ControlToValidate="txtTransport"
                                        ValidationGroup="Validation1" Text="*" ErrorMessage="Please Enter Transport" Style="color: Red" />
                                               </ItemTemplate>

                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NoofPackage">
                                               <ItemTemplate><asp:TextBox ID="txtnoofpackage" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator41" ControlToValidate="txtnoofpackage"
                                        ValidationGroup="Validation1" Text="*" ErrorMessage="Please Enter No of Package" Style="color: Red" />
                                               </ItemTemplate>

                                           </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                     <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:FileUpload ID="fluimg" runat="server" />
                                                                                                <asp:Button ID="btn_Upload"  runat="server" Text="Upload"  CommandName="upload" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn-primary"
                                                                                                    Width="100px" />
                                                                                              <asp:Button ID="linkbtn_Clear"  runat="server" Text="Clear" CommandName="clear" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"   CssClass="btn-primary"
                                                                                                    Width="100px" />
                                                                                                <asp:Image ID="imgpreview" runat="server" Width="70px" BorderColor="1" /><br />
                                                                                                <asp:Label ID="lblFilePath" runat="server" Visible="true"></asp:Label>
                                                                                                <asp:Label ID="lblsqaFile_Path1" runat="server" Visible="true"></asp:Label>
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:PostBackTrigger ControlID="btn_Upload" />
                                                                                             <asp:PostBackTrigger ControlID="linkbtn_Clear" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>

                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>

                     <div class="col-lg-2">
                                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" ValidationGroup="Validation1" Text="Save" OnClick="Save_Click"
                                    Width="130px" />
                           <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" OnClick="Cancel_Click"  Text="Cancel"
                                    Width="130px" />
                            </div>
                    <!-- /.col-lg-6 (nested) -->
                </div>
                <!-- /.row (nested) -->
            </div>
            <!-- /.panel-body -->
        </div>
        <!-- /.panel -->
    </div>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    BuyerOrder Sales:-</div>
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
    </asp:Panel>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
