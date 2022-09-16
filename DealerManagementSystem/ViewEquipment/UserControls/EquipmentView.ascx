<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquipmentView.ascx.cs" Inherits="DealerManagementSystem.ViewEquipment.UserControls.EquipmentView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %> 
 

<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB5plfGdJPhLvXriCfqIplJKBzbJVC8GlI"></script>

    <%--var geocoder;--%>

    <style>
        .fieldset-borderAuto {
            border: solid 1px #cacaca;
            margin: 1px 0;
            border-radius: 5px;
            padding: 10px;
            background-color: #b4b4b4;
        }

            .fieldset-borderAuto tr {
                /* background-color: #000084; */
                background-color: inherit;
                font-weight: bold;
                color: white;
            }

            .fieldset-borderAuto:hover {
                background-color: blue;
            }
    </style>

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px"> 
                
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Cold Visit</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Visit Number : </label>
                    <asp:Label ID="lblLeadNumber" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Visit Date : </label>
                    <asp:Label ID="lblLeadDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Customer : </label>
                    <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Contact Person : </label>
                    <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Email : </label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Address : </label>
                    <asp:Label ID="lblAddress" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Importance : </label>
                    <asp:Label ID="lblImportance" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Location : </label>
                    <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>

<asp:HiddenField ID="hfLatitude" runat="server" />
    <asp:HiddenField ID="hfLongitude" runat="server" />
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="1">
    <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="Activity">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                   
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel> 
</asp1:TabContainer>

 

 

 
 