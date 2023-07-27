<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpeningBalanceReport.aspx.cs" Inherits="Billing.Accountsbootstrap.OpeningBalanceReport" %>
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
    <title>Opening Balance List</title>
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
      <div class="col-lg-8">
            <h2 class="page-header" style="text-align: right; color: Red">
                Opening Balance Report</h2>
                </div>
                <div class="col-lg-4" align="right">
                <asp:Button ID="btnprint" runat="server" Text="Print" Width="125px" CssClass="btn btn-block center-block" OnClientClick="javascript:CallPrint('bill');" xmlns:asp="#unknown" />
                </div>
               
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
    <form id="Form1" runat="server">
        <div class="col-lg-12">
            <div class="panel panel-default">
             
                <div class="panel-body">
                    <div class="row">
                         <div class="col-lg-12" id="bill">
                                <div id="Div1">
                                 <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                  <h2 align="center"> <asp:Label ID="lblMessage" style="color:Blue;" runat="server"></asp:Label></h2>
                                   <div class="col-lg-2">
                                        <div class="form-group">
                                            <label>Select Company</label>
                                            <asp:DropDownList ID="ddloutlet" runat="server" OnSelectedIndexChanged="branch_selectedIndex" AutoPostBack="true" CssClass="form-control">
                                            
                                            </asp:DropDownList>
                                                                                       
                                        </div>
                                        </div>
                        <div>
                                   
                                   <table id="Table1" runat="server" style="border:1px solid Grey;height:15px;background-color:#59d3b4;text-transform:uppercase"  width="100%" >
                <tr>
                 <td align="center" style="font-size:small1;width:150px" >
                      Branch Name  
                    </td>
                                       
                    <td align="center" style="font-size:small1;width:446px" >
                      Ledger Name  
                    </td>
                   
                    <td align="center" style="font-size:small1;width:750px"  >
                    Debit
                    </td>
                    <td align="center" style="font-size:small">
                         Credit
                    </td>
                   
                    </tr>
                    </table>
                     <div align="center">
                        <asp:GridView ID="gridOpening"  width="100%"  onrowdatabound="gvstock_RowDataBound"  ShowFooter="true"  runat="server" AutoGenerateColumns="false" CssClass="myGridStyle1">
                        <Columns>
                      
                                                          
                                  <asp:BoundField DataField="branch"  ItemStyle-Width="15%" />
                                       <asp:BoundField DataField="PrintName"  ItemStyle-Width="55%" />
                                       <asp:BoundField DataField="Open_Depit"   ItemStyle-HorizontalAlign="Right"  />
                                        <asp:BoundField DataField="Open_Credit"  ItemStyle-Width="10%"   ItemStyle-HorizontalAlign="Right"  />
                                         
                                      
                         
                        </Columns>
                           <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Right" />
                        </asp:GridView>
                        </div>
                        </div>
                        
                        
                        
                        <%--<div align="right">
                            <asp:Button ID="Button1"   runat="server" CssClass="btn btn-block center-block" Text="Print" 
                                        Width="125px" onclick="Button1_Click"  />
                            </div>--%>
                       
                        
                    </div>
                    </div>
                    </div>
                    </div>
                    </div>
                    </div>
                     
 </form>


              
</body>
</html>
