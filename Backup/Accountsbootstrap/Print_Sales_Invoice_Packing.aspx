<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Sales_Invoice_Packing.aspx.cs"
    Inherits="Billing.Accountsbootstrap.Print_Sales_Invoice_Packing" %>

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
        .dotted-line
        {
            text-decoration: none;
            top: 10px;
        }
        .dotted-line:after
        {
            letter-spacing: 6px;
            font-size: 30px;
            color: #9cbfdb;
            display: inline-block;
            vertical-align: 3px;
            padding-left: 10px;
        }
        .style3
        {
            width: 15%;
        }
        .AlignLeft
        {
            text-align: left;
        }
        .verticalalg
        {
            vertical-align: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px"
            width="700px">
            <tr>
                <td colspan="3">
                    <table width="100%" style="border: solid 1px" border="1">
                        <tr>
                            <td colspan="4" align="center">
                                <label style="font-size: 30; font-weight: bold">
                                    INVOICE</label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border: solid 1px">
                        <tr>
                            <td width="18%" valign="top" align="center">
                                <asp:Image ID="imglogo" runat="server" ImageUrl="~/images/Flexiblelogo.jpg" Width="7pc"
                                    Style="background-position: bottom" />
                            </td>
                            <td width="15%" valign="top" align="left">
                                <asp:Label ID="lblCNddame" Font-Bold="true" runat="server" Style="text-transform: uppercase; font-size: 19px"></asp:Label><br />
                                <asp:Label ID="lblCAddress" CssClass="AlignLeft" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCAddressnew" CssClass="AlignLeft" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCCity" CssClass="AlignLeft" runat="server"></asp:Label>
                                <asp:Label ID="lblCPin" CssClass="AlignLeft" runat="server"></asp:Label>
                                <asp:Label ID="lblstate" CssClass="AlignLeft" runat="server"></asp:Label>
                                <asp:Label ID="lblCPhoneno" CssClass="AlignLeft" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCmobile" CssClass="AlignLeft" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCEmail" CssClass="AlignLeft" runat="server"></asp:Label><br />
                            </td>
                            <td class="verticalalg">
                            </td>
                            <td width="15%" align="left" valign="top">
                                <br />
                                
                                <asp:Label Font-Size="16px" runat="server" Text="Company's GSTIN :" ></asp:Label>
                                <asp:Label ID="lblcompanygstno" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label><br />
                                IEC No:<b><asp:Label ID="Label7" runat="server" Style="text-align: center"></asp:Label></b><br />
                               <%-- Company's PAN No :--%>
                                <asp:Label ID="lblcompanypanno" runat="server" Visible="false" Font-Bold="true" Font-Size="Large"></asp:Label><br />
                                 <%--DL No:<b><asp:Label ID="Label6" runat="server" Style="text-align: center"></asp:Label></b><br />--%>
                                            
                            </td>
                        </tr>
                        <tr>
                            <td width="35%" colspan="3" align="center" valign="bottom" style="border-right: solid 1px;
                                font-size: 14px" runat="server">
                                <b>
                            </td>
                            <td id="Td4" align="center" visible="false" valign="bottom" style="font-size: 14px"
                                runat="server">
                                <b>
                                    <asp:Label ID="lblCName" runat="server" Style="text-transform: uppercase; font-size: 19px"></asp:Label></b><br />
                                <asp:Label ID="Label1" Text="B.O.: 171, South Veli Street," runat="server"></asp:Label>
                                <asp:Label ID="Label2" Text="(V.Bose Dental Hospital Upstairs)" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="Label5" Text="TAMIL NADU," runat="server"></asp:Label>
                                <asp:Label ID="Label3" Text="MADURAI" runat="server"></asp:Label>-<asp:Label ID="Label4"
                                    Text="625 001." runat="server"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="1" >
                    <tr>
                    <td width="50%" >
                    
                                &nbsp
                                <label>
                                    Inv.No</label>
                                &nbsp&nbsp&nbsp&nbsp&nbsp
                                <label>
                                    :</label>
                                &nbsp<asp:Label Style="padding-left: 0px" ID="lblInvno" Visible="true" runat="server"></asp:Label><br />
                                &nbsp
                                <label>
                                    Inv.Date</label>
                                &nbsp&nbsp
                                <label>
                                    :</label>
                                &nbsp<asp:Label Style="padding-left: 0px" ID="lbldate" Visible="true" runat="server"></asp:Label><br />
                               
                    </td>
                    <td width="50%" >
                     &nbsp
                                <label>
                                    Order No</label>
                                &nbsp
                                <label>
                                    :</label>
                                &nbsp
                                <asp:Label Style="padding-left: 0px" ID="lblorderno" Visible="true" runat="server"></asp:Label><br />
                                &nbsp
                                <label>
                                    Order Date</label>
                                <label>
                                    :</label>&nbsp
                                <asp:Label Style="padding-left: 0px" ID="lblorderdate1" Visible="true" runat="server"></asp:Label><br />
                                &nbsp
                                <label>
                                    Transport
                                </label>
                                &nbsp
                                <label>
                                    :
                                </label>
                                &nbsp
                                <asp:Label Style="padding-left: 0px" ID="lbltransport" Visible="true" runat="server"></asp:Label><br />
                    </td>
                    </tr>
                        <tr>
                            <td width="50%" >
                                &nbsp&nbsp
                                <label style="font-size: 15px; font-weight: bold">
                                    Buyer</label><br />
                                &nbsp&nbsp
                                <asp:Label ID="lbllLedgerName" Font-Size="13px" runat="server" Style="font-weight: bold"></asp:Label><br />
                                &nbsp&nbsp
                                <asp:Label ID="lbllAddress" Font-Size="11px" runat="server" Style="font-weight: bold"></asp:Label><br />
                                &nbsp&nbsp
                                <asp:Label ID="lbllPincode" Font-Size="11px" runat="server" Style="font-weight: bold"></asp:Label><br />
                                &nbsp&nbsp
                                <asp:Label ID="lbllMobileNo" runat="server" Style="font-weight: bold"></asp:Label><br />
                                &nbsp&nbsp
                                <asp:Label ID="lbllPhoneNo" runat="server" Style="font-weight: bold"></asp:Label><br />
                                &nbsp&nbsp
                                <label>Buyer GST's:</label><asp:Label ID="lblgstin" runat="server" Style="font-weight: bold"></asp:Label><br />
                            </td>
                             <td width="50%" >
                             <label style="font-size: 15px; font-weight: bold">
                                                Ship To
                                            </label>
                                            <br />
                                            <asp:Label ID="lbllshipping" Font-Size="13px" runat="server" Style="font-weight: bold"></asp:Label>
                                            &nbsp&nbsp
                                            <asp:Label ID="lbllLedgerName1" runat="server" Style="font-weight: bold"></asp:Label><br />
                                            &nbsp&nbsp
                                            <asp:Label ID="lbllAddress1" Font-Size="11px" runat="server" Style="font-weight: bold"></asp:Label><br />
                                            &nbsp&nbsp
                                            <asp:Label ID="lbllPincode1" Font-Size="11px" runat="server" Style="font-weight: bold"></asp:Label><br />
                                            &nbsp&nbsp
                                            <asp:Label ID="lbllMobileNo1" runat="server" Style="font-weight: bold"></asp:Label><br />
                                            &nbsp&nbsp&nbsp<asp:Label ID="lbllPhoneNo1" runat="server" Style="font-weight: bold"></asp:Label><br />
                                            <br />
                             </td>
                           
                                
                            
                        </tr>
                    </table>
                    <table width="100%" border="1" runat="server" visible="false">
                        <tr>
                            <td width="54%" valign="top" style="border-right: solid 1px">
                                Place Of Supply &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp&nbsp<b><asp:Label Style="padding-left: 0px"
                                    ID="lblorderdate" runat="server" Visible="false"></asp:Label></b>
                                <br />
                                Despatched Through &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp&nbsp<b><asp:Label
                                    Style="padding-left: 0px" ID="lblthrough" Visible="true" runat="server"></asp:Label></b><br />
                                <b>
                                    <asp:Label Style="padding-left: 0px" ID="lbldespatchdate" Visible="false" runat="server"></asp:Label></b>
                                <b>
                                    <asp:Label Visible="false" Style="padding-left: 0px" ID="lblvechicle" runat="server"></asp:Label></b>
                            </td>
                            <td width="38%">
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border: solid 1px" runat="server" visible="false">
                        <tr>
                            <td>
                                Company's GSTTIN No: <b>
                                    <asp:Label ID="lbltinno" runat="server" Style="text-align: center"></asp:Label></b><br />
                            </td>
                            <td>
                                Export Code: <b>
                                    <asp:Label ID="lblexportcode" runat="server"></asp:Label></b>
                            </td>
                            <td id="Td1" runat="server" visible="false">
                                Company's PAN No:<b>ABAFA1138Q</b><br />
                                <b>
                                    <asp:Label ID="lbldlno" runat="server" Visible="false" Style="text-align: center"></asp:Label></b>
                                <b>
                                    <asp:Label ID="lblIECno" runat="server" Visible="false" Style="text-align: center"></asp:Label></b>
                            </td>
                        </tr>
                    </table>
                    <div runat="server" id="divgv" >
                        <asp:GridView runat="server" ID="gridprint" GridLines="Vertical" AutoGenerateColumns="false"
                            ShowHeader="true" Width="100%" OnRowDataBound="gridprint_RowDataBound" Style="font-family: 'Trebuchet MS';
                            font-size: 13px;">
                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                            <Columns>
                               <%-- <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField HeaderText="S.No" DataField="Row" />
                                <asp:BoundField HeaderText="Item" DataField="ItemName" />
                                <asp:BoundField HeaderText="Qty" DataField="Quantity" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Rate" DataField="UnitPrice" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:f}" />
                                <asp:BoundField HeaderText="Tax" DataField="Tax" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <table width="100%" border="1" style="height: 150px" class="style1">
                        <tr>
                            <td style="width: 70%">
                                <table id="Table4" border="0" class="style1" runat="server" visible="true">
                                    <tr>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <b>
                                                <label>
                                                    Rupees :</label></b><label id="lblamtinwords" runat="server"></label>
                                        </td>
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td align="left" colspan="3">
                                            <b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTax" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblDiscount" Visible="false" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="bnkco1" runat="server" visible="false">
                                        <td>
                                            <b>OUR BANK </b>
                                            <asp:Label ID="lblNofBundles" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblbnk" runat="server"></asp:Label>
                                            <asp:Label ID="lbllorry" Visible="false" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="acco1" runat="server" visible="false">
                                        <td>
                                            <b>A/c No.1 </b>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblacco1" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="acco2" runat="server" visible="false">
                                        <td>
                                            <b>A/c No.2 </b>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblacco2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="ifsco1" runat="server" visible="false">
                                        <td>
                                            <b>IFS CODE</b>
                                            <asp:Label ID="Label15" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblifscodee" runat="server"></asp:Label>
                                            <asp:Label Visible="false" ID="Label16" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" style="width: 30%">
                                <table border="1" class="style1">
                                    <tr>
                                        <td align="right" style="padding-right: 5px">
                                            Sub.Total
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblGrandtotalamt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td align="right" style="padding-right: 5px">
                                            Total Discount
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblTotDis" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="dg" runat="server" visible="false">
                                        <td id="dislbl" runat="server" align="right" style="padding-right: 5px">
                                            <asp:Label ID="lblDis" runat="server"></asp:Label>
                                        </td>
                                        <td id="amtlbl" runat="server" width="12%" style="text-align: right">
                                            <asp:Label ID="lblDiscountamt" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="fg" visible="false">
                                        <td id="Td2" runat="server" align="right" style="padding-right: 5px">
                                            <asp:Label ID="lblFreidfdghtAmt" runat="server">Freight Amount</asp:Label>
                                        </td>
                                        <td id="Td3" runat="server" width="12%" style="text-align: right">
                                            <asp:Label ID="lblFreightAmt" runat="server">0</asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td id="Td8" runat="server" align="right" style="padding-right: 5px">
                                            <asp:Label ID="Label17" runat="server">Total Amount Before Tax</asp:Label>
                                        </td>
                                        <td id="Td9" runat="server" width="12%" style="text-align: right">
                                            <asp:Label ID="lblTaxableValue" runat="server"></asp:Label>
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
                                    <tr runat="server" visible="false">
                                        <td align="right" style="padding-right: 5px">
                                            TAX AMOUNT : GST
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblTaxAmount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server" visible="false">
                                        <td id="tlbl" runat="server" align="right" style="padding-right: 5px">
                                            <asp:Label ID="lblt" Text="Gross Total" runat="server"></asp:Label>
                                        </td>
                                        <td id="ttxt" runat="server" width="12%" style="text-align: right">
                                            <asp:Label ID="lbltot" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="maintax" runat="server" visible="false">
                                        <td align="right" style="padding-right: 5px">
                                            <label id="lbltaxx" runat="server">
                                            </label>
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblVAT" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="mainroundoff" runat="server" visible="false">
                                        <td align="right" id="roundoff" runat="server" style="padding-right: 5px" width="150px">
                                            Round Off.
                                        </td>
                                        <td width="12%" style="text-align: right">
                                            <asp:Label ID="lblroundoff" runat="server"></asp:Label>
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
                                            <asp:Label ID="Label9" runat="server">Authorized Signature</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px"
                        width="100%" runat="server" visible="false">
                        <tr>
                            <td id="Td5" class="style3">
                                <label>
                                    PACKING:</label>
                                <asp:Label ID="lblpacking" runat="server"></asp:Label>
                            </td>
                            <td id="Td6" class="style3">
                                <label>
                                    CHECKED:</label>
                                <asp:Label ID="lblcheck" runat="server"></asp:Label>
                            </td>
                            <td id="Td7" class="style3">
                                <label>
                                    RE-CHECKED:</label>
                                <asp:Label ID="lblrecheck" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" runat="server" visible="false">
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                E.&O.E
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                            </td>
                            <td id="Co1" runat="server" visible="false" width="60%">
                                Company's Bank Details<br />
                                Bank Name&nbsp&nbsp:CANARA BANK<br />
                                C/A No&nbsp&nbsp:0961201001552<br />
                                Branch & IFS Code&nbsp&nbsp:TENKASI & CNRB0000961.
                            </td>
                            <td id="CO2" runat="server" visible="false" width="60%">
                                Company's Bank Details<br />
                                Bank Name&nbsp&nbsp:Karur Vysya Bank<br />
                                C/A No&nbsp&nbsp:1814135000000093<br />
                                Branch & IFS Code&nbsp&nbsp:New Siddhapudhur & KVBL0001814.
                            </td>
                            <td id="Co3" runat="server" visible="false" width="60%">
                                Company's Bank Details<br />
                                Bank Name&nbsp&nbsp:ICICI Bank<br />
                                C/A No&nbsp&nbsp:189705000483<br />
                                Branch & IFS Code&nbsp&nbsp:KOYAMBEDU & ICIC0001897.
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" colspan="2">
                                <b>Terms and Conditions:</b><br />
                                1.Advance Payment/COD Where ever applicable.<br />
                                2.Payment should be made by cheque/DD drawn in favour of APM MOTORS payable at Tenkasi.<br />
                                3.All Disputes Subject to Tenkasi Jurisdication only.<br />
                                4.Material Return(if any) to be reported within 7 days of supplies otherwise it
                                will not be taken back.<br />
                                5.Warranty(if any)Limited to Replacemetn/Repair.<br />
                                6.Our Responsiblity ceases on account of improper handling/Misuse.
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td width="70%">
                                Declaration :
                                <br />
                                We Declare that this Invoice shows the actual price of the goods.
                                <br />
                                described and that all particulars are true and correct.
                                <br />
                                Goods once sold will not be taken back or exchanged.
                            </td>
                            <td align="center" width="30%" runat="server" visible="false">
                                For&nbsp&nbsp<asp:Label ID="lblcompany" runat="server" Style="text-transform: uppercase;
                                    font-weight: bolder"></asp:Label><br />
                                <br />
                                <br />
                                <br />
                                Authorized Signatory
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
    <center>
    </center>
    <center>
        <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" />
        <asp:Button ID="btnexit" runat="server" Text="Exit" OnClick="btnexit_Click" />
    </center>
    </form>
</body>
</html>
