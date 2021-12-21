<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesOrderItems.aspx.cs" Inherits="DealerManagementSystem.ViewSales.SalesOrderItems" %>
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
                                            <div class="logheading">Filter : Sales Order Report </div>
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
                                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Dealer Code"></asp:Label></td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox" Width="250px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="Customer :"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtCustomer" runat="server" CssClass="input"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="SO Number"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSONumber" runat="server" CssClass="input"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text=" SO Date From"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSODateFrom" runat="server" CssClass="input"  AutoComplete="Off"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSODateFrom" PopupButtonID="txtSODateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtSODateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                            </td>
                                                            <td style="width: 20px"></td>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="SO Date To"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSODateTo" runat="server" CssClass="input"  AutoComplete="Off"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSODateTo" PopupButtonID="txtSODateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtSODateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Invoice Number"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="input"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Invoice Date From :"></asp:Label></td>
                                                            <td>
                                                                <asp:TextBox ID="txtInvoiceDateFrom" runat="server" CssClass="input"  AutoComplete="Off"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInvoiceDateFrom" PopupButtonID="txtInvoiceDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtInvoiceDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                            </td>
                                                            <td style="width: 20px"></td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Invoice Date To"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtInvoiceDateTo" runat="server" CssClass="input"  AutoComplete="Off"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtInvoiceDateTo" PopupButtonID="txtInvoiceDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtInvoiceDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Material"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMaterial" runat="server" CssClass="input"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" CssClass="label" Text="Sales Type"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSalesType" runat="server" CssClass="TextBox">
                                                                    <asp:ListItem Value="0">Parts Sales</asp:ListItem>
                                                                    <asp:ListItem Value="1">MC Sales All</asp:ListItem>
                                                                    <asp:ListItem Value="2">MC Sales Quotation</asp:ListItem>
                                                                    <asp:ListItem Value="3">MC Sales Order</asp:ListItem>
                                                                    <asp:ListItem Value="4">Warranty</asp:ListItem>
                                                                    <asp:ListItem Value="5">Warranty Quotation</asp:ListItem>
                                                                    <asp:ListItem Value="6">Warranty Order</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" CssClass="label" Text="SO Status"></asp:Label></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSOStatus" runat="server" CssClass="TextBox">
                                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                                    <asp:ListItem Value="QUOTATION">QUOTATION</asp:ListItem>
                                                                    <asp:ListItem Value="QUOT_CLOSED">QUOT_CLOSED</asp:ListItem>
                                                                    <asp:ListItem Value="ORDER_PLACED">ORDER_PLACED</asp:ListItem>
                                                                    <asp:ListItem Value="COMPLETED">COMPLETED</asp:ListItem>
                                                                    <asp:ListItem Value="DRAFT">DRAFT</asp:ListItem>
                                                                    <asp:ListItem Value="REJECTED">REJECTED</asp:ListItem>
                                                                    <asp:ListItem Value="CLOSED">CLOSED</asp:ListItem>
                                                                    <asp:ListItem Value="REQUEST">REQUEST</asp:ListItem>
                                                                    <asp:ListItem Value="PARTIAL_DELV">PARTIAL_DELV</asp:ListItem>
                                                                    <asp:ListItem Value="PARTIAL_CLOSE">PARTIAL_CLOSE</asp:ListItem>
                                                                    <asp:ListItem Value="TEMPLATE">TEMPLATE</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="right" colspan="4">
                                                                <asp:Button ID="btnSearch" runat="server" CssClass="InputButton" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" Text="Search" UseSubmitBehavior="true" />
                                                                &nbsp;
                                                        <asp:Button ID="btnExportExcel" runat="server" CssClass="InputButton" OnClick="btnExportExcel_Click" Text="<%$ Resources:Resource, btnExportExcel %>" UseSubmitBehavior="true" />
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
                                                                <td>Sales Order Report</td>

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
                                                <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="2700px" AllowPaging="true" PageSize="20"
                                                    OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Dealer" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dealer Name" HeaderStyle-Width="200px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer" HeaderStyle-Width="62px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Customer")%>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Name" HeaderStyle-Width="200px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST No" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGSTNo" Text='<%# DataBinder.Eval(Container.DataItem, "GSTNo")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SO No" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuotationNo" Text='<%# DataBinder.Eval(Container.DataItem, "SONumber")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SO Dt" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuotationDate" Text='<%# DataBinder.Eval(Container.DataItem, "SODate","{0:d}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SO Status" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtSOStatus" Text='<%# DataBinder.Eval(Container.DataItem, "SOStatus")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice No" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Dt" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material" HeaderStyle-Width="156px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartNumber" Text='<%# DataBinder.Eval(Container.DataItem, "PartNumber")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HSN" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Width="89px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnitBasicPrice" Text='<%# DataBinder.Eval(Container.DataItem, "UnitBasicPrice","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="So Qty" HeaderStyle-Width="45px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value" HeaderStyle-Width="65px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "Value","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Discount" HeaderStyle-Width="65px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Discounted Price" HeaderStyle-Width="96px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiscountedPrice" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountedPrice","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Freight Amt" HeaderStyle-Width="76px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFreightAmount" Text='<%# DataBinder.Eval(Container.DataItem, "FreightAmount","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Taxable Amt" HeaderStyle-Width="77px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaxableAmount" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableAmount","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SGST" HeaderStyle-Width="37px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "SGST","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SGST Amt" HeaderStyle-Width="70px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSGSTAmt" Text='<%# DataBinder.Eval(Container.DataItem, "SGSTAmt","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CGST" HeaderStyle-Width="37px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "CGST","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CGST Amt" HeaderStyle-Width="70px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCGSTAmt" Text='<%# DataBinder.Eval(Container.DataItem, "CGSTAmt","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST" HeaderStyle-Width="37px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "IGST","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST Amt" HeaderStyle-Width="70px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIGSTAmt" Text='<%# DataBinder.Eval(Container.DataItem, "IGSTAmt","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Tax" HeaderStyle-Width="65px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTax" Text='<%# DataBinder.Eval(Container.DataItem, "Tax","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amt" HeaderStyle-Width="90px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalAmt" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmt","{0:n}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mat Type" HeaderStyle-Width="80px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMatType" Text='<%# DataBinder.Eval(Container.DataItem, "MatType")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Div" HeaderStyle-Width="55px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDivision" Text='<%# DataBinder.Eval(Container.DataItem, "Division")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location" HeaderStyle-Width="120px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" Text='<%# DataBinder.Eval(Container.DataItem, "Location")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contact Person" HeaderStyle-Width="120px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContactPerson" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contact No" HeaderStyle-Width="120px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContactNumber" Text='<%# DataBinder.Eval(Container.DataItem, "ContactNumber")%>' runat="server"></asp:Label>
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