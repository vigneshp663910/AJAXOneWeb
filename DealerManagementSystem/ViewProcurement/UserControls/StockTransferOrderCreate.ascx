<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StockTransferOrderCreate.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.StockTransferOrderCreate" %>
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

<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbUploadMaterial" runat="server" OnClick="lbActions_Click">Upload Material</asp:LinkButton>
                <asp:LinkButton ID="lbDownloadMaterialTemplate" runat="server" OnClick="lbActions_Click">Download Material Template</asp:LinkButton>
                <asp:LinkButton ID="lbSave" runat="server" OnClick="lbActions_Click">Save</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message"   />
<div class="col-md-12">
    <fieldset class="fieldset-border">
        <div class="col-md-9">
            <div class="col-md-3 col-sm-12">
                <label class="modal-label">Dealer</label>
                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
            </div>
            <div class="col-md-3 col-sm-12">
                <label class="modal-label">
                    Receiving Location
            <samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlDestinationOffice" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3 col-sm-12">
                <label class="modal-label">
                    Source Location
            <samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlSourceOffice" runat="server" CssClass="form-control" />
            </div> 
            <div class="col-md-4 col-sm-12">
                <label class="modal-label">Remarks</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
        </div> 
         <div class="col-md-3"> 
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
    <div class="col-md-12">
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                <div class="col-md-12"> 
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

                        <asp:Button ID="btnAvailability" runat="server" Text="Availability" CssClass="btn Save" OnClick="btnAvailability_Click" />
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
                            <asp:TemplateField HeaderText="Order Qty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Taxable Value">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CGST">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "CGST","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="SGST">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "SGST","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="IGST">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "IGST","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                   <asp:TemplateField HeaderText="CGST Value">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "CGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="SGST Value">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "SGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="IGST Value">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "IGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="UOM">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblf_uom" Text='<%# DataBinder.Eval(Container.DataItem, "UOM")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>  --%>
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
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
</div>

<asp:Panel ID="pnlMaterialUpload" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Material Upload</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="Button3" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>

    <div class="col-md-12">
        <asp:Label ID="lblMessageMaterialUpload" runat="server" Text="" CssClass="message"  />
        <fieldset class="fieldset-border">
            <div class="col-md-12">
                <div class="col-md-2 col-sm-12">
                    <label class="modal-label">Upload Material</label>
                    <asp:FileUpload ID="fileUpload" runat="server" />
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnUploadMaterial" runat="server" Text="Submit" CssClass="btn Save" OnClick="btnUploadMaterial_Click" />
                </div>
            </div>
        </fieldset>
    </div>



</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_MaterialUpload" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlMaterialUpload" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>


<script type="text/javascript">

    function GetMaterial() {
        $("#MainContent_UC_StockTransferOrderCreate_hdfMaterialID").val('');
        $("#MainContent_UC_StockTransferOrderCreate_hdfMaterialCode").val('');
        $("#MainContent_UC_StockTransferOrderCreate_hdfMaterialCode").val('');
        var param = { Material: $('#MainContent_UC_StockTransferOrderCreate_txtMaterial').val(), MaterialType: '' }
        var Customers = [];
        if ($('#MainContent_UC_StockTransferOrderCreate_txtMaterial').val().trim().length >= 3) {
            $.ajax({
                url: "StockTransferOrder.aspx/GetMaterial",
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
                    $('#MainContent_UC_StockTransferOrderCreate_txtMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) {
                            $("#MainContent_UC_StockTransferOrderCreate_hdfMaterialID").val(u.item.value1);
                            $("#MainContent_UC_StockTransferOrderCreate_hdfMaterialCode").val(u.item.MaterialCode);
                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_StockTransferOrderCreate_txtMaterial').width() + 48,
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
            $('#MainContent_UC_StockTransferOrderCreate_txtMaterial').autocomplete({
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
