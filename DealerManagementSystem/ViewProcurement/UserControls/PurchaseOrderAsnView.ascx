<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderAsnView.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderAsnView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Src="~/ViewProcurement/UserControls/GrCreate.ascx" TagPrefix="UC" TagName="UC_GrCreate" %>

<script type="text/javascript">
    function GrValidation() {
        debugger;
        var UnrestrictedQty, MissingQty, DamagedQty, AsnBalanceQty;
        UnrestrictedQty = document.getElementById("MainContent_UC_PurchaseOrderASNView_txtUnrestrictedQty").value;
        MissingQty = document.getElementById("MainContent_UC_PurchaseOrderASNView_txtMissingQty").value;
        DamagedQty = document.getElementById("MainContent_UC_PurchaseOrderASNView_txtDamagedQty").value;
        AsnBalanceQty = document.getElementById("MainContent_UC_PurchaseOrderASNView_hfAsnBalanceQty").value;
        if (UnrestrictedQty == '' || MissingQty == '' || DamagedQty == '') {
            alert("Enter All Fields");
            return false;
        }
        if (parseFloat(AsnBalanceQty) != (parseFloat(UnrestrictedQty) + parseFloat(MissingQty) + parseFloat(DamagedQty))) {
            alert("Received Qty Not match with (UnRestricted+Missing+Damage) Quantity...!");
            return false;
        }
    }
</script>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbGrCreation" runat="server" OnClick="lbActions_Click">Gr Creation</asp:LinkButton>
                <asp:LinkButton ID="lbDowloadInvoice" runat="server" OnClick="lbActions_Click">Dowload Invoice</asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">ASN Details</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Asn Number : </label>
                    <asp:Label ID="lblAsnNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <%-- <div class="col-md-12">
                    <label>PO Number : </label>
                    <asp:Label ID="lblPoNumber" runat="server" CssClass="label"></asp:Label>
                </div>--%>
                <div class="col-md-12">
                    <label>Delivery Number : </label>
                    <asp:Label ID="lblDeliveryNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gr Number : </label>
                    <asp:Label ID="lblGrNumber" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Asn Date : </label>
                    <asp:Label ID="lblAsnDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <%--<div class="col-md-12">
                    <label>PO Date : </label>
                    <asp:Label ID="lblPoDate" runat="server" CssClass="label"></asp:Label>
                </div>--%>
                <div class="col-md-12">
                    <label>Delivery Date : </label>
                    <asp:Label ID="lblDeliveryDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gr Date : </label>
                    <asp:Label ID="lblGrDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <%--<div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Vendor : </label>
                    <asp:Label ID="lblVendor" runat="server" CssClass="label"></asp:Label>
                </div>--%>
                <div class="col-md-12">
                    <label>Asn Status : </label>
                    <asp:Label ID="lblAsnStatus" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>LR Number : </label>
                    <asp:Label ID="lblLRNo" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblRemarks" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
    <asp1:TabContainer ID="tbpAsn" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
        <asp1:TabPanel ID="tpnlAsnItem" runat="server" HeaderText="Asn Item" Font-Bold="True" ToolTip="">
            <ContentTemplate>
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;"></legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvPOAsnItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAsnItemID" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItemID")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblAsnItem" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asn Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gr Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Returned Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredQty" Text='<%# DataBinder.Eval(Container.DataItem, "ReturnedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText=" Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceivedQty" Text='<%# (DataBinder.Eval(Container.DataItem, "GrItem.GrID")==null)?DataBinder.Eval(Container.DataItem, "Qty"):DataBinder.Eval(Container.DataItem, "GrItem.ReceivedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--  <asp:TemplateField HeaderText="Unrestricted Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnrestrictedQty" Text='<%# (DataBinder.Eval(Container.DataItem, "GrItem.GrID")==null)?0:DataBinder.Eval(Container.DataItem, "GrItem.UnrestrictedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Restricted Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRestrictedQty" Text='<%# (DataBinder.Eval(Container.DataItem, "GrItem.GrID")==null)?0:DataBinder.Eval(Container.DataItem, "GrItem.RestrictedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Damaged Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDamagedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.DamagedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Returned Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblReturnedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.ReturnedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Missing Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMissingQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.MissingQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Net Weight">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetWeight" Text='<%# DataBinder.Eval(Container.DataItem, "NetWeight")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Uom Weight">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUomWeight" Text='<%# DataBinder.Eval(Container.DataItem, "UomWeight")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pack Count">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPackCount" Text='<%# DataBinder.Eval(Container.DataItem, "PackCount")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Stock Type">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStockType" Text='<%# DataBinder.Eval(Container.DataItem, "StockType")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="IsChangedpart">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsChangedpart" Text='<%# DataBinder.Eval(Container.DataItem, "IsChangedpart")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>

        <asp1:TabPanel ID="tpnlGrDetails" runat="server" HeaderText="GR Details" Font-Bold="True">
            <ContentTemplate>
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;"></legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="GVGr" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Gr Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrNumber" Text='<%# DataBinder.Eval(Container.DataItem, "GrNumber")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gr Date">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrDate" Text='<%# DataBinder.Eval(Container.DataItem, "GrDate","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.AsnItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Delivered Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveredQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.DeliveredQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Received Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceivedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.ReceivedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Damaged Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDamagedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.DamagedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Returned Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblReturnedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.ReturnedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Unrestricted Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnrestrictedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.UnrestrictedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Restricted Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRestrictedQty" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.RestrictedQty")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrItemID" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.GrItemID")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click">
                                                <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "GrItem.FileName")%>' runat="server"></asp:Label>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.GrStatus")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Posted By">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPostedBy" Text='<%# DataBinder.Eval(Container.DataItem, "PostedBy.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Posted On">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPostedOn" Text='<%# DataBinder.Eval(Container.DataItem, "PostedOn","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Cancelled By">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCancelledBy" Text='<%# DataBinder.Eval(Container.DataItem, "CancelledBy.ContactName")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cancelled On">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCancelledOn" Text='<%# DataBinder.Eval(Container.DataItem, "CancelledOn","{0:d}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>

        <asp1:TabPanel ID="tpnlPO" runat="server" HeaderText="PO" Font-Bold="True">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12 field-margin-top">
                        <fieldset class="fieldset-border">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Gr Details</legend>
                            <div class="col-md-12 View">
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label>PO Number : </label>
                                        <asp:Label ID="lblPurchaseOrderNumber" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Order To : </label>
                                        <asp:Label ID="lblOrderTo" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Division : </label>
                                        <asp:Label ID="lblDivision" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Ref No : </label>
                                        <asp:Label ID="lblRefNo" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label>PO Date : </label>
                                        <asp:Label ID="lblPurchaseOrderDate" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Order Type : </label>
                                        <asp:Label ID="lblOrderType" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Receiving Location : </label>
                                        <asp:Label ID="lblReceivingLocation" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Remarks : </label>
                                        <asp:Label ID="lblPORemarks" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-md-12">
                                        <label>Dealer : </label>
                                        <asp:Label ID="lblPODealer" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Vendor : </label>
                                        <asp:Label ID="lblPOVendor" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Expected Delivery Date : </label>
                                        <asp:Label ID="lblExpectedDeliveryDate" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <label>Net Amount : </label>
                                        <asp:Label ID="lblGrossAmount" runat="server" CssClass="LabelValue"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;"></legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="GVAsnPO" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Material Code">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.POItem")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Quantity")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Price")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Discount")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Taxable Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.TaxableValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="CGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.CGST")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGSTValue">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.CGSTValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.SGST")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SGSTValue">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.SGSTValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.IGST")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IGSTValue">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.IGSTValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Tax">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTax" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Tax","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxValue" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.TaxValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetValue" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.NetAmount","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#ffffff" />
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>
    </asp1:TabContainer>
</div>
<asp:Panel ID="pnlGrCreate" runat="server" CssClass="Popup" Style="display: none; width: 70%">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Gr Creation</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageGrCreation" runat="server" Text="" CssClass="message" />
            <%--<UC:UC_GrCreate ID="UC_GrCreate" runat="server"></UC:UC_GrCreate>--%>
            <fieldset class="fieldset-border" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Gr Details</legend>
                <div class="col-md-12">
                    <div class="col-md-12 col-sm-12">
                        <label>Asn Number : </label>
                        <asp:Label ID="lblGrAsnNumber" runat="server" CssClass="label"></asp:Label>
                        <asp:Label ID="lblGrAsnID" runat="server" CssClass="label" Visible="false"></asp:Label>
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label>Remarks</label>
                        <asp:TextBox ID="txtRemarksHeader" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <asp:GridView ID="gvPOAsnGrItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                            <Columns>
                                <%--    <asp:TemplateField HeaderText="Item">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate> 
                                   <asp:Label ID="lblAsnItem" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItem")%>' runat="server"></asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAsnItemID" Text='<%# DataBinder.Eval(Container.DataItem, "AsnItemID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblAsnID" Text='<%# DataBinder.Eval(Container.DataItem, "AsnID")%>' runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Desc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asn Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "AsnBalanceQty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="Received Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceivedQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Unrestricted Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnrestrictedQty" Text='<%# DataBinder.Eval(Container.DataItem, "UnrestrictedQty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Restricted Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRestrictedQty" Text='<%# DataBinder.Eval(Container.DataItem, "RestrictedQty")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSetRestrictedQty" runat="server" OnClick="lnkSetRestrictedQty_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                        <asp:HiddenField ID="HidAsnItemID" runat="server" Visible="false" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnGrCreate" runat="server" Text="Save" CssClass="btn Save" OnClick="btnGrCreate_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_GrCreate" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlGrCreate" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />
<asp:Panel ID="pnlUpdateRestrictedQty" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Gr</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button8" runat="server" Text="X" CssClass="PopupClose" />
        </a>
    </div>

    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageRestrictedQty" runat="server" Text="" CssClass="message" />
            <asp:HiddenField ID="hfAsnBalanceQty" runat="server" />
            <fieldset class="fieldset-border" id="Fieldset7" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Gr Creation</legend>
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Unrestricted Qty<samp style="color: red">*</samp></label>
                        <asp:TextBox ID="txtUnrestrictedQty" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Missing Qty<samp style="color: red">*</samp></label>
                        <asp:TextBox ID="txtMissingQty" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Damaged Qty<samp style="color: red">*</samp></label>
                        <asp:TextBox ID="txtDamagedQty" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Number"></asp:TextBox>
                    </div>
                    <%--<div class="col-md-6 col-sm-12">
                        <label class="modal-label">Status<samp style="color: red">*</samp></label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>--%>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Remark<samp style="color: red">*</samp></label>
                        <asp:TextBox ID="txtRemarksItem" runat="server" CssClass="form-control" Rows="5" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">File</label>
                        <asp:FileUpload ID="fileUpload" runat="server" Style="position: relative;" CssClass="TextBox" ViewStateMode="Inherit" Width="200px" />
                        <%-- <asp:Button ID="btnAddFile" runat="server" CssClass="btn Approval" Text="Add" OnClick="btnAddFile_Click" />--%>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn Save" OnClick="btnAdd_Click" OnClientClick="return GrValidation();" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_UpdateRestrictedQty" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlUpdateRestrictedQty" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
