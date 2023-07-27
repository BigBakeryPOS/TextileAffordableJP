<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="itempage.aspx.cs" Inherits="Billing.Accountsbootstrap.itempage" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Accessories Page - bootsrap</title>
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
                alert("false"); i
                return false;
            }
        }
        

    </script>
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
        function myFunction() {
            window.open("http://localhost:57111/Accountsbootstrap/customermaster.aspx?Mode=Vendor", "Popup", 'width=300,height=500,left=100,top=100,resizable=yes,modal=yes,center=yes');
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
            <h2 id="hd1" runat="server" class="page-header" style="text-align: left; color: #fe0002">
            </h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <form id="Form1" runat="server" method="post">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="selected_tab" runat="server" />
                            <asp:Button ID="Button3" runat="server" Text="Do PostBack" Visible="false" />
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <div id="add" runat="server" class="form-group">
                                <div class="row">
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                        <div runat="server" visible="false" class="form-group">
                                            <label>
                                                Sales Type</label>
                                            <asp:RadioButtonList ID="rbdtype" RepeatDirection="Horizontal" RepeatColumns="2"
                                                runat="server">
                                                <asp:ListItem Enabled="false" Text="Sales" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Purchase" Selected="True" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group ">
                                            <label>
                                                Select Branch</label>
                                            <asp:DropDownList ID="drpbranch" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Category</label>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlcategory"
                                                ValueToCompare="0" Operator="NotEqual" Type="String" ErrorMessage="Please Select Category"></asp:CompareValidator>
                                            <asp:DropDownList runat="server" ID="ddlcategory" class="form-control">
                                                <asp:ListItem Text="select Category" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Select UOM
                                            </label>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlmeter" ValueToCompare="Select Meter"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please Select Meter"></asp:CompareValidator>
                                            <asp:DropDownList CssClass="form-control" ID="ddlmeter" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label style="margin-top: -10px">
                                                Accessories Code</label>
                                            <asp:RequiredFieldValidator runat="server" ID="txtcat" ControlToValidate="txtPCode"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please enter Product Code!" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtPCode" runat="server" MaxLength="150"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars="-() "
                                                TargetControlID="txtPCode" />
                                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label style="margin-top: -100px">
                                                Accessories Name</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPName"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Items Description!"
                                                Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtPName" runat="server" MaxLength="150"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" .,/\@#$%&*()!-"
                                                TargetControlID="txtPName" />
                                        </div>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group" style="margin-top: -15px">
                                            <label style="margin-top: 15px">
                                                Is Active</label>
                                            <asp:DropDownList CssClass="form-control" ID="ddlIsActive" runat="server">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label style="margin-top: 3px">
                                                Tax
                                            </label>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddltax" ValueToCompare="0"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please Select Tax"></asp:CompareValidator>
                                            <asp:DropDownList CssClass="form-control" ID="ddltax" runat="server">
                                                <asp:ListItem Text="Select Tax" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="0" Selected="True" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="28" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Minimum Stock Alert</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtDis"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please enter Discount!" Style="color: Red" />
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Double" ValidationGroup="val1"
                                                MinimumValue="0" Text="*" MaximumValue="100" ControlToValidate="txtDis" ErrorMessage="Value must be 0 to 100"
                                                Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtDis" MaxLength="5" runat="server">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtDis" />
                                        </div>
                                        <div class="form-group" id="purchase" runat="server" visible="false">
                                            <label>
                                                Purchase</label>
                                            <asp:CheckBox ID="chkPurchsse" runat="server" />
                                        </div>
                                        <div class="col-lg-10">
                                            <label>
                                                File Upload</label>
                                            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                                <ContentTemplate>
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
                                            <br />
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>
                                                Purchase Price</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtpurchaseprice"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Purchase price!" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtpurchaseprice" runat="server" MaxLength="150">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                FilterType="Numbers,custom" ValidChars="." TargetControlID="txtpurchaseprice" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                MRP</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtmrp"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please Enter MRP!" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtmrp" runat="server" MaxLength="150">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                FilterType="Numbers,custom" ValidChars="." TargetControlID="txtmrp" />
                                        </div>
                                        <div runat="server" visible="false" class="form-group">
                                            <label>
                                                Dealer Price</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtdelearprice"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please Enter dealer Price.!" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtdelearprice" runat="server" MaxLength="150">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                FilterType="Numbers,custom" ValidChars="." TargetControlID="txtdelearprice" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Commodity Code</label>
                                            <asp:TextBox CssClass="form-control" ID="txtcommodity" runat="server" MaxLength="150">0</asp:TextBox>
                                        </div>
                                        <div runat="server" visible="false" class="form-group">
                                            <label>
                                                HSNCode</label>
                                            <asp:TextBox CssClass="form-control" ID="txthsncode" Text="0" runat="server" MaxLength="150">0</asp:TextBox>
                                        </div>
                                        <div runat="server" visible="false" class="form-group">
                                            <label>
                                                Opening Stock</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtopeningstock"
                                                ValidationGroup="val1" Text="*" ErrorMessage="Please Enter opening Stock!" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" Visible="false" ID="txtopeningstock" runat="server"
                                                MaxLength="150">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                FilterType="Numbers" ValidChars="" TargetControlID="txtopeningstock" />
                                            <asp:TextBox CssClass="form-control" Visible="false" ID="txtopstockid" runat="server">0</asp:TextBox>
                                        </div>
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-info" ValidationGroup="val1"
                                            Text="Save" Style="width: 120px; margin-top: 15px" OnClick="btnadd_Click" />
                                        <asp:Button ID="btnexit" runat="server" class="btn  btn-warning" Style="width: 120px;
                                            margin-top: 15px" Text="Exit" OnClick="btnexit_Click" />
                                    </div>
                                    <%--<button type="submit" class="btn btn-success" onclick="Add_Click">Add</button>--%>
                                </div>
                            </div>
                            <div id="div1" runat="server" align="center">
                                <table cellpadding="1" cellspacing="2" width="450px" style="border: 1px solid blue;
                                    height: 150px;">
                                    <tr class="headerPopUp">
                                        <td id="Td1" runat="server" colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table style="width: 100%">
                                                <tr style="height: 15px">
                                                </tr>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </form>
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
    <!-- /.row -->
    </div>
    <!-- /#page-wrapper -->
</body>
</html>
