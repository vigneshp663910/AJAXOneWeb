<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderItem.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderItem" %>

<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
         <div class="col-md-6 col-sm-12">
            <label class="modal-label">SupersedeYN</label>
             <asp:CheckBox ID="cbSupersedeYN" runat="server" Checked="true" />
        </div> 
        <div class="col-md-6 col-sm-12">
            <asp:HiddenField ID="hdfMaterialID" runat="server" />
            <label class="modal-label">Material</label>
            <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control"  onKeyUp="GetMaterial()"></asp:TextBox>
        </div> 
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Qty</label>
            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"  ></asp:TextBox>
        </div> 
    </div>
</fieldset>  

<script type="text/javascript">

    function GetMaterial() {
        $("#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_hdfMaterialID").val('');
        var param = { Material: $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtMaterial').val(), MaterialType: '' }
        var Customers = [];
        if ($('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtMaterial').val().trim().length >= 3) {
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
                    $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) { $("#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_hdfMaterialID").val(u.item.value1); },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtMaterial').width() + 48,
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
            $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtMaterial').autocomplete({
                source: function (request, response) {
                    response($.ui.autocomplete.filter(Customers, ""))
                }
            });
        }
    }
    function GetMaterialDefective() {
        $("#MainContent_UC_ICTicketView_UC_ICTicketAddMaterial_hdfDefectiveMaterialID").val('');
        var param = { Material: $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtDefectiveMaterial').val(), MaterialType: '' }
        var Customers = [];
        if ($('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtDefectiveMaterial').val().trim().length >= 3) {
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
                    $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtDefectiveMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) { $("#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_hdfDefectiveMaterialID").val(u.item.value1); },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtDefectiveMaterial').width() + 48,
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
            $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtDefectiveMaterial').autocomplete({
                source: function (request, response) {
                    response($.ui.autocomplete.filter(Customers, ""))
                }
            });
        }
    }
    function FormatAutocompleteList(item) {
        var inner_html = '<a>';
        inner_html += '<p style="margin:0;">eferdfed<strong>' + item.value + '</strong></p>';
        inner_html += '</a>';
        return inner_html;
    }
</script>