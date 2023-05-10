<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderItem.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderItem" %>

<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
         <div class="col-md-6 col-sm-12">
            <label class="modal-label">SupersedeYN</label>
             <asp:CheckBox ID="cbSupersedeYN" runat="server" Checked="true" />
        </div> 
        <div class="col-md-6 col-sm-12">
            <asp:HiddenField ID="hdfMaterialID" runat="server" />
            <asp:HiddenField ID="hdfMaterialCode" runat="server" />
            <label class="modal-label">Material</label>
            <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control"  onKeyUp="GetMaterial()"></asp:TextBox>
        </div> 
        <div class="col-md-6 col-sm-12">
            <label class="modal-label">Qty</label>
            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"  ></asp:TextBox>
        </div> 
    </div>
</fieldset>  
 <div class="col-md-12">
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">PO Item</legend>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvPOItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="2500px">
                        <Columns> 
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
                           <%-- <asp:TemplateField HeaderText="HSN">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "Material.HSN")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="UOM">
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
                                    <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "UnitPrice","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Taxable Amount">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableAmount","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="SGST">
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
                            </asp:TemplateField> --%>
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
    </div>
<script type="text/javascript">

    function GetMaterial() {        
        $("#MainContent_UC_PurchaseOrderCreate_UC_PurchaseOrderItem_hdfMaterialID").val('');
        $("#MainContent_UC_PurchaseOrderCreate_UC_PurchaseOrderItem_hdfMaterialCode").val('');
        var param = { Material: $('#MainContent_UC_PurchaseOrderCreate_UC_PurchaseOrderItem_txtMaterial').val(), MaterialType: '' }
        var Customers = [];
        if ($('#MainContent_UC_PurchaseOrderCreate_UC_PurchaseOrderItem_txtMaterial').val().trim().length >= 3) {
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
                    $('#MainContent_UC_PurchaseOrderCreate_UC_PurchaseOrderItem_txtMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) {
                            $("#MainContent_UC_PurchaseOrderCreate_UC_PurchaseOrderItem_hdfMaterialID").val(u.item.value1);
                            $("#MainContent_UC_PurchaseOrderCreate_UC_PurchaseOrderItem_hdfMaterialCode").val(u.item.MaterialCode);
                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_PurchaseOrderCreate_UC_PurchaseOrderItem_txtMaterial').width() + 48,
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
            $('#MainContent_UC_PurchaseOrderCreate_UC_PurchaseOrderItem_txtMaterial').autocomplete({
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