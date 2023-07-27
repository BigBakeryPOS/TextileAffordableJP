<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemRateChecking.aspx.cs"
    Inherits="Billing.Accountsbootstrap.ItemRateChecking" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Item Rate Checking</title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link href="../css/chosen.css" rel="Stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <link rel="stylesheet" href="../css/chosen.css" />
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="FontSize" ForeColor="White" CssClass="label" Visible="false"
        Text="17"> </asp:Label>
    <form runat="server" id="form1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-10">
                            <asp:GridView ID="gvPORates" runat="server" CssClass="myGridStyle1" EmptyDataText="No Records Found"
                                Caption='Purchase Order' AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle BackColor="White" />
                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:BoundField HeaderText="PONo" DataField="FullPONo" />
                                    <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="CompanyName" DataField="CompanyName" />
                                    <asp:BoundField HeaderText="Item" DataField="Description" />
                                    <asp:BoundField HeaderText="Color" DataField="Color" />
                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gvIPOERates" runat="server" CssClass="myGridStyle1" EmptyDataText="No Records Found"
                                Caption='Item Process Order' AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle BackColor="White" />
                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:BoundField HeaderText="OrderNo" DataField="FullPONo" />
                                    <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="CompanyName" DataField="CompanyName" />
                                    <asp:BoundField HeaderText="Item" DataField="Description" />
                                    <asp:BoundField HeaderText="Color" DataField="Color" />
                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                            </asp:GridView>

                              <asp:GridView ID="gvIPORRates" runat="server" CssClass="myGridStyle1" EmptyDataText="No Records Found"
                                Caption='Item Process Order Receive' AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle BackColor="White" />
                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:BoundField HeaderText="RecNo" DataField="FullRecPONo" />
                                    <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="CompanyName" DataField="CompanyName" />
                                    <asp:BoundField HeaderText="ReceiveItem" DataField="ReceiveItem" />
                                    <asp:BoundField HeaderText="ReceiveColor" DataField="ReceiveColor" />
                                    <asp:BoundField HeaderText="Shrink" DataField="Shrink" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField HeaderText="IssueItem" DataField="IssueItem" />
                                    <asp:BoundField HeaderText="IssueColor" DataField="IssueColor" />
                                </Columns>
                            </asp:GridView>


                             <asp:GridView ID="gvIPORRates1" runat="server" CssClass="myGridStyle1" 
                                Caption='Purchase Order' AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle BackColor="White" />
                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:BoundField HeaderText="PONo" DataField="FullPONo" />
                                    <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="CompanyName" DataField="CompanyName" />
                                    <asp:BoundField HeaderText="Item" DataField="Description" />
                                    <asp:BoundField HeaderText="Color" DataField="Color" />
                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                            </asp:GridView>
                             <asp:GridView ID="gvIPORRates2" runat="server" CssClass="myGridStyle1"
                                Caption='Item Process Order' AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle BackColor="White" />
                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:BoundField HeaderText="OrderNo" DataField="FullPONo" />
                                    <asp:BoundField HeaderText="OrderDate" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="CompanyName" DataField="CompanyName" />
                                    <asp:BoundField HeaderText="Item" DataField="Description" />
                                    <asp:BoundField HeaderText="Color" DataField="Color" />
                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                            </asp:GridView>

                            <asp:GridView ID="gvOPSRates" runat="server" CssClass="myGridStyle1" EmptyDataText="No Records Found"
                                Caption='Material Opening StockEntry' AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle BackColor="White" />
                                <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <Columns>
                                    <asp:BoundField HeaderText="Date" DataField="DefaultDate" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField HeaderText="CompanyName" DataField="CompanyName" />
                                    <asp:BoundField HeaderText="Item" DataField="Description" />
                                    <asp:BoundField HeaderText="Color" DataField="Color" />
                                    <asp:BoundField HeaderText="Rate" DataField="Rate" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-lg-1">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
