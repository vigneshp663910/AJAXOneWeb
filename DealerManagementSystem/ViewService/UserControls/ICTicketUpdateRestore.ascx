<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketUpdateRestore.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketUpdateRestore" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Restore Date and Time</label>
                    <asp:TextBox ID="txtRestoreDate" runat="server" CssClass="form-control" AutoComplete="Off" onkeyup="return removeText('MainContent_txtRestoreDate');" Enabled="false"></asp:TextBox>
                    <asp:CalendarExtender ID="ceRestoreDate" runat="server" TargetControlID="txtRestoreDate" PopupButtonID="txtRestoreDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtRestoreDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    <asp:DropDownList ID="ddlRestoreHH" runat="server" CssClass="TextBox" Width="60px" Enabled="false">
                        <asp:ListItem Value="-1">HH</asp:ListItem>
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlRestoreMM" runat="server" CssClass="TextBox" Width="65px" Enabled="false">
                        <asp:ListItem Value="0">MM</asp:ListItem>
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>35</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>55</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Customer Remarks</label>
                    <asp:TextBox ID="txtCustomerRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Customer Satisfaction Level</label>
                    <asp:DropDownList ID="ddlCustomerSatisfactionLevel" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Arrival Back Date and Time</label>
                    <asp:TextBox ID="txtArrivalBackDate" runat="server" CssClass="form-control" AutoComplete="Off" onkeyup="return removeText('MainContent_txtRestoreDate');"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtArrivalBackDate" PopupButtonID="txtArrivalBackDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtArrivalBackDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    <asp:DropDownList ID="ddlArrivalBackHH" runat="server" CssClass="TextBox" Width="60px">
                        <asp:ListItem Value="-1">HH</asp:ListItem>
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlArrivalBackMM" runat="server" CssClass="TextBox" Width="65px">
                        <asp:ListItem Value="0">MM</asp:ListItem>
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>35</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>55</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Complaint Status</label>
                    <asp:DropDownList ID="ddlComplaintStatus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlComplaintStatus_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem>Open</asp:ListItem>
                        <asp:ListItem>Close</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
