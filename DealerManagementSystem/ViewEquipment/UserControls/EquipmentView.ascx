<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquipmentView.ascx.cs" Inherits="DealerManagementSystem.ViewEquipment.UserControls.EquipmentView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerViewSoldTo" %>

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
        <legend style="background: none; color: #007bff; font-size: 17px;">Equipment</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Model : </label>
                    <asp:Label ID="lblModel" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Model Description : </label>
                    <asp:Label ID="lblModelDescription" runat="server" CssClass="label"></asp:Label>
                </div>
                
                <div class="col-md-12">
                    <label>Warranty ExpiryDate : </label>
                    <asp:Label ID="lblWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>HMR Date : </label>
                    <asp:Label ID="lblCurrentHMRDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>RF Warranty Start Date : </label>
                    <asp:Label ID="lblRFWarrantyStartDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>AMC : </label>
                    <asp:CheckBox ID="cbIsAMC" runat="server" Enabled="false" CssClass="mycheckBig" />
                </div>
                <div class="col-md-12">
                    <label>Variants Fitting Date : </label>
                    <asp:Label ID="lblVariantsFittingDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>ESN : </label>
                    <asp:Label ID="lblESN" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Special Variants : </label>
                    <asp:Label ID="lblSpecialVariants" runat="server" CssClass="label"></asp:Label>
                </div>                
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Engine SerialNo : </label>
                    <asp:Label ID="lblEngineSerialNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>District : </label>
                    <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                </div>
                
                <div class="col-md-12">
                    <label>Engine Model : </label>
                    <asp:Label ID="lblEngineModel" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>HMR Value : </label>
                    <asp:Label ID="lblCurrentHMRValue" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>RF Warranty Expiry Date : </label>
                    <asp:Label ID="lblRFWarrantyExpiryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>AMC Start Date : </label>
                    <asp:Label ID="lblAMCStartDate" runat="server" CssClass="label"></asp:Label>
                </div>
                
                <div class="col-md-12">
                    <label>Plant : </label>
                    <asp:Label ID="lblPlant" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>ProductionStatus : </label>
                    <asp:Label ID="lblProductionStatus" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Manufacturing Date : </label>
                    <asp:Label ID="lblManufacturingDate" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Equipment SerialNo : </label>
                    <asp:Label ID="lblEquipmentSerialNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>State : </label>
                    <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dispatched On : </label>
                    <asp:Label ID="lblDispatchedOn" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Commisioning On : </label>
                    <asp:Label ID="lblCommisioningOn" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Type Of Wheel Assembly : </label>
                    <asp:Label ID="lblTypeOfWheelAssembly" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>AMC Expiry Date : </label>
                    <asp:Label ID="lblAMCExpiryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Material Code : </label>
                    <asp:Label ID="lblMaterialCode" runat="server" CssClass="label"></asp:Label>
                </div>                
                <div class="col-md-12">
                    <label>Chassis No : </label>
                    <asp:Label ID="lblChassisSlNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dispatch : </label>
                    <asp:Label ID="lblDispatch" runat="server" CssClass="label"></asp:Label>
                </div>
                
                
            </div>
        </div>
    </fieldset>
</div>

<asp:HiddenField ID="hfLatitude" runat="server" />
<asp:HiddenField ID="hfLongitude" runat="server" />
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpIbase" runat="server" ToolTip="IBase..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="1">
    <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="IBase">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">IBase</legend>
                    <div class="col-md-12 View">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Installed BaseNo : </label>
                                <asp:Label ID="lblInstalledBaseNo" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Delivery Date : </label>
                                <asp:Label ID="lblDeliveryDate" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>IBase Warranty Start Date : </label>
                                <asp:Label ID="lblIbaseWarrantyStart" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>IBase Location : </label>
                                <asp:Label ID="lblIBaseLocation" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>IBase CreatedOn : </label>
                                <asp:Label ID="lblIBaseCreatedOn" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>IBase Warranty End Date : </label>
                                <asp:Label ID="lblIbaseWarrantyEnd" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Major Region : </label>
                                <asp:Label ID="lblMajorRegion" runat="server" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Financial Year Of Dispatch : </label>
                                <asp:Label ID="lblFinancialYearOfDispatch" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabCustomer" runat="server" HeaderText="Customer">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_CustomerViewSoldTo ID="CustomerViewSoldTo" runat="server"></UC:UC_CustomerViewSoldTo>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>