<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddFollowUp.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddFollowUp" %>

<div class="col-md-12">
    <fieldset class="fieldset-border" id="Fieldset1" runat="server">
        <div class="col-md-12">
            <div class="col-md-2 text-right">
                <label>Sales Engineer</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2 text-right">
                <label>Date</label>
            </div>
            <div class="col-md-4">
                 <asp:TextBox ID="txtFollowUpDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-2 text-right">
                <label>Remark</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtFollowUpNote" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>
            
        </div>
    </fieldset>
</div>