<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketAddOtherMachine.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketAddOtherMachine" %>
 
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Type Of Machine</label>
            <asp:DropDownList ID="ddlTypeOfMachine" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Quantity</label>
            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Make</label>
            <asp:DropDownList ID="ddlMake" runat="server" CssClass="form-control" />
        </div> 
    </div>
</fieldset> 