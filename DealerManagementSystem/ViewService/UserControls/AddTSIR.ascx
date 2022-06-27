<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTSIR.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.AddTSIR" %>
<asp:Label ID="lblMessageTSIR" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">SRO Code</label>
            <asp:DropDownList ID="ddlServiceChargeID" runat="server" CssClass="form-control"   DataValueField="ServiceChargeID" DataTextField="MaterialCode" />
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Nature Of Failures</label>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label"></label>
            <asp:TextBox ID="txtNatureOfFailures" runat="server" CssClass="form-control" AutoComplete="SP" TextMode="MultiLine"  ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">How Was Problem Noticed / Who  / When</label>
            <asp:TextBox ID="txtProblemNoticedBy" runat="server" CssClass="form-control" AutoComplete="SP" TextMode="MultiLine"  ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Under What Condition Did The Failure Taken Place</label>
            <asp:TextBox ID="txtUnderWhatConditionFailureTaken" runat="server" CssClass="form-control" AutoComplete="SP" TextMode="MultiLine" ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Failure Details</label>
            <asp:TextBox ID="txtFailureDetails" runat="server" CssClass="form-control" AutoComplete="SP" TextMode="MultiLine" ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Points Checked</label>
            <asp:TextBox ID="txtPointsChecked" runat="server" CssClass="form-control" AutoComplete="SP" TextMode="MultiLine" ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Possible Root Causes</label>
            <asp:TextBox ID="txtPossibleRootCauses" runat="server" CssClass="TextBox" AutoComplete="SP" TextMode="MultiLine"  ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Specific Points Noticed</label>
            <asp:TextBox ID="txtSpecificPointsNoticed" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Parts Invoice Number</label>
            <asp:TextBox ID="txtPartsInvoiceNumber" runat="server" CssClass="form-control" AutoComplete="SP"  ></asp:TextBox>
        </div>
    </div>
</fieldset>
- 

