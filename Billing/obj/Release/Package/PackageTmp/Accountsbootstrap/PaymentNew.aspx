<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentNew.aspx.cs" Inherits="Billing.Accountsbootstrap.PaymentNew" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Customer Payment</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <%-- <style type="text/css">
   .odd gradeX
   {
       margin-top:-2000px;
   }
   </style>--%>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
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
    <%--<script language="javascript" type="text/javascript">
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
                event.returnValue = true;

            else

                event.returnValue = false;

        }
        function validate() {
            var ledgerType = document.getElementById("ddlType").value;
            var LedgerName = document.getElementById("ddlLName").value;
            var Address = document.getElementById("txtAddress").value;
            var Area = document.getElementById("txtArea").value;
            var City = document.getElementById("txtCity").value;
            var PaymentNo = document.getElementById("txtPaymentNo").value;
            var pdate = document.getElementById("txtpdate").value;
            var PMode = document.getElementById("ddlPMode").value;
            var Amount = document.getElementById("txtAmount").value;
            var Narration = document.getElementById("txtNarration").value; 
            var BankName = document.getElementById("ddlBankName").value;
            var ChequeNo = document.getElementById("txtChequeNo").value;

//           if (ledgerType == "Customer" && LedgerName == "Select LedgerName" && Address == "" && Area == "" && City == "" && PaymentNo == "13" && pdate == "11/07/2015" && PMode == "Select Type" && Amount == "" && Narration == "" && BankName == "Select Bank Name" && ChequeNo == "") {
//              alert("Enter All Fields");
//               return false;
//           }


if (LedgerName == "Select LedgerName" && Address == "" && Area == "" && City == ""  && PMode == "Select Type" && Amount == "" && Narration == "" && BankName == "Select Bank Name" && ChequeNo == "") {
    alert("Enter All Fields Ledger name,Address,Area,City, select Payment Mode");
              return false;
          }






            if (ledgerType == "Select Type") {
                
                alert("select Ledger Type");
                return false;
            }
           

           


            if (PMode == "2" || PMode == "3") {
              
                if (BankName == "Select Bank Name") {
                    alert("select bank name");
                    return false;
                }
            
                if (ChequeNo == "") {
                    alert("Enter Check no.");

                    return false;
                }


            }
         else  if (PMode == "Select Type" ) {
               alert("select Payment Mode Type");
               return false;
           }
           
          
        
             
            
        }
         </script>--%>
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
    <link rel="stylesheet" href="../css/chosen.css" />
</head>
<body>
    <usc:header id="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
      <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1,5"> </asp:Label>
    <div class="row">
        <div class="col-lg-12">
            <%--    <h1 class="page-header"style="text-align:center;color:#fe0002;">Payment Master</h1>--%>
            <h2 style="text-align: left; color: #fe0002; margin-top: -2px">
                <asp:Label ID="lblTitle" Text="Payment" runat="server"></asp:Label></h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <form id="Form1" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-2">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                            <asp:TextBox ID="txtpaymentid" runat="server" Visible="false"></asp:TextBox>
                            <div class="form-group">
                                <label>
                                    Payment No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" Text="*"
                                    ControlToValidate="txtPaymentNo" ErrorMessage="Please enter your Payment No!"
                                    Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtPaymentNo" MaxLength="15" runat="server"
                                    Enabled="true" OnTextChanged="txtpaymentnochnaged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <div class="form-group">
                                    <label>
                                        Payment Date</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator5"
                                        Text="*" ControlToValidate="txtpdate" ErrorMessage="Please select Date !" Style="color: Red" />
                                    <asp:TextBox CssClass="form-control" onkeyup="ValidateDate(this, event.keyCode)"
                                        onkeydown="return DateFormat(this, event.keyCode)" ID="txtpdate" runat="server"
                                        Text="--Select Date--"></asp:TextBox>
                                </div>
                                <ajaxtoolkit:calendarextender id="CalendarExtender1" targetcontrolid="txtpdate" runat="server"
                                    format="dd/MM/yyyy" cssclass="cal_Theme1">
                                </ajaxtoolkit:calendarextender>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                <label>
                                    Ledger Type</label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlType" ValueToCompare="Select Type"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Ledger type"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlType" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>
                                    Ledger Name</label>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlLName" ValueToCompare="Select LedgerName"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Ledger Name"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlLName" CssClass="chzn-select" Width="193px" Height="60px"
                                    runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                <label>
                                    Address</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="" id="RequiredFieldValidator1" Text="*" controltovalidate="txtAddress" errormessage="Please enter your Address!" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtAddress" MaxLength="15" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                <label>
                                    Area</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="" id="RequiredFieldValidator2" Text="*" controltovalidate="txtArea" errormessage="Please enter your Area!" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtArea" MaxLength="15" runat="server"></asp:TextBox>
                            </div>
                            
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
                                    <asp:ListItem Text="Inner(CGST/SGST)" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Others(IGST)" Value="2"  ></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>
                                    Amount</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                    ControlToValidate="txtgstAmount" Text="*" ErrorMessage="Please enter your Amount!"
                                    Style="color: Red" />
                                <ajaxtoolkit:filteredtextboxextender id="FilteredTextBoxExtender11" runat="server"
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
                            <div class="form-group" id="div1" runat="server" visible="false">
                                <label>
                                    City</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="" id="RequiredFieldValidator3" Text="*" controltovalidate="txtCity" errormessage="Please enter your City!" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" ID="txtCity" MaxLength="15" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Net Amount</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="city" ControlToValidate="txtAmount"
                                    Text="*" ErrorMessage="Please enter your Amount!" Style="color: Red" />
                                <ajaxtoolkit:filteredtextboxextender id="FilteredTextBoxExtender1" runat="server"
                                    filtertype="Numbers,Custom" validchars="." targetcontrolid="txtAmount" />
                                <asp:TextBox CssClass="form-control" ID="txtAmount" MaxLength="15" AutoPostBack="true"
                                    runat="server" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Narration</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="Narration"
                                    Text="*" ControlToValidate="txtNarration" ErrorMessage="Please enter your narration"
                                    Style="color: Red" />
                                <ajaxtoolkit:filteredtextboxextender id="FilteredTextBoxExtender4" runat="server"
                                    filtertype="LowercaseLetters, UppercaseLetters,Numbers,Custom" validchars=" +,!@#$%^&*()-/:;."
                                    targetcontrolid="txtNarration" />
                                <asp:TextBox CssClass="form-control" ID="txtNarration" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    Payment Mode</label>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPMode" ValueToCompare="Select Paymode Type"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Payment Mode"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlPMode" runat="server" class="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPMode_SelectedIndexChanged">
                                </asp:DropDownList>
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
                            <div class="form-group"  runat="server" visible="false">
                                <label>
                                    Bank Name</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator4"
                                    Text="*" ControlToValidate="ddlBankName" ErrorMessage="Please enter your name!"
                                    Style="color: Red" />
                                <asp:DropDownList ID="ddlBankName" runat="server" CssClass="chzn-select" Width="193px"
                                    Height="60px" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group"  runat="server" visible="false">
                                <label>
                                    Cheque No
                                </label>
                                <asp:DropDownList ID="ddlChequeNo" runat="server" class="form-control">
                                </asp:DropDownList>
                                <%--
                                       <asp:TextBox CssClass="form-control" ID="txtChequeNo" MaxLength="6" onkeypress="NumberOnly()" runat="server"></asp:TextBox>--%>
                            </div>
                            <div class="form-group"  runat="server" visible="false">
                                <label>
                                    UTR No
                                </label>
                                <%--  <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                </asp:DropDownList>--%>
                                <asp:TextBox CssClass="form-control" ID="txtutr" MaxLength="6" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group" runat="server" visible="false">
                                <%-- <label id="lblAganistBno">Against</label>--%>
                                <asp:Label ID="Label1" Text="Against Bill No" runat="server" Style="font-weight: bold"></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtAgainst" MaxLength="10" runat="server"></asp:TextBox>
                                <ajaxtoolkit:filteredtextboxextender id="FilteredTextBoxExtender2" runat="server"
                                    filtertype="Numbers" validchars="" targetcontrolid="txtAgainst" />
                            </div>
                             <div runat="server" id="narra" visible="false" class="form-group">
                                    <label>
                                        Enter Narration
                                    </label>
                                    <asp:TextBox ID="txteditnarration" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                        </div>
                        <asp:Button ID="gridbutton" runat="server" Text="View grid" OnClick="gridbutton_Click" />
                        <asp:HiddenField ID="ldgID" runat="server" Value="New" />
                        <%--<div class="col-lg-12">
                             
                                 <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" style="margin-top:30PX" id="Table1">
                                        <thead>
                                        <tr>
                                        <th>Payment Mode</th>
                                            <th>Bank Name</th>
                                            <th> Reference No</th>
                                        </tr></thead>
                                         <tbody>
                                         <tr class="odd gradeX">
                                         <td> <asp:DropDownList CssClass="form-control" ID="ddmodeofpayment" runat="server"> 
                                                    
                                                   </asp:DropDownList></td>
                                            <td><asp:TextBox ID="txtbankname" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtrefno" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox></td>
                                         </tr>
                                         </tbody>
                                         </table>
                                        <asp:Label ID="Label1" runat="server"  style="color:Red"></asp:Label>
                                        </div>
                                </div>--%>
                        <%-- <div class="col-lg-1" >
                                          </div>

                                         <div  style="margin-top:500px"> --%>
                        <%-- <asp:GridView ID="GridPurchase"  runat="server" CssClass="myGridStyle" AllowPaging="true" 
                                        PageSize="10" AllowSorting="true"    EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" >
                             <HeaderStyle BackColor="#3366FF"  />
                              <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Previous" />
    <Columns>

                                    <asp:BoundField HeaderText="Invoice NO" DataField="DC_NO" />
                                    <asp:BoundField HeaderText="Bill NO" DataField="Bill_NO" />
                                    <asp:BoundField HeaderText="Bill Date" DataField="DC_Date" DataFormatString="{0:d}"  />
                                    <asp:BoundField HeaderText="TotalAmount1" DataField="TotalAmount"    />
                                      <asp:BoundField HeaderText="Balance" DataField="TotalAmount"    />
                                       <asp:TemplateField HeaderText="Amount1">
<ItemTemplate>
<asp:TextBox ID="txtPayAmount"  runat="server" ></asp:TextBox>          
</ItemTemplate>
</asp:TemplateField>
                                   
                                    </Columns>
                            
   
   </asp:GridView>  --%>
                        <div class="row">
                            <br />
                            <div class="col-lg-12">
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-8">
                                    <div class="table-responsive" runat="server" style="height: 100px; overflow: auto;
                                        margin-top: 20px">
                                        <asp:GridView ID="TransPaymentGrid" runat="server" EmptyDataText="No records Found"
                                            Width="87%" AllowPaging="false" AutoGenerateColumns="false" CssClass="myGridStyle"
                                            Style="margin-right: 200px" AllowSorting="true">
                                            <HeaderStyle BackColor="#59d3b4" Height="30px" ForeColor="Black" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black"
                                                Height="30px" />
                                            <PagerSettings FirstPageText="1" Mode="Numeric" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Invoice No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtDCNo" runat="server" Text='<%# Eval("DC_NO")%>'></asp:Label>
                                                         <asp:Label ID="txtPID" runat="server" Visible="false" Text='<%# Eval("P_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBillNo" runat="server" Text='<%# Eval("Bill_NO")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBillDate" runat="server" Text='<%# Eval("DC_Date")%>' DataFormatString="{0:dd/MM/yyyy}"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBillAmount" runat="server" Text='<%# Eval("BillAmount")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="PurchaseReturn">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPurchaseReturn" runat="server" Text='<%# Eval("PurchaseReturnAmount")%>'></asp:Label>
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
                                        <%--</div>
                                 <div class="col-lg-4" >
                                          </div>--%>
                                    </div>
                                    <br />
                                    <div style="text-align: center; margin-top: 20px; margin-left: 0px">
                                        <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" ValidationGroup="val1"
                                            Style="width: 120px;" OnClick="btnadd_Click" />
                                        <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" Style="width: 120px"
                                            OnClick="btnexit_Click1" />
                                    </div>
                                </div>
                            </div>
                            <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
                            <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
                            <script type="text/javascript">
                                window.onload = function () {
                                    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                                }
                            </script>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                    <!-- /.row (nested) -->
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
    </form>
    <!-- /.row -->
    <!-- /#page-wrapper -->
    <!-- jQuery -->
</body>
</html>
