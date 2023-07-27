<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptGrid.aspx.cs" Inherits="Billing.Accountsbootstrap.ReceiptGrid" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">

<head>

    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
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
    <style type="text/css">

        body
    {

            font: 12px verdana;

        }

         

        .myGridStyle
    {

            border-collapse:collapse;
             

        }

         

        .myGridStyle tr th

        {

            padding: 8px;

            color: white;

            border: 1px solid black;

        }

         

         

        .myGridStyle tr:nth-child(even)

        {

            background-color: #E1FFEF;

        }

         

        .myGridStyle tr:nth-child(odd)

        {

            background-color:Teal;

        }

         

        .myGridStyle td

        {

            border:1px solid black;

            padding: 8px;

        }

         

        .myGridStyle tr:last-child td

        {

        }

    </style>


</head> 
<body>
             <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
   <usc:Header ID="Header" runat="server" />
    <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Receipt Master</h1>
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
                                    <div class="form-group">
                                            <label>Filter By</label>
                                            <asp:DropDownList ID="ddlbillno" CssClass="form-control" style="width:150px;" 
                                                runat="server">
                                            <%--<asp:ListItem Text="Bill No" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Customer Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Area" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="City" Value="4"></asp:ListItem>--%>

                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlcustomername" CssClass="form-control" runat="server" style="width:273px; margin-left:160px; margin-top:-34px"></asp:DropDownList>
                                                 
                                                  

                                               
                                        <asp:Button ID="btnsearch" runat="server" class="btn btn-success" Text="Search" OnClick="Search_Click" style="margin-top: -34px;margin-left: 450px;"  /> 
                                        <asp:Button ID="btnrefresh" runat="server" class="btn btn-warning" Text="Reset" OnClick="refresh_Click" style="margin-top: -34px;margin-left: 550px;"  /> 
                                        </div> 
                               

                               <div class="table-responsive">
                                        
                                <table class="table table-bordered table-striped">
                                <tr>
                                <td>
                                <asp:GridView ID="gvReceipt"   runat="server" AllowPaging="true"   OnPageIndexChanging="Page_Change" AutoGenerateColumns="false"  CssClass="myGridStyle" onrowcommand="gvReceipt_RowCommand" >
                               
                                <HeaderStyle BackColor="Red" />
    <Columns>
    <asp:BoundField  HeaderText="Receipt ID" DataField="ReceiptID" />
      <asp:BoundField HeaderText="Receipt No" DataField="ReceiptNo" />
    <%--<asp:BoundField HeaderText="Bill No" DataField="BillNo" />--%>
    <asp:BoundField HeaderText="Customer Name" DataField="CustomerName" />
    <asp:BoundField HeaderText="Area" DataField="Area" />
    <asp:BoundField HeaderText="City" DataField="City" />
    <%--<asp:BoundField HeaderText="Amount" DataField="Amount" />--%>
     <asp:TemplateField HeaderText="View Details">
     <ItemTemplate>
     <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("ReceiptID") %>' CommandName="edit"><asp:Image ID="img" runat="server" ImageUrl="~/images/info_button.png" /></asp:LinkButton>
      
     </ItemTemplate>
    
     
     
     </asp:TemplateField>
     <asp:TemplateField HeaderText="Print">
     <ItemTemplate>
     <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#Eval("ReceiptID") %>' CommandName="print"><asp:Image ID="imgprint" runat="server" ImageUrl="~/images/print (2).png" /></asp:LinkButton>
     </ItemTemplate>
     </asp:TemplateField>
     
   </Columns>
   </asp:GridView>
                                </td>
                                </tr>
                                </table>
                                </div>



                                     <asp:Button ID="btnadd" runat="server" class="btn btn-success" Text="Add" OnClick="Add_Click" />  
                                   
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
