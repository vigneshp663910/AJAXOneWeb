<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCustomerConvocation.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddCustomerConvocation" %>

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
                <label>Progress Status</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlProgressStatus" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2 text-right">
                <label>Convocation Date</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtConvocationDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-md-2 text-right">
                <label>Convocation</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtConvocation" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </fieldset>
</div>
