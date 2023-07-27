<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LotRaw.aspx.cs" Inherits="Billing.Accountsbootstrap.LotRaw" %>


<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Cutting Accessories Issued</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
      <link rel="Stylesheet" type="text/css" href="../css/date.css" />
      <link href="../Styles/style1.css" rel="stylesheet"/>
    






      <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <link rel="stylesheet" href="../Styles/chosen.css" />
     <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 900px;
            text-align: center;
            border: 3px solid #0DA9D0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 40px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            padding: 5px;
        }
        .modalPopup .footer
        {
            padding: 3px;
        }
        .modalPopup .button
        {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
        .modalPopup td
        {
            text-align: left;
        }
        
        .pad
        {
            padding-top: 50px;
        }
    </style>
    <%-- <script type="text/javascript" language="javascript">
        function valchk() {
            if (blankchk(document.getElementById('txtcustomername'), "Customer Name")
            //&& dropdownchk(document.getElementById('ddlgroup'), "Account Group")  
        && phonechk(document.getElementById('txtmobileno'), "MobileNo") && phonechk(document.getElementById('txtphoneno'), "PhoneNo")
        && blankchk(document.getElementById('txtblnce'), "Opening Balance") 
        && blankchk(document.getElementById('txtmobileno'), "MobileNo")
        && blankchk(document.getElementById('txtphoneno'), "Phone No") && blankchk(document.getElementById('txtarea'), "Area")
        && blankchk(document.getElementById('txtaddress'), "Address") && blankchk(document.getElementById('txtcity'), "City")
        && emailchk(document.getElementById('txtemail'), "Email")) {
                alert("true");
            }
            else {
                alert("false");
                return false;
            }
        }
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
    <style type="text/css">
    /* The side navigation menu */
.sidenav {
    height: 100%; /* 100% Full-height */
    width: 0; /* 0 width - change this with JavaScript */
    position: absolute; /* Stay in place */
    z-index: 1; /* Stay on top */
    top: 0;
    left: 0;
    background-color: #e6e61a; /* Black*/
    overflow-x: hidden; /* Disable horizontal scroll */
    padding-top: 60px; /* Place content 60px from the top */
    transition: 0.5s; /* 0.5 second transition effect to slide in the sidenav */
}

/* The navigation menu links */
.sidenav a {
    padding: 8px 8px 8px 32px;
    text-decoration: none;
    font-size: 25px;
    color: #818181;
    display: block;
    transition: 0.3s
}

/* When you mouse over the navigation links, change their color */
.sidenav a:hover, .offcanvas a:focus{
    color: #f1f1f1;
}

/* Position and style the close button (top right corner) */
.sidenav .closebtn {
    position: absolute;
    top: 0;
    right: 25px;
    font-size: 36px;
    margin-left: 50px;
}

/* Style page content - use this if you want to push the page content to the right when you open the side navigation */
#main {
    transition: margin-left .5s;
    padding: 20px;
}

/* On smaller screens, where height is less than 450px, change the style of the sidenav (less padding and a smaller font size) */
@media screen and (max-height: 450px) {
    .sidenav {padding-top: 15px;}
    .sidenav a {font-size: 18px;}
}
    
    </style>
    <style type="text/css">
body
{
    margin: 0;
    padding: 0;
    height: 100%;
}
.modal
{
   
    position: absolute;
    top: 0px;
    left: 0px;
    
    z-index: 100;
    opacity: 0.8;
    filter: alpha(opacity=60);
    -moz-opacity: 0.8;
    min-height: 100%;
}
#divImage
{
    display: none;
    z-index: 1000;
    position: fixed;
    top: 0;
    left: 0;
    background-color: White;
    height: 550px;
    width: 600px;
    padding: 3px;
    border: solid 1px black;
}
</style>
<script type="text/javascript">
    function LoadDiv(url) {
        var img = new Image();

        var bcgDiv = document.getElementById("divBackground");
        var imgDiv = document.getElementById("divImage");
        var imgFull = document.getElementById("imgFull");
        var imgLoader = document.getElementById("imgLoader");
        imgLoader.style.display = "block";
        img.onload = function () {
            imgFull.src = img.src;
            imgFull.style.display = "block";
            imgLoader.style.display = "none";
        };
        img.src = url;
        var width = document.body.clientWidth;
        if (document.body.clientHeight > document.body.scrollHeight) {
            bcgDiv.style.height = document.body.clientHeight + "px";
        }
        else {
            bcgDiv.style.height = document.body.scrollHeight + "px";
        }
        imgDiv.style.left = (width - 650) / 2 + "px";
        imgDiv.style.top = "20px";
        bcgDiv.style.width = "100%";

        bcgDiv.style.display = "block";
        imgDiv.style.display = "block";
        return false;
    }
    function HideDiv() {
        var bcgDiv = document.getElementById("divBackground");
        var imgDiv = document.getElementById("divImage");
        var imgFull = document.getElementById("imgFull");
        if (bcgDiv != null) {
            bcgDiv.style.display = "none";
            imgDiv.style.display = "none";
            imgFull.style.display = "none";
        }
    }
</script>

    <script  type="text/javascript">

        function openNav() {
            //  alert("HI");
            document.getElementById("mySidenav").style.width = "400px";
        }

        /* Set the width of the side navigation to 0 */
        function closeNav() {
            //  alert("HIII");
            document.getElementById("mySidenav").style.width = "0";
        }


        //         function openNav1() {
        //              alert("HI");
        //             document.getElementById("mySidenav1").style.width = "200px";
        //         }

        //         /* Set the width of the side navigation to 0 */
        //         function closeNav1() {
        //             document.getElementById("mySidenav1").style.width = "0";
        //         }
     </script>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
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

                function switchViews(obj, imG) {
                    var div = document.getElementById(obj);
                    var img = document.getElementById(imG);
                    if (div.style.display == "none") {
                        div.style.display = "inline";


                        img.src = "../images/minus.gif";

                    }
                    else {
                        div.style.display = "none";
                        img.src = "../images/plus.gif";

                    }
                }
    </script>


</head>
<body>
      <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" Visible="false" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
      <form   runat="server" >
  
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <h1 id="head" runat="server" class="page-header" style="text-align: center; color: #fe0002">Cutting Accessories Issued
            </h1>
           
         
      
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row" style="background-color:#c6efce">
        <div class="col-lg-12">
            <div class="panel panel-default" style="background-color:#c6efce">
                <div class="panel-body" style="background-color:#c6efce">
               
                  
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                     <div class="form-group">
                    <div id="add" runat="server" class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="col-lg-2">
                            <div class="form-group ">
                                        <label>
                                            Branch</label>
                                       <asp:DropDownList ID="drpbranch" OnSelectedIndexChanged="company_SelectedIndexChnaged" AutoPostBack="true" runat="server" CssClass="form-control"  ></asp:DropDownList>
                                    </div>
                                    </div>
                        <div class="col-lg-2">
                        
                            <div class="form-group" id="divcode" runat="server"  Visible="false">
                                <label>
                                    Ledgerid</label>
                                <asp:TextBox CssClass="form-control" ID="txtcuscode" runat="server"></asp:TextBox>
                              
                            </div>
                            

                                        <div class="form-group" style="display:none;">
                          </div>

                             
                             

                            <div class="form-group" >
                                <label>
                                    Lot No</label><br />
                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1" ControlToValidate="ddlLotNo"
                                    Text="*" ErrorMessage="Please Select Lot No!" Style="color: Red" />
                                <asp:DropDownList CssClass="chzn-select" ID="ddlLotNo" MaxLength="150" runat="server" Width="150px"
                                  OnSelectedIndexChanged="CheckingInfo_Load" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <div class="col-lg-2"> <div id="Div2" >
                           
                                <label>
                                    Half Sleeve</label>
                                <asp:TextBox CssClass="form-control" ID="txthalfsleev" Width="100%"  MaxLength="30" runat="server"></asp:TextBox>
                            
                          
                            </div>

                          
                           
                          
                          
                            
                         
                       
                        </div>
                        <div class="col-lg-2"> <div id="div3" class="form-group" runat="server">
                                <label>
                                   Full Sleeve</label>
                                <asp:TextBox CssClass="form-control" ID="txtfullsleev" Width="100%"  MaxLength="30" runat="server">0</asp:TextBox>
                            </div></div>
                             <div class="col-lg-2"> <div id="div1" class="form-group" runat="server">
                                <label>
                                   TOTAL SHIRTS</label>
                                <asp:TextBox CssClass="form-control" ID="txttotsleev" Width="100%"  MaxLength="30" runat="server">0</asp:TextBox>
                            </div></div>
                     
                    </div>
                    
                    </div>

                    
                    <div class="col-lg-12">
                     <div class="col-lg-1">
                       
                             <div id="Div7"   runat="server"><label>
                                   S.No</label>
                          <asp:TextBox ID="txtsno"  runat="server"  Text="1" CssClass="form-control" ></asp:TextBox>
                            </div>
                             </div><div class="col-lg-2">
                            <div id="Div4" class="form-group" runat="server"><label>
                                   Product</label>
                            <asp:DropDownList ID="drpProd" CssClass="chzn-select" OnSelectedIndexChanged="drpProd_selected"  AutoPostBack="true"
                                                                              Width="100%" runat="server" Height="30px" AppendDataBoundItems="true">
                                                                            </asp:DropDownList>
                            </div></div><div class="col-lg-2">
                            <div id="Div5" class="form-group" runat="server"><label>
                                    Stock</label>
                             <asp:TextBox ID="txtstock1" runat="server" Enabled="false" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                             </div></div><div class="col-lg-2">
                               <div id="Div6" class="form-group" runat="server"><label>
                                     Qty</label>
                             <asp:TextBox ID="txtQty1" OnTextChanged="txtrecqtychnaged_text" AutoPostBack="true" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                             </div></div><div class="col-lg-2">
                                   <div class="form-group" runat="server">
                             <asp:Button ID="Button1" runat="server" class="btn btn-info" Text="Add" 
                                 Style="width: 120px; margin-top: 25px" OnCLick="Add_Lot" />
                                </div>
                        </div>
                        <div class="col-lg-3">
                        <label>Image Preview</label><br />
                       <%-- <asp:Image ID="previewimage" runat="server" Width="100px" Height="60px"  />--%>
                        <asp:ImageButton ID="previewimage" runat="server" Width="100px" Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />

                        </div>
                    </div>
                    

                            <div class="col-lg-6">


                                        <div style="overflow:scroll; height:300px" >
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                    <div class="panel-body">
                                        <div >
                                            <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                            <table  id="Table1">
                                                <tr>
                                                    <td colspan="5">
                                                        <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                            OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                            CssClass="chzn-container"  Width="100%" runat="server">
                                                               <HeaderStyle BackColor="#59d3b4" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                                                            <Columns>
                                                              <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                  

                                                                <asp:TemplateField Visible="false" HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="drpProcess" Visible="false"
                                                                          Width="100%" runat="server" Height="26px" AppendDataBoundItems="true"></asp:DropDownList>
                                                                          

                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Product" ItemStyle-Width="70%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="txtprod"  Text="0"  style="display:none" runat="server" Enabled="false"  AppendDataBoundItems="true"></asp:TextBox>
                                                                       <asp:TextBox ID="txtprodname"  Text="0"  runat="server" Enabled="false" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>

                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               
                                                                 
                                                                <asp:TemplateField HeaderText=" Quantity" ItemStyle-Width="25%">
                                                                    <ItemTemplate>
                                                                    <asp:TextBox ID="txtstock" runat="server" style="display:none" Enabled="false" AppendDataBoundItems="true"></asp:TextBox>
                                                                        <asp:TextBox ID="txtRecQuantity" Enabled="false" runat="server" Width="100%" Height="26px" AppendDataBoundItems="true"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                             
                                                                
                                                              
                                                                 <asp:CommandField  ControlStyle-Width="50px" ShowDeleteButton="True" ButtonType="Button" />
                                                            </Columns>
                                                         
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                    
                    </div>

                    <div class="col-lg-12">

                      <div id="but" runat="server" class="row">
                    <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" 
                                ValidationGroup="val1" Style="width: 120px; margin-top: 25px" OnCLick="Add_LotProcessDetails" />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit"  PostBackUrl="LotRawGrid.aspx"
                                Style="width: 120px; margin-top: 25px" />
                    </div>

                    </div>
                    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
 <%--   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script> 
<script type="text/javascript" src="../Scripts/gridviewScroll.min.js"></script> 
    <script src="../Scripts/gridviewscroll.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=gvcustomerorder.ClientID%>').gridviewScroll({width: 800, height: 300 });
        } 
</script>--%>
    </ContentTemplate>
    </asp:UpdatePanel>
      <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                    right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                        AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                        position: fixed; top: 50%; left: 40%;" />
                    <%--<asp:Image ID="imgUpdateProgress1" runat="server" ImageUrl="../images/loading.gif" />--%>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>


            <!-- /.row (nested) -->
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <!-- /#page-wrapper -->
    <!-- jQuery -->
      <div id="divBackground" >
</div>
<div id="divImage">
<table style="height: 100%; width: 100%">
    <tr>
        <td valign="middle" align="center">
            <img id="imgLoader" alt="" src="images/loader.gif" />
            <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
        </td>
    </tr>
    <tr>
        <td align="center" valign="bottom">
            <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
        </td>
    </tr>
</table>
</div>   

                    </form>
                
</body>
</html>
