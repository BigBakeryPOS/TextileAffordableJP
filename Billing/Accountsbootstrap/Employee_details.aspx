<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee_details.aspx.cs"
    Inherits="Billing.Accountsbootstrap.Employee_details" %>

<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Employee_details - bootsrap</title>
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <script src="" type="text/javascript"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <link rel="Stylesheet" type="text/css" href="Styles/style1.css" />
    <style type="text/css">
        .button-success, .button-error, .button-warning, .button-secondary
        {
            color: white;
            border-radius: 7px;
            text-shadow: 0 7px 5px rgba(0, 0, 0, 0.2);
        }
        
        .button-success
        {
            background: rgb(28, 184, 65); /* this is a green */
        }
        
        .button-error
        {
            background: rgb(202, 60, 60); /* this is a maroon */
        }
        
        .button-warning
        {
            background: rgb(223, 117, 20); /* this is an orange */
        }
        
        .button-secondary
        {
            background: rgb(66, 184, 221); /* this is a light blue */
        }
        
        
        
        
        .index1
        {
            text-align: center;
            font-size: 28px;
            font-weight: bold;
            background-color: orange;
            padding-top: 10px;
            padding-bottom: 10px;
            margin-left: 525px;
            margin-right: 525px;
            font-family: Californian FB;
        }
        .button1
        {
            margin-left: 70px;
        }
        .pad
        {
            padding left:300px;
        }
    </style>
    <script type="text/javascript">
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
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="../images/fav.ico" type="image/x-icon" rel="Shortcut Icon" />
    <!-- Bootstrap Core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
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
    <script language="javascript" type="text/javascript">
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 46))
                event.returnValue = true;

            else

                event.returnValue = false;

        }
        function validate() {

            var Employee_Id = document.getElementById("txtemployid").value;
            var Employee_code = document.getElementById("txtemploycode").value;
            var Employee_Name = document.getElementById("txtname").value;
            var Date_of_Birth = document.getElementById("txtdob").value;
            var Address = document.getElementById("txtaddress").value;

            var Mobile_No = document.getElementById("txtphno").value;
            var patterphno = /^[\s()+-]*([0-9][\s()+-]*){10,10}$/;



            var Service = document.getElementById("ddlservice").value;
            var Desigination = document.getElementById("ddldesignation").value;

            var Department = document.getElementById("txtDepartment").value;

            var pattern1 = /^-?[0-9]+(.[0-9]{1,6})?$/;
            var pattern2 = /^-?[0-9]+([0-9]{1,6})?$/;
            var cat = document.getElementById("txtEmpCategory").value;
            var esino = document.getElementById("txtESINO").value;
            //            var Email = document.getElementById("txtemail").value;
            //            var regexmail = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

            var Password = document.getElementById("txtpwd").value;
            var pattern3 = /^(?=.*\d)(?=.*[a-z])[a-z\d]{2,}$/i;

            //         var pattern1 = /^(?=.*\d)(?=.*[a-z])[a-z\d]{2,}$/i;
            //         var pattern3 = /^.*(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\d\W]).*$/;
            //         var pattern3 = ^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20};
            //         var pattern3 = (?=^.{6,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*;

            var Documents_Submitted = document.getElementById("txtDocumentsSubmitted").value;

            if (Employee_Id == "") {
                alert("Enter Customer Name.");

                return false;
            }
            if (Employee_code == "") {
                alert("Enter employee code.");

                return false;
            }



            if (Employee_Name == "") {
                alert("Enter Employee Name");

                return false;
            }

            if (Date_of_Birth == "-Select Date-") {
                alert("Enter Your age.");
                return false;
            }

            if (Address == "") {
                alert("Enter your Address.");
                return false;
            }


            if (!patterphno.test(Mobile_No)) {
                alert("It is not valid mobile number");
                return false;
            }
            if (cat == "") {
                alert("enter cat.");
                return false;
            }
            if (Department == "") {
                alert("enter Department");
                return false;
            }

            if (Desigination == "") {
                alert("Enter Your desigination");
                return false;
            }

        }
        
        
    </script>
</head>
<body>
    <div>
        <usc:Header ID="Header" runat="server" />
        <div class="row">
            <div class="col-lg-12">
                <br />
                <h2 class="page-header">
                    Employee Master</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <form id="Form2" runat="server">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:ValidationSummary runat="server" HeaderText="Validation Messages" ValidationGroup="val1"
                                    ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" />
                                <%-- <asp:UpdatePanel ID="Panel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                <div class="col-lg-3">
                                    <div id="Div2" class="form-group col-lg-4" runat="server" visible="false">
                                        <label>
                                            Employee id</label>
                                        <asp:TextBox CssClass="form-control" ID="txtemployid" MaxLength="60" runat="server"
                                            Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Employee Code</label>
                                        <asp:TextBox CssClass="form-control" ID="txtemploycode" MaxLength="60" AutoPostBack="true"
                                            runat="server" OnTextChanged="txtemploycode_TextChanged"></asp:TextBox>
                                        <asp:Label ID="lblerror" runat="server" Style="color: Red"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Employee Name</label>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator1"
                                            ControlToValidate="txtname" ErrorMessage="Please enter Employee Name!" Text="*"
                                            Style="color: White" />
                                        <asp:TextBox CssClass="form-control" ID="txtname" MaxLength="60" Width="100%" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Date of Birth:</label>
                                        <asp:TextBox ID="txtdob" runat="server" CssClass="form-control" onkeyup="ValidateDate(this, event.keyCode)"
                                            onkeydown="return DateFormat(this, event.keyCode)"> </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdob" runat="server"
                                            Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="phono" ControlToValidate="txtdob"
                                            ErrorMessage="Please enter your DOB!" Text="*" Style="color: White" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Date of Joining:</label>
                                        <asp:TextBox ID="txtdoj" runat="server" Text="-Select Date-" CssClass="form-control"> </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdoj" runat="server"
                                            Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                    </div>
                                     <div class="form-group">
                                        <label>
                                            Is Father/Husband</label>
                                        <asp:DropDownList runat="server" ID="ddlFather" Width="100%" CssClass="form-control">
                                            <asp:ListItem Text="Father" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Husband" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Father/Husband Name</label>
                                        <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" ID="txtfathername"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Marital Status</label>
                                        <asp:DropDownList runat="server" ID="ddlMaritalStatus" Width="100%" CssClass="form-control">
                                            <asp:ListItem Text="Married" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="UnMarried" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Is Mother/Wife</label>
                                        <asp:DropDownList runat="server" ID="ddlIsMotherWife" Width="100%" CssClass="form-control">
                                            <asp:ListItem Text="Mother" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Wife" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Mother/Wife Name</label>
                                        <asp:TextBox ID="txtMotherWife" runat="server" CssClass="form-control"> </asp:TextBox>
                                        <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator6"
                                            ControlToValidate="txtMotherWife" ErrorMessage="Please Enter Mother/Wife Name."
                                            Text="*" Style="color: White" />--%>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            Current Address</label>
                                        <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" TextMode="MultiLine"
                                            ID="txtaddress" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Permanent Address</label>
                                        <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" TextMode="MultiLine"
                                            ID="txtPermanentAddress" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Mobile No</label>
                                        <asp:TextBox CssClass="form-control" MaxLength="10" Width="100%" ID="txtphno" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Alternative Mobile No</label>
                                        <asp:TextBox CssClass="form-control" MaxLength="10" Width="100%" ID="txtaltphono"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Units</label>
                                        <asp:DropDownList ID="ddlUnit" runat="server" Width="100%" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                     <div class="form-group">
                                        <label>
                                            OT</label>
                                        <asp:DropDownList runat="server" ID="ddlOT" Width="100%" CssClass="form-control">
                                            <asp:ListItem Text="Allow" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Not Allow" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                      <div class="form-group">
                                        <label>
                                            UnActive Date:</label>
                                        <asp:TextBox ID="txtUnactivedate" runat="server" CssClass="form-control" onkeyup="ValidateDate(this, event.keyCode)"
                                            onkeydown="return DateFormat(this, event.keyCode)"> </asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtUnactivedate" runat="server"
                                            Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </ajaxToolkit:CalendarExtender>
                                      
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>
                                            Salary Type</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlSalary"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Select Salary Type!" Style="color: Red" />
                                        <asp:DropDownList runat="server" ID="ddlSalary" Width="100%" CssClass="form-control">
                                            <asp:ListItem Text="Daily Wages" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Monthly" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Piece Wise" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Salary Amount</label>
                                        <asp:TextBox ID="txtSalaryAmount" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="val1" ID="RequiredFieldValidator5"
                                            ControlToValidate="txtSalaryAmount" ErrorMessage="Please Enter Salary Amount."
                                            Text="*" Style="color: White" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender313ds" runat="server"
                                            TargetControlID="txtSalaryAmount" FilterType="Numbers,Custom" ValidChars="." />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Department
                                        </label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddldepartment"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Select Department!" Style="color: Red" />
                                        <asp:DropDownList ID="ddldepartment" runat="server" Width="100%" AutoPostBack="false"
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Desigination
                                        </label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddldesignation"
                                            ValidationGroup="val1" Text="*" ErrorMessage="Please Select Desigination!" Style="color: Red" />
                                        <asp:DropDownList ID="ddldesignation" runat="server" Width="100%" AutoPostBack="false"
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Email</label>
                                        <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" ID="txtemail" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Check For User Login</label><br />
                                        <asp:CheckBox ID="chklogin" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Check For Ticket Approval</label><br />
                                        <asp:CheckBox ID="chkticketapproval" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Upload Photo
                                            </label>
                                            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="fp_Upload" runat="server" />
                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-warning"
                                                        OnClick="btnUpload_OnClick" Width="100px" />
                                                    <asp:Image ID="img_Photo" runat="server" Width="100px" BorderColor="1" />
                                                    <asp:Label ID="lblfilename" runat="server" Visible="true"></asp:Label>
                                                    <asp:Label ID="lblFile_Path" runat="server" Visible="false"></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Upload Document 1
                                            </label>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                    <asp:Button ID="Button1" runat="server" Text="Upload" CssClass="btn btn-warning"
                                                        OnClick="btnUpload1_OnClick" Width="100px" />
                                                    <asp:Image ID="Image1" runat="server" Width="100px" BorderColor="1" />
                                                    <asp:Label ID="lblfilename2" runat="server" Visible="true"></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="Button1" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Upload document 2
                                            </label>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="FileUpload2" runat="server" />
                                                    <asp:Button ID="Button2" runat="server" Text="Upload" CssClass="btn btn-warning"
                                                        OnClick="btnUpload2_OnClick" Width="100px" />
                                                    <asp:Image ID="Image2" runat="server" Width="100px" BorderColor="1" />
                                                    <asp:Label ID="lblfilename3" runat="server" Visible="true"></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="Button2" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-group ">
                                            <label>
                                                Status</label>
                                            <asp:DropDownList runat="server" ID="ddlStatus" Width="100%" AutoPostBack="true"
                                                CssClass="form-control">
                                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="UnActive" Value="2"></asp:ListItem>
                                                <%--<asp:ListItem Text="Abscond" Value="3"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        Branches</label>
                                    <asp:DropDownList ID="ddlbranches" runat="server" Width="100%" AutoPostBack="true"
                                        CssClass="form-control" OnSelectedIndexChanged="ddlbranches_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-4">
                                </div>
                                <div id="Div1" class="form-group col-lg-4" runat="server" visible="true">
                                </div>
                                <div class="form-group col-lg-4">
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        Service</label>
                                    <asp:DropDownList ID="ddlservice" runat="server" Width="100%" AutoPostBack="true"
                                        CssClass="form-control" OnSelectedIndexChanged="ddlservice_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="true">
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        Job Type</label>
                                    <asp:DropDownList ID="ddljobtype" runat="server" Width="100%" AutoPostBack="true"
                                        CssClass="form-control" OnSelectedIndexChanged="ddljobtype_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        Salary</label>
                                    <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" ID="txtsalary" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        Annual salary</label>
                                    <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" ID="txtannulasal"
                                        runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        P.F.NO</label>
                                    <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" ID="txtpfno" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        E.S.I.NO</label>
                                    <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" ID="txtESINO" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4">
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        Password</label>
                                    <asp:TextBox CssClass="form-control" TextMode="Password" MaxLength="60" Width="100%"
                                        ID="txtpwd" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        Documents Submitted</label>
                                    <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" ID="txtDocumentsSubmitted"
                                        runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <label>
                                        Date of Leaving</label>
                                    <asp:TextBox CssClass="form-control" Visible="true" MaxLength="60" Width="100%" ID="ttDateofLeaving"
                                        runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="ttDateofLeaving"
                                        runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </ajaxToolkit:CalendarExtender>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <asp:Label runat="server" ID="lblcontract">Desigination</asp:Label>
                                    <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" ID="txtDepartment"
                                        runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4" runat="server" visible="false">
                                    <asp:Label runat="server" ID="Label1">Employee Category</asp:Label>
                                    <asp:TextBox CssClass="form-control" MaxLength="60" Width="100%" ID="txtEmpCategory"
                                        runat="server"></asp:TextBox>
                                </div>
                                <div style="padding-left: 100px">
                                    <div>
                                        <div class="btn-group col-lg-1">
                                            <asp:Button ID="btnsubmit" Style="width: 100px" runat="server" CssClass="btn btn-success"
                                                Text="SUBMIT" ValidationGroup="val1" OnClick="btnsubmit_Click1" />
                                        </div>
                                        <div class="form-group col-lg-1">
                                            <asp:Button ID="btnReset" Style="width: 100px" runat="server" CssClass="btn btn-danger "
                                                Text="Exit" OnClick="btnReset_Click" />
                                        </div>
                                    </div>
                                </div>
                                <%-- </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="Updateprogress" runat="server" AssociatedUpdatePanelID="Panel1">
                                    <ProgressTemplate>
                                        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                                            right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                                            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../images/01-progress.gif"
                                                AlternateText="Loading ..." ToolTip="Loading ..." Style="width: 150px; padding: 10px;
                                                position: fixed; top: 50%; left: 40%;" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>--%>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
