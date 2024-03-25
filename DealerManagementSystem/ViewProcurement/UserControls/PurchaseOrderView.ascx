<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderView.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddEnquiry.ascx" TagPrefix="UC" TagName="UC_AddEnquiry" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerViewSoldTo" %>
<%@ Register Src="~/ViewPreSale/UserControls/LeadViewHeader.ascx" TagPrefix="UC" TagName="UC_LeadView" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerCreate.ascx" TagPrefix="UC" TagName="UC_CustomerCreate" %>
<%@ Register Src="~/ViewPreSale/UserControls/AddLead.ascx" TagPrefix="UC" TagName="UC_AddLead" %>

<script type="text/javascript">
    function ConfirmReleasePO() {
        var x = confirm("Are you sure you want to Release Purchase Order?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmCancelPO() {
        var x = confirm("Are you sure you want to Cancel Purchase Order?");
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>
<asp:HiddenField ID="hdfItemCount" runat="server" Value="0" />
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbAddMaterial" runat="server" OnClick="lbActions_Click">Add Material</asp:LinkButton>
                <asp:LinkButton ID="lbReleasePO" runat="server" OnClientClick="return ConfirmReleasePO();" OnClick="lbActions_Click">Release PO</asp:LinkButton>
                <%--<asp:LinkButton ID="lbEditPO" runat="server" OnClick="lbActions_Click">Edit PO</asp:LinkButton>--%>
                <asp:LinkButton ID="lbCancelPO" runat="server" OnClientClick="return ConfirmCancelPO();" OnClick="lbActions_Click">Cancel PO</asp:LinkButton>
                <asp:LinkButton ID="lbReleaseApprove" runat="server" OnClientClick="return ConfirmReleasePO();" OnClick="lbActions_Click">Release Approve</asp:LinkButton>
                <asp:LinkButton ID="lbCancelApprove" runat="server" OnClientClick="return ConfirmCancelPO();" OnClick="lbActions_Click">Cancel Approve</asp:LinkButton>
                <asp:LinkButton ID="lbViewPurchaseOrder" runat="server" OnClick="lbActions_Click">PO Preview</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">PO</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>PO Number : </label>
                    <asp:Label ID="lblPurchaseOrderNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>PO Date : </label>
                    <asp:Label ID="lblPurchaseOrderDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>SO Number : </label>
                    <asp:Label ID="lblSoNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Order Type : </label>
                    <asp:Label ID="lblOrderType" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Order To : </label>
                    <asp:Label ID="lblOrderTo" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <%--<div class="col-md-12">
                    <label>SO Date : </label>
                    <asp:Label ID="lblSoDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>--%>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblPODealer" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Receiving Location : </label>
                    <asp:Label ID="lblReceivingLocation" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Vendor : </label>
                    <asp:Label ID="lblPOVendor" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Expected Delivery Date : </label>
                    <asp:Label ID="lblExpectedDeliveryDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

            </div>
            <div class="col-md-4">
               <%-- <div class="col-md-12">
                    <label>Price : </label>
                    <asp:Label ID="lblPrice" runat="server" CssClass="LabelValue"></asp:Label>
                </div>--%>
                <div class="col-md-12">
                    <label>Discount : </label>
                    <asp:Label ID="lblDiscount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>Taxable Amount : </label>
                    <asp:Label ID="lblTaxableAmount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Tax Amount : </label>
                    <asp:Label ID="lblTaxAmount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gross Amount : </label>
                    <asp:Label ID="lblGrossAmount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

            </div>
        </div>
    </fieldset>
</div>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<asp1:TabContainer ID="tbp" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
    <asp1:TabPanel ID="TabPanel1" runat="server" HeaderText="PO Details" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">PO</legend>
                    <div class="col-md-12 View">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Division : </label>
                                <asp:Label ID="lblDivision" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Ref No : </label>
                                <asp:Label ID="lblRefNo" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Remarks : </label>
                                <asp:Label ID="lblPORemarks" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">

                            <div class="col-md-12">
                                <label>Created By : </label>
                                <asp:Label ID="lblCreatedBy" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Cancelled By : </label>
                                <asp:Label ID="lblCancelledBy" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Cancelled On : </label>
                                <asp:Label ID="lblCancelledOn" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>

                        </div>

                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
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
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
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
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblf_uom" Text='<%# DataBinder.Eval(Container.DataItem, "Material.BaseUnit")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Price">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Material.CurrentPrice","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                                    <asp:TemplateField HeaderText="Net Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetValue" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmount","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
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
    <asp1:TabPanel ID="tpnlLead" runat="server" HeaderText="ASN" Font-Bold="True">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">ASN</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvPAsn" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid"
                                OnRowDataBound="gvPAsn_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="javascript:collapseExpand('AsnID-<%# Eval("AsnID") %>');">
                                                <img id="imageAsnID-<%# Eval("AsnID") %>" alt="Click to show/hide orders" border="0" src="../Images/grid_expand.png" height="10" width="10" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asn Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAsnID" Text='<%# DataBinder.Eval(Container.DataItem, "AsnID")%>' runat="server" Visible="false" />
                                            <asp:Label ID="lblAsnNumber" Text='<%# DataBinder.Eval(Container.DataItem, "AsnNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblAsnDate" Text='<%# DataBinder.Eval(Container.DataItem, "AsnDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gr Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Gr.GrNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblGrDate" Text='<%# DataBinder.Eval(Container.DataItem, "Gr.GrDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server" />
                                            <br />
                                            <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LR Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLRNo" Text='<%# DataBinder.Eval(Container.DataItem, "LRNo")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asn Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAsnStatus" Text='<%# DataBinder.Eval(Container.DataItem, "AsnStatus.ProcurementStatus")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>

                                            <tr>
                                                <td colspan="100%" style="padding-left: 96px">
                                                    <div id="AsnID-<%# Eval("AsnID") %>" style="display: none; position: relative;">
                                                        <asp:GridView ID="gvAsnItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Item">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAsnItemID" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItemID")%>' runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblAsnItem" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem")%>' runat="server"></asp:Label>
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
                                                                <asp:TemplateField HeaderText="Asn Qty">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delivered Qty">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDeliveredQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.DeliveredQty")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            <%--    <asp:TemplateField HeaderText="Received Qty">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblReceivedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.ReceivedQty")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Returned Qty">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblReturnedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.ReturnedQty")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Net Weight">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNetWeight" Text='<%# DataBinder.Eval(Container.DataItem, "NetWeight")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Uom Weight">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUomWeight" Text='<%# DataBinder.Eval(Container.DataItem, "UomWeight")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pack Count">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPackCount" Text='<%# DataBinder.Eval(Container.DataItem, "PackCount")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <%-- <asp:TemplateField HeaderText="Stock Type">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStockType" Text='<%# DataBinder.Eval(Container.DataItem, "StockType")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <%-- <asp:TemplateField HeaderText="IsChangedpart">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIsChangedpart" Text='<%# DataBinder.Eval(Container.DataItem, "IsChangedpart")%>' runat="server"></asp:Label>
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

<asp:Panel ID="pnlAddMaterial" runat="server" CssClass="Popup" Style="display: none" Height="500px">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Material</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblAddMaterialMessage" runat="server" Text="" CssClass="message" />
        <div class="col-md-12"> 
            <div class="col-md-5 col-sm-12">
                <asp:HiddenField ID="hdfMaterialID" runat="server" />
                <asp:HiddenField ID="hdfDivisionID" runat="server" />
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
        debugger;
        $("#MainContent_UC_PurchaseOrderView_hdfMaterialID").val('');
        $("#MainContent_UC_PurchaseOrderView_hdfMaterialCode").val('');
        var param = {
            Material: $('#MainContent_UC_PurchaseOrderView_txtMaterial').val(),
            MaterialType: '',
            DivisionID: $('#MainContent_UC_PurchaseOrderView_hdfDivisionID').val(),
            ItemCount: $('#MainContent_UC_PurchaseOrderView_hdfItemCount').val()
        }
        var Customers = [];
        if ($('#MainContent_UC_PurchaseOrderView_txtMaterial').val().trim().length >= 3) {
            $.ajax({
                url: "PurchaseOrder.aspx/GetMaterial",
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
                    $('#MainContent_UC_PurchaseOrderView_txtMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) {
                            $("#MainContent_UC_PurchaseOrderView_hdfMaterialID").val(u.item.value1);
                            $("#MainContent_UC_PurchaseOrderView_hdfMaterialCode").val(u.item.MaterialCode);
                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_PurchaseOrderView_txtMaterial').width() + 48,
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
            $('#MainContent_UC_PurchaseOrderView_txtMaterial').autocomplete({
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
