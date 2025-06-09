<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SalesCommissionClaimInvoiceVerify.aspx.cs" Inherits="DealerManagementSystem.ViewSales.SalesCommissionClaimInvoiceVerify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ViewSales/UserControls/SalesCommissionClaimInvoiceView.ascx" TagPrefix="UC" TagName="UC_ClaimInvoiceView" %>
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
                    var SAP = parseFloat(lblSAPInvoiceValue.innerHTML.replace(",", "")) + parseFloat(lblSAPInvoiceTDSValue.innerHTML.replace(",", ""));
                    var ValueDiff = parseFloat(lblGrandTotal.innerHTML.replace(",", "")) - SAP;
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
                imageID.src = "../Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "../Images/grid_expand.png";
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
            <div class="col-md-12">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                    <div class="col-md-12">
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Dealer</label>
                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Invoice Number</label>
                            <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date From</label>
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateFrom" WatermarkText="Date From"></asp:TextBoxWatermarkExtender>
                        </div>

                        <div class="col-md-2 col-sm-12">
                            <label class="modal-label">Date To</label>
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtDateTo" WatermarkText="Date To"></asp:TextBoxWatermarkExtender>
                        </div>

                        <div class="col-md-12 text-center">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="btnSearch_Click"></asp:Button>
                            <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Width="100px" />

                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-12">
                <div class="col-md-12 Report">
                    <fieldset class="fieldset-border">
                        <legend style="background: none; color: #007bff; font-size: 17px;">List</legend>
                        <div class="col-md-12 Report">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Invoice(s):</td>

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
                            <asp:GridView ID="gvClaimInvoice" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="SalesCommissionClaimInvoiceID" OnRowDataBound="gvICTickets_RowDataBound" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="javascript:collapseExpand('SalesCommissionClaimInvoiceID-<%# Eval("SalesCommissionClaimInvoiceID") %>');">
                                                <img id="imageSalesCommissionClaimInvoiceID-<%# Eval("SalesCommissionClaimInvoiceID") %>" alt="Click to show/hide orders" border="0" src="../Images/grid_expand.png" height="10" width="10" /></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Number">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalesCommissionClaimInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "SalesCommissionClaimInvoiceID")%>' runat="server" Visible="false" />
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
                                        <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnClainInvoiceView" runat="server" Text="View" CssClass="btn Back" OnClick="btnViewClaimInvoice_Click" Width="50px" Height="33px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="Verify">
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnVerify" runat="server" CssClass="btn Search" Text="Release" OnClick="btnVerify_Click" Width="100px" Height="30px"></asp:Button>
                                            <tr>
                                                <td colspan="100%" style="padding-left: 96px">
                                                    <div id="SalesCommissionClaimInvoiceID-<%# Eval("SalesCommissionClaimInvoiceID") %>" style="display: none; position: relative;">
                                                        <asp:GridView ID="gvClaimInvoiceItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Material">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSalesCommissionClaimItemInvoiceID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.SalesCommissionClaimInvoiceItemID")%>' runat="server" Visible="false" />

                                                                        <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialCode")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material Desc">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.MaterialDescription")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SAC/HSN Code">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material.HSN")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="55px">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Qty")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIRate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Rate","{0:n}")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--     <asp:TemplateField HeaderText="Value">
                                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.ApprovedValue","{0:n}")%>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="Taxable Value">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CGST %">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.CGST")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="CGSTValue">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.CGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SGST %">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.SGST")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SGSTValue">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnitOM" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.SGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="IGST %">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCategory" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.IGST")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="IGSTValue">
                                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.IGSTValue","{0:n}")%>' runat="server"></asp:Label>
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
                                                </td>
                                            </tr>
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
                    </fieldset>
                </div>

            </div>
        </div>
        <div class="col-md-12" id="divClaimInvoiceView" runat="server" visible="false">
            <div class="col-md-12 lead-back-btn">
                <div class="" id="boxHere"></div>
                <div class="back-buttton" id="backBtn">
                    <asp:Button ID="btnBackToList" runat="server" Text="Back" CssClass="btn Back" OnClick="btnBackToList_Click" />
                </div>
            </div>
            <UC:UC_ClaimInvoiceView ID="UC_ClaimInvoiceView" runat="server"></UC:UC_ClaimInvoiceView>
        </div>
    </div>

</asp:Content>
