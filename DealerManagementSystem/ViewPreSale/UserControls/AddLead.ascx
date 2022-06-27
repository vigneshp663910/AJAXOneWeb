<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddLead.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddLead" %>
<fieldset class="fieldset-border" id="fldCountry" runat="server">
    <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label>Lead Date</label>
            <asp:TextBox ID="txtLeadDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label>Product Type</label>
            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
        </div>
       <%-- <div class="col-md-6 col-sm-12">
            <label>Category</label>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataTextField="Category" DataValueField="CategoryID" />
        </div>--%>

        <div class="col-md-6 col-sm-12">
            <label>Qualification</label>
            <asp:DropDownList ID="ddlQualification" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label>Source</label>
            <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label>Project</label>
            <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label>Urgency</label>
            <asp:DropDownList ID="ddlUrgency" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label>Application</label>
            <asp:DropDownList ID="ddlApplication" runat="server" CssClass="form-control" />
        </div> 
        <div class="col-md-12 col-sm-12">
            <label>Customer Feedback</label>
            <asp:TextBox ID="txtCustomerFeedback" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label>Remarks</label>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
        </div>
    </div>
</fieldset>
