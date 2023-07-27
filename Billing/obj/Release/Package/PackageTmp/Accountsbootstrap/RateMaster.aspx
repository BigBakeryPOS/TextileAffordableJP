<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RateMaster.aspx.cs" Inherits="Billing.Accountsbootstrap.RateMaster" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Rate Master</title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <link href="../css/Header.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            ShowPopup();
            setTimeout(HidePopup, 2000);
        }

        function ShowPopup() {
            $find('modalpopup').show();
            //$get('Button1').click();
        }

        function HidePopup() {
            $find('modalpopup').hide();
            //$get('btnCancel').click();
        }
    </script>
    <style>
        .pagination-ys
        {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td
        {
            display: inline;
        }
        
        .pagination-ys table > tbody > tr > td > a, .pagination-ys table > tbody > tr > td > span
        {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            color: #dd4814;
            background-color: #ffffff;
            border: 1px solid #dddddd;
            margin-left: -1px;
        }
        
        .pagination-ys table > tbody > tr > td > span
        {
            position: relative;
            float: left;
            padding: 8px 12px;
            line-height: 1.42857143;
            text-decoration: none;
            margin-left: -1px;
            z-index: 2;
            color: #aea79f;
            background-color: #f5f5f5;
            border-color: #dddddd;
            cursor: default;
        }
        
        .pagination-ys table > tbody > tr > td:first-child > a, .pagination-ys table > tbody > tr > td:first-child > span
        {
            margin-left: 0;
            border-bottom-left-radius: 4px;
            border-top-left-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td:last-child > a, .pagination-ys table > tbody > tr > td:last-child > span
        {
            border-bottom-right-radius: 4px;
            border-top-right-radius: 4px;
        }
        
        .pagination-ys table > tbody > tr > td > a:hover, .pagination-ys table > tbody > tr > td > span:hover, .pagination-ys table > tbody > tr > td > a:focus, .pagination-ys table > tbody > tr > td > span:focus
        {
            color: #97310e;
            background-color: #eeeeee;
            border-color: #dddddd;
        }
        .style14
        {
            width: 14%;
        }
        .style15
        {
            width: 15%;
        }
        
        
        .myGridStyles
        {
            border-collapse: collapse;
            margin-left: 100px;
            border: 1px solid gray;
            overflow: hidden;
        }
    </style>
    <link href="../Styles/chosen.css" rel="Stylesheet" />
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
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
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
    <%--    <script type="text/javascript">
        $(document).ready(function () {

            //         Client Side Search (Autocomplete)
            //         Get the search Key from the TextBox
            //         Iterate through the 1st Column.
            //         td:nth-child(1) - Filters only the 1st Column
            //         If there is a match show the row [$(this).parent() gives the Row]
            //         Else hide the row [$(this).parent() gives the Row]

            $('#txtsearch').keyup(function (event) {
                var searchKey = $(this).val().toLowerCase();
                $("#gridview tr td:nth-child(1)").each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(searchKey) >= 0) {
                        $(this).parent().show();
                    }
                    else {
                        $(this).parent().hide();
                    }
                });
            });
        });

    </script>--%>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="col-lg-2">
                <h2 class="page-header" style="text-align: left; color: #fe0002; font-size: 20px">
                    Rate Master</h2>
            </div>
            <div class="col-lg-2">
            </div>
            <div class="col-lg-2">
                <asp:Label ID="Label2" runat="server" Style="color: Red"></asp:Label><br />
                <asp:TextBox CssClass="form-control" Enabled="true" onkeyup="Search_Gridview(this, 'gridview')"
                    ID="txtsearch" runat="server" placeholder="Enter Text to Search" Width="180px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                    ControlToValidate="txtsearch" ErrorMessage="Please enter your searching Data!"
                    Text="*" Style="color: White" />
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                    FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -.@"
                    TargetControlID="txtsearch" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-8">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div id="Div1" runat="server" style="height: 450px; overflow: scroll;width:100%">
                        <asp:GridView ID="gridview" runat="server" EmptyDataText="No Records Found" DataKeyNames="ID"
                            OnSelectedIndexChanged="gridview_SelectedIndexChanged" AutoGenerateColumns="false"
                            CssClass="myGridStyle">
                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="White" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField DataField="LotNo" HeaderText="LotNo" />
                                <asp:TemplateField HeaderText="CHK" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt1" Text='<%# Eval("LotNo")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CHK" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt2" Text='<%# Eval("LotNo")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CHK" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt3" Text='<%# Eval("LotNo")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CHK" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt5" Text='<%# Eval("LotNo")%>' runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="R30F" HeaderText="30/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R32F" HeaderText="32/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R34F" HeaderText="34/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R36F" HeaderText="36/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RXSF" HeaderText="XS/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RSF" HeaderText="S/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RMF" HeaderText="M/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RLF" HeaderText="L/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RXLF" HeaderText="XL/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RXXLF" HeaderText="XXL/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R3XLF" HeaderText="3XL/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R4XLF" HeaderText="4XL/F" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R30H" HeaderText="30/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R32H" HeaderText="32/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R34H" HeaderText="34/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R36H" HeaderText="36/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RXSH" HeaderText="XS/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RSH" HeaderText="S/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RMH" HeaderText="M/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RLH" HeaderText="L/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RXLH" HeaderText="XL/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="RXXLH" HeaderText="XXL/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R3XLH" HeaderText="3XL/H" DataFormatString="{0:f}" />
                                <asp:BoundField DataField="R4XLH" HeaderText="4XL/H" DataFormatString="{0:f}" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("ID") %>' CommandName="Select"
                                            runat="server">
                                            <asp:Image ID="imdedit" ImageUrl="~/images/pen-checkbox-128.png" runat="server" />
                                        </asp:LinkButton>
                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete" ItemStyle-Width="10%"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btndel" CommandArgument='<%#Eval("ID") %>' CommandName="Del"
                                            runat="server">
                                            <asp:Image ID="Image1" ImageUrl="~/images/DeleteIcon_btn.png" runat="server" />
                                            <asp:ImageButton ID="imgdisable" ImageUrl="~/images/delete.png" runat="server" Visible="false"
                                                Enabled="false" ToolTip="Not Allow To Delete" />
                                        </asp:LinkButton>
                                        <ajaxToolkit:ModalPopupExtender ID="lnkDelete_ModalPopupExtender" runat="server"
                                            CancelControlID="ButtonDeleteCancel" OkControlID="ButtonDeleleOkay" TargetControlID="btndel"
                                            PopupControlID="DivDeleteConfirmation" BackgroundCssClass="ModalPopupBG">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <ajaxToolkit:ConfirmButtonExtender ID="lnkDelete_ConfirmButtonExtender" runat="server"
                                            TargetControlID="btndel" Enabled="True" DisplayModalPopupID="lnkDelete_ModalPopupExtender">
                                        </ajaxToolkit:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4" style="margin-left: -14px; width: 470px">
            <div class="panel panel-default">
                <div class="panel-heading" style="background-color: #59d3b4; color: #333333; border-color: #06090c">
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblName" Text="Add Rate" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <table>
                        <tr>
                            <td style="width: 45%">
                                <div class="form-group">
                                    <label>
                                        Select LotNo</label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator029" ControlToValidate="ddllotno"
                                        ValidationGroup="val2" Text="*" ErrorMessage="Please Select IsActive!" Style="color: Red" />
                                    <asp:DropDownList ID="ddllotno" runat="server" class="chzn-select">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 45%">
                                <div class="form-group">
                                    <label>
                                        Prepared By</label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator03" ControlToValidate="ddlPreparedby"
                                        ValidationGroup="val2" Text="*" ErrorMessage="Please Select IsActive!" Style="color: Red" />
                                    <asp:DropDownList ID="ddlPreparedby" runat="server" class="chzn-select">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 10%">
                                <div class="form-group ">
                                    <label>
                                        Branch</label>
                                    <asp:DropDownList ID="drpbranch" Width="150px" runat="server" CssClass="chzn-select">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 55px">
                                <asp:Label ID="Label8" runat="server">30-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtf30" runat="server" TabIndex="7" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtf30" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label9" runat="server">32-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtf32" runat="server" TabIndex="8" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender01" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtf32" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label10" runat="server">34-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtf34" runat="server" TabIndex="9" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtf34" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label11" runat="server">36-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtf36" runat="server" TabIndex="10" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtf36" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label12" runat="server">XS-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtfxs" runat="server" TabIndex="11" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtfxs" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label13" runat="server">S-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtfs" runat="server" TabIndex="12" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtfs" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 55px">
                                <asp:Label ID="Label14" runat="server">M-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtfm" runat="server" TabIndex="13" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtfm" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label15" runat="server">L-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtfl" runat="server" TabIndex="14" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtfl" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label16" runat="server">XL-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtfxl" runat="server" TabIndex="15" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtfxl" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label17" runat="server">XXL-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtfxxl" runat="server" TabIndex="16" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtfxxl" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label18" runat="server">3XL-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtf3xl" runat="server" TabIndex="17" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtf3xl" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label19" runat="server">4XL-F</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtf4xl" runat="server" TabIndex="18" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtf4xl" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 55px;">
                                <asp:Label ID="Label20" runat="server">30-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txth30" runat="server" TabIndex="19" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txth30" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label21" runat="server">32-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txth32" runat="server" TabIndex="20" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txth32" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label22" runat="server">34-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txth34" runat="server" TabIndex="21" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txth34" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label23" runat="server">36-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txth36" runat="server" TabIndex="22" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txth36" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label24" runat="server">XS-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txthxs" runat="server" TabIndex="23" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txthxs" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label25" runat="server">S-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txths" runat="server" TabIndex="24" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txths" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 55px">
                                <asp:Label ID="Label26" runat="server">M-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txthm" runat="server" TabIndex="25" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txthm" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label27" runat="server">L-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txthl" runat="server" TabIndex="26" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txthl" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label28" runat="server">XL-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txthxl" runat="server" TabIndex="27" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txthxl" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label29" runat="server">XXL-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txthxxl" runat="server" TabIndex="28" Width="70px">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txthxxl" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label30" runat="server">3XL-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txth3xl" runat="server" Width="70px" TabIndex="29">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txth3xl" />
                            </td>
                            <td style="width: 55px">
                                <asp:Label ID="Label31" runat="server">4XL-H</asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txth4xl" runat="server" Width="70px" TabIndex="30">0</asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server"
                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txth4xl" />
                            </td>
                        </tr>
                    </table>
                    <div class="list-group">
                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val2"
                            ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="txtId" runat="server" Visible="false"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnSave" runat="server" class="btn btn-info" Text="Save" ValidationGroup="val2"
                            Style="width: 120px;" AccessKey="s" OnClick="btnSave_Click" />
                        <label>
                        </label>
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-warning" Text="Reset" Style="width: 120px;"
                            PostBackUrl="~/Accountsbootstrap/RateMaster.aspx" Visible="true"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
        runat="server">
        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Category List</div>
                <div class="TitlebarRight" onclick="$get('ButtonDeleteCancel').click();">
                </div>
            </div>
            <div class="popup_Body">
                <p>
                    Are you sure want to delete?
                </p>
            </div>
            <div class="popup_Buttons">
                <input id="ButtonDeleleOkay" type="button" value="Yes" />
                <input id="ButtonDeleteCancel" type="button" value="No" />
            </div>
        </div>
    </asp:Panel>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    </form>
</body>
</html>
