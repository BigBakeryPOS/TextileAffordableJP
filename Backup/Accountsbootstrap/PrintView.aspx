<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintView.aspx.cs" Inherits="Billing.Accountsbootstrap.PrintView" %>

<%--<%@ Register TagPrefix="cc2" Namespace="ControlFreak" Assembly="ExportPanel" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        .gradient
        {
            background: #ffffff; /* Old browsers */
            background: -moz-linear-gradient(top,  #ffffff 0%, #50d8fd 3%, #50d8fd 5%, #49d1f6 13%, #49d0f6 15%, #3cc4ea 28%, #3bc0e4 31%, #33b9dd 41%, #2db3d7 46%, #2bb1d5 51%, #199cc1 74%, #1091b4 85%, #0485a8 100%); /* FF3.6-15 */
            background: -webkit-linear-gradient(top,  #ffffff 0%,#50d8fd 3%,#50d8fd 5%,#49d1f6 13%,#49d0f6 15%,#3cc4ea 28%,#3bc0e4 31%,#33b9dd 41%,#2db3d7 46%,#2bb1d5 51%,#199cc1 74%,#1091b4 85%,#0485a8 100%); /* Chrome10-25,Safari5.1-6 */
            background: linear-gradient(to bottom,  #ffffff 0%,#50d8fd 3%,#50d8fd 5%,#49d1f6 13%,#49d0f6 15%,#3cc4ea 28%,#3bc0e4 31%,#33b9dd 41%,#2db3d7 46%,#2bb1d5 51%,#199cc1 74%,#1091b4 85%,#0485a8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#ffffff', endColorstr='#0485a8',GradientType=0 ); /* IE6-9 */
        }
        
        .watermark
        {
            display: block;
            z-index: 99999;
            width: 86%;
            position: absolute;
            text-align: center !important;
        }
        .watermark img
        {
            opacity: 0.2;
            filter: alpha(opacity=15);
        }
    </style>
    <style>
        .mynewGridStyle tr:nth-child(even)
        {
            background-color: #F0F0F0;
        }
        .mynewGridStyle tr:nth-child(odd)
        {
            /*background-color:#D3D3D3;*/
        }
        .mynewGridStyle td
        {
            border: 0px solid gray;
            padding: 8px;
        }
        
        .mynewGridStyle
        {
            border-collapse: collapse;
            border: 0px solid gray;
            overflow: hidden;
        }
        .mynewGridStyle tr th
        {
            padding: 8px;
            color: #ffffff; /*background-color:#27abcf;*/
            border: 0px solid gray;
            font-family: Segoe UI;
            font-size: 10pt;
            text-align: center;
            font-weight: normal;
        }
        
        .mynewGridStyle tr:nth-child(even)
        {
            background-color: #c5f3ff;
        }
        .mynewGridStyle tr:nth-child(odd)
        {
            /*background-color:#D3D3D3;*/
        }
        .mynewGridStyle td
        {
            border: 0px solid gray;
            padding: 2px;
        }
        
        
        table.ex1
        {
            border: 0px solid black;
        }
        .style1
        {
            width: 308px;
        }
        .style2
        {
            width: 187px;
        }
        
        .rightAlign
        {
            text-align: right;
        }
        .style5
        {
            width: 433px;
        }
        .style7
        {
            width: 379px;
        }
        
        .container img
        {
            vertical-align: middle;
        }
        
        .container .content
        {
            position: absolute;
            bottom: 0;
            background: rgba(0, 0, 0, 0.5); /* Black background with transparency */
            color: #f1f1f1;
            width: 100%;
            padding: 20px;
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
        
        .watermark123
        {
            display: block;
            z-index: 99999;
            width: 86%;
            position: absolute;
            text-align: center !important;
        }
        .watermark img
        {
            opacity: 0.2;
            filter: alpha(opacity=15);
        }
    </style>
</head>
<body style="background-color:Silver">
    <form id="form1" runat="server">
    <div class="watermark">
        <div>
            <img src="../images/Spd.png" style="padding-top: 420px; margin-right: -85px;" />
        </div>
    </div>
    <asp:Panel ID="Panel1" runat="server">
        <div>
            <table style="height: 100%;" border="1" bordercolor="brown">
                <tr>
                    <td>
                        <table width="100%" border="1">
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 20%">
                                                <asp:Image runat="server" Style="padding-left: 25px; width: 150px" AlternateText="imglogo"
                                                    ImageUrl="../images/Cer1.jpg" ID="img1" /><br />
                                                <label style="padding-left: 13px; font-size: 9px">
                                                </label>
                                            </td>
                                            <td style="width: 60%; text-align: center">
                                                <b>
                                                    <label style="font-weight: bold; color: #0485a8; font-family: Segoe UI; font-size: 25px">
                                                        &nbsp;&nbsp;&nbsp;&nbsp; SPEEDRO ENTERPRISES</label></b><br />
                                                <b>
                                                    <label style="font-weight: bold; color: #0485a8; font-family: Segoe UI; font-size: 25px">
                                                        &nbsp;&nbsp;&nbsp;&nbsp; India</label></b>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Image runat="server" Style="padding-left: 25px; width: 150px" AlternateText="imglogo"
                                                    ImageUrl="../images/Cer2.jpg" ID="Image1" /><br />
                                                <label style="padding-left: 13px; font-size: 9px">
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <table width="100%" border="2">
                            <tr>
                                <td style="width: 80%">
                                    <table style="width: 100%" border="1">
                                        <tr>
                                            <td colspan="2">
                                                <label style="font-weight: bold; color: #0485a8; font-size: 25px">
                                                    Vehicle Details</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label4" runat="server">Registration No.</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label5" runat="server">Registration Date.</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label3" runat="server">Chassis No.</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label6" runat="server">Engine No.</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label7" runat="server">Vehicle Type</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label8" runat="server">Vehicle Model</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label9" runat="server">Model No</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label10" runat="server">Model Name</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20%" style="text-align: center">
                                    <asp:Image runat="server" Style="padding-left: 25px; width: 150px" AlternateText="imglogo"
                                        ImageUrl="../images/QR.png" ID="Image2" /><br />
                                    <label style="padding-left: 13px; font-size: 9px">
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table id="Table1" width="100%" runat="server" visible="false">
                            <tr style="background-color: #c5d9f1">
                                <th>
                                    Contact Person
                                </th>
                                <th>
                                    Payment Terms
                                </th>
                                <th>
                                    Due Date
                                </th>
                            </tr>
                            <tr>
                                <td align="center">
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="2">
                            <tr>
                                <td style="width: 50%">
                                    <table style="width: 100%" border="1">
                                        <tr>
                                            <td colspan="2">
                                                <label style="font-weight: bold; color: #0485a8; font-size: 25px">
                                                    Vehicle Details</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label11" runat="server">Registration No.</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label12" runat="server">Registration Date.</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label13" runat="server">Chassis No.</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label14" runat="server">Engine No.</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label15" runat="server">Vehicle Type</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label16" runat="server">Vehicle Model</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label17" runat="server">Model No</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label18" runat="server">Model Name</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%">
                                    <table style="width: 100%" border="1">
                                        <tr>
                                            <td colspan="2">
                                                <label style="font-weight: bold; color: #0485a8; font-size: 25px">
                                                    Vehicle Details</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label19" runat="server">Registration No.</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label20" runat="server">Registration Date.</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label21" runat="server">Chassis No.</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label22" runat="server">Engine No.</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label23" runat="server">Vehicle Type</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label24" runat="server">Vehicle Model</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label25" runat="server">Model No</asp:Label>
                                            </td>
                                            <td valign="top" style="font-size: 16px" class="style5">
                                                <asp:Label ID="Label26" runat="server">Model Name</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table id="Table2" width="100%" runat="server" visible="true" style="border-spacing: 0px;"
                            border="1">
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <label style="font-weight: bold; color: #0485a8; font-size: 25px">
                                        Owner's Details</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>
                                        <asp:Label ID="Label27" runat="server">Owner Name/Company Name</asp:Label>
                                        <asp:Label ID="lblCompanyName1" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                </td>
                                <td style="padding-top: 5px; padding-left: 5px">
                                    <h5>
                                        <b>Benificiary Name :
                                            <asp:Label ID="lblBenificiaryName" runat="server"></asp:Label><br />
                                            Benificiary Account # :
                                            <asp:Label ID="lblBenificiaryAccount" runat="server"></asp:Label><br />
                                            Switch Code :
                                            <asp:Label ID="lblSwitchCode" runat="server"></asp:Label>
                                            <br />
                                            MICR CODE:
                                            <asp:Label ID="lblMICRCode" runat="server"></asp:Label><br />
                                            IFSC Code :
                                            <asp:Label ID="lblIFSCCode" runat="server"></asp:Label><br />
                                            Branch :
                                            <asp:Label ID="lblBranch" runat="server"></asp:Label></b></h5>
                                </td>
                            </tr>
                        </table>
                        <table id="Table3" width="100%" runat="server" visible="true" border="1">
                            <tr>
                                <td colspan="2" style="text-align: left">
                                    <label style="font-weight: bold; color: #0485a8; font-size: 25px">
                                        Prepared</label>
                                </td>
                            </tr>
                            <tr style="height: 40px">
                                <td valign="top">
                                    <b>
                                        <asp:Label ID="Label28" runat="server">Installed By:</asp:Label>
                                        <asp:Label ID="Label29" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                </td>
                                <td valign="top">
                                    <b>
                                        <asp:Label ID="Label30" runat="server">Certificate Issued By:</asp:Label>
                                        <asp:Label ID="Label31" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <table width="100%" style="margin-top: 10px">
                            <tr>
                                <td align="center" style="font-family: Segoe UI; padding-top: 0px; font-size: 13px">
                                    <b>If you have any Questions concerning to this Quotation please email to :
                                        <asp:Label ID="lbl08" runat="server"></asp:Label>
                                    </b>
                                    <br />
                                    <b>apmmotors.com</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="font-family: Segoe UI; font-size: 13px">
                                    <hr />
                                </td>
                            </tr>
                            <tr align="center">
                                <td style="font-family: Segoe UI; width: 100%; padding-left: 5px; font-size: 13px"
                                    class="style7">
                                    Head Office : Suite No.7, 3rd Floor, High Road, Triplicane, Chennai-600005.
                                    <br />
                                    <b>
                                        <asp:Label Visible="false" ID="lblFaxNo" runat="server" Style="font-weight: normal;"></asp:Label></b>
                                    Branch Office: No.2, 2 nd Ground floor, Plaza Apartment, Vimal Nagar, Delhi – 110025<br />
                                    <b>
                                        <label style="font-weight: normal;">
                                            Tel:</label><asp:Label ID="lblContactCompany" runat="server" Style="font-weight: normal;"></asp:Label>,</b>
                                    Website: www.apm.org
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
