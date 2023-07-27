<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BCPreviewLot.aspx.cs" Inherits="Billing.Accountsbootstrap.BCPreviewLot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Pre-Cutting Details Print</title>
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
        <div class="col-lg-6" id="sizediv" runat="server" visible="false" style="margin-left: -3pc;">
            <div class="panel panel-default" style="width: 170px">
                <label>
                    Size</label>
                <asp:CheckBoxList ID="chkSizes" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="chkChoice1"
                    runat="server">
                </asp:CheckBoxList>
            </div>
        </div>
        <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px"
            width="100%" height="100px" class="style1">
            <tr>
                <td>
                    <table width="100%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr>
                            <td align="center">
                                <label style="font-size: large; font-weight: bold">
                                    BottiCelli Pre-Cutting Details</label>
                                <asp:Label ID="lbbllott" Style="font-size: large; font-weight: bold" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 30%">
                                GSTIN :
                                <asp:Label ID="Label40" runat="server" Style="font-weight: 500; text-align: left"> 33ACCPR4802M1ZK </asp:Label><br />
                                PH.NO :
                                <asp:Label ID="Label16" runat="server" Style="font-weight: 500;"> 044 - 25983366 </asp:Label><br />
                            </td>
                            <td width="40%" valign="top" align="center">
                                <asp:Image ID="Image1" ImageUrl="../images/Flexiblelogo.jpg" Style="width: 8pc;" runat="server" /><br />
                                <asp:Label ID="Label12" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> No.30, JP KOIL STREET, </asp:Label><br />
                                <asp:Label ID="Label13" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> OLD WASHERMENPET </asp:Label><br />
                                <asp:Label ID="Label29" runat="server" Style="width: 100px; font-weight: bold; font-size: inherit"> CHENNAI-600021 </asp:Label><br />
                            </td>
                            <td valign="top" align="left" style="width: 30%">
                                Mobile.NO :
                                <asp:Label ID="lblmblrpll"  Visible="false"  runat="server" Style="font-weight: 500;"> +91 7358650703 </asp:Label>
                                 <asp:Label ID="lblmblbc"  Visible="false"  runat="server" Style="font-weight: 500;"> +91 9176290701 </asp:Label><br />
                                E-Mail :
                                <asp:Label ID="Label17" runat="server" Style="font-weight: 500; text-align: left"> jpfashion21@gmail.com </asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table height="100px" border="0" class="style1">
                        <tr>
                            <td valign="top" align="left" style="width: 40%">
                                <asp:Label ID="Label1" runat="server" Style="width: 500%; font-weight: bold">
                                DC.NO : </asp:Label>
                                <asp:Label ID="lblLot" runat="server"></asp:Label><br />
                                <asp:Label ID="Label5" runat="server" Style="width: 100px; font-weight: bold">
                                Cutting Master : </asp:Label>
                                <asp:Label ID="lblcut" runat="server"></asp:Label><br />
                                <asp:Label ID="Label15" runat="server" Style="width: 100px; font-weight: bold">
                                GSTIN  : </asp:Label>
                                <asp:Label ID="lblgastin" runat="server"></asp:Label><br />
                                <asp:Label ID="Label7" runat="server" Style="width: 100px; font-weight: bold">
                                Roll/Taka : </asp:Label>
                                <asp:Label ID="lblrolltaka" runat="server"></asp:Label><br />
                            </td>
                            <td valign="top" align="center" style="width: 30%">
                            </td>
                            <td valign="top" align="left" style="width: 30%">
                                <asp:Label ID="Label3" runat="server" Style="width: 100px; font-weight: bold">
                                Width : </asp:Label>
                                <asp:Label ID="lblwidth" runat="server"></asp:Label><br />
                                <asp:Label ID="Label2" runat="server" Style="width: 100px; font-weight: bold">
                                Issue date : </asp:Label>
                                <asp:Label ID="lblDeldate" runat="server"></asp:Label><br />
                                <asp:Label ID="Label6" runat="server" Style="width: 100px; font-weight: bold">
                                Received date : </asp:Label>
                                <asp:Label ID="lblrecdate" runat="server"></asp:Label><br />
                                <asp:Label Visible="false" ID="Label4" runat="server" Style="width: 100px; font-weight: bold">
                                Fit : </asp:Label>
                                <asp:Label ID="lblfit" Visible="false" runat="server"></asp:Label><br />
                            </td>
                        </tr>
                    </table>
                    <table width="595px" class="style1">
                        <tr valign="top">
                            <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                                <label style="font-weight: bold">
                                    Fabric Detailed Report :</label>
                                <asp:GridView runat="server" BorderWidth="1" ID="gridnewprint" CssClass="myleft"
                                    GridLines="Vertical" AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false"
                                    OnRowDataBound="RatioShirtProcess_OnDataBound" ShowHeader="true" ShowFooter="true"
                                    Width="106.5%" Style="font-family: 'Trebuchet MS'; font-size: 13px;">
                                    <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="FabName" DataField="FabName" />
                                        <asp:BoundField HeaderText="Meter" DataFormatString='{0:f}' DataField="Meter" />
                                        <asp:BoundField HeaderText="ActualMeter" DataFormatString='{0:f}' DataField="ActualMeter" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" style="border-spacing: 1px; border-collapse: collapse"
                        class="style1">
                        <tr height="100px">
                            <td width="20%" align="center">
                                <asp:Label ID="Label22" runat="server" Style="width: 500%; font-weight: bold">
                                TAILOR SIGN </asp:Label>
                            </td>
                            <td width="40%" align="center" runat="server" visible="false">
                                <asp:Label ID="Label23" runat="server" Style="width: 500%; font-weight: bold">
                               CHECKED BY </asp:Label>
                            </td>
                            <td id="Td1" width="20%" align="center" runat="server">
                                <asp:Label ID="Label24" runat="server" Style="width: 500%; font-weight: bold">
                                CHECKED BY </asp:Label>
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
                    <asp:Button ID="btnexit" runat="server" Text="Exit" PostBackUrl="~/Accountsbootstrap/viewcuttingBC.aspx" />
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
