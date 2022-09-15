<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddFSRAttachments.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.AddFSRAttachments" %>
 
 
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">FSR Attached Name</label>
            <asp:DropDownList ID="ddlFSRAttachedName" runat="server" CssClass="TextBox" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">File</label>
            <asp:FileUpload ID="fu" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
        </div> 
    </div>
</fieldset>


