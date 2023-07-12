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
                <div class="col-md-6 col-sm-12">
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
                </div>
            </div>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
