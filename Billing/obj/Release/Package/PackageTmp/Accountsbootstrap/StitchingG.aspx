<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StitchingG.aspx.cs" Inherits="Billing.Accountsbootstrap.StitchingG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Process Print</title>
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
    <%--
    <script type="text/javascript">
        window.onload = function callButtonClickEvent() {
            document.getElementById('<%=btnprint.ClientId %>').click();
        }
</script>--%>
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
        
        
        .RowStyle
        {
            height: 10px;
        }
        .AlternateRowStyle
        {
            height: 50px;
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
                                <asp:Label ID="lblmblrpll" Visible="false" runat="server" Style="font-weight: 500;">  +91 98431-98770</asp:Label>
                                E-Mail :
                                <asp:Label ID="Label17" runat="server" Style="font-weight: 500; text-align: left"> flexibleapparels@gmail.com</asp:Label>
                                <asp:Label ID="lblmblbc" Visible="false" runat="server" Style="font-weight: 500;"> +91 9176290701 </asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 30%">
                                <asp:Label ID="Label20" runat="server" Style="width: 500%; font-weight: bold">
                                Work Order Number : </asp:Label>
                                <asp:Label ID="lblworkorder" runat="server" Style="width: 100%; font-weight: bold"></asp:Label><br />
                                <asp:Label ID="Label1" runat="server" Style="width: 500%; font-weight: bold">
                                Cutting Number : </asp:Label>
                                <asp:Label ID="lblLot" runat="server" Style="width: 100%; font-weight: bold"></asp:Label><br />
                                <asp:Label ID="Label2" runat="server" Style="width: 100px; font-weight: bold">
                                Date : </asp:Label>
                                <asp:Label ID="lblDeldate" runat="server"></asp:Label><br />
                                <asp:Label ID="Label15" runat="server" Style="width: 100px; font-weight: bold">
                                GSTIN  : </asp:Label>
                                <asp:Label ID="lblgastin" runat="server"></asp:Label><br />
                            </td>
                            <td valign="top" align="center" style="width: 43%">
                                <asp:Image ID="Image1" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 8pc" runat="server" />
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
                            <td runat="server" visible="false" valign="top" align="right" style="width: 32%">
                                <asp:Label ID="Label10" runat="server" Style="width: 100px; font-weight: bold">
                                Sample : </asp:Label>
                                <asp:Label ID="lblsample" runat="server"></asp:Label><br />
                                <asp:Label ID="Label9" runat="server" Style="width: 100px; font-weight: bold">
                                Brand : </asp:Label>
                                <asp:Label ID="lblbrand" runat="server"></asp:Label><br />
                                <asp:Label ID="Label11" runat="server" Style="width: 100px; font-weight: bold">
                                Cutting Master : </asp:Label>
                                <asp:Label ID="lblcutmaster" runat="server"></asp:Label><br />
                                <div runat="server" visible="false">
                                    <asp:Label ID="Label3" runat="server" Style="width: 100px; font-weight: bold">
                                Total Qty : </asp:Label>
                                    <asp:Label ID="lblTotalQty" runat="server" Style="width: 100%; font-weight: bold"></asp:Label><br />
                                    <asp:Label ID="Label5" runat="server" Style="width: 100px; font-weight: bold">
                               WorkOrder Value : </asp:Label>
                                    <asp:Label ID="lblTotalAmount" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label8" runat="server" Style="width: 100px; font-weight: bold">
                                Rate : </asp:Label>
                                    <asp:Label ID="lbllrate" runat="server"></asp:Label><br />
                                    <asp:Label ID="lbllWash" runat="server" Style="width: 100px; font-weight: bold">
                                Washing : </asp:Label>
                                    <asp:Label ID="lblWash" runat="server"></asp:Label><br />
                                    <asp:Label ID="lbllEmbroidery" runat="server" Style="width: 100px; font-weight: bold">
                                Embroiding : </asp:Label>
                                    <asp:Label ID="lblEmbroidery" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label7" runat="server" Style="width: 100px; font-weight: bold">
                                Fit : </asp:Label>
                                    <asp:Label ID="lbllfit" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label14" runat="server" Style="width: 100px; font-weight: bold">
                                Model : </asp:Label>
                                    <asp:Label ID="lblmodel" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label18" runat="server" Style="width: 100px; font-weight: bold">
                                Compination : </asp:Label>
                                    <asp:Label ID="lblCompination" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label4" runat="server" Visible="false" Style="width: 100px; font-weight: bold">
                                PaidAmount : </asp:Label>
                                    <asp:Label ID="lblPaidAmount" runat="server" Visible="false"></asp:Label><br />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Label ID="Label6" runat="server" Style="width: 100px; font-weight: bold">
                                Service Provider Name : </asp:Label>
                                <asp:Label ID="lblLedgerName" Style="font-weight: bold" runat="server"></asp:Label><br />
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
                    <table width="80%" height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 50%">
                                <asp:GridView runat="server" ID="GridView1" CssClass="myleft" AutoGenerateColumns="false"
                                    ShowHeader="true" ShowFooter="true" Width="100%" OnRowDataBound="gvnewstiching_RowDataBound">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <FooterStyle HorizontalAlign="Center" />
                                    <RowStyle Font-Size="14px" />
                                    <Columns>
                                        <asp:BoundField DataField="recdate" HeaderText="Rec Date" DataFormatString="{0:dd/MM/yyyy}"
                                            Visible="true" ItemStyle-Height="7px" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Color" ItemStyle-Height="7px" />
                                        <asp:BoundField DataField="Fit" HeaderText="Fit" Visible="false" ItemStyle-Height="7px" />
                                        <asp:BoundField DataField="Patternname" HeaderText="Pattern" Visible="false" ItemStyle-Height="7px" />
                                        <asp:BoundField DataField="rate" HeaderText="Rate" DataFormatString='{0:f}' Visible="false"
                                            ItemStyle-Height="7px" />
                                        <asp:BoundField DataField="SendTotQty" HeaderText="SendQty" Visible="false" ItemStyle-Height="7px" />
                                        <asp:BoundField DataField="30FS" HeaderText="30" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Height="7px" />
                                        <asp:BoundField DataField="32FS" HeaderText="32" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Height="7px" />
                                        <asp:BoundField DataField="34FS" HeaderText="34" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="36FS" HeaderText="36" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="XSFS" HeaderText="XS" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Height="7px" />
                                        <asp:BoundField DataField="SFS" HeaderText="S" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="MFS" HeaderText="M" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="LFS" HeaderText="L" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="XLFS" HeaderText="XL" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="XXLFS" HeaderText="2XL" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="3XLFS" HeaderText="3XL" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="4XLFS" HeaderText="4XL" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="30HS" HeaderText="30" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="32HS" HeaderText="32" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="34HS" HeaderText="34" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="36HS" HeaderText="36" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="XSHS" HeaderText="XS" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="SHS" HeaderText="S" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="MHS" HeaderText="M" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="LHS" HeaderText="L" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="XLHS" HeaderText="XL" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="XXLHS" HeaderText="2XL" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="3XLHS" HeaderText="3XL" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="4XLHS" HeaderText="4XL" ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="GrandTtl" HeaderText="Total" ItemStyle-Font-Bold="true"
                                            ItemStyle-Height="7px" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridrawmaterial" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    Visible="false" ShowHeader="true" PrintPageSize="30" AllowPrintPaging="true"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;" ShowFooter="false" Caption="Accessories">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="prodname" HeaderText="Accessories Code" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Qty" HeaderText="Total Qty" DataFormatString='{0:0}' ItemStyle-HorizontalAlign="Center" />
                                        <%--  <asp:BoundField DataField="purchaserate" HeaderText="Purchase Rate / Qty" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="totrate" HeaderText="Total Rate" DataFormatString='{0:f}'
                                            ItemStyle-HorizontalAlign="Center" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left" style="width: 50%">
                                <asp:GridView runat="server" BorderWidth="1" ID="gvdamage" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    Caption="Damage" ShowFooter="true" PrintPageSize="30" AllowPrintPaging="true"
                                    Width="100%" OnRowDataBound="gvdamage_RowDataBound" Style="font-family: 'Trebuchet MS';
                                    font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <FooterStyle HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="recdate" HeaderText="Rec Date" DataFormatString="{0:dd/MM/yyyy}"
                                            Visible="false" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Color" />
                                        <asp:BoundField DataField="Fit" HeaderText="Fit" Visible="false" />
                                        <asp:BoundField DataField="Patternname" HeaderText="Pattern" Visible="false" />
                                        <asp:BoundField DataField="rate" HeaderText="Rate" DataFormatString='{0:f}' Visible="false" />
                                        <asp:BoundField DataField="SendTotQty" HeaderText="SendQty" Visible="false" />
                                        <asp:BoundField DataField="30FS" HeaderText="30F" />
                                        <asp:BoundField DataField="32FS" HeaderText="32F" />
                                        <asp:BoundField DataField="34FS" HeaderText="34F" />
                                        <asp:BoundField DataField="36FS" HeaderText="36F" />
                                        <asp:BoundField DataField="XSFS" HeaderText="XSF" />
                                        <asp:BoundField DataField="SFS" HeaderText="SF" />
                                        <asp:BoundField DataField="MFS" HeaderText="MF" />
                                        <asp:BoundField DataField="LFS" HeaderText="LF" />
                                        <asp:BoundField DataField="XLFS" HeaderText="XLF" />
                                        <asp:BoundField DataField="XXLFS" HeaderText="2XLF" />
                                        <asp:BoundField DataField="3XLFS" HeaderText="3XLF" />
                                        <asp:BoundField DataField="4XLFS" HeaderText="4XLF" />
                                        <asp:BoundField DataField="30HS" HeaderText="30H" />
                                        <asp:BoundField DataField="32HS" HeaderText="32H" />
                                        <asp:BoundField DataField="34HS" HeaderText="34H" />
                                        <asp:BoundField DataField="36HS" HeaderText="36H" />
                                        <asp:BoundField DataField="XSHS" HeaderText="XSH" />
                                        <asp:BoundField DataField="SHS" HeaderText="SH" />
                                        <asp:BoundField DataField="MHS" HeaderText="MH" />
                                        <asp:BoundField DataField="LHS" HeaderText="LH" />
                                        <asp:BoundField DataField="XLHS" HeaderText="XLH" />
                                        <asp:BoundField DataField="XXLHS" HeaderText="2XLH" />
                                        <asp:BoundField DataField="3XLHS" HeaderText="3XLH" />
                                        <asp:BoundField DataField="4XLHS" HeaderText="4XLH" />
                                        <asp:BoundField DataField="GrandTtl" HeaderText="Total" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr>
                            <td width="20%" align="center">
                                <asp:Label ID="Label23" runat="server" Style="width: 500%; font-weight: bold">
                               CHECKED BY </asp:Label>
                            </td>
                            <td width="40%" align="center">
                                <asp:Label ID="Label24" runat="server" Style="width: 500%; font-weight: bold">
                                DELIVERED BY </asp:Label>
                            </td>
                            <td width="20%" align="center">
                                <asp:Label ID="Label22" runat="server" Style="width: 500%; font-weight: bold">
                                RECEIVED BY </asp:Label>
                            </td>
                        </tr>
                        <tr height="50px">
                        </tr>
                        <tr runat="server" visible="false">
                            <td align="left" colspan="3">
                                <asp:Label ID="lbllContrasts" runat="server" Style="font-weight: bold; text-align: left">
                                </asp:Label>
                                <asp:Label ID="lblContrasts" runat="server" Style="font-weight: bold; text-align: left"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="Label27" runat="server" Style="width: 100px; font-weight: bold">
                               Item Narrations : </asp:Label>
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
                                <asp:Label ID="Label30" runat="server" Style="font-weight: 500; text-align: left"> 33CLNPS7587J1Z5 </asp:Label><br />
                                PH.NO :
                                <asp:Label ID="Label31" runat="server" Style="font-weight: 500;"> 0421-4238770 </asp:Label><br />
                            </td>
                            <td width="43%" valign="top" align="center">
                                <asp:Label ID="Label32" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit">83/84, Kavery Street,Odakkadu, </asp:Label><br />
                                <asp:Label ID="Label33" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> (Landmark: Deepa Hospital, Pushpa Threatre Dead End) </asp:Label><br />
                                <asp:Label ID="Label34" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> Tirupur – 641601. </asp:Label><br />
                                <asp:Label ID="Label35" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> Tamilnadu, INDIA. </asp:Label><br />
                            </td>
                            <td valign="top" align="right" style="width: 32%">
                                Mobile No:
                                <asp:Label ID="Label36" Visible="true" runat="server" Style="font-weight: 500;">  +91 98431-98770</asp:Label>
                                E-Mail :
                                <asp:Label ID="Label37" runat="server" Style="font-weight: 500; text-align: left"> flexibleapparels@gmail.com</asp:Label>
                                <asp:Label ID="Label38" Visible="false" runat="server" Style="font-weight: 500;"> +91 9176290701 </asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" class="style1">
                        <tr>
                            <asp:Label ID="Label25" runat="server" Style="width: 500%; font-weight: bold">
                                Work Order Number : </asp:Label>
                            <asp:Label ID="lblwrkorderrec" runat="server" Style="width: 100%; font-weight: bold"></asp:Label><br />
                            <td valign="top" align="left" style="width: 30%">
                                <asp:Label ID="Label39" runat="server" Style="width: 500%; font-weight: bold">
                                Lot Number : </asp:Label>
                                <asp:Label ID="lblLot1" runat="server" Style="width: 100%; font-weight: bold"></asp:Label><br />
                                <asp:Label ID="Label42" runat="server" Style="width: 100px; font-weight: bold">
                                Date : </asp:Label>
                                <asp:Label ID="lblDeldate1" runat="server"></asp:Label><br />
                                <asp:Label ID="Label44" runat="server" Style="width: 100px; font-weight: bold">
                                GSTIN  : </asp:Label>
                                <asp:Label ID="lblgastin1" runat="server"></asp:Label><br />
                            </td>
                            <td valign="top" align="center" style="width: 43%">
                                <asp:Image ID="Image2" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 8pc" runat="server" />
                            </td>
                            <td valign="top" align="right" style="width: 32%">
                                <asp:Label ID="Label46" runat="server" Style="width: 100px; font-weight: bold">
                                Sleeve : </asp:Label>
                                <asp:Label ID="lblsleeve1" runat="server"></asp:Label><br />
                                <asp:Label ID="Label48" runat="server" Style="width: 100px; font-weight: bold">
                                Label : </asp:Label>
                                <asp:Label ID="lbllabel1" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblitemname1" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td id="Td1" runat="server" visible="false" valign="top" align="right" style="width: 32%">
                                <asp:Label ID="Label51" runat="server" Style="width: 100px; font-weight: bold">
                                Sample : </asp:Label>
                                <asp:Label ID="Label52" runat="server"></asp:Label><br />
                                <asp:Label ID="Label53" runat="server" Style="width: 100px; font-weight: bold">
                                Brand : </asp:Label>
                                <asp:Label ID="Label54" runat="server"></asp:Label><br />
                                <asp:Label ID="Label55" runat="server" Style="width: 100px; font-weight: bold">
                                Cutting Master : </asp:Label>
                                <asp:Label ID="Label56" runat="server"></asp:Label><br />
                                <div id="Div1" runat="server" visible="false">
                                    <asp:Label ID="Label57" runat="server" Style="width: 100px; font-weight: bold">
                                Total Qty : </asp:Label>
                                    <asp:Label ID="Label58" runat="server" Style="width: 100%; font-weight: bold"></asp:Label><br />
                                    <asp:Label ID="Label59" runat="server" Style="width: 100px; font-weight: bold">
                               WorkOrder Value : </asp:Label>
                                    <asp:Label ID="Label60" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label61" runat="server" Style="width: 100px; font-weight: bold">
                                Rate : </asp:Label>
                                    <asp:Label ID="Label62" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label63" runat="server" Style="width: 100px; font-weight: bold">
                                Washing : </asp:Label>
                                    <asp:Label ID="Label64" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label65" runat="server" Style="width: 100px; font-weight: bold">
                                Embroiding : </asp:Label>
                                    <asp:Label ID="Label66" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label67" runat="server" Style="width: 100px; font-weight: bold">
                                Fit : </asp:Label>
                                    <asp:Label ID="Label68" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label69" runat="server" Style="width: 100px; font-weight: bold">
                                Model : </asp:Label>
                                    <asp:Label ID="Label70" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label71" runat="server" Style="width: 100px; font-weight: bold">
                                Compination : </asp:Label>
                                    <asp:Label ID="Label72" runat="server"></asp:Label><br />
                                    <asp:Label ID="Label73" runat="server" Visible="false" Style="width: 100px; font-weight: bold">
                                PaidAmount : </asp:Label>
                                    <asp:Label ID="Label74" runat="server" Visible="false"></asp:Label><br />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Label ID="Label75" runat="server" Style="width: 100px; font-weight: bold">
                                Service Provider Name : </asp:Label>
                                <asp:Label ID="lblLedgerName1" Style="font-weight: bold" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Label ID="Label281" runat="server" Style="width: 100px; font-weight: bold">
                                Item Name : </asp:Label>
                                <asp:Label ID="lblbitemname1" Style="font-weight: bold" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table width="80%" height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 50%">
                                <asp:GridView runat="server" ID="griddummy" CssClass="myleft" AutoGenerateColumns="false"
                                    ShowHeader="true" ShowFooter="true" Width="100%" OnRowDataBound="griddummy_RowDataBound">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:BoundField DataField="recdate" HeaderText="Rec Date" DataFormatString="{0:dd/MM/yyyy}"
                                            Visible="false" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Color" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="Fit" HeaderText="Fit" Visible="false" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="Patternname" HeaderText="Pattern" Visible="false" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="rate" HeaderText="Rate" DataFormatString='{0:f}' Visible="false"
                                            ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="SendTotQty" HeaderText="SendQty" Visible="false" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="30FS" HeaderText="30" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="32FS" HeaderText="32" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="34FS" HeaderText="34" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="36FS" HeaderText="36" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="XSFS" HeaderText="XS" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="SFS" HeaderText="S" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="MFS" HeaderText="M" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="LFS" HeaderText="L" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="XLFS" HeaderText="XL" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="XXLFS" HeaderText="2XL" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="3XLFS" HeaderText="3XL" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="4XLFS" HeaderText="4XL" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="30HS" HeaderText="30" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="32HS" HeaderText="32" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="34HS" HeaderText="34" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="36HS" HeaderText="36" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="XSHS" HeaderText="XS" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="SHS" HeaderText="S" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="MHS" HeaderText="M" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="LHS" HeaderText="L" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="XLHS" HeaderText="XL" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="XXLHS" HeaderText="2XL" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="3XLHS" HeaderText="3XL" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="4XLHS" HeaderText="4XL" ItemStyle-Height="4px" />
                                        <asp:BoundField DataField="GrandTtl" HeaderText="Total" ItemStyle-Height="4px" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr>
                            <td width="20%" align="center">
                                <asp:Label ID="lbljwhead" runat="server" Style="width: 500%; font-weight: bold">
                                </asp:Label><br />
                                (<asp:Label ID="lbljwname" runat="server" Style="width: 500%; font-weight: bold">
                                </asp:Label>)
                            </td>
                            <td id="Td2" runat="server" width="40%" align="center">
                                <asp:Label Visible="false" ID="Label76" runat="server" Style="width: 500%; font-weight: bold">
                                DELIVERED BY </asp:Label>
                            </td>
                            <td width="20%" align="center">
                                <asp:Label ID="Label77" runat="server" Style="width: 500%; font-weight: bold">
                                Flexible Apparels Staff BY </asp:Label>
                            </td>
                        </tr>
                        <tr height="60px">
                        </tr>
                        <tr id="Tr1" runat="server" visible="false">
                            <td align="left" colspan="3">
                                <asp:Label ID="Label78" runat="server" Style="font-weight: bold; text-align: left">
                                </asp:Label>
                                <asp:Label ID="Label79" runat="server" Style="font-weight: bold; text-align: left"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="Label41" runat="server" Style="width: 100px; font-weight: bold">
                               Item Narrations : </asp:Label>
                    <asp:Label ID="lblitemnarrations1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="595px" class="style1">
            <tr>
                <td align="center">
                    <asp:Button ID="btnprint" runat="server" Text="Print" OnClick="btnprint_Click" Visible="true" />
                    <%--<asp:Button ID="btnprintnew" runat="server" Text="Print"  />--%>
                    <asp:Button ID="btnexit" runat="server" Text="Exit" OnClick="btnexit_Click" />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
