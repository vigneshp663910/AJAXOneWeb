<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddICTicketCustomerFeedback.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.AddICTicketCustomerFeedback" %>

 
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12"> 
         <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Customer Satisfaction Level</label>
                    <asp:DropDownList ID="ddlCustomerSatisfactionLevel" runat="server" CssClass="form-control" />
                </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Remarks</label>
             <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">FilePhoto</label>
            <asp:FileUpload ID="fuPhoto" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
        </div>
         <div class="col-md-12 col-sm-12">
            <label class="modal-label">Signature</label>
            <asp:FileUpload ID="fuSignature" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
        </div> 
    </div>
</fieldset>

