<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderCreate.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<style>
    .Popup {
        width: 95%;
        height: 95%;
        top: 128px;
        left: 283px;
    }

        .Popup .model-scroll {
            height: 80vh;
            overflow: auto;
        }
</style>
<asp:HiddenField ID="hdfItemCount" runat="server" Value="0" />
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbDownloadMaterialTemplate" runat="server" OnClick="lbActions_Click">Download Material Template</asp:LinkButton>
                <asp:LinkButton ID="lbUploadMaterial" runat="server" OnClick="lbActions_Click">Upload Material</asp:LinkButton>
                <asp:LinkButton ID="lbAddMaterialFromCart" runat="server" OnClick="lbActions_Click">Add Material From Cart</asp:LinkButton>
                <asp:LinkButton ID="lbCopyFromPO" runat="server" OnClick="lbActions_Click">Copy From PO</asp:LinkButton>
                <asp:LinkButton ID="lbSave" runat="server" OnClick="lbActions_Click">Save</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<div class="col-md-12">
    <fieldset class="fieldset-border">

        <div class="col-md-9">
            <div class="col-md-3 col-sm-12">
                <label class="modal-label">Dealer</label>
                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
            </div>

            <div class="col-md-3 col-sm-12">
                <label class="modal-label">Order To<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlOrderTo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOrderTo_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="1">OE</asp:ListItem>
                    <asp:ListItem Value="2">Co-Dealer</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3 col-sm-12">
                <label class="modal-label">Vendor<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3 col-sm-12">
                <label class="modal-label">Order Type<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlPurchaseOrderType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPurchaseOrderType_SelectedIndexChanged" AutoPostBack="true" />
            </div>

            <div class="col-md-3 col-sm-12">
                <label class="modal-label">Division<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3 col-sm-12">
                <label class="modal-label">
                    Receiving Location
            <samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
            </div>
            <%--<div class="col-md-3 col-sm-12">
                <label class="modal-label">Expected Delivery Date<samp style="color: red">*</samp></label>
                <asp:TextBox ID="txtExpectedDeliveryDate" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoCompleteType="Disabled"></asp:TextBox>
                <asp1:CalendarExtender ID="cxExpectedDeliveryDate" runat="server" TargetControlID="txtExpectedDeliveryDate" PopupButtonID="txtExpectedDeliveryDate" Format="dd/MM/yyyy" />
                <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtExpectedDeliveryDate" WatermarkText="DD/MM/YYYY" />
            </div>--%>
            <div class="col-md-3 col-sm-12">
                <label class="modal-label">Ref. No</label>
                <asp:TextBox ID="txtReferenceNo" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="col-md-4 col-sm-12">
                <label class="modal-label">Remarks</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-3">
          <%--  <div class="col-md-12">
                <label>Price : </label>
                <asp:Label ID="lblPrice" runat="server" CssClass="label"></asp:Label>
            </div>--%>
            <div class="col-md-12">
                <label>Discount : </label>
                <asp:Label ID="lblDiscount" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label>Taxable Amount : </label>
                <asp:Label ID="lblTaxableAmount" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label>Tax Amount : </label>
                <asp:Label ID="lblTaxAmount" runat="server" CssClass="label"></asp:Label>
            </div>
            <div class="col-md-12">
                <label>Gross Amount : </label>
                <asp:Label ID="lblGrossAmount" runat="server" CssClass="label"></asp:Label>
            </div>


        </div>
    </fieldset>

    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Material Add</legend>
        <div class="col-md-12 Report">
            <div class="col-md-12">
                <%--  <div class="col-md-2 col-sm-12">
                        <label class="modal-label">SupersedeYN</label>
                        <asp:CheckBox ID="cbSupersedeYN" runat="server" Checked="true" />
                    </div>--%>
                <div class="col-md-3 col-sm-12">
                    <asp:HiddenField ID="hdfMaterialID" runat="server" />
                    <asp:HiddenField ID="hdfMaterialCode" runat="server" />
                    <label class="modal-label">Material</label>
                    <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" onKeyUp="GetMaterial()"></asp:TextBox>
                </div>
                <div class="col-md-1 col-sm-12">
                    <label class="modal-label">Qty</label>
                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4 text-left">
                    <label class="modal-label">.</label>
                    <asp:Button ID="btnAddMaterial" runat="server" Text="Add" CssClass="btn Search" OnClick="btnAddMaterial_Click" />

                    <asp:Button ID="Btn_MatAvailability" runat="server" Text="Availability" CssClass="btn Save" OnClick="Btn_MatAvailability_Click" Visible="false" />
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">PO Item</legend>
        <div class="col-md-12 Report">
            <asp:GridView ID="gvPOItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                <Columns>
                    <asp:TemplateField HeaderText="Item">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblItem" Text='<%# DataBinder.Eval(Container.DataItem, "Item")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Desc">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="HSN">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "HSN")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Order Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblf_uom" Text='<%# DataBinder.Eval(Container.DataItem, "UOM")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Price">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "UnitPrice","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Price","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblDiscountAmount" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Taxable Amount">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblTaxableAmount" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableAmount","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="SGST">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "SGST","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CGST">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "CGST","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IGST">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "IGST","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Tax">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblTax" Text='<%# DataBinder.Eval(Container.DataItem, "Tax","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tax Value">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblTaxValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxValue","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Net Amount">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "NetValue","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnPoItemDelete" runat="server" OnClick="lnkBtnPoItemDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="#ffffff" />
                <FooterStyle ForeColor="White" />
                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <RowStyle BackColor="#fbfcfd" ForeColor="Black" />
            </asp:GridView>
        </div>
    </fieldset>
</div>


<asp:Panel ID="pnlMaterialFromCart" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Material From Cart</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button12" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <asp:Label ID="lblMessageMaterialFromCart" runat="server" Text="" CssClass="message" />
            <fieldset class="fieldset-border">
                <div class="col-md-12">
                    <div class="col-md-12 Report">
                        <div class="table-responsive">
                            <asp:GridView ID="gvMaterialFromCart" ShowHeader="true" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvMaterialFromCart_RowDataBound" Width="100%" CssClass="table table-bordered table-condensed Grid" EmptyDataText="No Data Found">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="javascript:collapseExpand('OrderNo-<%# Eval("OrderNo") %>');">
                                                <img id="imageDeliveryNumber-<%# Eval("OrderNo") %>" alt="Click to show/hide orders" border="0" src="../Images/grid_expand.png" height="10" width="10" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSelect" runat="server" onchange="OnchangeHandler(this);" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dealer Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "DealerCode")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderNo" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNo")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate")%>' runat="server" />

                                            <tr>
                                                <td colspan="100%" style="padding-left: 96px">
                                                    <div id="OrderNo-<%# Eval("OrderNo") %>" style="display: none; position: relative;">
                                                        <asp:GridView ID="gvMaterialFromCartItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbSelectChild" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "PartNo")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material Desc">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "PartDescription")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPartQty" Text='<%# DataBinder.Eval(Container.DataItem, "PartQty")%>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField HeaderText="SAC/HSN Code">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIRate" Text='<%# DataBinder.Eval(Container.DataItem, "Rate","{0:n}")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Value">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "Value","{0:n}")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Discount">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Discount","{0:n}")%>' runat="server"></asp:Label>
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
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle ForeColor="White" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <RowStyle BackColor="#FBFCFD" ForeColor="Black" HorizontalAlign="Left" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnMaterialFromCart" runat="server" Text="Add" CssClass="btn Save" OnClick="btnMaterialFromCart_Click" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_MaterialFromCart" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlMaterialFromCart" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<asp:Panel ID="pnlCopyOrder" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Copy From PO</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button1" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMessageCopyOrder" runat="server" Text="" CssClass="message" />
        <fieldset class="fieldset-border">
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Po Number</label>
                    <asp:TextBox ID="txtPoNumber" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearchCopyOrder" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearchCopyOrder_Click" OnClientClick="return dateValidation();" Width="65px" />
                    <asp:Button ID="btnCopyPoAdd" runat="server" Text="Add" CssClass="btn Save" OnClick="btnCopyPoAdd_Click" Visible="false" />
                </div>
            </div>
        </fieldset>

    </div>
    <div class="col-md-12">
        <div class="model-scroll">
            <fieldset class="fieldset-border">
                <div class="col-md-12">
                    <div class="col-md-12 Report">
                        <div class="table-responsive">
                            <asp:GridView ID="gvMaterialCopyOrder" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="ChkMailH" Text="Select All" runat="server" AutoPostBack="true" OnCheckedChanged="ChkMailH_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSelectChild" runat="server" AutoPostBack="true" OnCheckedChanged="cbSelectChild_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ControlStyle-Width="100px">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartQty" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server" Visible="false" />
                                            <asp:TextBox ID="txtPartQty" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'></asp:TextBox>
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
                    </div>

                </div>
            </fieldset>
        </div>
        <div class="col-md-12 text-center">
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_CopyOrder" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCopyOrder" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlMaterialUpload" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Material Upload</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>

    <div class="col-md-12">
        <asp:Label ID="lblMessageMaterialUpload" runat="server" Text="" CssClass="message" />
        <fieldset class="fieldset-border">
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Upload Material</label>
                    <asp:FileUpload ID="fileUpload" runat="server" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnUploadMaterial" runat="server" Text="Add" CssClass="btn Save" OnClick="btnUploadMaterial_Click" />
                </div>
            </div>
        </fieldset>
    </div>



</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_MaterialUpload" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlMaterialUpload" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlSupersede" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Material Status</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button2" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>

    <div class="col-md-12">
        <fieldset class="fieldset-border">
            <div class="col-md-12">
                <asp:GridView ID="gvMaterialIssue" runat="server" CssClass="table table-bordered table-condensed Grid">
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>

                <asp:GridView ID="gvSupersede" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                    <Columns>
                        <asp:TemplateField HeaderText="Material">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supersede">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblSupersede" Text='<%# DataBinder.Eval(Container.DataItem, "Supersede.Material")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
                <div class="col-md-12 text-center">
                    <asp:Button ID="Button4" runat="server" Text="Continue" CssClass="btn Save" />
                </div>
            </div>
        </fieldset>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Supersede" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlSupersede" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>


<script type="text/javascript">

    function GetMaterial() {
        $("#MainContent_UC_PurchaseOrderCreate_hdfMaterialID").val('');
        $("#MainContent_UC_PurchaseOrderCreate_hdfMaterialCode").val('');
        var param = {
            Material: $('#MainContent_UC_PurchaseOrderCreate_txtMaterial').val(),
            MaterialType: '',
            DivisionID: $('#MainContent_UC_PurchaseOrderCreate_ddlDivision').val(),
            ItemCount: $('#MainContent_UC_PurchaseOrderCreate_hdfItemCount').val()
        }
        var Customers = [];
        if ($('#MainContent_UC_PurchaseOrderCreate_txtMaterial').val().trim().length >= 3) {
            $.ajax({
                url: "PurchaseOrder.aspx/GetMaterial",
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                data: JSON.stringify(param),
                dataType: 'JSON',
                success: function (data) {
                    var DataList = JSON.parse(data.d);
                    for (i = 0; i < DataList.length; i++) {
                        Customers[i] = {
                            value: DataList[i].MaterialCode + ' ' + DataList[i].MaterialDescription,
                            value1: DataList[i].MaterialID,
                            MaterialCode: DataList[i].MaterialCode
                        };
                    }
                    $('#MainContent_UC_PurchaseOrderCreate_txtMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) {
                            $("#MainContent_UC_PurchaseOrderCreate_hdfMaterialID").val(u.item.value1);
                            $("#MainContent_UC_PurchaseOrderCreate_hdfMaterialCode").val(u.item.MaterialCode);
                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_PurchaseOrderCreate_txtMaterial').width() + 48,
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
            $('#MainContent_UC_PurchaseOrderCreate_txtMaterial').autocomplete({
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
<script type="text/javascript">
    function collapseExpand(obj) {
        var gvObject = document.getElementById(obj);
        var imageID = document.getElementById('image' + obj);

        if (gvObject.style.display == "none") {
            gvObject.style.display = "inline";
            imageID.src = "../Images/grid_collapse.png";
        }
        else {
            gvObject.style.display = "none";
            imageID.src = "../Images/grid_expand.png";
        }
    }


    function OnchangeHandler(txtid) {

        var index = txtid.parentNode.parentNode.sectionRowIndex; // Get the corresponding index 
        index = (index + 1) / 2;
        index = index - 1;
        var checkBox = document.getElementById('MainContent_UC_PurchaseOrderCreate_gvMaterialFromCart_cbSelect_' + index);
        var gvMaterialFromCartItem = document.getElementById('MainContent_UC_PurchaseOrderCreate_gvMaterialFromCart_gvMaterialFromCartItem_' + index);
        for (i = 0; i < gvMaterialFromCartItem.rows.length; i++) {
            var cbSelectChild = document.getElementById('MainContent_UC_PurchaseOrderCreate_gvMaterialFromCart_gvMaterialFromCartItem_' + index + '_cbSelectChild_' + i);
            if (checkBox.checked) {
                cbSelectChild.checked = true;
            }
            else {
                cbSelectChild.checked = false;
            }

        }
        //var grid = txtid.parentNode.parentNode.parentNode;
        // var checkBox = grid.rows[index].cells[1].getElementsByTagName("INPUT")[0];
        //  var gvMaterialFromCartItem = grid.rows[index].cells[5].getElementsByTagName("INPUT")[0];


    }
</script>
