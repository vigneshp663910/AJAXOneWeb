<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesQuotationViewHeader.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.SalesQuotationViewHeader" %>  
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Quotation</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Ref Quotation No : </label>
                    <asp:Label ID="lblRefQuotationNo" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Ref Quotation Date : </label>
                    <asp:Label ID="lblRefQuotationDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Number : </label>
                    <asp:Label ID="lblQuotationNumber" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Date : </label>
                    <asp:Label ID="lblQuotationDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Quotation Type : </label>
                    <asp:Label ID="lblQuotationType" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Status : </label>
                    <asp:Label ID="lblQuotationStatus" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Valid From : </label>
                    <asp:Label ID="lblValidFrom" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Valid To : </label>
                    <asp:Label ID="lblValidTo" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Pricing Date : </label>
                    <asp:Label ID="lblPricingDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Price Group : </label>
                    <asp:Label ID="lblPriceGroup" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>User Status : </label>
                    <asp:Label ID="lblUserStatus" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <label>Total Effort : </label>
                <asp:Label ID="lblTotalEffort" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Total Expense : </label>
                <asp:Label ID="lblTotalExpense" runat="server" CssClass="label"></asp:Label>
            </div>
        </div>
    </fieldset> 
 
 