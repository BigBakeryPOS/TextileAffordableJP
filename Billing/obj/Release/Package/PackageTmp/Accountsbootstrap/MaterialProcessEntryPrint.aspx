<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialProcessEntryPrint.aspx.cs" Inherits="Billing.Accountsbootstrap.MaterialProcessEntryPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Store Challan Print</title>
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
</head>
<body>
    <form id="form1" runat="server" width="100%">
    <table width="100%" border="0" cellpadding="1" cellspacing="1">
        <tr>
            <td align="left" style="width: 30%; vertical-align: top">
                Phone :
                <asp:Label ID="lblFPhone" runat="server"></asp:Label><br />
                Mobile :
                <asp:Label ID="lblFMobile" runat="server"></asp:Label><br />
            </td>
            <td align="center" style="width: 40%">
                <b>
                    <asp:Label ID="lblFCompany" runat="server"></asp:Label>
                </b>
                <br />
                <asp:Label ID="lblFAddress" runat="server" Style="font-size: larger"></asp:Label><br />
                <asp:Label ID="lblFAreaandPincode" runat="server" Style="font-size: larger"></asp:Label><br />
                <asp:Label ID="lblFEmail" runat="server" Style="font-size: larger"></asp:Label>
            </td>
            <td align="right" style="width: 30%; vertical-align: top">
                Fax :
                <asp:Label ID="lblFax" runat="server"></asp:Label><br />
                GSTIN :
                <asp:Label ID="lblFGST" runat="server"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" style="width: 100%; height: 35px; font-weight: bold;
                font-size: larger">
                <hr />
                <asp:Label ID="lblprint" runat="server" Text="Print"></asp:Label>
                <hr />
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr>
            <td align="left">
                <br />
                Ledger : <b>
                    <asp:Label ID="lblcompanyname" runat="server"></asp:Label></b><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lbladdress" runat="server"></asp:Label><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblCityandPincode" runat="server"></asp:Label><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblArea" runat="server"></asp:Label><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblphoneno" runat="server"></asp:Label><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                    ID="lblGST" runat="server"></asp:Label><br />
            </td>
            <td align="left">
                EntryNo :
                <asp:Label ID="lblFullEntryNo" runat="server"></asp:Label><br />
                Entry Date :
                <asp:Label ID="lblEntryDate" runat="server"></asp:Label><br />
                <%--Between Date :
                <asp:Label ID="lblOrderDateBetween" runat="server"></asp:Label><br />--%>
            </td>
        </tr>
    </table>
    <table width="100%" border="0">
        <tr style="border: 0">
            <td colspan="2">
                <asp:GridView ID="gvCuttingProcessEntryStyles" runat="server" EmptyDataText="No Records Found" AutoGenerateColumns="false"
                    Width="100%">
                    <columns>
                    <asp:BoundField DataField="EXCNO" HeaderText="EXC.NO"  />
                    <asp:BoundField DataField="PRPNO" HeaderText="PRP.NO"  />
                    <asp:BoundField DataField="Description" HeaderText="Item Name"  />
                    <asp:BoundField DataField="color" HeaderText="Color"  />
                    <asp:BoundField DataField="issuestock" HeaderText="Issue Stock"  />
                    </columns>
                </asp:GridView>
                <br />
            </td>
        </tr>
       
    </table>
    <table width="100%" border="0" style="height: 5px">
        <tr id="Tr1" runat="server" visible="false">
            <td align="left">
                <hr />
                <asp:Label ID="TC1" runat="server" Text="1.PO NO Should print onevery Bill and delivery challan.">
                </asp:Label><br />
                <asp:Label ID="TC2" runat="server" Text="2.Billing to be per P.O. only & not 2-3 P.O.s together.">
                </asp:Label><br />
                <asp:Label ID="TC3" runat="server" Text="3.Bills Must have P.O. photocopy attached with it.">
                </asp:Label><br />
                <asp:Label ID="TC4" runat="server" Text="4.Bill must also have all relevant CHALLAM COPIES attached which have been received by Stores Department.">
                </asp:Label><br />
                <asp:Label ID="TC5" runat="server" Text="5.Delivery sholt be exactly on or before delivery date.">
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
                                    <asp:Label ID="lblCoName" runat="server"></asp:Label>
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
