<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Effort.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.Effort" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Sales Engineer</label>
            <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Effort Type</label>
            <asp:DropDownList ID="ddlEffortType" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Effort Date</label>
            <asp:TextBox ID="txtEffortDate" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            <asp1:CalendarExtender ID="cxEffortDate" runat="server" TargetControlID="txtEffortDate" PopupButtonID="txtEffortDate" Format="dd/MM/yyyy" />
            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtEffortDate" WatermarkText="DD/MM/YYYY" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Effort Start Time</label>
            <asp:TextBox ID="txtEffortStartTime" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Time"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Effort End Time</label>
            <asp:TextBox ID="txtEffortEndTime" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Time"></asp:TextBox>
        </div>
        <div class="col-md-6">
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Remark</label>
            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
</fieldset>
</div>
