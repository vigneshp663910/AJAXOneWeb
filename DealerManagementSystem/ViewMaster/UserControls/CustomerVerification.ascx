<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerVerification.ascx.cs" Inherits="DealerManagementSystem.ViewMaster.UserControls.CustomerVerification" %>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

<asp:Panel ID="pnlVerification" runat="server">

    <br />
    <br />
    <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true">
        <Columns>
            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerFullName")%>' runat="server" />
                    <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact Person">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblMobile" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                    </asp:Label>
                    <asp:Label ID="lblEMail" runat="server">
                  <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "EMail")%>'><%# DataBinder.Eval(Container.DataItem, "EMail")%></a>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="GST">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblGSTIN" Text='<%# DataBinder.Eval(Container.DataItem, "GSTIN")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Address1">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblAddress1" Text='<%# DataBinder.Eval(Container.DataItem, "Address1")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Address2">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblAddress2" Text='<%# DataBinder.Eval(Container.DataItem, "Address2")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Address3">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblAddress3" Text='<%# DataBinder.Eval(Container.DataItem, "Address3")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="District">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District.District")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Pincode">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblPincode" Text='<%# DataBinder.Eval(Container.DataItem, "Pincode")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Button ID="btnVerified" runat="server" CssClass="btn Save" Text="Verified" OnClick="btnVerified_Click" Width="150px"></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#ffffff" />
        <FooterStyle ForeColor="White" />
        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
    </asp:GridView>

    <br />
    <br />
    <asp:GridView ID="gvCustomerDuplicate" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid"
        EmptyDataText="No Data Found" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvCustomerDuplicate_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Customer Name" SortExpression="Country">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerFullName")%>' runat="server" />
                    <asp:Label ID="lblCustomerID" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerID")%>' runat="server" Visible="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact Person">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblMobile" runat="server">
                                                <a href='tel:<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'><%# DataBinder.Eval(Container.DataItem, "Mobile")%></a>
                    </asp:Label>
                    <asp:Label ID="lblEMail" runat="server">
                  <a href='mailto:<%# DataBinder.Eval(Container.DataItem, "EMail")%>'><%# DataBinder.Eval(Container.DataItem, "EMail")%></a>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="GST">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblGSTIN" Text='<%# DataBinder.Eval(Container.DataItem, "GSTIN")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Address1">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblAddress1" Text='<%# DataBinder.Eval(Container.DataItem, "Address1")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Address2">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblAddress2" Text='<%# DataBinder.Eval(Container.DataItem, "Address2")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Address3">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblAddress3" Text='<%# DataBinder.Eval(Container.DataItem, "Address3")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="District">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "District.District")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Pincode">
                <ItemStyle VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:Label ID="lblPincode" Text='<%# DataBinder.Eval(Container.DataItem, "Pincode")%>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Merge with this customer">
                <ItemTemplate>
                    <asp:Button ID="btnMergeCustomer" runat="server" Text="Merge with this customer" CssClass="btn Back" OnClick="btnMergeCustomer_Click" Width="250px" Height="25px" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#ffffff" />
        <FooterStyle ForeColor="White" />
        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
    </asp:GridView>


</asp:Panel>

