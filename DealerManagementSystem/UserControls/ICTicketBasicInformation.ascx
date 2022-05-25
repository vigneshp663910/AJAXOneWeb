<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketBasicInformation.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketBasicInformation" %>
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <legend style="background: none; color: indigo; font-size: 17px;">IC Ticket Basic Information</legend>
    <div class="col-md-12 View">
        <div class="col-md-4">
            <div class="col-md-12">
                <label class="modal-label">IC Ticket</label>
                <asp:Label ID="lblICTicket" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Complaint Description</label>
                <asp:Label ID="lblComplaintDescription" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Dealer</label>
                <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Contact Person Name & No</label>
                <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Is Margin Warranty</label>
                <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Enabled="false" />
            </div>
            <div class="col-md-12">
                <label class="modal-label">Warranty Expiry</label>
                <asp:Label ID="lblWarrantyExpiry" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">AMC Expiry</label>
                <asp:Label ID="lblAMCExpiryDate" runat="server" CssClass="label"></asp:Label>
            </div>
        </div>
        <div class="col-md-4">
            <div class="col-md-12">
                <label class="modal-label">Requested Date</label>
                <asp:Label ID="lblRequestedDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Status</label>
                <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Customer</label>
                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Old IC Ticket Number</label>
                <asp:Label ID="lblOldICTicketNumber" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Equipment</label>
                <asp:Label ID="lblEquipment" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Last HMR Date & Value</label>
                <asp:Label ID="lblLastHMRValue" runat="server" CssClass="label"></asp:Label>
            </div>
        </div>
        <div class="col-md-4">
            <div class="col-md-12">
                <label class="modal-label">District</label>
                <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Information</label>
                <asp:Label ID="lblInformation" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Customer Category</label>
                <asp:Label ID="lblCustomerCategory" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Warranty</label>
                <asp:CheckBox ID="cbIsWarranty" runat="server" Enabled="false" />
            </div>
            <div class="col-md-12">
                <label class="modal-label">Model</label>
                <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label class="modal-label">Refurbished Expiry</label>
                <asp:Label ID="lblRFWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
            </div>
        </div>
    </div>
</fieldset>
