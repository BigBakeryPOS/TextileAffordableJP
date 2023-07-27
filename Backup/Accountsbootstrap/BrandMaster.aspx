<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrandMaster.aspx.cs" Inherits="Billing.Accountsbootstrap.BrandMaster" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>
<html lang="en">

<head>

    <meta content="" charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Brand Registration</title>
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>

    <script type="text/javascript" language="javascript">
        function valchk() {
            if (blankchk(document.getElementById('txtBrandname'), "Brand Name")
            {
                alert("true");
            }
            else {
                alert("false");
                return false;
            }
        }
	</script>
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>

    <!-- MetisMenu CSS -->
    <link href="../css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="../css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="../font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head> 
<body>
<usc:Header ID="Header" runat="server" />
    



 
          <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label" >Welcome : </asp:Label>
                    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
                    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
 

            <div class="row" >
                <div class="col-lg-12">
                    <h1 id="hd1" runat="server" class="page-header"  style="text-align:center;color:#fe0002;" ></h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        
                        <div class="panel-body">
                         <form id="Form1" runat="server">



 <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
                         
                          <asp:scriptmanager id="ScriptManager1" runat="server">
</asp:scriptmanager>
 
                            <div id="add" runat="server" class="row">
                           
                             <div class="col-lg-4"></div>
                                <div class="col-lg-3">
                                    <asp:ValidationSummary runat="server" HeaderText="Validation Messages"  ValidationGroup="val1" ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                        <div class="form-group" id="divcode" runat="server"  >
                                         <asp:TextBox  CssClass="form-control" ID="txtBrandcode" runat="server" Enabled="false"></asp:TextBox>                                              
                                            
                                        </div>
                                        <div class="form-group " >
                                            <label>Brand Name</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" id="reqName" controltovalidate="txtBrandname" errormessage="Please enter Brand name!" style="color:Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtBrandname"  runat="server" ></asp:TextBox>

                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -"  TargetControlID="txtBrandname" />
                                            
                                        </div>
                                        <div class="form-group " runat="server" visible="false">
                                            <label>Brand Code</label>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" Text="*" id="RequiredFieldValidator2" controltovalidate="txtCode" errormessage="Please enter Brand!" style="color:Red" />
                                            <asp:TextBox CssClass="form-control" ID="txtCode"  runat="server" ></asp:TextBox>

                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" -"  TargetControlID="txtCode" />
                                            
                                        </div>

                                        <div class="form-group">
                                                <label>Fit</label>
                                                  <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="ddlFit" ValidationGroup="val1" Text="*" errormessage="Please Select IsActive!" style="color:Red" />
                                 <asp:DropDownList ID="ddlFit" runat="server" class="form-control"></asp:DropDownList>
                                           
                                        </div>	

                                        <div class="form-group">
                                        <div style="overflow-y: scroll; width: 284px; height: 170px">
                                            <div class="panel panel-default" style="width: 265px">
                                                <label>
                                                    Size Number</label>
                                                <asp:CheckBoxList ID="chkSize" CssClass="chkChoice"
                                                    runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                                    RepeatLayout="Table" Style="overflow: auto">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                                <label>Size Type</label>
                                                  <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="dllsizetype" ValidationGroup="val1" Text="*" errormessage="Please Select Size Type!" style="color:Red" />
                                 <asp:DropDownList ID="dllsizetype" runat="server" class="form-control">
                                 <asp:ListItem Text="T-Shirts/Shirts" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="Pants" Value="2"></asp:ListItem>
                                   <asp:ListItem Text="Chudidhar" Value="3"></asp:ListItem>
                                 </asp:DropDownList>
                                           
                                        </div>	  

                                          <div class="form-group">
                                                <label>IsActive</label>
                                                  <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="ddlIsActive" ValidationGroup="val1" Text="*" errormessage="Please Select IsActive!" style="color:Red" />
                                 <asp:DropDownList ID="ddlIsActive" runat="server" class="form-control">
                                 <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                   <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                 </asp:DropDownList>
                                           
                                        </div>	    
                                        
                                        <%--<div class="form-group input-group">

                                            <label>Heading</label>
                                            <asp:DropDownList ID="ddlHeadingType" runat="server" class="form-control" 
                                                ></asp:DropDownList>
                                           
                                        </div>--%>
                                     
                                    
                                         <asp:Label ID="lblerror" runat="server" style="color:Red"></asp:Label>
										<asp:Button ID="btnadd" runat="server" class="btn btn-info" Text="Save" OnClick="Add_Click" ValidationGroup="val1"  style="width:120px;"/>
                                        <asp:Button ID="btnexit" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"  style="width:120px;"/>
                                    
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                                <div class="col-lg-4">
                                    
										
                                        <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
										<%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                                        
										
                                </div>
                              
                                <!-- /.col-lg-6 (nested) -->
                                <!-- /.col-lg-6 (nested) -->
                            </div>

                               <div id="div1" runat="server" align="center">
                        <table cellpadding="1" cellspacing="2" width="450px" style="border: 1px solid blue; height:150px;">
                            <tr class="headerPopUp">
                                <td id="Td1" runat="server">
                                  
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width: 100%">
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
                                        <tr>
                                            <td colspan="3">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 30%">
                                                        </td>
                                                        <td align="center" style="width: 35%">
                                                            <asp:Button ID="btnUpload" runat="server" class="btn btn-info" Height="31px" 
                                                                OnClick="btnUpload_Click" Text="Upload" Width="100px" />
                                                        </td>
                                                        <td style="width: 35%">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 15%">
                                                        </td>
                                                        <td style="width: 70%" align="center">
                                                            <asp:Button ID="Button2" runat="server" class="btn btn-info" Text="Download the Sample Excel Format"
                                                                Height="31px" OnClick="btnFormat_Click" />
                                                                 <asp:Button ID="Button1" runat="server" class="btn btn-warning" Text="Exit" OnClick="Exit_Click"/>
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


          </ContentTemplate>
          <Triggers>

    

          </Triggers>
      </asp:UpdatePanel>


      <asp:UpdateProgress ID="Updateprogress" runat="server" AssociatedUpdatePanelID="Updatepanel1">
      <ProgressTemplate>
<div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; opacity: 0.7;">


                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                    position: fixed; top: 50%; left: 40%;" />


                    </div>
      </ProgressTemplate>
      </asp:UpdateProgress>



                              </form>
                            <!-- /.row (nested) -->
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
       
        <!-- /#page-wrapper -->
		
		
		
		<!-- jQuery -->
   

</body>

</html>
