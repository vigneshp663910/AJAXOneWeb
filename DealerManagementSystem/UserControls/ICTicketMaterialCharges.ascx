<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketMaterialCharges.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketMaterialCharges" %>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="15px" />
<table id="txnHistory1:panelGridid5" style="height: 100%; width: 100%">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">Material Charges</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandMaterialCharges();">
                        <img id="imgMaterialCharges" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>

            <asp:Panel ID="pnlMaterialCharges" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel5">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body5">
                        <div id="divWarrantyDistribution" runat="server">
                            <table class="labeltxt fullWidth">
                                <tr>
                                    <td>
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label10" runat="server" CssClass="label" Text="Customer Pay %"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtCustomerPayPercentage" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Pay %"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtDealerPayPercentage" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-row-left">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="AE Pay %"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtAEPayPercentage" runat="server" CssClass="hasDatepicker input" AutoComplete="Off" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSaveWarrantyDistribution" runat="server" Text="Save Warranty Distribution" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSaveWarrantyDistribution_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true" DataKeyNames="ServiceMaterialID" OnRowDataBound="gvMaterial_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbEdit" runat="server" OnCheckedChanged="cbEdit_CheckedChanged" AutoPostBack="true" />
                                        <asp:LinkButton ID="lbUpdate" runat="server" Text="Update" Visible="false" OnClick="lbUpdate_Click"></asp:LinkButton>
                                        <br />
                                        <br />
                                        <asp:LinkButton ID="lbEditCancel" runat="server" Text="Back" Visible="false" OnClick="lbEditCancel_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item" HeaderStyle-Width="30px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtMaterial" runat="server" CssClass="TextBox" Width="100px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:UpdatePanel ID="uptxtMaterialF" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtMaterialF" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="txtMaterialF" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="text-align: center">
                                            <tr>
                                                <td style="border: 0px">
                                                    <asp:Label ID="lblSupersedeYN" Text="Supersede Y/N" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="border: 0px">
                                                    <asp:CheckBox ID="cbSupersedeYN" runat="server" Checked="true" /></td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material S/N">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtMaterialSN" runat="server" CssClass="TextBox" Width="80px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMaterialSNF" runat="server" CssClass="TextBox" Width="80px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox" Width="60px" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtQtyF" runat="server" CssClass="TextBox" Width="60px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Avl Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAvailableQty" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableQty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbCheckAvailability" runat="server" OnClick="lbCheckAvailability_Click">Check Availability</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prime Faulty Part">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbIsFaultyPart" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsFaultyPart")%>' Enabled="false" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="cbIsFaultyPartF" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FLD Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialCode")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtDefectiveMaterial" runat="server" CssClass="TextBox" Width="100px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:UpdatePanel ID="up" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDefectiveMaterialF" runat="server" CssClass="TextBox" Width="100px"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="txtDefectiveMaterialF" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FLD Material S/N">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDefectiveMaterialSN" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtDefectiveMaterialSN" runat="server" CssClass="TextBox" Width="80px" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtDefectiveMaterialSNF" runat="server" CssClass="TextBox" Width="80px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recomened Parts">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbRecomenedParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsRecomenedParts")%>' Enabled="false" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="cbRecomenedPartsF" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quotation  Parts">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbQuotationParts" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsQuotationParts")%>' Enabled="false" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="cbQuotationPartsF" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialSource" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSource")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblMaterialSourceID" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialSource.MaterialSourceID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlMaterialSource" runat="server" CssClass="TextBox" Width="80px" Visible="false" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlMaterialSourceF" runat="server" CssClass="TextBox" Width="80px" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TSIR No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTsirNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirNumber")%>' runat="server"></asp:Label>
                                        <asp:Label ID="lblTsirID" Text='<%# DataBinder.Eval(Container.DataItem, "TSIR.TsirID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlTSIRNumber" runat="server" CssClass="TextBox" Width="122px" Visible="false" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlTSIRNumberF" runat="server" CssClass="TextBox" Width="122px" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qtn No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="SO Number" HeaderStyle-Width="55px">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>  
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Claim No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO No.">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPONumber" Text='<%# DataBinder.Eval(Container.DataItem, "PONumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parts Invoice">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOldInvoice" Text='<%# DataBinder.Eval(Container.DataItem, "OldInvoice")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtOldInvoiceF" runat="server" CssClass="TextBox" Width="80px"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCancel" Text="Canceled" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsDeleted")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblMaterialRemove" runat="server" OnClick="lblMaterialRemove_Click">Cancel</asp:LinkButton>
                                        <%-- <asp:LinkButton ID="lblMaterialRemove" runat="server" OnClick="lblMaterialRemove_Click" Visible='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrQtnRequested_N")%>'>Remove</asp:LinkButton>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lblMaterialAdd" runat="server" OnClick="lblMaterialAdd_Click">Add</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="btnGenerateQuotation" runat="server" Text="Generate Quotation" CssClass="InputButton" OnClick="btnGenerateQuotation_Click" OnClientClick="return ConfirmCreate();" />
                        <asp:Button ID="btnRequestForClaim" runat="server" Text="Request For Claim" CssClass="InputButton" OnClick="btnRequestForClaim_Click" OnClientClick="return ConfirmCreate();" />
                    </div>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>




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

    //$(document).ready(function () {
    //    var tablefixedWidthID = document.getElementById('tablefixedWidthID');
    //    var $width = $(window).width() - 28;
    //    tablefixedWidthID.style.width = $width + "px";
    //});

</script>

<script type="text/javascript">
    function InIEvent() { }

    $(document).ready(InIEvent);

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            $("#MainContent_DMS_ICTicketMaterialCharges_gvMaterial_txtMaterialF").autocomplete({
                source: function (request, response) {
                    var param = { input: $('#MainContent_DMS_ICTicketMaterialCharges_gvMaterial_txtMaterialF').val() };
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
    };

    $(function () {
        $("#MainContent_DMS_ICTicketMaterialCharges_gvMaterial_txtMaterialF").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_DMS_ICTicketMaterialCharges_gvMaterial_txtMaterialF').val() };
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





    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            $("#MainContent_DMS_ICTicketMaterialCharges_gvMaterial_txtDefectiveMaterialF").autocomplete({
                source: function (request, response) {
                    var param = { input: $('#MainContent_DMS_ICTicketMaterialCharges_gvMaterial_txtDefectiveMaterialF').val() };
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
    };

    $(function () {
        $("#MainContent_DMS_ICTicketMaterialCharges_gvMaterial_txtDefectiveMaterialF").autocomplete({
            source: function (request, response) {
                var param = { input: $('#MainContent_DMS_ICTicketMaterialCharges_gvMaterial_txtDefectiveMaterialF').val() };
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
