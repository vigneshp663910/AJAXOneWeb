<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderView.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SalesOrderView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewSales/UserControls/SalesOrderCreate.ascx" TagPrefix="UC" TagName="UC_SalesOrderEdit" %>
<%@ Register Src="~/ViewMaster/UserControls/CustomerViewHeader.ascx" TagPrefix="UC" TagName="UC_CustomerView" %>
<style>
    .portlet.box.green {
        border: 1px solid #5cd1db;
        border-top: 0;
    }

        .portlet.box.green > .portlet-title {
            background-color: #32c5d2;
        }

            .portlet.box.green > .portlet-title > .caption {
                color: #fff;
            }

    .pull-right {
        float: right !important;
    }

    .btn:not(.md-skip):not(.bs-select-all):not(.bs-deselect-all).btn-sm {
        font-size: 11px;
        padding: 6px 18px 6px 18px;
    }

    .btn.yellow:not(.btn-outline) {
        color: #fff;
        background-color: #c49f47;
        border-color: #c49f47;
    }

    .form-group {
        margin-bottom: 5px;
    }

    b, optgroup, strong {
        font-weight: 700;
    }
</style>
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
<script>
    function GetCustomers() {
        $("#MainContent_UC_SalesOrderView_hdfCustomerId").val('');
        var param = { CustS: $('#MainContent_UC_SalesOrderView_txtCustomer').val() };
        var Customers = [];
        if ($('#MainContent_UC_SalesOrderView_txtCustomer').val().trim().length >= 3) {
            $.ajax({
                url: 'SaleOrder.aspx/GetCustomer',
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                data: JSON.stringify(param),
                dataType: 'JSON',
                success: function (data) {
                    var DataList = JSON.parse(data.d);
                    if (DataList != null && DataList.length > 0) {
                        for (i = 0; i < DataList.length; i++) {
                            Customers[i] = {
                                value: DataList[i].CustomerName,
                                value1: DataList[i].CustomerID,
                                ContactPerson: DataList[i].ContactPerson,
                                Mobile: DataList[i].Mobile,
                                CustomerType: DataList[i].CustomerType,
                                Address: DataList[i].Address1
                            };
                        }
                    }
                    $('#MainContent_UC_SalesOrderView_txtCustomer').autocomplete({
                        source: function (request, response) {
                            response(Customers)
                        },
                        select: function (e, u) {
                            $("#MainContent_UC_SalesOrderView_hdfCustomerId").val(u.item.value1);
                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_SalesOrderView_txtCustomer').width() + 48,
                            });
                            $(this).autocomplete("widget").scrollTop(0);
                        }
                    }).focus(function (e) {
                        $(this).autocomplete("search");
                    }).click(function () {
                        $(this).autocomplete("search");
                    }).data('ui-autocomplete')._renderItem = function (ul, item) {
                        var inner_html = FormatAutocompleteList(item);
                        return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;"></li>')
                            .data('item.autocomplete', item)
                            .append(inner_html)
                            .appendTo(ul);
                    };

                }
            });
        }
        else {
            $('#MainContent_UC_SalesOrderView_txtCustomer').autocomplete({
                source: function (request, response) {
                    response($.ui.autocomplete.filter(Customers, ""))
                }
            });
        }
    }

    function FormatAutocompleteList(item) {

        var inner_html = '<a class="customer">';
        inner_html += '<p class="customer-name-info"><label>' + item.value + '</label></p>';
        inner_html += '<div class=customer-info><label class="contact-number">Contact :' + item.ContactPerson + '(' + item.Mobile + ') </label>';
        inner_html += '<label class="customer-type">' + item.CustomerType + '</label></div>';
        inner_html += '<p class="customer-address"><label>' + item.Address + '</label></p>';
        inner_html += '</a>';
        return inner_html;
    }


</script>
<script type="text/javascript">
    function GetMaterial() {
        $("#MainContent_UC_SalesOrderView_hdfMaterialID").val('');
        $("#MainContent_UC_SalesOrderView_hdfMaterialCode").val('');
        var param = { Material: $('#MainContent_UC_SalesOrderView_txtMaterial').val(), MaterialType: '', DivisionID: '15' }
        var Customers = [];
        if ($('#MainContent_UC_SalesOrderView_txtMaterial').val().trim().length >= 3) {
            $.ajax({
                url: 'SaleOrder.aspx/GetMaterial',
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
                    $('#MainContent_UC_SalesOrderView_txtMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) {
                            $("#MainContent_UC_SalesOrderView_hdfMaterialID").val(u.item.value1);
                            $("#MainContent_UC_SalesOrderView_hdfMaterialCode").val(u.item.MaterialCode);
                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_SalesOrderView_txtMaterial').width() + 48,
                            });
                            $(this).autocomplete("widget").scrollTop(0);
                        }
                    }).focus(function (e) {
                        $(this).autocomplete("search");
                    }).click(function () {
                        $(this).autocomplete("search");
                    }).data('ui-autocomplete')._renderItem = function (ul, item) {

                        var inner_html = FormatAutocompleteListMaterial(item);
                        return $('<li class="" style="padding:5px 5px 20px 5px;border-bottom:1px solid #82949a;  z-index: 10002"></li>')
                            .data('item.autocomplete', item)
                            .append(inner_html)
                            .appendTo(ul);
                    };
                }
            });
        }
        else {
            $('#MainContent_UC_SalesOrderView_txtMaterial').autocomplete({
                source: function (request, response) {
                    response($.ui.autocomplete.filter(Customers, ""))
                }
            });
        }
    }

    function FormatAutocompleteListMaterial(item) {
        var inner_html = '<a>';
        inner_html += '<p style="margin:0;"><strong>' + item.value + '</strong></p>';
        inner_html += '</a>';
        return inner_html;
    }
</script>
<script type="text/javascript">
    function ConfirmCancelSaleOrder() {
        var x = confirm("Are you sure you want to Cancel?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmItemUpdate() {
        var x = confirm("Are you sure you want to Update?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmItemDelete() {
        var x = confirm("Are you sure you want to Delete?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmItemDeleteDelivery() {
        var x = confirm("Are you sure you want to Delete?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmReleaseSaleOrder() {
        var x = confirm("Are you sure you want to Release?");
        if (x) {
            return true;
        }
        else
            return false;
    }
    function ConfirmSaleOrderDelivery() {
        var x = confirm("Are you sure you want to Delivery?");
        if (x) {
            return true;
        }
        else
            return false;
    }
</script>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <asp:LinkButton ID="lbEditSaleOrder" runat="server" OnClick="lbActions_Click">Edit</asp:LinkButton>
                <asp:LinkButton ID="lbCancelSaleOrder" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmCancelSaleOrder();">Cancel</asp:LinkButton>
                <asp:LinkButton ID="lbAddSaleOrderItem" runat="server" OnClick="lbActions_Click">Add Item</asp:LinkButton>
                <%--<asp:LinkButton ID="lbGenerateQuotation" runat="server" OnClick="lbActions_Click">Generate Quotation</asp:LinkButton>--%>
                <asp:LinkButton ID="lbGenerateProformaInvoice" runat="server" OnClick="lbActions_Click">Generate Proforma Invoice</asp:LinkButton>
                <asp:LinkButton ID="lbReleaseSaleOrder" runat="server" OnClick="lbActions_Click" OnClientClick="return ConfirmReleaseSaleOrder();">Release</asp:LinkButton>
                <asp:LinkButton ID="lbDelivery" runat="server" OnClick="lbActions_Click">Create Delivery</asp:LinkButton>
                <asp:LinkButton ID="lbPreviewQuotation" runat="server" OnClick="lbActions_Click">Preview Quotation</asp:LinkButton>
                <asp:LinkButton ID="lbDownloadQuotation" runat="server" OnClick="lbActions_Click">Download Quotation</asp:LinkButton>
                <asp:LinkButton ID="lbPreviewProformaInvoice" runat="server" OnClick="lbActions_Click">Preview Proforma Invoice</asp:LinkButton>
                <asp:LinkButton ID="lbDownloadProformaInvoice" runat="server" OnClick="lbActions_Click">Download Proforma Invoice</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div class="col-md-12 field-margin-top">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Sale Order</legend>
        <%--<div class="col-md-12 View">
            <div class="col-md-4">
               
                
                <div class="col-md-12">
                    <label>Remarks : </label>
                    <asp:Label ID="lblRemarks" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Ref Number : </label>
                    <asp:Label ID="lblRefNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Frieght PaidBy : </label>
                    <asp:Label ID="lblFrieghtPaidBy" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Select Tax : </label>
                    <asp:Label ID="lblTaxType" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Header Discount Percent : </label>
                    <asp:Label ID="lblHeaderDiscount" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Sale Order Date : </label>
                    <asp:Label ID="lblSaleOrderDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Customer : </label>
                    <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Contact Person Number : </label>
                    <asp:Label ID="lblContactPersonNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Expected Delivery Date : </label>
                    <asp:Label ID="lblExpectedDeliveryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Ref Date : </label>
                    <asp:Label ID="lblRefDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Attn : </label>
                    <asp:Label ID="lblAttn" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Sale Order Type : </label>
                    <asp:Label ID="lblSaleOrderType" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblSODealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Division : </label>
                    <asp:Label ID="lblDivision" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Product : </label>
                    <asp:Label ID="lblProduct" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Insurance Paid By : </label>
                    <asp:Label ID="lblInsurancePaidBy" runat="server" CssClass="label"></asp:Label>
                </div>
                
                <div class="col-md-12">
                    <label>Sales Engnieer : </label>
                    <asp:Label ID="lblSalesEngnieer" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </div>--%>
        <div class="col-md-12 View">
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>Quot No : </label>
                    <asp:Label ID="lblQuotationNumber" runat="server" CssClass="LabelValue"></asp:Label>

                </div>
                <div class="col-md-12">
                    <label>Quot Date : </label>
                    <asp:Label ID="lblQuotationDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>SO No : </label>
                    <asp:Label ID="lblSaleOrderNumber" runat="server" CssClass="LabelValue"></asp:Label>

                </div>
                <div class="col-md-12">
                    <label>SO Date : </label>
                    <asp:Label ID="lblSaleOrderDate" runat="server" CssClass="LabelValue"></asp:Label>
                </div>





            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblSODealer" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dealer Office : </label>
                    <asp:Label ID="lblDealerOffice" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

                <%--<div class="col-md-12">
                    <label>Customer : </label>
                    <asp:Label ID="lblCustomer" runat="server" CssClass="LabelValue"></asp:Label>
                </div>--%>
                <div class="col-md-12">
                    <label>Status : </label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="LabelValue"></asp:Label>
                </div>

                <div class="col-md-12">
                    <label>Order Type : </label>
                    <asp:Label ID="lblSaleOrderType" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>TCS Tax : </label>
                    <asp:Label ID="lblTcsTax" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Tax : </label>
                    <asp:Label ID="lblTaxType" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Header Discount% : </label>
                    <asp:Label ID="lblHeaderDiscount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Freight : </label>
                    <asp:Label ID="lblFreight" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Packing & Forward : </label>
                    <asp:Label ID="lblPackingAndForward" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
            <div class="col-md-3">
                <div class="col-md-12">
                    <label>TCS Value : </label>
                    <asp:Label ID="lblTcsValue" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Discount Amount : </label>
                    <asp:Label ID="lblDiscount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <%--<div class="col-md-12">
                    <label>Discount : </label>
                    <asp:Label ID="lblDiscount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>--%>
                <div class="col-md-12">
                    <label>Taxable Amount : </label>
                    <asp:Label ID="lblTaxableValue" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Tax Amount : </label>
                    <asp:Label ID="lblTaxValue" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Gross Amount : </label>
                    <asp:Label ID="lblNetAmount" runat="server" CssClass="LabelValue"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>
</div>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" />
<asp1:TabContainer ID="tbpContainer" runat="server" Font-Bold="True" Font-Size="Medium" ActiveTabIndex="0">
    <asp1:TabPanel ID="tbPSODetails" runat="server" HeaderText="SO Header" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">SO Header</legend>
                    <div class="col-md-12 View">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Pro Inv No : </label>
                                <asp:Label ID="lblProformaInvoiceNumber" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Pro Inv Date : </label>
                                <asp:Label ID="lblProformaInvoiceDate" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Attn : </label>
                                <asp:Label ID="lblAttn" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>

                            <div class="col-md-12">
                                <label>Sales Engnieer : </label>
                                <asp:Label ID="lblSalesEngnieer" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Equipment Serial No. : </label>
                                <asp:Label ID="lblEquipmentSerialNo" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Division : </label>
                                <asp:Label ID="lblDivision" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Product : </label>
                                <asp:Label ID="lblProduct" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Created By : </label>
                                <asp:Label ID="lblSoCreatedBy" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <label>Insurance Paid By : </label>
                                <asp:Label ID="lblInsurancePaidBy" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>

                            <div class="col-md-12">
                                <label>Frieght Paid By : </label>
                                <asp:Label ID="lblFrieghtPaidBy" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Remarks : </label>
                                <asp:Label ID="lblRemarks" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>

                            <div class="col-md-12">
                                <label>Ref Number : </label>
                                <asp:Label ID="lblRefNumber" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Ref Date : </label>
                                <asp:Label ID="lblRefDate" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Sales Type : </label>
                                <asp:Label ID="lblSalesType" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Contact No : </label>
                                <asp:Label ID="lblContactPersonNumber" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <label>Exp Delivery Date : </label>
                                <asp:Label ID="lblExpectedDeliveryDate" runat="server" CssClass="LabelValue"></asp:Label>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp1:TabPanel>
    <asp1:TabPanel ID="tpnlSOItem" runat="server" HeaderText="SO Item" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">SO Item</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvSOItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialCode")%>' runat="server"></asp:Label>
                                            <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialID")%>' runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblSaleOrderItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItemID")%>' runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "Material.MaterialDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HSN Code">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "Material.HSN")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' runat="server"></asp:Label>
                                            <asp:TextBox ID="txtBoxQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' CssClass="form-control" Visible="false" Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblBaseUnit" Text='<%# DataBinder.Eval(Container.DataItem, "Material.BaseUnit")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Unit Price">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "PerRate","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "Value","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Discount Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemDiscountValue" Text='<%# DataBinder.Eval(Container.DataItem, "ItemDiscountValue","{0:n}")%>' runat="server"></asp:Label>
                                            <asp:Panel ID="pnlItemDiscount" runat="server" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td>Percentage</td>
                                                        <td>Value</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtItemDiscountPercentage" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtItemDiscountValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemDiscountValue","{0:n}")%>' CssClass="form-control" Width="100px"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscountValue" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="CGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "Material.CGST")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="CGSTValue">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "Material.CGSTValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="SGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "Material.SGST")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="SGSTValue">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "Material.SGSTValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="IGST">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "Material.IGST")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="IGSTValue">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "Material.IGSTValue")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Taxable Amount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTax" Text='<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.SGST")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.CGST")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.IGST"))).ToString("N2") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxValue" Text='<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.CGSTValue")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.SGSTValue")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Material.IGSTValue"))).ToString("N2") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- <asp:TemplateField HeaderText="Discount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Discounted Price">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscountedPrice" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountedPrice","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Tax">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTax" Text='<%# DataBinder.Eval(Container.DataItem, "Tax","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Tax Value">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxValue","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseOrderStatus" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderStatus.PurchaseOrderStatus","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Button ID="btnCancelPoItem" runat="server" Text="Cancel" CssClass="btn Back" OnClick="btnCancelPoItem_Click" Width="75px" Height="25px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Net Amount">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmount","{0:n}")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalanceQuantity" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Quantity")) - Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "DeliveredQuantity"))%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnItemAction_Click"><i class="fa fa-fw fa-edit" style="font-size:18px"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkBtnUpdate" runat="server" OnClick="lnkBtnItemAction_Click" Visible="false" OnClientClick="return ConfirmItemUpdate();"><i class='fa fa-fw fa-refresh' style='font-size:18px'></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkBtnCancel" runat="server" OnClick="lnkBtnItemAction_Click" Visible="false"> <i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
                                            <%--<asp:LinkButton ID="lblMaterialRemove" runat="server" OnClick="lblMaterialRemove_Click"><i class="fa fa-fw fa-times" style="font-size:18px"  ></i></asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="lnkBtnItemAction_Click" OnClientClick="return ConfirmItemDelete();"><i class="fa fa-fw fa-times" style="font-size:18px" ></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkBtnStockAvailability" runat="server" OnClick="lnkBtnItemAction_Click">Availability</asp:LinkButton>
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
        </ContentTemplate>
    </asp1:TabPanel>
    <asp:TabPanel ID="TabCustomer" runat="server" HeaderText="Customer">
        <ContentTemplate>
            <div class="col-md-12 field-margin-top">
                <UC:UC_CustomerView ID="UC_CustomerView" runat="server"></UC:UC_CustomerView>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp1:TabPanel ID="TabPanel1" runat="server" HeaderText="SO Delivery" Font-Bold="True" ToolTip="">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">SO Delivery</legend>
                        <div class="col-md-12 Report">
                            <asp:GridView ID="gvSODelivery" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                                <Columns>
                                    <asp:TemplateField HeaderText="Delivery Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryNumber" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryNumber")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delivery Date">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryDate")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Date">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Desc">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Qty","{0:n}")%>' runat="server"></asp:Label>
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
        </ContentTemplate>
    </asp1:TabPanel>
</asp1:TabContainer>

<asp:Panel ID="pnlSaleOrderEdit" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Edit</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="PopupClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageSOEdit" runat="server" Text="" CssClass="message" />
                <fieldset class="fieldset-border" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Edit</legend>
                    <div class="col-md-12">
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Dealer Office<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Contact Person Number</label>
                            <asp:TextBox ID="txtContactPersonNumber" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtContactPersonNumber" WatermarkText="Mobile" WatermarkCssClass="WatermarkCssClass" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label class="modal-label">Product<samp style="color: red">*</samp></label>
                            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Expected Delivery Date<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtExpectedDeliveryDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="cxExpectedDeliveryDate" runat="server" TargetControlID="txtExpectedDeliveryDate" PopupButtonID="txtExpectedDeliveryDate" Format="dd/MM/yyyy" />
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtExpectedDeliveryDate" WatermarkText="DD/MM/YYYY" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Insurance Paid By</label>
                            <asp:DropDownList ID="ddlInsurancePaidBy" runat="server" CssClass="form-control" BorderColor="Silver">
                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="1">Seller</asp:ListItem>
                                <asp:ListItem Value="2">Buyer</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Frieght Paid By</label>
                            <asp:DropDownList ID="ddlFrieghtPaidBy" runat="server" CssClass="form-control" BorderColor="Silver">
                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="1">Seller</asp:ListItem>
                                <asp:ListItem Value="2">Buyer</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Attn</label>
                            <asp:TextBox ID="txtAttn" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Tax</label>
                            <asp:DropDownList ID="ddlTaxType" runat="server" CssClass="form-control" BorderColor="Silver">
                                <asp:ListItem Value="1">SGST & CGST</asp:ListItem>
                                <asp:ListItem Value="2">IGST</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Sales Type</label>
                            <asp:DropDownList ID="ddlSalesType" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Sales Engineer</label>
                            <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Remarks</label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Header Discount %</label>
                            <asp:TextBox ID="txtBoxHeaderDiscountPercent" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Ref Number</label>
                            <asp:TextBox ID="txtRefNumber" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <label>Ref Date<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtRefDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRefDate" PopupButtonID="txtRefDate" Format="dd/MM/yyyy" />
                            <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtRefDate" WatermarkText="DD/MM/YYYY" />
                        </div>

                        <div class="col-md-6 col-sm-12"  style="display:none">
                            <label class="modal-label">Freight</label>
                            <asp:TextBox ID="txtFreight" runat="server" CssClass="form-control" BorderColor="Silver" Text="0"></asp:TextBox>
                        </div>
                        <div class="col-md-6 col-sm-12"  style="display:none">
                            <label class="modal-label">Packing & Forward</label>
                            <asp:TextBox ID="txtPackingAndForward" runat="server" CssClass="form-control" BorderColor="Silver" Text="0"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
        <div class="col-md-12 text-center">
            <asp:Button ID="btnUpdateSO" runat="server" CssClass="btn Save" Text="Update" OnClick="btnUpdateSO_Click" Width="100px"></asp:Button>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_SaleOrderEdit" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlSaleOrderEdit" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlSaleOrderItemAdd" runat="server" CssClass="Popup" Style="display: none;">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogue">Add Item</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="PopupItemClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <div class="col-md-12">
        <div class="col-md-12">
            <div class="model-scroll">
                <asp:Label ID="lblMessageAddSOItem" runat="server" Text="" CssClass="message" />
                <fieldset class="fieldset-border" runat="server">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Add Item</legend>
                    <div class="col-md-12">

                        <div class="col-md-6 col-sm-12">
                            <asp:HiddenField ID="hdfMaterialID" runat="server" />
                            <asp:HiddenField ID="hdfMaterialCode" runat="server" />
                            <label class="modal-label">Material<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" onKeyUp="GetMaterial()"></asp:TextBox>
                        </div>
                        <div class="col-md-3 col-sm-12">
                            <label class="modal-label">Qty<samp style="color: red">*</samp></label>
                            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSaveSOItem" runat="server" CssClass="btn Save" Text="Save" OnClick="btnSaveSOItem_Click" Width="100px"></asp:Button>
            </div>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_SaleOrderItemAdd" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlSaleOrderItemAdd" BackgroundCssClass="modalBackground" CancelControlID="btnCancel" />

<asp:Panel ID="pnlCreateSODelivery" runat="server" CssClass="Popup" Style="display: none">
    <div class="PopupHeader clearfix">
        <span id="PopupDialogueCreateSODelivery">Delivery</span><a href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button">
            <asp:Button ID="PopupDeliveryClose" runat="server" Text="X" CssClass="PopupClose" /></a>
    </div>
    <asp:Label ID="lblMessageCreateSODelivery" runat="server" Text="" CssClass="message" />
    <div class="col-md-12">
        <div class="model-scroll">

            <div class="col-md-12">
                <div class="col-md-3">
                    <div class="col-md-6 col-sm-12">
                        <label>Billing Address </label>
                        <br />
                        <asp:Label ID="lblBillingAddress" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>

                </div>
                <div class="col-md-4">
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">
                            Payment Mode
                            <samp style="color: red">*</samp></label>
                        <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control" />
                    </div>

                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">Shift Address<samp style="color: red">*</samp></label>
                        <asp:DropDownList ID="ddlShiftTo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlShiftTo_SelectedIndexChanged" />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-12 col-sm-12" id="divEquipment" runat="server" visible="false">
                        <label>Equipment Serial No</label>
                        <asp:DropDownList ID="ddlEquipment" runat="server" CssClass="form-control" />
                    </div>
                    <%-- <div class="col-md-5">
                        <label>Delivery Address</label>
                        <br />
                        <asp:Label ID="lblDeliveryAddress" runat="server" CssClass="LabelValue"></asp:Label>
                    </div>--%>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">
                            Address
                        </label>
                        <asp:TextBox ID="txtShippingAddress" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine"></asp:TextBox>
                    </div>
                     <div class="col-md-12 col-sm-12">
                        <label>Remarks</label>
                        <asp:TextBox ID="txtBoxRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12" style="display:none">
                        <label class="modal-label">Freight</label>
                        <asp:TextBox ID="txtDeliveryFreight" runat="server" CssClass="form-control" BorderColor="Silver" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12" style="display:none">
                        <label class="modal-label">Packing & Forward</label>
                        <asp:TextBox ID="txtDeliveryPackingAndForward" runat="server" CssClass="form-control" BorderColor="Silver" Text="0"></asp:TextBox>
                    </div>
                    <%--   <div class="col-md-12 col-sm-12">
                        <label class="modal-label">
                            State
                        <samp style="color: red">*</samp></label>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataTextField="State" DataValueField="StateID" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-12 col-sm-12">
                        <label class="modal-label">
                            District
                        <samp style="color: red">*</samp></label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" DataTextField="District" DataValueField="DistrictID" />
                    </div>--%>
                </div>
            </div>
            <br />
            <div class="col-md-12 Report">
                <div class="table-responsive">
                    <asp:GridView ID="gvDelivery" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid"
                        EmptyDataText="No Data Found" AllowPaging="true" PageSize="2" OnPageIndexChanging="gvDelivery_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="RId" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    <itemstyle width="25px" horizontalalign="Right"></itemstyle>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialID")%>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblMaterialCode" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblSaleOrderItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderItemID")%>' runat="server" Visible="false"></asp:Label>
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
                                    <asp:Label ID="lblOrderQty" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Delivery Qty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDeliveryQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "DeliveryQuantity","{0:n}")%>' CssClass="form-control" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock Available">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStockAvailable" Text='<%# DataBinder.Eval(Container.DataItem, "StockAvailable")%>' runat="server"></asp:Label>
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
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSaveDelivery" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSaveDelivery_Click"  OnClientClick="return ConfirmSaleOrderDelivery();" />
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="MPE_Delivery" runat="server" TargetControlID="lnkMPE" PopupControlID="pnlCreateSODelivery" BackgroundCssClass="modalBackground" />


<div style="display: none">
    <asp:LinkButton ID="lnkMPE" runat="server">MPE</asp:LinkButton><asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
