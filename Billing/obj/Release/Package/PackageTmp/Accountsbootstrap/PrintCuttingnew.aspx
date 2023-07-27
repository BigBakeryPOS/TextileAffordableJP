-<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintCuttingnew.aspx.cs"
    Inherits="Billing.Accountsbootstrap.PrintCuttingnew" %>

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
                <asp:CheckBoxList ID="chkSizes" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="chkChoice1"
                    runat="server">
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
                                <asp:Label ID="lblllot" Style="font-size: large; font-weight: bold" runat="server"></asp:Label>
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
                                <asp:Label ID="Label6" runat="server" Style="width: 100px; font-weight: bold">
                                RollTaka : </asp:Label>
                                <asp:Label ID="lblrolltaka" runat="server"></asp:Label><br />
                                <asp:Label ID="Label13" runat="server" Style="width: 100px; font-weight: bold">
                                Complete Stitching : </asp:Label>
                                <asp:Label ID="lblCompleteStitching" runat="server"></asp:Label><br />
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
                                <asp:Label Visible="true" ID="Label4" runat="server" Style="width: 100px; font-weight: bold">
                                Fit : </asp:Label>
                                <asp:Label ID="lblfitName" Visible="true" runat="server"></asp:Label>
                                <asp:Label ID="lblfit" Visible="false" runat="server"></asp:Label><br />
                                <label runat="server" style="width: 100px; font-weight: bold">
                                    Sleeve:</label>
                                <asp:Label ID="lblsleeve" runat="server"></asp:Label><br />
                                <label runat="server" style="width: 100px; font-weight: bold">
                                    Label Design:</label>
                                <asp:Label ID="lbllabeldesign" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="595px" runat="server" height="100px" border="0" class="style1">
                        <tr>
                            <td id="Td2" runat="server" visible="true" valign="top" align="left" style="width: 20%">
                                <label style="font-weight: bold">
                                    Process Status</label>
                                <div>
                                    <asp:GridView runat="server" BorderWidth="1" ID="gvProcessStatus" CssClass="myleft"
                                        GridLines="Horizontal" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                        ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                                        font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Process" DataField="Process" />
                                            <asp:BoundField HeaderText="Workers" DataField="Worker" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td id="Td3" runat="server" style="width: 1050px">
                            </td>
                            <td id="Td1" runat="server" visible="true" valign="top" align="left" style="width: 20%">
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
                                            <asp:BoundField HeaderText="Given KG" DataFormatString='{0:f}' DataField="reqmeter" />
                                            <asp:BoundField HeaderText="Avg.KG" DataFormatString='{0:f}' DataField="avgmeter" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td>
                                <label>
                                    Item Narration</label>
                                <asp:Label ID="lblitemnarrations" runat="server"></asp:Label>
                            </td>
                            <td runat="server" style="width: 50px">
                            </td>
                            <td valign="top" align="left" style="width: 40%">
                                <label style="font-weight: bold">
                                </label>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridlabel" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Brand Name" DataField="cut" />
                                        <asp:BoundField HeaderText="Main Label" DataField="cut" />
                                        <asp:BoundField HeaderText="Fit Label" DataField="flab" />
                                        <asp:BoundField HeaderText="Wash care label" DataField="wlab" />
                                        <asp:BoundField HeaderText="Logo Embrodeng" DataField="llab" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td runat="server" visible="false" valign="top" align="left" style="width: 50px">
                                <label style="font-weight: bold">
                                    Avg. Rate Calculation</label>
                                <div>
                                    <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myleft" GridLines="Vertical"
                                        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                        PrintPageSize="30" AllowPrintPaging="true" Width="100%" Style="font-family: 'Trebuchet MS';
                                        font-size: 13px;">
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
                        <tr runat="server" visible="true">
                            <td style="font-weight: bold">
                                <label>
                                    Avg. Meter:</label>
                                <asp:Label ID="Lblvalue" runat="server"></asp:Label>
                            </td>
                            <td style="font-weight: bold; font-size: 12px" visible="false">
                                <label>
                                    Avg.Rate +Prod.Cost(Rs.90):</label>
                                <asp:Label ID="lblratee" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="Tr1" runat="server" visible="false">
                            <td valign="top" align="left" style="width: 50px">
                                <label style="font-weight: bold">
                                    Overall Lot Report</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
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
                                        <asp:BoundField HeaderText="Total" DataField="tot" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="595px" border="0.5px" class="style1">
                        <tr valign="top">
                            <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                                <label style="font-weight: bold">
                                    Detailed Cutting Report</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridprint" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Ava.KG" DataField="totalmeter" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Req.KG" DataField="reqmeter" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
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
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridnewprint" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    OnRowDataBound="RatioShirtProcess_OnDataBound" ShowFooter="true" ShowHeader="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Design no" DataField="designno" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' Visible="false" />
                                        <asp:BoundField HeaderText="Req.KG" DataField="reqmeter" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" Visible="false" />
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
                                        <asp:BoundField HeaderText="Total " DataField="totfs" Visible="true" />
                                        <asp:BoundField HeaderText="Total " DataField="toths" Visible="true" />
                                        <asp:ImageField HeaderText="Sample Fabric" Visible="false" />
                                        <asp:BoundField HeaderText="Contrast" DataField="Contrast" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView4" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
                                        <asp:BoundField HeaderText="36-FS" DataField="tsfs" />
                                        <asp:BoundField HeaderText="38-FS" DataField="tefs" />
                                        <asp:BoundField HeaderText="39-FS" DataField="tnfs" />
                                        <asp:BoundField HeaderText="40-FS" DataField="fzfs" />
                                        <asp:BoundField HeaderText="42-FS" DataField="ftfs" />
                                        <asp:BoundField HeaderText="44-FS" DataField="fffs" />
                                        <asp:BoundField HeaderText="36-HS" DataField="tshs" />
                                        <asp:BoundField HeaderText="38-HS" DataField="tehs" />
                                        <asp:BoundField HeaderText="39-HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40-HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42-HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44-HS" DataField="ffhs" />
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView5" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
                                        <asp:BoundField HeaderText="36-FS" DataField="tsfs" />
                                        <asp:BoundField HeaderText="38-FS" DataField="tefs" />
                                        <asp:BoundField HeaderText="39-FS" DataField="tnfs" />
                                        <asp:BoundField HeaderText="40-FS" DataField="fzfs" />
                                        <asp:BoundField HeaderText="42-FS" DataField="ftfs" />
                                        <asp:BoundField HeaderText="44-FS" DataField="fffs" />
                                        <asp:BoundField HeaderText="36-HS" DataField="tshs" />
                                        <asp:BoundField HeaderText="38-HS" DataField="tehs" />
                                        <asp:BoundField HeaderText="39-HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40-HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42-HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44-HS" DataField="ffhs" />
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView6" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
                                        <asp:BoundField HeaderText="36-FS" DataField="tsfs" />
                                        <asp:BoundField HeaderText="38-FS" DataField="tefs" />
                                        <asp:BoundField HeaderText="39-FS" DataField="tnfs" />
                                        <asp:BoundField HeaderText="40-FS" DataField="fzfs" />
                                        <asp:BoundField HeaderText="42-FS" DataField="ftfs" />
                                        <asp:BoundField HeaderText="44-FS" DataField="fffs" />
                                        <asp:BoundField HeaderText="36-HS" DataField="tshs" />
                                        <asp:BoundField HeaderText="38-HS" DataField="tehs" />
                                        <asp:BoundField HeaderText="39-HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40-HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42-HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44-HS" DataField="ffhs" />
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView7" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
                                        <asp:BoundField HeaderText="36-FS" DataField="tsfs" />
                                        <asp:BoundField HeaderText="38-FS" DataField="tefs" />
                                        <asp:BoundField HeaderText="39-FS" DataField="tnfs" />
                                        <asp:BoundField HeaderText="40-FS" DataField="fzfs" />
                                        <asp:BoundField HeaderText="42-FS" DataField="ftfs" />
                                        <asp:BoundField HeaderText="44-FS" DataField="fffs" />
                                        <asp:BoundField HeaderText="36-HS" DataField="tshs" />
                                        <asp:BoundField HeaderText="38-HS" DataField="tehs" />
                                        <asp:BoundField HeaderText="39-HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40-HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42-HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44-HS" DataField="ffhs" />
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView8" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
                                        <asp:BoundField HeaderText="36-FS" DataField="tsfs" />
                                        <asp:BoundField HeaderText="38-FS" DataField="tefs" />
                                        <asp:BoundField HeaderText="39-FS" DataField="tnfs" />
                                        <asp:BoundField HeaderText="40-FS" DataField="fzfs" />
                                        <asp:BoundField HeaderText="42-FS" DataField="ftfs" />
                                        <asp:BoundField HeaderText="44-FS" DataField="fffs" />
                                        <asp:BoundField HeaderText="36-HS" DataField="tshs" />
                                        <asp:BoundField HeaderText="38-HS" DataField="tehs" />
                                        <asp:BoundField HeaderText="39-HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40-HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42-HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44-HS" DataField="ffhs" />
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView9" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
                                        <asp:BoundField HeaderText="36-FS" DataField="tsfs" />
                                        <asp:BoundField HeaderText="38-FS" DataField="tefs" />
                                        <asp:BoundField HeaderText="39-FS" DataField="tnfs" />
                                        <asp:BoundField HeaderText="40-FS" DataField="fzfs" />
                                        <asp:BoundField HeaderText="42-FS" DataField="ftfs" />
                                        <asp:BoundField HeaderText="44-FS" DataField="fffs" />
                                        <asp:BoundField HeaderText="36-HS" DataField="tshs" />
                                        <asp:BoundField HeaderText="38-HS" DataField="tehs" />
                                        <asp:BoundField HeaderText="39-HS" DataField="tnhs" />
                                        <asp:BoundField HeaderText="40-HS" DataField="fzhs" />
                                        <asp:BoundField HeaderText="42-HS" DataField="fths" />
                                        <asp:BoundField HeaderText="44-HS" DataField="ffhs" />
                                        <asp:BoundField HeaderText="Total FS" DataField="totfs" />
                                        <asp:BoundField HeaderText="Total HS" DataField="toths" />
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView10" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
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
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView11" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
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
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView12" Visible="false" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
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
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" Visible="false" ID="GridView13" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
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
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" Visible="false" ID="GridView14" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
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
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" Visible="false" ID="GridView15" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField HeaderText="S.No" HeaderStyle-Width="5%" DataField="Num" />
                                        <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="86" />
                                        <asp:BoundField HeaderText="Width" DataField="Width" DataFormatString='{0:f}' ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Ava.Meter" DataField="totalmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}'
                                            ItemStyle-Height="60" />
                                        <asp:BoundField HeaderText="Fit" DataField="fit" />
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
                                        <asp:ImageField HeaderText="Sample Fabric" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr height="50px">
                            <td width="20%" align="center">
                                <asp:Label ID="Label7" runat="server" Style="width: 500%; font-weight: bold">
                               CREATED BY </asp:Label>
                            </td>
                            <td width="40%" align="center">
                                <asp:Label ID="Label8" runat="server" Style="width: 500%; font-weight: bold">
                               CHECKED BY </asp:Label>
                            </td>
                            <td width="20%" align="center">
                                <asp:Label ID="Label9" runat="server" Style="width: 500%; font-weight: bold">
                                DELIVERED BY </asp:Label>
                            </td>
                        </tr>
                        <tr height="30px">
                            <td width="20%" align="center">
                                <asp:Label ID="Label10" runat="server" Style="width: 500%; font-weight: bold">
                                </asp:Label>
                            </td>
                            <td width="40%" align="center">
                                <asp:Label ID="Label11" runat="server" Style="width: 500%; font-weight: bold">
                                </asp:Label>
                            </td>
                            <td width="20%" align="center">
                                <asp:Label ID="Label12" runat="server" Style="width: 500%; font-weight: bold">
                                </asp:Label>
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
