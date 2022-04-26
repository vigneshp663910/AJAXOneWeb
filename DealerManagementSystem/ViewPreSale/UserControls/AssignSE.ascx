<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignSE.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AssignSE" %>

<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Dealer Sales Engineer</label>
            <asp:DropDownList ID="ddlDealerSalesEngineer" runat="server" CssClass="form-control" />
        </div> 
         <div class="col-md-6 col-sm-12">
            <label class="modal-label">Ajax Sales Engineer</label>
            <asp:DropDownList ID="ddlAjaxSalesEngineer" runat="server" CssClass="form-control" />
        </div> 
    </div>
</fieldset>
