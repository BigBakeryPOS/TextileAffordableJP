<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fabricprocess.aspx.cs"
    Inherits="Billing.Accountsbootstrap.Fabricprocess" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Fabric Process</title>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <script src="" type="text/javascript"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        //        function valchk() {
        //            if (blankchk(document.getElementById('txtBrandname'), "Cheque Name")
        //            {
        //                alert("true");
        //            }
        //            else {
        //                alert("false");
        //                return false;
        //            }
        //        }
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header" style="text-align: center; color: #fe0002;">
                Fabric Process</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <form id="Form1" runat="server">

                        <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        
                       
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-3">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="form-group" id="divcode" runat="server">
                                <asp:TextBox CssClass="form-control" ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div runat="server" visible="false" class="form-group ">
                                <label>
                                    ID</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator3"
                                    ControlToValidate="TextBox3" ErrorMessage="Please enter ID" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox3" runat="server"></asp:TextBox>
                            </div>
                             <div id="single" runat="server" class="form-group ">
                                <label>
                                    Supplier Name</label>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlSupplier"
                                    ValueToCompare="Select Supplier Name" Operator="NotEqual" Type="String" ErrorMessage="Please select Supplier name!"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlSupplier" Enabled="false" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group ">
                                <label>
                                    Checked Sign</label>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlSupplier"
                                    ValueToCompare="Select Employee Name" Operator="NotEqual" Type="String" ErrorMessage="Please select Employee name!"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlEmployee" Enabled="false" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group ">
                                <label>
                                    Width</label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlWidth" ValueToCompare="Select Width"
                                    Operator="NotEqual" Type="String" ErrorMessage="Please select Width!"></asp:CompareValidator>
                                <asp:DropDownList ID="ddlWidth" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            
                            <div id="Div2" class="form-group" runat="server">
                                <label>
                                    Invoice No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator10"
                                    ControlToValidate="txtinvoiceNo" ErrorMessage="Please enter Invoice No" Style="color: Red" />
                                <asp:TextBox CssClass="form-control"  Enabled="false" ID="txtinvoiceNo" MaxLength="6" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group" runat="server" visible="false">
                                <label>
                                    Challen No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator6"
                                    ControlToValidate="txtChallenNo" ErrorMessage="Please enter Challen No" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtChallenNo" MaxLength="6" runat="server"></asp:TextBox>
                            </div>
                            
                        </div>
                        <div class="col-lg-3">
                        <div class="form-group">
                                <label>
                                    Date:</label>
                                <asp:TextBox ID="txtdate" runat="server" Enabled="false" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdate" runat="server"
                                    Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                            <div class="form-group ">
                                <label>
                                    Meter</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator2"
                                    ControlToValidate="txtMeter" ErrorMessage="Please enter Meter" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtMeter" MaxLength="6" runat="server"></asp:TextBox>
                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Numbers" ValidChars="" TargetControlID="txtMeter" />
                            </div>

                            <div class="form-group ">
                                <label>
                                    Available Meter</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator7"
                                    ControlToValidate="txtMeter" ErrorMessage="Please Available enter Meter" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtAvailableMeter" MaxLength="6" runat="server"></asp:TextBox>
                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="Numbers" ValidChars="" TargetControlID="txtAvailableMeter" />
                            </div>

                            <div class="form-group ">
                                <label>
                                    Rate</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator4"
                                    ControlToValidate="txtRate" ErrorMessage="Please enter Rate" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtRate" MaxLength="6" runat="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    FilterType="Numbers,custom" ValidChars="." TargetControlID="txtRate" />
                            </div>
                            </div>
                             <div class="col-lg-3">

                            <div class="form-group" runat="server" visible="false">
                                <label>
                                    RM Code</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator5"
                                    ControlToValidate="txtRMCode" ErrorMessage="Please enter RMCode" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtRMCode" MaxLength="6" runat="server"></asp:TextBox>
                            </div>
                            
                            <div class="form-group ">
                                <label>
                                    Design No</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator1"
                                    ControlToValidate="txtDNo" ErrorMessage="Please enter DNo" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtDNo" MaxLength="6" runat="server" OnTextChanged="txtDNo_TextChanged"  AutoPostBack="true" ></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="UppercaseLetters,lowercaseletters,numbers,custom" ValidChars="" TargetControlID="txtDNo" />
                            </div>

                            <div class="form-group ">
                                <label>
                                    Colour</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator8"
                                    ControlToValidate="txtcolour" ErrorMessage="Please enter Color" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtcolour" MaxLength="6" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group ">
                                <label>
                                    Piece</label>
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator9"
                                    ControlToValidate="txtcolour" ErrorMessage="Please enter Piece" Style="color: Red" />
                                <asp:TextBox CssClass="form-control" ID="txtpiece" MaxLength="6" runat="server"></asp:TextBox>
                            </div>

                            <div class="form-group" runat="server" visible="false">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                                    <ContentTemplate>
                                        <asp:FileUpload ID="FileUpload2" runat="server" Width="250px" CssClass="btn btn-danger" />
                                        <asp:Button ID="btnUpload1" runat="server" Text="Upload" CssClass="btn btn-danger"
                                            BackColor="" Style="margin-top: 4px" OnClick="btnUpload_Click" />
                                        <asp:TextBox ID="txt41" Visible="false" runat="server"></asp:TextBox>
                                        <asp:Label ID="lbl1" Visible="false" runat="server"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload1" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group " id="Div1" runat="server" visible="false">
                                <asp:Image ID="imgDemo1" CssClass="img" Width="50%" Height="50%" runat="server" />
                                <asp:TextBox ID="txt1" MaxLength="3" Width="10%" runat="server"></asp:TextBox>
                                <%-- </div>
                                <div id="Div2" runat="server">--%>
                                <asp:LinkButton ID="img1del" runat="server" OnClick="del1" CssClass="del" Text="Delete"></asp:LinkButton>
                                <%--<asp:Button ID="btn1del" runat="server" Text="Delete" />--%>
                            </div>
                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                            <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" OnClick="Add_Click"
                                ValidationGroup="val1" Style="width: 120px;" />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"
                                Style="width: 120px;" />
                        </div>
                        <div class="col-lg-1">
                            <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                            <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                        </div>

                         </ContentTemplate>
                        <Triggers></Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="Updatepanel1">

                        <ProgressTemplate>
                        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                        <asp:Image  ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                    position: fixed; top: 50%; left: 40%;" />
                        </div>
                        </ProgressTemplate>
                        </asp:UpdateProgress>


                        </form>
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
    <!-- /.row -->
    <!-- /#page-wrapper -->
    <!-- jQuery -->
</body>
</html>
