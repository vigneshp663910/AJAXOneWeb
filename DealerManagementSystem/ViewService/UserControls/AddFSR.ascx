<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddFSR.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.AddFSR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Label ID="lblMessageFSR" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Mode Of Payment</label>
            <asp:DropDownList ID="ddlModeOfPayment" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Operator Name</label>
            <asp:TextBox ID="txtOperatorName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Operator Contact No</label>
            <asp:TextBox ID="txtOperatorNumber" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Machine Maintenance Level</label>
            <asp:DropDownList ID="ddlMachineMaintenanceLevel" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Is Rental</label>
            <asp:CheckBox ID="cbIsRental" runat="server" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Rental Contractor Name</label>
            <asp:TextBox ID="txtRentalName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Rental Contractor Contact No</label>
            <asp:TextBox ID="txtRentalNumber" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Nature Of Complaint</label>
            <asp:TextBox ID="txtNatureOfComplaint" runat="server" CssClass="form-control"  ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Observation</label>
            <asp:TextBox ID="txtObservation" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">Work Carried Out</label>
            <asp:TextBox ID="txtWorkCarriedOut" runat="server" CssClass="form-control" TextMode="MultiLine"  ></asp:TextBox>
        </div>
        <div class="col-md-12 col-sm-12">
            <label class="modal-label">SE Suggestion</label>
            <asp:TextBox ID="txtReport" runat="server" CssClass="form-control" TextMode="MultiLine"  ></asp:TextBox>
        </div>
    </div>
</fieldset>


 
<script type="text/javascript">
 

    function InIEvent() { }

    $(document).ready(InIEvent);

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            $("#MainContent_DMS_ICTicketFSR_txtNatureOfComplaint").autocomplete({
                source: function (request, response) {
                    var param = { input: $('#MainContent_DMS_ICTicketFSR_txtNatureOfComplaint').val() };
                    $.ajax({
                        url: "DMS_ICTicketProcess.aspx/SearchMaterialNatureOfComplaint",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var err = eval("(" + XMLHttpRequest.responseText + ")");
                            alert(err.Message)
                            // console.log("Ajax Error!");  
                        }
                    });
                },
                minLength: 2 //This is the Char length of inputTextBox  
            });
        });
    };
    $(function () {
        $("#MainContent_DMS_ICTicketFSR_txtNatureOfComplaint").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_DMS_ICTicketFSR_txtNatureOfComplaint').val() };
                $.ajax({
                    url: "DMS_ICTicketProcess.aspx/SearchMaterialNatureOfComplaint",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        var err = eval("(" + XMLHttpRequest.responseText + ")");
                        alert(err.Message)
                        // console.log("Ajax Error!");  
                    }
                });
            },
            minLength: 2 //This is the Char length of inputTextBox  
        });
    });
</script>
