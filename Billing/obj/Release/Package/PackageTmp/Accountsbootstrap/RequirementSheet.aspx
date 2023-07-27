<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequirementSheet.aspx.cs"
    Inherits="Billing.Accountsbootstrap.RequirementSheet" %>

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
    <title>Requirement Sheet </title>
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
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
    <script type="text/javascript" src="../js/jquery-1.7.2.js"></script>
    <link rel="stylesheet" href="../css/chosen.css" />
    <script type="text/javascript">
        function Confirm(myButton) {

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
    <style type="text/css">
        .zoomed-element
        {
            zoom: 1.8;
        }
    </style>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <asp:Label runat="server" ID="lblContactTypeId" ForeColor="White" CssClass="label"
        Visible="false" Text="1"> </asp:Label>
    <asp:Label runat="server" ID="lblColorId" ForeColor="White" CssClass="label" Visible="false"
        Text="18"> </asp:Label>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="col-lg-12">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>
                                        Exc.No</label>
                                    <asp:DropDownList ID="drpexclist" runat="server" CssClass="chzn-select" OnSelectedIndexChanged="exc_selected_click"
                                        Width="100%" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <label>
                                    Shipment Date
                                </label>
                                <asp:Label ID="lblshipmentdate" runat="server"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" ValidationGroup="val1"
                                                Style="width: 90px;" OnClick="btnSave_OnClick" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" Style="width: 90px;"
                                                PostBackUrl="~/Accountsbootstrap/RequirementSheetGrid.aspx" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-12">
                <div class="panel-body">
                    <div class="row">
                        <asp:HiddenField ID="selected_tab" runat="server" />
                        <div id="tabs" style="background-color: white; padding-left: 30px">
                            <ul>
                                <li><a href="#tabs-1">Style Details</a></li>
                                <li><a href="#tabs-3">Style and Color Wise Details</a></li>
                                <li><a href="#tabs-2">Avl.Stock Details</a></li>
                                
                            </ul>
                            <div class="row" id="tabs-1" style="background-color: white; padding-top: 30px">
                                <div style="background-color: white;">
                                    <div class="col-lg-12">
                                        <div class="col-lg-4">
                                            <div class="col-lg-6">
                                                <label>
                                                    Style Under the exc</label>
                                                <br />
                                                <asp:GridView ID="gvstylewithcolor" CssClass="myGridStyle1" Width="100%" runat="server"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Style NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstyleno" runat="server" Text='<%#  Eval("styleno") %>'></asp:Label>
                                                                <asp:Label ID="lblstyleid" Visible="false" runat="server" Text='<%#  Eval("SamplingCostingId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Color Desc.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcolor" runat="server" Text='<%#  Eval("color") %>'></asp:Label>
                                                                <asp:Label ID="lblcolorid" runat="server" Visible="false" Text='<%#  Eval("colorid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label>
                                                    All Pcs Process</label>
                                                <div style="padding-left: 10px; overflow-y: scroll;">
                                                    <asp:CheckBoxList ID="chkpcsprocess" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                                <asp:Button ID="btnpcsprocess" runat="server" Text=">>" OnClick="Pcs_process_Click" />
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label>
                                                    selected Pcs Process</label>
                                                <div style="padding-left: 10px; overflow-y: scroll;">
                                                    <asp:GridView ID="gvPcsProcessDetails" CssClass="myGridStyle1" Width="100%" runat="server"
                                                        AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Pcs Process Details">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPname" Text='<%#  Eval("Pname") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblpid" Visible="false" Text='<%#  Eval("Pid") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Process Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSts" Text='<%#  Eval("Sts") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="col-lg-12">
                                            <label>
                                                Sample And Avg.Details/Change the Color</label>
                                            <div class="col-lg-3">
                                                <asp:Button ID="Button2" runat="server" Text="Update Details" CssClass="btn btn-info"
                                                    OnClick="color_change" />
                                            </div>
                                            <br />
                                            <br />
                                            <%--<div style="padding-left: 10px; overflow-y: scroll; height:50px;">--%>
                                            <asp:GridView ID="gridviewstyle" AutoGenerateColumns="false" OnRowDataBound="GricViewStyle_Color"
                                                Width="100%" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Style NO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstyleno" runat="server" Text='<%#  Eval("styleno") %>'></asp:Label>
                                                            <asp:Label ID="lblstyleid" Visible="false" runat="server" Text='<%#  Eval("SamplingCostingId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="color Desc.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemcolor" runat="server" Text='<%#  Eval("Color") %>'></asp:Label>
                                                            <asp:Label ID="lblstylecolorid" runat="server" Text='<%#  Eval("StyleColorId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemtype" runat="server" Text='<%#  Eval("Itemgroupname") %>'></asp:Label>
                                                            <asp:Label ID="lblItemgroupId" Visible="false" runat="server" Text='<%#  Eval("ItemgroupId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcategory" runat="server" Text='<%#  Eval("Category") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemname" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                            <asp:Label ID="lblitemid" Visible="false" runat="server" Text='<%#  Eval("itemmasterid") %>'></asp:Label>
                                                            <asp:Label ID="lblcolorid" Visible="false" runat="server" Text='<%#  Eval("colorid") %>'></asp:Label>
                                                            <asp:Label ID="lblCqty" Visible="false" runat="server" Text='<%#  Eval("Cqty") %>'></asp:Label>
                                                            <asp:Label ID="lblBQty" Visible="false" runat="server" Text='<%#  Eval("BQty") %>'></asp:Label>
                                                            <asp:Label ID="lblStotpcs" Visible="false" runat="server" Text='<%#  Eval("Stotpcs") %>'></asp:Label>
                                                            <asp:Label ID="lblPtotpcs" Visible="false" runat="server" Text='<%#  Eval("Ptotpcs") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select Color ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpcolorlist" runat="server" CssClass="chzn-select">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" HeaderText="Item Desc.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemdesc" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sampling Avg.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsampleavg" runat="server" Text='<%#  Eval("SmpAvg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Production Avg.">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblprodavg" runat="server" Text='<%#  Eval("prodavg") %>'></asp:Label>--%>
                                                            <asp:TextBox ID="txtprodavg" runat="server" Text='<%#  Eval("PrdAvg") %>'></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server"
                                                                TargetControlID="txtprodavg" ValidChars="." FilterType="Numbers,Custom" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Uom">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%#  Eval("Units") %>'></asp:Label>
                                                            <asp:Label ID="lblunitsid" Visible="false" runat="server" Text='<%#  Eval("UOMID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="tabs-2" style="background-color: #D0D3D6; padding-top: 30px">
                                <asp:UpdatePanel ID="updpanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="background-color: #D0D3D6;">
                                            <div runat="server" visible="false" class="col-lg-12">
                                                <label>
                                                    Item in the Selected Style</label>
                                                <div class="col-lg-3">
                                                    <label>
                                                        Code</label>
                                                    <asp:Label ID="lblcode" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <label>
                                                        Description</label>
                                                    <asp:Label ID="lbldesc" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <label>
                                                        Qty</label>
                                                    <asp:Label ID="lblprodqty" ForeColor="Red" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtqty" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:Button ID="btnmodifyproduction" runat="server" Text="Modify Production Avg." />
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" />
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <label>
                                                    Stock Details</label>
                                                <br />
                                                <%--<div class="col-lg-3">
                                            <asp:Button ID="Button1" runat="server" Text="Modify Production Avg." OnClick="Prod_click" />
                                        </div>--%>
                                                <div id="Div4" runat="server" class="form-group">
                                                    <label>
                                                        Select Company</label>
                                                    <asp:DropDownList ID="drpcompany" runat="server" CssClass="form-control" OnSelectedIndexChanged="Cmpny_chnaged"
                                                        Width="50%" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <asp:GridView ID="gridstockdetails" CssClass="myGridStyle1" Width="100%" runat="server"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Item Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemgroupname" runat="server" Text='<%#  Eval("Itemgroupname") %>'></asp:Label>
                                                                <asp:Label ID="lblItemgroupId" runat="server" Visible="false" Text='<%#  Eval("ItemgroupId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemanme" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                                <asp:Label ID="lblitemid" runat="server" Visible="false" Text='<%#  Eval("itemmasterid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Color">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcolor" runat="server" Text='<%#  Eval("Itemcolor") %>'></asp:Label>
                                                                <asp:Label ID="lblitemcolorid" runat="server" Visible="false" Text='<%#  Eval("Itemcolorid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sampling Req.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsampavg" runat="server" Text='<%#  Eval("STotalpcs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prod. Req.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblprodavg" runat="server" Text='<%#  Eval("PTotalpcs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Avl.Stock">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblavlstock" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Want To Purchase Stock">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpurchasestock" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Units">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunits" runat="server" Text='<%#  Eval("Units") %>'></asp:Label>
                                                                <asp:Label ID="lblunitsid" runat="server" Visible="false" Text='<%#  Eval("Unitsid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="row" id="tabs-3" style="background-color: #D0D3D6; padding-top: 30px">
                                <div style="background-color: #D0D3D6;">
                                    <div class="col-lg-12">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label>
                                                    Style Wise Item and color Details</label>
                                                <asp:GridView ID="gvstylewiseitemcolor" CssClass="myGridStyle1" Width="100%" runat="server"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Style NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstyleno" runat="server" Text='<%#  Eval("styleno") %>'></asp:Label>
                                                                <asp:Label ID="lblstyleid" Visible="false" runat="server" Text='<%#  Eval("SamplingCostingId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="color Desc.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcolor" runat="server" Text='<%#  Eval("Color") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Pcs.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotalpcs" runat="server" Text='<%#  Eval("AffectedQty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemanme" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                                <asp:Label ID="lblitemid" runat="server" Visible="false" Text='<%#  Eval("itemmasterid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Color">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcolor" runat="server" Text='<%#  Eval("Itemcolor") %>'></asp:Label>
                                                                <asp:Label ID="lblitemcolorid" Visible="false" runat="server" Text='<%#  Eval("Itemcolorid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sampling Avg.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsampavg" runat="server" Text='<%#  Eval("Quantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prod. Avg.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblprodavg" runat="server" Text='<%#  Eval("PQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Units">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunits" runat="server" Text='<%#  Eval("Units") %>'></asp:Label>
                                                                <asp:Label ID="lblunitsid" Visible="false" runat="server" Text='<%#  Eval("Unitsid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    Color wise Item Details</label>
                                                <asp:GridView ID="gvcolorwiseitem" CssClass="myGridStyle1" Width="100%" runat="server"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Item Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemgroupname" runat="server" Text='<%#  Eval("Itemgroupname") %>'></asp:Label>
                                                                <asp:Label ID="lblItemgroupId" Visible="false" runat="server" Text='<%#  Eval("ItemgroupId") %>'></asp:Label>
                                                                <%--<asp:Label ID="lblstyleid" Visible="false" runat="server" Text='<%#  Eval("SamplingCostingId") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemanme" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                                <asp:Label ID="lblitemid" Visible="false" runat="server" Text='<%#  Eval("itemmasterid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Color">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcolor" runat="server" Text='<%#  Eval("Itemcolor") %>'></asp:Label>
                                                                <asp:Label ID="lblitemcolorid" Visible="false" runat="server" Text='<%#  Eval("Itemcolorid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sampling Qty.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsampavg" runat="server" Text='<%#  Eval("STotalpcs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prod. Qty.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblprodavg" runat="server" Text='<%#  Eval("PTotalpcs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Units">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunits" runat="server" Text='<%#  Eval("Units") %>'></asp:Label>
                                                                <asp:Label ID="lblunitsid" Visible="false" runat="server" Text='<%#  Eval("Unitsid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <label>
                                                    Change Item Category</label>
                                                <asp:GridView ID="gvitemcategory" CssClass="myGridStyle1" Width="100%" runat="server"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Item Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemgroupname" runat="server" Text='<%#  Eval("Itemgroupname") %>'></asp:Label>
                                                                <asp:Label ID="lblItemgroupId" runat="server" Visible="false" Text='<%#  Eval("ItemgroupId") %>'></asp:Label>
                                                                <%--<asp:Label ID="lblstyleid" Visible="false" runat="server" Text='<%#  Eval("SamplingCostingId") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemanme" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                                <asp:Label ID="lblitemid" Visible="false" runat="server" Text='<%#  Eval("itemmasterid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Color">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcolor" runat="server" Text='<%#  Eval("Itemcolor") %>'></asp:Label>
                                                                <asp:Label ID="lblitemcolorid" Visible="false" runat="server" Text='<%#  Eval("Itemcolorid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Change Item Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblchangeitemanme" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                                <asp:Label ID="lblchnageitemid" runat="server" Visible="false" Text='<%#  Eval("itemmasterid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sampling Qty.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsampavg" runat="server" Text='<%#  Eval("STotalpcs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prod. Qty.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblprodavg" runat="server" Text='<%#  Eval("PTotalpcs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Units">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunits" runat="server" Text='<%#  Eval("Units") %>'></asp:Label>
                                                                <asp:Label ID="lblunitsid" runat="server" Visible="false" Text='<%#  Eval("Unitsid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="form-group">
                                                <label>
                                                    Calculate Required Item</label>
                                                <asp:GridView ID="gvrequireditem" CssClass="myGridStyle1" Width="100%" runat="server"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Item Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemgroupname" runat="server" Text='<%#  Eval("Itemgroupname") %>'></asp:Label>
                                                                <asp:Label ID="lblItemgroupId" runat="server" Visible="false" Text='<%#  Eval("ItemgroupId") %>'></asp:Label>
                                                                <%--<asp:Label ID="lblstyleid" Visible="false" runat="server" Text='<%#  Eval("SamplingCostingId") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemanme" runat="server" Text='<%#  Eval("Description") %>'></asp:Label>
                                                                <asp:Label ID="lblitemid" runat="server" Visible="false" Text='<%#  Eval("itemmasterid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Color">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcolor" runat="server" Text='<%#  Eval("Itemcolor") %>'></asp:Label>
                                                                <asp:Label ID="lblitemcolorid" runat="server" Visible="false" Text='<%#  Eval("Itemcolorid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total.Pcs.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotpcs" runat="server" Text='<%#  Eval("Totalpcs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sampling Req.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsampavg" runat="server" Text='<%#  Eval("STotalpcs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prod. Req.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblprodavg" runat="server" Text='<%#  Eval("PTotalpcs") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Units">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunits" runat="server" Text='<%#  Eval("Units") %>'></asp:Label>
                                                                <asp:Label ID="lblunitsid" runat="server" Visible="false" Text='<%#  Eval("Unitsid") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <script src="../js/jquery.min.js" type="text/javascript"></script>
                            <script src="../js/chosen.min.js" type="text/javascript"></script>
                            <script type="text/javascript">                                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
                            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
                            <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
                            <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
                                rel="stylesheet" type="text/css" />
                            <script type="text/javascript">
                                var selected_tab = 1;
                                $(function () {
                                    var tabs = $("#tabs").tabs({
                                        select: function (e, i) {
                                            selected_tab = i.index;
                                        }
                                    });
                                    selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
                                    tabs.tabs('select', selected_tab);
                                    $("form").submit(function () {
                                        $("[id$=selected_tab]").val(selected_tab);
                                    });
                                });
    
                            </script>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/chosen.min.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>--%>
    </form>
</body>
</html>
