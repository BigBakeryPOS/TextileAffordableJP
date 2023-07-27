<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeadMaster.aspx.cs" Inherits="Billing.Accountsbootstrap.LeadMaster" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Affordable Export Lead Generator</title>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <link rel="stylesheet" href="../Css/bootstrap.min.css" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link href="../Styles/chosen.css" rel="Stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <usc:Header ID="Header" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body">
                    <div visible="false" class="row" style="text-align: center; font-size: large">
                        <b>Lead Entry Form</b></div>
                    <br />
                    <%-- <label>
                        Select Assign Task</label>--%>
                    <asp:DropDownList ID="drptask" Visible="false" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btndownload" Text="Download" runat="server" Visible="false" OnClick="filedownload" />
                    <asp:TextBox CssClass="" ID="txtLoginID" Visible="false" runat="server"></asp:TextBox>
                    <div id="Div1" runat="server" class="row">
                        <div id="noexcel" visible="false" runat="server">
                            <table cellpadding="1" cellspacing="2" width="450px" style="border: 1px solid blue;
                                height: 150px;">
                                <tr class="headerPopUp">
                                    <td id="Td1" runat="server" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table style="width: 100%">
                                            <tr style="height: 15px">
                                            </tr>
                                            <tr>
                                                <td style="width: 30%">
                                                </td>
                                                <td style="width: 35%">
                                                    <div>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                        <asp:GridView ID="GridView1" runat="server">
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr style="height: 6px">
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 30%">
                                                            </td>
                                                            <td style="width: 35%" align="center">
                                                                <asp:Button ID="btnUpload" runat="server" Height="31px" class="btn btn-info" Text="Upload"
                                                                    Width="100px" OnClick="btnUpload_Click" />
                                                            </td>
                                                            <td style="width: 35%">
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 10px">
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 15%">
                                                            </td>
                                                            <td style="width: 70%" align="center">
                                                                <asp:Button ID="Button2" runat="server" class="btn btn-info" Text="Download the Sample Excel Format"
                                                                    Height="31px" OnClick="btnFormat_Click" />
                                                                <asp:Button ID="Button1" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit1_Click" />
                                                            </td>
                                                            <td style="width: 15%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="Sve" runat="server" visible="true">
                         <div class="col-lg-8">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label>
                                        Company Name</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtcompanyname" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Customer Name</label><font color="red">*</font><br />
                                    <asp:TextBox CssClass="form-control" ID="txtcustomername1" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblleadid" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Customer Name 2</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtcustomername2" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Customer Address</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtaddress" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                               
                            </div>
                             <div class="col-lg-3"> 
                              <div class="form-group">
                                    <label>
                                        Customer Contact No.1</label><font color="red">*</font><br />
                                    <asp:TextBox CssClass="form-control" ID="txtcustcontactno1" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Customer Contact No.2</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtcustcontactno2" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Customer Email Id</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtxemailid" runat="server"></asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <label>
                                        Customer Destignation</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtxdesignation" runat="server"></asp:TextBox>
                                </div>  
                             </div>
                            <div class="col-lg-3">                               
                                <div class="form-group">
                                    <label>
                                        Company Phone No</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtxcompanyno" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Result Of Meet</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtxresultofmeet" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Lead Generate Date</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtxnextappoint" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender11" TargetControlID="txtxnextappoint"
                                        runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Lead FollowUp Time</label><br />
                                    <asp:DropDownList ID="ddlTimeFrom" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                 </div>
                            <div class="col-lg-3">   
                                <div class="form-group">
                                    <label>
                                        Remarks/Comments</label><br />
                                    <asp:TextBox CssClass="form-control" ID="txtremarks" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Reference Type</label><br />
                                    <asp:DropDownList ID="ddlreference" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        Status</label><br />
                                    <asp:DropDownList ID="ddlstatus" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                  <div class="form-group">
                                    <label>
                                        To Assign</label><br />
                                    <asp:DropDownList ID="ddlEmpname" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Save"
                                        OnClick="btnSave_Click" Width="100px" />
                                    <asp:Button ID="btnExit" runat="server" CssClass="btn btn-danger" Text="Exit" PostBackUrl="../Accountsbootstrap/LeadGrid.aspx"
                                        Width="60px" />
                                </div>
                            </div>
                            </div>
                            <div class="col-lg-4">
                                <center>
                                    <label>
                                        Item Description Details</label>
                                </center>
                                <asp:GridView ID="grdiitem" runat="server" AutoGenerateColumns="false" Width="100%"
                                    OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpitem" runat="server" Width="300px" CssClass="chzn-select">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtqty" runat="server" Width="0px" CssClass="form-control">0</asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtrate" runat="server" Width="0px" CssClass="form-control">0</asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="ButtonAdd1" runat="server" AutoPostback="false" EnableTheming="false"
                                                    Text="Add New" OnClick="ButtonAdd1_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div runat="server" visible="false" class="col-lg-4">
                                <div class="panel-body">
                                <center>
                                    <label >
                                        Today's Records</label></center>
                                    <asp:GridView ID="gv_Employee" EmptyDataText="Sorry!! No Records Found" runat="server"
                                        Style="margin-left: 50px;" AutoGenerateColumns="false" CssClass="myGridSty" OnRowCommand="gv_Employee_RowCommand"
                                        Width="95%" OnRowEditing="gv_Employee_RowEditing">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <PagerSettings Mode="Numeric" />
                                        <Columns>
                                            <asp:BoundField HeaderText="company Name" DataField="companyname" />
                                            <asp:BoundField HeaderText="Customer Name" DataField="customername" />
                                            <asp:BoundField HeaderText="Company Number" DataField="CompanyNumber" />
                                            <asp:BoundField HeaderText="Customer Number" DataField="customermobile" />
                                            <asp:BoundField HeaderText="Email Id" DataField="emailid" />
                                            <asp:BoundField HeaderText="Next Appoinment" DataField="Appointdate" DataFormatString='{0:d}' />
                                            <asp:BoundField HeaderText="status" DataField="sts" />
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("EntryId") %>' CommandName="Edit"
                                                        runat="server">
                                                        <asp:Image ID="imdedit" ImageUrl="~/images/edit.png" runat="server" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <br />
                                    <center>
                                    <label >
                                        Follow Up Records</label></center>
                                    <asp:GridView ID="gridfollowup" EmptyDataText="Sorry!! No Records Found" runat="server"
                                        Style="margin-left: 50px;" AutoGenerateColumns="false" CssClass="myGridSty" OnRowCommand="gridfollowup_RowCommand"
                                        Width="95%" OnRowEditing="gridfollowup_RowEditing">
                                        <HeaderStyle BackColor="#3366FF" />
                                        <PagerSettings Mode="Numeric" />
                                        <Columns>
                                            <asp:BoundField HeaderText="company Name" DataField="companyname" />
                                            <asp:BoundField HeaderText="Customer Name" DataField="customername" />
                                            <asp:BoundField HeaderText="Company Number" DataField="CompanyNumber" />
                                            <asp:BoundField HeaderText="Customer Number" DataField="customermobile" />
                                            <asp:BoundField HeaderText="Email Id" DataField="emailid" />
                                            <asp:BoundField HeaderText="Next Appoinment" DataField="Appointdate" DataFormatString='{0:d}' />
                                            <asp:BoundField HeaderText="status" DataField="sts" />
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("EntryId") %>' CommandName="Edit"
                                                        runat="server">
                                                        <asp:Image ID="imdedit" ImageUrl="~/images/edit.png" runat="server" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div id="Upte" runat="server" visible="false">
                            <asp:GridView ID="gvProductSpecification" Width="100%" Style="margin-left: 0px" runat="server"
                                CssClass="myGridSty" ShowFooter="true" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="RowNumber" HeaderText="S.No" />
                                    <asp:TemplateField HeaderText="Cust.Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblentryid" runat="server" Visible="false" Text='<%Eval("EntryId")%>'></asp:Label>
                                            <asp:TextBox ID="txtcustomername" CssClass="form-control" Style="width: 100%" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust.Address">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtaddress" CssClass="form-control" Style="width: 100%" TextMode="MultiLine"
                                                runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-Width="10%" HeaderText="Cust.Contact No">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtno" CssClass="form-control" Style="width: 117px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust.Designation">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtdesignation" CssClass="form-control" Style="width: 121px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Result Of Meet">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtresult" CssClass="form-control" Style="width: 103px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Next Appt.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtappoint" CssClass="form-control" Style="width: 103px" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtappoint"
                                                runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks/Comments">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtremark" Style="width: 100%" CssClass="form-control" TextMode="MultiLine"
                                                runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpstatus" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Follow" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Confirm" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Close" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="ButtonAdd" Visible="false" OnClick="AddNeRow" runat="server" Text="Add New Row" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="row" style="margin-left: 99px; padding-top: 15px">
                            <div id="Div2" runat="server" visible="false" class="form-group">
                                <label style="width: 20%">
                                    Application Form No :
                                </label>
                                <asp:TextBox CssClass="" ID="txtApplicationID" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-1">
        </div>
    </div>
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
