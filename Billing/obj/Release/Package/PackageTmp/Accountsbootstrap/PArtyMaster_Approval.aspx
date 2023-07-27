<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PArtyMaster_Approval.aspx.cs"
    Inherits="Billing.Accountsbootstrap.PArtyMaster_Approval" %>

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
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link rel="Stylesheet" type="text/css" href="../Styles/style1.css" />
    <title>Party Master Approval</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <link rel="stylesheet" href="../css/chosen.css" />
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <style>
         blink, .blink {
            animation: blinker 1s linear infinite;
        }

       @keyframes blinker {  
            50% { opacity: 0; }
       }
      </style>
       <script src="../js/toastrmin.js" type="text/javascript"></script>
    <script src="../js/toastr.js" type="text/javascript"></script>
    <link href="../css/toastr.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function showpop1(msg, title) {

            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-bottom-right",
                "preventDuplicates": true,
                "onclick": null,
                "showDuration": "3000",
                "hideDuration": "1000",
                "timeOut": "12000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            // toastr['success'](msg, title);
            var d = Date();
            toastr.success(msg, title);
            return false;
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
            <h2 id="head" runat="server" style="text-align: left; color: #fe0002; margin-top: -8px">
            </h2>
        </div>
    </div>
    <div class="row">
        <form id="Form1" runat="server">
        <asp:UpdatePanel ID="upanal" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="form-group">
                            <div id="add" runat="server" class="row">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                <div class="col-lg-12">
                                    <div class="col-lg-3">
                                        <blink> <asp:Label  runat="Server" id="lblblinktext"  style="color:Green; font-size:12px" ></asp:Label> </blink>
                                        <div class="form-group">
                                            <label>
                                                Select Party Master</label>
                                            <asp:DropDownList ID="drppartymaster" runat="server" OnSelectedIndexChanged="PartyMAster_chnaged" Width="100%"
                                                AutoPostBack="true" CssClass="chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Party Type</label>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlPartyType"
                                                ValueToCompare="Select PartyType" Operator="NotEqual" Type="String" ErrorMessage="Please Select PartType."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlPartyType" runat="server" class="form-control">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblpartytype" runat="server" Visible="false"></asp:Label>
                                        </div>
                                         <div id="Div2" class="form-group" runat="server" visible="true">
                                            <label>
                                                Select Group</label>
                                            <asp:CompareValidator ID="CompareValidator8" runat="server" ValidationGroup="val2"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlGroup" ValueToCompare="Select"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please Select Group"></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Company Code</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                                                Text="*" ControlToValidate="txtCompanyCode" ErrorMessage="Please Enter Company Code."
                                                Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtCompanyCode" Style="font-weight: bold;"
                                                MaxLength="50" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars=" "
                                                TargetControlID="txtCompanyCode" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Company Name</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="reqName" Text="*"
                                                ControlToValidate="txtCompanyName" ErrorMessage="Please Enter Company Name."
                                                Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtCompanyName" Style="font-weight: bold"
                                                MaxLength="50" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars="./\][;!@#$%^&*()_-+ "
                                                TargetControlID="txtCompanyName" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Contact Person Name</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator5"
                                                Text="*" ControlToValidate="txtcontactpersonname" ErrorMessage="Please Enter contact Person Name."
                                                Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtcontactpersonname" Style="font-weight: bold;"
                                                MaxLength="50" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars=" ./"
                                                TargetControlID="txtcontactpersonname" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Phone</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                                Text="*" ControlToValidate="txtPhone" ErrorMessage="Please Enter Phone." Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtPhone" Style="font-weight: bold" MaxLength="33"
                                                runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterType="Numbers,custom" ValidChars="," TargetControlID="txtPhone" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Mobile</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator9"
                                                Text="*" ControlToValidate="txtMobile" ErrorMessage="Please Enter Mobile." Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtMobile" Style="font-weight: bold" MaxLength="12"
                                                runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                FilterType="Numbers,Custom" ValidChars="" TargetControlID="txtMobile" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Followed By
                                            </label>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpfollowedby"
                                                ValueToCompare="Select FollowedBy" Operator="NotEqual" Type="String" ErrorMessage="Please Select Followed By."></asp:CompareValidator>
                                            <asp:DropDownList ID="drpfollowedby" runat="server" class="form-control">
                                            </asp:DropDownList>
                                            <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>
                                                Address</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="address" ControlToValidate="txtaddress"
                                                Text="*" ErrorMessage="Please Enter Address." Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" Style="font-weight: bold" TextMode="MultiLine"
                                                ID="txtaddress" MaxLength="150" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Country</label>
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCountry" ValueToCompare="Select Country"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please Select Country."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlCountry" class="form-control" Width="100%" OnSelectedIndexChanged="ddlCountry_OnSelectedIndexChanged"
                                                CssClass="chzn-select" AutoPostBack="true" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                State
                                            </label>
                                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                            <asp:CheckBox runat="server" ID="chkDistrict" Text="New State" AutoPostBack="true"
                                                Visible="true" OnCheckedChanged="chkDistrict_CheckedChanged" />
                                            <asp:TextBox Visible="false" Style="font-weight: bold" CssClass="form-control" ID="txtstate"
                                                Width="100%" MaxLength="30" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlState" ValueToCompare="Select State"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please Select State."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlState" class="form-control" Width="100%" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlState_OnSelectedIndexChanged"
                                                CssClass="chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                City
                                            </label>
                                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                            <asp:CheckBox runat="server" ID="chkCity" Text="New City" AutoPostBack="true" OnCheckedChanged="chkCity_CheckedChanged"
                                                Visible="true" />
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator4"
                                                ControlToValidate="txtcity" Text="*" ErrorMessage="Please Enter City." Style="color: Red" />
                                            <asp:TextBox Visible="false" Style="font-weight: bold" CssClass="form-control" ID="txtcity"
                                                Width="100%" MaxLength="30" runat="server"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCity" ValueToCompare="Select City"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please Select City."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlCity" class="form-control" Width="100%" runat="server" CssClass="chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Pincode</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator3"
                                                ControlToValidate="txtpincode" Text="*" ErrorMessage="Please Enter Pincode."
                                                Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" Style="font-weight: bold" ID="txtpincode" MaxLength="6"
                                                runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                FilterType="Numbers" ValidChars="" TargetControlID="txtpincode" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Fax</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator7"
                                                ControlToValidate="txtFax" Text="*" ErrorMessage="Please Enter Fax." Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" Style="font-weight: bold" ID="txtFax" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars="_-@."
                                                TargetControlID="txtFax" />
                                        </div>
                                         <div class="form-group">
                                            <label>
                                                Province</label>
                                            <asp:CompareValidator ID="CompareValidator7" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlProvince"
                                                ValueToCompare="0" Operator="NotEqual" Type="String" ErrorMessage="Select Province type"></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlProvince" AutoPostBack="false" Style="font-weight: bold"
                                                runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Select Province type" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Inner" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Outer" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                         <div class="form-group">
                                            <label>
                                                GST Type</label>
                                            <asp:DropDownList ID="drpGSTType" AutoPostBack="true" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Exclusive" Selected="True" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Inclusive" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>
                                                E-mail</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator6"
                                                ControlToValidate="txtemail" Text="*" ErrorMessage="Please Enter Email." Style="color: Red" />
                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ValidationGroup="val1"
                                                Text="*" ControlToValidate="txtemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorMessage="Please Enter Correct Email." Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" Style="font-weight: bold" ID="txtemail" placeholder="For Ex: test@gmail.com"
                                                runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars="_-@."
                                                TargetControlID="txtemail" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Web Site</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator8"
                                                ControlToValidate="txtWebSite" Text="*" ErrorMessage="Please Enter Web Site."
                                                Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" Style="font-weight: bold" ID="txtWebSite" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars="_-@."
                                                TargetControlID="txtWebSite" />
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Currency</label>
                                            <asp:CompareValidator ID="CompareValidator6" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlCurrency"
                                                ValueToCompare="Select CurrencyName" Operator="NotEqual" Type="String" ErrorMessage="Please Select Currency."></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlCurrency" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                IsForeign</label>
                                            <asp:DropDownList ID="ddlIsForeign" runat="server" class="form-control">
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Is Active</label>
                                            <asp:DropDownList ID="ddlIsActive" runat="server" class="form-control">
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Payment Mode</label>
                                            <asp:TextBox CssClass="form-control" ID="txtPaymentMode" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                GSTIN NO</label>
                                            <asp:TextBox CssClass="form-control" ID="txtgstno" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>
                                                Terms</label>
                                            <asp:TextBox CssClass="form-control" ID="txtTerms" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Days</label>
                                            <asp:TextBox CssClass="form-control" ID="txtDays" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Nature of Contract</label>
                                            <asp:TextBox CssClass="form-control" ID="txtNatureofContract" runat="server"></asp:TextBox>
                                        </div>
                                        <div id="Div1" runat="server" visible="false" class="form-group">
                                            <label>
                                                Strength</label>
                                            <asp:TextBox CssClass="form-control" ID="txtStrength" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Final Destination</label>
                                            <asp:TextBox CssClass="form-control" ID="txtFinalDestination" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Shipment Mode</label>
                                            <%-- <asp:TextBox CssClass="form-control" ID="txtShipmentMode" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlShipmentMode" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                A/C Code</label>
                                            <asp:TextBox CssClass="form-control" ID="txtACCode" runat="server"></asp:TextBox>
                                        </div>
                                         <div class="form-group">
                                            <label>
                                                Notes</label>
                                            <asp:TextBox CssClass="form-control" ID="txtnotes" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Approve" OnClick="Add_Click"
                                                        ValidationGroup="val1" Width="110px" />
                                                </td>
                                                <td>
                                                    &nbsp&nbsp&nbsp
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"
                                                        Width="110px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="col-lg-2">
                                    </div>
                                    <div class="col-lg-8">
                                        <asp:GridView ID="GVPerson" AutoGenerateColumns="False" CssClass="myGrid" GridLines="None"
                                            runat="server" OnRowDeleting="GVPerson_RowDeleting">
                                            <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                            <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="1%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PersonName" HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPersonName" Height="30px" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="e-Mail" HeaderStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txteMail1" Height="30px" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Phone1" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPhone1" Height="30px" Width="100px" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                                            TargetControlID="txtPhone1" ValidChars="." FilterType="Numbers,Custom" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Phone2" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPhone2" Height="30px" Width="100px" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3133" runat="server"
                                                            TargetControlID="txtPhone2" ValidChars="." FilterType="Numbers,Custom" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtMobile1" Height="30px" Width="100px" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender313" runat="server"
                                                            TargetControlID="txtMobile1" ValidChars="." FilterType="Numbers,Custom" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Add">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnadd1" runat="server" SkinID="Add" OnClick="btnAdd1_Click" Height="30px"
                                                            Width="73px" Text="Add" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ControlStyle-Width="100%" ShowDeleteButton="True" ButtonType="Button" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-lg-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        <script src="../js/jquery.min.js" type="text/javascript"></script>
        <script src="../js/chosen.min.js" type="text/javascript"></script>
        <script type="text/javascript">            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
        </form>
    </div>
</body>
</html>
