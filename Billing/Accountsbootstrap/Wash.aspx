<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wash.aspx.cs" Inherits="Billing.Accountsbootstrap.Wash" %>
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
                                   Washing Process</label>
                                
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
                                <asp:Label  ID="Label4" runat="server" Style="width: 100px; font-weight: bold">
                                PaidAmount : </asp:Label>
                                <asp:Label ID="lblPaidAmount" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table width="80%" height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width:50%">
                               
                                <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                                    AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true" ShowFooter="true"
                                    PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                                    Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                    <asp:BoundField DataField="ItemName"  HeaderText="ItemName" />
                                     <asp:BoundField DataField="Fit"  HeaderText="Fit" />
                                      <asp:BoundField DataField="Patternname"  HeaderText="Pattern" />
                                    <asp:BoundField DataField="recdate" HeaderText="Received Date" DataFormatString="{0:d}" />
                                         <asp:BoundField DataField="RecQty"  HeaderText="RecQty" />
                                         <asp:BoundField DataField="Damageqty"  HeaderText="Damageqty" />

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
                    <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" OnClick="btnclick" />
                    <asp:Button ID="btnexit" runat="server" Text="Exit" PostBackUrl="WashingGrid.aspx" />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>