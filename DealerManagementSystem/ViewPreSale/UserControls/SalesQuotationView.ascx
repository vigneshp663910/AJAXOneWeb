<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesQuotationView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.SalesQuotationView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <%-- <asp:LinkButton ID="lbActions" runat="server" OnClick="lbActions_Click">Add Effort</asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbActions_Click">Add Expense</asp:LinkButton>
                <asp:LinkButton ID="lbtnStatusChangeToClose" runat="server" OnClick="lbActions_Click">Status Change to Close</asp:LinkButton>
                <asp:LinkButton ID="lbtnStatusChangeToCancel" runat="server" OnClick="lbActions_Click">Status Change to Cancel</asp:LinkButton>--%>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Cold Visit</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Ref Quotation No : </label>
                    <asp:Label ID="lblRefQuotationNo" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Ref Quotation Date : </label>
                    <asp:Label ID="lblRefQuotationDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Number : </label>
                    <asp:Label ID="lblQuotationNumber" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Date : </label>
                    <asp:Label ID="lblQuotationDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Type : </label>
                    <asp:Label ID="lblQuotationType" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Quotation Status : </label>
                    <asp:Label ID="lblQuotationStatus" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Valid From : </label>
                    <asp:Label ID="lblValidFrom" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Valid To : </label>
                    <asp:Label ID="lblValidTo" runat="server"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>Pricing Date : </label>
                    <asp:Label ID="lblPricingDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Price Group : </label>
                    <asp:Label ID="lblPriceGroup" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>User Status : </label>
                    <asp:Label ID="lblUserStatus" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Lead Number : </label>
                    <asp:Label ID="lblLeadNumber" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Visit Date : </label>
                    <asp:Label ID="lblLeadDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Customer : </label>
                    <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                </div>

            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Contact Person : </label>
                    <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Mobile : </label>
                    <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Email : </label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Address : </label>
                    <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Importance : </label>
                    <asp:Label ID="lblImportance" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<asp:TabContainer ID="tbpCust" runat="server" ToolTip="Geographical Location Master..." Font-Bold="True" Font-Size="Medium">
    <asp:TabPanel ID="tpnlFinancier" runat="server" HeaderText="Financier" Font-Bold="True" ToolTip="List of Countries...">
        <ContentTemplate>
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label>Ref Quotation No : </label>
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label>Ref Quotation Date : </label>
                            <asp:Label ID="Label3" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label>Quotation Number : </label>
                            <asp:Label ID="Label4" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label>Quotation Date : </label>
                            <asp:Label ID="Label5" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label>Quotation Type : </label>
                            <asp:Label ID="Label6" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label>Quotation Status : </label>
                            <asp:Label ID="Label7" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label>Valid From : </label>
                            <asp:Label ID="Label8" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label>Valid To : </label>
                            <asp:Label ID="Label9" runat="server"></asp:Label>
                        </div>

                        <div class="col-md-12">
                            <label>Pricing Date : </label>
                            <asp:Label ID="Label10" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label>Price Group : </label>
                            <asp:Label ID="Label11" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label>User Status : </label>
                            <asp:Label ID="Label12" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:TabPanel>

    <asp:TabPanel ID="tpnlProduct" runat="server" HeaderText="Product">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                      
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TabCompetitor" runat="server" HeaderText="Competitor">
        <ContentTemplate>
            
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="TaQuotationNote" runat="server" HeaderText="QuotationNote">
        <ContentTemplate>
            
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>

<asp:Panel ID="pnlEffort" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Quotation</span><a href="#" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageEffort" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <fieldset class="fieldset-border" id="Fieldset2" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Financier</label>
                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Incoterms</label>
                    <asp:DropDownList ID="ddlIncoterms" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Payment Terms</label>
                    <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">DO Number</label>
                    <asp:TextBox ID="txtDoNumber" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtDoNumber" WatermarkText="DO Number" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Do Date</label>
                    <asp:TextBox ID="txtDoDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Advance Amount</label>
                    <asp:TextBox ID="txtAdvanceAmount" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtAdvanceAmount" WatermarkText="Customer Name" WatermarkCssClass="WatermarkCssClass" />
                </div>

                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Financier Amount</label>
                    <asp:TextBox ID="txtFinancierAmount" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>

            </div>
        </fieldset>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnFinancier" runat="server" CssClass="btn Save" Text="Save" OnClick="btnFinancier_Click"></asp:Button>
        </div>

    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="MPE_Effort" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlEffort" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<fieldset class="fieldset-border" id="Fieldset4" runat="server">
    <div class="col-md-12">
        <asp:Panel ID="pnlAllowM" runat="server" Enabled="true">
            <asp:Label ID="Label1" runat="server" Text="" CssClass="label" Width="100%" />
            <asp:Panel ID="pnlMaterial" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" DataKeyNames="QuotationItemID">
                            <Columns>
                                <asp:TemplateField HeaderText="Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plant">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Plant.PlantCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic Price">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBasicPrice" Text='<%# DataBinder.Eval(Container.DataItem, "BasicPrice")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="TextBox"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Taxable value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax %">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxPersent" Text='<%# DataBinder.Eval(Container.DataItem, "TaxPersent")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxvalue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NetValue">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetValue" Text='<%# DataBinder.Eval(Container.DataItem, "NetValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMaterialRemove" runat="server" CommandArgument="QuotationItemID" OnClick="lblMaterialRemove_Click">Cancel</asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%--<asp:UpdatePanel ID="uptxtMaterialF" runat="server">
                                                    <ContentTemplate>--%>
                                        <asp:LinkButton ID="lblMaterialAdd" runat="server" OnClick="lblMaterialAdd_Click" AutoPostBack="false">Add</asp:LinkButton>
                                        <%--</ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lblMaterialAdd" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</fieldset>

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>


