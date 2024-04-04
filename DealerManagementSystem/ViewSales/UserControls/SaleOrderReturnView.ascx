<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaleOrderReturnView.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SaleOrderReturnView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Panel ID="PnlSaleOrderReturnView" runat="server" class="col-md-12">
    <div class="col-md-12">
        <div class="action-btn">
            <div class="" id="boxHere"></div>
            <div class="dropdown btnactions" id="customerAction">
                <div class="btn Approval">Actions</div>
                <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                    <asp:LinkButton ID="lbCancel" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmCancel();">Cancel</asp:LinkButton>
                    <asp:LinkButton ID="lbApprove" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmApprove();">Approve</asp:LinkButton>
                    <asp:LinkButton ID="lbCreateCreditNote" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmApprove();">Create Credit Note</asp:LinkButton>
                    <asp:LinkButton ID="lbPreviewCreditNote" runat="server" OnClick="lbActions_Click">Preview Credit Note</asp:LinkButton>
                    <asp:LinkButton ID="lbDowloadCreditNote" runat="server" OnClick="lbActions_Click">Dowload Credit Note</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12 field-margin-top" runat="server" id="divPoReturnView">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Sales Return</legend>
            <div class="col-md-12 View">
                <div class="col-md-3">
                    <div class="col-md-12">
                        <label>Sales Return Number : </label>
                        <asp:Label ID="lblSaleOrderReturnNumber" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Sales Return Date : </label>
                        <asp:Label ID="lblSaleOrderReturnDate" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>SO Invoice Number : </label>
                        <asp:Label ID="lblInvoiceNumber" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>SO Invoice Date : </label>
                        <asp:Label ID="lblInvoiceDate" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="col-md-12">
                        <label>Dealer : </label>
                        <asp:Label ID="lblSODealer" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Dealer Office : </label>
                        <asp:Label ID="lblDealerOffice" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Division : </label>
                        <asp:Label ID="lblDivision" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Order Type : </label>
                        <asp:Label ID="lblSaleOrderType" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="col-md-12">
                        <label>Return Status : </label>
                        <asp:Label ID="lblSaleOrderReturnStatus" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Customer : </label>
                        <asp:Label ID="lblCustomer" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Contact Person : </label>
                        <asp:Label ID="lblContactPerson" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Contact Person Number : </label>
                        <asp:Label ID="lblContactPersonNumber" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="col-md-12">
                        <label>Value : </label>
                        <asp:Label ID="lblValue" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Taxable Value : </label>
                        <asp:Label ID="lblTaxableValue" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Tax Value : </label>
                        <asp:Label ID="lblTaxValue" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Net Value : </label>
                        <asp:Label ID="lblNetValue" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>
                </div>
            </div>
        </fieldset>
        <asp:Label ID="lblMessageSoReturn" runat="server" Text="" CssClass="message" />
        <asp1:TabContainer ID="tbpSalesReturn" runat="server" ToolTip="Sales Return Info." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
            <asp1:TabPanel ID="tbPSalesReturnHeader" runat="server" HeaderText="Sales Return Header" Font-Bold="True" ToolTip="">
                <ContentTemplate>
                    <div class="col-md-12 field-margin-top">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">SO Header</legend>
                            <div class="col-md-12 View">
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label>Created By : </label>
                                        <asp:Label ID="lblCreatedBy" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Credit Note Number : </label>
                                        <asp:Label ID="lblCreditNoteNumber" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Credit Note Date : </label>
                                        <asp:Label ID="lblCreditNoteDate" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label>Remarks : </label>
                                        <asp:Label ID="lblRemarks" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Approved By : </label>
                                        <asp:Label ID="lblApprovedBy" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Approved Date : </label>
                                        <asp:Label ID="lblApprovedDate" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label>Cancelled By : </label>
                                        <asp:Label ID="lblCancelledBy" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Cancelled Date : </label>
                                        <asp:Label ID="lblCancelledDate" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </ContentTemplate>
            </asp1:TabPanel>
            <asp1:TabPanel ID="tpnlSOReturnItem" runat="server" HeaderText="Sales Return Item" Font-Bold="True" ToolTip="">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <div class="table-responsive">
                                <asp:GridView ID="gvSoReturnItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                <asp:Label ID="lblSaleOrderReturnItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderReturnItemID")%>' runat="server" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Description">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HSN Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.HSN")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Delivered Qty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Qty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.BaseUnit")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Value","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Taxable Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaxableAmount" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.CGST")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.CGSTValue")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SGST")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SGSTValue")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.IGST")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.IGSTValue")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Item Discount Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.ItemDiscountValue","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Freight Amount">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFreightAmount" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.FreightAmount","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Net Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.NetAmount","{0:n}")%>' runat="server"></asp:Label>
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
        </asp1:TabContainer>
    </div>
</asp:Panel>

<script type="text/javascript">
    function ConfirmCancel() {
        var x = confirm("Are you sure you want to Cancel?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmDeleteItem() {
        var x = confirm("Are you sure you want to Delete Item?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmApprove() {
        var x = confirm("Are you sure you want to Approve?");
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>
