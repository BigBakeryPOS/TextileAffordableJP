<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialOpeningStock.aspx.cs"
    Inherits="Billing.Accountsbootstrap.MaterialOpeningStock" %>

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
    <title>Material OpeningStock </title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../css/chosen.css" rel="Stylesheet" />
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
                                    <asp:Label ID="lblName" Text="Material OpeningStock" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="chzn-select" Style="height: 30px;
                                        color: Black" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="panel-body">
                        <div class="list-group">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="form-group">
                            </div>
                            <div class="col-lg-12">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Process On :</label>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlProcessOn"
                                            ValueToCompare="ProcessOn" Operator="NotEqual" Type="String" ErrorMessage="Please Select ProcessOn.">
                                        </asp:CompareValidator>
                                        <asp:DropDownList ID="ddlProcessOn" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlProcessOn_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            Items :</label>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                            Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlItems" ValueToCompare="Select Item"
                                            Operator="NotEqual" Type="String" ErrorMessage="Please Select Items.">
                                        </asp:CompareValidator>
                                        <asp:DropDownList ID="ddlItems" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            Color :</label>
                                        <asp:CompareValidator ID="CompareValidator10" runat="server" ValidationGroup="val1"
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
                                            Qty</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtQty"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Qty." Style="color: Red" />
                                        <asp:TextBox ID="txtQty" Height="30px" Width="100%" runat="server" CssClass="form-control"
                                            AutoComplete="Off">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                            TargetControlID="txtQty" ValidChars="." FilterType="Numbers,Custom" />
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <label>
                                            Rate</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtRate"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Enter Qty." Style="color: Red" />
                                        <asp:TextBox ID="txtRate" Height="30px" Width="100%" runat="server" CssClass="form-control"
                                            AutoComplete="Off">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            TargetControlID="txtRate" ValidChars="." FilterType="Numbers,Custom" />
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <label>
                                        Remarks</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtRemarks" AutoComplete="Off" Height="30px" Width="100%" runat="server">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="val1" OnClick="btnSubmit_OnClick"
                                            Height="30px" Width="95px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-12" style="height: 280px">
        <div class="col-lg-1">
        </div>
        <div class="col-lg-10">
            <%--      <div id="div2" runat="server" style="overflow: auto; height: 300px; width: 100%">--%>
            <asp:GridView ID="GVItem" AutoGenerateColumns="False" GridLines="Both" runat="server"
                CssClass="myGridStyle1" OnRowDeleting="GVItem_RowDeleting">
                <HeaderStyle BackColor="White" />
                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                    NextPageText="Next" PreviousPageText="Previous" />
                <Columns>
                    <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                            <asp:HiddenField ID="hdProcessOnID" runat="server" Value='<%#Eval("ProcessOnID") %>' />
                            <asp:HiddenField ID="hdItemId" runat="server" Value='<%#Eval("ItemId") %>' />
                            <asp:HiddenField ID="hdColorId" runat="server" Value='<%#Eval("ColorId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="ProcessOn" DataField="ProcessOn" />
                    <asp:BoundField HeaderText="Item" DataField="Item" />
                    <asp:BoundField HeaderText="ColorId" DataField="Color" />
                    <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Right" />
                      <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                    <asp:CommandField ControlStyle-Width="100%" ShowDeleteButton="True" ButtonType="Button" />
                </Columns>
                <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            </asp:GridView>
            <%--<%--  </div>--%>
        </div>
        <div class="col-lg-1">
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
