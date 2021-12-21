<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SaleOrderInvoiceReport.aspx.cs" Inherits="DealerManagementSystem.ViewSales.SaleOrderInvoiceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/UserControls/DMS_ICTicketBasicInformation.ascx" TagPrefix="UC" TagName="UC_BasicInformation" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMS/YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS/YDMS_Scripts.js"></script>

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

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <table id="txnHistory4:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead" style="height: 10px; background-color: #fbfbfb;"></div>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />

    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">Filter</div>
                    <div style="float: right; padding-top: 0px">
                        <%--  <a href="javascript:collapseExpand();">
                            <img id="Img1" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>--%>
                    </div>
                </div>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label1" runat="server" Text="Dealer Code"></asp:Label>
                                <asp:DropDownList ID="ddlDealerCode" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label5" runat="server" Text="Customer Code"></asp:Label>
                                <asp:TextBox ID="txtCustomerCode" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label2" runat="server" Text="Invoice Number"></asp:Label>
                                <asp:TextBox ID="txtInvoiceNumber" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Invoice Date From"></asp:Label>
                                <asp:TextBox ID="txtInvoiceDateFrom" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtInvoiceDateFrom" PopupButtonID="txtInvoiceDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtInvoiceDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Invoice Date To"></asp:Label>
                                <asp:TextBox ID="txtInvoiceDateTo" runat="server" CssClass="input" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtInvoiceDateTo" PopupButtonID="txtInvoiceDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtInvoiceDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label12" runat="server" CssClass="label" Text="Material"></asp:Label>
                                <asp:TextBox ID="txtMaterial" runat="server" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label3" runat="server" Text="Sales Type"></asp:Label>
                                <asp:DropDownList ID="ddlSalesType" runat="server">
                                    <asp:ListItem Value="0">Parts Sales</asp:ListItem>
                                    <asp:ListItem Value="1">MC Sales All</asp:ListItem>
                                    <asp:ListItem Value="2">MC Sales Quotation</asp:ListItem>
                                    <asp:ListItem Value="3">MC Sales Order</asp:ListItem>
                                    <asp:ListItem Value="4">Warranty</asp:ListItem>
                                    <asp:ListItem Value="5">Warranty Quotation</asp:ListItem>
                                    <asp:ListItem Value="6">Warranty Order</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Status (Use Shift or Ctrl key to select Multiple)"></asp:Label>
                                <asp:ListBox ID="lbStatus" runat="server" SelectionMode="Multiple" Height="130px">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1">DRAFT</asp:ListItem>
                                    <asp:ListItem Value="2">RELEASED</asp:ListItem>
                                    <asp:ListItem Value="3">INVALID</asp:ListItem>
                                    <asp:ListItem Value="4">SETTLED</asp:ListItem>
                                    <asp:ListItem Value="5">CLOSED</asp:ListItem>
                                    <asp:ListItem Value="6">CANCELLED</asp:ListItem>
                                    <asp:ListItem Value="7">PARTIAL_SETTLED</asp:ListItem>
                                </asp:ListBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbDetail" runat="server" GroupName="R" Text="Detail" Checked="true" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbDealerMaterial" runat="server" GroupName="R" Text="Dealer and Material Wise" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbDealerCustomerMaterial" runat="server" GroupName="R" Text="Dealer, Customer and Material Wise" /></td>
                                    </tr>
                                </table>



                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                &nbsp;
                                    <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <div class="col2">
        <div class="rf-p " id="txnHistory:j_idt1289">
            <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                    <tr>
                        <td>
                            <span id="txnHistory1:refreshDataGroup">
                                <div class="boxHead">
                                    <div class="logheading">
                                        <div style="float: left">
                                            <table>
                                                <tr>
                                                    <td>IC Ticket TSIR Report</td>
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
                                <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">

                                    <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="2700px" AllowPaging="true" PageSize="20"
                                        OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL. No">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceID")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                                    <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name" HeaderStyle-Width="200px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GST No" HeaderStyle-Width="75px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGSTNo" Text='<%# DataBinder.Eval(Container.DataItem, "GSTNo")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--    <asp:TemplateField HeaderText="SO No" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuotationNo" Text='<%# DataBinder.Eval(Container.DataItem, "SONumber")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                            <%--   <asp:TemplateField HeaderText="SO Dt" HeaderStyle-Width="75px">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuotationDate" Text='<%# DataBinder.Eval(Container.DataItem, "SODate","{0:d}")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="75px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSOStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' runat="server"></asp:Label>
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
                                                    <asp:Label ID="lblPartNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HSN" HeaderStyle-Width="75px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.HSN")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Width="89px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnitBasicPrice" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.UnitBasicPrice","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="45px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Qty","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value" HeaderStyle-Width="65px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValue" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Value","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount" HeaderStyle-Width="65px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiscount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Discount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discounted Price" HeaderStyle-Width="96px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiscountedPrice" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.DiscountedPrice","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Freight Amt" HeaderStyle-Width="76px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFreightAmount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.FreightAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Taxable Amt" HeaderStyle-Width="77px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaxableAmount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.TaxableAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST" HeaderStyle-Width="37px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.SGST","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SGST Amt" HeaderStyle-Width="70px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSGSTAmt" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.SGSTAmt","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST" HeaderStyle-Width="37px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.CGST","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CGST Amt" HeaderStyle-Width="70px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCGSTAmt" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.CGSTAmt","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST" HeaderStyle-Width="37px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.IGST","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IGST Amt" HeaderStyle-Width="70px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIGSTAmt" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.IGSTAmt","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tax" HeaderStyle-Width="65px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTax" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Tax","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amt" HeaderStyle-Width="90px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmt" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.TotalAmt","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mat Type" HeaderStyle-Width="80px">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMatType" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialType")%>' runat="server"></asp:Label>
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
                                <div style="background-color: white">
                                    <asp:GridView ID="gvDM" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" AllowPaging="true" PageSize="20"
                                        OnPageIndexChanging="gvDM_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL. No">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceID")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Qty","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Gross Amount">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmt" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.GrossAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Net Amount">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.NetAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Header Count" Visible="false">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHeaderCount" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderCount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Count">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.ItemCount")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="background-color: white">
                                    <asp:GridView ID="gvDCM" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%" AllowPaging="true" PageSize="20"
                                        OnPageIndexChanging="gvDCM_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL. No">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceID")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dealer Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Qty","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Amount">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmt" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.GrossAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amount">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetAmount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.NetAmount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Header Count" Visible="false">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHeaderCount" Text='<%# DataBinder.Eval(Container.DataItem, "HeaderCount","{0:n}")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Count">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.ItemCount")%>' runat="server"></asp:Label>
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
</asp:Content>
