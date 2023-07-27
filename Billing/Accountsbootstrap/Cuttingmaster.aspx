<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cuttingmaster.aspx.cs"
    Inherits="Billing.Accountsbootstrap.Cuttingmaster" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Cutting Registration</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <%-- <script type="text/javascript" language="javascript">
        function valchk() {
            if (blankchk(document.getElementById('txtcustomername'), "Customer Name")
            //&& dropdownchk(document.getElementById('ddlgroup'), "Account Group")  
        && phonechk(document.getElementById('txtmobileno'), "MobileNo") && phonechk(document.getElementById('txtphoneno'), "PhoneNo")
        && blankchk(document.getElementById('txtblnce'), "Opening Balance") 
        && blankchk(document.getElementById('txtmobileno'), "MobileNo")
        && blankchk(document.getElementById('txtphoneno'), "Phone No") && blankchk(document.getElementById('txtarea'), "Area")
        && blankchk(document.getElementById('txtaddress'), "Address") && blankchk(document.getElementById('txtcity'), "City")
        && emailchk(document.getElementById('txtemail'), "Email")) {
                alert("true");
            }
            else {
                alert("false");
                return false;
            }
        }
	</script>--%>
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
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12">
            <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">
            </h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <form id="Form1" runat="server">
                    <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group">
                                <div id="add" runat="server" class="row">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                        ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                    <%-- <div class="col-lg-2">
                        </div>--%>
                                    <div class="col-lg-2">
                                        <div class="form-group" id="divcode" runat="server">
                                            <label>
                                                Ledgerid</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Contact ID</label>
                                            <asp:TextBox CssClass="form-control" Width="153px" Style="margin-left: -15px" ID="txtCustomerid"
                                                runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-group" runat="server" visible="false" style="margin-top: 0px">
                                            <label id="Label1" runat="server">
                                                Inital</label>
                                            <asp:TextBox CssClass="form-control" Style="text-transform: uppercase" ID="TextBox1"
                                                runat="server" Width="140px"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Cutting Name</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="reqName" Text="*"
                                                ControlToValidate="txtcustomername" ErrorMessage="Please enter your name!" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtcustomername" MaxLength="50" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" ."
                                                TargetControlID="txtcustomername" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Print Name</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                                                Text="*" ControlToValidate="txtprintname" ErrorMessage="Please enter your Print name!"
                                                Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtprintname" MaxLength="50" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" ."
                                                TargetControlID="txtprintname" />
                                        </div>
                                        <div class="form-group" runat="server" visible="false">
                                            <label>
                                                Phone No</label>
                                            <asp:TextBox CssClass="form-control" ID="txtphoneno" MaxLength="12" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                FilterType="Numbers,Custom" ValidChars=" -" TargetControlID="txtphoneno" />
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                Area</label>
                                            <asp:TextBox CssClass="form-control" ID="txtarea" MaxLength="30" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                Delivery Address</label>
                                            <asp:TextBox CssClass="form-control" ID="txtDelivery" MaxLength="150" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                Designation</label>
                                            <asp:TextBox CssClass="form-control" ID="txtDesignation" MaxLength="150" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                City</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcity" MaxLength="30" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!-- /.col-lg-6 (nested) -->
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group" runat="server" visible="false">
                                            <label>
                                                Pincode</label>
                                            <asp:TextBox CssClass="form-control" ID="txtpincode" MaxLength="6" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                FilterType="Numbers" ValidChars="" TargetControlID="txtpincode" />
                                        </div>
                                        <div class="form-group" runat="server" visible="false">
                                            <label>
                                                E-mail</label>
                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ValidationGroup="val1"
                                                Text="*" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorMessage="Please enter a correct Email Id!" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtemail" placeholder="For Ex: test@gmail.com"
                                                runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars="_-@."
                                                TargetControlID="txtemail" />
                                        </div>
                                        <div id="Div1" class="form-group" runat="server" visible="false">
                                            <asp:Label ID="Label2" Visible="false" runat="server">
                                    Contact Type</asp:Label>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCustomerType"
                                                ValueToCompare="Select Contact Type" Operator="NotEqual" Type="String" ErrorMessage="Please Select Contact type"></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlCustomerType" runat="server" AutoPostBack="true" class="form-control"
                                                Visible="false" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group" runat="server" visible="false">
                                            <label style="margin-left: -15px">
                                                Opening Balance</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                                ControlToValidate="txtOBalance" Text="*" ErrorMessage="Please enter your opening balance amount!"
                                                Style="color: Red" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtOBalance" />
                                            <asp:TextBox CssClass="form-control" ID="txtOBalance" runat="server" MaxLength="8"
                                                Style="text-align: right; width: 140px;">0</asp:TextBox>
                                        </div>
                                        <div class="col-lg-1">
                                        </div>
                                        <div class="form-group" runat="server" visible="false">
                                            <label>
                                                Choose Type</label>
                                            <asp:DropDownList ID="ddlCDType" runat="server" class="form-control" Width="140px">
                                                <asp:ListItem Text="Credit" Value="Credit Note"></asp:ListItem>
                                                <asp:ListItem Text="Debit" Value="Debit Note"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group" id="Div2" runat="server">
                                            <label>
                                                Is Active</label>
                                            <asp:DropDownList ID="ddlIsActive" runat="server" class="form-control">
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group" id="Div4" runat="server">
                                            <label>
                                                Mobile No</label>
                                            <div class="form-group input-group">
                                                <span class="input-group-addon">+91</span>
                                                <asp:TextBox CssClass="form-control" ID="txtmobileno" MaxLength="10" runat="server"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Numbers" ValidChars="" TargetControlID="txtmobileno" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Address</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="address" ControlToValidate="txtaddress"
                                                Text="*" ErrorMessage="Please enter your Address!" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtaddress" MaxLength="150" runat="server"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div id="Div42" class="form-group" runat="server" visible="false">
                                            <label>
                                                Tin No</label>
                                            <asp:TextBox CssClass="form-control" ID="txtTinNO" MaxLength="30" Text="0" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                            <label>
                                                LotNo Starts From</label>
                                            <asp:TextBox CssClass="form-control" ID="txtLotNo" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Salary Type</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlSalary"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please Select Salary Type!" Style="color: Red" />
                                            <asp:DropDownList runat="server" ID="ddlSalary" Width="100%" CssClass="form-control">
                                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Monthly" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Piece Wise" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                Selling Price</label>
                                            <asp:DropDownList ID="ddlPrice" runat="server" class="form-control">
                                                <asp:ListItem Text="MRP" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="DSP" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="WSP" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                CST</label>
                                            <asp:TextBox CssClass="form-control" ID="txtCST" runat="server">CST</asp:TextBox>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                PAN
                                            </label>
                                            <asp:TextBox CssClass="form-control" ID="txtPAN" runat="server">PAN</asp:TextBox>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                Address Proof
                                            </label>
                                            <asp:TextBox CssClass="form-control" ID="txtAddressProof" runat="server">add</asp:TextBox>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                ID Proof
                                            </label>
                                            <asp:TextBox CssClass="form-control" ID="txtIDProof" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                Birth Date
                                            </label>
                                            <asp:TextBox CssClass="form-control" ID="txtdob" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdob" PopupButtonID="txtdate1"
                                                EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="form-group" style="display: none">
                                            <label>
                                                Anniversary Date
                                            </label>
                                            <asp:TextBox CssClass="form-control" ID="txtAnniversary" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtAnniversary"
                                                PopupButtonID="txtdate1" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                                CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="form-group" runat="server" visible="false">
                                            <label>
                                                Credit Days
                                            </label>
                                            <asp:TextBox CssClass="form-control" ID="txtCreditDays" runat="server">0</asp:TextBox>
                                        </div>
                                        <div class="form-group" runat="server" visible="false">
                                            <label>
                                                Select Agent
                                            </label>
                                            <asp:DropDownList ID="drpagent" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                        <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                                        <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group" runat="server" visible="false">
                                            <div class="col-lg-4">
                                                Item Name</label>
                                                <asp:DropDownList ID="drpitemname" Width="100%" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:Button ID="btnprocess" Text="Save" runat="server" class="btn btn-info" OnClick="process_click" />
                                            </div>
                                            <div class="col-lg-8">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                Cost-Per Qty
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtcostperqty" runat="server" CssClass="form-control">0</asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Costing Details
                                            </label>
                                            <asp:TextBox CssClass="form-control" onkeyup="Search_Gridview(this, 'gridprocesstype')"
                                                Enabled="true" ID="txtsearch" runat="server" placeholder="Search Item" Width="250px"></asp:TextBox>
                                            <asp:GridView ID="gridprocesstype" OnRowDeleting="GridView2_RowDeleting" runat="server"
                                                CssClass="chzn-container" GridLines="Both" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Item name" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemid" Visible="false" Text='<%# Eval("Itemid")%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblitemname" Text='<%# Eval("Itemname")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cost Per Qty" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblcutrate" Text='<%# Eval("cutrate")%>' runat="server"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender452" runat="server"
                                                                FilterType="Numbers,Custom" ValidChars="." TargetControlID="lblcutrate" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField HeaderStyle-Width="2%" ShowDeleteButton="True" ButtonType="Button"
                                                        Visible="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <!-- /.col-lg-6 (nested) -->
                                    <!-- /.col-lg-6 (nested) -->
                                </div>
                                <div id="div3" runat="server" align="center">
                                    <table cellpadding="1" cellspacing="2" width="450px" style="border: 1px solid black;
                                        height: 250px;">
                                        <tr style="height: 30px">
                                            <td>
                                                <table width="450px" style="border: 1px solid black; height: 36px;">
                                                    <tr>
                                                        <td id="Td1" runat="server" colspan="4">
                                                            <asp:RadioButtonList ID="masterradio" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                                                                <asp:ListItem style="padding-left: 10px" Text="Customer" Value="1"></asp:ListItem>
                                                                <asp:ListItem style="padding-left: 10px" Text="Vendor" Value="6"></asp:ListItem>
                                                                <asp:ListItem style="padding-left: 10px" Text="Dealer" Value="2"></asp:ListItem>
                                                                <asp:ListItem style="padding-left: 10px" Text="Service Center" Value="5"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 30%">
                                                        </td>
                                                        <td style="width: 35%">
                                                            <div>
                                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                <asp:GridView ID="GridView1" runat="server">
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                        <td style="width: 35%">
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 6px">
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 30%">
                                                                    </td>
                                                                    <td style="width: 35%" align="center">
                                                                        <asp:Button ID="btnUpload" runat="server" Height="31px" class="btn btn-info" Text="Upload"
                                                                            Width="100px" OnClick="btnUpload_Click" />
                                                                    </td>
                                                                    <td style="width: 35%">
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 10px">
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 15%">
                                                                    </td>
                                                                    <td style="width: 70%" align="center">
                                                                        <asp:Button ID="Button2" runat="server" class="btn btn-info" Text="Download the Sample Excel Format"
                                                                            Height="31px" OnClick="btnFormat_Click" />
                                                                        <asp:Button ID="Button1" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click" />
                                                                    </td>
                                                                    <td style="width: 15%">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-lg-12" style="margin-left: 280px">
                                    <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" OnClick="Add_Click"
                                        ValidationGroup="val1" Style="width: 120px; margin-top: 25px" />
                                    <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"
                                        Style="width: 120px; margin-top: 25px" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="Updateprogress" runat="server" AssociatedUpdatePanelID="Updatepanel1">
                        <ProgressTemplate>
                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                                right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                                    AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                                    position: fixed; top: 50%; left: 40%;" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    </form>
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
</body>
</html>
