<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineServiceTicketView.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.OnlineServiceTicketView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerViewSoldTo" %>
<script type="text/javascript">
    function ConfirmDeclineApprove() {
        var x = confirm("Are you sure you want to decline the IC Ticket?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmDeclineReject() {
        var x = confirm("Are you sure you want to reopen the IC Ticket?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmServiceClaim() {
        var x = confirm("Are you sure you want to request for Service Claim?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmServiceQuotation() {
        var x = confirm("Are you sure you want to generate Service Quotation?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmServiceProfarmaInvoice() {
        var x = confirm("Are you sure you want to generate Service Profarma Invoice?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmServiceInvoice() {
        var x = confirm("Are you sure you want to generate Service Invoice?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmMaterialClaim() {
        var x = confirm("Are you sure you want to request for Material Claim?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmMaterialQuotation() {
        var x = confirm("Are you sure you want to generate Material Quotation?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmUnlockTicket() {
        var x = confirm("Are you sure you want to unlock the IC Ticket?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmUnblockTicket() {
        var x = confirm("Are you sure you want to unblock the IC Ticket?");
        if (x) {
            return true;
        }
        else
            return false;
    }




    function ConfirmMarginWarrantyApprove() {
        var x = confirm("Are you sure you want to approve the Margin Warranty?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmMarginWarrantyReject() {
        var x = confirm("Are you sure you want to reject the Margin Warranty?");
        if (x) {
            return true;
        }
        else
            return false;
    }

</script>


<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px; overflow-x: auto; max-height: 300px">
                <%--  <asp:LinkButton ID="lbtnEditCallInformation" runat="server" OnClick="lbActions_Click">Edit Call Information</asp:LinkButton> --%>
                <asp:LinkButton ID="lbtnRestore" runat="server" OnClick="lbActions_Click">Restore</asp:LinkButton>
                <asp:LinkButton ID="lbtnEscalatedL1" runat="server" OnClick="lbActions_Click">Escalated to L1</asp:LinkButton>
                <asp:LinkButton ID="lbtnEscalatedDealer" runat="server" OnClick="lbActions_Click">Escalated to Dealer</asp:LinkButton>
                <asp:LinkButton ID="lbtnUpdateCustomerSatisfactionLevel" runat="server" OnClick="lbActions_Click">Update Customer Satisfaction Level</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<br />
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">IC Ticket</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>IC Ticket : </label>
                    <asp:Label ID="lblTicket" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>Complaint Description : </label>
                    <asp:Label ID="lblComplaintDescription" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Customer : </label>
                    <asp:Label ID="lblCustomer" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Equipment : </label>
                    <asp:Label ID="lblEquipment" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Model : </label>
                    <asp:Label ID="lblModel" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Warranty : </label>
                    <asp:CheckBox ID="cbIsWarranty" runat="server" Enabled="false" />
                </div>
                <div class="col-md-12">
                    <label>Priority : </label>
                    <asp:Label ID="lblPriority" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

            </div>
            <div class="col-md-4">


                <div class="col-md-12">
                    <label>Restore By : </label>
                    <asp:Label ID="lblRestoreBy" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>RestoreDate : </label>
                    <asp:Label ID="lblRestoreDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>RestoreRemarks : </label>
                    <asp:Label ID="lblRestoreRemarks" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>RegisteredBy : </label>
                    <asp:Label ID="lblRegisteredBy" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>SatisfactionLevel : </label>
                    <asp:Label ID="lblSatisfactionLevel" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>EscalatedL1 : </label>
                    <asp:Label ID="lblEscalatedL1" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>EscalatedL1 Date : </label>
                    <asp:Label ID="lblEscalatedL1Date" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Contact Person Name & No : </label>
                    <asp:Label ID="lblContactPerson" runat="server" CssClass="LabelValue"></asp:Label>
                </div>


                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>State : </label>
                    <asp:Label ID="lblState" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>District : </label>
                    <asp:Label ID="lblDistrict" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Location : </label>
                    <asp:Label ID="lblLocation" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
              <%--  <div class="col-md-12">
                    <label>Call Category : </label>
                    <asp:Label ID="lblCallCategory" runat="server" CssClass="LabelValue"></asp:Label>
                </div>--%>
            </div>

            <%--    <div class="col-md-4">
                <label>Last HMR Date & Value : </label>
                <asp:Label ID="lblLastHMRValue" runat="server" CssClass="LabelValue"></asp:Label>
            </div> --%>
        </div>
    </fieldset>
</div>
<%--<asp:TabContainer ID="tbpSaleQuotation" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="10">
    <asp:TabPanel ID="TabCustomer" runat="server" HeaderText="Customer">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_CustomerViewSoldTo ID="CustomerViewSoldTo" runat="server"></UC:UC_CustomerViewSoldTo>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>--%>

<asp:Panel ID="pnlUpdateRestore" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">IC Ticket Restoration</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button9" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageRestore" runat="server" Text="" CssClass="message" />
            <fieldset class="fieldset-border" runat="server">
                <div class="col-md-12">
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Customer Remarks</label>
                        <asp:TextBox ID="txtRestoreRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateRestore" runat="server" Text="Save" CssClass="btn Save" OnClick="btnPopup_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_UpdateRestore" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlUpdateRestore" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlEscalatedL1" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Escalated L1</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblEscalatedL1Message" runat="server" Text="" CssClass="message" />
            <fieldset class="fieldset-border" runat="server">
                <div class="col-md-12">
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Customer Remarks</label>
                        <asp:TextBox ID="txtEscalatedL1Remark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Ajax Employee</label>
                        <asp:DropDownList ID="ddlAjaxEmployee" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateEscalatedL1" runat="server" Text="Save" CssClass="btn Save" OnClick="btnPopup_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EscalatedL1" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEscalatedL1" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />



<asp:Panel ID="pnlCustomerFeedback" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Customer Feedback</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button101" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblCustomerSatisfactionLevelMessage" runat="server" Text="" CssClass="message" />
            <fieldset class="fieldset-border" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Customer Satisfaction Level</label>
                        <asp:DropDownList ID="ddlCustomerSatisfactionLevel" runat="server" CssClass="form-control" />
                    </div>

                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateCustomerSatisfactionLevel" runat="server" Text="Update" CssClass="btn Save" OnClick="btnPopup_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_CustomerSatisfactionLevelk" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomerFeedback" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
