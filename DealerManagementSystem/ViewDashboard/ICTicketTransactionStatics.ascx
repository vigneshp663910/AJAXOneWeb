<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketTransactionStatics.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.ICTicketTransactionStatics" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<div class="modbox">
    <div class="modtitle">
        <asp:Literal ID="ucTitle" runat="server" Text="ICTicket Transaction Statistics"></asp:Literal>
    </div>
    <div class="modboxin">
        <div class="ErrorRed" id="divError" runat="server" visible="false">
            <span id="errorMessage" runat="server"></span>
            <img alt="" src="../images/error_red.gif" />
        </div>
        <div class="dashboardGrid">
             <%--  CustomProperties="PieLabelStyle=Outside, PieStartAngle=270" --%>
          <%--  290px--%>
            <asp:Chart ID="Chart1" runat="server" Width="694px" Height="198px">
                <Series>
                    <asp:Series Name="Series1" ChartType="Pie"
                     CustomProperties="PieLabelStyle=Disabled , PieStartAngle=270"

                        ChartArea="ChartArea2"  >
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <%--  <asp:ChartArea Name="ChartArea1">
                        <AxisY TextOrientation="Rotated270" Title="Ticket Cound"></AxisY>
                        <AxisX>
                            <MajorGrid Enabled="False" />
                        </AxisX>
                    </asp:ChartArea>--%>
                  
                    <asp:ChartArea Name="ChartArea2"                        
                        AlignmentOrientation="All"
                        BackColor="Transparent">
                        <InnerPlotPosition Height="100" Width="99.98404" X="27.00798" Y="2.50000072" />
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Name="Open" Alignment="Center"></asp:Legend>
                </Legends>
            </asp:Chart>
        </div>
    </div>
</div>
