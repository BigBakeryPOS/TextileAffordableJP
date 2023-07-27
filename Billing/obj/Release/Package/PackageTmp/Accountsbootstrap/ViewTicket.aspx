<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewTicket.aspx.cs" Inherits="Billing.Accountsbootstrap.ViewTicket" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ticket</title>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style1.css" rel="stylesheet" type="text/css" />
    <script src="../js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../css/chosen.css" />
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
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM" runat="server">
    </asp:ScriptManager>
    <div>
        <%--Top Section--%>
        <%--Top Section--%>
        <%-- Body Section--%>
        <div class="row col-lg-12">
            <br />
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading" style="background-color: #59D3B4">
                        Ticket</div>
                    <div class="panel-body">
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-6">
                            <div class="col-lg-12">
                                <div class="col-lg-4">
                                    <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                                    <b>Date</b>
                                    <asp:TextBox ID="txtdate" runat="server" Enabled="false" CssClass="form-control"
                                        Width="200px" onkeydown="return DateFormat(this, event.keyCode)" Text="--Select Date--"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtdate"
                                        runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                    <br />
                                    <b>Name: </b>
                                    <asp:TextBox ID="txtname" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    <br />
                                    <br />
                                    <b>Phone No: </b>
                                    <asp:TextBox ID="txtphoneno" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    <br />
                                    <br />
                                    <b>Ticket No: </b>
                                    <asp:TextBox ID="txttickect" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    <br />
                                    <b>Subject: </b>
                                    <asp:TextBox ID="txtSubject" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                    <br />
                                </div>
                                <div class="col-lg-4">
                                    <b>Comment : </b>
                                    <textarea id="txtcomment" class="form-control" runat="server" enabled="false" style="width: 300px;
                                        height: 150px;"></textarea>
                                    <br />
                                    <b>Concern Person : </b>
                                    <asp:DropDownList ID="ddlConcern" runat="server" CssClass="form-control" Width="300px">
                                    </asp:DropDownList>
                                    <br />
                                    <b>Priority : </b>
                                    <asp:DropDownList ID="ddlPriorityStatus" runat="server" CssClass="form-control" Width="300px">
                                        <asp:ListItem Text="High" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Low" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <b>Status : </b>
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Width="300px">
                                        <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Assigned" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="In-Progress" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <b>Admin Comment: </b>
                                    <asp:TextBox ID="txtAdminComment" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                    <br />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success"
                                        OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-danger" OnClick="btnExit_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <label>
                                Chat History</label>
                            <br />
                            <br />
                            Ticket No :
                            <asp:Label ID="lblticketno" runat="server"></asp:Label><br />
                            Ticket Date :
                            <asp:Label ID="lblticketdate" runat="server"></asp:Label><br />
                            <br />
                            <label>
                                Chat Details</label>
                            <asp:GridView ID="gvChatDetails" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="myGridStyle">
                                <Columns>
                                    
                                    <%--<asp:BoundField HeaderText="TicketNo" DataField="ticketno" />
                                    <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:d}" />--%>
                                    <asp:BoundField HeaderText="Emp Name" DataField="Name" />
                                    <asp:BoundField HeaderText="Comments" DataField="Comments" />
                                    <asp:BoundField HeaderText="Status" DataField="Status1" />
                                    <asp:BoundField HeaderText="Priority" DataField="PriorityStatus_" />
                                    <asp:BoundField HeaderText="Entry Date" DataField="ticketdate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss tt}" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- Body Section--%>
        <asp:Panel class="popupConfirmation" ID="DivDeleteConfirmation" Style="display: none"
            runat="server">
            <div class="popup_Container">
                <div class="popup_Titlebar" id="PopupHeader">
                    <div class="TitlebarLeft">
                        Tax Master</div>
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
        <script src="../js/chosen.min.js" type="text/javascript"></script>
        <script type="text/javascript">            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
    </form>
</body>
</html>
