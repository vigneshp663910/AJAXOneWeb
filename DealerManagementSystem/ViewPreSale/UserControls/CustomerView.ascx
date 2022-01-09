<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerView.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.CustomerView" %>
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Customer</legend>
        <div class="col-md-12">
            <div class="col-md-2 text-right">
                <label>Customer</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Contact Person</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblContactPerson" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Mobile</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblMobile" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Alternative Mobile</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblAlternativeMobile" runat="server" CssClass="label"></asp:Label>
            </div>

            <div class="col-md-2 text-right">
                <label>Email</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
                <label>Location</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblLocation" runat="server" CssClass="label"></asp:Label>
            </div>

            <div class="col-md-2 text-right">
                <label>GSTIN</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblGSTIN" runat="server" CssClass="label"></asp:Label>
            </div>

            <div class="col-md-2 text-right">
                <label>PAN</label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblPAN" runat="server" CssClass="label"></asp:Label>
            </div>
        </div>
    </fieldset>
</div>
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Market Segment</legend>

        <asp:GridView ID="gvMarketSegment" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
            <Columns> 
                <asp:TemplateField HeaderText="Product Name">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField> 
            </Columns>
            <AlternatingRowStyle BackColor="#f2f2f2" />
            <FooterStyle ForeColor="White" />
            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
        </asp:GridView>
    </fieldset>
</div>
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Customer Products</legend>
        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
            <Columns>
                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product Name">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product Type">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Brand Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <AlternatingRowStyle BackColor="#f2f2f2" />
            <FooterStyle ForeColor="White" />
            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
        </asp:GridView>
    </fieldset>
</div>
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Customer Relations</legend>
        <asp:GridView ID="gvRelation" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed" EmptyDataText="No Data Found">
            <Columns>
                <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <itemstyle width="25px" horizontalalign="Center"></itemstyle>
                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Person">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Number">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Relation">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Birth Date">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Anniversary Date">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <AlternatingRowStyle BackColor="#f2f2f2" />
            <FooterStyle ForeColor="White" />
            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="Gainsboro" ForeColor="Black" HorizontalAlign="Left" />
        </asp:GridView>
    </fieldset>
</div>

<asp:ImageButton ID="ibtnMarketSegmentDelete" runat="server" OnClick="ibtnMarketSegmentDelete_Click"/>
<asp:ImageButton ID="ibtnProductDelete" runat="server" OnClick="ibtnProductDelete_Click" />
<asp:ImageButton ID="ibtnRelationDelete" runat="server" OnClick="ibtnRelationDelete_Click" />
