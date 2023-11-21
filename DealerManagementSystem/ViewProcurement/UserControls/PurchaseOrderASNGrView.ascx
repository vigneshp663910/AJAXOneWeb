<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderASNGrView.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderASNGrView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbGrCancel" runat="server" OnClick="lbActions_Click">Gr Cancel</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Gr Details</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Gr Number : </label>
                    <asp:Label ID="lblGrNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Asn Number : </label>
                    <asp:Label ID="lblAsnNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>PO Number : </label>
                    <asp:Label ID="lblPoNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Cancelled By : </label>
                    <asp:Label ID="lblCancelledBy" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Gr Date : </label>
                    <asp:Label ID="lblGrDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Asn Date : </label>
                    <asp:Label ID="lblAsnDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>PO Date : </label>
                    <asp:Label ID="lblPoDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Posted By : </label>
                    <asp:Label ID="lblPostedBy" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Cancelled On : </label>
                    <asp:Label ID="lblCancelledOn" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Vendor : </label>
                    <asp:Label ID="lblVendor" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gr Status : </label>
                    <asp:Label ID="lblAsnStatus" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Posted On : </label>
                    <asp:Label ID="lblPostedOn" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <asp1:TabContainer ID="tbpGr" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
        <asp1:TabPanel ID="tpnlGrItem" runat="server" HeaderText="GR Item" Font-Bold="True">
            <ContentTemplate>
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;"></legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="GVGr" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Material Code">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asn Item">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAsnItem" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.AsnItem")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asn Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAsnQty" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.Qty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivered Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredQty" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveredQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceivedQty" Text='<%# DataBinder.Eval(Container.DataItem, "ReceivedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Saleable Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaleableQty" Text='<%# DataBinder.Eval(Container.DataItem, "SaleableQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Blocked Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblBlockedQty" Text='<%# DataBinder.Eval(Container.DataItem, "BlockedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Returned Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblReturnedQty" Text='<%# DataBinder.Eval(Container.DataItem, "ReturnedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Weight">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetWeight" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.NetWeight")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Uom Weight">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUomWeight" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.UomWeight")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PackCount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPackCount" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PackCount")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Uom PackCount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUomPackCount" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.UomPackCount")%>' runat="server"></asp:Label>
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
            </ContentTemplate>
        </asp1:TabPanel>

        <asp1:TabPanel ID="tpnlGrPOItem" runat="server" HeaderText="PO" Font-Bold="True">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12 field-margin-top">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Gr Details</legend>
                            <div class="col-md-12 View">
                                <div class="col-md-4">
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
                                <div class="col-md-4">
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
                                <div class="col-md-4">
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
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;"></legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="GVGrPO" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Material Code">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.POItem")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Quantity")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Price")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Discount")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Taxable Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.TaxableValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Material.CGST")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGSTValue">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Material.CGSTValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Material.SGST")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SGSTValue">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Material.SGSTValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Material.IGST")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IGSTValue">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem.PurchaseOrderItem.Material.IGSTValue")%>' runat="server"></asp:Label>
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
            </ContentTemplate>
        </asp1:TabPanel>
    </asp1:TabContainer>
</div>
<asp:Panel ID="pnlGrCancel" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Gr Cancel</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageGrCancel" runat="server" Text="" CssClass="message" Visible="false" />
        <div class="col-md-12">
            <div class="col-md-12 col-sm-12">
                <label>Remarks</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnGrCancel" runat="server" Text="Save" CssClass="btn Save" OnClick="btnGrCancel_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_GrCancel" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlGrCancel" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
