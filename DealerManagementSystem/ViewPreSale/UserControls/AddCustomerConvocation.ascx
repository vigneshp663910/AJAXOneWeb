<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCustomerConvocation.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddCustomerConvocation" %>

 
    <fieldset class="fieldset-border" id="Fieldset1" runat="server">
        <div class="col-md-12">
           <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Sales Engineer</label> 
                <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
            </div>
           <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Progress Status</label> 
                <asp:DropDownList ID="ddlProgressStatus" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Convocation Date</label> 
                <asp:TextBox ID="txtConvocationDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
            </div>
           <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Convocation</label> 
                <asp:TextBox ID="txtConvocation" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </fieldset>
 
