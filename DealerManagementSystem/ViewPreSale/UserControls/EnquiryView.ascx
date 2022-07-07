<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnquiryView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.EnquiryView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddEnquiry.ascx" TagPrefix="UC" TagName="UC_AddEnquiry" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerViewSoldTo" %>
<%@ Register Src="~/ViewPreSale/UserControls/LeadViewHeader.ascx" TagPrefix="UC" TagName="UC_LeadView" %>
<div class="col-md-12">
    <div class="col-md-12">
        <div class="action-btn">
            <div class="" id="boxHere"></div>
            <div class="dropdown btnactions" id="customerAction">
                <div class="btn Approval">Actions</div>
                <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                    <asp:LinkButton ID="lbEditEnquiry" runat="server" OnClick="lbActions_Click">Edit Enquiry</asp:LinkButton>
                    <asp:LinkButton ID="lbInActive" runat="server" OnClick="lbActions_Click">ConvertToLead</asp:LinkButton>
                    <asp:LinkButton ID="lbReject" runat="server" OnClick="lbActions_Click">Reject</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12 field-margin-top">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Enquiry Details</legend>
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Enquiry Number : </label>
                        <asp:Label ID="lblEnquiryNumber" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Person Name : </label>
                        <asp:Label ID="lblPersonName" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Product : </label>
                        <asp:Label ID="lblProduct" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>District : </label>
                        <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Mail : </label>
                        <asp:Label ID="lblMail" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Enquiry Date : </label>
                        <asp:Label ID="lblEnquiryDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Source : </label>
                        <asp:Label ID="lblSource" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Country : </label>
                        <asp:Label ID="lblCountry" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Address : </label>
                        <asp:Label ID="lblAddress" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Remarks : </label>
                        <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Customer Name : </label>
                        <asp:Label ID="lblCustomerName" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Status : </label>
                        <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>State : </label>
                        <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Mobile : </label>
                        <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <asp1:TabContainer ID="tbpEnquiry" runat="server" ToolTip="Enquiry Info..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="2">
        <asp1:TabPanel ID="tpnlDealer" runat="server" HeaderText="Dealer" Font-Bold="True" ToolTip="">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12 Report">
                        <div class="table-responsive">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpnlCustomer" runat="server" HeaderText="Customer" Font-Bold="True" ToolTip="">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12 Report">
                        <div class="table-responsive">
                            <div class="col-md-12 field-margin-top">
                                <UC:UC_CustomerViewSoldTo ID="CustomerViewSoldTo" runat="server"></UC:UC_CustomerViewSoldTo>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpnlLead" runat="server" HeaderText="Lead" Font-Bold="True" ToolTip="">
            <ContentTemplate>
                <div class="col-md-12 field-margin-top">
                    <UC:UC_LeadView ID="UC_LeadView" runat="server"></UC:UC_LeadView>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>
    </asp1:TabContainer>
</div>

<asp:Panel ID="pnlEnquiry" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Edit Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblAddEnquiryMessage" runat="server" Text="" CssClass="message" />
            <UC:UC_AddEnquiry ID="UC_AddEnquiry" runat="server"></UC:UC_AddEnquiry>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSave_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Enquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEnquiry" BackgroundCssClass="modalBackground" />

<asp:Panel ID="pnlEnquiryReject" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Enquiry Reject</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button7" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="lblMessageResponsible" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="Fieldset5" runat="server">
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Remark</label>
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnEnquiryStatus" runat="server" Text="Save" CssClass="btn Save" OnClick="btnEnquiryStatus_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_EnquiryReject" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEnquiryReject" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlCustomer" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Customer List</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <asp:Label ID="Label1" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">  
                            <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                                EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvCustomer_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                                        <ItemTemplate> 
                                            <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerFullName")%>' runat="server" /> 
                                            <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Contact Person">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EMail">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMail" runat="server">
                                                <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "EMail")%>'><%# DataBinder.Eval(Container.DataItem, "EMail")%></a>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District.District")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemStyle VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                             <asp:Button ID="btnSelectCustomer" runat="server" Text="Select" CssClass="btn Back" OnClick="btnSelectCustomer_Click" Width="75px" Height="25px" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView> 
                        </div>
                    </fieldset>
                </div>
            </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnNewCustomer" runat="server" Text="Save" CssClass="btn Save" OnClick="btnNewCustomer_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_CustomerSelect" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCustomer" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
