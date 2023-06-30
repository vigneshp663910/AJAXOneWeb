<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketUpdateRestore.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketUpdateRestore" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12"> 
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Customer Satisfaction Level</label>
                    <asp:DropDownList ID="ddlCustomerSatisfactionLevel" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Customer Remarks</label>
                    <asp:TextBox ID="txtCustomerRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>  
            </div>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
