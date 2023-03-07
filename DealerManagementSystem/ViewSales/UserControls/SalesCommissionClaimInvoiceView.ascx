<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesCommissionClaimInvoiceView.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SalesCommissionClaimInvoiceView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewPreSale/UserControls/LeadViewHeader.ascx" TagPrefix="UC" TagName="UC_LeadView" %>
<%@ Register Src="~/ViewPreSale/UserControls/SalesQuotationViewHeader.ascx" TagPrefix="UC" TagName="UC_SalesQuotationView" %>
 <%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerViewSoldTo" %>
<asp:GridView ID="gvClaimInvoice" runat="server" AutoGenerateColumns="false" Width="100%"     CssClass="table table-bordered table-condensed Grid"   PageSize="20" >
    <Columns> 
        <asp:TemplateField HeaderText="Invoice Number">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblSalesCommissionClaimInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesCommissionClaimInvoiceID")%>' runat="server" Visible="false" />
                <asp:Label ID="lblClaimID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Invoice Date">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblClaimDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Dealer">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Dealer Name">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Grand Total">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblGrandTotal" Text='<%# DataBinder.Eval(Container.DataItem, "GrandTotal","{0:n}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="TCS Tax%">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblTCSTax" Text='<%# DataBinder.Eval(Container.DataItem, "TCSTax")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="TCS Value ">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblTCSValue" Text='<%# DataBinder.Eval(Container.DataItem, "TCSValue")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SAP Doc">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblSAPDoc" Text='<%# DataBinder.Eval(Container.DataItem, "SAPDoc")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="AE Inv. Accounted Date"><%--SAP Posting Date--%>
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblSAPPostingDate" Text='<%# DataBinder.Eval(Container.DataItem, "SAPPostingDate","{0:d}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Payment Voucher. No"><%-- SAP Clearing Document --%>
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblSAPClearingDocument" Text='<%# DataBinder.Eval(Container.DataItem, "SAPClearingDocument")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Payment Date"><%-- SAP Clearing Date--%>
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblSAPClearingDate" Text='<%# DataBinder.Eval(Container.DataItem, "SAPClearingDate","{0:d}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Payment Value"><%--SAP Invoice Value--%>
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblSAPInvoiceValue" Text='<%# DataBinder.Eval(Container.DataItem, "SAPInvoiceValue","{0:n}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="TDS Value"><%--SAP Invoice Value--%>
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblSAPInvoiceTDSValue" Text='<%# DataBinder.Eval(Container.DataItem, "SAPInvoiceTDSValue","{0:n}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="IsVerified"><%--SAP Invoice Value--%>
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblIsVerified" Text='<%# DataBinder.Eval(Container.DataItem, "IsVerified")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField> 
    </Columns>
    <AlternatingRowStyle BackColor="#ffffff" />
    <FooterStyle ForeColor="White" />
    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
</asp:GridView>
<asp:GridView ID="gvClaimInvoiceItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
    <Columns>

        <asp:TemplateField HeaderText="Material">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblSalesCommissionClaimItemInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.SalesCommissionClaimInvoiceItemID")%>' runat="server" Visible="false" />

                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialCode")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Material Desc">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SAC/HSN Code">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.HSN")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="55px">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Qty")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Rate">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblIRate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Rate","{0:n}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField> 
        <asp:TemplateField HeaderText="Taxable Value">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.TaxableValue","{0:n}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CGST %">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.CGST")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CGSTValue">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.CGSTValue","{0:n}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SGST %">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.SGST")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SGSTValue">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblUnitOM" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.SGSTValue","{0:n}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="IGST %">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.IGST")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="IGSTValue">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
            <ItemTemplate>
                <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.IGSTValue","{0:n}")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle BackColor="#ffffff" />
    <FooterStyle ForeColor="White" />
    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
</asp:GridView>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

<asp:TabContainer ID="tbpSaleQuotation" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="10">
  
    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Claim">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Quotation</legend>
                    <div class="col-md-12 View">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Claim Number : </label>
                                <asp:Label ID="lblClaimNumber" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Claim Date : </label>
                                <asp:Label ID="lblClaimDate" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Invoice Number : </label>
                                <asp:Label ID="lblInvoiceNumber" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Invoice Date : </label>
                                <asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
                            </div>
                             <div class="col-md-12">
                                <label>Dealer : </label>
                                <asp:Label ID="lblDealer" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Sales Invoice Number : </label>
                                <asp:Label ID="lblSalesInvoiceNumber" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Sales Invoice Date : </label>
                                <asp:Label ID="lblSalesInvoiceDate" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Equipment Serial No : </label>
                                <asp:Label ID="lblEquipmentSerialNo" runat="server"></asp:Label>
                            </div>
                           
                            <div class="col-md-12">
                                <label>Requested By : </label>
                                <asp:Label ID="lblRequestedBy" runat="server"></asp:Label>
                            </div>
                           
                        </div>
                        <div class="col-md-4">
                             <div class="col-md-12">
                                <label>Approved1By : </label>
                                <asp:Label ID="lblApproved1By" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Approved1On : </label>
                                <asp:Label ID="lblApproved1On" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Approved2By : </label>
                                <asp:Label ID="lblApproved2By" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Approved2On : </label>
                                <asp:Label ID="lblApproved2On" runat="server"></asp:Label>
                            </div>

                        </div>
                    </div>

                    <div class="col-md-12 View">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Material : </label>
                                <asp:Label ID="lblMaterial" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Material Des : </label>
                                <asp:Label ID="lblMaterialDescription" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Qty : </label>
                                <asp:Label ID="lblQty" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Amount : </label>
                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Base Tax : </label>
                                <asp:Label ID="lblBaseTax" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Approved 1 Amount : </label>
                                <asp:Label ID="lblApproved1Amount" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Approved 2 Amount : </label>
                                <asp:Label ID="lblApproved2Amount" runat="server"></asp:Label>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Approved 1 Remarks : </label>
                                <asp:Label ID="lblApproved1Remarks" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Approved 2 Remarks : </label>
                                <asp:Label ID="lblApproved2Remarks" runat="server"></asp:Label>
                            </div>

                        </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="tpnlProduct" runat="server" HeaderText="Quotation">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_SalesQuotationView ID="UC_SalesQuotationView" runat="server"></UC:UC_SalesQuotationView>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TabLead" runat="server" HeaderText="Lead">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_LeadView ID="UC_LeadView" runat="server"></UC:UC_LeadView>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TabCustomer" runat="server" HeaderText="Customer">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_CustomerViewSoldTo ID="CustomerViewSoldTo" runat="server"></UC:UC_CustomerViewSoldTo>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>
