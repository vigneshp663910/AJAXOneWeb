<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketEscalationOnBreakdownLevel.ascx.cs" Inherits="DealerManagementSystem.Dashboard.ICTicketEscalationOnBreakdownLevel" %>


<label class="modal-label">
    Dealer
    <asp:Literal ID="ucTitle" runat="server"></asp:Literal>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lbExportExcel" runat="server" OnClick="lbExportExcel_Click">ExportExcel</asp:LinkButton>
</label>

<asp:GridView ID="gvMaterialAnalysis" runat="server" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No data available!" Width="98%" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField HeaderText="IC Ticket." HeaderStyle-Width="62px">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ICTicketNumber")%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ticket Date" HeaderStyle-Width="92px">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ICTicketDate")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ser. Req. Dt" HeaderStyle-Width="75px" Visible="false">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.RequestedDate")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="SE Reached Dt" Visible="false">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ReachedDate")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Status" HeaderStyle-Width="147px">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblServiceStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ServiceStatus.ServiceStatus")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Customer" HeaderStyle-Width="100px">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Customer.CustomerCode")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Customer Name">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Customer.CustomerName")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Contact Name">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ContactPerson")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Contact Number">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblPresentContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.PresentContactNumber")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Dealer">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Dealer.DealerCode")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Problem Reported" Visible="false">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblproblem_reported" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.ComplaintDescription")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Model">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Equipment.EquipmentModel.Model")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Serial No.">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Equipment.EquipmentSerialNo")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Dealer Name">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Dealer.DealerName")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ser Engg Name" Visible="false">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblser_name" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.Technician.ContactName")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="HMR" Visible="false">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblr_hmr_desc" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.CurrentHMRValue")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Application" Visible="false">
            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lblr_application" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicket.MainApplication.MainApplication")%>' runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle BackColor="#ffffff" />
    <FooterStyle ForeColor="White" />
    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
</asp:GridView>



