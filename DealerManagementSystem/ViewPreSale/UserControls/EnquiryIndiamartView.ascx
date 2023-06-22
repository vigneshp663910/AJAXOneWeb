<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryIndiamartView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.EnquiryIndiamartView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddEnquiry.ascx" TagPrefix="UC" TagName="UC_AddEnquiry" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbConverttoEnquiry" runat="server" OnClick="lbActions_Click">Convert to Enquiry</asp:LinkButton>
                <asp:LinkButton ID="lbReject" runat="server" OnClick="lbActions_Click">Reject</asp:LinkButton>
                <asp:LinkButton ID="lbInProgress" runat="server" OnClick="lbActions_Click">InProgress</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Enquiry From Indiamart Details</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Query ID : </label>
                    <asp:Label ID="lblVQueryID" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Sender Name : </label>
                    <asp:Label ID="lblSenderName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Company Name : </label>
                    <asp:Label ID="lblCompanyName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>State : </label>
                    <asp:Label ID="lblState" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Message : </label>
                    <asp:Label ID="lblVMessage" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Mobile Alt : </label>
                    <asp:Label ID="lblMobileAlt" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Query Type : </label>
                    <asp:Label ID="lblQueryType" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:Label ID="lblMobile" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Address : </label>
                    <asp:Label ID="lblAddress" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Country : </label>
                    <asp:Label ID="lblCountry" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Date : </label>
                    <asp:Label ID="lblDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Email Alt : </label>
                    <asp:Label ID="lblEmailAlt" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblVStatus" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Sender Email : </label>
                    <asp:Label ID="lblSenderEmail" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>City : </label>
                    <asp:Label ID="lblCity" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Product Name : </label>
                    <asp:Label ID="lblProductName" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Receiver Mob : </label>
                    <asp:Label ID="lblReceiverMob" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<asp1:TabContainer ID="tbpEnquiry" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="1">
    <asp1:TabPanel ID="tpnlEnquiryPresaleStatus" runat="server" HeaderText="Status History" Font-Bold="True">
        <ContentTemplate>
            <asp:GridView ID="gvEnquiryPresaleStatus" runat="server" Width="100%" CssClass="table table-bordered table-condensed Grid" AutoGenerateColumns="false"
                EmptyDataText="No Data Found" >
                <Columns>
                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                        <ItemTemplate>
                            <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex+1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Status" DataField="PreSaleStatus" />
                    <asp:BoundField HeaderText="Remark" DataField="Remark" />
                    <asp:BoundField HeaderText="Reason" DataField="Reason" />
                    <asp:BoundField HeaderText="Created By" DataField="ContactName" />
                    <asp:BoundField HeaderText="Created On" DataField="CreatedOn" />
                </Columns>
                <AlternatingRowStyle BackColor="#ffffff" />
                <FooterStyle ForeColor="White" />
                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
            </asp:GridView>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>
<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton>
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>

<asp:Panel ID="pnlAddEnquiry" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblAddEnquiryMessage" runat="server" CssClass="message" Visible="false" />
            <div class="col-md-6 col-sm-12">
                <label class="modal-label">Query ID</label>
                <asp:Label ID="lblQueryIDAdd" runat="server" Text="" CssClass="message" />
            </div>
            <UC:UC_AddEnquiry ID="UC_AddEnquiry" runat="server"></UC:UC_AddEnquiry>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClientClick="return ConfirmCreate();" OnClick="btnSave_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddEnquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddEnquiry" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlRejectEnquiry" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueRejectEnquiry">Reject Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnRejectEnquiryClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="lblRejectEnquiryMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Query ID</label>
                        <asp:Label ID="lblQueryID" runat="server" Text="" CssClass="message" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Remarks</label>
                        <asp:DropDownList ID="ddlRejectionRemarks" runat="server" CssClass="form-control" DataTextField="Remark" DataValueField="EnquiryRemarkID" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Reason</label>
                        <asp:TextBox ID="txtRejectEnquiryReason" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnRejectEnquiry" runat="server" Text="Save" CssClass="btn Save" OnClick="btnRejectEnquiry_Click" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_RejectEnquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlRejectEnquiry" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlInprogressEnquiry" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueInprogressEnquiry">Inprogress Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnInprogressEnquiryClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="lblInprogressEnquiryMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="model-scroll">
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Query ID</label>
                        <asp:Label ID="lblInProgressQueryID" runat="server" Text="" CssClass="message" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Remarks</label>
                        <asp:DropDownList ID="ddlInprogressRemarks" runat="server" CssClass="form-control" DataTextField="Remark" DataValueField="EnquiryRemarkID" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Reason</label>
                        <asp:TextBox ID="txtInprogressEnquiryReason" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnInprogressEnquiry" runat="server" Text="Save" CssClass="btn Save" OnClick="btnInprogressEnquiry_Click" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_InprogressEnquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlInprogressEnquiry" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
