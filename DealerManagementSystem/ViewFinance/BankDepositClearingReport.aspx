<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="BankDepositClearingReport.aspx.cs" Inherits="DealerManagementSystem.ViewFinance.BankDepositClearingReport" %>
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
    <div class="col2">
        <div class="rf-p " id="txnHistory:j_idt1289">
            <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%" class="IC_basicInfo">
                    <tr>
                        <td>
                            <div class="boxHead">
                                <div class="logheading">Primary Purchase Order Information</div>
                                <div style="float: right; padding-top: 0px">
                                    <a href="javascript:collapseExpandCallInformation();">
                                        <img id="imgCallInformation" runat="server" alt="Click to show/hide orders" border="0" src="~/Images/grid_collapse.png" height="22" width="22" /></a>
                                </div>
                            </div>
                            <div class="rf-p " id="txnHistory1:inputFiltersPanel">
                                <div class="rf-p-b " id="txnHistory1:inputFiltersPanel_body">
                                    <table class="labeltxt fullWidth">
                                        <tr>
                                            <td>
                                                <div class="tbl-col">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label13" runat="server" Text="Code" CssClass="label"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="input" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="input"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Transaction Date Form"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtTransactionDateF" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTransactionDateF" PopupButtonID="txtTransactionDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtTransactionDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Transaction Date To"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtTransactionDateT" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTransactionDateT" PopupButtonID="txtTransactionDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtTransactionDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label3" runat="server" Text="Created By" CssClass="label"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="input" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Created Date From"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtCreatedDateF" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtCreatedDateF" PopupButtonID="txtCreatedDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtCreatedDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Created Date To"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtCreatedDateT" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtCreatedDateT" PopupButtonID="txtCreatedDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtCreatedDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label9" runat="server" Text="Accounted By" CssClass="label"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlAccountedBy" runat="server" CssClass="input" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label10" runat="server" CssClass="label" Text="Accounted Date From"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtAccountedDateF" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtAccountedDateF" PopupButtonID="txtAccountedDateF" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtAccountedDateF" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="Accounted Date To"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtAccountedDateT" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtAccountedDateT" PopupButtonID="txtAccountedDateT" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtAccountedDateT" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label6" runat="server" Text="Status" />
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label8" runat="server" Text="State" />
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="input" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="tbl-row-left">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label14" runat="server" Text="Region" />
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="input" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" />
                                </div>
                            </div>
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
                                                    <td>Web Quotation Report</td>
                                                    <td>
                                                        <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                    <td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                                    <asp:GridView ID="gvSo" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" AllowPaging="true" DataKeyNames="BankDepositClearingID" PageSize="20">
                                        <Columns>
                                             <asp:TemplateField HeaderText="ID">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankDepositClearingID" Text='<%# DataBinder.Eval(Container.DataItem, "BankDepositClearingID")%>' runat="server"></asp:Label> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDealerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerCode")%>' runat="server"></asp:Label>
                                                     <asp:Label ID="lblDealerName" Text='<%# DataBinder.Eval(Container.DataItem, "Dealer.DealerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="Bank Account">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAjaxBankAccount" Text='<%# DataBinder.Eval(Container.DataItem, "BankAccount")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transaction Date">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransactionDate" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionDate","{0:d}")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Value Date">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValueDate" Text='<%# DataBinder.Eval(Container.DataItem, "ValueDate","{0:d}")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank Description">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankDetail" Text='<%# DataBinder.Eval(Container.DataItem, "BankDescription")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BranchCode">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBranchCode" Text='<%# DataBinder.Eval(Container.DataItem, "BranchCode")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Is Multiple Customer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsMultipleCustomer" Text='<%# DataBinder.Eval(Container.DataItem, "IsMultipleCustomer")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerCode" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerCode")%>' runat="server"></asp:Label>
                                                      <asp:Label ID="lblCustomerName" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="Customer Name">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "Customer.CustomerName")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                            <asp:TemplateField HeaderText="DepositFor">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepositFor" Text='<%# DataBinder.Eval(Container.DataItem, "DepositFor")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Invoice Number">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceNumber" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNumber")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PO/SO Number">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPONumber" Text='<%# DataBinder.Eval(Container.DataItem, "PONumber")%>' runat="server"></asp:Label>
                                                     <asp:Label ID="lblSONumber" Text='<%# DataBinder.Eval(Container.DataItem, "SONumber")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="MachineModel">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMachineModel" Text='<%# DataBinder.Eval(Container.DataItem, "MachineModel")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" Text='<%# DataBinder.Eval(Container.DataItem, "Department")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Place">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlace" Text='<%# DataBinder.Eval(Container.DataItem, "Place")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" Text='<%# DataBinder.Eval(Container.DataItem, "State.State")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Region">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegion" Text='<%# DataBinder.Eval(Container.DataItem, "Region.Region")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Bill Detail Given By">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBillDetailGivenBy" Text='<%# DataBinder.Eval(Container.DataItem, "BillDetailGivenBy")%>' runat="server"></asp:Label>
                                                      <asp:Label ID="lblBillDetailUpdatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "BillDetailUpdatedOn")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                         <%--   <asp:TemplateField HeaderText="BillDetailUpdatedOn">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBillDetailUpdatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "BillDetailUpdatedOn")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

<%--                                            <asp:TemplateField HeaderText="Reference No">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReferenceNo" Text='<%# DataBinder.Eval(Container.DataItem, "ReferenceNo")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> --%>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "Status.Status")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Created">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedBy" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy.ContactName")%>' runat="server"></asp:Label>
                                                     <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <%--    <asp:TemplateField HeaderText="CreatedOn">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Accounted">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContactName" Text='<%# DataBinder.Eval(Container.DataItem, "AccountedBy.ContactName")%>' runat="server"></asp:Label>
                                                     <asp:Label ID="lblAccountedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AccountedOn")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="AccountedOn">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountedOn" Text='<%# DataBinder.Eval(Container.DataItem, "AccountedOn")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Sap Account No">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSapAccountNo" Text='<%# DataBinder.Eval(Container.DataItem, "SapAccountNo")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                         <%--   <asp:TemplateField HeaderText="Sap Posted On">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSapPostedOn" Text='<%# DataBinder.Eval(Container.DataItem, "SapPostedOn")%>' runat="server"></asp:Label>                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Sap Cleared On">
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSapClearedOn" Text='<%# DataBinder.Eval(Container.DataItem, "SapClearedOn")%>' runat="server"></asp:Label>
                                                   
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