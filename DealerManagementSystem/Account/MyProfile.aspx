<%@ Page Title="" Language="C#" MasterPageFile="../Dealer.Master" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="DealerManagementSystem.Account.MyProfile" %>

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
                        <asp:ImageButton ID="ibtnPhoto" ImageUrl="~/Images/User.jpg" runat="server" Width="60px" Height="55px" />
                    </div>
                    <div class="col-md-9"></div>
                    <div class="col-md-3 text-right">
                        <label>Full Name</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblFullName" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Designation</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lbDesignation" runat="server" CssClass="label"></asp:Label>
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
                        <label>Emp ID</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblEmpID" runat="server" CssClass="label"></asp:Label>
                    </div>
                    
                    <div class="col-md-3 text-right">
                        <label>MobileNumber</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblMobileNumber" runat="server" CssClass="label"></asp:Label>
                    </div>

                    <div class="col-md-3 text-right">
                        <label>User ID</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblUserID" runat="server" CssClass="label"></asp:Label>
                    </div>

                    
                    <div class="col-md-3 text-right">
                        <label>Contact No 1</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblContactNo1" runat="server" CssClass="label"></asp:Label>
                    </div>

                    <div class="col-md-3 text-right">
                        <label>Role</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblRole" runat="server" CssClass="label"></asp:Label>
                    </div>

                    
                    <div class="col-md-3 text-right">
                        <label>Contact No 2</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblContactNo2" runat="server" CssClass="label"></asp:Label>
                    </div>

                    <div class="col-md-3 text-right">
                        <label>-</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lbl_" runat="server" CssClass="label"></asp:Label>
                    </div>

                    
                    <div class="col-md-3 text-right">
                        <label>Emergency Contact No</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblEmergencyContact" runat="server" CssClass="label"></asp:Label>
                    </div>
                    
                </div>
            </fieldset>
            
        </div>
    </div>
</asp:Content>
