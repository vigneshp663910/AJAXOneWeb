<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddLead.ascx.cs" Inherits="DealerManagementSystem.ViewPreSale.UserControls.AddLead" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<fieldset class="fieldset-border" id="fldCountry" runat="server">
    <legend style="background: none; color: #007bff; font-size: 17px;">Lead</legend>
    <div class="col-md-12">
        <%-- <div class="col-md-6 col-sm-12">
            <label>Lead Date</label>
            <asp:TextBox ID="txtLeadDate" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date" AutoCompleteType="Disabled"></asp:TextBox>
        </div>--%>
        <div class="col-md-6 col-sm-12">
            <label>Product Type</label>
            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" />
        </div>

        <%--   <div class="col-md-6 col-sm-12">
            <label>Qualification</label>
            <asp:DropDownList ID="ddlQualification" runat="server" CssClass="form-control" />
        </div>--%>
        <div class="col-md-6 col-sm-12">
            <label>Source</label>
            <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-12 col-sm-12">

            <asp:HiddenField ID="hdfProjectID" runat="server" />
            <label>Project</label>
            <div>
                <div style="float: left; width: 95%">
                    <asp:TextBox ID="txtProject" runat="server" CssClass="form-control" BorderColor="Silver"
                        onKeyUp="GetProjectAuto('TextBox-id')"></asp:TextBox>
                </div>
                <div class="myDiv" style="float: left; width: 5%; height: 35px" onclick="myFunction()">
                    <i class="fa fa-fw fa-times" style="font-size: 25px"></i>
                </div>
                <%-- <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control"   />--%>
            </div>
        </div>
        <div class="col-md-6 col-sm-12">

            <label>Expected Date of Sale</label>
            <asp:TextBox ID="txtExpectedDateOfSale" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            <asp1:CalendarExtender ID="cxExpectedDateOfSale" runat="server" TargetControlID="txtExpectedDateOfSale" PopupButtonID="txtExpectedDateOfSale" Format="dd/MM/yyyy" />
            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="txtExpectedDateOfSale" WatermarkText="DD/MM/YYYY" />
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
        <div class="col-md-6 col-sm-12">
            <label>Next FollowUp Date</label>
            <asp:TextBox ID="txtNextFollowUpDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            <asp1:CalendarExtender ID="cxNextFollowUpDate" runat="server" TargetControlID="txtNextFollowUpDate" PopupButtonID="txtNextFollowUpDate" Format="dd/MM/yyyy" />
            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtNextFollowUpDate" WatermarkText="DD/MM/YYYY" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Sales Channel Type</label>
            <asp:DropDownList ID="ddlSalesChannelType" runat="server" CssClass="form-control" />
        </div>
    </div>
</fieldset>
<script type="text/javascript">

    function FormatAutocompleteProject(item) {

        var inner_html = '<a class="customer">';
        inner_html += '<p class="customer-name-info"><label>' + item.value + '</label></p>';
        inner_html += '<div class=customer-info><label class="contact-number">TenderNumber :' + item.TenderNumber + '</label>';
        inner_html += '<label class="customer-type">' + item.State + ',' + item.District + '</label></div>';

        inner_html += '<div class=customer-info><label class="contact-number">Award Date :' + item.ContractAwardDate + '</label>';
        inner_html += '<label class="customer-type" >' + 'End Date :' + item.ContractEndDate + '</label></div>';
        inner_html += '<div class=customer-info><label class="contact-number">Project Value :' + item.ProjectValue + '</label></div>';
        inner_html += '</a>';
        return inner_html;
    }

    function myFunction() {
        document.getElementById("MainContent_UC_AddLead_txtProject").disabled = false;
        document.getElementById("MainContent_UC_AddLead_txtProject").value = '';
        $("#MainContent_UC_AddLead_hdfProjectID").val('');

        document.getElementById("MainContent_UC_LeadView_UC_AddLead_txtProject").disabled = false;
        document.getElementById("MainContent_UC_LeadView_UC_AddLead_txtProject").value = '';
        $("#MainContent_UC_LeadView_UC_AddLead_hdfProjectID").val('');

    }
</script>

<style>
    .myDiv:hover {
        background-color: #CCC;
        cursor: pointer;
    }
</style>
