<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IroningG.aspx.cs" Inherits="Billing.Accountsbootstrap.IroningG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ironing Print</title>
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
        
        #mynewfooter
        {
            font-weight: bold;
            color: Blue;
            horizontalalign: Left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px"
            width="100%" class="style1">
            <tr>
                <td style="height: 1px">
                    <table width="100%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblheadingname" runat="server" Style="font-size: large; font-weight: bold"></asp:Label>
                                <asp:Label ID="lbllscutno" Visible="false" runat="server" Style="font-size: large;
                                    font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <b style="font-size: large; font-weight: bold">PROCESS: </b>
                                <asp:Label ID="lblprocessname" runat="server" Style="font-size: large; font-weight: bold"></asp:Label>
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
                            </td>
                            <td valign="top" align="right" style="width: 32%">
                                Mobile No:
                                <asp:Label ID="lblmblrpll" Visible="true" runat="server" Style="font-weight: 500;">  +91 98431-98770</asp:Label>
                                <br />
                                E-Mail :
                                <asp:Label ID="Label17" runat="server" Style="font-weight: 500; text-align: left"> flexibleapparels@gmail.com</asp:Label>
                                <asp:Label ID="lblmblbc" Visible="false" runat="server" Style="font-weight: 500;"> +91 9176290701 </asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" class="style1" width="100%">
                        <tr>
                            <td valign="top" align="left" style="width: 25%">
                                <asp:Label ID="Label8" runat="server" Style="width: 500%; font-weight: bold">
                                Work Order Number : </asp:Label>
                                <asp:Label ID="lblworkorder" runat="server" Style="width: 100%; font-weight: bold"></asp:Label><br />
                                <asp:Label ID="Label1" runat="server" Style="width: 500%; font-weight: bold">
                                Lot Number : </asp:Label>
                                <asp:Label ID="lblLot" runat="server" Style="width: 100%; font-weight: bold; font-size: 12px"></asp:Label><br />
                                <asp:Label ID="Label2" runat="server" Style="width: 100px; font-weight: bold">
                                Date : </asp:Label>
                                <asp:Label ID="lblDeldate" runat="server"></asp:Label><br />
                                <asp:Label ID="Label15" runat="server" Style="width: 100px; font-weight: bold">
                                GSTIN  : </asp:Label>
                                <asp:Label ID="lblgastin" runat="server"></asp:Label><br />
                                <asp:Label ID="Label9" runat="server" Style="width: 100px; font-weight: bold">
                                Brand : </asp:Label>
                                <asp:Label ID="lblbrand" runat="server"></asp:Label><br />
                            </td>
                            <td valign="top" align="center" style="width: 43%">
                                <asp:Image ID="Image1" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 8pc;"
                                    runat="server" />
                            </td>
                            <td valign="top" align="right" style="width: 32%">
                                <asp:Label ID="Label21" runat="server" Style="width: 100px; font-weight: bold">
                                Sleeve : </asp:Label>
                                <asp:Label ID="lblsleeve" runat="server"></asp:Label><br />
                                <asp:Label ID="Label26" runat="server" Style="width: 100px; font-weight: bold">
                                Label : </asp:Label>
                                <asp:Label ID="lbllabel" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblitemname" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td runat="server" visible="false" valign="top" align="left" style="width: 32%">
                                <asp:Label ID="Label3" runat="server" Style="width: 100px; font-weight: bold">
                                TotalQty : </asp:Label>
                                <asp:Label ID="lblTotalQty" runat="server" Style="width: 100%; font-weight: bold;
                                    font-size: large"></asp:Label><br />
                                <asp:Label ID="Label5" runat="server" Style="width: 100px; font-weight: bold">
                               TotalAmount : </asp:Label>
                                <asp:Label ID="lblTotalAmount" runat="server"></asp:Label><br />
                                <asp:Label ID="Label4" runat="server" Style="width: 100px; font-weight: bold">
                                PaidAmount : </asp:Label>
                                <asp:Label ID="lblPaidAmount" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Label ID="Label6" runat="server" Style="width: 100px; font-weight: bold">
                                Name : </asp:Label>
                                <asp:Label ID="lblLedgerName" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Label ID="Label28" runat="server" Style="width: 100px; font-weight: bold">
                                Item Name : </asp:Label>
                                <asp:Label ID="lblbitemname" Style="font-weight: bold" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table width="80%" height="100px">
                        <tr>
                            <td valign="top" align="left" style="width: 50%">
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView1" Visible="true" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    OnRowDataBound="gvnewstiching_RowDataBound" ShowFooter="true" ShowHeader="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <%--<asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>--%>
                                        <asp:BoundField DataField="RecDate" Visible="true" HeaderText="RecDate" DataFormatString='{0:dd/MM/yyyy}' />
                                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" Visible="false" />
                                        <asp:BoundField DataField="DesignCode" ItemStyle-HorizontalAlign="Left" HeaderText="Color"
                                            Visible="true" />
                                        <asp:BoundField DataField="Fit" HeaderText="Fit" Visible="false" />
                                        <asp:BoundField DataField="rate" HeaderText="Rate" DataFormatString='{0:f}' Visible="false" />
                                        <asp:BoundField DataField="RemainQty" HeaderText="RemainQty" Visible="false" />
                                        <%--   <asp:BoundField DataField="Damageqty" HeaderText="Damageqty" />
                                        <asp:BoundField DataField="RecQty" HeaderText="RecQty" />--%>
                                        <asp:BoundField DataField="Rec30FS" HeaderText="30" />
                                        <asp:BoundField DataField="Rec32FS" HeaderText="32" />
                                        <asp:BoundField DataField="Rec34FS" HeaderText="34" />
                                        <asp:BoundField DataField="Rec36FS" HeaderText="36" />
                                        <asp:BoundField DataField="RecXSFS" HeaderText="XS" />
                                        <asp:BoundField DataField="RecSFS" HeaderText="S" />
                                        <asp:BoundField DataField="RecMFS" HeaderText="M" />
                                        <asp:BoundField DataField="RecLFS" HeaderText="L" />
                                        <asp:BoundField DataField="RecXLFS" HeaderText="XL" />
                                        <asp:BoundField DataField="RecXXLFS" HeaderText="2XL" />
                                        <asp:BoundField DataField="Rec3XLFS" HeaderText="3XL" />
                                        <asp:BoundField DataField="Rec4XLFS" HeaderText="4XL" />
                                        <asp:BoundField DataField="Rec30HS" HeaderText="30" />
                                        <asp:BoundField DataField="Rec32HS" HeaderText="32" />
                                        <asp:BoundField DataField="Rec34HS" HeaderText="34" />
                                        <asp:BoundField DataField="Rec36HS" HeaderText="36" />
                                        <asp:BoundField DataField="RecXSHS" HeaderText="XS" />
                                        <asp:BoundField DataField="RecSHS" HeaderText="S" />
                                        <asp:BoundField DataField="RecMHS" HeaderText="M" />
                                        <asp:BoundField DataField="RecLHS" HeaderText="L" />
                                        <asp:BoundField DataField="RecXLHS" HeaderText="XL" />
                                        <asp:BoundField DataField="RecXXLHS" HeaderText="2XL" />
                                        <asp:BoundField DataField="Rec3XLHS" HeaderText="3XL" />
                                        <asp:BoundField DataField="Rec4XLHS" HeaderText="4XL" />
                                        <asp:BoundField DataField="GrandTtl" ItemStyle-Font-Bold="true" HeaderText="Total" />
                                    </Columns>
                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GVDamage" Visible="true" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    OnRowDataBound="GVDamage_RowDataBound" ShowFooter="true" ShowHeader="true" Caption="Damage"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField DataField="RecDate" HeaderText="RecDate" DataFormatString='{0:dd/MM/yyyy}'
                                            Visible="true" />
                                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" Visible="false" />
                                        <asp:BoundField DataField="DesignCode" HeaderText="Color" Visible="true" />
                                        <asp:BoundField DataField="Fit" HeaderText="Fit" Visible="false" />
                                        <asp:BoundField DataField="rate" HeaderText="Rate" DataFormatString='{0:f}' Visible="false" />
                                        <asp:BoundField DataField="RemainQty" HeaderText="RemainQty" Visible="false" />
                                        <asp:BoundField DataField="Rec30FS" HeaderText="30" />
                                        <asp:BoundField DataField="Rec32FS" HeaderText="32" />
                                        <asp:BoundField DataField="Rec34FS" HeaderText="34" />
                                        <asp:BoundField DataField="Rec36FS" HeaderText="36" />
                                        <asp:BoundField DataField="RecXSFS" HeaderText="XS" />
                                        <asp:BoundField DataField="RecSFS" HeaderText="S" />
                                        <asp:BoundField DataField="RecMFS" HeaderText="M" />
                                        <asp:BoundField DataField="RecLFS" HeaderText="L" />
                                        <asp:BoundField DataField="RecXLFS" HeaderText="XL" />
                                        <asp:BoundField DataField="RecXXLFS" HeaderText="2XL" />
                                        <asp:BoundField DataField="Rec3XLFS" HeaderText="3XL" />
                                        <asp:BoundField DataField="Rec4XLFS" HeaderText="4XL" />
                                        <asp:BoundField DataField="Rec30HS" HeaderText="30" />
                                        <asp:BoundField DataField="Rec32HS" HeaderText="32" />
                                        <asp:BoundField DataField="Rec34HS" HeaderText="34" />
                                        <asp:BoundField DataField="Rec36HS" HeaderText="36" />
                                        <asp:BoundField DataField="RecXSHS" HeaderText="XS" />
                                        <asp:BoundField DataField="RecSHS" HeaderText="S" />
                                        <asp:BoundField DataField="RecMHS" HeaderText="M" />
                                        <asp:BoundField DataField="RecLHS" HeaderText="L" />
                                        <asp:BoundField DataField="RecXLHS" HeaderText="XL" />
                                        <asp:BoundField DataField="RecXXLHS" HeaderText="2XL" />
                                        <asp:BoundField DataField="Rec3XLHS" HeaderText="3XL" />
                                        <asp:BoundField DataField="Rec4XLHS" HeaderText="4XL" />
                                        <asp:BoundField DataField="GrandTtl" HeaderText="Total" />
                                    </Columns>
                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" />
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="GVAlter" Visible="true" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    OnRowDataBound="GVAlter_RowDataBound" ShowFooter="true" ShowHeader="true" Caption="Alter"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="110%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField DataField="RecDate" HeaderText="RecDate" DataFormatString='{0:dd/MM/yyyy}'
                                            Visible="true" />
                                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" Visible="false" />
                                        <asp:BoundField DataField="DesignCode" HeaderText="DesignCode" Visible="true" />
                                        <asp:BoundField DataField="Fit" HeaderText="Fit" Visible="false" />
                                        <asp:BoundField DataField="rate" HeaderText="Rate" DataFormatString='{0:f}' Visible="true" />
                                        <asp:BoundField DataField="RemainQty" HeaderText="RemainQty" Visible="false" />
                                        <asp:BoundField DataField="Rec30FS" HeaderText="30F" />
                                        <asp:BoundField DataField="Rec32FS" HeaderText="32F" />
                                        <asp:BoundField DataField="Rec34FS" HeaderText="34F" />
                                        <asp:BoundField DataField="Rec36FS" HeaderText="36F" />
                                        <asp:BoundField DataField="RecXSFS" HeaderText="XSF" />
                                        <asp:BoundField DataField="RecSFS" HeaderText="SF" />
                                        <asp:BoundField DataField="RecMFS" HeaderText="MF" />
                                        <asp:BoundField DataField="RecLFS" HeaderText="LF" />
                                        <asp:BoundField DataField="RecXLFS" HeaderText="XLF" />
                                        <asp:BoundField DataField="RecXXLFS" HeaderText="2XLF" />
                                        <asp:BoundField DataField="Rec3XLFS" HeaderText="3XLF" />
                                        <asp:BoundField DataField="Rec4XLFS" HeaderText="4XLF" />
                                        <asp:BoundField DataField="Rec30HS" HeaderText="30H" />
                                        <asp:BoundField DataField="Rec32HS" HeaderText="32H" />
                                        <asp:BoundField DataField="Rec34HS" HeaderText="34H" />
                                        <asp:BoundField DataField="Rec36HS" HeaderText="36H" />
                                        <asp:BoundField DataField="RecXSHS" HeaderText="XSH" />
                                        <asp:BoundField DataField="RecSHS" HeaderText="SH" />
                                        <asp:BoundField DataField="RecMHS" HeaderText="MH" />
                                        <asp:BoundField DataField="RecLHS" HeaderText="LH" />
                                        <asp:BoundField DataField="RecXLHS" HeaderText="XLH" />
                                        <asp:BoundField DataField="RecXXLHS" HeaderText="2XLH" />
                                        <asp:BoundField DataField="Rec3XLHS" HeaderText="3XLH" />
                                        <asp:BoundField DataField="Rec4XLHS" HeaderText="4XLH" />
                                        <asp:BoundField DataField="GrandTtl" HeaderText="Total" />
                                    </Columns>
                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" />
                                </asp:GridView>
                                <asp:GridView runat="server" BorderWidth="1" ID="gvacessories" Visible="true" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    ShowFooter="true" ShowHeader="true" Caption="Accessories" Width="50%" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <Columns>
                                        <asp:BoundField DataField="Definition" HeaderText="Definition" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr height="50px">
                            <td width="20%" align="center">
                                <asp:Label ID="Label22" runat="server" Style="width: 500%; font-weight: bold">
                                RECEIVED BY </asp:Label>
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
                        <tr height="50px">
                        </tr>
                        <tr>
                            <td align="left" colspan="3">
                                <asp:Label ID="Label20" runat="server" Style="width: 100px; font-weight: bold">
                                Narration : </asp:Label>
                                <asp:Label ID="lbllnarration" runat="server" Style="font-weight: bold"></asp:Label><br />
                            </td>
                        </tr>
                        <%--<tr height="30px">
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
                        </tr>--%>
                    </table>
                    <label>
                        Item Narration :</label>
                    <asp:Label ID="lblitemnarrations" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table id="tabledummy" runat="server" style="border-spacing: 1px; border-collapse: collapse;
            outline: black solid 1px" width="100%" height="100px" class="style1">
            <tr>
                <td style="height: 1px">
                    <table width="100%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblheadingname1" runat="server" Style="font-size: large; font-weight: bold"></asp:Label>
                                <asp:Label ID="lbllscutno1" Visible="false" runat="server" Style="font-size: large;
                                    font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <b style="font-size: large; font-weight: bold">PROCESS: </b>
                                <asp:Label ID="lblprocessname1" runat="server" Style="font-size: large; font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 25%">
                                GSTIN :
                                <asp:Label ID="Label11" runat="server" Style="font-weight: 500; text-align: left"> 33CLNPS7587J1Z5 </asp:Label><br />
                                PH.NO :
                                <asp:Label ID="Label14" runat="server" Style="font-weight: 500;"> 0421-4238770 </asp:Label><br />
                            </td>
                            <td width="43%" valign="top" align="center">
                                <asp:Label ID="Label18" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit">83/84, Kavery Street,Odakkadu, </asp:Label><br />
                                <asp:Label ID="Label25" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> (Landmark: Deepa Hospital, Pushpa Threatre Dead End) </asp:Label><br />
                                <asp:Label ID="Label27" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> Tirupur – 641601. </asp:Label><br />
                                <asp:Label ID="Label30" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> Tamilnadu, INDIA. </asp:Label><br />
                            </td>
                            <td valign="top" align="right" style="width: 32%">
                                Mobile No:
                                <asp:Label ID="Label31" Visible="true" runat="server" Style="font-weight: 500;">  +91 98431-98770</asp:Label>
                                <br />
                                E-Mail :
                                <asp:Label ID="Label32" runat="server" Style="font-weight: 500; text-align: left"> flexibleapparels@gmail.com</asp:Label>
                                <asp:Label ID="Label33" Visible="false" runat="server" Style="font-weight: 500;"> +91 9176290701 </asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" class="style1" width="100%">
                        <tr>
                            <asp:Label ID="Label7" runat="server" Style="width: 500%; font-weight: bold">
                                Work Order Number : </asp:Label>
                            <asp:Label ID="lblwrkorderrec" runat="server" Style="width: 100%; font-weight: bold"></asp:Label><br />
                            <td valign="top" align="left" style="width: 25%">
                                <asp:Label ID="Label34" runat="server" Style="width: 500%; font-weight: bold">
                                Lot Number : </asp:Label>
                                <asp:Label ID="lblLot1" runat="server" Style="width: 100%; font-weight: bold; font-size: 12px"></asp:Label><br />
                                <asp:Label ID="Label36" runat="server" Style="width: 100px; font-weight: bold">
                                Date : </asp:Label>
                                <asp:Label ID="lblDeldate1" runat="server"></asp:Label><br />
                                <asp:Label ID="Label38" runat="server" Style="width: 100px; font-weight: bold">
                                GSTIN  : </asp:Label>
                                <asp:Label ID="lblgastin1" runat="server"></asp:Label><br />
                                <asp:Label ID="Label41" runat="server" Style="width: 100px; font-weight: bold">
                                Brand : </asp:Label>
                                <asp:Label ID="lblbrand1" runat="server"></asp:Label><br />
                            </td>
                            <td valign="top" align="center" style="width: 43%">
                                <asp:Image ID="Image2" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 8pc;"
                                    runat="server" />
                            </td>
                            <td valign="top" align="right" style="width: 32%">
                                <asp:Label ID="Label43" runat="server" Style="width: 100px; font-weight: bold">
                                Sleeve : </asp:Label>
                                <asp:Label ID="lblsleeve1" runat="server"></asp:Label><br />
                                <asp:Label ID="Label45" runat="server" Style="width: 100px; font-weight: bold">
                                Label : </asp:Label>
                                <asp:Label ID="lbllabel1" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="Label47" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td id="Td1" runat="server" visible="false" valign="top" align="left" style="width: 32%">
                                <asp:Label ID="Label48" runat="server" Style="width: 100px; font-weight: bold">
                                TotalQty : </asp:Label>
                                <asp:Label ID="Label49" runat="server" Style="width: 100%; font-weight: bold; font-size: large"></asp:Label><br />
                                <asp:Label ID="Label50" runat="server" Style="width: 100px; font-weight: bold">
                               TotalAmount : </asp:Label>
                                <asp:Label ID="Label51" runat="server"></asp:Label><br />
                                <asp:Label ID="Label52" runat="server" Style="width: 100px; font-weight: bold">
                                PaidAmount : </asp:Label>
                                <asp:Label ID="Label53" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Label ID="Label54" runat="server" Style="width: 100px; font-weight: bold">
                                Name : </asp:Label>
                                <asp:Label ID="lblLedgerName1" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Label ID="Label56" runat="server" Style="width: 100px; font-weight: bold">
                                Item Name : </asp:Label>
                                <asp:Label ID="lblbitemname1" Style="font-weight: bold" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table width="80%" height="100px">
                        <tr>
                            <td valign="top" align="left" style="width: 50%">
                                <asp:GridView runat="server" ID="griddummy" CssClass="myleft" AutoGenerateColumns="false"
                                    ShowHeader="true" ShowFooter="true" Width="100%" OnRowDataBound="griddummy_RowDataBound">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField DataField="ItemName" HeaderText="ItemName" Visible="false" />
                                        <asp:BoundField DataField="DesignCode" HeaderText="Color" Visible="true" />
                                        <asp:BoundField DataField="Fit" HeaderText="Fit" Visible="false" />
                                        <asp:BoundField DataField="RecDate" HeaderText="RecDate" DataFormatString='{0:dd/MM/yyyy}'
                                            Visible="false" />
                                        <asp:BoundField DataField="rate" HeaderText="Rate" DataFormatString='{0:f}' Visible="false" />
                                        <asp:BoundField DataField="RemainQty" HeaderText="RemainQty" Visible="false" />
                                        <asp:BoundField DataField="Rec30FS" HeaderText="30" />
                                        <asp:BoundField DataField="Rec32FS" HeaderText="32" />
                                        <asp:BoundField DataField="Rec34FS" HeaderText="34" />
                                        <asp:BoundField DataField="Rec36FS" HeaderText="36" />
                                        <asp:BoundField DataField="RecXSFS" HeaderText="XS" />
                                        <asp:BoundField DataField="RecSFS" HeaderText="S" />
                                        <asp:BoundField DataField="RecMFS" HeaderText="M" />
                                        <asp:BoundField DataField="RecLFS" HeaderText="L" />
                                        <asp:BoundField DataField="RecXLFS" HeaderText="XL" />
                                        <asp:BoundField DataField="RecXXLFS" HeaderText="2XL" />
                                        <asp:BoundField DataField="Rec3XLFS" HeaderText="3XL" />
                                        <asp:BoundField DataField="Rec4XLFS" HeaderText="4XL" />
                                        <asp:BoundField DataField="Rec30HS" HeaderText="30" />
                                        <asp:BoundField DataField="Rec32HS" HeaderText="32" />
                                        <asp:BoundField DataField="Rec34HS" HeaderText="34" />
                                        <asp:BoundField DataField="Rec36HS" HeaderText="36" />
                                        <asp:BoundField DataField="RecXSHS" HeaderText="XS" />
                                        <asp:BoundField DataField="RecSHS" HeaderText="S" />
                                        <asp:BoundField DataField="RecMHS" HeaderText="H" />
                                        <asp:BoundField DataField="RecLHS" HeaderText="L" />
                                        <asp:BoundField DataField="RecXLHS" HeaderText="XL" />
                                        <asp:BoundField DataField="RecXXLHS" HeaderText="2XL" />
                                        <asp:BoundField DataField="Rec3XLHS" HeaderText="3XL" />
                                        <asp:BoundField DataField="Rec4XLHS" HeaderText="4XL" />
                                        <asp:BoundField DataField="GrandTtl" HeaderText="Total" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr height="50px">
                            <td width="20%" align="center">
                                <asp:Label ID="lbljwhead" runat="server" Style="width: 500%; font-weight: bold">
                                 IRONING/PACKING BY</asp:Label><br />
                                <asp:Label ID="lbljwname" runat="server" Style="width: 500%; font-weight: bold">
                                </asp:Label>
                            </td>
                            <td width="40%" align="center">
                                <asp:Label Visible="false" ID="Label58" runat="server" Style="width: 500%; font-weight: bold">
                               CHECKED BY </asp:Label>
                            </td>
                            <td id="Td2" width="20%" align="center" runat="server" visible="true">
                                <asp:Label ID="Label59" runat="server" Style="width: 500%; font-weight: bold">
                                Flexible Apparels Staff BY </asp:Label>
                            </td>
                        </tr>
                        <tr height="50px">
                        </tr>
                    </table>
                    <asp:Label ID="Label10" runat="server" Style="width: 100px; font-weight: bold">
                               Item Narrations : </asp:Label>
                    <asp:Label ID="lblitemnarration" runat="server"></asp:Label>
                </td>
            </tr>
            <%-- <tr>
                <td valign="top" align="left" style="width: 50%">
                   
                </td>
                <td valign="top" align="left" style="width: 10%">
                </td>
            </tr>--%>
        </table>
        <table width="595px" class="style1">
            <tr>
                <td align="center">
                    <asp:Button ID="btnprint" runat="server" Text="Print" OnClick="btnprint_Click" />
                    <asp:Button ID="btnexit" runat="server" Text="Exit" PostBackUrl="IroningGrid.aspx" />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
