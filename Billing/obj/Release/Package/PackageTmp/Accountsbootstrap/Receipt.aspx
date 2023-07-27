<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="Billing.Accountsbootstrap.Receipt" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <title>Receipt Page</title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script language="javascript" type="text/javascript">

        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else

                event.returnValue = false;
        }

    </script>
    <script type="text/javascript">
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function ClientSideClick(myButton) {
            // Client side validation

            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // disable the button
                myButton.disabled = true;
                myButton.className = "btn-inactive";
                myButton.value = "processing...";

            }
            return true;
        }
    </script>
    <link rel="stylesheet" href="../css/chosen.css" />
</head>
<body>
    <usc:header id="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1,5"> </asp:Label>
    <form id="Form1" runat="server" role="form">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
            <div class="col-lg-12">
                 <h2 class="page-header" style="text-align: left; color: #fe0002;">
                        <asp:Label runat="server" ID="lbltype" ForeColor="#fe0002"></asp:Label>
                    </h2>
                </div>
                <div class="col-lg-12">

                 <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                        ID="val1" ShowSummary="true" />
                   
                </div>
                
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-1">
                                </div>
                                <div class="col-lg-2">                                   
                                    <asp:TextBox ID="txtreceiptid" runat="server" Visible="false"></asp:TextBox>
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rblist" runat="server" OnSelectedIndexChanged="rblist_selectedIndexChanged" RepeatColumns="3"
                                            AutoPostBack="true" RepeatDirection="Horizontal" Visible="false">
                                            <asp:ListItem Text="Customer" Value="1"></asp:ListItem>   
                                             <asp:ListItem Text="Dealer" Value="5"></asp:ListItem>                                        
                                            <asp:ListItem Text="Supplier" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Employee" Value="4"></asp:ListItem>
                                             <asp:ListItem Text="Agent" Value="6"></asp:ListItem>
                                           <%--  <asp:ListItem Text="Bank" Value="2"></asp:ListItem>--%>
                                            
                                           
                                            <asp:ListItem Text="SubDealer" Value="7"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Receipt Number</label>
                                        <asp:TextBox CssClass="form-control" Enabled="true" OnTextChanged="txtreceiptnochanged"
                                            AutoPostBack="true" ID="txtreceiptno" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Receipt Date</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator4"
                                            Text="*" ControlToValidate="txtreceiptdate" ErrorMessage="Please enter Receipt Date"
                                            Style="color: Red" />
                                        <asp:TextBox CssClass="form-control" ID="txtreceiptdate" runat="server" Text="--Select Date--"
                                            onkeyup="ValidateDate(this, event.keyCode)" onkeydown="return DateFormat(this, event.keyCode)"></asp:TextBox>
                                    </div>
                                    <ajaxtoolkit:calendarextender id="CalendarExtender1" targetcontrolid="txtreceiptdate"
                                        runat="server" format="dd/MM/yyyy" cssclass="cal_Theme1">
                                    </ajaxtoolkit:calendarextender>
                                    <div class="form-group" id="divagent" runat="server" visible="false">
                                        <asp:TextBox CssClass="form-control" ID="txtTransNo" runat="server" Visible="false"></asp:TextBox>
                                        <label>
                                            Agent Name</label>
                                        <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"  Text="*" style="color:Red" InitialValue="0"
                          ControlToValidate="ddlType" ValueToCompare="Select Agent" Operator="NotEqual"  Type="String"   errormessage="Please select Agent name!"></asp:CompareValidator>--%>
                                        <%--<asp:CompareValidator ID="id" runat="server" ControlToValidate="ddlType" ValueToCompare="0"  ValidationGroup="val1" Text="*" errormessage="Please Ledger Type"></asp:CompareValidator>--%>
                                        <asp:DropDownList runat="server" ID="ddlType" CssClass="chzn-select" Width="196px"
                                            Height="60px" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                            Enabled="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group" runat="server">
                                        <label id="lblcustomer" runat="server" visible="false">
                                            Customer Name</label>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlcustomerID"
                                            ValueToCompare="Select Customer" Operator="NotEqual" Type="String" ErrorMessage="Please select Customer name!"></asp:CompareValidator>
                                        <asp:DropDownList runat="server" ID="ddlcustomerID" CssClass="chzn-select" Width="194px"
                                            Height="60px" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcustomerID_SelectedIndexChanged"
                                            Visible="false" Enabled="true">
                                        </asp:DropDownList>
                                        <label id="lblbank" runat="server" visible="false">
                                            Bank Name</label>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlbankID" ValueToCompare="Select Bank"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please select Bank name!"></asp:CompareValidator>
                                        <asp:DropDownList runat="server" ID="ddlbankID" CssClass="chzn-select" Width="194px"
                                            Height="60px" class="form-control" AutoPostBack="true" Visible="false" Enabled="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group" id="aganistbillno" runat="server" visible="false">
                                        <asp:Label ID="Label1" Text="Against Bill NO" runat="server" Style="font-weight: bold"></asp:Label>
                                        <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender12" runat="server"
                                            filtertype="Numbers,Custom" validchars=". " targetcontrolid="txtAgainst" />
                                        <asp:TextBox CssClass="form-control" ID="txtAgainst" MaxLength="10" Text="0" OnTextChanged="billno"
                                            Visible="false" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                        </label>
                                        <asp:TextBox CssClass="form-control" ID="txtcustomername" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label style="margin-top: -21px;">
                                            Address</label>
                                        <asp:TextBox CssClass="form-control" ID="txtaddress" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Area</label>
                                        <asp:TextBox CssClass="form-control" ID="txtarea" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            City</label>
                                        <asp:TextBox CssClass="form-control" ID="txtcity" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                </div>
                                <div class="col-lg-2" runat="server" visible="true">
                                    <div class="form-group">
                                        <label>
                                            Select GST Type</label>
                                        <asp:DropDownList runat="server" ID="drpGsttype" CssClass="chzn-select" Width="196px"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpGsttype_SelectedIndexChanged"
                                            Height="60px" class="form-control">
                                            <asp:ListItem Text="GST Inclusive" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="GST Exclusive" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Select Tax</label>
                                        <asp:DropDownList runat="server" ID="ddltax" CssClass="chzn-select" Width="196px"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpGsttype_SelectedIndexChanged"
                                            Height="60px" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div runat="server" visible="true" class="form-group">
                                        <label>
                                            Province</label>
                                        <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpGsttype_SelectedIndexChanged">
                                            <asp:ListItem Text="Select Province type" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Inner(CGST/SGST)" Value="1" Selected="True" ></asp:ListItem>
                                            <asp:ListItem Text="Others(IGST)" Value="2" ></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Amount</label>

                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                            ControlToValidate="txtgstAmount" Text="*" ErrorMessage="Please enter your Amount!"
                                            Style="color: Red" />
                                        <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender11" runat="server"
                                            filtertype="Numbers,Custom" validchars="." targetcontrolid="txtgstAmount" />
                                        <asp:TextBox CssClass="form-control" ID="txtgstAmount" MaxLength="15" AutoPostBack="true"
                                            OnTextChanged="txtgstAmount_TextChanged" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label style="margin-left: -15px">
                                                    CGST</label>
                                                <asp:TextBox CssClass="form-control" ID="txtCGST" runat="server" Width="140px" Enabled="false"
                                                    MaxLength="8" Style="text-align: right; width: 75px; margin-left: -15px; font-weight: bold">0</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label style="margin-left: -15px">
                                                    SGST</label>
                                                <asp:TextBox CssClass="form-control" ID="txtSGST" runat="server" Width="140px" Enabled="false"
                                                    MaxLength="8" Style="text-align: right; width: 75px; margin-left: -11px; font-weight: bold">0</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label style="margin-left: -15px">
                                                    IGST</label>
                                                <asp:TextBox CssClass="form-control" ID="txtIGST" runat="server" Width="140px" Enabled="false"
                                                    MaxLength="8" Style="text-align: right; width: 75px; margin-left: -7px; font-weight: bold">0</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Net Amount</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                                            Text="*" ControlToValidate="txtAmount" ErrorMessage="Please enter Net Amount" Style="color: Red" />
                                        <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender1" runat="server"
                                            filtertype="Numbers,Custom" validchars="." targetcontrolid="txtAmount" />
                                        <asp:TextBox CssClass="form-control" MaxLength="15" ID="txtAmount" AutoPostBack="true"
                                            OnTextChanged="txtAmount_TextChanged" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Narration</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator3"
                                            Text="*" ControlToValidate="txtNarration" ErrorMessage="Please enter Narration"
                                            Style="color: Red" />
                                        <%--                              <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtNarration" ValidationExpression="^[a-zA-Z0-9]{0,25}$" ErrorMessage="Only Alphanumeric"></asp:RegularExpressionValidator>--%>
                                        <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender4" runat="server"
                                            filtertype="LowercaseLetters, UppercaseLetters,Numbers,Custom" validchars=" +,!@#$%^&*()-/:;."
                                            targetcontrolid="txtNarration" />
                                        <asp:TextBox CssClass="form-control" ID="txtNarration" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                            <ContentTemplate>
                                                <label>
                                                    Image Upload</label>
                                                <asp:FileUpload ID="fp_Upload" runat="server" />
                                                <asp:Button ID="btnUpload123" runat="server" Text="Upload" CssClass="btn btn-primary"
                                                    OnClick="btnUpload_Clickimg" Width="100px" /><asp:Image ID="img_Photo" runat="server"
                                                        Width="70px" BorderColor="1" />
                                                <asp:Label ID="lblFile_Path" runat="server" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpload123" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            PayMode</label>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddmodeofpayment"
                                            ValueToCompare="Select Payment Mode" Operator="NotEqual" Type="String" ErrorMessage="Please select Payment mode!"></asp:CompareValidator>
                                        <asp:DropDownList CssClass="form-control" ID="ddmodeofpayment" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddmodeofpayment_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        </td>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Bank Name</label>
                                        <asp:DropDownList runat="server" CssClass="chzn-select" Width="194px" Height="60px"
                                            ID="ddlBank" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Cheque Nos/UTR No</label>
                                        <ajaxToolkit:FilteredTextBoxExtender id="FilteredTextBoxExtender2" runat="server"
                                            filtertype="Numbers" validchars="" targetcontrolid="txtChequeno" />
                                        <asp:TextBox CssClass="form-control" ID="txtChequeno" MaxLength="10" runat="server"
                                            placeholder="Enter Cheque No"></asp:TextBox>
                                    </div>
                                     <div runat="server" id="narra" visible="false" class="form-group">
                                    <label>
                                        Enter Narration
                                    </label>
                                    <asp:TextBox ID="txteditnarration" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                </div>
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-2">
                                    <div class="col-lg-1">
                                        <asp:Button ID="gridbutton" runat="server" Text="View grid" OnClick="gridbutton_Click" />
                                    </div>
                                </div>
                                <div>
                                    <asp:HiddenField ID="hidfnwe" runat="server" Value="New" />
                                    <div class="row">
                                        <%--  <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" id="Table1">
                                        <thead>
                                        <tr>
                                        
                                            <th>Bank Name</th>
                                            <th> Reference No</th>
                                        </tr></thead>
                                         <tbody>
                                         <tr class="odd gradeX">
                                         <td> 
                                            <td><asp:TextBox ID="txtbankname" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtrefno" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox></td>
                                         </tr>
                                         </tbody>
                                         </table>
                                        
                                        </div>
                                        </div>
                                        </div>
                                        </div>--%>
                                        <!-- /.col-lg-12 -->
                                    </div>
                                    <!-- /.row -->
                                    <div class="row">
                                        <br />
                                        <div class="col-lg-1">
                                        </div>
                                        <div class="col-lg-5">
                                            <div runat="server" visible="false" style="overflow: auto; height: 150px">
                                                <asp:GridView ID="grdVehno" runat="server" EmptyDataText="No records Found" Width="95%" OnRowDeleting="grdVehno_RowDeleting"
                                                    AllowPaging="false" AutoGenerateColumns="false" CssClass="myGridStyle" AllowSorting="true">
                                                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                        Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                    <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtno" Height="30px" runat="server" Enabled="false"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="LoginID" ControlStyle-Width="100%"
                                                            ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtLoginID" Height="30px" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SIM No" ControlStyle-Width="100%"
                                                            ItemStyle-Width="25%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSIMno" Height="30px" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IMEI No" ControlStyle-Width="100%"
                                                            ItemStyle-Width="25%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtIMEIno" Height="30px" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Plate No" ControlStyle-Width="100%"
                                                            ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPlateNo" Height="30px" runat="server" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:CommandField ShowDeleteButton="True"   ButtonType="Button" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <!-- /.panel-heading -->
                                            <div style="padding-left: 10px; margin-top: -20px" class="table-responsive">
                                                <div style="overflow: auto; height: 150px">
                                                    <asp:GridView ID="gvledgrid" runat="server" EmptyDataText="No records Found" Width="95%"
                                                        AllowPaging="false" AutoGenerateColumns="false" CssClass="myGridStyle" AllowSorting="true">
                                                        <HeaderStyle BackColor="#59d3b4" Height="30px" ForeColor="Black" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black"
                                                            Height="30px" />
                                                        <PagerSettings FirstPageText="1" Mode="Numeric" />
                                                        <Columns>
                                                            <%--   <asp:TemplateField HeaderText="Branch">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBranch" runat="server" Text='<%# Eval("Branch")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Branchcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBranchcode" runat="server" Text='<%# Eval("Branchcode")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SalesID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtSalesid" runat="server" Text='<%# Eval("SalesID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bill No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBillNo" runat="server" Text='<%# Eval("BillNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bill Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBillDate" runat="server" Text='<%# Eval("BillDate")%>' DataFormatString="{0:dd/MM/yyyy}"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bill Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBillAmount" runat="server" Text='<%# Eval("BillAmount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBalance" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#59d3b4" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#59d3b4" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                </div>
                                                <%-- <div class="table-responsive">
                            <asp:Label ID="lblerrortable" runat="server"  style="color:Red"></asp:Label>
                                <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>Bill No</th>
                                            <th>Bill Date</th>
                                            
                                            <th>Bill Amount</th>
                                            <th>Balance</th>
											<th>Amount</th>
                                           
                                        </tr>
                                    </thead>
                                    <tbody>
                                    
                                        <tr class="odd gradeX">
                                        <td>
                                        <asp:DropDownList CssClass="form-control" ID="ddbillno1" runat="server"  AutoPostBack="true"
                                                onselectedindexchanged="ddbillno1_SelectedIndexChanged"></asp:DropDownList>
                                        
                                        </td>
                                           <td><asp:TextBox CssClass="form-control" ID="txtbilldate1" runat="server" Width="150px"></asp:TextBox></td>
                                            
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbillamount1" runat="server" Width="150px"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbalance1"  Width="150px"
                                                    runat="server" ontextchanged="txtbalance1_TextChanged" AutoPostBack="true" ></asp:TextBox></td>
											<td class="center"><asp:TextBox CssClass="form-control" ID="txtamount1" runat="server" Width="150px"></asp:TextBox></td>
                                            
                                        </tr>
                                        <tr class="odd gradeX">
                                       <td> <asp:DropDownList CssClass="form-control" ID="ddbillno2" runat="server"  
                                               AutoPostBack="true" onselectedindexchanged="ddbillno2_SelectedIndexChanged"
                                                ></asp:DropDownList></td>
                                        
                                       
                                           <td><asp:TextBox CssClass="form-control" ID="txtbilldate2" runat="server" Width="150px"></asp:TextBox></td>
                                            
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbillamount2" runat="server" Width="150px"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbalance2"  Width="150px"
                                                    runat="server" AutoPostBack="true" ontextchanged="txtbalance2_TextChanged"></asp:TextBox></td>
											<td class="center"><asp:TextBox CssClass="form-control" ID="txtamount2" runat="server" Width="150px"></asp:TextBox></td>
                                        </tr>

                                         <tr class="odd gradeX">
                                       <td> <asp:DropDownList CssClass="form-control" ID="ddbillno3" runat="server"  
                                               AutoPostBack="true" onselectedindexchanged="ddbillno3_SelectedIndexChanged"
                                                ></asp:DropDownList></td>
                                        
                                       
                                           <td><asp:TextBox CssClass="form-control" ID="txtbilldate3" Width="150px" runat="server"></asp:TextBox></td>
                                            
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbillamount3" runat="server" Width="150px"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbalance3"  Width="150px"
                                                    runat="server" AutoPostBack="true" ontextchanged="txtbalance3_TextChanged"></asp:TextBox></td>
											<td class="center"><asp:TextBox CssClass="form-control" ID="txtamount3" runat="server" Width="150px"></asp:TextBox></td>
                                        </tr>

                                         <tr class="odd gradeX">
                                       <td> <asp:DropDownList CssClass="form-control" ID="ddbillno4" runat="server"  
                                               AutoPostBack="true" onselectedindexchanged="ddbillno4_SelectedIndexChanged"
                                                ></asp:DropDownList></td>
                                        
                                       
                                            <td><asp:TextBox CssClass="form-control" ID="txtbilldate4" runat="server" Width="150px"></asp:TextBox></td>
                                            
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbillamount4" Width="150px" runat="server"></asp:TextBox></td>
                                            <td class="center"><asp:TextBox CssClass="form-control" ID="txtbalance4"  Width="150px"
                                                    runat="server" AutoPostBack="true" ontextchanged="txtbalance4_TextChanged"></asp:TextBox></td>
											<td class="center"><asp:TextBox CssClass="form-control" ID="txtamount4" runat="server" Width="150px"></asp:TextBox></td>
                                        </tr>
                                    </tbody>
                                </table>
								
                            </div>--%>
                                                <br />
                                                <div style="text-align: center; margin-top: -10px; margin-right: 150px">
                                                    <asp:Button ID="btnadd" runat="server" Style="width: 120px;" ValidationGroup="val1" 
                                                        class="btn btn-info" Text="Save" OnClick="Add_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="false"/>
                                                    <asp:Button ID="btnexit" runat="server" Style="width: 120px;" class="btn btn-warning"
                                                        Text="Exit" OnClick="Exit_Click" />
                                                </div>
                                                <%--<asp:LinkButton ID="btnop" runat="server" 
                                Text="click here to get oustanding payment records" onclick="btnop_Click"></asp:LinkButton>--%>
                                                <!-- /.table-responsive -->
                                                <!-- /.col-lg-6 (nested) -->
                                            </div>
                                            <!-- /.row (nested) -->
                                            <!-- /.panel-body -->
                                        </div>
                                        <!-- /.panel -->
                                    </div>
                                    <!-- /.col-lg-12 -->
                                </div>
                                <!-- /.row -->
                            </div>
                            <!-- /#page-wrapper -->
                        </div>
                    </div>
                </div>
            </div>
            <!-- jQuery -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/chosen.min.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
