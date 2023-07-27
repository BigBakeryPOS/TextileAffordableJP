<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StitchingDetails.aspx.cs" Inherits="Billing.Accountsbootstrap.StitchingDetails" %>

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
    <title>Cutting Info</title>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <script language="javascript" type="text/javascript" src="../js/Validation.js"></script>
    <script type="text/javascript" language="javascript">
        function valchk() {
            if (dropdownchk(document.getElementById('ddlcategory'), "Select Category")
            //&& dropdownchk(document.getElementById('ddlgroup'), "Account Group")  
        && blankchk(document.getElementById('txtdescription'), "Description")) {
                alert("true");
            }
            else {
                alert("false");
                return false;
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
    <script type="text/javascript">
        function myFunction() {
            window.open("http://localhost:57111/Accountsbootstrap/customermaster.aspx?Mode=Vendor", "Popup", 'width=300,height=500,left=100,top=100,resizable=yes,modal=yes,center=yes');
        }
    </script>


    <style type="text/css">
        .img
        {
            width: 160px;
            height: 160px;
            background-position: center center;
            background-size: cover;
            -webkit-box-shadow: 0 0 1px 1px rgba(0, 0, 0, .3);
            display: inline-block;
        }
        </style>

</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    
    <div class="col-lg-12" style="margin-top:6px">
                <h1  class="page-header" style="text-align:center;color:#fe0002;">STITCHING INFORMATION</h1>
            </div>
            <!-- /.col-lg-12 -->
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
               <form id="form1" runat="server" method="post">
               <div class="col-lg-12">
               <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                    ID="val1" ShowMessageBox="true" ShowSummary="false" />
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

               <div class="col-lg-4">

               <div class="form-group" runat="server" visible="false">
                        <label>Today's Date : </label>&nbsp;<asp:Label ID="lblTodaysDate" runat="server"></asp:Label>
                    </div>

                    <div class="form-group">
                        <label>Lot No</label>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="ddlLotNo"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please select Lot No!" Style="color: Red" />
                        <asp:DropDownList ID="ddlLotNo" CssClass="form-control" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="drpwidthChange"></asp:DropDownList>
                    </div>

                    <div class="form-group">
                                        <div style="overflow-y: scroll; width: 459px; height: 170px">
                                            <div class="panel panel-default" style="width: 441px">
                                                <label>
                                                    Fabric Design Number</label>
                                                <asp:CheckBoxList ID="chkinvno" CssClass="chkChoice"
                                                    runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                                    RepeatLayout="Table" Style="overflow: auto">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <%--<asp:Button ID="btnupdate" runat="server" class="btn btn-success" Text="Update" OnClick="Update_Click" />--%>
                                        <%--<asp:Button ID="btnedit" runat="server" class="btn btn-warning" Text="Edit/Delete" OnClick="Edit_Click" />--%>
                                    </div>

                    <div class="form-group">
                        <label>Employee Name</label>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ddlemployename"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please select Employee Name!" Style="color: Red" />
                        <asp:DropDownList ID="ddlemployename" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>

                    <div class="form-group" runat="server" visible="false">
                        <label>Design No</label>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtDesignNo"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please enter Design No!" Style="color: Red" />
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtDesignNo" runat="server" MaxLength="150" style="width:100px"></asp:TextBox>
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtDesignNo1" runat="server" MaxLength="150" style="width:100px; margin-top: -34px;margin-left: 112px;"></asp:TextBox>
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtDesignNo2" runat="server" MaxLength="150" style="width:100px;margin-top: -34px;margin-left: 225px;"></asp:TextBox>
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtDesignNo3" runat="server" MaxLength="150" style="width:100px;margin-top: -34px;margin-left: 340px;"></asp:TextBox>
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtDesignNo4" runat="server" MaxLength="150" style="width:100px;margin-top: -34px;margin-left: 454px;"></asp:TextBox>

                    </div>

                    <div class="form-group" runat="server" visible="false">
                        <label>Operator Name</label>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtOperatorName"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please enter Operator Name!" Style="color: Red" />
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtOperatorName" runat="server" MaxLength="150"></asp:TextBox>
                    </div>

                    <div id="Div1" class="form-group" runat="server" visible="false">
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtEmpCode" runat="server" MaxLength="150"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Quantity</label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" ValidChars=""  TargetControlID="txtQuantity" />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtQuantity"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please enter Quantity!" Style="color: Red" />
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtQuantity" runat="server" MaxLength="150">1</asp:TextBox>
                    </div>
                    <div runat="server" id="lblPendingQuantity" class="form-group">
                        <label>Pending Quantity</label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" ValidChars=""  TargetControlID="txtQuantity" />
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtPendingQuantity"  runat="server" MaxLength="150">0</asp:TextBox>
                    </div>

                   <div class="form-group">
                        <label>Rate Per Quantity</label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom" ValidChars="."  TargetControlID="txtRate" />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtRate"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please enter Rate!" Style="color: Red" />
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtRate" runat="server" MaxLength="150"></asp:TextBox>
                    </div>

                    <div class="form-group" runat="server" visible="false">
                        <label>Selling Price Per Quantity</label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom" ValidChars="."  TargetControlID="txtSellingPrice" />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtSellingPrice"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please enter Selling Price!" Style="color: Red" />
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtSellingPrice" runat="server" MaxLength="150"></asp:TextBox>
                    </div>

               </div>

               <div class="col-lg-4" style="margin-top:27px">

               <div class="form-group">
                        <label>Size</label>
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtItem" runat="server" MaxLength="150"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Range</label>
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtRange" runat="server" MaxLength="150"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Stitching Date</label>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtStitchingDate"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please enter Stitching Date!" Style="color: Red" />
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtStitchingDate" runat="server" MaxLength="150"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtStitchingDate"
                                                runat="server" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                    </div>

                    <div class="form-group">
                        <label>Due Date</label>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtDueDate"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please enter Due Date!" Style="color: Red" />
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtDueDate" runat="server" MaxLength="150"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtDueDate"
                                                runat="server" CssClass="cal_Theme1">
                                            </ajaxToolkit:CalendarExtender>
                    </div>

                    <div class="form-group">
                        <label>Received Date</label>
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtReceivedDate" runat="server" MaxLength="150"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtReceivedDate"
                                                runat="server" CssClass="cal_Theme1"></ajaxToolkit:CalendarExtender>
                    </div>

                    <div class="form-group">
                        <label>Status</label>
                        <asp:DropDownList CssClass="form-control" ID="ddStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_Changed">
                            <asp:ListItem Text="In-Progress" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Completed" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                <div class="form-group" style="margin-top:45px">
                         <label>Shirt</label>
                            <asp:CheckBox id="chckShirt" TextAlign="Right" runat="server" Checked="true"/>
                         <label style="margin-left: 40px;" runat="server" visible="false">Pant</label>
                            <asp:CheckBox id="ChckPant" TextAlign="Right" runat="server" Visible="false"/>
                            <label style="margin-left: 40px;" runat="server" visible="false">Coat</label>
                            <asp:CheckBox id="ChckCoat" TextAlign="Right" runat="server" Visible="false"/>
                            <label style="margin-left: 40px;" runat="server" visible="false">T-Shirt</label>
                            <asp:CheckBox id="ChckTshrt" TextAlign="Right" runat="server" Visible="false"/>
                 </div>

                 <div class="form-group" runat="server" visible="false" >
                        <label>Sample Price Per Quantity</label>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers,Custom" ValidChars="."  TargetControlID="txtSellingPrice" />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtSamplePrice"
                            ValidationGroup="val1" Text="*" ErrorMessage="Please enter Sample Price!" Style="color: Red" />
                        <asp:TextBox CssClass="form-control" TabIndex="4" ID="txtSamplePrice" runat="server" MaxLength="150"></asp:TextBox>
                    </div>

                  <div runat="server" id="lblRemarks" class="form-group">
                                            <label >Remarks</label>
                                            <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" TextMode="MultiLine"
                                                ID="txtRemarks" runat="server"></asp:TextBox>
                                        </div>

               </div>


               <div class="col-lg-4">

               <div class="form-group" runat="server" visible="false">
                      <div class="col-md-5 col-md-offset-4" style="width:100px; height:200px";>
                        <asp:Image ID="imgEmp"  runat="server"  style = "width:160px;height:160px;" />
                      </div>
                   </div>
                   <div class="form-group" runat="server" visible="false">
                      <div class="col-md-5 col-md-offset-4">
                      <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="val1"
                                    Text="*" Style="color: Red" InitialValue="0" ControlToValidate="ddlSignature"
                                    ValueToCompare="Please Select Employees" Operator="NotEqual" Type="String" ErrorMessage="Please Select Employee"></asp:CompareValidator>
                        <asp:DropDownList ID="ddlSignature" AutoPostBack="true" runat="server" Width="154%" 
                                                CssClass="form-control" style="margin-top: -25px;margin-left: -33px;" OnSelectedIndexChanged="ddlSignature_SelectedIndexChanged">
                                            </asp:DropDownList>
                      </div>
                      
                   </div>

                   <div class="form-group" id="Div11" runat="server" visible="false" style="margin-top: 113px;">
                        <label>
                            Sample Image Design No</label>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="210px" CssClass="btn btn-danger" />
                                                    
                                <asp:Button ID="btnUpload1" runat="server" Text="Upload" CssClass="btn btn-danger" OnClick="btnUpload1_Click"
                                    BackColor="" Style="margin-top: 4px" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUpload1" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                   <div id="Div12" runat="server" visible="false">
                        <asp:Image ID="imageSample" CssClass="img" runat="server" />
                        <asp:Label ID="lblSampleImage" runat="server" Visible="false" ></asp:Label>
                    </div>

                    <div class="form-group" id="Div2" visible="false" runat="server" style="margin-top: -270px;margin-left: 229px;">
                        <label>
                            Sample Image Design No 1</label>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:FileUpload ID="FileUpload2" runat="server" Width="210px" CssClass="btn btn-danger" />
                                                    
                                <asp:Button ID="btnUpload2" runat="server" Text="Upload" CssClass="btn btn-danger" OnClick="btnUpload2_Click"
                                    BackColor="" Style="margin-top: 4px" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUpload2" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div id="Div3" runat="server" visible="false" style="margin-left: 230px;">
                        <asp:Image ID="imageSample1" CssClass="img" runat="server" />
                        <asp:Label ID="lblSampleImage1" runat="server" Visible="false" ></asp:Label>
                    </div>

                    <div class="form-group" id="Div4" runat="server" visible="false">
                        <label>
                            Sample Image Design No 2</label>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:FileUpload ID="FileUpload3" runat="server" Width="210px" CssClass="btn btn-danger" />
                                                    
                                <asp:Button ID="btnUpload3" runat="server" Text="Upload" CssClass="btn btn-danger" OnClick="btnUpload3_Click"
                                    BackColor="" Style="margin-top: 4px" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUpload3" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div id="Div5" runat="server" visible="false">
                        <asp:Image ID="imageSample2" CssClass="img" runat="server" />
                        <asp:Label ID="lblSampleImage2" runat="server" Visible="false" ></asp:Label>
                    </div>

                    <div class="form-group" id="Div6" runat="server" visible="false" style="margin-top: -175px;margin-left: 229px;">
                        <label>
                            Sample Image Design No 3</label>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:FileUpload ID="FileUpload4" runat="server" Width="210px" CssClass="btn btn-danger" />
                                                    
                                <asp:Button ID="btnUpload4" runat="server" Text="Upload" CssClass="btn btn-danger" OnClick="btnUpload4_Click"
                                    BackColor="" Style="margin-top: 4px" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnUpload4" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div id="Div7" runat="server" visible="false" style="margin-left: 231px;">
                        <asp:Image ID="imageSample3" CssClass="img" runat="server" />
                        <asp:Label ID="lblSampleImage3" runat="server" Visible="false" ></asp:Label>
                    </div>

               </div>

               </div>
            <!-- /.panel-body -->

               <div class="col-lg-12">
                <div class="form-group">
                  <div class="col-md-5 col-md-offset-4">
                    <asp:Button ID="btnadd" runat="server" class="btn btn-info" ValidationGroup="val1" Text="Save" Style="width: 120px; margin-top: 15px" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnexit" runat="server" class="btn  btn-warning" Style="width: 120px; margin-top: 15px" Text="Exit" OnClick="btnExit_Click" />
                    <asp:Button ID="btnPrint" runat="server" class="btn btn-danger" Visible="false" Text="Print" style="margin-top:15px; width:120px;" OnClick="btnPrint_Data" />
                  </div>
                </div>
                                    
                </div>
               </form>
            </div><!-- /.col-lg-6 (nested) -->
            </div>
        </div>
    <!-- /col-lg-12 (nested) -->
</body>
</html>
