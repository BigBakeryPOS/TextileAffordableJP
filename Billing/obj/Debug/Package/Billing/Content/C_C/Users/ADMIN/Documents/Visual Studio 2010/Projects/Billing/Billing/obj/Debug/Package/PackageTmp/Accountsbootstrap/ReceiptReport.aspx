<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptReport.aspx.cs" Inherits="Billing.Accountsbootstrap.ReceiptReport" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">

<head runat="server">

    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <title>Receipt Grid Master - bootsrap</title>

   <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript">
        function alertMessage() {
            alert('Are You Sure, You want to delete This Customer!');
        }
    </script>
</head> 
<body>
             <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
   <usc:Header ID="Header" runat="server" />
    <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Receipt Report</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>


          <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <form runat="server" id="form1" method="post">
                                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                  <div class="form-group">
                                            <label>From Date</label>
											<asp:TextBox CssClass="form-control" ID="txtfrmdate" runat="server" Text="--Select Date--"></asp:TextBox>
                                                                                       
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="txtfrmdate1" TargetControlID="txtfrmdate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                       
										<div class="form-group">
                                            <label>To Date</label>
											<asp:TextBox CssClass="form-control" ID="txttodate" runat="server" Text="--Select Date--"></asp:TextBox>
                                                                                       
                                        </div>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txttodate" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                  <button type="reset" class="btn btn-primary">Generate Report</button>

                               <div class="table-responsive">
                                        
                                <table class="table table-bordered table-striped">
                                <tr>
                                <td>
                                <asp:GridView ID="gvReceipt"   runat="server" AllowPaging="true" PageSize="3"  OnPageIndexChanging="Page_Change" AutoGenerateColumns="false" onrowcommand="gvReceipt_RowCommand" >
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" PreviousPageText="Previous" />
    <Columns>
    <asp:BoundField HeaderText="Receipt ID" DataField="ReceiptID" />
      <asp:BoundField HeaderText="Receipt No" DataField="ReceiptNo" />
        <asp:BoundField HeaderText="Receipt Date" DataField="ReceiptDate" />
            <%--<asp:BoundField HeaderText="Bill No" DataField="BillNo" />--%>
    <asp:BoundField HeaderText="Customer Name" DataField="CustomerID" />
     <asp:BoundField HeaderText="Customer Name"  />
      <asp:BoundField HeaderText="Bill Amount" DataField="BillAmount" DataFormatString="{0:f}" />    
    <asp:BoundField HeaderText="Amount Paid" DataField="Amount"  DataFormatString="{0:f}"/>
    <asp:BoundField HeaderText="Balance" DataField="Balance" DataFormatString="{0:f}" />
        <asp:BoundField HeaderText="Payment By" DataField="Payment_ID" />

     
    
    <%--<asp:BoundField HeaderText="Amount" DataField="Amount" />--%>
     <asp:TemplateField HeaderText="GetDetails">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("ReceiptID") %>' CommandName="edit"><asp:Image ID="img" runat="server" ImageUrl="~/images/info_button.png" /></asp:LinkButton>
      
     </ItemTemplate>
    
     
     
     </asp:TemplateField>
     
   </Columns>
   </asp:GridView>
                                </td>
                                </tr>
                                </table>
                                </div>
                                <asp:Button ID="btnexcel" Text="Export" runat="server" CssClass="btn btn-danger" 
                                          onclick="btnexcel_Click" />


                                       
                                   
                                    </form>
                                </div>
                                
                                <!-- /.col-lg-6 (nested) -->
                            </div>
                            <!-- /.row (nested) -->
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>


</body>

</html>

