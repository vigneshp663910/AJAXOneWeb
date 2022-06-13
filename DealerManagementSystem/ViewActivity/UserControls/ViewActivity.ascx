<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewActivity.ascx.cs" Inherits="DealerManagementSystem.ViewActivity.UserControls.ViewActivity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>


<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbtnCloseActivity" runat="server" OnClick="lbActions_Click">End Activity</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
 <%-- <asp:LinkButton ID="lbtnConvertToProspect" runat="server" OnClick="lbActions_Click">Convert to Prospect</asp:LinkButton> --%>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>Activity Number : </label>
                <asp:Label ID="lblLeadNumber" runat="server"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Activity Date : </label>
                <asp:Label ID="lblLeadDate" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Activity Type : </label>
                <asp:Label ID="lblCategory" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Engineer : </label>
                <asp:Label ID="lblProgressStatus" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Start Date : </label>
                <asp:Label ID="lblQualification" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>End Date : </label>
                <asp:Label ID="lblSource" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Location : </label>
                <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Customer : </label>
                <asp:Label ID="lblType" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Amount : </label>
                <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Reference Activity : </label>
                <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Reference Activity Number : </label>
                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />


<asp:Panel ID="pnlActivity" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">End Activity</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button10" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageLead" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">
            <fieldset class="fieldset-border" id="fldCountry" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label>Activity Type</label>
                        <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label>End Date</label>
                        <asp:TextBox ID="txtEngineer" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label>Start Date</label>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label>End Date</label>
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    
                    <div class="col-md-6 col-sm-12">
                        <label>Customer Code</label>
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <label>Reference Type</label>
                        <asp:DropDownList ID="ddlReferenceType" runat="server" CssClass="form-control" />

                    </div>
                    
                    <div class="col-md-6 col-sm-12">
                        <label>Reference Number</label>
                        <asp:TextBox ID="txtReferenceNo" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>

                    <div class="col-md-12 col-sm-12">
                        <label>Remarks</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>

            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnActivityEdit" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnActivityEdit_Click" />
        </div>
    </div>
</asp:Panel>
<asp1:ModalPopupExtender ID="MPE_Activity" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlActivity" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>





