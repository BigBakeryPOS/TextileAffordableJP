<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JournalsReport.aspx.cs" Inherits="Billing.Accountsbootstrap.JournalsReport" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <title>Journals Report </title>
   

     

   <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
        <link href="../Styles/style1.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet"/>
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />

    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
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
   <usc:Header ID="Header" runat="server" />
<asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>

   
   <div class="row">
                <div class="col-lg-12" >
                     <form id="form1" runat="server">
                     <div class="col-lg-2">
                      <h1 class="page-header" style="text-align:center;color:Red;font-size:20px">Journals Report</h1>
                                 </div>
                                 <div class="col-lg-2">
                                  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                 <div class="form-group">
                                 <asp:Label ID="lblFromDate" runat="server" style="font-weight:bold" >From Date</asp:Label>
                                  <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator2"
                                    Text="*" ControlToValidate="txtFromDate" ErrorMessage="Please enter From date!"
                                    Style="color: Red" />
                                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control center-block" ></asp:TextBox>
                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromDate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                 </div>
                            
                             </div>
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                    <asp:Label ID="lblToDate" runat="server"  style="font-weight:bold" >To Date</asp:Label>
                                     <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                    Text="*" ControlToValidate="txtToDate" ErrorMessage="Please enter To date!"
                                    Style="color: Red" />
                                      <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control center-block" ></asp:TextBox>
                                      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                    </div>
                                      </div>
                               <div class="col-lg-2" style="margin-top:-6px;margin-left:1px">
                                      <div class="form-group">
                                            <label>Select Company</label>
                                            <asp:DropDownList ID="ddloutlet" runat="server" CssClass="form-control">
                                           
                                            </asp:DropDownList>
                                                                                       
                                        </div>
                                   
                                      </div>
                                      <div class="form-group" style="padding-top:20px">
                                      <div class="col-lg-2"  style="margin-top:-2px;margin-left:-35px">
                                  <asp:Button ID="btnSearch" runat="server" Text="Generate Report" CssClass="btn btn-success" ValidationGroup="val1" onclick="btnSearch_Click" Width="130px" style="margin-left:35px"
                                           />
                                 </div>
                                 </div>
                </div>
               
            </div>


          <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                         <div align="right">
                            <asp:Button ID="btnprint"   runat="server" CssClass="btn btn-block center-block" Text="Print" 
                                        Width="125px" OnClientClick="javascript:CallPrint('bill');" xmlns:asp="#unknown"    />
                                    </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                               
                                
                                 
                                    <div class="row">  
                                    <div class="col-lg-12" id="bill">
                                     <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                 <div class="row">
                                
                                   </div>
                                   <h2 align="center"> <asp:Label ID="lblMessage" style="color:Blue;" runat="server"></asp:Label></h2>
                                    <div class="table-responsive">
                                   <table class="table table-bordered table-striped">
                                <tr>
                                <td >

                                  <table id="Table1" visible="false" runat="server" style="border: 1px solid Grey; height: 15px; background-color: #59d3b4;
                            text-transform: uppercase" width="100%">
                            <tr>
                                <td align="center" style="font-size: small1;width:8%">
                                    Trans No
                                </td>
                               
                                <td align="center" style="font-size: small;width:8%">
                                    Date
                                </td>
                                                                
                                <td align="center" style="font-size: small;width:25%">
                                    Debtor
                                </td>
                                 <td align="center" style="font-size: small;width:30%">
                                    Creditor
                                </td>
                                <td align="center" style="font-size: small">
                                    Amount
                                </td>
                                <td align="center" style="font-size: small">
                                    Narration
                                </td>
                            </tr>
                        </table>
                          <div align="center">

                                    <asp:GridView ID="gridPurchase" Width="100%" runat="server" 
                                        EmptyDataText="Data Not Bound" AutoGenerateColumns="false" AllowPaging="false" 
                                         OnRowDataBound="gridPurchase_RowDataBound" ShowFooter="true"
                                        onpageindexchanging="gridPurchase_PageIndexChanging" >
                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                    <Columns>
                                    <asp:BoundField  DataField="TransNo" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderText="Trans No"    />
                                    <asp:BoundField  DataField="TransDate" ItemStyle-Width="11%"  ItemStyle-HorizontalAlign="Center" HeaderText="Trans Date"  DataFormatString="{0:dd/MM/yyyy}"/>
                                   
                       <%--             <asp:BoundField HeaderText="Branch" DataField="Branch" ItemStyle-HorizontalAlign="Center"   />--%>
                                    <asp:BoundField  DataField="Debtor" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center"  HeaderText="Debtor"  />
                                    <asp:BoundField  DataField="Creditor" ItemStyle-Width="30%"  ItemStyle-HorizontalAlign="Center" HeaderText="Creditor"   />
                                    <asp:BoundField  DataField="Amount" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:###,##0.00}"   />
                                    <asp:BoundField  DataField="Narration" ItemStyle-HorizontalAlign="Center"  HeaderText="Narration"   />
                                
                                    
                                    
                                    </Columns>
                                    </asp:GridView>
                                    </div>
                                    <table border="0" cellspacing="0">
                                    <tr id="Tr1" runat="server" visible="false">
                                        <td width="670px">
                                             <asp:Label ID="lblamt" runat="server"></asp:Label>
                                        </td>
                                        </tr>
                                        </table>
                                   
                                   </td>
                                   </tr>
                                   </table>
                                    </div>
                                    
                                    </div>
                                    </div>
                                    <div class="row">
                                    <div class="form-group">
                                       <div class="col-lg-4">
                                     </div>
                                    <div class="col-lg-12">
                                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-block center-block" Text="Export To Excel" 
                                        Width="125px" onclick="btnExport_Click"   />
                                    </div>
                                       <div class="col-lg-4">
                                     </div>
                                    </div>
                                    </div>

                                  </form>
          </div>
          </div>
          </div>
          </div>
          </div>
          </div>
   
</body>
</html>
