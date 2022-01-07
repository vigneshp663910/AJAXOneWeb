<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Effort.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.Effort" %>
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
                <label>Effort Type</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlEffortType" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2 text-right">
                <label>Effort Date</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtEffortDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-2 text-right">
                <label>Effort Start Time</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtEffortStartTime" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Time"></asp:TextBox>
            </div>
            <div class="col-md-2 text-right">
                <label>Effort End Time</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtEffortEndTime" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Time"></asp:TextBox>
            </div>
            <div class="col-md-2 text-right">
                <label>Remark</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" Rows="6" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </fieldset>
</div>
