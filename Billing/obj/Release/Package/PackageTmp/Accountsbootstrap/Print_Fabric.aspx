<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Fabric.aspx.cs" Inherits="Billing.Accountsbootstrap.Print_Fabric" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fabric Print</title>
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
                                </label>
                                <asp:Label ID="lblInvoiceNo" Style="font-size: large; font-weight: bold" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="80%" height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left">
                                <%--   <asp:Label ID="Label8" runat="server" Style="width: 100px">
                                 DC No : </asp:Label>
                                <asp:Label ID="Label9" runat="server"></asp:Label><br />
                                <asp:Label ID="lbldcno" runat="server" Style="width: 100px">
                                DC Date : </asp:Label>
                                <asp:Label ID="lbldcdate" runat="server"></asp:Label><br />--%>
                                <asp:Label ID="Label5" runat="server" Style="width: 100px">
                                 Inv No : </asp:Label>
                                <asp:Label ID="lblInRefNo" runat="server"></asp:Label><br />
                                <asp:Label runat="server" Style="width: 100px">
                                Inv Date : </asp:Label>
                                <asp:Label ID="lblRegisterdate" runat="server"></asp:Label><br />
                                <asp:Label ID="Label8" runat="server" Style="width: 100px">
                                LR.No : </asp:Label>
                                <asp:Label ID="lbllrno" runat="server"></asp:Label><br />
                                <asp:Label ID="Label1" runat="server" Style="width: 100px" Visible="false">
                                Total KG : </asp:Label>
                                <asp:Label ID="lblTotMtr" runat="server" Visible="false"></asp:Label><br />
                                <asp:Label ID="Label7" runat="server" Style="width: 100px" Visible="false">
                               Total Amount :</asp:Label>
                                <asp:Label ID="lblTotAmt" runat="server" Visible="false"></asp:Label><br />
                            </td>
                            <td valign="top">
                                <asp:Label ID="Label2" runat="server" Style="width: 100px">
                                Supplier Name : </asp:Label>
                                <asp:Label ID="lblSuppName" runat="server"></asp:Label><br />
                                <asp:Label ID="Label3" runat="server" Style="width: 100px">
                                Prepared By :</asp:Label>
                                <asp:Label ID="lblCheSign" runat="server"></asp:Label><br />
                                <asp:Label ID="Label9" runat="server" Style="width: 100px">
                                Transport : </asp:Label>
                                <asp:Label ID="lbltransport" runat="server"></asp:Label><br />
                                <asp:Label ID="Label4" runat="server" Style="width: 100px" Visible="false">
                                Supplier Invoice date :</asp:Label>
                                <asp:Label ID="lblInvDate" runat="server" Visible="false"></asp:Label><br />
                                <asp:Label ID="Label6" runat="server" Style="width: 100px" Visible="false">
                                Challan No :</asp:Label>
                                <asp:Label ID="lblchellanno" runat="server" Visible="false"></asp:Label><br />
                                <%--   <asp:Label ID="lblBrand" runat="server" style="width:100px">
                                Brand Name :</asp:Label>
                            <asp:Label ID="lblBrandName" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" height="100px" border="0" class="style1">
                        <tr>
                            <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                                <%-- <asp:GridView runat="server" BorderWidth="1" ID="gridprint" CssClass="myleft" GridLines="Vertical"
                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                                Style="font-family: 'Verdana'; font-size: 13px;">--%>
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
                                        <asp:BoundField HeaderText="Fabric Type" DataField="itemname" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Design No" Visible="false" DataField="DesignNo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Color" DataField="Color" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Rate" ItemStyle-HorizontalAlign="Center" DataField="Rate"
                                            DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Billing KG" DataField="billMeter" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Purchase KG" DataField="Meter" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Avaliable KG" DataField="AvaliableMeter" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Pinning" Visible="false" DataField="Pinning" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:ImageField DataImageUrlField="Imagepath" ControlStyle-Height="80" HeaderText="Sample Image"
                                            Visible="false" />
                                        <asp:ImageField Visible="false" ControlStyle-Width="100px" HeaderText="Sample" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="gvreturnprint" CssClass="myleft"
                                    GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" RowStyle-Height="1px" ShowFooter="true" PrintPageSize="30"
                                    AllowPrintPaging="true" Width="100%" OnRowDataBound="gvreturnprint_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <%--<asp:BoundField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" DataField="Orderno"
                                            ItemStyle-Height="140" />--%>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Fabric Type" DataField="itemname" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Design No" DataField="DesignNo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Color" DataField="Color" ItemStyle-HorizontalAlign="Center" />
                                        <%--<asp:BoundField HeaderText="Piece" DataField="Piece" ItemStyle-Height="60" ItemStyle-HorizontalAlign="Center" />--%>
                                        <asp:BoundField HeaderText="Width" DataField="Width" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Rate" ItemStyle-HorizontalAlign="Center" DataField="Rate"
                                            DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="ReturnMeter" DataField="Meter" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />
                                        <%--<asp:BoundField HeaderText="AvaliableMeter" DataField="Meter" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />--%>
                                        <%--<asp:BoundField HeaderText="Pinning" DataField="Pinning" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />--%>
                                        <asp:ImageField DataImageUrlField="Imagepath" ControlStyle-Height="80" HeaderText="Sample Image"
                                            Visible="false" />
                                        <asp:ImageField Visible="false" ControlStyle-Width="100px" HeaderText="Sample" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0">
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table class="style1" width="100%">
                        <tr>
                            <td style="width: 80%">
                            </td>
                            <td style="width: 20%">
                                <table border="1" class="style1">
                                    <tr>
                                        <td align="right" style="padding-right: 5px">
                                            Sub.Total
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblGrandtotalamt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="cg" runat="server">
                                        <td align="right" style="padding-right: 5px">
                                            CGST
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblCGST" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="sg" runat="server">
                                        <td align="right" style="padding-right: 5px">
                                            SGST
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblSGST" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="ig" runat="server">
                                        <td align="right" style="padding-right: 5px">
                                            IGST
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblIGST" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-right: 5px" width="200px">
                                            Total
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblBillAmt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" width="12%" style="text-align: center; height: 50px" valign="bottom">
                                            <asp:Label ID="Label11" runat="server">Authorized Signature</asp:Label>
                                        </td>
                                    </tr>
                                </table>
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
                            <td width="20%" align="center" runat="server" visible="false">
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
                    <asp:Button ID="btnexit" runat="server" Text="Exit" PostBackUrl="~/Accountsbootstrap/Fabric_Grid.aspx" />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
