<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketAddTechnicianWork.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketAddTechnicianWork" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="updatepnl" runat="server">
    <ContentTemplate>
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Technician</label>
                    <asp:DropDownList ID="ddlTechnician" runat="server" CssClass="TextBox" OnSelectedIndexChanged="ddlTechnician_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Worked Day</label>
                    <asp:TextBox ID="txtWorkedDate" runat="server" CssClass="TextBox" onkeyup="return removeText('MainContent_gvServiceCharges_txtServiceDate');"></asp:TextBox>
                    <asp:CalendarExtender ID="ceWorkedDate" runat="server" TargetControlID="txtWorkedDate" PopupButtonID="txtWorkedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtWorkedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                </div>
                <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Worked Hours</label>
                    <asp:TextBox ID="txtWorkedHours" runat="server" CssClass="TextBox"></asp:TextBox>
                </div>
            </div>
        </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
