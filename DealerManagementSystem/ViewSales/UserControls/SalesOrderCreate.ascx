<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderCreate.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SalesOrderCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div class="col-md-12">
    <div class="action-btn">
        <div class="" id="boxHere"></div>
        <div class="dropdown btnactions" id="customerAction">
            <div class="btn Approval">Actions</div>
            <div class="dropdown-content" style="font-size: small; margin-left: -105px">
                <%--<asp:LinkButton ID="lbSave" runat="server" OnClick="lbActions_Click">Save</asp:LinkButton>--%>
            </div>
        </div>
    </div>
</div>
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
                <label class="modal-label">DealerOffice<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6 col-sm-12">
                <label class="modal-label">Customer (Search by customer Code(6 char.)/Name(min 4 Char.)/Mobile(10 digits))<samp style="color: red">*</samp></label>
                <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"
                    onKeyUp="GetCustomers()"></asp:TextBox>
                <asp:HiddenField ID="hdfCustomerId" runat="server" />
            </div>
            <%--<div class="col-md-6 col-sm-12">
                <label class="modal-label">Order To<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlOrderTo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOrderTo_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="1">OE</asp:ListItem>
                    <asp:ListItem Value="2">Co-Dealer</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-6 col-sm-12">
                <label class="modal-label">Order Type<samp style="color: red">*</samp></label>
                <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged" AutoPostBack="true" />
            </div>--%>
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
                <asp:TextBox ID="txtInsurancePaidBy" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Frieght Paid By</label>
                <asp:TextBox ID="txtFrieghtPaidBy" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Attn</label>
                <asp:TextBox ID="txtAttn" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Equipment SerialNo</label>
                <asp:TextBox ID="txtEquipmentSerialNo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Select Tax</label>
                <asp:TextBox ID="txtSelectTax" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
            </div>
            <div class="col-md-6 col-sm-12">
                <label>Remarks</label>
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" AutoCompleteType="Disabled"></asp:TextBox>
            </div>

            <div class="col-md-12 Report">
                <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">SupersedeYN</label>
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
                    <legend style="background: none; color: #007bff; font-size: 17px;">SaleOrder Item</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvSOItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="2500px">
                            <Columns>
                                <asp:TemplateField HeaderText="Material">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Qty","{0:n}")%>' runat="server"></asp:Label>
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
                                <asp:TemplateField HeaderText="DiscountedPrice">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscountAmount" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountedPrice","{0:n}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Taxable Amount">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTaxableAmount" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableAmount","{0:n}")%>' runat="server"></asp:Label>
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

