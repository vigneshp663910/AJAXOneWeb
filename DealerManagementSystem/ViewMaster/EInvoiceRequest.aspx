<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="EInvoiceRequest.aspx.cs" Inherits="DealerManagementSystem.ViewMaster.EInvoiceRequest" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById(obj);
            var imageID = document.getElementById('image' + obj);

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }

        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
    <div class="container">
        <div class="col2">

            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
            <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <div class="boxHead">
                            <div class="logheading">Invoice</div>
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
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Code"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox" Width="250px" />
                                                    </div>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer Code"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="input"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="Invoice Number"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="input"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Invoice Date From "></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtInvoiceDateFrom" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInvoiceDateFrom" PopupButtonID="txtInvoiceDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtInvoiceDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </div>
                                                </div>
                                                <div class="tbl-row-right">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Invoice Date To"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">

                                                        <asp:TextBox ID="txtInvoiceDateTo" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtInvoiceDateTo" PopupButtonID="txtInvoiceDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtInvoiceDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </div>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                                <%--<asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnGenerate_Click" />--%>
                                                <asp:Button ID="Button1" runat="server" Text="Export Excel for SAP" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcelForSAP_Click" Visible="false" />

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
                                                <td>Invoice</td>
                                                <td>
                                                    <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div style="background-color: white">
                                <asp:GridView ID="gvClaimInvoice" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="EInvoiceID" OnRowDataBound="gvICTickets_RowDataBound" CssClass="TableGrid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Generat E-Invoice">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%-- <asp:CheckBox ID="cbChecked" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Checked")%>' />--%>
                                                <asp:Button ID="btnGenerateInvoice" runat="server" Text="Generate E-Invoice" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnGenerateInvoice_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="javascript:collapseExpand('EInvoiceID-<%# Eval("EInvoiceID") %>');">
                                                    <img id="imageEInvoiceID-<%# Eval("EInvoiceID") %>" alt="Click to show/hide orders" border="0" src="Images/grid_expand.png" height="10" width="10" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax Scheme">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTax_Scheme" Text='<%# DataBinder.Eval(Container.DataItem, "Tax_Scheme")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document Category">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentCategory" Text='<%# DataBinder.Eval(Container.DataItem, "DocumentCategory","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Document Type">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentType" Text='<%# DataBinder.Eval(Container.DataItem, "DocumentType")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Billing Document">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillingDocument" Text='<%# DataBinder.Eval(Container.DataItem, "BillingDocument")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Date">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier GSTIN">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierGSTIN" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierGSTIN")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Trade Name">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierTrade_Name" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierTrade_Name")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier addr1">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplier_addr1" Text='<%# DataBinder.Eval(Container.DataItem, "Supplier_addr1")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Location">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierLocation" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierLocation")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Pincode">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierPincode" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierPincode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier State Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierStateCode" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierStateCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer GSTIN">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBuyerGSTIN" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerGSTIN")%>' runat="server"></asp:Label>
                                                <asp:TextBox ID="txtBuyerGSTIN" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerGSTIN")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer Name">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBuyerName" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerName")%>' runat="server"></asp:Label> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer State Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBuyerStateCode" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerStateCode")%>' runat="server"></asp:Label>
                                                <asp:TextBox ID="txtBuyerStateCode" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerStateCode")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer Addr1">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBuyer_addr1" Text='<%# DataBinder.Eval(Container.DataItem, "Buyer_addr1")%>' runat="server"></asp:Label>
                                                <asp:TextBox ID="txtBuyer_addr1" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "Buyer_addr1")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer Loc">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBuyer_loc" Text='<%# DataBinder.Eval(Container.DataItem, "Buyer_loc")%>' runat="server"></asp:Label>
                                                <asp:TextBox ID="txtBuyer_loc" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "Buyer_loc")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer Pincode">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBuyerPincode" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerPincode")%>' runat="server"></asp:Label>
                                                <asp:TextBox ID="txtBuyerPincode" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerPincode")%>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Disp Sup Trade Name">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbldisp_sup_trade_Name" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierTrade_Name")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Disp Sup Addr1">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbldisp_sup_addr1" Text='<%# DataBinder.Eval(Container.DataItem, "Supplier_addr1")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Disp Sup Loc">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbldisp_sup_loc" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierLocation")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Disp Sup Pin">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbldisp_sup_pin" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierPincode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Disp Sup Stcd">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbldisp_sup_stcd" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierStateCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTALLINE ITEMS">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTOTALLINEITEMS" Text='<%# DataBinder.Eval(Container.DataItem, "TOTALLINEITEMS")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Accumulated Total Amount">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccumulatedTotalAmount" Text='<%# DataBinder.Eval(Container.DataItem, "AccumulatedTotalAmount")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accumulated Ass Total Amount">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccumulatedAssTotalAmount" Text='<%# DataBinder.Eval(Container.DataItem, "AccumulatedAssTotalAmount")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accumulated Sgst Val">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccumulatedSgstVal" Text='<%# DataBinder.Eval(Container.DataItem, "AccumulatedSgstVal")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accumulated Igst Val">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccumulatedIgstVal" Text='<%# DataBinder.Eval(Container.DataItem, "AccumulatedIgstVal")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accumulated Cgst Val">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccumulatedCgstVal" Text='<%# DataBinder.Eval(Container.DataItem, "AccumulatedCgstVal")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accumulated Ces Val">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccumulatedCesVal" Text='<%# DataBinder.Eval(Container.DataItem, "AccumulatedCesVal")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accumulated Tot Item Val">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccumulatedTotItemVal" Text='<%# DataBinder.Eval(Container.DataItem, "AccumulatedTotItemVal")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IRN">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIRN" Text='<%# DataBinder.Eval(Container.DataItem, "IRN")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason For Cancellation">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblReasonForCancellation" Text='<%# DataBinder.Eval(Container.DataItem, "ReasonForCancellation")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancellation Comment">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCancellationComment" Text='<%# DataBinder.Eval(Container.DataItem, "CancellationComment")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" Text='<%# DataBinder.Eval(Container.DataItem, "Type")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="false" OnClick="btnEdit_Click" />
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" OnClick="btnCancel_Click" />
                                                <tr>
                                                    <td colspan="100%" style="padding-left: 96px">
                                                        <div id="EInvoiceID-<%# Eval("EInvoiceID") %>" style="display: none; position: relative;">
                                                            <asp:GridView ID="gvClaimInvoiceItem" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SlNo">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSlNo" Text='<%# DataBinder.Eval(Container.DataItem, "SlNo")%>' runat="server"></asp:Label>
                                                                             <asp:Label ID="lblInvoiceItemID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItemID")%>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblBillingDocument" Text='<%# DataBinder.Eval(Container.DataItem, "BillingDocument")%>' runat="server" Visible="false"></asp:Label>
                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PrdDesc">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPrdDesc" Text='<%# DataBinder.Eval(Container.DataItem, "PrdDesc")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="IsServc">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIsServc" Text='<%# DataBinder.Eval(Container.DataItem, "IsServc")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="HSNCode">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' runat="server"></asp:Label>
                                                                            <asp:TextBox ID="txtHSNCode" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' Visible="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="55px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UnitOfMeasure">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitOfMeasure" Text='<%# DataBinder.Eval(Container.DataItem, "UnitOfMeasure","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UnitPrice">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitPrice" Text='<%# DataBinder.Eval(Container.DataItem, "UnitPrice","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Amount">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotalAmount" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="AssesseebleAmount">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAssesseebleAmount" Text='<%# DataBinder.Eval(Container.DataItem, "AssesseebleAmount")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="TaxRate">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTaxRate" Text='<%# DataBinder.Eval(Container.DataItem, "TaxRate","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SGSTAmount">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSGSTAmount" Text='<%# DataBinder.Eval(Container.DataItem, "SGSTAmount")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="IGSTAmount">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIGSTAmount" Text='<%# DataBinder.Eval(Container.DataItem, "IGSTAmount","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CGSTAmount">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCGSTAmount" Text='<%# DataBinder.Eval(Container.DataItem, "CGSTAmount")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CESSRate">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCESSRate" Text='<%# DataBinder.Eval(Container.DataItem, "CESSRate","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CESSAmount">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCESSAmount" Text='<%# DataBinder.Eval(Container.DataItem, "CESSAmount")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="TotalItem Value">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotalItemValue" Text='<%# DataBinder.Eval(Container.DataItem, "TotalItemValue")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="Edit">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnEditItem" runat="server" Text="Edit" Visible="false" OnClick="btnEditItem_Click" />
                                                                            <asp:Button ID="btnUpdateItem" runat="server" Text="Update" Visible="false" OnClick="btnUpdateItem_Click" />
                                                                            <asp:Button ID="btnCancelItem" runat="server" Text="Cancel" Visible="false" OnClick="btnCancelItem_Click" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
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