<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyClaimDebitNoteAcknowledge.aspx.cs" Inherits="DealerManagementSystem.ServiceView.WarrantyClaimDebitNoteAcknowledge" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                            <div class="logheading">Filter : Debit Note Acknowledge</div>
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
                                                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="TextBox" Width="250px" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="Debit Note Number"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtDebitNoteNumber" runat="server" CssClass="input"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Debit Note Date From "></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtDebitNoteDateF" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDebitNoteDateF" PopupButtonID="txtDebitNoteDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDebitNoteDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </div>
                                                </div>
                                                <div class="tbl-row-right">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Debit Note Date To"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">

                                                        <asp:TextBox ID="txtDebitNoteDateT" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDebitNoteDateT" PopupButtonID="txtDebitNoteDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDebitNoteDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </div>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Invoice Number"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="input"></asp:TextBox>
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
            <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <span id="txnHistory1:refreshDataGroup">
                            <div class="boxHead">
                                <div class="logheading">
                                    <div style="float: left">
                                        <table>
                                            <tr>
                                                <td>Debit Note Acknowledge</td>
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
                                <asp:GridView ID="gvClaimInvoice" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="WarrantyClaimDebitNoteID" OnRowDataBound="gvICTickets_RowDataBound" CssClass="TableGrid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="javascript:collapseExpand('WarrantyClaimDebitNoteID-<%# Eval("WarrantyClaimDebitNoteID") %>');">
                                                    <img id="imageWarrantyClaimDebitNoteID-<%# Eval("WarrantyClaimDebitNoteID") %>" alt="Click to show/hide orders" border="0" src="Images/grid_expand.png" height="10" width="10" /></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Number">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWarrantyClaimDebitNoteID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyClaimDebitNoteID")%>' runat="server" Visible="false" />
                                                <asp:Label ID="lblClaimID" Text='<%# DataBinder.Eval(Container.DataItem, "DebitNoteNumber")%>' runat="server" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Date">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblClaimDate" Text='<%# DataBinder.Eval(Container.DataItem, "DebitNoteDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Number">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAnnexureNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
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
                                        <%-- <asp:TemplateField HeaderText="Invoice Type">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceType" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceType.InvoiceType")%>' runat="server"></asp:Label>
                                                <asp:Label ID="lblInvoiceTypeID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceType.InvoiceTypeID")%>' runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Grand Total">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "GrandTotal","{0:n}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Button ID="btnCreateDebitNote" runat="server" Text="Acknowledge" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnCreateDebitNote_Click" />
                                                <tr>
                                                    <td colspan="100%" style="padding-left: 96px">
                                                        <div id="WarrantyClaimDebitNoteID-<%# Eval("WarrantyClaimDebitNoteID") %>" style="display: none; position: relative;">
                                                            <asp:GridView ID="gvClaimInvoiceItem" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Material">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWarrantyClaimItemID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyClaimDebitNoteItemID")%>' runat="server" Visible="false" />

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
                                                                    <asp:TemplateField HeaderText="Debit Qty" Visible="false">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDebitQty" runat="server" CssClass="input" Width="40px" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--  <asp:TemplateField HeaderText="Rate">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIRate" Text='<%# DataBinder.Eval(Container.DataItem, "Rate","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                                    <asp:TemplateField HeaderText="Debit Value">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTaxableValue" Text='<%# DataBinder.Eval(Container.DataItem, "TaxableValue","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="CGST %">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCGST" Text='<%# DataBinder.Eval(Container.DataItem, "CGST")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CGSTValue">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "CGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SGST %">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSGST" Text='<%# DataBinder.Eval(Container.DataItem, "SGST")%>' runat="server"></asp:Label>
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
                                                                            <asp:Label ID="lblIGST" Text='<%# DataBinder.Eval(Container.DataItem, "IGST")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="IGSTValue">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIGSTValue" Text='<%# DataBinder.Eval(Container.DataItem, "IGSTValue","{0:n}")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRemark" Text='<%# DataBinder.Eval(Container.DataItem, "Remark" )%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Attachment">
                                                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkDownload" runat="server"  CommandName='<%# DataBinder.Eval(Container.DataItem, "WarrantyClaimDebitNoteItemID")%>' OnClick="lnkDownload_Click">
                                                                                <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                                            </asp:LinkButton>
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