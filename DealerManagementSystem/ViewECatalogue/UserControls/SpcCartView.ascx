<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpcCartView.ascx.cs" Inherits="DealerManagementSystem.ViewECatalogue.UserControls.SpcCartView" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px"> 
                <asp:LinkButton ID="lbCancelPO" runat="server" OnClientClick="return ConfirmCancelPO();" OnClick="lbActions_Click">Cancel PO</asp:LinkButton> 
            </div>
        </div>
    </div>
</div>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">PO</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>PO Number : </label>
                    <asp:Label ID="lblPurchaseOrderNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>PO Date : </label>
                    <asp:Label ID="lblPurchaseOrderDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>SO Number : </label>
                    <asp:Label ID="lblSoNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Order Type : </label>
                    <asp:Label ID="lblOrderType" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Order To : </label>
                    <asp:Label ID="lblOrderTo" runat="server" CssClass="LabelValue"></asp:Label>
                </div> 
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblPODealer" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Receiving Location : </label>
                    <asp:Label ID="lblReceivingLocation" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Vendor : </label>
                    <asp:Label ID="lblPOVendor" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Expected Delivery Date : </label>
                    <asp:Label ID="lblExpectedDeliveryDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

            </div>
            <div class="col-md-4"> 
                <div class="col-md-12">
                    <label>Discount : </label>
                    <asp:Label ID="lblDiscount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>Taxable Amount : </label>
                    <asp:Label ID="lblTaxableAmount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Tax Amount : </label>
                    <asp:Label ID="lblTaxAmount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gross Amount : </label>
                    <asp:Label ID="lblGrossAmount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

            </div>
        </div>
    </fieldset>
</div>

 

 