<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullLotDetails.aspx.cs"
    Inherits="Billing.Accountsbootstrap.FullLotDetails" %>

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
    <script type="text/javascript">


        function myFunctionpdf() {
            var ButtonControl = document.getElementById("btnprint");
            var fist = document.getElementById("btnexit");

            ButtonControl.style.visibility = "hidden";
            btnexit.style.visibility = "hidden";
            pdfDoc.Open();
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
                                <asp:Label ID="lbllscutno" runat="server" Style="font-size: large; font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table height="100px" border="0" class="style1">
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
                                <asp:Label ID="lblLot" runat="server" Style="width: 100%; font-weight: bold"></asp:Label><br />
                                <asp:Label ID="Label2" runat="server" Style="width: 100px; font-weight: bold">
                                Date : </asp:Label>
                                <asp:Label ID="lblDeldate" runat="server"></asp:Label><br />
                                <asp:Label ID="Label6" runat="server" Style="width: 100px; font-weight: bold">
                                Name : </asp:Label>
                                <asp:Label ID="lblLedgerName" runat="server"></asp:Label><br />
                                <asp:Label ID="Label13" runat="server" Style="width: 100px; font-weight: bold">
                                Complete Stitching : </asp:Label>
                                <asp:Label ID="lblCompleteStitching" runat="server"></asp:Label><br />
                                <asp:Label ID="lblLotCombination" runat="server"></asp:Label><br />
                            </td>
                            <td valign="top" align="right" style="width: 20%">
                            </td>
                            <td valign="top" align="left" style="width: 40%">
                                <asp:Label ID="lblcompanyname" runat="server" Visible="false"></asp:Label><br />
                                <asp:Label ID="Label3" runat="server" Style="width: 100px; font-weight: bold">
                                Brand : </asp:Label>
                                <asp:Label ID="lblbrand" runat="server"></asp:Label><br />
                                <asp:Label ID="Label10" runat="server" Style="width: 100px; font-weight: bold">
                                Item : </asp:Label>
                                <asp:Label ID="lblItem" runat="server"></asp:Label><br />
                                <asp:Label ID="Label7" runat="server" Style="width: 100px; font-weight: bold">
                                Fit : </asp:Label>
                                <asp:Label ID="lbllfit" runat="server"></asp:Label><br />
                                <asp:Label ID="lblShirtDescription" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="table-responsive" id="div1" runat="server">
                                    <div>
                                        <asp:GridView ID="GVCutting" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            Width="80%" Caption="Pre Cutting And Master Cutting" EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="Process" HeaderText="Process" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalQty" HeaderText="Total Quantity" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVFabricdetails" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            OnRowDataBound="GVFabricdetails_RowDataBound" Width="80%" Caption="FabricDetails Details"
                                            ShowFooter="true" EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="LedgerName" HeaderText="LedgerName" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="FabNo" HeaderText="DCNo" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="InvDate" HeaderText="InvDate" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="Refno" HeaderText="Inv.No" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Rate" HeaderText="Rate" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Reqmeter" HeaderText="Req. KG/gms" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Endbit" HeaderText="Endbit" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="DesignNo" HeaderText="DesignNo" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Type" HeaderText="Type" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="gridrawmaterial" runat="server" AllowPaging="false" CssClass="myGridStyle"
                                            Width="80%" Caption="Accessories Details" ShowFooter="true" OnRowDataBound="Gridrawmaterial_rowdatabound"
                                            AutoGenerateColumns="false" EmptyDataText="No data found!" ShowHeaderWhenEmpty="True">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="prodname" HeaderText="Accessories Code" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Qty" HeaderText="Total Qty" DataFormatString='{0:f}' ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="purchaserate" HeaderText="Purchase Rate / Qty" DataFormatString='{0:f}'
                                                    ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVJpStiching" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            OnRowDataBound="GVJpStiching_RowDataBound" Width="80%" Caption="Stiching Process"
                                            ShowFooter="true" EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVJpEmbroiding" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVJpEmbroiding_RowDataBound" Width="80%" Caption="Embroiding Process"
                                            EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVJpKajaButton" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVJpKajaButton_RowDataBound" Width="80%" Caption="KajaButton Process"
                                            EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVJpPrinting" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVJpPrinting_RowDataBound" Width="80%" Caption="Printing Process"
                                            EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVJpWashing" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVJpWashing_RowDataBound" Width="80%" Caption="Washing Process"
                                            EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVJpBarTag" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVJpBarTag_RowDataBound" Width="80%" Caption="BarTag Process"
                                            EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVJpTrimming" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVJpTrimming_RowDataBound" Width="80%" Caption="Trimming Process"
                                            EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVJpConsai" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVJpConsai_RowDataBound" Width="80%" Caption="Consai Process"
                                            EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVJpIroning" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVJpIroning_RowDataBound" Width="80%" Caption="Ironing Process"
                                            EmptyDataText="Not In Process">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="name" HeaderText="Worker" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="PaidAmount" HeaderText="PaidAmount" DataFormatString="{0:n}"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalIssue" HeaderText="TotalIssue" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalReceive" HeaderText="TotalReceive" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="TotalDamage" HeaderText="TotalDamage" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="AlterQty" HeaderText="TotalAlter" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVFinishedStock" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" Width="80%" Caption="Finished Stock" EmptyDataText="Not To Despatch">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center"
                                                    Visible="false" />
                                                <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVDespatchstock" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVDespatchstock_RowDataBound" Width="80%" Caption="Despatch Stock"
                                            EmptyDataText="Not To Despatch">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="DcNo" HeaderText="DcNo" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center"
                                                    Visible="false" />
                                                <asp:BoundField DataField="DcDate" HeaderText="DcDate" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="TotalDespatchqty" HeaderText="TotalDespatchQty" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVDespatchReturn" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVDespatchReturn_RowDataBound" Width="80%"
                                            Caption="Despatch Return" EmptyDataText="Not To Despatch">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="DcNo" HeaderText="DcNo" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="CompanyLotNo" HeaderText="CompanyLotNo" ItemStyle-HorizontalAlign="Center"
                                                    Visible="false" />
                                                <asp:BoundField DataField="DcDate" HeaderText="DcDate" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="TotalDespatchqty" HeaderText="TotalReturnQty" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <asp:GridView ID="GVPaymentDetails" runat="server" CssClass="myGridStyle" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowDataBound="GVPaymentDetails_RowDataBound" Width="80%"
                                            Caption="Payment Details" EmptyDataText="Not To Despatch">
                                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="PaymentNo" HeaderText="PaymentNo" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PaymentDate" HeaderText="PaymentDate" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="ProcessType" HeaderText="ProcessType" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="PieceRate" HeaderText="PieceRate" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="DebitAmount" HeaderText="DebitAmount" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:n}" />
                                                <asp:BoundField DataField="Miscellaneous" HeaderText="Miscellaneous" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:n}" />
                                            </Columns>
                                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:GridView>
                                        <div class="col-lg-12">
                                            <asp:Panel ID="panel1" runat="server">
                                                <table border="1" style="width: 600px; height: 70px">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label runat="server" ID="Label5" ForeColor="Black" CssClass="label" Font-Size="Large">Details</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label runat="server" ID="Label8" ForeColor="Black" CssClass="label" Font-Size="Large">Value</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label12" ForeColor="Black" CssClass="label" Font-Size="Large">Lot Expenses Amount</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label runat="server" ID="lblExpensesamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label14" ForeColor="Black" CssClass="label" Font-Size="Large">Lot Debit Amount </asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label runat="server" ID="lblDebitamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label11" ForeColor="Black" CssClass="label" Font-Size="Large">Lot Miscellaneous Amount</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label runat="server" ID="lblMiscellaneousamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label runat="server" ID="Label15" ForeColor="Black" CssClass="label" Font-Size="Large">Lot Value :-</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label runat="server" ID="lblLotValueamt" ForeColor="Black" CssClass="label">0</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                        runat="server" visible="false" class="style1">
                        <tr height="50px">
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
                        <tr>
                            <td align="left" colspan="3">
                                <asp:Label ID="Label9" runat="server" Style="font-weight: bold; text-align: left">
                                CONTRAST : </asp:Label>
                                <asp:Label ID="lblContrasts" runat="server" Style="font-weight: bold; text-align: left"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td>
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
                    <asp:Button ID="btnexit" runat="server" Text="Exit" PostBackUrl="~/Accountsbootstrap/OverallProcess.aspx" />
                    <asp:Button ID="Button1" runat="server" Text="On" OnClick="btnExport_Clicknew" Visible="false" />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
