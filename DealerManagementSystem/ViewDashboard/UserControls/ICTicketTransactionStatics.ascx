<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketTransactionStatics.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.ICTicketTransactionStatics" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<div style="display:none">
    <div class="ErrorRed" id="divError" runat="server" visible="false">
        <span id="errorMessage" runat="server"></span>
        <img alt="" src="../images/error_red.gif" />
    </div>
    <%--  CustomProperties="PieLabelStyle=Outside, PieStartAngle=270" --%>
          <%--  290px--%>
</div>
<div class="tile-size-three grid-item">
    <div class="content">
        <asp:Literal ID="ucTitle" runat="server" Text="ICTicket Transaction Statistics"></asp:Literal>
      <%--   <div class="dashboardGrid">--%>
            <asp:Chart ID="Chart1" runat="server">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie"
                        CustomProperties="PieLabelStyle=Disabled , PieStartAngle=270"
                        ChartArea="ChartArea2"  >
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea2"                        
                        AlignmentOrientation="All"
                        BackColor="Transparent">
                        <InnerPlotPosition Height="100" Width="95.98404" X="27.00798" Y="2.50000072" />
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Name="Open" Alignment="Center"></asp:Legend>
                </Legends>
            </asp:Chart>
        <%--</div>--%>
    </div>
</div>