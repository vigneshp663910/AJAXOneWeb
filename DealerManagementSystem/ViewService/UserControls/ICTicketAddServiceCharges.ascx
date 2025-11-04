<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketAddServiceCharges.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketAddServiceCharges" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Service Date</label>
            <asp:HiddenField ID="hdfMaterialID" runat="server" />
            <asp:TextBox ID="txtServiceDate" runat="server" CssClass="form-control" onkeyup="return removeText('MainContent_gvServiceCharges_txtServiceDate');"></asp:TextBox>
            <asp:CalendarExtender ID="ceServiceDate" runat="server" TargetControlID="txtServiceDate" PopupButtonID="txtServiceDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtServiceDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">SRO Code</label>
            <asp:TextBox ID="txtServiceMaterial" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" onKeyUp="GetServiceCharges()"></asp:TextBox>
           
        </div>

        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Worked Hours</label>
            <asp:TextBox ID="txtWorkedHours" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Base Price</label>
            <asp:TextBox ID="txtBasePrice" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Discount</label>
            <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
        </div>

    </div>
</fieldset> 
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" /> 
<script type="text/javascript">
    function collapseExpandServiceCharges(obj) {
        var gvObject = document.getElementById("MainContent_DMS_ICTicketServiceCharges_pnlServiceCharges");
        var imageID = document.getElementById("MainContent_DMS_ICTicketServiceCharges_imgServiceCharges");

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
    }
    function removeText(id) {
        var TheTextBox = document.getElementById(id);
        TheTextBox.value = "";
        return false;
    }
    function ConfirmCreate() {
        var x = confirm("No changes will be allowed after saving");
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>


<script type="text/javascript">
    function GetServiceCharges() {
        $("#MainContent_UC_ICTicketView_UC_ICTicketAddServiceCharges_hdfMaterialID").val('');
        var param = { Material: $('#MainContent_UC_ICTicketView_UC_ICTicketAddServiceCharges_txtServiceMaterial').val(), MaterialType: 'DIEN' }
        var Customers = [];
        if ($('#MainContent_UC_ICTicketView_UC_ICTicketAddServiceCharges_txtServiceMaterial').val().trim().length >= 3) {
            $.ajax({
                url: "ICTicket.aspx/GetMaterial",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                data: JSON.stringify(param),
                dataType: 'JSON',
                success: function (data) {
                    var DataList = JSON.parse(data.d);
                    for (i = 0; i < DataList.length; i++) {
                        Customers[i] = {
                            value: DataList[i].MaterialCode + ' ' + DataList[i].MaterialDescription,
                            value1: DataList[i].MaterialID
                        };
                    }
                    $('#MainContent_UC_ICTicketView_UC_ICTicketAddServiceCharges_txtServiceMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) { $("#MainContent_UC_ICTicketView_UC_ICTicketAddServiceCharges_hdfMaterialID").val(u.item.value1); },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_ICTicketView_UC_ICTicketAddServiceCharges_txtServiceMaterial').width() + 48,
                            });
                            $(this).autocomplete("widget").scrollTop(0);
                        }
                    }).focus(function (e) {
                        $(this).autocomplete("search");
                    }).click(function () {
                        $(this).autocomplete("search");
                    }).data('ui-autocomplete')._renderItem = function (ul, item) {

                        var inner_html = FormatAutocompleteList(item);
                        return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;  z-index: 10002"></li>')
                            .data('item.autocomplete', item)
                            .append(inner_html)
                            .appendTo(ul);
                    };
                }
            });
        }
        else {
            $('#MainContent_UC_ICTicketView_UC_ICTicketAddServiceCharges_txtServiceMaterial').autocomplete({
                source: function (request, response) {
                    response($.ui.autocomplete.filter(Customers, ""))
                }
            });
        }
    }
    function FormatAutocompleteList(item) {
        var inner_html = '<a>';
        inner_html += '<p style="margin:0;"><strong>' + item.value + '</strong></p>';
        inner_html += '</a>';
        return inner_html;
    }     
</script>
 