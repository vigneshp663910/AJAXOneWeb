<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddQuotation.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddQuotation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script>
    $(document).ready(function () {
        $("#DivTechnician").click(function () {
            $("#pnlTechnicianInformation").toggle(function () {
                $(this).animate({ height: '150px', });
            });
        });
    });
</script>


<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Quotation Type</label>
            <asp:DropDownList ID="ddlQuotationType" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Status</label>
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Rejection Reason</label>
            <asp:DropDownList ID="ddlRejectionReason" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Customer </label>
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>

            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtCustomerName" WatermarkText="Customer Name" WatermarkCssClass="WatermarkCssClass" />
        </div>

        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Valid From</label>
            <asp:TextBox ID="txtValidFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Valid To</label>
            <asp:TextBox ID="txtValidTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Pricing Date</label>
            <asp:TextBox ID="txtPricingDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Price Group</label>
            <asp:DropDownList ID="ddlPriceGroup" runat="server" CssClass="form-control" />
        </div>
    </div>
</fieldset>



<fieldset class="fieldset-border" id="Fieldset2" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Financier</label>
            <asp:DropDownList ID="ddlFinancier" runat="server" CssClass="form-control" />
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
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtCustomerName" WatermarkText="DO Number" WatermarkCssClass="WatermarkCssClass" />
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
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">User Status</label>
            <asp:DropDownList ID="ddlUserStatus" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Visit Date</label>
            <asp:TextBox ID="txtVisitDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Competitor</label>
            <asp:DropDownList ID="ddlCompetitor" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Competitor Products</label>
            <asp:DropDownList ID="ddlCompetitorProducts" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Competitor Product Type</label>
            <asp:DropDownList ID="ddlCompetitorProductType" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">User Status Remarks</label>
            <asp:DropDownList ID="ddlUserStatusRemarks" runat="server" CssClass="form-control" />
        </div>
    </div>
</fieldset>

commisssion agent
Employee resonsible
contact person

<fieldset class="fieldset-border" id="Fieldset3" runat="server">
    <div class="col-md-12">

        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Sold To Party</label>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtCustomerName" WatermarkText="DO Number" WatermarkCssClass="WatermarkCssClass" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Bill To Party</label>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtCustomerName" WatermarkText="DO Number" WatermarkCssClass="WatermarkCssClass" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Ship To Party</label>
            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtCustomerName" WatermarkText="DO Number" WatermarkCssClass="WatermarkCssClass" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">payer</label>
            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" MaxLength="35" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="txtCustomerName" WatermarkText="DO Number" WatermarkCssClass="WatermarkCssClass" />
        </div>

        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Dealer Office</label>
            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
        </div>
       
    </div>
</fieldset>


<div class="container IC_ticketManageInfo">
    <div class="col2">
        <div class="rf-p " id="txnHistory:j_idt1289">
            <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

                <div id="divSO" runat="server">

                    <div style="float: right; padding-top: 0px">
                        <a href="javascript:collapseExpandCallInformation();">
                            <img id="imgCallInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                    </div>
                </div>
                <asp:Panel ID="pnlAllowM" runat="server" Enabled="false">
                    <table id="txnHistory4:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Material</div>
                                    <div style="float: right; padding-top: 0px">
                                        <a href="javascript:collapseExpandMaterialCharges();">
                                            <img id="imgMaterialCharges" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlMaterial" runat="server">
                                    <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                        <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                            <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" DataKeyNames="WebQuotationItemID">
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
                                                            <asp:Label ID="Label61" Text='<%# DataBinder.Eval(Container.DataItem, "BasicPrice")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount 1">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label63" Text='<%# DataBinder.Eval(Container.DataItem, "Discount1")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtDiscount1" runat="server" CssClass="TextBox"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount 2">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label63" Text='<%# DataBinder.Eval(Container.DataItem, "Discount2")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtDiscount2" runat="server" CssClass="TextBox"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount 3">
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label63" Text='<%# DataBinder.Eval(Container.DataItem, "Discount3")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txtDiscount3" runat="server" CssClass="TextBox"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblMaterialRemove" runat="server" OnClick="lblMaterialRemove_Click">Cancel</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lblMaterialAdd" runat="server" OnClick="lblMaterialAdd_Click">Add</asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div> 
        </div>
    </div>
