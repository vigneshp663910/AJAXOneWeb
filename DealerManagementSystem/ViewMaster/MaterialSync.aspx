<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="MaterialSync.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.MaterialSync" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="txtMaterialCode" runat="server"></asp:TextBox>
    <asp:Button ID="BtnMaterialSync" runat="server" Text="Material" OnClick="BtnMaterialSync_Click" />

    <div style="background-color: white">

        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid"    EmptyDataText="No Data Found"
             Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                    <ItemTemplate>
                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Material" HeaderStyle-Width="120px">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Material Desc">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblMaterialDescription" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Reso, BaseUnit%>">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblBaseUnit" Text='<%# DataBinder.Eval(Container.DataItem, "BaseUnit")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Reso, MaterialType%>">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblMaterialType" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialType")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Division Code">
                    <%--<ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialGroup" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialGroup")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>--%>
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblDivisionCode" Text='<%# DataBinder.Eval(Container.DataItem, "Model.Division.DivisionCode")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mode Code">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblModeCode" Text='<%# DataBinder.Eval(Container.DataItem, "Model.ModelCode")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mode">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblMode" Text='<%# DataBinder.Eval(Container.DataItem, "Model.Model")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Model Description">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblModelDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Model.ModelDescription")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Reso, GrossWeight%>">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblGrossWeight" Text='<%# DataBinder.Eval(Container.DataItem, "GrossWeight","{0:n}")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Reso, NetWeight%>">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblNetWeight" Text='<%# DataBinder.Eval(Container.DataItem, "NetWeight","{0:n}")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Reso, WeightUnit%>">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblWeightUnit" Text='<%# DataBinder.Eval(Container.DataItem, "WeightUnit")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="<%$ Resources:Reso, MaterialDivision%>">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialDivision" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDivision")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="HSN">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "HSN")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CST %">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblCST" Text='<%# DataBinder.Eval(Container.DataItem, "CST","{0:n}")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SST %">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSST" Text='<%# DataBinder.Eval(Container.DataItem, "SST","{0:n}")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="GST %">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblGST" Text='<%# DataBinder.Eval(Container.DataItem, "GST","{0:n}")%>' runat="server"></asp:Label>
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
</asp:Content>
