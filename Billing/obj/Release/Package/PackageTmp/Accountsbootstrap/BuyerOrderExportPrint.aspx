<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuyerOrderExportPrint.aspx.cs"
    Inherits="Billing.Accountsbootstrap.BuyerOrderExportPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Buyer Sales Order Export Print</title>
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
            height: 50px;
        }
        .style2
        {
            height: 150px;
        }
    </style>
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
            font-size: 15.5px;
            font-family: Calibri;
        }
        #MH4
        {
            width: 100%;
            border-collapse: collapse;
            border: 0px solid black;
            font-size: 15.5px;
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
            font-size: 15.5px;
            font-family: Calibri;
        }
        #MH
        {
            width: 100%;
            border-collapse: collapse;
            border: 1px solid black;
            font-size: 15.5px;
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
            font-size: 15.5px;
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
             border-width:1px;
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
        #footerline td{
             padding: 5px;
            border-left: 1px solid black;
            border-bottom: 1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" width="100%">
   <table  width="100%" style="border: 0px solid black">
       <tr>
           <td><center><h3>INVOICE</h3></center></td>
       </tr>
       <tr>


       <td>


   
    <table width="100%"   border="0" style="border: 0px solid black">
        <tr>
            <td style="width:40%;height:100%" valign="top" >
                <table  width="100%"  ><tr ><td style="border-bottom:solid 1px;border-right:solid 1px ;border-left:solid 1px;border-top:solid 1px">

               
               Exporter : <br /> <b>
                    <asp:Label ID="lblCoName" runat="server"></asp:Label></b><br />
               <asp:Label
                    ID="lblcoaddress" runat="server"></asp:Label><br />
               <asp:Label
                    ID="lblpincode" runat="server"></asp:Label><br />
               <asp:Label
                    ID="lblGSTINNO" runat="server"></asp:Label><br />
               &nbsp;<asp:Label
                    ID="lblgstno" runat="server"></asp:Label><br />
              
            </td>

                    </tr>
                    <tr>
                        <td style="border-bottom:solid 1px;border-right:solid 1px; border-left:solid 1px;">Consignee</td>
                    </tr>
                    <tr><td style="border-bottom:solid 1px;border-right:solid 1px; border-left:solid 1px;" >
                         <b><asp:Label ID="lblcompanyname" runat="server"></asp:Label></b><br />
               <asp:Label
                    ID="lbladdress" runat="server"></asp:Label><br />
               <asp:Label
                    ID="lblCityandPincode" runat="server"></asp:Label><br />
                <asp:Label
                    ID="lblArea" runat="server"></asp:Label><br />
              PH : <asp:Label
                    ID="lblphoneno" runat="server"></asp:Label><br />
               
                        </td></tr>
                    </table>
                </td>
            <td style="width:60%; height:100%" valign="top" >
                <table   width="100%">
                    <tr ><td style="border-bottom:solid 1px;border-right:solid 1px; border-top: solid 1px;">
               Invoice No:
                <asp:Label ID="lblProcessOrderNo" runat="server"></asp:Label><br />
                Invoice Date :
                <asp:Label ID="lblOrderDate" runat="server"></asp:Label><br />
                        <br /><br /><br />
     </td>
 <td style="border-bottom:solid 1px; border-right:solid 1px; border-top:solid 1px">Exporter Ref
                <asp:Label ID="blexporterref" runat="server"></asp:Label><br />
            </td>
           
            </tr>
                   
        <tr><td colspan="2" style="border-bottom:solid 1px ; border-right:solid 1px">
            Buyer's Order No.& Date  <asp:Label ID="lblbuyerorderno" runat="server"></asp:Label>

            </td>
            </tr>
                    <tr><td colspan="2" style="border-bottom:solid 1px ; border-right:solid 1px">
                        Buyer (If other than consignee)<br />
                     <b>   <asp:Label ID="lblcompanyname1" runat="server"></asp:Label></b><br />
               <asp:Label
                    ID="lbladdress1" runat="server"></asp:Label><br />
                <asp:Label
                    ID="lblCityandPincode1" runat="server"></asp:Label><br />
               <asp:Label
                    ID="lblArea1" runat="server"></asp:Label><br />
               PH : <asp:Label
                    ID="lblphoneno1" runat="server"></asp:Label><br />
              
                        </td>
                  
        </tr>
    </table>
</td></tr>
<tr>
    <td style="width:30%; height:100%" valign="top">
        <table width="100%">
            <tr><td style="border-bottom:solid 1px ; border-right:solid 1px ;border-left:solid 1px">
                Pre Carrier by<br />
                <asp:Label ID="lblprecarrierby" runat="server"></asp:Label>
                </td><td style="border-bottom:solid 1px ; border-right:solid 1px">
                      Place of receipt of pre-carrier   <br />  <asp:Label ID="lblplaceofcarrier" runat="server"></asp:Label>
                     </td></tr>
            <tr>
                <td style="border-bottom:solid 1px ; border-right:solid 1px; border-left:solid 1px">
                      Vessel/<asp:Label ID="lblvesseltype" runat="server"></asp:Label><br />
                    <asp:Label ID="type" runat="server"></asp:Label>
                     <br /><br /><br />
              
                </td>
                <td style="border-bottom:solid 1px ; border-right:solid 1px">
                      Port of Loading<br />
                <asp:Label ID="lblloadingport" runat="server"></asp:Label>
                    <br /><br /><br />
                </td>
            </tr>
            <tr>
                <td style="border-bottom:solid 1px ; border-right:solid 1px;border-left:solid 1px">
                     Port of discharge<br />
                <asp:Label ID="lbldischargeport" runat="server"></asp:Label>
                </td>
                <td style="border-bottom:solid 1px ; border-right:solid 1px"> Place of delivery<br />
                <asp:Label ID="lbldeliveryplace" runat="server"></asp:Label></td>

            </tr>

        </table>
        

        
    </td>

    <td style="width:70%; height:100%" >
        <table  width="100%">
            <tr><td style="border-bottom:solid 1px; border-right:solid 1px">
                  Country of origin<br />
                <asp:Label ID="lblorigin" runat="server"></asp:Label>

                </td>
                <td style="border-bottom:solid 1px;border-right:solid 1px">
  Country of Final Destination<br />
                <asp:Label ID="lbldestination" runat="server"></asp:Label>
                </td>

            </tr>
            <tr><td colspan="2" style="border-bottom:solid 1px;border-right:solid 1px">Terms of Delivery and Payment: - 100% ADVANCE</td></tr>
            <tr><td colspan="2" style="border-bottom:solid 1px;border-right:solid 1px" >
                Our Banker's<br />
                <b> <asp:Label ID="lblbankname" runat="server"></asp:Label></b><br />
              <asp:Label
                    ID="lblbankaddress" runat="server"></asp:Label><br />
              Account Number: <asp:Label
                    ID="lblaccountnumer" runat="server"></asp:Label><br />
               Swift Code<asp:Label
                    ID="lblswiftcode" runat="server"></asp:Label><br />
              

                </td></tr>
        </table>

    </td>

</tr>

<tr><td colspan="2">

   

    <table width="100%" border="0">
        <tr style="border: 0">
            <td>
                <asp:GridView ID="gvItemProcessOrder" runat="server" EmptyDataText="No Records Found"
                    GridLines="Both" ShowFooter="true" AutoGenerateColumns="false" OnRowDataBound="gvItemProcessOrder_OnRowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                     <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Visible="false"  Text='<%#Eval("Salesfortype") %>' Style="text-align: left;"></asp:Label><br /><br />
                                <asp:Label ID="Label1" runat="server" Visible="false"   Style="text-align: Center;"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="Description" Visible="true" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="HSNcode" Visible="true" HeaderText="HSN Code" HeaderStyle-HorizontalAlign="Left" />
                         <asp:BoundField DataField="Styleno" Visible="true" HeaderText="Style" HeaderStyle-HorizontalAlign="Left" />
                         <asp:BoundField DataField="size" Visible="true" HeaderText="Size" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Color" HeaderText="Color" FooterStyle-HorizontalAlign="Right"
                            FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="USDRate" HeaderText="Rate" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
                <br />
                <br />
            </td>
        </tr>
    </table>
     </td></tr></table>
           
    <table width="100%" border="0" style="height: 5px"  >
        <tr>
            <td align="left" runat="server" visible="false">
                <hr />
                <asp:Label ID="TC1" runat="server" Text="1.PO NO Should print on every Bill and delivery challan.">
                </asp:Label><br />
                <asp:Label ID="TC2" runat="server" Text="2.Billing to be per P.O. only & not 2-3 P.O.s together.">
                </asp:Label><br />
                <asp:Label ID="TC3" runat="server" Text="3.Bills Must have P.O. photocopy attached with it.">
                </asp:Label><br />
                <asp:Label ID="TC4" runat="server" Text="4.Bill must also have all relevant CHALLAN COPIES attached which have been received by Stores Department.">
                </asp:Label><br />
                <asp:Label ID="TC5" runat="server" Text="5.Delivery should be exactly on or before delivery date.">
                </asp:Label>
                <hr />
            </td>
        </tr>
        <tr>
            <td style="border: none">
                <table width="100%" align="right" style="margin-top: -21px">
                    <tr>
                        <td align="right" valign="top">
                            <h5>
                                for<b>
                                    <asp:Label ID="lblCoName1" runat="server"></asp:Label>
                                </b>
                            </h5>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="border: none">
                <table width="100%" style="margin-top: 0px">
                    <tr>
                        <td valign="bottom" style="width: 400px; text-align: center">
                            Signature of the Supplier
                        </td>
                        <td valign="bottom" style="width: 400px; text-align: center">
                            Prepared By
                        </td>
                        <td valign="bottom" style="width: 400px; text-align: right">
                            Authorised Signatory
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
           </td></tr></table>
    <table width="100%" border="0">
        <tr>
            <td align="center">
                <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" />
                <asp:Button ID="btnexit" runat="server" Text="Exit" OnClick="btnexit_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
