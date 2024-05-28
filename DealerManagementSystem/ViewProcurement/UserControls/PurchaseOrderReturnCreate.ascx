<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrderReturnCreate.ascx.cs" Inherits="DealerManagementSystem.ViewProcurement.UserControls.PurchaseOrderReturnCreate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>

<asp:Label ID="lblMessagePoReturnCreate" runat="server" Text="" CssClass="message" Visible="false" />

<div class="col-md-12">
    <div class="col-md-12">
        <div class="col-md-2 col-sm-12">
            <label class="modal-label">Dealer<samp style="color: red">*</samp></label>
            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
        </div>
          <div class="col-md-2 col-sm-12">
            <label class="modal-label">Location<samp style="color: red">*</samp></label>
            <asp:DropDownList ID="ddlDealerOffice" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2 col-sm-12">
            <label class="modal-label">Vendor<samp style="color: red">*</samp></label>
            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2 col-sm-12">
            <label class="modal-label">Division</label>
            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" />
        </div>
      
        <div class="col-md-2 text-left">
            <label class="modal-label">-</label>
            <asp:Button ID="btnSearch" runat="server" Text="Retrieve" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
        </div>
    </div>
</div>
<div class="col-md-12">
    <div class="col-md-12 Report">
        <div class="col-md-12">
            <div class="col-md-12">
                <div class="boxHead">
                    <div class="logheading">
                        <div style="float: left">
                            <table>
                                <tr>
                                    <td>GR(s):</td>
                                    <td>
                                        <asp:Label ID="lblRowCountGR" runat="server" CssClass="label"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvGr" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%" AllowPaging="true" PageSize="10" EmptyDataText="No Data Found"
                    DataKeyNmes="GRItem.GrItemID" OnPageIndexChanging="gvGr_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Select GR">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="cbIsChecked" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblPurchaseOrderNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.PurchaseOrder.PurchaseOrderNumber")%>' runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblPurchaseOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.PurchaseOrder.PurchaseOrderDate")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ASN">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblAsnNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.AsnNumber")%>' runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblAsnDate" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.AsnDate")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delivery">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblDelivery" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.DeliveryNumber")%>' runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lblDeliveryDate" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.DeliveryDate")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Invoice">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ASN.InvoiceNumber")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GR Number">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblGrNumber" Text='<%# DataBinder.Eval(Container.DataItem, "GRNumber")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblGrID" Text='<%# DataBinder.Eval(Container.DataItem, "GrID")%>' runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblGrItemID" Text='<%# DataBinder.Eval(Container.DataItem, "GRItem.GrItemID")%>' runat="server" Visible="false"></asp:Label>
                                 <br />
                                <asp:Label ID="lblGrDate" Text='<%# DataBinder.Eval(Container.DataItem, "GrDate")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Material">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialCodeGR" Text='<%# DataBinder.Eval(Container.DataItem, "GRItem.AsnItem.PurchaseOrderItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Description">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialDescGR" Text='<%# DataBinder.Eval(Container.DataItem, "GRItem.AsnItem.PurchaseOrderItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Restricted Qty">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "GRItem.RestrictedQty","{0:n}")%>' runat="server"></asp:Label>
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
            <div class="col-md-12 col-sm-12" runat="server" id="divPendingGR" visible="false">
                <div class="col-md-12 col-sm-12">
                    <label class="modal-label">Remarks</label>
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="MultiLine" Rows="5" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</div>




