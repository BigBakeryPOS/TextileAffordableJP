<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StitchingDetails_Print.aspx.cs" Inherits="Billing.Accountsbootstrap.StitchingDetails_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stitching Details - Print</title>
</head>
<body onload="print()">
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    <td>
    <label>Process Date :</label>
    <asp:Label ID="lblProcessDate" runat="server"></asp:Label>
    </td>

    <td>
    <label>Lot No :</label>
    <asp:Label ID="lblLotNo" runat="server"></asp:Label>
    </td>

    </tr>

    <tr>
    <td>
    <label>Brand :</label>
    <asp:Label ID="lblBrand" runat="server"></asp:Label>
    </td>

    <td>
    <label>Cutting Master :</label>
    <asp:Label ID="lblCuttingMaster" runat="server"></asp:Label>
    </td>

    </tr>

    <tr>
    <td>
    <label>Full Qty :</label>
    <asp:Label ID="lblFullQty" runat="server"></asp:Label>
    </td>

    <td>
    <label>Half Qty :</label>
    <asp:Label ID="lblHalfQty" runat="server"></asp:Label>
    </td>
    </tr>

    <tr>
    <td>
    <label>Total Quantity :</label>
    <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
    </td>

    <td>
    <label>Unit Name :</label>
    <asp:Label ID="lblUnit" runat="server"></asp:Label>
    </td>
    </tr>

    <tr>
    <td>
    <label>Kaja :</label>
    <asp:Label ID="lblKaja" runat="server"></asp:Label>
    </td>

    <td>
    <label>Embroiding :</label>
    <asp:Label ID="lblEmb" runat="server"></asp:Label>
    </td>

    <td>
    <label>Whasing :</label>
    <asp:Label ID="lblWhasing" runat="server"></asp:Label>
    </td>

    </tr>

    </table>

    <table  id="Table1" width="100%">
    <tr>
        <td style="padding-left:0px">

    <asp:GridView ID="gridPurchase" CssClass="myGridStyle" ShowHeaderWhenEmpty="true" Width="100%"
            EmptyDataText="No Records Found" runat="server" PageSize="10" AllowPaging="true" AutoGenerateColumns="false" >
    <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
    <Columns>
        <asp:BoundField HeaderText="Process Type" DataField="ProcessType" HeaderStyle-Width="70%" />
        <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' HeaderStyle-Width="30%"/>
    </Columns>
    <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
</asp:GridView>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
