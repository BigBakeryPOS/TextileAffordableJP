<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketRaise.aspx.cs" Inherits="Billing.Accountsbootstrap.TicketRaise" %>

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
<body> <usc:Header ID="Header" runat="server" />
    <form id="form1" runat="server">
     
     <asp:ScriptManager ID="SM" runat="server"></asp:ScriptManager>
    <div>
    <%--Top Section--%>   
   
    <%--Top Section--%>


   <%-- Body Section--%>
    <div class="row col-lg-12">
       <div class="col-lg-12">
         <div class="panel panel-default">
     <div class="panel-heading" style="background-color:#59D3B4">Ticket</div>
     <div class="panel-body">  
       <div class="col-lg-12">   
          <div class="col-lg-4"> 
              <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
              <asp:Label ID="lblempid" runat="server" Visible="false" ></asp:Label>
              <asp:Label ID="lbldept" runat="server" Text="2,3,9" Visible="false" > </asp:Label>
              <asp:Label ID="lbldesg" runat="server" Text="14,16" Visible="false" > </asp:Label>
         <b>Date</b>
       <asp:TextBox ID="txtdate" runat="server" Enabled="false" CssClass="form-control" Width="200px"
       onkeydown="return DateFormat(this, event.keyCode)" Text="--Select Date--"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtdate" runat="server" CssClass="cal_Theme1"> </ajaxToolkit:CalendarExtender>

          <br />
           
             <b> </b><asp:TextBox ID="textid" runat="server" Visible="false" Text='<%= VendorID %>' Enabled="false" CssClass="form-control" ></asp:TextBox> <br />       
             <b>Name: </b><asp:TextBox ID="txtname" runat="server" Text='<%= VendorName %>' Enabled="false" CssClass="form-control" ></asp:TextBox> <br />       
<br />
<b>Phone No: </b>
<asp:TextBox ID="txtphoneno" runat="server" Enabled="false" Text='<%= ContactNumber %>' CssClass="form-control" ></asp:TextBox> <br />       
       <br />

       <b>Ticket No: </b><asp:TextBox ID="txttickect" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox> <br />  
       <b>Subject: </b><asp:TextBox ID="txtSubject" runat="server" Enabled="true" CssClass="form-control" ></asp:TextBox> <br />  
       
              </div>
                <div class="col-lg-4"> 
    
       <b>Concern Person : </b><asp:DropDownList ID="ddlConcern" Enabled="true" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>
        <br /> 
        
         <b>Comment : </b><textarea id="txtcomment" runat="server" class="form-control" style="width:300px; height:150px;"></textarea>
        <br />  
        <b>Priority : </b><asp:DropDownList id="ddlPriorityStatus" runat="server" CssClass="form-control" Width="300px">
              <asp:ListItem Text="High" Value="1"></asp:ListItem>
                <asp:ListItem Text="Medium" Value="2"></asp:ListItem>
                  <asp:ListItem Text="Low" Value="3"></asp:ListItem>
              </asp:DropDownList><br />
              <b>Status : </b><asp:DropDownList id="ddlStatus" runat="server" CssClass="form-control" Width="300px">
              <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                <asp:ListItem Text="Assigned" Value="2"></asp:ListItem>
                  <asp:ListItem Text="In-Progress" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Closed" Value="4"></asp:ListItem>
              </asp:DropDownList><br />
               <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Save" 
               Width="150px" onclick="btnSave_Click" ValidationGroup="val1" />
       <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" 
          Text="Cancel" Width="150px" onclick="btnCancel_Click" />
                         </div>

                         <div class="col-lg-4"> 
                         <asp:Button ID="btnView" runat="server" CssClass="btn btn-info" Text="View All Ticket" 
               Width="150px" PostBackUrl="~/Accountsbootstrap/TicketGrid.aspx" ValidationGroup="val1" />

                         </div>
 </div>
     </div>
     </div>
     </div></div>
     

  
 
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
