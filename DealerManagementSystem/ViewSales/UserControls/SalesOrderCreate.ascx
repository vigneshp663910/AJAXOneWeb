<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderCreate.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SalesOrderCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="true" />
<div class="col-md-12">
    <fieldset class="fieldset-border" runat="server">
        <legend style="background: none; color: #007bff; font-size: 17px;">Sale Order Creation</legend>
        <div class="col-md-12">
            <div class="col-md-6 col-sm-12">
                <label class="modal-label">Dealer<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
            </div>
            <div class="col-md-6 col-sm-12">
                <label class="modal-label">Dealer Office<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6 col-sm-12">
                <label class="modal-label">Customer (Search by Customer Code(6 Char.)/Name(min 4 Char.)/Mobile(10 digits))<samp style="color: red">*</samp></label>
                <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"
                    onKeyUp="GetCustomers()"></asp:TextBox>
                <asp:HiddenField ID="hdfCustomerId" runat="server" />
            </div>
            <div class="col-md-6 col-sm-12">
                <label class="modal-label">Contact Person</label>
                <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" BorderColor="Silver" MaxLength="35"></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtContactPerson" WatermarkText="Contact Person" WatermarkCssClass="WatermarkCssClass" />
            </div>
            <div class="col-md-6 col-sm-12">
                <label class="modal-label">Contact Person Number</label>
                <asp:TextBox ID="txtContactPersonNumber" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Phone" MaxLength="10"></asp:TextBox>
                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtContactPersonNumber" WatermarkText="Mobile" WatermarkCssClass="WatermarkCssClass" />
            </div>
            <div class="col-md-6 col-sm-12">
                <label class="modal-label">Division<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
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
                <%--<asp:TextBox ID="txtInsurancePaidBy" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlInsurancePaidBy" runat="server" CssClass="form-control" BorderColor="Silver">
                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="1">Seller</asp:ListItem>
                    <asp:ListItem Value="2">Buyer</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Frieght Paid By</label>
                <%--<asp:TextBox ID="txtFrieghtPaidBy" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlFrieghtPaidBy" runat="server" CssClass="form-control" BorderColor="Silver">
                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="1">Seller</asp:ListItem>
                    <asp:ListItem Value="2">Buyer</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Attn.</label>
                <asp:TextBox ID="txtAttn" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Equipment Serial No</label>
                <asp:TextBox ID="txtEquipmentSerialNo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Select Tax<samp style="color: red">*</samp></label>
                <%--<asp:TextBox ID="txtSelectTax" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>--%>
                <asp:DropDownList ID="ddlTax" runat="server" CssClass="form-control" BorderColor="Silver">
                    <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="1">SGST & CGST</asp:ListItem>
                    <asp:ListItem Value="2">IGST</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Sales Engineer</label>
                <asp:DropDownList ID="ddlSalesEngineer" runat="server" CssClass="form-control" AutoPostBack="true" />
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Remarks</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Header Discount %</label>
                <asp:TextBox ID="txtBoxHeaderDiscountPercent" runat="server" CssClass="form-control" BorderColor="Silver" AutoPostBack="true" OnTextChanged="txtBoxHeaderDiscountPercent_TextChanged"></asp:TextBox>
            </div>
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                    <div class="col-md-12">
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
                </fieldset>
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Sale Order Item</legend>
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
                                        <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Qty","{0:n}")%>' runat="server"></asp:Label>
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
                                        <asp:Label ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "UnitPrice","{0:n}")%>' runat="server"></asp:Label>
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
                                        <asp:TextBox ID="txtBoxDiscountPercent" runat="server" CssClass="form-control" Text='<%# DataBinder.Eval(Container.DataItem, "Discount","{0:n}")%>' AutoPostBack="true" OnTextChanged="txtBoxDiscountPercent_TextChanged"></asp:TextBox>
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
                                <asp:TemplateField HeaderText="Tax Amount">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxAmount" Text='<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "SGSTAmt")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "CGSTAmt")) + Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IGSTAmt"))).ToString("N2") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amount">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmt","{0:n}")%>' runat="server"></asp:Label>
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
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSaveSOItem" runat="server" CssClass="btn Save" Text="Save" OnClick="btnSaveSOItem_Click" Width="100px"></asp:Button>
            </div>
        </div>
    </fieldset>
</div>

