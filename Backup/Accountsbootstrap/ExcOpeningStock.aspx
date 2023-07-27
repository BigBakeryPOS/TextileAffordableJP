<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcOpeningStock.aspx.cs"
    Inherits="Billing.Accountsbootstrap.ExcOpeningStock" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Exc OpeningStock </title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../Styles/chosen.css" rel="Stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <link rel="stylesheet" href="../css/chosen.css" />
    <script type="text/javascript">
        function Confirm(myButton) {

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
    <style type="text/css">
        .zoomed-element
        {
            zoom: 1.8;
        }
    </style>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"></asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false">
    </asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row" style="margin-top: -10px">
        <div class="col-lg-12">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #336699; color: Black; border-color: #06090c">
                        <table>
                            <tr>
                                <td>
                                    <i class="fa fa-briefcase"></i>
                                    <asp:Label ID="lblName" Text="Exc OpeningStock" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="chzn-select" Style="height: 30px;
                                        color: Black" Width="250px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExcNo" runat="server" Width="100%" Height="30px" CssClass="form-control"
                                        placeholder="Enter ExcNo"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel-body">
                        <div class="list-group">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="form-group">
                                <div class="panel-body" style="height: 100%">
                                    <div class="col-lg-12">
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label>
                                                    Style:</label>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlStyle" ValueToCompare="Select Style"
                                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Style.">
                                                </asp:CompareValidator>
                                                <asp:DropDownList ID="ddlStyle" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                    Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label>
                                                    Color:</label>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlColor" ValueToCompare="Select Color"
                                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Color.">
                                                </asp:CompareValidator>
                                                <asp:DropDownList ID="ddlColor" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                    Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <div class="form-group">
                                                <label>
                                                    Rate:</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtRate"
                                                    ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Rate." Style="color: Red" />
                                                <asp:TextBox ID="txtRate" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender313d" runat="server"
                                                    TargetControlID="txtRate" ValidChars="." FilterType="Numbers,Custom" />
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <div class="form-group">
                                                <label>
                                                    Size:</label>
                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlSize" ValueToCompare="Select Size"
                                                    Operator="NotEqual" Type="String" ErrorMessage="Please Select Size.">
                                                </asp:CompareValidator>
                                                <asp:DropDownList ID="ddlSize" runat="server" CssClass="chzn-select" Style="height: 30px"
                                                    Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlSize_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:GridView ID="GVSizes" AutoGenerateColumns="False" GridLines="Both" runat="server">
                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" HeaderStyle-Width="100px" ItemStyle-Font-Size="Large"
                                                        ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdSize" runat="server" Value='<%#Eval("SizeId") %>' />
                                                            <asp:Label ID="lblSize" Height="30px" Width="100px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQty" Height="30px" Width="100%" runat="server" Text='<%#Eval("Qty")%>'></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312fff" runat="server"
                                                                TargetControlID="txtQty" FilterType="Numbers" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-lg-1">
                                            <br />
                                            <div class="form-group">
                                                <asp:Button ID="btnSubmit1" runat="server" Text="Submit and Clear" Width="130px"
                                                    OnClick="btnSubmit1_OnClick" ValidationGroup="val1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel-body" style="margin-top: 30px; height: 100%">
        <div class="col-lg-12">
            <div class="col-lg-9">
                <asp:GridView ID="GVItem" AutoGenerateColumns="False" runat="server" OnRowCommand="GVItem_OnRowCommand"
                    GridLines="Both" Caption="Style Details">
                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                        Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StyleNo" HeaderStyle-Width="70px" ItemStyle-Width="70px"
                            ItemStyle-Font-Size="Large" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdStyleNoId" runat="server" Value='<%#Eval("StyleNoId")  %>' />
                                <asp:Label ID="lblStyleNo" runat="server" Text='<%#Eval("StyleNo")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-Width="30%" ItemStyle-Width="30%"
                            ItemStyle-Font-Size="Large" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Color" HeaderStyle-Width="50px" ItemStyle-Width="50px"
                            ItemStyle-Font-Size="Large" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId")  %>' />
                                <asp:Label ID="lblColor" runat="server" Text='<%#Eval("Color")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="30px" ItemStyle-Width="30px"
                            ItemStyle-Font-Size="Large" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Buyer Order Qty" HeaderStyle-Width="30px" ItemStyle-Width="30px"
                            ItemStyle-Font-Size="Large" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                            ItemStyle-Font-Size="Large" ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdRangeId" runat="server" Value='<%#Eval("RangeId")  %>' />
                                <asp:Label ID="lblSize" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Modify" ItemStyle-HorizontalAlign="Center" Visible="true"
                            HeaderStyle-Width="20px" ItemStyle-Width="20px">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")  %>' />
                                <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                    CommandName="Modify">
                                    <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" Visible="true"
                            HeaderStyle-Width="20px" ItemStyle-Width="20px">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                    CommandName="View">
                                    <asp:Image ID="imgView" runat="server" ImageUrl="~/images/pen-checkbox-128.png" /></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-lg-3">
                <asp:GridView ID="GVSizesView" AutoGenerateColumns="False" GridLines="Both" runat="server"
                    Caption="Style Size Details">
                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                        Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size" HeaderStyle-Width="100px" ItemStyle-Font-Size="Large"
                            ItemStyle-Font-Bold="true">
                            <ItemTemplate>
                            
                                <asp:HiddenField ID="hdSize" runat="server" Value='<%#Eval("SizeId") %>' />
                                <asp:Label ID="lblSize" Height="30px" Width="150px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQty" Height="30px" Width="80px" runat="server" Text='<%#Eval("Qty")%>'></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312fff" runat="server"
                                    TargetControlID="txtQty" FilterType="Numbers" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="col-lg-12">
        <div class="col-lg-10">
        </div>
        <div class="col-lg-1">
            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" Style="width: 90px;"
                OnClick="btnSave_OnClick" />
        </div>
        <div class="col-lg-1">
            <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" Style="width: 90px;"
                OnClick="btnExit_OnClick" />
        </div>
    </div>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/chosen.min.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
