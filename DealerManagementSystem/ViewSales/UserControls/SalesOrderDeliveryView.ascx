<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderDeliveryView.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SalesOrderDeliveryView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerView" %>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <%--<asp:LinkButton ID="lbGenerateQuotation" runat="server" OnClick="lbActions_Click">Generate Quotation</asp:LinkButton>--%>
                <asp:LinkButton ID="lbGenerateInvoice" runat="server" OnClick="lbActions_Click">Generate Invoice</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Sale Order Delivery</legend>
        <div class="col-md-12 View">
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>Delivery No & Dt: </label>
                    <asp:Label ID="lblDeliveryNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Delivery Dt: </label>
                    <asp:Label ID="lblDeliveryDate" runat="server" CssClass="LabelValue"></asp:Label>

                </div>
                <div class="col-md-12">
                    <label>Invoice No & Dt: </label>
                    <asp:Label ID="lblInvoiceNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Invoice Dt: </label>
                    <asp:Label ID="lblInvoiceDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblDealer" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dealer Office : </label>
                    <asp:Label ID="lblDealerOffice" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Division : </label>
                    <asp:Label ID="lblDivision" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>Customer : </label>
                    <asp:Label ID="lblCustomer" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Order Type : </label>
                    <asp:Label ID="lblSaleOrderType" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Equipment : </label>
                    <asp:Label ID="lblEquipment" runat="server" CssClass="LabelValue"></asp:Label>
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
                    <asp:Label ID="lblNetAmount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp1:TabContainer ID="tbpContainer" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
    <asp1:TabPanel ID="tpnlSODeliveryItem" runat="server" HeaderText="Delivery Item" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <asp:GridView ID="gvSODeliveryItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                    <Columns>
                        <asp:TemplateField HeaderText="Material">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblSaleOrderDeliveryItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItemID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Desc">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HSN Code">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.HSN")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Qty","{0:n}")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblBaseUnit" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.BaseUnit")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.Value","{0:n}")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discount">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblDiscountValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.DiscountValue","{0:n}")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Taxable Amount">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CGST">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblCGST" Text='<%#  DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.CGST")  %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SGST">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.SGST")  %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IGST">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblCGST" Text='<%#  DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.IGST") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--   <asp:TemplateField HeaderText="Tax">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblTax" Text='<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.SGST")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.CGST")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.IGST"))).ToString("N2") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="CGST Value">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblTaxValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.CGSTValue") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="SGST Value">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblTaxValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.SGSTValue") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="IGST Value">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblTaxValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.Material.IGSTValue") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net Amount">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItem.NetAmount","{0:n}")%>' runat="server"></asp:Label>
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
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlSO" runat="server" HeaderText="SO" Font-Bold="True">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 field-margin-top">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">SO Details</legend>
                        <div class="col-md-12 View">
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Quotation No & Dt: </label>
                                    <asp:Label ID="lblQuotationNumber" runat="server" CssClass="LabelValue"></asp:Label><asp:Label ID="lblQuotationDate" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Sale Order No & Dt : </label>
                                    <asp:Label ID="lblSaleOrderNumber" runat="server" CssClass="LabelValue"></asp:Label><asp:Label ID="lblSaleOrderDate" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Proforma Invoice No & Dt : </label>
                                    <asp:Label ID="lblProformaInvoiceNumber" runat="server" CssClass="LabelValue"></asp:Label><asp:Label ID="lblProformaInvoiceDate" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Expected Delivery Date : </label>
                                    <asp:Label ID="lblSOExpectedDeliveryDate" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Ref Number : </label>
                                    <asp:Label ID="lblRefNumber" runat="server" CssClass="LabelValue"> 
                                    </asp:Label>
                                    <asp:Label ID="lblRefDate" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Contact Person Number : </label>
                                    <asp:Label ID="lblContactPersonNumber" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Product : </label>
                                    <asp:Label ID="lblProduct" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <label>Equipment Serial No : </label>
                                        <asp:Label ID="lblEquipmentSerialNo" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <label>Frieght PaidBy : </label>
                                        <asp:Label ID="lblFrieghtPaidBy" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <label>Insurance Paid By : </label>
                                    <asp:Label ID="lblInsurancePaidBy" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Remarks : </label>
                                    <asp:Label ID="lblRemarks" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>

                            </div>
                            <div class="col-md-4">
                                <div class="col-md-12">
                                    <label>Attn : </label>
                                    <asp:Label ID="lblAttn" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Sales Engnieer : </label>
                                    <asp:Label ID="lblSalesEngnieer" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Tax Type: </label>
                                    <asp:Label ID="lblTaxType" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Header Discount Percent : </label>
                                    <asp:Label ID="lblHeaderDiscountPercent" runat="server" CssClass="LabelValue"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <label>Net Amount : </label>
                                    <asp:Label ID="lblGrossAmount" runat="server" CssClass="LabelValue"></asp:Label>
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
                        <asp:GridView ID="gvSOItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                            <Columns>
                                <asp:TemplateField HeaderText="Material Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "Value")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Taxable Value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTax" Text='<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.SGST")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.CGST")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.IGST"))).ToString("N2") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxValue" Text='<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.CGSTValue")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.SGSTValue")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.IGSTValue"))).ToString("N2") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetValue" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmount","{0:n}")%>' runat="server"></asp:Label>
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
    <asp1:TabPanel ID="tpnlShipToParty" runat="server" HeaderText="Address" Height="350px">
        <ContentTemplate>
            <br />
            <div class="col-md-12">
                <div class="col-md-12 field-margin-top">
                    <fieldset class="fieldset-border">
                        <%--<legend style="background: none; color: #007bff; font-size: 17px;">Address</legend>--%>
                        <div class="col-md-12 View">
                            <div class="col-md-4">
                                <label>Billing Address : </label>
                                <asp:Label ID="lblBillingAddress" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <label>Delivery Address : </label>
                                <asp:Label ID="lblDeliveryAddress" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="TabCustomer" runat="server" HeaderText="Customer">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_CustomerView ID="UC_CustomerView" runat="server"></UC:UC_CustomerView>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>