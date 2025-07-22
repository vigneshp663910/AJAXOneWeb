<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketView.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketAddTechnician.ascx" TagPrefix="UC" TagName="UC_ICTicketAddTechnician" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketUpdateCallInformation.ascx" TagPrefix="UC" TagName="UC_ICTicketUpdateCallInformation" %>
<%@ Register Src="~/ViewService/UserControls/AddFSR.ascx" TagPrefix="UC" TagName="UC_AddFSR" %>
<%@ Register Src="~/ViewService/UserControls/AddFSRAttachments.ascx" TagPrefix="UC" TagName="UC_AddFSRAttachments" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketAddOtherMachine.ascx" TagPrefix="UC" TagName="UC_ICTicketAddOtherMachine" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketAddServiceCharges.ascx" TagPrefix="UC" TagName="UC_ICTicketAddServiceCharges" %>
<%@ Register Src="~/ViewService/UserControls/AddTSIR.ascx" TagPrefix="UC" TagName="UC_AddTSIR" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketAddMaterialCharges.ascx" TagPrefix="UC" TagName="UC_ICTicketAddMaterialCharges" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketAddNotes.ascx" TagPrefix="UC" TagName="UC_ICTicketAddNotes" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketAddTechnicianWork.ascx" TagPrefix="UC" TagName="UC_ICTicketAddTechnicianWork" %>
<%@ Register Src="~/ViewService/UserControls/ICTicketUpdateRestore.ascx" TagPrefix="UC" TagName="UC_ICTicketUpdateRestore" %>
<%@ Register Src="~/ViewService/UserControls/AddICTicketCustomerFeedback.ascx" TagPrefix="UC" TagName="UC_ICTicketCustomerFeedback" %>

<%@ Register Src="~/ViewService/UserControls/AddFsrSignature.ascx" TagPrefix="UC" TagName="UC_FsrSignature" %>
<script type="text/javascript">
    function ConfirmDeclineApprove() {
        var x = confirm("Are you sure you want to decline the IC Ticket?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmDeclineReject() {
        var x = confirm("Are you sure you want to reopen the IC Ticket?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmServiceClaim() {
        var x = confirm("Are you sure you want to request for Service Claim?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmServiceQuotation() {
        var x = confirm("Are you sure you want to generate Service Quotation?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmServiceProfarmaInvoice() {
        var x = confirm("Are you sure you want to generate Service Profarma Invoice?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmServiceInvoice() {
        var x = confirm("Are you sure you want to generate Service Invoice?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmMaterialClaim() {
        var x = confirm("Are you sure you want to request for Material Claim?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmMaterialQuotation() {
        var x = confirm("Are you sure you want to generate Material Quotation?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmUnlockTicket() {
        var x = confirm("Are you sure you want to unlock the IC Ticket?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmUnblockTicket() {
        var x = confirm("Are you sure you want to unblock the IC Ticket?");
        if (x) {
            return true;
        }
        else
            return false;
    }




    function ConfirmMarginWarrantyApprove() {
        var x = confirm("Are you sure you want to approve the Margin Warranty?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmMarginWarrantyReject() {
        var x = confirm("Are you sure you want to reject the Margin Warranty?");
        if (x) {
            return true;
        }
        else
            return false;
    }

</script>


<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px; overflow-x: auto; max-height: 300px">
                <asp:LinkButton ID="lbtnAddTechnician" runat="server" OnClick="lbActions_Click">Add Technician</asp:LinkButton>
                <asp:LinkButton ID="lbtnEditCallInformation" runat="server" OnClick="lbActions_Click">Edit Call Information</asp:LinkButton>
                <asp:LinkButton ID="lbtnEditFSR" runat="server" OnClick="lbActions_Click">Edit FSR</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddFSRAttachments" runat="server" OnClick="lbActions_Click">Add FSR Attachments</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddOtherMachine" runat="server" OnClick="lbActions_Click">Add Other Machine</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddServiceCharges" runat="server" OnClick="lbActions_Click">Add Service Charges</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddTSIR" runat="server" OnClick="lbActions_Click">Add TSIR</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddMaterialCharges" runat="server" OnClick="lbActions_Click">Add Material Charges</asp:LinkButton>
                <asp:LinkButton ID="lbtnMaterialQuotation" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmMaterialQuotation();">Material Quotation</asp:LinkButton>
                <asp:LinkButton ID="lbtnAddNotes" runat="server" OnClick="lbActions_Click">Add Notes</asp:LinkButton>
                <asp:LinkButton ID="lbtAddTechnicianWork" runat="server" OnClick="lbActions_Click">Add Technician Work</asp:LinkButton>
                <asp:LinkButton ID="lbtnRestore" runat="server" OnClick="lbActions_Click">Restore</asp:LinkButton>
                <%-- <asp:LinkButton ID="lbtnCustomerFeedback" runat="server" OnClick="lbActions_Click">Customer Feedback</asp:LinkButton>--%>
                <asp:LinkButton ID="lbtnServiceClaim" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmServiceClaim()">Service Claim</asp:LinkButton>
                <asp:LinkButton ID="lbtnServiceQuotation" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmServiceQuotation();">Service Quotation</asp:LinkButton>
                <asp:LinkButton ID="lbtnServiceProfarmaInvoice" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmServiceProfarmaInvoice();">Service Profarma Invoice</asp:LinkButton>
                <asp:LinkButton ID="lbtnServiceInvoice" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmServiceInvoice();">Service Invoice</asp:LinkButton>
                <asp:LinkButton ID="lbtnMaterialClaim" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmMaterialClaim()">Material Claim</asp:LinkButton>
                <asp:LinkButton ID="lbtnUnlockTicket" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmUnlockTicket();">Unlock Ticket</asp:LinkButton>
                <asp:LinkButton ID="lbtnUnblockTicket" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmUnblockTicket();">Unblock Ticket</asp:LinkButton>

                <asp:LinkButton ID="lbtnRequestForDecline" runat="server" OnClick="lbActions_Click">Request for Decline</asp:LinkButton>
                <asp:LinkButton ID="lbtnDeclineApprove" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmDeclineApprove();">Decline Approve</asp:LinkButton>
                <asp:LinkButton ID="lbtnDeclineReject" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmDeclineReject();">Decline Reject</asp:LinkButton>

                <asp:LinkButton ID="lbtnMarginWarrantyRequest" runat="server" OnClick="lbActions_Click">Margin Warranty Request</asp:LinkButton>
                <asp:LinkButton ID="lbtnMarginWarrantyApprove" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmMarginWarrantyApprove();">Margin Warranty Approve</asp:LinkButton>
                <asp:LinkButton ID="lbtnMarginWarrantyReject" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmMarginWarrantyReject();">Margin Warranty Reject</asp:LinkButton>

                <asp:LinkButton ID="lbtnRequestDateChange" runat="server" OnClick="lbActions_Click">Request Date Change</asp:LinkButton>

                <asp:LinkButton ID="lbtnFsrSignature" runat="server" OnClick="lbActions_Click">FSR Signature</asp:LinkButton>
                <asp:LinkButton ID="lbtnRemoveRestoreDate" runat="server" OnClick="lbActions_Click">Remove Restore Date</asp:LinkButton>

                <asp:LinkButton ID="lbtnDepartureToSite" runat="server" OnClick="lbActions_Click">Departure To Site</asp:LinkButton>
                <asp:LinkButton ID="lbtnReachedInSite" runat="server" OnClick="lbActions_Click">Reached in Site</asp:LinkButton>
                <asp:LinkButton ID="lbtnArrivalBack" runat="server" OnClick="lbActions_Click">Arrival Back</asp:LinkButton>
                <asp:LinkButton ID="lbtnHmrDevUpdate" runat="server" OnClick="lbActions_Click">HMR Deviation Update</asp:LinkButton>
            </div>


        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">IC Ticket</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <label>IC Ticket : </label>
                <asp:Label ID="lblICTicket" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Requested Date : </label>
                <asp:Label ID="lblRequestedDate" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>District : </label>
                <asp:Label ID="lblDistrict" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Complaint Description : </label>
                <asp:Label ID="lblComplaintDescription" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Status : </label>
                <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <%--<div class="col-md-4">
                <label>Information : </label>
                <asp:Label ID="lblInformation" runat="server" CssClass="LabelValue"></asp:Label>
            </div>--%>
            <div class="col-md-4">
                <label>Dealer : </label>
                <asp:Label ID="lblDealer" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Customer : </label>
                <asp:Label ID="lblCustomer" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Customer Category : </label>
                <asp:Label ID="lblCustomerCategory" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Contact Person Name & No : </label>
                <asp:Label ID="lblContactPerson" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Old IC Ticket Number : </label>
                <asp:Label ID="lblOldICTicketNumber" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Warranty : </label>
                <asp:CheckBox ID="cbIsWarranty" runat="server" Enabled="false" />
            </div>
            <div class="col-md-4">
                <label>Is Margin Warranty : </label>
                <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Enabled="false" />
            </div>
            <div class="col-md-4">
                <label>Equipment : </label>
                <asp:Label ID="lblEquipment" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Model : </label>
                <asp:Label ID="lblModel" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Warranty Expiry : </label>
                <asp:Label ID="lblWarrantyExpiry" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Last HMR Date & Value : </label>
                <asp:Label ID="lblLastHMRValue" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>Refurbished Expiry : </label>
                <asp:Label ID="lblRFWarrantyExpiryDate" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
            <div class="col-md-4">
                <label>AMC Expiry : </label>
                <asp:Label ID="lblAMCExpiryDate" runat="server" CssClass="LabelValue"></asp:Label>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<asp1:TabContainer ID="tbpCust" runat="server" Font-Bold="True" Font-Size="Medium">
    <asp1:TabPanel ID="tpnlTechnician" runat="server" HeaderText="Technician" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvTechnician" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Technician">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Technician Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedBy" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedBy.ContactName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AssignedOn")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remove">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbTechnicianRemove" runat="server" OnClick="lbTechnicianDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                </ItemTemplate>

                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabReached" runat="server" HeaderText="Reached Info">
        <ContentTemplate>
            <br />
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <label>Departure Date and Time : </label>
                    <asp:Label ID="lblDepartureDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Reached Date and Time : </label>
                    <asp:Label ID="lblReachedDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Location : </label>
                    <asp:Label ID="lblLocation" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Service Type : </label>
                    <asp:Label ID="lblServiceType" runat="server" CssClass="LabelValue"></asp:Label>
                    <asp:DropDownList ID="ddlServiceTypeOverhaul" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceTypeOverhaul" DataValueField="ServiceTypeOverhaulID" />
                    <asp:DropDownList ID="ddlServiceSubType" runat="server" CssClass="TextBox" Visible="false" DataTextField="ServiceSubType" DataValueField="ServiceSubTypeID" />

                </div>
                <div class="col-md-4">
                    <label>Service Priority : </label>
                    <asp:Label ID="lblServicePriority" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Delivery Location : </label>
                    <asp:Label ID="lblDealerOffice" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Current HMR Value : </label>
                    <asp:Label ID="lblHMRValue" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Current HMR Date : </label>
                    <asp:Label ID="lblHMRDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>

        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlCallInformation" runat="server" HeaderText="Call Info">
        <ContentTemplate>
            <br />
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <label>Cess : </label>
                    <asp:CheckBox ID="cbCess" runat="server" />
                </div>
                <div class="col-md-4">
                    <label>Type Of Warranty : </label>
                    <asp:Label ID="lblTypeOfWarranty" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Main Application : </label>
                    <asp:Label ID="lblMainApplication" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Sub Application Manual : </label>
                    <asp:Label ID="lblSubApplication" runat="server" CssClass="LabelValue"></asp:Label>
                    <asp:Label ID="lblSubApplicationEntry" runat="server" CssClass="LabelValue" Visible="false"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Site Contact Person’s Name : </label>
                    <asp:Label ID="lblOperatorName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Site Contact Person’s Number : </label>
                    <asp:Label ID="lblSiteContactPersonNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Site Contact Person’s Number 2 : </label>
                    <asp:Label ID="lblSiteContactPersonNumber2" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Designation : </label>
                    <asp:Label ID="lblDesignation" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Scope of Work : </label>
                    <asp:Label ID="lblScopeOfWork" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>No Claim : </label>
                    <asp:CheckBox ID="cbNoClaim" runat="server" />
                </div>
                <div class="col-md-4">
                    <label>No Claim Reason : </label>
                    <asp:Label ID="lblNoClaimReason" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Mc Entered Service Date : </label>
                    <asp:Label ID="lblMcEnteredServiceDate" runat="server" CssClass="LabelValue"></asp:Label>

                </div>
                <div class="col-md-4">
                    <label>Service Started Date : </label>
                    <asp:Label ID="lblServiceStartedDate" runat="server" CssClass="LabelValue"></asp:Label>

                </div>
                <div class="col-md-4">
                    <label>Service Ended Date : </label>
                    <asp:Label ID="lblServiceEndedDate" runat="server" CssClass="LabelValue" Text=""></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Kind Attn : </label>
                    <asp:Label ID="lblKindAttn" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Remarks : </label>
                    <asp:Label ID="lblRemarks" runat="server" CssClass="LabelValue" Text=""></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Is Machine Active: </label>
                    <asp:CheckBox ID="cbIsMachineActive" runat="server" Checked="true" />
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlFSR" runat="server" HeaderText="FSR">
        <ContentTemplate>
            <br />
            <%-- <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">IC Ticket</legend>--%>
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <label>Mode Of Payment : </label>
                    <asp:Label ID="lblModeOfPayment" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Operator Name : </label>
                    <asp:Label ID="lblOperatorNameFSR" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Operator Contact No : </label>
                    <asp:Label ID="lblOperatorNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Machine Maintenance Level : </label>
                    <asp:Label ID="lblMachineMaintenanceLevel" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>SIs Rental : </label>
                    <asp:CheckBox ID="cbIsRental" runat="server" />
                </div>
                <div class="col-md-4">
                    <label>Rental Contractor Name : </label>
                    <asp:Label ID="lblRentalName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Rental Contractor Contact No : </label>
                    <asp:Label ID="lblRentalNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Nature Of Complaint : </label>
                    <asp:Label ID="lblNatureOfComplaint" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Observation : </label>
                    <asp:Label ID="lblObservation" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Work Carried Out : </label>
                    <asp:Label ID="lblWorkCarriedOut" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>SE Suggestion : </label>
                    <asp:Label ID="lblReport" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <%--</fieldset>--%>

            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" DataKeyNames="AttachedFileID">
                        <Columns>
                            <asp:TemplateField HeaderText="Attachment Description">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFSRAttachedName" Text='<%# DataBinder.Eval(Container.DataItem, "FSRAttachedName.FSRAttachedName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkFSRDownload_Click" Text="Download"></asp:LinkButton>
                                    <asp:Label ID="lblFileType" Text='<%# DataBinder.Eval(Container.DataItem, "FileType")%>' runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblAttachedFileRemove" runat="server" OnClick="lbFSRAttachedFileRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlAvailabilityOfOtherMachine" runat="server" HeaderText="Other Machine">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvAvailabilityOfOtherMachine" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="AvailabilityOfOtherMachineID">
                        <Columns>
                            <asp:TemplateField HeaderText="Type Of Machine">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeOfMachine" Text='<%# DataBinder.Eval(Container.DataItem, "TypeOfMachine.TypeOfMachine")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Make">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMake" Text='<%# DataBinder.Eval(Container.DataItem, "Make.Make")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbAvailabilityOfOtherMachineRemove" runat="server" OnClick="lbAvailabilityOfOtherMachineRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlServiceCharges" runat="server" HeaderText="Service Charges">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvServiceCharges" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="ServiceChargeID">
                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbClaimRequested" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrInvRequested_N")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ser Prod ID">

                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ser Prod Desc">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" Text='<%# DataBinder.Eval(Container.DataItem, "Date","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Worked Hours">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedHours")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Base Price">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBasePrice" Text='<%# DataBinder.Eval(Container.DataItem, "BasePrice")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td style="border-width: 0px">Discount</td>
                                            <td style="border-width: 0px">
                                                <asp:TextBox ID="txtTaxP" runat="server" CssClass="TextBox" Width="30px"></asp:TextBox>%</td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quotation Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pro. Inv. Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProformaInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ProformaInvoiceNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inv. Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim / Invoice Requested" Visible="false">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbIsClaimRequested" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrInvRequested")%>' Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tsir Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblServiceRemove" runat="server" OnClick="lblServiceRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                    <asp:LinkButton ID="lblServiceEdit" runat="server" OnClick="lblServiceEdit_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"  ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlTSIR" runat="server" HeaderText="TSIR">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvTSIR" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="TsirID" OnRowDataBound="gvTSIR_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href="javascript:collapseExpand('TsirID-<%# Eval("TsirID") %>');">
                                        <img id="imageTsirID-<%# Eval("TsirID") %>" alt="Click to show/hide orders" border="0" src="Images/grid_expand.png" height="10" width="10" /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--  <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbCheck" runat="server" OnCheckedChanged="cbCheck_CheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="TSIR">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TsirNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tsir Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTsirDate" Text='<%# DataBinder.Eval(Container.DataItem, "TsirDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tsir Status">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusID" Text='<%# DataBinder.Eval(Container.DataItem, "Status.StatusID")%>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SRO Code">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceCharge.Material.MaterialCode")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SRO Code Description">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceCharge.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    <%--  </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblCancelTSIR" runat="server" OnClick="lblCancelTSIR_Click">Cancel</asp:LinkButton>--%>
                                    <tr>
                                        <td colspan="100%" style="padding-left: 96px">
                                            <div id="TsirID-<%# Eval("TsirID") %>" style="display: none; position: relative;">
                                                <table>
                                                    <tr>
                                                        <td colspan="100%" style="border-bottom-width: 0px; border-right-width: 0px;">
                                                            <asp:GridView ID="gvAttachedFile" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" DataKeyNames="AttachedFileID" ShowFooter="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Note Type">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFSRAttachedName" Text='<%# DataBinder.Eval(Container.DataItem, "FSRAttachedName.FSRAttachedName")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="File Name" HeaderStyle-Width="250px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                                            <asp:UpdatePanel ID="upManage" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownloadR_Click" Text="Download">
                                                                                    </asp:LinkButton>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="lnkDownload" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lblAttachedFileRemove" runat="server" OnClick="lblAttachedFileRemoveR_Click">Remove</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border-bottom-width: 0px; border-right-width: 0px;">
                                                            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-4 col-sm-12">
                                                                        <label class="modal-label">Name Type</label>
                                                                        <asp:DropDownList ID="ddlFSRAttachedName" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-4 col-sm-12">
                                                                        <label class="modal-label">File</label>
                                                                        <asp:FileUpload ID="fu" runat="server" CssClass="form-control" ViewStateMode="Inherit" Width="200px" />
                                                                    </div>
                                                                    <div class="col-md-4 col-sm-12">
                                                                        <asp:UpdatePanel ID="upManage" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:LinkButton ID="lblAttachedFileAdd" runat="server" OnClick="lblAttachedFileAddR_Click">Add</asp:LinkButton>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:PostBackTrigger ControlID="lblAttachedFileAdd" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlMaterialCharges" runat="server" HeaderText="Material Charges">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                        <div id="divWarrantyDistribution" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Customer Pay %</label>
                                    <asp:TextBox ID="txtCustomerPayPercentage" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" />
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Dealer Pay %</label>
                                    <asp:TextBox ID="txtDealerPayPercentage" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" />
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">AE Pay %</label>
                                    <asp:TextBox ID="txtAEPayPercentage" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" />
                                </div>
                                <div class="col-md-8 text-left">
                                    <asp:Button ID="btnSaveWarrantyDistribution" runat="server" Text="Save Warranty Distribution" CssClass="btn Search" Width="300px" UseSubmitBehavior="true" OnClick="btnSaveWarrantyDistribution_Click" />
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="ServiceMaterialID">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbEdit" runat="server" OnCheckedChanged="cbEdit_CheckedChanged" AutoPostBack="true" />
                                        <asp:LinkButton ID="lbUpdate" runat="server" Text="Update" Visible="false" OnClick="lbUpdate_Click"></asp:LinkButton>
                                        <br />
                                        <br />
                                        <asp:LinkButton ID="lbEditCancel" runat="server" Text="Back" Visible="false" OnClick="lbEditCancel_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item" HeaderStyle-Width="30px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="TextBox" Width="100px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material S/N">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtMaterialSN" runat="server" CssClass="TextBox" Width="80px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox" Width="60px" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Avl Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAvailableQty" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableQty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prime Faulty Part">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbIsFaultyPart" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsFaultyPart")%>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FLD Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialCode")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtDefectiveMaterial" runat="server" CssClass="TextBox" Width="100px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FLD Material S/N">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefectiveMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtDefectiveMaterialSN" runat="server" CssClass="TextBox" Width="80px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recomened Parts">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbRecomenedParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsRecomenedParts")%>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quotation  Parts">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbQuotationParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsQuotationParts")%>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialSource" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSource")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblMaterialSourceID" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSourceID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlMaterialSource" runat="server" CssClass="TextBox" Width="80px" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TSIR No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirNumber")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblTsirID" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlTSIRNumber" runat="server" CssClass="TextBox" Width="122px" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qtn No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Claim No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPONumber" Text='<%# DataBinder.Eval(Container.DataItem, "PONumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parts Invoice">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOldInvoice" Text='<%# DataBinder.Eval(Container.DataItem, "OldInvoice")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCancel" Text="Canceled" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsDeleted")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMaterialRemove" runat="server" OnClick="lblMaterialRemove_Click">
                                            <i class="fa fa-fw fa-times" style="font-size:18px"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlNotes" runat="server" HeaderText="Notes" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvNotes" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" DataKeyNames="ServiceNoteID">
                        <Columns>
                            <asp:TemplateField HeaderText="Note Type">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNoteType" Text='<%# DataBinder.Eval(Container.DataItem, "NoteType.NoteType")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comments">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblComments" Text='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtComments" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created On">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblNoteRemove" runat="server" OnClick="lblNoteRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabechnicianWorkHours" runat="server" HeaderText="Tech Work Hours" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvTechnicianWorkDays" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Technician">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName_ContactName" Text='<%# DataBinder.Eval(Container.DataItem, "UserName_ContactName")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblServiceTechnicianWorkDateID" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceTechnicianWorkDateID")%>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblUserID" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' runat="server" Visible="false"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Worked Day">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkedDay" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Worked Hours">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedHours")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remove">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbWorkedDayRemove" runat="server" OnClick="lbWorkedDayRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabRestore" runat="server" HeaderText="IC Ticket Restore" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <br />
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <label>Restore Date and Time : </label>
                    <asp:Label ID="lblRestoreDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Arrival Back Date and Time : </label>
                    <asp:Label ID="lblArrivalBackDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Complaint Status : </label>
                    <asp:Label ID="lblComplaintStatus" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Customer Remarks : </label>
                    <asp:Label ID="lblCustomerRemarks" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Customer Satisfaction Level : </label>
                    <asp:Label ID="lblCustomerSatisfactionLevel" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabSignature" runat="server" HeaderText="Signature" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <br />
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <label>Technician Name : </label>
                    <asp:Label ID="lblTName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Technician Photo : </label>
                    <asp:Label ID="lblTPhoto" runat="server" CssClass="LabelValue"></asp:Label>
                    <asp:LinkButton ID="lbtnTPhoto" runat="server" OnClick="lbtnFsrSignatureDownload_Click">Technician Photo</asp:LinkButton>
                </div>
                <div class="col-md-4">
                    <label>Technician Sign : </label>
                    <asp:Label ID="lblTSignature" runat="server" CssClass="LabelValue"></asp:Label>
                    <asp:LinkButton ID="lbtnTSignature" runat="server" OnClick="lbtnFsrSignatureDownload_Click">Technician Sign</asp:LinkButton>
                </div>
                <div class="col-md-4">
                    <label>Customer Name : </label>
                    <asp:Label ID="lblCName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Customer Photo : </label>
                    <asp:Label ID="lblCPhoto" runat="server" CssClass="LabelValue"></asp:Label>
                    <asp:LinkButton ID="lbtnCPhoto" runat="server" OnClick="lbtnFsrSignatureDownload_Click">Customer Photo</asp:LinkButton>
                </div>
                <div class="col-md-4">
                    <label>Customer Sign : </label>
                    <asp:Label ID="lblCSignature" runat="server" CssClass="LabelValue"></asp:Label>
                    <asp:LinkButton ID="lbtnCSignature" runat="server" OnClick="lbtnFsrSignatureDownload_Click">Customer Sign</asp:LinkButton>
                </div>

            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlDeclined" runat="server" HeaderText="Declined Info" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <br />
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <label>Declined Requested Date: </label>
                    <asp:Label ID="lblDeclinedDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-4">
                    <label>Declined Requested Reson : </label>
                    <asp:Label ID="lblDeclinedReson" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>

        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>
<asp:Panel ID="pnlAddTechnician" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Technician</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageAssignEngineer" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketAddTechnician ID="UC_ICTicketAddTechnician" runat="server"></UC:UC_ICTicketAddTechnician>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveAssignSE" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveAssignSE_Click" />
        </div>

    </div>
</asp:Panel>


<ajaxToolkit:ModalPopupExtender ID="MPE_AddTechnician" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddTechnician" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlCallInformation" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Call Information</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageCallInformation" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketUpdateCallInformation ID="UC_ICTicketUpdateCallInformation" runat="server"></UC:UC_ICTicketUpdateCallInformation>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnCallInformation" runat="server" Text="Save" CssClass="btn Save" OnClick="btnCallInformation_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_CallInformation" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCallInformation" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlAddFSR" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add FSR</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblFSRMessage" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_AddFSR ID="UC_AddFSR" runat="server"></UC:UC_AddFSR>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateFSR" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateFSR_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddFSR" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddFSR" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlAddFSRAttachments" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add FSR Attachments</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageFsrAttachments" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_AddFSRAttachments ID="UC_AddFSRAttachments" runat="server"></UC:UC_AddFSRAttachments>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateFSRAttachments" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateFSRAttachments_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddFSRAttachments" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddFSRAttachments" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlICTicketAddOtherMachine" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Other Machine</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button4" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageOtherMachine" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketAddOtherMachine ID="UC_ICTicketAddOtherMachine" runat="server"></UC:UC_ICTicketAddOtherMachine>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnICTicketAddOtherMachine" runat="server" Text="Save" CssClass="btn Save" OnClick="btnICTicketAddOtherMachine_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_ICTicketAddOtherMachine" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlICTicketAddOtherMachine" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlICTicketAddServiceCharges" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Service Charge</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button5" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblServiceChargeSessage" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketAddServiceCharges ID="UC_ICTicketAddServiceCharges" runat="server"></UC:UC_ICTicketAddServiceCharges>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnICTicketAddServiceCharges" runat="server" Text="Save" CssClass="btn Save" OnClick="btnICTicketAddServiceCharges_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_ICTicketAddServiceCharges" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlICTicketAddServiceCharges" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlAddTSIR" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add TSIR</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageAddTSIR" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_AddTSIR ID="UC_AddTSIR" runat="server"></UC:UC_AddTSIR>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAddTSIR" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddTSIR_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddTSIR" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddTSIR" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlAddMaterialCharges" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">AddMaterialCharges</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button7" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageMaterialCharges" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketAddMaterialCharges ID="UC_ICTicketAddMaterialCharges" runat="server"></UC:UC_ICTicketAddMaterialCharges>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAddMaterialCharges" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddMaterialCharges_Click" />
            <asp:Button ID="btnAddMaterialAvailability" runat="server" Text="Availability" Width="85px" CssClass="btn Save" OnClick="btnAddMaterialCharges_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddMaterialCharges" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddMaterialCharges" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlICTicketAddNotes" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Notes</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button8" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageNote" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketAddNotes ID="UC_ICTicketAddNotes" runat="server"></UC:UC_ICTicketAddNotes>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAddNotes" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddNotes_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_ICTicketAddNotes" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlICTicketAddNotes" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlAddTechnicianWork" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Technician Work</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button10" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageTechnicianWork" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketAddTechnicianWork ID="UC_ICTicketAddTechnicianWork" runat="server"></UC:UC_ICTicketAddTechnicianWork>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAddTechnicianWork" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddTechnicianWork_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddTechnicianWork" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddTechnicianWork" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlUpdateRestore" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">IC Ticket Restoration</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button9" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageRestore" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketUpdateRestore ID="UC_ICTicketUpdateRestore" runat="server"></UC:UC_ICTicketUpdateRestore>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateRestore" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateRestore_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_UpdateRestore" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlUpdateRestore" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlCustomerFeedback" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Customer Feedback</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button101" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageCustomerFeedback" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_ICTicketCustomerFeedback ID="UC_ICTicketCustomerFeedback" runat="server"></UC:UC_ICTicketCustomerFeedback>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateCustomerFeedback" runat="server" Text="Update" CssClass="btn Save" OnClick="btnUpdateCustomerFeedback_Click" />
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_CustomerFeedback" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomerFeedback" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />



<asp:Panel ID="pnlRequestForDecline" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Request for Decline</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button11" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="col-md-12">
            <asp:Label ID="Label1" runat="server" Text="" CssClass="message" Visible="false" />
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">Reason For Decline</label>
                <asp:TextBox ID="txtDeclineReason" runat="server" TextMode="MultiLine" Width="700px" Height="300px" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>
            <div class="col-md-19 text-center">
                <asp:Button ID="btnSaveRequestForDecline" runat="server" Text="Save" CssClass="btn Save" UseSubmitBehavior="true" OnClientClick="return dateValidation();" OnClick="btnSaveRequestForDecline_Click" />
            </div>
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_RequestForDecline" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlRequestForDecline" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlRequestDateChange" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Request Date Change</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button12" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageRequestDateChange" runat="server" Text="" CssClass="message" Visible="false" />
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Request Date Change</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Requested Date</label>
                    <asp:TextBox ID="txtRequestedDate" runat="server" CssClass="form-control" AutoComplete="SP" onkeyup="return removeText('MainContent_txtRequestedDate');"></asp:TextBox>
                    <asp1:CalendarExtender ID="ceRequestedDate" runat="server" TargetControlID="txtRequestedDate" PopupButtonID="txtRequestedDate" Format="dd/MM/yyyy"></asp1:CalendarExtender>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtRequestedDate" WatermarkText="DD/MM/YYYY"></asp1:TextBoxWatermarkExtender>
                </div>
                <div class="col-md-1 col-sm-12">
                    <label class="modal-label">-</label>
                    <asp:DropDownList ID="ddlRequestedHH" runat="server" CssClass="form-control" Width="70px">
                        <asp:ListItem Value="-1">HH</asp:ListItem>
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1 col-sm-12">
                    <label class="modal-label">-</label>
                    <asp:DropDownList ID="ddlRequestedMM" runat="server" CssClass="form-control" Width="75px">
                        <asp:ListItem Value="0">MM</asp:ListItem>
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>35</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>55</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-6 text-left">
                    <label class="modal-label">-</label>
                    <asp:Button ID="btnSaveRequestDateChange" runat="server" Text="Save" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnSaveRequestDateChange_Click" OnClientClick="return dateValidation();" />
                </div>
            </div>
        </fieldset>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_RequestDateChange" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlRequestDateChange" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlMarginWarrantyRequest" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Margin Warranty Request</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button14" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageMarginWarrantyRequest" runat="server" Text="" CssClass="message" Visible="false" />
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Margin Warranty Request</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Margin Remark</label>
                    <asp:TextBox ID="txtMarginRemarkRequest" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-6 text-left">
                    <label class="modal-label">-</label>
                    <asp:Button ID="btnReqMarginWarrantyChange" runat="server" Text="Save" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnReqMarginWarrantyChange_Click" OnClientClick="return dateValidation();" />
                </div>
            </div>
        </fieldset>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_MarginWarrantyRequest" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlMarginWarrantyRequest" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlMarginWarrantyReject" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueMarginWarrantyReject">Margin Warranty Reject</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button15" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageMarginWarrantyReject" runat="server" Text="" CssClass="message" Visible="false" />
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Margin Warranty Reject</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Remarks</label>
                    <asp:TextBox ID="txtRejectRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-6 text-left">
                    <label class="modal-label">-</label>
                    <asp:Button ID="btnMarginWarrantyReject" runat="server" Text="Save" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnMarginWarrantyReject_Click" OnClientClick="return dateValidation();" />
                </div>
            </div>
        </fieldset>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_MarginWarrantyReject" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlMarginWarrantyReject" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlFsrSignature" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueMarginWarrantyReject">FSR Signature</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button13" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageFsrSignature" runat="server" Text="" CssClass="message" Visible="false" />
            <%--  <asp:Panel ID="pnlDynamicControl" runat="server">
            </asp:Panel>--%>
            <UC:UC_FsrSignature ID="UC_FsrSignature" runat="server"></UC:UC_FsrSignature>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSignSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSignSave_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_FsrSignature" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFsrSignature" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />



<asp:Panel ID="pnlReachedSite" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueReachedSite">Reached in Site</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button16" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblReachedSiteMessage" runat="server" Text="" CssClass="message" Visible="false" />
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Reached Site"</legend>
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Location</label>
                    <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Current HMR Value</label>
                    <asp:TextBox ID="txtHMRValue" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Site Contact Person’s Name</label>
                    <asp:TextBox ID="txtOperatorName" runat="server" CssClass="form-control" AutoComplete="SP"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Site Contact Person’s Number</label>
                    <asp:TextBox ID="txtSiteContactPersonNumber" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Designation</label>
                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnReachedSite" runat="server" Text="Save" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnReachedSite_Click" />
                </div>
            </div>
        </fieldset>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_ReachedSite" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlReachedSite" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlHmrDevUpdate" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueHmrDevUpdate">HMR Deviation Update</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button17" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblHmrDevUpdateMessage" runat="server" Text="" CssClass="message"/>
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">HMR Deviation Update</legend>
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Old HMR Value</label>
                    <asp:TextBox ID="txtOldHMRValue" runat="server" CssClass="form-control" AutoComplete="SP" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Current HMR Value</label>
                    <asp:TextBox ID="txtCurrentHMRValue" runat="server" CssClass="form-control" AutoComplete="SP" onchange="return IsNumbericOnlyCheck(this);"></asp:TextBox>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnHmrDevUpdate" runat="server" Text="Save" CssClass="btn Save" UseSubmitBehavior="true" OnClick="btnHmrDevUpdate_Click" OnClientClick="return ConfirmUpdateHMRDeviation();"/>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_HmrDevUpdate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlHmrDevUpdate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />



<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
<script type="text/javascript">
    function ConfirmUpdateHMRDeviation() {
        var x = confirm("Are you sure you want to Update HMR Deviation?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function collapseExpand(obj) {
        var gvObject = document.getElementById(obj);
        var imageID = document.getElementById('image' + obj);
        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
    }
</script>




<asp:HiddenField ID="hfLatitude" runat="server" />
<asp:HiddenField ID="hfLongitude" runat="server" />
<script> 
    function success(position) {
        const latitude = position.coords.latitude;
        const longitude = position.coords.longitude;
        document.getElementById('MainContent_UC_ICTicketView_hfLatitude').value = latitude;
        document.getElementById('MainContent_UC_ICTicketView_hfLongitude').value = longitude;
        status.textContent = '';
    }
    function error() {
        status.textContent = 'Unable to retrieve your location';
    }

    if (!navigator.geolocation) {
        status.textContent = 'Geolocation is not supported by your browser';

    } else {
        status.textContent = 'Locating…';
        navigator.geolocation.getCurrentPosition(success, error);
    }
</script>
<script src="../../SignJS/signature_pad.js"></script>
<script src="../../SignJS/app.js"></script>
