<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dillo_Employeewisereport.aspx.cs" Inherits="Billing.Accountsbootstrap.Dillo_Employeewisereport" %>

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
    <title>Flexible Apparels || Employee Wise Report </title>
    <style type="text/css">
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
            prtWindow.document.write(("Employee Wise Report"));
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
            window.open("../Accountsbootstrap/Dillo_Employeewisereport.aspx");
            return;
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
                    <h1 class="page-header" style="text-align:center;color:Red">Lot Employee Wise Report </h1>
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
                                    <div class="form-group">
                                        <label>
                                            Select Employee</label>
                                 <asp:DropDownList ID="drpemp" runat="server" CssClass="form-control"></asp:DropDownList>
                                 <asp:Label ID="lblemployeename" runat="server" Visible="false"></asp:Label>
                                    </div>
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
                                   <table class="table table-bordered table-striped">
                                <tr>
                                <td>
                                 <table id="Table1" visible="false" runat="server" style="border: 1px solid Grey; height: 15px; background-color: Black;color:White;
                            text-transform: uppercase" width="100%">
                            <tr>
                                <td align="center" style="font-size: small;width:8%">
                                    Lot No
                                </td>
                                <td align="right" style="font-size: small;width:5%">
                                    Received Date
                                </td>
                                <td align="center" style="font-size: small;width:31%">
                                   Employee Name
                                </td>
                                <td align="left" style="font-size: small;width:18%">
                                    Process Name
                                </td>
                                <td align="left" style="font-size: small;width:13%">
                                    Qty
                                </td>
                                <td align="center" style="font-size: small;width:8%">
                                    Rate
                                </td>
                                 <td align="center" style="font-size: small;width:8%">
                                    Total Rate
                                </td>
                                
                            </tr>
                        </table>
                                
                                    <asp:GridView ID="gridPurchase" Width="75%" runat="server" EmptyDataText="Sorry Data Not Found!" CssClass="myGridStyle1" 
                                            AutoGenerateColumns="false" onrowcreated="gridPurchase_RowCreated" 
                                            onrowdatabound="gridPurchase_RowDataBound">
                                  <Columns>
                                        <asp:BoundField HeaderText="Lot No" DataField="lotno" HeaderStyle-Width="5px" />
                                        <asp:BoundField HeaderText="Received date" HeaderStyle-Width="15px" DataField="date" DataFormatString='{0:d}' />
                                        <asp:BoundField HeaderText="Employee Name" Visible="false" DataField="name" />
                                       <%-- <asp:BoundField HeaderText="Employee ID" Visible="false" DataField="Employee_Id" />--%>
                                        <asp:BoundField HeaderText="Process Name" DataField="processtype"/>
                                        <asp:BoundField HeaderText="Qty" ItemStyle-HorizontalAlign="Right" DataField="qty" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Rate"  ItemStyle-HorizontalAlign="Right"  DataField="rate" DataFormatString='{0:f}' />
                                        <asp:BoundField HeaderText="Total Rate" ItemStyle-HorizontalAlign="Right" DataField="ratee" DataFormatString='{0:f}' />
                                        
                                    </Columns>
                                    </asp:GridView>
                                  
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
                                    <div id="Div1"  runat="server" visible="false" class="col-lg-4" >
                                   
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
