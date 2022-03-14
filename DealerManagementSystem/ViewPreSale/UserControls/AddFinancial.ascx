<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddFinancial.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddFinancial" %>
 
    <fieldset class="fieldset-border" id="Fieldset1" runat="server">
        <div class="col-md-12">
            <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Bank Name</label> 
               <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6 col-sm-12">
                    <label class="modal-label">Finance Percentage</label> 
                <asp:TextBox ID="txtFinancePercentage" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
            </div>
           <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Remark</label> 
               <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
            </div> 
        </div>
    </fieldset>
 