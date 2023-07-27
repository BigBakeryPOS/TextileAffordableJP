<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyerSalesOrderPrintNew.aspx.cs"
    Inherits="Billing.Accountsbootstrap.BuyerSalesOrderPrintNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Buyer Sales Order Print</title>
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
        P.pagebreakhere
        {
            page-break-before: always;
        }
        .AlignLeft
        {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }
        
        #footer
        {
            font-size: 10px;
            color: Black;
            text-align: center;
        }
        
        
        @media print
        {
        
            #footer
            {
                position: fixed;
                bottom: 0;
            }
            table
            {
                page-break-inside: auto;
            }
            tr
            {
                page-break-inside: avoid;
                page-break-after: auto;
            }
            thead
            {
                display: table-header-group;
            }
            tfoot
            {
                display: table-footer-group;
            }
        }
        
        .top
        {
            vertical-align: top;
        }
        
        .pdleft
        {
            padding-left: 30px;
        }
        
        .pdright
        {
            text-align: right;
        }
        #MH3
        {
            width: 100%;
            border-collapse: collapse;
            border: 0px solid black;
            font-size: 13.5px;
            font-family: Calibri;
        }
        #MH4
        {
            width: 100%;
            border-collapse: collapse;
            border: 0px solid black;
            font-size: 13.5px;
            font-family: Calibri;
        }
        #MH2
        {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid black;
            font-size: 11.5px;
            font-family: Calibri;
        }
        #Table1
        {
            width: 100%;
            border-collapse: collapse;
            border: 0px solid black;
            font-size: 11.5px;
            font-family: Calibri;
        }
        #Table2
        {
            width: 100%;
            border-collapse: collapse;
            border: 0px solid black;
            font-size: 13.5px;
            font-family: Calibri;
        }
        #MH
        {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid black;
            font-size: 11.5px;
            font-family: Calibri;
        }
        
        #MH hr
        {
            height: 0;
            border: 0;
            border-top: 1px solid #083972;
        }
        
        #MH tr td
        {
            padding: 5px;
            border-right: 1px solid black;
        }
        #MH tr td:last-child
        {
            border-right: none;
        }
        
        #MH1
        {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid black;
            font-size: 11.5px;
            font-family: Calibri;
        }
        
        #MH1 hr
        {
            height: 0;
            border: 0;
            border-top: 1px solid #083972;
        }
        
        #MH1 tr td
        {
            padding: 5px;
            border-right: 1px solid black;
        }
        #MH1 tr td:last-child
        {
            border-right: none;
        }
        
        #RTbl
        {
            border-collapse: collapse;
        }
        
        /* SPACE CELLS
#RTbl td:not(:last-child) {
  padding-right: 15px;
} */
        
        #RTbl tr:not(:last-child)
        {
            border-bottom: 1px solid black;
        }
    </style>
    <style type="text/css">
        .style1
        {
            height: 50px;
        }
        .style2
        {
            height: 150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: larger; font-weight: bold">
                <asp:Label ID="lblFormName" runat="server" Text="SALES ORDER"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" id="Table2" border="0">
        <tr>
            <td style="border: 1px solid black">
                <table width="100%" id="MH" border="0" style="border: 0px solid black">
                    <tr>
                        <td style="width: 50%; border: 0px solid black; border-right: 1px solid black; border-bottom: 1px solid black;">
                            <div id="karthikamart" runat="server" visible="false">
                                <b>
                                    <label style="text-align: right; font-size: 22px;">
                                        A.KARTHIKA MARTS</label><br />
                                </b>
                                <label>
                                    2/232,Malligai cross street,
                                </label>
                                <br />
                                <label>
                                    1<sup>st</sup> Floor,Gomathipuram II road ,
                                </label>
                                <br />
                                <label>
                                    Madurai- 625020,Tamilnadu,India.
                                </label>
                                <br />
                                <label>
                                    GSTIN/UIN :</label>
                                <label>
                                    33AIEPA8062G1ZN</label>
                                <br />
                                <label>
                                    E-Mail :</label>
                                <label>
                                    info@blaackforest.com
                                </label>
                            </div>
                            <div id="blaackforest" runat="server" visible="true">
                                <%--<label>
                        Vendor Name :
                    </label>
                    <br />--%>
                    <table width="100%" id="Table3" border="0" style="border: 0px solid black">
                    <tr>
                        <td style="border: 0px solid black; border-right: 0px solid black; border-bottom: 0px solid black;">
                                <div id="idimglog" runat="server" visible="true">
                                    <asp:Image ID="imglogo" ImageAlign="Middle" runat="server" 
                                        Style="background-position: bottom; height: 72px; width: 84px" />
                                    <br />
                                </div>

                                </td>
                                  <td style="border: 0px solid black; border-right: 0px solid black; border-bottom: 0px solid black;">
                                 <b>
                                    <%-- <label style="text-align: right; font-size: 22px;">
                            BLAACKFOREST</label><br />--%>
                                    <asp:Label Text="" ID="lblFCompany" runat="server" Style="text-align: right; font-size: 22px;">
                                    </asp:Label><br />
                                </b>
                                <asp:Label Text="" ID="lblFAddress" runat="server">
                                    <%-- No.2/232 Malligai Cross Street--%>                                    
                                </asp:Label>
                                <br />
                                <asp:Label Text="" ID="lblFAreaandPincode" runat="server">
                                 <%--   278, Selvakumar complex, Kurumandur Medu,--%>
                                </asp:Label>
                              
                               
                                <br />
                                <label>
                                    GSTIN/UIN :
                                </label>
                                <asp:Label ID="lblFGST"  runat="server"></asp:Label>
                                <br />
                                <%--<label>
                        CIN :
                    </label>
                    <label>
                        U74999TN2013PTC091007</label>
                    <br />--%>
                                <label>
                                    E-Mail :</label>
                               <asp:Label ID="lblFEmail" runat="server"  ></asp:Label>
                                    <%--Sivanelectricals.Company@gmail.com--%>
                              
                                <br />
                                <label>
                                    Contact :</label>
                                <asp:Label ID="lblFPhone"  runat="server"></asp:Label>
                                   <%-- +91 7402727888, +91 7402724888--%>
                                <asp:Label ID="lblFMobile"  runat="server"></asp:Label>
                                </td>
                        </div>
                                </td>
                                </tr>
                               
                            </table>
                               
                               
                           
                            <hr />
                            Buyer Name:
                            <br />
                            <b>
                                <asp:Label ID="lblcompanyname" runat="server"></asp:Label></b><br />
               <asp:Label
                    ID="lbladdress" runat="server"></asp:Label><br />
               Pincode: <asp:Label
                    ID="lblCityandPincode" runat="server"></asp:Label><br />
                <asp:Label
                    ID="lblArea" runat="server"></asp:Label><br />
        Mobile No: <asp:Label
                    ID="lblphoneno" runat="server"></asp:Label><br />
               GST: <asp:Label
                    ID="lblGST" runat="server"></asp:Label><br />
                           
                        </td>
                        <td style="width: 50%; border: 0px solid black; border-bottom: 1px solid black;">
                            <div id="Div1" runat="server" visible="true" style="height: 100px">
                                <table width="100%" id="RTbl">
                                    <tr style="height: 40px">
                                        <td width="43%">
                                            <b>
                                                <label>
                                                    Order No :
                                                </label>
                                            </b>
                                            <asp:Label ID="lblProcessOrderNo" Style="text-align: left;" runat="server"></asp:Label>
                                        </td>
                                        <td width="50%">
                                            <b>
                                                <label>
                                                    Dated :
                                                </label>
                                            </b>
                                            <asp:Label ID="lblOrderDate" Style="text-align: right" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height: 42px">
                                        <td>
                                            <b>
                                                <label>
                                                    Payment Terms :
                                                </label>
                                            </b>
                                            <asp:Label ID="lblotherreference" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <b>
                                                <label>
                                                    Mode/Terms of Payment :
                                                </label>
                                            </b>
                                            <asp:Label ID="lbltermspayment" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr style="height: 42px">
                                        <td>
                                            <b>
                                                <label>
                                                    Delivery Terms :
                                                </label>
                                            </b>
                                            <asp:Label ID="lblDeliveryTerms" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <b>
                                                <label>
                                                   Delivered Person Name :
                                                </label>
                                            </b>
                                            <asp:Label ID="lblDeliveredPersonName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height: 42px">
                                        <td>
                                            <b>
                                                <label>
                                                    Dispatch Doc No :
                                                </label>
                                            </b>
                                            <asp:Label ID="lblDespatchedBy" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <b>
                                                <label>
                                                   Dispatched Through :
                                                </label>
                                            </b>
                                            <asp:Label ID="lbldespatchthrogh" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <b>
                                                <label>
                                                    Dispatch Date :
                                                </label>
                                            </b>
                                          
                                            <asp:Label ID="lbltermsofdelivery" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lblProvince" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="Div2" runat="server" visible="true" style="height: 147px">
                            </div>
                        </td>
                    </tr>
                </table>
                <div>
                    <table width="100%" border="0" class="style1" id="MH3">
                        <tr>
                            <td valign="top" align="left" style="width: 50%">
                                <asp:GridView runat="server" BorderWidth="0" ID="gvItemProcessOrder" GridLines="Vertical"
                                    OnRowDataBound="gvItemProcessOrder_OnRowDataBound" AlternatingRowStyle-CssClass="even"
                                    AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true" AllowPrintPaging="true"
                                    Width="100%">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <FooterStyle BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" HorizontalAlign="Right" />
                                    <%--   <asp:GridView ID="gvProduct" Visible="true" CssClass="mynewGridStyle" ShowHeaderWhenEmpty="true" GridLines="Vertical"
            Width="100%" EmptyDataText="No Records Found" runat="server" AutoGenerateColumns="false"
            ShowFooter="false" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="White">
            <HeaderStyle CssClass="gradient" BorderStyle="Solid" />--%>
                                    <%--<RowStyle Height="2px" />--%>
                                    <Columns>
                                       <asp:TemplateField HeaderText="SNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Description of Goods" ItemStyle-HorizontalAlign="left"
                                            ItemStyle-Width="47.9%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProduct" runat="server" Text='<%#Eval("Styleno") %>' Style="text-align: left;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <%--  <asp:TemplateField HeaderText="GST Rate" ItemStyle-HorizontalAlign="left" Visible="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGST" runat="server" Text='<%#Eval("GST") %>' Style="text-align: Center;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                         <asp:BoundField DataField="Styleno" Visible="false" HeaderText="Style" HeaderStyle-HorizontalAlign="Left" />
                         <asp:BoundField DataField="size" Visible="true" HeaderText="Size" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Color" HeaderText="Color" Visible="false" FooterStyle-HorizontalAlign="Right"
                            FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("Rate") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BeforeTAX" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBeforeTAX" runat="server" Text='<%#Eval("Amount") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPer" runat="server" Text='<%#Eval("GST") %>' Style="text-align: Center;"></asp:Label><br />
                                                <asp:Label ID="lbltax" runat="server" Visible="false" Text='<%#Eval("tax","{0:n2}") %>'  Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("TotalAmount") %>' Style="text-align: Center;"></asp:Label>
                                              <%--  <asp:Label ID="lblTot" runat="server" Text='<%#Eval("TotAmount") %>' Style="text-align: Center;"
                                                    Visible="false"></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; border: solid 0px" runat="server" visible="false">
                        <tr style="border: solid 0px">
                            <td style="width: 25%; font-size: 13.5px;">
                                <label style="font-family: 'Calibri'; margin-left: 586px; font-weight: bold;">
                                    Cash Amount</label>
                                <asp:Label Font-Size="13.5px" Style="padding-left: 0px; margin-left: 548px; font-family: 'Calibri';
                                    font-weight: bold" ID="txtCashAmount" Visible="true" Text="0" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border: solid 0px">
                            <td style="width: 25%; font-size: 13px;">
                                <label style="font-family: 'Calibri'; margin-left: 599px; font-weight: bold;">
                                    Grand Total
                                </label>
                                <asp:Label Font-Size="13.5px" Style="padding-left: 0px; margin-left: 548px; font-family: 'Calibri';
                                    font-weight: bold" ID="txtGrandTotal" Visible="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--<div>
        <table width="100%" id="Table2">
            <tr>
                <td style="width: 50%">
                    Round Off<br />
                </td>
                <td style="width: 50%; text-align: right">
                    <asp:Label ID="lblRoundOff" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
    </div>--%>
                <div style="border: 1px solid black;" runat="server">
                    <table width="100%" id="MH2" border="0" style="border: 0px solid black">
                        <tr>
                            <td style="width: 50%; border-top: 0px solid black; border-bottom: 1px solid black;">
                                Amount Chargeable (in words)<br />
                                <asp:Label ID="lblAmountinwords" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width: 50%; text-align: right; border-top: 0px solid black; border-bottom: 1px solid black;">
                                E. & O.E
                            </td>
                        </tr>
                    </table>
                </div>
                
<div style="border-bottom: 1px solid black" runat="server">
                    <table width="100%" border="0" class="style1" id="MH4">
                        <tr>
                            <td valign="top" align="left" style="width: 50%">
                                <asp:GridView runat="server" BorderWidth="0" ID="gvGST" GridLines="Vertical" OnRowDataBound="gvGST_OnRowDataBound"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                                    ShowFooter="true" AllowPrintPaging="true" Width="100%">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <FooterStyle BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" HorizontalAlign="Right" />
                                    <%--    <asp:GridView ID="gvGST" Visible="true" CssClass="mynewGridStyle" ShowHeaderWhenEmpty="true"
            Width="100%" EmptyDataText="No Records Found" runat="server" AutoGenerateColumns="false"
            ShowFooter="false" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="White">
            <HeaderStyle CssClass="gradient" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="HSN/SAC" ItemStyle-HorizontalAlign="left" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCode" runat="server" Text='<%#Eval("HSNCode") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Taxable Value" ItemStyle-HorizontalAlign="right" ItemStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST Rate" ItemStyle-HorizontalAlign="center" ItemStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGSTRate" runat="server" Text='<%#Eval("CGSTRate") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST Amount" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGSTAmount" runat="server" Text='<%#Eval("CGSTAmount") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST Rate" ItemStyle-HorizontalAlign="center" ItemStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGSTRate" runat="server" Text='<%#Eval("SGSTRate") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST Amount" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGSTAmount" runat="server" Text='<%#Eval("SGSTAmount") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST Rate" ItemStyle-HorizontalAlign="center" ItemStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGSTRate" runat="server" Text='<%#Eval("IGSTRate") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST Amount" ItemStyle-HorizontalAlign="right" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGSTAmount" runat="server" Text='<%#Eval("IGSTAmount") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Tax Amount" ItemStyle-HorizontalAlign="right"
                                            ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>' Style="text-align: Center;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
      

                <div style="border: 0px solid black">
                    <table width="100%" id="Table1">
                        <tr>
                            <td style="width: 100%">
                                Tax Amount (in words)<br />
                                <asp:Label ID="lblTaxAmountinwords" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table width="100%" id="Table2">
                        <tr>
                            <td style="width: 50%">
                                <%--Company's PAN &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <b>JVHPS9020R</b><br />--%>
                                <%-- O/S Amount &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <b><asp:Label ID="lblOSAmt" runat="server" Font-Bold="true"></asp:Label></b><br />--%>
                                <u>Declaration</u><br />
                                We Declare that this order shows the actual price of the goods described and that
                                all particulars are true and correct.<br />
                                Thank for doing business with us.
                            </td>
                            <td style="width: 50%">
                                Company's Bank Details<br />
                                Bank Name&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp
                                <b>
                                    <asp:Label ID="lblBrBank" runat="server"></asp:Label></b><br />
                                Branch Name&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp
                                <b>
                                    <asp:Label ID="lblBrBranch" runat="server"></asp:Label></b><br />
                                A/c no.&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp
                                <b>
                                    <asp:Label ID="lblBrAccNo" runat="server"></asp:Label></b><br />
                                IFSC Code&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp:&nbsp
                                <b>
                                    <asp:Label ID="lblBrIFSC" runat="server"></asp:Label></b><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%; border: 0px solid black; text-align: left">
                                <%-- Customer's Seal and Signature<br />
                    <br />
                    <br />
                    <br />--%>
                            </td>
                            <td style="width: 50%; border: 1px solid black; text-align: right">
                                For&nbsp&nbsp <b>
                                    <asp:Label Text="" ID="lblForName" runat="server"></asp:Label></b> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<br />
                                <br />
                                <br />
                                <br />
                                Authorized Signatory&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            </td>
                        </tr>
                    </table>
                </div>
                <center>
                    <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" />
                    <asp:Button ID="btnexit" runat="server" Text="Exit" OnClick="btnexit_Click" />
                    <%-- <a href="javascript:void(window.open('http://www.htmltopdfconverter.net/?convert='+window.location))">Convert To PDF</a>--%>
                    <%--   <a href="http://localhost:55096/Accountsbootstrap/InvoicePrint.aspx?iSalesID=22" download > pdf link of your choice </a>
                   <a href="http://localhost:55096/Accountsbootstrap/InvoicePrint.aspx?iSalesID=22" onclick="onClick()">click to download</a>--%>
                </center>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
