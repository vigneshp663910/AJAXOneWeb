<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderReturnView.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderReturnView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<%@ Register Src="~/ViewProcurement/UserControls/PurchaseOrderReturnDeliveryCreate.ascx" TagPrefix="UC" TagName="UC_PurchaseOrderReturnDeliveryCreate" %>

<asp:Panel ID="PnlPurchaseOrderReturnView" runat="server" class="col-md-12">
    <div class="col-md-12">
        <div class="action-btn">
            <div class="" id="boxHere"></div>
            <div class="dropdown btnactions" id="customerAction">
                <div class="btn Approval">Actions</div>
                <div class="dropdown-content" style="font-size: small; margin-left: -105px">

                    <asp:LinkButton ID="lbPreviewPoReturn" runat="server" OnClick="lbActions_Click">PO Return Preview</asp:LinkButton>
                    <asp:LinkButton ID="lbDownloadPoReturn" runat="server" OnClick="lbActions_Click">PO Return Download</asp:LinkButton>
                    <asp:LinkButton ID="lbRequestForApproval" runat="server" OnClick="lbActions_Click">Request For Approval</asp:LinkButton>
                    <asp:LinkButton ID="lbApprove" runat="server" OnClick="lbActions_Click">Approve</asp:LinkButton>
                    <asp:LinkButton ID="lbReject" runat="server" OnClick="lbActions_Click">Reject</asp:LinkButton>
                    <asp:LinkButton ID="lbCancel" runat="server" OnClick="lbActions_Click">Cancel</asp:LinkButton>
                    <asp:LinkButton ID="lbDeliveryCreate" runat="server" OnClick="lbActions_Click">Create Delivery</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
  
    <div class="col-md-12 field-margin-top" runat="server" id="divPoReturnView">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Purchase Return</legend>
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>PO Return Number : </label>
                        <asp:Label ID="lblPurchaseOrderReturnNumber" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>PO Return Date : </label>
                        <asp:Label ID="lblPurchaseOrderReturnDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Remarks : </label>
                        <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>PO Return Status : </label>
                        <asp:Label ID="lblPurchaseOrderReturnStatus" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
            </div>
        </fieldset>
          <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
        <asp1:TabContainer ID="tbpPoReturn" runat="server" ToolTip="Purchase Return Info..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
            <asp1:TabPanel ID="tpnlPoReturnItem" runat="server" HeaderText="Purchase Return Item" Font-Bold="True" ToolTip="">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <div class="table-responsive">
                                <asp:GridView ID="gvPOReturnItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PO Number">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPoNumber" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrder.PurchaseOrderNumber")%>' runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblPurchaseOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrder.PurchaseOrderDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ASN Number">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAsnNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.AsnNumber")%>' runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblAsnDate" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.AsnDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDelivery" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.DeliveryNumber")%>' runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.DeliveryDate")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.InvoiceNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GR Number">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Gr.GrNumber")%>' runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblGrDate" Text='<%# DataBinder.Eval(Container.DataItem, "Gr.GrDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Item">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPurchaseOrderReturnItemID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItemID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Material Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Description">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--    <asp:TemplateField HeaderText="UOM">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM" Text='<%# DataBinder.Eval(Container.DataItem, "Material.BaseUnit")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "Value","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Taxable Amount">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "Material.CGST")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "Material.CGSTValue")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "Material.SGST")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "Material.SGSTValue")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "Material.IGST")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "Material.IGSTValue")%>' runat="server"></asp:Label>
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
            <asp1:TabPanel ID="tpnlPoReturnDelivery" runat="server" HeaderText="Delivery" Font-Bold="True" ToolTip="">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <div class="table-responsive">
                                <asp:GridView ID="gvPoReturnDelivery" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Delivery Number Date">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnDeliveryItem.PurchaseOrderReturnItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Description">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnDeliveryItem.PurchaseOrderReturnItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery Qty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnDeliveryItem.DeliveryQty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnDeliveryItem.PurchaseOrderReturnItem.Material.BaseUnit")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnDeliveryItem.Remarks")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="White" />
                                    <FooterStyle ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp1:TabPanel>
        </asp1:TabContainer>
    </div>
</asp:Panel>

<asp:Panel ID="pnlPoReturnDeliveryCreate" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Create Delivery</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="btnPoReturnDeliveryCreatePopupClose" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageDeliveryCreate" runat="server" Text="" CssClass="message" />
            <fieldset class="fieldset-border">
                <UC:UC_PurchaseOrderReturnDeliveryCreate ID="UC_PurchaseOrderReturnDeliveryCreate" runat="server"></UC:UC_PurchaseOrderReturnDeliveryCreate>
                <div class="col-md-12 text-center" id="divProceeedDelivery" runat="server">
                    <asp:Button ID="btnProceedDelivery" runat="server" Text="Proceed Delivery" CssClass="btn Search" OnClick="btnProceedDelivery_Click" Width="150px" />
                </div>
                <div class="col-md-12 text-center" id="divSave" runat="server" visible="false">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" />
                </div>
            </fieldset>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_PoReturnDeliveryCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlPoReturnDeliveryCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlPoReturnCancel" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PoReturnCancelPopupDialogue">PO Return Cancel</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="PoReturnCancelPopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageCancel" runat="server" Text="" CssClass="message" />
        <div class="col-md-12">
            <div class="col-md-12 col-sm-12">
                <label>Remarks</label>
                <asp:TextBox ID="txtCancelRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnCancel" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateStatus_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_PoReturnCancel" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlPoReturnCancel" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlPoReturnReject" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PoReturnCancelPopupDialogue">PO Return Reject</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="Label1" runat="server" Text="" CssClass="message" />
        <div class="col-md-12">
            <div class="col-md-12 col-sm-12">
                <label>Remarks</label>
                <asp:TextBox ID="txtRejectRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnReject" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateStatus_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_PoReturnReject" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlPoReturnReject" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel1" runat="server" Text="Cancel" />
</div>
