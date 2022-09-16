<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketAddMaterialCharges.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketAddMaterialCharges" %>

<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Material</label>
            <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" ></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">SupersedeYN</label>
             <asp:CheckBox ID="cbSupersedeYN" runat="server" Checked="true" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Material S/N</label>
             <asp:TextBox ID="txtMaterialSN" runat="server" CssClass="form-control"  ></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Qty</label>
            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"  ></asp:TextBox>
        </div>
       
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">FLD Material</label>
            <asp:TextBox ID="txtDefectiveMaterial" runat="server" CssClass="form-control"  ></asp:TextBox>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">FLD Material S/N</label>
            <asp:TextBox ID="txtDefectiveMaterialSN" runat="server" CssClass="form-control"  ></asp:TextBox>
        </div>
         <div class="col-md-6 col-sm-12">
            <label class="modal-label">Prime Faulty Part</label>
              <asp:CheckBox ID="cbIsFaultyPart" runat="server" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Recomened Parts</label>
            <asp:CheckBox ID="cbRecomenedParts" runat="server" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Quotation  Parts</label>
            <asp:CheckBox ID="cbQuotationParts" runat="server" />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Source</label> 
            <asp:DropDownList ID="ddlMaterialSource" runat="server" CssClass="form-control"   />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">TSIR No</label> 
            <asp:DropDownList ID="ddlTSIRNumber" runat="server" CssClass="form-control"  />
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Parts Invoice</label> 
            <asp:TextBox ID="txtOldInvoice" runat="server" CssClass="form-control"> </asp:TextBox>
        </div>
    </div>
</fieldset>      
                        
<script type="text/javascript">
    function collapseExpandMaterialCharges(obj) {
        var gvObject = document.getElementById("MainContent_DMS_ICTicketMaterialCharges_pnlMaterialCharges");
        var imageID = document.getElementById("MainContent_DMS_ICTicketMaterialCharges_imgMaterialCharges");

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
    }

    $(document).ready(function () {
        var gvTickets = document.getElementById('MainContent_DMS_ICTicketMaterialCharges_gvMaterial');

        if (gvTickets != null) {
            for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                var lblNoteType = document.getElementById('MainContent_DMS_ICTicketMaterialCharges_gvMaterial_lblItem_' + i);
                if (lblNoteType != null) {
                    if (lblNoteType.innerHTML == "0") {
                        lblNoteType.parentNode.parentNode.style.display = "none";
                    }
                }
            }
        }
    });

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
    function InIEvent() { }

    $(document).ready(InIEvent);

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            $("#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtDefectiveMaterial").autocomplete({
                source: function (request, response) {
                    var param = { input: $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtDefectiveMaterial').val() };
                    $.ajax({
                        url: "ICTicket.aspx/SearchServiceMaterials",
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
        $("#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtDefectiveMaterial").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtDefectiveMaterial').val() };
                $.ajax({
                    url: "ICTicket.aspx/SearchServiceMaterials",
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
  
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            $("#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtMaterial").autocomplete({
                source: function (request, response) {
                    var param = { input: $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtMaterial').val() };
                    $.ajax({
                        url: "ICTicket.aspx/SearchServiceMaterials",
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
        $("#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtMaterial").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_UC_ICTicketView_UC_ICTicketAddMaterialCharges_txtMaterial').val() };
                $.ajax({
                    url: "ICTicket.aspx/SearchServiceMaterials",
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
<style>
    .footer {
        height: 15px;
        width: 100%;
    }

        .footer td {
            border: none;
        }

        .footer th {
            border: none;
        }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        var gvTickets = document.getElementById('MainContent_UC_BasicInformation1_gvMaterial');

        if (gvTickets != null) {
            for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                var lblItem = document.getElementById('MainContent_UC_BasicInformation1_gvMaterial_lblItem_' + i);
                if (lblItem.innerHTML == "0") {
                    lblItem.parentNode.parentNode.style.display = "none";
                }
            }
        }
    });
</script>
