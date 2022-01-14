<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddFinancial.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddFinancial" %>

<div class="col-md-12">
    <fieldset class="fieldset-border" id="Fieldset1" runat="server">
        <div class="col-md-12">
            <div class="col-md-2 text-right">
                <label>Bank Name</label>
            </div>
            <div class="col-md-4">
               <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2 text-right">
                <label>Finance Percentage</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtFinancePercentage" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
            </div>
            <div class="col-md-2 text-right">
                <label>Remark</label>
            </div>
            <div class="col-md-4">
               <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
            </div> 
        </div>
    </fieldset>
</div>