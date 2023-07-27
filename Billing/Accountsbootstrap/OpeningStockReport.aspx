<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpeningStockReport.aspx.cs"  Inherits="Billing.Accountsbootstrap.OpeningStockReport" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Accessories Opening Stock Report</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet"/>
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via  -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            //        var prtContent = document.getElementById(gridOpening);
            var WinPrint = window.open('', '', 'letf=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
</script>
</head>
<body>
    <usc:header id="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header" style="text-align: center; color: Red">
              Accessories Opening Stock Report</h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
    <form id="Form1" runat="server">
        <div class="col-lg-12">
            <div class="panel panel-default">
            <div align="right">
                            <asp:Button ID="Button1"   runat="server" CssClass="btn btn-block center-block" Text="Print" 
                                        Width="125px" OnClientClick="javascript:CallPrint('bill');" xmlns:asp="#unknown"  />
                            </div>
                <div class="panel-body" id="bill">
                    <div class="row">
                        
                        <div>
                                   
                                   <table id="Table1" runat="server" style="border:1px solid Grey;height:15px;background-color:#59d3b4;text-transform:uppercase"  width="100%" >
                <tr>
                                       
                    <td align="center" style="font-size:small1;width:70px" >
                      Date  
                    </td>
                   
                    <td align="center" style="font-size:small1;width:750px"  >
                    Category
                    </td>
                    <td align="center" style="font-size:small">
                         Product
                    </td>
                    <td align="center" style="font-size:small" >
                         Quantity
                    </td>
                       <td align="center" style="font-size:small" >
                         Rate
                    </td>
                       <td align="center" style="font-size:small" >
                         Stock Rate
                    </td>
                    </tr>
                    </table>
                     <div align="center">
                        <asp:GridView ID="gridOpening"  width="100%"  onrowdatabound="gvstock_RowDataBound"  ShowFooter="true"  runat="server" AutoGenerateColumns="false" CssClass="myGridStyle1">
                        <Columns>
                         <asp:BoundField DataField="OpenStockID"  Visible="false" />
                                                              <asp:BoundField DataField="StockDate" ItemStyle-Width="5%"  dataformatstring="{0:dd/MM/yyyy}" />
                                    <%--  <asp:BoundField DataField="BrandName" HeaderText="Brand Name" />--%>
                                       <asp:BoundField DataField="category"  ItemStyle-Width="55%" />
                                        <asp:BoundField DataField="Serial_No"  ItemStyle-Width="15%"  />
                                         <asp:BoundField DataField="Nos"  />
                                          <asp:BoundField DataField="purchaserate"  />
                                           <asp:BoundField DataField="amnt"  />
                         
                        </Columns>
                           <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        </asp:GridView>
                        </div>
                        </div>
                        
                       
                        
                    </div>
                    </div>

                     
 </form>


              
</body>
</html>
