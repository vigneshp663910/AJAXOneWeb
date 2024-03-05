<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderCreate.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SalesOrderCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<style>
    label {
        display: inline-block;
        margin-bottom: 0rem;
    }
</style>
<asp:Label ID="lblMessage" runat="server" CssClass="message" />
<asp:HiddenField ID="hfEquipmentID" runat="server" Value="0" />
<asp:HiddenField ID="hfShiptToID" runat="server" Value="0" />
<div class="col-md-12">

    <fieldset class="fieldset-border" runat="server">
        <legend style="background: none; color: #007bff; font-size: 17px;">Sale Order</legend>
        <div class="col-md-12" style="margin-top: -15px; margin-bottom: -20px">
            <div class="col-md-3">
                <div class="col-sm-10">
                    <label class="modal-label">Dealer<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                </div>
                <div class="col-sm-10">
                    <label class="modal-label">Dealer Office<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" />
                </div>
                <div class="col-sm-10">
                    <label>Sales Engineer</label>
                    <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" AutoPostBack="true" />
                </div>
                <div class="col-sm-10" style="display: none">
                    <label class="modal-label">Division<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                </div>

                <div class="col-sm-12">
                    <label>Remarks</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled" Height="30px"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-3">
                <div class="col-sm-10">
                    <label class="modal-label">Customer<samp style="color: red">*</samp></label>

                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass" AutoPostBack="true"
                        onKeyUp="GetCustomers()" OnTextChanged="txtCustomer_TextChanged"></asp:TextBox>

                    <asp:HiddenField ID="hdfCustomerId" runat="server" />
                </div>
                <div class="col-sm-10">
                    <label>Attn.</label>
                    <asp:TextBox ID="txtAttn" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                </div>
                <div class="col-sm-10">
                    <label class="modal-label">Contact Person Number</label>
                    <asp:TextBox ID="txtContactPersonNumber" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtContactPersonNumber" WatermarkText="Mobile" WatermarkCssClass="WatermarkCssClass" />
                </div>
                <div class="col-sm-10">
                    <label>Equipment Serial No</label>
                    <asp:DropDownList ID="ddlEquipment" runat="server" CssClass="form-control" />
                </div>
                <%--   <div class="col-sm-10">
                    <label class="modal-label">Shift Address<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlShiftTo" runat="server" CssClass="form-control" />
                </div>--%>
            </div>
            <div class="col-md-2">
                <div class="col-sm-10">
                    <label class="modal-label">Product<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" />
                </div>

                <div class="col-sm-12">
                    <label>Expected Delivery Date<samp style="color: red">*</samp></label>
                    <asp:TextBox ID="txtExpectedDeliveryDate" runat="server" CssClass="form-control" BorderColor="Silver" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp1:CalendarExtender ID="cxExpectedDeliveryDate" runat="server" TargetControlID="txtExpectedDeliveryDate" PopupButtonID="txtExpectedDeliveryDate" Format="dd/MM/yyyy" />
                    <asp1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtExpectedDeliveryDate" WatermarkText="DD/MM/YYYY" />
                </div>
            </div>
            <div class="col-md-2">
                <div class="col-sm-12">
                    <label>Insurance Paid By</label>
                    <asp:DropDownList ID="ddlInsurancePaidBy" runat="server" CssClass="form-control" BorderColor="Silver">
                        <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                        <asp:ListItem Value="1">Seller</asp:ListItem>
                        <asp:ListItem Value="2">Buyer</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-12">
                    <label>Frieght Paid By</label>
                    <asp:DropDownList ID="ddlFrieghtPaidBy" runat="server" CssClass="form-control" BorderColor="Silver">
                        <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                        <asp:ListItem Value="1">Seller</asp:ListItem>
                        <asp:ListItem Value="2">Buyer</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-12">
                    <label>Tax<samp style="color: red">*</samp></label>
                    <asp:DropDownList ID="ddlTaxType" runat="server" CssClass="form-control" BorderColor="Silver">
                        <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                        <asp:ListItem Value="1">SGST & CGST</asp:ListItem>
                        <asp:ListItem Value="2">IGST</asp:ListItem>
                    </asp:DropDownList>
                </div>



                <div class="col-sm-12">
                    <label>Header Discount %</label>
                    <asp:TextBox ID="txtBoxHeaderDiscountPercent" runat="server" CssClass="form-control" BorderColor="Silver" AutoPostBack="true" OnTextChanged="txtBoxHeaderDiscountPercent_TextChanged"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-2">
                <div class="col-sm-12">
                    <label>Discount Value : </label>
                    <asp:Label ID="lblDiscountValue" runat="server" BorderColor="Silver"></asp:Label>
                </div>
                <div class="col-sm-12">
                    <label>Taxable Value : </label>
                    <asp:Label ID="lblTaxableValue" runat="server" BorderColor="Silver"></asp:Label>
                </div>
                <div class="col-sm-12">
                    <label>Tax Value : </label>
                    <asp:Label ID="lblTaxValue" runat="server" BorderColor="Silver"></asp:Label>
                    <%--   </div>
                <div class="col-sm-12">
                    <label>Cess Value : </label>
                    <asp:Label ID="txtCessValue" runat="server" BorderColor="Silver"></asp:Label>
                </div>--%>
                    <div class="col-sm-12">
                        <label>TCS Value : </label>
                        <asp:Label ID="lblTCSValue" runat="server" BorderColor="Silver"></asp:Label>
                    </div>
                    <%--  <div class="col-sm-12">
                    <label>Sub Total Value : </label>
                    <asp:Label ID="lblNetValue" runat="server" BorderColor="Silver"></asp:Label>
                </div>--%>

                    <div class="col-sm-12">
                        <label>Total Value : </label>
                        <asp:Label ID="lblTotalValue" runat="server" BorderColor="Silver"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </fieldset>

    <fieldset class="fieldset-border" id="Fieldset1" runat="server">
        <legend style="background: none; color: #007bff; font-size: 17px;">Sale Order Item</legend>
        <div class="col-md-12" style="margin-top: -15px; margin-bottom: -20px">
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">Supersede Y/N</label>
                <asp:CheckBox ID="cbSupersedeYN" runat="server" Checked="true" />
            </div>
            <div class="col-md-3 col-sm-12">
                <asp:HiddenField ID="hdfMaterialID" runat="server" />
                <asp:HiddenField ID="hdfMaterialCode" runat="server" />
                <label class="modal-label">Material<samp style="color: red">*</samp></label>
                <asp:TextBox ID="txtMaterial" runat="server" CssClass="form-control" onKeyUp="GetMaterial()"></asp:TextBox>
            </div>
            <div class="col-md-3 col-sm-12">
                <label class="modal-label">Qty<samp style="color: red">*</samp></label>
                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 text-left">
                <label class="modal-label">.</label>
                <asp:Button ID="btnAddMaterial" runat="server" Text="Add" CssClass="btn Search" OnClick="btnAddMaterial_Click" />
            </div>
        </div>
        <div class="col-md-12 Report">
            <asp:GridView ID="gvSOItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid">
                <Columns>
                    <asp:TemplateField HeaderText="Material">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server"></asp:Label>
                            <asp:Label ID="lblMaterialID" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialID")%>' runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Desc">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDescription")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HSN Code">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "HSN")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Qty">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" Text='<%# DataBinder.Eval(Container.DataItem, "UOM")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Price">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "PerRate","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Value">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" Text='<%# DataBinder.Eval(Container.DataItem, "Value","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Discounted Price">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount","{0:n}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Discount %">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtBoxDiscountPercent" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "ItemDiscountPercentage","{0:n}")%>' AutoPostBack="true" OnTextChanged="txtBoxDiscountPercent_TextChanged"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discount">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblDiscountValue" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountValue","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Taxable Amount">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="SGST">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "SGST","{0:n}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="CGST">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "CGST","{0:n}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="IGST">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "IGST","{0:n}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Tax">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblTax" Text='<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "SGST")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "CGST")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IGST"))).ToString("N2") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tax Value">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblTaxValue" Text='<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "SGSTValue")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "CGSTValue")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IGSTValue"))).ToString("N2") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Net Amount">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmount","{0:n}")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBtnSoItemDelete" runat="server" OnClick="lnkBtnSoItemDelete_Click"><i class="fa fa-fw fa-times" style="font-size:18px"></i></asp:LinkButton>
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

    <div class="col-md-12 text-center">
        <asp:Button ID="btnSaveSOItem" runat="server" CssClass="btn Save" Text="Save" OnClick="btnSaveSOItem_Click" Width="100px"></asp:Button>
    </div>

</div>
<script>
    function GetCustomers() {
        $("#MainContent_UC_SalesOrderCreate_hdfCustomerId").val('');
        var param = { CustS: $('#MainContent_UC_SalesOrderCreate_txtCustomer').val() };
        var Customers = [];
        if ($('#MainContent_UC_SalesOrderCreate_txtCustomer').val().trim().length >= 3) {
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
                    $('#MainContent_UC_SalesOrderCreate_txtCustomer').autocomplete({
                        source: function (request, response) {
                            response(Customers)
                        },
                        select: function (e, u) {
                            $("#MainContent_UC_SalesOrderCreate_hdfCustomerId").val(u.item.value1);
                            //FillEquipment(u.item.value1);
                            //FillShipTo(u.item.value1);
                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_SalesOrderCreate_txtCustomer').width() + 48,
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
            $('#MainContent_UC_SalesOrderCreate_txtCustomer').autocomplete({
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
        $("#MainContent_UC_SalesOrderCreate_hdfMaterialID").val('');
        $("#MainContent_UC_SalesOrderCreate_hdfMaterialCode").val('');
        var param = { Material: $('#MainContent_UC_SalesOrderCreate_txtMaterial').val(), MaterialType: '', DivisionID: $('#MainContent_UC_SalesOrderCreate_ddlDivision').val() }
        var Customers = [];
        if ($('#MainContent_UC_SalesOrderCreate_txtMaterial').val().trim().length >= 3) {
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
                    $('#MainContent_UC_SalesOrderCreate_txtMaterial').autocomplete({
                        source: function (request, response) { response(Customers) },
                        select: function (e, u) {
                            $("#MainContent_UC_SalesOrderCreate_hdfMaterialID").val(u.item.value1);
                            $("#MainContent_UC_SalesOrderCreate_hdfMaterialCode").val(u.item.MaterialCode);
                        },
                        open: function (event, ui) {
                            $(this).autocomplete("widget").css({
                                "max-width":
                                    $('#MainContent_UC_SalesOrderCreate_txtMaterial').width() + 48,
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
            $('#MainContent_UC_SalesOrderCreate_txtMaterial').autocomplete({
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
    var ddlEquipmentNO;
    function FillEquipment(custID) {
        debugger;
        ddlEquipmentNO = document.getElementById("MainContent_UC_SalesOrderCreate_ddlEquipment");
        PageMethods.GetEquipment(custID, OnSuccessEquipment, OnErrorEquipment);
    }
    function OnErrorEquipment(error) {
        alert(error);
    }
    function OnSuccessEquipment(response) {
        debugger;
        ddlEquipmentNO.options.length = 0;
        AddOptionEquipment("Select", "0");
        for (var i in response) {
            AddOptionEquipment(response[i].Name, response[i].Id);
        }
    }
    function AddOptionEquipment(text, value) {
        var option = document.createElement('option');
        option.value = value;
        option.innerHTML = text;
        ddlEquipmentNO.options.add(option);
    }

    function SetEquipmentInHiddenField() {
        ddlEquipmentNO = document.getElementById("MainContent_UC_SalesOrderCreate_ddlEquipment");
        document.getElementById('MainContent_hfShiptToID').value = ddlEquipmentNO.value;
    }
</script>

<script type="text/javascript">    
    var ddlShiftTo;
    function FillShipTo(custID) {
        debugger;
        ddlShiftTo = document.getElementById("MainContent_UC_SalesOrderCreate_ddlShiftTo");
        PageMethods.GetShiftTo(custID, OnSuccessShiftTo, OnErrorShiftTo);
    }
    function OnErrorShiftTo(error) {
        alert(error);
    }
    function OnSuccessShiftTo(response) {
        debugger;
        ddlShiftTo.options.length = 0;
        AddOptionShiftTo("Select", "0");
        for (var i in response) {
            AddOptionShiftTo(response[i].Name, response[i].Id);
        }
    }
    function AddOptionShiftTo(text, value) {
        var option = document.createElement('option');
        option.value = value;
        option.innerHTML = text;
        ddlShiftTo.options.add(option);
    }

    function SetShipToInHiddenField() {
        ddlShiftTo = document.getElementById("MainContent_UC_SalesOrderCreate_ddlShiftTo");
        document.getElementById('MainContent_hfShiptToID').value = ddlShiftTo.value;
    }
</script>
