<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WarrantyClaimDebitNoteAcknowledgePending.ascx.cs" Inherits="DealerManagementSystem.Dashboard.WarrantyClaimDebitNoteAcknowledgePending" %>
<div class="modbox">

    <div class="modtitle">
        <asp:Literal ID="ucTitle" runat="server" Text="Debit Note Acknowledge Pending"></asp:Literal>
    </div>
    <div class="modboxin">
        <div class="dashboardGrid">
            <asp:GridView ID="gvClaimInvoice" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="WarrantyClaimDebitNoteID" CssClass="TableGrid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                <Columns>                 
                    <asp:TemplateField HeaderText="Debit Note Number">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                              <asp:Label ID="lblClaimID" Text='<%# DataBinder.Eval(Container.DataItem, "DebitNoteNumber")%>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Date">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblClaimDate" Text='<%# DataBinder.Eval(Container.DataItem, "DebitNoteDate","{0:d}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice Number">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblAnnexureNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dealer">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dealer Name">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grand Total">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "GrandTotal","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="#BFE4FF" ForeColor="Black" />
                <HeaderStyle BackColor="#BFE4FF" Font-Bold="True" ForeColor="Black" />
                <RowStyle ForeColor="Black" BackColor="#bfe4ff" />
            </asp:GridView>
        </div>
    </div>

</div>