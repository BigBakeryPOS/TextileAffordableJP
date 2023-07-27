<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dillo_OrderProcessPrintNew.aspx.cs" Inherits="Billing.Accountsbootstrap.Dillo_OrderProcessPrintNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Order Process Print</title>
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
    <style>
@media print

{

    .PrintButton{

        display:none;

    }

}



@media screen

{

    .PrintButton{

        display:block;

    }

} 

</style>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px";
        width="100%"; height="100px" class="style1">
        <tr>
            <td align="center" style="height:1px">
                <table width="100%" border="1" style="border-spacing: 1px; border-collapse: collapse"
                    class="style1">
                    <tr>
                        <td align="center">
                            <label style="font-size: large; font-weight: bold">
                                Order Process Print</label>
                        </td>
                    </tr>
                </table>
               
               <br />
                
                <table width="80%" height="80px" border="0" class="style1">
                    <tr>
                    <td  align="left" width="40%">
                    <img src="../images/LOGOJP.jpg" style="width:150px;height:80px;" alt="logo" />
                        
                    </td>
                    <td  align="left" width="60%">
                    <asp:Label ID="Label10" runat="server" Font-Size="20px" Font-Bold="true" style="width:100px">
                              <b> JP EXPORTS</b> </asp:Label> <br />
                        

                               <asp:Label ID="Label3" runat="server" style="width:100px">
                             33/10 singara garden, <br /> 1 st lane old washermenpet,chennai-600021 <br />Tamil Nadu, India</asp:Label>
                                <br />
                                <asp:Label ID="Label8" runat="server" style="width:100px">
                                Mobile : </asp:Label>

                               <asp:Label ID="Label9" runat="server" style="width:100px">
                               	098410 64610</asp:Label>
                               	
                            </td>
                                                        
                    </tr>
                  
                    </table>
                   
                    <hr />
                 
                   <table width="80%" height="80px" border="0" class="style1">

                    <tr>                       
                        <td valign="top" align="left" >

                        <asp:Label ID="Label5" runat="server" style="width:100px">
                                Order No : </asp:Label>
                            <asp:Label ID="lblOrderNo" runat="server" ></asp:Label><br />

                            <asp:Label ID="Label1" runat="server" style="width:100px">
                                Order date : </asp:Label>
                            <asp:Label ID="lblOrderdate" runat="server" ></asp:Label><br />


                            <asp:Label ID="Label4" runat="server" style="width:100px">
                               Supplier Name :</asp:Label>
                            <asp:Label ID="lblSupplierrName" runat="server"></asp:Label><br />

                            <%--<asp:Label ID="Label2" runat="server" style="width:100px">
                                Brand Name : </asp:Label>
                            <asp:Label ID="lblBrandName" runat="server"></asp:Label><br />

                            <asp:Label ID="Label3" runat="server" style="width:100px">
                                Unit Name :</asp:Label>
                            <asp:Label ID="lblUnitName" runat="server"></asp:Label>--%>

                        </td>
                        <td valign="top" align="left"  width="60%">
                        <asp:Label ID="Label6" runat="server" style="width:100px">
                               Address : </asp:Label>
                            <asp:Label ID="lblAddress" runat="server"></asp:Label><br />
                           
                           <asp:Label ID="Label7" runat="server" style="width:100px">
                               Agent Name :</asp:Label>
                            <asp:Label ID="lblAgentName" runat="server"></asp:Label><br />

                         <%--   <asp:Label ID="Label2" runat="server" style="width:100px">
                               Desgin No :</asp:Label>
                            <asp:Label ID="lblDesignNo" runat="server"></asp:Label>--%>

                        </td>
                    </tr>
                </table>
                <table width="90%" height="643px" border="0.5px" class="style1">
                    <tr valign="top">
                        <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                            <asp:GridView runat="server" BorderWidth="1" ID="gridprint" CssClass="left" GridLines="Both"
                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true" 
                                AllowPrintPaging="true" Width="100%" OnRowDataBound="gridprint_RowDataBound"
                                Style="font-family: 'Verdana'; font-size: 12px;">
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                                <Columns>
                                <asp:TemplateField HeaderText="S.no">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" DataField="orderno1" ItemStyle-Height="140" />--%>
                                     <asp:BoundField HeaderText="Item Name" DataField="Itemname" />
                                    <asp:BoundField HeaderText="Design No" DataField="Designno" />
                                    <%--<asp:BoundField HeaderText="color Name" DataField="color" ItemStyle-Height="140"/>--%>
                                    <asp:BoundField HeaderText="Piece" DataField="piecelist" />
                                    <asp:BoundField HeaderText="Meter" DataField="Meter" DataFormatString="{0:0}"  />
                                    <asp:BoundField HeaderText="Rate" ItemStyle-HorizontalAlign="right"  DataField="Rate" DataFormatString='{0:f}' />
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
                <asp:Button ID="btnexit" runat="server" Text="Exit"  PostBackUrl="~/Accountsbootstrap/OrderGrid.aspx" />
            </td>
        </tr>
    </table>
    </center>

    </form>

    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
                        <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
                        <script type="text/javascript">
                            window.onload = function () {
                                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
                            }
                        </script>

</body>
</html>
