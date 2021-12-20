<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="WarrantyFailureMaterialDCTemplateCreation.aspx.cs" Inherits="DealerManagementSystem.ViewService.WarrantyFailureMaterialDCTemplateCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
       
    </script>
    <div class="container">
        <div class="col2">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
            <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <div class="boxHead">
                            <div class="logheading">Filter : Failed Material Draft </div>
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
                                                <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox" Width="250px"  OnSelectedIndexChanged="ddlDealerCode_SelectedIndexChanged" AutoPostBack="true"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="IC Service Ticket :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtICServiceTicket" runat="server" CssClass="input"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="IC Login Date From :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtICLoginDateFrom" runat="server" CssClass="hasDatepicker" AutoComplete="Off"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </td>
                                            <td style="width: 20px"></td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" CssClass="label" Text="IC Login Date To"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtICLoginDateTo" runat="server" CssClass="hasDatepicker" AutoComplete="Off"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="Claim ID :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtClaimID" runat="server" CssClass="input"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Claim Date From :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtClaimDateF" runat="server" CssClass="hasDatepicker" AutoComplete="Off"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtClaimDateF" PopupButtonID="txtClaimDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtClaimDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </td>
                                            <td style="width: 20px"></td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Claim Date To"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtClaimDateT" runat="server" CssClass="hasDatepicker" AutoComplete="Off"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtClaimDateT" PopupButtonID="txtClaimDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtClaimDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="TSIR Number :"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtTSIRNumber" runat="server" CssClass="input"></asp:TextBox>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Status"></asp:Label></td>
                                            <td>

                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="TextBox" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />

                                                <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" Visible="false" />
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
                                                <td>Failed Material Draft</td>
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
                                <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="WarrantyInvoiceHeaderID"
                                    OnRowDataBound="gvICTickets_RowDataBound" CssClass="TableGrid" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbSelectedMaterial" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Claim" HeaderStyle-Width="62px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWarrantyInvoiceHeaderID" Text='<%# DataBinder.Eval(Container.DataItem, "WarrantyInvoiceHeaderID")%>' runat="server" Visible="false" />
                                                <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Claim Dt">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceDate" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IC Ticket" HeaderStyle-Width="75px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblICTicketID" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketID")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IC Ticket Dt" HeaderStyle-Width="75px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblICTicketDate" Text='<%# DataBinder.Eval(Container.DataItem, "ICTicketDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Restore Dt" HeaderStyle-Width="75px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRestoreDate" Text='<%# DataBinder.Eval(Container.DataItem, "RestoreDate","{0:d}")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust. Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust. Name">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="HMR">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblHMR" Text='<%# DataBinder.Eval(Container.DataItem, "HMR")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin Warranty">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMarginWarranty" Text='<%# DataBinder.Eval(Container.DataItem, "MarginWarranty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblModel" Text='<%# DataBinder.Eval(Container.DataItem, "Model")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MC Serial No">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMachineSerialNumber" Text='<%# DataBinder.Eval(Container.DataItem, "MachineSerialNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FSR No">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFSRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "FSRNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TSIR No">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTSIRNumber" Text='<%# DataBinder.Eval(Container.DataItem, "TSIRNumber")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-Width="55px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "ClaimStatus")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SAC / HSN Code">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWarrantyInvoiceItemID" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.WarrantyInvoiceItemID")%>' runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblHSNCode" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.HSNCode")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material" HeaderStyle-Width="78px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterial" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Material")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Desc" HeaderStyle-Width="150px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialDesc" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.MaterialDesc")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="40px">
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceItem.Qty")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                    </Columns>
                                    <AlternatingRowStyle BackColor="#BFE4FF" ForeColor="Black" />
                                    <HeaderStyle BackColor="#BFE4FF" Font-Bold="True" ForeColor="Black" />
                                    <RowStyle ForeColor="Black" BackColor="#bfe4ff" />
                                </asp:GridView>
                            </div>
                            <table class="labeltxt">
                                <tr>
                                    <td>

                                        <div class="tbl-col">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="Label9" runat="server" CssClass="label" Text="DC Draft"></asp:Label>

                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:DropDownList ID="ddlDCTemplate" runat="server" CssClass="TextBox" Width="250px" OnSelectedIndexChanged="ddlDCTemplate_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="tbl-col">
                                            <div class="tbl-col-left">
                                                <asp:Label ID="lblTemplateName" runat="server" CssClass="label" Text="Draft Remarks"></asp:Label>
                                            </div>
                                            <div class="tbl-col-right">
                                                <asp:TextBox ID="txtTemplateName" runat="server" CssClass="input"></asp:TextBox>
                                            </div>
                                        </div>
                                </tr>
                                <tr>
                                    <td colspan="4" align="right">
                                        <asp:Button ID="btnCreateTemplate" runat="server" Text="Create" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnCreateTemplate_Click" />
                                    </td>
                                </tr>
                            </table>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>