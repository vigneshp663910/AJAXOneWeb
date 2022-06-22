<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCustomerConversation.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddCustomerConversation" %>


<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Sales Engineer</label>
            <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
        </div>
       <%-- <div class="col-md-6 col-sm-12">
            <label class="modal-label">Progress Status</label>
            <asp:DropDownList ID="ddlProgressStatus" runat="server" CssClass="form-control" />
        </div>--%>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Conversation Date</label>
            <asp:TextBox ID="txtConversationDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Conversation</label>
            <asp:TextBox ID="txtConversation" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" Height="200px"></asp:TextBox>
        </div>
    </div>
</fieldset>

