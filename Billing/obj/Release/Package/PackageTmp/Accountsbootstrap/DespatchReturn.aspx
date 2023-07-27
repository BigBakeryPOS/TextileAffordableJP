<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DespatchReturn.aspx.cs"
    Inherits="Billing.Accountsbootstrap.DespatchReturn" %>

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
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <title>Despatch Return</title>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
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
    <link rel="stylesheet" href="../Styles/chosen.css" />
    <style type="text/css">
        .GroupHeaderStyle
        {
            background-color: #afc3dd;
            color: Black;
            font-weight: bold;
            text-transform: uppercase;
        }
        .SubTotalRowStyle
        {
            background-color: #cccccc;
            color: Black;
            font-weight: bold;
        }
        .GrandTotalRowStyle
        {
            background-color: #000000;
            color: white;
            font-weight: bold;
        }
        .align1
        {
            text-align: right;
        }
        
        .myGridStyle1 tr th
        {
            padding: 8px;
            color: #afc3dd;
            background-color: #000000;
            border: 1px solid gray;
            font-family: Arial;
            font-weight: bold;
            text-align: center;
            text-transform: uppercase;
        }
        
        
        
        
        
        .myGridStyle1 tr:nth-child(even)
        {
            background-color: #ffffff;
        }
        
        
        
        .myGridStyle1 tr:nth-child(odd)
        {
            background-color: #ffffff;
        }
        
        
        
        .myGridStyle1 td
        {
            border: 1px solid gray;
            padding: 8px;
        }
    </style>
    <script type="text/javascript">
        function Search_Gridview(strKey, strGV) {


            var strData = strKey.value.toLowerCase().split(" ");

            var tblData = document.getElementById(strGV);

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)

                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }    
    </script>
    <script type="text/javascript">
        function Search_Gridviewlot(strKey, strGV) {


            var strData = strKey.value.toLowerCase().split(" ");

            var tblData = document.getElementById(strGV);

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)

                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }    
    </script>
    <script type="text/javascript">
        function SearchEmployees(txtSearch, cblEmployees) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html((count) + ' match');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
        }
    </script>
    <script type="text/javascript">
        function Denomination() {


            var gridData = document.getElementById('gridcatqty');


            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();


            var prtWindow = window.open(windowUrl, windowName,
           'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head></head>');
            prtWindow.document.write('<body style="background:none !important">');

            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();


        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <usc:Header ID="Header" runat="server" />
    <div class="row">
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-3">
                <h1 class="page-header">
                    Despatch Return</h1>
            </div>
            <div class="col-lg-2">
                <asp:Label ID="Label6" runat="server">DC.No</asp:Label>
                <asp:DropDownList ID="ddldcno" runat="server" class="chzn-select" AutoPostBack="true"
                    OnSelectedIndexChanged="ddldcno_OnSelectedIndexChanged" Width="200px">
                </asp:DropDownList>
            </div>
            <div class="col-lg-6">
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-2">
                                            <div class="form-group ">
                                                <asp:Label ID="Label5" runat="server">Branch</asp:Label><br />
                                                <asp:DropDownList ID="drpbranch" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpbranch_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Label ID="Label1" runat="server">Re.No</asp:Label>
                                            <asp:TextBox ID="txtdcno" runat="server" CssClass="form-control center-block" AutoPostBack="true"
                                                OnTextChanged="txtdcno_OnTextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                                            </asp:ScriptManager>
                                            <div class="form-group">
                                                <asp:Label ID="lblFromDate" runat="server">Re.Date</asp:Label>
                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control center-block" Width="100px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" PopupButtonID="txtFromDate"
                                                    EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label4" runat="server">Customer Name</asp:Label>
                                            <asp:DropDownList ID="ddlcustomer" runat="server" class="chzn-select" Enabled="false"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="lblsupplier" runat="server">Prepared By </asp:Label>
                                            <asp:DropDownList ID="ddlpacker" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label3" runat="server">Narration</asp:Label>
                                            <asp:TextBox ID="txtnarration" runat="server" TextMode="MultiLine" CssClass="form-control center-block"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label2" runat="server">Return Qty</asp:Label><br />
                                            <asp:Label ID="lblalltotalqty" runat="server" ForeColor="Green" Font-Bold="true"
                                                Font-Size="XX-Large"></asp:Label><br />
                                            <asp:Label ID="Label7" runat="server" visible="false">Despatch Amt</asp:Label ><br />
                                            <asp:Label ID="lblalltotalamt" runat="server" ForeColor="Green" Font-Bold="true" visible="false"
                                                Font-Size="XX-Large"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtsearching" runat="server" onkeyup="Search_Gridviewlot(this,'GVLotqty');"
                                                    placeholder="Search LotNo" CssClass="form-control" Width="200px"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-9">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-lg-10" runat="server" visible="false">
                                                <div style="overflow-y: scroll; height: 150px">
                                                    <asp:GridView ID="GVLotqty" Visible="true" Width="100%" runat="server" EmptyDataText="Sorry Data Not Found!"
                                                        CssClass="myGridStyle" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CHK" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkitemchecked" Text='<%# Eval("CompanyLotNo")%>' AutoPostBack="true"
                                                                        Width="100px" OnCheckedChanged="chkinvnochanged" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="GrandTotal" HeaderText="Total" />
                                                            <asp:BoundField DataField="S30F" HeaderText="30/F" />
                                                            <asp:BoundField DataField="S32F" HeaderText="32/F" />
                                                            <asp:BoundField DataField="S34F" HeaderText="34/F" />
                                                            <asp:BoundField DataField="S36F" HeaderText="36/F" />
                                                            <asp:BoundField DataField="XSF" HeaderText="XS/F" />
                                                            <asp:BoundField DataField="SF" HeaderText="S/F" />
                                                            <asp:BoundField DataField="MF" HeaderText="M/F" />
                                                            <asp:BoundField DataField="LF" HeaderText="L/F" />
                                                            <asp:BoundField DataField="XLF" HeaderText="XL/F" />
                                                            <asp:BoundField DataField="XXLF" HeaderText="XXL/F" />
                                                            <asp:BoundField DataField="S3XLF" HeaderText="3XL/F" />
                                                            <asp:BoundField DataField="S4XLF" HeaderText="4XL/F" />
                                                            <asp:BoundField DataField="S30H" HeaderText="30H" />
                                                            <asp:BoundField DataField="S32H" HeaderText="32H" />
                                                            <asp:BoundField DataField="S34H" HeaderText="34H" />
                                                            <asp:BoundField DataField="S36H" HeaderText="36H" />
                                                            <asp:BoundField DataField="XSH" HeaderText="XSH" />
                                                            <asp:BoundField DataField="SH" HeaderText="SH" />
                                                            <asp:BoundField DataField="MH" HeaderText="MH" />
                                                            <asp:BoundField DataField="LH" HeaderText="LH" />
                                                            <asp:BoundField DataField="XLH" HeaderText="XLH" />
                                                            <asp:BoundField DataField="XXLH" HeaderText="XXLH" />
                                                            <asp:BoundField DataField="S3XLH" HeaderText="3XLH" />
                                                            <asp:BoundField DataField="S4XLH" HeaderText="4XLH" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <div id="Div1" class="panel panel-default" runat="server" visible="false">
                                                        <label>
                                                            Fabric Register Number</label>
                                                        <asp:CheckBoxList ID="chkinvno" CssClass="chkChoice1" runat="server" RepeatColumns="5"
                                                            AutoPostBack="true" OnTextChanged="chkinvnochanged" RepeatDirection="Horizontal"
                                                            RepeatLayout="Table" Style="overflow: auto">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:GridView ID="gridcatqty" Visible="true" Width="100%" runat="server" EmptyDataText="Sorry Data Not Found!"
                                                    CssClass="mGrid" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="LotNo" HeaderText="LotNo" />
                                                        <asp:BoundField DataField="Total" HeaderText="Total" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:TextBox ID="TextBox1" runat="server" onkeyup="Search_Gridview(this, 'gvdeliverstock')"
                                        placeholder="Search Text" CssClass="form-control" Width="200px"></asp:TextBox>
                                    <asp:GridView ID="gvdeliverstock" Visible="true" Width="80%" runat="server" EmptyDataText="Sorry Data Not Found!"
                                        CssClass="myGridStyle" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField Visible="false" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFinishedStockRatioId" runat="server">0</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="true" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCutid" runat="server">0</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DesignCode" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignCode" runat="server" ForeColor="Green" Font-Bold="true">0</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Itemname" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemname" runat="server" ForeColor="Green" Font-Bold="true">0</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LotNo" HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompanyLotNo" runat="server" ForeColor="Green" Font-Bold="true">0</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="30FS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl30FS" runat="server" ForeColor="Green" Width="35px" Font-Bold="true">1000</asp:Label><br />
                                                    <asp:Label ID="lblr30f" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR30FS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtf30" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR30FS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="32FS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl32FS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr32f" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR32FS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtf32" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR32FS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="34FS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl34FS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr34f" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR34FS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtf34" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR34FS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="36FS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl36FS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr36f" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR36FS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtf36" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR36FS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="XSFS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblXSFS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrxsf" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRXSFS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtfxs" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRXSFS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SFS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSFS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrsf" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRSFS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtfs" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRSFS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MFS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMFS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrmf" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRMFS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtfm" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRMFS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LFS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLFS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrlf" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRLFS" runat="server" Height="26px" Width="50px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtfl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRLFS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="XLFS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblXLFS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrxlf" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRXLFS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtfxl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRXLFS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="XXLFS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblXXLFS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrxxlf" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRXXLFS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtfxxl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRXXLFS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="3XLFS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl3XLFS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr3xlf" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR3XLFS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtf3xl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR3XLFS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="4XLFS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl4XLFS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr4xlf" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR4XLFS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamtf4xl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR4XLFS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="30HS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl30HS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr30h" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR30HS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamth30" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR30HS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="32HS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl32HS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr32h" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR32HS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamth32" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR32HS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="34HS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl34HS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr34h" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR34HS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamth34" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR34HS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="36HS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl36HS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr36h" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR36HS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamth36" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR36HS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="XSHS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblXSHS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrxsh" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRXSHS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamthxs" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRXSHS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SHS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSHS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrsh" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRSHS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamths" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRSHS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MHS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMHS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrmh" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRMHS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamthm" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRMHS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LHS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLHS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrlh" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRLHS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamthl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRLHS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="XLHS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblXLHS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrxlh" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRXLHS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamthxl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRXLHS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="XXLHS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblXXLHS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblrxxlh" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtRXXLHS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamthxxl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtRXXLHS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="3XLHS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl3XLHS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr3xlh" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR3XLHS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamth3xl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR3XLHS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="4XLHS" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl4XLHS" runat="server" Width="35px" ForeColor="Green" Font-Bold="true">0</asp:Label><br />
                                                    <asp:Label ID="lblr4xlh" runat="server" ForeColor="Gray" Width="35px" Font-Bold="true">0</asp:Label>
                                                    <asp:TextBox ID="txtR4XLHS" runat="server" Width="50px" Height="26px">0</asp:TextBox>
                                                    <asp:Label ID="lblamth4xl" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server"
                                                        FilterType="Numbers" ValidChars="" TargetControlID="txtR4XLHS" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" HeaderStyle-Width="1%" ItemStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllotqty" runat="server" Width="50px" ForeColor="Red" Font-Bold="true">0</asp:Label>
                                                    <asp:Label ID="lbldespatchqty" runat="server" Width="50px" ForeColor="Red" Font-Bold="true">0</asp:Label>
                                                    <asp:Label ID="lblttlamt" runat="server" ForeColor="Brown" Width="35px" Font-Bold="true"
                                                        Visible="false">0</asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <table class="table table-bordered table-striped">
                                                    <tr>
                                                        <td id="Td1" runat="server" visible="true">
                                                            <%--<asp:GridView ID="gridcatqty" Visible="true" Width="100%" runat="server" EmptyDataText="Sorry Data Not Found!"
                                                                CssClass="mGrid" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="LotNo" HeaderText="LotNo" />
                                                                    <asp:BoundField DataField="Total" HeaderText="Total" />
                                                                </Columns>
                                                            </asp:GridView>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-lg-6">
                                            </div>
                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" Visible="true" CssClass="btn btn-success"
                                                    OnClick="btnSave_OnClick" Width="100px" />
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:Button ID="btnexit" runat="server" Text="Exit" Visible="true" CssClass="btn btn-danger"
                                                    PostBackUrl="~/Accountsbootstrap/DespatchReturnGrid.aspx" Width="100px" />
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:Button ID="btncal" runat="server" Text="Calc" Visible="true" CssClass="btn btn-group"
                                                    OnClick="btncal_OnClick" Width="100px" />
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
                            <script type="text/javascript">                                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
