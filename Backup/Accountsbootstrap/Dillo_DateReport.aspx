<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dillo_DateReport.aspx.cs" Inherits="Billing.Accountsbootstrap.Dillo_DateReport" %>

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
    <title>Flexible Apparels || Date Wise Report</title>
    <style type="text/css">
        
        .HeaderFreez  
        {  
        position:relative ;   
        top:expression(this.offsetParent.scrollTop);  
        z-index: 10;  
        }   

        .GroupHeaderStyle
        {
            background-color:#afc3dd;
            color:Black;
            font-weight:bold;
            text-transform: uppercase;
        }
        .SubTotalRowStyle
        {
            background-color:#cccccc;
            color:Black;
            font-weight:bold;
        }
        .GrandTotalRowStyle
        {
           background-color:#000000;
            color:white;
            font-weight:bold; 
        }
        .align1
        {
           text-align:right; 
        }
        
        .myGridStyle1 tr th

        {
           

            padding: 8px;

            color: #afc3dd;
           
            background-color:#000000;
            border: 1px solid gray;
            font-family : Arial;
            font-weight:bold;
            text-align: center;
            text-transform:uppercase;

        }

         

         

        .myGridStyle1 tr:nth-child(even)

        {

            background-color: #ffffff ;

        }

         

        .myGridStyle1 tr:nth-child(odd)

        {

            background-color:#ffffff;

        }

         

        .myGridStyle1 td

        {

            border:1px solid gray;

            padding: 8px;

        }
        
       
    </style>

    <%--<script type='text/javascript' src='../Scripts/x.js'></script>--%>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script src="../Scripts/ScrollableTablePlugin_1.0_min.js" type="text/javascript"></script>
<%--<script type="text/javascript">
    $(function () {
        $('#Table1').Scrollable({
            ScrollHeight: 100
        });
    });
</script>--%>

    <script type="text/javascript">
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    
    <script type="text/javascript">
        function Denomination() {
            var gridData = document.getElementById('gridPurchase');
            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
            'left=100,top=100,right=100,bottom=100,width=900,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');
            //prtWindow.document.write(("Current date:" + document.getElementById('lblStartdate').textContent) + "</br>");
            prtWindow.document.write(("Date Wise Report"));
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
            //window.open("../Accountsbootstrap/Dillo_DateReport.aspx");
        }
</script>

   <!-- Start Styles. Move the 'style' tags and everything between them to between the 'head' tags -->
   <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
  <link href="../Styles/style1.css" rel="stylesheet"/>

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
   
 
</head>
 
<body>
 <usc:Header ID="Header" runat="server" />
<asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
  
   
   <div class="row">
                <div class="col-lg-12" >
                    <h1 class="page-header" style="text-align:center;color:Red">Lot Datewise Report </h1>
                </div>
               
            </div>


          <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                 <form id="form1" runat="server">
                                 <div class="row">
                                 <div class="col-lg-3">
                                 </div>
                                 <div class="col-lg-2">
                                  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                 <div class="form-group">
                                        <label>
                                            Start Date</label>
                                    <asp:TextBox ID="txtstartdate" onkeydown="return DateFormat(this, event.keyCode)" runat="server" CssClass="form-control"></asp:TextBox>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtstartdate"
                                        PopupButtonID="txtstartdate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                    </div>
                                 </div>
                                  <div class="col-lg-2">
                                    <div class="form-group">
                                        <label>
                                            End Date</label>
                                     <asp:TextBox ID="txtenddate" onkeydown="return DateFormat(this, event.keyCode)" runat="server" CssClass="form-control"></asp:TextBox>
                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtenddate"
                                        PopupButtonID="txtenddate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                        CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                        
                                    </div>

                                    <div class="col-lg-2">
                                    <div class="form-group">
                                     <asp:Button ID="btnsearch" runat="server" style="margin-top: -50px; margin-left: 223px;" class="btn btn-success" Text="Generate Process" OnClick="btnsearch_click" />
                                        
                                    </div>
                                </div>

                                <div class="col-lg-2">
                                    <div class="form-group">
                                <asp:Button ID="btnPrint" runat="server" class="btn btn-success" Text="Print" style="margin-top:-50px; margin-left:359px"   OnClientClick="Denomination()"/>
                            </div>
                            </div>

                                  </div>
                                   </div>
                                      <h2 align="center"> <asp:Label ID="lblMessage" style="color:Blue;" runat="server"></asp:Label></h2>
                                    <div class="row">
                                      <div class="col-lg-12">
                                    <div class="table-responsive">
                                   <table class="table table-bordered table-striped" id="Table1">
                                <tr>
                                <td>
                                     <div >
                                    <asp:GridView ID="gridPurchase" Width="100%" runat="server" EmptyDataText="Sorry Data Not Found!" CssClass="myGridStyle1" 
                                            AutoGenerateColumns="false" onrowcreated="gridPurchase_RowCreated" onrowdatabound="gridPurchase_RowDataBound">
                                            <HeaderStyle CssClass="HeaderFreez" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Lot No" DataField="lotno" />
                                        <asp:BoundField HeaderText="Unit Name" DataField="UnitName" />
                                        <asp:BoundField HeaderText="Received date" DataField="date" DataFormatString='{0:d}' />
                                        <asp:BoundField HeaderText="Employee Name" Visible="false" DataField="name" />
                                       <%-- <asp:BoundField HeaderText="Employee ID" Visible="false" DataField="Employee_Id" />--%>
                                        <asp:BoundField HeaderText="Process Name" DataField="processtype"/>
                                        <asp:BoundField HeaderText="Qty" ItemStyle-HorizontalAlign="Right" DataField="qty" DataFormatString='{0:N2}' />
                                        <asp:BoundField HeaderText="Rate" Visible="false" ItemStyle-HorizontalAlign="Right"  DataField="rate" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Total Rate" Visible="false" ItemStyle-HorizontalAlign="Right" DataField="ratee" DataFormatString='{0:f}' />
                                        
                                    </Columns>
                                    </asp:GridView>
                                    </div>
                                    </td>
                                    </tr>
                                    </table>
                                    </div>
                                    
                                    </div>
                                    </div>
                                    
                                    </div>
                                     <div class="row">
                                    <div class="form-group">
                                    <div class="col-lg-4">
                                    </div>
                                    <div  runat="server" visible="false" class="col-lg-4" >
                                   
                                    <asp:Button ID="btnExport"  runat="server" CssClass="btn btn-block center-block" Text="Export To Excel" 
                                        Width="125px"/>
                                   
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
