<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GSTPortalReport.aspx.cs"
    Inherits="Billing.Accountsbootstrap.GSTPortalReport" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <title>GST Portal Report</title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
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
    <div class="row">
        <div class="col-lg-12" style="margin-top: 6px">
            <div class="col-lg-2">
                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 20px">
                    GST Portal Report</h1>
            </div>
            <div class="col-lg-2">
                <form id="Form1" runat="server" role="form">
                <div class="form-group">
                    <label>
                        From Date</label>
                    <asp:TextBox CssClass="form-control" ID="txtfrmdate" runat="server" Text="Select Date"></asp:TextBox>
                </div>
                <ajaxToolkit:CalendarExtender ID="txtfrmdate1" TargetControlID="txtfrmdate" Format="dd/MM/yyyy"
                    runat="server" CssClass="cal_Theme1">
                </ajaxToolkit:CalendarExtender>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </div>
            <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        To Date</label>
                    <asp:TextBox CssClass="form-control" ID="txttodate" runat="server" Text="Select Date"></asp:TextBox>
                </div>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txttodate"
                    Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                </ajaxToolkit:CalendarExtender>
            </div> 
             <div class="col-lg-2">
                <div class="form-group">
                    <label>
                        GSTNo</label>
                    <asp:TextBox CssClass="form-control" ID="txtGSTNo" runat="server" Enabled="false"></asp:TextBox>
                </div>               
            </div>  
             <div class="col-lg-2" runat="server" visible="false" id="year">
                <div class="form-group">
                    <label>
                        File Year</label>
                    <asp:TextBox CssClass="form-control" ID="txtFileYear" runat="server" placeholder="Ex: 032021"></asp:TextBox>
                </div>               
            </div>          
            <div class="col-lg-2" runat="server" visible="false" id="company" >
                <div class="form-group">
                    <label>
                        Select Company</label>
                    <asp:DropDownList ID="ddloutlet" runat="server" CssClass="form-control">
                        <%--  <asp:ListItem Text="CO1" Value="CO1"></asp:ListItem>
                        <asp:ListItem Text="CO2" Value="CO2"></asp:ListItem>
                        <asp:ListItem Text="CO3" Value="CO3"></asp:ListItem>
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <div class="row">
        <div class="col-lg-2">
        </div>
        <div class="col-lg-2">
            <div class="form-group">
                <asp:Button ID="btnreport" runat="server" class="btn btn-info" Text="Generate Excel"
                    Style="width: 160px;" OnClick="btnreport_Click" />
            </div>
        </div>
        <div class="col-lg-2" runat="server" visible="false" >
            <div class="form-group">
                <asp:Button ID="btnJSON" runat="server" class="btn btn-info" Text="Generate JSON"
                    Style="width: 160px;" OnClick="btnJSON_Click" />
            </div>
        </div>
        <div class="col-lg-2">
            <div class="form-group">
                <asp:Button ID="btnCSV" runat="server" class="btn btn-info" Text="Generate CSV"
                    Style="width: 160px;" OnClick="btnCSV_Click" />
            </div>
        </div>
         <div class="col-lg-2" runat="server" id="doc" visible="false" >
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="Button1" Style="width: 200px;" runat="server" CssClass="btn btn-success"
                    Text="Generate GSTR1 JSON" OnClick="btnUpload123_Click" />
                <asp:TextBox ID="txtDoc" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
            </div>
            <div class="col-lg-2" runat="server" visible="false" id="r3b" >
            <div class="form-group">
                <asp:Button ID="Button2" runat="server" class="btn btn-info" Text="Generate GSTR3B Json"
                    Style="width: 160px;" OnClick="btnGSTR3B_Click" />
            </div>
        </div>
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12" runat="server" visible="false">
            <div class="panel panel-default">
                <div align="right">
                    <asp:button id="btnprint" runat="server" cssclass="btn btn-block center-block" text="Print"
                        width="125px" onclientclick="javascript:CallPrint('bill');" xmlns:asp="#unknown" />
                </div>
                <div class="panel-body" id="bill">
                    <div class="row">
                        <div class="col-lg-12">
                            <div id="Div3">
                                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                <h2 align="center">
                                    <asp:Label ID="lblMessage" Style="color: Blue;" runat="server"></asp:Label></h2>
                                <div>
                                    <div class="row">
                                        <!-- /.col-lg-12 -->
                                    </div>
                                    <!-- /.row -->
                                    <!-- /.panel-body -->
                                </div>
                                <!-- /.panel -->
                            </div>
                            <!-- /.col-lg-12 -->
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </div>
            <!-- /#page-wrapper -->
        </div>
    </div>
    </div> </div> </div>
    <!-- jQuery -->
    </form>
</body>
</html>
