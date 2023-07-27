<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DilloMultiLotPrint.aspx.cs" Inherits="Billing.Accountsbootstrap.DilloMultiLotPrint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">

<head id="Head1" runat="server">

    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
	<script src="../jqueryCalendar/script.js" type="text/javascript"></script>
	<script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
<script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
<link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css"/>
<script type="text/javascript">
    jQuery(function () {
        jQuery("#inf_custom_someDateField").datepicker();
    });
                

	    
                            
</script>
<script type="text/javascript">
    function fixform() {
        if (opener.document.getElementById("aspnetForm").target != "_blank") return;
        opener.document.getElementById("aspnetForm").target = "";
        opener.document.getElementById("aspnetForm").action = opener.location.href;
    }
</script>
      
     

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
    <script type = "text/javascript">
        window.print();
</script>
 
    <style type="text/css">
@media print{
  body{ background-color:#FFFFFF; background-image:none; color:#000000 }
  #ad{ display:none;}
  #leftbar{ display:none;}
  #contentarea{ width:100%;}
}
</style>
</head> 

<body onload="window.print()" >


                   <form id="Form1" runat="server" role="form">
                    <asp:Panel id="pnlContents" runat="server" >
                   

            <!-- /.row -->
            <div    align="center" >
         
        <%-- <table style="border-spacing: 1px; border-collapse: collapse; outline: black solid 1px";
        width="100%"; height="100px" class="style1">
        <tr>
            <td align="center" style="height:1px">
             --%>
                         <table width="80%" height="80px" border="0" class="style1">
                    <tr>
                    <td  align="center" width="40%">
                    <img src="../images/dillo1.PNG" style="width:150px;height:80px;" alt="logo" />
                
                    </td>
                   <%-- <td  align="left" width="60%">
                    <asp:Label ID="Label10" runat="server" Font-Size="20px" Font-Bold="true" style="width:100px">
                              <b> Dillo</b> </asp:Label> <br />
                        

                               <asp:Label ID="Label3" runat="server" style="width:100px">
                               1/320/18a- Dillo Tower,<br /> Ashok Nagar Nattam, <br />Tamil Nadu, India</asp:Label>
                                <br />
                                <asp:Label ID="Label8" runat="server" style="width:100px">
                                Phone : </asp:Label>

                               <asp:Label ID="Label9" runat="server" style="width:100px">
                               	04544 244 517</asp:Label>
                               	
                            </td>--%>
                                                        
                    </tr>
                  
                    </table>
                    <br />
                      <table width="80%" height="80px" border="0" class="style1">

                    <tr>                       
                        <td valign="top" align="left" >

                        <asp:Label Font-Bold="true" ID="Label5" runat="server" style="width:100px">
                                Multi No : </asp:Label>
                            <asp:Label ID="lblMultiNo" Font-Bold="true" runat="server" ></asp:Label><br />

                            <asp:Label ID="Label1" Font-Bold="true" runat="server" style="width:100px">
                                Emp. Name : </asp:Label>
                            <asp:Label ID="lblemployeename" Font-Bold="true" runat="server" ></asp:Label><br />


                        </td>
                        <td valign="top" align="left"  width="60%">
                        <asp:Label ID="Label6" Font-Bold="true" runat="server" style="width:100px">
                               Date : </asp:Label>
                            <asp:Label ID="lblmultidate" Font-Bold="true" runat="server"></asp:Label><br />

                             <asp:Label ID="Label2" Font-Bold="true" runat="server" style="width:100px">
                               Total Qty : </asp:Label>
                            <asp:Label ID="lbltotalQty" Font-Bold="true" runat="server"></asp:Label><br />
                     
                        </td>
                    </tr>
                </table>

                 <table  class="style1">
                    <tr valign="top">
                        <td style="border-bottom: 1px solid black" colspan="3" width="595px">
                            <asp:GridView runat="server" BorderWidth="1" ID="gridprint" CssClass="left" GridLines="Both"
                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="false" ShowHeader="true" 
                                AllowPrintPaging="true" Width="100%" 
                                Style="font-family: 'Verdana'; font-size: 12px;">
                                <HeaderStyle BorderStyle="Solid" BorderWidth="1px" />
                               
                                <Columns>
                                <asp:TemplateField HeaderText="S.no">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
                                    <%--<asp:BoundField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" DataField="orderno1" ItemStyle-Height="140" />--%>
                                     <asp:BoundField HeaderText="Lot No" DataField="lotno" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField HeaderText="Bundle No" DataField="BundleNo" ItemStyle-Font-Bold="true" />
                                    <%--<asp:BoundField HeaderText="color Name" DataField="color" ItemStyle-Height="140"/>--%>
                                    <asp:BoundField HeaderText="Process Name" DataField="processtype" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField HeaderText="Send Qty" DataField="sendfQty" DataFormatString="{0:0}" ItemStyle-Font-Bold="true"  />
                                    <asp:BoundField HeaderText="Send Date" ItemStyle-HorizontalAlign="right"  DataField="senddate" ItemStyle-Font-Bold="true"  />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
               <br />
               <br />
               <br />
                <asp:PlaceHolder ID="plBarCode"   runat="server" />

                  
               <br />
               <br />
               
              <%--  </td>
                </tr>
                </table>--%>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                     
                    <!-- /.panel -->
               
      
        </div>
        
       </asp:Panel>
</form>
</body>

</html>
