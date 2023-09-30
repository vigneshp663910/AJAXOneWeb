<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderView.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddEnquiry.ascx" TagPrefix="UC" TagName="UC_AddEnquiry" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerViewSoldTo" %>
<%@ Register Src="~/ViewPreSale/UserControls/LeadViewHeader.ascx" TagPrefix="UC" TagName="UC_LeadView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLead.ascx" TagPrefix="UC" TagName="UC_AddLead" %>

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbReleasePO" runat="server" OnClick="lbActions_Click">Release PO</asp:LinkButton>
                <asp:LinkButton ID="lbEditPO" runat="server" OnClick="lbActions_Click">Edit PO</asp:LinkButton> 
                <asp:LinkButton ID="lbCancelPO" runat="server" OnClick="lbActions_Click">Cancel PO</asp:LinkButton>
                <asp:LinkButton ID="lbReleaseApprove" runat="server" OnClick="lbActions_Click">Release Approve</asp:LinkButton>
                <asp:LinkButton ID="lbCancelApprove" runat="server" OnClick="lbActions_Click">Cancel Approve</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Enquiry Details</legend>
        <div class="col-md-12 View">
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>PO Number : </label>
                    <asp:Label ID="lblPurchaseOrderNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Order To : </label>
                    <asp:Label ID="lblOrderTo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Division : </label>
                    <asp:Label ID="lblDivision" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Ref No : </label>
                    <asp:Label ID="lblRefNo" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>PO Date : </label>
                    <asp:Label ID="lblPurchaseOrderDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Order Type : </label>
                    <asp:Label ID="lblOrderType" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Receiving Location : </label>
                    <asp:Label ID="lblReceivingLocation" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblPORemarks" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblPODealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Vendor : </label>
                    <asp:Label ID="lblPOVendor" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Expected Delivery Date : </label>
                    <asp:Label ID="lblExpectedDeliveryDate" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>Price : </label>
                    <asp:Label ID="lblPrice" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Discount : </label>
                    <asp:Label ID="lblDiscount" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Taxable Amount : </label>
                    <asp:Label ID="lblTaxableAmount" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Tax Amount : </label>
                    <asp:Label ID="lblTaxAmount" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gross Amount : </label>
                    <asp:Label ID="lblGrossAmount" runat="server" CssClass="label"></asp:Label>
                </div>

                
            </div>
        </div>
    </fieldset>
</div>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp1:TabContainer ID="tbpEnquiry" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
    <asp1:TabPanel ID="tpnlCustomer" runat="server" HeaderText="PO Item" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">PO Item</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvPOItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseOrderItemID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItemID")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "POItem")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HSN">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "Material.HSN")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblf_uom" Text='<%# DataBinder.Eval(Container.DataItem, "Material.BaseUnit")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Unit Price">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Price">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscountAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Taxable Amount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTax" Text='<%# DataBinder.Eval(Container.DataItem, "Tax","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderStatus.PurchaseOrderStatus","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnCancelPoItem" runat="server" Text="Cancel" CssClass="btn Back" OnClick="btnCancelPoItem_Click" Width="75px" Height="25px" />
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
                    </fieldset>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlLead" runat="server" HeaderText="ASN" Font-Bold="True">
        <ContentTemplate>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>


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
            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn Save" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Enquiry" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEnquiry" BackgroundCssClass="modalBackground" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
