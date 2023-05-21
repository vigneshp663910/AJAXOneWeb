<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderAsnView.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderAsnView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <%--<asp:LinkButton ID="lbReleasePO" runat="server" OnClick="lbActions_Click">Release PO</asp:LinkButton>--%>
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">ASN Details</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Asn Number : </label>
                    <asp:Label ID="lblAsnNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Delivery Number : </label>
                    <asp:Label ID="lblDeliveryNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>LR Number : </label>
                    <asp:Label ID="lblLRNo" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Asn Date : </label>
                    <asp:Label ID="lblAsnDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Delivery Date : </label>
                    <asp:Label ID="lblDeliveryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gr Number : </label>
                    <asp:Label ID="lblGrNumber" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Asn Status : </label>
                    <asp:Label ID="lblAsnStatus" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gr Date : </label>
                    <asp:Label ID="lblGrDate" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>
<asp:Label ID="Label1" runat="server" Text="" CssClass="message" />
<asp1:TabContainer ID="tbpAsn" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
    <asp1:TabPanel ID="tpnlAsnItem" runat="server" HeaderText="POAsn Item" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">Asn Item</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvPOAsnItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
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
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NetWeight">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetWeight" Text='<%# DataBinder.Eval(Container.DataItem, "NetWeight")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UomWeight">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUomWeight" Text='<%# DataBinder.Eval(Container.DataItem, "UomWeight")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PackCount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPackCount" Text='<%# DataBinder.Eval(Container.DataItem, "PackCount")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="StockType">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStockType" Text='<%# DataBinder.Eval(Container.DataItem, "StockType")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IsChangedpart">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsChangedpart" Text='<%# DataBinder.Eval(Container.DataItem, "IsChangedpart")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DeliveredQty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredQty" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveredQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ReceivedQty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceivedQty" Text='<%# DataBinder.Eval(Container.DataItem, "ReceivedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="DamagedQty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDamagedQty" Text='<%# DataBinder.Eval(Container.DataItem, "DamagedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ReturnedQty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblReturnedQty" Text='<%# DataBinder.Eval(Container.DataItem, "ReturnedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GrRemarks">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "GrRemarks")%>' runat="server"></asp:Label>
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
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>