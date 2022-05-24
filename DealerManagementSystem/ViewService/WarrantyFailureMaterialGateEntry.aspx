<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyFailureMaterialGateEntry.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyFailureMaterialGateEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Delivery Challan Number</label>
                        <asp:TextBox ID="txtDeliveryChallanNumber" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-10 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                    </div>
                </div>
            </fieldset>
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Failed Material Details</legend>
                <div class="col-md-12 View">
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="modal-label">Delivery Challan Number</label>
                            <asp:Label ID="lblDeliveryChallanNumber" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label class="modal-label">Transporter Name</label>
                            <asp:Label ID="lblTransporterName" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label class="modal-label">Dealer</label>
                            <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="modal-label">Delivery Challan Date</label>
                            <asp:Label ID="lblDeliveryChallanDate" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label class="modal-label">Docket Details</label>
                            <asp:Label ID="lblDocketDetails" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-12">
                            <label class="modal-label">Delivery To</label>
                            <asp:Label ID="lblDeliveryTo" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <label class="modal-label">Created By</label>
                            <asp:Label ID="lblCreatedBy" runat="server" CssClass="label"></asp:Label>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Failed Material Report</legend>
                <div class="col-md-12 Report">
                    <asp:GridView ID="gvDCItem" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="DeliveryChallanItemID"
                        CssClass="table table-bordered table-condensed Grid">
                        <Columns>
                            <asp:TemplateField HeaderText="Claim" HeaderStyle-Width="62px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblWarrantyInvoiceItemID" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.WarrantyInvoiceItemID")%>' runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceNumber")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Dt">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="75px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.ICTicketID")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IC Ticket Dt" HeaderStyle-Width="75px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Restore Dt" HeaderStyle-Width="75px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRestoreDate" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.RestoreDate","{0:d}")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust. Code">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.CustomerCode")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust. Name">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.CustomerName")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HMR">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHMR" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.HMR")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Margin Warranty">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMarginWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.MarginWarranty")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.Model")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MC Serial No">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMachineSerialNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.MachineSerialNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FSR No">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFSRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.FSRNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TSIR No">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.TSIRNumber")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="55px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.ClaimStatus")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SAC / HSN Code">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.HSNCode")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material" HeaderStyle-Width="78px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.Material")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="150px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.MaterialDesc")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="40px">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice.InvoiceItem.Qty")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnConfirm_Click" OnClientClick="return dateValidation();" Visible='<%# DataBinder.Eval(Container.DataItem, "_IsAcknowledged")%>' />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="InputButton" UseSubmitBehavior="true" OnClientClick="return dateValidation();" OnClick="btnCancel_Click" Visible='<%# DataBinder.Eval(Container.DataItem, "_IsAcknowledged")%>' />
                                    <asp:Label ID="lblAcknowledgeStatus" Text='<%# DataBinder.Eval(Container.DataItem, "AcknowledgeStatus")%>' runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsAcknowledged")%>'></asp:Label>
                                    <asp:Label ID="Label3" Text='<%# DataBinder.Eval(Container.DataItem, "AcknowledgeStatus")%>' runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsCanceled")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>