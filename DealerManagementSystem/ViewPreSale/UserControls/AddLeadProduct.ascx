<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddLeadProduct.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddLeadProduct" %>

<div class="col-md-12">
    <fieldset class="fieldset-border" id="Fieldset1" runat="server">
        <div class="col-md-12">
            <div class="col-md-2 text-right">
                <label>Product Type</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2 text-right">
                <label>Product</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" />
            </div> 
            <div class="col-md-2 text-right">
                <label>Quantity</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
            </div>
            <div class="col-md-2 text-right">
                <label>Remark</label>
            </div>
            <div class="col-md-10">
                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </fieldset>
</div>