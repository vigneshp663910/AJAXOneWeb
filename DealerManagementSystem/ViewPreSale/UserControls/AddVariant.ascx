<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddVariant.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddVariant" %>
<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Variant Type</label>
                    <asp:DropDownList ID="ddlVariantType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlVariantType_SelectedIndexChanged" />
                </div>
                <%--  <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Material</label>
                    <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Qty</label>
                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Discount Amount</label>
                    <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn Save" Text="Add" OnClick="btnAdd_Click"></asp:Button>
                </div>--%>
            </div>
        </fieldset>

        <div class="col-md-12 Report">
            <div class="table-responsive">
                <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <div style="margin:-6px;margin-left: -20px;margin-right: -20px">
                                    <asp:Image ID="lblMaterialImage" runat="server" Width="75px" Height="75px" ImageUrl="~/Images/Delete1.png"  />
                                </div>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material">
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialID")%>' runat="server" Visible="false" />
                                <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%--  <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- <asp:TemplateField HeaderText="Discount Amount">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField> --%>
                        <%-- <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                                <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "Remark")%>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                    </Columns>
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
