<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JournalScreen.aspx.cs" Inherits="Billing.Accountsbootstrap.JournalScreen" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Journal Entry</title>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
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


    <script type = "text/javascript">
        var isShift = false;
        var seperator = "/";
        function DateFormat(txt, keyCode) {
            if (keyCode == 16)
                isShift = true;
            //Validate that its Numeric
            if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 ||
         keyCode <= 37 || keyCode <= 39 ||
         (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
                if ((txt.value.length == 2 || txt.value.length == 5) && keyCode != 8) {
                    txt.value += seperator;
                }
                return true;
            }
            else {
                return false;
            }
        }
 </script>

</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" Visible="false">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="black" CssClass="label" Visible="false">Welcome: </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="black" CssClass="label" Visible="false"> </asp:Label>
    <form runat="server" id="form1" method="post">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
                                       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   
        
            <div class="row">
            <div class="col-lg-12">
            </div>
                <div class="col-lg-12" style="margin-top:7px">
                                <h2 class="page-header"  style="text-align:left;color:#fe0002;">Journal</h2>
                                 </div>
                    <div class="panel panel-default">
                        <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel-body">
                                    <div >

                                       <%-- <asp:Label ID="lblError" runat="server" Style="color: Red"></asp:Label>--%>
                                        <table class="table table-striped table-bordered table-hover" id="dataTables-example" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GridView2" AutoGenerateColumns="False" ShowFooter="True" 
                                                       CssClass="myGrid" GridLines="None" Width="100%"   OnRowDataBound="GridView2_RowDataBound"
                                                          OnRowDeleting="GridView2_RowDeleting"
                                                        runat="server">
                                                   
                                                        <Columns>
                                                         <asp:TemplateField HeaderText="J.V No" >
                                                                <ItemTemplate>
                                                                 <%# Container.DataItemIndex + 1 %>
                                                                    <asp:TextBox ID="txtJvno" MaxLength="7" Text="1" style="display:none"  runat="server" Width="60px"></asp:TextBox>
                                                                      <ajaxToolkit:filteredtextboxextender id="FilteredTextBoxExtender3121" runat="server" targetcontrolid="txtJvno"
                                                                        validchars="" filtertype="Numbers" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Date" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDate"   runat="server" Width="78px" onkeyup = "ValidateDate(this, event.keyCode)"
 onkeydown = "return DateFormat(this, event.keyCode)"
 ></asp:TextBox>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" PopupButtonID="txtDate"
                                        EnabledOnClient="true" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                                                </ItemTemplate>
                                                                
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="	Debtor" >
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlDebtor" runat="server" Width="140px" Height="26px"
                                                                        AppendDataBoundItems="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Creditor" HeaderStyle-Width="19%" ItemStyle-Width="19%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlCreditor" runat="server"  Height="26px" Width="140px"
                                                                        AppendDataBoundItems="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Narration"  >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNarration"  runat="server" Width="110px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Amount" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount"  MaxLength="10" runat="server" Width="110px">
                                                                    </asp:TextBox>
                                                                    <ajaxToolkit:filteredtextboxextender id="FilteredTextBoxExtender312" runat="server" targetcontrolid="txtAmount"
                                                                        validchars="." filtertype="Numbers, Custom" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="PayMode" HeaderStyle-Width="19%" ItemStyle-Width="19%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlpaymentmode" runat="server"  Height="26px" Width="90px" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged"
                                                                        AutoPostBack="true" >
                                                                  
                                                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                                    <asp:ListItem Text="Cheque" Value="Cheque"></asp:ListItem>
                                                    <%--<asp:ListItem Text="DD" Value="DD"></asp:ListItem>--%>
                                                    <asp:ListItem Text="Online" Value="Online"></asp:ListItem>
                                                    <asp:ListItem Text="Card" Value="Atm"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                 <%--   OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged"--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Bank Name" HeaderStyle-Width="19%" ItemStyle-Width="19%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlBankname" runat="server"  Height="26px" Width="130px"
                                                                        AppendDataBoundItems="true">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="	Cheque/DD No"  >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtChequeno" MaxLength="10" runat="server" Width="110px" Height="26px"></asp:TextBox>
                                                                      <ajaxToolkit:filteredtextboxextender id="FilteredTextBoxExtender3" runat="server" targetcontrolid="txtChequeno"
                                                                        validchars="" filtertype="Numbers" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField  HeaderText="Against Bill No">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAganistBno" MaxLength="5" Width="100px" runat="server"></asp:TextBox>
                                                                    <ajaxToolkit:filteredtextboxextender id="FilteredTextBoxExtender345" runat="server" targetcontrolid="txtAganistBno"
                                                                        validchars="" filtertype="Numbers" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                          
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                <asp:Button ID="ButtonAdd1" runat="server" AutoPostback="false" EnableTheming="false"
                                                                        Text="Add New" OnClick="ButtonAdd1_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField  ShowDeleteButton="True" ButtonType="Button" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                   
                                    </div>
                                    <asp:Button ID="btnadd" Text="Save" runat="server" class="btn btn-info" 
                                        Width="120px" onclick="btnadd_Click" />
                                    <asp:Button ID="btnExit" Text="Exit" runat="server" class="btn btn-warning" 
                                        Width="120px" onclick="btnExit_Click" />
                                </div>
                            </div>
                        </div>
                          </div>
                    </div>
                    
                              
                               </div>
                               </ContentTemplate>
                    </asp:UpdatePanel>           
    

    </form>
</body>
</html>
