<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyFailureMaterialGateEntry.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyFailureMaterialGateEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
       
    </script>
    <div class="container IC_ticketManageInfo">
        <div class="col2">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
            <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%" class="IC_basicInfo">
                <tr>
                    <td>
                        <div class="boxHead">
                            <div class="logheading">Filter : Failed Material Report </div>
                            <div style="float: right; padding-top: 0px">
                                <a href="javascript:collapseExpand();">
                                    <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                            </div>
                        </div>
                        <asp:Panel ID="pnlFilterContent" runat="server">
                            <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                    <table class="labeltxt fullWidth">
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Delivery Challan Number"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtDeliveryChallanNumber" runat="server" CssClass="TextBox" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>

            <table id="txnHistory3:panelGridid" style="height: 100%; width: 100%" class="IC_basicInfo">
                <tr>
                    <td>
                        <div class="boxHead">
                            <div class="logheading">Filter : Failed Material Report </div>
                            <div style="float: right; padding-top: 0px">
                                <a href="javascript:collapseExpand();">
                                    <img id="Img1" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="rf-p " id="txnHistory2:inputFiltersPanel">
                                <div class="rf-p-b " id="txnHistory2:inputFiltersPanel_body">
                                  <table class="labeltxt fullWidth">
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Delivery Challan Number"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblDeliveryChallanNumber" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Delivery Challan Date"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblDeliveryChallanDate" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Delivery To"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblDeliveryTo" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Transporter Name"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblTransporterName" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label10" runat="server" CssClass="label" Text="Docket Details"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblDocketDetails" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label12" runat="server" CssClass="label" Text="Created By"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblCreatedBy" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label14" runat="server" CssClass="label" Text="Dealer"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:Label ID="lblDealer" runat="server" CssClass="label"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <span id="txnHistory1:refreshDataGroup">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Failed Material Report</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div style="background-color: white">
                                <asp:GridView ID="gvDCItem" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="DeliveryChallanItemID"
                                    CssClass="TableGrid" >
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
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="InputButton" UseSubmitBehavior="true"   OnClientClick="return dateValidation();" OnClick="btnCancel_Click"  Visible='<%# DataBinder.Eval(Container.DataItem, "_IsAcknowledged")%>'  />
                                                <asp:Label ID="lblAcknowledgeStatus" Text='<%# DataBinder.Eval(Container.DataItem, "AcknowledgeStatus")%>' runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsAcknowledged")%>'></asp:Label>
                                                <asp:Label ID="Label3" Text='<%# DataBinder.Eval(Container.DataItem, "AcknowledgeStatus")%>' runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsCanceled")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#BFE4FF" ForeColor="Black" />
                                    <HeaderStyle BackColor="#BFE4FF" Font-Bold="True" ForeColor="Black" />
                                    <RowStyle ForeColor="Black" BackColor="#bfe4ff" />
                                </asp:GridView>
                                
                            </div>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>