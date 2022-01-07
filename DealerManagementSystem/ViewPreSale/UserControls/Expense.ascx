<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Expense.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.Expense" %>


<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-3 text-right">
            <label>Sales Engineer</label>
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-3 text-right">
            <label>Expense Type</label>
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-3 text-right">
            <label>Expense Date</label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtExpenseDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
        </div>

        <div class="col-md-3 text-right">
            <label>Amount</label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
        </div>
        <div class="col-md-3 text-right">
            <label>Remark</label>
        </div>
        <div class="col-md-3">
            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
</fieldset>
