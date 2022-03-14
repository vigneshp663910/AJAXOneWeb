<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Expense.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.Expense" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Sales Engineer</label>
            <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Expense Type</label> 
            <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Expense Date</label> 
            <asp:TextBox ID="txtExpenseDate" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            <asp1:CalendarExtender ID="cxExpenseDate" runat="server" TargetControlID="txtExpenseDate" PopupButtonID="txtExpenseDate" Format="dd/MM/yyyy" />
            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtExpenseDate" WatermarkText="DD/MM/YYYY" />
        </div>

        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Amount</label> 
            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Remark</label> 
            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
</fieldset> 
