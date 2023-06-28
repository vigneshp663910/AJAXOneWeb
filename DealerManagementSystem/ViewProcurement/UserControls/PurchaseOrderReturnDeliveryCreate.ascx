<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderReturnDeliveryCreate.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderReturnDeliveryCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Label ID="lblMessagePoReturnDeliveryCreate" runat="server" Text="" CssClass="message" Visible="false" />

<asp:Panel ID="pnlPoReturnDeliveryCreate" runat="server">
    <%--<div class="col-md-12">--%>
    <div class="col-md-12 Report">
        <%--<fieldset class="fieldset-border">--%>
        <div class="col-md-12">
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Purchase Order Return Item(s):</td>
                                    <td>
                                        <asp:Label ID="lblRowCountPoReturnItem" runat="server" CssClass="label"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvPoReturnItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" AllowPaging="true" PageSize="1" EmptyDataText="No Data Found"
                    DataKeyNmes="PurchaseOrderReturnItemID" OnPageIndexChanging="gvPoReturnItem_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Select PO Return">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="cbIsChecked" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO Return Number">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblPurchaseOrderReturnNumber" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnNumber")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblPurchaseOrderReturnID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblPurchaseOrderReturnItemID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItem.PurchaseOrderReturnItemID")%>' runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItem.Item")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Code">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Description">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItem.Quantity","{0:n}")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblUOM" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItem.Material.BaseUnit")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Delivery Qty">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtDeliveredQty" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnProceedDelivery" runat="server" Text="Proceed Delivery" CssClass="btn Search" OnClick="btnProceedDelivery_Click" Width="150px" />
                </div>
            </div>
            <%--<div class="col-md-12 col-sm-12" runat="server" id="divDeliveryRemarks" visible="false">
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Remarks</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" Rows="5" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
            </div>--%>
            <%--<div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Search" OnClick="btnSave_Click" Width="150px" />
            </div>--%>
        </div>
        <%--</fieldset>--%>
        <div class="col-md-12" id="divDeliveryEntry" runat="server" visible="false">
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Purchase Order Return Item(s):</td>
                                    <td>
                                        <asp:Label ID="lblRowCountPoReturnItemDelivery" runat="server" CssClass="label"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvPoReturnItemForDelivery" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" EmptyDataText="No Data Found"
                    DataKeyNmes="PurchaseOrderReturnItemID">
                    <Columns>
                        <%--<asp:TemplateField HeaderText="PO Return Number">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblPurchaseOrderReturnNumber" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnNumber")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblPurchaseOrderReturnID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Item">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblPurchaseOrderReturnID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnID")%>' runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPurchaseOrderReturnItemID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItemID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblUOM" Text='<%# DataBinder.Eval(Container.DataItem, "Material.BaseUnit")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delivery Qty">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtDeliveredQty" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
                <%--<div class="col-md-12 col-sm-12" runat="server">--%>
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Remarks</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" Rows="5" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <%--</div>--%>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Search" OnClick="btnSave_Click" Width="150px" />
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">PO Return Delivery Item</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvPoReturnDeliveryItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="PO Return Delivery Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                            <asp:Label ID="lblPurchaseOrderReturnID" Text='<%# DataBinder.Eval(Container.DataItem, "PoReturnDeliveryID")%>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderReturnItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryQty" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryQty","{0:n}")%>' runat="server"></asp:Label>
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
        </div>

    </div>
    <%--</div>--%>
</asp:Panel>
