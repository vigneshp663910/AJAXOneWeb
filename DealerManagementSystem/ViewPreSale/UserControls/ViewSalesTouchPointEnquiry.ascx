<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSalesTouchPointEnquiry.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.ViewSalesTouchPointEnquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLead.ascx" TagPrefix="UC" TagName="UC_AddLead" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddEnquiry.ascx" TagPrefix="UC" TagName="UC_AddEnquiry" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbReject" runat="server" OnClick="lbActions_Click">Reject</asp:LinkButton>
                <asp:LinkButton ID="lbConvertToEnquiry" runat="server" OnClick="lbActions_Click">Convert To Enquiry</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Enquiry Details</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>STP Enquiry Number : </label>
                    <asp:Label ID="lblSalesTouchPointEnquiryNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Customer Name : </label>
                    <asp:Label ID="lblCustomerName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Mail : </label>
                    <asp:Label ID="lblMail" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>District : </label>
                    <asp:Label ID="lblDistrict" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Address 3 : </label>
                    <asp:Label ID="lblAddress3" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>STP Enquiry Date : </label>
                    <asp:Label ID="lblSalesTouchPointEnquiryDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Person Name : </label>
                    <asp:Label ID="lblPersonName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Country : </label>
                    <asp:Label ID="lblCountry" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Address 1 : </label>
                    <asp:Label ID="lblAddress" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Pincode : </label>
                    <asp:Label ID="lblPincode" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Enquiry Number : </label>
                    <asp:Label ID="lblEnquiryNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:Label ID="lblMobile" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>State : </label>
                    <asp:Label ID="lblState" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Address 2 : </label>
                    <asp:Label ID="lblAddress2" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblRemarks" runat="server" CssClass="LabelValue"></asp:Label>
                </div>                
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<asp:Panel ID="pnlEnquiryReject" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Enquiry Reject</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button7" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="lblMessageResponsible" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset5" runat="server">
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Remark</label>
                    <asp:TextBox ID="txtEnquiryRejectReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnEnquiryReject" runat="server" Text="Save" CssClass="btn Save" OnClick="btnEnquiryReject_Click"/>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EnquiryReject" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEnquiryReject" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlEnquiry" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Convert Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblAddEnquiryMessage" runat="server" Text="" CssClass="message" />
            <UC:UC_AddEnquiry ID="UC_AddEnquiry" runat="server"></UC:UC_AddEnquiry>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSave_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Enquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEnquiry" BackgroundCssClass="modalBackground" />
<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>