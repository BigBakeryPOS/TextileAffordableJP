<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Emp_gird.aspx.cs" Inherits="Billing.Accountsbootstrap.Emp_gird" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Employee Details</title>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../Styles/AjaxPopUp.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
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
    <link rel="stylesheet" href="../Css/bootstrap.min.css" />
    <script type="text/javascript" src="../jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../jquery/bootstrap.min.js"></script>
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <link href="../Styles/chosen.css" rel="Stylesheet" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <form id="Form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <div class="row">
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" style="margin-top: 6px">
                            <div class="col-lg-2">
                                <h1 class="page-header" style="text-align: center; color: #fe0002; font-size: 18px;
                                    font-weight: bold">
                                    Employee Master</h1>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Units</label>
                                    <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" CssClass="form-control"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <label>
                                        Department
                                    </label>
                                    <asp:DropDownList ID="ddldepartment" runat="server" Width="100%" CssClass="form-control"
                                        OnSelectedIndexChanged="ddldepartment_OnSelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <label>
                                        Status</label>
                                    <asp:DropDownList runat="server" ID="ddlStatus" Width="105px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddldepartment_OnSelectedIndexChanged" CssClass="form-control">
                                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="UnActive" Value="2"></asp:ListItem>
                                        <%--<asp:ListItem Text="Abscond" Value="3"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <br />
                                    <asp:TextBox CssClass="form-control" ID="txtsearch" onkeyup="Search_Gridview(this, 'gridviewhrm')"
                                        placeholder="Enter Search Text" MaxLength="50" Style="width: 230px" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <br />
                                <asp:Button ID="btnsearch" runat="server" Visible="true" CssClass="btn btn-success"
                                    Text="Export-Excel" OnClick="btnsearch_Click" />
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <br />
                                    <asp:Button ID="btnadd" runat="server" CssClass="btn btn-primary" Text="Add New"
                                        Style="width: 110px" OnClick="btnadd_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="col-lg-1">
                            <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                OnRowDataBound="gvDesignationExcel_OnRowDataBound" ShowFooter="true" ShowHeader="true"
                                CssClass="myGridStyle" Width="100%">
                                <HeaderStyle BackColor="#3366FF" />
                                <PagerSettings Mode="Numeric" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Desigination" DataField="DesiginationName" />
                                    <asp:BoundField HeaderText="Count" DataField="Count" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-lg-11">
                            <asp:GridView ID="gridviewhrm" runat="server" Style="margin-left: 140px;" AutoGenerateColumns="false"
                                EmptyDataText="No Records Found" ShowHeader="true" CssClass="myGridStyle" OnRowCommand="gridviewhrm_RowCommand"
                                Width="90%">
                                <HeaderStyle BackColor="#3366FF" />
                                <PagerSettings Mode="Numeric" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Emp Code" DataField="Emp_code" />
                                    <asp:BoundField HeaderText="Name" DataField="name" />
                                    <asp:BoundField HeaderText="Dept.Name" DataField="DeptName" />
                                    <asp:BoundField HeaderText="Designation" DataField="DesiginationName" />
                                    <asp:BoundField HeaderText="Unit" DataField="UnitName" />
                                    <asp:BoundField HeaderText="EmployeeStatus" DataField="EmployeeStatus" />
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnedit" CommandArgument='<%#Eval("Employee_Id") %>' CommandName="edit"
                                                runat="server">
                                                <asp:Image ID="imdedit" ImageUrl="~/images/edit.png" runat="server" /></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btndel" CommandArgument='<%#Eval("Employee_Id") %>' CommandName="Del"
                                                runat="server">
                                                <asp:Image ID="Image1" ImageUrl="~/images/delete.png" runat="server" /></asp:LinkButton>
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
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="Excel" runat="server">
                        <div class="col-lg-12">
                            <div class="col-lg-2">
                                <asp:GridView ID="gvDesignationExcel" runat="server" AutoGenerateColumns="false"
                                    OnRowDataBound="gvDesignationExcel_OnRowDataBound" EmptyDataText="No Records Found"
                                    ShowHeader="true" ShowFooter="true" CssClass="myGridStyle">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Desigination" DataField="DesiginationName" />
                                        <asp:BoundField HeaderText="Count" DataField="Count" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-lg-10">
                                <asp:GridView ID="gridempdetails" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                    CssClass="myGridStyle">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo  " HeaderStyle-Width="1%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblempcode" runat="server" Text='<%#Eval("empcode") %>'></asp:Label>
                                                <%--<asp:Image ID="img1" runat="server" Height="40px" Width="45px" ImageUrl='<%#Eval("uploadimage") %>'></asp:Image>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Emp.Name" DataField="name" />
                                        <asp:BoundField HeaderText="Father Name" DataField="fathername" />
                                        <asp:BoundField HeaderText="MaritalStatus" DataField="MaritalStatus" />
                                        <asp:BoundField HeaderText="Is Mother/Wife" DataField="IsMotherWife" />
                                        <asp:BoundField HeaderText="Mother/Wife Name" DataField="MotherWife" />
                                        <asp:BoundField HeaderText="Emp.DOB" DataField="dob" />
                                        <asp:BoundField HeaderText="Mobile 1" DataField="phno_No" />
                                        <asp:BoundField HeaderText="Mobile 2" DataField="mobile1" />
                                        <asp:BoundField HeaderText="CurrentAddress" DataField="CurrentAddress" />
                                        <asp:BoundField HeaderText="PermanentAddress" DataField="PermanentAddress" />
                                        <asp:BoundField HeaderText="Email Id" DataField="email_id" />
                                        <asp:BoundField HeaderText="DOJ" DataField="DOJ" />
                                        <asp:BoundField HeaderText="Salary Type" DataField="salarytype" />
                                        <asp:BoundField HeaderText="Unit" DataField="UnitName" />
                                        <asp:BoundField HeaderText="Desgination Name" DataField="DesiginationName" />
                                        <asp:BoundField HeaderText="Dept.Name" DataField="DeptName" />
                                        <asp:BoundField HeaderText="EmployeeStatus" DataField="EmployeeStatus" />


                                        <%--<asp:ImageField DataImageUrlField="uploadimage" HeaderText="Image" ItemStyle-Height="25px"
                                                    ItemStyle-Width="25px" />--%>
                                        <%-- <asp:TemplateField HeaderText="Reporting Status">
                                                        <ItemTemplate>
                                                            <asp:Image ID="img1" runat="server" Width="100px" Height="100px" ImageUrl='<%#Eval("uploadimage") %>'>
                                                            </asp:Image>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <asp:Panel CssClass="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
                        runat="server">
                        <div class="popup_Container">
                            <div class="popup_Titlebar" id="PopupHeader">
                                <div class="TitlebarLeft">
                                    Employee Master</div>
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
                </div>
            </div>
        </div>
    </div>
    </form>
    <asp:Label ID="lblempid" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblempname" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbllogintime" runat="server"></asp:Label>
    <asp:Label ID="lbllogtime" runat="server"></asp:Label>
    <asp:Label ID="id" runat="server"></asp:Label>
</body>
</html>
