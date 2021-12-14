<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EquipmentView.ascx.cs" Inherits="DealerManagementSystem.UserControls.EquipmentView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript">
    function collapseExpand(obj) {
        var gvObject = document.getElementById(obj);
        var imageID = document.getElementById('image' + obj);

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
    }

    function OpenInNewTab(url) {
        var win = window.open(url, '_blank');
        win.focus();
    }
</script>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="24px" />
<table id="txnHistory1:panelGridid6" style="height: 100%; width: 100%" class="IC_materialInfo">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">Equipment Information</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandNotes();">
                        <img id="imgNotes" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="pnlNotes" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel6">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body6">

                        <asp:GridView ID="gvEquipment" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderText="Customer Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Equipment Serial No">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentSerialNo")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Model">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "EquipmentModel.Model")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Warranty Expiry Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblWarrantyExpiryDate" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyExpiryDate")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current HMR Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "CurrentHMRDate")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current HMR Value">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentHMRValue" Text='<%# DataBinder.Eval(Container.DataItem, "CurrentHMRValue")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>


<table id="txnHistory:panelGridid" style="height: 100%; width: 100%" class="IC_materialInfo">
    <tr>
        <td>
            <div class="boxHead">
                <div class="logheading">IC Ticket Information</div>
                <div style="float: right; padding-top: 0px">
                    <a href="javascript:collapseExpandNotes();">
                        <img id="img1" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                </div>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <div class="rf-p " id="txnHistory:inputFiltersPanel">
                    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">

                        <asp:GridView ID="gvICTicket" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" DataKeyNames="ICTicketID" Width="100%" OnRowDataBound="gvICTicket_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="javascript:collapseExpand('ICTicketID-<%# Eval("ICTicketID") %>');">
                                            <img id="imageICTicketID-<%# Eval("ICTicketID") %>" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="10" width="10" /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IC Ticket Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketNumber")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IC Ticket Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceType" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceType.ServiceType")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Priority">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "ServicePriority.ServicePriority")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Status">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEquipmentSerialNo" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceStatus.ServiceStatus")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Margin Warranty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsMarginWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "IsMarginWarranty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requested Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestedDate" Text='<%# DataBinder.Eval(Container.DataItem, "RequestedDate")%>' runat="server"></asp:Label>
                                        <tr>
                                            <td colspan="100%" style="padding-left: 96px">
                                                <div id="ICTicketID-<%# Eval("ICTicketID") %>" style="display: inline; position: relative;">
                                                    <asp:GridView ID="gvServiceCharges" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" ShowFooter="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbClaimRequested" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrInvRequested_N")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ser Prod ID">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ser Prod Desc">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDate" Text='<%# DataBinder.Eval(Container.DataItem, "Date","{0:d}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Worked Hours">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "WorkedHours")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Base Price">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBasePrice" Text='<%# DataBinder.Eval(Container.DataItem, "BasePrice")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discount">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Claim Number">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quotation Number">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pro. Invoice Number">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProformaInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ProformaInvoiceNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Invoice Number">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Claim / Invoice Requested" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbIsClaimRequested" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsClaimOrInvRequested")%>' Enabled="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Item" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material" HeaderStyle-Width="85px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="85px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSerProdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material S/N" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDate" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Defective Material" HeaderStyle-Width="55px" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWorkedHours" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialCode")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Defective Material S/N" HeaderStyle-Width="55px" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBasePrice" Text='<%# DataBinder.Eval(Container.DataItem, "DefectiveMaterial.MaterialSerialNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           <%-- <asp:TemplateField HeaderText="Customer Stock" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbIsCustomerStock" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsCustomerStock")%>' Enabled="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Receiving Status" HeaderStyle-Width="55px" Visible="false">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "ReceivingStatus")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Prime Faulty Part" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbIsFaultyPart" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsFaultyPart")%>' Enabled="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Available Qty" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAvailableQty" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableQty")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quotation Number" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuotationNumber" Text='<%# DataBinder.Eval(Container.DataItem, "QuotationNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delivery Number" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SO Number" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSaleOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Claim Number" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClaimNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimNumber")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>

<script type="text/javascript">
    function collapseExpandNotes(obj) {
        var gvObject = document.getElementById("MainContent_DMS_ICTicketNote_pnlNotes");
        var imageID = document.getElementById("MainContent_DMS_ICTicketNote_imgNotes");

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "Images/grid_expand.png";
        }
    }

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
        var gvTickets = document.getElementById('MainContent_DMS_ICTicketNote_gvNotes');

        if (gvTickets != null) {
            for (var i = 0; i < gvTickets.rows.length - 1; i++) {
                var lblNoteType = document.getElementById('MainContent_DMS_ICTicketNote_gvNotes_lblNoteType_' + i);
                if (lblNoteType != null) {
                    if (lblNoteType.innerHTML == "") {
                        lblNoteType.parentNode.parentNode.style.display = "none";
                    }
                }
            }
        }
    });
</script>