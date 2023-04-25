<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderCreate.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewProcurement/UserControls/PurchaseOrderItem.ascx" TagPrefix="UC" TagName="UC_PurchaseOrderItem" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px"> 
                <asp:LinkButton ID="lbAddMaterial" runat="server" OnClick="lbActions_Click">Add Material</asp:LinkButton>
                <asp:LinkButton ID="lbAddProduct" runat="server" OnClick="lbActions_Click">Upload Material</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <div class="col-md-5 col-sm-12">
            <label class="modal-label">Dealer</label>
            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
        </div>
        <div class="col-md-5 col-sm-12">
            <label class="modal-label">Order Type<samp style="color: red">*</samp></label>
            <asp:DropDownList ID="ddlPurchaseOrderType" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-5 col-sm-12">
            <label class="modal-label">Order To<samp style="color: red">*</samp></label>
            <asp:DropDownList ID="ddlOrderTo" runat="server" CssClass="form-control">
                <asp:ListItem Value="1">OE</asp:ListItem>
                <asp:ListItem Value="2">Co-Dealer</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-5 col-sm-12">
            <label class="modal-label">Vendor<samp style="color: red">*</samp></label>
            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-5 col-sm-12">
            <label class="modal-label">Division<samp style="color: red">*</samp></label>
            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-5 col-sm-12">
            <label class="modal-label">
                Receiving Location
            <samp style="color: red">*</samp></label>
            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-5 col-sm-12">
            <label class="modal-label">Expected Delivery Date<samp style="color: red">*</samp></label>
            <asp:TextBox ID="txtExpectedDeliveryDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
            <asp1:CalendarExtender ID="cxExpectedDeliveryDate" runat="server" TargetControlID="txtExpectedDeliveryDate" PopupButtonID="txtExpectedDeliveryDate" Format="dd/MM/yyyy HH:mm:ss" />
            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtExpectedDeliveryDate" WatermarkText="DD/MM/YYYY HH:mm:ss" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Ref. No<samp style="color: red">*</samp></label>
            <asp:TextBox ID="txtReferenceNo" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Remarks</label>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" Rows="5" AutoCompleteType="Disabled"></asp:TextBox>
        </div>
    </fieldset>
    <div class="col-md-12">
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">PO Item</legend>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvPOItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="2500px">
                        <Columns>
                            <asp:TemplateField HeaderText="Item">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
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
                                    <asp:Label ID="lblf_uom" Text='<%# DataBinder.Eval(Container.DataItem, "UOM")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "UnitPrice","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "UnitPrice","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscountAmount" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Taxable Amount">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableAmount","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SGST">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "SGST","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CGST">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "CGST","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IGST">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "IGST","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Gross Amt" HeaderStyle-Width="85px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblr_gross_amt" Text='<%# DataBinder.Eval(Container.DataItem, "GrossAmount","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
</div>

<asp:Panel ID="pnlAddMaterialCharges" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">AddMaterial</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button7" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageMaterial" runat="server" Text="" CssClass="message" Visible="false" />
            <UC:UC_PurchaseOrderItem ID="UC_PurchaseOrderItem" runat="server"></UC:UC_PurchaseOrderItem>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAddMaterial" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAddMaterial_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddMaterial" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddMaterial" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

 
<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>