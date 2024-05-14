<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StockTransferOrderView.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.StockTransferOrderView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<style>
    .Popup {
        width: 95%;
        height: 95%;
        top: 128px;
        left: 283px;
    }

        .Popup .model-scroll {
            height: 80vh;
            overflow: auto;
        }
</style>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbAddMaterial" runat="server" OnClick="lbActions_Click">Add Material</asp:LinkButton>
                <asp:LinkButton ID="lbRelease" runat="server" OnClick="lbActions_Click">Release</asp:LinkButton>
                <asp:LinkButton ID="lbCancel" runat="server" OnClick="lbActions_Click">Cancel</asp:LinkButton>
                <asp:LinkButton ID="lbDelivery" runat="server" OnClick="lbActions_Click">Delivery</asp:LinkButton>
                <asp:LinkButton ID="lbPDF" runat="server" OnClick="lbActions_Click">PO Preview</asp:LinkButton>
                <asp:LinkButton ID="lbPreviewSTO" runat="server" OnClick="lbActions_Click">Preview STO</asp:LinkButton>
                <asp:LinkButton ID="lbDowloadSTO" runat="server" OnClick="lbActions_Click">Dowload STO</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">PO</legend>
        <div class="col-md-12 View">
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>STO Number : </label>
                    <asp:Label ID="lblPurchaseOrderNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>STO Date : </label>
                    <asp:Label ID="lblPurchaseOrderDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblPORemarks" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblPODealer" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>Receiving Location : </label>
                    <asp:Label ID="lblReceivingLocation" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Source Location : </label>
                    <asp:Label ID="lblSourceLocation" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
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

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<asp1:TabContainer ID="tbpEnquiry" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
    <asp1:TabPanel ID="tpnlCustomer" runat="server" HeaderText="Item" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvPOItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStockTransferOrderItemID" Text='<%# DataBinder.Eval(Container.DataItem, "StockTransferOrderItemID")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "ItemNo")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                              <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialID")%>' runat="server"></asp:Label>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
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
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"  Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' Visible="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Taxable Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "CGST","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "SGST","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "IGST","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGST Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "CGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SGST Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "SGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IGST Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "IGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transit Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransitQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "TransitQuantity","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivered Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveredQuantity","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnItemAction_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkBtnupdate" runat="server" OnClick="lnkBtnItemAction_Click" Visible="false" OnClientClick="return ConfirmItemUpdate();"><i class='fa fa-fw fa-refresh' style='font-size:18px'></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkBtnCancel" runat="server" OnClick="lnkBtnItemAction_Click" Visible="false"> <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnItemAction_Click" OnClientClick="return ConfirmItemDelete();"> <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                            <asp:Button ID="btnCancelPoItem" runat="server" Text="Cancel" CssClass="btn Back" OnClick="btnCancelPoItem_Click" Width="75px" Height="25px" Visible="false" />
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
    <asp1:TabPanel ID="tpnlLead" runat="server" HeaderText="Delivery" Font-Bold="True">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvDeliveryView" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid"
                                OnRowDataBound="gvDeliveryView_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="javascript:collapseExpand('DeliveryID-<%# Eval("DeliveryID") %>');">
                                                <img id="imageAsnID-<%# Eval("DeliveryID") %>" alt="Click to show/hide orders" border="0" src="../Images/grid_expand.png" height="10" width="10" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryID" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Date">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gr Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrNumber" Text='<%# DataBinder.Eval(Container.DataItem, "GrNumber")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gr Date">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrDate" Text='<%# DataBinder.Eval(Container.DataItem, "GrDate","{0:d}")%>' runat="server"></asp:Label>
                                            <tr>
                                                <td colspan="100%" style="padding-left: 96px">
                                                    <div id="DeliveryID-<%# Eval("DeliveryID") %>" style="display: none; position: relative;">
                                                        <asp:GridView ID="gvDeliveryViewItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDeliveryItemID" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryItemID")%>' runat="server" Visible="false"></asp:Label>
                                                                        <%--<asp:Label ID="lblAsnItem" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem")%>' runat="server"></asp:Label>--%>
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
                                                                <asp:TemplateField HeaderText="Delivery Quantity">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDeliveryQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryQuantity")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Gr Quantity">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGrQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "GrQuantity")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unrestricted Quantity">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnrestrictedQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "UnrestrictedQuantity")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Restricted Quantity">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRestrictedQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "RestrictedQuantity")%>' runat="server"></asp:Label>
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
                                                </td>
                                            </tr>
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
</asp1:TabContainer>

<asp:Panel ID="pnlAddMaterial" runat="server" CssClass="Popup" Style="display: none" Height="500px">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Material</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageAddMaterial" runat="server" Text="" CssClass="message" />
        <div class="col-md-12">
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">SupersedeYN</label>
                <asp:CheckBox ID="cbSupersedeYN" runat="server" Checked="true" />
            </div>
            <div class="col-md-5 col-sm-12">
                <asp:HiddenField ID="hdfMaterialID" runat="server" />
                <asp:HiddenField ID="hdfMaterialCode" runat="server" />
                <label class="modal-label">Material</label>
                <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" onKeyUp="GetMaterial()"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">Qty</label>
                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSubmitMaterial" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSubmitMaterial_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_AddMaterial" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlAddMaterial" BackgroundCssClass="modalBackground" />

<asp:Panel ID="pnlItems" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Details</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageDelivery" runat="server" Text="" CssClass="message" />
        <div class="model-scroll">
            <asp:GridView ID="gvDelivery" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
                EmptyDataText="No Data Found" PageSize="10" OnSelectedIndexChanged="gvDelivery_SelectedIndexChanged">
                <Columns>
                   <%-- <asp:TemplateField HeaderText="Item">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                              <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "ItemNo")%>' runat="server"></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Material">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblStockTransferOrderItemID" Text='<%# DataBinder.Eval(Container.DataItem, "StockTransferOrderItemID")%>' runat="server" Visible="false"></asp:Label>
                         
                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Desc">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="HSN">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "Material.HSN")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Order Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>'   runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Balance Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblBalanceQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "BalanceQuantity","{0:n}")%>'   runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delivery Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtDeliveryQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryQuantity","{0:n}")%>' CssClass="form-control" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnRemove" runat="server" OnClick="lnkBtngvDeliveryAction_Click" OnClientClick="return ConfirmItemDelete();"> <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveDelivery" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveDelivery_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Delivery" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlItems" BackgroundCssClass="modalBackground" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>

<script type="text/javascript">
    function ConfirmItemUpdate() {
        var x = confirm("Are you sure you want to Update?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmItemDelete() {
        var x = confirm("Are you sure you want to Delete?");
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>
<script type="text/javascript">

    function GetMaterial() {
        $("#MainContent_UC_StockTransferOrderView_hdfMaterialID").val('');
        $("#MainContent_UC_StockTransferOrderView_hdfMaterialCode").val('');
        var param = { Material: $('#MainContent_UC_StockTransferOrderView_txtMaterial').val(), MaterialType: '' }
        var Customers = [];
        if ($('#MainContent_UC_StockTransferOrderView_txtMaterial').val().trim().length >= 3) {
            $.ajax({
                url: "StockTransferOrder.aspx/GetMaterial",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                data: JSON.stringify(param),
                dataType: 'JSON',
                success: function (data) {
                    var DataList = JSON.parse(data.d);
                    for (i = 0; i < DataList.length; i++) {
                        Customers[i] = {
                            value: DataList[i].MaterialCode + ' ' + DataList[i].MaterialDescription,
                            value1: DataList[i].MaterialID,
                            MaterialCode: DataList[i].MaterialCode
                        };
                    }
                    $('#MainContent_UC_StockTransferOrderView_txtMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) {
                            $("#MainContent_UC_StockTransferOrderView_hdfMaterialID").val(u.item.value1);
                            $("#MainContent_UC_StockTransferOrderView_hdfMaterialCode").val(u.item.MaterialCode);
                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_StockTransferOrderView_txtMaterial').width() + 48,
                            });
                            $(this).autocomplete("widget").scrollTop(0);
                        }
                    }).focus(function (e) {
                        $(this).autocomplete("search");
                    }).click(function () {
                        $(this).autocomplete("search");
                    }).data('ui-autocomplete')._renderItem = function (ul, item) {

                        var inner_html = FormatAutocompleteList(item);
                        return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;  z-index: 10002"></li>')
                            .data('item.autocomplete', item)
                            .append(inner_html)
                            .appendTo(ul);
                    };

                }
            });
        }
        else {
            $('#MainContent_UC_StockTransferOrderView_txtMaterial').autocomplete({
                source: function (request, response) {
                    response($.ui.autocomplete.filter(Customers, ""))
                }
            });
        }
    }

    function FormatAutocompleteList(item) {
        var inner_html = '<a>';
        inner_html += '<p style="margin:0;"><strong>' + item.value + '</strong></p>';
        inner_html += '</a>';
        return inner_html;
    }
</script>
<script type="text/javascript">
    function collapseExpand(obj) {
        var gvObject = document.getElementById(obj);
        var imageID = document.getElementById('image' + obj);

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "../Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "../Images/grid_expand.png";
        }
    }

    function OpenInNewTab(url) {
        var win = window.open(url, '_blank');
        win.focus();
    }
</script>
