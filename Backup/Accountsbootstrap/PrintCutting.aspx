<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintCutting.aspx.cs" Inherits="Billing.Accountsbootstrap.PrintCutting" %>

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
     <div class="col-lg-6" id="sizediv" runat="server" visible="false" style="margin-left: -3pc;">
                                            <div class="panel panel-default" style="width: 170px">
                                                <label>
                                                    Size</label>
                                                <asp:CheckBoxList ID="chkSizes" 
                                                    RepeatDirection="Horizontal" RepeatColumns="2" CssClass="chkChoice1" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
        <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px"
            width="100%" height="100px" class="style1">
            <tr>
                <td style="height: 1px">
                    <table width="100%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr>
                            <td align="center">
                                <label style="font-size: large; font-weight: bold">
                                    Pre-Cutting Process:</label>
                                <asp:Label ID="lbbllott" Style="font-size: large; font-weight: bold" runat="server"></asp:Label>
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
                                <asp:Label ID="Label5" runat="server" Style="width: 100px; font-weight: bold">
                                Cutting Master : </asp:Label>
                                <asp:Label ID="lblcut" runat="server"></asp:Label><br />
                                <asp:Label Visible="false" ID="Label4" runat="server" Style="width: 100px; font-weight: bold">
                                Fit : </asp:Label>
                                <asp:Label ID="lblfit" Visible="false" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table width="80%" height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width:50%">
                                <label style="font-weight: bold">
                                    Overall Lot Report</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Both"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
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
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td id="Td1" runat="server" visible="true" valign="top"  align="left" style="width: 20%">
                                <label style="font-weight: bold">
                                    Fabric Meter Calculation</label>
                                <div>
                                    <asp:GridView runat="server" BorderWidth="1" ID="fablistcalcalcuationgrid" CssClass="myleft" GridLines="Horizontal"
                                        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                        PrintPageSize="30" AllowPrintPaging="true" Width="100%" 
                                        Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Shirt Type"  DataField="stype" />
                                            <asp:BoundField HeaderText="Fabric Name"  DataField="Designno" />
                                            <asp:BoundField HeaderText="Given Meter"  DataFormatString='{0:f}'  DataField="reqmeter" />
                                            <asp:BoundField HeaderText="Avg. Meter"  DataFormatString='{0:f}'  DataField="avgmeter" />
                                            
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td runat="server" visible="false" valign="top"  align="left" style="width: 20%">
                                <label style="font-weight: bold">
                                    Cost Rate Calculation</label>
                                <div>
                                    <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myleft" GridLines="Vertical"
                                        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                        PrintPageSize="30" AllowPrintPaging="true" Width="100%" 
                                        Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Total Meter" DataFormatString='{0:f}' DataField="met" />
                                            <asp:BoundField Visible="false" HeaderText="Rate" DataFormatString='{0:f}' DataField="rat" />
                                            <asp:BoundField HeaderText="Total" DataFormatString='{0:f}' DataField="tot" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td valign="top" align="left" style="width: 30%">
                                <label style="font-weight: bold">
                                   </label>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridlabel" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" 
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
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
                        <tr>
                            <td style="font-weight: bold">
                                <label>
                                    Avg. Meter:</label>
                                <asp:Label ID="Lblvalue" runat="server"></asp:Label>
                            </td>
                            <td runat="server" visible="false" style="font-weight: bold; font-size: 12px">
                                <label>
                                    Fab.Rate +Prod.Cost(Rs:<asp:Label ID="lblmrp" runat="server"></asp:Label>):</label>
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
                                <asp:GridView runat="server" Visible="false" BorderWidth="1" ID="gridprint" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="106.5%" 
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Party Name" DataField="brandname" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="Fit" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="30FS" DataField="30FS" />
                                        <asp:BoundField HeaderText="32FS" DataField="32FS" />
                                        <asp:BoundField HeaderText="34FS" DataField="34FS" />
                                        <asp:BoundField HeaderText="36FS" DataField="36FS" />
                                        <asp:BoundField HeaderText="XSFS" DataField="XSFS" />
                                        <asp:BoundField HeaderText="SFS" DataField="SFS" />
                                        <asp:BoundField HeaderText="MFS" DataField="MFS" />
                                        <asp:BoundField HeaderText="LFS" DataField="LFS" />
                                        <asp:BoundField HeaderText="XLFS" DataField="XLFS" />
                                        <asp:BoundField HeaderText="XXLFS" DataField="XXLFS" />
                                        <asp:BoundField HeaderText="3XLFS" DataField="3XLFS" />
                                        <asp:BoundField HeaderText="4XLFS" DataField="4XLFS" />

                                        <asp:BoundField HeaderText="30HS" DataField="30HS" />
                                        <asp:BoundField HeaderText="32HS" DataField="32HS" />
                                        <asp:BoundField HeaderText="34HS" DataField="34HS" />
                                        <asp:BoundField HeaderText="36HS" DataField="36HS" />
                                        <asp:BoundField HeaderText="XSHS" DataField="XSHS" />
                                        <asp:BoundField HeaderText="SHS"  DataField="SHS" />
                                        <asp:BoundField HeaderText="MHS"  DataField="MHS" />
                                        <asp:BoundField HeaderText="LHS"  DataField="LHS" />
                                        <asp:BoundField HeaderText="XLHS" DataField="XLHS" />
                                        <asp:BoundField HeaderText="XXLHS" DataField="XXLHS" />
                                        <asp:BoundField HeaderText="3XLHS" DataField="3XLHS" />
                                        <asp:BoundField HeaderText="4XLHS" DataField="4XLHS" />


                                        <asp:BoundField HeaderText="Total FS" DataField="TotFS" />
                                        <asp:BoundField HeaderText="Total HS" DataField="TotHS" />
                                        <asp:BoundField HeaderText="Total Qty" DataField="Qty" />
                                        <asp:BoundField HeaderText="Avg Meter" DataFormatString='{0:f}' DataField="AvgMtr" />
                                        <asp:BoundField HeaderText="Avg Rate" DataFormatString='{0:f}' DataField="AvgRate" />
                                        <asp:BoundField HeaderText="Margin" DataFormatString='{0:f}' DataField="MarginRAte" />
                                        <asp:BoundField HeaderText="WSP" DataFormatString='{0:f}' DataField="MRPRat" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridnewprint" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="106.5%" 
                                     Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Party Name" DataField="brandname" ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="Fit" ItemStyle-Height="60" />
                                         <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="30FS" DataField="30FS" />
                                        <asp:BoundField HeaderText="32FS" DataField="32FS" />
                                        <asp:BoundField HeaderText="34FS" DataField="34FS" />
                                        <asp:BoundField HeaderText="36FS" DataField="36FS" />
                                        <asp:BoundField HeaderText="XSFS" DataField="XSFS" />
                                        <asp:BoundField HeaderText="SFS" DataField="SFS" />
                                        <asp:BoundField HeaderText="MFS" DataField="MFS" />
                                        <asp:BoundField HeaderText="LFS" DataField="LFS" />
                                        <asp:BoundField HeaderText="XLFS" DataField="XLFS" />
                                        <asp:BoundField HeaderText="XXLFS" DataField="XXLFS" />
                                        <asp:BoundField HeaderText="3XLFS" DataField="3XLFS" />
                                        <asp:BoundField HeaderText="4XLFS" DataField="4XLFS" />

                                        <asp:BoundField HeaderText="30HS" DataField="30HS" />
                                        <asp:BoundField HeaderText="32HS" DataField="32HS" />
                                        <asp:BoundField HeaderText="34HS" DataField="34HS" />
                                        <asp:BoundField HeaderText="36HS" DataField="36HS" />
                                        <asp:BoundField HeaderText="XSHS" DataField="XSHS" />
                                        <asp:BoundField HeaderText="SHS"  DataField="SHS" />
                                        <asp:BoundField HeaderText="MHS"  DataField="MHS" />
                                        <asp:BoundField HeaderText="LHS"  DataField="LHS" />
                                        <asp:BoundField HeaderText="XLHS" DataField="XLHS" />
                                        <asp:BoundField HeaderText="XXLHS" DataField="XXLHS" />
                                        <asp:BoundField HeaderText="3XLHS" DataField="3XLHS" />
                                        <asp:BoundField HeaderText="4XLHS" DataField="4XLHS" />
                                        <asp:BoundField HeaderText="Total FS" DataField="TotFS" />
                                        <asp:BoundField HeaderText="Total HS" DataField="TotHS" />
                                        <asp:BoundField HeaderText="Total Qty" DataField="Qty" />
                                        <asp:BoundField HeaderText="Avg Meter" DataFormatString='{0:f}' DataField="AvgMtr" />
                                        <asp:BoundField HeaderText="Avg Rate" Visible="false"  DataFormatString='{0:f}' DataField="AvgRate" />
                                        <asp:BoundField HeaderText="Margin" Visible="false" DataFormatString='{0:f}' DataField="MarginRAte" />
                                        <asp:BoundField HeaderText="WSP" Visible="false" DataFormatString='{0:f}' DataField="MRPRat" />
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
