<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyClaimDebitNoteAcknowledge.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyClaimDebitNoteAcknowledge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Code</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control"/>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Debit Note Number</label>
                        <asp:TextBox ID="txtDebitNoteNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Debit Note Date From</label>
                        <asp:TextBox ID="txtDebitNoteDateF" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDebitNoteDateF" PopupButtonID="txtDebitNoteDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDebitNoteDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Debit Note Date To</label>
                        <asp:TextBox ID="txtDebitNoteDateT" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDebitNoteDateT" PopupButtonID="txtDebitNoteDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDebitNoteDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Invoice Number</label>
                        <asp:TextBox ID="txtInvoiceNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-left">
                        <label class="modal-label">-</label>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Debit Note Acknowledge</legend>
                <div class="col-md-12 Report">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
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
                    <asp:GridView ID="gvClaimInvoice" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="WarrantyClaimDebitNoteID" OnRowDataBound="gvICTickets_RowDataBound" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
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
                                                <asp:GridView ID="gvClaimInvoiceItem" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed Grid" Width="100%">
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
                                                                <asp:LinkButton ID="lnkDownload" runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem, "WarrantyClaimDebitNoteItemID")%>' OnClick="lnkDownload_Click">
                                                                    <asp:Label ID="lblFileName" Text='<%# DataBinder.Eval(Container.DataItem, "FileName")%>' runat="server"></asp:Label>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <AlternatingRowStyle BackColor="#ffffff" />
                                                    <FooterStyle ForeColor="White" />
                                                    <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                    <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
