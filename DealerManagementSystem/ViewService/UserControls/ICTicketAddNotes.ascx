<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketAddNotes.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketAddNotes" %>
 
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Note Type</label>
            <asp:DropDownList ID="ddlNoteType" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Comments</label>
             <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div> 
    </div>
</fieldset> 
 

 