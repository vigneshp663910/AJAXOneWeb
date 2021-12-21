<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="DealerManagementSystem.ViewSales.PurchaseOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="updateProgress1" runat="server">
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
                                            <div class="logheading">Filter : PO Report </div>
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
                                                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="PO Number :"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtPoNumber" runat="server" CssClass="input"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="PO Date From :"></asp:Label></td>
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
                                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="PO Status"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPOStatus" runat="server" CssClass="TextBox">
                                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                                    <asp:ListItem Value="NEW">NEW</asp:ListItem>
                                                                    <asp:ListItem Value="REQUEST">REQUEST</asp:ListItem>
                                                                    <asp:ListItem Value="DRAFT">DRAFT</asp:ListItem>
                                                                    <asp:ListItem Value="COMPLETED">COMPLETED</asp:ListItem>
                                                                    <asp:ListItem Value="CLOSED">CLOSED</asp:ListItem>
                                                                    <asp:ListItem Value="PARTIAL_RECEIVED">PARTIAL_RECEIVED</asp:ListItem>
                                                                    <asp:ListItem Value="AUTO PO EMER DRAFT">AUTO PO EMER DRAFT</asp:ListItem>
                                                                    <asp:ListItem Value="ORDER_PLACED">ORDER_PLACED</asp:ListItem>
                                                                    <asp:ListItem Value="PARTIAL_CLOSE">PARTIAL_CLOSE</asp:ListItem>
                                                                    <asp:ListItem Value="AUTO PO STOCK DRAFT">AUTO PO STOCK DRAFT</asp:ListItem>
                                                                    <asp:ListItem Value="REJECTED">REJECTED</asp:ListItem>
                                                                    <asp:ListItem Value="TEMPLATE">TEMPLATE</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="right">
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />

                                                                <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />

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
                                                                        <td>PO Report</td>

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

                                                        <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="2500px" AllowPaging="true" PageSize="20"
                                                            OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                                            <Columns>
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
                                                                <asp:TemplateField HeaderText="PO Type" HeaderStyle-Width="92px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPOType" Text='<%# DataBinder.Eval(Container.DataItem, "POType")%>' runat="server"></asp:Label>
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
                                                                <asp:TemplateField HeaderText="Curr" HeaderStyle-Width="70px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblf_currency" Text='<%# DataBinder.Eval(Container.DataItem, "Currency")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vendor Code" HeaderStyle-Width="76px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblf_bill_to" Text='<%# DataBinder.Eval(Container.DataItem, "BillTo")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <%--  public string r_insurance_p { get; set; }
        public decimal r_tax_amt { get; set; }
 
        public decimal r_net_amt { get; set; }
        public decimal r_gross_amt { get; set; }
        public decimal r_discount_amt { get; set; }--%>

                                                                <%--     <asp:TemplateField HeaderText="Sold To" HeaderStyle-Width="77px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblf_sold_to" Text='<%# DataBinder.Eval(Container.DataItem, "f_sold_to")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="PO Status" HeaderStyle-Width="79px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbls_status" Text='<%# DataBinder.Eval(Container.DataItem, "POStatus")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Div" HeaderStyle-Width="40px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblf_division" Text='<%# DataBinder.Eval(Container.DataItem, "Division")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Material" HeaderStyle-Width="95px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblf_material_id" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="HSN" HeaderStyle-Width="60px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHSN" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.HSN")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--     <asp:TemplateField HeaderText="Mat Type">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialType" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.MaterialType")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="170px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbld_material_desc" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Order Qty" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblr_order_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.OrderQuantity","{0:n}")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ship. Qty" HeaderStyle-Width="85px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblr_shiped_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.ShipedQuantity","{0:n}")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Apr Qty" HeaderStyle-Width="85px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblr_approved_qty" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.ApprovedQuantity","{0:n}")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="35px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblf_uom" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.UOM")%>' runat="server"></asp:Label>
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
                                                                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Width="85px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblr_unit_price" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderItem.UnitPrice","{0:n}")%>' runat="server"></asp:Label>
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

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>