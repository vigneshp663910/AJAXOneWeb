<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="Enquiry.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Enquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerViewSoldTo" %>
<%@ Register Src="~/ViewPreSale/UserControls/LeadViewHeader.ascx" TagPrefix="UC" TagName="UC_LeadView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <asp:Panel ID="pnlEnquiry" runat="server" CssClass="Popup" Style="display: none">
            <div class="PopupHeader clearfix">
                <span id="PopupDialogue">Create Enquiry</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
                    <asp:Button ID="Button6" runat="server" Text="X" CssClass="PopupClose" /></a>
            </div>
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Create Enquiry</legend>
                    <div class="model-scroll">
                        <asp:Label ID="lblAddEnquiryMessage" runat="server" Text="" CssClass="message" />
                        <div class="col-md-12">
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Customer Name<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Enquiry Date<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtEnquiryDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp1:CalendarExtender ID="calendarextender1" runat="server" TargetControlID="txtEnquiryDate" PopupButtonID="txtEnquiryDate" Format="dd/MM/yyyy HH:mm:ss" />
                                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtEnquiryDate" WatermarkText="DD/MM/YYYY HH:mm:ss" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Contact Person Name</label>
                                <asp:TextBox ID="txtPersonName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Contact Person Mobile<samp style="color: red">*</samp></label>
                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">EMail</label>
                                <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Source<samp style="color: red">*</samp></label>
                                <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" DataTextField="Source" DataValueField="SourceID" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Country<samp style="color: red">*</samp></label>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">State<samp style="color: red">*</samp></label>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">District<samp style="color: red">*</samp></label>
                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Address</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="40" autocomplete="off"></asp:TextBox>
                                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtAddress" WatermarkText="Address 1" WatermarkCssClass="WatermarkCssClass" />
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <label class="modal-label">Product</label>
                                <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-12 col-sm-12">
                                <label class="modal-label">Remarks</label>
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" Rows="5" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="BtnSave_Click" />
                                <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="btn Back" OnClick="BtnBack_Click" />
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="MPE_Enquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEnquiry" BackgroundCssClass="modalBackground" />
        <div style="display: none">
            <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </div>
        <div class="col-md-12" id="divEnquiryView" runat="server" visible="false">
            <div class="" id="boxHere"></div>
            <div class="back-buttton" id="backBtn">
                <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
            </div>
            <div class="col-md-12">
                <div class="col-md-12">
                    <div class="action-btn">
                        <div class="" id="boxHere"></div>
                        <div class="dropdown btnactions" id="customerAction">
                            <%--<asp:Button ID="BtnActions" runat="server" CssClass="btn Approval" Text="Actions" />--%>
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
                                <UC:uc_leadview id="UC_LeadView" runat="server"></UC:uc_leadview>
                            </div>
                        </ContentTemplate>
                    </asp1:TabPanel>
                </asp1:TabContainer>
            </div>
        </div>
        <div class="col-md-12 Report" id="divEnquiryList" runat="server">
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Enquiry Number</label>
                            <asp:TextBox ID="txtSEnquiryNumber" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Customer Name</label>
                            <asp:TextBox ID="txtSCustomerName" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Country</label>
                            <asp:DropDownList ID="ddlSCountry" runat="server" CssClass="form-control" DataTextField="Country" DataValueField="CountryID" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">State</label>
                            <asp:DropDownList ID="ddlSState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" AutoPostBack="true" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">District</label>
                            <asp:DropDownList ID="ddlSDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">From Date</label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp1:CalendarExtender ID="calendarextender2" runat="server" TargetControlID="txtFromDate" PopupButtonID="txtFromDate" Format="dd/MM/yyyy" />
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtFromDate" WatermarkText="DD/MM/YYYY" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">To Date</label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp1:CalendarExtender ID="calendarextender3" runat="server" TargetControlID="txtToDate" PopupButtonID="txtToDate" Format="dd/MM/yyyy" />
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtToDate" WatermarkText="DD/MM/YYYY" />
                        </div>
                        <div class="col-md-2 text-left">
                            <label class="modal-label">-</label>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn Search" OnClick="BtnSearch_Click" />
                            <asp:Button ID="BtnAdd" runat="server" Text="Add Enquiry" CssClass="btn Save" Width="100px" OnClick="BtnAdd_Click" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <%--<div class="col-md-12">--%>
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                    <div class="col-md-12 Report">
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Enquiry(s):</td>

                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnEnqArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnEnqArrowLeft_Click" /></td>
                                            <td>
                                                <asp:ImageButton ID="ibtnEnqArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnEnqArrowRight_Click" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenEnquiryID" runat="server" />
                        <asp:GridView ID="gvEnquiry" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="5" runat="server" ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvEnquiry_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Enquiry Number" DataField="EnquiryNumber"></asp:BoundField>
                                <asp:BoundField HeaderText="Enquiry Date" DataField="EnquiryDate"></asp:BoundField>
                                <asp:BoundField HeaderText="Customer Name" DataField="CustomerName"></asp:BoundField>
                                <asp:BoundField HeaderText="PersonName" DataField="PersonName"></asp:BoundField>
                                <asp:BoundField HeaderText="Mail" DataField="Mail"></asp:BoundField>
                                <asp:BoundField HeaderText="Mobile" DataField="Mobile"></asp:BoundField>
                                <asp:BoundField HeaderText="Country" DataField="Country.Country"></asp:BoundField>
                                <asp:BoundField HeaderText="State" DataField="State.State"></asp:BoundField>
                                <asp:BoundField HeaderText="District" DataField="District.District"></asp:BoundField>
                                <asp:BoundField HeaderText="Address" DataField="Address"></asp:BoundField>
                                <asp:BoundField HeaderText="Product" DataField="Product"></asp:BoundField>
                                <asp:BoundField HeaderText="Remarks" DataField="Remarks"></asp:BoundField>
                                <asp:BoundField HeaderText="Source" DataField="Source.Source"></asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="Status.Status"></asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="BtnView" runat="server" Text="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EnquiryID")%>' CssClass="btn Back" Width="75px" Height="25px" OnClick="BtnView_Click" />
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
            <%--</div>--%>
            <asp:Panel ID="pnlFoloowUpStatus" runat="server" CssClass="Popup" Style="display: none">
                <div class="PopupHeader clearfix">
                    <span id="PopupDialogue">PreSales Status</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
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
                        <asp:Button ID="btnFoloowUpStatus" runat="server" Text="Save" CssClass="btn Save" OnClick="btnFoloowUpStatus_Click" />
                    </div>
                </div>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="MPE_FoloowUpStatus" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlFoloowUpStatus" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
        </div>
</asp:Content>
