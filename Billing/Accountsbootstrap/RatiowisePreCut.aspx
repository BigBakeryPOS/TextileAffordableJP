﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RatiowisePreCut.aspx.cs"
    Inherits="Billing.Accountsbootstrap.RatiowisePreCut" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Pre-Cutting Process</title>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <script src="" type="text/javascript"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link href="../css/responsive-tabs.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" href="../css/bootstrap.min.css" runat="server" rel="stylesheet" />
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
    <script src="../js/jquery.responsiveTabs.min.js" type="text/javascript"></script>
    <script src="../js/jquery-2.1.0.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("body").on("click", "#btnadd", function () {
            alert("Button was clicked.");
        });
    </script>
    <script type="text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        row.style.backgroundColor = "aqua";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            row.style.backgroundColor = "white";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

    </script>
    <script type="text/javascript">

        function MouseEvents(objRef, evt) {

            var checkbox = objRef.getElementsByTagName("input")[0];

            if (evt.type == "mouseover") {

                objRef.style.backgroundColor = "orange";

            }

            else {

                if (checkbox.checked) {

                    objRef.style.backgroundColor = "aqua";

                }

                else if (evt.type == "mouseout") {

                    if (objRef.rowIndex % 2 == 0) {

                        //Alternating Row Color

                        objRef.style.backgroundColor = "white";

                    }

                    else {

                        objRef.style.backgroundColor = "white";

                    }

                }

            }

        }

    </script>
    <script type="text/javascript">

        function Check_Click(objRef) {

            //Get the Row based on checkbox

            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {

                //If checked change color to Aqua

                row.style.backgroundColor = "aqua";

            }

            else {

                //If not checked change back to original color

                if (row.rowIndex % 2 == 0) {

                    //Alternating Row Color

                    row.style.backgroundColor = "White";

                }

                else {

                    row.style.backgroundColor = "white";

                }

            }



            //Get the reference of GridView

            var GridView = row.parentNode;



            //Get all input elements in Gridview

            var inputList = GridView.getElementsByTagName("input");



            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];



                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;



        }

    </script>
    <script type="text/javascript">
        function ClientSideClick(myButton) {
            // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // disable the button
                myButton.disabled = true;
                myButton.className = "btn-inactive";
                myButton.value = "processing...";
            }
            return true;
        }
    </script>

    <style>
        .chkChoice input {
            margin-left: -30px;
        }

        .chkChoice td {
            padding-left: 45px;
        }

        .chkChoice1 input {
            margin-left: -20px;
        }

        .chkChoice1 td {
            padding-left: 20px;
        }
    </style>
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <script type="text/javascript">
        function Search_Gridview(strKey, strGV) {
//alert('ok');
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
    <script src="../js/Searchjs.js" type="text/javascript"></script>
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <div class="row">
        <div class="col-lg-12">
            <h4 class="page-header" style="text-align: center; color: #fe0002;">Ratio-Wise Pre-Cutting Process</h4>
        </div>
    </div>
    <div class="row">
        <asp:Label runat="server" ID="lblEmployee" Text="7" ForeColor="White" CssClass="label"
            Visible="false"> </asp:Label>
        <form id="Form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <%-- <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
            <div id="SDiv13" runat="server" class="form-group " style="padding-left: 30px">
                <asp:Label ID="lblBodyAvgmeter" runat="server" Visible="false"></asp:Label>
                <asp:RadioButtonList ID="radbtnshirttype" runat="server" RepeatColumns="3" OnSelectedIndexChanged="Shirttype"
                    AutoPostBack="true">
                    <asp:ListItem Text="Body Type" Value="1" Selected="True"></asp:ListItem>
                    <%-- <asp:ListItem Text="Contrast Type" Value="2"></asp:ListItem>
                <asp:ListItem Text="Reverse Type" Value="3"></asp:ListItem>--%>
                </asp:RadioButtonList>
            </div>
            <%--    </ContentTemplate>
        </asp:UpdatePanel>--%>
            <div class="col-lg-12" style="padding-top: -60px">
                <div id="horizontalzTab" style="background-color: #D0D3D6; padding-left: 30px">
                    <ul>
                        <li><a href="#tab-1">Cutting Details</a></li>
                        <li><a href="#tab-2">Ratio-Wise Process</a></li>
                        <li><a href="#tab-3">Final Process</a></li>
                        <li><a href="#tab-4">Contrast Process</a></li>
                    </ul>
                    <div class="row" id="tab-2" style="background-color: #D0D3D6; padding-top: -50px">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="background-color: #D0D3D6;">
                                    <div class="row">
                                        <table runat="server" id="metertab" style="background-color: #ffb85f">
                                            <tr>
                                                <td id="Ttd1" runat="server" visible="false">
                                                    <label>
                                                        Design - Color</label>
                                                    <asp:DropDownList ID="dddldesign" Width="150px" runat="server" CssClass="form-control"
                                                        OnSelectedIndexChanged="dddldesignchanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td id="Td2" runat="server" visible="false">
                                                    <label>
                                                        Rate</label>
                                                    <asp:TextBox ID="txtDesignRate" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <label>
                                                        Available Mtr</label>
                                                    <asp:TextBox ID="txtAvailableMtr" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <label>
                                                        No. of Shirts</label>
                                                    <asp:TextBox ID="txtNoofShirts" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <label>
                                                        Issued Mtr</label>
                                                    <asp:TextBox ID="txtReqMtr" OnTextChanged="reqchanged" Enabled="false" AutoPostBack="true"
                                                        runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <label>
                                                        No. of Shirts</label>
                                                    <asp:TextBox ID="txtReqNoShirts" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td id="Td1" runat="server" visible="false">
                                                    <label>
                                                        Remaining Meter</label>
                                                    <asp:TextBox ID="Ntxtremmeter" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td id="Td3" runat="server" visible="false">
                                                    <label>
                                                        MAX. Shirts</label>
                                                    <asp:TextBox ID="txtextrashirt" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td id="Td4" runat="server" visible="false">
                                                    <label>
                                                        MIN. Shirts</label>
                                                    <asp:TextBox ID="txtminshirt" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td id="Td5" visible="false" runat="server">
                                                    <label>
                                                        Party</label><br />
                                                    <asp:RadioButton ID="rdSingle" runat="server" GroupName="rdPar" Text="Single" OnCheckedChanged="rdSingle_CheckedChanged"
                                                        AutoPostBack="true" /><br />
                                                    <asp:RadioButton ID="rdMultiple" runat="server" GroupName="rdPar" Text="Multiple"
                                                        OnCheckedChanged="rdMultiple_CheckedChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcatid" runat="server"></asp:Label>
                                                    <asp:Label ID="lblSubcatid" runat="server"></asp:Label>
                                                    <asp:Label ID="stockid" runat="server"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <asp:Button ID="Ratioallprocess" class="btn btn-danger" runat="server" Text="Process All Color Ratio"
                                        OnClick="Ratio_processall" />
                                    <asp:Button ID="processallratiosample" runat="server" class="btn btn-primary" Text="Check Once Given Ratio"
                                        OnClick="processall_sampleratio" />
                                    <div class="row" runat="server">
                                        <div class="col-lg-12">
                                            <div class="col-lg-9">
                                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Both" Height="300" Width="100%">
                                                    <asp:GridView ID="NewSizeRatioGrid" AutoGenerateColumns="False" ShowFooter="True" OnRowCommand="NewSizeRatioGrid_OnRowCommand"
                                                        CssClass="chzn-container" GridLines="Both" Width="100%" Height="25px" runat="server">
                                                        <HeaderStyle BackColor="#F9F9F9" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                        <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Item Name" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <asp:Label Enabled="false" ID="Label2" Text='<%# Eval("RowId")%>' runat="server"></asp:Label>
                                                                    <asp:Label Enabled="false" ID="Nlblitemname" Text='<%# Eval("ItemName")%>' runat="server"></asp:Label>
                                                                    <asp:Label ID="Nlbltransid" Visible="false" Text='<%# Eval("Itemid")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fit" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                HeaderStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Nlblfitname" Text='<%# Eval("Fitname")%>' runat="server" Enabled="false"></asp:Label>
                                                                    <asp:Label ID="Nlblfitid" Text='<%# Eval("Fitid")%>' runat="server" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Given Meter" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Nlblrequiredmeter" Text='<%# Eval("Givenmeter")%>' runat="server"
                                                                        Enabled="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Avg. Meter" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Nlblavgmeter" Text='<%# Eval("Avgmeter")%>' runat="server" Enabled="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Contrast" ControlStyle-Width="100%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtcontrast" runat="server" Height="26px">0</asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt30fs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt32fs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt34fs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt36fs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtxsfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtsfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="M " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtmfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="L " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtlfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtxlfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtxxlfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt3xlfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt4xlfs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt30hs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt32hs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt34hs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt36hs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtxshs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtshs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="M " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtmhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="L " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtlhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtxlhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxtxxlhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt3xlhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Ntxt4xlhs" runat="server" Height="26px">0</asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Process Meter Ratio" ControlStyle-Width="100%" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Nlblprocessmeterratio" runat="server" Enabled="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Process Shirts" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Nlblprocessshirt" runat="server" Enabled="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Shirts" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Nlbltotalshirt" Text='<%# Eval("Totalshirt")%>' runat="server" Enabled="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SizeWise Total Qty" ControlStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="NlblSizeshirt" Text='<%# Eval("TotalshirtSize")%>' runat="server" Enabled="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SetQty" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1px"
                                                                ItemStyle-Width="1px">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")  %>' />
                                                                    <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                                                        CommandName="Modify">
                                                                        <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" />
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-lg-3">
                                                <asp:Button ID="btnSubmitQty" runat="server" Text="Submit Qty" Width="100%" OnClick="btnSubmitQty_OnClick" />

                                                <asp:Label ID="ItemName" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="Itemid" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="Fitname" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="Fitid" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="Givenmeter" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="Avgmeter" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="Totalshirt" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="RowId" runat="server" Visible="false"></asp:Label>

                                                <asp:Panel ID="Panel5" runat="server" ScrollBars="Both" Height="300" Width="100%">
                                                    <asp:GridView ID="GVSizes" AutoGenerateColumns="False" GridLines="Both" runat="server" ShowFooter="true" Width="100%" Height="25px"
                                                        Caption="Assign Qty Details">
                                                        <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                                            Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size" HeaderStyle-Width="100px" ItemStyle-Font-Size="Large"
                                                                ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <%-- <asp:HiddenField ID="hdTransSizeId" runat="server" Value='<%#Eval("TransSizeId") %>' />--%>
                                                                    <asp:HiddenField ID="hdSize" runat="server" Value='<%#Eval("SizeId") %>' />
                                                                    <asp:Label ID="lblSize" Height="30px" Width="150px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IssueQty" HeaderStyle-Width="50px" ItemStyle-Font-Size="Large"
                                                                ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtIssueQty" Text='<%# Eval("Qty")%>' AutoComplete="off" Height="30px" Width="100%" runat="server"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3125" runat="server"
                                                                        TargetControlID="txtIssueQty" FilterType="Numbers" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                    </div>

                                    <asp:Button ID="ratioprocessall" runat="server" class="btn btn-success" Text="Final Process"
                                        OnClick="ProcessShirt" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row" id="tab-3" style="background-color: #D0D3D6; padding-top: -50px">
                        <div class="col-lg-12">
                            <div class="col-lg-9">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="background-color: #D0D3D6;">
                                            <asp:Panel ID="Panel4" runat="server" ScrollBars="Both" Height="300" Width="100%">
                                                <asp:GridView ID="RatioShirtProcess" AutoGenerateColumns="False" ShowFooter="True" OnRowCommand="RatioShirtProcess_OnRowCommand"
                                                    OnRowDataBound="RatioShirtProcess_OnDataBound" CssClass="chzn-container" GridLines="Both"
                                                    Width="100%" Height="25px" runat="server">
                                                    <HeaderStyle BackColor="#F9F9F9" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                        Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                    <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Item Name" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Label Enabled="false" ID="Nlblitemname" Text='<%# Eval("ItemName")%>' runat="server"></asp:Label>
                                                                <asp:Label ID="Nlbltransid" Visible="false" Text='<%# Eval("Itemid")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fit" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                            HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Nlblfitname" Text='<%# Eval("Fitname")%>' runat="server" Enabled="false"></asp:Label>
                                                                <asp:Label ID="Nlblfitid" Text='<%# Eval("Fitid")%>' runat="server" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Given Meter" ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Nlblrequiredmeter" Text='<%# Eval("Givenmeter")%>' runat="server"
                                                                    Enabled="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Avg. Kg/Gms" ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Nlblavgmeter" Text='<%# Eval("Avgmeter")%>' runat="server" Enabled="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt30fs" Text='<%# Eval("S30FS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt32fs" Text='<%# Eval("S32FS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt34fs" Text='<%# Eval("S34FS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt36fs" Text='<%# Eval("S36FS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtxsfs" Text='<%# Eval("SXSFS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtsfs" Text='<%# Eval("SSFS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="M " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtmfs" Text='<%# Eval("SMFS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="L " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtlfs" Text='<%# Eval("SLFS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtxlfs" Text='<%# Eval("SXLFS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtxxlfs" Text='<%# Eval("SXXLFS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt3xlfs" Text='<%# Eval("S3XLFS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt4xlfs" Text='<%# Eval("S4XLFS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt30hs" Text='<%# Eval("S30HS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt32hs" Text='<%# Eval("S32HS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt34hs" Text='<%# Eval("S34HS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt36hs" Text='<%# Eval("S36HS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtxshs" Text='<%# Eval("SXSHS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtshs" Text='<%# Eval("SSHS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="M " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtmhs" Text='<%# Eval("SMHS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="L " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtlhs" Text='<%# Eval("SLHS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtxlhs" Text='<%# Eval("SXLHS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxtxxlhs" Text='<%# Eval("SXXLHS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt3xlhs" Text='<%# Eval("S3XLHS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Ntxt4xlhs" Text='<%# Eval("S4XLHS")%>' runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SizeWise Total Qty" ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NlblSizeshirt" Text='<%# Eval("TotalshirtSize")%>' runat="server" Enabled="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual Kg/Gms" ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NlblActmeter" Text='<%# Eval("Actmeter")%>' runat="server" Enabled="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="EndBit" ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEndBit" Text='<%# Eval("EndBit","{0:n}")%>' runat="server" Enabled="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total T-Shirts" ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Nlbltotalshirt" Text='<%# Eval("Totalshirt")%>' runat="server" Enabled="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Required T-Shirts" ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Nlblreqshirts" Text='<%# Eval("Reqshirt")%>' runat="server" Enabled="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contrast" ControlStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtcontra" runat="server" Height="26px">0</asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ViewQty" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1px"
                                                            ItemStyle-Width="1px">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdRowId" runat="server" Value='<%#Eval("RowId")  %>' />
                                                                <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%#Eval("RowId") %>'
                                                                    CommandName="Modify">
                                                                    <asp:Image ID="img" runat="server" ImageUrl="~/images/pen-checkbox-128.png" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <asp:Button ID="btngohead" runat="server" class="btn btn-info" Text="GO Head" OnClick="GOheadprocessclick"
                                            ValidationGroup="val1" Style="width: 120px; margin-left: 1136px" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-lg-3">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="ItemNameSize" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="ItemidSize" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="FitnameSize" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="FitidSize" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="GivenmeterSize" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="AvgmeterSize" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="TotalshirtSize" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="RowIdSize" runat="server" Visible="false"></asp:Label>

                                        <asp:Panel ID="Panel6" runat="server" ScrollBars="Both" Height="300" Width="100%">
                                            <asp:GridView ID="RatioShirtProcessSizes" AutoGenerateColumns="False" GridLines="Both" runat="server" ShowFooter="true" Width="100%" Height="25px"
                                                Caption="View Qty Details">
                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"
                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" HeaderStyle-Width="100px" ItemStyle-Font-Size="Large"
                                                        ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <%-- <asp:HiddenField ID="hdTransSizeId" runat="server" Value='<%#Eval("TransSizeId") %>' />--%>
                                                            <asp:HiddenField ID="hdSize" runat="server" Value='<%#Eval("SizeId") %>' />
                                                            <asp:Label ID="lblSize" Height="30px" Width="150px" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IssueQty" HeaderStyle-Width="50px" ItemStyle-Font-Size="Large"
                                                        ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIssueQty" Text='<%# Eval("Qty")%>' AutoComplete="off" Height="30px" Width="100%" runat="server" Enabled="false"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3125" runat="server"
                                                                TargetControlID="txtIssueQty" FilterType="Numbers" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="tab-4" style="background-color: #D0D3D6; padding-top: -50px">
                        <div style="background-color: #D0D3D6;">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <table runat="server" id="contrastpart" class="table" style="background-color: #ffb85f">
                                            <tr align="center">
                                                <td colspan="3">
                                                    <asp:Label ID="headinglabel" Font-Bold="true" Font-Size="Larger" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="tr1" runat="server" style="width: 100%">
                                                <td>
                                                    <div>
                                                        <div class="panel panel-default" style="width: 100px">
                                                            <label>
                                                                Width</label>
                                                            <asp:CheckBoxList ID="contrastwidth" OnSelectedIndexChanged="contrastwidthselect"
                                                                CssClass="chkChoice1" AutoPostBack="true" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                                RepeatLayout="Table" Style="overflow: auto">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="overflow-y: scroll; width: 400px; height: 490px">
                                                        <asp:TextBox ID="txtfabcontrast" runat="server" onkeyup="SearchEmployees(this,'#contrastfabric');"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <div class="panel panel-default" style="width: 350px">
                                                            <label>
                                                                Select Fabric</label>
                                                            <asp:CheckBoxList ID="contrastfabric" OnSelectedIndexChanged="contfablist" CssClass="chkChoice1"
                                                                AutoPostBack="true" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                                RepeatLayout="Table" Style="overflow: auto">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="overflow-y: scroll; width: 300px; height: 490px">
                                                        <asp:TextBox ID="TextBox1" runat="server" onkeyup="SearchEmployees(this,'#chkitem');"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <div class="panel panel-default" style="width: 250px">
                                                            <label>
                                                                Select Item</label>
                                                            <asp:CheckBoxList ID="chkitem" OnSelectedIndexChanged="itemfablist" CssClass="chkChoice1"
                                                                AutoPostBack="true" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                                RepeatLayout="Table" Style="overflow: auto">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="overflow-y: scroll; width: 400px; height: 490px">
                                                        <div class="panel panel-default">
                                                            <asp:TextBox ID="txtcontrasttype" onkeyup="Search_Gridview(this, 'contrastgridfab')"
                                                                runat="server" CssClass="form-control" placeholder="Search Text" Style="width: 170px; margin-bottom: 26px;"></asp:TextBox>
                                                            <label>
                                                                Contrast Fabric List</label>
                                                            <div id="Div16" runat="server" style="overflow: scroll; height: 24pc">
                                                                <asp:GridView ID="contrastgridfab" AutoGenerateColumns="False" ShowFooter="True"
                                                                    CssClass="chzn-container" GridLines="None" Width="100%" runat="server">
                                                                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                        Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                    <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Fab Code" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                                            HeaderStyle-Width="2%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="newfabcode" Text='<%# Eval("Design")%>' runat="server"></asp:Label>
                                                                                <asp:Label ID="newfabid" Text='<%# Eval("id")%>' runat="server" Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbltype" Text='<%# Eval("type")%>' runat="server" Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblinvdate" Text='<%# Eval("invdate")%>' runat="server" Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Avl.Meter" Visible="true" HeaderStyle-Width="3%" ItemStyle-Width="3%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="newtxtAvlmeter" Enabled="false" Text='<%# Eval("AvaliableMeter","{0:n}")%>'
                                                                                    runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Req.Meter" ControlStyle-Width="100%" HeaderStyle-Width="3%"
                                                                            ItemStyle-Width="3%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="newtxtreqmeter" Width="50%" runat="server" Text='<%# Eval("AvaliableMeter","{0:n}")%>'
                                                                                    CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Avg. Meter" ControlStyle-Width="100%" HeaderStyle-Width="3%"
                                                                            ItemStyle-Width="3%">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtavgmetercontast" Width="50%" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <%-- --%>
                    </div>
                    <div class="row" id="tab-1" style="background-color: #D0D3D6; padding-top: -50px">
                        <div class="row">
                            <%--    <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>--%>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-lg-3">
                                        <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                            ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                        <div class="form-group" id="divcode" runat="server">
                                            <asp:TextBox CssClass="form-control" ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div id="DDiv2" runat="server" visible="false" class="form-group ">
                                            <label>
                                                ID</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator3"
                                                ControlToValidate="TextBox3" ErrorMessage="Please enter ID" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="TextBox3" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group ">
                                            <label>
                                                Branch</label>
                                            <asp:DropDownList ID="drpbranch" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="drpbranch_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div runat="server" visible="false" class="form-group ">
                                            <label>
                                                Fabric Issue DC No:</label>
                                            <%--   <asp:DropDownList ID="ddlBottilot" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlBottilot_OnSelectedIndexChanged">
                                </asp:DropDownList>--%>
                                            <asp:TextBox ID="TextBox2" onkeyup="Search_Gridview(this, 'ddlBottilot')" runat="server"
                                                CssClass="form-control" placeholder="Search DC" Style="width: 170px; margin-bottom: 26px;"></asp:TextBox>
                                            <div id="Div20" runat="server" style="overflow: scroll; height: 10pc">
                                                <asp:CheckBoxList ID="ddlBottilot" OnSelectedIndexChanged="ddlBottilot_OnSelectedIndexChanged"
                                                    CssClass="chkChoice1" AutoPostBack="true" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                                    RepeatLayout="Table" Style="overflow: auto">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="form-group ">
                                            <label>
                                                Type</label>
                                            <div class="form-group " runat="server" visible="false">
                                                <asp:RadioButtonList ID="drpsubtype" OnSelectedIndexChanged="drpsubtype_changed"
                                                    CssClass="chkChoice1" AutoPostBack="true" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="New" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Existing" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <asp:RadioButtonList ID="drpnewtype" OnSelectedIndexChanged="type_changed" CssClass="chkChoice1"
                                                AutoPostBack="true" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Pre-Cutting" Value="1" Selected="True"></asp:ListItem>
                                                <%--  <asp:ListItem Text="Job-Worker" Value="2"></asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group ">
                                            <label>
                                                Cutting/Job Work Master</label>
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpcutting" ValueToCompare="Select Cutting"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please select Cutting Master!"></asp:CompareValidator>
                                            <asp:DropDownList ID="drpcutting" OnSelectedIndexChanged="company_SelectedIndexChnaged"
                                                AutoPostBack="true" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div runat="server" visible="false" class="form-group ">
                                            <asp:RadioButtonList ID="rdncore" OnSelectedIndexChanged="rdncore_changed" CssClass="chkChoice1"
                                                AutoPostBack="true" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Core" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="UnCore" Value="2" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group ">
                                            <table>
                                                <tr>
                                                    <td colspan="4">
                                                        <label>
                                                            Cutting No: (COMPANY+CUTNo+VERSION+CORE)
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblcompany" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtcompanylot" Width="75px" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                            FilterType="Numbers" ValidChars="" TargetControlID="txtcompanylot" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtVersionLotNo" runat="server" CssClass="form-control" Width="75px"
                                                            MaxLength="2"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                            FilterType="Numbers" TargetControlID="txtVersionLotNo" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtcompanysublot" Width="75px" MaxLength="2" runat="server" CssClass="form-control"
                                                            Enabled="false"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                            FilterType="Numbers" TargetControlID="txtcompanysublot" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <div>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" visible="false">
                                            <div class="col-lg-6">
                                                <label>
                                                    Select Item :
                                                </label>
                                                <asp:DropDownList ID="drpitemtype" runat="server" CssClass="form-control" OnSelectedIndexChanged="Itemlotnumber_chnaged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-6">
                                                <label>
                                                    Item Lot No:
                                                </label>
                                                <br />
                                                <div>
                                                    <asp:Label ID="lblitemlotcode" Font-Bold="true" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtitemlotno" Enabled="false" Style="margin-left: 3pc; margin-top: -1pc; width: 71%;"
                                                        runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                        <div id="Div1" runat="server" visible="false" class="form-group ">
                                            <label>
                                                Lot No</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator1"
                                                ControlToValidate="txtLotNo" ErrorMessage="Please enter Meter" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="txtLotNo" MaxLength="6"
                                                runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Numbers" ValidChars="" TargetControlID="txtLotNo" />
                                        </div>
                                        <div class="form-group">
                                            <br />
                                            <label>
                                                Cutting Issue Date:</label>
                                            <asp:TextBox ID="txtdate" runat="server" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdate" runat="server"
                                                Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Receive Date:</label>
                                            <asp:TextBox ID="txtdeliverydate" runat="server" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdeliverydate"
                                                runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </div>
                                        <div class="form-group ">
                                            <label>
                                                Select Width</label>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drpwidth" ValueToCompare="Select Width"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please select Width!"></asp:CompareValidator>
                                            <asp:DropDownList ID="drpwidth" OnSelectedIndexChanged="drpwidthChange" AutoPostBack="true"
                                                runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div id="Div2" runat="server" class="form-group ">
                                            <div class="col-lg-6">
                                                <label>
                                                    Avg. Kg/Gms</label>
                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator2"
                                                    ControlToValidate="txtavgmeter" ErrorMessage="Please enter Sharp" Style="color: Red" />
                                                <asp:TextBox CssClass="form-control" Enabled="true" ID="txtavgmeter" MaxLength="6"
                                                    Width="100px" runat="server">0</asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Numbers,custom" ValidChars="." TargetControlID="txtavgmeter" />
                                            </div>
                                            <div class="col-lg-6">
                                                <label>
                                                    Roll/Taka</label>
                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator7"
                                                    ControlToValidate="txtrolltaka" ErrorMessage="Please enter Sharp" Style="color: Red" />
                                                <asp:TextBox CssClass="form-control" Enabled="true" ID="txtrolltaka" MaxLength="6"
                                                    Width="100px" runat="server">0</asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                    FilterType="Numbers,custom" ValidChars="." TargetControlID="txtrolltaka" />
                                            </div>
                                        </div>
                                        <div id="Div22" visible="false" runat="server" class="form-group ">
                                            <label>
                                                Comfort Fit</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator4"
                                                ControlToValidate="txtexec" ErrorMessage="Please enter Margin" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtexec" MaxLength="6" runat="server">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                FilterType="Numbers,custom" ValidChars="." TargetControlID="txtexec" />
                                        </div>
                                        <div id="SDiv3" runat="server" visible="false" class="form-group ">
                                            <label>
                                                Fit</label>
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlFit" ValueToCompare="Select Fit"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please select Fit!"></asp:CompareValidator>
                                            <asp:DropDownList ID="ddlFit" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div id="SDiv4" visible="false" runat="server" class="form-group ">
                                            <label>
                                                Production Cost</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator8"
                                                ControlToValidate="txtprod" ErrorMessage="Please enter Production Cost" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" Text="0" ID="txtprod" MaxLength="6" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                FilterType="Numbers,custom" ValidChars="." TargetControlID="txtprod" />
                                        </div>
                                        <div id="SDiv11" visible="false" runat="server" class="form-group ">
                                            <label>
                                                Margin</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator9"
                                                ControlToValidate="txtmargin" ErrorMessage="Please enter Margin" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtmargin" MaxLength="6" runat="server">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                FilterType="Numbers,custom" ValidChars="." TargetControlID="txtmargin" />
                                        </div>
                                        <div id="SDiv12" visible="false" runat="server" class="form-group ">
                                            <label>
                                                MRP</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator10"
                                                ControlToValidate="txtmrp" ErrorMessage="Please enter Margin" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtmrp" MaxLength="6" runat="server">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                FilterType="Numbers,custom" ValidChars="." TargetControlID="txtmrp" />
                                        </div>
                                        <div id="Div55" visible="false" runat="server" class="form-group ">
                                            <label>
                                                Adjustment Meter</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" ID="RequiredFieldValidator5"
                                                ControlToValidate="txtadjmeter" ErrorMessage="Please enter Margin" Style="color: Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtadjmeter" MaxLength="6" runat="server">0</asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                FilterType="Numbers,custom" ValidChars="." TargetControlID="txtadjmeter" />
                                        </div>
                                        <div id="Div77" visible="false" class="form-group " runat="server">
                                            <label>
                                                Min Meter:</label>
                                            <asp:Label ID="lblmin" runat="server"></asp:Label>
                                        </div>
                                        <div id="Div78" visible="false" class="form-group" runat="server">
                                            <label>
                                                Max Meter:</label>
                                            <asp:Label ID="lblmax" runat="server"></asp:Label>
                                        </div>
                                        <div id="Div6" visible="false" class="form-group " runat="server">
                                            <asp:RadioButtonList ID="radcuttype" RepeatColumns="2" OnSelectedIndexChanged="radcuttype_selectedindex"
                                                AutoPostBack="true" runat="server">
                                                <%--<asp:ListItem Text="single Cutting" Selected="True" Value="1"></asp:ListItem>--%>
                                                <asp:ListItem Text="Bulk Cutting" Selected="True" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div id="Div17" visible="true" class="form-group" runat="server">
                                            <label>
                                                Sample:</label>
                                            <asp:DropDownList ID="ddlsample" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Sample" Value="Sample"></asp:ListItem>
                                                <asp:ListItem Text="Issued" Value="Issued"></asp:ListItem>
                                                <asp:ListItem Text="NotIssued" Selected="True" Value="NotIssued"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div id="Div18" visible="true" class="form-group" runat="server">
                                            <label>
                                                Contrasts:</label>
                                            <asp:TextBox ID="txtcontrasts" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div id="Div7" style="padding-top: 2pc" runat="server">
                                            <div style="overflow-y: scroll; width: 312px; height: 490px">
                                                <asp:TextBox ID="txtsearching" runat="server" onkeyup="SearchEmployees(this,'#chkinvno');"
                                                    CssClass="form-control"></asp:TextBox>
                                                <div class="panel panel-default" style="width: 293px">
                                                    <label>
                                                        Fabric Register Number</label>
                                                    <asp:CheckBoxList ID="chkinvno" OnSelectedIndexChanged="chkinvnochanged" CssClass="chkChoice1"
                                                        AutoPostBack="true" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                        RepeatLayout="Table" Style="overflow: auto">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                                            <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                                        </div>
                                        <div id="Div10" visible="false" runat="server" class="col-lg-9">
                                            <div style="overflow-y: scroll; width: 315px; height: 244px">
                                                <div class="panel panel-default" style="width: 298px">
                                                    <label>
                                                        Design Code</label>
                                                    <%-- <asp:CheckBoxList ID="CheckBoxList1" OnSelectedIndexChanged="chkinvnochanged" AutoPostBack="true" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                        Width="100%" RepeatLayout="Table" Style="overflow: auto">--%>
                                                    <asp:CheckBoxList ID="CheckBoxList2" CssClass="chkChoice1" OnSelectedIndexChanged="check2_changed"
                                                        AutoPostBack="true" RepeatDirection="Horizontal" RepeatColumns="4" runat="server">
                                                    </asp:CheckBoxList>
                                                    <%--</asp:CheckBoxList>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div id="Div44" runat="server" visible="false" class="form-group ">
                                            <asp:RadioButtonList ID="radbtn" OnSelectedIndexChanged="radchecked" AutoPostBack="true"
                                                RepeatColumns="2" runat="server">
                                                <asp:ListItem Text="Single Party" Selected="True" Value="1"></asp:ListItem>
                                                <%-- <asp:ListItem Text="Multiple Party" Value="2"></asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div id="sing" runat="server">
                                            <div id="Div3" runat="server" visible="false" class="form-group ">
                                                <label>
                                                    Customer Name</label>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="val1"
                                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlSupplier"
                                                    ValueToCompare="Select Party Name" Operator="NotEqual" Type="String" ErrorMessage="Please select Party name!"></asp:CompareValidator>
                                                <asp:DropDownList ID="ddlSupplier" OnSelectedIndexChanged="supplierfill" AutoPostBack="true"
                                                    runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div runat="server" visible="false">
                                                <label>
                                                    Item Narration</label>
                                                <asp:TextBox ID="txtitemnarration" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div>
                                                <label>
                                                    Brand Name</label>
                                                <asp:CompareValidator ID="CompareValidator7" runat="server" ValidationGroup="val1"
                                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlbrand" ValueToCompare="Select Brand Name"
                                                    Operator="NotEqual" Type="String" ErrorMessage="Please select Brand name!"></asp:CompareValidator>
                                                <asp:DropDownList ID="ddlbrand" OnSelectedIndexChanged="brandindexchnaged" AutoPostBack="true"
                                                    runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <br />
                                            <div id="fitdiv" runat="server">
                                                <label>
                                                    Fit Label</label>
                                                <asp:DropDownList ID="drpNchkfit" class="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div id="Div21" runat="server" visible="false">
                                                <label>
                                                    Sleeve</label>
                                                <asp:DropDownList ID="drpnewsleevetype" class="form-control" runat="server">
                                                    <asp:ListItem Text="Half Sleeve" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Full Sleeve" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="3/4 Sleeve" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div id="Div23" runat="server" visible="false">
                                                <label>
                                                    Label</label>
                                                <asp:DropDownList ID="drpnewlabeltype" class="form-control" runat="server">
                                                    <asp:ListItem Text="Half (Blue)" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Full (Blue)" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Half (Pink)" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Full (Pink)" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <br />
                                            <div id="Div19" runat="server" visible="false">
                                                <label>
                                                    Complete Stitching</label>
                                                <asp:DropDownList ID="ddlcompletestitching" class="form-control" runat="server" Width="150px">
                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <br />

                                            <div style="width: 284px; height: 190px" runat="server" visible="true">
                                                <div class="panel panel-default" style="width: 265px">
                                                    <asp:GridView ID="grdmaster" AutoGenerateColumns="False" BorderWidth="1px" BorderStyle="Solid"
                                                        GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="myGridStyle">
                                                        <RowStyle CssClass="dataRow" />
                                                        <SelectedRowStyle CssClass="SelectdataRow" />
                                                        <AlternatingRowStyle CssClass="altRow" />
                                                        <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                        <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                        <FooterStyle CssClass="dataRow" />
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDebtorID" runat="server" Text='<%# Eval("processid")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Process" HeaderText="Process" ReadOnly="true" ApplyFormatInEditMode="false"
                                                                HeaderStyle-BorderColor="Gray" HeaderStyle-Wrap="false" />
                                                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="InHouse"
                                                                HeaderStyle-BorderColor="Gray">
                                                                <ItemTemplate>
                                                                    <asp:RadioButton ID="chkInHouse" GroupName="chk" runat="server" Style="color: Black" Text="" Font-Names="arial"
                                                                        Font-Size="11px"></asp:RadioButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Jobwork"
                                                                HeaderStyle-BorderColor="Gray">
                                                                <ItemTemplate>
                                                                    <asp:RadioButton ID="chkJobwork" GroupName="chk" runat="server" Style="color: Black" Text="" Font-Names="arial"
                                                                        Font-Size="11px"></asp:RadioButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div style="width: 284px; height: 190px" runat="server" visible="false">
                                                <div class="panel panel-default" style="width: 265px">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Process</label>
                                                            </td>
                                                            <td>
                                                                <label style="margin-left: 2.5pc">
                                                                    JP</label>
                                                                <label style="margin-left: 2.5pc">
                                                                    JobWork</label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Embroiding</label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="Nchkemb" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    CssClass="chkChoice" runat="server">
                                                                    <asp:ListItem Text="" Value="In"></asp:ListItem>
                                                                    <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Stitching</label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="Nchkstch" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    CssClass="chkChoice" runat="server">
                                                                    <asp:ListItem Text="" Value="In"></asp:ListItem>
                                                                    <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    K.Button</label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="Nchkkbut" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    CssClass="chkChoice" runat="server">
                                                                    <asp:ListItem Text="" Value="In"></asp:ListItem>
                                                                    <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Washing</label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="Nchkwash" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    CssClass="chkChoice" runat="server">
                                                                    <asp:ListItem Text="" Value="In"></asp:ListItem>
                                                                    <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Printing</label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="Nchkprint" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    CssClass="chkChoice" runat="server">
                                                                    <asp:ListItem Text="" Value="In"></asp:ListItem>
                                                                    <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Ironing</label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="Nchkiron" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    CssClass="chkChoice" runat="server">
                                                                    <asp:ListItem Text="" Value="In"></asp:ListItem>
                                                                    <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Bar Tag</label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="Nchkbartag" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    CssClass="chkChoice" runat="server">
                                                                    <asp:ListItem Text="" Value="In"></asp:ListItem>
                                                                    <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Trimming</label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="Nchktrimming" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    CssClass="chkChoice" runat="server">
                                                                    <asp:ListItem Text="" Value="In"></asp:ListItem>
                                                                    <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Consai</label>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="Nchkconsai" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    CssClass="chkChoice" runat="server">
                                                                    <asp:ListItem Text="" Value="In"></asp:ListItem>
                                                                    <asp:ListItem Text="" Value="Out"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnclear" runat="server" OnClick="btnclear_OnClick" Text="Clear" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>

                                            </div>
                                        </div>
                                        <div id="Div4" runat="server" visible="false" class="form-group ">
                                            <label>
                                                Main Label</label>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="val1"
                                                Text="*" Style="color: Red" InitialValue="0" ControlToValidate="drplab" ValueToCompare="Select Label"
                                                Operator="NotEqual" Type="String" ErrorMessage="Please select Label name!"></asp:CompareValidator>
                                            <asp:DropDownList ID="drplab" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div id="Div5" runat="server" visible="false" class="form-group ">
                                            <label>
                                                Fit Label</label>
                                            <asp:CheckBox ID="chkfit" runat="server" />
                                        </div>
                                        <div id="SDiv6" runat="server" visible="false" class="form-group ">
                                            <label>
                                                Wash Care Label</label>
                                            <asp:CheckBox ID="Chkwash" runat="server" />
                                        </div>
                                        <div id="sDiv7" runat="server" visible="false" class="form-group ">
                                            <label>
                                                Logo Embrodiery</label>
                                            <asp:CheckBox ID="Chllogo" runat="server" />
                                        </div>
                                        <div id="Div8" runat="server" visible="false" class="form-group ">
                                            <label>
                                                Margin</label>
                                            <asp:TextBox ID="Stxtmargin" Width="20%" runat="server">0</asp:TextBox>
                                        </div>
                                    </div>
                                    <div id="mul" runat="server" visible="false">
                                        <div id="Div9" runat="server" class="col-lg-5">
                                            <div style="overflow-y: scroll; width: 290px; height: 170px">
                                                <div class="panel panel-default" style="width: 272px">
                                                    <label>
                                                        Select Customer</label>
                                                    <asp:CheckBoxList ID="chkcust" OnSelectedIndexChanged="chkgridview" AutoPostBack="true"
                                                        CssClass="chkChoice1" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                        Width="100%" RepeatLayout="Table" Style="overflow: auto">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="SDiv10" runat="server" visible="false" class="form-group">
                                            <asp:GridView ID="grdcust" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="Grdcust_RowDataBound"
                                                CssClass="chzn-container" GridLines="None" Width="100%" runat="server">
                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cust. name" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcust" Enabled="false" Text='<%# Eval("Ledgername")%>' runat="server"></asp:TextBox>
                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Main Label" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drrplab" CssClass="chzn-select" runat="server" Height="26px"
                                                                Width="100%">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Mchkfit" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Wash" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Mchkwash" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Logo" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Mchklogo" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Margin" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtmargin" Height="30px" runat="server">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fit" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddrrpfit" CssClass="chzn-select" runat="server" Height="26px"
                                                                Width="100%">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                    <div class="col-lg-3">
                                        <div class="col-lg-12">
                                            <div class="col-lg-6" id="sizediv" runat="server" visible="false" style="margin-left: -3pc;">
                                                <div class="panel panel-default" style="width: 170px">
                                                    <label>
                                                        Size</label>
                                                    <asp:CheckBoxList ID="chkSizes" OnSelectedIndexChanged="ckhsize_index" AutoPostBack="true"
                                                        RepeatDirection="Horizontal" RepeatColumns="2" CssClass="chkChoice1" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="Div11" runat="server" visible="false" class="col-lg-6">
                                            <div class="form-group">
                                                <label style="margin-left: -15px">
                                                    Remaining Meters</label>
                                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtremameter" runat="server"
                                                    MaxLength="4" Style="text-align: right; width: 140px;">0</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                        </div>

                                        <div class="col-lg-12">
                                            <%--<asp:UpdatePanel ID="upitem" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>--%>
                                            <asp:TextBox ID="txtAutoName" onkeyup="Search_Gridview(this, 'newgridfablist')" runat="server"
                                                CssClass="form-control" placeholder="Search Text" Style="width: 170px; margin-bottom: 26px;"></asp:TextBox>
                                            <label>
                                                Fabric List</label>
                                            <div id="Div12" runat="server" style="overflow: scroll; height: 19pc">
                                                <asp:GridView ID="newgridfablist" AutoGenerateColumns="False" OnRowDataBound="RowDataBound"
                                                    ShowFooter="True" CssClass="chzn-container" GridLines="None" Width="100%" runat="server">
                                                    <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                        Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                    <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Fab Code" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                            HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="newfabcode" Text='<%# Eval("Design")%>' runat="server"></asp:Label>
                                                                <asp:Label ID="newfabid" Text='<%# Eval("id")%>' runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lbltype" Text='<%# Eval("type")%>' runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblinvdate" Text='<%# Eval("date")%>' runat="server" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Avl. Meter" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="newtxtAvlmeter" Enabled="false" Text='<%# Eval("Qty","{0:n}")%>'
                                                                    runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Req. Meter" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="newtxtreqmeter" runat="server" Text='<%# Eval("Qty","{0:n}")%>'
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CHK" ItemStyle-Width="3%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitemchecked" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="Div13" runat="server">
                                                <asp:Button ID="Newbtnclick" runat="server" class="btn btn-success" OnClick="newfabclick"
                                                    Text="Process" />
                                            </div>
                                            <%--  </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                        <div id="Div14" runat="server" class="col-lg-6">
                                        </div>
                                        <div id="Div15" runat="server" visible="false" class="col-lg-6">
                                            <div class="form-group">
                                                <label>
                                                    Remaining Shirts</label>
                                                <asp:TextBox CssClass="form-control" Enabled="false" ID="txtremashirt" OnTextChanged="remainingshirt"
                                                    AutoPostBack="true" runat="server" MaxLength="8" Style="text-align: right; width: 140px;">0</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                               
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <script src="../js/jquery.responsiveTabs.js" type="text/javascript"></script>
                        <script type="text/javascript">
                        $(document).ready(function () {
                            var $tabs = $('#horizontalzTab');

                            $tabs.responsiveTabs({
                                rotate: false,
                                startCollapsed: 'accordion',
                                collapsible: 'accordion',
                                setHash: true,

                                activate: function (e, tab) {
                                    $('.info').html('Tab <strong>' + tab.id + '</strong> activated!');
                                },
                                activateState: function (e, state) {
                                    //console.log(state);
                                    $('.info').html('Switched from <strong>' + state.oldState + '</strong> state to <strong>' + state.newState + '</strong> state!');
                                }
                            });

                            /* $('#start-rotation').on('click', function () {
                            $tabs.responsiveTabs('startRotation', 1000);
                            });
                            $('#stop-rotation').on('click', function () {
                            $tabs.responsiveTabs('stopRotation');
                            });
                            $('#start-rotation').on('click', function () {
                            $tabs.responsiveTabs('active');
                            });
                            $('#enable-tab').on('click', function () {
                            $tabs.responsiveTabs('enable', 3);
                            });
                            $('#disable-tab').on('click', function () {
                            $tabs.responsiveTabs('disable', 3);
                            });
                            $('.select-tab').on('click', function () {
                            $tabs.responsiveTabs('activate', $(this).val()); */

                        });
            
        
                        </script>
                    </div>

                    <table class="table table-striped table-bordered table-hover" style="background-color: #ffb85f">
                        <tr id="tr4" runat="server">
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div id="Div66" runat="server" align="right">
                                            <asp:Button ID="btnavgsize" runat="server" Text="calc." OnClick="callcclick" />
                                        </div>
                                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="200" Width="100%">
                                            <asp:GridView ID="gridsize" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gridsize_RowDataBound"
                                                OnRowDeleting="gridsize_RowDeleting" CssClass="chzn-container" GridLines="None"
                                                Width="100%" Height="25px" runat="server">
                                                <HeaderStyle BackColor="#F9F9F9" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Party Name" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddrparty" OnSelectedIndexChanged="ddrpartyselected_changed"
                                                                AutoPostBack="true" runat="server" CssClass="chzn-select">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="6%"
                                                        HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddrpfit" Enabled="false" runat="server" CssClass="chzn-select">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="36 FS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxttsfs" OnTextChanged="change36fs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="38 FS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxttefs" OnTextChanged="change38fs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="39 FS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxttnfs" OnTextChanged="change39fs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="40 FS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxtfzfs" OnTextChanged="change40fs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="42 FS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxtftfs" OnTextChanged="change42fs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="44 FS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxtfffs" OnTextChanged="change44fs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="36 HS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxttshs" OnTextChanged="change36hs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="38 HS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxttehs" OnTextChanged="change38hs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="39 HS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxttnhs" OnTextChanged="change39hs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="40 HS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxtfzhs" OnTextChanged="change40hs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="42 HS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxtfths" OnTextChanged="change42hs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="44 HS" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxtffhs" OnTextChanged="change44hs" AutoPostBack="true" runat="server"
                                                                Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WSP" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxtwsp" runat="server" Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Req.Meter" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="dtxtreqmeter" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Avg.Size" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="avgsize" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Shirts" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdRowId" runat="server" />
                                                            <asp:TextBox ID="dtxtshirt" Enabled="false" runat="server" Height="26px">0</asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add" ControlStyle-Width="100%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="dbtnadd" ImageUrl="~/images/edit.png" runat="server" OnClick="ButtonAdd1_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField Visible="false" ShowDeleteButton="True" ButtonType="Button" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <tr id="tr2" visible="false" runat="server">
                                    <td id="Td6" runat="server" style="width: 8%">
                                        <label>
                                            Item Name</label>
                                        <%-- <asp:DropDownList ID="drpCustomer" runat="server" Enabled="false" Width="150px" CssClass="form-control">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <asp:TextBox ID="txtitemname" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td id="Td7" runat="server" style="width: 8%">
                                        <label>
                                            Fit</label>
                                        <asp:DropDownList ID="drpFit" OnSelectedIndexChanged="drpfitchanged" AutoPostBack="true"
                                            runat="server" CssClass="form-control" Width="100%">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td id="Td8" runat="server" style="width: 8%">
                                        <label>
                                            Pattern Type</label>
                                        <asp:DropDownList ID="drppattern" runat="server" CssClass="form-control" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td id="Td9" runat="server" style="width: 5%">
                                        <label>
                                            Aval.Meter</label>
                                        <asp:TextBox ID="txtavamet1" runat="server" OnTextChanged="remainingmeter_chnaged"
                                            AutoPostBack="true" Width="100%" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="S30fs" runat="server">
                                        <label>
                                            30 FS</label>
                                        <asp:TextBox ID="Btxt30fs" OnTextChanged="Schange30fs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="S32fs" runat="server">
                                        <label>
                                            32 FS</label>
                                        <asp:TextBox ID="Btxt32fs" OnTextChanged="Schange32fs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="S34fs" runat="server">
                                        <label>
                                            34 FS</label>
                                        <asp:TextBox ID="Btxt34fs" OnTextChanged="Schange34fs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="S36fs" runat="server">
                                        <label>
                                            36 FS</label>
                                        <asp:TextBox ID="Btxt36fs" OnTextChanged="NSchange36fs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="Xsfs" runat="server">
                                        <label>
                                            XS FS</label>
                                        <asp:TextBox ID="Btxtxsfs" OnTextChanged="SchangeXSfs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="sfs" runat="server">
                                        <label>
                                            S FS</label>
                                        <asp:TextBox ID="txtsfs" OnTextChanged="SchangeSfs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="mfs" runat="server">
                                        <label>
                                            M FS</label>
                                        <asp:TextBox ID="txtmfs" OnTextChanged="SchangeMfs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="lfs" runat="server">
                                        <label>
                                            L FS</label>
                                        <asp:TextBox ID="txtlfs" OnTextChanged="SchangeLfs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="xlfs" runat="server">
                                        <label>
                                            XL FS</label>
                                        <asp:TextBox ID="txtxlfs" OnTextChanged="SchangeXLfs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="xxlfs" runat="server">
                                        <label>
                                            XXL FS</label>
                                        <asp:TextBox ID="txtxxlfs" OnTextChanged="SchangeXXLfs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="xxxlfs" runat="server">
                                        <label>
                                            3XL FS</label>
                                        <asp:TextBox ID="txtxxxlfs" OnTextChanged="SchangeXXXLfs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="xxxxlfs" runat="server">
                                        <label>
                                            4XL FS</label>
                                        <asp:TextBox ID="txtxxxxlfs" OnTextChanged="SchangeXXXXLfs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="S30hs" runat="server">
                                        <label>
                                            30 HS</label>
                                        <asp:TextBox ID="Btxt30hs" OnTextChanged="Schange30hs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="S32hs" runat="server">
                                        <label>
                                            32 HS</label>
                                        <asp:TextBox ID="Btxt32hs" OnTextChanged="Schange32hs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="S34hs" runat="server">
                                        <label>
                                            34 HS</label>
                                        <asp:TextBox ID="Btxt34hs" OnTextChanged="Schange34hs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="S36hs" runat="server">
                                        <label>
                                            36 HS</label>
                                        <asp:TextBox ID="Btxt36hs" OnTextChanged="NSchange36hs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="Xshs" runat="server">
                                        <label>
                                            XS HS</label>
                                        <asp:TextBox ID="Btxtxshs" OnTextChanged="SchangeXShs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="shs" runat="server">
                                        <label>
                                            S HS</label>
                                        <asp:TextBox ID="txtshs" OnTextChanged="SchangeShs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="mhs" runat="server">
                                        <label>
                                            M HS</label>
                                        <asp:TextBox ID="txtmhs" OnTextChanged="SchangeMhs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="lhs" runat="server">
                                        <label>
                                            L HS</label>
                                        <asp:TextBox ID="txtlhs" OnTextChanged="SchangeLhs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="xlhs" runat="server">
                                        <label>
                                            XL HS</label>
                                        <asp:TextBox ID="txtxlhs" OnTextChanged="SchangeXLhs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="xxlhs" runat="server">
                                        <label>
                                            XXL HS</label>
                                        <asp:TextBox ID="txtxxlhs" OnTextChanged="SchangeXXLhs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="xxxlhs" runat="server">
                                        <label>
                                            3XL HS</label>
                                        <asp:TextBox ID="txtxxxlhs" OnTextChanged="SchangeXXXLhs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="xxxxlhs" runat="server">
                                        <label>
                                            4XL HS</label>
                                        <asp:TextBox ID="txtxxxxlhs" OnTextChanged="SchangeXXXXLhs" AutoPostBack="true" runat="server"
                                            CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="Td10" runat="server" style="width: 5%">
                                        <label>
                                            Act.Meter</label>
                                        <asp:TextBox ID="txtactualmet" Width="100%" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="Td11" runat="server" style="width: 5%">
                                        <label>
                                            Act.Shirt</label>
                                        <asp:TextBox ID="Ntxtactshirt" Width="100%" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="Td12" runat="server" visible="false">
                                        <label>
                                            WSP</label>
                                        <asp:TextBox ID="Stxtwsp" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="Td13" runat="server" style="width: 5%">
                                        <label>
                                            Tot.Shirts</label>
                                        <asp:TextBox ID="txttotshirt1" Enabled="false" Width="100%" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="Td14" runat="server" style="width: 5%">
                                        <label>
                                            Avg.Size</label>
                                        <asp:TextBox ID="txtavvgmeter" Enabled="false" Width="100%" runat="server" CssClass="form-control">0</asp:TextBox>
                                    </td>
                                    <td id="addsingle" runat="server">
                                        <label>
                                            Add</label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="Addfirst" CssClass="img-responsive"
                                            ImageUrl="~/images/edit_add.png" EnableViewState="true" />
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr id="Trr1" visible="false" runat="server">
                            <td>
                                <label>
                                    Party Name</label>
                                <asp:DropDownList ID="drpCustomer2" runat="server" Width="150px" Enabled="false"
                                    CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <label>
                                    Fit</label>
                                <asp:DropDownList ID="drpFit2" runat="server" Enabled="false" CssClass="form-control"
                                    Width="150px">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <label>
                                    36 FS</label>
                                <asp:TextBox ID="txt36FS2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    38 FS</label>
                                <asp:TextBox ID="txt38FS2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    40 FS</label>
                                <asp:TextBox ID="txt40FS2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    42 FS</label>
                                <asp:TextBox ID="txt42FS2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    36 HS</label>
                                <asp:TextBox ID="txt36HS2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    38 HS</label>
                                <asp:TextBox ID="txt38HS2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    40 HS</label>
                                <asp:TextBox ID="txt40HS2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    42 HS</label>
                                <asp:TextBox ID="txt42HS2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    Aval.Meter</label>
                                <asp:TextBox ID="txtavamet2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    Tot.Shirts</label>
                                <asp:TextBox ID="txttotshirt2" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    Add</label>
                                <asp:ImageButton ID="ImageButton2" runat="server" OnClick="Addsecond" CssClass="img-responsive"
                                    ImageUrl="~/images/edit_add.png" EnableViewState="true" />
                            </td>
                        </tr>
                        <tr id="Tr3" visible="false" runat="server">
                            <td>
                                <label>
                                    Party Name</label>
                                <asp:DropDownList ID="drpCustomer3" runat="server" Width="150px" CssClass="form-control">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <label>
                                    Fit</label>
                                <asp:DropDownList ID="drpFit3" runat="server" CssClass="form-control" Width="150px">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <label>
                                    36 FS</label>
                                <asp:TextBox ID="txt36FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    38 FS</label>
                                <asp:TextBox ID="txt38FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    39 FS</label>
                                <asp:TextBox ID="txt39FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    40 FS</label>
                                <asp:TextBox ID="txt40FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    42 FS</label>
                                <asp:TextBox ID="txt42FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    44 FS</label>
                                <asp:TextBox ID="txt44FS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    36 HS</label>
                                <asp:TextBox ID="txt36HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    38 HS</label>
                                <asp:TextBox ID="txt38HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    39 HS</label>
                                <asp:TextBox ID="txt39HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    40 HS</label>
                                <asp:TextBox ID="txt40HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    42 HS</label>
                                <asp:TextBox ID="txt42HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    44 HS</label>
                                <asp:TextBox ID="txt44HS3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    Aval.Meter</label>
                                <asp:TextBox ID="txtavamet3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    Tot.Shirts</label>
                                <asp:TextBox ID="txttotshirt3" runat="server" CssClass="form-control">0</asp:TextBox>
                            </td>
                            <td>
                                <label>
                                    Add</label>
                                <asp:ImageButton ID="ImageButton3" runat="server" CssClass="img-responsive" ImageUrl="~/images/edit_add.png"
                                    EnableViewState="true" />
                                <asp:Button ID="btlrecal" Visible="false" runat="server" class="btn btn-info" Text="Calc"
                                    OnClick="Recalclick" ValidationGroup="val1" Style="width: 120px; margin-left: 1000px" />
                            </td>
                        </tr>
                        <label>
                            Narration</label>
                        <asp:TextBox ID="txtnarration" runat="server" TextMode="MultiLine" CssClass="form-control"
                            Width="250px"></asp:TextBox>
                        <asp:Button ID="btnprocessall" runat="server" class="btn btn-info" Text="Process-All"
                            OnClick="processclickall" ValidationGroup="val1" Style="width: 120px; margin-left: 1000px; margin-bottom: -32px" />
                        <asp:Button ID="btnprocess" runat="server" class="btn btn-info" Text="Process" OnClick="processclick"
                            ValidationGroup="val1" Style="width: 120px; margin-left: 1136px" />
                    </table>
                    <br />
                    <br />
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel7">
                        <ProgressTemplate>
                            <div class="modal">
                                <div class="center">
                                    <img alt="" src="../images/01-progress.gif" />
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div class="row">
                        <div class="col-lg-12" style="margin-top: -35px">
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div>
                                            <asp:Label ID="Label7" runat="server" Style="color: Red"></asp:Label>
                                            <table class="table table-striped table-bordered table-hover" id="Table1" width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="200" Width="100%">
                                                            <asp:GridView ID="gvcustomerorder" AutoGenerateColumns="False" ShowFooter="True"
                                                                OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                                                CssClass="chzn-container" GridLines="None" Width="100%" runat="server">
                                                                <HeaderStyle BackColor="#59d3b4" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"
                                                                    Font-Names="arial" Font-Size="Smaller" HorizontalAlign="Center" />
                                                                <RowStyle BorderStyle="Solid" BorderWidth="0.5px" BorderColor="Gray" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="3%"
                                                                        HeaderStyle-Width="2%">
                                                                        <ItemTemplate>
                                                                            <%--  <asp:TextBox ID="txtno" Enabled="false" Text='<%# Eval("num")%>' runat="server" ></asp:TextBox>--%>
                                                                            <asp:Label ID="lblid" Visible="false" Text='<%# Eval("transid")%>' runat="server"></asp:Label>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Design/Color Code" Visible="true" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="15%" HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtdesigno" Enabled="false" Text='<%# Eval("design")%>' runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Name" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtitemname" Enabled="false" Text='<%# Eval("Itemname")%>' runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fit" ControlStyle-Width="100%" ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfitt" Enabled="false" Text='<%# Eval("Fit")%>' runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Pattern" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtpatt" Enabled="false" Text='<%# Eval("PAtternname")%>' runat="server"></asp:TextBox>
                                                                            <%-- <asp:TextBox ID="txtno" Height="30px" runat="server"></asp:TextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Party Name" Visible="false" HeaderStyle-Width="9%"
                                                                        ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtledgerid" Visible="false" Text='<%# Eval("ledgerid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtparty" Enabled="false" Text='<%# Eval("party")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rate" Visible="false" ControlStyle-Width="100%" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtRate" Enabled="false" Height="30px" Text='<%# Eval("Rate")%>'
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Aval.Kg/Gms" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtmet" Enabled="false" Text='<%# Eval("meter")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Shirt" ControlStyle-Width="100%" Visible="false" ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtshirt" Height="30px" Text='<%# Eval("Shirt")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="tctextra" Height="30px" Text='<%# Eval("Extra")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Required Meter" Visible="false" ControlStyle-Width="100%"
                                                                        ItemStyle-Width="6%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txteqrmeter" Enabled="false" Text='<%# Eval("reqmeter")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Avg.Kg/Gms " ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtavgsize" Enabled="false" Text='<%# Eval("avgsize")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req.T-Shirt" ControlStyle-Width="100%" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtreqshirt" Enabled="false" Text='<%# Eval("reqshirt")%>' Height="30px"
                                                                                runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fit" Visible="false" HeaderStyle-Width="9%" ItemStyle-Width="9%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfitid" Visible="false" Text='<%# Eval("Fitid")%>' runat="server"></asp:TextBox>
                                                                            <asp:TextBox ID="txtfit" Enabled="false" Text='<%# Eval("Fit")%>' runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30fs" Enabled="false" Text='<%# Eval("S30FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32fs" Enabled="false" Text='<%# Eval("S32FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34fs" Enabled="false" Text='<%# Eval("S34FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36fs" Enabled="false" Text='<%# Eval("S36FS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxsfs" Enabled="false" Text='<%# Eval("SXSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtssfs" Enabled="false" Text='<%# Eval("SSFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmfs" Enabled="false" Text='<%# Eval("SMFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslfs" Enabled="false" Text='<%# Eval("SLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlfs" Enabled="false" Text='<%# Eval("SXLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlfs" Enabled="false" Text='<%# Eval("SXXLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlfs" Enabled="false" Text='<%# Eval("S3XLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlfs" Enabled="false" Text='<%# Eval("S4XLFS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="30 " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts30hs" Enabled="false" Text='<%# Eval("S30HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="32 " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts32hs" Enabled="false" Text='<%# Eval("S32HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="34 " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts34hs" Enabled="false" Text='<%# Eval("S34HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="36 " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts36hs" Enabled="false" Text='<%# Eval("S36HS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XS " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxshs" Enabled="false" Text='<%# Eval("SXSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsshs" Enabled="false" Text='<%# Eval("SSHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="M " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsmhs" Enabled="false" Text='<%# Eval("SMHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="L " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtslhs" Enabled="false" Text='<%# Eval("SLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XL " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxlhs" Enabled="false" Text='<%# Eval("SXLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="XXL " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtsxxlhs" Enabled="false" Text='<%# Eval("SXXLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="3XL " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts3xlhs" Enabled="false" Text='<%# Eval("S3XLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="4XL " ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txts4xlhs" Enabled="false" Text='<%# Eval("S4XLHS")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="WSP" Visible="false" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtwwsp" Enabled="false" Text='<%# Eval("WSP")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total T-Shirt" Visible="true" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttotshit" Enabled="false" Text='<%# Eval("Total")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="42 HS" Visible="false" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="selectcust" Text='<%# Eval("LLedger")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                            <asp:TextBox ID="mainllab" Text='<%# Eval("Mainlab")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                            <asp:TextBox ID="fitllab" Text='<%# Eval("FItLab")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                            <asp:TextBox ID="washllab" Text='<%# Eval("Washlab")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                            <asp:TextBox ID="logollab" Text='<%# Eval("Logolab")%>' runat="server" Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField Visible="false" ShowDeleteButton="True" ButtonType="Button" />
                                                                    <asp:TemplateField HeaderText="Contrast" ControlStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtcont" Enabled="false" Text='<%# Eval("Contrast")%>' runat="server"
                                                                                Height="26px">0</asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="Td15" runat="server" visible="false" align="right">
                                                        <asp:Button ID="ButtonAdd1" runat="server" EnableTheming="false" Text="Add New" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <table id="Table3" style="margin-top: -36px" width="45%">
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    <%--Total Qty.--%></label>
                                                                <asp:TextBox Visible="false" ID="totqty" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <%--Total Meter.--%>
                                                                </label>
                                                                <asp:TextBox Visible="false" ID="totmeter" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <%--Item His.--%></label>
                                                                <asp:TextBox Visible="false" ID="txtitemhis" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    <%--Cust.His--%>
                                                                </label>
                                                                <asp:TextBox Visible="false" ID="txtcusthis" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <%-- <td>
                                                        <asp:TextBox ID="txtdamt5" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                    </td>--%>
                                                            <td>
                                                                <asp:TextBox ID="txtTamt5" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </table>
                                            <%--</tr>
                                            </tbody>--%>
                                        </td> </tr> </tbody>
                                        <table id="Table2" runat="server" visible="false">
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:TextBox CssClass="form-control" ID="TextBox13" Visible="false" runat="server"
                                                        Style="width: 110px; margin-left: 46px; margin-top: 11px; text-align: right">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <%--<asp:TextBox CssClass="form-control" ID="txtDiscamt" Visible="false"  Enabled="false" runat="server" style="width: 110px;margin-left: 43px; margin-top:17px; text-align:right" >0</asp:TextBox>--%>
                                                    <%-- <asp:Label ID="lblDisc" runat="server" ></asp:Label>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Payment Type
                                                </td>
                                                <td>Cheque/Card/DD No
                                                </td>
                                                <td>Amount
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlAgainst" runat="server" CssClass="form-control" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="form-control" ID="txtchequedd" runat="server" Style="width: 290px;">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="form-control" ID="txtAgainstAmount" runat="server" Style="width: 200px;">0</asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlAgainst1" runat="server" CssClass="form-control" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="form-control" ID="txtchequedd1" runat="server" Style="width: 290px;">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="form-control" ID="txtAgainstAmount1" runat="server" Style="width: 200px;">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox CssClass="form-control" ID="TextBox18" runat="server" Enabled="false"
                                                        Style="width: 250px;">Cash</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="form-control" ID="txtchequedd2" runat="server" Style="width: 290px;">0</asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox CssClass="form-control" ID="txtAgainstAmount2" runat="server" Style="width: 200px;">0</asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
                                        <br />
                                        <asp:Button ID="Button1" AccessKey="s" Visible="false" runat="server" class="btn btn-info"
                                            BorderWidth="3px" BorderColor="#e41300" BorderStyle="Inset" OnClick="Add_Click"
                                            onmouseover="this.style.backgroundColor='#5bc0de'" onmousedown="this.style.backgroundColor='olive'"
                                            onfocus="this.style.backgroundColor='#1b293e'" Text="Save" ValidationGroup="val1"
                                            Width="120px" />
                                        <asp:Button ID="btncalc" runat="server" Visible="false" class="btn btn-info" Text="Calc."
                                            OnClick="call_Click" ValidationGroup="val1" Style="width: 120px;" />
                                        <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" OnClick="Add_Click"
                                            OnClientClick="ClientSideClick(this)" UseSubmitBehavior="false" ValidationGroup="val1"
                                            Style="width: 120px;" />
                                        <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"
                                            Style="width: 120px;" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
