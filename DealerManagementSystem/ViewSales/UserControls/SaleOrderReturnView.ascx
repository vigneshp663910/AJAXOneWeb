<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaleOrderReturnView.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SaleOrderReturnView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Panel ID="PnlSaleOrderReturnView" runat="server" class="col-md-12">
    <div class="col-md-12">
        <div class="action-btn">
            <div class="" id="boxHere"></div>
            <div class="dropdown btnactions" id="customerAction">
                <div class="btn Approval">Actions</div>
                <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                    <asp:LinkButton ID="lbSoReturnCancel" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmCancelSalesReturn();">Cancel</asp:LinkButton>
                    <asp:LinkButton ID="lbSoReturnDeliveryCreate" runat="server" OnClick="lbActions_Click">Create Delivery</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12 field-margin-top" runat="server" id="divPoReturnView">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">Sales Return</legend>
            <div class="col-md-12 View">
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Sales Return Number : </label>
                        <asp:Label ID="lblSaleOrderReturnNumber" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Dealer Office : </label>
                        <asp:Label ID="lblDealerOffice" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Contact Person : </label>
                        <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Sales Return Date : </label>
                        <asp:Label ID="lblSaleOrderReturnDate" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Customer : </label>
                        <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Contact Person Number : </label>
                        <asp:Label ID="lblContactPersonNumber" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12">
                        <label>Dealer : </label>
                        <asp:Label ID="lblSODealer" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Sales Return Status : </label>
                        <asp:Label ID="lblSaleOrderReturnStatus" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <label>Division : </label>
                        <asp:Label ID="lblDivision" runat="server" CssClass="label"></asp:Label>
                    </div>
                </div>
            </div>
        </fieldset>
        <asp:Label ID="lblMessageSoReturn" runat="server" Text="" CssClass="message" Visible="false" />
        <asp1:TabContainer ID="tbpSoReturn" runat="server" ToolTip="SO Return Info..." Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
            <asp1:TabPanel ID="tpnlPoReturnItem" runat="server" HeaderText="Sales Return Item" Font-Bold="True" ToolTip="">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-12 Report">
                            <div class="table-responsive">
                                <asp:GridView ID="gvSoReturnItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                                    <Columns>
                                       <asp:TemplateField HeaderText="Material Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                <asp:Label ID="lblSaleOrderReturnItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderReturnItemID")%>' runat="server" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Description">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HSN Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.HSN")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivered Qty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Qty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Return Qty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.CGST")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.CGSTValue")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.SGST")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.SGSTValue")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.IGST")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.IGSTValue")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Material.BaseUnit")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.Value","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Discount">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.ItemDiscountValue","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Freight Amount">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFreightAmount" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.FreightAmount","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Taxable Value">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaxableAmount" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderItem.TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnDeleteSalesReturnItem" runat="server" OnClick="lnkBtnDeleteSalesReturnItem_Click" OnClientClick="return ConfirmDeleteSalesReturnItem();"> <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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
    function ConfirmCancelSalesReturn() {
        var x = confirm("Are you sure you want to cancel this Sales Return?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmDeleteSalesReturnItem() {
        var x = confirm("Are you sure you want to Delete?");
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>
