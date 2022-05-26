<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderPerformance.aspx.cs" Inherits="DealerManagementSystem.ViewProcurement.PurchaseOrderPerformance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upManageSubContractorASN" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="updateProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/Loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="position: fixed; top: 35%; right: 46%" Width="100px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="container">
                <div class="col2">
                    <div class="rf-p " id="txnHistory:j_idt1289">
                        <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                            <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                                <tr>
                                    <td>
                                        <div class="boxHead">
                                            <div class="logheading">Filter : PO Performance Report </div>
                                            <div style="float: right; padding-top: 0px">
                                                <a href="javascript:collapseExpand();">
                                                    <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlFilterContent" runat="server">
                                            <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                                <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                                    <table class="labeltxt">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Code"></asp:Label></td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox" Width="250px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Order Type"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="TextBox">
                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    <asp:ListItem Value="101">Stock Order</asp:ListItem>
                                                                    <asp:ListItem Value="102">Emergency Order</asp:ListItem>
                                                                    <asp:ListItem Value="103">Auto PO Order</asp:ListItem>
                                                                    <asp:ListItem Value="104">Warranty Order</asp:ListItem>
                                                                    <asp:ListItem Value="201">Machine Order</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Location"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="TextBox">
                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="PO Number "></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtPoNumber" runat="server" CssClass="input"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Material"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtMaterial" runat="server" CssClass="input"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Status"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="TextBox">
                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                    <asp:ListItem>NEW</asp:ListItem>
                                                                    <asp:ListItem>REQUEST</asp:ListItem>
                                                                    <asp:ListItem>COMPLETED</asp:ListItem>
                                                                    <asp:ListItem>CLOSED</asp:ListItem>
                                                                    <asp:ListItem>DRAFT</asp:ListItem>
                                                                    <asp:ListItem>PARTIAL_RECEIVED</asp:ListItem>
                                                                    <asp:ListItem>AUTO PO EMER DRAFT</asp:ListItem>
                                                                    <asp:ListItem>PARTIAL_CLOSE</asp:ListItem>
                                                                    <asp:ListItem>ORDER_PLACED</asp:ListItem>
                                                                    <asp:ListItem>AUTO PO STOCK DRAFT</asp:ListItem>
                                                                    <asp:ListItem>REJECTED</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="PO Date From "></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtPoDateFrom" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPoDateFrom" PopupButtonID="txtPoDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPoDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                            </td>
                                                            <td style="width: 20px"></td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="PO Date To"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPoDateTo" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPoDateTo" PopupButtonID="txtPoDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtPoDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="GR Date From "></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtGRDateFrom" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtGRDateFrom" PopupButtonID="txtGRDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtGRDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                            </td>
                                                            <td style="width: 20px"></td>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" CssClass="label" Text="GR Date To"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGRDateTo" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtGRDateTo" PopupButtonID="txtGRDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtGRDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="right">
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />

                                                                <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcel_Click"  />

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
                                                                <td>PO Performance Report</td>

                                                                <td>
                                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                                <td>
                                                                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                                                <td>
                                                                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="background-color: white">

                                                <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="3000px" AllowPaging="true" PageSize="20"
                                                    OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.GRDate","{0:d}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GR Number" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.GRNumber")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order Type" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "POType")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material" HeaderStyle-Width="95px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblf_material_id" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="170px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="HSN" HeaderStyle-Width="60px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.HSN")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Order Qty" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_order_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.OrderQuantity","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_unit_price" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.UnitPrice","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Net Amt" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_net_amt" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.NetAmount","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Discount" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_discount_amt" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.DiscountAmount","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Freight" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFright" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Fright","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insurance" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInsurance" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Insurance","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Packing" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPackingAndForwarding" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.PackingAndForwarding","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Taxable Amount" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.TaxableAmount","{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SGST" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.SGST","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CGST" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.CGST","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.IGST","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gross Amt" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_gross_amt" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.GrossAmount","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Dealer Code" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dealer Name" HeaderStyle-Width="120px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Location")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PO Status" HeaderStyle-Width="79px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbls_status" Text='<%# DataBinder.Eval(Container.DataItem, "POStatus")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GR Status" HeaderStyle-Width="79px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbls_status" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.GRStatus")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PO Number" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderID")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PO Item" HeaderStyle-Width="50px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblp_po_item" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.POItem")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO Date" HeaderStyle-Width="92px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderDate" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderDate","{0:d}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="PO Month" HeaderStyle-Width="50px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblp_po_item" Text='<%# DataBinder.Eval(Container.DataItem, "POMonth")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="35px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblf_uom" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.UOM")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ASN Qty" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.ASNQuantity","{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ASN Date" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.ASNDate","{0:d}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GR Qty" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.GRQuantity","{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <%--   <asp:TemplateField HeaderText="GR Date" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.GRDate","{0:d}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Missing Qty" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.MissingQuantity","{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Damaged Qty" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.DamagedQuantity","{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Wrong Supply Qty" HeaderStyle-Width="120px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchaseOrderID" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.WrongSupplyQuantity","{0:n}")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PO - ASN Qty" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_approved_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.POMinusAsnQuantity","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ASN Dt - PO Dt" HeaderStyle-Width="85px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_approved_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.AsnMinusPODate")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO - GR Qty" HeaderStyle-Width="90px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_approved_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.POMinusGrQuantity","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GR Dt - PO Dt" HeaderStyle-Width="92px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_approved_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.GrMinusPODate")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cum. Asn Qty" HeaderStyle-Width="92px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_approved_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.CumulativeAsnQuantity","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Latest GR Dt" HeaderStyle-Width="92px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_approved_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.LatestGrDate","{0:d}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cum. Gr Qty" HeaderStyle-Width="92px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_approved_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.CumulativeGrQuantity","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById("MainContent_pnlFilterContent");
            var imageID = document.getElementById("MainContent_imageID");
            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }
    </script>
</asp:Content>