<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketUpdateCallInformation.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketUpdateCallInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12"> 
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Location</label>
                    <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Service Type</label>
                    <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-control" OnTextChanged="ddlServiceType_TextChanged" AutoPostBack="true" />
                    <asp:DropDownList ID="ddlServiceTypeOverhaul" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceTypeOverhaul" DataValueField="ServiceTypeOverhaulID" />
                    <asp:DropDownList ID="ddlServiceSubType" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceSubType" DataValueField="ServiceSubTypeID" />

                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Service Priority</label>
                    <asp:DropDownList ID="ddlServicePriority" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Delivery Location</label>
                    <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Current HMR Value</label>
                    <asp:TextBox ID="txtHMRValue" runat="server" CssClass="form-control" AutoComplete="SP" OnTextChanged="txtHMRValue_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div> 
            </div>
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Type Of Warranty</label>
                    <asp:DropDownList ID="ddlTypeOfWarranty" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Main Application</label>
                    <asp:DropDownList ID="ddlMainApplication" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMainApplication_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Sub Application</label>
                    <asp:DropDownList ID="ddlSubApplication" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSubApplication_SelectedIndexChanged" AutoPostBack="true" />
                    <asp:TextBox ID="txtSubApplicationEntry" runat="server" CssClass="form-control" AutoComplete="Off" Visible="false" OnTextChanged="txtSubApplicationEntry_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Site Contact Person’s Name</label>
                    <asp:TextBox ID="txtOperatorName" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Site Contact Person’s Number</label>
                    <asp:TextBox ID="txtSiteContactPersonNumber" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Site Contact Person’s Number 2</label>
                    <asp:TextBox ID="txtSiteContactPersonNumber2" runat="server" CssClass="form-control"  MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Designation</label>
                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Scope of Work</label>
                    <asp:TextBox ID="txtScopeOfWork" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>

                <%--</div>
    <div class="col-md-12">--%>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">No Claim</label>
                    <asp:CheckBox ID="cbNoClaim" runat="server" />
                </div>
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">No Claim Reason</label>
                    <asp:TextBox ID="txtNoClaimReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Mc Entered Service Date</label>
                    <asp:TextBox ID="txtMcEnteredServiceDate" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtMcEnteredServiceDate" PopupButtonID="txtMcEnteredServiceDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtMcEnteredServiceDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Service Started Date</label>
                    <asp:TextBox ID="txtServiceStartedDate" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtServiceStartedDate" PopupButtonID="txtServiceStartedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtServiceStartedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Service Ended Date</label>
                    <asp:TextBox ID="txtServiceEndedDate" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtHMRDate');"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtServiceEndedDate" PopupButtonID="txtServiceEndedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtServiceEndedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                </div>
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Kind Attn</label>
                    <asp:TextBox ID="txtKindAttn" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Remarks</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Is Machine Active</label>
                    <asp:CheckBox ID="cbIsMachineActive" runat="server" Checked="true" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Cess</label>
                    <asp:CheckBox ID="cbCess" runat="server" />
                </div>
            </div>
        </fieldset> 
    </ContentTemplate>
</asp:UpdatePanel>




