<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyClaimInvoiceReport.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyClaimInvoiceReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            var gvTickets = document.getElementById('MainContent_gvClaimInvoice');
            if (gvTickets != null) {
                for (var i = 0; i < gvTickets.rows.length - 1; i++) { 
                    var lblGrandTotal = document.getElementById('MainContent_gvClaimInvoice_lblGrandTotal_' + i);
                    var lblSAPInvoiceValue = document.getElementById('MainContent_gvClaimInvoice_lblSAPInvoiceValue_' + i);
                    var lblSAPInvoiceTDSValue = document.getElementById('MainContent_gvClaimInvoice_lblSAPInvoiceTDSValue_' + i);

                    if (lblSAPInvoiceValue.innerHTML == "") {
                        continue;
                    }
                    var SAP = parseFloat(lblSAPInvoiceValue.innerHTML.replace(",","")) + parseFloat(lblSAPInvoiceTDSValue.innerHTML.replace(",",""));
                    var ValueDiff = parseFloat(lblGrandTotal.innerHTML.replace(",","")) - SAP;
                    if ((ValueDiff > 10) || (ValueDiff < -10)) {
                        lblGrandTotal.parentNode.parentNode.style.background = "#f4757f";
                    }
                }
            }
        });

    </script>
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
            <%--  <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <div style="width: 100%; height: 50px; background-color: #f2f2f2;">
                        <br />
                        <span style="font-family: Franklin Gothic Medium; color: black!important; font-size: 20px; margin-left: 9px;">Claim Request List</span>
                    </div>
                </div>
            </div>--%>
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
            <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <div class="boxHead">
                            <div class="logheading">Filter : Invoice Report</div>
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
                                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="Year"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="TextBox" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Approved Month"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="TextBox">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="01">Jan</asp:ListItem>
                                                    <asp:ListItem Value="02">Feb</asp:ListItem>
                                                    <asp:ListItem Value="03">Mar</asp:ListItem>
                                                    <asp:ListItem Value="04">Apr</asp:ListItem>
                                                    <asp:ListItem Value="05">May</asp:ListItem>
                                                    <asp:ListItem Value="06">Jun</asp:ListItem>
                                                    <asp:ListItem Value="07">Jul</asp:ListItem>
                                                    <asp:ListItem Value="08">Aug</asp:ListItem>
                                                    <asp:ListItem Value="09">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Month Range"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlMonthRange" runat="server" CssClass="TextBox">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="1">W1 (1st to 7th) </asp:ListItem>
                                                    <asp:ListItem Value="2">W2 (8th to 15th)</asp:ListItem>
                                                    <asp:ListItem Value="3">W3 (16th to 23rd) </asp:ListItem>
                                                    <asp:ListItem Value="4">W4 (24th to 31st)</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Invoice Type"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlInvoiceTypeID" runat="server" CssClass="TextBox">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="1">NEPI & Commission</asp:ListItem>
                                                    <asp:ListItem Value="2">Warranty Below 50K</asp:ListItem>
                                                    <asp:ListItem Value="3">Warranty Above 50K</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="Not Accounted"></asp:Label></td>
                                            <td>
                                                <asp:CheckBox ID="cbNotAccounted" runat="server" />
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="E-Invoice Generated"></asp:Label></td>
                                            <td>
                                                <asp:CheckBox ID="cbEInvoiceGenerated" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                                <%--<asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnGenerate_Click" />--%>
                                                <asp:Button ID="Button1" runat="server" Text="Export Excel for SAP" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcelForSAP_Click" />

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
                                                <td>Invoice Report</td>
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
                                <asp:GridView ID="gvClaimInvoice" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="WarrantyClaimInvoiceID" OnRowDataBound="gvICTickets_RowDataBound" CssClass="TableGrid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="javascript:collapseExpand('WarrantyClaimInvoiceID-<%# Eval("WarrantyClaimInvoiceID") %>');">
                                                    <img id="imageWarrantyClaimInvoiceID-<%# Eval("WarrantyClaimInvoiceID") %>" alt="Click to show/hide orders" border="0" src="Images/grid_expand.png" height="10" width="10" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Number">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWarrantyClaimInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyClaimInvoiceID")%>' runat="server" Visible="false" />
                                                <asp:Label ID="lblClaimID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Date">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblClaimDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealer">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dealer Name">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grand Total">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrandTotal" Text='<%# DataBinder.Eval(Container.DataItem, "GrandTotal","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TCS Tax%">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTCSTax" Text='<%# DataBinder.Eval(Container.DataItem, "TCSTax")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TCS Value ">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTCSValue" Text='<%# DataBinder.Eval(Container.DataItem, "TCSValue")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Annexure Number">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAnnexureNumber" Text='<%# DataBinder.Eval(Container.DataItem, "AnnexureNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SAP Doc">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSAPDoc" Text='<%# DataBinder.Eval(Container.DataItem, "SAPDoc")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AE Inv. Accounted Date"><%--SAP Posting Date--%>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSAPPostingDate" Text='<%# DataBinder.Eval(Container.DataItem, "SAPPostingDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Voucher. No"><%-- SAP Clearing Document --%>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSAPClearingDocument" Text='<%# DataBinder.Eval(Container.DataItem, "SAPClearingDocument")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Date"><%-- SAP Clearing Date--%>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSAPClearingDate" Text='<%# DataBinder.Eval(Container.DataItem, "SAPClearingDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Value"><%--SAP Invoice Value--%>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSAPInvoiceValue" Text='<%# DataBinder.Eval(Container.DataItem, "SAPInvoiceValue","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TDS Value"><%--SAP Invoice Value--%>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSAPInvoiceTDSValue" Text='<%# DataBinder.Eval(Container.DataItem, "SAPInvoiceTDSValue","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="PDF"><%--SAP Invoice Value--%>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                               <asp:ImageButton ID="ibPDF" runat="server" Width="20px" ImageUrl="~/FileFormat/Pdf_Icon.jpg" OnClick="ibPDF_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Generate E-Invoice">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate> 
                                                <asp:Button ID="btnGenerateEInvoice" runat="server" Text="Generate E-Invoice" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnGenerateEInvoice_Click" />
                                                 <tr>
                                                    <td colspan="100%" style="padding-left: 96px">
                                                        <div id="WarrantyClaimInvoiceID-<%# Eval("WarrantyClaimInvoiceID") %>" style="display: none; position: relative;">
                                                            <asp:GridView ID="gvClaimInvoiceItem" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Category">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWarrantyClaimItemID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyClaimInvoiceItemID")%>' runat="server" Visible="false" />
                                                                            <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "Category")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Material">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "Material")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Material Desc">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialDesc")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SAC/HSN Code">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "HSNCode")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="55px">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIRate" Text='<%# DataBinder.Eval(Container.DataItem, "Rate","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Value">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "ApprovedValue","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Discount">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Discount","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Taxable Value">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CGST %">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "CGST")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CGSTValue">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "CGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SGST %">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "SGST")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SGSTValue">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitOM" Text='<%# DataBinder.Eval(Container.DataItem, "SGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="IGST %">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "IGST")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="IGSTValue">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "IGSTValue","{0:n}")%>' runat="server"></asp:Label>
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