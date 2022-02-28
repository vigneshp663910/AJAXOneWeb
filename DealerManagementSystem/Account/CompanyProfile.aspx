<%@ Page Title="" Language="C#" MasterPageFile="../Dealer.Master" AutoEventWireup="true" CodeBehind="CompanyProfile.aspx.cs" Inherits="DealerManagementSystem.Account.CompanyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Info</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <asp:ImageButton ID="ibtnPhoto" ImageUrl="~/Ajax/Images/Ajax-New-Logo.png" runat="server" Width="170px" Height="55px" />
                    </div>
                    <div class="col-md-9"></div>
                    <div class="col-md-3 text-right">
                        <label>Company Name</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblCompanyName" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Company Code</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblCompanyCode" runat="server" CssClass="label"></asp:Label>
                    </div>
                    
                    <div class="col-md-3 text-right">
                        <label>Address</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblAddress" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>City</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblCity" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>State</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Country</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblCountry" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Pincode</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblPincode" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Email</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>MobileNumber</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblMobileNumber" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Website</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblWebsite" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Contact No 1</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblContactNo1" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Contact No 2</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblContactNo2" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Registration Date</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblRegistrationDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Activation Date</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblActivationDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Service Request Url</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblURL" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
            </fieldset>
            
        </div>
    </div>
</asp:Content>
