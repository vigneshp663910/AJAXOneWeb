<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadViewHeader.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.LeadViewHeader" %>
 <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>Lead Number : </label>
                <asp:Label ID="lblLeadNumber" runat="server"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Lead Date : </label>
                <asp:Label ID="lblLeadDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Category : </label>
                <asp:Label ID="lblCategory" runat="server" CssClass="label"></asp:Label>
            </div>
           <div class="col-md-4">
                <label>Urgency : </label>
                <asp:Label ID="lblUrgency" runat="server" CssClass="label"></asp:Label>
            </div>
             <div class="col-md-4">
                <label>Application : </label>
                <asp:Label ID="lblApplication" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Qualification : </label>
                <asp:Label ID="lblQualification" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Source : </label>
                <asp:Label ID="lblSource" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Status : </label>
                <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Project : </label>
                <asp:Label ID="lblProject" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Dealer : </label>
                <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
            </div>
             <div class="col-md-4">
                <label>Customer Feed back : </label>
                <asp:Label ID="lblCustomerFeedback" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Remarks : </label>
                <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Customer : </label>
                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Contact Person : </label>
                <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Mobile : </label>
                <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Email : </label>
                <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Address : </label>
                <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Financial Info : </label>
                <asp:Label ID="lblFinancialInfo" runat="server" CssClass="label"></asp:Label>
            </div> 
        </div>
    </fieldset>
