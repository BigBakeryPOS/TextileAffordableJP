<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterprintCutting.aspx.cs"
    Inherits="Billing.Accountsbootstrap.MasterprintCutting" %>

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
                                <asp:Label ID="lbbllott" Style="font-size: large; font-weight: bold" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="595px" height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 40%">
                            </td>
                            <td valign="top" align="right" style="width: 20%">
                                <asp:Image ID="Image2" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 8pc;"
                                    runat="server" /><br />
                            </td>
                            <td valign="top" align="right" style="width: 10%">
                            </td>
                            <td valign="top" align="left" style="width: 30%">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left" style="width: 40%">
                                <asp:Label ID="Label1" runat="server" Style="width: 500%; font-weight: bold">
                                Lot Number : </asp:Label>
                                <asp:Label ID="lblLot" runat="server"></asp:Label><br />
                                <asp:Label ID="Label2" runat="server" Style="width: 100px; font-weight: bold">
                                Issue date : </asp:Label>
                                <asp:Label ID="lblDeldate" runat="server"></asp:Label><br />
                                <asp:Label ID="Label9" runat="server" Style="width: 100px; font-weight: bold">
                                Brand : </asp:Label>
                                <asp:Label ID="lbllbrand" runat="server"></asp:Label><br />
                                <asp:Label Visible="false" ID="Label4" runat="server" Style="width: 100px; font-weight: bold">
                                Fit : </asp:Label>
                                <asp:Label ID="lblfit" Visible="false" runat="server"></asp:Label><br />
                            </td>
                            <td valign="top" align="right" style="width: 5%">
                                <%--<asp:Image ID="Image1" ImageUrl="../images/jplogo.png" Style="width: 15pc;" runat="server" />--%><br />
                            </td>
                            <td valign="top" align="left" style="width: 55%">
                                <asp:Label ID="Label3" runat="server" Style="width: 100px; font-weight: bold">
                                Width : </asp:Label>
                                <asp:Label ID="lblwidth" runat="server"></asp:Label><br />
                                <asp:Label ID="Label5" runat="server" Style="width: 100px; font-weight: bold">
                                Cutting Master : </asp:Label>
                                <asp:Label ID="lblcut" runat="server"></asp:Label><br />
                                <asp:Label ID="Label6" runat="server" Style="width: 100px; font-weight: bold">
                                Grand Total : </asp:Label>
                                <asp:Label ID="grandtotalfhs" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="Label7" runat="server" Style="width: 100px; font-weight: bold">
                               Item Narrations : </asp:Label>
                                <asp:Label ID="lblitemnarrations" runat="server"></asp:Label>
                                <asp:Label ID="lblShirtDescription" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="700px" border="0.5px" class="style1">
                        <tr runat="server" visible="false" valign="top">
                            <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                                <asp:GridView runat="server" BorderWidth="1" ID="griddam" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;" Caption="Overall Lot Report">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Reason" DataField="reason" />
                                        <asp:BoundField HeaderText="Required Shirt" DataField="reqshirt" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Master Cutting Shirt" DataField="mas" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Variance Qty" DataField="damageqty" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Variance KG" DataField="dmgmet" DataFormatString='{0:f}' />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridnewmaster" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" OnRowDataBound="gridnewmaster_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;" ShowFooter="true" Caption="Master Cutting Report">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                        <%--<asp:BoundField HeaderText="Party Name" DataField="ledgername" ItemStyle-Height="60" />--%>
                                        <asp:BoundField HeaderText="Fit" DataField="Fit" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Used Meter" Visible="false" DataField="usedmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <%--<asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60" />--%>
                                        <asp:BoundField HeaderText="30" DataField="30FS" />
                                        <asp:BoundField HeaderText="32" DataField="32FS" />
                                        <asp:BoundField HeaderText="34" DataField="34FS" />
                                        <asp:BoundField HeaderText="36" DataField="36FS" />
                                        <asp:BoundField HeaderText="XS" DataField="XSFS" />
                                        <asp:BoundField HeaderText="S" DataField="SFS" />
                                        <asp:BoundField HeaderText="M" DataField="MFS" />
                                        <asp:BoundField HeaderText="L" DataField="LFS" />
                                        <asp:BoundField HeaderText="XL" DataField="XLFS" />
                                        <asp:BoundField HeaderText="XXL" DataField="XXLFS" />
                                        <asp:BoundField HeaderText="3XL" DataField="3XLFS" />
                                        <asp:BoundField HeaderText="4XL" DataField="4XLFS" />
                                        <asp:BoundField HeaderText="30" DataField="30HS" />
                                        <asp:BoundField HeaderText="32" DataField="32HS" />
                                        <asp:BoundField HeaderText="34" DataField="34HS" />
                                        <asp:BoundField HeaderText="36" DataField="36HS" />
                                        <asp:BoundField HeaderText="XS" DataField="XSHS" />
                                        <asp:BoundField HeaderText="S" DataField="SHS" />
                                        <asp:BoundField HeaderText="M" DataField="MHS" />
                                        <asp:BoundField HeaderText="L" DataField="LHS" />
                                        <asp:BoundField HeaderText="XL" DataField="XLHS" />
                                        <asp:BoundField HeaderText="XXL" DataField="XXLHS" />
                                        <asp:BoundField HeaderText="3XL" DataField="3XLHS" />
                                        <asp:BoundField HeaderText="4XL" DataField="4XLHS" />
                                        <asp:BoundField HeaderText="Total" DataField="TotFS" />
                                        <asp:BoundField HeaderText="Total HS" DataField="TotHS" />
                                        <asp:BoundField HeaderText="Total Qty" DataField="Qty" />
                                        <asp:BoundField HeaderText="Avg Meter" DataFormatString='{0:f}' DataField="AvgMtr"
                                            Visible="false" />
                                        <asp:BoundField HeaderText="Avg Rate" DataFormatString='{0:f}' DataField="AvgRate"
                                            Visible="false" />
                                        <asp:BoundField HeaderText="Margin" DataFormatString='{0:f}' DataField="MarginRAte"
                                            Visible="false" />
                                        <asp:BoundField HeaderText="WSP" DataFormatString='{0:f}' DataField="MRPRat" Visible="false" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td>
                                <asp:GridView runat="server" BorderWidth="1" ID="gvprocessaccesscost" CssClass="myleft"
                                    Caption="Product Cost" GridLines="Both" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true"
                                    OnRowDataBound="gvfabriccost_rowbound" Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Process" DataField="Process" />
                                        <asp:BoundField HeaderText="Cost" DataField="Cost" DataFormatString="{0:n}" />
                                        <%--  <asp:TemplateField HeaderText="Cost" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:Label ID="txtProcessType" Enabled="true" Text='<%# Eval("Process")%>' Width="180px"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtProcessCost" Enabled="true" Text='<%# Eval("Cost","{0:n}")%>'
                                                    Width="70px" runat="server" CssClass="form-control"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%-- <asp:GridView ID="gridrawmaterial" runat="server" AllowPaging="false" CssClass="myleft"  GridLines="Vertical" AlternatingRowStyle-CssClass="even"
                                    ShowFooter="true" OnRowDataBound="Gridrawmaterial_rowdatabound" AutoGenerateColumns="false"
                                    EmptyDataText="No data found!" ShowHeaderWhenEmpty="True">--%>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridrawmaterial" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" OnRowDataBound="Gridrawmaterial_rowdatabound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;" ShowFooter="true" Caption="Accessories">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="prodname" HeaderText="Accessories Code" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Qty" HeaderText="Total Qty" DataFormatString='{0:0}' ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="purchaserate" HeaderText="Purchase Rate / Qty" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="totrate" HeaderText="Total Rate" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="595px" border="0.5px" class="style1">
                        <tr runat="server" visible="false" valign="top">
                            <td id="Td1" runat="server" visible="false" style="border-bottom: 1px solid black"
                                colspan="3" width="595px">
                                <label style="font-weight: bold">
                                    Detailed Cutting Report</label>
                                <asp:GridView runat="server" Visible="false" BorderWidth="1" ID="gridprint" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="106.5%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Party Name" DataField="ledgername" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="Fit" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="30-FS" DataField="30FS" />
                                        <asp:BoundField HeaderText="32-FS" DataField="32FS" />
                                        <asp:BoundField HeaderText="34-FS" DataField="34FS" />
                                        <asp:BoundField HeaderText="36-FS" DataField="36FS" />
                                        <asp:BoundField HeaderText="XS-FS" DataField="XSFS" />
                                        <asp:BoundField HeaderText="S-FS" DataField="SFS" />
                                        <asp:BoundField HeaderText="M-FS" DataField="MFS" />
                                        <asp:BoundField HeaderText="L-FS" DataField="LFS" />
                                        <asp:BoundField HeaderText="XL-FS" DataField="XLFS" />
                                        <asp:BoundField HeaderText="XXL-FS" DataField="XXLFS" />
                                        <asp:BoundField HeaderText="3XL-FS" DataField="3XLFS" />
                                        <asp:BoundField HeaderText="4XL-FS" DataField="4XLFS" />
                                        <asp:BoundField HeaderText="30-HS" DataField="30HS" />
                                        <asp:BoundField HeaderText="32-HS" DataField="32HS" />
                                        <asp:BoundField HeaderText="34-HS" DataField="34HS" />
                                        <asp:BoundField HeaderText="36-HS" DataField="36HS" />
                                        <asp:BoundField HeaderText="XS-HS" DataField="XSHS" />
                                        <asp:BoundField HeaderText="S-HS" DataField="SHS" />
                                        <asp:BoundField HeaderText="M-HS" DataField="MHS" />
                                        <asp:BoundField HeaderText="L-HS" DataField="LHS" />
                                        <asp:BoundField HeaderText="XL-HS" DataField="XLHS" />
                                        <asp:BoundField HeaderText="XXL-HS" DataField="XXLHS" />
                                        <asp:BoundField HeaderText="3XL-HS" DataField="3XLHS" />
                                        <asp:BoundField HeaderText="4XL-HS" DataField="4XLHS" />
                                        <asp:BoundField HeaderText="Total FS" DataField="TotFS" />
                                        <asp:BoundField HeaderText="Total HS" DataField="TotHS" />
                                        <asp:BoundField HeaderText="Total Qty" DataField="Qty" />
                                        <asp:BoundField HeaderText="Avg Meter" DataFormatString='{0:f}' DataField="AvgMtr" />
                                        <asp:BoundField HeaderText="Avg Rate" DataFormatString='{0:f}' DataField="AvgRate" />
                                        <asp:BoundField HeaderText="Margin" DataFormatString='{0:f}' DataField="MarginRAte" />
                                        <asp:BoundField HeaderText="WSP" DataFormatString='{0:f}' DataField="MRPRat" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridnewprint" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="106.5%" OnRowCreated="gridnewprint_RowCreated"
                                    OnRowDataBound="gridnewprint_RowDataBound" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Party Name" DataField="cut" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="Fit" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="30-FS" DataField="30FS" />
                                        <asp:BoundField HeaderText="32-FS" DataField="32FS" />
                                        <asp:BoundField HeaderText="34-FS" DataField="34FS" />
                                        <asp:BoundField HeaderText="36-FS" DataField="36FS" />
                                        <asp:BoundField HeaderText="XS-FS" DataField="XSFS" />
                                        <asp:BoundField HeaderText="S-FS" DataField="SFS" />
                                        <asp:BoundField HeaderText="M-FS" DataField="MFS" />
                                        <asp:BoundField HeaderText="L-FS" DataField="LFS" />
                                        <asp:BoundField HeaderText="XL-FS" DataField="XLFS" />
                                        <asp:BoundField HeaderText="XXL-FS" DataField="XXLFS" />
                                        <asp:BoundField HeaderText="3XL-FS" DataField="3XLFS" />
                                        <asp:BoundField HeaderText="4XL-FS" DataField="4XLFS" />
                                        <asp:BoundField HeaderText="30-HS" DataField="30HS" />
                                        <asp:BoundField HeaderText="32-HS" DataField="32HS" />
                                        <asp:BoundField HeaderText="34-HS" DataField="34HS" />
                                        <asp:BoundField HeaderText="36-HS" DataField="36HS" />
                                        <asp:BoundField HeaderText="XS-HS" DataField="XSHS" />
                                        <asp:BoundField HeaderText="S-HS" DataField="SHS" />
                                        <asp:BoundField HeaderText="M-HS" DataField="MHS" />
                                        <asp:BoundField HeaderText="L-HS" DataField="LHS" />
                                        <asp:BoundField HeaderText="XL-HS" DataField="XLHS" />
                                        <asp:BoundField HeaderText="XXL-HS" DataField="XXLHS" />
                                        <asp:BoundField HeaderText="3XL-HS" DataField="3XLHS" />
                                        <asp:BoundField HeaderText="4XL-HS" DataField="4XLHS" />
                                        <asp:BoundField HeaderText="Total FS" DataField="TotFS" />
                                        <asp:BoundField HeaderText="Total HS" DataField="TotHS" />
                                        <asp:BoundField HeaderText="Total Qty" DataField="Qty" />
                                        <asp:BoundField HeaderText="Avg Meter" DataFormatString='{0:f}' DataField="AvgMtr" />
                                        <asp:BoundField HeaderText="Avg Rate" DataFormatString='{0:f}' DataField="AvgRate" />
                                        <asp:BoundField HeaderText="Margin" DataFormatString='{0:f}' DataField="MarginRAte" />
                                        <asp:BoundField HeaderText="WSP" DataFormatString='{0:f}' DataField="MRPRat" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="50%" height="100px" border="0" class="style1">
                        <tr runat="server" visible="true">
                            <td valign="top" align="left" style="width: 60%">
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;" Visible="false">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField DataField="Fitt" HeaderText="Type/Size" HeaderStyle-Width="25px" />
                                        <asp:BoundField HeaderText="30" DataField="s30" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="32" DataField="s32" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="34" DataField="s34" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="36" DataField="s36" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="XS" DataField="sxs" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="S" DataField="ss" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="M" DataField="sm" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="L" DataField="sl" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="XL" DataField="sxl" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="XXL" DataField="sxxl" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="3XL" DataField="s3xl" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="4XL" DataField="s4xl" ItemStyle-HorizontalAlign="Center" />
                                        <%--  <asp:BoundField HeaderText="36HS" DataField="ts" />
                                        <asp:BoundField HeaderText="38HS" DataField="te" />
                                        <asp:BoundField HeaderText="39HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44HS" DataField="ffhs" />--%>
                                        <asp:BoundField HeaderText="Total" DataField="tot" ItemStyle-HorizontalAlign="Center" />
                                        <%--  <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:BoundField HeaderText="Total Shirt" DataField="tott" />--%>
                                        <%--  <asp:BoundField HeaderText="36FS" DataField="tsfs" />
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
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td id="Td2" runat="server" valign="top" align="left" style="width: 20%" visible="false">
                                <label style="font-weight: bold">
                                    Fabric Meter Calculation</label>
                                <div>
                                    <asp:GridView runat="server" BorderWidth="1" ID="fablistcalcalcuationgrid" CssClass="myleft"
                                        GridLines="Horizontal" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                        ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                                        font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Shirt Type" DataField="stype" />
                                            <asp:BoundField HeaderText="Fabric Name" DataField="Designno" />
                                            <asp:BoundField HeaderText="Given Meter" DataFormatString='{0:f}' DataField="Givenmeter" />
                                            <asp:BoundField HeaderText="Used Meter" DataFormatString='{0:f}' DataField="Reqmeter" />
                                            <asp:BoundField HeaderText="End Meter" DataFormatString='{0:f}' DataField="Endbit" />
                                            <asp:BoundField HeaderText="Avg. Meter" DataFormatString='{0:f}' DataField="avgmeter" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td runat="server" visible="false" valign="top" align="left" style="width: 40%">
                                <label style="font-weight: bold">
                                    Cost Rate Calculation</label>
                                <div>
                                    <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myleft" GridLines="Vertical"
                                        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                        PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                                        font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Total Meter" DataFormatString='{0:f}' DataField="met" />
                                            <asp:BoundField HeaderText="Dmg/ shortage Meter" DataFormatString='{0:f}' DataField="dsmet" />
                                            <asp:BoundField HeaderText="Actual Meter" DataFormatString='{0:f}' DataField="amet" />
                                            <asp:BoundField Visible="false" HeaderText="Rate" DataFormatString='{0:f}' DataField="rat" />
                                            <asp:BoundField HeaderText="Total" DataFormatString='{0:f}' DataField="tot" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td valign="top" runat="server" visible="false" align="left" style="width: 50px">
                                <label style="font-weight: bold">
                                    Customer Labels Details</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridlabel" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Party Name" DataField="cut" />
                                        <asp:BoundField HeaderText="Main Label" DataField="MainLabel" />
                                        <asp:BoundField HeaderText="Fit Label" DataField="flab" />
                                        <asp:BoundField HeaderText="Wash care label" DataField="wlab" />
                                        <asp:BoundField HeaderText="Logo Embrodeng" DataField="llab" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr runat="server" visible="false">
                            <td style="font-weight: bold">
                                <label>
                                    Avg. Meter:</label>
                                <asp:Label ID="Lblvalue" runat="server"></asp:Label>
                            </td>
                            <td style="font-weight: bold; font-size: 12px">
                                <label>
                                    Fab.Rate +Prod.Cost(Rs.<asp:Label runat="server" ID="lblmrp"></asp:Label>):</label>
                                <asp:Label ID="lblratee" runat="server"></asp:Label>
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
