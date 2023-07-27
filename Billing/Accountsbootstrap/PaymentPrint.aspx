<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentPrint.aspx.cs" Inherits="Billing.Accountsbootstrap.PaymentPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales Print</title>
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
    <style>
@media print

{

    .PrintButton{

        display:none;

    }

}



@media screen

{

    .PrintButton{

        display:block;

    }

} 
}
</style>
    <style type="text/css">
        .style1
        {
            font-size: 12px;
            font-family: Verdana;
        }
        .style2
        {
            height: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px;"
            width="100%" class="style1">
            <tr>
                <td align="center" style="height: 1px">
                    <table width="100%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr>
                            <td align="center">
                                <label id="fabregno" runat="server" style="font-size: large; font-weight: bold">
                                    Payment Voucher
                                </label>
                                <%--<asp:Label ID="lblInvoiceNo" Style="font-size: large; font-weight: bold" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                    </table>
                    <table border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 25%">
                                GSTIN :
                                <asp:Label ID="Label40" runat="server" Style="font-weight: 500; text-align: left"> 33CLNPS7587J1Z5 </asp:Label><br />
                                PH.NO :
                                <asp:Label ID="Label16" runat="server" Style="font-weight: 500;"> 0421-4238770 </asp:Label><br />
                            </td>
                            <td width="43%" valign="top" align="center">
                                <asp:Label ID="Label12" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit">83/84, Kavery Street,Odakkadu, </asp:Label><br />
                                <asp:Label ID="Label13" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> (Landmark: Deepa Hospital, Pushpa Threatre Dead End) </asp:Label><br />
                                <asp:Label ID="Label29" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> Tirupur – 641601. </asp:Label><br />
                                <asp:Label ID="Label19" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> Tamilnadu, INDIA. </asp:Label><br />
                                <asp:Image ID="Image1" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 8pc;"
                                    runat="server" />
                            </td>
                            <td valign="top" align="right" style="width: 32%">
                                Mobile No:
                                <asp:Label ID="lblmblrpll" Visible="false" runat="server" Style="font-weight: 500;">  +91 98431-98770</asp:Label>
                                <br />
                                E-Mail :
                                <asp:Label ID="Label17" runat="server" Style="font-weight: 500; text-align: left"> flexibleapparels@gmail.com</asp:Label>
                                <asp:Label ID="lblmblbc" Visible="false" runat="server" Style="font-weight: 500;"> +91 9176290701 </asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 30%">
                                <asp:Label ID="Label5" runat="server" Style="width: 100px">
                                 PaymentNo : </asp:Label>
                                <asp:Label ID="lblpayno" runat="server"></asp:Label><br />
                                <asp:Label ID="Label1" runat="server" Style="width: 100px">
                                PaymentDate : </asp:Label>
                                <asp:Label ID="lblpaydate" runat="server"></asp:Label><br />
                                <asp:Label ID="lblpaydatess" runat="server" Style="width: 100px">
                               JobWorker : </asp:Label>
                                <asp:Label ID="lbljobworker" runat="server"></asp:Label><br />
                                <asp:Label ID="Label2" runat="server" Style="width: 100px">
                               GSTIN : </asp:Label>
                                <asp:Label ID="lblgastin" runat="server"></asp:Label><br />
                            </td>
                            <td valign="top" align="center" style="width: 40%">
                            </td>
                            <td valign="top" align="left" style="width: 30%">
                                <asp:Label ID="Label4" runat="server" Style="width: 100px">
                               Process Type :</asp:Label>
                                <asp:Label ID="lblProcessType" runat="server"></asp:Label><br />
                                <asp:Label ID="Label7" runat="server" Style="width: 100px" Visible="true">
                               Total Amount :</asp:Label>
                                <asp:Label ID="lblTotAmt" runat="server" Visible="true"></asp:Label><br />
                                <asp:Label ID="Label3" runat="server" Style="width: 100px">
                                Narration : </asp:Label>
                                <asp:Label ID="lblnarrationpay" runat="server"></asp:Label><br />
                                <asp:Label ID="Label9" runat="server" Style="width: 100px" Visible="false"> 
                                Transport : </asp:Label>
                                <asp:Label ID="lbltransport" runat="server"></asp:Label><br />
                                <asp:Label ID="Label55" runat="server" Style="width: 100px" Visible="false">
                                Supplier Invoice date :</asp:Label>
                                <asp:Label ID="lblInvDate" runat="server" Visible="false"></asp:Label><br />
                                <asp:Label ID="Label6" runat="server" Style="width: 100px" Visible="false">
                                Challan No :</asp:Label>
                                <asp:Label ID="lblchellanno" runat="server" Visible="false"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" class="style1">
                        <tr>
                            <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                                <asp:GridView runat="server" BorderWidth="1" ID="gridprint" CssClass="myleft" GridLines="Both"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    RowStyle-Height="1px" ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true"
                                    Width="100%" OnRowDataBound="gridprint_RowDataBound" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="WorkOrderNo" DataField="WorkOrderNo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="LotNo" DataField="CompanyLotNo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="DebitAmount" DataField="DebitAmount" ItemStyle-HorizontalAlign="Center"
                                            DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Quantity" DataField="Quantity1" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="PieceRate" ItemStyle-HorizontalAlign="Center" DataField="PieceRate1"
                                            DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Amount" ItemStyle-HorizontalAlign="Center" DataField="Amount1"
                                            DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Advance" ItemStyle-HorizontalAlign="Center" DataField="Advance"
                                            DataFormatString='{0:f}' />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr height="100px">
                            <td width="20%" align="center">
                                <asp:Label ID="Label22" runat="server" Style="width: 500%; font-weight: bold">
                                PREPARED BY </asp:Label>
                            </td>
                            <td width="40%" align="center">
                                <asp:Label ID="Label23" runat="server" Style="width: 500%; font-weight: bold">
                               CHECKED BY </asp:Label>
                            </td>
                            <td id="Td1" width="20%" align="center" runat="server" visible="false">
                                <asp:Label ID="Label24" runat="server" Style="width: 500%; font-weight: bold">
                                DELIVERED BY </asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div id="hidenar" runat="server" visible="false">
                        <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                            class="style1">
                            <tr>
                                <td width="20%" align="left">
                                    <asp:Label ID="Label10" runat="server" Style="width: 100px">
                                Narration : </asp:Label>
                                    <asp:Label ID="lblnarration" runat="server" Style="width: 500%; font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table width="595px" class="style1">
            <tr>
                <td align="center">
                    <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" />
                    <asp:Button ID="btnexit" runat="server" Text="Exit" PostBackUrl="~/Accountsbootstrap/PaymentGridView.aspx" />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
