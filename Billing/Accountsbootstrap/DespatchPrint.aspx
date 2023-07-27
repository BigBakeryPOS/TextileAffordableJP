<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DespatchPrint.aspx.cs"
    Inherits="Billing.Accountsbootstrap.DespatchPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Despatch</title>
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
    <style>
        .floating-box
        {
            display: inline-block;
            width: 150px;
            height: 75px;
            margin: 10px;
            border: 3px solid #73AD21;
        }
        
        .after-box
        {
            border: 3px solid red;
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
        .watermark
        {
            display: block;
            z-index: 99999;
            width: 86%;
            position: absolute;
            text-align: center !important;
        }
        .watermark img
        {
            opacity: 0.2;
            filter: alpha(opacity=15);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%--<img id="imgchkmark" runat="server" alt="imglogow" style="padding-top: 241px;margin-right: -85px;" />--%>
    <center>
        <%--<div class="watermark">
            <div>
                <img id="imgchkmark" runat="server" src="../images/jplogo%201.png" alt="imglogow"
                    style="padding-top: 160px; margin-right: -85px; width: 15pc;" /></div>
        </div>--%>
        <asp:Panel ID="Panel" runat="server">
            <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px"
                width="100%" height="100px" class="style1">
                <tr>
                    <td style="height: 1px">
                        <table width="100%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                            class="style1">
                            <tr>
                                <td align="center">
                                    <label id="lblDeliveryprint" runat="server" style="font-size: large; font-weight: bold">
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                            class="style1">
                            <tr>
                                <td width="20%" valign="top" align="left">
                                    <asp:Image ID="Image2" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 6pc;" runat="server" />
                                </td>
                                <td width="50%" valign="top" align="center">
                                    <asp:Image ID="Image1" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 17pc;" runat="server" /><br />
                                    <asp:Label ID="Label10" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> No.30, JP KOIL STREET, </asp:Label><br />
                                    <asp:Label ID="Label12" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> OLD WASHERMENPET </asp:Label><br />
                                    <asp:Label ID="Label29" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> CHENNAI-600021 </asp:Label><br />
                                </td>
                                <td width="30%" valign="top" align="left">
                                    GSTIN :
                                    <asp:Label ID="Label40" runat="server" Style="font-weight: 500; text-align: left"> 33ACCPR4802M1ZK </asp:Label><br />
                                    PH.NO :
                                    <asp:Label ID="Label16" runat="server" Style="font-weight: 500;"> 044 - 25967391 </asp:Label><br />
                                    Mobile.NO :
                                    <asp:Label ID="lblmblrpll" Visible="false" runat="server" Style="font-weight: 500;"> +91 7358650703 </asp:Label>
                                    <asp:Label ID="lblmblbc" Visible="false" runat="server" Style="font-weight: 500;"> +91 9176290701 </asp:Label><br />
                                    E-Mail :
                                    <asp:Label ID="Label17" runat="server" Style="font-weight: 500; text-align: left"> jpfashion21@gmail.com </asp:Label><br />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" style="padding-top: 10px" border="0" class="style1">
                            <tr>
                                <td width="50%" valign="top" align="left">
                                    <asp:Label ID="Label7" runat="server" Style="width: 100%;">
                                Cusmtomer Name : </asp:Label>
                                    <asp:Label ID="lbllcustname" runat="server" Style="font-size: larger; font-weight: bold"></asp:Label><br />
                                    <asp:Label ID="Label9" runat="server" Style="width: 100px;">
                                Address : </asp:Label>
                                    <asp:Label ID="lblladdress" runat="server" Style="font-size: larger; font-weight: bold"></asp:Label><br />
                                    <asp:Label ID="Label11" runat="server" Style="width: 100px;">
                                MobileNo : </asp:Label>
                                    <asp:Label ID="lbllmobile" runat="server" Style="font-size: larger; font-weight: bold"></asp:Label><br />
                                    <asp:Label ID="Label30" runat="server" Style="width: 100px;">
                                GSTIN : </asp:Label>
                                    <asp:Label ID="lblgastin" runat="server" Style="font-size: larger; font-weight: bold"></asp:Label><br />
                                    <asp:Label ID="Label19" runat="server" Style="width: 100px;" Visible="false">
                                PhoneNo : </asp:Label>
                                    <asp:Label ID="lbllphone" runat="server" Visible="false"></asp:Label><br />
                                    <asp:Label ID="Label18" runat="server" Style="width: 100px;" Visible="false">
                                E-Mail : </asp:Label>
                                    <asp:Label ID="lbllemail" runat="server" Visible="false"></asp:Label><br />
                                </td>
                                <td width="20%">
                                </td>
                                <td width="30%" valign="top" align="left">
                                    <asp:Label ID="Label13" runat="server" Style="width: 100%;">
                                DC.No : </asp:Label>
                                    <asp:Label ID="lblldcno" runat="server" Style="font-size: larger; font-weight: bold"></asp:Label><br />
                                    <asp:Label ID="Label15" runat="server" Style="width: 100px;">
                                DC.Date : </asp:Label>
                                    <asp:Label ID="lblldcdate" runat="server" Style="font-size: larger; font-weight: bold"></asp:Label><br />
                                    <asp:Label ID="Label21" runat="server" Style="width: 100px;">
                                Total Qty : </asp:Label>
                                    <asp:Label ID="lblltotalqty" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label28" runat="server" Style="width: 100px;">
                                Prepared By : </asp:Label>
                                    <asp:Label ID="lblldespatch" runat="server"></asp:Label><br />
                                </td>
                            </tr>
                        </table>
                        <table width="595px" height="100px" border="0" class="style1" runat="server" visible="false">
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label ID="Label1" runat="server" Style="width: 500%; font-weight: bold">
                                Lot Number : </asp:Label>
                                    <asp:Label ID="lblLot" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label2" runat="server" Style="width: 100px; font-weight: bold">
                                Date : </asp:Label>
                                    <asp:Label ID="lblDeldate" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label6" runat="server" Style="width: 100px; font-weight: bold">
                                Name : </asp:Label>
                                    <asp:Label ID="lblLedgerName" runat="server"></asp:Label><br />
                                </td>
                                <td valign="top" align="left">
                                    <asp:Label ID="Label3" runat="server" Style="width: 100px; font-weight: bold">
                                TotalQty : </asp:Label>
                                    <asp:Label ID="lblTotalQty" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label5" runat="server" Style="width: 100px; font-weight: bold">
                               TotalAmount : </asp:Label>
                                    <asp:Label ID="lblTotalAmount" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label4" runat="server" Style="width: 100px; font-weight: bold">
                                PaidAmount : </asp:Label>
                                    <asp:Label ID="lblPaidAmount" runat="server"></asp:Label><br />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" height="100px" border="1" class="style1">
                            <tr>
                                <td valign="top" align="left" style="width: 50%">
                                    <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                                        AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                        RowStyle-Height="1px" ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true"
                                        Width="100%" OnRowDataBound="gridprint_RowDataBound" Style="font-family: 'Trebuchet MS';
                                        font-size: 13px;">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                            <%--<asp:BoundField DataField="ItemName" HeaderText="ItemName" />
                                        <asp:BoundField DataField="Fit" HeaderText="Fit" />
                                        <asp:BoundField DataField="Patternname" HeaderText="Pattern" />
                                        <asp:BoundField DataField="rate" HeaderText="Rate" DataFormatString='{0:f}' />
                                        <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" />
                                        <asp:BoundField DataField="RemainQty" HeaderText="RemainQty" />
                                        <asp:BoundField DataField="Damageqty" HeaderText="Damageqty" />
                                        <asp:BoundField DataField="RecQty" HeaderText="RecQty" />--%>
                                            <%--<asp:BoundField DataField="SLEEVE" HeaderText="Sleeve" />--%>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CompanyLotNo" HeaderText="LotNo" />
                                            <asp:BoundField DataField="Itemname" HeaderText="Itemname" />
                                            <asp:BoundField DataField="DesignCode" HeaderText="DesignCode" />
                                            <asp:BoundField DataField="BrandName" HeaderText="BrandName" />
                                            <asp:BoundField DataField="FSMLXLXXL" HeaderText="Rate" DataFormatString="{0:f}" />
                                            <asp:BoundField DataField="S30" HeaderText="30-FS" />
                                            <asp:BoundField DataField="S32" HeaderText="32-FS" />
                                            <asp:BoundField DataField="S34" HeaderText="34-FS" />
                                            <asp:BoundField DataField="S36" HeaderText="36-FS" />
                                            <asp:BoundField DataField="XS" HeaderText="XS-FS" />
                                            <asp:BoundField DataField="S" HeaderText="S-FS" />
                                            <asp:BoundField DataField="M" HeaderText="M-FS" />
                                            <asp:BoundField DataField="L" HeaderText="L-FS" />
                                            <asp:BoundField DataField="XL" HeaderText="XL-FS" />
                                            <asp:BoundField DataField="XXL" HeaderText="2XL-FS" />
                                            <asp:BoundField DataField="F3XL4XL" HeaderText="Rate" DataFormatString="{0:f}" />
                                            <asp:BoundField DataField="S3XL" HeaderText="3XL-FS" />
                                            <asp:BoundField DataField="S4XL" HeaderText="4XL-FS" />
                                            <asp:BoundField DataField="HSMLXLXXL" HeaderText="Rate" DataFormatString="{0:f}" />
                                            <asp:BoundField DataField="HS30" HeaderText="30-HS" />
                                            <asp:BoundField DataField="HS32" HeaderText="32-HS" />
                                            <asp:BoundField DataField="HS34" HeaderText="34-HS" />
                                            <asp:BoundField DataField="HS36" HeaderText="36-HS" />
                                            <asp:BoundField DataField="HXS" HeaderText="XS-HS" />
                                            <asp:BoundField DataField="HS" HeaderText="S-HS" />
                                            <asp:BoundField DataField="HM" HeaderText="M-HS" />
                                            <asp:BoundField DataField="HL" HeaderText="L-HS" />
                                            <asp:BoundField DataField="HXL" HeaderText="XL-HS" />
                                            <asp:BoundField DataField="HXXL" HeaderText="2XL-HS" />
                                            <asp:BoundField DataField="H3XL4XL" HeaderText="Rate" DataFormatString="{0:f}" />
                                            <asp:BoundField DataField="HS3XL" HeaderText="3XL-HS" />
                                            <asp:BoundField DataField="HS4XL" HeaderText="4XL-HS" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                            class="style1">
                            <tr height="50px">
                                <td width="20%" align="center">
                                    <asp:Label ID="Label22" runat="server" Style="width: 500%; font-weight: bold">
                                PACKED BY </asp:Label>
                                </td>
                                <td width="40%" align="center">
                                    <asp:Label ID="Label23" runat="server" Style="width: 500%; font-weight: bold">
                               CHECKED BY </asp:Label>
                                </td>
                                <td width="20%" align="center">
                                    <asp:Label ID="Label24" runat="server" Style="width: 500%; font-weight: bold">
                                DELIVERED BY </asp:Label>
                                </td>
                            </tr>
                            <tr height="30px">
                                <td width="20%" align="center">
                                    <asp:Label ID="Label25" runat="server" Style="width: 500%; font-weight: bold">
                                    </asp:Label>
                                </td>
                                <td width="40%" align="center">
                                    <asp:Label ID="Label26" runat="server" Style="width: 500%; font-weight: bold">
                                    </asp:Label>
                                </td>
                                <td width="20%" align="center">
                                    <asp:Label ID="Label27" runat="server" Style="width: 500%; font-weight: bold">
                                    </asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                            class="style1">
                            <tr>
                                <td width="35%" align="left">
                                    <asp:Label ID="Label20" runat="server" Style="width: 100px; font-weight: bold">
                                Narration : </asp:Label>
                                    <asp:Label ID="lbllnarration" runat="server" Style="width: 500%; font-weight: bold"></asp:Label><br />
                                </td>
                                <td width="30%" align="left">
                                </td>
                                <td width="35%" align="left">
                                    <asp:GridView runat="server" BorderWidth="1" ID="gvlotqtyDetails" CssClass="myleft"
                                        GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                        ShowHeader="true">
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                        <Columns>
                                            <asp:BoundField DataField="LotNo" HeaderText="LotNo" ItemStyle-Width="20px" />
                                            <asp:BoundField DataField="DespatchQty" HeaderText="DespatchQty" ItemStyle-Width="20px" />
                                            <asp:BoundField DataField="GodownQty" HeaderText="GodownQty" ItemStyle-Width="20px" />
                                            <asp:BoundField DataField="BalanceQty" HeaderText="BalanceQty" ItemStyle-Width="20px" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table width="595px" class="style1">
            <tr>
                <td align="center">
                    <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" />
                    <asp:Button ID="btnexit" runat="server" Text="Exit" PostBackUrl="~/Accountsbootstrap/DespatchGrid.aspx" />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
