<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GodownOpeningStock.aspx.cs"
    Inherits="Billing.Accountsbootstrap.GodownOpeningStock" %>

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
    <title>Godown Stock Entry</title>
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
        function Denomination123() {


            var gridData = document.getElementById('GVFinalStock');


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
    <script type="text/javascript">
        function Denomination() {


            var gridData = document.getElementById('gvprint');


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
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <usc:Header ID="Header" runat="server" />
    <div class="row">
        <div class="col-lg-12" style="margin-top: 5px">
            <h1 class="page-header">
                Godown Stock Entry</h1>
        </div>
    </div>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <asp:Panel ID="pan" runat="server">
                    <div class="col-lg-12">
                        <div class="col-lg-2">
                            <div class="form-group ">
                                <asp:Label ID="Label1" runat="server">Branch</asp:Label><br />
                                <asp:DropDownList ID="drpbranch" Width="150px" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:Label ID="lbllotno" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div id="Div7" runat="server">
                                <asp:Label ID="Label2" runat="server">LotNo</asp:Label><br />
                                <asp:TextBox ID="txtlotno" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnTextChanged="Companylotchecked"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div id="Div10" class="form-group" runat="server">
                                <asp:Label ID="Label3" runat="server">Brand</asp:Label><br />
                                <asp:DropDownList ID="ddlbrand" CssClass="chzn-select" Width="100%" runat="server"
                                    Height="30px" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div id="Div2" class="form-group" runat="server">
                                <asp:Label ID="Label4" runat="server">Fit</asp:Label><br />
                                <asp:DropDownList ID="ddlfit" CssClass="chzn-select" Width="100%" runat="server"
                                    Height="30px" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div id="Div3" class="form-group" runat="server">
                                <asp:Label ID="Label7" runat="server"></asp:Label><br />
                                <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Add" Style="width: 120px;"
                                    OnClick="btnadd_OnCLick" TabIndex="31" />
                            </div>
                        </div>
                        <div id="Div1" class="col-lg-1" runat="server" visible="false">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <div class="form-group">
                                <asp:Label ID="lblFromDate" runat="server">Date</asp:Label><br />
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Width="100px"
                                    Enabled="false"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate"
                                    PopupButtonID="txtFromDate" EnabledOnClient="true" Format="dd/MM/yyyy" runat="server"
                                    CssClass="cal_Theme1">
                                </ajaxToolkit:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div id="Div5" class="form-group" runat="server">
                                <asp:Label ID="Label5" runat="server"></asp:Label><br />
                                <asp:Button ID="btnsave" runat="server" class="btn btn-success" Text="Save" Style="width: 100px;"
                                    OnClick="btnsave_OnCLick" />
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <asp:Label ID="Label6" runat="server"></asp:Label><br />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-danger" Text="Exit" Style="width: 100px;"
                                PostBackUrl="~/Accountsbootstrap/Home_Page.aspx" />
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-2">
                            <div id="Div4" class="form-group" runat="server">
                                <label>
                                    ItemName</label>
                                <asp:DropDownList ID="ddlItem" CssClass="chzn-select" Width="100%" runat="server"
                                    TabIndex="4" Height="30px" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div id="Div8" runat="server">
                                <label>
                                    Design.No</label>
                                <asp:TextBox ID="txtdesignno" runat="server" CssClass="form-control" TabIndex="5"
                                    MaxLength="3"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                    FilterType="Numbers" ValidChars="" TargetControlID="txtdesignno" />
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div id="Div9" runat="server">
                                <label>
                                    Color</label>
                                <asp:TextBox ID="txtcolor" runat="server" TabIndex="6" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <table>
                                <tr>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label8" runat="server">30/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtf30" runat="server" TabIndex="7" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtf30" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label9" runat="server">32/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtf32" runat="server" TabIndex="8" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtf32" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label10" runat="server">34/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtf34" runat="server" TabIndex="9" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtf34" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label11" runat="server">36/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtf36" runat="server" TabIndex="10" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtf36" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label12" runat="server">XS/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtfxs" runat="server" TabIndex="11" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtfxs" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label13" runat="server">S/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtfs" runat="server" TabIndex="12" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtfs" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label14" runat="server">M/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtfm" runat="server" TabIndex="13" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtfm" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label15" runat="server">L/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtfl" runat="server" TabIndex="14" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtfl" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label16" runat="server">XL/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtfxl" runat="server" TabIndex="15" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtfxl" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label17" runat="server">XXL/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtfxxl" runat="server" TabIndex="16" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtfxxl" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label18" runat="server">3XL/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtf3xl" runat="server" TabIndex="17" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtf3xl" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label19" runat="server">4XL/F</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txtf4xl" runat="server" TabIndex="18" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txtf4xl" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 55px;">
                                        <asp:Label ID="Label20" runat="server">30/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txth30" runat="server" TabIndex="19" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txth30" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label21" runat="server">32/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txth32" runat="server" TabIndex="20" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txth32" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label22" runat="server">34/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txth34" runat="server" TabIndex="21" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txth34" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label23" runat="server">36/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txth36" runat="server" TabIndex="22" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txth36" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label24" runat="server">XS/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txthxs" runat="server" TabIndex="23" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txthxs" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label25" runat="server">S/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txths" runat="server" TabIndex="24" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txths" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label26" runat="server">M/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txthm" runat="server" TabIndex="25" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txthm" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label27" runat="server">L/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txthl" runat="server" TabIndex="26" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txthl" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label28" runat="server">XL/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txthxl" runat="server" TabIndex="27" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txthxl" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label29" runat="server">XXL/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txthxxl" runat="server" TabIndex="28" Width="50px">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txthxxl" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label30" runat="server">3XL/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txth3xl" runat="server" Width="50px" TabIndex="29">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txth3xl" />
                                    </td>
                                    <td style="width: 55px">
                                        <asp:Label ID="Label31" runat="server">4XL/H</asp:Label>
                                        <asp:TextBox CssClass="form-control" ID="txth4xl" runat="server" Width="50px" TabIndex="30">0</asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server"
                                            FilterType="Numbers" ValidChars="" TargetControlID="txth4xl" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-1">
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-1">
            </div>
            <div class="col-lg-10">
                <div class="row">
                    <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                        OnRowDeleting="GridView2_RowDeleting" CssClass="chzn-container" Width="100%"
                        runat="server">
                        <HeaderStyle BackColor="#59d3b4" />
                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Desing No" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesignno" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemName" ControlStyle-Width="100%" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblitemname" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemName" ControlStyle-Width="100%" ItemStyle-Width="10%"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblitemname1" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemName" ControlStyle-Width="100%" ItemStyle-Width="10%"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblitemname2" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="30 FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts30fs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="32 FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts32fs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="34 FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts34fs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="36 FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts36fs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="XS FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsxsfs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtssfs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsmfs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="L FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtslfs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="XL FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsxlfs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="XXL FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsxxlfs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="3XL FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts3xlfs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="4XL FS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts4xlfs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="30 HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts30hs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="32 HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts32hs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="34 HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts34hs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="36 HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts36hs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="XS HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsxshs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsshs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsmhs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="L HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtslhs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="XL HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsxlhs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="XXL HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsxxlhs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="3XL HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts3xlhs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="4XL HS" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txts4xlhs" Width="100%" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" Width="100%" Enabled="false" runat="server" Height="26px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ControlStyle-Width="50px" ShowDeleteButton="True" ButtonType="Button" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-lg-1">
            </div>
        </div>
    </div>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
