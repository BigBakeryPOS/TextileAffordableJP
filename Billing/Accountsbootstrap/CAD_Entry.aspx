<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CAD_Entry.aspx.cs" Inherits="Billing.Accountsbootstrap.CAD_Entry" %>

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
    <title>CAD ENTRY </title>
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
            <div class="col-lg-1">
            </div>
            <div class="col-lg-10">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                        <i class="fa fa-briefcase"></i>
                        <asp:Label ID="lblName" Text="CAD Entry" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Style="background-color: Orange; height: 16px;
                            vertical-align: top" Enabled="false" /><label>New</label>&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" Style="background-color: Green; height: 16px;
                            vertical-align: top" Enabled="false" /><label>Refresh</label>
                    </div>
                    <div class="panel-body">
                        <div class="list-group">
                            <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                            <div class="form-group">
                            </div>
                            <div class="col-lg-12">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>
                                            StyleNo As Well as New:</label>
                                        <asp:DropDownList ID="ddlStyles" runat="server" CssClass="chzn-select" Style="height: 30px"
                                            Width="100%">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblcadid" runat="server" Visible="false" ></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Date:</label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control center-block"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" PopupButtonID="txtFromDate"
                                            EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                    <div runat="server" visible="false" class="form-group">
                                        <label>
                                            Description(General Notes):</label>
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Upload PP Image
                                        </label>
                                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                            <ContentTemplate>
                                                <asp:FileUpload ID="fp_Upload" runat="server" />
                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-warning"
                                                    OnClick="btnUpload_OnClick" Width="100px" />
                                                <asp:Image ID="img_Photo" runat="server" Width="100px" BorderColor="1" />
                                                <asp:Label ID="lblFile_Path" runat="server" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpload" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <br />
                                        <br />
                                    </div>
                                     <div class="form-group">
                                        <label>
                                           PP Description(Optional):</label>
                                        <asp:TextBox ID="txtppdesc" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="form-group">
                                        <label>
                                            Upload Marker Image
                                        </label>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:FileUpload ID="fp_Upload1" runat="server" />
                                                <asp:Button ID="btnUpload1" runat="server" Text="Upload" CssClass="btn btn-warning"
                                                    OnClick="btnUpload1_OnClick" Width="100px" />
                                                <asp:Image ID="img_Photo1" runat="server" Width="100px" BorderColor="1" />
                                                <asp:Label ID="lblFile_Path1" runat="server" Visible="false"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpload1" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <br />
                                        <br />
                                    </div>
                                     <div class="form-group">
                                        <label>
                                           Marker Description(Optional):</label>
                                        <asp:TextBox ID="txtmarkerdesc" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                    <br />
                                </div>
                                <div class="col-lg-6">
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label style="text-align: right">
                                                        Fit 1:</label>
                                                    <br />
                                                    <asp:CheckBox ID="chkfit1" runat="server" />
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Fit 1 Notes :</label>
                                                    <asp:TextBox ID="txtfit1notes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Fit 1 Image (if Avaliable)
                                                    </label>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="fp_Upload2" runat="server" />
                                                            <asp:Button ID="btnUpload2" runat="server" Text="Upload" CssClass="btn btn-warning"
                                                                OnClick="btnUpload2_OnClick" Width="100px" />
                                                            <asp:Image ID="img_Photo2" runat="server" Width="100px" BorderColor="1" />
                                                            <asp:Label ID="lblFile_Path2" runat="server" Visible="false"></asp:Label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnUpload2" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <br />
                                                    <br />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label style="text-align: right">
                                                        Fit 2:</label>
                                                    <br />
                                                    <asp:CheckBox ID="chkfit2" runat="server" />
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Fit 2 Notes :</label>
                                                    <asp:TextBox ID="txtfit2notes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Fit 2 Image (if Avaliable)
                                                    </label>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="fp_Upload3" runat="server" />
                                                            <asp:Button ID="btnUpload3" runat="server" Text="Upload" CssClass="btn btn-warning"
                                                                OnClick="btnUpload3_OnClick" Width="100px" />
                                                            <asp:Image ID="img_Photo3" runat="server" Width="100px" BorderColor="1" />
                                                            <asp:Label ID="lblFile_Path3" runat="server" Visible="false"></asp:Label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnUpload3" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <br />
                                                    <br />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="form-group">
                                                    <label style="text-align: right">
                                                        Fit 3:</label>
                                                    <br />
                                                    <asp:CheckBox ID="chkfit3" runat="server" />
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Fit 3 Notes :</label>
                                                    <asp:TextBox ID="txtfit3notes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <div class="form-group">
                                                    <label>
                                                        Fit 3 Image (if Avaliable)
                                                    </label>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="fp_Upload4" runat="server" />
                                                            <asp:Button ID="btnUpload4" runat="server" Text="Upload" CssClass="btn btn-warning"
                                                                OnClick="btnUpload4_OnClick" Width="100px" />
                                                            <asp:Image ID="img_Photo4" runat="server" Width="100px" BorderColor="1" />
                                                            <asp:Label ID="lblFile_Path4" runat="server" Visible="false"></asp:Label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnUpload4" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <br />
                                                    <br />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" ValidationGroup="val1"
                                                    Style="width: 110px;" OnClick="btnSave_OnClick" />
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Button ID="btnExit" runat="server" class="btn btn-info" Text="Exit" Style="width: 110px;"
                                                    OnClick="btnExit_OnClick" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-1">
            </div>
        </div>
    </div>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/chosen.min.js" type="text/javascript"></script>
    <script type="text/javascript">        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
