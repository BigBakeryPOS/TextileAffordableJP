<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customerlabelprint.aspx.cs" Inherits="Billing.Accountsbootstrap.Customerlabelprint" %>

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

          border-collapse:collapse;
          width:85%;
          margin-left:0px;
            
         border: 1px solid gray;
       
        overflow: hidden;
             

        }

         

        .myleft tr th

        {
           

            padding: 8px;

            color: Black;
           
           
            border: 1px solid gray;
            font-family : Arial;
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

            border:1px solid gray;

            padding: 8px;

        }
        
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px";
        width="100%"; height="100px" class="style1">
        <tr>
            <td style="height:1px">
                <table width="100%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                    class="style1">
                    <tr>
                        <td align="center">
                            <label style="font-size: large; font-weight: bold">
                                Customer Label Report:</label>
                                 <asp:Label ID="lbbllott" style="font-size: large; font-weight: bold"  runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="595px" height="100px" border="0" class="style1">
                    <tr>                       
                        <td valign="top" align="left">
                            <asp:Label ID="Label1" runat="server" style="width:500%;font-weight:bold">
                                Lot Number : </asp:Label>
                            <asp:Label ID="lblLot" runat="server"></asp:Label><br />

                            <asp:Label ID="Label2" runat="server" style="width:100px;font-weight:bold">
                                Delivery date : </asp:Label>
                            <asp:Label ID="lblDeldate" runat="server" ></asp:Label><br />
                            </td>
                            <td valign="top" align="left">

                            <asp:Label ID="Label3" runat="server" style="width:100px;font-weight:bold">
                                Width : </asp:Label>
                            <asp:Label ID="lblwidth" runat="server"></asp:Label><br />
                             <asp:Label   ID="Label5" runat="server" style="width:100px;font-weight:bold">
                                Cutting Master : </asp:Label>
                            <asp:Label ID="lblcut" runat="server"></asp:Label><br />

                            <asp:Label Visible="false"  ID="Label4" runat="server" style="width:100px;font-weight:bold">
                                Fit : </asp:Label>
                            <asp:Label ID="lblfit" Visible="false"  runat="server"></asp:Label><br />
                        </td>
                            </tr>
                            
                </table>
                  <table width="595px" height="100px" border="0" class="style1">
                    <tr>                       
                        <td valign="top"  runat="server" visible="false" align="left" style="width:50px">
                        <label style="font-weight:bold">Overall Lot Report</label>
                          <asp:GridView runat="server" BorderWidth="1" ID="GridView1" CssClass="myleft" GridLines="Vertical"
                              AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                            PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                            Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                <Columns>
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
                                </Columns>
                            </asp:GridView>
                        </td>
                         <td valign="top"  runat="server" visible="false" align="left" style="width:50px">
                          <label style="font-weight:bold">Avg. Rate Calculation</label>
                          <div>
                           <asp:GridView runat="server" BorderWidth="1" ID="GridView2" CssClass="myleft" GridLines="Vertical"
                              AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                            PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                            Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                <Columns>
                                                                                <asp:BoundField HeaderText="Used Meter" DataFormatString='{0:f}' DataField="met" />
                                                                                <asp:BoundField Visible="false" HeaderText="Rate" DataFormatString='{0:f}' DataField="rat" />
                                                                                <asp:BoundField HeaderText="Total" DataFormatString='{0:f}' DataField="tot" />
                                                                             
                                </Columns>
                            </asp:GridView>
                            </div>
                         </td>
                          <td valign="top" align="left" style="width:50px">
                            <label style="font-weight:bold">Customer Labels Details</label>
                          <asp:GridView runat="server" BorderWidth="1" ID="gridlabel" CssClass="myleft" GridLines="Vertical"
                              AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                            PrintPageSize="30" AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                            Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField HeaderText="Brand Name" DataField="cut" />
                                  <asp:BoundField HeaderText="Main Label" DataField="MainLabel" />
                                  <asp:BoundField HeaderText="Fit Label" DataField="flab" />
                                <asp:BoundField HeaderText="Wash care label" DataField="wlab" />
                                <asp:BoundField HeaderText="Logo Embrodeng" DataField="llab" />
                           
                                </Columns>
                            </asp:GridView>
                          </td>
                        </tr>
                        <tr  runat="server" visible="false">
                        <td style="font-weight:bold">
                        <label>Avg. Meter:</label>
                        <asp:Label ID="Lblvalue" runat="server"></asp:Label>

                        </td>
                        <td style="font-weight:bold; font-size:12px">
                        <label>Avg.Rate +Prod.Cost(Rs.90):</label>
                        <asp:Label ID="lblratee"   runat="server"></asp:Label>

                        </td>
                        </tr>
                        </table>
                        <br />
                <table width="595px" height="643px"  border="0.5px" class="style1">
                    <tr  runat="server" visible="false" valign="top">
                        <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                         <label style="font-weight:bold">Detailed Cutting Report</label>
                           <asp:GridView runat="server" Visible="false" BorderWidth="1" ID="gridprint" CssClass="myleft" GridLines="Vertical"
                              AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                            PrintPageSize="30" AllowPrintPaging="true" Width="106.5%" OnRowDataBound="gridprint_RowDataBound"
                            Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                    <asp:BoundField HeaderText="Party Name" DataField="ledgername" ItemStyle-Height="60"/>
                                    <asp:BoundField HeaderText="Fit" DataField="Fit" ItemStyle-Height="60"/>

                                    <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}' ItemStyle-Height="60"/>
                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60"/>
                                 <asp:BoundField HeaderText="36FS" DataField="36FS" />
                                  <asp:BoundField HeaderText="36HS" DataField="36HS" />
                                                                                <asp:BoundField HeaderText="38FS" DataField="38FS" />
                                                                                <asp:BoundField HeaderText="39FS" DataField="39FS" />
                                                                                <asp:BoundField HeaderText="40FS" DataField="40FS" />
                                                                                <asp:BoundField HeaderText="42FS" DataField="42FS" />
                                                                                <asp:BoundField HeaderText="44FS" DataField="44FS" />
                                                                                  <asp:BoundField HeaderText="38HS" DataField="38HS" />
                                                                                   <asp:BoundField HeaderText="39HS" DataField="39HS" />
                                                                                   <asp:BoundField HeaderText="40HS" DataField="40HS" />
                                                                                   <asp:BoundField HeaderText="42HS" DataField="42HS" />
                                                                                <asp:BoundField HeaderText="44HS" DataField="44HS" />
                                                                                <asp:BoundField HeaderText="Total FS" DataField="TotFS" />
                                                                                <asp:BoundField HeaderText="Total HS" DataField="TotHS" />
                                                                                <asp:BoundField HeaderText="Total Qty" DataField="Qty" />
                                                                                 <asp:BoundField HeaderText="Avg Meter" DataFormatString='{0:f}' DataField="AvgMtr" />
                                                                                  <asp:BoundField HeaderText="Avg Rate" DataFormatString='{0:f}' DataField="AvgRate" />
                                                                                <asp:BoundField HeaderText="Margin" DataFormatString='{0:f}' DataField="MarginRAte" />
                                                                                 <asp:BoundField HeaderText="WSP" DataFormatString='{0:f}'  DataField="MRPRat" />
                                </Columns>
                            </asp:GridView>

                             <asp:GridView runat="server" BorderWidth="1" ID="gridnewprint" CssClass="myleft" GridLines="Vertical"
                              AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true"
                            PrintPageSize="30" AllowPrintPaging="true" Width="106.5%" onrowcreated="gridnewprint_RowCreated" OnRowDataBound="gridnewprint_RowDataBound"
                            Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField HeaderText="Design no" DataField="designno" ItemStyle-Height="60" />
                                    <asp:BoundField HeaderText="Brand Name" DataField="brandname" ItemStyle-Height="60"/>
                                    <asp:BoundField HeaderText="Fit" DataField="Fit" ItemStyle-Height="60"/>

                                    <asp:BoundField HeaderText="Meter" DataField="reqmeter" DataFormatString='{0:f}' ItemStyle-Height="60"/>
                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString='{0:f}' ItemStyle-Height="60"/>
                                 <asp:BoundField HeaderText="36FS" DataField="36FS" />
                                  <asp:BoundField HeaderText="36HS" DataField="36HS" />
                                                                                <asp:BoundField HeaderText="38FS" DataField="38FS" />
                                                                                <asp:BoundField HeaderText="39FS" DataField="39FS" />
                                                                                <asp:BoundField HeaderText="40FS" DataField="40FS" />
                                                                                <asp:BoundField HeaderText="42FS" DataField="42FS" />
                                                                                <asp:BoundField HeaderText="44FS" DataField="44FS" />
                                                                                  <asp:BoundField HeaderText="38HS" DataField="38HS" />
                                                                                   <asp:BoundField HeaderText="39HS" DataField="39HS" />
                                                                                   <asp:BoundField HeaderText="40HS" DataField="40HS" />
                                                                                   <asp:BoundField HeaderText="42HS" DataField="42HS" />
                                                                                <asp:BoundField HeaderText="44HS" DataField="44HS" />
                                                                                <asp:BoundField HeaderText="Total FS" DataField="TotFS" />
                                                                                <asp:BoundField HeaderText="Total HS" DataField="TotHS" />
                                                                                <asp:BoundField HeaderText="Total Qty" DataField="Qty" />
                                                                                 <asp:BoundField HeaderText="Avg Meter" DataFormatString='{0:f}' DataField="AvgMtr" />
                                                                                  <asp:BoundField HeaderText="Avg Rate" DataFormatString='{0:f}' DataField="AvgRate" />
                                                                                <asp:BoundField HeaderText="Margin" DataFormatString='{0:f}' DataField="MarginRAte" />
                                                                                 <asp:BoundField HeaderText="WSP" DataFormatString='{0:f}'  DataField="MRPRat" />
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
                <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="myFunction()" />
                <asp:Button ID="btnexit" runat="server" Text="Exit" OnClick="btnexit_Click" />
            </td>
        </tr>
    </table>
    </center>
    </form>
</body>
</html>

