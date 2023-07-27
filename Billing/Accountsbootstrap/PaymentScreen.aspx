<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentScreen.aspx.cs" Inherits="Billing.Accountsbootstrap.PaymentScreen" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Employee Payment</title>
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
    <script type = "text/javascript">
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

      <link rel="stylesheet" href="../Styles/chosen.css" />
</head>
<body>

    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12">
            <%--    <h1 class="page-header"style="text-align:center;color:#fe0002;">Payment Master</h1>--%>
            <h2  style="text-align: left; color: #fe0002;margin-top:-2px">
                <asp:Label ID="lblTitle" Text="Payment" runat="server"></asp:Label></h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2">
                        </div>
                        <div class="col-lg-3">
                            <form id="Form1" runat="server">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />

                                 <div class="form-group">
                                <label>
                                    Payment No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" Text="*"
                                    ControlToValidate="txtPaymentNo" ErrorMessage="Please enter your Payment No!"
                                    Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtPaymentNo" MaxLength="15" runat="server"
                                    Enabled="false"></asp:TextBox><asp:TextBox ID="TextBox1" Visible="false" runat="server"
                                    Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <div class="form-group">
                                    <label>
                                        Payment Date</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator5"
                                        Text="*" ControlToValidate="txtpdate" ErrorMessage="Please select Date !" Style="color: Red" />
                                    <asp:TextBox CssClass="form-control" onkeyup = "ValidateDate(this, event.keyCode)"
 onkeydown = "return DateFormat(this, event.keyCode)"
 ID="txtpdate" runat="server" Text="--Select Date--"></asp:TextBox>
                                </div>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtpdate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>

                            <div class="form-group">
                                <label>
                                     Name</label>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlEName" ValueToCompare="Select Employee Name"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Ledger Name"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlEName" CssClass="chzn-select form-control"  runat="server" class="form-control" AutoPostBack="true"
                                    >
                                </asp:DropDownList>
                            </div>
                            
                            <div class="form-group">
                                <label>
                                   LotNo</label>
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtLotNo" MaxLength="15" runat="server"
></asp:TextBox><asp:Label ID="txtLotDetailId" runat="server" Visible="false"></asp:Label>

                            </div> 
                          
                              </div>

                        <div class="col-lg-3">
                              <div class="form-group">
                                <label>
                                   Process</label>
                                  <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="DpProcess" ValueToCompare="Select Process Name"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Process Name"></asp:CompareValidator>
                                <asp:DropDownList ID="DpProcess" Enabled="false" CssClass="chzn-select form-control"  runat="server" class="form-control" AutoPostBack="true"
                                    >
                                </asp:DropDownList>

                            </div>
                            <div class="form-group">
                                <label>
                                   TotalAmount</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="" id="RequiredFieldValidator1" Text="*" controltovalidate="txtAddress" errormessage="Please enter your Address!" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtTotalAmount" MaxLength="15" runat="server"
></asp:TextBox>
                               <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtFromDate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>--%>

                            </div>

                            <div class="form-group">
                                <label>
                                    PaidAmount</label>
                                <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="" id="RequiredFieldValidator2" Text="*" controltovalidate="txtArea" errormessage="Please enter your Area!" style="color:Red" />--%>
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtPaidAmount" MaxLength="15" runat="server"
 ></asp:TextBox>
                                <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtToDate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>--%>
                                <asp:Button ID="btlprcess" runat="server" OnClick="Process_CIclk" Visible="false" Text="Process" />
                            </div>
                            <div class="form-group">
                                <label>
                                    Amount</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="city" ControlToValidate="txtAmount"
                                    Text="*" ErrorMessage="Please enter your Amount!" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtAmount" MaxLength="8" AutoPostBack="true"
                                    runat="server" onkeypress="return NumberOnly()" ></asp:TextBox>
                            </div>
                            </div>

                        <div class="col-lg-3">
                           
                            
                            <div class="form-group">
                                <label>
                                    Narration</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="Narration"
                                    Text="*" ControlToValidate="txtNarration" ErrorMessage="Please enter your narration"
                                    Style="color: Red" />
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" +,!@#$%^&*()-/:;."  TargetControlID="txtNarration" />
                                <asp:TextBox CssClass="form-control" ID="txtNarration" runat="server"></asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label>
                                    Payment Mode</label>
                                <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPMode" ValueToCompare="Select Paymode Type"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Payment Mode"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlPMode" runat="server" class="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPMode_SelectedIndexChanged">
                                </asp:DropDownList>--%>

                                <asp:DropDownList ID="ddlPayment" runat="server" CssClass="form-control" >
                                <asp:ListItem Text="Cash" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Cheque" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>
                                   BankName </label>
                         
                                <asp:TextBox CssClass="form-control" ID="txtbank"
                                    runat="server" ></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                   Cheque </label>
                         
                                <asp:TextBox CssClass="form-control" ID="txtcheque"
                                    runat="server" ></asp:TextBox>
                            </div>
                            </div>
                                      
   
                <!-- /.col-lg-6 (nested) -->
                <!-- /.col-lg-6 (nested) -->
            </div>
            <div class="row">
                        <div class="col-lg-3">
                        <asp:Button runat="server" ID="save"  OnClick="Add_Click"   class="btn btn-info"  Width="120px" Text="Save"/>
                        <asp:Button runat="server" ID="exit" OnClick="btnExit_Click" class="btn btn-danger" Text="Exit" Width="120px"/>
                        </div>
                        </div>
            <!-- /.row (nested) -->
        </div>
        <!-- /.panel-body -->
    </div>
    <!-- /.panel -->
    </div>
    <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <!-- /#page-wrapper -->
    <!-- jQuery -->

    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
       <script type="text/javascript">
           window.onload = function () {
               $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
           }
    </script>
    </form>
</body>
</html>
