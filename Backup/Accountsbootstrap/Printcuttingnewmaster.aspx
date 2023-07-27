<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Printcuttingnewmaster.aspx.cs" Inherits="Billing.Accountsbootstrap.Printcuttingnewmaster" %>

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
        
        
        .myleft
        {
            border-collapse: collapse;
            width: 85%;
            margin-left: 0px;
            border: 1px solid gray;
            overflow: hidden;
        }
        
        
        
        .myleft tr th
        {
            padding: 8px;
            color: Black;
            border: 1px solid gray;
            font-family: Arial;
            font-size: 10pt;
            text-align: center;
        }
        
        
        
        
        
        .myleft tr:nth-child(even)
        {
        }
        
        
        
        .myleft tr:nth-child(odd)
        {
        }
        
        
        
        .myleft td
        {
            border: 1px solid gray;
            padding: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px"
            width="100%" height="100px" class="style1">
            <tr>
                <td style="height: 1px">
                    <table width="100%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr>
                            <td align="center">
                                <label style="font-size: large; font-weight: bold">
                                    Master Cutting Process:</label>
                                     <asp:Label ID="lblllot" style="font-size: large; font-weight: bold" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="595px" height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left">
                                <asp:Label ID="Label1" runat="server" Style="width: 500%; font-weight: bold">
                                Lot Number : </asp:Label>
                                <asp:Label ID="lblLot" runat="server"></asp:Label><br />
                                <asp:Label ID="Label2" runat="server" Style="width: 100px; font-weight: bold">
                                Delivery date : </asp:Label>
                                <asp:Label ID="lblDeldate" runat="server"></asp:Label><br />
                            </td>
                            <td valign="top" align="left">
                                <asp:Label ID="Label3" runat="server" Style="width: 100px; font-weight: bold">
                                Width : </asp:Label>
                                <asp:Label ID="lblwidth" runat="server"></asp:Label><br />
                                 <asp:Label   ID="Label5" runat="server" style="width:100px;font-weight:bold">
                                Cutting Master : </asp:Label>
                            <asp:Label ID="lblcut" runat="server"></asp:Label><br />
                                <asp:Label  Visible="false" ID="Label4" runat="server" Style="width: 100px; font-weight: bold">
                                Fit : </asp:Label>
                                <asp:Label ID="lblfit" Visible="false" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table id="Table1" width="595px" runat="server" visible="false" height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 50px">
                                <label style="font-weight: bold">
                                    Overall Lot Report</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="36FS" DataField="tsfs" />
                                        <asp:BoundField HeaderText="38FS" DataField="tefs" />
                                        <asp:BoundField HeaderText="39FS" DataField="tnfs" />
                                        <asp:BoundField HeaderText="40FS" DataField="fzfs" />
                                        <asp:BoundField HeaderText="42FS" DataField="ftfs" />
                                        <asp:BoundField HeaderText="44FS" DataField="fffs" />
                                        <asp:BoundField HeaderText="36HS" DataField="tshs" />
                                        <asp:BoundField HeaderText="38HS" DataField="tehs" />
                                        <asp:BoundField HeaderText="39HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44HS" DataField="ffhs" />
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td valign="top" align="left" style="width: 50px">
                                <label style="font-weight: bold">
                                    Avg. Rate Calculation</label>
                                <div>
                                    <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myleft" GridLines="Vertical"
                                        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                        PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                                        Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Used Meter" DataField="met" />
                                            <asp:BoundField HeaderText="Rate" DataField="rat" />
                                            <asp:BoundField HeaderText="Total" DataField="tot" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold">
                                <label>
                                    Avg. Meter:</label>
                                <asp:Label ID="Lblvalue" runat="server"></asp:Label>
                            </td>
                            <td style="font-weight: bold; font-size: 12px">
                                <label>
                                    Avg.Rate +Prod.Cost(Rs.90):</label>
                                <asp:Label ID="lblratee" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="595px" height="643px" border="0.5px" class="style1">
                        <tr valign="top">
                            <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                                <label style="font-weight: bold">
                                    Detailed Cutting Report</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridprint" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100.5%" OnRowDataBound="gridprint_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                    <%--    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                         <asp:BoundField HeaderText="Main Label" DataField="MainLabel" />
                                          <asp:BoundField HeaderText="Fit Label" DataField="flab" />
                                           <asp:BoundField HeaderText="Wash Care Label" DataField="wlab" />
                                            <asp:BoundField HeaderText="Logo Embroideng" DataField="llab" />--%>
                                        <asp:BoundField HeaderText="36FS" DataField="tsfs" />
                                        <asp:BoundField HeaderText="38FS" DataField="tefs" />
                                        <asp:BoundField HeaderText="39FS" DataField="tnfs" />
                                        <asp:BoundField HeaderText="40FS" DataField="fzfs" />
                                        <asp:BoundField HeaderText="42FS" DataField="ftfs" />
                                        <asp:BoundField HeaderText="44FS" DataField="fffs" />
                                        <asp:BoundField HeaderText="36HS" DataField="tshs" />
                                        <asp:BoundField HeaderText="38HS" DataField="tehs" />
                                        <asp:BoundField HeaderText="39HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44HS" DataField="ffhs" />
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                          <tr valign="top">
                            <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                                <label style="font-weight: bold">
                                    Master Cutting Report</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridmaster" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100.5%" 
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                    <%--    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                         <asp:BoundField HeaderText="Main Label" DataField="MainLabel" />
                                          <asp:BoundField HeaderText="Fit Label" DataField="flab" />
                                           <asp:BoundField HeaderText="Wash Care Label" DataField="wlab" />
                                            <asp:BoundField HeaderText="Logo Embroideng" DataField="llab" />--%>
                                        <asp:BoundField HeaderText="36FS" DataField="tsfs" />
                                        <asp:BoundField HeaderText="38FS" DataField="tefs" />
                                        <asp:BoundField HeaderText="39FS" DataField="tnfs" />
                                        <asp:BoundField HeaderText="40FS" DataField="fzfs" />
                                        <asp:BoundField HeaderText="42FS" DataField="ftfs" />
                                        <asp:BoundField HeaderText="44FS" DataField="fffs" />
                                        <asp:BoundField HeaderText="36HS" DataField="tshs" />
                                        <asp:BoundField HeaderText="38HS" DataField="tehs" />
                                        <asp:BoundField HeaderText="39HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44HS" DataField="ffhs" />
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                          <asp:BoundField HeaderText="Damage Qty" DataField="damageqty" />
                                            <asp:BoundField HeaderText="Reason" DataField="reason" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="595px" class="style1">
            <tr>
                <td align="center">
                    <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" />
                    <asp:Button ID="btnexit" runat="server" Text="Exit" OnClick="btnexit_Click" />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
