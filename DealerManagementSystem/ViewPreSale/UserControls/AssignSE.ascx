<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignSE.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AssignSE" %>

<div class="col-md-12">
    <fieldset class="fieldset-border" id="Fieldset1" runat="server">
        <div class="col-md-12">
            <div class="col-md-2 text-right">
                <label>Sales Engineer</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
            </div>
           
        </div>
    </fieldset>
</div>