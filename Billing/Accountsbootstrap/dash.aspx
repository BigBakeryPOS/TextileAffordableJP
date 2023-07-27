<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dash.aspx.cs" Inherits="Billing.Accountsbootstrap.dash" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register TagPrefix="usc" TagName="Header" Src="~/HeaderMaster/Header.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="System.Web.DataVisualization" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="Stylesheet" type="text/css" href="../css/date.css" />
    <link href="../Styles/style1.css" rel="stylesheet" />
    <script src="../jqueryCalendar/script.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-1.6.2.min.js"></script>
    <script type="text/javascript" src="../jqueryCalendar/jquery-ui-1.8.15.custom.min.js"></script>
    <link rel="stylesheet" href="../jqueryCalendar/jqueryCalendar.css" />
    <title>Dash Board</title>
</head>
<body>
    <usc:Header ID="Header" runat="server" />
    <asp:Label runat="server" ID="lblWelcome" ForeColor="White" CssClass="label">Welcome : </asp:Label>
    <asp:Label runat="server" ID="lblUser" ForeColor="White" CssClass="label"> </asp:Label>
    <asp:Label runat="server" ID="lblUserID" ForeColor="White" CssClass="label" Visible="false"> </asp:Label>
    <form id="form1" runat="server">
    <div>
        <div class="row">
       
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-4">
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group ">
                                    <label>
                                        Screen</label>
                                    <asp:DropDownList ID="ddlDD" runat="server" class="form-control" OnSelectedIndexChanged="ddlDD_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="Sales" Value="Sales"></asp:ListItem>
                                        <asp:ListItem Text="Purchase" Value="Purchase"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group ">
                                    <label>
                                        Company</label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                        <div class="col-lg-2">
                            </div>
                                <div class="table-responsive" >
                            <div class="col-lg-10">
                                  
                                <asp:Chart ID="Chart1" runat="server" Width="900px" Height="300px">
                                    <Titles>
                                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Sales - Socks (NR)"
                                            Text="">
                                        </asp:Title>
                                    </Titles>
                                    <Series>
                                        <asp:Series Name="Series1" Color="CadetBlue" Font="Times New Roman, 10pt, style=Bold"
                                            IsValueShownAsLabel="true" IsVisibleInLegend="false" LegendText="Overall">
                                        </asp:Series>
                                    </Series>
                                    <Legends>
                                        <asp:Legend Name="Admin">
                                        </asp:Legend>
                                    </Legends>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <AxisX Interval="1" IntervalType="Number" ArrowStyle="Triangle">
                                                <MajorGrid LineWidth="0" />
                                                <MinorGrid />
                                            </AxisX>
                                            <AxisY IsStartedFromZero="true" IsLabelAutoFit="true" ArrowStyle="Triangle">
                                                <MajorGrid LineWidth="0" />
                                            </AxisY>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                            </div>
                            
                        </div>
                      
                        <div class="row">
                        <div class="col-lg-2">
                            </div>
                              <div class="table-responsive" >
                        <div class="col-lg-10">
                                <asp:Chart ID="Chart2" runat="server" Width="900px" Height="300px">
                                    <Titles>
                                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Sales - Socks (NR)"
                                            Text="">
                                        </asp:Title>
                                    </Titles>
                                    <Series>
                                        <asp:Series Name="Series123">
                                        </asp:Series>
                                    </Series>
                                    <Legends>
                                        <%--<asp:Legend Name="Admin"></asp:Legend>
                </Legends>--%>
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                            LegendStyle="Row" />
                                    </Legends>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea123">
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        </div>
                        </div>
                        <div class="row">
                        <div class="col-lg-2">
                            </div>
                              <div class="table-responsive" >
                            <div class="col-lg-10">
                                <asp:Chart ID="Chart3" runat="server" Width="900px" Height="300px">
                                    <Titles>
                                        <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Sales - Socks (NR)"
                                            Text="">
                                        </asp:Title>
                                    </Titles>
                                    <Series>
                                        <asp:Series Name="Ser1" Color="CadetBlue" Font="Times New Roman, 10pt, style=Bold"
                                            IsValueShownAsLabel="true" IsVisibleInLegend="false" LegendText="Overall">
                                        </asp:Series>
                                    </Series>
                                    <Legends>
                                        <asp:Legend Name="Admin">
                                        </asp:Legend>
                                    </Legends>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartA1">
                                            <AxisX Interval="1" IntervalType="Number" ArrowStyle="Triangle">
                                                <MajorGrid LineWidth="0" />
                                                <MinorGrid />
                                            </AxisX>
                                            <AxisY IsStartedFromZero="true" IsLabelAutoFit="true" ArrowStyle="Triangle">
                                                <MajorGrid LineWidth="0" />
                                            </AxisY>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                            </div>
                            
                        </div>
                                            </div>
                </div>
            </div>
            </div>
        </div>
  
    </form>
</body>
</html>
