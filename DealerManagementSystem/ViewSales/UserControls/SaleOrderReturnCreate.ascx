<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SaleOrderReturnCreate.ascx.cs" Inherits="DealerManagementSystem.ViewSales.UserControls.SaleOrderReturnCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Label ID="lblMessageSoReturnCreate" runat="server" Text="" CssClass="message"   />

<div class="col-md-12" id="divInvoiceSearch" runat="server">
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
        <div class="col-md-12">
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">Invoice Number</label>
                <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
            </div>
            <div class="col-md-2 col-sm-12">
                <label class="modal-label">Delivery Number</label>
                <asp:TextBox ID="txtDeliveryNo" runat="server" CssClass="form-control" BorderColor="Silver" WatermarkCssClass="WatermarkCssClass"></asp:TextBox>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
            </div>
        </div>
    </fieldset>
</div>
<div class="col-md-12" id="divInvoiceDetails" runat="server" visible="false" >
    <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">Invoice</legend>
        <div class="col-md-12 View">
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Dealer : </label>
                    <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Dealer Office : </label>
                    <asp:Label ID="lblDealerOffice" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Customer : </label>
                    <asp:Label ID="lblCustomer" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Division : </label>
                    <asp:Label ID="lblDivision" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Order Type : </label>
                    <asp:Label ID="lblSaleOrderType" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="col-md-12">
                    <label>Delivery No: </label>
                    <asp:Label ID="lblDeliveryNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Delivery Date: </label>
                    <asp:Label ID="lblDeliveryDate" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Invoice No: </label>
                    <asp:Label ID="lblInvoiceNumber" runat="server" CssClass="label"></asp:Label>
                </div>
                <div class="col-md-12">
                    <label>Invoice Date: </label>
                    <asp:Label ID="lblInvoiceDate" runat="server" CssClass="label"></asp:Label>
                </div>
            </div>
        </div>
    </fieldset>  
</div>
<div class="col-md-12" id="divSoDelivery" runat="server" visible="false">
     <fieldset class="fieldset-border">
        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
        <div class="col-md-12 Report">
            <div class="col-md-12">
                <div class="col-md-12">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>Invoice Item(s):</td>
                                        <td>
                                            <asp:Label ID="lblRowCountSoDelivery" runat="server" CssClass="label"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvSoDelivery" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" AllowPaging="true" PageSize="3" EmptyDataText="No Data Found"
                        DataKeyNmes="SaleOrderDeliveryID,SaleOrderDeliveryItem.SaleOrderDeliveryItemID" OnPageIndexChanging="gvSoDelivery_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbInvoiceH" Text="Select All" runat="server" AutoPostBack="true" OnCheckedChanged="cbInvoiceH_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbInvoice" runat="server" AutoPostBack="true" OnCheckedChanged="cbInvoice_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Dealer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Dealer.DealerCode")%>' runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Dealer.DealerName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dealer Office">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Dealer.DealerOffice.OfficeName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Customer.CustomerCode")%>' runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrder.Customer.CustomerName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Number">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblSaleOrderDeliveryID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryID")%>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblSaleOrderDeliveryItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderDeliveryItemID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Date">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Material Code">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                    <asp:Label ID="lblSaleOrderDeliveryID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryID")%>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblSaleOrderDeliveryItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderDeliveryItemID")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material Description">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Qty","{0:n}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.BaseUnit")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:Button ID="btnCreateSalesReturn" runat="server" Text="Create Sales Return" CssClass="btn Search" OnClick="btnCreateSalesReturn_Click" Width="150px" />
                </div>
            </div>
        </div>
    </fieldset>
</div>
<div class="col-md-12" id="divSoDeliveryItem" runat="server" visible="false">
    <div class="col-md-12 Report">
        <fieldset class="fieldset-border">
            <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>Invoice Item(s):</td>
                                    <td>
                                        <asp:Label ID="lblRowCountSoDeliveryItemDelivery" runat="server" CssClass="label"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvSoDeliveryItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" EmptyDataText="No Data Found"
                    DataKeyNmes="lblSaleOrderDeliveryID,SaleOrderDeliveryItem.SaleOrderDeliveryItemID">
                    <Columns>
                        <%--<asp:TemplateField HeaderText="InvoiceNumber">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblSaleOrderDeliveryID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryID")%>' runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblSaleOrderDeliveryItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderDeliveryItemID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Date">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblSaleOrderDeliveryID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryID")%>' runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblSaleOrderDeliveryItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderDeliveryItemID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Material Code">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblSaleOrderDeliveryID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryID")%>' runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblSaleOrderDeliveryItemID" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.SaleOrderDeliveryItemID")%>' runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Description">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Qty","{0:n}")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblUOM" Text='<%# DataBinder.Eval(Container.DataItem, "SaleOrderDeliveryItem.Material.BaseUnit")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Return Qty">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtReturnQty" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#ffffff" />
                    <FooterStyle ForeColor="White" />
                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                </asp:GridView>
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Remarks</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" Rows="5" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn Save" OnClick="btnSave_Click" Width="100px" />
            </div>
        </fieldset>
    </div>
</div>

