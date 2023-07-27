<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderPrint.aspx.cs"
    Inherits="Billing.Accountsbootstrap.PurchaseOrderPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PurchaseOrder Print</title>
    <link href="../Styles/style1.css" rel="stylesheet" />
    <script type="text/javascript">
        function myFunction() {
            var ButtonControl = document.getElementById("btnprint");
            var fist = document.getElementById("btnexit");

            ButtonControl.style.visibility = "hidden";
            btnexit.style.visibility = "hidden";
            window.print();
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 50px;
        }
        .style2
        {
            height: 150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" width="100%">
    <table width="100%" border="0" cellpadding="1" cellspacing="1">
        <tr>
            <td align="left" style="width: 30%; vertical-align: top">
                Phone :
                <asp:Label ID="lblFPhone" runat="server"></asp:Label><br />
                Mobile :
                <asp:Label ID="lblFMobile" runat="server"></asp:Label><br />
            </td>
            <td align="center" style="width: 40%">
                <b>
                    <asp:Label ID="lblFCompany" runat="server"></asp:Label>
                </b>
                <br />
                <asp:Label ID="lblFAddress" runat="server" Style="font-size: larger"></asp:Label><br />
                <asp:Label ID="lblFAreaandPincode" runat="server" Style="font-size: larger"></asp:Label><br />
                <asp:Label ID="lblFEmail" runat="server" Style="font-size: larger"></asp:Label>
            </td>
            <td align="right" style="width: 30%; vertical-align: top">
                Fax :
                <asp:Label ID="lblFax" runat="server"></asp:Label><br />
                GSTIN :
                <asp:Label ID="lblFGST" runat="server"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" style="width: 100%; height: 35px; font-weight: bold;
                font-size: larger">
                <hr />
                <asp:Label ID="lblprint" runat="server" Text="PurchaseOrder Print"></asp:Label>
                <hr />
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td align="left">
                <br />
                Supplier : <b>
                    <asp:Label ID="lblcompanyname" runat="server"></asp:Label></b><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lbladdress" runat="server"></asp:Label><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblCityandPincode" runat="server"></asp:Label><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblArea" runat="server"></asp:Label><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblphoneno" runat="server"></asp:Label><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblGST" runat="server"></asp:Label><br />
            </td>
            <td align="left">
                Process Order No :
                <asp:Label ID="lblProcessOrderNo" runat="server"></asp:Label><br />
                Order Date :
                <asp:Label ID="lblOrderDate" runat="server"></asp:Label><br />
                Delivery Date :
                <asp:Label ID="lblOrderDateBetween" runat="server"></asp:Label><br />
                Delivery Place :
                <asp:Label ID="lblDeliveryPlace" runat="server"></asp:Label><br />
                ProcessOn :
                <asp:Label ID="lblProcessOn" runat="server"></asp:Label><br />
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr style="border: 0">
            <td>
                <asp:GridView ID="gvItemProcessOrder" runat="server" EmptyDataText="No Records Found"
                    GridLines="Both" ShowFooter="true" AutoGenerateColumns="false" OnRowDataBound="gvItemProcessOrder_OnRowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PurchaseFor" HeaderText="PurchaseFor" Visible="false" />
                        <asp:BoundField DataField="purchasefortype" HeaderText="PO Against" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderText="Item" ItemStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Item") %>' Style="text-align: left;"></asp:Label><br /><br />-
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("remarks") %>' Style="text-align: Center;"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Item" Visible="false" HeaderText="Item" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Color" HeaderText="Color" FooterStyle-HorizontalAlign="Right"
                            FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Rate" HeaderText="Rate" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
                <br />
                <br />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" style="height: 5px">
        <tr>
            <td align="left">
                <hr />
                <asp:Label ID="TC1" runat="server" Text="1.PO NO Should print on every Bill and delivery challan.">
                </asp:Label><br />
                <asp:Label ID="TC2" runat="server" Text="2.Billing to be per P.O. only & not 2-3 P.O.s together.">
                </asp:Label><br />
                <asp:Label ID="TC3" runat="server" Text="3.Bills Must have P.O. photocopy attached with it.">
                </asp:Label><br />
                <asp:Label ID="TC4" runat="server" Text="4.Bill must also have all relevant CHALLAN COPIES attached which have been received by Stores Department.">
                </asp:Label><br />
                <asp:Label ID="TC5" runat="server" Text="5.Delivery should be exactly on or before delivery date.">
                </asp:Label>
                <hr />
            </td>
        </tr>
        <tr>
            <td style="border: none">
                <table width="100%" align="right" style="margin-top: -21px">
                    <tr>
                        <td align="right" valign="top">
                            <h5>
                                for<b>
                                    <asp:Label ID="lblCoName" runat="server"></asp:Label>
                                </b>
                            </h5>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="border: none">
                <table width="100%" style="margin-top: 0px">
                    <tr>
                        <td valign="bottom" style="width: 400px; text-align: center">
                            Signature of the Supplier
                        </td>
                        <td valign="bottom" style="width: 400px; text-align: center">
                            Prepared By
                        </td>
                        <td valign="bottom" style="width: 400px; text-align: right">
                            Authorised Signatory
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td align="center">
                <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" />
                <asp:Button ID="btnexit" runat="server" Text="Exit" OnClick="btnexit_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
