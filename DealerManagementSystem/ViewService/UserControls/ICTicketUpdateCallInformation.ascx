<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketUpdateCallInformation.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketUpdateCallInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Departure Date and Time</label>
            <asp:TextBox ID="txtDepartureDate" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" onkeyup="return removeText('MainContent_txtDepartureDate');" BorderColor="Silver"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDepartureDate" PopupButtonID="txtDepartureDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDepartureDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
            <asp:DropDownList ID="ddlDepartureHH" runat="server" CssClass="TextBox" Width="60px">
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
            <asp:DropDownList ID="ddlDepartureMM" runat="server" CssClass="TextBox" Width="65px">
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
            <label class="modal-label">Reached Date and Time</label>
            <asp:TextBox ID="txtReachedDate" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" onkeyup="return removeText('MainContent_txtReachedDate');" OnTextChanged="txtReachedDate_TextChanged" AutoPostBack="true" BorderColor="Silver"></asp:TextBox>
            <asp:CalendarExtender ID="ceReachedDate" runat="server" TargetControlID="txtReachedDate" PopupButtonID="txtReachedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtReachedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
            <asp:DropDownList ID="ddlReachedHH" runat="server" CssClass="TextBox" Width="60px">
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
            <asp:DropDownList ID="ddlReachedMM" runat="server" CssClass="TextBox" Width="65px">
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
            <label class="modal-label">Location</label>
            <asp:TextBox ID="txtLocation" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Service Type</label>
            <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="TextBox" OnTextChanged="ddlServiceType_TextChanged" AutoPostBack="true" />
            <asp:DropDownList ID="ddlServiceTypeOverhaul" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceTypeOverhaul" DataValueField="ServiceTypeOverhaulID" />
            <asp:DropDownList ID="ddlServiceSubType" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceSubType" DataValueField="ServiceSubTypeID" />

        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Service Priority</label>
            <asp:DropDownList ID="ddlServicePriority" runat="server" CssClass="TextBox" />
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Delivery Location</label>
            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="TextBox" />
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Current HMR Value</label>
            <asp:TextBox ID="txtHMRValue" runat="server" CssClass="input" AutoComplete="SP" OnTextChanged="txtHMRValue_TextChanged" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Current HMR Date</label>
            <asp:TextBox ID="txtHMRDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
            <asp:CalendarExtender ID="ceHMRDate" runat="server" TargetControlID="txtHMRDate" PopupButtonID="txtHMRDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtHMRDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Cess</label>
            <asp:CheckBox ID="cbCess" runat="server" />
        </div>
    </div>
    <div class="col-md-12">
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Type Of Warranty</label>
            <asp:DropDownList ID="ddlTypeOfWarranty" runat="server" CssClass="TextBox" />
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Main Application</label>
            <asp:DropDownList ID="ddlMainApplication" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlMainApplication_SelectedIndexChanged" AutoPostBack="true" />
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Sub Application Manual</label>
            <asp:DropDownList ID="ddlSubApplication" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlSubApplication_SelectedIndexChanged" AutoPostBack="true" />
            <asp:TextBox ID="txtSubApplicationEntry" runat="server" CssClass="TextBox" AutoComplete="Off" Visible="false" OnTextChanged="txtSubApplicationEntry_TextChanged"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Site Contact Person’s Name</label>
            <asp:TextBox ID="txtOperatorName" runat="server" CssClass="input" AutoComplete="SP"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Site Contact Person’s Number</label>
            <asp:TextBox ID="txtSiteContactPersonNumber" runat="server" CssClass="input"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Site Contact Person’s Number 2</label>
            <asp:TextBox ID="txtSiteContactPersonNumber2" runat="server" CssClass="input"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Designation</label>
            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="TextBox" />
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Scope of Work</label>
            <asp:TextBox ID="txtScopeOfWork" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
        </div>

    </div>
    <div class="col-md-12">
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">No Claim</label>
            <asp:CheckBox ID="cbNoClaim" runat="server" />
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">No Claim Reason</label>
            <asp:TextBox ID="txtNoClaimReason" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Mc Entered Service Date</label>
            <asp:TextBox ID="txtMcEnteredServiceDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtMcEnteredServiceDate" PopupButtonID="txtMcEnteredServiceDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtMcEnteredServiceDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Service Started Date</label>
            <asp:TextBox ID="txtServiceStartedDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtServiceStartedDate" PopupButtonID="txtServiceStartedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtServiceStartedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Service Ended Date</label>
            <asp:TextBox ID="txtServiceEndedDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtServiceEndedDate" PopupButtonID="txtServiceEndedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtServiceEndedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Kind Attn</label>
            <asp:TextBox ID="txtKindAttn" runat="server" CssClass="input"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Remarks</label>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Is Machine Active</label>
            <asp:CheckBox ID="cbIsMachineActive" runat="server" Checked="true" />
        </div> 
    </div>
</fieldset> 






