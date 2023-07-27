<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditNote.aspx.cs" Inherits="Billing.Accountsbootstrap.CreditNote"%>
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
    <title>Credit Note </title>
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

         <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="background-color: #336699; color: White; border-color: #06090c">
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblName" Text="Credit Note " runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                             <asp:ValidationSummary runat="server" HeaderText="Validation Messages"  ValidationGroup="val1" ID="val1" ShowMessageBox="true" ShowSummary="false" />
                               <div class="col-lg-2">
                               </div>

                                <div class="col-lg-3">
                                  <div class="form-group">
                                            <label>Note No</label>
                                             <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="Noteno" controltovalidate="txtxNoteno" Text="*" errormessage="Please Enter Note no!" style="color:Red" ></asp:RequiredFieldValidator>
                                            <asp:TextBox CssClass="form-control" ID="txtxNoteno" MaxLength="15" runat="server" Enabled="false" ></asp:TextBox>
                                             
                                           
                                        </div>
                                        
                                        	<div class="form-group">
                                            <label>Ledger Name</label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"  Text="*" style="color:Red" InitialValue="0"
                          ControlToValidate="ddlLname" ValueToCompare="Select Ledger" Operator="NotEqual"  Type="String"   errormessage="Please select Ledger name!"></asp:CompareValidator>

                                            <asp:DropDownList ID="ddlLname"  Width="305px" Height="70px" CssClass="chzn-select" AutoPostBack="true"
                                                    runat="server" class="form-control" 
                                                    onselectedindexchanged="ddlLname_SelectedIndexChanged">
                                                </asp:DropDownList>
                                        </div>
                                
                              
                                    <div class="form-group">
                                            <label>Date</label>
                                                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                                  <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="RequiredFieldValidator1" controltovalidate="txtDCDate" Text="*" errormessage="Please Enter the Date!" style="color:Red" ></asp:RequiredFieldValidator>
                                           <asp:TextBox ID="txtDCDate"  runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDCDate" Format="dd/MM/yyyy" runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                                        	
                                        </div>
                                       </div>
                                          <div class="col-lg-1">
                                          </div>
                                             <div class="col-lg-3">
                                       
                                       <div class="form-group"  runat="server" visible="false">
                                            <label>Credit or Debit Note</label>
                                                <div class="panel panel-default" style="height:37px">
                        
                        <div class="panel-body">
                                           
                                           <asp:RadioButtonList ID="RbtnCD" runat="server" Enabled="false" RepeatDirection="Horizontal"  style="margin-top:-8px;">     
                                           <asp:ListItem Text="Credit" Value="Credit Note" Selected="True"> </asp:ListItem>
                                            <asp:ListItem Text="Debit" Value="Debit Note">  </asp:ListItem>
                                           </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        </div>


<%--
                                         <div class="form-group" >
                                            <label>Credit or Debit Note</label>
                                           
                                           <asp:RadioButtonList ID="RbtnCD" runat="server" RepeatDirection="Horizontal" style="margin-top:7px">     
                                           <asp:ListItem Text="Credit" Value="Credit Note" Selected="True"> </asp:ListItem>
                                            <asp:ListItem Text="Debit" Value="Debit Note"> </asp:ListItem>
                                           </asp:RadioButtonList>
                                            
                                        </div>--%>
										
                                        <div class="form-group">
                                            <label>Amount</label>
                                             <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="phono" Text="*" controltovalidate="txtAmount" errormessage="Please enter your Amount !" style="color:Red" />
                                               <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Numbers,custom" ValidChars="."
                                    TargetControlID="txtAmount" />
                                            <asp:TextBox CssClass="form-control" ID="txtAmount" MaxLength="10" AutoPostBack="true"
                                                runat="server" ontextchanged="txtAmount_TextChanged"></asp:TextBox>
                                           
                                        </div>
                                       

                                        <div class="form-group">
                                            <label>Narration</label>
                                             <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" id="area" controltovalidate="txtNar" Text="*" errormessage="Please enter your commends!" style="color:Red" />
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" +,!@#$%^&*()-/:;."  TargetControlID="txtNar" />
                                             <asp:TextBox CssClass="form-control" ID="txtNar" MaxLength="150" runat="server"></asp:TextBox>
                                           
                                        </div>
                                        <asp:Label ID="lblMaxID" runat="server"></asp:Label>
                                        
									<%-- <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>
										<asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save"  
                                            ValidationGroup="val1" style="width:120px;" onclick="btnadd_Click" /> 
                                        <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit"  
                                            style="width:120px;" onclick="btnexit_Click" /> --%>
                                    

                              </div>




                              <div class="row">
                                        <br />
  <div class="col-lg-12">
                       <div class="col-lg-2">
                         </div>
                              
                                   <div class="col-lg-8">
                        <div style="margin-top:20px" class="table-responsive">
                            <asp:GridView ID="TransPaymentGrid" runat="server" EmptyDataText="No records Found" Width="87%" 
                               AllowPaging="true" AutoGenerateColumns="false" CssClass="myGridStyle" style="margin-right:200px"
                                AllowSorting="true">
                                <HeaderStyle BackColor="#59d3b4" Height="30px" ForeColor="Black" />
                                <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black" Height="30px" />
                                <PagerSettings FirstPageText="1" Mode="Numeric" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Invoice No">
                                        <ItemTemplate>
                                            <asp:Label ID="txtDCNo" runat="server" Text='<%# Eval("DC_NO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill No">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBillNo" runat="server" Text='<%# Eval("Bill_NO")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Date">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBillDate" runat="server" Text='<%# Eval("DC_Date")%>' DataFormatString="{0:dd/MM/yyyy}"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBillAmount" runat="server" Text='<%# Eval("BillAmount")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBalance" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#59d3b4" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#59d3b4" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            </asp:GridView>
                            <%--</div>
                                 <div class="col-lg-4" >
                                          </div>--%>
                        </div>
                     
                    
                 
                          
                           





                        
                                      
                                                <!-- /.panel-heading -->
                                                <div style="padding-left:10px; margin-top:-20px" class="table-responsive" >
                                                    <asp:GridView ID="gvledgrid" runat="server" EmptyDataText="No records Found" Width="86.5%" 
                                                        AllowPaging="true" AutoGenerateColumns="false" CssClass="myGridStyle" AllowSorting="true">
                                                        <HeaderStyle BackColor="#59d3b4" Height="30px" ForeColor="Black" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#59d3b4" ForeColor="Black"  Height="30px"/>
                                                        <PagerSettings FirstPageText="1" Mode="Numeric" />
                                                        <Columns>
                                                        <asp:TemplateField HeaderText="SalesID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtSalesid" runat="server" Text='<%# Eval("SalesID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bill No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBillNo" runat="server" Text='<%# Eval("BillNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bill Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBillDate" runat="server" Text='<%# Eval("BillDate")%>' DataFormatString="{0:dd/MM/yyyy}"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bill Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBillAmount" runat="server" Text='<%# Eval("BillAmount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtBalance" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#59d3b4" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <HeaderStyle BackColor="#59d3b4" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    </asp:GridView>
                                                   
                                                </div>
                                              <br />
                        <div style="text-align: center; margin-top: 20px; margin-left:0px">
                            <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                            <asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" ValidationGroup="val1"
                                Style="width: 120px;" onclick="btnadd_Click"  />
                            <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" Style="width: 120px"
                              onclick="btnexit_Click" />
                        </div>
                                           
                                     </div>
                                     </div>
                                     </div>




                      

                  
              
                  </div>


                    </ContentTemplate>
                </asp:UpdatePanel>
                        </div></div></div>
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
