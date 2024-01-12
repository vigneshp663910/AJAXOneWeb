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

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Customer Code</label>
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Invoice Number</label>
                        <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Invoice Date From</label>
                        <asp:TextBox ID="txtInvoiceDateFrom" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInvoiceDateFrom" PopupButtonID="txtInvoiceDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtInvoiceDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Invoice Date To</label>
                        <asp:TextBox ID="txtInvoiceDateTo" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtInvoiceDateTo" PopupButtonID="txtInvoiceDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtInvoiceDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                        <%--<asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnGenerate_Click" />--%>
                        <asp:Button ID="Button1" runat="server" Text="Export Excel for SAP" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcelForSAP_Click" Visible="false" />

                    </div>
                </div>
            </fieldset>

            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="boxHead">
                            <div class="logheading">
                                <div style="float: left">
                                    <table>
                                        <tr>
                                            <td>Invoice(s):</td>
                                            <td>
                                                <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvInv" runat="server" AutoGenerateColumns="false" Width="100%" OnRowDataBound="gvInv_RowDataBound" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvInv_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Generat E-Invoice">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%-- <asp:CheckBox ID="cbChecked" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Checked")%>' />--%>
                                        <asp:Button ID="btnGenerateInvoice" runat="server" Text="Generate E-Invoice" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnGenerateInvoice_Click" Width="150px" Height="25px" />
                                        <asp:Label ID="lblInvType" Text='<%# DataBinder.Eval(Container.DataItem, "InvType")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="javascript:collapseExpand('EInvoiceID-<%# Eval("EInvoice.DocDtls.No") %>');">
                                            <img id="imageEInvoiceID-<%# Eval("EInvoice.DocDtls.No") %>" alt="Click to show/hide orders" border="0" src="Images/grid_expand.png" height="10" width="10" /></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tax Scheme">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTax_Scheme" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.TranDtls.TaxSch")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Document Category">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocumentCategory" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.TranDtls.SupTyp","{0:d}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Document Type">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocumentType" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.DocDtls.Typ")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Billing Document">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBillingDocument" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.DocDtls.No")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.DocDtls.Dt","{0:n}")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier GSTIN">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierGSTIN" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.SellerDtls.Gstin")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Trade Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierTrade_Name" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.SellerDtls.LglNm")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier addr1">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplier_addr1" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.SellerDtls.Addr1")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Location">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierLocation" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.SellerDtls.Loc")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Pincode">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierPincode" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.SellerDtls.Pin")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier State Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierStateCode" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.SellerDtls.Stcd")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buyer GSTIN">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuyerGSTIN" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Gstin")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtBuyerGSTIN" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Gstin")%>' Visible="false" Width="200px" Height="30px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buyer Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuyerName" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.LglNm")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buyer State Code">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuyerStateCode" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Stcd")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtBuyerStateCode" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Stcd")%>' Visible="false" Width="200px" Height="30px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buyer Addr1">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuyer_addr1" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Addr1")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtBuyer_addr1" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Addr1")%>' Visible="false" Width="200px" Height="30px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buyer Loc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuyer_loc" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Loc")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtBuyer_loc" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Loc")%>' Visible="false" Width="200px" Height="30px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buyer Pincode">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuyerPincode" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Pin")%>' runat="server"></asp:Label>
                                        <asp:TextBox ID="txtBuyerPincode" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.BuyerDtls.Pin")%>' Visible="false" Width="200px" Height="30px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disp Sup Trade Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbldisp_sup_trade_Name" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.DispDtls.Nm")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disp Sup Addr1">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbldisp_sup_addr1" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.DispDtls.Addr1")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disp Sup Loc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbldisp_sup_loc" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.DispDtls.Loc")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disp Sup Pin">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbldisp_sup_pin" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.DispDtls.Pin")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disp Sup Stcd">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lbldisp_sup_stcd" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.DispDtls.Stcd")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="TOTALLINE ITEMS">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTOTALLINEITEMS" Text='<%# DataBinder.Eval(Container.DataItem, "TOTALLINEITEMS")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                <%--                                        <asp:TemplateField HeaderText="Accumulated Total Amount">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccumulatedTotalAmount" Text='<%# DataBinder.Eval(Container.DataItem, "ValDtls.AssVal")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Accumulated Ass Total Amount">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccumulatedAssTotalAmount" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.AssVal")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accumulated Sgst Val">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccumulatedSgstVal" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.SgstVal")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accumulated Igst Val">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccumulatedIgstVal" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.IgstVal")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accumulated Cgst Val">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccumulatedCgstVal" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.CgstVal")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accumulated Ces Val">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccumulatedCesVal" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.CesVal")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accumulated St Ces Val">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccumulatedStCesVal" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.StCesVal")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accumulated Discount">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.Discount")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accumulated OthChrg">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotOthChrg" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.OthChrg")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RndOffAmt">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRndOffAmt" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.RndOffAmt")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TotInvVal">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotInvVal" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.TotInvVal")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TotInvValFc">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotInvValFc" Text='<%# DataBinder.Eval(Container.DataItem, "EInvoice.ValDtls.TotInvValFc")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="Accumulated Tot Item Val">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccumulatedTotItemVal" Text='<%# DataBinder.Eval(Container.DataItem, "ValDtls.AccumulatedTotItemVal")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <%-- <asp:TemplateField HeaderText="IRN">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIRN" Text='<%# DataBinder.Eval(Container.DataItem, "IRN")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <%-- <asp:TemplateField HeaderText="Reason For Cancellation">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblReasonForCancellation" Text='<%# DataBinder.Eval(Container.DataItem, "ReasonForCancellation")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <%-- <asp:TemplateField HeaderText="Cancellation Comment">
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
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="false" OnClick="btnEdit_Click" CssClass="btn Search" Width="75px" Height="25px" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="btnUpdate_Click" CssClass="btn Search" Width="75px" Height="25px" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" OnClick="btnCancel_Click" CssClass="btn Search" Width="75px" Height="25px" />
                                        <tr>
                                            <td colspan="100%" style="padding-left: 96px">
                                                <div id="EInvoiceID-<%# Eval("EInvoice.DocDtls.No") %>" style="display: block; position: relative;">
                                                    <asp:GridView ID="gvInvItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SlNo">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSlNo" Text='<%# DataBinder.Eval(Container.DataItem, "SlNo")%>' runat="server"></asp:Label>
                                                                    <%--  <asp:Label ID="lblInvoiceItemID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItemID")%>' runat="server"  Visible="false"></asp:Label> 
                                                                    <asp:Label ID="lblBillingDocument" Text='<%# DataBinder.Eval(Container.DataItem, "BillingDocument")%>' runat="server" Visible="false"></asp:Label>--%>
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
                                                                    <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HsnCd")%>' runat="server"></asp:Label>
                                                                    <asp:TextBox ID="txtHSNCode" runat="server" CssClass="input" Text='<%# DataBinder.Eval(Container.DataItem, "HsnCd")%>' Visible="false" Width="200px" Height="30px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="55px">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UnitOfMeasure">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitOfMeasure" Text='<%# DataBinder.Eval(Container.DataItem, "Unit","{0:n}")%>' runat="server"></asp:Label>
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
                                                                    <asp:Label ID="lblTotalAmount" Text='<%# DataBinder.Eval(Container.DataItem, "TotAmt","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discount">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AssesseebleAmount">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAssesseebleAmount" Text='<%# DataBinder.Eval(Container.DataItem, "AssAmt")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TaxRate">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTaxRate" Text='<%# DataBinder.Eval(Container.DataItem, "GstRt","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="IGSTAmount">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIGSTAmount" Text='<%# DataBinder.Eval(Container.DataItem, "IgstAmt","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CGSTAmount">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCGSTAmount" Text='<%# DataBinder.Eval(Container.DataItem, "CgstAmt")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SGSTAmount">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSGSTAmount" Text='<%# DataBinder.Eval(Container.DataItem, "SgstAmt")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="CESSRate">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCESSRate" Text='<%# DataBinder.Eval(Container.DataItem, "CesRt","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CESSAmount">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCESSAmount" Text='<%# DataBinder.Eval(Container.DataItem, "CesAmt")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CesNonAdvlAmt">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCesNonAdvlAmt" Text='<%# DataBinder.Eval(Container.DataItem, "CesNonAdvlAmt")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="StateCesRt">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStateCesRt" Text='<%# DataBinder.Eval(Container.DataItem, "StateCesRt")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="StateCesAmt">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStateCesAmt" Text='<%# DataBinder.Eval(Container.DataItem, "StateCesAmt")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="StateCesNonAdvlAmt">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStateCesNonAdvlAmt" Text='<%# DataBinder.Eval(Container.DataItem, "StateCesNonAdvlAmt")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="OthChrg">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOthChrg" Text='<%# DataBinder.Eval(Container.DataItem, "OthChrg")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TotalItem Value">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalItemValue" Text='<%# DataBinder.Eval(Container.DataItem, "TotItemVal")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnEditItem" runat="server" Text="Edit" Visible="false" OnClick="btnEditItem_Click" CssClass="btn Search" Width="75px" Height="25px" />
                                                                    <asp:Button ID="btnUpdateItem" runat="server" Text="Update" Visible="false" OnClick="btnUpdateItem_Click" CssClass="btn Search" Width="75px" Height="25px" />
                                                                    <asp:Button ID="btnCancelItem" runat="server" Text="Cancel" Visible="false" OnClick="btnCancelItem_Click" CssClass="btn Search" Width="75px" Height="25px" />
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
                    </fieldset>
                </div>
            </div>


        </div>
    </div>
</asp:Content>
