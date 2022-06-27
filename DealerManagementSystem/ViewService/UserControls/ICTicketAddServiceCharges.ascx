<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketAddServiceCharges.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.ICTicketAddServiceCharges" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">SRO Code</label>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtServiceMaterial" runat="server" CssClass="form-control" ></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="txtServiceMaterial" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Service Date</label>

            <asp:TextBox ID="txtServiceDate" runat="server" CssClass="form-control" onkeyup="return removeText('MainContent_gvServiceCharges_txtServiceDate');"></asp:TextBox>
            <asp:CalendarExtender ID="ceServiceDate" runat="server" TargetControlID="txtServiceDate" PopupButtonID="txtServiceDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtServiceDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

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
<script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


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
    function InIEvent() {

    }

    $(document).ready(InIEvent);

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            $("#MainContent_UC_ICTicketView_UC_ICTicketAddServiceCharges_txtServiceMaterial").autocomplete({
                source: function (request, response) {
                    var param = { input: $('#MainContent_DMS_ICTicketServiceCharges_gvServiceCharges_txtServiceMaterial').val() };
                    $.ajax({
                        url: "ICTicket.aspx/SearchMaterials",
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
        $("#MainContent_DMS_ICTicketServiceCharges_gvServiceCharges_txtServiceMaterial").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_DMS_ICTicketServiceCharges_gvServiceCharges_txtServiceMaterial').val() };
                $.ajax({
                    url: "DMS_ICTicketProcess.aspx/SearchMaterials",
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

<script type="text/javascript">
    $(function () {
        $("#MainContent_DMS_ICTicketServiceCharges_txtMaterial").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_DMS_ICTicketServiceCharges_txtMaterial').val() };

                $.ajax({
                    url: "DMS_ICTicketProcess.aspx/SearchSMaterial",
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
<style>
    .AutoExtender {
        font-family: Verdana, Helvetica, sans-serif;
        font-size: .8em;
        font-weight: normal;
        border: solid 1px #006 height: 25px;
        ing: 20px;


    {
        om: do ted 1p cursor: pointer;
        color: roon;
        ighlight;

    {
        color: White;
        background-color: #006699;
        cursor: pointer;
    }

    /*#divwidt
                 width: 150px !important;
        }

            #divwidth div {
                width: 150px !important;
            }*/
</style>


<script type="text/javascript">
    $(document).ready(function () {
        $("#MainContent_txtSearch").autocomplete('Search_CS.ashx');
    });
</script>

<script type="text/javascript">
    $(document).ready(function () {
        var gvTickets = document.getElementById('MainContent_DMS_ICTicketServiceCharges_gvServiceCharges');
        if (gvTickets != null) {
            for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                var lblItem = document.getElementById('MainContent_DMS_ICTicketServiceCharges_gvServiceCharges_lblItem_' + i);

                if (lblItem != null) {
                    if (lblItem.innerHTML == "0") {
                        lblItem.parentNode.parentNode.style.display = "none";
                    }
                }
            }
        }

        var gvAttachedFile = document.getElementById('MainContent_DMS_ICTicketServiceCharges_gvAttachedFile');

        if (gvAttachedFile != null) {
            for (var i = 0; i < gvAttachedFile.rows.length - 1; i++) {
                var lblFileName = document.getElementById('MainContent_DMS_ICTicketServiceCharges_gvAttachedFile_lblFileName_' + i);
                if (lblFileName != null) {
                    if (lblFileName.innerHTML == "") {
                        lblFileName.parentNode.parentNode.parentNode.style.display = "none";
                    }
                }
            }
        }
    });
</script>
