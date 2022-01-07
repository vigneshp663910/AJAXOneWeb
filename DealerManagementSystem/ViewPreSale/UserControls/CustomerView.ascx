<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.CustomerView" %>
 <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Customer</legend>
                    <div class="col-md-12">
                       <div class="col-md-2 text-right">
                            <label>Customer</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Contact Person</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Mobile</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                        </div>
                            <div class="col-md-2 text-right">
                            <label>Alternative Mobile</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblAlternativeMobile" runat="server" CssClass="label"></asp:Label>
                        </div>
                        
                        <div class="col-md-2 text-right">
                            <label>Email</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-2 text-right">
                            <label>Location</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
                        </div>  

                         <div class="col-md-2 text-right">
                            <label>GSTIN</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblGSTIN" runat="server" CssClass="label"></asp:Label>
                        </div> 

                         <div class="col-md-2 text-right">
                            <label>PAN</label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblPAN" runat="server" CssClass="label"></asp:Label>
                        </div> 
                    </div>
                </fieldset>