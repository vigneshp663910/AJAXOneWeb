<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerViewHeader.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.CustomerViewHeader" %>
<fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Customer</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Customer : </label>
                    <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Alternative Mobile : </label>
                    <asp:Label ID="lblAlternativeMobile" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>GSTIN : </label>
                    <asp:Label ID="lblGSTIN" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Active : </label>

                    <asp:CheckBox ID="cbIsActive" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>
                <div class="col-md-12">
                    <label>BillingBlock : </label>
                    <asp:CheckBox ID="cbBillingBlock" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Contact Person : </label>
                    <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Email : </label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>PAN : </label>
                    <asp:Label ID="lblPAN" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>OrderBlock : </label>
                    <asp:CheckBox ID="cbOrderBlock" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Address : </label>
                    <asp:Label ID="lblAddress" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Verified : </label>
                    <asp:CheckBox ID="cbVerified" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>
                <div class="col-md-12">
                    <label>DeliveryBlock : </label>
                    <asp:CheckBox ID="cbDeliveryBlock" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>

            </div>
        </div>
    </fieldset>