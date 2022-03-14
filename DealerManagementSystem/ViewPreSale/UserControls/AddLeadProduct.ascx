<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddLeadProduct.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddLeadProduct" %>

<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Product Type</label>
            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Product</label>
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Quantity</label>
            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Remark</label>
            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
</fieldset>
