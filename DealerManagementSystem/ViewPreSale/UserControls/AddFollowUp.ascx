<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddFollowUp.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddFollowUp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
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
                <label>Date</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtFollowUpDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"  ></asp:TextBox>
                <asp1:CalendarExtender ID="cxFollowUpDate" runat="server" TargetControlID="txtFollowUpDate" PopupButtonID="txtFollowUpDate" Format="dd/MM/yyyy" />
                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtFollowUpDate" WatermarkText="DD/MM/YYYY" />
            </div>
            <div class="col-md-2 text-right">
                <label>Remark</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtFollowUpNote" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>

        </div>
    </fieldset>
</div>
